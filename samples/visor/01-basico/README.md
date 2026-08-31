# Sample `visor/01-basico` — La página integradora mínima: crear la escena, dibujar `E-1` y liberar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Básico
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 9 líneas coinciden con §6**, desde que el 2026-08-30 su documento pasó a 2.0 y se alineó con la fachada vigente.
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-13`, en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify
```

---

## 1. Objetivo del sample

Demostrar el recorrido mínimo del archivo de guion sobre una página sin ninguna pieza del backend: crear una instancia sobre una superficie de dibujo, cargar el texto del escenario `E-1` y ver dibujadas sus **tres** piezas, y liberar la instancia. Es la primera de las tres partes del sample **S-1** del `PRODUCT-INTAKE` §18.

## 2. Prerequisites

- **Entorno de ejecución de la cadena de herramientas**, en versión de soporte prolongado, **dentro** del entorno de desarrollo contenido.
- **Navegador con capacidad gráfica tridimensional**, declarada por capacidad y no por versión.
- **Conductor de navegador capaz de contar peticiones de red.**
- **Etapa que genera el archivo de guion, cerrada.**
- **Sin backend, y es la propiedad del sample**: ni base de datos, ni servicio de datos, ni credencial, ni acceso a redes de distribución externas.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion con el comando corto: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/01-basico run verify`.
4. Para mirarlo a mano, abrir `samples/visor/01-basico/index.html` en un navegador con capacidad gráfica tridimensional y pegar el texto de `E-1` en el área de texto.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

La página integradora mínima, el anfitrión que invoca tres de las seis funciones de la fachada, y un recorrido que conduce **un navegador de verdad** y compara con §6. Sin backend: ni base de datos, ni servicio, ni credencial. El motor de dibujo va dentro del archivo de guion.

**El documento que gobierna esta carpeta se mudó.** El README apuntaba a `SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/`, que ya no existe: el Visor se consolidó dentro de `GeometriaFactory-Web` el 2026-08-16. El enlace de arriba es el vigente.

**`index.html` no trae nada de afuera.** Sin hojas de estilo externas, sin bibliotecas de interfaz y sin ninguna referencia a una red de distribución: es la propiedad que `PT-03` mide sobre el producto, ejercida acá desde el sample. La línea `[8]` la comprueba con el navegador contando: **cero** peticiones originadas por el archivo de guion.

## 5. Las dos divergencias contra §6, y son la misma

| # | §6 espera | El árbol |
| --- | --- | --- |
| `D-1` | `Texto de E-1 cargado: ...` | **`Piezas de E-1 cargadas`** — los números son los que §6 espera |
| `D-2` | `Estructura del texto devuelta para el arbol: piezas=3` | **no la devuelve** |

**El visor ya no recibe el texto del alumno.** `loadPieces` se llamaba `loadJson` y lo recibía; cambió el **2026-08-16** por `ADR-08006`, y el nombre cambió junto con la firma porque una función que se llama «cargar JSON» y recibe otra cosa promete lo que no cumple. Quien reconstruye es el laboratorio, del lado del servicio.

De ahí salen las dos: si no recibe el texto, no carga un texto (`D-1`) y no tiene estructura de texto que devolver (`D-2`). **Ninguna capacidad falta**: lo que el visor sí devuelve —lo dibujado y lo no dibujado con su motivo— es la garantía por la que existe, y el árbol lo arma el anfitrión con las piezas que ya tiene.

## 6. Lo que sí salió como está escrito

- **`Ortoedro=1`, que es el caso insignia del producto.** En el visualizador previo ningún ortoedro generado por la aplicación de los alumnos se dibujaba. Acá se dibuja, medido sobre las piezas **dibujadas** y no sobre las entregadas: entregar no es dibujar.
- **`piezas dibujadas: 0` al nacer la instancia.** `initialize` no dibuja nada hasta que se le den piezas; una escena que naciera con contenido sería deriva.
- **Cero peticiones de red** originadas por el archivo de guion, contadas por el navegador durante todo el recorrido.
- **`UNKNOWN_INSTANCE` al usar el identificador liberado**, devuelto y no lanzado: un anfitrión que pasa un identificador viejo tiene que poder seguir.

## 7. Tres cosas que el sample resolvió corriéndose

- **Una página abierta desde el disco no puede leer sus archivos vecinos.** §4 paso 4 pide abrir `index.html` directamente en un navegador, y la primera versión leía los datos con `fetch`: el navegador lo prohíbe y el sample se colgaba esperando datos que nunca llegaban. Las piezas entran ahora por **etiqueta de guion**, que es lo único que funciona igual abierto a mano y conducido.
- **Y tampoco puede cargar módulos.** Mismo motivo, misma solución: el anfitrión es un guion clásico.
- **El determinismo de `[5]` se mide comparando imágenes y no listas.** La disposición es lo que se ve; comparar el orden de un arreglo pasaría igual con dos piezas cambiadas de lugar en la escena. `G-6` compromete la **posición** derivada del índice, y dos dibujos con la misma posición producen el mismo cuadro.
