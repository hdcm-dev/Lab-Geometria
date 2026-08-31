# Sample `api/03-avanzado` — Composición de raíz y arranque en dos fases: qué pasa antes de la primera petición

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Avanzado
**Estado de esta carpeta:** **Implementado.** Corre en 0; **9 de 10 líneas coinciden con §6** y la única divergencia es el `503` de `A-16`, que **no tiene camino** y está declarado como tal.
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-api.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-03`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/api/03-avanzado/run.sh
```

---

## 1. Objetivo del sample

Demostrar lo que este proyecto de código tiene de propio y ningún otro del producto tiene: **la composición de raíz** —conectar los **cuatro** puertos con sus adaptadores, en un solo lugar y tomando la configuración de afuera— y **el arranque en dos fases**, que deja el almacén en condiciones **antes** de atender la primera petición y **se detiene** en lugar de atender sobre un almacén dudoso.

## 2. Prerequisites

- **Entorno de desarrollo contenido del repositorio.**
- **El servicio construido y sin advertencias.**
- **Dos almacenes de partida**: uno en su estado de primer arranque y **uno con linaje desconocido**, que el propio sample compone y declara como compuesto por él.
- **La configuración provista por el entorno.** El sample no contiene ninguna dirección, ninguna clave de firma y ninguna ruta de almacén.
- **El servicio lo arranca el sample, no un paso previo**: acá el arranque **es** el objeto de la verificación.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `bash samples/api/03-avanzado/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Los cuatro actos de §5. **Este es el único sample de la unidad que levanta el servicio él mismo**, y tiene que ser así: lo que mide son propiedades del arranque, y un servicio que ya está arriba no tiene arranque que mostrar. Arranca **dos veces** —una sobre un almacén sano y otra sobre uno de linaje desconocido— y la segunda no debe llegar a escuchar.

**El almacén de linaje desconocido lo compone el sample**, y `almacenes/linaje-desconocido.md` deja constancia de eso para que nadie lo lea como un dato del producto. Lo compone `almacenes/almacen.cs`, una aplicación de un solo archivo: en el entorno contenido no hay `sqlite3` ni `python3`, y el repositorio ya resolvió esto antes de la misma forma con `tools/informe-cobertura.cs`.

**Todo ocurre en un directorio temporal propio que se borra al terminar.** Componer un almacén roto cerca del almacén de trabajo sería exactamente el descuido que `scripts/store-path.sh` documenta.

## 5. Las dos divergencias contra §6, y la que se cerró

| # | §6 espera | El árbol |
| --- | --- | --- |
| `D-3` | `Punto de salud con el almacen indisponible: 503` | **sin camino** |

### La otra que se cerró: el punto expuesto que el contrato no declaraba

Este sample contaba **diecisiete** operaciones expuestas contra **dieciséis** declaradas en `Contratos-REST.md`. La que sobraba era **`A-18`** —`POST /interpretaciones`—, y no era un punto olvidado en el código: **estaba declarado en la categoría 02** y `ADR-08006` lo creó como su contrapartida. El barrido de alcance de esa decisión **llegó a la 02 y no a la 05**.

Se adoptó en `Contratos-REST.md` **1.5** el 2026-08-31, con sus cuatro recuentos actualizados. Hoy el servicio expone diecisiete y el contrato declara diecisiete.

### La que se cerró: el mensaje del arranque detenido

Hasta el **2026-08-30** el arranque **se colgaba** ante un almacén de linaje desconocido: se detenía —eso `US-00028` lo exige y funcionaba— pero dejando escapar la excepción, así que el runtime volcaba la cadena entera, con la traza del proveedor y su mensaje sobre una tabla que ya existe. Quien despliega leía **el síntoma y una traza**, no la causa.

**Envolver la excepción no alcanzó**, y fue la primera corrección intentada: el runtime imprime también las internas. Lo que hacía falta era **atraparla en el arranque y terminar por decisión propia**, con código de salida `78` —`EX_CONFIG`—, que además distingue esta parada de un cuelgue para un orquestador de contenedores.

**Y al cerrarlo hubo que corregir la medición.** Antes había una sola cosa que mirar; ahora hay dos y son de naturaleza distinta: **el mensaje**, que es lo que `RA-03` gobierna y hoy sale limpio, y **el registro**, donde la excepción original queda con su traza — que es donde tiene que estar. Medirlas juntas decía que había traza en el mensaje cuando la traza estaba en el registro: **medir dos cosas y reportar una**. El sample las separa, y deja constancia de que el detalle técnico no se perdió.

**`D-3` — el `503` de `A-16` no tiene camino.** `Contratos-REST.md` §3 le da a ese punto exactamente dos códigos, y la rama existe en `HealthEndpoint`. Pero `StorePreparation` o termina poniendo la marca en verdadero, o lanza; y si lanza, el proceso no llega a escuchar. **No es un defecto: es la consecuencia de que el producto eligiera detenerse en el arranque en lugar de atender degradado.** Con el almacén indisponible no hay `503` porque no hay servicio, y el acto 3 mide exactamente ese otro lado.

## 6. Dos cosas que el sample resolvió corriéndose

- **La fase 1 sólo se puede medir por ausencia.** Mientras las transformaciones se aplican el escucha no está abierto, así que cada sondeo muere en la conexión y no devuelve código HTTP. Lo que el sample cuenta no son respuestas de error: son **respuestas**. Si alguna llegara, habría habido una petición atendida sobre un almacén a medio preparar. Salió **0**, las dos veces.
- **El orden de los actos se escribe y no se deja al directorio.** La primera versión los recorrió con un comodín y salieron en orden alfabético: el arranque detenido antes que el sano. Ninguna medición fue falsa —cada acto midió lo suyo— pero la salida no se podía comparar con §6, y un sample cuya salida depende de cómo ordena el sistema de archivos no es reproducible.
