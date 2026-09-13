# ADR-00009 — La API autentica personas, no aplicaciones

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-09-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05), sobre decisión del Product Owner del 2026-09-12
**Categoría:** Seguridad

---

## 1. Contexto

La entrada **4.3** del intake abrió la exposición pública de la API (`E-02`: «la API debe ser expuesta públicamente; la idea es ofrecerla para otros clientes») y, sin saber todavía quién era ese cliente, tomó **el caso general**: credenciales **por cliente** —clave de API o `client_credentials`, RFC 6749 §4.4— además del ROPC de las personas (`PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Quién la consume»; §17.1.P.5, párrafo «Autenticación de clientes externos»). La pregunta de quién es el cliente quedó como ítem diferido `D-01` («podría ser otro front, una aplicación MAUI, aún no sé»).

Ese caso general produjo una tarea técnica sin fuente: `BT-00028` —«Autenticación por cliente para terceros»— no pasó la Definition of Ready porque la decisión vivía en el intake y **no tenía ADR propia** en esta categoría (`Audit/DoR-Tramo-k-2026-09-12.md` §4, lote de detenciones). Y arrastraba a cinco tareas más por dependencia (`BT-00029`, `BT-00030`, `BT-00032`, `BT-00034`, `BT-00035`).

Lo que hacía falta no era la ADR que la detención pedía —una de autenticación de clientes externos, «análoga a `ADR-00003`»— sino **una decisión de producto que dijera si esa clase de identidad existe**. El Product Owner la tomó el 2026-09-12, y la respuesta es que no:

> «El cliente o usuario del sistema: hay dos, el administrador que recepciona los trabajos y los visa —aprobar o rechazar— y gestiona a los usuarios; y los usuarios, que en un caso particular son alumnos. No hay diferencia entre alumno y usuario general; no importa que sea alumno.»

Y, sobre un tercero que no sea del producto:

> «No hay tercero — es re simple: un administrador, y luego usuarios generales, que pueden ser cualquiera, entre estos alumnos.»

El sistema ya lo tenía así. El papel es un **conjunto cerrado de dos valores** (`src/GeometriaFactory.Domain/Values/Role.cs`: `Student = 1`, `Administrator = 2`); la credencial firmada lleva **exactamente cuatro reclamos** —identificador, correo, papel y expiración— (`PRODUCT-INTAKE` §17.1.P.5, fila «Reclamos»; `CU-00022` `CA-06`); y [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) construyó la guardia sobre esa credencial: verifica firma y expiración, exige el papel que el punto declara, aplica la guardia del cambio pendiente. **Ninguna de las tres piezas tiene lugar para una aplicación como sujeto.** Meterla habría sido inventar una tercera clase de identidad que el producto no tiene.

Motivación upstream: `PRODUCT-INTAKE` **4.4** §17.1.P.3 (filas «Quién la consume», «CORS», «Endpoint de autenticación») y §17.1.P.5 · GeometriaFactory-Api; `Audit/Mesa-2026-09-12-ciclo-2.md` §8 (`D-01`, respondida); `Audit/DoR-Tramo-k-2026-09-12.md` §4; RN-00013, INV-09.

## 2. Decisión

**La API autentica personas, no aplicaciones. Toda aplicación del producto obtiene el acceso canjeando las credenciales de una persona en `POST /auth/token`, y no existe otra clase de identidad.** Cuatro puntos:

1. **Dos identidades y nada más: administrador y usuario.** Son los dos valores del papel (`Role.cs`, «conjunto cerrado de dos valores») y son los únicos sujetos que la credencial firmada puede nombrar. **No hay claves de API ni `client_credentials`**, porque no existe una tercera clase de identidad a la que dárselas. La vía de acceso de cualquier aplicación del producto es la que el front usa hoy: `POST /auth/token` con correo y contraseña de la persona (ROPC), y la credencial resultante con sus cuatro reclamos ([`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md)).
2. **Cliente es toda aplicación propia del producto que actúa en nombre de una persona.** La premisa con la que el intake acepta ROPC —«el intermediario es el propio front del mismo sistema» (§17.1.P.5, nota de seguridad, RT §9.3)— **se extiende explícitamente a todo cliente propio**: otro front, una aplicación MAUI, un script del docente. Deja de estar atada al front Blazor. Lo que la premisa exige es que el intermediario sea del producto, no que sea uno en particular.
3. **`D-01` se cierra con esta decisión, completa.** El cliente es cualquier aplicación propia que actúe en nombre de una de las dos personas; su forma —servidor a servidor, navegador de otro origen, móvil— **no cambia la autenticación**. El Product Owner descartó modelar terceros: quien usa la API es una persona con uno de los dos papeles, a través de la aplicación que sea. **No queda ningún ítem diferido** de esta decisión.
4. **«Alumno» es la etiqueta del papel `Student` en este despliegue; el papel es «usuario».** Se declara la equivalencia y **no se renombra ningún uso**: hacerlo sería una sustitución masiva del tipo que `Vocabulario-Rules.md` §9.5 prohíbe (medido en §10: 195 ocurrencias en el intake, 183 en los casos de uso vivos de esta unidad, 43 en el ensamblado de contratos). El testimonio lo refuerza: «usuarios generales, que pueden ser cualquiera, entre estos alumnos».

