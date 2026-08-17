# ADR-06007 — Transformaciones de esquema al arrancar, con linaje inmutable y arranque detenido

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

El intake decide dos cosas sobre el esquema y las decide juntas: las transformaciones se **aplican automáticamente al arrancar**, sobre un almacén inexistente o desactualizado, y **cada transformación se versiona con el código de su etapa; no se editan las ya fusionadas** (`PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.7 · GeometriaFactory-Infrastructure). La segunda es criterio de aceptación de la etapa `c`: «las migraciones se aplican solas sobre una base inexistente».

Lo que ninguna fuente resuelve es **qué pasa cuando no se pueden aplicar**. Y ahí están los dos atajos más destructivos del producto, los dos declarados en el catálogo de condiciones de la categoría 03: descartar el almacén y crearlo de nuevo —«deja el servicio impecable y sin los trabajos de nadie»— y caer hacia una ruta alternativa dentro de la imagen —«el servicio arranca, acepta trabajos de la comisión entera y los pierde en el siguiente reemplazo de versión»—. Los dos comparten la forma: **el servicio arranca y nadie se entera**.

El despliegue del producto agrava la apuesta. El reemplazo de versión es *detener y arrancar*, con ventana de indisponibilidad y sin proxy inverso; el despliegue es manual, a cargo del docente; y el respaldo es una copia del archivo con frecuencia a definir. No hay red de contención automática.

Motivación upstream: NB-00003, NB-00008; RN-06008; `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Infrastructure, §17.1.P.7 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure, §17.1.P.4 · GeometriaFactory-Api, §17.1.P.7 · GeometriaFactory-Api.

## 2. Decisión

**El almacén se deja en condiciones antes de la primera petición, y si no se puede, el arranque se detiene.** Cinco reglas:

1. **La preparación ocurre una sola vez, al arrancar, y antes de que el servicio atienda.** No es un paso manual de despliegue y no se dispara a demanda desde ninguna operación.
2. **El linaje de transformaciones es inmutable.** Una transformación ya fusionada no se edita: si hay que corregirla, entra una nueva. La causa frecuente de `MIGRACION_NO_APLICABLE` es exactamente esa edición.
3. **Un esquema que no corresponde al linaje conocido detiene el arranque.** No se aplica un esquema por aproximación y **no se descarta el almacén**: la salida es restaurar el respaldo o revisar la transformación, y las dos son decisiones de una persona.
4. **Una ubicación que no admite escritura detiene el arranque.** **No se cae hacia una ruta alternativa dentro de la imagen**, y el mensaje **no incluye la ruta** (`RA-03`).
5. **No hay modo de sólo lectura ni arranque parcial.** Un servicio que atiende sobre un almacén en el que no se puede confiar es peor que un servicio que no arranca: el segundo se nota enseguida, el primero se nota cuando alguien busca su trabajo y no está.

**Quién dispara la preparación es `GeometriaFactory-Api`, y quién la ejecuta es este proyecto de código.** La ruta del almacén llega desde la configuración que aquél provee: esta capa **la recibe y no la busca**.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Preparación al arrancar, con linaje inmutable y arranque detenido (**adoptada**) | El despliegue no tiene paso manual que olvidar; el fallo es ruidoso y ocurre antes de aceptar un solo trabajo; la etapa `c` tiene su criterio de aceptación verificable | Una transformación mal escrita deja el servicio caído hasta que una persona intervenga |
| Aplicar las transformaciones por un paso manual de despliegue | La persona decide cuándo, y puede respaldar antes | **Descartada por el intake §17.1.P.4 · GeometriaFactory-Infrastructure**, que las declara automáticas al arrancar. Además el despliegue es manual y domiciliario: un paso más es un paso más para olvidar |
| Arrancar en modo de sólo lectura cuando el esquema no corresponde | El listado seguiría funcionando y el docente vería algo | **Descartada.** Un producto cuya única acción de guardado no funciona no está degradado, está roto, y presentarlo como funcionando retrasa el diagnóstico. Además el esquema que no corresponde puede hacer que la lectura devuelva datos incompletos sin decirlo |
| Descartar el almacén y recrearlo cuando el esquema no corresponde | El servicio siempre arranca y el criterio de aceptación siempre pasa | **Descartada, y es el atajo más destructivo del producto.** Deja el servicio impecable y sin los trabajos de nadie |
| Caer hacia una ruta alternativa cuando el volumen no está montado | El servicio arranca en cualquier entorno sin configurar | **Descartada.** Acepta trabajos y los pierde en el siguiente reemplazo de versión, que es el modo de falla que más tarda en notarse de todo el producto |

## 5. Consecuencias positivas

1. La etapa `c` tiene un criterio de aceptación ejecutable: las transformaciones se aplican solas sobre un almacén inexistente.
2. Los dos atajos más destructivos del producto quedan cerrados con condiciones propias y con su fundamento escrito.
3. El fallo de despliegue se detecta **antes** de aceptar un trabajo, que es la única ventana en la que todavía no hay nada que perder.
4. El linaje inmutable hace que el esquema sea reconstruible y auditable, y que la causa frecuente del fallo tenga nombre.
5. `RA-03` se sostiene en el peor momento posible —el arranque fallido, cuando la tentación de decir la ruta es máxima—, con la contracara del registro del lado del servidor.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que una transformación mal escrita deje el servicio caído** hasta que una persona intervenga. Es el precio de no tener ningún camino automático que pueda perder datos.
2. **Se acepta la ventana de indisponibilidad de cada reemplazo de versión**, que el intake declara aceptada a cambio de no montar un proxy inverso.
3. **Se acepta que el diagnóstico dependa del registro del lado del servidor**, porque el mensaje no puede llevar la ruta. Sin ese registro, la prohibición de exponer se convierte en imposibilidad de diagnosticar.
4. **Se acepta que el guion de restablecimiento reproduzca el estado de primer arranque**, o sea un almacén vacío: no es un camino de producción y esta ADR lo declara para que nadie lo use como si lo fuera.

## 7. Implementación

- El componente de mecanismo de acceso firmado y preparación del almacén de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 es el único lugar donde el esquema se transforma.
- **Convención impuesta:** ninguna operación de adaptador dispara la preparación. Se ejecuta una vez, desde el arranque, y no a demanda.
- **Convención impuesta:** la ubicación del almacén se recibe. Este proyecto de código no lee variables de entorno ni archivos de configuración por su cuenta.
- **Convención impuesta:** una transformación fusionada no se edita. La revisión rechaza el cambio aunque compile y aunque el resultado sea equivalente.
- La etapa de verificación de transformaciones del pipeline (`PRODUCT-INTAKE` §17.1.P.8 · GeometriaFactory-Infrastructure) es la puerta bloqueante que comprueba la regla 1 de §2 en cada fusión.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Aplicación de transformaciones sobre almacén inexistente | **1 de 1** intento exitoso, sin paso manual | Etapa de verificación de transformaciones del pipeline |
| Almacenes descartados o recreados automáticamente | Exactamente **0** | Prueba con un esquema que no corresponde al linaje: debe detener el arranque y **dejar el archivo intacto** |
| Rutas alternativas usadas cuando la configurada no admite escritura | Exactamente **0** | Prueba con la ubicación no escribible |
| Peticiones atendidas con la preparación incompleta | Exactamente **0** | Prueba de arranque fallido contra el punto de salud |
| Transformaciones fusionadas editadas | Exactamente **0** | Inspección del historial en cada revisión |
| Mensajes de arranque detenido que incluyen la ruta del almacén | Exactamente **0** | Prueba de inspección sobre las dos condiciones de arranque detenido |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §14 (RA-03), §17.1.P.4 · GeometriaFactory-Infrastructure, §17.1.P.7 · GeometriaFactory-Infrastructure, §17.1.P.8 · GeometriaFactory-Infrastructure, §17.1.P.4 · GeometriaFactory-Api y §17.1.P.7 · GeometriaFactory-Api.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-06010-Preparar-El-Almacen-Al-Arrancar.md`](../Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md).
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.3 (forma de terminación «arranque detenido»), §2.4 y §3.9.
- [`../Modelo-Datos-Logico.md`](../Modelo-Datos-Logico.md) §5, que declara la transformación inicial con su identificador.
- ADR relacionadas: [`ADR-06002`](ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md), [`ADR-06004`](ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija la preparación del almacén antes de la primera petición, el linaje inmutable de transformaciones y el arranque detenido como única salida ante un esquema que no corresponde o una ubicación que no admite escritura, con los dos atajos destructivos cerrados por decisión y no por recomendación. Evalúa cinco alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
