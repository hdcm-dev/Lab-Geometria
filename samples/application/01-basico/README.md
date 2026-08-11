# Sample `application/01-basico` — La cuenta entra al laboratorio: alta, administrador, credencial y la guarda que corta primero

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Application/10-Examples/ejemplo-01-basico.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-01`](../../../SDD/Docs/Proyectos/GeometriaFactory-Application/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/application/01-basico
```

---

## 1. Objetivo del sample

Demostrar el camino de entrada al laboratorio tal como esta capa lo orquesta: constituir la cuenta de un alumno sin credencial y en situación pendiente, configurar la única cuenta de administrador mientras no exista ninguna, resolver la admisibilidad de un ingreso con su motivo, y reemplazar la credencial propia.

## 2. Prerequisites

- **.NET 10**; entorno de desarrollo contenido del repositorio; etapa `a` cerrada; Linux.
- **Sin servicios externos, sin base de datos y sin frontera de proceso**: los **cuatro** puertos se satisfacen con dobles que viven dentro del sample.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-01` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
