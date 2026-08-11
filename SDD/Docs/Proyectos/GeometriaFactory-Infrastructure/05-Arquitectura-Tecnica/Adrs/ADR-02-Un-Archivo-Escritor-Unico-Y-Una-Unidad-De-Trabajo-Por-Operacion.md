# ADR-02 — Archivo único, escritor único y una unidad de trabajo por operación

**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** ADR-02-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Persistencia

---

## 1. Contexto

El intake declara la persistencia «la responsabilidad central del proyecto de código» y fija nueve decisiones de almacenamiento: motor de archivo único, ubicación configurable en un volumen persistente, modo de diario con registro por delante, **escritor único**, una unidad de trabajo por operación, transformaciones de esquema aplicadas al arrancar, el texto del alumno guardado como texto y no consultado por su contenido, una instancia por despliegue y respaldo por copia del archivo (`PRODUCT-INTAKE` §17.3.P.4).

Lo que esas nueve decisiones **no** resuelven es lo que esta ADR tiene que decidir: qué hace el adaptador cuando el almacén está tomado por otra escritura, cómo se garantiza que la baja no deje trabajos huérfanos, y con qué zona horaria y con qué precisión se guardan los tres tiempos que el modelo distingue. Los tres huecos tienen la misma forma: son decisiones de **mecanismo** sobre un alcance que la capa de aplicación ya fijó.

Motivación upstream: NB-01, NB-03, NB-09; RN-04, RN-07, RN-08, RN-12, RN-13; INV-07, INV-09; `RC-01`, `RC-05`, `RC-06`, `RC-07`; `PRODUCT-INTAKE` §17.3.P.4, §17.3.P.12.

## 2. Decisión

**Una unidad de trabajo por operación, con el todo o nada como propiedad exigible, y ninguna anidada.** El alcance lo fija `GeometriaFactory-Application` —un caso de uso, una unidad de trabajo— y acá se materializa el mecanismo. Cuatro concreciones:

1. **El arrastre de la baja ocurre dentro de la misma unidad que el retiro de la cuenta.** O se retira la cuenta con todos sus trabajos, o no se retira nada. Es la propiedad entera del contrato de retiro y su caso testigo.
2. **La escritura que llega con el almacén tomado no espera: termina.** El adaptador emite `ESCRITURA_CONCURRENTE_RECHAZADA` como terminación degradada, **y esta capa no reintenta**. Reintentar es del consumidor, que es el que sabe si la operación es repetible.
3. **El texto original se escribe una sola vez.** Toda materialización que aporte, para un trabajo existente, un texto distinto del conservado se rechaza con `ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL` (`RN-08`, `RC-01`). La reedición cambia los datos del trabajo y el texto que la persona **vuelve a pegar**, nunca el ya guardado.
4. **Los tres tiempos del trabajo se guardan en tiempo universal coordinado**, con la precisión que el puerto de reloj entrega y **sin truncarla**. La `Fecha` que el alumno escribe se guarda tal como la escribió y **no se convierte**, porque no es un sello. La conversión a la zona de quien lee es de la superficie que lo muestra. Es la decisión que cierra el punto abierto de zona horaria y precisión que la categoría 02 derivó acá.

**Sin borrado lógico y sin marca de baja.** El retiro es físico, y por eso el arrastre es verificable comprobando que **no queda nada**.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Una unidad por operación, sin reintento, con retiro físico (**adoptada**) | El todo o nada es exigible con una prueba; la limitación de escritor único queda visible en lugar de escondida; el arrastre se verifica por ausencia | Traslada al consumidor la decisión de reintentar, que es trabajo que alguien tiene que hacer |
| Reintento automático con espera dentro del adaptador | Absorbería la limitación de escritor único sin que el consumidor se entere | **Descartada.** La categoría 03 declara por escrito que esta capa no reintenta; con escritor único el reintento multiplica la espera en vez de reducirla, y esconde la única señal de que el almacén no responde |
| Una unidad de trabajo por adaptador, compartida entre operaciones | Menos aperturas y cierres | Rompe el alcance que la capa de aplicación fijó, y haría que dos casos de uso concurrentes compartieran destino: uno fallando arrastraría al otro |
| Borrado lógico con marca de baja | Permite deshacer una baja y conserva el historial | **Descartada.** `RN-07` exige que no quede ningún trabajo del alumno dado de baja, y una marca deja el dato ahí: el criterio de verificación de la regla dejaría de poder ejecutarse. El modelo conceptual ya declara que la marca «no existe y no va a existir» |
| Guardar los sellos en la zona local del servidor | Se leen sin convertir en el lugar donde se opera | Ata el dato guardado a la configuración regional de una máquina, que es exactamente el modo de falla que el escenario `E-8` documenta para el texto del alumno. Un cambio de zona del servidor reinterpretaría los sellos ya guardados |

