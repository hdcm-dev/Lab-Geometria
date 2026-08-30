# Sample `api/02-intermedio` — La colección de peticiones reproducible: los ocho escenarios contra la superficie ensamblada

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Intermedio
**Estado de esta carpeta:** **Implementado.** Corre en 0; **22 de 23 líneas coinciden con §6** y la única que no es una contradicción del propio documento (abajo).
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-api.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-02`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/api/02-intermedio/run.sh
```

---

## 1. Objetivo del sample

**Es la muestra `S-2` del producto.** Recorrer la superficie ensamblada en el orden en que el producto se usa, con **los ocho escenarios del `PRODUCT-INTAKE` §20 como cuerpo**, y comparar cada respuesta con la que su propia fuente declara esperada.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`.
- **Un prerequisito de datos:** los ocho cuerpos se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, en archivos `.txt`, incluidas las **dos comas finales** y la clave `"Tapas"` de `E-2`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio: `bash scripts/run-api.sh`.
4. Ejecutar la colección: `bash samples/api/02-intermedio/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Los ocho pasos de `coleccion/`, recorridos en orden por `run.sh` — que es el orden de `CU-00012` §4 y no se elige acá. **Treinta y cinco peticiones contra el servicio corriendo, sin dobles.**

**Ni la dirección ni ninguna contraseña real están escritas.** La dirección llega en `GF_API_BASE`; las contraseñas se producen al correr y la provisoria la devuelve el servicio. Por eso `[datos] Cuerpos inventados: 0` es una medición y no una promesa: los ocho cuerpos se comparan byte a byte contra los escenarios del árbol, comas finales incluidas (`CA-02`).

**El almacén es propio del sample.** La colección aprueba, rechaza, elimina y resetea; correrla contra el almacén de trabajo se llevaría puesto lo que haya.

## 5. La única divergencia contra §6

| §6 dice | El árbol |
| --- | --- |
| `Pasos de la coleccion: 3 \| Peticiones: 34` | **8 pasos y 35 peticiones** |

**`§6` se contradice consigo mismo**: dice tres pasos, pero sus propias líneas van de `[1]` a `[8]` y §5 declara ocho archivos en `coleccion/`. El árbol recorre los ocho.

**La petición de más es la que hace falta para tener un segundo alumno**, y sin ella el paso 7 no se puede medir. Ver abajo.

## 6. Lo que el sample encontró corriéndose

**«Un trabajo ajeno» no se puede pedir con el administrador, y creer que sí invierte lo que la regla dice.** La primera versión usó el acceso del administrador como solicitante de la baja del trabajo de otro, y el servicio respondió **`204`**: la baja procedió. Es correcto — el administrador tiene alcance sobre los trabajos de la comisión— pero mide lo contrario de `RN-00003`, que habla de un trabajo que **no le pertenece a quien lo pide y sobre el que no tiene alcance**. Hizo falta dar de alta un **segundo alumno**, y recién con él aparece el `404`.

**Y el `404` es el punto entero.** Eliminar un trabajo propio fuera de `Borrador` responde `409` —el estado no lo permite—; eliminar uno ajeno responde `404`, porque el ajeno es **indistinguible del inexistente**. Colapsarlas en un `403` revelaría que ese trabajo existe, que es justo lo que la regla evita. Las dos líneas de `[7]` no son intercambiables, y sólo se distinguen si el solicitante del segundo caso es alguien sin alcance.

**Lo que la salida no declara, a propósito.** El recuento de observaciones de `E-7` no aparece: ninguna fuente lo declara para ese escenario y esta categoría no lo calcula. Es la misma abstención que `CU-00012` §6 declara en su fila, y el sample la respeta en vez de rellenarla.

**El contraste `E-3` contra `E-4` salió como está escrito.** El mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra: el primero advierte con su par de valores —`36.00` contra `54.00`—, el segundo produce **cero** observaciones. Un validador que advirtiera siempre pasaría el primero y fallaría el segundo.
