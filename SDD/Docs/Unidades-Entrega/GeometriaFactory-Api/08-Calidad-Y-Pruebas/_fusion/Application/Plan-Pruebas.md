# Plan de pruebas — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Plan-Pruebas.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.0; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) 1.1 §2 y §3; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Application/Backlog-Tecnico.md); [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Application/Mini-Plan.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md)
**Trazabilidad downstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Alcance del plan](#1-alcance-del-plan)
- [2. Criterios de entrada](#2-criterios-de-entrada)
- [3. Criterios de salida](#3-criterios-de-salida)
- [4. Riesgos de calidad](#4-riesgos-de-calidad)
- [5. Plan por etapa](#5-plan-por-etapa)
- [6. Recursos](#6-recursos)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Alcance del plan

**Qué cubre.** Los **treinta y un** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** Las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la interpretación efectiva del texto, la derivación de la contraseña, la producción de la provisoria y el guardado, en `GeometriaFactory-Infrastructure`; el transporte de los datos por la frontera de proceso, en `GeometriaFactory-Contracts`; **la batería de integración contra el almacén real y la API real**, en `GeometriaFactory-Api` (intake §17.2.P.6); las superficies y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y el producto no tiene sprints. Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**: sería un plazo que ninguna fuente da.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §2 declara que no producen épica en este proyecto de código, porque ninguna de las dos orquesta un caso de uso ni ejerce una comprobación de autorización.

## 2. Criterios de entrada

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo ([`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §5).
- [ ] Las historias de la etapa cumplen los **siete** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Application/Definition-Of-Ready.md) §1, incluidos el 5 —cuál de las cuatro comprobaciones la alcanza— y el 6 —toda condición existe en el catálogo de las 36—.
- [ ] `BT-04001` está cerrada: el proyecto de código y su proyecto de pruebas existen, la batería corre aunque sea vacía, y el archivo de proyecto declara **1** dependencia saliente.
- [ ] `BT-04002` está cerrada: los nombres de tipos, de espacios de nombres y **el del cuarto puerto** están fijados y validados en el punto de control de la etapa `a`. Sin esto los dobles de puerto se escriben contra un nombre que va a cambiar.
- [ ] **Las guardas de `GeometriaFactory-Domain` que la etapa invoca ya existen.** [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Application/Product-Backlog.md) §1.1 lo declara: dentro de cada etapa, el trabajo del nivel 0 va primero.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta.

## 3. Criterios de salida

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca. Gate condicionado mientras el valor siga rotulado [ASUNCIÓN].
- [ ] `TC-04026` da **0** en sus tres recuentos: ninguna prueba de la etapa abrió el almacén real.
- [ ] `TC-04027` sigue dando **1** dependencia saliente y **0** de las prohibidas.
- [ ] `TC-04028` cierra en las dos direcciones sobre las condiciones que la etapa incorporó.
- [ ] `TC-04011` y la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5 cierran sobre las comprobaciones que la etapa toca: **cada una con prueba de su negativa sin base de datos**.
- [ ] Los gates `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06`, `QG-07`, `QG-08`, `QG-09` y `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

## 4. Riesgos de calidad

Alineados con los **seis** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que un caso de uso consulte el almacén por su cuenta y deje de ser probable con dobles | Alto | Media | `TC-04026` y `TC-04027` corren en **cada** etapa, no sólo en la `a`; `QG-04` y `QG-05` bloquean la fusión |
| RQ-02 | Que aparezca un camino que ejerza una capacidad **sin** resolver antes la marca de cambio de contraseña pendiente | **Muy alto** | Media | `TC-04011` como prueba de orden en cada etapa a partir de la `d`; umbral de **100 %** de líneas y ramas en la guarda de autorización; criterio 5 de la Definition of Ready |
| RQ-03 | Que la negativa por pertenencia y la negativa por facultad se confundan, y un trabajo ajeno responda «no autorizado» | Alto | Media | `TC-04012`, que verifica la indistinguibilidad **en los dos sentidos** contra la tabla de traducciones prohibidas de `03` §2.4 |
| RQ-04 | Que un caso de uso reparta su efecto entre dos unidades de trabajo y la baja deje trabajos huérfanos | Alto | Baja | `TC-04029`, con la baja de cuenta como caso testigo y el recuento de aperturas instrumentado |
| RQ-05 | Que el consumidor trate el resultado tipado como excepción y descarte los rechazos | Medio | Media | `TC-04031` verifica que ninguna de las 36 condiciones lance; el efecto sobre el consumidor se verifica en `GeometriaFactory-Api` |
| RQ-06 | Que el nombre del cuarto puerto se fije sin punto de control y haya que renombrar los dobles | Bajo, de retrabajo | Alta | Criterio de entrada de §2: `BT-04002` cerrada **antes** de escribir los casos de prueba que usan ese doble |
| RQ-07 | **Que un escenario del intake §20 se sustituya por un resultado de interpretación inventado** «porque es más cómodo de armar» | Alto | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-04015` a `TC-04017` y de `TC-04022` |
| RQ-08 | **Que la matriz de cobertura quede desactualizada** y siga diciendo `Pendiente` con pruebas ya escritas | Medio | Alta, es el anti-patrón más común de la categoría | Criterio de salida de §3: la matriz se actualiza al cerrar cada etapa, y su desactualización bloquea el cierre |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-04001 Esqueleto ambulante y verificación de viabilidad | Ninguna capacidad funcional. Se ponen en pie las pruebas de inspección estructural y la batería vacía | `TC-04026`, `TC-04027` | Batería que corre; `QG-01`, `QG-02`, `QG-04` y `QG-05` medidos por primera vez; `BT-04002` cerrada con el nombre del cuarto puerto |
| `c` | EP-04002 Identidad del administrador y sesión | Configuración del administrador, admisibilidad con su motivo y reemplazo de credencial | `TC-04003`, `TC-04008`, `TC-04009`, `TC-04031` | Matriz con `CU-04003` y `CU-04010` cerrados; primera medición de `QG-11` |
| `d` | EP-04003 Ciclo de vida de la cuenta de alumno | Auto-registro, las cuatro operaciones de admisión, la provisoria, el reseteo y **la comprobación transversal de la marca** | `TC-04001`, `TC-04002`, `TC-04004`, `TC-04005`, `TC-04006`, `TC-04007`, `TC-04010`, `TC-04011`, `TC-04029` | `INV-09` ejercido con la prueba de orden; la baja como caso testigo de la unidad de trabajo; `BT-04018` cerrada o elevada |
| `e` | EP-04004 Gestión del trabajo | Constitución y reedición del trabajo, las dos consultas con su predicado y la eliminación en sus dos alcances | `TC-04012`, `TC-04013`, `TC-04014`, `TC-04020`, `TC-04021`, `TC-04022`, `TC-04025`, `TC-04030` | Matriz con `CU-04004`, `CU-04006`, `CU-04007` y `CU-04009` cerrados; `QG-09` medido |
| `f` | EP-04005 Interpretación y verificación del dato del alumno | El envío por el puerto, con los **ocho** escenarios del intake como resultado de interpretación, y la terminación controlada | `TC-04015`, `TC-04016`, `TC-04017`, `TC-04018`, `TC-04019`, `TC-04028` | Los ocho escenarios ejercitados; catálogo de **36** condiciones cerrado en las dos direcciones; `BT-04019` mide los 500 ms sobre `E-1` |
| `h` | EP-04006 Desenlace de la entrega | Aprobar y rechazar desde `Pendiente`, con terminalidad, y la lectura del desenlace por el alumno | `TC-04023`, `TC-04024`, y reejecución de `TC-04022` y `TC-04025` | Matriz completa: 11 de 11 casos de uso, 16 de 16 reglas, 4 de 4 comprobaciones y 9 de 9 invariantes |

**La suma cubre los treinta y un casos de prueba.** `TC-04022` y `TC-04025` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido, que es cuando el comentario y la terminalidad se pueden verificar de verdad.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código. **No hay base de datos que preparar**, y el umbral de pruebas que la tocan es 0 |
| Datos | Los **ocho** escenarios del intake §20, en la forma que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara; los **cuatro** dobles de puerto y los **cuatro** fixtures compartidos de su §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a` |
| Guiones | `scripts/build.sh` y `scripts/test.sh`, que son los que el intake §17.2.P.8 declara como puertas |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance del plan sobre las **seis** etapas que este proyecto de código toca, con las dos que no lo tocan declaradas y con la constancia de que la batería de integración es de `GeometriaFactory-Api`; **seis** criterios de entrada y **once** de salida, todos verificables; **ocho** riesgos de calidad alineados con los seis riesgos arquitectónicos de `05` §9 más dos propios de la categoría; el plan por etapa con los treinta y un casos de prueba repartidos y **sin fechas ni duraciones**, porque el intake declara sin plazo calendario; y los recursos, con la constancia de que el equipo es de una sola persona y de que no hay base de datos que preparar. |
