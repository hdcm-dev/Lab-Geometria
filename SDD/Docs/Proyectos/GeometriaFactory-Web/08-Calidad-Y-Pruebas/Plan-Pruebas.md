# Plan de pruebas — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Plan-Pruebas.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.1; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.1; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 y §3; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md); [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md)
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

**Qué cubre.** Los **treinta y cinco** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) y las **61** filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md), repartidos entre las **ocho** etapas comprometidas del producto —`a` a `h`—. **Este es el único de los siete proyectos de código del producto que produce épica en las ocho**, y así lo declara [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2.

**Qué no cubre, y dónde se cubre.** Las reglas de negocio y su cumplimiento, en `GeometriaFactory-Domain` y en las capas que las ejercen; la batería de integración contra el servicio de datos real, en `GeometriaFactory-Api`; el interior del bundle y sus siete condiciones, en `GeometriaFactory-Visor`; la interpretación del texto, en `GeometriaFactory-Infrastructure`.

**Y algo que no cubre y conviene decir aparte:** esta pieza **no verifica que una regla se cumpla**, porque no la hace cumplir (`02` §5). Lo que verifica de cada acotación es **que forzar la solicitud sin pasar por la pantalla la reciba rechazada del otro lado**. Seis casos de verificación existen sólo para eso.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

## 2. Criterios de entrada

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los **ocho** criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluidos el 4 —superficie declarada—, el 6 —toda condición es uno de los **quince** códigos vivos o el camino de ausencia de respuesta— y el 7 —ninguna afirmación depende de que esta pieza haga cumplir una regla—.
- [ ] **`PT-01` está medida en sus cuatro partes** y su resultado registrado. El intake §15 la ubica en la etapa `a`, **antes que cualquier otra cosa**: sin ella el modelo de front no está confirmado.
- [ ] **Antes de la etapa `g`: `PT-02` y `PT-03` están medidas.** Una puerta que no pasa **detiene la planificación de la etapa que depende de ella** y no se arrastra como deuda.
- [ ] El servicio de datos está levantado desde el contenedor de desarrollo, a partir de la etapa `c`. El guion no se ejecuta contra un doble.
- [ ] El bundle del visor **generado en el flujo y no tomado de un artefacto viejo**, a partir de la etapa `g`.
- [ ] Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están identificadas, con el método resuelto que declara [`Estrategia-Testing.md`](Estrategia-Testing.md) §8.1.

## 3. Criterios de salida

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están ejecutados y pasan.
- [ ] **El guion de la etapa y los de todas las anteriores pasan al 100 %** (`TC-35`). Es la regla de no-regresión acumulativa del intake §15, que **no es asunción**.
- [ ] Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, **con estado y fecha actualizados**.
- [ ] **Ninguna deriva mayor queda sin resolver.** Se corrige lo construido, o se actualiza la línea de base con aprobación humana explícita. **Nunca por omisión.**
- [ ] Las cinco inspecciones estructurales —`TC-29` a `TC-33`— dan **0** en cada uno de sus recuentos, en la condición declarada.
- [ ] Los seis casos que verifican **forzando la solicitud** —`TC-01`, `TC-05`, `TC-07`, `TC-15`, `TC-25`, `TC-26`— se ejecutaron para las acotaciones que la etapa introdujo.
- [ ] Los gates `QG-01`, `QG-02`, `QG-03`, `QG-05`, `QG-06`, `QG-07`, `QG-08`, `QG-09`, `QG-10` y `QG-11` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] `QG-04` **se cumple**: el guion de la etapa y los de todas las anteriores pasan al 100 %. Es **bloqueante**, no condicionado (ver [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.1).
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

## 4. Riesgos de calidad

Alineados con los **siete** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más tres propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que aparezca un guion del navegador que llame al servicio de datos | **Muy alto** | Media | `TC-29` y `TC-30` en **cada** etapa, con el conteo hecho **con los dos movimientos prendidos**; `QG-05` y `QG-06` bloquean la fusión |
| RQ-02 | Que el proceso del hosting recicle y la persona pierda la sesión en mitad de un acto | Alto | Media, y **medida**: es `PT-01.c` | No hay mitigación técnica que inventar. Lo que hay es tratamiento verificado: `TC-27` ejerce el estado «sesión no restablecible», y el envío como **única** acción de guardado hace que un corte no deje un trabajo a medias |
| RQ-03 | Que un mensaje mostrado lleve una dirección de servicio, una ruta de datos o una traza | Alto | Media, porque entra por el camino de excepción | `TC-31` recorre los **quince** códigos **y** el camino de ausencia de respuesta, sobre el traductor, que es el único punto por el que un mensaje llega a la persona |
| RQ-04 | Que un componente termine tocando el interior del bundle porque la fachada no expone algo | Alto | Media | `TC-32` en cada etapa a partir de la `g`; y el procedimiento del `Visor` para cuando falta algo en la fachada, que **no es tocar el interior** |
| RQ-05 | Que la liberación de la instancia no se invoque y recorrer trabajos acumule contextos gráficos | Alto | Media, porque es la clase de omisión que no falla la primera vez | `TC-21` como puerta `PT-02`, medida **antes de comprometer la etapa `g`** y reejecutada al cerrarla |
| RQ-06 | Que una subida deje la aplicación caída y se reporte como exitosa | Alto | Media, porque la subida **no es transaccional** | `QG-03`: el flujo **no termina en la subida**, termina comprobando que la dirección pública responde |
| RQ-07 | Que un listado incorpore un campo del detalle y arrastre el texto completo de cada trabajo | Medio | Alta | `TC-15` y `TC-24` verifican la forma del listado; la proyección separada es decisión de `GeometriaFactory-Contracts` y esta pieza la consume sin invertirla |
| RQ-08 | **Que una acotación se dé por verificada mirando que el control no se dibuja**, sin forzar la solicitud | **Muy alto**: es la forma exacta en que una regla se cree cumplida y no lo está | Alta, porque es lo cómodo | Criterio de salida de §3: los **seis** casos que fuerzan la solicitud se ejecutan para toda acotación que la etapa introduce. La Definition of Ready §1 criterio 7 lo exige desde la entrada |
| RQ-09 | **Que una deriva mayor se resuelva por omisión**, dejando la fila en `Sin verificar` y siguiendo | Alto: la línea de base deja de ser línea de base | Media | Criterio de salida de §3 y `QG-11`: ninguna deriva mayor queda sin resolver, y la decisión —corregir o actualizar la línea de base— es del Product Owner con constancia escrita |
| RQ-10 | **Que el guion se ejecute sólo para la etapa en curso** y la regla acumulativa se erosione | Alto: es la única red de seguridad de regresión que este proyecto de código tiene | Alta, porque el guion crece en cada etapa y ejecutarlo entero se vuelve caro | `TC-35` es acumulativo por definición y su criterio de salida lo exige. **La regla acumulativa no es la parte rotulada [ASUNCIÓN]**: lo rotulado es expresarla como puerta con umbral del 100 % |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1. `TC-35` aparece en **todas** las etapas porque es acumulativo.

| Etapa | Épica | Alcance de testing | Casos de verificación en alcance | Filas de la matriz de sensado | Entregable de esta categoría |
| --- | --- | --- | --- | --- | --- |
| `a` | EP-01 Esqueleto ambulante y verificación de viabilidad | Las **cuatro** mediciones de `PT-01` y la inspección de la única salida | `TC-34`, `TC-30`, `TC-35` | Ninguna: todavía no hay superficie construida | `PT-01` medida y registrada, con la salida declarada si alguna parte no pasa |
| `b` | EP-02 Navegación y sistema visual | Los **dos** shells, el mapa de rutas y las **once** superficies con marcador de posición | `TC-05`, `TC-29`, `TC-35` | `SD-01` a `SD-11`, `SD-23` a `SD-27`, `SD-54` a `SD-56`, `SD-59` a `SD-61` | Las once superficies sensadas contra la maqueta; el primer conteo de peticiones del navegador |
| `c` | EP-03 Identidad del administrador y sesión | Aprovisionamiento, ingreso con la credencial custodiada, cambio de contraseña y estado degradado | `TC-01`, `TC-03`, `TC-04`, `TC-06`, `TC-27`, `TC-28`, `TC-31`, `TC-35` | `SD-14`, `SD-15`, `SD-16`, `SD-22`, `SD-57`, `SD-58` | **0 apariciones de la credencial en el navegador**, criterio de aceptación de esta etapa; los quince códigos traducidos |
| `d` | EP-04 Ciclo de vida de la cuenta de alumno | Registro, panel de cuentas con sus cinco operaciones, provisoria comunicada y confinamiento | `TC-02`, `TC-07`, `TC-08`, `TC-09`, `TC-10`, `TC-35` | `SD-09`, `SD-13`, `SD-19`, `SD-28`, `SD-35` | El cuarto guardián verificado **forzando la solicitud**; las operaciones de `F-26` verificadas contra `CU-03` y `CU-04`, **sin sonda propia** |
| `e` | EP-05 Gestión del trabajo | Carga con el texto intacto, listado propio y listado de la comisión | `TC-11`, `TC-15`, `TC-24`, `TC-26`, `TC-35` | `SD-05`, `SD-10`, `SD-21`, `SD-29`, `SD-34`, `SD-36` | Comparación carácter por carácter del texto de `E-2`; indistinguibilidad verificada forzando |
| `f` | EP-06 Interpretación y verificación del dato del alumno | Previsualización que dibuja y no verifica, y presentación de advertencias y errores | `TC-12`, `TC-13`, `TC-14`, `TC-35` | `SD-06`, `SD-17`, `SD-30`, `SD-33`, `SD-37`, `SD-38` | Los escenarios `E-1`, `E-3`, `E-5` y `E-8` ejercitados; **exactamente dos** advertencias en `E-1` |
| `g` | EP-07 Visualización del trabajo | La vista de trabajo con sus cuatro elementos, el árbol, la sincronización por índice y el gobierno del movimiento | `TC-17`, `TC-18`, `TC-19`, `TC-20`, `TC-21`, `TC-22`, `TC-23`, `TC-32`, `TC-33`, `TC-35` | `SD-07`, `SD-18`, `SD-31`, `SD-39` a `SD-53` | `PT-02` y `PT-03` pasadas **antes** de comprometer la etapa; los escenarios `E-6` y `E-7` ejercitados |
| `h` | EP-08 Desenlace de la entrega | El desenlace en el listado propio, la resolución con comentario opcional y el retiro | `TC-16`, `TC-25`, `TC-35`, y reejecución de `TC-24` y `TC-26` | `SD-08`, `SD-20`, `SD-26`, `SD-27` | Matriz completa: 10 de 10 casos de uso, 13 de 13 restricciones, 61 de 61 sondas verificadas |

**La suma cubre los treinta y cinco casos de verificación y las sesenta y una filas.** `TC-24` y `TC-26` aparecen dos veces porque la etapa `h` los reejecuta con el desenlace ya construido; `TC-35` aparece en las ocho porque es acumulativo por definición.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la verificación observada y la aprobación |
| Ambientes | **Dos, y no son intercambiables**: el contenedor de desarrollo con el navegador del equipo anfitrión para el guion, y el **hosting público** para `PT-01`, porque lo que esa puerta mide son las capacidades de ese hosting |
| Servicio de datos | Levantado y **real** a partir de la etapa `c`. Su indisponibilidad se provoca deteniéndolo, no simulándola |
| Datos | Los **ocho** escenarios del intake §20 **en su forma original y completa**; y los datos de maqueta de [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md), con la salvedad de los **valores compuestos para la maqueta que no viajan al producto** |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función: panel de herramientas de desarrollo, lector de pantalla, medición de contraste y un cliente de peticiones para forzar la solicitud |
| Línea de base | [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md), [`../03-UX-UI-DX/Contrato-Datos-Maqueta.md`](../03-UX-UI-DX/Contrato-Datos-Maqueta.md) y [`../03-UX-UI-DX/Bitacora-Validacion-Maqueta.md`](../03-UX-UI-DX/Bitacora-Validacion-Maqueta.md), aprobados por el Product Owner |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** El criterio de salida que pedía que `QG-04` «se midiera y se registrara aunque sea condicionado» pasa a exigir que **se cumpla**: el gate es bloqueante. Ningún caso ni umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance del plan sobre las **ocho** etapas comprometidas —este es el único proyecto de código del producto que produce épica en todas—, con la constancia de que esta pieza **no verifica que una regla se cumpla** sino que forzar la solicitud la recibe rechazada del otro lado. Declara **siete** criterios de entrada —incluidas las tres puertas técnicas en su momento— y **once** de salida, todos verificables; **diez** riesgos de calidad alineados con los siete riesgos arquitectónicos de `05` §9 más tres propios, entre ellos el de dar una acotación por verificada mirando la pantalla y el de resolver una deriva mayor por omisión; el plan por etapa con los treinta y cinco casos de verificación **y las sesenta y una filas de la matriz de sensado** repartidas, **sin fechas ni duraciones**; y los recursos, con los **dos** ambientes que no son intercambiables. |
