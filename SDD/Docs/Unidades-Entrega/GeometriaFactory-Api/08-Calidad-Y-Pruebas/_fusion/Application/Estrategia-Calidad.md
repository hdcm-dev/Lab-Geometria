# Estrategia de calidad — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Estrategia-Calidad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) 1.7 §3, §4, §5 y §6; [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md) §7.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8, §9 y §11; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) §5; [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Application/Mini-Plan.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §15, §17.2.P.6, §17.2.P.8, §17.2.P.10 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`, que materializa como etapas del pipeline los quality gates de §3; `11-Documentacion`, que cita esta estrategia sin redefinirla

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Qué significa que un gate esté condicionado](#31-qué-significa-que-un-gate-esté-condicionado)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Application` tiene calidad cuando **las cuatro comprobaciones de autorización se ejercen en todos los caminos que las alcanzan y en el orden fijo declarado**, cuando **cada uno de los once casos de uso se puede ejercer entero con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso**, y cuando toda negativa prevista viaja como una de las **36** condiciones catalogadas, con su código estable y sin efecto parcial sobre la unidad de trabajo.

Las tres partes de esa definición no son intercambiables. La primera es la que sostiene `INV-02`, `INV-03` e `INV-09`; la segunda es la propiedad estructural que justifica el estilo entero del proyecto de código ([`../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md)) y es lo que hace que la primera sea verificable sin ambiente; la tercera es la que impide que un rechazo se convierta en un fallo silencioso aguas arriba.

La consecuencia operativa es que **la calidad de este proyecto de código se mide entera con pruebas unitarias con dobles y con inspecciones**. No hay ambiente donde descubrir un defecto suyo: se descubre en una prueba que falla o en una revisión que rechaza. La batería de integración del producto existe, pero **no es de esta capa**: vive en `GeometriaFactory-Api` (intake §17.2.P.6).

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los dos valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | 100 % de los **once** casos de uso con al menos un caso de prueba por criterio de aceptación de sus historias; **4 de 4** comprobaciones de autorización con prueba de su negativa ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §8) |
| Seguridad | **Crítica, como autorización y no como mecanismo** | La cuarta comprobación corta antes que las otras tres, con **1** prueba dedicada a ese orden (`05` §8). Esta capa no compara contraseñas ni emite accesos: la contraseña llega ya derivada y la provisoria ya producida (intake §17.2.P.5) |
| Fiabilidad | **Crítica** | 100 % de las **36** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md) §7.1 alcanzadas por al menos una prueba, y **0** condiciones producidas por la capa que no figuren en el catálogo (`05` §8); **a lo sumo 1** unidad de trabajo por caso de uso, sin efecto repartido |
| Mantenibilidad | **Alta** | Exactamente **1** referencia a otro proyecto de código del producto —`GeometriaFactory-Domain`— y **0** a bibliotecas de persistencia, transporte, serialización o marco web (`05` §8); **0** advertencias de construcción (intake §17.2.P.8) |
| Eficiencia de desempeño | **Media** | El caso de uso más pesado —el envío que interpreta el texto semilla de **3** piezas del escenario `E-1`— resuelve en menos de **500 ms**, medido **sin acceso a base** [ASUNCIÓN del intake §17.2.P.10]. **0** componentes de pieza cargados en las consultas de listado |
| Compatibilidad | **Media** | La superficie pública es contrato para `GeometriaFactory-Api`, y los **cuatro** puertos son contrato para `GeometriaFactory-Infrastructure`; su estabilidad la gobierna [`ADR-04003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false (`PRODUCT-MANIFEST` §5). Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.2.P.9). No hay matriz de plataformas que probar |

**Los tres atributos críticos se sostienen entre sí.** El intake declara que la verificación de pertenencia existe porque «el rol no alcanza» (§17.2.P.5), y `05` §9 declara como riesgo de impacto **muy alto** que aparezca un camino que ejerza una capacidad sin resolver antes la marca de cambio de contraseña pendiente. Esta estrategia trata esos dos enunciados como el eje de su prioridad.

## 3. Quality gates

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cuatro primeros los declara el intake §17.2.P.8 —que remite a §17.1.P.8 y agrega uno propio—; los demás los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §8, con una fila por NFR.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.2.P.8, que remite a §17.1.P.8) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | La cobertura alcanza el mínimo declarado: **85 %** de líneas y **80 %** de ramas [ASUNCIÓN del intake §17.2.P.6] | Informe de cobertura de la etapa `test`, **por componente** | **Condicionado**, ver §3.1 |
| QG-04 | **Ninguna prueba de esta capa toca la base de datos real.** El umbral es exactamente **0** | Prueba de inspección `TC-04026` y revisión del pull request | **Bloquea la fusión.** Es la puerta propia que el intake §17.2.P.8 declara: «si una lo hace, está mal ubicada y pertenece a integración» |
| QG-05 | El archivo de proyecto declara exactamente **1** referencia a otro proyecto de código del producto y **0** a bibliotecas de persistencia, transporte, serialización o marco web | Inspección del archivo de proyecto, en revisión y como prueba de inspección (`TC-04027`) | Bloquea la fusión. Es la propiedad que sostiene `QG-04` |
| QG-06 | **100 %** de las **36** condiciones del catálogo alcanzadas por prueba, y **0** condiciones emitidas fuera del catálogo | Prueba de inspección en las dos direcciones (`TC-04028`) | Bloquea la fusión |
| QG-07 | **4 de 4** comprobaciones de autorización con al menos una prueba de su negativa **sin base de datos**, y **1** sola prueba que verifique que la cuarta corta antes que las otras tres | Matriz comprobación contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5, revisada al cerrar cada etapa (`TC-04011`) | Bloquea el cierre de la etapa |
| QG-08 | **A lo sumo 1** unidad de trabajo por caso de uso, y **0** casos de uso que repartan su efecto entre dos | Inspección de los once orquestadores y `TC-04029`, con la baja de cuenta como caso testigo | Bloquea la fusión |
| QG-09 | **0** componentes de pieza cargados en el listado del alumno y en el de la comisión | `TC-04030`, sobre la proyección que devuelve la consulta | Bloquea la fusión |
| QG-10 | El caso de uso más pesado resuelve en menos de **500 ms** para el texto semilla de **3** piezas de `E-1`, medido sin acceso a base [ASUNCIÓN del intake §17.2.P.10] | Medición sobre la batería unitaria con doble del puerto de validación, en la etapa `test` | **Condicionado**, ver §3.1 |
| QG-11 | Ninguna condición prevista viaja como excepción de control de flujo | `TC-04031` y revisión de la superficie pública contra [`ADR-04006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Se rechaza en revisión aunque compile |

**Once gates, y ninguno inventado.** Los que no salen del intake salen de una fila de `05` §8, que es la sección que declara los **nueve** NFR de este proyecto de código con su objetivo numérico. No se agregó ninguna puerta técnica: las cinco del producto —`PT-01` a `PT-05`— se miden en `GeometriaFactory-Web` y en `GeometriaFactory-Api`, y el intake §15 no le asigna ninguna a esta capa.

### 3.1 Qué significa que un gate esté condicionado

`QG-03` y `QG-10` son los dos gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para los 500 ms—. [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) `PA-05` y [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md) `BT-04018` declaran el tratamiento y esta estrategia lo adopta sin cambiarlo: **los dos valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática. Un incumplimiento se trata como hallazgo del punto de control de la etapa y no como rechazo de la fusión.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, y confirmar los dos valores rotulados [ASUNCIÓN] |
| Revisión mecánica | El pipeline | Los once gates de §3. Es lo único que no depende de que alguien se acuerde |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15, regla de delivery 2), exactamente con el mismo fundamento con el que lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) §4. Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance, según las historias de la etapa | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera, incluida la de las cuatro comprobaciones; el estado de cada `TC-XX` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `d` | Los dos valores rotulados [ASUNCIÓN], por `BT-04018` | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa ([`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md), citado por [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2). **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y una cadencia en semanas sería un plazo que ninguna fuente da.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad del proyecto de código en sus tres partes —comprobaciones ejercidas en orden fijo, casos de uso ejercibles con dobles y catálogo cerrado de condiciones—, los ocho atributos ISO 25010 con su prioridad y su métrica de origen, los **once** quality gates con condición, verificación y consecuencia —dos de ellos condicionados por depender de un valor rotulado [ASUNCIÓN] en el intake §22, y uno de ellos la puerta propia y bloqueante que el intake §17.2.P.8 declara—, el reparto de papeles con la constancia de que `equipo_n` es 1, y la cadencia de revisión por etapa, sin inventar ninguna frecuencia calendaria ni ninguna puerta técnica. |
