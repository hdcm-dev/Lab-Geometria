# Informe de auditoría — Fase B · GeometriaFactory-Application · categorías 02 y 03 · ronda 1

**Producto:** Fábrica de Geometría
**Fase auditada:** B (02-Especificacion-Funcional y 03-UX-UI-DX)
**Unidad de entrega:** GeometriaFactory-Api
**Alcance:** los diecisiete documentos de `SDD/Docs/Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` y `.../03-UX-UI-DX/`, contra `Rules-Especificacion-Funcional.md`, `Rules-UX-UI-DX.md`, `Vocabulario-Rules.md`, D1-D9, el upstream de nivel producto (00, 01), el intake 1.3, el manifiesto 1.1 y `GeometriaFactory-Domain/02-Especificacion-Funcional/`
**Auditor:** Arquitecto de Soluciones + QA Senior, invocado desde cero, sin participación en la generación
**Fecha:** 2026-08-09
**Ronda:** 1 (primera auditoría de este proyecto de código)

**Categoría 04:** omitida por gating (`usa_llm` == false). Su ausencia no es hallazgo y no se evalúa.

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Matriz D1-D9 por documento](#2-matriz-d1-d9-por-documento)
- [3. Matriz de estructura obligatoria por documento](#3-matriz-de-estructura-obligatoria-por-documento)
- [4. Verificación mecánica del catálogo de errores](#4-verificación-mecánica-del-catálogo-de-errores)
- [5. Verificación de la orquestación](#5-verificación-de-la-orquestación)
- [6. Coherencia cross-doc y gobierno del glosario](#6-coherencia-cross-doc-y-gobierno-del-glosario)
- [7. Hallazgos](#7-hallazgos)
- [8. Veredicto y condiciones para promover](#8-veredicto-y-condiciones-para-promover)

---

## 1. Resumen ejecutivo

Los diecisiete documentos existen, nacen en `Versión: 1.0`, estado `Propuesto`, fecha 2026-08-09, sin sufijo de versión y sin `_legacy/`; los 114 enlaces relativos resuelven; el catálogo de veintisiete condiciones de error cuadra con diferencia simétrica vacía contra las §6 de los nueve casos de uso, recontado de forma independiente; las tres negativas de autorización están tratadas con el error simétrico incluido y ancladas en criterios verificables; la frontera «autoriza, no autentica» está declarada y ningún artefacto la cruza; el punto abierto del puerto de repositorio de cuentas está bien fundado y bien declarado, y ningún artefacto lo usa como si el intake lo hubiera nombrado.

Se emiten **catorce hallazgos: 1 P0, 2 P1, 4 P2 y 7 P3**. El P0 es de fidelidad de la orquestación: `CU-01` constituye la cuenta del administrador **habilitada**, comportamiento que `GeometriaFactory-Domain` rechaza con un código propio, y lo ancla en un criterio de aceptación que no puede pasar. Los dos P1 son un motivo propagado que el dominio no devuelve y un dato de entrada obligatorio del dominio que esta capa nunca aporta.

**Veredicto: RECHAZADO** (existe un P0). La calidad general del cuerpo documental es alta y los defectos son localizados: corregidos los tres primeros, el conjunto queda en condiciones de promover.

---

## 2. Matriz D1-D9 por documento

Leyenda: **C** cumple · **O** cumple con observación · **X** incumple · **n/a** no aplica.

D1 idioma español rioplatense neutro sin emojis · D2 encoding y tablas completas · D3 Título-Con-Guiones y prefijos en mayúscula · D4 sin sufijo de versión en el archivo vivo · D5 versionado y control de cambios · D6 trazabilidad · D7 sin vocabulario del dominio fuente ni stacks concretos · D8 conjunto cerrado de tipo de proyecto de código · D9 evidencia verificable.

| Documento | D1 | D2 | D3 | D4 | D5 | D6 | D7 | D8 | D9 |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `02/Especificacion-Funcional.md` | C | C | C | C | C | **O** (H-04, H-05, H-07) | C | C | C |
| `02/Glosario-Funcional.md` | C | C | C | C | C | C | C | C | C |
| `02/README.md` | C | C | C | C | C | C | C | C | C |
| `02/CU-01-Registrar-El-Alta-De-Una-Cuenta.md` | C | C | C | C | C | **X** (H-01) | C | C | **X** (H-01) |
| `02/CU-02-Gobernar-Las-Cuentas-De-La-Comision.md` | C | C | C | C | C | **O** (H-07, H-14) | C | C | C |
| `02/CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md` | C | C | C | C | C | **O** (H-06, H-14) | C | C | C |
| `02/CU-04-Cargar-Y-Reeditar-Un-Trabajo-Propio.md` | C | C | C | C | C | **O** (H-06, H-14) | C | C | C |
| `02/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md` | C | C | C | C | C | **X** (H-02, H-03) | C | C | C |
| `02/CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md` | C | C | C | C | C | C | C | C | C |
| `02/CU-07-Revisar-Los-Trabajos-De-La-Comision.md` | C | C | C | C | C | C | C | C | C |
| `02/CU-08-Dar-Desenlace-A-Un-Trabajo.md` | C | C | C | C | C | **O** (H-07) | C | C | C |
| `02/CU-09-Eliminar-Un-Trabajo.md` | C | C | C | C | C | C | C | C | C |
| `03/DX-Developer-Experience.md` | C | C | C | C | C | **O** (H-05 heredado) | C | C | C |
| `03/DX-Error-Messages.md` | **O** (H-10) | C | C | C | C | C | C | C | C |
| `03/Guia-Onboarding-Developer.md` | **O** (H-11) | C | C | C | C | **O** (H-05 heredado) | C | C | C |
| `03/Glosario-UX.md` | C | C | C | C | C | C | C | C | C |
| `03/README.md` | C | C | C | C | C | C | C | C | C |

Notas sobre la matriz:

- **D4 y D5, verificados mecánicamente.** Los diecisiete archivos declaran `Versión: 1.0`, `Estado: Propuesto`, `Fecha: 2026-08-09` y `Autor` (AG-02 en 02, AG-03 en 03); ninguno lleva sufijo de versión; **no existe `_legacy/` bajo `GeometriaFactory-Application/`**, y su ausencia es correcta por tratarse de la emisión inicial. Los cinco artefactos de 03 declaran `Variante: DX`. Los diecisiete tienen tabla de contenido y sección de control de cambios.
- **D6.** Ninguna cabecera cita «PRODUCT-INTAKE» a secas: las diecisiete citas de intake traen secciones concretas (`§17.2.P.1`, `§4.1 (RN-03, RN-04, RN-08)`, `§20.E-1`, etcétera). El upstream de nivel producto (00 y 01) y `GeometriaFactory-Domain` figuran en la cabecera del índice maestro y en las de los artefactos de 03. Las marcas O y X remiten a hallazgos de contenido, no de forma de la cabecera.
- **D7.** No hay menciones a motor de persistencia, protocolo ni producto comercial como capacidad propia. Las dos apariciones de «HTTP» (`03/README.md:94`, `Guia-Onboarding-Developer.md:238`) ubican responsabilidad **afuera** de esta capa, que es el uso correcto. La única cita de identificadores de código —`IRepositorioTrabajos`, `IValidadorFiguras`, `IRelojDelSistema`— está confinada a `Especificacion-Funcional.md:61` dentro de 02, tal como ese párrafo declara, y a las dos notas de punto abierto de 03.
- **D9.** No hay sistema construido y ningún documento afirma que lo haya. Las afirmaciones ejecutables van rotuladas como criterio o como compromiso: `DX-Developer-Experience.md:157` declara «Los pasos son ejecutables a partir de la etapa `a`» y fija la verificación en el punto de control. La única X es H-01, que afirma un comportamiento del dominio contradicho por el propio dominio.

---

## 3. Matriz de estructura obligatoria por documento

### 3.1 Categoría 02, contra `Rules-Especificacion-Funcional.md` §2.1, §2.2 y §4

| Exigencia | Regla | Resultado |
| --- | --- | --- |
| `Especificacion-Funcional.md` con índice maestro y matriz NB→CU→RN→US | §2.1, §6 | **Cumple.** Índice en §5, matriz en §7.1, cobertura bidireccional en §7.2 |
| Mínimo de CU para `library` | §2.2 (mínimo 5) | **Cumple.** Nueve casos de uso, serie contigua CU-01 a CU-09 |
| Las once secciones obligatorias de §4.2 en cada CU | §4.2, §6 | **Cumple en los nueve.** Verificado por encabezados: §1 a §11 presentes y en orden en CU-01 a CU-09 |
| Sección opcional por tipo D8 sin desplazar obligatorias | §4.3 (§17 para `library`) | **Cumple.** Los nueve llevan «§17 Compatibilidad de la superficie pública» después de §11; `02/README.md:71` lo declara explícitamente |
| Mínimo tres criterios Given/When/Then con valores concretos | §4.2 punto 8, §6 | **Cumple con holgura.** CU-01 a CU-04 y CU-06 a CU-09 con 5; CU-05 con 6. Todos con valores concretos (fechas, cantidades, correos, valores 36.00/54.00) |
| Trazabilidad NB→CU→US en cada CU | §4.4, §6 | **Cumple en los nueve.** Tabla de §9 con siete a ocho dimensiones, incluidas «Casos de uso de dominio orquestados» y «Puertos que consume», propias de esta capa |
| `Reglas-De-Negocio/RN-XX` | §2.2 (`library`: no obligatorias) | **Omitidas con motivo declarado** en `Especificacion-Funcional.md` §9 y `README.md` §4. Correcto |
| `Modelo-Datos/Modelo-Conceptual.md` y `RC-XX` | §2.2 (`library`: no) | **Omitidos con motivo declarado**, con cita a `tiene_persistencia` == false e intake §17.2.P.4. Correcto |
| `Definicion-<Concepto-Central>.md` | §2.1 (recomendado para `library` con superficie estrecha) | **Omitido con motivo declarado.** Correcto |
| `Glosario-Funcional.md` con las cinco secciones de §4.2.4 y tabla no vacía | §4.2.4, §6 | **Cumple.** §1 alcance, §2 tabla de 12 términos, §3 términos con más de un referente (cinco subsecciones más §3.6), §4 referenciados y no redefinidos (§4.1 al raíz, §4.2 a Domain), §5 control de cambios. La §3 no se omite |
| `README.md` de sección | §2.1 (recomendado) | **Cumple.** Índice, orden de lectura y omisiones con motivo |
| Un solo archivo por nombre lógico, sin sufijo, sin `_legacy/` | §6 | **Cumple** |
| Slugs sin mayúsculas indebidas, espacios ni acentos | §6 | **Cumple.** Título-Con-Guiones estricto, prefijos `CU` en mayúscula |
| Tabla de contenido tras la cabecera | §4.1, §6 | **Cumple en los doce** |

### 3.2 Categoría 03, contra `Rules-UX-UI-DX.md` §2.1, §2.2 y §4

| Exigencia | Regla | Resultado |
| --- | --- | --- |
| Variante declarada y coherente con el tipo D8 | §6 | **Cumple.** `Variante: DX` en los cinco artefactos; `library` + `tiene_ui_final` == false |
| Los tres `dx-` obligatorios para `library` | §2.2 | **Cumple.** `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md` |
| Mínimo de wireframes | §2.2 (`library`: 0) | **Cumple.** Cero, con el motivo declarado en `03/README.md:32` |
| `DX-Developer-Experience.md` con las nueve secciones de §4.2.3 | §4.2.3, §6 | **Cumple, numeración intacta.** §1 Rol · §2 Onboarding por tramos · §3 Quick-start · §4 Diátaxis · §5 Mensajes de error · §6 Métricas DX · §7 Feedback loop · §8 Trazabilidad · §9 Control de cambios |
| Onboarding por tramos verificables 5/30/60 | §6 | **Cumple.** Los tres tramos con objetivo verificable y no con lectura declarada |
| `Guia-Onboarding-Developer.md` con las seis secciones de §4.2.4 **conservando su numeración** | §4.2.4 | **Cumple, y es el tratamiento correcto.** §1 Audiencia y prerrequisitos · §2 Instalación o acceso · §3 Primer ejemplo ejecutable · §4 Diagnóstico de problemas frecuentes · §5 Próximos pasos · §6 Control de cambios. El contenido propio —«La inversión de dependencias, en la práctica»— va **al final, como §7**, después del control de cambios, sin desplazar ninguna. `Guia-Onboarding-Developer.md:200` lo declara |
| `DX-Error-Messages.md` con las seis secciones de §4.2.5 | §4.2.5 | **Cumple en contenido, con observación de numeración (H-09).** §1 Principios · §2 Taxonomía · §3 Catálogo · §4 Tono y voz · §5 Localización · §7 Control de cambios. Se intercala §6 «Cobertura y trazabilidad», que la §6 de la regla exige por otra vía, y el control de cambios queda numerado 7 |
| Quick-start verificable con snippet ejecutable en cada `dx-` doc | §6 | **Cumple en dos y se declara no aplicable en el tercero con motivo.** `DX-Developer-Experience.md` §3 y `Guia-Onboarding-Developer.md` §2-§3 lo traen; `DX-Error-Messages.md` §6.4 lo declara no aplicable por ser del modo reference, **sin darlo por cumplido**. `03/README.md:89` lo repite. Es el tratamiento correcto |
| Trazabilidad upstream y downstream en cada artefacto | §6 | **Cumple en los cinco** |
| `Glosario-UX.md` con tabla no vacía y términos con más de un referente | §6 | **Cumple.** 23 filas; §3.1 «error» con cuatro referentes y §3.2 «negativa» con dos |
| No duplicación con `Glosario-Funcional.md` de 02 ni con el raíz | §6 | **Cumple.** Ver §6.2 de este informe |
| WCAG 2.2 AA | §6 | **No aplicable, declarado con motivo y sin darlo por cumplido** (`03/README.md:84`). Correcto |
| Artefactos de la variante UX/UI y de maqueta | §2.1, §6 | **Omitidos con motivo declarado, los ocho.** `03/README.md` §4 enumera `Experiencia-De-Uso.md`, `wireframes-<superficie>.md`, `representacion-<concepto>.md`, `DX-Portal-Developers.md`, `DX-Operability.md`, `Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md`, cada uno con su flag. Además §5 declara siete criterios de §6 no aplicables sin darlos por cumplidos. **Es el tratamiento que la regla pide y no hay hallazgo** |
| Sin sufijo de versión, un archivo por nombre lógico | §6 | **Cumple** |
| Tabla de contenido tras la cabecera | §4.1, §6 | **Cumple en los cinco** |

---

## 4. Verificación mecánica del catálogo de errores

Recontado de forma independiente, sin apoyarse en el recuento que el propio documento declara.

### 4.1 Identificadores extraídos de las §6 de los nueve casos de uso

| CU | Filas en su §6 | Identificadores |
| --- | --- | --- |
| CU-01 | 4 | `CORREO_YA_REGISTRADO`, `DATO_OBLIGATORIO_AUSENTE`, `ADMINISTRADOR_YA_CONFIGURADO`, `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` |
| CU-02 | 5 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CONFIRMACION_DE_BAJA_NO_COINCIDE`, `TRANSICION_DE_CUENTA_NO_ADMITIDA`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`, `CUENTA_INEXISTENTE` |
| CU-03 | 6 | `CUENTA_PENDIENTE`, `CUENTA_BLOQUEADA`, `CREDENCIAL_NO_ESTABLECIDA`, `CUENTA_NO_HABILITADA_PARA_CREDENCIAL`, `CREDENCIAL_VIGENTE_NO_VERIFICADA`, `CUENTA_INEXISTENTE` |
| CU-04 | 4 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `DATO_OBLIGATORIO_AUSENTE`, `TRABAJO_SIN_DUENO` |
| CU-05 | 4 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `ENVIO_FUERA_DE_BORRADOR`, `INTERPRETACION_NO_DISPONIBLE`, `OBSERVACION_MAL_FORMADA` |
| CU-06 | 2 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `SOLICITANTE_NO_DECLARADO` |
| CU-07 | 3 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `TRABAJO_INEXISTENTE` |
| CU-08 | 5 | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `DESENLACE_FUERA_DE_PENDIENTE`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `DESENLACE_DESCONOCIDO` |
| CU-09 | 4 | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `PAPEL_NO_RECONOCIDO` |
| **Total** | **37 filas** | **27 identificadores distintos** |

Las seis condiciones que aparecen en más de un caso de uso son `DATO_OBLIGATORIO_AUSENTE`, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_INEXISTENTE`, `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR` y `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, con 10 reapariciones. 27 + 10 = 37. **El cuadre que `DX-Error-Messages.md` §6.1 y §6.2 declaran es exacto.**

### 4.2 Contraste contra el catálogo de `DX-Error-Messages.md` §3

| Comprobación | Resultado |
| --- | --- |
| Identificadores distintos en las §6 de los nueve CU | 27 |
| Identificadores distintos en las tablas de §3.1 a §3.9 | 27 |
| **Diferencia simétrica** | **Vacía.** Ninguna condición del catálogo carece de respaldo en una §6, y ninguna condición de una §6 quedó sin entrada |
| Condiciones inventadas por 03 | 0 |
| Condiciones sin entrada | 0 |
| Filas de tabla en §3 | 28, por la doble fila de `DATO_OBLIGATORIO_AUSENTE` (§3.1 y §3.4). Ver H-08 |

Verificación adicional de la taxonomía de §2.1, recontada entrada por entrada sobre las tablas de §3: entrada inválida 8, recurso ausente 4, conflicto de estado 10, conflicto de facultad 2, conflicto de alcance 1, error transitorio 1, error interno 1. **Suman 27 y coinciden con los valores declarados.**

### 4.3 Citas de motivo en las §5 de flujos alternativos

| CU | Citas de motivo en su §5 | ¿Alguna ausente de su propia §6? |
| --- | --- | --- |
| CU-01 | 2 (`ADMINISTRADOR_YA_CONFIGURADO`, `CORREO_YA_REGISTRADO`) | No |
| CU-02 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`) | No |
| CU-03 | 2 (`CREDENCIAL_NO_ESTABLECIDA`, `CREDENCIAL_VIGENTE_NO_VERIFICADA`) | No |
| CU-04 | 2 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`) | No |
| CU-05 | 1 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`) | No |
| CU-06 | 0 | — |
| CU-07 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`) | No |
| CU-08 | 2 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRANSICION_DESDE_ESTADO_TERMINAL`) | No |
| CU-09 | 3 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`) | No |
| **Total** | **16** | **Ninguna** |

**Las dieciséis citas de motivo de las §5 corresponden a un motivo ya declarado en la §6 del mismo caso de uso. Ninguna introduce una condición que la §6 no declare.** La cifra de dieciséis que `DX-Error-Messages.md:337` declara es exacta.

### 4.4 Las tres negativas de autorización

| Comprobación | Resultado |
| --- | --- |
| Distinción pertenencia / facultad / alcance sostenida | **Sí.** `Especificacion-Funcional.md` §4 (tabla y cuatro precisiones), `DX-Error-Messages.md` §2.4, `DX-Developer-Experience.md` §1.4 y `Guia-Onboarding-Developer.md` §3.5. Las cuatro presentaciones son consistentes entre sí |
| La pertenencia responde «no encontrado» y nunca «no autorizado» | **Sí**, y con la razón declarada: confirmar la existencia habilita el tanteo. CU-04 §6, CU-06 §6, CU-09 §6, `Especificacion-Funcional.md:80` |
| La facultad admite negativa explícita | **Sí**, con el motivo correcto: «no hay recurso ajeno cuya existencia proteger» (`Especificacion-Funcional.md:81`, `DX-Error-Messages.md:130`) |
| Tabla de traducciones prohibidas con el **error simétrico** | **Sí.** `DX-Error-Messages.md` §2.4 declara cuatro, la cuarta es «`FACULTAD_DE_ADMINISTRADOR_REQUERIDA` → «no encontrado» ... El error simétrico, y también es un defecto: oculta lo que no hace falta ocultar y deja al integrador sin diagnóstico». Es exactamente la comprobación pedida y está presente |
| Criterios de aceptación que la anclan, existentes y verificables | **Sí.** CA-03 de CU-06 y CA-03 de CU-09 exigen que el motivo del recurso ajeno sea **el mismo** que el del identificador inexistente; CA-05 de CU-05 lo verifica contando 0 invocaciones del validador doble; CA-03 de CU-07 cuenta 0 consultas al repositorio. Los cuatro son mecánicamente evaluables |
| Métrica con tolerancia cero | **Sí.** `DX-Developer-Experience.md` §6, fila «Traducciones prohibidas», objetivo «0, sin tolerancia» |

### 4.5 Frontera de autenticación

Declarada en `Especificacion-Funcional.md:46` («Es autorización, no autenticación: no se comparan contraseñas ni se emiten accesos») y desarrollada en tabla en `DX-Developer-Experience.md` §1.3, con ocho filas que separan lo que vive acá de lo que vive afuera. **Ningún artefacto la cruza:** no hay comparación de contraseña, derivación, emisión de acceso ni sesión documentada como capacidad propia; CU-03 recibe el valor de credencial **ya derivado** y exige que la verificación de la vigente se **declare**, que es la forma de hacer exigible la regla sin conocer el mecanismo. Coherente con el intake §17.2.P.5.

### 4.6 El punto abierto del puerto de repositorio de cuentas

| Comprobación | Resultado |
| --- | --- |
| ¿Está nombrado en lenguaje de dominio? | **Sí.** «puerto de repositorio de cuentas» en los cuatro lugares donde aparece; nunca se le inventa un identificador de tipo |
| ¿Está declarado como punto abierto? | **Sí.** `Especificacion-Funcional.md` §11, primera fila, y §3 párrafo de cierre |
| ¿Está diferido a 05 y al punto de control de la primera etapa? | **Sí**, en los tres artefactos que lo tocan |
| ¿Está fundado? | **Sí, y bien.** `Especificacion-Funcional.md:70` apoya la necesidad en que `GeometriaFactory-Domain` asigna a esta capa la verificación de unicidad del correo «sobre el conjunto de alumnos» —verificado: RN-01 §3 último punto y Domain CU-01 §6 `UNICIDAD_DE_CORREO_NO_VERIFICADA`—, y en que ninguna verificación sobre un conjunto es posible sin una frontera que lo alcance |
| ¿Se lo usa como si estuviera declarado en el intake? | **No, en ningún lugar.** Los tres artefactos que lo mencionan (`Especificacion-Funcional.md` §3, `DX-Developer-Experience.md:70`, `Guia-Onboarding-Developer.md:219`) declaran explícitamente que **el intake nombra tres puertos y no éste**. `Guia-Onboarding-Developer.md:172` incluso convierte la búsqueda fallida del identificador en un diagnóstico de la primera hora |

**Es un punto abierto legítimo, bien fundado y bien declarado. No es hallazgo.**

---

## 5. Verificación de la orquestación

### 5.1 Correspondencia declarada contra el cuerpo de cada caso de uso

Se contrastó la tabla de `Especificacion-Funcional.md` §7.4 con el paso o flujo alternativo del cuerpo de cada CU que efectivamente invoca al dominio.

| CU de Application | CU de Domain que el **cuerpo** invoca | ¿Respaldado? |
| --- | --- | --- |
| CU-01 Registrar el alta de una cuenta | CU-01 (paso 4, constitución del alumno) | Sí |
| CU-02 Gobernar las cuentas de la comisión | CU-02 (paso 4, transición; FA-03, rechazo de baja del administrador) | Sí |
| CU-03 Resolver el ingreso y la credencial | CU-04 (paso 3, admisibilidad); CU-03 (FA-02 fijación, FA-03 reemplazo) | Sí |
| CU-04 Cargar y reeditar un trabajo propio | CU-05 (paso 3, constitución); CU-09 (FA-01, acceso del alumno) | Sí |
| CU-05 Enviar un trabajo e interpretar su texto | CU-06 (paso 4, piezas); CU-07 (paso 5, observaciones); CU-08 (paso 6, estado); CU-09 (paso 2, pertenencia) | Sí, **con la salvedad de H-03** |
| CU-06 Consultar los trabajos propios | CU-09 (FA-01, si el solicitante puede verlo) | Sí |
| CU-07 Revisar los trabajos de la comisión | CU-11 (paso 3, predicado de alcance; FA-03, detalle) | Sí |
| CU-08 Dar desenlace a un trabajo | CU-11 (paso 3, alcance); CU-10 (pasos 4-5, desenlace) | Sí |
| CU-09 Eliminar un trabajo | CU-09 (paso 3, alumno); CU-11 (FA-01, administrador) | Sí |

**Ninguna orquestación declarada en §7.4 es una afirmación vacía:** las nueve están respaldadas por el cuerpo del caso de uso que las declara.

### 5.2 Huérfanos

| Dirección | Resultado |
| --- | --- |
| CU de Domain sin orquestar | **Ninguno.** Los once quedan cubiertos: Dom CU-01→App CU-01; Dom CU-02→App CU-02; Dom CU-03 y CU-04→App CU-03; Dom CU-05→App CU-04; Dom CU-06, CU-07 y CU-08→App CU-05; Dom CU-09→App CU-04, CU-05, CU-06 y CU-09; Dom CU-10→App CU-08; Dom CU-11→App CU-07, CU-08 y CU-09 |
| CU de Application sin NB upstream | **Ninguno.** Los nueve trazan a al menos una necesidad de negocio (§7.2) |
| NB sin CU en este proyecto de código | **NB-00008**, declarada como alerta explícita con su motivo y su lugar de cobertura. Es el tratamiento correcto, no un silencio |

### 5.3 Fidelidad de lo que Application afirma sobre el dominio

**Lo que coincide, verificado y sin hallazgo:**

- Los cuatro estados del trabajo y los tres de cuenta se usan con el mismo vocabulario en las dos capas. **Ningún artefacto trata la baja como estado de cuenta**, coherente con `Definicion-Modelo-De-Dominio.md:210` («La baja no es un estado»).
- Las cinco citas `INV-XX` son correctas: INV-01 en CU-01, INV-05 en CU-01 y CU-02, INV-02 en CU-04, CU-06 y CU-09, INV-03 en CU-09. Ninguna atribución errónea.
- Pertenencia indistinguible de inexistencia y acotación al `Borrador`: App CU-04, CU-06 y CU-09 son fieles a Dom CU-09, con los motivos `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` y `OPERACION_FUERA_DE_BORRADOR` textualmente idénticos.
- Alcance del administrador: App CU-07, CU-08 y CU-09 fieles a Dom CU-11, incluido el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` para el borrador. **El CA-05 de App CU-08 devuelve ese motivo y no `DESENLACE_FUERA_DE_PENDIENTE`, y es correcto**, porque App CU-08 consulta el alcance (Dom CU-11) antes que el desenlace (Dom CU-10): se evaluó y no es hallazgo.
- Terminalidad, comentario opcional y exclusividad del administrador: App CU-08 fiel a Dom CU-10 y a RN-10.
- Admisibilidad: `CUENTA_PENDIENTE`, `CUENTA_BLOQUEADA`, `CREDENCIAL_NO_ESTABLECIDA`, `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` y `CREDENCIAL_VIGENTE_NO_VERIFICADA` existen literalmente en Dom CU-03 y CU-04.
- Posición reservada de una figura no reconstruida, advertencia no bloqueante y error de validación bloqueante: App CU-05 fiel a Dom CU-06 FA-03, Dom CU-07 y Dom CU-08.

**Lo que no coincide:** H-01 (P0), H-02 (P1), H-03 (P1) y H-06 (P2), desarrollados en §7.

### 5.4 Las once reglas: referenciadas, no redactadas

| Comprobación | Resultado |
| --- | --- |
| ¿Se redacta alguna RN en esta capa? | **No hay ningún archivo `RN-XX` ni ningún enunciado normativo autónomo**, con la excepción crítica de H-01, que enuncia como regla del dominio un estado inicial que el dominio no tiene |
| ¿Se referencian las once por identificador y con enlace relativo? | **Sí.** `Especificacion-Funcional.md` §6 trae las once con enlace a `../../GeometriaFactory-Domain/.../RN-XX-....md`, y cada CU las repite en su §9 con enlace `../../../GeometriaFactory-Domain/...` |
| ¿Resuelven los enlaces? | **Sí, los 114 enlaces relativos de 02 y los de 03 resuelven a archivos existentes.** Ninguno roto |
| ¿Se distingue «dónde está enunciada» de «dónde se ejerce»? | Sí, y está declarado en `Especificacion-Funcional.md:109`, aunque esa misma línea contiene el defecto H-04 |

---

## 6. Coherencia cross-doc y gobierno del glosario

### 6.1 Coherencia cross-doc

| Comprobación | Resultado |
| --- | --- |
| 03 construida sobre los nueve casos de uso de 02 | **Sí, y de forma rastreable.** `DX-Error-Messages.md` deriva cada entrada de la §6 de un CU y deja su verificación escrita en §6.2; `03/README.md:93` lo declara como principio de autoridad («Lo que 02 no declara, esta sección no lo declara») |
| Enlaces entre archivos de la fase | **Todos resuelven.** Verificación mecánica sobre los diecisiete documentos |
| Identificadores no duplicados | **Cumple.** CU-01 a CU-09 contiguos y únicos; sin `CA-XX` ni `FA-XX` repetidos dentro de un mismo CU; US-01 a US-27 sin repeticiones en `Especificacion-Funcional.md` §7.3 |
| Localidad de los identificadores declarada | **Sí.** `Especificacion-Funcional.md` §10 y `02/README.md:69` advierten que los `CU-XX` son locales y que la correspondencia se lee por §7.1 y §7.4, nunca por número |
| Cifras que 03 afirma sobre 02 | **Correctas.** 27 condiciones, 37 filas, 6 repetidas, 10 reapariciones, 16 citas de §5, nueve CU, cuatro puertos: todas recontadas y coincidentes |
| Cifras internas de 03 | **Correctas.** Dieciocho diagnósticos en `Guia-Onboarding-Developer.md` §4, siete métricas en `DX-Developer-Experience.md` §6 («tres canónicas + cuatro propias»), ocho omisiones y siete criterios no aplicables en `03/README.md` |
| Inconsistencias detectadas | H-04, H-05 y H-07, en §7 |
| Vocabulario normativo de `Vocabulario-Rules.md` §10 | Cumple, salvo H-10. «Proyecto» a secas no se usa en ningún artefacto —siempre «proyecto de código» o «proyecto de prueba»—, y así lo declaran `Glosario-Funcional.md:35` y `Glosario-UX.md:39`. Los cuatro planos de identidad son distinguibles. Los seis términos conservan su sentido y se declara explícitamente que no se redefinen |

### 6.2 Gobierno del glosario, los cuatro criterios

**Tres capas.** Raíz en `Vision-Producto.md` §9, glosario de 02 y glosario de 03. La regla de referenciar y no redefinir se cumple y está declarada en las tres direcciones: `Vision-Producto.md:177` («Las categorías 02 y 03 referencian sus términos en lugar de redefinirlos»), `Glosario-Funcional.md:33` y `Glosario-UX.md:37` («Ninguna entrada de §2 pisa a ninguna de las tres fuentes»). El glosario de 02 despliega 18 punteros al raíz y 13 al de `GeometriaFactory-Domain`; el de 03 despliega las tres fuentes en §4.1, §4.2 y §4.3.

| Criterio | Resultado |
| --- | --- |
| **Sin contradicciones** | **Cumple.** Verificado entrada por entrada: ningún término tiene dos definiciones incompatibles entre artefactos de la fase ni contra el raíz. Los términos de 03 que rozan a 02 —`motivo`, `puerto`, `verificación de pertenencia`, `verificación de facultad`, `alcance del administrador`, `unidad de trabajo`— **citan** a 02 en lugar de redefinirlo |
| **Completitud** | **Cumple.** Tabla de 12 términos en 02 y de 23 en 03, ninguna vacía, con la columna de artefactos donde cada término aparece. Única laguna, sin severidad: «escenario semilla» circula en `CU-04` y `CU-05` y no está declarado en ninguna de las tres capas; es vocabulario heredado de 01 y no acuñado por esta fase, así que el criterio no lo alcanza |
| **Polisemia gobernada** | **Cumple en lo sustantivo.** 02 declara cinco polisemias con referentes, forma obligatoria y evidencia de colisión —repositorio, pieza, `Pendiente`, rol, trabajo/unidad de trabajo—; 03 declara dos —error con cuatro referentes, negativa con dos—. La única forma desnuda que queda sin resolver en una sección que se despacha por separado es la de H-11, y es de baja severidad |
| **Criterio negativo** | **Cumple, y de forma ejemplar.** Los dos glosarios **anticipan** el falso positivo y declaran los casos de contextos disjuntos para que una ronda posterior no los levante: `Glosario-Funcional.md` §3.6 y `Glosario-UX.md` §3.3, esta última con la frase «Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica» |

**Decisiones cerradas aguas arriba, verificadas una por una:**

| Decisión | Resultado |
| --- | --- |
| `Pendiente` siempre calificado | **Cumple, cero formas desnudas reales.** Barrido completo sobre los diecisiete documentos: todas las ocurrencias no calificadas caen dentro de las dos excepciones declaradas —enumeración del conjunto cerrado (`CU-02:94`, `CU-06:85`) e identificadores literales (`CUENTA_PENDIENTE`, `DESENLACE_FUERA_DE_PENDIENTE`)— o son menciones metalingüísticas del propio término. En prosa siempre «cuenta `Pendiente`» o «trabajo en estado `Pendiente`» |
| Sin calificar enumeraciones del conjunto cerrado ni identificadores literales | **Cumple, y la excepción está declarada** en `Glosario-Funcional.md` §3.3, `DX-Error-Messages.md` §4 y `Glosario-UX.md` §3.3 |
| Los dos referentes de «pieza», con el desplegable calificado | **Cumple.** `Glosario-Funcional.md` §3.2 despliega los dos; el referente desplegable sólo aparece calificado («pieza pública», «piezas desplegables») |
| «trabajo» no es «unidad de entrega» | **Cumple.** Declarado en `Glosario-Funcional.md:102` y en `Glosario-UX.md:154`, con la razón: es un registro de datos y no se despliega |
| «observación» superordinado de «advertencia» y «error de validación» | **Cumple.** Tratado como hiperonimia y no como ambigüedad, en las tres capas |
| El comentario del administrador **no** es una observación | **Cumple en las tres capas.** `Glosario-Funcional.md:109`, `Glosario-UX.md:101`, y la tabla de tres nociones de `DX-Error-Messages.md` §1.2, cuyo título es «Una condición de error no es una observación, y el comentario tampoco» |

**Polisemias evaluadas y DESCARTADAS por contextos disjuntos.** Ninguna se reporta como defecto; se enumeran para que la ronda siguiente no las vuelva a levantar.

1. **«Puerto» (contrato) contra puerto de red.** Descartada. No hay una sola ocurrencia del sentido de red en los diecisiete documentos. Ya declarada en `Glosario-Funcional.md` §3.6 y `Glosario-UX.md` §3.3.
2. **«Observación» superordinado contra sus dos especies.** Descartada: es hiperonimia, no polisemia. Reportar la relación género-especie como ambigüedad sería tratar como defecto lo que no lo es.
3. **«Comentario» del administrador contra comentarios dentro del texto del alumno.** Descartada: la sintaxis del dato de entrada no aparece en ningún artefacto de esta fase.
4. **«Rol»: rol de intervención contra papel de la cuenta.** Descartada, y no por contextos disjuntos sino por forma calificada aplicada al cien por ciento. La única ocurrencia desnuda es el encabezado «| Actor | Tipo | Rol |» que `Rules-Especificacion-Funcional.md` §4.2 impone, y `Glosario-Funcional.md` §3.4 lo admite explícitamente.
5. **«Trabajo» contra «unidad de trabajo» contra «flujo de trabajo».** Descartada: las formas compuestas se usan completas en todas las ocurrencias, y el referente «esfuerzo de construcción» se dice «etapa» o «tarea».
6. **«Categoría» (de error) contra «categoría» (02, 03 del framework).** **Descartada.** Se examinó por subsección, que es el contexto de lectura de `Vocabulario-Rules.md` §9.2: en §2.1, §2.2 y §6.3 de `DX-Error-Messages.md` el único referente presente es el de error; en §6.1, §6.2 y §2.4 el único presente es el del framework. **Los contextos no colisionan dentro de ninguna sección que se despache por separado, y calificar todas las ocurrencias sería el falso positivo que §9.1 tipifica.**
7. **«Error» dentro del sintagma «catálogo de errores».** **Descartada.** Las dos ocurrencias (`DX-Developer-Experience.md:181`, `03/README.md:106`) nombran el artefacto `DX-Error-Messages.md`, no un referente de la taxonomía, y el encabezado «Mensajes de error y diagnóstico» de `DX-Developer-Experience.md` §5 lo impone `Rules-UX-UI-DX.md` §4.2.3. No hay colisión que resolver.
8. **«Doble» (implementación de prueba) contra «doble» numeral.** Descartada: no hay uso numeral concurrente.
9. **«Motivo» (valor de enumeración cerrada) contra «motivo» de causa corriente.** Descartada: el uso corriente vive en las tablas de omisiones de los README y no comparte sección con el catálogo.
10. **«Alcance de consulta» contra «alcance del administrador» contra «alcance del producto».** Descartada: las tres formas van calificadas en toda ocurrencia y cada una tiene entrada o puntero.
11. **«Migración» (`Vocabulario-Rules.md` §9.6).** Descartada: cero ocurrencias en los artefactos auditados.
12. **«Repositorio» en los artefactos de 02.** **Descartada la mayor parte.** «Repositorio de código» **no aparece en ningún artefacto de 02 fuera de la propia fila de glosario que declara la regla**, de modo que las ~12 formas desnudas de los CU («el repositorio sigue con 1 cuenta», «el repositorio queda exactamente como estaba») se leen sin ambigüedad dentro de su sección: los contextos son disjuntos y **no son hallazgo**. Lo que sí queda como hallazgo menor es H-11, acotado a la única sección donde los dos referentes conviven.

---

## 7. Hallazgos

### H-01 · P0 · La cuenta del administrador se constituye «habilitada», que el dominio rechaza

**Archivo:** `SDD/Docs/Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Una-Cuenta.md`
**Secciones:** §5 FA-01 (línea 68), §7 (línea 85), §8 CA-03 (línea 94), y §6 por omisión

**Evidencia.** CU-01 FA-01, línea 68:

> «El caso de uso consulta primero al puerto de repositorio si ya existe una cuenta con papel `Administrador`. Si no existe, el alta procede y **la cuenta se constituye habilitada**, porque es quien habilita a los demás (RN-01, INV-05)»

Reforzado en §7, línea 85 —«estado `Pendiente` —**o habilitada, en el caso de FA-01**—»— y en CA-03, línea 94: «El caso de uso **constituye la cuenta habilitada** y devuelve que procede».

El dominio lo rechaza con un código propio. `GeometriaFactory-Domain/.../CU-01-Registrar-El-Alta-De-Un-Alumno.md`, paso 6 del flujo principal: «El dominio fija el estado de cuenta en `Pendiente`». Su §6: «`ESTADO_INICIAL_NO_NEGOCIABLE` | Se solicita constituir el alumno con la cuenta en un estado distinto de `Pendiente` | Rechaza la constitución. **El estado inicial es siempre `Pendiente`**». Su CA-05 evalúa exactamente este pedido: «una solicitud de constituirlo con la cuenta en estado `Habilitado` → El dominio rechaza con el código `ESTADO_INICIAL_NO_NEGOCIABLE`». Y su FA-01 —que es precisamente el alta con papel `Administrador`— retorna a «**Paso 5** del flujo principal, con papel `Administrador`», es decir atraviesa el paso 6 y queda `Pendiente`.

`Definicion-Modelo-De-Dominio.md` cierra la puerta dos veces: «`Pendiente` | El alumno que se registra | **Estado inicial, no negociable**» y «`Pendiente` → `Habilitado` | El administrador | Acto explícito. **No hay habilitación automática**».

Las dos fuentes que FA-01 invoca como fundamento no lo sostienen: RN-01 §1 dice sólo «Existe **exactamente un** administrador, y su alta sólo es posible mientras no exista ninguno», y INV-05 lo mismo. **Ninguna de las dos dice que su cuenta nazca habilitada.**

**Por qué es P0.** Concurren tres defectos: la orquestación es infiel a lo que el dominio especificó; se redacta en esta capa un invariante de estado inicial que el dominio no tiene y que además contradice, atribuyéndolo a una regla que no lo dice; y CA-03 es un criterio de aceptación que ninguna implementación fiel al dominio puede hacer pasar. Rompe la cadena de trazabilidad D6 hacia `GeometriaFactory-Domain` y viola D9 al afirmar un comportamiento del dominio contradicho por el propio dominio. Se propaga además a `DX-Error-Messages.md` §3.1, cuya fila de `ADMINISTRADOR_YA_CONFIGURADO` remite a «El alta inicial del administrador es el flujo alternativo FA-01».

**Recomendación.** Decidir el circuito y escribirlo una sola vez. Si el administrador debe quedar operativo desde el alta, el camino compatible con el dominio es constituirlo `Pendiente` y encadenar la habilitación —y entonces CU-01 debe declararlo como paso explícito y CU-02 debe admitir esa transición sin facultad previa, lo que a su vez es una decisión que hay que declarar—; si no, corregir FA-01, §7 y CA-03 para que el administrador nazca `Pendiente` como cualquier cuenta. En cualquiera de los dos casos, agregar `ESTADO_INICIAL_NO_NEGOCIABLE` a la §6 de CU-01 —lo que llevará el catálogo de 03 de 27 a 28 condiciones, con su recuento y su verificación mecánica actualizados— y quitar de FA-01 la atribución a RN-01 e INV-05.

---

### H-02 · P1 · `ENVIO_FUERA_DE_BORRADOR` dice propagar un motivo que el dominio no devuelve para dos de sus tres causas

**Archivo:** `.../Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md`
**Sección:** §6, línea 80

**Evidencia.** CU-05 §6:

> «`ENVIO_FUERA_DE_BORRADOR` | Se envía un trabajo en estado `Pendiente`, **`Finalizado` o `Rechazado`** | **Propaga el rechazo del dominio** y conserva el estado actual»

El dominio separa los dos casos. `GeometriaFactory-Domain/.../CU-08-Gobernar-El-Estado-Del-Trabajo.md` §6:

> «`ENVIO_FUERA_DE_BORRADOR` | Se solicita enviar un trabajo que no está en `Borrador` | Rechaza la operación y conserva el estado actual
> `TRANSICION_DESDE_ESTADO_TERMINAL` | Se solicita **cualquier** transición sobre un trabajo en estado `Finalizado` o `Rechazado` | ... (INV-07, RN-10)»

y su CA-04 fija el resultado sin ambigüedad: trabajo en estado `Finalizado` + «solicita enviarlo de nuevo» → «El dominio rechaza con el código **`TRANSICION_DESDE_ESTADO_TERMINAL`**».

CU-05 nunca menciona `TRANSICION_DESDE_ESTADO_TERMINAL`: afirma propagar el rechazo del dominio pero nombra un motivo distinto del que el dominio devuelve en dos de los tres estados que enumera. El defecto se propaga a `DX-Error-Messages.md` §3.5, que reproduce la misma causa.

**Recomendación.** Acotar la causa de `ENVIO_FUERA_DE_BORRADOR` al estado `Pendiente`, o declarar explícitamente que esta capa colapsa los dos motivos del dominio en uno y por qué. Si se agrega `TRANSICION_DESDE_ESTADO_TERMINAL` a la §6 de CU-05, el catálogo de 03 gana una reaparición y hay que actualizar §6.1, §6.2 y §6.3 en consecuencia (la condición ya existe, declarada por CU-08).

---

### H-03 · P1 · La orquestación de Dom CU-06 y CU-07 no aporta un dato de entrada que el dominio exige

**Archivo:** `.../Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md`
**Secciones:** §4 pasos 3 y 4, §6 (`OBSERVACION_MAL_FORMADA`)

**Evidencia.** `GeometriaFactory-Domain/.../CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md` §3, precondición:

> «El resultado de la interpretación llega ... y **declara cuántas figuras trae ese conjunto raíz**, incluidas las que no se pudieron reconstruir.»

y su paso 1: «La capa de aplicación entrega al trabajo el conjunto de piezas interpretadas **y la cantidad de figuras del conjunto raíz**». `Domain/.../CU-07-Registrar-Las-Observaciones-Del-Trabajo.md` §3 la convierte en precondición propia: «**El trabajo conoce la cantidad de figuras de su conjunto raíz** ... Es el rango contra el que se valida la posición de cada observación». `Definicion-Modelo-De-Dominio.md` §2.2 la declara atributo de la entidad Trabajo: «Cantidad de figuras del conjunto raíz | ... Es el rango de posiciones válidas del trabajo. **Sin ella, una observación sobre una figura no reconstruida no tendría contra qué validarse (RN-09)**».

App CU-05 §4 nunca la menciona: el paso 3 dice sólo «devuelve las piezas reconstruidas con su posición y sus valores declarado y derivado, y el conjunto de observaciones», y el paso 4 «incorpora al trabajo el conjunto de piezas, con su identidad posicional». **CU-05 es el único caso de uso que orquesta Dom CU-06 y Dom CU-07, y por lo tanto el único que puede aportar ese dato.**

La consecuencia es interna al propio documento: su §6 declara `OBSERVACION_MAL_FORMADA` con la causa «observación sobre una posición inexistente», comprobación que sin ese rango no tiene contra qué evaluarse, y su FA-03 depende del mecanismo de posición reservada que ese rango sostiene.

**Recomendación.** Agregar el dato al paso 3 —lo que el puerto de validación de figuras devuelve— y al paso 4 —lo que el caso de uso entrega al dominio—, y reflejarlo en la fila del puerto de validación de `Especificacion-Funcional.md` §3 y en la de `Guia-Onboarding-Developer.md` §7.1.

---

### H-04 · P2 · El índice maestro se contradice sobre las once reglas

**Archivo:** `.../02-Especificacion-Funcional/Especificacion-Funcional.md`
**Sección:** §6, línea 109

**Evidencia.**

> «**Nueve de las once se ejercen acá; las dos restantes se ejercen enteras en otras capas y su fila lo dice.**»

Las once filas de la tabla que sigue (líneas 113 a 123) asignan **todas** al menos un caso de uso de esta capa: RN-01 a CU-01, CU-02, CU-03, CU-07 y CU-08; ... ; RN-11 a CU-07, CU-08 y CU-09. **Ninguna fila dice que la regla se ejerza entera en otra capa.** La afirmación de la línea 109 no tiene referente.

Lo más parecido a la intención son las dos filas con nota de reparto —RN-09, cuya producción del mensaje ubicado «es del validador, detrás del puerto», y RN-05, que el dominio resuelve—, pero las dos declaran igualmente dónde se ejercen acá.

**Recomendación.** Reescribir la línea 109 para que describa la tabla, o marcar en las dos filas correspondientes el reparto que la línea anuncia.

---

### H-05 · P2 · El índice maestro atribuye a CU-02 un puerto que CU-02 no consume

**Archivos:** `Especificacion-Funcional.md` §3 línea 67; propagado a `03-UX-UI-DX/Guia-Onboarding-Developer.md` §7.1 línea 217
**Sección:** tabla de puertos

**Evidencia.** `Especificacion-Funcional.md:67`:

> «| Reloj del sistema | La fecha de alta y la de modificación, **para que sean verificables en prueba** | CU-01, **CU-02**, CU-03, CU-04, CU-05, CU-08 |»

`CU-02-Gobernar-Las-Cuentas-De-La-Comision.md` **no menciona el reloj ni ninguna fecha en ninguna de sus once secciones** —su §2 lista como actores el puerto de repositorio de cuentas, el de trabajos y el modelo de dominio, y su §9 declara «Puertos que consume | Repositorio de cuentas, repositorio de trabajos»—, aunque las cuatro operaciones que gobierna modifican el estado de la cuenta y CU-03 sí toma la fecha de modificación del reloj para operaciones sobre la misma entidad.

Las otras tres filas de la tabla de puertos se verificaron contra las §9 de los nueve CU y son exactas.

**Recomendación.** Decidir si CU-02 debe registrar fecha de modificación. Si sí, agregar el puerto a su §2, §4 y §9; si no, quitar CU-02 de la fila del reloj en §3 y en `Guia-Onboarding-Developer.md` §7.1.

---

### H-06 · P2 · Fechas de reloj atribuidas a entidades que el modelo de dominio no declara

**Archivos:** `CU-04-Cargar-Y-Reeditar-Un-Trabajo-Propio.md` §4 paso 2, §7, §8 CA-01; `CU-05-...` §4 paso 6; `CU-03-...` §2 y §7; `Especificacion-Funcional.md` §7.3 US-10

**Evidencia.** CU-04 §4 paso 2: «El caso de uso **toma la fecha de alta del puerto de reloj**»; §7: «existe un trabajo ... con **fecha de alta del reloj**»; CA-01: «con dueño A, **fecha de alta 2026-04-02**». CU-05 paso 6: «toma la **fecha de modificación** del puerto de reloj». CU-03: «**fecha de modificación de la cuenta**».

El dominio dice lo contrario para el trabajo. `GeometriaFactory-Domain/.../CU-05-Crear-Y-Reeditar-Un-Trabajo.md` §10: «**La fecha del trabajo es un dato que declara el alumno y no una lectura del reloj del sistema**», y `Definicion-Modelo-De-Dominio.md` §2.2: «Fecha | Fecha que el alumno declara para el trabajo | Obligatoria; **es dato del alumno, no del reloj del sistema**». La entidad Trabajo del modelo **no declara «fecha de alta» ni «fecha de modificación»**, y la entidad Alumno declara sólo «Fecha de alta | ... La provee el consumidor: el dominio no lee el reloj», sin fecha de modificación.

La existencia de esas fechas está sostenida por el intake (§17.2.P.11 punto 3, «para que las fechas de alta y modificación sean verificables en prueba»), así que el defecto no es inventarlas: es que Application las atribuye a entidades cuyo modelo conceptual no las tiene, sin declarar que son metadatos de orquestación.

**Recomendación.** Declarar explícitamente —en `Especificacion-Funcional.md` §3 y en la §10 de CU-03, CU-04 y CU-05— que la fecha de alta y la de modificación son metadatos que esta capa aporta al materializar, distintos de la «Fecha» que el alumno declara y que el dominio modela, o bien elevar la discrepancia a `GeometriaFactory-Domain` para que su modelo las incorpore.

---

### H-07 · P2 · Asimetría entre la tabla de reglas del índice y las §9 de los casos de uso

**Archivos:** `Especificacion-Funcional.md` §6; `CU-08-Dar-Desenlace-A-Un-Trabajo.md` §9; `CU-02-Gobernar-Las-Cuentas-De-La-Comision.md` §9

**Evidencia.** `Especificacion-Funcional.md` §6 asigna RN-01 a «CU-01 (alta inicial y su negativa), CU-02, CU-03, CU-07, **CU-08** (verificación de facultad)», y `DX-Error-Messages.md` §6.3 mapea `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` a «RN-01, RN-10». Pero `CU-08` §9 declara sólo «RN-10, RN-11, RN-05»: **omite RN-01**, que es justamente la regla de la comprobación que su FA-01 y su §6 ejercen.

En sentido inverso, §6 asigna RN-04 a «CU-09 en sus dos alcances, y **CU-02 en el arrastre de la baja**», pero `CU-02` §9 declara «RN-01, RN-06, RN-07»: **omite RN-04**.

Los casos contrarios —CU-06 declarando RN-04, CU-02 declarando RN-06, CU-08 declarando RN-05— se examinaron y **no son hallazgo**: el índice declara «dónde se ejerce» y la §9 del CU declara «reglas aplicables», que son nociones distintas y admiten que la segunda sea más amplia. Lo que no admite es que una regla que el índice declara ejercida en un CU no figure entre las aplicables de ese CU.

**Recomendación.** Agregar RN-01 a la §9 de CU-08 y RN-04 a la de CU-02, con sus enlaces relativos.

---

### H-08 · P3 · El catálogo declara «una sola entrada» para las seis condiciones repetidas, y una tiene dos filas

**Archivo:** `03-UX-UI-DX/DX-Error-Messages.md`
**Secciones:** §3 preámbulo (línea 166) contra §3.1 y §3.4

**Evidencia.** §3, línea 166:

> «Seis condiciones se declaran en más de un caso de uso y llevan **una sola entrada**, en el caso de uso donde aparecen primero, con la nota de sus apariciones restantes.»

Cinco de las seis se tratan así, con nota en prosa. `DATO_OBLIGATORIO_AUSENTE` recibe en cambio **fila completa de tabla en §3.1 y otra en §3.4**, esta última con el reconocimiento «Entrada única en §3.1; ésta es su segunda declaración». El resultado es 28 filas de tabla para 27 condiciones.

El recuento no se ve afectado: §6.2 cuenta correctamente 3 entradas nuevas para CU-04 y la diferencia simétrica sigue vacía. El defecto es sólo la inexactitud del preámbulo.

**Recomendación.** Convertir la fila de §3.4 en nota de prosa, como las otras cinco, o matizar el preámbulo.

---

### H-09 · P3 · El control de cambios de `DX-Error-Messages.md` queda numerado §7

**Archivo:** `03-UX-UI-DX/DX-Error-Messages.md`
**Sección:** §6 y §7

**Evidencia.** `Rules-UX-UI-DX.md` §4.2.5 numera las seis secciones obligatorias con «Control de cambios» en la posición 6. El documento intercala §6 «Cobertura y trazabilidad» —que la §6 de la regla exige por otra vía, «Cada artefacto declara trazabilidad upstream y downstream»— y el control de cambios queda en §7.

Las seis obligatorias están presentes y en orden relativo, y el contenido intercalado es sustantivo y necesario, así que la severidad es baja. Se anota porque `Guia-Onboarding-Developer.md` resolvió el mismo problema con la convención opuesta y preferible —contenido propio **después** del control de cambios, como §7—, de modo que los dos documentos de la misma sección no siguen la misma regla.

**Recomendación.** Unificar la convención: mover «Cobertura y trazabilidad» a §7 y devolver «Control de cambios» a §6, como hace la guía de onboarding.

---

### H-10 · P3 · «Solución» a secas designando el agrupador de construcción

**Archivo:** `03-UX-UI-DX/DX-Error-Messages.md`
**Sección:** §1.3, línea 82

**Evidencia.**

> «no cruza ninguna frontera de proceso y sus contratos son referencias de proyecto de código dentro de la misma **solución** (`PRODUCT-INTAKE` §17.2.P.3)»

El referente es el agrupador de construcción, y `Vocabulario-Rules.md` §10 lo prohíbe: «No aparece «solución» a secas designando el agrupador de construcción». El resto del corpus usa la forma correcta —`Guia-Onboarding-Developer.md:70`, `DX-Developer-Experience.md:135` y `:157`, todas «solución de código»—, lo que confirma que es una ocurrencia suelta. **Es la única en los diecisiete documentos.**

**Recomendación.** Escribir «dentro de la misma solución de código».

---

### H-11 · P3 · «Repositorio» a secas en la única sección donde los dos referentes conviven

**Archivo:** `03-UX-UI-DX/Guia-Onboarding-Developer.md`
**Sección:** §4, líneas 164 y 169

**Evidencia.** `Glosario-Funcional.md:65` fija una regla absoluta —«**No se nombra «repositorio» a secas en esta categoría.** Se dice «repositorio de código»»— y `Guia-Onboarding-Developer.md:59` la repite. En §4 conviven las dos formas sin que la primera se califique:

> línea 164: «Abrir **el repositorio** en el entorno contenido del propio repositorio y repetir desde el paso 0» (referente: repositorio de código)
> línea 169: «se **entrega** al **puerto de repositorio** dentro de una única unidad de trabajo» (referente: puerto)

Es la única sección de los diecisiete documentos donde los dos referentes comparten contexto de lectura, que es el criterio de colisión de `Vocabulario-Rules.md` §9.2. **Las demás formas desnudas de «repositorio» —unas doce en los CU de 02 y unas diez en 03— se evaluaron y NO son hallazgo**, porque en 02 el referente «repositorio de código» no aparece fuera de la fila de glosario que declara la regla, y en 03 las secciones donde aparece el sentido de código no contienen el sentido de puerto: los contextos son disjuntos y calificarlas todas sería el falso positivo que §9.1 tipifica.

**Recomendación.** Calificar la ocurrencia de la línea 164 como «repositorio de código», que es la forma que el propio glosario fija. No tocar el resto.

---

### H-12 · P3 · CU-01 §3 habla de «los tres puertos» y consume dos

**Archivo:** `.../Casos-De-Uso/CU-01-Registrar-El-Alta-De-Una-Cuenta.md`
**Sección:** §3, última precondición

**Evidencia.** «- **Los tres puertos** están provistos por la composición de raíz.» La §2 del mismo caso de uso lista como puertos el de repositorio de cuentas y el de reloj del sistema —el tercer actor de sistema es el modelo de dominio, que no es un puerto—, y su §9 declara «Puertos que consume | Repositorio de cuentas, reloj del sistema».

**Recomendación.** Escribir «los dos puertos», o «los puertos que este caso de uso consume».

---

### H-13 · P3 · La equivalencia entre el motivo de facultad de esta capa y los dos del dominio no está declarada

**Archivos:** `Especificacion-Funcional.md` §4; `CU-02`, `CU-07` y `CU-08` §6

**Evidencia.** Application emite un único `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`. El dominio declara dos códigos distintos para la misma negativa: `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` (Dom CU-10 §6, con su CA-03 sobre el mismo escenario que App CU-08 CA-03) y `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` (Dom CU-11 §6).

**No es un defecto de comportamiento:** las tres §6 de Application declaran que el caso de uso «no recupera ni modifica nada» y «no consulta nada», es decir corta antes de llegar al dominio, de modo que los códigos del dominio nunca llegan a producirse. Lo que falta es dejar escrita la equivalencia, para que quien lea las dos capas no crea que hay tres negativas de facultad donde hay una.

**Recomendación.** Una línea en `Especificacion-Funcional.md` §4 que declare que esta capa corta antes y colapsa los dos motivos del dominio en uno.

---

### H-14 · P3 · Rechazos del dominio sin camino de propagación declarado

**Archivos:** `CU-01`, `CU-02`, `CU-03`, `CU-04` y `CU-05` §6

**Evidencia.** Contraste mecánico de los códigos declarados en las §6 de los once CU de dominio contra los declarados en las §6 de los nueve de Application. Quedan sin correspondencia y sin mención:

| Código del dominio | Origen | Situación en Application |
| --- | --- | --- |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | Dom CU-01 | No mencionado. Riesgo bajo: App CU-01 paso 4 declara la verificación |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Dom CU-02 | Aludido en prosa en CU-02 §10, sin nombrarlo |
| `CREDENCIAL_YA_FIJADA`, `VALOR_DERIVADO_VACIO` | Dom CU-03 | No mencionados |
| `TEXTO_ORIGINAL_ALTERADO`, `REEDICION_FUERA_DE_BORRADOR` | Dom CU-05 | No mencionados. El segundo tiene equivalente funcional en `OPERACION_FUERA_DE_BORRADOR`, que viene de Dom CU-09 |
| `ENVIO_SIN_INTERPRETACION`, `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | Dom CU-08 | No mencionados |
| `TIPO_DE_PIEZA_DESCONOCIDO`, `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`, `POSICION_DE_PIEZA_INVALIDA`, `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` | Dom CU-06 | No mencionados. **App CU-05 declara una condición agregada para el conjunto de observaciones (`OBSERVACION_MAL_FORMADA`) pero ninguna para el conjunto de piezas** |
| `OPERACION_DESCONOCIDA` | Dom CU-09 y CU-11 | No mencionado; parcialmente cubierto por `PAPEL_NO_RECONOCIDO` |

**No son hallazgo** los cuatro códigos de observación de Dom CU-07 (`ESPECIE_DE_OBSERVACION_DESCONOCIDA`, `ERROR_SIN_UBICACION`, `ADVERTENCIA_SIN_LOS_DOS_VALORES`, `OBSERVACION_SOBRE_PIEZA_INEXISTENTE`) ni los dos de papel de Dom CU-10 y CU-11: los primeros están **explícitamente agregados** en la causa de `OBSERVACION_MAL_FORMADA`, y los segundos son H-13.

El más sustantivo es el hueco de Dom CU-06: la reconstrucción de piezas puede fallar por cuatro causas y ninguna tiene camino de vuelta declarado en App CU-05, que es su único orquestador.

**Recomendación.** Declarar en las §6 correspondientes qué hace el caso de uso con cada rechazo del dominio, o —cuando la agregación sea deliberada, como en el conjunto de observaciones— declararla explícitamente. En particular, agregar a App CU-05 una condición agregada para el conjunto de piezas mal formado, simétrica a `OBSERVACION_MAL_FORMADA`. Cualquier condición nueva obliga a actualizar el recuento y la verificación mecánica de `DX-Error-Messages.md` §6.

---

### 7.1 Recuento por nivel

| Nivel | Cantidad | Hallazgos |
| --- | --- | --- |
| **P0** | 1 | H-01 |
| **P1** | 2 | H-02, H-03 |
| **P2** | 4 | H-04, H-05, H-06, H-07 |
| **P3** | 7 | H-08, H-09, H-10, H-11, H-12, H-13, H-14 |
| **Total** | **14** | |

---

## 8. Veredicto y condiciones para promover

### Veredicto: **RECHAZADO**

Existe un hallazgo P0 (H-01). Por `Master-Prompt.md` §10, cualquier P0 produce RECHAZADO y obliga a corrección y re-auditoría en una ronda nueva.

La calificación no debe leerse como un juicio sobre el conjunto. El cuerpo documental es de calidad alta y verificada: el catálogo de veintisiete condiciones cuadra con diferencia simétrica vacía cuando se lo recuenta de forma independiente; las dieciséis citas de motivo de las §5 no introducen ninguna condición nueva; las tres negativas de autorización están tratadas con precisión, con el error simétrico incluido y con criterios de aceptación mecánicamente evaluables que las anclan; la frontera entre autorizar y autenticar está declarada y ningún artefacto la cruza; las once reglas se referencian y no se redactan, con enlaces que resuelven; los dos glosarios anticipan los falsos positivos de polisemia en lugar de inducirlos; y las omisiones —las `RN-XX`, el modelo conceptual, el concepto central, los ocho artefactos de la variante UX/UI y de maqueta— están todas declaradas con su motivo y su flag, sin darse por cumplidas.

Los defectos son localizados y de corrección acotada. H-01, H-02 y H-03 comparten causa: la fidelidad de la orquestación se verificó por caso de uso y no por sección de error del dominio.

### Condiciones para promover a la ronda 2

**Bloqueantes:**

1. **H-01 resuelto.** Decidir y escribir una sola vez cómo se constituye la cuenta del administrador, con el resultado compatible con Dom CU-01 paso 6, su §6 y su CA-05. Corregir FA-01, §7 y CA-03 de App CU-01, quitar la atribución a RN-01 e INV-05, y propagar a `DX-Error-Messages.md` §3.1.

**Antes del cierre de fase:**

2. **H-02 y H-03 resueltos** en App CU-05, con la actualización correspondiente del catálogo de 03.
3. **H-04, H-05, H-06 y H-07 resueltos**, que son cuatro correcciones de coherencia sin impacto en el catálogo salvo la tabla de puertos.
4. **H-08 a H-14 evaluados** y corregidos o aceptados por escrito.

**Regla de versionado aplicable.** Los diecisiete documentos están en estado `Propuesto`, de modo que por `Master-Prompt.md` §5 las correcciones derivadas de este audit **se absorben dentro de la versión 1.0 en curso, sin subir versión**. Cada corrección absorbida deja su fila en el control de cambios del documento citando el hallazgo de este informe que la origina.

**Efecto sobre el catálogo.** Si H-01, H-02 y H-14 agregan condiciones nuevas a alguna §6, `DX-Error-Messages.md` §3, §6.1, §6.2 y §6.3 y las cifras de `DX-Developer-Experience.md` §6 y de `03/README.md` deben actualizarse en la misma operación, y la ronda 2 debe recontar la diferencia simétrica.

**No se reportan como defecto**, y se dejan asentadas para que la ronda siguiente no las levante: las doce polisemias de contextos disjuntos enumeradas en §6.2 de este informe; la ausencia de los artefactos de la variante UX/UI, de los de maqueta y de la categoría 04; la ausencia de `_legacy/`; la ausencia de las `RN-XX`, del modelo conceptual y del documento de concepto central; el punto abierto del puerto de repositorio de cuentas; y la divergencia de motivo entre App CU-08 CA-05 y Dom CU-10 CA-05, que es consecuencia correcta del orden de orquestación.

---

## Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Ronda 1 de auditoría de la Fase B de `GeometriaFactory-Application`, categorías 02 y 03. Catorce hallazgos —1 P0, 2 P1, 4 P2 y 7 P3— y veredicto RECHAZADO. Incluye el recuento independiente del catálogo de veintisiete condiciones con diferencia simétrica vacía, la verificación de las dieciséis citas de motivo de las §5, la verificación de la orquestación de los once casos de uso de dominio sin huérfanos, y las doce polisemias evaluadas y descartadas por contextos disjuntos. | Auditor independiente (Arquitecto de Soluciones + QA Senior) |
