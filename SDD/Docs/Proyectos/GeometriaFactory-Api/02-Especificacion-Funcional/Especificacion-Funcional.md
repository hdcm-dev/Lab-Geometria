# Especificación funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Especificacion-Funcional.md
**Versión:** 1.5
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `rest-api`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md`; `01-Necesidades-Negocio/Necesidades-Negocio.md` y las necesidades NB-01 a NB-09; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26**, §17.5 íntegro —en particular §17.5.P.2, §17.5.P.3, §17.5.P.4, §17.5.P.5, §17.5.P.6, §17.5.P.8, §17.5.P.10, §17.5.P.11 y §17.5.P.12—, §4 (**F-04** precisada), §4.1 (**las dieciséis reglas**, con **RN-16** nueva del intake 1.13), §4.2, §7 (CL-2, CL-5, CL-8), §9 (X-9), §10, §11 (RN-B5), §13, §14 (**RA-01, RA-02 y RA-03**), §15, §16.1, §18 (S-2), §20 y §21; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** §1, §2, §3 y §5; `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/` completo, cuyos ocho contratos de uso y cuyo conjunto cerrado de **diecisiete** códigos esta capa transporta y traduce; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` completo, cuyos **once** casos de uso esta capa orquesta; `Proyectos/GeometriaFactory-Infrastructure/02-Especificacion-Funcional/` completo, cuyos adaptadores esta capa conecta
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Alcance funcional de este proyecto de código](#1-alcance-funcional-de-este-proyecto-de-código)
- [2. Documentos de esta categoría](#2-documentos-de-esta-categoría)
- [3. Las cinco responsabilidades de esta capa](#3-las-cinco-responsabilidades-de-esta-capa)
- [4. Lo que esta capa decide y lo que sólo transporta](#4-lo-que-esta-capa-decide-y-lo-que-sólo-transporta)
- [5. Catálogo de casos de uso](#5-catálogo-de-casos-de-uso)
- [6. Reglas de negocio que esta capa hace cumplir](#6-reglas-de-negocio-que-esta-capa-hace-cumplir)
- [7. Matriz NB → CU → RN → US](#7-matriz-nb--cu--rn--us)
  - [7.1 Matriz](#71-matriz)
  - [7.2 Cobertura bidireccional](#72-cobertura-bidireccional)
  - [7.3 Historias de usuario previstas](#73-historias-de-usuario-previstas)
  - [7.4 Casos de uso de la capa de aplicación orquestados](#74-casos-de-uso-de-la-capa-de-aplicación-orquestados)
- [8. Criterio de recorte aplicado](#8-criterio-de-recorte-aplicado)
- [9. Omisiones declaradas](#9-omisiones-declaradas)
- [10. Numeración y nombres de archivo](#10-numeración-y-nombres-de-archivo)
- [11. Puntos abiertos](#11-puntos-abiertos)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Alcance funcional de este proyecto de código

`GeometriaFactory-Api` es el **proyecto de código principal** del producto —así lo declaran el intake §13 y el `PRODUCT-MANIFEST` §1— y es donde el producto **se vuelve alcanzable**. Es el único de los siete que ensambla a los demás: depende de `GeometriaFactory-Application`, de `GeometriaFactory-Infrastructure` y de `GeometriaFactory-Contracts`, y es el **nivel 3**, el último, del orden topológico. Nadie depende de él por compilación; lo alcanza `GeometriaFactory-Web` por HTTP, en tiempo de ejecución.

Su forma es la del **host delgado** que el intake §17.5.P.2 declara: puntos de acceso que traducen petición a caso de uso y resultado a tipo de transferencia, más la composición de raíz que conecta los puertos con sus adaptadores. El actor primario de los **doce** casos de uso es el código de `GeometriaFactory-Web`, servidor a servidor; el alumno y el administrador aparecen como sujetos de las reglas y nunca como actores, porque **el navegador nunca alcanza esta superficie** (RA-01).

Cuatro rasgos distinguen a esta capa de las tres que ensambla, y los cuatro recorren sus casos de uso:

1. **Acá está la frontera del proceso.** Todo lo que las tres capas de adentro resuelven con referencias de proyecto de código —tipos, motivos, excepciones— acá tiene que convertirse en algo que viaje por un protocolo y sobreviva a un salto de red. Es el único lugar del backend donde un dato puede alterarse por codificación, por serialización o por un intermediario.
2. **Acá se traduce, y traducir es decidir.** Un motivo de la capa de aplicación no es un código de respuesta; un código del contrato no es un número de protocolo. Las dos traducciones son de esta capa y **ninguna otra las puede reparar**: si acá se elige mal, la regla se rompe hacia afuera sin que ninguna capa de adentro se entere. El caso más caro está en RN-03, y §6 lo declara.
3. **Acá vive la única puerta.** El intake §17.5.P.9 declara que un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio. Todo lo que este proyecto de código no exponga, no existe para nadie de afuera.
4. **Acá se aplica RA-03 en el único lugar donde se puede violar hacia afuera.** Las capas de adentro producen códigos y motivos; ninguno de ellos llega solo a una persona. Lo que llega es lo que esta capa emite, y por eso **ningún mensaje de esta superficie incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma**.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización por pertenencia y la verificación de facultad, en `GeometriaFactory-Application`; la interpretación del texto, el guardado, la derivación de credenciales y la emisión del acceso firmado, en `GeometriaFactory-Infrastructure`; los tipos que cruzan la frontera, en `GeometriaFactory-Contracts`; las páginas, el dibujo y todo lo que una persona ve, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) | Documento de concepto central: los **quince** puntos de acceso, los **diez** códigos de respuesta, las dos traducciones y qué está declarado por una fuente y qué es derivación de esta categoría |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Doce casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

## 3. Las cinco responsabilidades de esta capa

Salen de §17.5 del intake y no se amplían acá. Cada una tiene su caso de uso o su grupo de casos de uso, y ninguna queda sin contrato.

| Responsabilidad | Qué significa | CU |
| --- | --- | --- |
| **Superficie de acceso** | Los puntos de acceso con su verbo y sus códigos de respuesta, sobre los tipos de `GeometriaFactory-Contracts` | CU-01, CU-03, CU-04, CU-05, CU-06, CU-07, CU-08 |
| **Admisión de la petición** | Verificar el acceso firmado, exigir el papel que cada punto declara y aplicar la guardia del cambio de contraseña pendiente | CU-02 |
| **Traducción a protocolo** | Convertir el motivo de la capa de aplicación en código del contrato, y el código del contrato en código de respuesta | CU-09 |
| **Composición de la aplicación** | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | CU-10 |
| **Arranque y salud** | Aplicar las transformaciones de esquema al arrancar y responder por el estado del servicio | CU-11 |

Y una sexta cosa que no es una responsabilidad de tiempo de ejecución y por eso se lista aparte: **la colección de peticiones reproducible** (CU-12), que el intake §16.1 y §18 declaran como la forma de demostración de este tipo de proyecto de código.

**El alcance de la unidad de trabajo llega decidido**: la capa de aplicación declara un caso de uso, una unidad de trabajo, y esta capa no abre ninguna por su cuenta. Una petición ejerce a lo sumo un caso de uso.

## 4. Lo que esta capa decide y lo que sólo transporta

Es la frontera que hay que dejar imposible de confundir, porque **acá es donde una decisión ya tomada puede deshacerse sin que nadie lo note**.

**Enunciado en una línea: esta capa decide cómo se dice, y no decide qué se dice.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Qué punto de acceso existe, con qué verbo y con qué código de respuesta | **Sí** (CU-01 y CU-03 a CU-08) | — |
| Verificar la firma y la expiración del acceso, y exigir el papel del punto | **Sí** (CU-02). El mecanismo de verificación es de `GeometriaFactory-Infrastructure`; **exigirlo en cada punto es de acá** | — |
| Aplicar la guardia del cambio de contraseña pendiente sobre todo punto salvo uno | **Sí** (CU-02). La comprobación es de la capa de aplicación; **que ningún punto la saltee es de acá** | — |
| Elegir el código de respuesta de cada motivo, y en particular **no distinguir el recurso ajeno del inexistente** | **Sí** (CU-09). Es la traducción que RN-03 exige por escrito | — |
| Conectar cada puerto con su adaptador y tomar de configuración la ubicación del almacén y la clave de firma | **Sí** (CU-10) | — |
| Aplicar las transformaciones de esquema al arrancar | **Sí** (CU-11), como **disparo**. La transformación la ejecuta el adaptador | `GeometriaFactory-Infrastructure` |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar la pertenencia de un trabajo o la facultad de administrador **sobre el dato** | **No.** Lo que acá se exige es el papel declarado en el acceso; la comprobación sobre el dato recuperado es de la capa de aplicación, y **el papel no la reemplaza** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Llega en el resultado y se transporta en una respuesta exitosa | `GeometriaFactory-Domain` |
| Interpretar el texto del alumno o verificar sus valores | **No.** El texto viaja como cadena y **no se normaliza en el borde** | `GeometriaFactory-Infrastructure` |
| Declarar qué campos cruzan la frontera | **No.** Los tipos son del ensamblado de contratos, y **esta capa no agrega ni recorta campos** | `GeometriaFactory-Contracts` |
| Presentar el estado degradado a una persona | **No.** Acá termina en un código de respuesta | `GeometriaFactory-Web` |

Seis precisiones que rigen en toda la categoría:

1. **Exigir el papel no es autorizar.** El papel viaja en el acceso firmado y esta capa lo exige por punto; la verificación de pertenencia y la de facultad se hacen sobre el dato recuperado y son de la capa de aplicación. Que un punto exija `Administrador` no exime a la capa de adentro de comprobar, y **duplicar la comprobación acá crearía un segundo lugar donde la regla puede decir otra cosa**.
2. **La guardia del cambio de contraseña pendiente tiene una sola excepción declarada**: el cambio de la propia contraseña. La comprobación es de la capa de aplicación; lo que esta capa aporta es que **ningún punto de acceso quede fuera de ella**, que es la parte que se rompe agregando un punto nuevo y olvidándose.
3. **Ningún mensaje de esta superficie incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma.** Es RA-03, que es regla de nivel producto, y su contracara es que **todo error que se responda queda registrado del lado del servidor**, junto con todo intento de acceso rechazado.
4. **Esta superficie no tiene ningún cliente legítimo que no sea `GeometriaFactory-Web`.** Es RA-01. De ahí salen tres ausencias que no son olvidos y se declaran: **no hay CORS**, **no hay WebSockets** y **no hay ningún punto de acceso pensado para que lo invoque un navegador**.
5. **RA-02 no tiene tramo acá, y se declara.** El visor es un visualizador puro, sin red, sin configuración y sin identidad; esta capa **no compone su bundle, no lo sirve y no lo configura**. Su contribución a RA-02 es negativa y estructural: al no existir ningún punto de acceso pensado para el navegador, no hay nada que el bundle pudiera llamar aunque quisiera. No tener tramo no es incumplirla.
6. **Sin estado.** El intake §17.5.P.3 y §17.5.P.11 declaran REST sin estado y sin sesiones persistentes: lo que se parece a una sesión vive en el circuito de la pieza pública. Ningún punto de acceso de esta superficie depende de lo que ocurrió en la petición anterior.

## 5. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-01 | [Canjear credenciales por un acceso firmado](Casos-De-Uso/CU-01-Canjear-Credenciales-Por-Un-Acceso-Firmado.md) | El único punto declarado por una fuente, con su `401` genérico y su `403` con motivo | Propuesto |
| CU-02 | [Admitir la petición: acceso, papel y marca](Casos-De-Uso/CU-02-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md) | La guardia transversal de los **catorce** puntos restantes, y la excepción única | Propuesto |
| CU-03 | [Exponer el alta de cuenta y la credencial propia](Casos-De-Uso/CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md) | Los cuatro puntos que se ejercen sin acceso o sobre la propia cuenta | Propuesto |
| CU-04 | [Exponer el gobierno de las cuentas de la comisión](Casos-De-Uso/CU-04-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md) | Listado, cambio de situación y baja física con confirmación escrita | Propuesto |
| CU-05 | [Exponer el reseteo de la contraseña de un alumno](Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md) | El punto que devuelve la provisoria una sola vez y **no la registra** | Propuesto |
| CU-06 | [Exponer el envío y la eliminación de un trabajo](Casos-De-Uso/CU-06-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md) | La única acción de guardado, con el texto que **no se normaliza en el borde**, y la eliminación con sus dos alcances | Propuesto |
| CU-07 | [Exponer el listado y el detalle de los trabajos](Casos-De-Uso/CU-07-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Los dos puntos de lectura, con el alcance que llega decidido y la proyección que no arrastra el texto | Propuesto |
| CU-08 | [Exponer el desenlace de la revisión](Casos-De-Uso/CU-08-Exponer-El-Desenlace-De-La-Revision.md) | Aprobar o rechazar desde el estado `Pendiente`, con su terminalidad | Propuesto |
| CU-09 | [Traducir el motivo del contrato a respuesta de protocolo](Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md) | Las dos traducciones, los **dieciséis** códigos con destino y **el que no lo tiene** | Propuesto |
| CU-10 | [Componer la aplicación y conectar los puertos con sus adaptadores](Casos-De-Uso/CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md) | La composición de raíz y la configuración que el despliegue provee | Propuesto |
| CU-11 | [Arrancar el servicio y dejar el almacén en condiciones](Casos-De-Uso/CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md) | El arranque que aplica las transformaciones y **se detiene antes que atender mal** | Propuesto |
| CU-12 | [Ejercitar la superficie con la colección de peticiones reproducible](Casos-De-Uso/CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md) | La forma de demostración que el intake declara para este tipo de proyecto de código | Propuesto |

**Doce casos de uso.** El piso mínimo por tipo lo fija `Rules-Especificacion-Funcional.md` §2.2; **ese archivo no está en este repositorio** y esta categoría no lo transcribe ni lo supone. Lo que sí se declara es el techo y el criterio con el que se cortó, en §8.

## 6. Reglas de negocio que esta capa hace cumplir

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa.

**Trece de las dieciséis tienen tramo acá y tres no lo tienen**, y el recuento cierra en dieciséis. **Dos son las que esta capa puede romper hacia afuera sin que ninguna capa de adentro se entere** —**RN-03** y **RN-13**—, y por eso llevan marca propia: la primera se rompe eligiendo un código de respuesta que confirma la existencia de un recurso ajeno; la segunda, dejando un punto de acceso fuera de la guardia.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-01](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-03: el punto de configuración del administrador y su negativa cuando ya existe una. CU-02: el papel llega en el acceso y cada punto declara cuál exige |
| [RN-02](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-03: el punto de registro traduce el correo ocupado a una respuesta que **no declara la situación ni el papel** de la cuenta que lo ocupa |
| [**RN-03**](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | **Tramo de traducción, y es el que esta capa puede romper sola.** CU-06, CU-07, CU-08 y CU-09: el trabajo ajeno y el inexistente reciben **el mismo código de respuesta y el mismo texto**. La capa de aplicación declara el motivo «que el consumidor traduce a “no encontrado” y nunca a “no autorizado”»; el consumidor es esta capa |
| [RN-04](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-06: los dos alcances sobre el mismo punto. **Es la única regla del producto con un criterio de verificación que exige forzar la petición contra esta superficie**, declarado en el intake §17.5.P.6 |
| [RN-05](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | **Sin tramo acá.** El estado llega decidido por el dominio y viaja en una respuesta **exitosa**: un envío cuyo texto no verifica **no es un fallo de protocolo**. Es la confusión más cara de esta capa y CU-06 la declara |
| [RN-06](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | CU-01: la respuesta **con motivo** que el intake §17.5.P.5 declara, distinta de la respuesta genérica de credenciales inválidas |
| [RN-07](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-04: el punto de baja **transporta el correo escrito como confirmación** y no procede sin él. La comparación y el arrastre son de las capas de adentro |
| [RN-08](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | CU-06: **el borde del proceso es el primer lugar donde el texto puede alterarse** —por codificación, por normalización o por recorte de tamaño— y este contrato declara que no se toca |
| [RN-09](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | CU-07 y CU-09: la ubicación del defecto **cruza la frontera sin recortarse**. Producirla es de las capas de adentro; no perderla al traducir es de acá |
| [RN-10](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | CU-08: el papel exigido en el punto y la traducción del estado que no admite desenlace, **incluido el terminal** |
| [RN-11](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-07, **de forma negativa**: la superficie **no declara ningún parámetro** con el que el administrador pueda pedir borradores. El alcance llega decidido y acá no se ofrece la puerta por la que la regla se rompería |
| [RN-12](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo conserva la cuenta y sus trabajos, y no es una baja | CU-05, y **CU-04 por contraste**: son dos puntos de acceso distintos, con verbos distintos, y el del reseteo **no toca ninguna ruta de retiro** |
| [**RN-13**](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | **Tramo transversal, y es el otro que esta capa puede romper sola.** CU-02: la guardia alcanza a **todos** los puntos que exigen acceso salvo el cambio de la propia contraseña. Un punto nuevo que quede fuera de la guardia la rompe sin que nada falle |
| [RN-14](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-14-Provisoria-Producida-Por-El-Sistema.md) | La provisoria la produce el sistema: no es adivinable y no se repite | **Sin tramo acá.** El valor llega producido y derivado desde `GeometriaFactory-Infrastructure`. Lo que CU-05 sí declara es **lo que no se hace con él**: no se registra en ninguna traza y se devuelve una sola vez |
| [RN-15](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-15-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-05 **de forma estructural**: el punto de acceso **no declara ningún parámetro de situación** y su tabla de respuestas **no tiene ninguna fila por cuenta no habilitada**, porque esa causa no existe |
| [RN-16](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-16-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria | **Sin tramo propio acá, y con dos efectos estructurales sobre esta superficie.** El primero es un **retiro**: el punto **A-04** deja de existir, porque la escritura anónima que exponía dejó de existir. El segundo es el resultado de **A-07** en CU-04, que devuelve la provisoria una sola vez. La regla la hace cumplir la capa de aplicación; lo que esta capa aporta es **no exponer ningún punto que la contradiga**, y `Definicion-Superficie-HTTP.md` §7 lo declara como ausencia sostenida |

## 7. Matriz NB → CU → RN → US

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-01](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-03, CU-04, CU-05 | RN-01, RN-02, RN-07, RN-12, RN-15 | US-05, US-06, US-11, US-12, US-13, US-14, US-15 |
| [NB-02](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-01, CU-02, CU-03, CU-05 | RN-01, RN-06, RN-13, RN-14 | US-01, US-02, US-03, US-04, US-07, US-08, US-09, US-10, US-16 |
| [NB-03](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-06, CU-07, CU-11 | RN-03, RN-04, RN-08 | US-17, US-18, US-19, US-20, US-21, US-26, US-27 |
| [NB-04](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-06, CU-09 | RN-05, RN-08, RN-09 | US-18, US-19, US-24, US-25 |
| [NB-05](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-07 (parcial) | RN-09 | US-22 |
| [NB-06](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-07 (parcial) | RN-03 | US-22 |
| [NB-07](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-07 (parcial) | RN-11 | US-21, US-22 |
| [NB-08](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | CU-09, CU-11 | — | US-25, US-28, US-29, US-30 |
| [NB-09](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-06, CU-07 (parcial), CU-08 | RN-04, RN-10, RN-11 | US-20, US-21, US-23 |

**Dos de las reglas que la columna cita —RN-05 y RN-14— son exactamente las dos que §6 declara *sin tramo en esta capa*.** Figuran igual, y conviene que se lea así: están porque el caso de uso correspondiente **declara explícitamente qué no hace con ellas** —CU-06, que un envío cuyo texto no verifica no es un fallo de protocolo; CU-05, que la provisoria no se registra y se devuelve una sola vez—, no porque acá se ejerzan. Quitarlas de la matriz escondería las dos declaraciones que evitan el defecto.

### 7.2 Cobertura bidireccional

**De NB a CU. Las nueve necesidades reciben al menos un caso de uso en este proyecto de código**, y **NB-08 lo recibe acá por primera vez con tramo propio y no parcial**: `GeometriaFactory-Application` declara explícitamente que no la toca, y `GeometriaFactory-Infrastructure` la declara parcial. Su dolor es de acceso y de despliegue, y **es acá donde el producto se vuelve alcanzable**: el punto de salud y el arranque que se detiene son de esta capa, y la respuesta que la pieza pública convierte en estado degradado explícito sale de acá.

**Nota sobre un recuento de un documento hermano.** `GeometriaFactory-Infrastructure` `Especificacion-Funcional.md` §7.2 declara ser «una de las **dos** secciones del producto» que cubren las nueve necesidades, junto con `GeometriaFactory-Web`. Esa afirmación era exacta cuando se escribió y **con esta emisión pasa a ser tres**. No se corrige desde acá: se declara, y queda listado en §11 para que la próxima intervención sobre aquel documento lo absorba.

**Tres de las nueve quedan cubiertas parcialmente**, y conviene que se lea así:

- **NB-05.** Lo que esta capa aporta es que la observación con su severidad y su par de valores **cruce la frontera sin recortarse**. Que el alumno la vea, y cómo, es de `GeometriaFactory-Web`.
- **NB-06.** Lo que aporta es que las piezas, sus componentes y el texto original lleguen al otro lado del proceso. El dibujo es de `GeometriaFactory-Visor` y el árbol y la sincronización son de `GeometriaFactory-Web`.
- **NB-07.** Lo que aporta es un único punto de listado cuyo alcance llega decidido y que **no ofrece ningún parámetro para pedir borradores ajenos**. La agrupación, el orden y el filtro tal como la persona los ejerce son de `GeometriaFactory-Web`.

**De CU a NB. Diez de los doce casos de uso trazan al menos a una necesidad de negocio, y dos no trazan a ninguna**, lo cual se declara en vez de forzarles una:

| CU | NB que implementa |
| --- | --- |
| CU-01 | NB-02 |
| CU-02 | NB-02 |
| CU-03 | NB-01, NB-02 |
| CU-04 | NB-01 |
| CU-05 | NB-01, NB-02 |
| CU-06 | NB-03, NB-04, NB-09 |
| CU-07 | NB-03, NB-05 (parcial), NB-06 (parcial), NB-07 (parcial), NB-09 (parcial) |
| CU-08 | NB-09 |
| CU-09 | NB-04, NB-08 |
| **CU-10** | **Ninguna.** Ver abajo |
| CU-11 | NB-03, NB-08 |
| **CU-12** | **Ninguna.** Ver abajo |

**CU-10 no traza a ninguna necesidad de negocio, y es correcto que no lo haga.** Conectar un puerto con su adaptador es construcción, no capacidad: ninguna necesidad la pide y nadie la percibe. Inventarle una traza haría creer que hay una necesidad de negocio detrás de una decisión de estructura. Su valor se mide en que **todo lo demás sea probable con dobles**, que es lo que las tres capas de adentro dan por sentado.

**CU-12 tampoco, y por un motivo distinto: no implementa nada, demuestra.** La colección de peticiones ejercita capacidades que otros casos de uso ya implementan; asignarle las necesidades de esas capacidades las contaría dos veces. Lo que sí tiene es una obligación propia y verificable: **reproducirse en cinco pasos o menos y no inventar ningún texto de prueba**.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-01 | Canjear correo y contraseña por un acceso firmado con sus cuatro reclamos | CU-01 |
| US-02 | Responder credenciales inválidas **sin declarar cuál de los dos campos falló** | CU-01 |
| US-03 | Responder con motivo a la cuenta `Pendiente` o `Bloqueado` | CU-01 |
| US-04 | Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde | CU-02 |
| US-05 | Exigir el papel declarado por cada punto de acceso | CU-02 |
| US-06 | Aplicar la guardia del cambio de contraseña pendiente a todos los puntos salvo uno | CU-02 |
| US-07 | Registrar una cuenta de alumno sin campo de contraseña | CU-03 |
| US-08 | Configurar la cuenta de administrador sólo mientras no exista ninguna | CU-03 |
| US-09 | Cambiar la contraseña propia con la provisoria como vigente, que es el camino del primer ingreso y el del cambio posterior a un reseteo | CU-03 |
| US-10 | Cambiar la contraseña propia exigiendo la vigente | CU-03 |
| US-11 | Listar las cuentas de la comisión con su situación y su marca | CU-04 |
| US-12 | Cambiar la situación de una cuenta con verificación de papel | CU-04 |
| US-13 | Dar de baja una cuenta transportando el correo escrito como confirmación | CU-04 |
| US-14 | Resetear la contraseña de un alumno y devolver la provisoria **una sola vez** | CU-05 |
| US-15 | No exigir ni comprobar la situación de la cuenta al resetear | CU-05 |
| US-16 | No registrar la provisoria en ninguna traza | CU-05 |
| US-17 | Enviar un trabajo nuevo y recibir el estado que la interpretación decidió | CU-06 |
| US-18 | Reenviar un trabajo en `Borrador` con el texto que la persona volvió a pegar | CU-06 |
| US-19 | Transportar el texto original **sin normalizarlo en el borde** | CU-06 |
| US-20 | Eliminar un trabajo con los dos alcances, verificado **forzando la petición** | CU-06 |
| US-21 | Listar trabajos con el alcance ya decidido y sin parámetro para pedir borradores ajenos | CU-07 |
| US-22 | Devolver el detalle con piezas, componentes, observaciones y comentario | CU-07 |
| US-23 | Aprobar o rechazar un trabajo en estado `Pendiente`, con comentario opcional | CU-08 |
| US-24 | Traducir cada código del contrato al código de respuesta que le corresponde | CU-09 |
| US-25 | Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor | CU-09 |
| US-26 | Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee | CU-10 |
| US-27 | Aplicar las transformaciones de esquema al arrancar, sobre almacén inexistente | CU-11 |
| US-28 | Detener el arranque en lugar de atender peticiones sobre un almacén en el que no se puede confiar | CU-11 |
| US-29 | Responder por el estado del servicio en un punto que no exige acceso | CU-11 |
| US-30 | Ejercitar la superficie con una colección reproducible en cinco pasos o menos | CU-12 |

**Treinta historias previstas, US-01 a US-30, sin huecos.**

### 7.4 Casos de uso de la capa de aplicación orquestados

Los **once** casos de uso de `GeometriaFactory-Application` quedan orquestados por los de esta capa. Ninguno queda sin orquestar, y la correspondencia **no se lee por número**.

| CU de esta capa | CU de la capa de aplicación que orquesta |
| --- | --- |
| CU-01 | [CU-03](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md), en su consulta de admisibilidad |
| CU-02 | Ninguno. **Es la única guardia de esta capa que no invoca un caso de uso**: verifica el acceso con el mecanismo de `GeometriaFactory-Infrastructure` y exige el papel que el acceso declara |
| CU-03 | [CU-01](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Una-Cuenta.md), [CU-10](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-10-Configurar-La-Cuenta-De-Administrador.md), CU-03 en su fijación y su reemplazo |
| CU-04 | [CU-02](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-02-Gobernar-Las-Cuentas-De-La-Comision.md) |
| CU-05 | [CU-11](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-11-Resetear-La-Contrasena-De-Un-Alumno.md) |
| CU-06 | [CU-04](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-04-Cargar-Y-Reeditar-Un-Trabajo-Propio.md), [CU-05](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md), [CU-09](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-09-Eliminar-Un-Trabajo.md) |
| CU-07 | [CU-06](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-Los-Trabajos-Propios-Del-Alumno.md), [CU-07](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-07-Revisar-Los-Trabajos-De-La-Comision.md) |
| CU-08 | [CU-08](../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-08-Dar-Desenlace-A-Un-Trabajo.md) |
| CU-09 | Ninguno: traduce lo que los demás devuelven |
| CU-10 | Ninguno: los construye |
| CU-11 | Ninguno: invoca la preparación del almacén de `GeometriaFactory-Infrastructure` `CU-10`, que **no es un caso de uso de la capa de aplicación** |
| CU-12 | Ninguno directamente: ejercita la superficie, y por ella todos |

**Cuatro de los doce no orquestan ningún caso de uso de la capa de aplicación, y es correcto**: son la guardia, la traducción, la composición y el arranque, que son lo propio de un host y no de una capa de casos de uso. Es la contracara exacta del «host delgado» del intake §17.5.P.2: **lo que esta capa agrega es cableado, y el cableado no orquesta, conecta**.

## 8. Criterio de recorte aplicado

- **Piso y techo.** El piso por tipo lo fija la regla de la categoría y **no se transcribe acá porque el archivo de reglas no está en este repositorio**; no se lo supone ni se lo redondea. El techo lo da la cobertura de las cinco responsabilidades de §3 más la demostración: quedaron **doce**.
- **Particiones.** **La admisión se separó de los puntos de acceso** —CU-02 frente a los siete restantes— porque es una condición de **todos** ellos y su defecto característico es de omisión: se rompe cuando un punto nuevo queda afuera, y eso no se detecta leyendo el punto sino comparándolo contra la guardia. **La traducción se separó de todo** —CU-09— porque su unidad de verificación es el conjunto cerrado de códigos del contrato y no un punto de acceso: se prueba recorriendo los quince, no ejerciendo una ruta. **La composición se separó del arranque** —CU-10 frente a CU-11— porque terminan distinto: la primera falla en construcción, y la segunda **detiene el servicio**, que es una forma de terminación que ninguna otra parte de esta capa tiene. **El reseteo se separó del gobierno de cuentas** —CU-05 frente a CU-04— por el mismo fundamento con el que lo separaron las dos capas de adentro: uno **conserva** la cuenta y sus trabajos y el otro los **elimina**, y ponerlos en el mismo contrato es exactamente la confusión que la capacidad del reseteo vino a cerrar.
- **Fusiones.** El envío y la eliminación quedaron juntos en CU-06 porque son las dos escrituras que el alumno ejerce sobre su propio trabajo y comparten la comprobación que las acota; el listado y el detalle quedaron juntos en CU-07 porque son los dos puntos de lectura y se distinguen sólo por la forma del resultado, no por su admisión. **Los cuatro puntos de la credencial propia y del alta quedaron en CU-03** porque comparten un rasgo que ninguno de los demás tiene y que es lo que hay que poder verificar de una vez: **son los únicos que se ejercen sin acceso firmado o sin que el papel importe**. **Aprobar y rechazar quedaron en CU-08**, que es la misma fusión que el ensamblado de contratos ya justificó: se distinguen por el valor de un campo de conjunto cerrado.
- **Lo que no se convirtió en caso de uso.** El registro del lado del servidor no recibió contrato propio: es una propiedad transversal que §4 declara una vez y que cada caso de uso ejerce. Tampoco lo recibieron **la ausencia de CORS y la ausencia de WebSockets**, que no son comportamientos sino ausencias declaradas de RA-01, ni **la pasarela de reenvío** del front, que el intake §9 X-9 declara especificada y **no implementada**. Y no lo recibió el despliegue: el intake §17.5.P.8 lo declara manual y a cargo del docente, y su lugar es `09-Devops`.

## 9. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`**, las dieciséis con archivo propio allá, y acá se **referencian** por identificador y con enlace. §6 declara, regla por regla, dónde se ejerce cada una en esta capa. Es el mismo criterio que aplican `GeometriaFactory-Application` §9 y `GeometriaFactory-Infrastructure` §9 |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Omitidos** | El flag `tiene_persistencia` vale **true** en este proyecto de código y en `GeometriaFactory-Infrastructure`, y el `PRODUCT-MANIFEST` §5 declara por qué: acá vale porque **toma de configuración la ruta del archivo y dispara las transformaciones al arrancar**, no porque modele el dato. El intake §17.5.P.4 lo dice en una línea: «delega en `GeometriaFactory.Infrastructure`». El modelo conceptual del producto **ya está emitido**, en `GeometriaFactory-Infrastructure/02-Especificacion-Funcional/Modelo-Datos/`, con sus cinco entidades y sus siete reglas conceptuales; redactarlo de nuevo acá crearía dos descripciones del mismo dato guardado. Lo que sí se documenta acá es lo que esta capa hace con él, en CU-10 y CU-11 |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es la **superficie HTTP** | No es una elección de gusto: es lo único de este proyecto de código que existe hacia afuera, es lo que la pieza pública consume y es donde se decide qué se puede romper sin que ninguna capa de adentro se entere. Un lector que abra los doce casos de uso sin haber visto la superficie entera no puede saber si falta un punto de acceso |
| `_legacy/` | **No existe** | Es la emisión inicial de la categoría para este proyecto de código: no hay ninguna versión superada que archivar |

## 10. Numeración y nombres de archivo

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-05` de esta categoría no es el `CU-05` de `GeometriaFactory-Application`, ni el de `GeometriaFactory-Contracts`, ni el de `GeometriaFactory-Infrastructure`. La correspondencia se lee por §3, por la matriz de §7.1 y por la tabla de §7.4, **nunca por número**.
2. **La serie es contigua de CU-01 a CU-12**, sin huecos, y su orden es el del recorrido de una petición: primero cómo se obtiene el acceso, después cómo se admite, después qué puntos existen, después cómo se traduce lo que sale, y al final cómo se construye, cómo se arranca y cómo se demuestra.
3. **Los `A-XX` son los puntos de acceso** que [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §3 enumera, y son propios de esta categoría. **No son casos de uso**: un caso de uso puede describir más de un punto de acceso, y §3 de aquel documento declara la correspondencia.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que aquel proyecto de código declaró.
5. **Los códigos `CONTRATO_*` son del ensamblado de contratos** y se citan con su identificador literal, sin renombrarlos y sin traducirlos. Esta categoría **no agrega ninguno**.
6. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las que previó `01-Necesidades-Negocio` ni la de los proyectos de código hermanos.
7. **Los `E-X` son los escenarios del intake §20** y se citan con su identificador de origen, sin renumerar. **Ningún dato de prueba se inventó**: es la regla de delivery del producto que lo prohíbe.

## 11. Puntos abiertos

**Once filas, y ninguna bloqueante. Seis son propias de esta categoría y cinco vienen declaradas de aguas arriba y no se reabren.** De las once, **cuatro están cerradas** —la del establecimiento de la contraseña, las **dos** que eran huecos de la superficie y la del alcance de la colección de peticiones—, y **siete siguen abiertas**. Las tres que cierra `PRODUCT-INTAKE` **1.29** conservan su fila con el desenlace y la fecha, en lugar de retirarse. Eran doce y tres respectivamente en la emisión 1.0.

**Tres cierres del 2026-08-12, por decisión del Product Owner en `PRODUCT-INTAKE` 1.29.** Los **dos huecos de la superficie** que esta categoría encontró y no resolvió —qué código recibe una operación de administrador pedida por quien no lo es fuera del desenlace, y qué código recibe un envío o una reedición forzados fuera de `Borrador`— quedaron cerrados con **dos códigos nuevos del conjunto cerrado**, `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` (§17.4 P.3), que `GeometriaFactory-Contracts` emite y que `Definicion-Superficie-HTTP.md` §6 traduce a `403` y a `409`. **Esta categoría no inventó ninguno de los dos**, que era la condición con la que los declaró abiertos. Y el **alcance de la colección de peticiones** quedó cerrado a favor de **los ocho escenarios `E-1` a `E-8`** (§18), que es exactamente la lectura que esta categoría ya había adoptado: **no cambia ningún artefacto**.

**Cerrado antes, y es el que encabezaba esta tabla: cómo se identifica la cuenta al establecer la contraseña del primer ingreso.** Era el más importante de los propios: la única escritura **de contraseña** de la superficie que ocurría **sin acceso firmado**, con la solicitud de establecimiento declarando «la contraseña elegida» y ninguna fuente declarando cómo viajaba la identidad. **Lo resolvió el Product Owner en `PRODUCT-INTAKE` 1.13 §4.1 con la regla RN-16**, y no por ninguna de las dos salidas que esta categoría había anticipado —punto anónimo con prueba de posesión, o acceso de alcance acotado— sino **suprimiendo la operación**: habilitar produce una contraseña provisoria, el administrador se la comunica en persona y la cuenta cambia la suya por **A-05**, autenticada. La fila de control de cambios 1.13 del intake registra que fue la emisión de este proyecto de código la que levantó el hueco. Su rastro vive hoy en `CU-03` §10, en `Definicion-Superficie-HTTP.md` §9 y en la ausencia declarada de §7 de ese mismo documento.

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| ~~**Cómo se identifica la cuenta al establecer la contraseña del primer ingreso**~~ | **CERRADO por `PRODUCT-INTAKE` 1.13 §4.1 (RN-16)**, ver la prosa de arriba. Enunciado original: es la única escritura de la superficie que ocurre **sin acceso firmado**, porque la persona todavía no puede obtener uno: el ensamblado de contratos declara la solicitud de establecimiento con «la contraseña elegida» y **no declara cómo viaja la identidad de la cuenta**. Un punto de acceso anónimo que acepte correo y contraseña nueva permitiría fijarle la contraseña a cualquier cuenta habilitada que todavía no la tenga. `CU-03` §10 deja escritas las dos salidas —transportar también la identidad con alguna prueba de posesión, o emitir un acceso de alcance acotado— y **no elige**, porque es una decisión de seguridad y no de forma | **Product Owner**, y `05-Arquitectura-Tecnica` |
| ~~**Qué código del contrato recibe una operación de administrador pedida por un alumno**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §17.4 P.3 (2026-08-12).** El Product Owner incorporó al conjunto cerrado `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR`, que cubre el rechazo por papel **fuera del desenlace** —gobierno de cuentas, listado de la comisión y reseteo—; `GeometriaFactory-Contracts` lo emite en su `Contratos-Abstractions.md` §5.1 y `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` **no cambia de enunciado**. Su fila de traducción está en `Definicion-Superficie-HTTP.md` §6, con destino `403` | **Cerrado**, sin acción pendiente |
| ~~**Qué código del contrato recibe un envío o una reedición forzados fuera de `Borrador`**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §17.4 P.3 (2026-08-12).** El Product Owner incorporó `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR`, que cubre el envío y la reedición sobre un trabajo en `Pendiente`, `Finalizado` o `Rechazado`; `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **sigue acotado a la eliminación**. Su fila de traducción está en `Definicion-Superficie-HTTP.md` §6, con destino `409` | **Cerrado**, sin acción pendiente |
| **Las rutas y los verbos definitivos** | **Propio.** Las **dos** únicas cosas que una fuente declara de la superficie son el punto de canje de credenciales y la existencia de un punto de salud, cuya ruta la fuente **no da**. Las **quince** filas de `Definicion-Superficie-HTTP.md` §3 son una **propuesta derivada** de esta categoría, rotulada como tal fila por fila, y su forma definitiva se fija en 05 y se valida en el punto de control de la primera etapa | `05-Arquitectura-Tecnica` |
| **Qué código de respuesta corresponde a una terminación degradada del almacén** | **Propio.** `GeometriaFactory-Infrastructure` declara terminaciones degradadas que **no tienen código propio en el conjunto cerrado del contrato**, y el único que las podría transportar es el genérico. `CU-09` §6 adopta un código de respuesta para ellas **y lo declara como derivación**, distinguiéndolo del que corresponde a un defecto interno | `05-Arquitectura-Tecnica`, y Product Owner si quisiera un código de contrato propio |
| ~~**El alcance de la colección de peticiones**~~ | **CERRADO por `PRODUCT-INTAKE` 1.29 §18 (2026-08-12).** El Product Owner resolvió la divergencia **a favor de los ocho escenarios `E-1` a `E-8`**: §18 `S-2` pasa a decir lo mismo que §16.1 ya decía. La lectura que esta categoría había adoptado —los ocho, por `E-8`— **queda confirmada y no cambia ningún artefacto de la categoría 02** | **Cerrado**, sin acción pendiente |
| **Vigencia exacta del acceso firmado** | **Heredado.** El intake declara «corta» y «sin token de refresco», y no fija un número. Es el mismo punto que `GeometriaFactory-Infrastructure` §11 declara abierto, y esta categoría **no lo reabre ni lo resuelve**: lo hereda como condición de su guardia | `05-Arquitectura-Tecnica`, y Product Owner si quisiera fijarlo |
| **Límite de tamaño del cuerpo de una petición** | **Propio.** Ninguna fuente lo declara, y acá se vuelve visible por segunda vez: `GeometriaFactory-Infrastructure` §11 lo declara abierto para el texto que interpreta, y en el borde del proceso el mismo hueco reaparece como límite de cuerpo. **Un límite mal elegido rompe RN-08 en silencio**, truncando el texto de un alumno | **Product Owner**, y `05-Arquitectura-Tecnica` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la primera etapa. **No es ambigüedad de esta categoría** | `05-Arquitectura-Tecnica` |
| Versiones exactas de los paquetes | El intake §17.5.P.11 lo declara abierto y lo ancla en la primera etapa | `05-Arquitectura-Tecnica`, en la primera etapa |
| Construcción de la imagen en destino desde el repositorio | El intake §17.5.P.11 lo rotula **[A VERIFICAR]** y exige probarlo una vez antes de depender del mecanismo. **No es una asunción de esta categoría** | `09-Devops`, midiendo |
| Valores numéricos de los requerimientos no funcionales | La latencia, el caudal y el arranque en frío están rotulados como asunción aguas arriba. Se usan como vigentes | Product Owner, y `08-Calidad-Y-Pruebas` |

**Y uno que quedó resuelto aguas arriba y se registra para que nadie lo vuelva a abrir**: el desenlace del envío del escenario **E-8**, que el `PRODUCT-INTAKE` **1.12** fija en §20.E-8 punto 5 y en la fila «Dimensión no legible» de §21 —es **error de validación**, el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo—. Para esta capa la consecuencia es directa y está en `CU-06`: **ese envío responde con éxito**, porque el trabajo se guardó y su estado se decidió; lo que no verifica es el texto, no la petición.

**Un residuo de forma de un documento hermano**, que no es un punto abierto de decisión y se anota para que se absorba: la afirmación de `GeometriaFactory-Infrastructure` §7.2 sobre las «dos secciones» que cubren las nueve necesidades, descrita en §7.2 de este documento.

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial de la categoría para este proyecto de código, contra el `PRODUCT-INTAKE` **1.12** y el `PRODUCT-MANIFEST` **1.2**. Índice maestro de **doce** casos de uso, con las **cinco responsabilidades** de la capa más la demostración, la frontera entre lo que se decide y lo que se transporta con sus **seis precisiones** —entre ellas la no aplicabilidad declarada de RA-02 y las tres ausencias que RA-01 produce—, la tabla de las **quince** reglas de negocio referenciadas —**trece con tramo acá, dos sin él, y dos que esta capa puede romper hacia afuera sola**, RN-03 y RN-13—, la matriz NB → CU → RN → US con la verificación bidireccional, la declaración de que **las nueve necesidades reciben caso de uso** y de que **NB-08 recibe acá su primer tramo propio**, la declaración de que **CU-10 y CU-12 no trazan a ninguna necesidad**, las **treinta** historias previstas, la correspondencia con los once casos de uso de la capa de aplicación y los cuatro casos de uso propios que no orquestan ninguno, el criterio de recorte con sus particiones y fusiones, las tres omisiones y el documento de concepto central con su fundamento, las siete notas de numeración y los **doce puntos abiertos**, ocho propios y cuatro heredados, con **tres huecos de la superficie elevados al Product Owner**. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13**, que incorpora la regla **RN-16** —habilitar una cuenta produce su contraseña provisoria, con el mismo mecanismo y el mismo tratamiento que el reseteo, y la deja con cambio de contraseña pendiente— y precisa la capacidad **F-04**. **Cierra el punto abierto más importante de esta categoría**, que era la identidad en el establecimiento de la contraseña del primer ingreso: la fila sale de la tabla de §11 y pasa a la prosa de cerrados con su resolución, y los puntos abiertos pasan de **doce a once**, siete propios y cuatro heredados; los huecos de superficie elevados al Product Owner pasan de tres a **dos**. **§3**: el documento de concepto central pasa a declarar **quince** puntos de acceso, y CU-02 a guardar **catorce**. **§6**: las reglas del producto pasan de quince a **dieciséis**, con **RN-16**, que entra **sin tramo propio** y con sus dos efectos estructurales —el retiro de A-04 y la provisoria en el resultado de A-07—; el reparto pasa a **trece con tramo y tres sin él**. **§7.3**: se reescribe **US-09**. **§9 y §11**: los recuentos del conjunto cerrado del ensamblado pasan de diecisiete a **quince** códigos, y los de rutas derivadas de dieciséis a quince. La cabecera cita el intake **1.13**. **Los doce casos de uso no cambian de número ni de recorte.** Sube minor. |
| 1.2 | 2026-08-10 | **Absorbe la corrección de `PRODUCT-INTAKE` 1.15 §4.1 (RN-16)**: lo que la regla elimina es la escritura anónima **de credencial** y no toda escritura anónima —el **registro de cuenta**, que esta superficie expone por **A-02**, es anónimo por diseño y debe seguir siéndolo—. **§11** acota la prosa del punto abierto cerrado: lo único de su clase era la escritura de **contraseña** sin acceso firmado. **El enunciado original conservado en la fila tachada de §11 no se toca**, porque es el registro literal de lo que la categoría había elevado. **Ningún caso de uso, punto de acceso, regla ni recuento cambia.** Sube minor. |
| 1.3 | 2026-08-10 | **Corrige el fundamento de una derivación propia, en la misma familia del hallazgo `C-05-02` (P1)** del informe de auditoría [`../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0. La auditoría levantó la cita falsa en la categoría **05** de este proyecto de código; **este documento es el origen de esa cita** y la contiene con las mismas palabras, fuera de los 76 archivos que el informe alcanzó. La fila del alcance de la colección citaba `PRODUCT-INTAKE` §16.1 como «los escenarios **E-1 a E-7** como cuerpo» y afirmaba que «ninguna de las dos se actualizó»: **las dos afirmaciones son falsas** contra el intake **1.18**, que dice «**E-1 a E-8**» y que corrigió §16.1 como uno de sus seis lugares. La divergencia **subsiste**, pero es de **alcance** —ocho escenarios contra dos— y no de envejecimiento, y así se enuncia; lo que se pide al Product Owner pasa de «actualizar» a «declarar cuál alcance rige». **La derivación no cambia: `CU-12` sigue adoptando los ocho, con el mismo fundamento.** Sube minor. |
| 1.4 | 2026-08-11 | **Cierra los hallazgos `B-API-04` (P1), `B-API-12` (P2) y `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0. **§5**, fila `CU-09`: los códigos con destino pasan de **dieciséis** a **catorce**. **§8**, viñeta «Particiones»: la traducción se prueba recorriendo los **quince** códigos del conjunto cerrado, no los diecisiete. Los dos eran residuo del conjunto anterior a **RN-16**, y el control de cambios 1.1 declaró actualizados sólo §9 y §11. **§11**: la fila «Vigencia exacta del acceso firmado» se rotula **Heredado** —es lo que su propia celda describe y lo que declaran `Definicion-Superficie-HTTP.md` §9 y `../03-UX-UI-DX/README.md` §6— y el reparto pasa de siete propios y cuatro heredados a **seis y cinco**; el total de **once** no cambia. **Cabecera**: pasa a citar `PRODUCT-INTAKE` **1.26** y `PRODUCT-MANIFEST` **1.3**, vigentes hoy. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**, según la condición de método del informe: «diecisiete» como tamaño del conjunto cerrado no sobrevive en ningún otro lugar vivo de las categorías 02 y 03 —las demás ocurrencias son filas de control de cambios, que son registro histórico y no se tocan—; el reparto de puntos abiertos se citaba en otros **dos** lugares vivos, [`README.md`](README.md) §1 de esta categoría y `../03-UX-UI-DX/README.md` §6, y los dos se corrigen en la misma tanda. **Ninguna decisión, ningún caso de uso y ningún punto abierto cambia.** Sube minor. |
| 1.5 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Absorbe la decisión (b) del Product Owner** (`PRODUCT-INTAKE` **1.29** §18): el alcance de la colección de peticiones (`S-2`) son los **ocho escenarios `E-1` a `E-8`**, y la divergencia entre §16.1 y §18 queda resuelta a favor de los ocho. La lectura que este proyecto de código ya había adoptado **queda confirmada**: no cambia ningún paso, ningún criterio ni ningún recuento. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **9**. Sube minor. |
