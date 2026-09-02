# La suite de extremo a extremo, primera corrida

```text
Passed!  - Failed:     0, Passed:    13, Skipped:     0, Total:    13, Duration: 39 s - GeometriaFactory.E2ETests.dll (net10.0)
```

## Lo que la suite encontró antes de dar verde

| # | Qué apareció | De quién era |
| --- | --- | --- |
| 1 | El `.runsettings` no se podía leer y `dotnet test` **no corrió nada sin decirlo con esas palabras** | mío, y es una trampa que se repite |
| 2 | `401` en `POST /cuentas`: el alta es **del que se registra**, no del administrador | del contrato, y mi plomería lo violaba |
| 3 | `401` en `POST /cuenta/contrasena`: elegir la clave es **de la persona** | ídem |
| 4 | **El anfitrión de somee es intermitente**: 45 s sin responder, después 9.9 s, después 200 en 4.7 s | del anfitrión — el servicio de datos respondía en 0.0006 s |

El cuarto produjo **trece rojos con nombres de producto**. Por eso la suite ahora comprueba
que el laboratorio atienda **antes de correr una sola prueba**, y falla una vez con el motivo
en la cara en vez de trece veces disfrazado de defecto.

## Y un quinto, que la suite se hizo a sí misma

| # | Qué apareció | De quién era |
| --- | --- | --- |
| 5 | La siembra fallaba a mitad y **dejaba la cuenta creada**; el desmontaje no tenía qué borrar | mío — dejó **cuatro cuentas** en el panel del docente |

**No falló la limpieza: falló la siembra a mitad de camino.** Y el aviso que lo habría dicho iba a
`TestContext.Progress`, que no llega a la salida de la consola. **Un aviso que no se ve es lo mismo
que no avisar** — el mismo defecto que las claves efímeras escritas en un registro apagado.

Ahora la siembra se hace cargo de su propio desastre: **el que ensucia limpia, incluso cuando se
cae en el intento.**

## Corrida final

```text
Passed!  -  Failed: 0,  Passed: 13,  Total: 13,  Duration: 37 s
almacén: 4 cuentas, 0 sembradas sin limpiar, 5 trabajos
```
