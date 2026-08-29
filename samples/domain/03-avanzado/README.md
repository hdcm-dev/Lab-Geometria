# Sample `domain/03-avanzado` — Acceso, alcance del administrador y desenlace, con la superficie tipada bajo inspección

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Avanzado
**Estado de esta carpeta:** **IMPLEMENTADO y VERIFICADO el 2026-08-29**, en la Fase I, incremento 2. Su contrato cumple y la comparación contra el snapshot de §6 devuelve CONFORME
**Documento que la gobierna:** [`ejemplo-03-avanzado-dominio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-dominio.md) — **la ruta se corrigió el 2026-08-29**: apuntaba a `SDD/Docs/Proyectos/GeometriaFactory-Domain/`, que la consolidación de las unidades de entrega retiró, y el documento se renombró con el sufijo del proyecto de código 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-02003` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/domain/03-avanzado
```

---

## 1. Objetivo del sample

Demostrar las tres decisiones que el dominio toma sobre un trabajo ya enviado —quién accede a él, qué ve el administrador y cómo se cierra—, junto con la propiedad estructural que sostiene todo lo anterior: que **ninguna condición prevista viaja como excepción**.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`.
- **Acceso de lectura al archivo de proyecto de la biblioteca**: el acto `[8]` cuenta sus referencias declaradas.
- **Sin fijar el reloj del entorno**: el acto `[9]` corre dos veces seguidas y compara los resultados.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/03-avanzado`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

**El sample está implementado y corre.** El árbol es el que declara la §5 del documento que gobierna
esta carpeta, sin desviarse.

```bash
dotnet run --project samples/domain/03-avanzado              # ejecuta el recorrido
dotnet run --project samples/domain/03-avanzado -- --verificar   # y lo compara contra §6
```

**No entra en `GeometriaFactory.sln` a propósito**, como los otros dos: su ensamblado contaría en la
cobertura de `QG-00003` y movería un número que mide otra cosa.

**Y coincidió con su snapshot a la primera**, que es lo que se estaba verificando: §6 se escribió
**antes** que este código y el código lo reprodujo sin que hubiera que tocar el contrato. La fila de
sensado de deriva quedó en `Verificado` con **cero deriva**.
