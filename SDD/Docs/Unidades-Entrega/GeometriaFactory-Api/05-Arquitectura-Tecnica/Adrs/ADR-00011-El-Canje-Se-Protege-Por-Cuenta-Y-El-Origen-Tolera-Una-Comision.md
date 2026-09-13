# ADR-00011 — El canje se protege por cuenta, y el origen tolera una comisión

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-00011-El-Canje-Se-Protege-Por-Cuenta-Y-El-Origen-Tolera-Una-Comision.md
**Versión:** 1.0
**Estado:** Aceptado
**Fecha:** 2026-09-13
**Autor:** Arquitecto de Software Senior (AG-05), autocorrección de la corrida sobre un defecto introducido por `BT-00029` (`Master-Prompt.md` §8.1)
**Categoría:** Seguridad
**Corrige a:** la política `canje` de [`BT-00029`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md) y los dos umbrales por origen de [`../Contratos-REST.md`](../Contratos-REST.md) §4.1 **1.9**

---

## 1. Contexto

`BT-00029` (`v1.1.0`) protegió el canje `A-01` con **30 intentos por minuto por dirección de origen**, y los puntos anónimos con **120 por minuto por dirección**. En producción las dos cifras castigaron a una clase y no a un atacante, por dos hechos que ya estaban escritos:

1. **El front es un solo origen.** `GeometriaFactory.Web` habla con esta superficie servidor a servidor y no reenvía la dirección del navegador (`git grep -i "X-Forwarded\|X-Real-IP" -- src/GeometriaFactory.Web` → vacío). Todo ingreso por el front llega desde el contenedor del front: la comisión entera compartía **un** cupo de treinta canjes por minuto.
2. **Aunque lo reenviara, la facultad es un solo origen.** Los alumnos salen por un mismo NAT. Y alcanzar el laboratorio desde la red de la facultad es `RN-B1` (`PRODUCT-INTAKE` §11), riesgo de impacto **Alto**: «sin acceso, el laboratorio no existe».

El síntoma que lo delató fue el E2E del banco local, en rojo desde la fusión de `BT-00029`: `429` en la preparación, en la limpieza y en aprobar o rechazar, porque su arnés canjea desde una sola dirección. La mitigación de producción fue subir las dos cifras por variable de entorno. Esta ADR es la corrección.

**Tamaño de la comisión.** No tiene cifra: el Product Owner cerró `D5` **por incognoscible** el 2026-08-20 ([`../../../../README.md`](../../../../README.md) §8). La mayor que el producto midió es el corte de **334 alumnos** de [`../../../../Audit/Medicion-Volumen-De-Comision-2026-08-31.md`](../../../../Audit/Medicion-Volumen-De-Comision-2026-08-31.md), el escenario de «cientos» con el que esa medición razona.

## 2. Decisión

1. **La defensa contra la fuerza bruta es por cuenta** (OWASP Authentication Cheat Sheet, *login throttling*): **10 intentos fallidos** por correo normalizado en una ventana deslizante de **900 segundos**, desde cualquier origen. La normalización es la del dominio, `EmailIdentity.Normalize` ([`ADR-06003`](ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md)). Cuentan los fallos y no los ingresos. Agotada, el punto responde `429` con `Retry-After` y sin cuerpo **antes de buscar la cuenta y antes de derivar** ([`ADR-06004`](ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)), aunque la contraseña sea la correcta.
2. **La misma cuota cubre la forma sin sesión de `A-05`**, que también comprueba una contraseña por correo. Allí sólo cuenta la vigente que no verificó; una nueva que no cumple la regla no, porque quien la escribió ya conoce la vigente.
3. **El rechazo no dice si la cuenta existe.** La partición es el correo escrito, exista o no una cuenta; un correo desconocido gasta un permiso por fallo igual que una contraseña equivocada.
4. **Los topes por origen quedan holgados, del tamaño de una comisión detrás de una dirección**: trescientos alumnos (el corte medido, redondeado), con **600 canjes por minuto** (uno con un error de tipeo cada uno) y **1200 peticiones anónimas por minuto** (cuatro cada uno: sonda de aprovisionamiento, registro, cambio de la provisoria y un reintento). Sirven contra quien rocía muchas cuentas o inunda el servicio. **La cuota por persona autenticada no cambia** (60).
5. **La cuota por cuenta vive en el punto y no en la partición de `UseRateLimiter`.** Es un `PartitionedRateLimiter` inyectado (`Composition/CredentialAttemptThrottle.cs`) que el punto consulta pidiendo cero permisos y al que le anota un permiso por fallo.
6. Las seis cifras son configurables (`RateLimiting__*`) y se ajustan cuando `PT-05` mida.

## 3. Estado

