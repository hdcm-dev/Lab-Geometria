# El recorrido del alumno, de punta a punta

Hasta hoy **ningún guion de navegador abría sesión de alumno**: los cuatro que había eran del
docente o anónimos. El camino del alumno atraviesa siete superficies y la más grande del producto
—`WorkSubmission`, 714 líneas— y sólo estaba cubierto por pruebas HTTP.

## Los ocho pasos

```text
1  /registro-de-cuenta      registrarse, y que la pantalla diga que falta el docente
2  el docente habilita      ← por el servicio de datos: es acto del ADMINISTRADOR
3  /ingreso con provisoria  y que OBLIGUE a cambiarla
4  cambio obligado          elegir la propia
5  /trabajo-nuevo           texto que no verifica → queda en BORRADOR
6  /mis-trabajos            aparece, con su insignia y con «Editar»
7  /trabajos/{id}/editar    LA FECHA VUELVE  ← cuida el defecto arreglado hoy
8  corregir y reenviar      → Pendiente, confirmado contra el servicio de datos
```

## Qué texto deja un borrador, medido y no supuesto

```text
una cara sin «Area»      → Submitted   (produce ADVERTENCIA, no error)
un área declarada mal    → Submitted   (ídem)
un texto que no es JSON  → Draft       ← el único que deja borrador
```

Sólo un **error** de validación deja el trabajo en borrador; las discrepancias de valor son
advertencias y el trabajo entra igual. No se adivina.

## Los tres rojos que costó, y de quién era cada uno

| Rojo | De quién |
| --- | --- |
| `#forced-email` no existe | **del producto, y para bien**: cuando ya sabe el correo lo manda oculto en vez de hacérselo reescribir |
| Ocho coincidencias del nombre en `/mis-trabajos` | **del producto, y por diseño**: tabla + tarjeta apilada + un rótulo accesible por acción |
| `ERR_NETWORK_CHANGED` | de la máquina que corría las pruebas |

**Ninguno fue un defecto.** La prueba se adaptó al producto, no al revés.

## Corrida final

```text
Passed!  -  Failed: 0,  Passed: 14,  Total: 14,  Duration: 39 s
almacén: 4 cuentas, 0 sembradas sin limpiar, 5 trabajos
```
