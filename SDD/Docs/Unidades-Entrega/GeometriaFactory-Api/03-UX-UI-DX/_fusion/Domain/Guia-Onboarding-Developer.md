# Guía de onboarding — la primera hora contra el modelo de dominio

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.6
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` §2, §4.1, §4.2 (recorrido de adopción de INV-08), §4.3 (correspondencia entre reglas e invariantes), §5.1, §5.2 y §7; CU-02001 §4, §5, §6 y §8; CU-02012 §1, §3, §4, §5, §6 y §8; CU-02002 §5 y §6; CU-02003 §6; CU-02004 §5 y §6; CU-02005 §5 y §6; CU-02006 §5 y §6; CU-02007 §5 y §6; CU-02008 §4, §5 y §6; CU-02009 §5 y §6; CU-02010 §5 y §6; CU-02011 §5 y §6; CU-02013 §1, §5, §6 y §10; RN-02001 a **RN-02016**; `02-Especificacion-Funcional/Especificacion-Funcional.md` §9 (puntos abiertos); `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.4; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1.P.1 · GeometriaFactory-Domain, §17.1.P.2 · GeometriaFactory-Domain, §17.1.P.4 · GeometriaFactory-Domain, §17.1.P.5 · GeometriaFactory-Domain, §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain, §4.1, §4.2, §16
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
- [2. Instalación o acceso](#2-instalación-o-acceso)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 El resultado que se busca](#31-el-resultado-que-se-busca)
  - [3.2 Los pasos](#32-los-pasos)
  - [3.3 Leer la guarda que se negó](#33-leer-la-guarda-que-se-negó)
  - [3.4 Las tres máquinas de estado, en diez minutos](#34-las-tres-máquinas-de-estado-en-diez-minutos)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Control de cambios](#6-control-de-cambios)
- [7. Dónde va una regla nueva](#7-dónde-va-una-regla-nueva)
  - [7.1 Dieciséis reglas, nueve invariantes](#71-dieciséis-reglas-nueve-invariantes)
  - [7.2 Por qué seis reglas no tienen invariante](#72-por-qué-seis-reglas-no-tienen-invariante)
  - [7.3 El procedimiento de decisión](#73-el-procedimiento-de-decisión)

---

## 1. Audiencia y prerrequisitos

Esta guía está escrita para dos lectores concretos, y no para un integrador hipotético:

- **El mantenedor que vuelve sobre este proyecto de código sin el contexto de la etapa en que lo escribió.** Es el caso más frecuente en un equipo de una persona.
- **El agente de IA que construye por etapas** y que necesita, en cada arranque, reconstruir por qué una guarda existe antes de tocarla.

Los dos escriben además `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, que son los únicos consumidores de esta superficie pública.

Prerrequisitos:

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio abierto en el entorno de desarrollo contenido del propio repositorio | Todo el ciclo ocurre adentro. El host no tiene las herramientas y no va a tenerlas (`Alcance-Producto.md` §4.4) |
| Nada más | Sin base de datos, sin red, sin servicio levantado y sin credencial de acceso. Este proyecto de código no tiene dependencias (`PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain) y su persistencia está declarada como «no aplica» (§17.1.P.4 · GeometriaFactory-Domain) |

Conocimiento previo que **no** se supone: el estilo de modelo de dominio con invariantes explícitas. Es lo que la §7 de esta guía enseña.

Vocabulario mínimo para no perderse en la primera media hora. Los términos están definidos en `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz, y en `02-Especificacion-Funcional/Glosario-Funcional.md`; acá sólo se enumeran para que el lector sepa qué buscar: alumno, trabajo, pieza, componente, observación, advertencia, error de validación, estado de cuenta, **camino de alta**, estado del trabajo, credencial derivada, texto original, desenlace y comentario.

Una convención que conviene conocer desde el primer minuto porque se cruza en cada documento: **`Pendiente` va siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos (`Vision-Producto.md` §9.2).

## 2. Instalación o acceso

No hay instalación: este proyecto de código no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain). El acceso es abrir el repositorio.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh
```

Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido y se pasa a §3. Si alguno falla, la §4 tiene los tres arranques que fallan de verdad.

## 3. Primer ejemplo ejecutable

### 3.1 El resultado que se busca

El primer resultado exitoso de este proyecto de código es **la batería de dominio en verde en menos de 10 segundos**. Vale la pena detenerse en por qué es tan barato: las pruebas son unitarias puras y **sin dobles**, porque no hay nada que sustituir. No hay base de datos que preparar, ni servicio que levantar, ni reloj que congelar —la fecha llega como dato, porque el reloj es un puerto de la capa de aplicación—. Eso es consecuencia directa de la regla de dependencias, y es lo que justifica que este sea el proyecto de código con la cobertura mínima más alta del producto (`PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain).

### 3.2 Los pasos

```bash
# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y completa en menos de 10 segundos.
dotnet test tests/GeometriaFactory.Domain.Tests
```

El segundo resultado, que es el que tiene sentido de dominio, es **ver una guarda negándose**. El caso más corto es el criterio de aceptación CA-02 de CU-02001, el **auto-registro del alumno**, transcripto acá tal como el caso de uso lo declara:

| Given | When | Then |
| --- | --- | --- |
| Los datos de registro con apellido vacío y correo `ana@example.com` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `DATO_OBLIGATORIO_AUSENTE` y **no devuelve ninguna entidad** |

Y el contraste, que es CA-01 del mismo caso de uso:

| Given | When | Then |
| --- | --- | --- |
| Correo `ana@example.com`, nombre `Ana`, apellido `Rossi`, fecha de alta 2026-08-09, con la unicidad del correo declarada como verificada | La capa de aplicación solicita constituir el alumno | El dominio devuelve un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor y 0 trabajos |

Los dos criterios se materializan en `tests/GeometriaFactory.Domain.Tests` y se ejecutan con el paso 3. **Los nombres de tipos y de espacios de nombres son un punto abierto declarado**, que se resuelve en `05-Arquitectura-Tecnica` y se valida en el punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain; `Especificacion-Funcional.md` §9). Por eso acá los conceptos se nombran en lenguaje de dominio y no se propone ninguna firma: la que valga la va a fijar 05, y esta guía la va a citar entonces.

### 3.3 Leer la guarda que se negó

Con el rechazo a la vista, el recorrido de lectura es siempre el mismo, y es lo que hay que automatizar en la cabeza:

1. **El código** dice qué guarda se negó: `DATO_OBLIGATORIO_AUSENTE`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer del lado del consumidor.
3. **El caso de uso** que lo declara (§6 de CU-02001) dice cuál es la respuesta del dominio: rechaza la constitución, no se produce ninguna instancia, no hay efecto parcial.
4. **La regla o el invariante** (§9 del mismo caso de uso) dice por qué la guarda existe.

Cuatro saltos, todos con enlace. Si en algún rechazo la cadena se corta, es un defecto de esta sección.

Y la observación que le da sentido a todo el ejercicio: **el dominio no arregló nada**. No completó el apellido, no consultó a nadie y no reintentó. Devolvió la causa y se quedó quieto. Quien tiene que decidir qué hacer es el consumidor.

### 3.4 Las tres máquinas de estado, en diez minutos

Son las tres figuras que más se consultan durante la primera hora. Están completas, con sus transiciones inadmisibles, en `Definicion-Modelo-De-Dominio.md` §5.1, §5.2 y §5.3. Lo que hay que retener:

**Cuenta.** Tres estados: `Pendiente`, `Habilitado`, `Bloqueado`. **El estado con el que nace depende del camino de alta, y hay dos**:

| Camino de alta | Caso de uso | Nace | Credencial |
| --- | --- | --- | --- |
| Auto-registro del alumno | CU-02001 | cuenta `Pendiente` | Sin valor: se fija **en el acto de habilitación** (CU-02002, que invoca la fijación de CU-02003) con la contraseña provisoria que el sistema produce (RN-02016) |
| Configuración del administrador en el primer arranque | CU-02012 | cuenta **`Habilitado`** | Se aporta ya derivada en el mismo acto |

**La cuenta del administrador nace habilitada porque es la que habilita a las demás**: ninguna cuenta anterior podría habilitarla a ella, y si naciera `Pendiente` por INV-06 tampoco obtendría acceso, de modo que la instancia quedaría inutilizable en el primer arranque. Cada camino **rechaza el del otro**, y por eso `ESTADO_INICIAL_NO_NEGOCIABLE` tiene causas opuestas en cada uno (`DX-Error-Messages.md` §1.4).

Ningún estado vuelve a `Pendiente`. El administrador habilita, bloquea y rehabilita, siempre con acto explícito: no hay habilitación automática. La baja no es un estado: es la desaparición de la cuenta y de sus trabajos (RN-02007). Bloquear una cuenta `Pendiente` sin haber pasado por `Habilitado` no está declarado, y el dominio no lo infiere (CU-02002 FA-03).

**Las cuatro operaciones alcanzan sólo a las cuentas con papel `Alumno`**, y no es una decisión del modelo: es el enunciado literal de la capacidad F-03, «habilitar, bloquear, rehabilitar y dar de baja física cuentas **de alumno**». Sobre la cuenta del administrador no procede ninguna de las cuatro, y el dominio las rechaza con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`. El motivo es la contracara del anterior: **la cuenta que habilita a las demás no puede quedar en un estado del que nadie pueda sacarla**. Y el efecto no se agota en el acceso —que ya sería grave por INV-06—: **sin administrador nadie aprueba ni rechaza**, así que todo trabajo enviado queda en estado `Pendiente` para siempre y el circuito de revisión entero se detiene (RN-02010).

Las dos guardas, la del alta y la del ciclo de vida, son la misma familia vista en dos momentos, y es la propiedad que **INV-08** enuncia de una sola vez. **Está adoptado desde `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain** y se cuenta entre los nueve invariantes vigentes de §7.1; la guía lo citaba como candidato y esa cita quedó desactualizada.

**Trabajo.** Cuatro estados: `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`. Dos propiedades gobiernan lo demás:

1. **`Borrador` significa exactamente «el texto no verificó»**, o que el trabajo recién se creó. Guardar y enviar se unificaron en una sola acción, enviar: no se puede conservar en borrador un trabajo cuyo texto sí verifica.
2. **`Finalizado` y `Rechazado` son terminales.** No sale ninguna transición de ellos, y corregir un rechazo significa cargar un trabajo nuevo (INV-07, RN-02010). Lo único que un trabajo terminal admite es que el administrador lo elimine.

Un trabajo sin piezas y sin observaciones es un estado normal: es el trabajo recién creado, antes de que su texto se haya interpretado.

Tres distinciones que conviene fijar antes de seguir, porque confundirlas es el error más caro de esta sección:

| No son lo mismo | Diferencia | Dónde está declarado |
| --- | --- | --- |
| Observación y condición de error del dominio | La **observación** es una entidad del dominio, con varias filas por trabajo, que el producto emite al interpretar el texto del alumno; sus dos especies son la advertencia y el error de validación. La **condición de error** es una guarda que impide una operación ilegítima del consumidor y no se guarda en ninguna parte | `Definicion-Modelo-De-Dominio.md` §2.5; [`Glosario-UX.md`](Glosario-UX.md) §3.1 |
| Observación y comentario | La observación la emite el producto y hay tantas como defectos; el **comentario** lo escribe una persona, el administrador, y hay a lo sumo uno por trabajo. No es una calificación | `Vision-Producto.md` §9.1; `Definicion-Modelo-De-Dominio.md` §2.5 |
| Advertencia y error de validación | Las dos son especies de observación. Sólo el **error de validación** impide que el trabajo pase a estado `Pendiente`; la advertencia no lo impide, y es deliberado | RN-02005; `Vision-Producto.md` §9.1 |

## 4. Diagnóstico de problemas frecuentes en la primera hora

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio en el entorno contenido del propio repositorio y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar (§17.1.P.8 · GeometriaFactory-Domain) | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| La batería de dominio tarda notablemente más de 10 segundos | Se coló una dependencia de entrada y salida en una prueba que debería ser pura y sin dobles | Buscar qué prueba toca algo externo. Este proyecto de código no tiene dependencias: una prueba que necesita preparar algo está probando otra capa |
| Se busca dónde se guarda la entidad y no aparece | La persistencia está declarada como «no aplica» (§17.1.P.4 · GeometriaFactory-Domain). El dominio no guarda nada; la materialización es posterior y externa | Buscar en `GeometriaFactory-Infrastructure`. En el fallo de una operación de dominio no queda estado intermedio, justamente porque no guarda |
| Se busca dónde se interpreta el texto del alumno | No está acá. La interpretación y el cálculo del valor derivado los hace el validador de figuras, detrás de un puerto de la capa de aplicación | `Definicion-Modelo-De-Dominio.md` §7. El dominio **recibe** el resultado de la interpretación por CU-02006 y CU-02007 |
| La configuración del administrador rechaza con `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, o queda una cuenta de administrador `Pendiente` que nadie puede habilitar | Se está usando el camino del auto-registro para constituir el administrador. Son **dos caminos de alta distintos**, con estado inicial y credencial propios | Usar CU-02012, que constituye la cuenta `Habilitado` y con su credencial en el mismo acto. Es la corrección del P0: con el camino equivocado la instancia queda inutilizable en el primer arranque |
| Se busca la comparación de contraseñas o la emisión del acceso | El dominio no implementa autenticación. Sí modela las reglas que la condicionan | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3 |
| Una llamada de constitución rechaza con `UNICIDAD_DE_CORREO_NO_VERIFICADA` y el correo evidentemente está libre | El consumidor no **declaró** haber verificado la unicidad. El dominio no consulta: exige la declaración | Resolver la unicidad en la capa de aplicación con el puerto de repositorio y declararla al invocar (CU-02001 §3) |
| Un envío rechaza con `ENVIO_SIN_INTERPRETACION` | Se envió un trabajo cuyo texto original nunca fue interpretado. El envío decide **sobre** el resultado de la interpretación | Invocar antes CU-02006 y CU-02007 con el resultado que produjo el validador, y recién después CU-02008 |
| Un envío devuelve el trabajo en `Borrador` y se lo interpreta como error | No es un error: es el resultado declarado del envío cuando hay al menos una observación de especie error de validación (CU-02008 FA-01). Las advertencias no lo impiden | No traducirlo a fallo hacia afuera: el alumno corrige y vuelve a enviar cuantas veces haga falta |
| Se intenta corregir un trabajo `Rechazado` y todo rebota | `Rechazado` es terminal por decisión aceptada por escrito. Corregir un rechazo significa cargar un trabajo nuevo | CU-02010 FA-03. El rechazado queda como registro del intento y sólo el administrador puede quitarlo |
| No se encuentra el identificador propio de una pieza | No existe: **la identidad de la pieza es su posición** en el conjunto raíz, porque el dato del alumno no trae identificador | `Definicion-Modelo-De-Dominio.md` §2.3 y §6. Por eso el orden del texto del alumno es significativo |
| Dos correos que parecen el mismo se tratan como distintos | El criterio con el que dos correos se consideran el mismo es un **punto abierto declarado y no bloqueante**: el dominio conserva el dato como lo recibe y no normaliza | `Especificacion-Funcional.md` §9. Lo resuelve `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación |
| Se busca el nombre exacto de un tipo o de un espacio de nombres | Es el otro punto abierto declarado: se fija en 05 y se valida en el punto de control de la etapa `a` | `PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain. Hasta entonces, los conceptos se nombran en lenguaje de dominio |

## 5. Próximos pasos

Los cuatro modos de documentación, con el orden en que conviene visitarlos después de esta guía. El plan completo está en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4.

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los trece contratos de uso | Cuando hay que invocar una operación concreta y saber qué resolver antes |
| Reference | [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) para entidades y transiciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Domain/Glosario-Funcional.md) para el vocabulario | Cuando hace falta un dato puntual |
| Explanation | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2 y §1.3; `Definicion-Modelo-De-Dominio.md` §4, §6 y §7; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su `README.md` propone un orden de lectura de ocho pasos que este onboarding no duplica.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Recorrido de la primera hora para el mantenedor y para el agente de IA, sin integradores externos. Declara los prerrequisitos reducidos a abrir el repositorio en el entorno de desarrollo contenido, el primer resultado exitoso como la batería de dominio en verde sin dobles, el primer ejemplo con sentido de dominio tomado de CA-01 y CA-02 de CU-02001, el recorrido de cuatro saltos para leer una guarda, el resumen de las dos máquinas de estado y las tres distinciones que separan observación, condición de error y comentario, el procedimiento de decisión de cuatro pasos sobre dónde va una regla nueva, catorce diagnósticos de la primera hora y el enlace explícito a los cuatro modos de Diátaxis. Cita los dos puntos abiertos declarados por 02 sin reabrirlos. **Corrección de la ronda r1 del audit, sobre esta misma emisión**: el contenido «Dónde va una regla nueva», que se había insertado como §4 y desplazaba tres secciones obligatorias de `Rules-UX-UI-DX.md` §4.2.4, se reubica **al final como §7**, con la misma convención de sección opcional numerada después del control de cambios que ya usan los casos de uso de 02; las seis secciones obligatorias recuperan su numeración 1 a 6 y se corrigieron las referencias cruzadas de los dos documentos que la citaban. **Corrección de la ronda r2 del audit, hallazgos N-01 y N-04**: la afirmación anterior era incompleta y acá queda rectificada. La renumeración había dejado además **una referencia interna** a la numeración vieja, en §2, que apuntaba a §5 en lugar de a §4 para el diagnóstico; se corrigió y se barrió el documento entero en busca de otras, sin encontrar ninguna más. Se corrige también el recuento de la tabla de §4, que en ese momento tenía **trece** filas y no catorce. La tabla de §7.1 pierde la columna «Dónde se ejerce», redundante con la §9 de cada caso de uso. Los comandos y las rutas del quick-start pasan a nombrarse **por su papel** —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— conservando su forma literal, que es la que permite ejecutarlos. |
| 1.1 | 2026-08-09 | Alineación con la **corrección del P0** que reporta `B-02-03-GeometriaFactory-Application-r1.md` y que AG-02 resolvió emitiendo **CU-02012**. **§3.4 se reescribe**: la máquina de la cuenta deja de tener un estado inicial único y pasa a declarar los **dos caminos de alta** en tabla, con el fundamento de por qué la cuenta del administrador nace `Habilitado` —es la que habilita a las demás y ninguna anterior podría habilitarla a ella— y con el reenvío a la advertencia de que `ESTADO_INICIAL_NO_NEGOCIABLE` tiene causas opuestas en cada camino. §1 suma «camino de alta» al vocabulario mínimo y §3.2 acota su ejemplo al auto-registro. **§4 suma un diagnóstico**, el del administrador constituido por el camino equivocado, y pasa de trece a **catorce** filas. §5 actualiza el how-to a doce contratos de uso. La §7 no cambia: la correspondencia entre las once reglas y los siete invariantes sigue igual, y el invariante candidato **INV-08 no es vigente**. |
| 1.2 | 2026-08-09 | Alineación con la corrección del **P1** de la ronda r3, informe `B-02-03-GeometriaFactory-Domain-r3.md`. **Hallazgo H-02**: §7.1 remitía a §4.2 de `Definicion-Modelo-De-Dominio.md` a buscar la correspondencia entre reglas e invariantes, que vive en **§4.3**; corregida, y la cabecera declara las dos subsecciones por separado. **Hallazgo H-01**: §3.4 suma que **las cuatro operaciones de ciclo de vida alcanzan sólo a las cuentas con papel `Alumno`** —enunciado literal de la capacidad F-03—, que sobre la cuenta del administrador el dominio las rechaza con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR`, y que el efecto no se agota en el acceso: **sin administrador nadie aprueba ni rechaza y el circuito de revisión entero se detiene**. Declara además que la guarda del alta y la del ciclo de vida son la misma familia en dos momentos, que es lo que el candidato **INV-08** propone enunciar de una sola vez, y que sigue no vigente. La tabla de §4 **no cambia**: sigue en catorce diagnósticos. |
| 1.3 | 2026-08-09 | Alineación con `PRODUCT-INTAKE` **1.7** y con la categoría 02 en su versión 1.4. §7.1 pasa de «once reglas, siete invariantes» a **trece reglas, nueve invariantes**, con **RN-02012** y **RN-02013** compartiendo **INV-09** —el único invariante que sostiene dos reglas— y con **INV-08 declarado adoptado**, que esta guía venía citando como candidato no vigente. §5 actualiza el how-to a trece contratos de uso y la cabecera suma CU-02013. La distinción entre resetear y dar de baja se lee en [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.5, que es donde vive el catálogo. |
| 1.4 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10** y **cierra la fila de este archivo del hallazgo `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0. **Intake 1.10**: **§7.1** pasa de «trece reglas» a **quince**, con **RN-02014** y **RN-02015** en su tabla de correspondencia, las dos **sin invariante**; el recuento del párrafo pasa de «nueve de las trece … y cuatro no» a **«nueve de las quince … y seis no»**, contado sobre la tabla, y se declara además de dónde viene la lectura que le da INV-09 a RN-02012 —de la **columna** de `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain y no de su prosa—, para no repetir el hallazgo `F26-09`. **§7.2** pasa de «cuatro reglas» a **seis** y suma el motivo de cada una de las dos nuevas: RN-02014 describe cómo se produce un valor **y además no se ejerce en este proyecto de código**, y RN-02015 enuncia la **ausencia** de una precondición. La tabla de §7.1, que llegaba hasta RN-02011, suma también **RN-02012** y **RN-02013**, que la versión 1.3 había contado en prosa sin agregarlos a la tabla. **`F26-20`**: **§3.4** se titulaba «Las **dos** máquinas de estado» y son **tres** desde `Definicion-Modelo-De-Dominio.md` §5.3, la de la marca de cambio de contraseña pendiente; el título, el índice y la remisión pasan a nombrar las tres. **Ningún paso del quick-start, ningún procedimiento de decisión y ningún diagnóstico cambia.** Sube minor. |
| 1.6 | 2026-08-10 | Absorbe el `PRODUCT-INTAKE` **1.13** y su regla **RN-02016** —habilitar una cuenta produce y fija su contraseña provisoria y la deja con cambio de contraseña pendiente—, con la precisión de **F-04**. **§7.1** pasa de «quince reglas» a **dieciséis**, con RN-02016 en la tabla de correspondencia asociada a **INV-09**, y el recuento del párrafo pasa de «nueve de las quince … y seis no» a **«diez de las dieciséis … y seis no»**, contado sobre la tabla; la nota del invariante compartido pasa de dos reglas a tres. **§7.2** suma el motivo por el que RN-02016 **sí** tiene invariante, que es la contracara de las seis que no lo tienen. **§3.2** corrige la tabla de los dos caminos de alta: la credencial del auto-registro deja de fijarse en el primer ingreso efectivo y se fija en el acto de habilitación. **Ningún paso del quick-start, ningún procedimiento de decisión y ningún diagnóstico cambia.** Sube minor. |
| 1.5 | 2026-08-10 | **Cierra la parte del hallazgo de forma que alcanza a este archivo**, detectada al propagar `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 contra `PRODUCT-INTAKE` **1.11**. **§6 Control de cambios**: la fila **1.3** estaba **fuera de la tabla**, al final del archivo, detrás de §7, y además **después** de la 1.4. Se reincorpora a la tabla de §6 en su lugar cronológico, entre la 1.2 y la 1.4, **sin alterar una palabra de lo que dice**. **Ningún paso del recorrido, ejemplo, diagnóstico ni criterio de esta guía cambia.** Sube minor: repara el renderizado de una tabla y ordena su historial. |

## 7. Dónde va una regla nueva

Este es el tramo de una hora y el que más rinde a largo plazo. La pregunta que responde es: aparece una regla nueva, ¿va como guarda de una entidad de este proyecto de código, o va en otra capa?

### 7.1 Dieciséis reglas, nueve invariantes

Los invariantes **no son reglas distintas** de las **dieciséis** del negocio: son las mismas vistas desde el dominio. La regla declara qué decidió el negocio; el invariante declara qué condición sobre los datos no puede romperse nunca, sin importar la operación ni quién la ejecute, aunque la petición llegue por fuera de la interfaz.

**Diez** de las dieciséis reglas tienen invariante asociado y **seis** no; **RN-02012, RN-02013 y RN-02016 comparten INV-09**, que es el único invariante que sostiene más de una regla — y la lectura que sostiene esa fila es la **columna** «regla de negocio que sostiene» de `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain, no su prosa, que enumera a RN-02012 entre las que no tienen ninguno (`02-Especificacion-Funcional/Especificacion-Funcional.md` §4). La correspondencia es la de `Definicion-Modelo-De-Dominio.md` §4.3 y se transcribe acá porque es el corazón de este tramo:

| Regla | Enunciado abreviado | Invariante que la expresa como condición permanente |
| --- | --- | --- |
| RN-02001 | Existe exactamente un administrador; su alta sólo es posible mientras no exista ninguno | INV-05 |
| RN-02002 | El correo del alumno es único | INV-01 |
| RN-02003 | Un alumno sólo ve y opera sus propios trabajos | INV-02 |
| RN-02004 | El alumno elimina sólo en `Borrador`; el administrador, cualquier trabajo que ve | INV-03 |
| RN-02005 | Un trabajo no pasa a estado `Pendiente` con errores de validación; las advertencias sí lo permiten | INV-04 |
| RN-02006 | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | INV-06 |
| RN-02007 | La baja arrastra los trabajos y exige confirmación escrita | — |
| RN-02008 | El texto original del alumno se conserva íntegro | — |
| RN-02009 | Toda observación de error indica la posición de la pieza y el campo | — |
| RN-02010 | El desenlace es exclusivo del administrador y los dos estados de cierre son terminales | INV-07 |
| RN-02011 | El administrador no ve los trabajos en `Borrador` | — |
| RN-02012 | El reseteo de contraseña conserva la cuenta y todos sus trabajos | INV-09 |
| RN-02013 | Con la contraseña provisoria sin cambiar, la cuenta no llega a ninguna otra parte | INV-09 |
| RN-02014 | La contraseña provisoria la produce el sistema: no es adivinable y no se repite | — |
| RN-02015 | Resetear no exige que la cuenta esté habilitada | — |
| RN-02016 | Habilitar una cuenta produce su contraseña provisoria | INV-09 |

### 7.2 Por qué seis reglas no tienen invariante

No es un olvido y no hay que «completarlas». El motivo está declarado en `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain y es el criterio que se reusa cada vez que aparece una regla nueva:

- **RN-02007, RN-02008 y RN-02009 describen comportamientos**, no condiciones permanentes sobre el estado. «La baja arrastra los trabajos», «el texto no se reescribe» y «el error indica dónde está» son cosas que el sistema **hace** en un momento; no son afirmaciones que tengan que ser verdaderas siempre sobre cualquier dato guardado.
- **RN-02011 es una regla de alcance de consulta.** Restringe qué devuelve un listado, y un listado no es un dato: no hay ningún estado que la regla vuelva imposible.
- **RN-02014 describe cómo se produce un valor**, y además **no se ejerce en este proyecto de código**: la contraseña provisoria llega al dominio ya derivada, de modo que acá no hay con qué comprobar que no sea adivinable ni que no se repita. Se ejerce donde el valor nace, en `GeometriaFactory-Application` CU-02011 y en `GeometriaFactory-Contracts` CU-02008.
- **RN-02016 sí tiene invariante, y es INV-09.** No enuncia un comportamiento: enuncia una condición sobre los datos —ninguna cuenta de alumno `Habilitado` sin credencial derivada, y ninguna habilitación sin la marca puesta—. Lo que agrega a INV-09 no es una mitad nueva de la condición sino un **segundo origen** de la marca, junto al reseteo.
- **RN-02015 enuncia la ausencia de una precondición.** «Resetear no exige cuenta habilitada» no vuelve imposible ningún estado: retira una comprobación. Un invariante que la expresara tendría que afirmar algo, y no hay nada que afirmar.

Dos precisiones de ubicación, que evitan que alguien busque en la capa equivocada:

- **INV-01 es del sistema y el dominio no lo puede verificar solo.** La unicidad se afirma sobre el conjunto de alumnos y una entidad no conoce a ese conjunto. Por eso el dominio la **exige declarada** —y rechaza con `UNICIDAD_DE_CORREO_NO_VERIFICADA` si no lo está— mientras quien la ejerce efectivamente es `GeometriaFactory-Application` con su puerto de repositorio.
- **INV-06 se cumple aunque el acceso se materialice afuera.** El dominio modela la condición; el mecanismo que emite el acceso vive en `GeometriaFactory-Infrastructure` y en `GeometriaFactory-Api`. Es la frontera de autenticación de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3.

### 7.3 El procedimiento de decisión

Ante una regla nueva, en este orden:

1. **¿Es una afirmación que tiene que ser verdadera siempre sobre el estado de una entidad, sin importar la operación ni quién la ejecute?**
   Si la respuesta es no, no es invariante: va como comportamiento en la capa que lo ejecuta, o como alcance de consulta en la capa que consulta. Termina acá.
2. **Si es sí: ¿la condición se afirma sobre una entidad, o sobre un conjunto de entidades?**
   Sobre una entidad, va como **guarda de esa entidad** en este proyecto de código, con su código de condición y su entrada en el catálogo.
   Sobre un conjunto, el dominio **no la puede verificar solo**: se modela como exigencia declarada —el consumidor afirma haberla resuelto y el dominio rechaza si no lo declara, como INV-01— y quien la ejerce es la capa de aplicación.
3. **¿La regla condiciona un mecanismo que el dominio no implementa** —acceso, persistencia, transporte, serialización, cálculo del valor derivado, interpretación del texto—?
   Entonces acá va **la condición** y afuera va el mecanismo, como INV-06. Nunca las dos cosas en el mismo lugar, y nunca el mecanismo acá.
4. **Si la regla entra:** sube el enunciado a `02-Especificacion-Funcional/`, obtiene su identificador `RN-XX` y su `INV-XX` si corresponde, aparece en el §6 de algún caso de uso como código de condición, y **recién entonces** entra al catálogo de [`DX-Error-Messages.md`](DX-Error-Messages.md). El catálogo no inventa condiciones: las deriva.

La tabla de fronteras de `Definicion-Modelo-De-Dominio.md` §7 es la referencia rápida del paso 3: enumera las ocho responsabilidades que este proyecto de código no tiene y dónde vive cada una.
