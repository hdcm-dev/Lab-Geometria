# Sample `visor/02-intermedio` — Árbol y escena sincronizados por índice, y ninguna pieza que desaparezca sin aviso

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Intermedio
**Estado de esta carpeta:** **Implementado.** Corre en 0; **9 de 15 líneas coinciden con §6**. Las otras 6 son divergencias declaradas, y **una de ellas es un defecto del visor** (abajo).
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-02-intermedio.md) 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-14`, en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify
```

---

## 1. Objetivo del sample

Demostrar las dos capacidades que convierten al visor en instrumento didáctico y no en una escena bonita: que el árbol del texto y la escena **se sincronizan por índice** sin traducir identidades, y que **ninguna pieza desaparece sin quedar enumerada** con su índice y su código. Es la segunda de las tres partes del sample **S-1**.

## 2. Prerequisites

- Los mismos cinco ítems del sample `01-basico`, sin agregados de herramienta.
- **Un agregado de datos:** los **cinco** textos de escenario se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, en archivos `.txt`, incluido el de `E-2`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/02-intermedio run verify`.
4. Para mirarlo a mano, abrir `samples/visor/02-intermedio/index.html` y elegir un escenario del selector.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

La página con el árbol y el selector de escenarios, el anfitrión que sincroniza el árbol con la escena **por índice**, y un recorrido que conduce Chromium sobre los cinco escenarios.

**El árbol lo dibuja el anfitrión, no la fachada**, y es la frontera que más fácil se cruza sin darse cuenta. El visor devuelve qué dibujó y qué no —con el motivo— y nada más; qué se muestra con eso, y cómo, es de este lado.

**Las piezas vienen ya reconstruidas**, producidas corriendo el intérprete real del producto sobre los `.txt` de `datos/`. Ningún valor se escribió a mano. Los `.txt` siguen ahí porque son el dato de origen; el visor no los lee.

## 5. Un defecto, y cinco divergencias con una sola causa

### El defecto: una selección rechazada borra la vigente

§6 dice, en `[11]`, que al pedir un índice fuera del conjunto la **selección vigente se conserva**. No se conserva.

`ViewerInstance.select` recorre **todas** las mallas apagando el resalte de las que no coinciden, y recién al terminar el recorrido descubre que ninguna coincidía. Para cuando `INDEX_OUT_OF_RANGE` se informa, el estado ya se tocó: **la comprobación está después del efecto en lugar de antes.**

El código que se informa es el correcto. Lo que falla es que informarlo no deshace nada. Se ve mirando: el cuadro posterior al rechazo no es el anterior.

### Las cinco divergencias, todas de `ADR-08006`

| # | §6 espera | El árbol |
| --- | --- | --- |
| `D-1` `[5]` | `no dibujadas=1 \| codigo=NON_DRAWABLE_TYPE` | **no dibujadas=0** |
| `D-1` `[6]` | `dibujadas=1 no dibujadas=1 \| indice=1` | **0 y 1, índice 0** — mismo código, mismo mecanismo |
| `D-1` `[10]` | `indice 1 de E-8` | **índice 0** |
| `D-2` `[8]` | `Estructura del texto de E-8: piezas=2` | **no la devuelve** |
| `D-3` `[13]` | `INVALID_CANVAS_ELEMENT curso C-2` | **sin aviso, redimensiona a 1×1** |

**`D-1` es una sola cosa dicha tres veces: el laboratorio no entrega lo que rechaza.** La figura mal escrita de `E-5` nunca se vuelve pieza —el laboratorio la rechaza con una observación—, así que al visor no le llega nada que no pueda dibujar. Desde `ADR-08006`, **«no dibujada» del lado del visor y «rechazada» del lado del laboratorio son dos cosas en dos componentes distintos**, y §6 —escrito antes— las trataba como una.

**Y de ahí sale algo más: `NON_DRAWABLE_TYPE` no tiene camino.** El laboratorio sólo reconstruye los seis tipos que el visor dibuja. El séptimo del dominio, `RectanguloDesarrollado`, existe únicamente como componente; puesto como figura raíz, el laboratorio lo **rechaza**. Se probó. La guarda del visor es defensa en profundidad y no cubre ningún caso alcanzable hoy.

**`D-2` es la misma de `visor/01-basico`**: el visor no recibe el texto, así que no tiene estructura de texto que devolver. Lo que el árbol necesita sí lo devuelve.

**`D-3` — `resize` no comprueba el tamaño.** Cae a `clientWidth || 1` y redimensiona a un píxel. La mitad que §6 afirma se cumple —la instancia sigue viva, con su escena y su selección intactas— y lo que falta es el aviso.

## 6. Lo que sí salió como está escrito

- **`E-6` se dibuja y `E-8` no**, que es la distinción que el producto viene a instalar: lo que produce `UNREADABLE_DIMENSION` es **la ausencia de la clave**, nunca el valor que trae. El visualizador previo perdía la figura de `E-6` sin aviso porque evaluaba la verdad del número en lugar de su presencia.
- **`E-2` y `E-7` dibujan su ortoedro con las dos claves.** El sinónimo lo resolvió el laboratorio: `Tapas` y `Bases` llegan acá como el mismo rol, que es por qué el contrato del visor dice que no hay claves sinónimas — «y no las hay porque no llegan».
- **Cero piezas no dibujadas sin registro**, sobre los cinco escenarios: cada pieza entregada está dibujada o enumerada.
- **Cero peticiones de red.**

## 7. Cómo se mide lo que no tiene contador

**El resalte se mide mirando.** La fachada no publica cuál pieza está resaltada, así que el recorrido compara **imágenes**: dos selecciones distintas dan dos cuadros distintos, y volver a la primera reproduce el primero. Eso es exclusivo y determinista — un resalte que se acumulara daría un tercer cuadro al volver. Es también lo que deja ver el defecto de `[11]`.
