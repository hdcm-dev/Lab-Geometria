# Sample `infrastructure/02-intermedio` — El almacén: guardar, recuperar con el recorte ya decidido, retirar y arrastrar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Nivel:** Intermedio
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-02-intermedio.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/10-Examples/ejemplo-02-intermedio.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-02`](../../../SDD/Docs/Proyectos/GeometriaFactory-Infrastructure/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/infrastructure/02-intermedio
```

---

## 1. Objetivo del sample

Demostrar la otra mitad de esta capa: la que **sí abre el almacén**. Materializar un trabajo con sus piezas, sus componentes y sus observaciones; resolver la consulta **con el recorte ya trasladado al pedido**; devolver el listado **sin componentes ni texto original** frente al detalle que sí los lleva; retirar físicamente un trabajo; y arrastrar todos los trabajos de una cuenta dada de baja, todo o nada.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`.
- **Un almacén en su estado de primer arranque**, obtenido con el guion de reinicio del repositorio. **La ruta del almacén llega de configuración y no está escrita en el sample.**

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/02-intermedio`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-02` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-02` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
