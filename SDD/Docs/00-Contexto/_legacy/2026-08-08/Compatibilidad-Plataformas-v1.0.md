> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Compatibilidad-Plataformas.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`Compatibilidad-Plataformas.md`](../../Compatibilidad-Plataformas.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Compatibilidad de Plataformas

**Producto:** Fábrica de Geometría
**Documento:** Compatibilidad-Plataformas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Product Manager Senior (AG-00), actuando también como Analista de Negocio Senior (AG-01) por `Rules-Contexto.md` §1.3
**Trazabilidad upstream:** PRODUCT-INTAKE §10 (restricciones del cliente: red, servidor propio, hosting sin estado, host de desarrollo sin SDK), §13 (los siete proyectos de código), §15 (puertas técnicas y dónde se miden), §17 P.9 de los siete bloques (compatibilidad y plataformas target), §17.5 P.8 y §17.6 P.8 (ambientes y canales de entrega), §22 (incógnitas marcadas para verificar)
**Trazabilidad downstream:** 09-Devops, 05-Arquitectura-Tecnica, 08-Calidad-Y-Pruebas, 03-UX-UI-DX

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
  - [1.1 Por qué este documento existe pese a no ser obligatorio para el tipo del principal](#11-por-qué-este-documento-existe-pese-a-no-ser-obligatorio-para-el-tipo-del-principal)
  - [1.2 Las tres matrices y cuál rige ante conflicto](#12-las-tres-matrices-y-cuál-rige-ante-conflicto)
- [2. Matriz de compatibilidad](#2-matriz-de-compatibilidad)
  - [2.1 Proyectos de código y sus plataformas target](#21-proyectos-de-código-y-sus-plataformas-target)
  - [2.2 Plataforma del navegador](#22-plataforma-del-navegador)
  - [2.3 Plataformas de construcción](#23-plataformas-de-construcción)
- [3. Restricciones de plataforma justificadas](#3-restricciones-de-plataforma-justificadas)
- [4. Alternativas para plataformas no soportadas](#4-alternativas-para-plataformas-no-soportadas)
- [5. Estado de implementación por plataforma](#5-estado-de-implementación-por-plataforma)
- [6. Trazabilidad downstream](#6-trazabilidad-downstream)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Resumen ejecutivo

Fábrica de Geometría se ejecuta sobre **tres matrices de plataforma que no coinciden entre sí**: los proyectos de código de servidor apuntan a `net10.0` sobre Linux, la pieza pública corre sobre la versión de plataforma que soporte el hosting gratuito —dato que el intake deja explícitamente marcado para verificar— y el navegador debe proveer WebGL y un transporte de sesión interactiva. Este documento las declara juntas y fija cuál rige cuando divergen.

### 1.1 Por qué este documento existe pese a no ser obligatorio para el tipo del principal

`Rules-Contexto.md` §2.1 marca `Compatibilidad-Plataformas.md` como recomendado, y no obligatorio, para los tipos D8 presentes en este producto. Se incluye por **decisión del humano al aprobar el plan de fase**, con este motivo declarado: el producto tiene tres matrices de plataforma que no coinciden entre sí, y una de ellas —la versión de plataforma del hosting— es una incógnita abierta que condiciona el modelo entero de la pieza pública. Dejar esa divergencia sin documento propio la volvería invisible para la categoría 09, que es la que tiene que materializar la matriz de sistema operativo, entorno de ejecución e integración continua.

### 1.2 Las tres matrices y cuál rige ante conflicto

| Matriz | Alcance | Quién la fija |
|---|---|---|
| Servidor de datos | `net10.0` sobre Linux, en contenedor, en el servidor propio | Decisión técnica declarada en el intake |
| Pieza pública | `net10.0` como objetivo, **sujeto a la versión que soporte el hosting gratuito** (marcado para verificar, puerta técnica PT-01.a) | Se resuelve midiendo, no decidiendo |
| Navegador | Requisito declarado **por capacidad y no por número de versión**: WebGL, y WebSockets o repliegue a long polling | El intake declara que las fuentes no fijan versiones mínimas |

**Regla de precedencia ante conflicto, declarada aguas arriba y no decidida acá** (PRODUCT-INTAKE §17.6 P.9): si el hosting no soporta la versión objetivo, se **baja la versión objetivo de la pieza pública, no la del servicio de datos**. Son dos artefactos independientes que no comparten proceso ni servidor, y sólo comparten el ensamblado de contratos, que se compila para el mínimo común. No hay ambigüedad de precedencia: la regla está escrita.

## 2. Matriz de compatibilidad

### 2.1 Proyectos de código y sus plataformas target

Las siete filas cubren los siete proyectos de código del manifiesto, tomadas de PRODUCT-INTAKE §17 P.9 de cada bloque.

| Componente | Entorno de ejecución | Sistema operativo | Navegador | Notas |
|---|---|---|---|---|
| GeometriaFactory-Domain | `net10.0`, sin sufijo de plataforma | Linux | No aplica | Sin dependencias de plataforma. **No** apunta a `net10.0-windows`: eso pertenece a la Actividad 1, que es el emisor del dato y no forma parte del producto |
| GeometriaFactory-Application | `net10.0` | Linux | No aplica | Sin dependencias de plataforma |
| GeometriaFactory-Infrastructure | `net10.0` | Linux, en el entorno de desarrollo y en el servidor propio | No aplica | El motor de base embebido queda anclado en la versión que provee el proveedor de acceso a datos, fijada en la etapa `a` |
| GeometriaFactory-Contracts | `net10.0` | Linux | No aplica | Se carga en los **dos** procesos: el del hosting y el del servidor propio. Es la fila que obliga a que las dos matrices de servidor sean compatibles entre sí |
| GeometriaFactory-Api | `net10.0` | Linux exclusivamente: entorno de desarrollo, imagen de producción y servidor propio | No aplica | La imagen final lleva sólo el entorno de ejecución, sin SDK ni depurador, y no tiene linaje con la imagen del entorno de desarrollo. Un único puerto publicado hacia el enrutador |
| GeometriaFactory-Web | `net10.0` como objetivo, **marcado para verificar** contra el hosting gratuito | El del hosting público gratuito, con servidor de información, HTTPS y dominio | Requerido: WebGL y WebSockets o long polling | Es la única fila con la versión sujeta a verificación. Puerta técnica PT-01.a |
| GeometriaFactory-Visor | Archivo de guion servido como recurso estático; en tiempo de ejecución **no hay entorno Node** | El del navegador | Requerido: WebGL | Node.js en versión de soporte extendido anclada, **sólo en tiempo de construcción** |

### 2.2 Plataforma del navegador

| Capacidad requerida | Umbral | Qué pasa si falta |
|---|---|---|
| WebGL | Presente | Sin WebGL no hay visualización tridimensional: la combinación se considera **no soportada** |
| Transporte de sesión interactiva | WebSockets, o repliegue a long polling | El repliegue es aceptable y se documenta la latencia percibida; **no es motivo de rediseño**. La ausencia de los dos deja el producto sin sesión interactiva |
| HTTPS | Presente | Es el que provee el hosting público; el navegador nunca alcanza al servicio de datos |

**No se fija versión mínima de navegador y es deliberado:** el intake declara que ninguna fuente la fija, y expresa el requisito por capacidad. Enumerar versiones acá sería originar un compromiso de soporte que el Product Owner no tomó.

### 2.3 Plataformas de construcción

| Plataforma | Uso | Restricción declarada |
|---|---|---|
| Entorno de desarrollo contenido | Todo el ciclo de construcción, ejecución y prueba | El host de desarrollo **no tiene ni va a tener** el SDK instalado. Ningún guion puede asumir herramientas del SDK en el host |
| Node.js en versión de soporte extendido, anclada | Construcción del paquete de la visualización | Sólo en tiempo de construcción, y siempre dentro del entorno de desarrollo |
| Integración continua del proveedor de repositorio | Publicación de la pieza pública por transferencia de archivos | Restringida a los cambios de la pieza pública y de la visualización. Termina comprobando que la dirección pública responde, no en la subida |

## 3. Restricciones de plataforma justificadas

| Id | Restricción | Justificación declarada |
|---|---|---|
| CP-01 | Linux exclusivamente en los seis proyectos de código de servidor | Entorno de desarrollo, imagen de producción y servidor propio son Linux. Toda combinación no listada se considera no soportada |
| CP-02 | Ningún proyecto de código apunta a un objetivo específico de Windows | El único artefacto que lo usa es la Actividad 1, que emite el dato y **no forma parte de este producto** |
| CP-03 | La versión de plataforma de la pieza pública está sujeta a lo que soporte el hosting | El hosting es gratuito y su capacidad no es contrastable sin medirla. Es la puerta técnica PT-01.a, y se mide en la etapa `a` antes que cualquier otra cosa |
| CP-04 | Requisito de navegador expresado por capacidad y no por versión | Ninguna fuente fija versiones mínimas; sin WebGL no hay visualización |
| CP-05 | El motor gráfico viaja dentro del paquete, no desde una red de distribución externa | La pieza pública debe funcionar sin acceso a redes de distribución de contenido externas. Es la puerta técnica PT-03 |
| CP-06 | En tiempo de ejecución no hay entorno Node | La visualización se sirve como archivo estático. Node existe sólo en tiempo de construcción |
| CP-07 | El host de desarrollo no tiene el SDK y no lo va a tener | Decisión declarada del propietario del entorno. Obliga a que todo el ciclo ocurra dentro del entorno contenido |
| CP-08 | La versión de todo paquete se ancla explícitamente y se registra en la etapa que lo introduce | Regla de anclaje de versiones declarada para los siete proyectos de código: un cambio de versión mayor es una decisión que se documenta, nunca el efecto colateral de una actualización |
| CP-09 | El servidor propio no tiene dirección fija y se admite apuntar a la dirección directa | Decisión declarada del propietario del servidor, con nombre dinámico como recomendación. Cada cambio de dirección obliga a volver a publicar la pieza pública |

## 4. Alternativas para plataformas no soportadas

| Plataforma o escenario no soportado | Alternativa declarada | Origen |
|---|---|---|
| El hosting no soporta la versión de plataforma objetivo | Bajar la versión objetivo **de la pieza pública**, no la del servicio de datos | PT-01.a |
| El hosting no sostiene el transporte de sesión interactiva por WebSockets | Repliegue a long polling: **aceptable**, se documenta la latencia percibida. No es motivo de rediseño | PT-01.b |
| El hosting no sostiene ninguna sesión interactiva, o recicla el proceso | Es el peor escenario y **no tiene mitigación en el código**. Salidas documentadas: cambiar el modelo de la pieza pública a ejecución en el navegador con reenvío de peticiones, o servir la pieza pública desde el servidor propio, que reabre el bloqueo desde la facultad | PT-01.c y las salidas documentadas del intake |
| La pieza pública no alcanza al servicio de datos | Publicar el servicio en un puerto convencional. El reenvío de peticiones **no ayuda** en este caso | PT-01.d |
| Navegador sin WebGL | **No soportado.** No hay alternativa: sin WebGL no hay visualización, que es una de las capacidades comprometidas | PRODUCT-INTAKE §17.6 P.9 y §17.7 P.9 |
| Red de la facultad que bloquea el acceso directo al servidor propio | Es la premisa que ordena la partición del producto, no un escenario a mitigar. La salida alternativa —canal saliente con dominio propio— está **declarada y deliberadamente no adoptada**, porque debilitaría la premisa | Exclusión X-10 del alcance |
| Host de desarrollo sin SDK | Entorno de desarrollo contenido, obligatorio para todo el ciclo | CP-07 |

## 5. Estado de implementación por plataforma

Estado a la fecha de emisión: el producto no tiene código construido. La evidencia es `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §1.1, bloque de decisiones de reconciliación, que declara que «`SDD/Docs/` estaba vacía al arrancar, de modo que la reconciliación normativa de `Master-Prompt.md` §2.1 no se disparó». Todas las filas están **declaradas y sin verificar**, y cada una indica dónde se verifica.

| Plataforma | Componentes que la usan | Estado | Dónde se verifica |
|---|---|---|---|
| `net10.0` sobre Linux, servidor propio | Api, Infrastructure, Application, Domain, Contracts | Declarada, sin verificar | Etapa `a`: el producto compila y las dos piezas desplegables arrancan; PT-04 construye y arranca la imagen |
| Plataforma del hosting público | Web, Contracts | Declarada, **con la versión marcada para verificar** | Etapa `a`, PT-01.a. Es la primera medición del producto |
| Transporte de sesión interactiva del hosting | Web | Declarada, sin verificar | Etapa `a`, PT-01.b y PT-01.c |
| Salida del hosting hacia el servidor propio | Web hacia Api | Declarada, sin verificar | Etapa `a`, PT-01.d |
| Navegador con WebGL | Web, Visor | Declarada, sin verificar | Etapa `g`, PT-02 y PT-03 |
| Node.js de construcción | Visor | Declarada, sin verificar | Etapa `a`, al generar el paquete por primera vez |
| Red de la facultad hacia el hosting | Producto completo | Declarada, sin verificar | Etapa `h`, PT-05. El intake recomienda no relegarla |

## 6. Trazabilidad downstream

| Contenido | Destino | Qué consume |
|---|---|---|
| §2 Matriz de compatibilidad | 09-Devops | Matriz de sistema operativo, entorno de ejecución e integración continua; imágenes base y objetivos de construcción |
| §2.3 Plataformas de construcción | 09-Devops | Definición del entorno de desarrollo contenido y de los guiones de construcción |
| §3 Restricciones justificadas | 05-Arquitectura-Tecnica | Insumo de las decisiones de arquitectura sobre la partición del producto y sobre el aislamiento de la visualización |
| §4 Alternativas | 05-Arquitectura-Tecnica, 09-Devops | Salidas ya documentadas ante el resultado de cada puerta técnica |
| §5 Estado por plataforma | 08-Calidad-Y-Pruebas | Qué se mide, dónde y con qué umbral |
| §2.2 Plataforma del navegador | 03-UX-UI-DX | Capacidades que la experiencia de uso puede dar por presentes |

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-08 | Emisión inicial, incluida por decisión del humano al aprobar el plan de fase pese a no ser obligatoria para el tipo D8 del proyecto de código principal, con su motivo declarado en §1.1. Agrega las plataformas target de los siete proyectos de código, la matriz del navegador expresada por capacidad, las tres plataformas de construcción, nueve restricciones justificadas, siete alternativas para escenarios no soportados y el estado de verificación de cada plataforma con la puerta técnica que la mide. | Product Manager Senior (AG-00) |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit A-00-01-r1, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-05**: el párrafo introductorio de §5 cita `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §1.1, decisiones de reconciliación, como evidencia localizable de la única afirmación del documento sobre el estado del sistema. **H-01**: se califica la ocurrencia desnuda de «pieza» de la primera fila de §5, sobre la familia que declara `Vision-Producto.md` §9.2. | Product Manager Senior (AG-00) |
