# La verificación en las condiciones del anfitrión

El 2026-09-01 el Product Owner reportó que el botón de aprobar seguía sin funcionar
en `aplicada.somee.com` DESPUÉS de entregar el arreglo. La verificación que lo daba por
cerrado corría en `Development` sobre HTTP: **el único entorno donde el producto no se usa**.

## Los cuatro defectos del banco, encontrados al hacerlo correr como el anfitrión

| # | Qué pasaba | Por qué importa |
| --- | --- | --- |
| 1 | La pieza pública llamaba al servicio de datos por HTTPS con certificado propio | El `HttpClient` lo rechaza y **todo** responde «no podemos llegar a tus datos». Producción no lo tiene: somee sirve HTTPS al frente y llama al API por HTTP. |
| 2 | Chromium no guardaba la marca de sesión | `ignoreHTTPSErrors` **silencia el aviso, no vuelve seguro el origen**, y un origen inseguro descarta cookies `Secure` — que es lo que la marca es en producción. |
| 3 | Se corría `dotnet run` en vez de la publicación | Fuera de `Development` **`/_framework/blazor.web.js` responde 404**: sin ese guion no hay circuito y NINGÚN componente interactivo funciona. |
| 4 | La raíz de contenido apuntaba al repositorio | El `wwwroot` publicado no se encontraba: hoja de estilos, guion de superficie y visor, todos 404. |

Los cuatro producían **el mismo síntoma que el defecto real** —botones dibujados y muertos—.
Un banco que no corre lo mismo que el anfitrión **inventa defectos propios y tapa los ajenos**.

## El resultado, ya en condiciones fieles

```text
Entorno: Production · esquema: https
PASA  1. El bloque de resolución se dibuja
PASA  2. Apretar «Aprobar» abre el diálogo de confirmación
PASA  3. El diálogo nombra el trabajo, declara la terminalidad y muestra el comentario · nombre=true terminalidad=true comentario=true
PASA  4. «Cancelar» cierra sin aplicar y el bloque sigue disponible
PASA  5. Confirmar aterriza en /entrega-comision · aterrizó en /entrega-comision
CONFORME · los 5 pasos pasaron
PASA  6. El servicio de datos dice que el trabajo quedó en Approved —«Finalizado»—
CONFORME · el botón de aprobar hace lo que dice
```

## Y lo que se midió contra el anfitrión real

```text
El circuito engancha en somee (sonda anónima sobre /estado):
   marca de lectura ANTES : 13:31:34
   marca de lectura DESPUES: 13:31:43   -> el @onclick corrió
   transporte: Long Polling (WebSockets no pasa en ese anfitrión)

El .dll desplegado contiene el cambio:
   data-gf-dialog-modal        PRESENTE
   resolution-confirm-heading  PRESENTE
   /entrega-comision           PRESENTE (6)
   /comision (ruta vieja)      AUSENTE
```
