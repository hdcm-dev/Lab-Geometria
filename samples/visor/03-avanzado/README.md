# Sample `visor/03-avanzado` — Las seis funciones sin backend, con los dos movimientos prendidos y el contador de red en cero

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Avanzado
**Estado de esta carpeta:** **Implementado.** Corre en 0; **14 de 17 líneas coinciden con §6**, estable en corridas repetidas. Las otras 3 son divergencias declaradas, y **una es un defecto** (abajo).
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-03-avanzado.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-15`, en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify
```

---

## 1. Objetivo del sample

Demostrar el punto de extensión del producto entero: las **seis** funciones de la fachada, recorridas de punta a punta **sin ninguna pieza del backend**, con los dos movimientos automáticos prendidos y sostenidos, y con el contador de peticiones de red en **cero**. Es la tercera parte del sample **S-1**, y la que cierra su promesa.

## 2. Prerequisites

- Los mismos cinco ítems del sample `01-basico`.
- **Conductor de navegador capaz de declarar preferencia de movimiento reducido** del sistema: es el único doble admitido, y lo que se simula es el entorno del anfitrión.
- **Comprobación reproducible de texto sobre el archivo de guion generado**, porque el acto `[10]` inspecciona el **bundle generado** y no sólo la fuente.
- **Sin acceso a redes de distribución externas**: el acto `[11]` mide `PT-03` en esas condiciones y darle acceso invalidaría la medición.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido, con el conductor configurado para declarar preferencia de movimiento reducido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/03-avanzado run verify`.
4. Para mirarlo a mano, abrir `samples/visor/03-avanzado/index.html` y usar los dos controles de movimiento.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Los tres recorridos de §5 —las seis funciones, el gobierno del movimiento, y las dos puertas técnicas—, más un cuarto que compara contra §6. Todo contra Chromium de verdad y **sin backend**.

**El anfitrión conserva la preferencia y la fachada no.** Es la línea divisoria de `G-2` y `G-3`, y no es una sutileza de diseño: gracias a eso el recorrido puede prender los dos movimientos aunque el entorno declare preferencia de movimiento reducido. Sin esa propiedad, la medición de cero red de `[13]` quedaría en verde **sin haber ejercitado nunca el bucle de dibujo**.

**Las piezas vienen ya reconstruidas**, producidas corriendo el intérprete real sobre los `.txt` de `datos/`.

## 5. Un defecto y dos divergencias

### El defecto: apagar un movimiento no deshace lo que hizo

| | §6 espera | El árbol |
| --- | --- | --- |
| `[7]` | piezas de vuelta en su orientación de partida | **quedan donde estaban** |

El bucle deja de incrementar `mesh.rotation.y` y nada más. **Apagar no es deshacer**, y acá la diferencia se ve: el cuadro posterior al apagado no es el anterior al encendido.

**Lo mismo vale para la cámara**, aunque §6 no lo pida en un renglón propio: prender la órbita la mueve y apagarla la deja donde quedó. Es la misma causa, y por eso el sample la nombra en `[5]` en vez de dejarla implícita.

*(Es la segunda vez que este patrón aparece en el visor: en `visor/02-intermedio`, una selección rechazada borraba la vigente. Las dos son «el efecto ya ocurrió cuando se decide sobre él».)*

### Las dos divergencias

| # | §6 espera | El árbol |
| --- | --- | --- |
| `D-2` `[15]` | `7 de 7` códigos, `0` acuñados | **6 de 7**; los acuñados **ya son 0** |
| `D-3` `[10]` | `globales sueltas: 0` | **1 — `__THREE__`** |

**`D-2` tenía dos mitades y una ya está cerrada.** El código que falta es `UNREADABLE_TEXT`, y no es un olvido: era el código del texto del alumno, que la fachada ya no recibe desde `ADR-08006`. La otra mitad —**uno acuñado aguas abajo**, `UNKNOWN`, el respaldo de `reason ?? 'UNKNOWN'`— **se cerró el 2026-08-30**: se retiró el respaldo y `MeshOutcome` pasó a ser una unión discriminada, de modo que el caso que lo justificaba **no compila**. Hoy el renglón dice `0`.

**Y al cerrarla apareció un falso positivo del propio instrumento.** Retirado el literal, la inspección lo seguía contando: leía los dos comentarios que explican **por qué** se retiró. Ahora quita los comentarios antes de contar. Es la misma clase de falso positivo que la mesa del 2026-08-27 midió con los seis enlaces rotos que no eran enlaces — **un identificador nombrado en prosa no es un identificador emitido**.

**`D-3` no lo pone el producto.** `__THREE__` la registra el motor gráfico al cargarse, para avisar si hay dos copias suyas en la página. El nombre propio del paquete sigue siendo uno solo y las seis funciones están donde tienen que estar.

## 6. Lo que sí salió como está escrito

- **`PT-02` entera, en sus cinco tramos**: carga, escena, `E-1` con su ortoedro, **diez recorridos de ida y vuelta sin degradar**, y sincronización por índice. Diez está elegido por encima del límite de contextos gráficos vivos del navegador, que es lo que hace aparecer el defecto si `destroy` no libera.
- **`PT-03`**: motor de dibujo dentro del bundle, comprobado buscando su firma en el archivo generado y no confiando en el `package.json`.
- **Cero peticiones de red con los dos movimientos prendidos y sostenidos, y durante rotar y acercar.** La condición de medición es vinculante: medirlo con los movimientos apagados dejaría la prueba en verde sin haber ejercitado el bucle.
- **Cero ocurrencias de las tres formas de petición, en la fuente y en el bundle generado.** Las dos inspecciones hacen falta: una dependencia que pidiera por dentro no aparecería en la fuente.
- **Cero claves en el almacenamiento del navegador**, contadas después de haber ejercitado la fachada entera.

## 7. Dos cosas que el sample resolvió corriéndose

- **El orden en que se puede medir no es el orden en que §6 se lee.** `[13]` y `[14]` sólo se pueden medir con el movimiento gobernado —son del segundo recorrido— y §6 los lee después de las puertas técnicas, que son del tercero. El comparador ubica cada renglón **por su etiqueta**: se reordena la emisión, nunca la medición.
- **Y las divergencias se anotan por etiqueta, no por número de renglón.** Con número falló en silencio: `[10b]` corre la numeración un lugar, así que la declaración de `[15]` apuntaba al renglón equivocado y aparecía como no declarada.

**`[5]` se mide sin dejar correr un solo cuadro entre prender y apagar**, y la ausencia de espera es deliberada. Lo que §6 afirma es que **gobernar** no mueve nada más; si se dejara correr la órbita, la cámara se movería por el movimiento y no por el acto de gobernarlo. Sin esa precisión el renglón daba distinto en dos corridas iguales.