**Lo que no cambia:** el riesgo `R-02` (el tramo front→API en claro si ese salto es HTTP plano) sigue aceptado como está y no se reabre; los **cuatro** puntos fuera de la guardia siguen siendo cuatro ([`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) regla 2); `RN-B5` no se toca.

## 3. Estado

**Aceptado** desde 2026-09-12, por decisión del Product Owner del mismo día (testimonio en §1). No pasa por `Propuesto`: la decisión no es de esta categoría, es de producto, y esta ADR la registra con su fundamento técnico.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Autenticar personas, y sólo personas, por `POST /auth/token` desde cualquier aplicación propia (**adoptada**) | Una sola clase de identidad, una sola credencial, una sola guardia; la superficie no crece ni un punto; el modelo coincide con el dominio (`Role.cs`) y con lo que el Product Owner describe | Cada aplicación propia maneja la contraseña de la persona en claro, como ya lo hace el front: es el mismo riesgo aceptado, extendido a más de un intermediario |
| Clave de API por cliente | Trivial de emitir y revocar; no toca contraseñas de personas | **Descartada.** Nombra a una aplicación como sujeto, y el producto no tiene ese sujeto: la credencial lleva cuatro reclamos de persona y la guardia exige un papel del conjunto cerrado de dos. Una clave sin persona detrás obligaría a inventar un papel «aplicación» o a dejar pasar peticiones sin papel, y las dos cosas rompen [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) regla 1 |
| `client_credentials` (RFC 6749 §4.4) | Estándar; separa la identidad de la aplicación de la de la persona | **Descartada.** El flujo existe para el cliente que pide acceso a recursos protegidos **bajo su propio control** (paráfrasis de RFC 6749 §4.4, primer párrafo): acá **ningún recurso es de una aplicación**, todos son de una persona con uno de dos papeles, y la verificación de pertenencia se hace sobre esa persona ([`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) regla 4). Un acceso sin persona no puede pasar esa verificación |
| Un tercer papel «aplicación» en el conjunto cerrado | Deja a la guardia intacta: un valor más en el mismo enum | **Descartada.** Rompe el conjunto cerrado de dos valores que el dominio declara y que el Product Owner confirmó («hay dos»); y no resuelve nada: un punto que exija `Administrador` o `Alumno` seguiría rechazando al tercer papel, o habría que cablear qué puede hacer una aplicación en cada uno de los quince puntos, que es reescribir la autorización entera para un sujeto que no existe |
| Dejar `D-01` abierto y esperar al primer cliente | No decide nada antes de tiempo | **Descartada por el Product Owner**: el cliente concreto no importa, porque su forma no cambia la autenticación. Mantenerlo abierto dejaba cinco tareas técnicas bloqueadas detrás de una que no había que construir |

## 5. Consecuencias positivas

1. **`BT-00028` no se construye**: pasa a `Descartada` con esta ADR como fundamento. Las cinco tareas que dependían de ella dejan de estar bloqueadas.
2. La superficie sigue teniendo **quince** puntos y la guardia **una** lista de cuatro excepciones: nada de lo que [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) hizo contable deja de serlo.
3. El límite de tasa (`BT-00029`) tiene un sujeto claro: **la persona autenticada** o, antes de autenticarse, **la dirección de origen**. No hace falta una «clave» a la que atarlo.
4. CORS (`BT-00030`) queda condicionado a lo único que lo dispara: que aparezca un cliente propio que sea JavaScript de navegador desde otro origen. La condición ya no depende de un ítem diferido.
5. El modelo queda dicho en el lenguaje del producto: dos personas, y aplicaciones que actúan por ellas.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que toda aplicación propia maneje la contraseña de una persona en claro.** Es la premisa de ROPC que el intake ya aceptaba para el front, extendida. Si alguna vez apareciera una aplicación que no sea del producto, esa aceptación no la cubre: haría falta otra decisión, porque a quien no es del producto no se le entrega la contraseña de una persona. Hoy el Product Owner descartó que exista.
2. **Se acepta no distinguir, en el registro de uso, desde qué aplicación actuó la persona.** La credencial no lleva reclamo de aplicación y esta ADR no lo agrega.
3. **Se acepta que «alumno» siga escrito donde el papel es «usuario».** La equivalencia está declarada acá y en el intake; el corpus no se reescribe.

## 7. Implementación

