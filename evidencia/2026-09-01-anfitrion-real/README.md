# La prueba en el anfitrión real

El Product Owner aportó su credencial de administrador, y con eso se pudo hacer lo que
faltaba: **aprobar un trabajo desde , de punta a punta**.

## 1 · El botón responde (sonda no destructiva)

Se apretó «Aprobar» sobre el trabajo que el PO reporta, y **se canceló en el diálogo**:
medir no es decidir sobre el trabajo de un alumno.

```text
tras ingresar: https://aplicada.somee.com/entrega-comision
página del trabajo: /trabajos/b5c0197e-…
¿se abrió el diálogo?: SI — EL BOTON ANDA
se canceló: NO se aplicó ningún desenlace
transporte: Long Polling
```

## 2 · El desenlace se aplica (prueba completa, sobre datos desechables)

Se creó un alumno y un trabajo temporales con el JSON real del trabajo del PO, se aprobó
**desde la pantalla**, y se borraron los dos. Ningún dato de ningún alumno se tocó.

```text
el diálogo dice: Aprobar «VERIFICACION - borrar» · El trabajo pasa a Finalizado.
                 Es definitivo: no se puede volver atrás.
aterrizó en: /entrega-comision
el trabajo figura en el listado: true

EL SERVICIO DE DATOS:
  "status":"Approved"
  "administratorComment":"Aprobado en la verificacion de punta a punta."
```

Limpieza verificada: **4 cuentas, 5 trabajos**, los cinco reales en `Submitted`.

## 3 · Y lo que NO se puede afirmar

El único cambio de producto entre el reporte «sigue sin funcionar» y esta medición es la
**persistencia de las claves de protección de datos** (PR #168). Es la causa más probable.

**Pero no está probado**, y la mesa no lo va a dar por probado: entre el despliegue #42 y el
#44 no se pudo medir con una sesión real —no había credencial— así que **falta el punto de
medición que separaría «lo arregló el PR #168» de «la página estaba vieja en el navegador».**

H-2 pasa de *plausible* a **verosímil y sin refutar**, no a probada.
