# Sample `web/01-datos-seed` — Datos seed: la comisión desde la que arranca el guion de demostración

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Nivel:** Básico
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 13 líneas coinciden con §6**, sin ninguna divergencia.
**Documento que la gobierna:** [`ejemplo-01-datos-seed.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/10-Examples/ejemplo-01-datos-seed.md) 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-62`, en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/web/01-datos-seed/run.sh
```

---

## 1. Objetivo del sample

Dejar la comisión en un **estado de partida reproducible** para que el guion de demostración de cada etapa arranque siempre desde lo mismo, y verificarlo **sin pasar por la pantalla**: una cuenta de administrador, dos cuentas de alumno, y los **ocho** escenarios del `PRODUCT-INTAKE` §20 ya enviados como trabajos, con los cuatro estados representados.

## 2. Prerequisites

- **Entorno de desarrollo contenido del repositorio.**
- **El servicio de datos levantado**, sobre un almacén llevado a su estado de primer arranque.
- **La dirección del servicio de datos tomada de configuración.** No se escribe ninguna dirección concreta en el sample.
- **Ninguna credencial real.** Las identidades del seed son valores evidentemente ficticios y se declaran como tales.
- **No hace falta navegador y no hace falta la pieza pública construida.**

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio de datos: `bash scripts/run-api.sh`.
4. Ejecutar el sample: `bash samples/web/01-datos-seed/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Un punto de entrada único que **siembra y verifica**: un docente, dos alumnos, los ocho escenarios enviados y dos resueltos. Contra el servicio de datos corriendo, con almacén propio del sample.

**No hace falta navegador ni la pieza pública construida**, y es deliberado: lo que este sample deja es el **dato**. Lo que la pieza pública hace con él es materia del guion de demostración.

**Ni la dirección ni ninguna credencial real están escritas.** La dirección llega en `GF_API_BASE` (`ADR-10007`); las contraseñas se producen al correr con material impredecible y la provisoria la devuelve el servicio. `identidades.env.ejemplo` lleva **sólo nombres y correos ficticios** sobre `ejemplo.edu`, que es la forma reservada en castellano para exactamente esto.

## 5. Qué queda sembrado, y por qué así

| Estado | Cuántos |
| --- | --- |
| Pendiente | 4 |
| Borrador | 2 |
| Aprobado | 1 |
| Rechazado | 1 |

**Las dos líneas del listado de la comisión son la razón de ser del seed.** El alumno ve **8** y el administrador **6**: la diferencia son exactamente los dos borradores —los de `E-5` y `E-8`—, y `RN-10011` no admite que se vea ninguno. Un seed que dejara los ocho en el mismo estado haría invisible esa diferencia justo en el dato con el que después se demuestra el producto.

**Dos resueltos y no ninguno**, por lo mismo: sin un aprobado y un rechazado, la pieza pública no tiene con qué mostrar el desenlace.

**El par `E-3` contra `E-4` está a propósito.** Mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra: el primero produce una advertencia con su par de valores —`36.00` contra `54.00`— y el segundo **cero** observaciones. Es el contraste que la superficie de envío tiene que saber presentar, y el que `SD-10033` protege exigiendo que los dos valores se muestren sin reformatear.

## 6. `RN-10008`, medido donde se puede medir

`texto original identico al enviado: si` no es una promesa: el sample **envía el texto y lo vuelve a leer del detalle**, y compara contra el mismo literal que mandó. Si algo lo normalizó en el camino —el borde del servicio, el transporte o el propio sample— esta línea lo dice **antes de que nadie abra una pantalla**.

Se comprueba sobre `E-2`, que es el que tiene las dos comas finales: es el que se rompe primero si alguien reformatea. El texto viaja carácter por carácter — `datos/escapar.awk` escapa lo que JSON exige y **nada más**: no reordena, no compacta y no reindenta. `SD-10036` declara deriva mayor, sin gradación, ante cualquier normalización.

## 7. Dos errores de medición que el sample encontró en sí mismo

Ninguno era del producto, y los dos quedan escritos donde pasaron porque los dos son fáciles de repetir.

- **Contar `"kind":` en el detalle daba treinta y seis observaciones donde hay una.** El detalle trae también el árbol del texto, y sus nodos llevan un `kind` propio. Contar una clave que dos estructuras comparten mide las dos. Ahora se cuenta por `piecePosition`, que es de las observaciones y sólo de ellas.
- **Contar los estados partiendo el listado en filas daba `Aprobado=0`** mientras el renglón de al lado —que contaba por literal— decía que había dos trabajos con desenlace. Dos mediciones de lo mismo que no coincidían, y la que estaba mal era la más elaborada: partía por `}},{{` y leía el estado de cada trozo con una expresión codiciosa, así que un trozo con dos elementos contaba uno solo.
