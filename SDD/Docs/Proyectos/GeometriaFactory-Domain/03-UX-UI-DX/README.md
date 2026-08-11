# 03 · UX / UI / DX — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** README.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/` completo (**trece** casos de uso, **dieciséis** reglas de negocio, `Definicion-Modelo-De-Dominio.md`, `Glosario-Funcional.md` y su `README.md`); `00-Contexto/Vision-Producto.md` §9 y `00-Contexto/Alcance-Producto.md` §4.1, §4.4 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §17.1, §4 (**F-04** precisada), §4.1 (**RN-16**) y §4.2
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Variante aplicada y por qué](#1-variante-aplicada-y-por-qué)
- [2. Qué hay en esta carpeta](#2-qué-hay-en-esta-carpeta)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y su motivo](#4-artefactos-omitidos-y-su-motivo)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Variante aplicada y por qué

**Variante DX**, por el tipo `library` del proyecto de código y por `tiene_ui_final` == false. El producto se consume por código: el foco está en la superficie pública, en la documentación de cada contrato de uso y en el recorrido de quien tiene que trabajar contra el dominio.

**Cero wireframes.** El mínimo para `library` es cero y no hay ninguna superficie que dibujar: este proyecto de código no expone protocolo, no cruza ninguna frontera de proceso y no lo mira ninguna persona.

Lo que hace específica a esta sección, y lo que hay que llevarse si se lee una sola línea: **la superficie pública de un modelo de dominio son sus guardas**, y su catálogo de errores es, casi entero, el catálogo de invariantes violados.

## 2. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: rol de intervención, qué es la superficie pública acá, la frontera de autenticación, onboarding en tres tramos, quick-start, ubicación de los cuatro modos de Diátaxis, principios de error, métricas y lazo de retroalimentación. **Es el punto de entrada** | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | Recorrido de la primera hora: prerrequisitos, primer ejemplo, cómo leer una guarda, las tres máquinas de estado, dónde va una regla nueva y catorce diagnósticos frecuentes | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de las **42 condiciones de error** derivadas una por una de la §6 de los trece casos de uso, con su categoría, su causa y su diagnóstico accionable | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario que esta categoría acuña, los dos términos con más de un referente y los que se referencian sin redefinir | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones declaradas | Propuesto |

No hay carpeta `_legacy/`: esta categoría nunca se había emitido y no hay ninguna versión superada que archivar.

## 3. Orden de lectura sugerido

1. **`DX-Developer-Experience.md` §1** — quién interviene, qué es la superficie pública de un modelo de dominio y dónde está la frontera de autenticación. Sin esto, el resto se lee como documentación de una API de servicio, que es lo que este proyecto de código no es.
2. **`Guia-Onboarding-Developer.md`** — de punta a punta, con el repositorio abierto. Es la primera hora completa.
3. **`DX-Error-Messages.md` §1 y §2** — la distinción entre condición de error, observación y comentario, y la taxonomía. El catálogo de §3 se consulta después, por código.
4. **`Glosario-UX.md`** — conviene tenerlo a mano desde el principio si el lector viene de otra categoría, sobre todo por la regla de que «error» a secas no se escribe acá.
5. **`DX-Developer-Experience.md` §4 a §7** — Diátaxis, métricas y retroalimentación, que son las decisiones de mantenimiento de la sección.

La sección aguas arriba, [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/), tiene su propio orden de lectura de ocho pasos en su `README.md`, y esta sección no lo duplica: lo referencia.

## 4. Artefactos omitidos y su motivo

Cada omisión se declara con su motivo, y ninguna es una deuda pendiente.

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Experiencia-De-Uso.md` | Es de la variante UX/UI. `tiene_ui_final` == false y la tabla maestra de `Rules-UX-UI-DX.md` §2.1 lo omite explícitamente para `library`. La experiencia de las personas que usan el producto —el alumno y el administrador— se documenta en la categoría 03 de los proyectos de código de la pieza pública, no acá |
| `wireframes-<superficie>.md` | Es de la variante UX/UI. El mínimo para `library` es **cero** (§2.2) y no hay ninguna superficie que dibujar: este proyecto de código no expone protocolo ni cruza frontera de proceso (`PRODUCT-INTAKE` §17.1.P.3) |
| `representacion-<concepto>.md` | Condicional a que exista una representación visual o estructural reutilizada. No existe: el dominio no serializa, no dibuja y no exporta ningún documento. Dibujar las piezas es de `GeometriaFactory-Visor` y exponer datos hacia afuera es de `GeometriaFactory-Contracts` y `GeometriaFactory-Api` (`Definicion-Modelo-De-Dominio.md` §7) |
| `DX-Portal-Developers.md` | `tiene_portal_developers` == false. No hay portal hospedado ni integradores externos: este proyecto de código no se publica en ningún feed y sus dos consumidores son proyectos de código del mismo producto (`PRODUCT-INTAKE` §17.1.P.7) |
| `DX-Operability.md` | Es obligatorio para `worker-service` y este proyecto de código es `library`. No hay nada que operar: no atiende peticiones, no abre conexiones y no registra ni instrumenta (§17.1.P.10) |
| `Linea-Base-Visual.md` | `requiere_maqueta` == false: no hay Fase B2 de validación visual de maqueta para este proyecto de código |
| `Contrato-Datos-Maqueta.md` | `requiere_maqueta` == false, por el mismo motivo |
| `Bitacora-Validacion-Maqueta.md` | `requiere_maqueta` == false, por el mismo motivo |

Criterios de aceptación de `Rules-UX-UI-DX.md` §6 declarados **no aplicables** por ser de la variante UX/UI, con su motivo:

| Criterio | Motivo de no aplicabilidad |
| --- | --- |
| Existe `Experiencia-De-Uso.md` con sus once secciones | Es «para todo tipo con UI final». `tiene_ui_final` == false |
| Existe un `wireframes-<superficie>.md` por superficie clave, con sus nueve secciones | Es «para tipos con UI final». El mínimo para `library` es cero |
| Cada wireframe enumera los estados vacío, cargando, con datos y error | No hay wireframes que enumerarlos |
| Toda accesibilidad declarada toma WCAG 2.2 nivel AA como piso | No hay superficie perceptible por una persona, así que esta sección no declara ninguna accesibilidad. **No se da por cumplido**: el compromiso WCAG 2.2 AA del producto vive en la categoría 03 del proyecto de código de la pieza pública, que sí dibuja pantallas |
| Nombre canónico de superficie y estados a demostrar, con `requiere_maqueta` == true | `requiere_maqueta` == false |
| Artefactos de la Fase B2 con maqueta aprobada | `requiere_maqueta` == false |
| Catálogo de diseño aplicado en la trazabilidad (§1.4) | Es normativo para artefactos con UI. En variante DX la fila se declara N/A, y así figura en la trazabilidad de cada artefacto |

## 5. Notas de uso de esta sección

- **Autoridad.** Nada se origina acá. Cada condición del catálogo deriva de la §6 de un caso de uso de [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/); **no hay ninguna inventada y no falta ninguna**. Lo que 02 no declara, esta sección no lo declara.
- **Ubicación de responsabilidades.** Un enunciado de esta sección que documente persistencia, transporte, serialización, emisión de acceso o consulta sobre conjuntos de entidades está mal ubicado: la tabla de fronteras es `Definicion-Modelo-De-Dominio.md` §7.
- **Decisiones de otras categorías.** Los nombres de tipos y de espacios de nombres y los ADR son de 05; el backlog es de 06; las pruebas, de 08; los ejemplos de uso, de 10 y de 11. Esta sección los referencia y no los toma.
- **Puntos abiertos que esta sección roza y no reabre.** Los dos que `Especificacion-Funcional.md` §9 declara, ninguno bloqueante: los nombres de tipos y de espacios de nombres, que se validan en el punto de control de la etapa `a`, y el criterio con el que dos correos se consideran el mismo.
- **Vocabulario.** `Vision-Producto.md` §9 es el glosario raíz y `Glosario-Funcional.md` de 02 declara lo que esa categoría acuña; [`Glosario-UX.md`](Glosario-UX.md) sólo agrega lo de esta. `Pendiente` va siempre calificado salvo en las tres excepciones declaradas, y la palabra «proyecto» a secas no se usa.
- **Nombres de archivo.** Ningún archivo vivo lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera, y el sufijo queda reservado a las copias de `_legacy/`.
- **Verificación del quick-start.** Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código (`DX-Developer-Experience.md` §3.2). Un quick-start que dejó de correr es un defecto de esta sección.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial del índice de la sección, que nunca se había emitido: el proyecto de código estuvo detenido esperando la resolución de dos ambigüedades, ya resueltas en `PRODUCT-INTAKE` 1.3 y absorbidas por `02-Especificacion-Funcional/` 1.1. Enumera los cuatro artefactos DX vigentes con su propósito y su estado, la variante aplicada con su justificación, el orden de lectura de cinco pasos, las ocho omisiones con su motivo declarado y los siete criterios de aceptación de la variante UX/UI declarados no aplicables sin darlos por cumplidos. **Corrección de la ronda r2 del audit, hallazgo N-04**: el recuento de los diagnósticos de `Guia-Onboarding-Developer.md` pasa de catorce a **trece**, que era la cantidad de filas de su tabla en ese momento. |
| 1.1 | 2026-08-09 | Alineación con la **corrección del P0** que reporta `B-02-03-GeometriaFactory-Application-r1.md` y que AG-02 resolvió emitiendo **CU-12**, la configuración de la cuenta de administrador en el primer arranque. §2 actualiza el catálogo de **37 a 40 condiciones** sobre **doce** casos de uso, y los diagnósticos de la guía de trece a **catorce**, por la fila nueva del administrador constituido por el camino equivocado. La cabecera declara los doce casos de uso como upstream. Ningún artefacto se agrega ni se omite: las ocho omisiones y los siete criterios no aplicables siguen valiendo con el mismo motivo. |
| 1.2 | 2026-08-09 | Alineación con `PRODUCT-INTAKE` **1.7** y con la categoría 02 en su versión 1.4, que emite **CU-13** —reseteo de contraseña, capacidad **F-26**— y las reglas **RN-12** y **RN-13**. §2 actualiza el catálogo de **40 a 43 condiciones** sobre **trece** casos de uso, y la cabecera declara trece casos de uso y trece reglas como upstream. Ningún artefacto se agrega ni se omite: las omisiones y los criterios no aplicables siguen valiendo con el mismo motivo. |
| 1.3 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10**, que lleva las reglas del producto de trece a **quince** con **RN-14** —la contraseña provisoria la produce el sistema, no es adivinable y no se repite— y **RN-15** —resetear no exige cuenta habilitada—, y **cierra la fila de esta sección del hallazgo `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. La cabecera de trazabilidad pasa a declarar **quince** reglas de negocio sobre los trece casos de uso de la categoría 02, y cita el intake 1.10. **`F26-20`**: la fila de `Guia-Onboarding-Developer.md` de §2 describía el recorrido como el de «las **dos** máquinas de estado», y son **tres** desde que `Definicion-Modelo-De-Dominio.md` §5.3 sumó la de la marca de cambio de contraseña pendiente. **Ningún artefacto, ninguna omisión declarada y ningún orden de lectura cambia.** Sube minor. |
| 1.4 | 2026-08-10 | Absorbe el `PRODUCT-INTAKE` **1.13**, que lleva las reglas del producto de quince a **dieciséis** con **RN-16** —habilitar una cuenta produce su contraseña provisoria y la deja con cambio de contraseña pendiente— y precisa **F-04**. §2 actualiza el catálogo de **43 a 42 condiciones** sobre los mismos trece casos de uso: entra `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-02 y salen `CREDENCIAL_NO_ESTABLECIDA` de CU-04 y `RESETEO_SOBRE_CREDENCIAL_NO_FIJADA` de CU-13, las dos porque su causa dejó de ser posible. La cabecera declara dieciséis reglas y cita el intake 1.13. **Ningún artefacto se agrega ni se omite.** Sube minor. |
