# Por qué hay dos archivos de datos y no uno

`E1.txt` es el escenario del `PRODUCT-INTAKE` §20.E-1, **transcripto sin modificación**. Es el dato de origen y la razón por la que los números de `E1-piezas.json` son los que son.

`E1-piezas.json` es **ese mismo escenario ya reconstruido en piezas**, que es lo que el visor recibe.

## La diferencia no es de comodidad

**El visor ya no recibe el texto del alumno.** `loadPieces` se llamaba `loadJson` y lo recibía; cambió el **2026-08-16** por `ADR-08006`, y el nombre cambió junto con la firma porque una función que se llama «cargar JSON» y recibe otra cosa promete lo que no cumple. Quien reconstruye es el laboratorio, del lado del servicio.

**Este sample no reconstruye nada**, y es deliberado: lo que verifica es **qué dibuja el visor**, no qué interpreta el laboratorio. Si la reconstrucción corriera acá, una diferencia en la salida no diría cuál de las dos piezas se movió. Es el mismo criterio que `samples/infrastructure/02-intermedio` aplica con sus interpretaciones congeladas.

**Los valores no se inventaron.** Salen de correr el intérprete real del producto sobre `E1.txt`, que es lo que el sample `infrastructure/01-basico` hace y publica.
