# ADR-04003 — Versionado por compilación compartida y estabilidad de la superficie de dos caras

**Proyecto de código:** GeometriaFactory-Application
**Documento:** ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

Este proyecto de código tiene una superficie pública **de dos caras**, y esa forma es lo que hace que su versionado no sea el de una biblioteca común:

- Hacia arriba expone **casos de uso** a `GeometriaFactory-Api`.
- Hacia abajo expone **puertos** que `GeometriaFactory-Infrastructure` implementa.

Un cambio incompatible en la primera cara rompe a un consumidor; un cambio incompatible en la segunda rompe a un implementador. Son dos direcciones de ruptura distintas, y la segunda es la menos intuitiva: agregar una operación a un puerto —que en una superficie normal sería cambio menor— **rompe a quien lo implementa**.

La fuente declara la estrategia de versionado de este proyecto de código idéntica a la del nivel 0: versionado semántico, convenciones de mensaje de confirmación, **sin publicación en ningún repositorio de paquetes**, y una rama y una etiqueta por etapa. `redistribuible` es false. Los dos proyectos de código que lo consumen se compilan dentro del mismo artefacto de agrupación.

Motivación upstream: `PRODUCT-INTAKE` §17.2.P.3, §17.2.P.7 y §17.2.P.8; §15, regla de no-regresión acumulativa y punto de control bloqueante.

## 2. Decisión

**El contrato se protege por compilación compartida y no por descripción formal ni por convivencia de versiones.** Un cambio incompatible en cualquiera de las dos caras rompe la compilación de la solución, que es la señal más temprana posible, y la política es corregir a los dos lados **en la misma etapa**.

Sobre la clase de cambio se decide, además, la asimetría que la superficie de dos caras impone: **agregar una operación a un puerto es cambio mayor**, porque obliga a todo implementador a proveerla; agregar un caso de uso es cambio menor.

**No se publica en ningún repositorio de paquetes**, de modo que no hay deprecación gradual, ni versiones conviviendo, ni consumidor externo al que avisar.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Compilación compartida, con la asimetría de las dos caras declarada (**adoptada**) | Rompe en construcción y no en ejecución; costo de cadena de herramientas nulo; refleja que los dos consumidores viven en el mismo artefacto de agrupación | No protege contra un cambio de **semántica** que no cambie ninguna firma: eso lo tiene que atrapar la revisión y la batería |
| Publicar la biblioteca como paquete versionado | Permitiría consumidores fuera de la solución y deprecación gradual | `redistribuible` es false y no hay ningún consumidor fuera de la solución. Agregaría un feed y una ceremonia de publicación sin resolver ningún problema del producto |
| Descripción formal del contrato, generada y verificada por herramienta | Contrato explícito, verificable fuera del compilador | No hay protocolo que describir: esta capa no cruza frontera de proceso (`PRODUCT-INTAKE` §17.2.P.3). Sería una descripción de una superficie de biblioteca que el propio compilador ya verifica |
| Tratar los puertos con la misma regla que los casos de uso | Una sola regla, más simple de recordar | Escondería la asimetría real: agregar una operación a un puerto **rompe a quien lo implementa**, y llamarlo cambio menor haría que se hiciera sin punto de control |

## 5. Consecuencias positivas

1. Un cambio incompatible es imposible de desplegar a medias: la solución no compila.
2. La asimetría de las dos caras queda escrita, de modo que ampliar un puerto pasa por el mismo punto de control que quitar un caso de uso.
3. No hay ceremonia de publicación que mantener ni feed que administrar.
4. La regla de no-regresión acumulativa del producto tiene con qué apoyarse: la etiqueta por etapa permite volver a cualquier demostración ya aprobada.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que un cambio de semántica sin cambio de firma no lo atrape el compilador.** Perder una comprobación de autorización, o cambiar el orden en que se ejercen, compila igual. La mitigación no es de versionado sino de pruebas: el NFR de las cuatro comprobaciones ejercitadas y la prueba de que la cuarta corta primero.
2. **Se acepta que no exista deprecación gradual.** Si un caso de uso cambia de forma, los dos lados se corrigen en la misma etapa. Con un solo equipo y un solo artefacto de agrupación, el costo es bajo; con más consumidores dejaría de serlo, y entonces esta decisión merecería una ADR nueva que la supere.
3. **Se acepta que la versión del proyecto de código no se publique en ninguna parte observable**, de modo que el único registro de qué versión corre es la etiqueta de la etapa.

## 7. Implementación

| Cambio sobre la superficie | Cara | Clase |
| --- | --- | --- |
| Quitar o renombrar un caso de uso, o cambiar su postcondición | Hacia arriba | Mayor |
| Cambiar qué exige resuelto un caso de uso antes de invocarlo | Hacia arriba | Mayor |
| Quitar, renombrar o cambiar la firma de una operación de un puerto | Hacia abajo | Mayor |
| **Agregar** una operación a un puerto existente | Hacia abajo | **Mayor**, por la asimetría de §2 |
| Agregar un puerto nuevo | Hacia abajo | Mayor |
| Quitar una condición del catálogo de 03, o reciclar su identificador | Las dos | Mayor |
| Agregar un caso de uso | Hacia arriba | Menor |
| Agregar una condición al catálogo de 03 | Las dos | Menor |
| Corregir un orquestador para que ejerza la comprobación que ya declaraba | Ninguna | Parche |

- La versión vive en el archivo de proyecto y se calcula a partir de las convenciones de mensaje de confirmación; la herramienta que la calcula **no está elegida** y se ancla en la etapa `a`.
- Una rama y un pull request por etapa; el pull request **es** el punto de control.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Advertencias de construcción | Exactamente **0** | Etapa de `build` del pipeline, bloqueante para fusionar |
| Cambios mayores desplegados sin corregir las dos caras en la misma etapa | Exactamente **0** | Imposible por construcción: la solución no compila. Se verifica en cada pull request |
| Paquetes publicados en un repositorio de paquetes | Exactamente **0** | Inspección del pipeline |
| Etapas cerradas sin etiqueta | Exactamente **0** | Inspección de etiquetas contra el índice de informes de cierre |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §15, §17.2.P.3, §17.2.P.7 y §17.2.P.8; §17.1.P.7, al que §17.2.P.7 se declara idéntica.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** §2, por `redistribuible` == false.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md), que toma la misma decisión para la frontera entre las dos unidades desplegables.
- ADR relacionadas: [`ADR-04002`](ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md), [`ADR-04006`](ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el versionado por compilación compartida sin publicación, declara la asimetría de la superficie de dos caras —agregar una operación a un puerto es cambio mayor—, evalúa cuatro alternativas, declara tres trade-offs incluido lo que el compilador no atrapa, fija la clasificación de nueve clases de cambio y cuatro métricas de validación. |