## 5. Consecuencias positivas

1. `RN-07` queda verificable con la prueba que la propia regla declara: no queda ningún trabajo del alumno dado de baja.
2. `RN-08` queda exigible en el único lugar donde el texto puede perderse, con una condición propia y no con una convención.
3. La limitación de escritor único queda **declarada hacia afuera** en lugar de absorbida, lo que permite que el estado degradado del producto sea honesto.
4. Los sellos son comparables entre sí y reproducibles en prueba, porque no dependen de la configuración regional de la máquina que los produjo.
5. `RN-12` y `RN-15` quedan sostenidas por construcción: escribir la marca no toca el estado de la cuenta ni sus trabajos, y no pasa por ninguna ruta de retiro.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta la limitación de escritor único** a cambio de un despliegue sin servicio de base de datos aparte. Es una restricción que el intake acepta por escrito, y su síntoma visible es una condición de terminación degradada.
2. **Se acepta que una baja interrumpida no deje ningún efecto**, ni siquiera parcial, aunque eso signifique repetirla entera. Es preferible a dejar trabajos sin dueño, que es la forma más silenciosa de romper el modelo: nada falla y el listado del administrador los sigue mostrando.
3. **Se acepta que el retiro sea irreversible.** No hay deshacer, y la única red es el respaldo, cuya frecuencia la fuente deja a criterio del docente.
4. **Se acepta que quien lea un sello tenga que convertirlo.** El costo cae en la superficie que lo muestra, que es la que conoce a la persona que mira.

## 7. Implementación

- El contexto de persistencia y mapeo de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único lugar donde se abre y se cierra una unidad de trabajo.
- **Convención impuesta:** ninguna operación de adaptador abre más de una unidad, y ninguna anida.
- **Convención impuesta:** ningún adaptador espera activamente a que el almacén se libere. La espera, si la hubiera, es del consumidor.
- El esquema físico que materializa estas decisiones está en [`../Modelo-Datos-Logico.md`](../Modelo-Datos-Logico.md), incluidas las restricciones de integridad que hacen que el arrastre sea comprobable y el índice de la fila siguiente.
- La forma de terminación de cada condición está en [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.3, y esta ADR no la redefine.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Unidades de trabajo por operación de adaptador | **A lo sumo 1**, y **0** anidadas | Inspección de los tres adaptadores que escriben |
| Retiros parciales tras una baja interrumpida | Exactamente **0** | Prueba de baja con el almacén interrumpido a mitad de operación |
| Escrituras aceptadas que reemplazan el texto original | Exactamente **0** | Prueba que materializa un trabajo existente con un texto distinto |
| Esperas activas o reintentos dentro de un adaptador | Exactamente **0** | Inspección en revisión, y prueba con el almacén tomado |
| Sellos guardados fuera del tiempo universal coordinado | Exactamente **0** | Prueba que fija el reloj y compara el valor guardado |
| Marcas de borrado lógico en el esquema | Exactamente **0** columnas | Inspección del esquema contra [`../Modelo-Datos-Logico.md`](../Modelo-Datos-Logico.md) |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4.1 (RN-04, RN-07, RN-08, RN-12, RN-13, RN-15), §17.3.P.4 y §17.3.P.12.
- [`../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) §2, §6 y §7, y las reglas conceptuales `RC-01`, `RC-05`, `RC-06` y `RC-07`.
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md`](../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-05-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md), que fija el alcance que esta ADR materializa.
- ADR relacionadas: [`ADR-01`](ADR-01-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), [`ADR-03`](ADR-03-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md), [`ADR-07`](ADR-07-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Materializa el alcance transaccional que la capa de aplicación fijó, con el todo o nada del arrastre como propiedad exigible, la ausencia declarada de reintento y de espera activa, la escritura única del texto original, y la zona horaria y la precisión de los tres tiempos del trabajo, que cierra un punto abierto derivado por la categoría 02. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
