# La escena acompaña el tamaño, y la compuerta que la cuida

`resize` estaba **exportado por el visor y nadie lo llamaba**. Al girar el teléfono, la
escena quedaba con la medida vieja.

## Los dos lados, medidos
```text
SIN observador  lienzo 702 px en un recuadro de 392 px   desvío 79.1 %   NO CONFORME
CON observador  lienzo 390 px en un recuadro de 392 px   desvío  0.5 %   CONFORME
```

## Y la compuerta tuvo que corregirse DOS veces, las dos por probarla fallando

| intento | criterio | qué pasó |
| --- | --- | --- |
| 1 | «que ocupe más del 90 % del recuadro» | **dio CONFORME con el defecto puesto**: el lienzo había quedado *grande* —104 %—, no chico |
| 2 | «que coincida, 5 % de tolerancia», 1280 → 700 | **dio CONFORME**: el desvío era 4.5 %, y el caso demasiado suave para separar |
| 3 | «que coincida, 2 % de tolerancia», 1280 → **420** | separa limpio: **79.1 % contra 0.5 %** |

Un arreglo correcto con una compuerta que no lo cuida deja pasar la próxima regresión sin
que nadie se entere. **La compuerta se probó fallando tres veces hasta que sirvió.**