**Aceptado** el 2026-09-13. `RN-B1` es restricción dura del intake y decide la dirección; los umbrales son decisión técnica dentro de ella (`Master-Prompt.md` §8.1, hecho de la corrida).

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Cuota por cuenta **en el punto**, con limitador inyectado (**adoptada**) | El cuerpo ya está enlazado y se lee una vez; la consulta ocurre antes de la derivación; puede contar **fallos** | Consultar y anotar son dos pasos: intentos concurrentes sobre la misma cuenta pueden pasar la consulta juntos. El exceso es la concurrencia en vuelo, acotada por el tope por origen, y se declara |
| Partición de la política por correo, con búfer del cuerpo en `POST /v1/auth/token` | Una sola política declarativa | Deserializa dos veces una entrada sin validar; la partición sólo sabe contar **peticiones**, así que los ingresos correctos gastarían cuota y un guion propio del docente, o el arnés E2E, quedaría bloqueado |
| Mantener la partición por origen y subir la cifra | Nada que construir | Una cifra que tolera una clase deja pasar un diccionario contra una cuenta; una que lo frena deja afuera a la clase. Es la mitigación de producción, no una corrección |
| Que el front reenvíe `X-Forwarded-For` | Devuelve la dirección real del navegador | No resuelve el NAT de la facultad. Es decisión de `GeometriaFactory.Web` y queda fuera de esta ADR |
| Bloqueo duro de la cuenta hasta intervención del docente | Frena por completo | Convierte la fuerza bruta en denegación de servicio sobre la cuenta de un alumno; OWASP desaconseja el bloqueo sin vencimiento |

## 5. Consecuencias positivas

1. Una comisión entera detrás del front o del NAT entra sin `429`, y la mitigación por variable de entorno de producción sobra.
2. Un diccionario contra una cuenta queda acotado a cuarenta contraseñas por hora, cualquiera sea la cantidad de orígenes que use.
3. El `429` por cuenta es indistinguible para cuenta existente e inexistente.

## 6. Consecuencias negativas y trade-offs

1. **Un atacante puede trabar el ingreso de una cuenta conocida durante quince minutos** equivocándose diez veces. Es el costo estándar del *throttling* por cuenta; se acota con vencimiento y no con bloqueo, y el docente puede resetear la contraseña.
2. **El tope por origen ya no frena a un atacante lento que rocía pocas cuentas**: lo frena la cuota de cada cuenta.
3. **Hasta que `PT-05` mida, trescientos es una cifra razonada y no medida.**
4. **El `Retry-After` por cuenta es la ventana entera (900)**: la ventana deslizante del marco no informa la espera, y se responde la que seguro alcanza.

## 7. Implementación

- `Composition/CredentialAttemptThrottle.cs` (nuevo), registrado como único en `ContractRateLimiting.AddContractRateLimiting`.
- `Endpoints/AuthenticationEndpoints.cs` (`A-01`) y `Endpoints/AccountEndpoints.cs` (`A-05`, forma sin sesión): consulta antes de resolver, anotación después del fallo.
- `RateLimitingOptions`: `PermitsPerAddress` 120 → **1200**, `CredentialExchangePermitsPerAddress` 30 → **600**, nuevos `CredentialFailuresPerAccount` **10** y `CredentialFailureWindowSeconds` **900**; `appsettings.json`.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Canjes de cuentas distintas desde un origen que reciben `429` antes del tope | **0** hasta 600 | `RateLimitingTests.AWholeCommissionBehindOneOriginIsNotLimitedUntilTheGenerousOriginCap` |
| Fallos sobre una cuenta antes del `429`, desde orígenes distintos | Exactamente **10** | `TheSameAccountIsLimitedAfterItsFailuresFromAnyOrigin` |
| Diferencia observable entre el `429` de una cuenta existente y el de una inexistente | **Ninguna** (intento, código, cabecera, cuerpo) | `TheAccountRefusalIsIndistinguishableForAnExistingAndAnUnknownAccount` |
| E2E del banco local con `429` | **0** | `.github/workflows/e2e.yml`, trabajo `banco-local` |

## 9. Referencias

- [`BT-00029`](../../06-Backlog-Tecnico/tareas-tecnicas/BT-00029-Rate-Limiting-Por-Clave-O-Por-Ip.md) §8, fila 1.4; [`../Contratos-REST.md`](../Contratos-REST.md) §4.1; [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §8.1, fila «Caudal sostenido».
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §11, `RN-B1`; [`../../../../README.md`](../../../../README.md) §8 (`D5`); [`../../../../Audit/Medicion-Volumen-De-Comision-2026-08-31.md`](../../../../Audit/Medicion-Volumen-De-Comision-2026-08-31.md).
- [`ADR-06003`](ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md), [`ADR-06004`](ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md), [`ADR-00009`](ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md).
- OWASP Authentication Cheat Sheet, sección *Login Throttling* (`https://cheatsheetseries.owasp.org/cheatsheets/Authentication_Cheat_Sheet.html`).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-13 | Emisión inicial, **Aceptado**. Rama `fix/canje-por-cuenta`, base `b58dec3` (`main` = `v1.1.1`). |