- **Nada que construir.** `POST /auth/token` (`A-01`), la credencial de cuatro reclamos y la guardia de [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) ya son la vía de toda aplicación propia.
- **Convención impuesta:** ningún punto de acceso admite una credencial que no nombre a una persona con uno de los dos papeles. Agregar una clase de identidad es un cambio de esta ADR y de [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), no de un punto.
- **Convención impuesta:** el límite de tasa se ata a la persona autenticada o a la dirección de origen; nunca a una clave de aplicación.
- La documentación pública (`BT-00031`) y el sample de onboarding (`BT-00034`) describen **el canje de credenciales de una persona**, no una clave de cliente.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Clases de identidad que la guardia admite | Exactamente **2** (los dos valores de `Role`) | Inspección de `Role.cs` y de la guardia de admisión |
| Puntos de acceso que aceptan una credencial sin persona (clave de API, `client_credentials`) | Exactamente **0** | Inspección de los quince puntos; `git grep -n -i -e client_credentials -e "api key" -e ApiKey -- src` sin resultado en código de producción |
| Puntos fuera de la guardia | Exactamente **4**, sin cambio | Prueba de inspección de [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) |
| Reclamos de la credencial | Exactamente **4**, sin cambio | `CU-00022` `CA-06` |
| Usos de «alumno» renombrados por esta decisión | Exactamente **0** | Recuentos de §10 antes y después |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **4.4** §17.1.P.3 (filas «Quién la consume», «CORS», «Endpoint de autenticación») y §17.1.P.5 · GeometriaFactory-Api (fila «Reclamos», nota de seguridad RT §9.3, párrafo «Autenticación de las aplicaciones propias»).
- [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md): la credencial firmada de cuatro reclamos y la guardia que esta ADR deja intactas.
- [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) `CA-06`.
- `src/GeometriaFactory.Domain/Values/Role.cs`.
- [`../../../../Audit/Mesa-2026-09-12-ciclo-2.md`](../../../../Audit/Mesa-2026-09-12-ciclo-2.md) §8 (`D-01`, respondida) y [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md) §4.
- [`BT-00028`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00028-Autenticacion-Por-Cliente-Para-Terceros-Api-Key-O-Client-Credentials.md) (descartada por esta ADR), [`BT-00029`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md), [`BT-00030`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00030-Declarar-Cors-Condicional-Al-Cliente-Que-Aparezca-D-01.md).
- RFC 6749 §4.4 (Client Credentials Grant), citada para descartarla.
- ADR relacionadas: [`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md), que `BT-00027` reescribe con la exposición pública; esta ADR no la toca.

## 10. Evidencia de las citas

Toda cita de esta ADR se verificó con un comando sobre la base `9d33d6d` (`main`) el 2026-09-12. Comando y salida:

```
$ ls SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ | grep "ADR-0000"
ADR-00001-… a ADR-00008-… (ocho archivos; 00009 es el siguiente libre)

$ grep -n "Student = 1\|Administrator = 2\|Conjunto cerrado" src/GeometriaFactory.Domain/Values/Role.cs
4:/// Papel de una cuenta. Conjunto cerrado de dos valores.
15:    Student = 1,
18:    Administrator = 2

$ grep -n "^| Reclamos" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md
877:| Reclamos | Identificador de usuario, correo, **rol** (`Alumno` / `Administrador`), expiración |

$ grep -n "CA-06" SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/CU-00022*.md
204:| CA-06 | Una sesión emitida | Se inspecciona | Lleva **exactamente los cuatro reclamos** —identificador, correo, papel y expiración— y su vigencia es corta |

$ grep -n "el intermediario es el propio front del mismo sistema" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md | cut -c1-40
884:**Nota de seguridad registrada como

$ git grep -n "auth/token" -- src/GeometriaFactory.Api/Endpoints/AuthenticationEndpoints.cs
…:28:    public const string TokenRoute = "/auth/token";

$ grep -o -i "alumno" SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md | wc -l
195
$ git grep -o -i "alumno" -- SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso ':!*/_legacy/*' | wc -l
183
$ git grep -o -i "alumno" -- src/GeometriaFactory.Contracts | wc -l
43
```

(Con `_legacy/` incluido, los casos de uso dan 558; se publica el recuento vivo.)

**Vocabulario.** Esta ADR usa «cliente propio» / «aplicación propia» para lo que el punto 2 define. No colisiona con ningún uso previo del árbol vivo, medido por el contexto de lectura del archivo (`Vocabulario-Rules.md` §9.2):

```
$ git grep -n -i "cliente propio\|clientes propios" -- SDD ':!*/_legacy/*'
(sin resultado antes de esta ADR)
```

«Persona» ya es el término del intake para el sujeto de la credencial (§17.1.P.5) y no se acuña acá.

## 11. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-12 | Emisión inicial, **Aceptado** por decisión del Product Owner del 2026-09-12 (testimonio en §1). Registra que la API autentica personas y no aplicaciones: dos identidades, `POST /auth/token` como única vía para toda aplicación propia, sin claves de API ni `client_credentials`; extiende a todo cliente propio la premisa con que el intake acepta ROPC; cierra `D-01` sin ítem diferido nuevo; declara la equivalencia «alumno» ≡ papel «usuario» sin renombrar. Evalúa cinco alternativas, declara tres trade-offs, fija cinco métricas y deja en §10 los comandos con que se verificó cada cita. Descarta `BT-00028` y destraba `BT-00029`, `BT-00030`, `BT-00032`, `BT-00034` y `BT-00035`. |
