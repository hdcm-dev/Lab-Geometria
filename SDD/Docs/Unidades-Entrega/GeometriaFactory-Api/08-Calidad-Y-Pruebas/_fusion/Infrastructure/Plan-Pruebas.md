# Plan de pruebas — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Plan-Pruebas.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md) 1.1; [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) 1.0; [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2 y §3; [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Backlog-Tecnico.md); [`../07-Plan-Sprint/Mini-Plan.md`](../../../07-Plan-Sprint/_fusion/Infrastructure/Mini-Plan.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) 1.0 §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md)
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

**Qué cubre.** Los **treinta y cinco** casos de prueba de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), repartidos entre las **cinco** etapas del producto que este proyecto de código toca —`a`, `c`, `d`, `e` y `f`—, que son las que [`../06-Backlog-Tecnico/Product-Backlog.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Product-Backlog.md) §2 declara como sus épicas.

**Qué no cubre, y dónde se cubre.** Las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización y el alcance transaccional declarado, en `GeometriaFactory-Application`; **la persistencia real ejercida por la superficie del producto**, en `GeometriaFactory-Api`, que es donde el intake §17.1.P.6 · GeometriaFactory-Infrastructure ubica la batería de integración; las superficies y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

**La unidad de planificación es la etapa y no el sprint.** El intake declara «sin plazo calendario; el avance se mide por etapas cerradas». Por eso §5 se titula «Plan por etapa» y **ninguna de sus filas lleva una fecha ni una duración**.

**Las etapas `b`, `g` y `h` no aparecen en el plan**, y es declaración y no olvido. `../06-Backlog-Tecnico/Product-Backlog.md` §2 lo fundamenta: la `b` y la `g` no tocan el almacén, los motores ni los mecanismos, y lo que esta capa aporta a la `h` —guardar el estado terminal y el comentario del administrador— **ya está construido en la etapa `e`**, porque el comentario es **campo y no entidad**.

## 2. Criterios de entrada

Lo que tiene que estar listo para que este plan se ejecute en una etapa:

- [ ] La rama de la etapa está abierta y la sesión de refinamiento se hizo.
- [ ] Las historias de la etapa cumplen los criterios de [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../../../06-Backlog-Tecnico/_fusion/Infrastructure/Definition-Of-Ready.md) §1, incluida la cita por identificador de las reglas y los invariantes que viven en `GeometriaFactory-Domain`.
- [ ] **Los puertos que esta etapa implementa ya están declarados en `GeometriaFactory-Application`.** Es un proyecto de código de nivel 2: un puerto que no exista arriba no se puede implementar acá.
- [ ] Los nombres de tipos y de espacios de nombres están fijados en el punto de control de la etapa `a` (`05` §11 `PA-02`).
- [ ] **A partir de la etapa `c`: la función de derivación de clave está anclada** (`05` §11 `PA-03`). Sin eso, los valores esperados de `TC-06025` y `TC-06026` no se pueden escribir sin retrabajo.
- [ ] Los **ocho** textos literales de los escenarios del intake §20 están cargados como fixture, **sin ninguna modificación**.
- [ ] El contenedor de desarrollo levanta y `scripts/test.sh` corre de punta a punta, con su etapa de **verificación de transformaciones**.

## 3. Criterios de salida

Lo que tiene que cumplirse para declarar el plan ejecutado con éxito en una etapa:

- [ ] Todos los `TC-XX` en alcance de la etapa están escritos, ejecutados y en verde.
- [ ] **Ningún `TC-XX` que estaba en verde en la etapa anterior pasó a rojo** sin justificación escrita en el informe de cierre.
- [ ] La cobertura por componente alcanza los umbrales de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 en los componentes que la etapa toca, **con el informe acotado a los dos motores reportado por separado**. Gates condicionados mientras los valores sigan rotulados [ASUNCIÓN].
- [ ] **A partir de la etapa `f`: la batería del validador pasa entera, 10 de 10**, contra la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6.
- [ ] `TC-06009` da **exactamente 2** advertencias sobre `E-1`, y no 3.
- [ ] `TC-06034` cierra en las dos direcciones sobre las condiciones que la etapa incorporó, y `TC-06035` da **0** en sus dos recuentos —mensajes y registro del servidor—.
- [ ] Todos los NFR con umbral **cero** que la etapa toca se midieron **en la condición declarada**, y no se dieron por cumplidos por no haberse observado lo contrario.
- [ ] Los gates `QG-01`, `QG-02`, `QG-03`, `QG-04`, `QG-07`, `QG-08`, `QG-09`, `QG-10`, `QG-11`, `QG-12` y `QG-13` de [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3 pasan.
- [ ] La matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró.
- [ ] Todo defecto cerrado durante la etapa generó al menos un `TC-XX` nuevo o extendió uno existente.
- [ ] El punto de control de la etapa tiene el OK explícito del Product Owner (intake §15, regla de delivery 2).

## 4. Riesgos de calidad

Alineados con los **ocho** riesgos arquitectónicos de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §9, más dos propios de esta categoría.

| Id | Riesgo | Impacto | Probabilidad | Mitigación en este plan |
| --- | --- | --- | --- | --- |
| RQ-01 | Que el validador se escriba **sin leer el análisis** y no sirva para el dato que existe | **Muy alto**: deja el producto inútil para el dato real | **Alta si no se controla**, y así lo declara la fuente | Los **diez** casos de la batería con los **ocho** escenarios como texto literal (`TC-06001` a `TC-06010`), la cobertura de **95 %** de los dos motores, y la prohibición explícita de escribir a mano un texto de figuras |
| RQ-02 | Que un texto **ilegible** devuelva «motor no disponible» en lugar de una observación | Alto: el alumno esperaría a que se recupere de un problema que no tiene | **Alta**: es la garantía que más veces se rompe al implementar | `TC-06013`, con sus **tres** resultados distintos verificados en la misma prueba |
| RQ-03 | Que la provisoria se componga por un medio distinto de la fuente de material impredecible cuando ésta no responde | **Muy alto**: **un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** | Media | `TC-06028`, con la fuente doblada como no disponible, y `TC-06027` con **0** provisorias iguales y **0** derivables de un dato conocido |
| RQ-04 | Que ante la ausencia de clave de firma se genere una al vuelo o se emita sin firmar | **Muy alto**: el sistema arranca, emite accesos y **nadie lo nota hasta que alguien falsifica uno** | Media | `TC-06030`, con **0** accesos emitidos por cualquiera de los dos atajos |
| RQ-05 | Que la preparación del almacén, ante un esquema que no corresponde, **descarte el almacén y lo cree de nuevo** | **Muy alto**: deja el servicio impecable y **sin los trabajos de nadie** | Baja, pero es el atajo más destructivo del producto | `TC-06033`, con el arranque detenido y la verificación de que el almacén **no se descarta** |
| RQ-06 | Que la ubicación del almacén **caiga hacia una ruta alternativa** dentro de la imagen cuando el volumen no está montado | Alto: el servicio acepta trabajos de la comisión entera y **los pierde en el siguiente reemplazo de versión** | Media, porque es el comportamiento por defecto de casi cualquier biblioteca de acceso a archivos | `TC-06033`, segunda mitad: la ubicación no disponible **detiene el arranque** |
| RQ-07 | Que una consulta de listado arrastre los componentes de cada pieza o el texto original | Medio | **Media-alta**: es el comportamiento por defecto de cualquier carga completa de entidad | `TC-06019`, con dos recuentos en cero para el listado y presencia completa en el detalle |
| RQ-08 | Que la unicidad del correo se sostenga **sólo** con la consulta previa del consumidor | Alto | Media | `TC-06022`, que verifica el rechazo del almacén **aunque la consulta previa no lo hubiera visto** |
| RQ-09 | **Que un escenario del intake §20 se sustituya por un texto escrito a mano** «porque es más corto» | **Muy alto**: un texto escrito por quien conoce las cuatro trampas **las pasa sin ejercitarlas**, que es exactamente el modo en que `RQ-01` se materializa sin que nadie lo note | Media | [`Estrategia-Testing.md`](Estrategia-Testing.md) §6 lo prohíbe; el criterio de salida exige que los ocho escenarios sigan siendo el material de `TC-06001` a `TC-06011` y de `TC-06016` |
| RQ-10 | **Que la batería se dé por completa con nueve casos**, arrastrando la redacción que los dos gates del intake tuvieron hasta 1.19 en lugar de la tabla de §21 | Alto: dejaría `E-8` sin cubrir, que es el escenario que cerró la única condición del contrato de fachada sin dato de prueba | **Baja desde el intake 1.20**, que corrigió los dos gates a **diez**; queda como riesgo vivo sólo por las copias del texto viejo que puedan circular | [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §3.2 declara el desenlace y fija **diez**; el criterio de salida de §3 exige 10 de 10 contra la tabla de la matriz §6 |

## 5. Plan por etapa

Sin fechas y sin duraciones, por lo declarado en §1.

| Etapa | Épica | Alcance de testing | Casos de prueba en alcance | Entregable de esta categoría |
| --- | --- | --- | --- | --- |
| `a` | EP-06001 Esqueleto ambulante y verificación de viabilidad | El almacén se crea y se transforma al arrancar, y el arranque se detiene antes que operar sobre uno dudoso. **`PT-04` se mide acá** | `TC-06032`, `TC-06033` | La etapa de **verificación de transformaciones** del pipeline en pie; `QG-01`, `QG-02` y `QG-04` medidos por primera vez; `PT-04` medida |
| `c` | EP-06002 Identidad del administrador y sesión | Unicidad en el almacén, las dos preguntas sobre el conjunto, derivación y verificación de credenciales, y emisión del acceso firmado | `TC-06022`, `TC-06023`, `TC-06025`, `TC-06026`, `TC-06029`, `TC-06030`, `TC-06031`, `TC-06035` | **0** emisiones sin clave de firma y **0** apariciones de un secreto en mensajes o registro; los tres valores [ASUNCIÓN] confirmados o elevados |
| `d` | EP-06003 Ciclo de vida de la cuenta de alumno | La provisoria que el sistema produce, la marca que viaja sin ser un estado de cuenta, y el arrastre de la baja | `TC-06021`, `TC-06024`, `TC-06027`, `TC-06028` | `RN-06014` ejercida en su tramo principal y único; **0** provisorias repetidas; **0** retiros parciales con el almacén interrumpido |
| `e` | EP-06004 Gestión del trabajo | Materialización con el texto literal, consulta con el recorte ya trasladado y retiro físico | `TC-06016`, `TC-06017`, `TC-06018`, `TC-06019`, `TC-06020` | El texto original comparado carácter por carácter; **0** componentes y **0** texto original en la proyección de listado |
| `f` | EP-06005 Interpretación y verificación del dato del alumno | **El validador entero**: lectura tolerante con las cuatro trampas, derivación por tipo, tolerancia estricta y la batería de **10** casos sobre los **ocho** escenarios | `TC-06001` a `TC-06015`, y `TC-06034` | **10 de 10** casos de la batería; `E-1` con **exactamente 2** advertencias; **0** peticiones de red de los dos motores; catálogo de **17** condiciones cerrado en las dos direcciones; la medición de los 200 ms |

**La suma cubre los treinta y cinco casos de prueba.** La etapa `f` concentra dieciséis porque es donde vive el validador, que es el corazón de este proyecto de código y el riesgo de negocio que la fuente pone primero.

## 6. Recursos

| Recurso | Detalle |
| --- | --- |
| Personas | **Una**, `equipo_n = 1` (intake §2), que ejerce a la vez la construcción, la prueba y la aprobación |
| Ambiente | El contenedor de desarrollo, único ambiente de este proyecto de código |
| Almacén | **Efímero, creado y descartado por cada prueba de integración interna**, con su ubicación recibida por configuración de prueba. **Nunca el almacén de desarrollo ni el de producción** |
| Datos | Los **ocho** textos literales de los escenarios del intake §20 y los cuatro fixtures de [`Estrategia-Testing.md`](Estrategia-Testing.md) §5. **Ningún texto de figuras se escribe a mano** |
| Secretos de prueba | Una clave de firma **evidentemente ficticia**, provista por configuración de prueba, y la posibilidad de **no proveerla**, que es lo que `TC-06030` necesita |
| Herramientas | Las de [`Estrategia-Testing.md`](Estrategia-Testing.md) §3, nombradas por función. Su elección concreta es de la etapa `a`, con la función de derivación de clave como punto abierto propio |
| Guiones | `scripts/build.sh`, `scripts/test.sh` y el guion de reposición del almacén al estado de primer arranque, que el intake §17.1.P.8 · GeometriaFactory-Infrastructure declara como mecanismo de reversión |

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** El riesgo `RQ-10` describía en presente la redacción de los dos gates del intake como de «nueve casos»; el intake **1.20** dice **diez**. El riesgo se conserva con su probabilidad reevaluada a **baja** y el nueve ubicado **hasta 1.19**. Ningún caso ni umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el alcance del plan sobre las **cinco** etapas que este proyecto de código toca, con las tres que no lo tocan declaradas y con la frontera precisa contra la batería de integración del producto, que vive en `GeometriaFactory-Api`. Declara **siete** criterios de entrada y **once** de salida, todos verificables; **diez** riesgos de calidad alineados con los ocho riesgos arquitectónicos de `05` §9 más dos propios de la categoría —sustituir un escenario por un texto escrito a mano, y dar la batería por completa con nueve casos—; el plan por etapa con los treinta y cinco casos repartidos y **sin fechas ni duraciones**; y los recursos, con el almacén efímero y los secretos de prueba declarados como tales. |
