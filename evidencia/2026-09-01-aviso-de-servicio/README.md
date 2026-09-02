# El aviso de servicio, en sus dos estados

Verificado sobre la publicación real, deteniendo el servicio de datos con el panel abierto:

```text
CON EL SERVICIO EN PIE  → [en-linea] Servicio de datos en línea · comprobado a las 02:40:50
CON EL SERVICIO CAIDO   → [caido]    El laboratorio no está alcanzando sus datos.
                                     La causa más frecuente en este despliegue es que haya
                                     cambiado la dirección pública del laboratorio. Hay que
                                     actualizarla en el secreto de publicación y volver a
                                     publicar. Nada de lo cargado se pierde.
```

**El texto cambia según quién mira**, porque la acción es distinta: el docente puede arreglarlo y
el alumno no.

## Lo que no cubre, dicho antes de que lo pregunten

Si el servicio está caído **antes** de entrar, no hay sesión posible y lo que se ve es la pantalla
de ingreso con su propio aviso degradado, no este indicador. Cubre el caso real —el servicio se
cae con la persona ya adentro— y no pretende cubrir el otro.
