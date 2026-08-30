# El almacén de linaje desconocido

**Este almacén lo compone el sample, y esta constancia existe para que nadie lo lea como un dato del producto.** No es la captura de un almacén real, no viene de ninguna máquina y no reproduce ningún incidente registrado: es una **condición provocada a propósito** para medir qué hace el arranque cuando se la encuentra.

## Cómo se compone

Un archivo SQLite con **una tabla que se parece a la del producto y ningún registro de linaje**:

```sql
CREATE TABLE Account (Id TEXT NOT NULL PRIMARY KEY);
```

Eso es todo. No hay `__EFMigrationsHistory`, así que el arranque cree que el almacén está vacío y trata de aplicar la primera transformación; la transformación crea `Account`, que ya existe, y el proveedor se planta.

## Por qué ésa y no otra forma de romperlo

**Porque es la forma en que el defecto aparece de verdad.** Un archivo corrupto o truncado se detecta solo y nadie duda de qué pasó. Lo peligroso es el almacén que alguien **tocó por fuera** —una migración aplicada a mano, una restauración parcial, un `CREATE TABLE` de prueba que quedó—: tiene estructura verosímil y no dice de qué versión viene. Es el caso en el que atender peticiones sería peor que no atender ninguna, y es el que `US-00028` manda detener.

## Lo que el sample mide sobre él

1. Que el arranque **se detiene**, y que durante todo el intento las peticiones atendidas son **cero**.
2. Que el mensaje del arranque detenido **no lleva la ruta del archivo, ninguna dirección ni una traza**. Es `RA-03` en el peor momento posible: quien lee ese mensaje está diagnosticando, y es cuando más tienta incluirlas.

**El archivo se crea y se borra dentro de la corrida.** No queda en el árbol ni en el directorio del almacén de trabajo.
