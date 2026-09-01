# El botón de aprobar, antes y después

Reportado por el Product Owner el **2026-09-01**: «cuando hago aprobar trabajo no lo hace».
Las dos corridas son de `tools/verificar-resolucion-del-trabajo.sh`, que levanta un
laboratorio efímero, siembra un trabajo enviado y **abre un navegador de verdad**.

## Antes — con el código de `main`

```text
Trabajo «Cubo y ortoedro» en estado Submitted · c90178b2-07b6-4e4d-84c9-0d6223497acd
PASA  1. El bloque de resolución se dibuja
FALLA 2. Apretar «Aprobar» abre el diálogo de confirmación · EL BOTÓN NO HIZO NADA: no hay diálogo. Es el defecto reportado.
FALLA 3. El diálogo nombra el trabajo y declara la terminalidad · no hay diálogo
FALLA 4. «Cancelar» cierra sin aplicar · no hay diálogo
FALLA 5. Confirmar aterriza en /entrega-comision · quedó en /trabajos/c90178b2-07b6-4e4d-84c9-0d6223497acd
FALLA 6. El servicio de datos dice Finalizado · no se llegó a aplicar
NO CONFORME · 5 de 6 paso(s) fallaron
FALLA 6. El servicio de datos dice que el trabajo quedó en 'Submitted', no en 'Approved'
NO CONFORME · ver los pasos de arriba
```

## Después

```text
Trabajo «Cubo y ortoedro» en estado Submitted · ba25d170-8199-4ccd-b05e-021a7a7c0d52
PASA  1. El bloque de resolución se dibuja
PASA  2. Apretar «Aprobar» abre el diálogo de confirmación
PASA  3. El diálogo nombra el trabajo, declara la terminalidad y muestra el comentario · nombre=true terminalidad=true comentario=true
PASA  4. «Cancelar» cierra sin aplicar y el bloque sigue disponible
PASA  5. Confirmar aterriza en /entrega-comision · aterrizó en /entrega-comision
CONFORME · los 5 pasos pasaron
PASA  6. El servicio de datos dice que el trabajo quedó en Approved —«Finalizado»—
CONFORME · el botón de aprobar hace lo que dice
```

## Por qué las 511 pruebas no lo veían

Ninguna abre un navegador. El defecto era que el manejador `@onclick` **no llegaba al
cliente**, y eso no lo ve ninguna prueba por servidor: el servicio de datos nunca recibía
la petición, así que no había nada que verificar del lado que las pruebas miran.
