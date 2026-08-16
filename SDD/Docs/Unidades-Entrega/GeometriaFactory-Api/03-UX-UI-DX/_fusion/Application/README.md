# 03 · UX / UI / DX — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) completo (once casos de uso, `Especificacion-Funcional.md` con su §3 de puertos y su §4 de comprobaciones transversales, `Glosario-Funcional.md` y su `README.md`); `00-Contexto/Vision-Producto.md` §9 y `00-Contexto/Alcance-Producto.md` §4.1, §4.4 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` (RN-04001 a RN-04016 y los trece casos de uso que esta capa orquesta); `PRODUCT-MANIFEST-Fabrica-De-Geometria.md`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.2, §4.1, §4.2 y §16
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Application

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

**Variante DX**, por el tipo `library` del proyecto de código y por `tiene_ui_final` == false. El producto se consume por código: el foco está en la superficie pública, en la documentación de cada contrato de uso y de cada puerto, y en el recorrido de quien tiene que trabajar contra esta capa.

**Cero wireframes.** El mínimo para `library` es cero y no hay ninguna superficie que dibujar: este proyecto de código no atiende peticiones, no expone protocolo y no lo mira ninguna persona.

Lo que hace específica a esta sección, y lo que hay que llevarse si se lee una sola línea: **lo que esta capa expone son casos de uso y puertos, y la dependencia se invierte.** La capa **declara** los puertos y la infraestructura los implementa; es lo que permite probar los casos de uso enteros con dobles, sin base de datos. Quien no entienda eso va a intentar consultar datos desde acá.

Y lo segundo, que es la razón de que `tiene_auth` valga true: **esta capa no autentica, autoriza**, y sus **cuatro** negativas —pertenencia, facultad, alcance y cambio de contraseña pendiente— no se confunden entre sí. Confundir las dos primeras es el error más caro que un consumidor puede cometer contra esta capa, y olvidar la cuarta es el más fácil: **corta antes que las otras tres**.

## 2. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: rol de intervención, qué es la superficie pública de dos caras, la frontera entre autorizar y autenticar, las cuatro negativas, onboarding en tres tramos, quick-start, ubicación de los cuatro modos de Diátaxis, principios de error, métricas y lazo de retroalimentación. **Es el punto de entrada** | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | Recorrido de la primera hora: prerrequisitos, primer ejemplo con dobles, cómo leer una negativa, veintitrés diagnósticos frecuentes y, al final, la inversión de dependencias en la práctica con los cuatro puertos y el procedimiento de dónde va algo nuevo | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de las **36 condiciones de error** derivadas una por una de la §6 de los once casos de uso, con su categoría, su forma de terminación y su diagnóstico accionable, más el tratamiento de las **cuatro** negativas de autorización y la verificación mecánica de cobertura | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario que esta categoría acuña, los dos términos con más de un referente y los que se referencian sin redefinir | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura, omisiones y criterios no aplicables | Propuesto |

No hay carpeta `_legacy/`: esta categoría nunca se había emitido para este proyecto de código y no hay ninguna versión superada que archivar.

## 3. Orden de lectura sugerido

