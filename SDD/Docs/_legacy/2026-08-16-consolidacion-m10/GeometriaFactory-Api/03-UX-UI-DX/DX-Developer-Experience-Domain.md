# DX — Superficie pública del modelo de dominio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** DX-Developer-Experience.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` §1, §4.1, §4.2 (recorrido de adopción de INV-08), §4.3 (correspondencia entre reglas e invariantes), §5.1, §5.2 y §7; `02-Especificacion-Funcional/Especificacion-Funcional.md` §9; los **trece** casos de uso CU-02001 a CU-02013 (§6 de cada uno) y las **dieciséis** reglas RN-02001 a RN-02016 de `02-Especificacion-Funcional/`; `00-Contexto/Vision-Producto.md` §9 (glosario raíz) y §7; `00-Contexto/Alcance-Producto.md` §4.1, §4.4 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2 (NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1 (P.1, P.2, P.3, P.4, P.5, P.6, P.7, P.9, P.10, P.11, P.12), §4.1, §4.2, §16
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Rol de intervención developer](#1-rol-de-intervención-developer)
  - [1.1 Quién interviene acá](#11-quién-interviene-acá)
  - [1.2 Qué es la superficie pública de este proyecto de código](#12-qué-es-la-superficie-pública-de-este-proyecto-de-código)
  - [1.3 La frontera de autenticación](#13-la-frontera-de-autenticación)
- [2. Onboarding por tramos](#2-onboarding-por-tramos)
- [3. Quick-start](#3-quick-start)
  - [3.1 Pasos](#31-pasos)
  - [3.2 Verificación del quick-start](#32-verificación-del-quick-start)
- [4. Diátaxis](#4-diátaxis)
  - [4.1 Dónde vive cada modo](#41-dónde-vive-cada-modo)
  - [4.2 Cómo se enlazan](#42-cómo-se-enlazan)
- [5. Mensajes de error y diagnóstico](#5-mensajes-de-error-y-diagnóstico)
- [6. Métricas DX](#6-métricas-dx)
- [7. Feedback loop](#7-feedback-loop)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Rol de intervención developer

### 1.1 Quién interviene acá

No hay integradores externos. `GeometriaFactory-Domain` no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain); sus únicos consumidores son otros dos proyectos de código del mismo producto, que lo referencian por referencia de proyecto de código y no cruzan ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Domain).

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Mantenedor | La persona que sostiene el producto y que vuelve sobre este proyecto de código semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** (`equipo_n` = 1) | Dónde poner una regla nueva, por qué un rechazo existe, y qué se prueba sin nada |
| Integrador de capa | La misma persona, o el agente, escribiendo `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure` contra esta superficie | Qué contrato de uso invoca, qué tiene que haber resuelto **antes** de invocar, y qué código de condición recibe cuando no lo resolvió |
| Operador | **No aplica.** Este proyecto de código no atiende peticiones, no abre conexiones, no registra ni instrumenta (§17.1.P.10 · GeometriaFactory-Domain). No hay nada que operar | — |

Nivel de experiencia esperado: quien ya escribe código de aplicación y conoce el vocabulario del laboratorio, pero **no** necesariamente el estilo de modelo de dominio con invariantes explícitas. La documentación no supone ese estilo conocido: lo explica en §1.2 y lo apoya en `Definicion-Modelo-De-Dominio.md` §4.

Herramientas que ya conoce: el entorno de desarrollo contenido del propio repositorio y los scripts de `scripts/` (`PRODUCT-INTAKE` §16). No se supone ninguna otra.

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, porque es la razón por la que este proyecto de código existe y por la que no tiene dependencias:

> **La superficie pública de un modelo de dominio son sus guardas.** Lo que un consumidor invoca acá no es una API de servicio: es la construcción y la transición de entidades que **se niegan a entrar en un estado prohibido**.

Tres consecuencias operativas, que gobiernan todo lo demás:

1. **El resultado de una invocación no es un dato, es una entidad que ya verificó sus invariantes.** Si el dominio devolvió el alumno constituido, es porque el correo, el nombre y el apellido estaban presentes, la unicidad venía declarada como verificada, no se aportó credencial y el estado inicial es cuenta `Pendiente`, que es el del **auto-registro** (CU-02001 §4). El otro camino de alta, la configuración del administrador, tiene el suyo y es `Habilitado` (CU-02012). No hay que volver a comprobar nada de eso aguas abajo.
2. **El dominio no resuelve nada por su cuenta.** No consulta, no reintenta, no lee el reloj, no interpreta el texto del alumno, no deriva contraseñas y no emite acceso. Cuando una condición se afirma sobre un conjunto de entidades —la unicidad del correo, INV-01— el dominio **la exige declarada** y quien la ejerce es la capa de aplicación con su puerto de repositorio (`Definicion-Modelo-De-Dominio.md` §4.1 y §7).
3. **Un rechazo es una terminación controlada, no una avería.** El dominio no construye la entidad, o la deja exactamente como estaba, y devuelve la causa; no queda estado intermedio porque no guarda nada. El catálogo completo de esas causas es [`DX-Error-Messages.md`](DX-Error-Messages.md).

Nueve invariantes vigentes y **dieciséis** reglas de negocio son las dos caras de esto, y la relación entre ambos es lo que le dice al mantenedor dónde poner una regla nueva. Está desarrollada en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7, con su procedimiento de decisión en §7.3, sobre la correspondencia que declara `Definicion-Modelo-De-Dominio.md` §4.3.

### 1.3 La frontera de autenticación

Es sutil y conviene dejarla imposible de confundir, porque un error acá se paga en dos capas a la vez.

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| El estado de cuenta `Pendiente`, `Habilitado`, `Bloqueado` y sus transiciones admitidas | Sí (CU-02002) | — |
| La **condición** de que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso (INV-06, RN-02006) | Sí (CU-02004) | — |
| La respuesta de admisibilidad con sus **tres** motivos: cuenta `Pendiente`, cuenta bloqueada y **cambio de contraseña pendiente**. El cuarto, credencial no establecida, quedó retirado por **RN-02016** | Sí (CU-02004 §6) | — |
| La exigencia de que la credencial derivada se fije **en el acto de habilitación**, con la provisoria que el sistema produce, y de que el reemplazo declare verificada la vigente | Sí (CU-02002, CU-02003) | — |
| La exigencia de que la cuenta del administrador nazca `Habilitado` y **con su credencial ya aportada**, porque es la que habilita a las demás y ninguna anterior podría habilitarla a ella | Sí (CU-02012) | — |
| La exigencia de que **ninguna de las cuatro operaciones** de ciclo de vida —habilitar, bloquear, rehabilitar y dar de baja— proceda sobre la cuenta del administrador: las cuatro están declaradas sobre cuentas de alumno (F-03) | Sí (CU-02002) | — |
| Comparar una contraseña, derivarla, emitir o validar un acceso, sostener una sesión | **No** | `GeometriaFactory-Infrastructure` (§17.1.P.5 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Infrastructure) y `GeometriaFactory-Api` |
| Autorizar por papel el acceso a un endpoint | **No.** La evaluación de admisibilidad se resuelve por estado y por credencial, nunca por papel (CU-02004 FA-02) | La capa que expone los endpoints |

Enunciado en una línea, que es como conviene recordarlo: **el dominio no implementa autenticación, pero sí modela las reglas que la condicionan.** La contraseña llega ya derivada y el dominio no la conoce nunca en claro (§17.1.P.5 · GeometriaFactory-Domain).

Y una advertencia que la frontera hace fácil de subestimar: estas reglas **no protegen sólo el acceso**. Si la cuenta del administrador queda bloqueada o dada de baja, nadie aprueba ni rechaza, todo trabajo enviado se queda en estado `Pendiente` para siempre y **el circuito de revisión entero se detiene** (RN-02010). Por eso las guardas de CU-02002 y de CU-02012 sobre esa cuenta son de dominio y no de la capa que emite el acceso.

Quien busque acá el mecanismo no lo va a encontrar, y quien lo implemente afuera ignorando la regla va a construir un camino de acceso que INV-06 no cubre. Las dos son la misma equivocación leída desde dos lados.

## 2. Onboarding por tramos

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y la batería de dominio queda en verde | `./scripts/build.sh` termina en 0 y sin advertencias, y `./scripts/test.sh` pasa entero. La batería de dominio completa en menos de 10 segundos (§17.1.P.10 · GeometriaFactory-Domain) |
| 30 minutos | Sabe leer una guarda: elige un rechazo del catálogo, ubica el caso de uso que lo declara y la regla o el invariante que lo sostiene, y encuentra la prueba que lo ejercita | Escribe, sin abrir el intake, la tríada código → CU → RN o INV de tres rechazos cualesquiera, y la contrasta con `DX-Error-Messages.md` §6 |
| 1 hora | Sabe dónde poner una regla nueva: distingue una condición permanente sobre el estado, que es un invariante y va como guarda de la entidad, de un comportamiento o de un alcance de consulta, que no lo es y va en otra capa | Clasifica RN-02007, RN-02008, RN-02009 y RN-02011 como reglas sin invariante asociado y justifica por qué, coincidiendo con `Definicion-Modelo-De-Dominio.md` §4.3 |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

## 3. Quick-start

Objetivo del quick-start: **el primer resultado exitoso**, que acá es la batería de dominio en verde. Es el resultado más barato de obtener del producto entero, y es deliberado: este proyecto de código **se prueba sin nada** —sin base de datos, sin red y sin dobles (§17.1.P.6 · GeometriaFactory-Domain)—, que es exactamente lo que justifica su cobertura mínima más alta del producto.

### 3.1 Pasos

Todo el ciclo ocurre **dentro del entorno de desarrollo contenido definido en el propio repositorio**. El host no tiene las herramientas y no va a tenerlas (`PRODUCT-INTAKE` Parte C, decisiones comunes; `Alcance-Producto.md` §4.4). Ningún paso de acá se ejecuta en el host.

```bash
# 0. Abrir el repositorio en el entorno de desarrollo contenido, que el propio
#    repositorio define en `.devcontainer/`. Todos los pasos siguientes corren adentro.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh

# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y completa en menos de 10 segundos.
dotnet test tests/GeometriaFactory.Domain.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de script salen de `PRODUCT-INTAKE` §16 y el proyecto de prueba, de §17.1.P.6 · GeometriaFactory-Domain: no se eligen acá.

Después del paso 3 ya hubo primer resultado exitoso. El primer resultado **con sentido de dominio** llega al observar una guarda negándose, y está en `Guia-Onboarding-Developer.md` §3.

Lo que el quick-start deliberadamente **no** incluye, porque este proyecto de código no lo tiene: levantar una base de datos, aplicar una transformación de esquema, arrancar un servicio, configurar una credencial de acceso o pedir un dato de red. Si algún paso futuro los pide, el paso está mal ubicado.

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la etapa `a`, que es la que crea el andamiaje de la solución de código y ancla las versiones (§17.1.P.7 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain). El compromiso de verificación es el siguiente, y es lo que impide que este documento quede describiendo un quick-start que dejó de correr:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los scripts y las rutas salen de `PRODUCT-INTAKE` §16 y no se inventan acá.

## 4. Diátaxis

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza. Duplicarlos sería fabricar una segunda fuente de verdad sobre reglas que 02 ya declaró.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca trabajé contra este dominio; llevame de la mano una hora» |
| How-to | Tarea | Los trece casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`, cada uno con sus precondiciones, su flujo principal y sus flujos alternativos. En la etapa que corresponda, los ejemplos de uso que produzca `11-Documentacion` | «Tengo que constituir un alumno / enviar un trabajo / aplicar un desenlace: qué tengo que haber resuelto antes» |
| Reference | Información | `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` para entidades, atributos, cardinalidades y transiciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y `02-Especificacion-Funcional/Glosario-Funcional.md` para el vocabulario | «Qué atributos tiene una observación» / «qué significa `ENVIO_SIN_INTERPRETACION`» |
| Explanation | Comprensión | §1.2 y §1.3 de este documento; `Definicion-Modelo-De-Dominio.md` §4, §6 y §7; `Guia-Onboarding-Developer.md` §7 | «Por qué el dominio no verifica la unicidad del correo si es un invariante suyo» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes (`Guia-Onboarding-Developer.md` §5).
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla y el invariante que lo restringen, que son su explanation.
- El glosario de esta sección referencia el glosario funcional de 02 y el glosario raíz de 00 en lugar de redefinir términos.

Regla de mantenimiento, que evita el anti-patrón de documentación mezclada: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza. La regla vale también para el agente de IA que construye por etapas.

## 5. Mensajes de error y diagnóstico

Principio de redacción, aplicado sin excepción a las **42** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene una forma particular:

> El diagnóstico accionable de una condición de error del dominio dice siempre **qué hacer del lado del consumidor**, porque el dominio no resuelve nada por su cuenta: no consulta, no reintenta y no corrige el dato.

Cuatro precisiones que el catálogo hace cumplir:

1. **El dominio emite un código, no un texto.** No produce mensajes para personas, no los traduce y no los formatea: no conoce ningún formato de serialización (§17.1.P.1 · GeometriaFactory-Domain) ni cruza ninguna frontera de proceso (§17.1.P.3 · GeometriaFactory-Domain). El enunciado en lenguaje plano del catálogo es la base que la capa que expone usa para componer su mensaje, y la traducción a respuesta de protocolo pertenece a `GeometriaFactory-Api` (CU-02001 §6, CU-02004 §6).
2. **Ningún código es genérico.** Un rechazo dice qué guarda se negó, no «operación inválida». Es la misma exigencia que RN-02009 le impone al producto frente al alumno, aplicada acá frente al consumidor.
3. **Un código no filtra lo que la regla oculta.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia y el consumidor lo traduce a «no encontrado», nunca a «no autorizado» (RN-02003, INV-02, CU-02009 §6).
4. **Una condición de error no es una observación.** La distinción es la que sostiene todo el modelo y está desarrollada en [`Glosario-UX.md`](Glosario-UX.md) §3.1.

El catálogo completo, con su taxonomía y su cobertura por caso de uso, es [`DX-Error-Messages.md`](DX-Error-Messages.md).

## 6. Métricas DX

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: este proyecto de código no registra ni instrumenta (§17.1.P.10 · GeometriaFactory-Domain), el producto no tiene canal de correo (`Alcance-Producto.md` §5, exclusión X-1) y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio hasta la batería de dominio en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio, en el punto de control de la etapa |
| TTFV | Tiempo hasta el primer valor: haber visto una guarda negándose y saber ubicar su regla o su invariante | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora, resuelto sin abrir el intake |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones de error declaradas en los trece casos de uso que tienen entrada en el catálogo | 42 de 42, sin inventados | Recuento contra `DX-Error-Messages.md` §6, verificable por lectura de la §6 de cada caso de uso |
| Tiempo de diagnóstico de un rechazo | Tiempo desde ver un código de condición hasta ubicar el caso de uso, la regla y la acción esperada | <= 2 minutos | Cronometrado sobre tres códigos elegidos al azar del catálogo |

Las tres primeras son las métricas DX canónicas. Las dos últimas son propias de este proyecto de código y existen porque acá el catálogo de errores **es** la superficie pública: una condición sin entrada en el catálogo es superficie no documentada.

## 7. Feedback loop

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y los consumidores son proyectos de código del mismo producto. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner (`Vision-Producto.md` §9.1). Es donde se corre la verificación del quick-start de §3.2 y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control (§17.1.P.8 · GeometriaFactory-Domain). La compilación de los consumidores es la señal más temprana posible de un cambio incompatible de la superficie pública (§17.1.P.3 · GeometriaFactory-Application) | Un cambio que rompe la compilación de `GeometriaFactory-Application` es retroalimentación DX inmediata, no un accidente de construcción |
| Informe de cierre por etapa | Documento autocontenido por etapa, que se lee sin abrir el análisis ni el código (`Alcance-Producto.md` §4.3) | Lo que costó entender en la etapa se anota ahí y baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor del dominio e integrador de capa, los dos internos al producto (`00-Contexto/Vision-Producto.md` §2.2, concentración de roles en una persona) |
| Superficie pública que se documenta | Los trece contratos de uso de `02-Especificacion-Funcional/Casos-De-Uso/`: los **dos caminos de alta**, ciclo de vida de la cuenta, credencial derivada, **reseteo de contraseña**, admisibilidad, ciclo de vida del trabajo, reconstrucción de piezas, observaciones, envío, desenlace y los dos contratos de alcance |
| CU origen | CU-02001 a CU-02012 |
| Reglas de negocio relevantes | RN-02001 a RN-02016; invariantes INV-01 a INV-09 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de documentación de la superficie pública, US del quick-start verificable en el punto de control, US del catálogo de condiciones de error como artefacto mantenido junto al código |
| Tests previstos en 08 | Pruebas unitarias puras y sin dobles sobre cada guarda del catálogo; la batería completa en menos de 10 segundos (§17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain) |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. El dominio no tiene superficies de configuración |
| Primer arranque aplicado | N/A. El dominio no se despliega por instancia |
| Acceso de operador único aplicado | N/A. El dominio no dibuja ninguna superficie de acceso; ver §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed (§17.1.P.7 · GeometriaFactory-Domain) |
| Modelo UX-UI aplicado en la Fase B2 | N/A. `requiere_maqueta` == false |
| Validación visual de maqueta | N/A. `requiere_maqueta` == false |
| Línea de base emitida | N/A. `requiere_maqueta` == false |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial de la categoría, que nunca se había emitido. Declara el rol de intervención sin integradores externos, el enunciado de que la superficie pública de un modelo de dominio son sus guardas, la frontera de autenticación entre lo que el dominio modela y lo que no implementa, el onboarding en tres tramos con objetivo verificable, el quick-start entero dentro del entorno de desarrollo contenido con su compromiso de verificación por punto de control, la ubicación de los cuatro modos de Diátaxis sobre artefactos ya existentes de la cadena, los principios de redacción de las condiciones de error, cinco métricas DX medibles a mano sin telemetría y el lazo de retroalimentación apoyado en el punto de control de la etapa. **Corrección de la ronda r1 del audit, sobre esta misma emisión**: los pasos del quick-start pasan a nombrarse **por su papel** —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— conservando su forma literal, porque un quick-start que no se puede ejecutar no sirve, y se declara que las rutas y los nombres salen de `PRODUCT-INTAKE` §16 y §17.1.P.6 · GeometriaFactory-Domain. Se corrigen las referencias cruzadas a `Guia-Onboarding-Developer.md`, cuyo contenido sobre dónde va una regla nueva pasó a §7 y cuyos próximos pasos pasaron a §5, al recuperar esa guía la numeración de sus secciones obligatorias. El recuento de la métrica de cobertura del catálogo **no cambia**: sigue en 37 de 37. |
| 1.1 | 2026-08-09 | Alineación con la **corrección del P0** que reporta `B-02-03-GeometriaFactory-Application-r1.md` y que AG-02 resolvió emitiendo **CU-02012**, la configuración de la cuenta de administrador en el primer arranque. §1.2 acota el estado inicial de la entidad devuelta al **camino de alta** que la constituyó, en lugar de presentarlo como uniforme, que es la generalización que produjo el defecto. **§1.3 suma una fila a la frontera de autenticación**: la cuenta del administrador nace `Habilitado` y con su credencial ya aportada, porque es la que habilita a las demás; el mecanismo sigue afuera. La métrica de cobertura del catálogo pasa de 37 de 37 a **40 de 40** y el how-to de Diátaxis, de once a doce casos de uso. §8 declara los doce contratos y los dos caminos de alta. |
| 1.2 | 2026-08-09 | Alineación con la corrección del **P1** de la ronda r3, informe `B-02-03-GeometriaFactory-Domain-r3.md`. **Hallazgo H-02**: §1.2 y §2 remitían a §4.2 de `Definicion-Modelo-De-Dominio.md` a buscar la correspondencia entre reglas e invariantes, que vive en **§4.3**; §4.2 es el invariante candidato. Las dos remisiones quedan corregidas y la cabecera declara las dos subsecciones por separado. **Hallazgo H-01**: §1.3 suma a la frontera de autenticación la fila de que **ninguna de las cuatro operaciones de ciclo de vida procede sobre la cuenta del administrador** (F-03, CU-02002), y la advertencia de que estas reglas no protegen sólo el acceso: sin administrador **el circuito de revisión entero se detiene** y todo trabajo enviado queda en estado `Pendiente` para siempre. La métrica de cobertura **no cambia**: sigue en 40 de 40, porque el hallazgo produjo un renombre y no una condición nueva. |
| 1.3 | 2026-08-09 | Alineación con `PRODUCT-INTAKE` **1.7** y con la categoría 02 en su versión 1.4: capacidad **F-26**, caso de uso **CU-02013** de reseteo de contraseña, reglas **RN-02012** y **RN-02013**, e invariante **INV-09**. Los contratos de uso pasan de doce a **trece** y las reglas de once a **trece**; la métrica de cobertura del catálogo pasa de 40 de 40 a **43 de 43**; los invariantes pasan de siete a **nueve vigentes**, porque el intake **adoptó INV-08**, que esta cadena venía citando como candidato no vigente. §1.2 y la cabecera dejan de rotularlo como propuesto. La distinción operativa nueva —**resetear no es dar de baja**— vive en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.5 y no se duplica acá. |
| 1.4 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10** y **cierra dos filas de este archivo del hallazgo `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. **Intake 1.10**: las reglas del producto pasan de trece a **quince** con **RN-02014** y **RN-02015**; §1.2 y §8 actualizan el recuento y el rango, que pasa a **`RN-02001` a `RN-02016`**, y el de invariantes a **`INV-01` a `INV-09`**, que ya eran nueve. **`F26-20`**: **§5** aplicaba su principio de redacción a «las **treinta y siete** condiciones del catálogo» y son **43** desde la emisión de CU-02013 —el mismo archivo dice «43 de 43» veintidós líneas más abajo—; y **§1.3** enumeraba **tres** motivos de la respuesta de admisibilidad de CU-02004 y son **cuatro**: faltaba `CAMBIO_DE_CONTRASENA_PENDIENTE`, que es el que INV-09 hace exigible. **Ninguna condición del catálogo, ningún invariante y ningún contrato de uso cambia.** Sube minor. |
| 1.5 | 2026-08-10 | Absorbe el `PRODUCT-INTAKE` **1.13**, que incorpora **RN-02016** —habilitar una cuenta produce y fija su contraseña provisoria y la deja con cambio de contraseña pendiente— y precisa **F-04**. Las reglas del producto pasan de quince a **dieciséis**: §1.2, §5 y §8 actualizan el rango a **`RN-02001` a `RN-02016`**. La cobertura del catálogo pasa de **43 de 43** a **42 de 42**, y el principio de redacción de §5 se aplica a **42** condiciones: entra una en CU-02002 y salen dos cuyo camino anónimo desapareció. **§1.3** rehace dos filas de la frontera de autenticación: los motivos de admisibilidad pasan de cuatro a **tres**, con la constancia de cuál se retiró y por qué, y la fijación de la credencial pasa a declararse en el acto de habilitación, con CU-02002 junto a CU-02003. **Ningún invariante y ningún contrato de uso cambia de número.** Sube minor. |
