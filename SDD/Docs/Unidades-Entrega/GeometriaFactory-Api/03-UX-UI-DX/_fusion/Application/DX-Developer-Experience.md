# DX — Superficie pública de los casos de uso y los puertos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** DX-Developer-Experience.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4, §6, §7.4 y §11; §6 de los once casos de uso CU-04001 a CU-04011 de `02-Especificacion-Funcional/Casos-De-Uso/`, y sus §3, §5, §9 y §10; `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md` §4.1, §4.4 y §5; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2 (NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00006, NB-00007, NB-00009); RN-04001 a RN-04016 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, más **RN-04012** y **RN-04013**, hoy con archivo propio allá, y el invariante **INV-09** de `PRODUCT-INTAKE` **1.8** §17.1.P.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10**, §4 (F-26), §17.2 íntegro —en particular §17.2.P.1, §17.2.P.2, §17.2.P.3, §17.2.P.4, §17.2.P.5, §17.2.P.6, §17.2.P.8, §17.2.P.10, §17.2.P.11 y §17.2.P.12—, §4.1, §4.2 y §16
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Rol de intervención developer](#1-rol-de-intervención-developer)
  - [1.1 Quién interviene acá](#11-quién-interviene-acá)
  - [1.2 Qué es la superficie pública de este proyecto de código](#12-qué-es-la-superficie-pública-de-este-proyecto-de-código)
  - [1.3 La frontera entre autorizar y autenticar](#13-la-frontera-entre-autorizar-y-autenticar)
  - [1.4 Las cuatro negativas, y la que nunca se traduce a «no autorizado»](#14-las-cuatro-negativas-y-la-que-nunca-se-traduce-a-no-autorizado)
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

No hay integradores externos. `GeometriaFactory-Application` no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.2.P.7); sus dos consumidores son proyectos de código del mismo producto y no cruzan ninguna frontera de proceso (§17.2.P.3). Y son **dos consumidores de naturaleza distinta**, que es el rasgo que ordena toda esta sección:

| Tipo de developer | Quién es acá | Qué necesita de esta documentación |
| --- | --- | --- |
| Mantenedor de la capa | La persona que sostiene el producto y vuelve sobre este proyecto de código semanas después, sin el contexto de la etapa en que lo escribió. El equipo es de **una persona más un agente de IA** (`equipo_n` = 1) | Dónde va un caso de uso nuevo, qué puerto le corresponde declarar, y por qué una negativa existe |
| Integrador por casos de uso | La misma persona, o el agente, escribiendo `GeometriaFactory-Api` contra los casos de uso de esta capa | Qué contrato de uso invoca, qué tiene que haber resuelto **antes** de invocar, qué motivo recibe cuando no lo resolvió y **cómo se traduce ese motivo hacia afuera del proceso** |
| Implementador de puertos | La misma persona, o el agente, escribiendo `GeometriaFactory-Infrastructure` contra los puertos de esta capa | Qué le pide cada puerto, qué garantías tiene que sostener y qué **no** puede devolver sin romper un caso de uso |
| Operador | **No aplica.** Este proyecto de código no atiende peticiones, no abre conexiones, no registra ni instrumenta. Sus únicos NFR son el tiempo del caso de uso más pesado y la exclusión de los componentes en las consultas de listado (§17.2.P.10) | — |

Nivel de experiencia esperado: quien ya escribe código de aplicación, pero **no** necesariamente conoce el estilo de casos de uso con inversión de dependencias. La documentación no lo supone conocido: lo explica en §1.2 y lo enseña paso a paso en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.

Herramientas que ya conoce: el entorno de desarrollo contenido del propio repositorio y los scripts de `scripts/` (`PRODUCT-INTAKE` §16). No se supone ninguna otra.

### 1.2 Qué es la superficie pública de este proyecto de código

Lo primero que hay que entender, porque es la razón por la que esta capa existe y por la que se puede probar entera sin base de datos:

> **La superficie pública de esta capa tiene dos caras que miran para lados opuestos.** Una son los **casos de uso**, que un consumidor invoca. La otra son los **puertos**, que esta capa **declara** y otra capa implementa. La dependencia se invierte: acá se dice qué hace falta, y afuera se dice con qué.

Quien no entienda eso va a intentar consultar datos desde acá, y esa es la equivocación más frecuente contra esta capa. Cuatro consecuencias operativas, que gobiernan todo lo demás:

1. **Un puerto no es un cliente.** Esta capa no abre conexiones, no arma consultas y no elige motor. Declara «recuperar un trabajo», «resolver una consulta ya acotada por dueño o por alcance», «interpretar este texto y devolverme cuántas figuras trae el conjunto raíz, las piezas y las observaciones», «dame el sello» (`Especificacion-Funcional.md` §3). El cómo vive en `GeometriaFactory-Infrastructure`, detrás del contrato. Se renunció a consultar la base con proyecciones ad-hoc desde el caso de uso, y lo que se compró con esa renuncia es poder probar el caso de uso entero con dobles (`PRODUCT-INTAKE` §17.2.P.12).
2. **El recorte se traslada al puerto, no se aplica después.** El alumno pide sus trabajos y el pedido ya sale acotado al dueño; el administrador pide los de la comisión y el pedido ya sale con el predicado de alcance aplicado (CU-04006 §10, CU-04007 §10). Pedir todo y descartar en memoria da el mismo resultado visible y es exactamente el patrón que la separación entre alumnos viene a impedir.
3. **Esta capa orquesta y decide quién puede, pero no declara reglas.** Las **dieciséis** reglas del producto viven en `GeometriaFactory-Domain`, **las dieciséis con archivo propio allá**, y acá se **ejercen** sobre el pedido concreto (`Especificacion-Funcional.md` §6). Un caso de uso que enunciara una regla nueva estaría mal ubicado; el procedimiento de decisión está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7.3.
4. **Una negativa es una terminación controlada, no una avería.** El caso de uso no escribe nada, o deja todo exactamente como estaba, y devuelve un motivo de una enumeración cerrada. **El motivo no es un código de protocolo**: su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2). El catálogo completo de esas condiciones es [`DX-Error-Messages.md`](DX-Error-Messages.md).

El alcance transaccional se declara una sola vez y vale para los once contratos: **un caso de uso, una unidad de trabajo** (`Especificacion-Funcional.md` §3). Ninguna operación reparte sus escrituras entre varias, y por eso ninguna condición de error deja efecto parcial.

**Dos cosas que viajan por los puertos y que conviene reconocer antes de escribir nada**, porque las dos son fuente de equivocaciones caras:

- **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa**, no atributos del dominio. El modelo del dominio declara la fecha de alta del alumno —que recibe del consumidor, sin leer el reloj— y la «Fecha» que el alumno declara en su trabajo, y nada más. La discrepancia está elevada al Product Owner y declarada como punto abierto (`Especificacion-Funcional.md` §3 y §11). Que el reloj sea un puerto es lo que hace verificable en prueba cada uno de esos sellos.
- **La cantidad de figuras del conjunto raíz la produce el validador y la hace viajar CU-04005.** Entra por el puerto de validación junto con las piezas y las observaciones, y llega hasta el dominio, que la exige como precondición. **No es derivable de las piezas adoptadas**, porque ésas admiten huecos: la posición de una figura que no se pudo reconstruir queda reservada. Sin ese dato el dominio no tiene rango contra el cual validar la posición de una observación, y el mecanismo entero de RN-04009 deja de ser comprobable. CU-04005 es el único orquestador de la reconstrucción y del registro de observaciones, de modo que es el único que puede aportarlo.

**Un puerto no lleva identificador declarado aguas arriba y conviene saberlo antes de buscarlo.** El intake nombra tres —`IWorkRepository`, `IFigureValidator` e `ISystemClock` (`PRODUCT-INTAKE` §17.2.P.1)— y no nombra el **puerto de repositorio de cuentas**, que la orquestación de las cuentas y la verificación de unicidad del correo necesitan. No es una regla nueva ni una decisión de alcance: es un nombre, está declarado como punto abierto en `Especificacion-Funcional.md` §11 y esta sección **no lo reabre**. Acá se lo nombra en lenguaje de dominio, y su identificador se difiere a `05-Arquitectura-Tecnica` y al punto de control de la etapa `a`.

### 1.3 La frontera entre autorizar y autenticar

Es la frontera que hace que `tiene_auth` valga true en este proyecto de código, y conviene dejarla imposible de confundir porque un error acá se paga en dos capas a la vez. El proyecto de código hermano declaró la suya —lo que el dominio modela y lo que no implementa— y ésta es la de esta capa, con la misma forma.

**Enunciado en una línea, que es como conviene recordarlo: esta capa no autentica, autoriza.** Quién es la persona llega ya resuelto desde afuera; lo que se decide acá es qué puede hacer esa persona sobre este recurso concreto.

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Verificar que el trabajo pedido sea del alumno solicitante, sobre el dato recuperado y antes de escribir | **Sí** (CU-04004, CU-04005, CU-04006, CU-04009) | — |
| Verificar que quien pide una operación reservada tenga el papel `Administrador` | **Sí** (CU-04002, CU-04007, CU-04008) | — |
| Acotar lo que el administrador ve y opera, excluyendo los trabajos en `Borrador` | **Sí** (CU-04007, CU-04008, CU-04009) | — |
| Consultar si una cuenta admite el ingreso, y devolver el motivo cuando no lo admite | **Sí** (CU-04003 §4 y §6) | — |
| Exigir que el reemplazo de la credencial derivada declare verificada la vigente | **Sí** (CU-04003 FA-04) | — |
| Exigir que la configuración del administrador aporte credencial derivada, y que el auto-registro no la aporte | **Sí** (CU-04010 §6, CU-04001 §6). Son los dos caminos de alta, con reglas opuestas | — |
| Comparar una contraseña, derivarla, comparar la credencial vigente | **No.** El valor llega **ya derivado** y el valor en claro nunca atraviesa esta capa (CU-04003 §10, CU-04010 §3) | `GeometriaFactory-Infrastructure` (`PRODUCT-INTAKE` §17.2.P.5, §17.3.P.5) |
| Emitir o validar un acceso, sostener una sesión | **No.** Acá se resuelve si la cuenta lo admite y por qué; quién lo emite y con qué mecanismo es de las capas externas (CU-04003 §10) | `GeometriaFactory-Infrastructure` y `GeometriaFactory-Api` |
| Autenticar la petición y establecer quién la firma | **No.** La identidad del solicitante llega **declarada** por el consumidor, ya autenticada (CU-04004 §3, CU-04006 §3) | `GeometriaFactory-Api` |
| Traducir un motivo a respuesta de protocolo | **No.** El motivo es un valor de una enumeración cerrada, no un código de protocolo | `GeometriaFactory-Api` |

Dos precisiones que la tabla no alcanza a decir sola:

1. **Que el consumidor haya autenticado a la persona no alcanza.** El papel no dice de quién es el trabajo, y por eso la pertenencia se verifica igual y sobre el dato recuperado, no sobre lo que declara la petición (CU-04004 §10).
2. **La verificación no se resuelve ocultando un control en la pantalla.** Un alumno que fuerce la petición contra el servicio de datos tiene que ser rechazado igual, y eso es exactamente lo que esta capa hace verificable con dobles (CU-04008 §10, `Especificacion-Funcional.md` §4 punto 3).

Quien busque acá el mecanismo no lo va a encontrar; quien lo implemente afuera creyendo que la autorización viaja con él va a construir un camino que la pertenencia no cubre. Las dos son la misma equivocación leída desde dos lados.

### 1.4 Las cuatro negativas, y la que nunca se traduce a «no autorizado»

Las **cuatro** comprobaciones transversales de `Especificacion-Funcional.md` §4 producen cuatro negativas distintas, y **confundir las dos primeras es el error más caro que un consumidor puede cometer contra esta capa**: revelar que un recurso ajeno existe habilita el tanteo de identificadores.

| Negativa | Qué se preguntó | Motivo | Qué oculta | Traducción obligatoria del consumidor |
| --- | --- | --- | --- | --- |
| **Por pertenencia** | ¿Este trabajo es del alumno que lo pide? | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | **La existencia del recurso.** El trabajo ajeno y el identificador inexistente comparten motivo por diseño | «No encontrado», y **nunca** «no autorizado» |
| **Por facultad** | ¿Quien pide esta operación reservada tiene el papel `Administrador`? | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | **Nada.** No hay recurso ajeno cuya existencia proteger: se preguntó por una facultad, no por un recurso | Puede ser explícita: «requiere la facultad de administrador» |
| **Por alcance** | ¿Este trabajo entra en lo que el administrador ve? | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | **Nada.** Tampoco oculta la existencia: expresa que el trabajo está fuera de su flujo de trabajo | Puede ser explícita: «los borradores no forman parte de la revisión» |
| **Por cambio de contraseña pendiente** | ¿La cuenta solicitante fue reseteada y todavía no cambió la provisoria? | `CAMBIO_DE_CONTRASENA_PENDIENTE` | **Nada**, y además **corta antes que las otras tres**: no lee ni escribe nada (INV-09). Su única excepción declarada es el reemplazo de `CU-04003` FA-05, que es lo que la levanta | Debe ser explícita y **debe derivar al cambio de contraseña**: la cuenta se autentica y **no obtiene sesión de trabajo** (RN-04013) |

La regla mnemotécnica, que es la que hay que poder recitar sin abrir el documento: **el papel no reemplaza a la pertenencia, y la pertenencia no se confiesa.** Un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso (`Especificacion-Funcional.md` §4 punto 1).

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos motivos distintos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador— y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una (`Especificacion-Funcional.md` §4).

El tratamiento completo —con el procedimiento de decisión, la tabla de traducciones prohibidas y las pruebas que lo sostienen— está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4. Y lo que el dominio rechaza sin que acá llegue a ocurrir —por construcción, por equivalencia o por agregación deliberada— está reunido en su §2.5, que es la sección que evita que la ausencia de un motivo del dominio se lea como olvido.

## 2. Onboarding por tramos

Cada tramo cierra con un objetivo verificable: algo que se ejecuta o se responde, no una lectura declarada como hecha.

| Tramo | Objetivo | Cómo se verifica |
| --- | --- | --- |
| 5 minutos | El ciclo de construcción y de prueba corre entero dentro del entorno de desarrollo contenido, y la batería de la capa de aplicación queda en verde **sin base de datos** | `./scripts/build.sh` termina en 0 y sin advertencias, `./scripts/test.sh` pasa entero, y `dotnet test tests/GeometriaFactory.Application.Tests` queda en verde. Ninguna prueba de esta capa toca la base de datos real: es la puerta de calidad propia y bloqueante de §17.2.P.8 |
| 30 minutos | Sabe distinguir las tres negativas **de autorización** (`DX-Error-Messages.md` §2.4): dado un motivo del catálogo, dice si oculta la existencia del recurso o no, y cómo se traduce hacia afuera | Clasifica `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` sin abrir el intake, y coincide con [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4 |
| 1 hora | Entiende la inversión: nombra los cuatro puertos que esta capa declara, dice qué le pide a cada uno y ejercita un caso de uso entero con dobles, sin base de datos ni frontera de proceso | Recorre el criterio de aceptación CA-01 de CU-04005 con un validador doble y un repositorio simulado, explica por qué el reloj es un puerto y por qué la cantidad de figuras del conjunto raíz no se puede derivar de las piezas adoptadas. La tabla de puertos que tiene que reproducir es la de `Especificacion-Funcional.md` §3 |

El recorrido completo de esa primera hora, paso por paso, está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md).

## 3. Quick-start

Objetivo del quick-start: **el primer resultado exitoso**, que acá es la batería de la capa de aplicación en verde **sin base de datos, sin red y sin servicio levantado**. Es el resultado que mejor explica la capa: si hiciera falta preparar algo externo para correrla, la inversión de dependencias no estaría hecha.

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
#    proyecto de código. Criterio de éxito: verde, y sin haber preparado
#    ninguna base de datos, ningún servicio y ninguna credencial de acceso.
dotnet test tests/GeometriaFactory.Application.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque el lector los tiene que poder ejecutar. Las rutas y los nombres de script salen de `PRODUCT-INTAKE` §16 y el proyecto de prueba, de §17.2.P.6: no se eligen acá.

Después del paso 3 ya hubo primer resultado exitoso. El primer resultado **con sentido de aplicación** llega al ver un caso de uso entero resuelto con dobles, y está en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §3.

Lo que el quick-start deliberadamente **no** incluye, porque esta capa no lo tiene: levantar una base de datos, aplicar una transformación de esquema, arrancar un servicio, configurar una credencial de acceso o pedir un dato de red. Si algún paso futuro los pide, **el paso está mal ubicado y probablemente la prueba también**: la integración vive en `GeometriaFactory.Integration.Tests`, que pertenece a la Api (§17.2.P.6).

### 3.2 Verificación del quick-start

Los pasos son ejecutables a partir de la etapa `a`, que es la que crea el andamiaje de la solución de código y ancla las versiones. El compromiso de verificación es el siguiente, y es lo que impide que este documento quede describiendo un quick-start que dejó de correr:

- Se ejecuta a mano, sobre un clon limpio, en el punto de control de cada etapa que toque este proyecto de código.
- Si un paso deja de valer, el documento sube versión en la misma operación y declara el motivo en su control de cambios.
- Los nombres de los scripts y las rutas salen de `PRODUCT-INTAKE` §16 y §17.2.P.6, y no se inventan acá.

## 4. Diátaxis

Los cuatro modos existen, pero **tres de ellos ya viven en artefactos de la cadena** y este documento no los duplica: los ubica y los enlaza. Duplicarlos sería fabricar una segunda fuente de verdad sobre contratos que 02 ya declaró.

### 4.1 Dónde vive cada modo

| Modo | Orientación | Dónde vive | Qué responde |
| --- | --- | --- | --- |
| Tutorial | Aprendizaje | [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | «Nunca trabajé contra esta capa; llevame de la mano una hora» |
| How-to | Tarea | Los once casos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), cada uno con sus precondiciones, su flujo principal y sus flujos alternativos. En la etapa que corresponda, los ejemplos de uso que produzca `11-Documentacion` | «Tengo que dar de alta una cuenta / enviar un trabajo / aplicar un desenlace: qué tengo que haber resuelto antes» |
| Reference | Información | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §3 para los puertos y §4 para las cuatro comprobaciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Glosario-Funcional.md) para el vocabulario | «Qué le pide el caso de uso al puerto de validación de figuras» / «qué significa `OBSERVACION_MAL_FORMADA`» |
| Explanation | Comprensión | §1.2, §1.3 y §1.4 de este documento; `Especificacion-Funcional.md` §1, §4 y §8; [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §7 | «Por qué el reloj es un puerto» / «por qué la negativa por pertenencia no se distingue de la inexistencia» |

Este documento es, él mismo, del modo **explanation**: fija el marco y no enseña ninguna tarea.

### 4.2 Cómo se enlazan

- El tutorial termina en «próximos pasos» y enlaza explícitamente a los tres modos restantes ([`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §5).
- Cada entrada del catálogo de errores enlaza al caso de uso que la declara, que es su how-to.
- Cada caso de uso declara en su §9 la regla de negocio y el caso de uso de dominio que orquesta, que son su explanation.
- El glosario de esta sección referencia el glosario funcional de 02 y el glosario raíz de 00 en lugar de redefinir términos.

Regla de mantenimiento, que evita el anti-patrón de documentación mezclada: **un how-to que empieza a explicar por qué, se corta y enlaza**; una explanation que empieza a enumerar pasos, se corta y enlaza. La regla vale también para el agente de IA que construye por etapas.

## 5. Mensajes de error y diagnóstico

Principio de redacción, aplicado sin excepción a las **36** condiciones del catálogo: **qué pasó, por qué pasó, qué hacer al respecto**. La tercera parte es la que decide si el catálogo sirve, y acá tiene dos destinatarios en vez de uno:

> El diagnóstico accionable de una condición de esta capa dice **qué hacer del lado del consumidor** cuando la negativa nace de lo que el consumidor pidió, y **qué corregir del lado del adaptador del puerto** cuando nace de lo que un puerto devolvió.

Cinco precisiones que el catálogo hace cumplir:

1. **Esta capa emite un motivo, no un texto.** Es un valor de una enumeración cerrada, no un código de protocolo: la traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2). El enunciado en lenguaje plano del catálogo es la base con la que la capa que expone compone lo que una persona lee.
2. **Ningún motivo es genérico.** Una negativa dice qué comprobación se negó, no «operación inválida». Es la misma exigencia que RN-04009 le impone al producto frente al alumno, aplicada acá frente al consumidor.
3. **Un motivo no filtra lo que la regla oculta.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia y el consumidor lo traduce a «no encontrado», nunca a «no autorizado» (RN-04003, CU-04006 §6). Lo mismo vale para la cuenta inexistente en la consulta de admisibilidad, que no distingue el motivo hacia afuera para no revelar qué correos están registrados (CU-04003 §6 y §10).
4. **Una condición de error no es una observación.** Un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de este catálogo**: es el resultado declarado del envío (CU-04005 FA-01). La distinción está desarrollada en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2 y en [`Glosario-UX.md`](Glosario-UX.md) §3.1.
5. **El comentario del administrador tampoco es una observación**, y no aparece en ningún lugar de este catálogo: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala (CU-04008 §10).
6. **Un mismo motivo puede tener causas opuestas cuando los caminos son opuestos.** `ESTADO_INICIAL_NO_NEGOCIABLE` rechaza en el auto-registro un estado distinto de `Pendiente` y en la configuración del administrador uno distinto de `Habilitado`. No es una inconsistencia: el enunciado es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino. Es la única condición del catálogo con fila completa en dos subsecciones ([`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4).

El catálogo completo, con su taxonomía, su tratamiento de las tres negativas **de autorización** y su verificación de cobertura, es [`DX-Error-Messages.md`](DX-Error-Messages.md).

## 6. Métricas DX

Las métricas se miden **a mano**, cronometradas sobre un clon limpio en el punto de control de la etapa. No hay telemetría y no la va a haber: esta capa no registra ni instrumenta (§17.2.P.10), el producto no tiene canal de correo y no hay developers externos a quienes encuestar.

| Métrica | Definición | Objetivo | Cómo se mide |
| --- | --- | --- | --- |
| TTFS | Tiempo desde abrir el repositorio hasta la batería de la capa de aplicación en verde | <= 10 minutos, con el entorno de desarrollo contenido ya construido | Cronometrado a mano sobre un clon limpio, en el punto de control de la etapa |
| TTFV | Tiempo hasta el primer valor: haber ejercitado un caso de uso entero con dobles y saber nombrar los cuatro puertos | <= 1 hora, que es el tramo largo de §2 | El objetivo verificable del tramo de 1 hora, resuelto sin abrir el intake |
| Tasa de error en onboarding | Proporción de pasos del quick-start que fallan en la verificación del punto de control | 0 de 4 pasos | Ejecución del quick-start de §3.1 en cada punto de control |
| Cobertura del catálogo de errores | Condiciones declaradas en la §6 de los once casos de uso que tienen entrada en el catálogo | 36 de 36, sin inventadas | Recuento contra [`DX-Error-Messages.md`](DX-Error-Messages.md) §7, verificable por lectura de la §6 de cada caso de uso |
| Tiempo de diagnóstico de una negativa | Tiempo desde ver un motivo hasta ubicar el caso de uso, la comprobación que se negó y la acción esperada | <= 2 minutos | Cronometrado sobre tres motivos elegidos al azar del catálogo |
| **Traducciones prohibidas** | Cantidad de lugares del consumidor donde una negativa por pertenencia se traduce a «no autorizado», o donde un motivo revela la existencia de un recurso ajeno | **0, sin tolerancia** | Revisión de la traducción de motivos en `GeometriaFactory-Api` en cada punto de control, contra la tabla de [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4 |
| Pruebas de esta capa que tocan la base de datos | Cantidad de pruebas de `tests/GeometriaFactory.Application.Tests` que necesitan preparar algo externo | 0. Es la puerta de calidad propia y bloqueante de §17.2.P.8 | Verificación en el punto de control: una prueba que necesita preparar algo está mal ubicada y pertenece a integración |

Las tres primeras son las métricas DX canónicas. Las cuatro últimas son propias de este proyecto de código: dos porque acá el catálogo de motivos **es** la mitad de la superficie pública, y dos porque la inversión de dependencias sólo se sostiene si se mide.

## 7. Feedback loop

No hay canal de issues externo ni encuesta a developers de adopción: el equipo es de una persona más un agente de IA, y los consumidores son proyectos de código del mismo producto. El lazo existe igual y usa los mecanismos que el producto ya tiene:

| Vía | Qué recoge | Cómo se incorpora |
| --- | --- | --- |
| Punto de control de la etapa | Detención obligatoria a la espera del OK explícito del Product Owner. Es donde se corre la verificación del quick-start de §3.2 y se miden las métricas de §6 | Lo que falla se corrige antes de avanzar; el documento afectado sube versión en la misma operación |
| Pull request de la etapa | El pull request de la etapa **es** el punto de control (§17.2.P.8). Un cambio incompatible en un caso de uso o en un puerto rompe la compilación de `GeometriaFactory-Api` o de `GeometriaFactory-Infrastructure`, que es la señal más temprana posible (§17.2.P.3) | Una compilación rota aguas abajo es retroalimentación DX inmediata, no un accidente de construcción. La §17 de cada caso de uso declara qué cambio es compatible y cuál sube versión mayor |
| La puerta de calidad de la capa | «Ninguna prueba de esta capa toca la base de datos real» (§17.2.P.8). Cuando una prueba empieza a necesitar preparar algo, es señal de que un caso de uso dejó de pasar por un puerto | Se corrige la ubicación antes de fusionar, y si la señal se repite se revisa el diseño del puerto, no la prueba |
| Informe de cierre por etapa | Documento autocontenido por etapa, que se lee sin abrir el análisis ni el código | Lo que costó entender en la etapa se anota ahí y baja a esta sección como corrección de documentación |
| Uso por el agente de IA | Un tramo del onboarding que el agente no puede completar con los documentos enlazados es un defecto de esta sección, no del agente | Se corrige acá y se declara en el control de cambios |

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Mantenedor de la capa, integrador por casos de uso e implementador de puertos, los tres internos al producto (`00-Contexto/Vision-Producto.md` §2.2, concentración de roles en una persona) |
| Superficie pública que se documenta | Los once contratos de uso de [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) y los cuatro puertos de `Especificacion-Funcional.md` §3: repositorio de trabajos, repositorio de cuentas, validación de figuras y reloj del sistema |
| CU origen | CU-04001 a CU-04010 de este proyecto de código |
| Reglas de negocio relevantes | RN-04001 a RN-04016, **las dieciséis con archivo en `GeometriaFactory-Domain`**, con el lugar donde se ejerce cada una declarado en `Especificacion-Funcional.md` §6 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00006 (parcial), NB-00007 (parcial), NB-00009. NB-00008 no la toca este proyecto de código, y su motivo está declarado en `Especificacion-Funcional.md` §7.2 |
| Wireframes asociados | N/A. `tiene_ui_final` == false; el mínimo de wireframes para `library` es cero (`Rules-UX-UI-DX.md` §2.2) |
| US a generar en 06 | US de documentación de los once contratos y de los cuatro puertos; US del quick-start verificable en el punto de control; US del catálogo de condiciones mantenido junto al código; US de la traducción de motivos en el consumidor, con la traducción prohibida como criterio de aceptación |
| Tests previstos en 08 | Unitarias con dobles sobre cada condición del catálogo, **ninguna tocando la base de datos real** (§17.2.P.6, §17.2.P.8); el tiempo de resolución de CU-04005 medido sin acceso a base (§17.2.P.10) |
| Catálogo de diseño aplicado | N/A para variante DX (`Rules-UX-UI-DX.md` §1.4) |
| Configuración dirigida por esquema aplicada | N/A. Esta capa no tiene superficies de configuración |
| Primer arranque aplicado | N/A. Esta capa no se despliega por instancia. El alta inicial del administrador es un flujo alternativo de CU-04001, no una superficie de aprovisionamiento |
| Acceso de operador único aplicado | N/A. Esta capa no dibuja ninguna superficie de acceso; la frontera está en §1.3 |
| Identidad de versión aplicada | N/A. No produce artefacto desplegable identificable: no se publica en ningún feed (§17.2.P.7) |
| Modelo UX-UI aplicado en la Fase B2 | N/A. `requiere_maqueta` == false |
| Validación visual de maqueta | N/A. `requiere_maqueta` == false |
| Línea de base emitida | N/A. `requiere_maqueta` == false |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial de la categoría para este proyecto de código. Declara el rol de intervención con sus tres tipos internos y sin integradores externos; el enunciado de que la superficie pública de esta capa tiene dos caras opuestas —casos de uso y puertos— con la inversión de dependencias como rasgo que la gobierna; la frontera entre autorizar y autenticar en tabla, con las dos precisiones que la tabla no alcanza a decir; las tres negativas con su tabla de traducción obligatoria y la advertencia de que confundir las dos primeras es el error más caro contra esta capa; el onboarding en tres tramos con objetivo verificable; el quick-start entero dentro del entorno de desarrollo contenido, con su compromiso de verificación por punto de control; la ubicación de los cuatro modos de Diátaxis sobre artefactos ya existentes de la cadena; los principios de redacción de las condiciones de error con sus dos destinatarios; siete métricas DX medibles a mano sin telemetría, entre ellas la de traducciones prohibidas con tolerancia cero; y el lazo de retroalimentación apoyado en el punto de control y en la puerta de calidad propia de la capa. Cita el punto abierto del identificador del puerto de repositorio de cuentas sin reabrirlo. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **Alineación con el 02 corregido**: los recuentos pasan de nueve a **diez casos de uso** y de 27 a **34 condiciones**, por la partición de CU-04001 en los dos caminos de alta y el alta de CU-04010. §1.2 suma las **dos cosas que viajan por los puertos** —los sellos como metadatos de orquestación, y la **cantidad de figuras del conjunto raíz**, que el validador produce, que no es derivable de las piezas adoptadas porque admiten huecos, y sin la cual el dominio no tiene rango contra el cual validar la posición de una observación—. §1.3 suma la fila de la credencial en los dos caminos de alta. §1.4 suma la equivalencia entre la negativa de facultad de esta capa y los dos motivos del dominio, y remite a la sección nueva de rechazos que acá no ocurren. §2 suma al tramo de una hora la explicación de la cantidad de figuras. §5 suma la precisión del motivo con causas opuestas. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. Los recuentos pasan de diez a **once casos de uso** y de **34 a 36 condiciones**, por el alta de CU-04011, el reseteo de contraseña por el administrador. §1 pasa de once a **trece reglas**, con RN-04012 y RN-04013 citadas contra el intake porque todavía no tienen archivo aguas arriba. §3 pasa de **tres a cuatro comprobaciones transversales** y de tres a cuatro negativas: se suma la del cambio de contraseña pendiente, que hace exigible el invariante INV-09. |
| 1.2 | 2026-08-09 | **Reconciliación con lo que `GeometriaFactory-Domain` ya emitió y con el `PRODUCT-INTAKE` 1.8.** La cabecera, el principio 3 de §1 y la fila de reglas de §8 declaraban que **RN-04012 y RN-04013 no tenían archivo** en esa categoría y se citaban contra el intake: **las trece tienen archivo**, y los tres lugares pasan a decirlo. Las citas al intake que quedan vivas apuntan a **1.8**, que es la versión donde §4.1 precisó RN-04013 —la cuenta reseteada **se autentica y no obtiene sesión de trabajo**—; la consecuencia para esta capa vive en `02-Especificacion-Funcional` CU-04003 1.2 y no se duplica acá. Ningún recuento de esta sección cambia. |
| 1.3 | 2026-08-09 | **Cierra los hallazgos `F26-14` y las dos filas de este archivo del `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-14`**: **§1.4** se titulaba «Las **tres** negativas» y su propio cuerpo decía, dos líneas más abajo, que las cuatro comprobaciones transversales «producen **cuatro** negativas distintas»; la tabla tenía tres filas y `CAMBIO_DE_CONTRASENA_PENDIENTE` **no aparecía ni una vez en el archivo**, de modo que el marco DX de la capa no nombraba la negativa que **corta primero**. Entra la cuarta fila, con lo que oculta —nada—, con su excepción declarada —el reemplazo de `CU-04003` FA-05, que es lo que la levanta— y con su traducción obligatoria, que es derivar al cambio de contraseña sin emitir sesión (RN-04013). El título pasa a decir **cuatro**. **`F26-20`**: **§6** hablaba de «las **treinta y cuatro** condiciones del catálogo», que son **36** desde `CU-04011`, y el mismo archivo declara 36 más abajo; y **§8** citaba «RN-04001 a RN-04013», que son **RN-04001 a RN-04016** desde el intake 1.10, las quince con archivo en `GeometriaFactory-Domain`. **Ningún caso de uso, puerto ni condición del catálogo cambia.** Sube minor. |
| 1.4 | 2026-08-10 | Alineación de recuento con `PRODUCT-INTAKE` **1.13**, que incorpora la regla **RN-04016** —habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo— y lleva las reglas de negocio del producto de quince a **dieciséis**. La cabecera de trazabilidad y la tabla de referencias pasan a declarar el rango **`RN-04001` a `RN-04016`**. **Ninguna decisión, ningún artefacto y ninguna condición de este documento cambia**: RN-04016 no tiene tramo propio acá. Sube minor. |
| 1.5 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en las declaraciones vivas de este archivo que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** El tercer punto de §1 decía que las **quince** reglas del producto viven en `GeometriaFactory-Domain`, «las quince con archivo propio allá». Son **dieciséis**, `RN-04001` a `RN-04016`, contadas sobre los archivos de ese directorio, y las dieciséis tienen archivo propio. **Ningún procedimiento de decisión, ningún diagnóstico y ninguna otra afirmación de este documento cambia.** Sube minor. |
| 1.6 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](../../../../../Producto/Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`—. Acá son **3 ocurrencias**, las de la nota sobre el puerto de repositorio de cuentas, que son **reporte de la fuente** (norma §4.1): el intake se renombró en este mismo tramo. **El punto abierto del cuarto puerto no se reabre ni se resuelve.** **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-04001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
