# Sample `infrastructure/01-basico` — Leer el texto del alumno y verificar sus números, sin abrir el almacén

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Nivel:** Básico
**Estado de esta carpeta:** **IMPLEMENTADO y VERIFICADO el 2026-08-29.** Sus trece líneas coinciden con el snapshot **a la primera**
**Documento que la gobierna:** [`ejemplo-01-basico-infraestructura.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-infraestructura.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-06001` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/infrastructure/01-basico
```

---

## 1. Objetivo del sample

Demostrar la mitad de esta capa que **no toca el almacén**: leer el texto que el programa del alumno emite de verdad, con las **cuatro** tolerancias `T1` a `T4`; reconstruir las piezas con su posición y sus componentes; derivar `Area` y `Volumen` y compararlos con los declarados con tolerancia **0.01** y operador **estricto**; y emitir la observación con lo que hace falta para ubicarla.

## 2. Prerequisites

- **.NET 10**; entorno de desarrollo contenido del repositorio; etapa `a` cerrada; Linux.
- **Sin almacén, sin red y sin ninguna otra pieza del producto.**
- **Un prerequisito de datos:** los ocho textos se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, en archivos `.txt`.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/infrastructure/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

**El sample está implementado y sus trece líneas coinciden.**

```bash
dotnet run --project samples/infrastructure/01-basico              # los ocho escenarios
dotnet run --project samples/infrastructure/01-basico -- --verificar   # y la comparación
```

**Corre contra el intérprete REAL y sin un solo doble**, que es lo contrario del sample de la capa
de aplicación — y por eso los dos existen. Allá el intérprete era un doble que devolvía lo
declarado, para que se viera que **la capa no interpreta**; acá es el componente de verdad, porque
lo que hay que ver es **qué interpreta**.

**Y coincidió a la primera**, sin tocar el contrato. Las tres tolerancias que el snapshot nombra
—`T1` la clave leída con otro nombre, `T2` las comas finales, `T3` las caras de dos tipos— salieron
tal como §6 las describía desde el 2026-08-11.
