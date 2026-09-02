# La ventana muerta, medida

El Product Owner reportó **cuatro veces** que «Aprobar» no hacía nada, con capturas del
botón apretado y el trabajo en `Pendiente`. Todas las verificaciones daban conforme.

## La causa, medida contra el anfitrión real

```text
página cargada a los 0.9 s
EL BOTON RESPONDIO a los 6.4 s desde la carga
```

**Cinco segundos y medio** en que la pantalla parece lista y no lo está: en somee
WebSockets no pasa, y la conexión de tiempo real tiene que fallar el intento antes de
replegarse a sondeo largo. El Product Owner apretaba en ese hueco.

## Por qué el banco no lo veía

Porque **esperaba doce segundos antes de apretar**. Medía el producto en condiciones que
ningún docente reproduce: nadie abre un trabajo y espera doce segundos mirando la pantalla.

Es la cuarta forma del mismo error en dos días: **el banco no se parecía a la persona.**

## Y el defecto silencioso que venía adentro

Lo que se escribiera en el comentario antes del enganche **no está en la copia del
servidor**, y al engancharse el marco redibuja con la suya —vacía—. El texto se perdía sin
aviso. Por eso el campo se inhabilita con los botones.

## El resultado

```text
Entorno: Production · esquema: https
PASA  1. El bloque de resolución se dibuja
PASA  2. Mientras el circuito no engancha, el control está inhabilitado Y lo dice · inhabilitado=true avisa=true
PASA  3. Cuando el circuito engancha, el control se habilita solo
PASA  4. Apretar «Aprobar» abre el diálogo de confirmación
PASA  5. El diálogo nombra el trabajo, declara la terminalidad y muestra el comentario · nombre=true terminalidad=true comentario=true
PASA  6. «Cancelar» cierra sin aplicar y el bloque sigue disponible
PASA  7. Confirmar aterriza en /entrega-comision · aterrizó en /entrega-comision
PASA  8. El trabajo resuelto declara su desenlace y SIGUE permitiendo retirar · sin decisiones=true con retirar=true declara=true fecha=true
CONFORME · los 8 pasos pasaron
PASA  9. El servicio de datos dice que el trabajo quedó en Approved —«Finalizado»—
CONFORME · el botón de aprobar hace lo que dice
```

---

## La misma ventana muerta estaba en otras dos pantallas

El barrido posterior encontró que el defecto **no era del bloque de resolución**, sino de toda
superficie interactiva:

| Superficie | Cómo envía | Estado |
| --- | --- | --- |
| `/aprovisionamiento-inicial` | `<form @onsubmit>` **sin** `method="post"` | **expuesta** — y es lo primero que hace un laboratorio nuevo |
| `/mi-contrasena` | ídem | **expuesta** |
| `/credencial-propia/cambio-obligado` | `method="post"` + `@formname` | a salvo: publica por POST de verdad |

Las dos expuestas **no tienen red de seguridad**: fuera del circuito el navegador recarga la
página y se lleva lo tipeado, sin crear nada y sin decir nada.

### El primer arranque, probado por pantalla por primera vez

```text
PASA 1. La pantalla de aprovisionamiento se dibuja en un laboratorio vacío
PASA 2. Inhabilitado Y lo dice mientras el circuito no engancha
PASA 3. Se habilita solo
PASA 4. Se crea el administrador y avanza a /ingreso
        el servicio de datos: {"administratorConfigured":true}
```
