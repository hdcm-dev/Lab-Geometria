# 02 · Especificación funcional — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** README.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md` §9; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los once casos de uso](#2-los-once-casos-de-uso)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: catálogo, tabla de puertos, autorización transversal, matriz NB → CU → RN → US, criterio de recorte, omisiones y puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Once casos de uso, uno por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

## 2. Los once casos de uso

| CU | Nombre | En una línea |
| --- | --- | --- |
| CU-04001 | [Registrar el alta de una cuenta](../../Casos-De-Uso/CU-04001-Registrar-El-Alta-De-Una-Cuenta.md) | Auto-registro del alumno: correo libre, cuenta en estado `Pendiente` y sin credencial |
| CU-04002 | [Gobernar las cuentas de la comisión](../../Casos-De-Uso/CU-04002-Gobernar-Las-Cuentas-De-La-Comision.md) | Las cuatro operaciones del administrador, con confirmación escrita y arrastre de trabajos en la baja |
| CU-04003 | [Resolver el ingreso y la credencial del alumno](../../Casos-De-Uso/CU-04003-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md) | Admisibilidad con su motivo, y fijación y reemplazo de la credencial derivada |
| CU-04004 | [Cargar y reeditar un trabajo propio](../../Casos-De-Uso/CU-04004-Cargar-Y-Reeditar-Un-Trabajo-Propio.md) | Trabajo con dueño y texto original íntegro; reedición sólo en `Borrador` |
| CU-04005 | [Enviar un trabajo e interpretar su texto](../../Casos-De-Uso/CU-04005-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md) | La única acción de guardado, con el validador detrás de un puerto |
| CU-04006 | [Consultar los trabajos propios del alumno](../../Casos-De-Uso/CU-04006-Consultar-Los-Trabajos-Propios-Del-Alumno.md) | Listado acotado al dueño y sin componentes; detalle con desenlace y comentario |
| CU-04007 | [Revisar los trabajos de la comisión](../../Casos-De-Uso/CU-04007-Revisar-Los-Trabajos-De-La-Comision.md) | La comisión sin borradores, con dueño para agrupar y filtrar |
| CU-04008 | [Dar desenlace a un trabajo](../../Casos-De-Uso/CU-04008-Dar-Desenlace-A-Un-Trabajo.md) | Aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad |
| CU-04009 | [Eliminar un trabajo](../../Casos-De-Uso/CU-04009-Eliminar-Un-Trabajo.md) | Los dos alcances opuestos del retiro, en un solo contrato |
| CU-04010 | [Configurar la cuenta de administrador](../../Casos-De-Uso/CU-04010-Configurar-La-Cuenta-De-Administrador.md) | El segundo camino de alta: cuenta única, `Habilitado` y con credencial, sólo en el primer arranque |
| CU-04011 | [Resetear la contraseña de un alumno](../../Casos-De-Uso/CU-04011-Resetear-La-Contrasena-De-Un-Alumno.md) | Contraseña provisoria con marca de cambio pendiente, conservando la cuenta y todos sus trabajos |

## 3. Orden de lectura sugerido

