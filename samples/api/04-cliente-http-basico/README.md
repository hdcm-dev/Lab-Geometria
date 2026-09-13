# Sample `api/04-cliente-http-basico` — Un cliente propio contra `/v1/`, sin el código fuente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Básico (por capacidad: `cliente-http-basico`)
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 6 líneas coinciden con §6**, contra el servicio de la rama `fase-k/bt-00034-sample-onboarding` en `127.0.0.1:5081` el 2026-09-13.
**Documento que la gobierna:** [`ejemplo-04-cliente-http-basico-api.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-04-cliente-http-basico-api.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-00004`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-00004`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Verificado`
**Tarea:** `BT-00034`

**Comando previsto:**

```bash
API_EMAIL=... API_PASSWORD=... bash samples/api/04-cliente-http-basico/run.sh
```

---

## 1. Objetivo del sample

Ser **el cliente de referencia de una aplicación propia**: alguien que **no tiene el código fuente** ni el entorno contenido del repositorio corre esta carpeta, sola, contra la superficie publicada bajo `/v1/`, **autenticándose como una persona** por `POST /v1/auth/token` ([`ADR-00009`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md): no hay clave de cliente), y en cinco pasos descubre el contrato, entra, lee, escribe y **se topa con la cuota** para aprender a leer `Retry-After`. Es lo que `BT-00020` hace desde adentro, hecho desde afuera.

## 2. Prerequisites

- **`curl`, `bash`, `awk` y `sed`.** Nada más: ni `jq`, ni `.NET`, ni el repositorio.
- **Un alumno ya habilitado por el administrador** (papel `Student`: el paso 4 escribe un trabajo, y eso es del alumno), cuyo correo y contraseña llegan por `API_EMAIL` y `API_PASSWORD`. El sample **no crea cuentas**: una aplicación propia no puede, y no tiene por qué.
- **La dirección del servicio en `API_BASE_URL`**, por omisión `https://api-geometria.aplicada.stream`. Para correrlo contra un servicio propio: `API_BASE_URL=http://127.0.0.1:5081`.
- **Que el documento OpenAPI esté publicado** en esa dirección (`/openapi/v1.json`; fuera de desarrollo hace falta `Documentacion__Publicada=true`, [`ADR-08008`](../../../SDD/Docs/Producto/Adrs/ADR-08008-La-Superficie-HTTP-Se-Describe-Y-El-Explorador-No-Se-Publica-Solo.md)). En producción lo está.

## 3. Cómo correrlo — los cinco pasos, y qué verificar en cada uno

Un solo comando ejecuta los cinco pasos; acá se dice qué hace cada uno y **qué mirar en su línea de salida**.

```bash
export API_EMAIL='persona@dominio' API_PASSWORD='su-contraseña'
# opcional: export API_BASE_URL=http://127.0.0.1:5081
bash run.sh
```

| Paso | Qué hace | Qué verificar en la línea |
| --- | --- | --- |
| **1 · contrato** | `GET /openapi/v1.json` y lista las operaciones del documento generado | `200`; **17** operaciones, **16** bajo `/v1/` y **una** exenta del prefijo, `/salud`. Si el recuento cambia, cambió la superficie ([`Contratos-REST.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md) §3.1) |
| **2 · canje** | `POST /v1/auth/token` con las credenciales de la persona | `200`, **acceso firmado recibido** y el papel de la persona, `Student`. El acceso **no se imprime** |
| **3 · listado** | `GET /v1/trabajos` con `Authorization: Bearer` | `200` y **una lista**. Cuántos hay es del almacén y se informa aparte, sin compararse |
| **4 · envío** | `POST /v1/trabajos` con el escenario `E-1` del intake §20 como texto | `201`, trabajo en **`Pendiente`**, **2 advertencias** (área del cubo, volumen del ortoedro) y **0 errores**: exactamente lo que el intake declara de `E-1` |
| **5 · cuota** | `GET /v1/trabajos` en bucle hasta el primer `429`, con tope de 130 | `429` recibido, **`Retry-After` presente, entre 1 y 60**, y **sin cuerpo** (`Contratos-REST.md` §4.1). Cuántas hicieron falta depende del umbral del despliegue y se informa aparte |

La sexta línea es el pie —`Pasos ejecutados: 5 | Respuestas comparadas: 6 | Diferencias: 0`— y después `verificar.sh` compara las seis contra [`esperado/salida.txt`](esperado/salida.txt).

**El paso 5 gasta la cuota de la persona a propósito.** Por omisión son sesenta peticiones por minuto; después de correr el sample, esa persona recibe `429` durante lo que diga `Retry-After`, y nadie más. Es el comportamiento que un cliente propio tiene que conocer antes de escribir un reintento.

## 4. Qué hay acá

```
samples/api/04-cliente-http-basico/
├── README.md                 # Esta copia corta
├── run.sh                    # Los cinco pasos; al final invoca verificar.sh
├── verificar.sh              # Compara una salida con esperado/salida.txt; corre solo
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

**La carpeta se copia sola y corre igual.** No hay ninguna ruta hacia `src/`, `scripts/` ni `SDD/`: la verificación de `BT-00034` la corrió desde una copia fuera del repositorio.

**Ni la dirección, ni el correo, ni la contraseña están escritos acá.** Llegan por el entorno. El acceso firmado se usa y no se muestra.

**Sin `jq`**: el documento generado sale con sangría de dos espacios —rutas a cuatro, verbos a seis— y `awk` alcanza para listar las operaciones; el texto del alumno se escapa con `cuerpos/escapar.awk`, el mismo de los otros samples.

**Las líneas comparadas no dependen del despliegue.** Cuántos trabajos ve la persona y cuántas peticiones hicieron falta para el `429` se informan con `·` y no entran al snapshot, porque son del almacén y de la configuración, no del contrato.

## 5. Lo que este sample dejó a la vista

- **`E-1` no queda en `Borrador`: queda en `Pendiente` con dos advertencias.** Es lo que el intake §20.E-1 y el flujo 4 declaran («pasa a `Pendiente` con las dos advertencias; ninguna lo bloquea»), y distinto de `E-5` y `E-8` en `api/01-basico`, que traen un error de validación y por eso quedan en `Borrador`. El código `201` es el mismo en los tres: habla de la petición, no del texto.
- **El `429` llega en la sexagésima primera petición de la persona en la ventana**, contando el listado del paso 3 y el envío del paso 4: contra el servicio de la rama, 59 en el paso 5. Contra un despliegue con otro umbral cambia el número informado y no la línea comparada.
- **El documento generado dice `"version": "1.0.0"` mientras `/salud` dice `1.1.0+…`.** Es la versión del **documento** (`ApiDocumentation.cs`: «el `v1` del nombre es el del documento generado»), no la del producto; el sample la informa y no la compara.
