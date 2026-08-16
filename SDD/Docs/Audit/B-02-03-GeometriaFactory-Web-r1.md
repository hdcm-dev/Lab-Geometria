# Audit Fase B — 02 Especificación Funcional y 03 UX/UI/DX — GeometriaFactory-Web — ronda 1

**Fase:** B (categorías 02 y 03; la 04 omitida por gating, `usa_llm` == false)
**Proyecto de código:** `GeometriaFactory-Web`
**Producto:** Fábrica de Geometría
**`tipo_proyecto_codigo` (D8):** `web-monolith` · **Variante 03:** UX/UI
**Alcance auditado:** los 30 documentos de `SDD/Docs/Proyectos/GeometriaFactory-Web/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/`
**Auditor:** Arquitecto de Soluciones + QA Senior, independiente, invocado desde cero, sin participación en la generación
**Fecha:** 2026-08-09
**Ronda:** 1 (primera auditoría de este proyecto de código)
**Normativa aplicada:** `Rules-Especificacion-Funcional.md` §2.1, §2.2, §3.3, §4, §6; `Rules-UX-UI-DX.md` §1.2, §1.4, §1.5, §2.1, §2.2, §3.1, §3.3, §4, §6; `Vocabulario-Rules.md` §9 y §10; `Master-Prompt.md` §5 (D1–D9) y §10; catálogos de `Devs/References/Design/`; intake **1.3** y manifiesto **1.1**

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Matriz D1–D9 por documento](#2-matriz-d1d9-por-documento)
- [3. Matriz de estructura obligatoria](#3-matriz-de-estructura-obligatoria)
- [4. Verificación de las tres reglas de arquitectura de nivel producto](#4-verificación-de-las-tres-reglas-de-arquitectura-de-nivel-producto)
- [5. Verificación de accesibilidad](#5-verificación-de-accesibilidad)
- [6. Coherencia cross-doc y gobierno del glosario](#6-coherencia-cross-doc-y-gobierno-del-glosario)
- [7. Hallazgos enumerados](#7-hallazgos-enumerados)
- [8. Verificaciones específicas del encargo](#8-verificaciones-específicas-del-encargo)
- [9. Veredicto y condiciones para promover](#9-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Los 30 documentos cumplen D1–D9 sin excepción y satisfacen íntegramente §6 de las dos reglas constructivas, salvo tres puntos menores de completitud formal. Las tres reglas de arquitectura de nivel producto bajan correctamente a `Especificacion-Funcional.md` §6 (RT-01 a RT-11) y a `Experiencia-De-Uso.md` §2.4, con criterios de aceptación **medibles** —conteo de peticiones del navegador con umbral exactamente 0, cero apariciones de la credencial de sesión en herramientas de desarrollo— y **ninguna de las veintiuna superficies o casos de uso las contradice**. La accesibilidad toma WCAG 2.2 AA como piso y resuelve la escena tridimensional componiendo la alternativa textual desde lo que la fachada devuelve, que es lo correcto bajo RA-02.

**Hallazgos: P0 = 0 · P1 = 0 · P2 = 3 · P3 = 7.** Todos son de completitud formal o de disciplina léxica; ninguno afecta la trazabilidad, la cobertura ni la corrección de diseño.

**Veredicto: APROBADO CON OBSERVACIONES.** La Fase B2 puede arrancar.

---

## 2. Matriz D1–D9 por documento

Leyenda: ✔ cumple · — no aplica.

**D1** idioma español rioplatense neutro técnico con tildes y eñes, filename ASCII · **D2** tablas con encabezado completo, sin `TBD` ni placeholders abiertos · **D3** Título-Con-Guiones estricto en filename y carpeta, identificadores `CU-XX`/`US-XX`/`CA-XX` con dos dígitos · **D4** archivo vivo sin sufijo de versión; versión en la cabecera · **D5** `Versión: 1.0`, estado `Propuesto`, fecha 2026-08-09, control de cambios presente, un archivo por nombre lógico, sin `_legacy/` · **D6** trazabilidad upstream/downstream declarada y resoluble · **D7** sin vocabulario del dominio fuente ni stacks comerciales en la prosa normativa · **D8** `web-monolith` del conjunto cerrado, variante UX/UI coherente · **D9** afirmaciones sobre el estado del sistema con evidencia; acá no hay sistema construido, así que el alcance de D9 se reduce a que ninguna afirmación se presente como hecho verificado.

### 2.1 Categoría 02 — Especificación Funcional (13 documentos)

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `README.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Especificacion-Funcional.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Glosario-Funcional.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-07-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-09-Resolver-Un-Trabajo-Con-Comentario-Opcional.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |

### 2.2 Categoría 03 — UX/UI/DX (17 documentos)

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `README.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Experiencia-De-Uso.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Glosario-UX.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Aprovisionamiento-Inicial.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Registro-De-Cuenta.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Ingreso.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Credencial-Propia.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Panel-De-Trabajos-Del-Alumno.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Envio-De-Trabajo.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Vista-De-Trabajo.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Resolucion-Del-Trabajo.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Panel-De-Cuentas.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Listado-De-La-Comision.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Wireframes-Estado-Degradado-Y-Reconexion.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Representacion-Fila-De-Trabajo.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ (parcial, H-04) | ✔ | ✔ | ✔ |
| `Representacion-Lista-De-Observaciones.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ (parcial, H-04) | ✔ | ✔ | ✔ |
| `Representacion-Sello-De-Version.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ (parcial, H-04) | ✔ | ✔ | ✔ |

**Verificaciones de forma ejecutadas sobre los 30 archivos.** Barrido de cabeceras: **cero** desviaciones de `Versión: 1.0`, `Estado: Propuesto`, `Fecha: 2026-08-09` y, en la 03, `Variante: UX/UI`. **Ningún** nombre de archivo lleva sufijo de versión. **No existe `_legacy/`** bajo `GeometriaFactory-Web` en ninguna de sus carpetas ni subcarpetas —a diferencia de otros proyectos de código del árbol, que sí la tienen y quedan fuera de este alcance—. Todos los documentos superan las tres secciones de primer nivel y **todos** llevan tabla de contenido inmediatamente después de la cabecera. Todos cierran con `Control de cambios`.

Sobre **D7**: la mención del stack aparece solo como referencia normativa al catálogo (`Design-Rules-Blazor-Mudblazor.md`) y a `PRODUCT-INTAKE` §17.6 P.1, que es exactamente el uso que `Rules-UX-UI-DX.md` §1.4 prescribe. La prosa de diseño no nombra componentes comerciales: escribe «el sistema de componentes declarado». Cumple.

Sobre **D9**: no hay sistema construido, y ningún documento afirma haberlo verificado. Los supuestos abiertos están rotulados explícitamente —`[ASUNCIÓN]` para el punto de quiebre de 768 px y para la relación de aspecto de la escena, `[A VERIFICAR]` para el volumen de la comisión— en `Experiencia-De-Uso.md` §10, `Wireframes-Vista-De-Trabajo.md` §2 y §6 y `Wireframes-Listado-De-La-Comision.md` §7. Es el tratamiento correcto.

---

## 3. Matriz de estructura obligatoria

### 3.1 Casos de uso — once secciones de `Rules-Especificacion-Funcional.md` §4.2

Columnas 1 a 11: Propósito · Actores · Precondiciones · Flujo principal · Flujos alternativos · Excepciones y errores · Postcondiciones · Criterios de aceptación · Trazabilidad · Notas y supuestos · Control de cambios.

| CU | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | CA (mín. 3) | §13 opcional |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| CU-01 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 5 | — |
| CU-02 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 7 | — |
| CU-03 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 5 | — |
| CU-04 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 7 | ✔ §13 |
| CU-05 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 8 | — |
| CU-06 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 7 | — |
| CU-07 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 10 | — |
| CU-08 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 7 | — |
| CU-09 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 8 | ✔ §13 |
| CU-10 | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 7 | ✔ §13 |

Diez casos de uso sobre el mínimo de **ocho** que §2.2 fija para `web-monolith`. Numeración contigua `CU-01` a `CU-10`, sin huecos. Los tres `§13 Interacción multiusuario y concurrencia` son la sección opcional que §4.3 admite exclusivamente para `web-monolith` y `web-microservices`: uso correcto y numeración correcta —§13, no §12—. **Setenta y un criterios de aceptación**, todos en forma Given/When/Then y **todos con valores concretos**: `alumno@ejemplo.test`, `clave-nueva-01`, `343.00` contra `1029.00`, «3 de 3 piezas», «4 de 4 elementos», «cero peticiones», «cero apariciones». No hay un solo criterio narrativo.

### 3.2 Artefactos de 02 emitidos y omitidos — `Rules-Especificacion-Funcional.md` §2.1

| Fila de la tabla maestra | Situación | Evaluación |
| --- | --- | --- |
| `Especificacion-Funcional.md` | Emitido | ✔ Con índice maestro, matriz NB→CU→RN→US (§4), cobertura inversa (§4.1) y correspondencia con las 27 previsiones de 01 (§4.2) |
| `Casos-De-Uso/CU-XX` | Emitidos, 10 | ✔ Sobre el mínimo de 8 |
| `Glosario-Funcional.md` | Emitido | ✔ Con las cinco secciones de §4.2.4 |
| `README.md` | Emitido | ✔ Recomendado |
| `Reglas-De-Negocio/RN-XX` | **Omitido, declarado** | ✔ Motivo sustantivo en `README.md` §3 y `Especificacion-Funcional.md` §5: las once reglas viven en `GeometriaFactory-Domain` y se referencian por identificador con enlace relativo. El fundamento no es formal: «**la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable**». Tres criterios de aceptación —CU-06 CA-04, CU-09 CA-04 y CA-05— verifican la acotación **forzando la solicitud sin pasar por la pantalla**, que es la prueba de que la decisión se tomó en serio. No es hallazgo |
| `Modelo-Datos/Modelo-Conceptual.md` | **Omitido, declarado como decisión técnica** | ✔ Verificado. `README.md` §3 lo declara textualmente «**Omitidos como decisión técnica declarada, no por no-aplicabilidad**», reconoce que §2.1 y §2.2 lo marcan obligatorio para `web-monolith`, funda la omisión en `tiene_persistencia` == false deliberado (`PRODUCT-INTAKE` §17.6 P.4) y declara que «**corresponde una ADR en 05-Arquitectura-Tecnica**» con su alternativa descartada y su consecuencia aceptada. Es exactamente la forma que el encargo exige. No es hallazgo |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX` | Omitido | ✔ Consecuencia de lo anterior; declarado en la misma fila |
| `Definicion-<Concepto-Central>.md` | Omitido, declarado | ✔ El concepto central vive aguas arriba, en `Definicion-Modelo-De-Dominio.md` de `GeometriaFactory-Domain` y `Definicion-Contrato-De-Fachada.md` de `GeometriaFactory-Visor`. No es hallazgo |

La aritmética del propio documento cierra: ocho filas, cuatro emitidas, cuatro omitidas agrupadas en tres motivos.

### 3.3 Wireframes — nueve secciones de `Rules-UX-UI-DX.md` §4.2.1

Columnas 1 a 9: Pantalla y propósito · Layout · Componentes principales · Interacciones · Estados · Versión móvil o responsive · Notas de implementación · Trazabilidad · Control de cambios.

| Wireframe | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | Nombre canónico | N.º estados | Vacío / Cargando / Con datos / Error |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `Aprovisionamiento-Inicial` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 14 | Vacío declarado N/A con motivo · ✔ · ✔ · ✔ |
| `Registro-De-Cuenta` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 10 | Vacío declarado N/A con motivo · ✔ · ✔ · ✔ |
| `Ingreso` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 17 | Vacío declarado N/A con motivo · ✔ · ✔ · ✔ |
| `Credencial-Propia` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 14 | Vacío declarado N/A con motivo · ✔ · ✔ · ✔ |
| `Panel-De-Trabajos-Del-Alumno` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 12 | ✔ · ✔ · ✔ · ✔ |
| `Envio-De-Trabajo` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 16 | ✔ · ✔ · ✔ · ✔ |
| `Vista-De-Trabajo` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 16 | Vacío declarado N/A con motivo · ✔ · ✔ · ✔ |
| `Resolucion-Del-Trabajo` | ✔ | ✔ | ✔ | ✔ | ✔ parcial | ✔ | ✔ | ✔ | ✔ | ✔ | 16 | Vacío declarado N/A con motivo · ✔ · **falta, ver H-01** · ✔ |
| `Panel-De-Cuentas` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 14 | ✔ · ✔ · ✔ · ✔ |
| `Listado-De-La-Comision` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 10 | ✔ · ✔ · ✔ · ✔ |
| `Estado-Degradado-Y-Reconexion` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | 12 | ✔ · ✔ · ✔ · ✔ |

**Once wireframes** sobre el mínimo de **cuatro** que §2.2 fija para `web-monolith` (login, home, flujo principal, error). Las cuatro superficies clave del mínimo están cubiertas: `Ingreso`, `Panel-De-Trabajos-Del-Alumno` y `Listado-De-La-Comision` como raíces por papel, `Envio-De-Trabajo` y `Vista-De-Trabajo` como flujo principal, `Estado-Degradado-Y-Reconexion` como superficie de error.

**Los once declaran nombre canónico de superficie** en su sección 1, en la forma `**Nombre canónico de superficie: \`X\`.**`, en Título-Con-Guiones y coincidente con la tabla de `Experiencia-De-Uso.md` §3.1 y con la de `README.md` §4. Es el requisito de `Rules-UX-UI-DX.md` §1.5 y §6 para `requiere_maqueta` == true, y cierra el anti-patrón «wireframe cuya superficie no tiene nombre canónico estable». Cumple.

**Sobre el rótulo de la sección 6.** Los once la rotulan «Versión angosta» en lugar de «Versión móvil o responsive». La sustitución está **declarada con motivo** en `Glosario-UX.md` §2: «La regla la llama «versión móvil o responsive»; acá se usa «angosta» porque el criterio es el ancho disponible y no la clase de dispositivo». El contenido obligatorio —punto de quiebre, reflujo, elementos que se reorganizan— está en las once. Se acepta: es una renombración declarada, no una omisión.

**Ningún wireframe invade la categoría 05.** Verificado sección por sección. Lo que las secciones 7 rotulan «Restricciones de arquitectura» **restituye** restricciones ya decididas aguas arriba —`RT-01` a `RT-11` de `Especificacion-Funcional.md` §6 y `RA-01` a `RA-03` de `PRODUCT-INTAKE` §14— y no decide ninguna. Los tres puntos donde la frontera se acerca están correctamente delegados en lugar de resueltos: `Wireframes-Aprovisionamiento-Inicial.md` §4 declara «Su mecánica técnica es de `05-Arquitectura-Tecnica`; acá se declara el comportamiento observable»; `Representacion-Sello-De-Version.md` cabecera declara «`05-Arquitectura-Tecnica`, que resuelve el contrato en el punto de composición»; `README.md` §8 y `Experiencia-De-Uso.md` §10 declaran «La arquitectura de la capa de presentación y los registros de decisión son de `05`». **No es hallazgo.**

### 3.4 Marco de experiencia — once secciones de `Rules-UX-UI-DX.md` §4.2

| Sección obligatoria | Presente | Evidencia |
| --- | --- | --- |
| 1. Audiencia y contexto de uso | ✔ | §1.1 dos personas con quién, qué sabe, qué viene a hacer, qué lo frustra y cuántos son; §1.2 dónde, con qué, cuándo y cuánto, estado emocional y momento crítico único; §1.3 lo que la audiencia no incluye |
| 2. Principios de diseño | ✔ | §2.1 **nueve** heurísticas de Nielsen con aplicación y verificación; §2.2 **cinco** leyes UX (Hick, Fitts, Miller, Jakob, región común) |
| 3. Flujos clave | ✔ | §3.3 a §3.8, **seis** flujos con disparador, pasos, fricción anticipada y salida |
| 4. Estados y feedback | ✔ | §4.1 tabla canónica heredada del catálogo con **cinco filas propias**; §4.2 mapa de estados por superficie; §4.3 los cuatro estados del trabajo por papel |
| 5. Accesibilidad | ✔ | §5.1 compromiso WCAG 2.2 AA con once criterios prioritarios; §5.2 escena tridimensional; §5.3 plan de verificación |
| 6. Internacionalización | ✔ | Idioma único, dirección, expansión del 30 %, formato de fecha y de número con su excepción declarada |
| 7. Performance percibida | ✔ | Seis acciones con tolerancia y tratamiento, más cuatro reglas transversales |
| 8. Errores y recuperación | ✔ | §8.1 taxonomía de cinco clases; §8.2 tono; §8.3 lo que ningún mensaje contiene |
| 9. Trazabilidad | ✔ | Tabla completa de §4.3 de la regla, incluidas las filas de los cuatro documentos del catálogo, de las cuatro extensiones por capacidad y de las tres dimensiones de maqueta |
| 10. Notas y supuestos | ✔ | Con los tres supuestos rotulados |
| 11. Control de cambios | ✔ | — |

### 3.5 Representaciones — siete secciones de `Rules-UX-UI-DX.md` §4.2.2

| Documento | 1 Concepto | 2 Apariencia | 3 Variantes | 4 Datos que consume | 5 Accesibilidad | 6 Reutilización | 7 Control de cambios |
| --- | --- | --- | --- | --- | --- | --- | --- |
| `Representacion-Fila-De-Trabajo.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Representacion-Lista-De-Observaciones.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |
| `Representacion-Sello-De-Version.md` | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ | ✔ |

Las siete secciones están completas en las tres. §4.2.2 **no exige** tabla de trazabilidad, y por eso su ausencia no se computa como sección faltante; lo que sí queda corto es el criterio de §6 sobre downstream `US` y tests, tratado en **H-04**.

### 3.6 Artefactos de 03 emitidos, previstos y omitidos — `Rules-UX-UI-DX.md` §2.1

| Artefacto | Situación | Evaluación |
| --- | --- | --- |
| `Experiencia-De-Uso.md` | Emitido | ✔ Obligatorio para `web-monolith` |
| `wireframes-<superficie>.md` × 11 | Emitidos | ✔ Sobre el mínimo de 4 |
| `representacion-<concepto>.md` × 3 | Emitidos | ✔ Corresponden a representaciones efectivamente reutilizadas: la fila de trabajo en cinco artefactos, las observaciones en dos superficies, el sello en los once wireframes |
| `Glosario-UX.md` | Emitido | ✔ Obligatorio para los ocho tipos D8; tabla no vacía |
| `README.md` | Emitido | ✔ Recomendado |
| `Linea-Base-Visual.md` | **Previsto para la Fase B2** | ✔ `README.md` §5 lo declara **previsto y no omitido**, con su emisor (AG-03M), su momento y su contenido. Su ausencia hoy es correcta |
| `Contrato-Datos-Maqueta.md` | **Previsto para la Fase B2** | ✔ Ídem |
| `Bitacora-Validacion-Maqueta.md` | **Previsto para la Fase B2** | ✔ Ídem |
| `DX-Developer-Experience.md` | Omitido | ✔ §2.1 lo declara omitible para tipos con UI final únicamente; `tiene_ui_final` == true |
| `Guia-Onboarding-Developer.md` | Omitido | ✔ No hay integrador que recorra una primera hora |
| `DX-Error-Messages.md` | Omitido | ✔ Los mensajes son de superficie; el catálogo de códigos que este proyecto de código consume vive en la 03 de `GeometriaFactory-Contracts` |
| `DX-Portal-Developers.md` | Omitido | ✔ `tiene_portal_developers` == false |
| `DX-Operability.md` | Omitido | ✔ El tipo D8 no es `worker-service` |

La distinción entre **previsto** y **omitido** está hecha explícitamente y con la razón correcta: «un artefacto omitido no vuelve y éstos sí» (`README.md` §5). El criterio de §6 que exige los tres artefactos «en proyectos de código con `requiere_maqueta` == true **y maqueta ya aprobada**» no aplica todavía y está declarado como pendiente en la trazabilidad de `Experiencia-De-Uso.md` §9 —«Validación visual de maqueta: Pendiente», «Línea de base emitida: Pendiente»—. Cumple.

---

## 4. Verificación de las tres reglas de arquitectura de nivel producto

Las tres nacen en `PRODUCT-INTAKE` §14 (líneas 498 a 500) y son la razón de ser de la topología.

### 4.1 Dónde bajan

| Regla | Enunciado en el intake | Bajada en la 02 | Bajada en la 03 |
| --- | --- | --- | --- |
| **RA-01** Ningún JavaScript del navegador invoca la API | §14, con su fundamento: «sin contenido mixto, sin CORS y sin exposición de la IP del servidor propio» | `Especificacion-Funcional.md` §2 (tabla de las tres reglas) y §6 **RT-01**, «Ninguna llamada a la pieza de datos se origina en el navegador: todas salen del servidor de la pieza pública». También **RT-10** para el tramo de la escena | `Experiencia-De-Uso.md` §2.4, fila RA-01, traducida a **prohibición de diseño**: «Prohíbe toda actualización parcial que implique una llamada del navegador al servicio de datos: nada de autocompletado que consulte, nada de validación remota al escribir, nada de listado que se rellene solo, nada de sondeo de estado». `README.md` §8 lo repite como restricción de lectura |
| **RA-02** El bundle del visor es un visualizador puro | §14 y §17.6 P.3 (regla de aislamiento) | §6 **RT-04**, «El bundle del visualizador se invoca **exclusivamente** por sus cinco funciones. Ningún componente accede a su interior ni manipula el elemento de dibujo por su cuenta». **RT-05** para `destruir`. §7 fija en una sola tabla qué función consume cada caso de uso | `Experiencia-De-Uso.md` §2.4, fila RA-02: «nada de superponer marcas sobre la escena, nada de leer su contenido, nada de capturarla, nada de tocar su interior». `Wireframes-Vista-De-Trabajo.md` §4 la reitera como «**Regla de aislamiento, y es una restricción de diseño y no sólo de implementación**» |
| **RA-03** Todo lo que el navegador deba obtener del backend pasa por el front | §14, §17.6 P.5 | §6 **RT-03**, «Ningún mensaje mostrado a la persona incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza de la implementación» | `Experiencia-De-Uso.md` §2.4 y **§8.3 «Lo que ningún mensaje contiene»**, con cuatro prohibiciones enumeradas, incluida la del motivo por el que un guard redirigió |

Las once restricciones transversales de `Especificacion-Funcional.md` §6 **traen cada una su columna «Dónde se verifica»** con criterio de aceptación nombrado. Se comprobó que cada puntero resuelve.

### 4.2 Cómo se verifican — medibilidad de los criterios de aceptación

| Regla | Criterio | Texto | Medible |
| --- | --- | --- | --- |
| RA-01 | CU-01 CA-05 | «Se inspecciona el tráfico del navegador con las herramientas de desarrollo → **Cero peticiones del navegador hacia la pieza de datos**» | ✔ Conteo, umbral 0 |
| RA-01 | CU-02 CA-07 | «Cero peticiones del navegador hacia la pieza de datos, y ningún mensaje visible contiene una dirección de servicio interno» | ✔ Conteo |
| RA-01 | CU-05 CA-06 | «Se inspecciona el tráfico de red del navegador durante el dibujo y la interacción con la escena → **Cero peticiones originadas por el navegador**» | ✔ Conteo |
| RA-01 / RT-10 | CU-07 CA-10 | «Se inspecciona el tráfico de red del navegador mientras se rota y se acerca la escena → Cero peticiones originadas por el navegador, y el texto del trabajo viajó **una sola vez**» | ✔ Conteo, dos magnitudes |
| RA-02 / RT-05 | CU-07 CA-05 | «La persona recorre de uno a otro y vuelve, **diez veces** → Las diez idas y vueltas no degradan la visualización: se invoca `destruir` en cada descarte» | ✔ Repeticiones contadas |
| RA-02 | CU-05 CA-07 | «El alumno navega a otra ruta → Se invoca `destruir`; repetir el recorrido diez veces no degrada la visualización» | ✔ |
| RA-03 | CU-10 CA-02 | «Se lee el mensaje mostrado → **Cero** apariciones de una dirección de servicio interno, de un nombre de archivo de datos o de una traza de implementación» | ✔ Conteo, umbral 0, y §8.3 declara que «se verifica leyendo, no inspeccionando» |
| RA-03 | CU-01 CA-04, CU-08 CA-07, CU-09 CA-08 | «el mensaje **no contiene ninguna dirección de servicio interno**» con el servicio detenido | ✔ |

Ninguno es declarativo. `Experiencia-De-Uso.md` §9, fila «Tests previstos en 08», lleva la medición al plano de pruebas: «**recuento de peticiones originadas por el navegador, con umbral exactamente 0**», y `Wireframes-Vista-De-Trabajo.md` §8 y `Wireframes-Registro-De-Cuenta.md` §8 la repiten por superficie.

### 4.3 ¿Alguna superficie o caso de uso las contradice?

**No.** Se revisaron las diez superficies con datos y los diez casos de uso buscando: petición originada en el navegador, acceso al interior del bundle o al elemento de dibujo fuera de las cinco funciones, y dirección de servicio en texto visible.

- **RA-01.** Todas las menciones son prohibiciones, no permisos. Los dos lugares donde una interfaz web moderna rompería la regla por reflejo están explícitamente cerrados: el filtro del `Panel-De-Cuentas` («**Filtra sobre lo ya recibido. No consulta al servicio de datos**») y el del `Listado-De-La-Comision` («Acota lo ya recibido, **sin ida y vuelta al servidor**»); y la verificación de unicidad del correo al tipear en `Registro-De-Cuenta` («**Ninguna verificación de unicidad del correo mientras se escribe**: la decide el servicio de datos y consultarla al tipear violaría la regla»). `Experiencia-De-Uso.md` §7 cierra el caso general: «**Ninguna validación consulta al servidor mientras se escribe.** Lo prohíbe RA-01». Y §2.4 deriva la consecuencia que un diseñador podría no anticipar: «**no hay optimismo de interfaz**».
- **RA-02.** Sólo dos superficies alojan el elemento de dibujo, `Envio-De-Trabajo` y `Vista-De-Trabajo`, y las dos operan por el componente anfitrión. `Wireframes-Vista-De-Trabajo.md` §4 declara: «Ninguna interacción de esta superficie superpone marcas sobre la escena, lee su contenido, la captura ni toca su interior. La lista de piezas no dibujadas está **al lado** y no encima». `redimensionar` se invoca explícitamente y se declara que «**No ocurre solo**: la fachada no observa tamaños ni decide cuándo ajustar», que es exactamente la consecuencia que `Especificacion-Funcional.md` §7 punto 1 pedía no perder. `destruir` está declarado no opcional en RT-05, en CU-05 CA-07, en CU-07 CA-05 y en las secciones 4 de los dos wireframes.
- **RA-03.** Once artefactos de la 03 repiten la prohibición en su sección 7. El punto más expuesto —el estado degradado, donde la tentación de diagnosticar es máxima— la lleva reforzada: `Wireframes-Estado-Degradado-Y-Reconexion.md` §7 declara que «Una dirección de servicio que dejó de responder porque cambió es, desde la persona, indistinguible de un corte: **mismo aviso, mismo mensaje**». Y `Representacion-Sello-De-Version.md` §5 agrega la prohibición específica que el catálogo no trae: «el detalle de diagnóstico expone la **identidad del artefacto**, nunca la **topología**».

**Un punto de refuerzo que conviene registrar**, porque es la clase de coherencia que se pierde en la iteración siguiente: la 02 y la 03 declaran las dos, en tres lugares distintos, que ocultar un control **no hace cumplir nada**. `Especificacion-Funcional.md` §5, `Glosario-Funcional.md` (entrada «Ruta protegida») y `Wireframes-Resolucion-Del-Trabajo.md` §4: «**Que la pantalla deje de ofrecer las decisiones no es lo que hace cumplir la regla.** La acotación se verifica forzando la solicitud sin pasar por la pantalla, y quien la hace cumplir es el servicio de datos». Es la lectura correcta y evita el error de considerar la interfaz una defensa.

### 4.4 Que el token no llegue al navegador

Es la decisión más consecuente del producto y el intake la exige verificable con las herramientas de desarrollo (§17.6 P.5). Está redactada como **aserción comprobable**, no como promesa:

> **CU-02 CA-02** — *Given* una sesión iniciada correctamente · *When* se inspecciona el navegador con las herramientas de desarrollo —almacenamiento local, almacenamiento de sesión, marcas de sesión y cuerpo de las respuestas recibidas— · *Then* **Cero apariciones de la credencial de sesión**. La única marca presente es la del circuito y no transporta credencial.

El criterio nombra **los cuatro lugares que hay que abrir**, fija el umbral en cero y separa la marca de sesión de la credencial. Se apoya en tres capas más: `RT-02` como restricción transversal; CU-02 §7, cuarta postcondición, redactada en negativo absoluto («En ningún caso: la credencial de sesión aparece en el navegador, ni en el documento, ni en el almacenamiento del navegador, ni en ninguna respuesta que el navegador reciba»); y CU-03 CA-05 para las contraseñas. La 03 lo recoge en `Wireframes-Ingreso.md` §7: «**La credencial de sesión no aparece en el navegador**, ni en el documento, ni en el almacenamiento, ni en el cuerpo de ninguna respuesta que el navegador reciba: **es criterio verificable con las herramientas de desarrollo**». **Cumple sin reservas.**

---

## 5. Verificación de accesibilidad

**Piso declarado.** `Experiencia-De-Uso.md` §5.1: «**WCAG 2.2 nivel AA es el piso obligatorio de este producto**, no una mejora deseable». `README.md` §8 lo repite. **No hay ninguna mención a versiones anteriores de WCAG** en los 30 documentos, lo que satisface el criterio de §6 que sólo las admite en notas de evolución histórica. El anti-patrón «accesibilidad ausente o reducida a una mención genérica» no se verifica: hay once criterios prioritarios enumerados con su aplicación concreta, y notas propias en la sección 7 de los once wireframes y en la sección 5 de las tres representaciones.

**Cobertura de los criterios que §4.2 punto 5 enumera:** contraste (heredado de los tokens del tema, con la nota de que «el sello de versión cumple el piso pese a su jerarquía baja»), foco visible (anillo de al menos 2 px que no depende sólo del color), navegación por teclado (completa, en orden lógico, sin trampas, incluidos los diálogos y el árbol), etiquetas semánticas (encabezado de primer nivel en toda superficie **incluido el shell de acceso**, que «no puede quedar sin estructura por no tener navegación»), y alternativas textuales. Además: objetivos de toque de 24×24 px, mensajes de error asociados y anunciados, regiones activas, no-solo-color, movimiento reducido y gestión de foco.

**El punto difícil: la escena tridimensional.** `Experiencia-De-Uso.md` §5.2 y `Wireframes-Vista-De-Trabajo.md` §7 la resuelven con cuatro movimientos, y **la resolución es coherente con RA-02**:

1. **La escena no es la única vía a la información.** El árbol de la estructura presenta el mismo contenido, se recorre con flechas, se activa con la barra o el ingreso, y la selección va en los dos sentidos por el mismo índice. «Quien no puede ver la escena tiene el árbol; quien no puede recorrer el árbol tiene la escena».
2. **Ninguna pieza desaparece sin quedar enumerada en texto.** La lista de piezas no dibujadas «es, además de la eliminación del fallo silencioso, **lo que hace que la información del dibujo exista fuera del dibujo**».
3. **La alternativa textual del elemento de dibujo.** Éste es el punto que el encargo señala como decisivo, y está resuelto en el sentido correcto:

   > «**El elemento de dibujo declara su naturaleza y su contenido en una alternativa textual**, que se compone con el recuento de piezas dibujadas y no dibujadas del resultado. La alternativa la arma el componente anfitrión con lo que la fachada devuelve: **no se lee del interior de la escena, porque eso violaría RA-02**.» (`Experiencia-De-Uso.md` §5.2)

   El wireframe lo repite con la misma redacción (§7 punto 2). La alternativa se **compone desde el resultado de dibujo que la fachada devolvió**, no se **lee** del canvas. Es exactamente la solución que la regla del visualizador puro admite y la única que no obliga a inspeccionar el interior del bundle. **Correcto.**
4. **La ausencia de capacidad gráfica no rompe nada.** «Escena no disponible» es estado declarado en tres superficies —`Envio-De-Trabajo`, `Vista-De-Trabajo` y la tabla canónica de §4.1—, y su tratamiento es reemplazar el área de la escena por un bloque explicativo manteniendo las otras partes. Es coherente con `RT-11`.

Un quinto punto que refuerza la solución sin haber sido pedido: «**La escena tridimensional no gira sola en ningún momento**: sólo se mueve por acción de la persona, lo que la deja fuera del problema de movimiento ambiental» (§5.1, fila de movimiento reducido). Y el cambio de selección se anuncia como región activa, «porque quien no ve la escena tiene que enterarse de que el resaltado cambió» (`Wireframes-Vista-De-Trabajo.md` §7).

**Plan de verificación.** §5.3 declara seis verificaciones con su método y su momento, y delega correctamente la ejecución a `08-Calidad-Y-Pruebas` sin invadirla: «acá se declara qué tiene que poder verificarse, para que esa categoría tenga contra qué escribir». Fija además un **criterio de cierre** operable para la Fase B2: «Una superficie no se da por aprobada en la Fase B2 si no pasó el recorrido por teclado y la revisión en escala de grises».

**Evaluación: cumple, y con margen.** Sin hallazgos en este eje.

---

## 6. Coherencia cross-doc y gobierno del glosario

### 6.1 Coherencia cross-doc

**La 03 se construyó sobre los diez casos de uso de la 02.** Verificado en las dos direcciones:

| CU de 02 | Superficie de 03 que lo materializa |
| --- | --- |
| CU-01 | `Registro-De-Cuenta` |
| CU-02 | `Ingreso` |
| CU-03 | `Credencial-Propia`, con sus dos cursos |
| CU-04 | `Panel-De-Cuentas` (flujo principal, FA-01, FA-02, FA-05) y `Aprovisionamiento-Inicial` (FA-03, FA-04) |
| CU-05 | `Envio-De-Trabajo` |
| CU-06 | `Panel-De-Trabajos-Del-Alumno` |
| CU-07 | `Vista-De-Trabajo` |
| CU-08 | `Listado-De-La-Comision` |
| CU-09 | `Resolucion-Del-Trabajo`, alojada en `Vista-De-Trabajo` |
| CU-10 | `Estado-Degradado-Y-Reconexion` |

**Cobertura bidireccional completa: ningún caso de uso queda sin superficie y ninguna superficie queda huérfana.** La única partición —CU-04 en dos superficies— está declarada y justificada en `Experiencia-De-Uso.md` §3.1 y en `README.md` §4, con la observación correcta de que no contradice la fusión de la 02 porque los criterios de recorte son distintos: «allá el criterio era el objeto sobre el que se actúa, acá es la unidad maquetable».

**Cobertura NB→CU.** Las nueve `NB-XX` tienen al menos un caso de uso y ningún caso de uso queda huérfano (`Especificacion-Funcional.md` §4 y §4.1). La correspondencia con las 27 previsiones de 01 se declara en §4.2 con un prefijo de desambiguación explícito, `P·`, para separar las dos series homónimas; 26 de 27 se realizan y la única que no —`P·CU-21`, verificar el acceso desde la red de la facultad— se declara con motivo: «No es un acto de la persona dentro del producto». Es el tratamiento correcto.

**Trazabilidad con secciones concretas.** Los 30 documentos citan upstream con sección: `PRODUCT-INTAKE` siempre con §4, §4.1, §4.2, §6, §7, §9, §11, §13, §14, §17.6 P.1 a P.12; `Vision-Producto.md` §2, §3, §7, §9; `Alcance-Producto.md` §4.1, §5, §8; `Compatibilidad-Plataformas.md` §2.2 y §4; los contratos de uso de `GeometriaFactory-Contracts` por caso de uso, flujo alternativo y señal; `Definicion-Contrato-De-Fachada.md` de `GeometriaFactory-Visor` por §3.1, §3.2, §4, §5.2 y §6; y los catálogos de diseño por sección. **Ninguna cita dice «PRODUCT-INTAKE» a secas.** La única granularidad floja son las `NB-XX` en las cabeceras de la 03 (**H-06**).

**Consistencia de identificadores.** `CU-01` a `CU-10` sin huecos; `US-01` a `US-27` repartidas sin colisión entre los diez casos de uso y consistentes entre la matriz de §4 y las tablas de trazabilidad de cada wireframe; `RT-01` a `RT-11` con destinatario resoluble; `RA-01` a `RA-03` idénticos al intake; `RN-01` a `RN-11` referenciadas por enlace relativo a `GeometriaFactory-Domain` y nunca redactadas acá. **Ninguna duplicación de identificador.**

**Dos precisiones heredadas correctamente propagadas.** `Especificacion-Funcional.md` §5 y las tablas de trazabilidad de CU-05 y CU-09 advierten que `RN-04-Eliminacion-Acotada-Al-Borrador.md` cubre hoy los dos caminos de eliminación y que `RN-05-Finalizacion-Sin-Errores-De-Validacion.md` corta hoy en el envío y no en el cierre, y que se cita el contenido vigente y no el que sugiere el slug. Es exactamente lo que corresponde hacer con una desactualización de nombre decidida aguas arriba.

**Una inconsistencia menor de recuento** entre el cuerpo de dos documentos y su propio control de cambios: **H-07**.

### 6.2 Gobierno del glosario — los cuatro criterios de `Master-Prompt.md` §10 y `Vocabulario-Rules.md` §10

**Criterio 1 — Sin contradicciones.** Ningún término tiene dos definiciones incompatibles entre artefactos de la fase. `Glosario-UX.md` §1 declara la regla de precedencia —«**Ningún término de §5 se redefine acá**»— y §5 lista veintiún términos referenciados con puntero a su definición canónica en `Glosario-Funcional.md` §2 o en `Vision-Producto.md` §9. El único caso donde los dos glosarios hablan del mismo concepto es «confirmación escrita», y la 03 declara explícitamente que registra **su forma de superficie, no su semántica**. **Cumple.**

**Criterio 2 — Completitud.** `Glosario-Funcional.md` §2 declara los términos que la 02 acuña, cada uno con la lista de artefactos donde aparece; `Glosario-UX.md` §2 declara diecisiete términos de superficie, con la misma columna. Se verificó por muestreo que los términos que aparecen en más de un artefacto están declarados: «estado degradado», «cartel de reconexión», «panel», «ruta protegida», «vista de trabajo», «elemento de dibujo», «componente anfitrión del visualizador», «papel», «situación de cuenta», «desenlace», «retiro de un trabajo» en la 02; «superficie», «shell», «bloque», «insignia de estado», «banda de resultado», «aviso de indisponibilidad», «estado vacío explicado», «requisito declarado», «orientación posterior», «sello de versión», «detalle de diagnóstico», «versión angosta» en la 03. Las tablas no están vacías en ninguno de los dos. **Cumple**, con una imprecisión de referencia cruzada en una fila (**H-05**).

**Criterio 3 — Polisemia gobernada.** Cinco términos con más de un referente están declarados con evidencia de colisión, forma resuelta y justificación de por qué el escalón más barato de `Vocabulario-Rules.md` §9.3 no alcanzaba:

| Término | Referentes | Forma adoptada | Dónde |
| --- | --- | --- | --- |
| **«vista»** | 3: la página del trabajo, la porción de página que arma un componente, la perspectiva de datos de un papel | Forma calificada obligatoria «vista de trabajo» para el primero; sustitución por «página»/«ruta»/«componente» y por «alcance»/«visibilidad» para los otros dos. Locución «a la vista» declarada explícitamente **fuera** del término | `Glosario-Funcional.md` §3.1 |
| **«pieza»** | 2: la figura del conjunto raíz; el servicio desplegable | Forma desnuda reservada al referente del dominio; «pieza pública», «pieza de datos», «piezas desplegables» siempre calificadas | `Glosario-Funcional.md` §3.2 |
| **`Pendiente`** | 2: situación de cuenta; estado de trabajo | Siempre calificado, con dos excepciones declaradas que **no** son formas desnudas: las enumeraciones del conjunto cerrado y los identificadores literales entre acentos graves | `Glosario-Funcional.md` §3.3 |
| **«panel»** | 2: conjunto de rutas por papel; agrupación visual | «panel» desnudo para el primero; «bloque» o «tarjeta» para el segundo, con una excepción de nombre propio declarada: «panel de resumen» y «panel de cuentas» | `Glosario-UX.md` §3.1 |
| **«sección»** | 2: división numerada de un documento; franja de contenido de una superficie | «sección» reservada al documento; «bloque» o «franja» para la superficie | `Glosario-UX.md` §3.2 |

Los cinco **citan su verificación de colisión**, como exige `Vocabulario-Rules.md` §9.4 y §10. Ejemplo, «vista»: «en CU-07 conviven, en la misma sección, la página que presenta el trabajo y la afirmación de que el administrador ve lo mismo que el alumno. Un subagente que reciba esa sección suelta y lea «la vista es la misma» no puede decidir si le hablan de la página o del alcance de datos». Ejemplo, «panel»: «Un lector que reciba esa sección suelta y lea «el panel de decisión» no puede decidir si le hablan de un conjunto de rutas o de un recuadro». Las evidencias son por ocurrencia y por sección, no genéricas. **Cumple.**

**La 03 respeta la desambiguación de «vista» de la 02 en lugar de crear una propia.** Verificado y explícito: `Glosario-UX.md` §3 abre con «**«Vista» no figura acá y es deliberado.** Su polisemia ya está resuelta aguas arriba, en `Glosario-Funcional.md` §3.1 (…). Esta sección **respeta esa resolución en lugar de crear una propia** (…). Reabrirla acá habría producido dos resoluciones distintas del mismo término dentro del mismo proyecto de código». La §4 lo reitera como prohibición registrada. La disciplina se sostiene en la práctica salvo tres ocurrencias, tratadas en **H-02**.

**Decisiones cerradas del encargo, verificadas una por una:**

| Decisión cerrada | Estado |
| --- | --- |
| `Pendiente` siempre calificado, sin calificar enumeraciones del conjunto cerrado ni identificadores literales | ✔ Declarada en `Glosario-Funcional.md` §3.3 y en `Glosario-UX.md` §4; respetada en el corpus. Las enumeraciones «`Borrador`, `Pendiente`, `Finalizado` y `Rechazado`» y los literales de tabla no se califican, correctamente |
| Los dos referentes de «pieza», y este proyecto de código usa los dos | ✔ Declarado en `Glosario-Funcional.md` §3.2 con reconocimiento explícito de que «los dos aparecen en esta categoría, muchas veces en la misma sección». Los sinónimos informales «mitad» y «parte» quedan prohibidos para el segundo referente |
| «trabajo» no es «unidad de entrega» | ✔ `Glosario-Funcional.md` §4: «**No es una «unidad de entrega»**: es un registro de datos y no se despliega» |
| «observación» superordinado de «advertencia» y «error de validación» | ✔ `Glosario-Funcional.md` §4; `Experiencia-De-Uso.md` §8.1; `Glosario-UX.md` §4 (««observación» cuando el enunciado abarca las dos especies») |
| El comentario del administrador **no es** una observación ni una calificación | ✔ Declarado en cuatro lugares: `Glosario-Funcional.md` §4, `Experiencia-De-Uso.md` §8.1 in fine, `Representacion-Lista-De-Observaciones.md` §3, `Wireframes-Resolucion-Del-Trabajo.md` §3. Y materializado en el diseño: bloque propio, sin severidad, sin índice, sin campo señalado, con tono neutro |
| «proyecto» a secas no se usa | ✔ Barrido sobre los 30 archivos: todas las ocurrencias son «proyecto de código». `Glosario-UX.md` §4 lo registra como prohibición |
| La 03 respeta la desambiguación de «vista» de la 02 | ✔ Con la salvedad de **H-02** |

**Criterio 4 — Criterio negativo.** Se evaluaron las siguientes polisemias candidatas y **se descartaron por contextos disjuntos**. Se enumeran para que la ronda siguiente no las vuelva a levantar; reportarlas sería un defecto del informe, no del documento auditado.

| Candidata | Referentes en juego | Por qué se descarta |
| --- | --- | --- |
| **«instancia»** | (a) la instancia desplegada del laboratorio; (b) la instancia del visualizador | Los contextos son disjuntos **por sección**. El referente (a) vive en `Aprovisionamiento-Inicial`, `Ingreso`, `Registro-De-Cuenta` y `Representacion-Sello-De-Version`, superficies donde no hay elemento de dibujo; el referente (b) vive en `Envio-De-Trabajo` y `Vista-De-Trabajo`, donde el sello está en la barra lateral y el término no se usa para él. No se encontró ninguna sección donde las dos formas desnudas convivan. Calificar todas las ocurrencias sería el falso positivo de §9.1 |
| **«contrato»** | (a) contrato de uso de `GeometriaFactory-Contracts`; (b) contrato de fachada del visualizador; (c) contrato de identidad de versión | Las formas calificadas dominan y la forma desnuda «el contrato» aparece siempre en secciones donde hay un solo referente en juego: §3 y §5 de los wireframes de acceso resuelven contra (a); §4 del wireframe de aprovisionamiento y `Representacion-Sello-De-Version.md` resuelven contra (c); `Wireframes-Vista-De-Trabajo.md` califica siempre «contrato de fachada». Contextos disjuntos por sección |
| **«a la vista»** | Locución del español corriente | Ya declarada fuera del término en `Glosario-Funcional.md` §3.1 —«No se califica ni se sustituye: hacerlo sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica»—. Se confirma: las ocurrencias en `Wireframes-Envio-De-Trabajo.md`, `Wireframes-Listado-De-La-Comision.md` y `Representacion-Fila-De-Trabajo.md` significan «disponible para mirar». **No es hallazgo** |
| **«estado»** | (a) estado del trabajo; (b) situación de cuenta; (c) estado del circuito; (d) estado degradado; (e) estado de superficie | El corpus ya resolvió el par crítico eligiendo **«situación de cuenta»** para (b), y las demás formas son calificadas y estables. La forma desnuda «estado» aparece únicamente en las tablas de estados de superficie, donde el encabezado de la tabla fija el referente. Contextos disjuntos |
| **«nota»** | (a) anotación de superficie —«Nota de alcance», «Nota de acción única», «Nota de ausencia»—; (b) calificación numérica, excluida del producto | El referente (b) **no existe en el producto** y sólo aparece en enunciados de exclusión («ninguna nota ni escala de calificación»). Un término cuyo segundo referente está declarado inexistente no colisiona. **No es hallazgo** |
| **«tarjeta»** | (a) tarjeta de acceso / de aprovisionamiento del catálogo; (b) reflujo de una fila a tarjeta apilada en versión angosta | Contextos disjuntos por sección: (a) vive en las secciones 2 y 3 de las superficies de acceso, (b) exclusivamente en la sección 6 de los listados. Además `Glosario-UX.md` §2 declara que la fila de trabajo «conserva el nombre» al reflujar |
| **«pieza pública» vs «pieza de datos»** | Formas calificadas de una familia | Las formas calificadas **no son** el caso a mirar (`Vocabulario-Rules.md` §9.2, corolario). Sólo lo sería la forma desnuda, que está reservada al referente del dominio y así se usa |
| **«migración»** | R1/R2/R3 de `Vocabulario-Rules.md` §9.6 | No aparece en el corpus auditado. Nada que evaluar |

**Conformidad con `Vocabulario-Rules.md` §10, criterios de los seis términos:** no aparece «proyecto» a secas designando unidad de compilación ni producto; no aparece «solución» a secas designando el agrupador de construcción; los cuatro planos de identidad son distinguibles —`GeometriaFactory-Web` como `Nombre-Proyecto-Codigo`, «Fábrica de Geometría» como `Nombre-Producto`, «pieza pública» como servicio desplegable, «panel» como módulo—; los documentos nombran al producto por su nombre y no por la raíz de código; y el choque de vocabulario del dominio del cliente está resuelto aguas arriba en `Vision-Producto.md` §9.3 y referenciado sin redefinirse. **Cumple.**

---

## 7. Hallazgos enumerados

### H-01 · P2 · Estado «con datos» ausente y no declarado en un wireframe

- **Archivo:** `SDD/Docs/Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Wireframes-Resolucion-Del-Trabajo.md`
- **Sección:** §5 Estados (líneas 130 a 149)
- **Regla:** `Rules-UX-UI-DX.md` §6, «Cada wireframe enumera al menos los estados vacío, cargando, con datos y error»
- **Evidencia:** la tabla enumera dieciséis estados. «Vacío» está correctamente declarado inaplicable **con su motivo** —línea 134: `| **Vacío** | **No aplica**: el bloque no presenta ninguna colección | Se declara para que la ausencia sea deliberada |`—, «Cargando» está presente (línea 135) y los tres estados de error también (líneas 145 a 147). **«Con datos» no figura ni por su nombre canónico ni como ausencia declarada.** Su equivalente funcional aparece bajo nombre propio: `| **Resoluble** | Administrador y trabajo en estado \`Pendiente\` | Campo de comentario, dos decisiones y la acción de retirar |` (línea 136). La omisión es además **inconsistente con el marco**, que sí lo marca: `Experiencia-De-Uso.md` §4.2 registra `| \`Resolucion-Del-Trabajo\` | — | Sí | Sí | ... |`, es decir «Con datos: Sí».
- **Impacto:** formal. La cobertura sustantiva existe y la maqueta de la Fase B2 puede demostrar el estado; lo que falta es la correspondencia nominal que el criterio de §6 y el mapa de §4.2 usan como checklist. Es el único wireframe de los once donde uno de los cuatro estados canónicos no está ni presente ni declarado ausente: los otros diez lo resuelven, incluidos los cinco que declaran «Vacío» inaplicable con motivo.
- **Recomendación:** renombrar la fila 136 a «**Con datos · resoluble**», o agregar una fila «Con datos» que remita a «Resoluble» y «No resoluble por estado» como sus dos materializaciones. Corrección de una línea, absorbible dentro de la versión 1.0 por la política de §5 del master-prompt mientras el documento esté en estado `Propuesto`.

### H-02 · P2 · Forma desnuda de «vista» en un artefacto, contra la resolución que la propia sección declaró respetar

- **Archivo:** `SDD/Docs/Proyectos/GeometriaFactory-Web/03-UX-UI-DX/Representacion-Sello-De-Version.md`
- **Sección:** §1 (línea 33), §4 (línea 116), §4 (línea 119)
- **Regla:** `Glosario-Funcional.md` §3.1, forma calificada obligatoria; `Glosario-UX.md` §4, «**Vista**, sin calificar → «superficie», «ruta» o «componente» para los otros dos [referentes]»; `Vocabulario-Rules.md` §10, tercer criterio de §9
- **Evidencia:** tres ocurrencias de «la vista» en el referente «la porción de página que un componente arma», que es exactamente el referente para el que la 02 prohibió la palabra: línea 33, «la versión se deriva del proceso que construyó el artefacto, **no la escribe nadie en la vista**»; línea 116, «una constante de **la vista**»; línea 119, «**La superficie no distingue entornos por su cuenta.** Si un entorno debe verse distinto, esa distinción llega como campo del contrato y no como condicional en **la vista**». La misma línea 119 usa «superficie» y «vista» para lo mismo en una sola oración, que es la evidencia de colisión.
- **Atenuante registrado:** la formulación replica literalmente la del catálogo, `Rules-UX-UI-DX.md` §1.4, que escribe «La versión se deriva del proceso de construcción; **la vista** no la compone ni la transcribe». La deriva es por arrastre de la fuente normativa y no por decisión propia. Esto es lo que baja el hallazgo de P1 a P2.
- **Impacto:** el documento es el único de los diecisiete de la 03 donde la disciplina se rompe, y lo hace en el artefacto que los once wireframes invocan. Un subagente aguas abajo que reciba §4 suelta puede leer «la vista» como «la vista de trabajo», que es una superficie concreta y distinta.
- **Recomendación:** sustituir las tres ocurrencias por «la superficie» (líneas 116 y 119) y «en la superficie que lo aloja» (línea 33). Es la sustitución que `Glosario-Funcional.md` §3.1 y `Glosario-UX.md` §4 ya prescriben. Verificar con un barrido por ocurrencia y no por reemplazo global, según `Vocabulario-Rules.md` §9.5.

### H-03 · P2 · Las tres representaciones no declaran downstream `US` ni tests previstos

- **Archivos:** `Representacion-Fila-De-Trabajo.md`, `Representacion-Lista-De-Observaciones.md`, `Representacion-Sello-De-Version.md`
- **Sección:** cabecera, campo `Trazabilidad downstream`; y §6 `Reutilización`
- **Regla:** `Rules-UX-UI-DX.md` §6, «Cada artefacto declara trazabilidad upstream (persona objetivo, CU origen, RN si aplica) y **downstream (US, tests)**»
- **Evidencia:** el upstream está bien: las tres cabeceras citan casos de uso con sección, flujo alternativo y criterio —`Representacion-Fila-De-Trabajo.md` línea 10 cita «CU-06 §4 pasos 3 y 4, FA-03, CA-01 a CA-03; CU-08 §4 paso 3» y los catálogos con sección—. El downstream, en cambio, nombra **categorías** y no artefactos: `**Trazabilidad downstream:** Fase B2 de validación visual de maqueta; \`05-Arquitectura-Tecnica\`; \`06-Backlog-Tecnico\`; \`08-Calidad-Y-Pruebas\``. Ningún `US-XX` y ningún escenario de prueba. `Representacion-Sello-De-Version.md` omite además `06-Backlog-Tecnico` por completo. La §6 `Reutilización` lista los artefactos que las invocan, que es lo que §4.2.2 pide, pero no suple la trazabilidad downstream.
- **Contraste:** los once wireframes y `Experiencia-De-Uso.md` sí lo hacen, con filas «US a generar en 06» y «Tests previstos en 08» pobladas con identificadores y escenarios concretos.
- **Impacto:** medio-bajo. Las representaciones son invocadas por wireframes que sí trazan, de modo que la cadena D6 no se rompe; lo que falta es la declaración directa que §6 exige por artefacto.
- **Recomendación:** completar el campo `Trazabilidad downstream` de los tres con las `US-XX` que heredan de sus wireframes invocantes y con los escenarios de prueba correspondientes —para el sello, la verificación de las dos ubicaciones obligatorias y del copiado en un solo gesto; para las observaciones, la revisión en escala de grises y el anuncio del par declarado/derivado; para la fila, el recuento de acciones por estado—. Alternativamente, agregar a las tres una sección de trazabilidad breve, que §4.2.2 no prohíbe.

### H-04 · P3 · Anglicismo declarado prohibido, usado en el marco de experiencia

- **Archivo:** `Experiencia-De-Uso.md`
- **Sección:** §3.1, línea 166
- **Regla:** `Glosario-UX.md` §4, que registra «**Modal**, **toast**, **tooltip**, **wizard**, **breakpoint**, **layout**» como palabras que esta categoría deliberadamente no usa, con «**diálogo**» como sustituto de la primera
- **Evidencia:** «(…) y sus dos diálogos de confirmación **son modales con flujo propio**». Es la única ocurrencia de la palabra en los diecisiete documentos de la 03 fuera de la propia fila de prohibición del glosario. Los demás artefactos escriben «diálogo» consistentemente.
- **Impacto:** bajo. Es una inconsistencia del corpus con su propia prohibición registrada, en el documento que más se lee.
- **Recomendación:** reescribir como «son diálogos con flujo propio», que es además lo que `Rules-UX-UI-DX.md` §3.2 llama superficie: «un modal con flujo propio» en la regla, «un diálogo con flujo propio» en la traducción que esta sección adoptó.

### H-05 · P3 · Referencia cruzada del glosario que no resuelve

- **Archivo:** `Glosario-UX.md`
- **Sección:** §2, fila «Orientación posterior», columna «Artefactos de 03 donde aparece»
- **Evidencia:** la fila declara `\`Wireframes-Panel-De-Cuentas.md\`, \`Wireframes-Aprovisionamiento-Inicial.md\``. El término aparece efectivamente seis veces en `Wireframes-Panel-De-Cuentas.md` (§1, §2, §3, §4, §5 y control de cambios) y **cero veces en `Wireframes-Aprovisionamiento-Inicial.md`**, que es coherente con el diseño: la orientación posterior se aloja en el destino al completar, no en la superficie de arranque. La segunda referencia es incorrecta.
- **Impacto:** bajo. Un lector que entre por el glosario buscando la orientación posterior en el wireframe de aprovisionamiento no la encuentra.
- **Recomendación:** reemplazar la segunda referencia por `Experiencia-De-Uso.md` §2.3 y §3.3, que sí la nombran, o quitarla.

### H-06 · P3 · Granularidad de la cita upstream a las necesidades de negocio en la 03

- **Archivos:** los once wireframes y `Representacion-Lista-De-Observaciones.md`
- **Sección:** cabecera, campo `Trazabilidad upstream`
- **Regla:** `Rules-UX-UI-DX.md` §3.3 y criterio de trazabilidad con secciones concretas
- **Evidencia:** todas las demás fuentes se citan con sección —`PRODUCT-INTAKE` §4, §4.1, §6, §14, §17.6 P.x; los casos de uso por §, FA y CA; los catálogos por §—, pero las `NB-XX` se citan como ruta de archivo desnuda y, a partir de la segunda, como identificador suelto. Ejemplo, `Wireframes-Panel-De-Cuentas.md` línea 10: `` `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md`, `NB-00002` ``. El mismo patrón en los otros diez wireframes.
- **Contraste:** la 02 lo hace bien. `CU-02` línea 9 cita `NB-00002 (…) §1, §5 (tercer, cuarto y quinto criterio); NB-00001 (…) §5 (segundo criterio)`.
- **Impacto:** bajo. La referencia resuelve al documento correcto y la cobertura NB→CU está declarada en la matriz de `Especificacion-Funcional.md` §4; lo que se pierde es el punto exacto del criterio de negocio que la superficie materializa.
- **Recomendación:** añadir §1 y §5, con el criterio numerado, a las citas de `NB-XX` de las cabeceras de la 03, siguiendo la forma que ya usan los casos de uso.

### H-07 · P3 · Tres recuentos declarados que no coinciden con el contenido

- **Archivos y evidencia:**
  1. `Glosario-Funcional.md` §5, control de cambios: «Declara **diecinueve** términos acuñados por la especificación funcional de la pieza pública». La tabla de §2 tiene **veinte** filas: pieza pública, pieza de datos, circuito, estado del circuito, marca de sesión, estado degradado, cartel de reconexión, panel, ruta protegida, vista de trabajo, árbol de la estructura, componente anfitrión del visualizador, previsualización, confirmación escrita, acción única de guardado, retiro de un trabajo, papel, situación de cuenta, desenlace y elemento de dibujo.
  2. `03-UX-UI-DX/README.md` §1, primera línea: «**Dieciséis** documentos: el marco de experiencia, once wireframes, tres representaciones, el glosario y este índice». La enumeración misma suma **diecisiete** (1 + 11 + 3 + 1 + 1) y la tabla que sigue tiene diecisiete filas. El directorio contiene diecisiete archivos.
  3. Consecuencia de lo anterior: el control de cambios de ese mismo README declara «Enumera los **dieciséis** documentos emitidos».
- **Impacto:** bajo. No afecta contenido ni trazabilidad, pero un recuento declarado es una afirmación verificable y conviene que cierre.
- **Recomendación:** corregir a «veinte términos» y a «diecisiete documentos» en los tres lugares.

### H-08 · P3 · Especificación tipográfica concreta en un wireframe

- **Archivo:** `Wireframes-Envio-De-Trabajo.md`
- **Sección:** §3 Componentes principales, fila «Área de texto del trabajo»
- **Regla:** `Rules-UX-UI-DX.md` §4.2.1 punto 2 —«No se incluyen colores, tipografías ni valores de CSS»— y el anti-patrón de §4.4 «wireframe con detalle de CSS, paleta de colores o tipografía exacta»
- **Evidencia:** «**Fuente monoespaciada**, ancho suficiente para una línea del texto sin cortar».
- **Atenuante:** no es una tipografía exacta ni un valor de CSS; es una clase de fuente con justificación funcional —el texto del alumno se lee mejor con avance uniforme— y no compromete la capa visual. Se registra por completitud del barrido, no como defecto de diseño.
- **Recomendación:** reformular como «**avance uniforme por carácter**, para que el texto del alumno se lea sin desalineación», o promover la decisión al catálogo si es transversal, que es la vía que `Rules-UX-UI-DX.md` §1.4 admite para un token nuevo.

### H-09 · P3 · Excepción de rótulo declarada para «Pantalla» pero no para «Layout»

- **Archivo:** `Glosario-UX.md`
- **Sección:** §4, filas «Pantalla» y «Modal, toast, tooltip, wizard, breakpoint, layout»
- **Evidencia:** la fila «Pantalla» declara correctamente la excepción del rótulo impuesto por la regla constructiva: «La regla constructiva rotula «Pantalla y propósito» la sección 1 de cada wireframe, y **ese rótulo se conserva por ser el de la regla**; el cuerpo escribe «superficie»». La fila que prohíbe «layout» no declara la excepción análoga, y sin embargo los once wireframes rotulan su sección 2 «## 2. Layout», que es el rótulo que `Rules-UX-UI-DX.md` §4.2.1 punto 2 impone. La palabra no aparece en prosa en ninguno de los diecisiete documentos: la disciplina se cumple, lo que falta es el registro de la excepción.
- **Impacto:** bajo, y de la misma naturaleza que la sección §4 viene a prevenir: «una palabra prohibida sin registro reaparece en el primer documento que alguien escriba después».
- **Recomendación:** extender la nota de la fila «Pantalla» a «layout», o agregar una línea que declare que los rótulos de sección impuestos por la regla constructiva quedan fuera del alcance de la prohibición.

### H-10 · P3 · Forma desnuda de «pantalla» en el referente de superficie, en dos ocurrencias

- **Archivos:** `Wireframes-Panel-De-Cuentas.md` §4 y `Wireframes-Resolucion-Del-Trabajo.md` §4
- **Regla:** `Glosario-UX.md` §4, fila «Pantalla»
- **Evidencia:** `Wireframes-Panel-De-Cuentas.md`, «**En el mismo lugar donde se pide la confirmación**, no en otra pantalla»; `Wireframes-Resolucion-Del-Trabajo.md`, «No hay una pantalla distinta para cada una». En los dos casos «pantalla» designa una **superficie**, que es el referente para el que el glosario prescribe «superficie».
- **Descartado explícitamente y no reportado:** las demás ocurrencias de la palabra en el corpus —«pantalla angosta», «pantalla rota», «pantalla ancha», «forzando la solicitud sin pasar por la pantalla», «lector de pantalla», «la pantalla de confirmación»— designan el **dispositivo físico**, la **interfaz como capa** o son compuestos lexicalizados, no la unidad de diseño. Calificarlas o sustituirlas sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica y que su §10 declara defecto del informe. Se registra la distinción para que la ronda siguiente no la levante entera.
- **Recomendación:** sustituir las dos ocurrencias señaladas por «superficie».

---

## 8. Verificaciones específicas del encargo

### 8.1 La vista de trabajo no se rediseñó

**Confirmado.** La disposición decidida aguas arriba —datos y texto a la izquierda, elemento de dibujo arriba y árbol abajo a la derecha— se conserva sin alteración y se declara como no negociable en cuatro lugares: `Especificacion-Funcional.md` §4.1 fila NB-00006 y `Glosario-Funcional.md` entrada «Vista de trabajo»; `README.md` de la 02 §4, «no porque la decida acá sino porque **viene decidida aguas arriba y probada en el aula**»; `Experiencia-De-Uso.md` §3.6 y §10; y `Wireframes-Vista-De-Trabajo.md` §1: «**Disposición decidida aguas arriba.** Viene del visualizador que la cátedra ya usa y está probada en el aula. Este wireframe la **documenta y la precisa** —qué pasa en pantalla angosta, qué pasa mientras carga, qué pasa si el texto no verifica— y **no la rediseña**».

El diagrama ASCII de §2 respeta la geometría: columna izquierda con datos, comentario, observaciones y texto original; columna derecha partida en escena arriba y árbol abajo. CU-07 CA-02 la fija como criterio verificable: «Son cuatro: datos y texto a la izquierda; elemento de dibujo arriba y árbol abajo, a la derecha».

El wireframe **precisa sin cambiar**, y anticipa correctamente la objeción de que las observaciones y el comentario agreguen una quinta parte: «Las cuatro partes son datos, texto, escena y árbol. Las observaciones y el comentario **pertenecen a los datos del trabajo** (…). Ubicarlos a la derecha habría empujado la escena o el árbol fuera de vista, que es exactamente lo que la disposición probada evita». Lo único que decide de nuevo es lo que aguas arriba no dice: el orden interno de la columna izquierda con su fundamento, y el comportamiento en pantalla angosta, donde declara explícitamente que «Es la parte de esta superficie que el diseño sí decide, porque la disposición probada en el aula es de pantalla ancha y aguas arriba no dice qué pasa cuando no la hay». **No es hallazgo; es el tratamiento correcto.**

### 8.2 Los cuatro catálogos aplicados y los dos no aplicables

Declarados en `README.md` §7 y desarrollados en `Experiencia-De-Uso.md` §2.3, con la columna «Qué aporta acá» poblada por documento. La aplicación es **real y verificable por rastro**, no una cita de cortesía:

| Catálogo | Estado | Verificación de aplicación real |
| --- | --- | --- |
| `Design-Rules-Web-Generico.md` | Aplicado | Los patrones se referencian **por su nombre y su sección del catálogo** en la columna «Patrón del catálogo» de la sección 3 de los once wireframes: «Base §2.2», «Base §4.4», «Base §4.6», «Base §4.9», «Base §5», «Base §6.3». La tabla canónica de estados de §4.1 se declara heredada del documento base con cinco filas propias añadidas. `Glosario-UX.md` §5 in fine lista doce patrones referenciados por su nombre del catálogo |
| `Design-Rules-Blazor-Mudblazor.md` | Aplicado | Aporta seis reglas rastreables en el corpus: todo color sale del tema —de ahí que **ningún token visual se defina acá**—; feedback obligatorio en cada acción que cruza el circuito —presente en los once, estado «Enviando»—; **prevención de doble envío** —declarada en siete wireframes—; prohibición de almacenamiento de navegador improvisado —coherente con RT-02 y RT-06—; y la nota de que **los formularios de identidad se envían por petición y no por interactividad de componente**, que aparece literalmente en `Wireframes-Ingreso.md` §7, `Wireframes-Aprovisionamiento-Inicial.md` §7 y `Wireframes-Credencial-Propia.md` §7, con su fundamento explicado |
| `Design-Rules-Primer-Arranque.md` | Aplicado | Los seis requisitos que `Rules-UX-UI-DX.md` §1.4 exige están los seis: predicado único «existe la cuenta de administrador»; **corte en tres capas** contra ese mismo predicado, enumeradas en `Wireframes-Aprovisionamiento-Inicial.md` §4 —ruteo, superficie y acción, «todas contra el mismo predicado»—; shell partido sin chrome y **sin acción de cancelar**, con el fundamento «En el primer arranque no existe un estado previo al que volver»; **destino al completar declarado explícitamente**, `Ingreso`; y **orientación posterior** que «orienta, no bloquea», alojada en `Panel-De-Cuentas`. Los patrones se citan por sección: «Primer arranque §4.2», «§4.3», «§4.4», «§4.5», «§4.6» |
| `Design-Rules-Identidad-De-Version.md` | Aplicado | Los tres requisitos de §1.4 presentes: contrato de identidad declarado en `Representacion-Sello-De-Version.md` §4; **las dos ubicaciones obligatorias** —superficie de acceso, con el sello al pie de la tarjeta en `Aprovisionamiento-Inicial`, `Registro-De-Cuenta`, `Ingreso` y `Credencial-Propia`; y superficie del sistema en funcionamiento, en la barra lateral del shell de trabajo—; y el **distintivo de artefacto preliminar**, el **marcador de origen indeterminado** y el **detalle de diagnóstico con copiado en un solo gesto**, los tres con variante propia y con estados declarados en los wireframes de acceso. La regla «la versión se deriva de la construcción y la vista no la compone» está respetada |
| `Design-Rules-Config-Esquema.md` | **No aplica, con motivo** | «**No hay superficies de configuración que la persona fije.** El único parámetro configurable del proyecto de código es la dirección de la pieza de datos, que es **configuración de entorno** —se inyecta al publicar— y no configuración de aplicación. Por la frontera de `Rules-UX-UI-DX.md` §1.4, un parámetro que la superficie no gobierna **no se dibuja, ni siquiera deshabilitado**». El motivo cita la frontera exacta de la regla y saca la conclusión correcta. Coherente con CU-02 §3, que declara la dirección tomada de configuración |
| `Design-Rules-Acceso-Monousuario.md` | **No aplica, con motivo** | «El producto declara **dos papeles** —alumno y administrador— y tiene gestión de cuentas con registro, habilitación, bloqueo y baja. La condición de carga de la extensión es una sola identidad de operación sin gestión de usuarios, y no se cumple». Y hace algo mejor que descartarla: **declara qué hereda igual, por coincidencia de forma y no por aplicación**, para que no parezca omisión —el shell partido, que la extensión de primer arranque también fija, y el rechazo de credenciales indiferenciado, «que acá viene exigido por `CU-02` §6 y no por esta extensión»— |

Las filas del catálogo aparecen además en la tabla de trazabilidad de `Experiencia-De-Uso.md` §9 con el formato que `Rules-UX-UI-DX.md` §4.3 estandariza, incluidas las cuatro filas de extensión por capacidad con `Sí` o `N/A` y su remisión. **Cumple: la aplicación es real.**

### 8.3 El primer arranque

`Wireframes-Aprovisionamiento-Inicial.md` es, para su tamaño, el documento mejor resuelto de la sección, y lo justifica en su §1: «Es la superficie de mayor consecuencia del producto en relación con su tamaño: si falla, no hay administrador, y sin administrador nadie habilita cuentas y el laboratorio no existe».

Los riesgos clásicos de esta superficie están los cinco cerrados: **el wizard multipaso** no existe —«una sola superficie, un acto indivisible»—; **la salida a medias** no existe —«**No hay acción secundaria y no hay «cancelar»**. En el primer arranque no existe un estado previo al que volver, y ofrecer una salida dejaría el laboratorio a medio configurar sin que nadie lo note»—; **el guard en una sola capa** no ocurre, hay tres contra el mismo predicado; **la condición de carrera** está prevista y resuelta sin culpar a la persona —«Enviar el formulario después de que otro lo aprovisionó → Redirección neutra a `Ingreso`. **No se devuelve un error**: el intento tardío es una condición de carrera esperable y no una falta de la persona»—; y **la segunda apertura** redirige de forma neutra sin explicar el motivo, coherente con `Experiencia-De-Uso.md` §8.3.

Catorce estados declarados, incluidos «Resolviendo destino» y «Ya aprovisionado», que son los dos que una maqueta olvidaría. El estado transitorio de resolución se dibuja como superficie propia, con su fundamento: «la persona ve por menos de un segundo y que igual es un estado del sistema y se muestra como tal».

### 8.4 El P0 abierto en `GeometriaFactory-Application` y su alcance sobre este proyecto de código

El encargo establece que el defecto está en el dominio, que la corrección está en curso, y que **si este proyecto de código asume que el administrador puede entrar inmediatamente después de configurarse, es correcto y no es hallazgo**. Lo que corresponde verificar es que la superficie de aprovisionamiento **cierre el lazo hacia el ingreso**.

**El lazo está cerrado, y en tres tramos declarados:**

1. **Salida del aprovisionamiento.** `Wireframes-Aprovisionamiento-Inicial.md` §5, estado «Éxito»: «Navegación a `Ingreso`, que **acusa recibo** con la banda de confirmación. **El lazo lo cierra la superficie siguiente, no ésta**». El destino al completar está declarado explícitamente y no dejado en la portada por omisión, que es lo que `Design-Rules-Primer-Arranque` exige.
2. **Acuse en el destino.** `Wireframes-Ingreso.md` §5 declara el estado «**Confirmación de aprovisionamiento** → Banda de confirmación que declara qué quedó creado y **que el paso siguiente es entrar**». La superficie de ingreso está diseñada como receptora de cuatro acuses de recibo de actos ocurridos fuera de ella, y éste es uno.
3. **Aterrizaje.** `Experiencia-De-Uso.md` §3.3: «Al completarse, el destino declarado es `Ingreso`, que **acusa recibo** con una banda de confirmación, y **el primer ingreso lleva a `Panel-De-Cuentas` con la orientación posterior**». Y `Wireframes-Panel-De-Cuentas.md` §1: «Es además el **destino al completar el aprovisionamiento**, declarado explícitamente (…): es la primera cosa que el docente necesita hacer con el laboratorio recién configurado».

Ninguna superficie deja al administrador sin camino, y ninguna presupone un estado intermedio de habilitación para él. CU-04 FA-03 declara además la continuidad: «El flujo continúa en CU-02, paso 1». **No es hallazgo, y el diseño es compatible con la corrección en curso del dominio.**

### 8.5 Fase B2 — lo que le tocaba a esta sección antes de que corra

| Obligación de `Rules-UX-UI-DX.md` §1.5 y §6 | Estado |
| --- | --- |
| Cada wireframe corresponde a una superficie maquetable y **declara su nombre canónico**, que es el que va a llevar el archivo HTML de la maqueta | ✔ Los once, en su sección 1, en Título-Con-Guiones, coincidentes entre wireframe, `Experiencia-De-Uso.md` §3.1 y `README.md` §4 |
| La tabla de estados de cada wireframe es **la lista de estados que la maqueta va a tener que demostrar** | ✔ Declarado como tal en `README.md` §5 y en `Glosario-UX.md` §2 («Un estado no declarado no se maqueta y por lo tanto no se valida»), con el mapa consolidado en `Experiencia-De-Uso.md` §4.2. Ciento cincuenta y un estados declarados entre las once superficies. Único desajuste: **H-01** |
| Los flujos clave de `Experiencia-De-Uso` son las rutas que la maqueta va a materializar | ✔ Seis flujos, A a F, con disparador, pasos, fricción y salida |
| AG-03 **no dibuja la maqueta ni define valores visuales concretos** | ✔ Ningún token visual propio; los patrones se referencian por su nombre del catálogo. Los tres valores dimensionales que aparecen —768 px, 320 px, 24×24 px— son punto de quiebre del catálogo, ancho mínimo de legibilidad y objetivo de toque de WCAG, no decisiones visuales |
| Los tres artefactos de la Fase B2 **previstos, no omitidos** | ✔ `README.md` §5, con emisor, momento y contenido |
| Los criterios que dependen de una maqueta aprobada, declarados no aplicables todavía | ✔ `Experiencia-De-Uso.md` §9: «Validación visual de maqueta: **Pendiente**», «Línea de base emitida: **Pendiente**», «Modelo UX-UI aplicado en la Fase B2: **Sin definir a esta fecha**. Lo elige el humano en el paso 1 de la Fase B2» |

Los tres supuestos abiertos están rotulados y **dirigidos explícitamente a la Fase B2** como las cosas que la maqueta puede desmentir: el punto de quiebre, la proporción de la escena y el volumen de la comisión. Es el uso correcto de la fase.

---

## 9. Veredicto y condiciones para promover

### 9.1 Veredicto

> ## APROBADO CON OBSERVACIONES

**P0 = 0 · P1 = 0 · P2 = 3 · P3 = 7.**

No hay ningún hallazgo bloqueante. La trazabilidad está completa y es resoluble en las dos direcciones; ningún documento obligatorio falta; las omisiones declaradas son las tres que el encargo anticipa y están fundadas en el lugar y en la forma que corresponde, incluida la del modelo conceptual como **decisión técnica con ADR pendiente en 05** y no como no-aplicabilidad; no hay vocabulario prohibido de la norma de producto; las cabeceras están completas en los treinta; y **las tres reglas de arquitectura de nivel producto bajan a restricción con punto de verificación medible y ninguna superficie las contradice**.

Los tres P2 son de completitud formal —un estado canónico no nombrado, tres ocurrencias de una forma léxica arrastrada del catálogo, y una fila de trazabilidad downstream con granularidad de categoría en tres artefactos— y **ninguno impide que la maqueta se construya ni que se valide**.

### 9.2 Condiciones para promover

Las diez correcciones son absorbibles **dentro de la versión 1.0**, sin subir minor, por la política de versionado de `Master-Prompt.md` §5: los treinta documentos están en estado `Propuesto` y el audit forma parte del ciclo de emisión. Cada corrección deja su fila en el control de cambios del documento afectado citando el hallazgo de este informe que la origina.

**Antes de dar la fase por cerrada:**

1. Corregir **H-01**, **H-02** y **H-03**, que son los P2.
2. Corregir **H-04** a **H-10**, que son de una línea cada uno.

**Recomendado, no condición:** que la corrección de H-02 se ejecute por barrido de ocurrencias y no por sustitución global de cadena, según `Vocabulario-Rules.md` §9.5, y que su registro declare cuántas ocurrencias se revisaron y cuántas se cambiaron.

### 9.3 Sobre el arranque de la Fase B2

> **La Fase B2 puede arrancar.**

Los insumos que `Maqueta-Rules.md` requiere de esta categoría están completos: once superficies con **nombre canónico estable** que la maqueta va a reusar como nombre de archivo y la línea de base como `SUP-XX`; ciento cincuenta y un estados declarados como el conjunto que la maqueta tiene que demostrar, consolidados además en un mapa único; seis flujos clave como rutas de navegación; tres representaciones reutilizables que los wireframes invocan por nombre; y un glosario de superficie que fija el vocabulario con el que el inventario identificado va a nombrar lo aprobado. Los tres artefactos que la fase produce están declarados **previstos** y no omitidos, con su emisor y su momento.

**H-01 conviene corregirlo antes de que la maqueta se construya**, no porque bloquee la fase, sino porque la sección 5 de cada wireframe es literalmente el contrato de lo que se va a maquetar: un estado que no está nombrado ahí no se maqueta y por lo tanto no se valida, y el estado con datos de `Resolucion-Del-Trabajo` sí tiene que demostrarse. Los otros nueve hallazgos no tienen efecto sobre la maqueta y pueden corregirse en paralelo.

La elección del modelo UX-UI de `Devs/Modelos-UX-UI/` queda correctamente pendiente del paso 1 de la Fase B2 y es del humano, tal como esta sección declara.

---

## Control de cambios de este informe

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Ronda 1 de auditoría de la Fase B de `GeometriaFactory-Web`, categorías 02 y 03, sobre treinta documentos. Matriz D1–D9 completa; matriz de estructura obligatoria con las once secciones de cada caso de uso, las nueve de cada wireframe, las once del marco de experiencia y las siete de cada representación; verificación de las tres reglas de arquitectura de nivel producto con su bajada, su medibilidad y su contraste contra las once superficies; verificación de accesibilidad con foco en la resolución de la escena tridimensional; coherencia cross-doc, gobierno del glosario en sus cuatro criterios y ocho polisemias evaluadas y descartadas por contextos disjuntos; diez hallazgos, ninguno bloqueante. Veredicto APROBADO CON OBSERVACIONES, con la Fase B2 habilitada para arrancar. | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
