# Sample `domain/02-intermedio` — Un trabajo real del alumno: constitución, adopción de la interpretación y envío

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Intermedio
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Domain/10-Examples/ejemplo-02-intermedio.md) 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-02`](../../../SDD/Docs/Proyectos/GeometriaFactory-Domain/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/domain/02-intermedio
```

---

## 1. Objetivo del sample

Demostrar el ciclo de vida del trabajo dentro del dominio, con los datos reales que salen de la aplicación de escritorio de los alumnos: constituir un trabajo con su texto original íntegro, adoptar el conjunto de piezas y las observaciones que el consumidor interpretó afuera, y resolver el envío. Al terminar, quien lo ejecuta sabe por qué el dominio **adopta** una interpretación y no la produce, y por qué una advertencia deja pasar el trabajo mientras un error de validación lo retiene en `Borrador`.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`: **.NET 10**, entorno de desarrollo contenido, etapa `a` cerrada y Linux.
- **Un prerequisito de datos, no de herramientas:** los seis textos de escenario se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, en archivos `.txt`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/02-intermedio`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-02` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-02` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
