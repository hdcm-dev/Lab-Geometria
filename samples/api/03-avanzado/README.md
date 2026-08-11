# Sample `api/03-avanzado` — Composición de raíz y arranque en dos fases: qué pasa antes de la primera petición

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Nivel:** Avanzado
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-03`](../../../SDD/Docs/Proyectos/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/api/03-avanzado/run.sh
```

---

## 1. Objetivo del sample

Demostrar lo que este proyecto de código tiene de propio y ningún otro del producto tiene: **la composición de raíz** —conectar los **cuatro** puertos con sus adaptadores, en un solo lugar y tomando la configuración de afuera— y **el arranque en dos fases**, que deja el almacén en condiciones **antes** de atender la primera petición y **se detiene** en lugar de atender sobre un almacén dudoso.

## 2. Prerequisites

- **Entorno de desarrollo contenido del repositorio.**
- **El servicio construido y sin advertencias.**
- **Dos almacenes de partida**: uno en su estado de primer arranque y **uno con linaje desconocido**, que el propio sample compone y declara como compuesto por él.
- **La configuración provista por el entorno.** El sample no contiene ninguna dirección, ninguna clave de firma y ninguna ruta de almacén.
- **El servicio lo arranca el sample, no un paso previo**: acá el arranque **es** el objeto de la verificación.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `bash samples/api/03-avanzado/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-03` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-03` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
