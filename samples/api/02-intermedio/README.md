# Sample `api/02-intermedio` — La colección de peticiones reproducible: los ocho escenarios contra la superficie ensamblada

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Intermedio
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio.md) 1.0, del que este README es la copia corta de §1, §3 y §4
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

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-02` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-02` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
