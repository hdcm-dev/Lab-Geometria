# Sample `infrastructure/03-avanzado` — Los mecanismos que no guardan nada: credencial, provisoria, acceso firmado, reloj y arranque

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Nivel:** Avanzado
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/10-Examples/ejemplo-03-avanzado.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-03`](../../../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/infrastructure/03-avanzado
```

---

## 1. Objetivo del sample

Demostrar los cinco mecanismos que esta capa provee y que **ninguna otra puede proveer**: derivar una contraseña y verificar una credencial sin guardar nada en claro; producir la contraseña provisoria, no adivinable y sin repetirse; emitir el acceso firmado con sus **cuatro** reclamos; dar el sello del reloj **por un puerto**; y dejar el almacén en condiciones antes de la primera petición.

## 2. Prerequisites

- Los mismos ítems del sample `02-intermedio`, incluido el almacén en su estado de primer arranque.
- **Una clave de firma de prueba, tomada de configuración del entorno.** El sample no contiene ninguna clave.
- **La fuente de material impredecible del sistema**, disponible; el sample recorre también el caso en que no responde.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/03-avanzado`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-03` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-03` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
