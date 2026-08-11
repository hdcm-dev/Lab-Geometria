# ADR-04 — Tres capas de presentación: ninguna superficie llega sola al servicio de datos

**Proyecto de código:** GeometriaFactory-Web
**Documento:** ADR-04-Tres-Capas-De-Presentacion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

[`ADR-01`](ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md) decide que hay **una sola salida** hacia el servicio de datos. Lo que esa decisión deja abierto es **quién puede usarla**, y ahí hay una presión concreta: en un modelo de componentes con render en el servidor, nada impide que una superficie invoque directamente al cliente tipado, y hacerlo es más corto.

Si once superficies lo hicieran, once lugares tendrían que repetir la traducción de condiciones, la decisión de qué estado mostrar y la composición del resultado. Y la propiedad que la Fase B2 ya usó —**que una superficie se puede maquetar y validar sin servicio de datos**— dejaría de valer.

Hay además un dato que esta decisión tiene que respetar y que no es de arquitectura sino de historia: **la maqueta se aprobó**, con una línea de base identificada de **once** superficies, **setenta y tres** componentes, **setenta y cuatro** estados y **veinticuatro** rutas de navegación, y con **sesenta y una** filas de sensado de deriva que la codificación va a tener que ir verificando. La arquitectura tiene que hacer que esa línea de base sea sostenible, no que haya que reconstruirla.

Motivación upstream: NB-03, NB-06, NB-07, NB-09; `PRODUCT-INTAKE` §17.6.P.2 y §17.6.P.6; las once superficies, los dos shells y las tres representaciones de la categoría 03.

## 2. Decisión

**El proyecto de código se organiza en tres capas internas con dependencias unidireccionales:**

1. **Presentación** —armazón y encaminamiento, las once superficies y las tres representaciones reutilizadas—.
2. **Aplicación de front** —servicios de aplicación, sesión y estado del circuito, y traductor de condiciones a presentación—.
3. **Salidas** —el cliente tipado hacia el servicio de datos, y el anfitrión del visor hacia la fachada del bundle—.

**Ninguna superficie invoca a la capa 3.** Entre una superficie y cualquiera de las dos salidas hay siempre un componente de la capa 2. La única excepción es el **anfitrión del visor**, que es un componente de capa 3 al que las superficies sí instancian, porque **es el componente anfitrión del contrato de fachada** y su ciclo de vida está atado al de la superficie que lo aloja; incluso ahí, la superficie no toca la fachada: la toca el anfitrión.

**Las tres representaciones reutilizadas no piden datos.** Reciben lo ya traído y sólo presentan, que es lo que permite que las use cualquier superficie sin arrastrar dependencias.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Tres capas, con la capa 2 obligatoria entre superficie y salida (**adoptada**) | La traducción de condiciones y la decisión de estado viven en un lugar; una superficie se maqueta y se valida sin servicio de datos; la línea de base visual sigue siendo verificable | Una capa más de indirección para operaciones triviales, donde el servicio de aplicación es casi un pasamanos |
| Superficies que invocan directamente al cliente tipado | Menos indirección y menos archivos; cada superficie se lee entera | Once lugares repitiendo traducción de condiciones y decisión de estado, con el riesgo cierto de que diverjan; y se perdería la propiedad que la Fase B2 ya usó. **Descartada por esta categoría** |
| Un único servicio de aplicación para todas las superficies | Un solo lugar donde mirar | Se convierte en el punto por donde pasa todo, sin cohesión: mezclaría la composición del listado de la comisión con la del cambio de contraseña. La cohesión se pierde y el archivo crece sin límite. **Descartada por esta categoría** |
| Estado compartido entre superficies en la capa 2, para no volver a pedir | Menos ida y vuelta al navegar entre superficies | Es una caché con otro nombre, y [`ADR-02`](ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) ya la descartó con su fundamento. **Descartada, y se declara acá para que no vuelva por la puerta de la capa 2** |

## 5. Consecuencias positivas

