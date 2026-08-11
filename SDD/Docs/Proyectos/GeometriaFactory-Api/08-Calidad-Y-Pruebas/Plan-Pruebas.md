# Plan de pruebas — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Plan-Pruebas.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.1; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 y §3; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md); [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md)
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

**Qué cubre.** Los **treinta y siete** casos de verificación de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **seis** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e`, `f` y `h`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §2 declara como sus épicas.

**Y cubre algo más, que conviene declarar aparte porque no es sólo de este proyecto de código: la batería de integración del producto.** El intake §17.5.P.6 declara que `GeometriaFactory.Integration.Tests` golpea la superficie real por su protocolo contra el almacén real, y §17.3.P.6 le asigna **la persistencia real** de `GeometriaFactory-Infrastructure`. Esa batería vive acá y este plan la planifica.

**Qué no cubre, y dónde se cubre.** Las reglas del dominio y sus invariantes, en `GeometriaFactory-Domain`; la orquestación y las cuatro comprobaciones de autorización sobre el dato, en `GeometriaFactory-Application`; la interpretación del texto y los mecanismos de seguridad, en `GeometriaFactory-Infrastructure`; el recorrido de la persona, en `GeometriaFactory-Web`; el dibujo, en `GeometriaFactory-Visor`.

**Y una cosa que este plan explícitamente no planifica: el despliegue.** El intake §17.5.P.8 lo declara **manual, por el docente**, y que el agente **entrega el archivo de construcción y el de composición y no ejecuta el despliegue**. Lo que sí se verifica es que el artefacto se construya, arranque y responda.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

**Las etapas `b` y `g` no aparecen en el plan**, y es declaración y no olvido: `../06-Backlog-Tecnico/Product-Backlog.md` §2 lo fundamenta. La `b` no agrega ningún punto de acceso, y **todo lo que la `g` necesita de esta superficie ya está expuesto en la `e`**.

## 2. Criterios de entrada

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1, incluida la declaración del punto de acceso que ejercen y la de que **ninguna acuña, renombra ni traduce a texto un código del contrato**.
- [ ] **Las capas de adentro que la etapa consume ya emitieron lo que esta superficie expone.** Es el proyecto de código de nivel 3: un caso de uso que no exista en `GeometriaFactory-Application` no se puede exponer acá.
- [ ] **`PT-04` está medida en la etapa `a`** y su resultado registrado.
- [ ] **Todo punto de acceso nuevo de la etapa está declarado como dentro de la guardia, o como una de las cuatro exenciones con su motivo.** Sin esa declaración `TC-07` no puede correr.
- [ ] Los **ocho** textos literales de los escenarios del intake §20 están cargados, **sin ninguna modificación**.
- [ ] El contenedor de desarrollo levanta, `scripts/test.sh` corre de punta a punta y la batería de integración levanta el proceso contra un almacén efímero.

## 3. Criterios de salida

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están ejecutados y pasan.
- [ ] **`TC-07` cierra con 4 y 11 sobre los quince puntos, en las dos direcciones**, y ningún punto nuevo de la etapa quedó fuera de la guardia sin exención declarada.
- [ ] **`TC-25` da 3 de 3 comparaciones idénticas**, y ninguna familia empobrecida se enriqueció al agregar un punto.
- [ ] `TC-24` y `TC-27` cierran en las dos direcciones sobre los códigos que la etapa incorporó, con **0** inventados y **0** renombrados.
- [ ] **`TC-26` da 0 exposiciones** sobre las respuestas de fallo de los puntos que la etapa toca, y el registro del servidor los tiene todos.
- [ ] **La batería del validador que corre desde acá pasa entera: 10 de 10**, a partir de la etapa `f`.
- [ ] Todos los NFR con umbral **cero** que la etapa toca se midieron **en la condición declarada**, y no se dieron por cumplidos por no haberse observado lo contrario.
- [ ] Los gates `QG-01`, `QG-02`, `QG-05`, `QG-06`, `QG-07`, `QG-08`, `QG-09`, `QG-10`, `QG-11`, `QG-12` y `QG-15` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] Los gates condicionados —`QG-03`, `QG-04`, `QG-13`, `QG-14`— **se midieron y se registraron**.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, **incluida su tabla de quince puntos**.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

## 4. Riesgos de calidad

Alineados con los **nueve** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que un punto de acceso nuevo quede **fuera de la guardia** del cambio de contraseña pendiente | **Muy alto**: `RN-13` e `INV-09` dejan de valer **y nada falla** | **Alta**: es un defecto de **omisión**, y los defectos de omisión **no se ven leyendo el punto nuevo** | `TC-07` en **cada** etapa, recorriendo los quince en las dos direcciones; y el criterio de entrada de §2 que exige declarar la ubicación de todo punto nuevo **antes** de construirlo |
| RQ-02 | Que el trabajo ajeno responda «no autorizado» en lugar de «no encontrado» | **Muy alto**: permite averiguar por tanteo qué identificadores existen, y **ninguna capa de adentro puede repararlo** | Media: es la traducción que parece más informativa y por eso es la tentadora | `TC-25` con sus **3 de 3** comparaciones, ejecutado en cada etapa que agrega una respuesta de fallo |
| RQ-03 | Que el límite de tamaño del cuerpo **trunque** el texto de un alumno en lugar de rechazarlo | Alto: **rompe `RN-08` en silencio** y el alumno lo descubre al ver el dibujo | Media: truncar es el comportamiento por defecto de varias capas de transporte | `TC-19`, con comparación byte a byte y con el caso del cuerpo por encima del límite **rechazado y no truncado** |
| RQ-04 | Que los dos extremos serialicen distinto y el contrato deje de ser el mismo | Alto: el fallo aparece en tiempo de ejecución y **no lo detecta la compilación**, que es la única red del producto | Media, y es un trade-off aceptado por escrito aguas arriba | `TC-29`, con **1** sola configuración declarada; y la batería de integración golpeando el servicio real |
| RQ-05 | Que un envío cuyo texto no verifica responda con un **código de fallo** | Medio: le diría a la persona que su petición estaba mal cuando lo que pasa es que su programa emitió algo que no se puede interpretar | Media: es la lectura intuitiva de «no verificó» | `TC-17`, con los escenarios `E-1`, `E-5` y `E-8`: **las tres respuestas son exitosas** |
| RQ-06 | Que se agregue un punto pensado para el navegador, o se configure el intercambio de origen cruzado | **Muy alto**: rompe `RA-01`, que es regla de nivel producto | Baja, pero el costo de equivocarse es de **rediseño** | `TC-36`, con sus tres ausencias verificadas, en cada etapa que agrega superficie |
| RQ-07 | Que la composición de raíz deje un puerto sin adaptador y el fallo aparezca en la primera petición | Medio: el servicio arranca y **falla al primer uso, en producción y sin nadie mirando** | Media | `TC-28`, con **fallo en construcción** y no en la primera petición |
| RQ-08 | Que el listado de la comisión crezca por encima de lo que el requerimiento de tiempo sostiene | Medio | Baja en el alcance declarado | `TC-34`, con la **condición de reingreso escrita**: cuando el percentil deje de cumplirse, entra paginación, y es cambio del ensamblado de contratos |
| RQ-09 | Que el mecanismo de construcción de la imagen en destino no funcione y el despliegue quede sin camino | Alto: es el único canal de entrega declarado | Media, **y la fuente lo rotula [A VERIFICAR]** | Probarlo **una vez antes de depender de él**. **No es criterio de esta categoría**: el despliegue es manual y del Product Owner |
| RQ-10 | **Que la batería de integración se dé por suficiente sin las inspecciones de umbral exacto** | Alto: las propiedades más peligrosas de este proyecto de código —los cuatro puntos exentos, los catorce códigos con destino, las tres familias— **no se ven ejerciendo el cable, se ven contándolo** | Media, porque una batería de integración verde da sensación de cobertura | Criterio de salida de §3: las **cinco** inspecciones con umbral exacto se ejecutan aparte y su resultado se registra por separado |
| RQ-11 | **Que la batería del validador se dé por completa con nueve casos**, arrastrando la redacción que el gate del intake tuvo hasta 1.19 | Alto: dejaría `E-8` sin cubrir | **Baja desde el intake 1.20**, que corrigió el gate a **diez**; queda como riesgo vivo sólo por las copias del texto viejo que puedan circular | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 declara el desenlace y fija **diez**; el criterio de salida de §3 lo exige |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de verificación en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-01 Esqueleto ambulante y verificación de viabilidad | La composición de raíz, el arranque en dos fases y el punto de salud; y la ausencia de canal de sesión interactiva | `TC-28`, `TC-29`, `TC-30`, `TC-31`, `TC-32`, `TC-33`, `TC-36` | `PT-04` medida; **4 de 4** puertos con fallo en construcción; **0** peticiones atendidas con la preparación incompleta; las tres ausencias de `RA-01` verificadas |
| `c` | EP-02 Identidad del administrador y sesión | El canje, la guardia sobre los once puntos, los cuatro puntos de acceso y credencial propia, y **las dos traducciones con su tabla única** | `TC-01`, `TC-02`, `TC-03`, `TC-04`, `TC-05`, `TC-07`, `TC-08`, `TC-09`, `TC-10`, `TC-24`, `TC-25`, `TC-26`, `TC-27` | La tabla de traducción cerrada en las dos direcciones; **3 de 3** familias indistinguibles; **4** puntos fuera de la guardia |
| `d` | EP-03 Ciclo de vida de la cuenta de alumno | El gobierno de la comisión, el reseteo y **la guardia del cambio pendiente sobre todos los puntos salvo uno** | `TC-06`, `TC-11`, `TC-12`, `TC-13`, `TC-14`, `TC-15`, `TC-16` | `INV-09` sostenido desde el borde: **diez rechazos y una excepción**; la provisoria devuelta una vez y **0** apariciones en trazas |
| `e` | EP-04 Gestión del trabajo | Los cinco puntos sobre trabajos, con el texto sin normalizar y **la eliminación forzando la petición** | `TC-18`, `TC-19`, `TC-20`, `TC-21`, `TC-22` | **0** eliminaciones fuera de alcance al forzar; **0** caracteres de diferencia y **0** truncamientos; la ausencia verificada del parámetro de borradores ajenos |
| `f` | EP-05 Interpretación y verificación del dato del alumno | El envío y el reenvío, que **responden con éxito** transportando el estado que la interpretación decidió | `TC-17`, `TC-34`, `TC-37` | Los escenarios `E-1`, `E-5` y `E-8` con respuesta exitosa; **la batería del validador 10 de 10** desde acá; percentil y caudal medidos |
| `h` | EP-06 Desenlace de la entrega | El punto de desenlace con su terminalidad, y **la colección de peticiones reproducible** | `TC-23`, `TC-35`, y reejecución de `TC-07`, `TC-25` y `TC-26` | Matriz completa: 12 de 12 casos de uso, **15 de 15** puntos, 16 de 16 reglas y 9 de 9 invariantes; la colección en **5 pasos o menos** con **0** datos inventados |

**La suma cubre los treinta y siete casos de verificación.** `TC-07`, `TC-25` y `TC-26` se reejecutan en la etapa `h` porque son los tres cuyo resultado **cambia cada vez que se agrega un punto o una respuesta de fallo**, y la `h` agrega las dos cosas.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación. **El despliegue lo ejecuta el Product Owner**, no el agente |
| Ambiente | El contenedor de desarrollo, con el proceso levantado por el anfitrión en memoria de la batería de integración |
| Almacén | **Real y efímero**, el mismo motor que en producción, creado y descartado por la batería. **Nunca el almacén de desarrollo ni el de producción**; y **sin paralelismo entre pruebas que compartan archivo**, porque el motor es de escritor único |
| Datos | Los **ocho** textos literales de los escenarios del intake §20, como cuerpo de petición; y los cuatro fixtures de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5, incluidos los **cinco** accesos firmados en sus formas |
| Secretos de prueba | Una clave de firma **evidentemente ficticia**, provista por configuración de prueba. **Ningún secreto real entra al repositorio, ni en el pipeline** |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función: anfitrión en memoria, cliente de carga acotada, cliente de peticiones para forzar y el archivo de colección versionado |
| Artefactos de despliegue | El archivo de construcción **multietapa** y el de composición, que `PT-04` ejercita. **El agente los entrega y no ejecuta el despliegue** |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** El riesgo `RQ-11` describía en presente la redacción del gate del intake como de «nueve casos»; el intake **1.20** dice **diez**. El riesgo se conserva con su probabilidad reevaluada a **baja** y el nueve ubicado **hasta 1.19**. El riesgo no se retira porque las copias del texto viejo pueden seguir circulando. Ningún caso ni umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance del plan sobre las **seis** etapas que este proyecto de código toca, con las dos que no lo tocan declaradas, con la constancia de que **la batería de integración del producto vive acá** y con la frontera del despliegue, que es manual y del Product Owner. Declara **siete** criterios de entrada —incluido el que exige declarar la ubicación de todo punto de acceso nuevo **antes** de construirlo— y **doce** de salida; **once** riesgos de calidad alineados con los nueve riesgos arquitectónicos de `05` §9 más dos propios, entre ellos el de dar la batería de integración por suficiente sin las inspecciones de umbral exacto; el plan por etapa con los treinta y siete casos repartidos y **sin fechas ni duraciones**; y los recursos. |
