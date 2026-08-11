# Sample `web/01-datos-seed` — Datos seed: la comisión desde la que arranca el guion de demostración

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Nivel:** Básico
**Estado de esta carpeta:** **Esqueleto — sin código.**
**Documento que la gobierna:** [`ejemplo-01-datos-seed.md`](../../../SDD/Docs/Proyectos/GeometriaFactory-Web/10-Examples/ejemplo-01-datos-seed.md) 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** [`SD-62`](../../../SDD/Docs/Proyectos/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), en estado `Sin verificar`

**Comando previsto:**

```bash
bash samples/web/01-datos-seed/run.sh
```

---

## 1. Objetivo del sample

Dejar la comisión en un **estado de partida reproducible** para que el guion de demostración de cada etapa arranque siempre desde lo mismo, y verificarlo **sin pasar por la pantalla**: una cuenta de administrador, dos cuentas de alumno, y los **ocho** escenarios del `PRODUCT-INTAKE` §20 ya enviados como trabajos, con los cuatro estados representados.

## 2. Prerequisites

- **Entorno de desarrollo contenido del repositorio.**
- **El servicio de datos levantado**, sobre un almacén llevado a su estado de primer arranque.
- **La dirección del servicio de datos tomada de configuración.** No se escribe ninguna dirección concreta en el sample.
- **Ninguna credencial real.** Las identidades del seed son valores evidentemente ficticios y se declaran como tales.
- **No hace falta navegador y no hace falta la pieza pública construida.**

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio de datos: `bash scripts/run-api.sh`.
4. Ejecutar el sample: `bash samples/web/01-datos-seed/run.sh`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay hoy acá, y qué falta

Hoy esta carpeta tiene **sólo este README**. La carpeta se crea en la **pasada de diseño** de `Rules-Examples.md` §0.2, que le asigna exactamente esto: la carpeta esqueletada, con su README local y su comando previsto. El código del sample lo produce la **pasada de ejecución**, durante la codificación.

**El comando previsto todavía no resuelve, y esta carpeta no promete lo contrario.** Es la consecuencia declarada de que el sample no esté implementado: el campo `evidencia` del contrato `VER-01` dice `No verificado — sin código`, sin fecha y sin salida, y la fila `SD-62` de la matriz de sensado nace en `Sin verificar`. Ninguna corrida se afirma acá.

**Qué va a vivir acá cuando la pasada de ejecución corra.** El árbol de archivos que el sample va a tener está declarado en la §5 del documento que gobierna esta carpeta, y la salida exacta que va a producir, en su §6. Los dos se escribieron antes que el código, a propósito.