1. La traducción de condiciones vive en **un** componente, que es lo que hace verificable en un solo punto que ningún mensaje exponga una dirección de servicio (`RA-03`).
2. Una superficie se puede recorrer y validar sin servicio de datos, que es exactamente lo que la Fase B2 hizo con la maqueta aprobada.
3. Las **sesenta y una** filas de sensado de deriva tienen contra qué verificarse: la superficie sigue siendo la unidad, y la capa 2 no altera lo que la línea de base identificó.
4. Las tres representaciones reutilizadas se usan desde cinco, dos y once superficies respectivamente sin arrastrar dependencias, porque no piden datos.
5. El anfitrión del visor queda como componente propio y no disuelto en la superficie, lo que hace que su ciclo de vida —incluida la liberación— tenga dueño.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta la indirección en operaciones triviales.** Hay servicios de aplicación que serán casi un pasamanos, y se acepta a cambio de que no exista la duda de dónde poner la traducción cuando la operación deje de ser trivial.
2. **Se acepta que el anfitrión del visor sea una excepción declarada** a la regla de que las superficies no tocan la capa 3. La excepción tiene fundamento —es el componente anfitrión del contrato de fachada, y su ciclo de vida es el de la superficie— y está acotada a dos superficies de once.
3. **Se acepta que la capa 2 no comparta estado entre superficies**, con el ida y vuelta que eso implica al navegar. Es la misma decisión de [`ADR-02`](ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md), y se repite acá porque la capa 2 es el lugar más natural por donde una caché volvería.

## 7. Implementación

- Los ocho componentes de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 llevan declarada su capa; §3.2 declara las cinco precisiones de la regla de dependencias.
- Cada superficie declara su **nombre canónico**, que es el que la maqueta y la línea de base visual ya usan y que **no se cambia**: cambiarlo invalidaría filas de la matriz de sensado de deriva.
- Las tres representaciones reutilizadas reciben datos por parámetro y no invocan a nadie.
- La **sección 5 de cada wireframe es la lista de estados que hay que sostener**: un estado que la implementación no reproduzca es una deriva, y la matriz lo levanta.
- Convención impuesta: una superficie nueva nace con su servicio de aplicación de front, aunque sea un pasamanos.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Superficies que invocan al cliente tipado directamente | Exactamente **0** de **11** | Inspección del árbol de fuentes |
| Superficies que invocan a la fachada del visor sin pasar por el anfitrión | Exactamente **0** | Inspección del árbol de fuentes |
| Representaciones reutilizadas que piden datos | Exactamente **0** de **3** | Inspección de sus dependencias |
| Nombres canónicos de superficie cambiados respecto de la línea de base | Exactamente **0** de **11** | Comparación con la línea de base visual aprobada |
| Filas de la matriz de sensado de deriva verificadas al cierre de cada sprint | **100 %** de las filas cuyos elementos toca el sprint, sobre **61** | Actualización de estado y fecha en la matriz |
| Pasos del guion de demostración de la etapa y de todas las anteriores | **100 %** ejecutados y en verde antes del punto de control [ASUNCIÓN en cuanto a expresarlo como puerta] | Ejecución del guion en el navegador del equipo anfitrión |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §15 (regla de no-regresión acumulativa y punto de control bloqueante), §17.6.P.2 y §17.6.P.6; §22 asunción A-4.
- [`../../03-UX-UI-DX/Experiencia-De-Uso.md`](../../03-UX-UI-DX/Experiencia-De-Uso.md) §3.1 y §3.2; [`../../03-UX-UI-DX/README.md`](../../03-UX-UI-DX/README.md) §4.
- [`../../03-UX-UI-DX/Linea-Base-Visual.md`](../../03-UX-UI-DX/Linea-Base-Visual.md) y [`../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md).
- ADR relacionadas: [`ADR-01`](ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-02`](ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md), [`ADR-05`](ADR-05-Estado-Degradado-Como-Superficie.md), [`ADR-06`](ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la separación en tres capas de presentación con la capa 2 obligatoria entre superficie y salida, declara la excepción acotada del anfitrión del visor con su fundamento, evalúa cuatro alternativas —incluida la caché que volvería por la puerta de la capa 2—, declara tres trade-offs, ata la arquitectura a la línea de base visual aprobada y fija seis métricas de validación. |
