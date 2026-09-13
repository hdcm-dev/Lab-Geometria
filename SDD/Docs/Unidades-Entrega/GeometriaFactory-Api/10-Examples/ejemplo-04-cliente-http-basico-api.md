# Ejemplo 04 — Un cliente propio contra `/v1/`, sin el código fuente: descubrir, entrar, leer, escribir y toparse con la cuota

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-04-cliente-http-basico-api.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-09-13
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10), tarea `BT-00034`
**Nivel:** Básico (progresión por capacidad: `cliente-http-basico`)
**Ubicación del código:** `/samples/api/04-cliente-http-basico/`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md`](../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md); [`../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.2 regla 5; [`../05-Arquitectura-Tecnica/Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) **1.9** §3.1 y §4.1; [`../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md`](../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md); [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-00022`, `CU-00026` y `CU-00028`; `05` §8.1, fila «Pasos de la colección de peticiones reproducible»; `PRODUCT-INTAKE` **4.4** §17.1.P.3 y §20, escenario `E-1`; [`../06-Backlog-Tecnico/tareas-tecnicas/BT-00034-Sample-De-Onboarding-Para-Un-Cliente-Externo-Contra-V1.md`](../06-Backlog-Tecnico/tareas-tecnicas/BT-00034-Sample-De-Onboarding-Para-Un-Cliente-Externo-Contra-V1.md)
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-00004` como sonda; `11-Documentacion` cuando se emita, y la documentación publicada de `BT-00031`, que lo referencia sin duplicarlo

---

## 1. Objetivo del sample

Ser **el cliente de referencia de una aplicación propia** ([`ADR-00009`](../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) §2 punto 2: otro front, una aplicación móvil, un guion del docente): alguien que **no tiene el código fuente** ni el entorno contenido del repositorio copia una carpeta, exporta tres variables y en **cinco pasos** descubre el contrato desde el documento generado, canjea las credenciales de **una persona** por un acceso firmado, lista, escribe con un escenario real del intake y **recibe el `429` con su `Retry-After`**. Al terminar, quien lo corrió sabe las tres cosas que un cliente propio tiene que saber antes de escribir la suya: que **la única identidad es una persona**, que **el contrato se lee de `/openapi/v1.json` y no se adivina**, y que **la cuota es suya y la respuesta que la anuncia no trae cuerpo**.

Es lo que [`ejemplo-02-intermedio-api.md`](ejemplo-02-intermedio-api.md) (`BT-00020`) hace desde adentro del repositorio, hecho desde afuera y contra las rutas versionadas: la ficha `BT-00034` lo declara análogo y ésa es la única diferencia.

## 2. Nivel

**Básico, por capacidad.** El slug es `cliente-http-basico`, uno de los que `Rules-Examples.md` §3.1 admite y **el que la matriz de §2.3 reserva para el primer sample de una `rest-api`**: el cliente HTTP que un integrador corre desde afuera. Este producto nunca lo tuvo con ese nombre porque sus tres samples de la API se nombraron por nivel y corren **adentro** del entorno contenido ([`README.md`](README.md) §6, desvío 1). La progresión que este cuarto agrega respecto de los tres anteriores no es de complejidad —es el más corto de los cuatro— sino **de lugar**: es el primero que no necesita el repositorio. Usa **uno** de los ocho escenarios y **tres** de los diecisiete puntos de acceso —`A-01`, `A-10` y `A-13`— más la ruta exenta `/openapi/v1.json`, que describe la superficie y no es parte de ella.

## 3. Prerequisites

- **`curl`, `bash`, `awk` y `sed`.** Lo mismo que ya usan los tres samples de la API, y nada más: ni `jq`, ni `.NET`, ni el repositorio. La carpeta se copia sola y corre igual.
- **Una persona ya habilitada por el administrador**, con su correo y su contraseña en `API_EMAIL` y `API_PASSWORD`. El sample **no crea cuentas**: dar de alta y habilitar son actos del administrador (`CU-00021`, `CU-00023`) y una aplicación propia no los tiene. **La persona tiene que ser un alumno** (papel `Student`): el paso 4 escribe un trabajo, y cargar trabajos es del alumno (`A-10` responde `403` a un administrador). Con un administrador corren los pasos 1, 2, 3 y 5 y el 4 no; §7 lo declara como variación.
- **La dirección del servicio en `API_BASE_URL`**, por omisión `https://api-geometria.aplicada.stream`, que es la superficie publicada desde el 2026-09-06. Ninguna dirección, clave ni contraseña está escrita en el sample.
- **El documento OpenAPI publicado en esa dirección** (`/openapi/v1.json`). En desarrollo siempre lo está; fuera de desarrollo hace falta `Documentacion__Publicada=true` ([`ADR-08008`](../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md)). En producción está publicado desde el 2026-09-13.

## 4. Cómo correrlo

1. Copiar la carpeta `samples/api/04-cliente-http-basico/` a cualquier directorio, o clonar el repositorio: da lo mismo, no lee nada por fuera de sí misma.
2. `export API_EMAIL='persona@dominio' API_PASSWORD='su-contraseña'` — las credenciales de la persona habilitada.
3. Opcional: `export API_BASE_URL=http://127.0.0.1:5081` para correrlo contra un servicio propio; sin esto, contra la superficie publicada.
4. `bash run.sh`.
5. Leer las seis líneas y el veredicto de `verificar.sh`, que se invoca solo; §6 dice qué mirar en cada una.

**Cinco pasos, el máximo que `Rules-Examples.md` §4.2 admite, y ninguno dentro del entorno contenido.** Los cinco pasos **del recorrido** —contrato, canje, listado, envío, cuota— son los que `run.sh` ejecuta en su único comando.

## 5. Estructura del código

```
samples/api/04-cliente-http-basico/
├── README.md                 # Copia corta de §1, §3 y §4 de este documento
├── run.sh                    # Los cinco pasos; al final invoca verificar.sh
├── verificar.sh              # Compara una salida guardada con esperado/salida.txt
├── peticiones/
│   ├── 01-canjear            # A-01
│   ├── 02-listar-trabajos    # A-13
│   └── 03-enviar-trabajo     # A-10, con E1.txt como cuerpo
├── cuerpos/
│   ├── E1.txt                # Transcripto del PRODUCT-INTAKE §20.E-1, sin modificación
│   └── escapar.awk           # El texto a literal JSON, y nada más (US-00019)
└── esperado/
    └── salida.txt            # Snapshot de §6
```

**`verificar.sh` es un guion aparte, a diferencia de los tres samples anteriores**, que comparan adentro de `run.sh`. La separación existe para que un cliente propio pueda guardar una salida y verificarla después, o verificar la de otro; `run.sh` lo invoca al terminar y el veredicto es el mismo. **Ningún otro archivo del sample se comparte con el repositorio**: `E1.txt` y `escapar.awk` son copias byte a byte de los de `api/02-intermedio` y `api/01-basico`, porque la carpeta tiene que correr sin ellos.

**El cuerpo lleva extensión `.txt` y no `.json`, a propósito**, por el mismo motivo que en los tres anteriores: el texto del alumno se transporta **sin normalizar** (`US-00019`) y nombrarlo `.json` invitaría a reformatearlo.

**Lo que se informa y lo que se compara son dos cosas.** Las líneas con `·` —las diecisiete operaciones listadas, la versión del documento, cuántos trabajos ve la persona, cuántas peticiones hicieron falta para el `429`— **se informan y no se comparan**, porque dependen del almacén y de la configuración del despliegue. Las seis líneas de §6 dependen sólo del contrato.

## 6. Qué esperar

```
[1 contrato] GET /openapi/v1.json: 200 | operaciones: 17 | bajo /v1/: 16 | exentas del prefijo: /salud
[2 canje] POST /v1/auth/token: 200 | acceso firmado recibido: si | papel: Student
[3 listado] GET /v1/trabajos: 200 | es una lista: si
[4 envio] POST /v1/trabajos con E-1: 201 | estado del trabajo: Pendiente | advertencias: 2 | errores: 0
[5 cuota] GET /v1/trabajos hasta el limite: 429 recibido: si | Retry-After presente: si | entre 1 y 60: si | sin cuerpo: si
Pasos ejecutados: 5 | Respuestas comparadas: 6 | Diferencias: 0
```

**Qué verificar en cada paso, y por qué ese número.**

| Paso | Qué verificar | De dónde sale |
| --- | --- | --- |
| 1 | `200`; **17** operaciones, **16** bajo `/v1/`, **una** exenta: `/salud` | [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §3.1: «dieciséis bajo `/v1/` y una exenta»; `/openapi/v1.json` y `/documentacion` no se describen a sí mismas |
| 2 | `200`, acceso firmado recibido, papel de la persona | `CU-00022` paso 6: «se responde `200` con el acceso, el identificador, el correo y el papel». **El acceso no se imprime** |
| 3 | `200` y una lista | `CU-00028` paso 3: la colección recortada por dueño |
| 4 | `201`, **`Pendiente`**, **2** advertencias, **0** errores | `PRODUCT-INTAKE` §20.E-1 y flujo 4: «pasa a `Pendiente` **con** las dos advertencias; ninguna lo bloquea»; `CU-00026` pasos 7 y 8 |
| 5 | `429`, `Retry-After` presente, entre 1 y 60, **sin cuerpo** | [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §4.1: ventana deslizante de 60 s, `Retry-After` en segundos enteros, sin cuerpo y sin código del contrato |

**La línea 4 es la lección que este sample agrega a la de `api/01-basico`.** Allá `E-5` y `E-8` traen un error de validación y quedan en `Borrador`; acá `E-1` trae **dos advertencias y ningún error** y pasa a `Pendiente`. **El código es `201` en los tres casos**: habla de la petición y no del texto, y el estado lo decide la interpretación (`CU-00026`: «el estado llega decidido y el borde no lo interpreta»).

**La línea 5 no dice cuántas peticiones hicieron falta, y es deliberado.** Contra el servicio de la rama con los umbrales por omisión, el `429` llegó en la **sexagésima primera** petición de la persona en la ventana —el listado del paso 3, el envío del paso 4 y 59 del paso 5—, que es exactamente lo que `RateLimitingTests` mide. Pero ese número es **configuración** (`RateLimiting__PermitsPerPerson`) y no contrato: un despliegue con otra cuota lo cambia sin que cambie nada de lo que el cliente tiene que saber. Lo que es contrato —que llega, que trae `Retry-After` en segundos, que no trae cuerpo— es lo que se compara.

**El paso 5 gasta la cuota de la persona a propósito, y de nadie más.** La partición es por persona ([`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §4.1): después de correr el sample, esa persona recibe `429` durante lo que diga `Retry-After` y otra persona detrás de la misma dirección sigue en `200`. El bucle tiene tope (130, `API_TOPE_CUOTA`) y corta en el primer `429`; **no espera el plazo, lo informa**, que es lo que un cliente propio tiene que hacer con esa cabecera.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Correr como administrador | `API_EMAIL` y `API_PASSWORD` de la cuenta de administrador | Línea 2 con `papel: Administrator`; la línea 3 lista los de la comisión **sin borradores** ([`RN-02011`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md)); la línea 4 pasa a **`403 UNCLASSIFIED_ERROR`**: cargar trabajos es del alumno. El cliente propio que actúa por un administrador lee y da desenlace; no escribe trabajos |
| Quitar el prefijo | `GET /trabajos` en lugar de `GET /v1/trabajos` en `peticiones/02-listar-trabajos` | `404` sin redirección y sin cuerpo del contrato: «sin prefijo no hay contrato» ([`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §3.1). No `401`: la guardia no reconoce rutas que no existen |
| Reintentar sin leer `Retry-After` | Volver a pedir inmediatamente después del `429` | Otro `429`, con el mismo plazo o menor: la ventana es deslizante y no se reinicia al minuto. El cliente que reintenta en bucle es exactamente el que la cuota existe para frenar |
| Mandar `E-5` en lugar de `E-1` | Copiar `E5.txt` de `api/01-basico/cuerpos/` y cambiar el nombre en `run.sh` | `201` igual, pero **`Borrador`, 0 advertencias y 1 error** localizado en índice 1 y campo `Tipo`: el código no cambió y el estado sí |
| Presentar una clave de aplicación | Un cuerpo de `client_credentials` (`grant_type`, `client_id`, `client_secret`) en `peticiones/01-canjear` | `400 REQUIRED_FIELD_MISSING` con `Email` y `Password` como faltantes: el canje sólo conoce a una persona ([`ADR-00009`](../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)). Una cabecera `X-Api-Key` agregada al canje válido **se ignora** y el `200` es el de las credenciales de la persona, no el de la clave |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`ADR-00009`](../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) | Decisión arquitectónica | El paso 2 canjea las credenciales de **una persona** y no hay clave de cliente en ningún archivo del sample; §7 muestra qué pasa si se presenta una |
| [`ADR-00010`](../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §2.2 regla 5 | Decisión arquitectónica | «El sample de onboarding de `BT-00034` la ejerce contra `/v1/`»: los tres puntos van con prefijo y la línea 1 cuenta 16 bajo `/v1/` |
| [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §3.1 | Contrato | La línea 1: dieciséis operaciones bajo el prefijo y una exenta, contadas sobre el documento generado y no sobre una lista escrita |
| [`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §4.1 | Contrato | La línea 5: `429` por persona, `Retry-After` entre 1 y 60, sin cuerpo |
| [`ADR-08008`](../../../Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md) | Decisión de producto | El paso 1 descubre el contrato desde el documento generado, que es la única fuente que un cliente sin código puede creer |
| [`CU-00022`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md) | Caso de uso | Pasos 2 a 6 del flujo principal (el canje) y 7 a 8 (la guardia admite el acceso en los pasos 3, 4 y 5 del sample) |
| [`CU-00026`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00026-Enviar-Un-Trabajo-Y-Ver-Sus-Observaciones.md) | Caso de uso | Pasos 2 a 8 del flujo principal con `E-1`: `201`, `Pendiente`, dos advertencias con posición y campo |
| [`CU-00028`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Caso de uso | Pasos 2 y 3 del flujo principal: el listado recortado por dueño |
| [`US-00001`](../06-Backlog-Tecnico/historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md), [`US-00017`](../06-Backlog-Tecnico/historias-usuario/US-00017-Enviar-Un-Trabajo-Nuevo-Y-Recibir-El-Estado-Que-La-Interpretacion-Decidio.md), [`US-00019`](../06-Backlog-Tecnico/historias-usuario/US-00019-Transportar-El-Texto-Original-Sin-Normalizarlo-En-El-Borde.md), [`US-00021`](../06-Backlog-Tecnico/historias-usuario/US-00021-Listar-Trabajos-Sin-Parametro-Para-Pedir-Borradores-Ajenos.md) | Historias de usuario | El canje, el envío con el estado que la interpretación decidió, el texto sin normalizar (`escapar.awk`), el listado sin parámetro |
| `05` §8.1, fila «Pasos de la colección de peticiones reproducible» | NFR | Cinco pasos, cero datos inventados: `E-1` transcripto del intake y ninguna identidad escrita |
| `PRODUCT-INTAKE` §20 `E-1` | Escenario con payload real | El único cuerpo del sample, sin modificación: tres piezas, dos advertencias |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-00004
  verifica:
    - id: CU-00022
      recorre: "pasos 2 a 6 del flujo principal (el canje) y 7 a 8 (la guardia admite el acceso en los pasos 3, 4 y 5 del sample)"
      no_recorre: "los flujos alternativos: credenciales inválidas, cuenta pendiente, acceso vencido, firma ajena, papel insuficiente y marca de cambio pendiente, que api/01-basico recorre"
    - id: CU-00026
      recorre: "pasos 2 a 8 del flujo principal con E-1: constitución, interpretación, dos advertencias, Pendiente, 201"
      no_recorre: "A-11 (el reenvío) y el flujo alternativo del texto que no verifica y queda en Borrador"
    - id: CU-00028
      recorre: "pasos 2 y 3 del flujo principal: A-13 devuelve la colección recortada por dueño"
      no_recorre: "pasos 4 a 6: A-14, el detalle"
    - id: US-00001
      recorre: "el canje con credenciales válidas"
      no_recorre: "los criterios sobre credenciales inválidas"
    - id: US-00017
      recorre: "el envío nuevo que recibe el estado que la interpretación decidió"
      no_recorre: "nada más de la historia"
    - id: US-00019
      recorre: "el texto se escapa como literal JSON y viaja sin normalizar"
      no_recorre: "la comprobación byte a byte del detalle, que es de la batería"
    - id: US-00021
      recorre: "el listado sin parámetro"
      no_recorre: "la exclusión de borradores ajenos, que exige un administrador"
  comando: "API_EMAIL=... API_PASSWORD=... bash samples/api/04-cliente-http-basico/run.sh"
  precondiciones:
    - "curl, bash, awk y sed; ningún otro binario y ningún archivo del repositorio fuera de la carpeta"
    - "Una persona habilitada por el administrador, con papel Student (el paso 4 escribe un trabajo y eso es del alumno); correo y contraseña en API_EMAIL y API_PASSWORD, nunca escritos en el sample"
    - "API_BASE_URL apuntando a un servicio con /openapi/v1.json publicado (Documentacion__Publicada=true fuera de desarrollo); por omisión, la superficie publicada"
    - "La cuota de esa persona no agotada al arrancar: el paso 5 la agota a propósito"
    - "cuerpos/E1.txt transcripto del PRODUCT-INTAKE §20 sin modificación y sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    http:
      - punto: "documento OpenAPI"
        status: 200
      - punto: "canje con credenciales válidas"
        status: 200
      - punto: "listado de trabajos con acceso"
        status: 200
      - punto: "envío del escenario E-1"
        status: 201
        body_json: { status: "Submitted" }
      - punto: "petición que excede la cuota"
        status: 429
        headers: { Retry-After: "entero entre 1 y 60" }
        body: ""
    stdout_contiene:
      - "[1 contrato] GET /openapi/v1.json: 200 | operaciones: 17 | bajo /v1/: 16 | exentas del prefijo: /salud"
      - "[2 canje] POST /v1/auth/token: 200 | acceso firmado recibido: si | papel: Student"
      - "[4 envio] POST /v1/trabajos con E-1: 201 | estado del trabajo: Pendiente | advertencias: 2 | errores: 0"
      - "[5 cuota] GET /v1/trabajos hasta el limite: 429 recibido: si | Retry-After presente: si | entre 1 y 60: si | sin cuerpo: si"
      - "CONFORME · las 6 líneas coinciden con el snapshot de §6"
    stdout_no_contiene:
      - "operaciones: 16 | bajo /v1/: 16"
      - "estado del trabajo: Borrador"
      - "429 recibido: no"
      - "eyJhbGciOi"
  discrimina:
    - asercion: "línea 1 con 17 operaciones, 16 bajo /v1/ y /salud exenta"
      rompe: "Contratos-REST.md §3.1 (ADR-00010): un punto sin prefijo, un punto de más o de menos, o el documento sin publicar"
    - asercion: "línea 2 con 200 y acceso recibido"
      rompe: CU-00022
    - asercion: "línea 3 con 200 y una lista"
      rompe: CU-00028
    - asercion: "línea 4 con 201, Pendiente, 2 advertencias y 0 errores"
      rompe: CU-00026
    - asercion: "línea 5 con 429, Retry-After entre 1 y 60 y sin cuerpo"
      rompe: "Contratos-REST.md §4.1 (BT-00029)"
    - asercion: "stdout no contiene eyJhbGciOi"
      rompe: "la regla de que el acceso firmado se usa y no se muestra"
  evidencia:
    fecha: 2026-09-13
    contra: "servicio de la rama fase-k/bt-00034-sample-onboarding en http://127.0.0.1:5081, almacén propio, umbrales por omisión, Documentacion__Publicada=true; el sample corrido desde una copia fuera del repositorio"
    salida: |
      [1 contrato] GET /openapi/v1.json: 200 | operaciones: 17 | bajo /v1/: 16 | exentas del prefijo: /salud
      [2 canje] POST /v1/auth/token: 200 | acceso firmado recibido: si | papel: Student
      [3 listado] GET /v1/trabajos: 200 | es una lista: si
      [4 envio] POST /v1/trabajos con E-1: 201 | estado del trabajo: Pendiente | advertencias: 2 | errores: 0
      [5 cuota] GET /v1/trabajos hasta el limite: 429 recibido: si | Retry-After presente: si | entre 1 y 60: si | sin cuerpo: si
      Pasos ejecutados: 5 | Respuestas comparadas: 6 | Diferencias: 0
      CONFORME · las 6 líneas coinciden con el snapshot de §6
    informado: "17 operaciones listadas; 59 peticiones en el paso 5 hasta el 429 (Retry-After: 60 s); 63 peticiones en total"
    estado: Verificado
```

**Por qué el criterio incluye cuatro aserciones negativas.** Las cuatro son defectos que pasarían las positivas: un documento que cuente dieciséis operaciones en total sería uno donde `/salud` entró al prefijo o desapareció; un `E-1` en `Borrador` sería una interpretación que convirtió una advertencia en error; un paso 5 sin `429` sería una cuota que dejó de aplicarse al grupo; y un acceso firmado en la salida sería un sample que enseña a imprimir secretos.

**Lo que no verifica, y por qué no es un hueco.** Los seis flujos alternativos del canje y la guardia los recorre `api/01-basico`; los ocho escenarios y los desenlaces del administrador, `api/02-intermedio`; los umbrales exactos de la cuota, `RateLimitingTests`. Un sample de onboarding que los duplicara dejaría de ser el más corto de los cuatro, que es su única razón de ser.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-09-13 | Emisión inicial **en la pasada de ejecución** (`Rules-Examples.md` §0.2): el sample se escribió, se corrió y `evidencia` trae la salida real con su fecha. Tarea `BT-00034`, sobre `1748503` (`main` = `v1.1.0`). Cuarto sample de la API y el primero nombrado **por capacidad** (`cliente-http-basico`, el slug que §2.3 reserva para el cliente HTTP de una `rest-api`), y el primero que corre **sin el repositorio**: sólo `curl`, `bash`, `awk` y `sed`, con la carpeta copiada afuera. Cinco pasos —contrato desde `/openapi/v1.json`, canje de una persona ([`ADR-00009`](../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)), listado, envío de `E-1`, `429` con `Retry-After` ([`Contratos-REST.md`](../05-Arquitectura-Tecnica/Contratos-REST.md) §4.1)—, **un** escenario real transcripto sin modificación y **cero** identidades escritas. El contrato `VER-00004` lleva `recorre`/`no_recorre` por caso de uso y `discrimina` por aserción, en la forma completa de `Rules-Examples.md` §4.6, y `verificar.sh` como guion aparte. Verificado `CONFORME 6/6` contra el servicio de la rama en `127.0.0.1:5081`. |
