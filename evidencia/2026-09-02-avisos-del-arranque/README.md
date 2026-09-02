# El producto lee sus propios avisos · `AB-2`

## Lo que costó no tenerlo

El anfitrión venía diciendo, **en la primera línea de cada arranque desde el primer despliegue**:

```text
warn: EphemeralXmlRepository[50]
      Using an in-memory repository. Keys will not be persisted to storage.
```

Nadie lo leyó nunca: el registro de salida estaba **apagado**. El defecto estuvo escrito todo el
tiempo, en un archivo que no existía.

## Lo que se dio vuelta

En vez de ir a leer el registro del anfitrión —que depende de un interruptor ajeno y de que
alguien se acuerde—, **el producto escucha a su propio marco y publica lo que oyó** en `/estado`.
Y la comprobación final de la publicación ya falla cuando esa página trae `class="failure"`, de
modo que **un aviso sin declarar tumba el despliegue**.

## Los dos lados, probados

```text
DECLARADO    → «Sólo avisos ya declarados. Nada nuevo que mirar.»   failure: False
SIN DECLARAR → «1 aviso(s) SIN DECLARAR al arrancar…»               failure: True
```

El segundo se probó **vaciando la tabla de declarados en una compilación descartable**: una
compuerta que no se probó bloqueando no es una compuerta.

## Las tres decisiones que la hacen sostenible

| | |
| --- | --- |
| **Sólo el arranque** | Un fallo de funcionamiento lo puede causar una pestaña vieja. Si contara, la publicación fallaría por algo que no es un defecto. |
| **Tres categorías** | Las que **fallan en silencio**. Se probaron `Hosting` y `Server` y se retiraron: emiten avisos del entorno —«Overriding address(es)» aparece en contenedor y no bajo IIS—. |
| **Declarado ≠ silenciado** | Un aviso declarado **se sigue mostrando, con su razón al lado**. Lo único que cambia es que no bloquea. |

## El apartamiento declarado que quedó

`No XML encryptor configured`: las claves quedan sin cifrar en disco. **Se intentó cifrarlas y se
retiró**, con motivo medido — el registro del propio anfitrión dice
`Neither user profile nor HKLM registry available`, de modo que el cifrado del sistema **fallaría
al crear la clave y habría roto el sitio para tapar un aviso**. Sin certificado en el laboratorio
—decisión declarada del Product Owner— no hay tercera vía.
