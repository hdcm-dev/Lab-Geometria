# Estrategia de calidad — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Estrategia-Calidad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Domain/Especificacion-Funcional.md) 1.9 §3 y §4; [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Domain/DX-Error-Messages.md) 1.5 §6.1; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) 1.0 §8, §9 y §11; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) 1.0; [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Domain/Mini-Plan.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.19** §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.8 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](../../Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`, que materializa como etapas del pipeline los quality gates de §3; `11-Documentacion`, que cita esta estrategia sin redefinirla

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

`GeometriaFactory-Domain` tiene calidad cuando **ninguna de las dieciséis reglas de negocio y ninguno de los nueve invariantes puede violarse invocando su superficie pública**, y cuando cada rechazo que produce viaja como una de las **42** condiciones catalogadas, con su código estable y sin efecto parcial sobre la entidad.

Es una definición estrecha a propósito. Este proyecto de código no atiende peticiones, no abre conexiones y no persiste nada ([`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) §17.1.P.3 · GeometriaFactory-Domain, P.4 y P.10), de modo que no hay disponibilidad, latencia ni throughput que medir acá. Lo único que puede fallar es que una guarda no esté, que esté en un solo componente y no en el otro, o que un rechazo llegue como excepción en lugar de como valor.

La consecuencia operativa es que **la calidad de este proyecto de código se mide entera con pruebas unitarias puras y con inspecciones**, y que un defecto suyo no se descubre en un ambiente: se descubre en una prueba que falla o en una revisión que rechaza.

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010, con la métrica de origen cuando existe. Los dos valores rotulados **[ASUNCIÓN]** vienen así desde el intake y **no son compromisos**: se usan como vigentes hasta que el Product Owner los confirme (§22 del intake, asunciones `A-3` y `A-5`).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Adecuación funcional | **Crítica** | 100 % de los **trece** casos de uso con al menos un caso de prueba por criterio de aceptación; 100 % de los **nueve** invariantes con prueba de violación rechazada, sin dobles ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8) |
| Fiabilidad | **Crítica** | 100 % de las **42** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Domain/DX-Error-Messages.md) alcanzadas por al menos una prueba, y **0** condiciones producidas por la biblioteca que no figuren en el catálogo (`05` §8) |
| Mantenibilidad | **Alta** | **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización (`05` §8); **0** advertencias de construcción (intake §17.1.P.8 · GeometriaFactory-Domain) |
| Eficiencia de desempeño | **Media**, y sólo de construcción | Batería de dominio completa en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain]. No hay métrica de runtime porque no hay runtime propio |
| Seguridad | **Baja como implementación, alta como regla** | El proyecto de código no deriva ni compara credenciales: la contraseña llega ya derivada (intake §17.1.P.5 · GeometriaFactory-Domain). Lo que sí se verifica es `INV-06` e `INV-09`, que condicionan el acceso |
| Compatibilidad | **Media** | La superficie pública es contrato para `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`; su estabilidad la gobierna [`ADR-02003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false. Su equivalente es la experiencia del desarrollador, que documenta [`../03-UX-UI-DX/DX-Developer-Experience.md`](../../../03-UX-UI-DX/_fusion/Domain/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única sin sufijo de sistema operativo (intake §17.1.P.9 · GeometriaFactory-Domain). No hay matriz de plataformas que probar |

**Los dos atributos críticos son los que justifican la existencia de este proyecto de código.** El intake declara que sus invariantes son «la última defensa de las reglas» (§17.1.P.6 · GeometriaFactory-Domain), y esa frase es la que fija la prioridad: si una guarda falla acá, ninguna capa de más arriba la repone.

## 3. Quality gates

Cada gate declara condición, cómo se verifica y qué pasa cuando no se cumple. Los cinco primeros los declara el intake §17.1.P.8 · GeometriaFactory-Domain; los tres siguientes los deriva [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Domain/Arquitectura-Proyecto-Codigo.md) §8.

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El guion de construcción termina en **0 y sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.1.P.8 · GeometriaFactory-Domain) |
| QG-02 | El guion de pruebas pasa **entero**: cero pruebas rojas y cero deshabilitadas sin motivo escrito | Etapa `test` del pipeline | Bloquea la fusión |
| QG-03 | La cobertura alcanza el mínimo declarado: **90 %** de líneas y **85 %** de ramas [ASUNCIÓN del intake §17.1.P.6 · GeometriaFactory-Domain] | Informe de cobertura de la etapa `test` | **Condicionado**, ver §3.1 |
| QG-04 | El archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización | Inspección del archivo de proyecto, en revisión y como prueba de inspección (`TC-02024`) | Bloquea la fusión. Es la propiedad que justifica el estilo entero ([`ADR-02001`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md)) |
| QG-05 | **100 %** de las **42** condiciones del catálogo alcanzadas por prueba, y **0** condiciones emitidas fuera del catálogo | Prueba de inspección en las dos direcciones (`TC-02023`) | Bloquea la fusión |
| QG-06 | **100 %** de los **nueve** invariantes con al menos una prueba que verifique su violación rechazada, **sin dobles de prueba** | Matriz invariante contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5, revisada al cerrar cada etapa (`TC-02026`) | Bloquea el cierre de la etapa |
| QG-07 | La batería completa termina en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10 · GeometriaFactory-Domain] | Duración total reportada por el ejecutor en la etapa `test` | **Condicionado**, ver §3.1 |
| QG-08 | Ninguna condición prevista viaja como excepción de control de flujo | Revisión de la superficie pública contra [`ADR-02002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | Se rechaza en revisión aunque compile |

### 3.1 Qué significa que un gate esté condicionado

`QG-03` y `QG-07` son los dos gates cuyo umbral es un valor rotulado **[ASUNCIÓN]** en el intake §22 —`A-3` para la cobertura, `A-5` para el tiempo de la batería—. [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Backlog-Tecnico.md) `BT-02015` declara el tratamiento y esta estrategia lo adopta sin cambiarlo: **los dos valores se usan como vigentes y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner los confirme sobre su propio documento**.

Condicionado no quiere decir opcional. La medición se hace igual y el resultado se registra; lo que queda en suspenso es la consecuencia automática. Un incumplimiento se trata como hallazgo del punto de control de la etapa y no como rechazo de la fusión.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2): la misma persona diseña las pruebas, las ejecuta y aprueba el cierre. Declararlo es más útil que simular un RACI de tres columnas con un solo nombre.

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Diseñar los casos de prueba, mantener la matriz de cobertura y la Definition of Done, y declarar si un criterio de validación se cumple |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | Aprobar el cierre de cada etapa en su punto de control, y confirmar los dos valores rotulados [ASUNCIÓN] |
| Revisión mecánica | El pipeline | Los ocho gates de §3. Es lo único que no depende de que alguien se acuerde |

**Lo que reemplaza al revisor humano independiente es el punto de control bloqueante de cada etapa** (intake §15), exactamente con el mismo fundamento con el que lo declara [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Definition-Of-Ready.md) §4. Esta categoría no inventa un segundo revisor que no existe.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) entran en alcance, según las historias de la etapa | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| Al cerrar cada etapa | La matriz de cobertura entera, incluida la de los nueve invariantes; el estado de cada `TC-XX` | Matriz actualizada y la constancia de los gates medidos |
| Al cerrar la etapa `d` | Los dos valores rotulados [ASUNCIÓN], por `BT-02015` | La confirmación del Product Owner, o su continuidad como asunción |
| Ante todo defecto cerrado | Que exista al menos un `TC-XX` nuevo o extendido que lo prevenga | La entrada correspondiente en el catálogo de casos de prueba |

**La cadencia es por etapa y no por sprint**, porque este producto no tiene sprints: la unidad de planificación es la etapa ([`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §1.2, citado por [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Domain/Product-Backlog.md) §4.1). **No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y una cadencia en semanas sería un plazo que ninguna fuente da.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad del proyecto de código, los ocho atributos ISO 25010 con su prioridad y su métrica de origen, los **ocho** quality gates con condición, verificación y consecuencia —dos de ellos condicionados por depender de un valor rotulado [ASUNCIÓN] en el intake §22—, el reparto de papeles con la constancia de que `equipo_n` es 1 y de que el filtro real es el punto de control de la etapa, y la cadencia de revisión por etapa, sin inventar ninguna frecuencia calendaria. |