1. [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §1, §3 y §4: qué es esta capa, qué puertos declara y cómo decide quién puede hacer qué. **Sin §3 y §4, los once casos de uso se leen mal**, porque los dos rasgos que los recorren están enunciados una sola vez ahí.
2. Los casos de uso del circuito del trabajo, en el orden en que ocurren: CU-04004, CU-04005, CU-04006, CU-04007, CU-04008, CU-04009.
3. Los casos de uso de la cuenta, en el orden en que ocurren: **CU-04010** —el primer arranque—, CU-04001, CU-04002, CU-04003 y **CU-04011**. **CU-04010 y CU-04001 se leen juntos**: son los dos caminos de alta del producto y sus reglas son opuestas, aunque el número los separe. **CU-04011 y CU-04003 también**: uno pone la marca de cambio de contraseña pendiente y el otro es el único que la levanta.
4. [`Glosario-Funcional.md`](Glosario-Funcional.md), en particular §3.1 y §3.5, que resuelven las dos polisemias propias de esta capa.
5. Para el lector que llega desde el dominio: la tabla de §7.4 del índice dice qué caso de uso de `GeometriaFactory-Domain` orquesta cada uno de éstos.

## 4. Artefactos omitidos y su motivo

Los tres artefactos siguientes **no se emiten**, y el motivo se declara acá y en §9 del índice maestro:

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Definicion-<Concepto-Central>.md` | **El concepto central de esta capa son los puertos, y los casos de uso ya los describen.** Cada uno declara cuáles consume y qué le pide a cada uno, y la tabla de §3 del índice los reúne. Un documento aparte repetiría eso sin agregar semántica. La regla lo declara recomendado, y no obligatorio, para `library` con superficie estrecha |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Las reglas del producto viven en `GeometriaFactory-Domain`** —**dieciséis** desde el `PRODUCT-INTAKE` 1.13, **las dieciséis con archivo allá**, de modo que acá se enlazan todas—, son atemporales y acá se **referencian**, no se redactan. Volver a enunciarlas crearía dos textos de la misma regla en la misma cadena documental, que es exactamente el defecto que la regla de no duplicación previene. §6 del índice declara, regla por regla, dónde se ejerce en esta capa |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | La regla de la categoría los omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false: el intake declara «no aplica directamente» en §17.2.P.4. Esta capa declara el puerto de repositorio y el alcance de la unidad de trabajo, no el modelo de datos. El modelo del dominio vive en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` |

## 5. Notas de uso de esta sección

- **Los identificadores `CU-XX` son locales a este proyecto de código.** No coinciden con los de `GeometriaFactory-Domain` ni con los veintisiete casos de uso que previó `01-Necesidades-Negocio`. La correspondencia se lee por la matriz de §7.1 del índice y por la tabla de §7.4, nunca por número. **CU-04010 y CU-04011 llevan esos números por haberse dado de alta después**, aunque temáticamente formen par con CU-04001 y con CU-04002: renumerar habría roto las citas de los casos de uso intermedios.
- **Los motivos que devuelven los casos de uso no son códigos de protocolo.** Su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api`, y la equivalencia crítica —trabajo ajeno igual a «no encontrado», nunca a «no autorizado»— está declarada en §4 del índice para que ninguna capa la invente.
- **Cada caso de uso lleva una sección §17 «Compatibilidad de la superficie pública»**, que es la sección opcional que `Rules-Especificacion-Funcional.md` §4.3 asigna al tipo `library`, con ese número. No es una sección obligatoria desplazada.
- Esta categoría **no toma decisiones de arquitectura**: los nombres de tipos, la elección de mecanismos y los ADR pertenecen a `05-Arquitectura-Tecnica`, y la estrategia de pruebas a `08-Calidad-Y-Pruebas`. Lo que acá se declara como «tests previstos» es una previsión, no un plan.
- No hay `_legacy/` en esta carpeta: es la emisión inicial de la categoría para este proyecto de código.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Índice navegable de los nueve casos de uso y del glosario, orden de lectura sugerido, las tres omisiones con su motivo declarado y las notas de uso de la sección. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-01**: el índice navegable pasa de nueve a **diez** casos de uso, con **CU-04010** para la configuración del administrador; el orden de lectura declara que CU-04010 y CU-04001 se leen juntos por ser los dos caminos de alta, y §5 declara por qué CU-04010 lleva el último número. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. El índice navegable pasa de diez a **once** casos de uso, con **CU-04011** para el reseteo de contraseña por el administrador; §3 suma CU-04011 al orden de lectura de la cuenta y declara que se lee junto con CU-04003, que es el único que levanta la marca que aquél pone; §4 deja de decir «once reglas» y declara que son trece desde 1.7, con dos todavía sin archivo aguas arriba; §5 extiende a CU-04011 el motivo por el que lleva el último número. |
| 1.2 | 2026-08-09 | **Reconciliación con lo que `GeometriaFactory-Domain` ya emitió.** §4 declaraba que RN-04012 y RN-04013 «todavía no tienen archivo allá» y que acá se citaban contra el intake: **las trece reglas tienen archivo**, y la fila pasa a decirlo. Los recuentos —once casos de uso, trece reglas— no cambian. `Especificacion-Funcional.md` sube a 1.2 en la misma reconciliación y cierra los dos puntos abiertos que esta situación sostenía. |
| 1.3 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10**: las reglas del producto pasan de trece a **quince** con **RN-04014** y **RN-04015**, y **las quince tienen archivo** en `GeometriaFactory-Domain`, de modo que la nota de omisión de `Reglas-De-Negocio/` actualiza su recuento y su cita del intake. **Ningún documento de esta sección, ningún caso de uso y ningún recuento propio de la capa cambia.** Sube minor. |
| 1.4 | 2026-08-10 | Alineación con el `PRODUCT-INTAKE` **1.13**, regla **RN-04016** y precisión de **F-04**: habilitar una cuenta produce y fija su contraseña provisoria. Los **once** casos de uso no cambian de número ni de recorte; lo que cambia es el alcance de **CU-04002**, que pasa a producir la provisoria y a poner la marca, y el solicitante de la fijación de **CU-04003**, que deja de ser el alumno anónimo. Ningún artefacto se agrega ni se omite y el orden de lectura no cambia. (Analista Funcional + API Designer, AG-02). |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en una declaración viva que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La fila de `Reglas-De-Negocio/` decía «**quince** desde el `PRODUCT-INTAKE` 1.10, las quince con archivo allá». Las reglas del producto son **dieciséis**, `RN-04001` a `RN-04016`, contadas sobre los archivos de `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. La decimosexta, **RN-04016**, entró con el intake **1.13** y tiene archivo propio como las otras quince. **Ningún documento de la sección, ningún caso de uso y ninguna omisión declarada cambia.** Sube minor. |
