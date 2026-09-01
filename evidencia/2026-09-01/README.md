# Evidencia visual — 2026-09-01

**Qué es:** seis imágenes del producto corriendo, tomadas con
[`tools/captura-de-superficies.mjs`](../../tools/captura-de-superficies.mjs) contra las dos piezas
levantadas de verdad, en **360 px** —un teléfono— y **1280 px**.

**Por qué existe.** El Product Owner lo pidió con una frase: *«hasta ahora no vi resultados
funcionando»*. Tenía razón. Se había cerrado un `P0` que decía «en un teléfono el docente ve **cero
filas**» y **nunca se mostró un teléfono con filas**: sólo pruebas en verde y compuertas conformes,
que no es lo mismo. Y la primera vez que se sacaron, **las imágenes quedaron en un mensaje de
commit y no en el repositorio** — lo levantó la mesa del 2026-09-01. Por eso están acá.

---

## Lo que se ve

| Imagen | Ancho | Qué demuestra |
|---|---|---|
| `listado-de-la-comision-telefono.png` | 360 px | **El `P0` cerrado.** Cinco alumnas, quince trabajos, cada uno con su estado y su botón **Abrir**. Hace dos días esta pantalla mostraba las cabeceras de grupo con «3 trabajos» y **ninguna forma de abrir nada** |
| `panel-de-cuentas-telefono.png` | 360 px | **El caso peor del `P0`.** Cada cuenta con sus tres operaciones —Bloquear, Resetear la contraseña, Dar de baja—. Sin esto, **el administrador desde un teléfono no podía habilitar a un alumno**, y sin habilitar no entra nadie al laboratorio |
| `mis-trabajos-telefono.png` | 360 px | El listado propio, **con sesión de docente**: muestra el mensaje que corresponde al rol equivocado y remite a la entrega de la comisión. No está vacío por defecto: está vacío porque el docente no entrega trabajos |
| `*-escritorio.png` | 1280 px | Que **nada cambió por encima de 768 px**: las tres superficies siguen dibujando su tabla |

**Los conteos que el instrumento imprime junto a las imágenes:**

```text
/entrega-comision    360 px   filas de tabla: 0   tarjetas: 15
/cuentas             360 px   filas de tabla: 0   tarjetas: 5
/entrega-comision   1280 px   filas de tabla: 15  tarjetas: 0
/cuentas            1280 px   filas de tabla: 5   tarjetas: 0
```

**La primera fila era `0 y 0`.** Eso es el `P0`.

---

## Lo que estas imágenes destaparon, y no lo habría visto ninguna prueba

**Tres recortes contra el borde a 360 px** —el sello de versión, el nombre del alumno y el recuento
del grupo—, corregidos en el PR #157. Mi propia verificación decía «15 tarjetas, correcto» mientras
la pantalla mostraba texto cortado. **Costó cuatro intentos**, y los tres primeros fueron mejoras
reales que no eran la causa; la causa la dio **medir la geometría en el navegador** y no mirarla.

**Y una que queda abierta, visible en `mis-trabajos-telefono.png`:** el aviso que remite a la
entrega de la comisión **parte mal a 360 px** — el enlace queda en una columna aparte y el punto
final queda huérfano a la derecha. No es contenido inalcanzable, es una frase que se lee partida.
Queda registrado acá y no se corrige en esta tanda.

---

## Cómo se vuelven a sacar

Con las dos piezas levantadas —el servicio de datos y la pieza pública— y una comisión sembrada:

```bash
node tools/captura-de-superficies.mjs <base> <correo> <clave> evidencia/<fecha>
```

**Y una advertencia sobre qué prueban y qué no.** Prueban que **el producto dibuja lo que tiene que
dibujar con el dato que le llega**. No prueban el despliegue: al 2026-09-01 el sitio publicado en
somee **no alcanza a su servicio de datos**, y eso no es ninguna de las unidades de este lote.