1. **[`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1** — quién interviene, qué es la superficie pública de dos caras, dónde está la frontera entre autorizar y autenticar, y las cuatro negativas. Sin esto, el resto se lee como documentación de un servicio de datos, que es lo que esta capa no es.
2. **[`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md)** — de punta a punta, con el repositorio abierto. Es la primera hora completa, y su §7 es el tramo que explica la inversión.
3. **[`DX-Error-Messages.md`](DX-Error-Messages.md) §1 y §2** — la distinción entre condición de error, observación y comentario; **§1.4**, los dos caminos de alta y el único motivo con causas opuestas; las tres formas de terminación; **§2.4**, las tres negativas con su tabla de traducciones prohibidas; y **§2.5**, lo que el dominio rechaza sin que acá llegue a ocurrir. El catálogo de §3 se consulta después, por motivo.
4. **[`Glosario-UX.md`](Glosario-UX.md)** — conviene tenerlo a mano desde el principio si el lector viene de otra categoría, sobre todo por la regla de que «error» a secas no se escribe acá y por la polisemia de «repositorio», que 02 ya había declarado.
5. **[`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4 a §7** — Diátaxis, métricas y retroalimentación, que son las decisiones de mantenimiento de la sección.

La sección aguas arriba, [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), tiene su propio orden de lectura de cinco pasos en su [`README.md`](../../../02-Especificacion-Funcional/_fusion/Application/README.md), y esta sección no lo duplica: lo referencia. Su recomendación vale también acá: **sin §3 y §4 de `Especificacion-Funcional.md`, los once casos de uso se leen mal.**

## 4. Artefactos omitidos y su motivo

Cada omisión se declara con su motivo, y ninguna es una deuda pendiente.

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Experiencia-De-Uso.md` | Es de la variante UX/UI. `tiene_ui_final` == false y la tabla maestra de `Rules-UX-UI-DX.md` §2.1 lo omite explícitamente para `library`. La experiencia de las personas que usan el producto —el alumno y el administrador— se documenta en la categoría 03 de los proyectos de código de la pieza pública, no acá. Lo que esta capa aporta a esa experiencia es el dato: el listado con el recorte ya aplicado, el dato de dueño que permite agrupar, y las piezas con su identidad posicional (CU-04006 §10, CU-04007 §10) |
| `wireframes-<superficie>.md` | Es de la variante UX/UI. El mínimo para `library` es **cero** (§2.2) y no hay ninguna superficie que dibujar: este proyecto de código no atiende peticiones ni cruza frontera de proceso (`PRODUCT-INTAKE` §17.2.P.3). La advertencia previa que le muestra al administrador qué se elimina al dar de baja una cuenta es una decisión de presentación, y vive en la categoría 03 de la pieza pública; acá vive la exigencia de la confirmación escrita (CU-04002 §10) |
| `representacion-<concepto>.md` | Condicional a que exista una representación visual o estructural reutilizada. No existe: esta capa no serializa, no dibuja y no exporta ningún documento. Los datos que cruzan la frontera del proceso son de `GeometriaFactory-Contracts` y el dibujo es de `GeometriaFactory-Visor` |
| `DX-Portal-Developers.md` | `tiene_portal_developers` == false. No hay portal hospedado ni integradores externos: este proyecto de código no se publica en ningún feed y sus dos consumidores son proyectos de código del mismo producto (`PRODUCT-INTAKE` §17.2.P.7, §17.2.P.3) |
| `DX-Operability.md` | Es obligatorio para `worker-service` y este proyecto de código es `library`. No hay nada que operar: no atiende peticiones, no abre conexiones y no registra ni instrumenta. Sus únicos NFR son el tiempo del caso de uso más pesado y la exclusión de los componentes en las consultas de listado (§17.2.P.10) |
| `Linea-Base-Visual.md` | `requiere_maqueta` == false: no hay Fase B2 de validación visual de maqueta para este proyecto de código |
| `Contrato-Datos-Maqueta.md` | `requiere_maqueta` == false, por el mismo motivo |
| `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` == false, por el mismo motivo |

## 5. Criterios de aceptación declarados no aplicables

Los criterios siguientes de `Rules-UX-UI-DX.md` §6 son de la variante UX/UI o están condicionados a un flag que acá vale false. **Ninguno se da por cumplido: se declara no aplicable con su motivo.**

| Criterio | Motivo de no aplicabilidad |
| --- | --- |
| Existe `Experiencia-De-Uso.md` con sus once secciones | Es «para todo tipo con UI final». `tiene_ui_final` == false |
| Existe un `wireframes-<superficie>.md` por superficie clave, con sus nueve secciones | Es «para tipos con UI final». El mínimo para `library` es cero |
| Cada wireframe enumera los estados vacío, cargando, con datos y error | No hay wireframes que los enumeren |
| Toda accesibilidad declarada toma WCAG 2.2 nivel AA como piso | No hay superficie perceptible por una persona, así que esta sección no declara ninguna accesibilidad. **No se da por cumplido**: el compromiso WCAG 2.2 AA del producto vive en la categoría 03 del proyecto de código de la pieza pública, que sí dibuja pantallas |
| Nombre canónico de superficie y estados a demostrar, con `requiere_maqueta` == true | `requiere_maqueta` == false |
| Artefactos de la Fase B2 con maqueta aprobada | `requiere_maqueta` == false |
| Catálogo de diseño aplicado en la trazabilidad (§1.4) | Es normativo para artefactos con UI. En variante DX la fila se declara N/A, y así figura en la trazabilidad de cada artefacto |

**El quick-start, además, se declara no aplicable en un documento concreto y por un motivo distinto.** El criterio pide un quick-start verificable en cada documento `dx-`; [`DX-Error-Messages.md`](DX-Error-Messages.md) es del modo reference y se consulta por motivo, no se recorre de principio a fin, de modo que no hay secuencia de pasos que produzca un primer resultado. Su §7.4 lo declara no aplicable con su motivo y remite al quick-start único del proyecto de código, que vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3 y se recorre guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. En esos dos documentos el criterio **sí se cumple**.

## 6. Notas de uso de esta sección

- **Autoridad.** Nada se origina acá. Cada condición del catálogo deriva de la §6 de un caso de uso de [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/); **no hay ninguna inventada y no falta ninguna**, y la verificación mecánica está escrita en `DX-Error-Messages.md` §7.2 para que se pueda repetir. Lo que 02 no declara, esta sección no lo declara.
- **Ubicación de responsabilidades.** Un enunciado de esta sección que documentara persistencia, transporte HTTP, emisión de tokens o una consulta ad-hoc armada desde el caso de uso estaría mal ubicado: la frontera está en `Especificacion-Funcional.md` §1 y en `DX-Developer-Experience.md` §1.3.
- **Las reglas de negocio no viven acá.** Las **dieciséis** del producto están en `GeometriaFactory-Domain` y esta capa las **ejerce**; `Especificacion-Funcional.md` §6 dice, regla por regla, dónde se ejerce cada una.
- **Decisiones de otras categorías.** Los nombres de tipos y de espacios de nombres y los ADR son de 05; el backlog es de 06; las pruebas, de 08; los ejemplos de uso, de 11. Esta sección los referencia y no los toma.
- **Puntos abiertos que esta sección roza y no reabre.** Los cinco que `Especificacion-Funcional.md` §11 declara. El primero es propio de esta capa y conviene tenerlo presente: **el intake nombra tres puertos y no nombra el de repositorio de cuentas**, que la orquestación necesita. No es una regla nueva ni una decisión de alcance, es un nombre; acá se lo nombra en lenguaje de dominio y su identificador se difiere a `05-Arquitectura-Tecnica` y al punto de control de la primera etapa. Los otros cuatro son los nombres de tipos, el criterio de comparación de dos correos, **los sellos de alta, de modificación y de desenlace** —que el intake sostiene como puertos verificables en prueba y que el modelo del dominio no declara como atributos, con la discrepancia elevada al Product Owner— y los valores numéricos de los requerimientos no funcionales, rotulados como asunción aguas arriba.
- **Vocabulario.** `Vision-Producto.md` §9 es el glosario raíz y `Glosario-Funcional.md` de 02 declara lo que esa categoría acuña, incluida la polisemia propia de esta capa; [`Glosario-UX.md`](Glosario-UX.md) sólo agrega lo de esta sección. `Pendiente` va siempre calificado salvo en las dos excepciones declaradas, «repositorio» a secas no se escribe, «trabajo» no es «unidad de entrega», el comentario del administrador no es una observación y la palabra «proyecto» a secas no se usa.
- **Nombres de archivo.** Ningún archivo vivo lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera, y el sufijo queda reservado a las copias de `_legacy/`.
- **Verificación del quick-start.** Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código (`DX-Developer-Experience.md` §3.2). Un quick-start que dejó de correr es un defecto de esta sección.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial del índice de la sección, que nunca se había emitido para este proyecto de código. Enumera los cuatro artefactos DX vigentes con su propósito y su estado, la variante aplicada con su justificación y los dos rasgos que hacen específica a la sección —la inversión de dependencias y la frontera entre autorizar y autenticar con sus tres negativas—, el orden de lectura de cinco pasos, las ocho omisiones con su motivo declarado, los siete criterios de aceptación de la variante UX/UI declarados no aplicables sin darlos por cumplidos, y la declaración de no aplicabilidad del quick-start en el catálogo de errores por ser del modo reference. Deja citado el punto abierto del identificador del puerto de repositorio de cuentas sin reabrirlo. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **Alineación con el 02 corregido**: diez casos de uso y **34 condiciones** en lugar de nueve y 27, veintitrés diagnósticos en la guía en lugar de dieciocho, y el orden de lectura remite además a §1.4 —los dos caminos de alta— y a §2.5 —los rechazos del dominio que acá no ocurren— del catálogo. Los puntos abiertos citados y no reabiertos pasan de cuatro a **cinco**, con el de los sellos de alta, de modificación y de desenlace, que el modelo del dominio no declara como atributos. Las referencias internas al catálogo se corrigen tras la renumeración de **H-09**: la verificación mecánica es su §7.2 y la declaración de no aplicabilidad del quick-start, su §7.4. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-15**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. La cabecera de trazabilidad decía **once** casos de uso de `GeometriaFactory-Domain` y son **doce** desde que ese proyecto de código emitió su CU-02012; el catálogo de esta misma sección ya decía doce, de modo que la sección se contradecía consigo misma. Es el único cambio. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. Los recuentos de la sección aguas arriba pasan a **once casos de uso**, el catálogo de condiciones de **34 a 36**, y las negativas de autorización de **tres a cuatro**. |
| 1.2 | 2026-08-09 | **Reconciliación con lo que `GeometriaFactory-Domain` ya emitió.** La cabecera de trazabilidad declaraba que RN-04012 y RN-04013 estaban «todavía sin archivo allá»: **las trece reglas tienen archivo** y la cita pasa a ser `RN-04001 a RN-04013`. Ningún recuento de esta sección cambia. |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-14`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este índice, y absorbe el `PRODUCT-INTAKE` **1.10**. **`F26-14`**: §1, §2 y §3 nombraban **tres** negativas —pertenencia, facultad y alcance— donde la capa tiene **cuatro**: falta la de **cambio de contraseña pendiente**, que es la que corta primero y la que `Especificacion-Funcional.md` §4 declara como cuarta comprobación transversal. Los tres lugares pasan a decir cuatro y a nombrarla. **Intake 1.10**: las reglas del producto pasan de trece a **quince** con RN-04014 y RN-04015, y §5 y la cabecera de trazabilidad actualizan el recuento y el rango a `RN-04001 a RN-04016` sobre los **trece** casos de uso de dominio que esta capa orquesta. **Ningún artefacto, ninguna omisión declarada y ningún orden de lectura cambia.** Sube minor. |
| 1.4 | 2026-08-10 | Alineación con el `PRODUCT-INTAKE` **1.13** —regla **RN-04016**, habilitar produce la contraseña provisoria— y con la categoría 02 en sus versiones 1.2 de `CU-04002`, 1.3 de `CU-04003` y 1.5 de `CU-04011`. §2 conserva el recuento de **36 condiciones de error** sobre los once casos de uso, porque **entra una y sale una**: `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-04002, y `CREDENCIAL_NO_ESTABLECIDA` sale de CU-04003 y CU-04011 al dejar de ser posible su causa. Ningún artefacto se agrega ni se omite. (DX Lead, AG-03). |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en una declaración viva que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La nota «las reglas de negocio no viven acá» decía **quince**. Las reglas del producto son **dieciséis**, `RN-04001` a `RN-04016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. **Ningún documento de la sección y ningún orden de lectura cambia.** Sube minor. |
