# Sample `domain/02-intermedio` — Un trabajo real del alumno: constitución, adopción de la interpretación y envío

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Intermedio
**Estado de esta carpeta:** **IMPLEMENTADO y VERIFICADO el 2026-08-29**, en la Fase I, incremento 2. Su contrato cumple y la comparación contra el snapshot de §6 devuelve CONFORME
**Documento que la gobierna:** [`ejemplo-02-intermedio-dominio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-dominio.md) — **la ruta se corrigió el 2026-08-29**: apuntaba a `SDD/Docs/Proyectos/GeometriaFactory-Domain/`, que la consolidación de las unidades de entrega retiró, y el documento se renombró con el sufijo del proyecto de código 1.1, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-02002` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

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

## 4. Qué hay acá

**El sample está implementado y corre.** El árbol es el que declara la §5 del documento que gobierna
esta carpeta, sin desviarse.

```bash
dotnet run --project samples/domain/02-intermedio              # ejecuta el recorrido
dotnet run --project samples/domain/02-intermedio -- --verificar   # y lo compara contra §6
```

**No entra en `GeometriaFactory.sln` a propósito**, como los otros dos: su ensamblado contaría en la
cobertura de `QG-00003` y movería un número que mide otra cosa.

**Y coincidió con su snapshot a la primera**, que es lo que se estaba verificando: §6 se escribió
**antes** que este código y el código lo reprodujo sin que hubiera que tocar el contrato. La fila de
sensado de deriva quedó en `Verificado` con **cero deriva**.
