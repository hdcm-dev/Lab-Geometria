# 03 · UX / UI / DX — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** README.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) completo (diez casos de uso, `Especificacion-Funcional.md` con su §3 de puertos y su §4 de frontera, `Definicion-Contrato-Del-Validador-De-Figuras.md`, `Modelo-Datos/` con sus siete `RC`, `Glosario-Funcional.md` y su `README.md`); `00-Contexto/Vision-Producto.md` y `00-Contexto/Alcance-Producto.md`; `01-Necesidades-Negocio/Necesidades-Negocio.md`; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` (RN-01 a RN-16); `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` (los cuatro puertos); `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2**; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §11, §14, §16 y §17.3
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Variante aplicada y por qué](#1-variante-aplicada-y-por-qué)
- [2. Qué hay en esta carpeta](#2-qué-hay-en-esta-carpeta)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Criterios de aceptación declarados no aplicables](#5-criterios-de-aceptación-declarados-no-aplicables)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Variante aplicada y por qué

**Variante DX**, por el tipo `library` del proyecto de código y por `tiene_ui_final` == false. El producto se consume por código: el foco está en la superficie pública, en la documentación de cada contrato y en el recorrido de quien tiene que trabajar contra esta capa.

**Cero wireframes.** El mínimo para `library` es cero (`Rules-UX-UI-DX.md` §2.2) y no hay ninguna superficie que dibujar: este proyecto de código no atiende peticiones, no expone protocolo y no lo mira ninguna persona.

Lo que hace específica a esta sección, y lo que hay que llevarse si se lee una sola línea: **esta capa no tiene superficie propia, tiene la forma de los contratos que otra capa declaró, y acá vive el mecanismo y no la decisión.** Quien busque acá una regla de negocio, una autorización o una transición de estado está en la capa equivocada.

Y lo segundo, que es lo que ordena el resto de la sección: **acá está el riesgo declarado del producto y acá están los tres secretos.** El intake registra, con probabilidad alta y con impacto alto, que el validador se escribe sin leer el análisis y rechaza el dato real de los alumnos; y esta capa es el **último punto del recorrido de la contraseña en claro** —de acá para adentro sólo circula su valor derivado—, el lugar donde vive la clave de firma y aquel donde el texto de un alumno puede perderse. De ahí sale la parte más distintiva de estos documentos: **los tres atajos prohibidos**, que no fallan, y las **tres reglas de negocio cuyo tramo principal vive acá** —RN-08, RN-09 y RN-14—, que se rompen produciendo algo válido.

## 2. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: los tres roles de intervención —**incluido el operador del despliegue**—, qué es la superficie pública de esta capa, la frontera entre el mecanismo y la decisión, las tres reglas que sólo se rompen acá, onboarding en tres tramos, quick-start, Diátaxis, principios de error, ocho métricas y lazo de retroalimentación. **Es el punto de entrada** | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | Recorrido de la primera hora: prerrequisitos con lectura obligatoria, el primer ejemplo sobre el **texto real** del escenario E-2, el contraste E-3 contra E-4, veintitrés diagnósticos frecuentes y, al final, **los tres atajos que no fallan** con el procedimiento de dónde va algo nuevo | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de las **17 condiciones de error** derivadas una por una de la §6 de los diez casos de uso, con su categoría, su forma de terminación y su diagnóstico accionable; los **siete resultados que no son condiciones**; la prohibición sobre secretos y rutas; las **dos categorías vacías**; y la verificación mecánica de cobertura | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario que esta categoría acuña, los dos términos con más de un referente y los que se referencian sin redefinir | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura, omisiones y criterios no aplicables | Propuesto |

No hay carpeta `_legacy/`: esta categoría nunca se había emitido para este proyecto de código.

## 3. Orden de lectura sugerido

1. **[`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md), entero, antes que nada de esta sección.** Es el único orden de lectura del producto que empieza fuera de su propia carpeta, y el motivo está declarado: el intake registra que **el defecto que más veces se repite es escribir el validador sin leer el análisis**.
2. **[`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1** — quién interviene, qué es la superficie pública de esta capa, dónde está la frontera entre el mecanismo y la decisión, y las tres reglas que sólo se rompen acá. Sin esto, el resto se lee como documentación de una capa que decide cosas, que es lo que esta capa no hace.
3. **[`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md)** — de punta a punta, con el repositorio de código abierto. Es la primera hora completa, y su **§7 es el tramo que más rinde a largo plazo**.
4. **[`DX-Error-Messages.md`](DX-Error-Messages.md) §1 y §2** — en particular **§1.2**, los siete resultados que no son condiciones; **§1.4**, lo que ningún mensaje puede decir; **§2.2**, las dos categorías vacías y por qué acá lo están; y **§2.4**, las tres condiciones que fallan hacia el lado seguro. El catálogo de §3 se consulta después, por código.
5. **[`../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md)** y sus siete `RC`, si lo que se va a tocar guarda algo.
6. **[`Glosario-UX.md`](Glosario-UX.md)** — conviene tenerlo a mano desde el principio, sobre todo por la regla de que «error» a secas no se escribe acá y por las tres polisemias que 02 ya había declarado: «validador», «repositorio» y «derivado».
7. **[`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4 a §7** — Diátaxis, métricas y retroalimentación, que son las decisiones de mantenimiento de la sección.

La sección aguas arriba tiene su propio orden de lectura de siete pasos en su [`README.md`](../02-Especificacion-Funcional/README.md), y esta sección no lo duplica: lo referencia.

## 4. Artefactos omitidos y su motivo

Cada omisión se declara con su motivo, y ninguna es una deuda pendiente.

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Experiencia-De-Uso.md` | Es de la variante UX/UI. `tiene_ui_final` == false y la tabla maestra de `Rules-UX-UI-DX.md` §2.1 lo omite explícitamente para `library`. La experiencia de las personas que usan el producto se documenta en la categoría 03 de la pieza pública. Lo que esta capa aporta a esa experiencia es el dato: la observación ubicada, la advertencia con sus dos valores y el trabajo que sobrevive |
| `wireframes-<superficie>.md` | Es de la variante UX/UI. El mínimo para `library` es **cero** (§2.2) y no hay ninguna superficie que dibujar: este proyecto de código no expone endpoints ni cruza frontera de proceso |
| `representacion-<concepto>.md` | Condicional a que exista una representación visual o estructural reutilizada. **No existe, y conviene decir por qué no lo es el texto del alumno**: este proyecto de código **no lo representa, lo conserva y lo lee**. La representación visual de ese mismo dato es de `GeometriaFactory-Visor`, y el contrato de lo que cruza el proceso es de `GeometriaFactory-Contracts` |
| `DX-Portal-Developers.md` | `tiene_portal_developers` == false. No hay portal hospedado ni integradores externos: este proyecto de código no se publica en ningún feed y **su único consumidor es la composición de raíz de `GeometriaFactory-Api`** |
| `DX-Operability.md` | Es obligatorio para `worker-service` y este proyecto de código es `library`: no atiende peticiones, no abre conexiones y no se despliega por sí mismo. **Y conviene declarar dónde sí está lo operable, porque acá hay más que en las capas hermanas**: lo que se opera es el contenedor de la pieza de datos, con su volumen persistente, su clave de firma y su respaldo, y eso pertenece a `GeometriaFactory-Api` y a `09-Devops`. Lo que esta sección aporta al operador es el **diagnóstico accionable** de sus seis condiciones de despliegue, en [`DX-Error-Messages.md`](DX-Error-Messages.md) |
| `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` == false: no hay Fase B2 de validación visual de maqueta para este proyecto de código |

## 5. Criterios de aceptación declarados no aplicables

Los criterios siguientes de `Rules-UX-UI-DX.md` §6 son de la variante UX/UI o están condicionados a un flag que acá vale false. **Ninguno se da por cumplido: se declara no aplicable con su motivo.**

| Criterio | Motivo de no aplicabilidad |
| --- | --- |
| Existe `Experiencia-De-Uso.md` con sus once secciones | Es «para todo tipo con UI final». `tiene_ui_final` == false |
| Existe un `wireframes-<superficie>.md` por superficie clave, con sus nueve secciones | Es «para tipos con UI final». El mínimo para `library` es cero |
| Cada wireframe enumera los estados vacío, cargando, con datos y error | No hay wireframes que los enumeren |
| Toda accesibilidad declarada toma WCAG 2.2 nivel AA como piso | No hay superficie perceptible por una persona, así que esta sección no declara ninguna accesibilidad. **No se da por cumplido**: el compromiso del producto vive en la categoría 03 de la pieza pública, que sí dibuja pantallas |
| Nombre canónico de superficie y estados a demostrar, con `requiere_maqueta` == true | `requiere_maqueta` == false |
| Artefactos de la Fase B2 con maqueta aprobada | `requiere_maqueta` == false |
| Catálogo de diseño aplicado en la trazabilidad (§1.4) | Es normativo para artefactos con UI. En variante DX la fila se declara N/A, y así figura en la trazabilidad de cada artefacto |

**El quick-start, además, se declara no aplicable en un documento concreto y por un motivo distinto.** El criterio pide un quick-start verificable en cada documento `dx-`; [`DX-Error-Messages.md`](DX-Error-Messages.md) es del modo reference y se consulta por código, no se recorre de principio a fin, de modo que no hay secuencia de pasos que produzca un primer resultado. Su §7.4 lo declara no aplicable con su motivo y remite al quick-start único del proyecto de código, que vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3 y se recorre guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. En esos dos documentos el criterio **sí se cumple**.

## 6. Notas de uso de esta sección

- **Autoridad.** Nada se origina acá. Cada condición del catálogo deriva de la §6 de un caso de uso de [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/); **no hay ninguna inventada y no falta ninguna**, y la verificación mecánica está escrita en `DX-Error-Messages.md` §7.2 para que se pueda repetir. Lo que 02 no declara, esta sección no lo declara.
- **Ubicación de responsabilidades.** Un enunciado de esta sección que decidiera un estado, una autorización, una transición o una traducción a protocolo estaría mal ubicado: la frontera está en `Especificacion-Funcional.md` §4 y en `DX-Developer-Experience.md` §1.3.
- **Las reglas de negocio no viven acá.** Las **dieciséis** del producto están en `GeometriaFactory-Domain` y esta capa las **ejerce**; `Especificacion-Funcional.md` §6 dice, regla por regla, dónde se ejerce cada una: **trece con tramo acá, dos sin él y tres con su tramo principal acá**.
- **Las siete reglas conceptuales de modelo no son reglas de negocio**, y no compiten con ellas: declaran cómo el dato sobrevive.
- **Ningún texto de prueba se inventó.** Los escenarios `E-1` a `E-8` y las trampas `T1` a `T4` se citan por el identificador del intake, sin renumerar. Es una regla de delivery del producto, y tiene métrica propia con objetivo cero.
- **Nada de esta sección expone un secreto ni una ruta.** Ningún ejemplo, ningún mensaje y ningún diagnóstico incluye la clave de firma, una contraseña, una provisoria, la ruta del almacén ni el texto de un alumno concreto. Los textos que sí aparecen son los escenarios declarados del intake, que son datos de prueba del producto y no de una persona.
- **Decisiones de otras categorías.** Los nombres de tipos, la elección de la función de derivación, el esquema físico y los ADR son de 05; el backlog es de 06; las pruebas, de 08; la operación del contenedor, de 09; los ejemplos de uso, de 11. Esta sección los referencia y no los toma.
- **Puntos abiertos que esta sección roza y no reabre.** Los **quince** que `Especificacion-Funcional.md` §11 declara: **nueve propios** de la capa —hasta dónde llega el conjunto de tipos reconstruibles, **cómo se sostiene que la provisoria no se repite**, la longitud y el alfabeto de la provisoria, la vigencia exacta del acceso, de dónde sale el valor derivado del área de una pieza volumétrica, el límite de tamaño del texto, la zona horaria y precisión de los sellos, la fecha de última modificación de la cuenta y la condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`— y **seis** heredados de aguas arriba, entre ellos cuál función de derivación se ancla, el criterio con el que dos correos son el mismo y la frecuencia del respaldo.
- **Uno que ya no está abierto y esta sección no lo trata como tal.** Qué devuelve el validador ante el texto de `E-8` lo resolvió el Product Owner y el `PRODUCT-INTAKE` **1.12** lo declara en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21: **error**, el trabajo queda en `Borrador` y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-09.
- **Vocabulario.** `Vision-Producto.md` §9 es el glosario raíz y `Glosario-Funcional.md` de 02 declara lo que esa categoría acuña, incluidas las tres polisemias propias de esta capa; [`Glosario-UX.md`](Glosario-UX.md) sólo agrega lo de esta sección. **«Error» a secas no se escribe**, `Pendiente` va siempre calificado, «derivado» a secas designa la geometría, «repositorio» a secas no se escribe, «trabajo» no es «unidad de entrega» y la palabra «proyecto» a secas no se usa.
- **Nombres de archivo.** Ningún archivo vivo lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera.
- **Verificación del quick-start.** Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código. Un quick-start que dejó de correr es un defecto de esta sección.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del índice de la sección, que nunca se había emitido para este proyecto de código. Enumera los cuatro artefactos DX vigentes con su propósito y su estado; la variante aplicada con su justificación y los dos rasgos que hacen específica a la sección —que esta capa no tiene superficie propia y que acá están el riesgo declarado del producto y los tres secretos—; el orden de lectura de siete pasos, **que empieza fuera de esta carpeta** por el documento de concepto central y con su motivo declarado; las seis omisiones con su motivo, entre ellas la de operabilidad, que declara dónde sí vive lo operable; los siete criterios de aceptación de la variante UX/UI declarados no aplicables sin darlos por cumplidos; la declaración de no aplicabilidad del quick-start en el catálogo por ser del modo reference; y las notas de uso, con los **diez puntos abiertos** citados y no reabiertos. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-01**: las notas de uso dejan de citar como punto abierto el desenlace del envío de `E-8` y declaran la decisión del intake 1.12 —error, trabajo en `Borrador`, mensaje localizado por índice de figura y campo—. **H-04**: el conjunto citado pasa de **diez** a **quince** puntos abiertos, nueve propios y seis heredados, enumerados contra la §11 vigente del índice maestro. **H-02**: la trazabilidad upstream cita el `PRODUCT-INTAKE` **1.12**. |
| 1.2 | 2026-08-10 | Alineación de recuento con `PRODUCT-INTAKE` **1.13**, que incorpora la regla **RN-16** —habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo— y lleva las reglas de negocio del producto de quince a **dieciséis**. La cabecera de trazabilidad y la tabla de referencias pasan a declarar el rango **`RN-01` a `RN-16`**. **Ninguna decisión, ningún artefacto y ninguna condición de este documento cambia**: RN-16 no tiene tramo propio acá. Sube minor. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en una declaración viva que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La nota «las reglas de negocio no viven acá» decía **quince**. Las reglas del producto son **dieciséis**, `RN-01` a `RN-16`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. **Ningún documento de la sección y ningún orden de lectura cambia.** Sube minor. |
