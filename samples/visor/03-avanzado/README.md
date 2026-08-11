# Sample `visor/03-avanzado` — Las seis funciones sin backend, con los dos movimientos prendidos y el contador de red en cero

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Nivel:** Avanzado
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-03-avanzado.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/10-Examples/ejemplo-03-avanzado.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-15`](../../../SDD/Docs/Proyectos/GeometriaFactory-Visor/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify
```

---

## 1. Objetivo del sample

Demostrar el punto de extensión del producto entero: las **seis** funciones de la fachada, recorridas de punta a punta **sin ninguna pieza del backend**, con los dos movimientos automáticos prendidos y sostenidos, y con el contador de peticiones de red en **cero**. Es la tercera parte del sample **S-1**, y la que cierra su promesa.

## 2. Prerequisites

- Los mismos cinco ítems del sample `01-basico`.
- **Conductor de navegador capaz de declarar preferencia de movimiento reducido** del sistema: es el único doble admitido, y lo que se simula es el entorno del anfitrión.
- **Comprobación reproducible de texto sobre el archivo de guion generado**, porque el acto `[10]` inspecciona el **bundle generado** y no sólo la fuente.
- **Sin acceso a redes de distribución externas**: el acto `[11]` mide `PT-03` en esas condiciones y darle acceso invalidaría la medición.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido, con el conductor configurado para declarar preferencia de movimiento reducido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/03-avanzado run verify`.
4. Para mirarlo a mano, abrir `samples/visor/03-avanzado/index.html` y usar los dos controles de movimiento.
5. Comparar con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-03` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-15` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
