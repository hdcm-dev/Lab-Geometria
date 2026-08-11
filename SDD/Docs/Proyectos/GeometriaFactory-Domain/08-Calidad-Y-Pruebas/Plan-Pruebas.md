# Plan de pruebas — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Plan-Pruebas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.0; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) 1.1 §2; [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md)
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

**Qué cubre.** Los **veintisiete** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** La interpretación del texto del alumno y la tolerancia de formato, en `GeometriaFactory-Infrastructure`; el transporte de los datos por la frontera de servicio, en `GeometriaFactory-Contracts`; el dibujo, en `GeometriaFactory-Visor`; los recorridos de punta a punta del producto, en `GeometriaFactory-Api` y `GeometriaFactory-Web`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas», y el producto no tiene sprints ([`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §4.1). Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**: sería un plazo que ninguna fuente da.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: no producen épica en este proyecto de código, porque no tocan entidades, invariantes ni transiciones.

## 2. Criterios de entrada

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo ([`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §5).
- [ ] Las historias de la etapa cumplen los **seis** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluido el de tener criterios de aceptación en Given/When/Then.
- [ ] Toda condición de rechazo que las historias de la etapa producen **ya existe** en el catálogo de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md), o su alta está comprometida bajo la excepción de la DoR §3.
- [ ] `BT-01` está cerrada: el proyecto de código y su proyecto de pruebas existen y la batería corre, aunque sea vacía.
- [ ] `BT-02` está cerrada: los nombres de tipos y de espacios de nombres están fijados y validados en el punto de control de la etapa `a`. Sin esto ningún caso de prueba se puede escribir sin retrabajo.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta.

## 3. Criterios de salida

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca. Gate condicionado mientras el valor siga rotulado [ASUNCIÓN].
- [ ] `TC-23` cierra en las dos direcciones sobre las condiciones que la etapa incorporó.
- [ ] `TC-26` cierra sobre los invariantes que la etapa toca: **cada uno con prueba de violación rechazada y sin dobles**.
- [ ] Los gates `QG-01`, `QG-02`, `QG-04`, `QG-05`, `QG-06` y `QG-08` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15).

## 4. Riesgos de calidad

Alineados con los **cinco** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que una dependencia se cuele en el nivel 0 y el dominio deje de ser probable sin infraestructura | Alto | Media | `TC-24` corre en cada etapa, no sólo en la `a`; `QG-04` bloquea la fusión |
| RQ-02 | Que un invariante se ejerza en un componente y no en otro, y quede una puerta por la que se lo saltea | Alto | Media, **con precedente registrado**: la familia se abrió dos veces | `TC-05` como prueba de regresión de esa familia; `TC-26` sobre los nueve invariantes; umbral de cobertura de 100 % de ramas en el evaluador de admisibilidad |
| RQ-03 | Que el consumidor trate el resultado tipado como excepción y descarte los rechazos | Medio | Media | `TC-27` verifica que ninguna condición prevista lance; el efecto sobre el consumidor se verifica en `GeometriaFactory-Application` |
| RQ-04 | Que alguna operación lea el reloj por comodidad y rompa la reproducibilidad | Medio | Baja | `TC-25`, con la comparación de dos ejecuciones consecutivas sin fijar el reloj |
| RQ-05 | Que los nombres abiertos se fijen sin punto de control y haya que renombrar | Bajo, de retrabajo | Media | Criterio de entrada de §2: `BT-02` cerrada antes de escribir casos de prueba |
| RQ-06 | **Que un escenario del intake §20 se sustituya por un dato sintético** «porque es más cómodo de escribir» | Alto | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-13` a `TC-18` |
| RQ-07 | **Que la matriz de cobertura quede desactualizada** y siga diciendo `Pendiente` con pruebas ya escritas | Medio | Alta, es el anti-patrón más común de la categoría | Criterio de salida de §3: la matriz se actualiza al cerrar cada etapa, y su desactualización bloquea el cierre |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-01 Esqueleto ambulante | Ninguna capacidad funcional. Se ponen en pie las pruebas de inspección estructural y la batería vacía | `TC-24` | Batería que corre; `QG-01`, `QG-02` y `QG-04` medidos por primera vez |
| `c` | EP-02 Identidad del administrador y sesión | Configuración del administrador, admisibilidad y reemplazo de credencial | `TC-06`, `TC-08`, `TC-10`, `TC-27` | Matriz con `CU-03`, `CU-04` y `CU-12` cerrados |
| `d` | EP-03 Ciclo de vida de la cuenta de alumno | Alta, ciclo de vida, provisoria, reseteo y marca | `TC-01`, `TC-02`, `TC-03`, `TC-04`, `TC-05`, `TC-07`, `TC-09`, `TC-25`, `TC-26` | `INV-09` ejercido en la puerta única; `BT-15` y `BT-16` cerradas o elevadas |
| `e` | EP-04 Gestión del trabajo | Constitución del trabajo, acceso del alumno y alcance del administrador | `TC-11`, `TC-12`, `TC-20`, `TC-21` | Matriz con `CU-05`, `CU-09` y `CU-11` cerrados |
| `f` | EP-05 Interpretación y verificación | Adopción del conjunto de piezas y de las observaciones, y envío | `TC-13`, `TC-14`, `TC-15`, `TC-16`, `TC-17`, `TC-18`, `TC-19`, `TC-23` | Los **ocho** escenarios del intake ejercitados; catálogo de **42** condiciones cerrado en las dos direcciones |
| `h` | EP-06 Desenlace de la entrega | Aprobar y rechazar desde `Pendiente`, con terminalidad, y eliminación por el administrador | `TC-22`, y reejecución de `TC-19` y `TC-21` | Matriz completa: 13 de 13 casos de uso, 16 de 16 reglas y 9 de 9 invariantes |

**La suma cubre los veintisiete casos de prueba.** `TC-19` y `TC-21` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido, que es cuando la terminalidad se puede verificar de verdad.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código. No hay ambiente desplegado que preparar |
| Datos | Los **ocho** escenarios del intake §20, en la forma que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara; y los cuatro fixtures de entidad de su §5 |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a` |
| Guiones | `scripts/build.sh` y `scripts/test.sh`, que son los que el intake §17.1.P.8 declara como puertas |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance del plan sobre las **seis** etapas que este proyecto de código toca, con las dos que no lo tocan declaradas; **seis** criterios de entrada y **nueve** de salida, todos verificables; **siete** riesgos de calidad alineados con los cinco riesgos arquitectónicos de `05` §9 más dos propios de la categoría; el plan por etapa con los veintisiete casos de prueba repartidos y **sin fechas ni duraciones**, porque el intake declara sin plazo calendario; y los recursos, con la constancia de que el equipo es de una sola persona. |
