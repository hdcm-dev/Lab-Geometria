# Sample `contracts/03-avanzado` — Error, desenlace y reseteo: quince códigos vivos y una frontera que no filtra

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Nivel:** Avanzado
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Contracts/10-Examples/ejemplo-03-avanzado.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-03`](../../../SDD/Docs/Proyectos/GeometriaFactory-Contracts/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/contracts/03-avanzado
```

---

## 1. Objetivo del sample

Demostrar las tres familias que cierran el contrato —error, desenlace y reseteo— y las dos inspecciones que sostienen la frontera entera: que el conjunto cerrado de códigos tiene **quince vivos** sobre **dieciocho** emitidos, con **tres** retirados que no se reciclan, y que **ningún** tipo del ensamblado habilita al navegador a invocar el servicio de datos.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`, y **sin servicio de datos levantado**.
- **Acceso de lectura al archivo de proyecto del ensamblado**: el acto `[9]` cuenta sus referencias hacia `GeometriaFactory-Domain`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/contracts/03-avanzado`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-03` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-03` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
