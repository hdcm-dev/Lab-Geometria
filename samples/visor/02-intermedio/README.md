# Sample `visor/02-intermedio` — Árbol y escena sincronizados por índice, y ninguna pieza que desaparezca sin aviso

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Intermedio
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-02-intermedio.md) 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-14`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify
```

---

## 1. Objetivo del sample

Demostrar las dos capacidades que convierten al visor en instrumento didáctico y no en una escena bonita: que el árbol del texto y la escena **se sincronizan por índice** sin traducir identidades, y que **ninguna pieza desaparece sin quedar enumerada** con su índice y su código. Es la segunda de las tres partes del sample **S-1**.

## 2. Prerequisites

- Los mismos cinco ítems del sample `01-basico`, sin agregados de herramienta.
- **Un agregado de datos:** los **cinco** textos de escenario se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, en archivos `.txt`, incluido el de `E-2`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/02-intermedio run verify`.
4. Para mirarlo a mano, abrir `samples/visor/02-intermedio/index.html` y elegir un escenario del selector.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-02` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-14` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
