# Plan de pruebas — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Plan-Pruebas.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.1; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.1; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) 1.1 §2; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §9; [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md)
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

**Qué cubre.** Los **veintidós** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **siete** etapas comprometidas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f`, `g` y `h`—, más `TC-11`, que cae en la fase `i…` y **no se compromete**.

**La etapa `b` no aparece**, y es declaración y no olvido: construye la cáscara del front con pantallas de marcador de posición y no hay ningún dato que cruce la frontera todavía.

**Qué no cubre, y dónde se cubre.** Las reglas de negocio, en `GeometriaFactory-Domain`; la interpretación del texto, en `GeometriaFactory-Infrastructure`; los puntos de acceso del servicio y su batería de integración **como infraestructura de prueba**, en `GeometriaFactory-Api`; la presentación, en `GeometriaFactory-Web`.

**La unidad de planificación es la etapa y no el sprint**, y **ninguna fila de §5 lleva fecha ni duración**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

**Una asimetría de este plan que conviene decir de entrada.** Las inspecciones de superficie se pueden ejecutar desde la etapa `c`, apenas la familia existe. Las pruebas de integración **no**: dependen de que `GeometriaFactory-Api` esté levantado, y ese proyecto de código es de nivel topológico 3. En consecuencia, cada fila de §5 declara **qué se puede verificar ya** y **qué queda pendiente de la batería de integración**.

## 2. Criterios de entrada

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los **siete** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluidos el sexto —refinada contra la regla de exposición— y el séptimo —todo código de error pertenece al conjunto cerrado—.
- [ ] `BT-01` está cerrada: el ensamblado compila y **no declara ninguna biblioteca de serialización**.
- [ ] `BT-04` está cerrada para la familia de la etapa: sus nombres de tipos, de campos y de espacios de nombres están decididos y registrados en el punto de control de esa etapa.
- [ ] `BT-06` y `BT-07` están cerradas antes de cualquier etapa posterior a la `c`: el tipo de error existe con sus cuatro campos y el conjunto cerrado de **diecisiete** códigos está declarado. **Las siete familias restantes dependen de la familia de error**.
- [ ] Para las filas que exigen integración: el servicio real levanta y la batería de `GeometriaFactory-Api` corre.

## 3. Criterios de salida

- [ ] Todos los `TC-XX` de inspección de superficie en alcance de la etapa están ejecutados y en verde.
- [ ] Todos los `TC-XX` de integración en alcance están ejecutados y en verde, **o declarados diferidos con el motivo «la batería de integración todavía no existe»** y con la etapa en que se ejecutan.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita.
- [ ] Los cinco gates de superficie —`QG-02`, `QG-03`, `QG-04`, `QG-07` y `QG-09`— pasan sobre las familias que la etapa tocó.
- [ ] `QG-01` pasa: el ensamblado compila sin advertencias.
- [ ] Si la etapa cambió el conjunto cerrado de códigos, de estados, de papeles, de situaciones, de severidades o de desenlaces, **el cambio está declarado como incompatible** en el `§17` del contrato de uso afectado y el despliegue conjunto está previsto (`QG-08`).
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner.

## 4. Riesgos de calidad

Alineados con los **seis** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que aparezca una referencia hacia `GeometriaFactory-Domain` y el acoplamiento vuelva por esa vía | Alto | Media | `TC-20` en **cada** etapa, no sólo en la `a`; `QG-02` bloquea la fusión |
| RQ-02 | **Que un campo nuevo transporte una dirección de servicio o una traza, sin que nadie lo note porque compila** | Alto | Media, y es la forma habitual en que este defecto entra | `TC-15`, más la cadencia de revisión **por cambio de superficie** de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §5 |
| RQ-03 | Que el listado incorpore un campo del detalle «porque hace falta en una pantalla» | Medio | **Alta**: es la presión natural de la capa de presentación | `TC-09`, con sus tres recuentos en 0, ejecutado en cada etapa que toque la familia de listado |
| RQ-04 | Que un identificador de código retirado se recicle para otra condición | Medio | Baja, **pero con precedente**: ya hay tres retirados | `TC-16`, que compara los diecisiete vivos contra los tres retirados |
| RQ-05 | Que una de las dos unidades desplegables se despliegue sin la otra tras un cambio incompatible | Alto | Media | `QG-08` y el criterio de salida correspondiente de §3 |
| RQ-06 | Que aparezca un tipo pensado para que el navegador invoque el servicio de datos | Alto | Baja | `TC-22` |
| RQ-07 | **Que las pruebas de integración se difieran indefinidamente** porque dependen de un proyecto de código de nivel 3 | Alto: el gate de 100 % de tipos ejercitados quedaría sin medir hasta el final | **Alta**, por construcción del orden topológico | Cada fila de §5 declara qué queda diferido y en qué etapa se ejecuta; el criterio de salida exige declararlo por escrito y no callarlo |
| RQ-08 | **Que un caso de prueba de este proyecto de código verifique una regla de negocio** en lugar de verificar qué transporta el contrato de ella | Medio: confunde titularidades y duplica lo que ya verifica `GeometriaFactory-Domain` | Media | La DoR §1 criterio 5 lo prohíbe en la historia, y [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §4 lo declara explícitamente en su cierre |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Qué se verifica ya | Qué queda pendiente de la batería de integración |
| --- | --- | --- | --- |
| `a` | EP-01 Esqueleto ambulante | `TC-20`: cero referencias hacia el dominio. `QG-01` medido por primera vez | Todo lo demás: no hay tipos todavía |
| `c` | EP-02 Identidad del administrador y sesión | `TC-01` en su tramo de inspección, `TC-15`, `TC-16`, `TC-22` | `TC-02`, y el tramo de integración de `TC-01` y `TC-15` |
| `d` | EP-03 Ciclo de vida de la cuenta de alumno | `TC-19` en su tramo de inspección, `TC-04` en su tramo de inspección | `TC-03`, `TC-05`, `TC-06`, y los tramos de integración de `TC-04` y `TC-19` |
| `e` | EP-04 Gestión del trabajo | `TC-08` y `TC-09` en sus tramos de inspección | `TC-07`, `TC-10`, y los tramos de integración de `TC-08` y `TC-09`. **`TC-11` no entra acá**: cae en la fase `i…` |
| `f` | EP-05 Interpretación y verificación | `TC-13` y `TC-14` en sus tramos de inspección | `TC-12`, `TC-17`, y los tramos de integración de `TC-13` y `TC-14` |
| `g` | EP-06 Visualización del trabajo | Nada nuevo de superficie: la familia de detalle ya existe | Reejecución de `TC-12` verificando que el texto original que el árbol despliega sigue llegando íntegro |
| `h` | EP-07 Desenlace de la entrega | `TC-18` en su tramo de inspección | El tramo de integración de `TC-18` y la reejecución de `TC-14` |
| `i…` | EP-08 Capacidades de prioridad menor | — | `TC-11`. **Fuera del tramo comprometido**, se declara y no se compromete |
| Al cerrar cada etapa | — | `TC-21`: la matriz tipo contra prueba, con lo ejercitado hasta ese momento | — |

**La suma cubre los veintidós casos de prueba.** `TC-21` aparece en todas las etapas porque es la medición acumulativa del gate equivalente a la cobertura.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` |
| Ambiente | El contenedor de desarrollo para las inspecciones; el servicio real levantado, con lo que `GeometriaFactory-Api` exija, para la integración |
| Datos | Los cuerpos del sample **S-2** del intake §18 —con los escenarios `E-2` y `E-5`— y los otros seis escenarios de §20, en la forma que [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 declara |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función; y las dos comprobaciones reproducibles que `03` §3 ya publica |
| Guiones | `scripts/build.sh` para la construcción; `scripts/test.sh` para la batería de integración, que **no es de este proyecto de código** |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance sobre las **siete** etapas comprometidas que este proyecto de código toca, con la `b` declarada ausente y la fase `i…` declarada fuera del compromiso; **seis** criterios de entrada y **nueve** de salida; **ocho** riesgos de calidad alineados con los seis riesgos arquitectónicos de `05` §9 más dos propios —el diferimiento indefinido de la integración y la confusión de titularidad con las reglas de negocio—; y el plan por etapa **sin fechas ni duraciones**, con la asimetría declarada entre lo que se verifica ya por inspección y lo que queda pendiente de una batería que vive en un proyecto de código de nivel topológico 3. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
