# Sample `domain/01-basico` — Ciclo de vida de una cuenta, de la configuración del administrador a la admisibilidad

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Nivel:** Básico
**Estado de esta carpeta:** **IMPLEMENTADO el 2026-08-27.** Es el primer sample con código del producto, y con él la precondición dura de la Fase I (`Master-Prompt.md` §7.1) queda cumplida
**Documento que la gobierna:** [`ejemplo-01-basico-dominio.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-dominio.md) 1.0 — **la ruta se corrigió el 2026-08-27**: apuntaba a `SDD/Docs/Proyectos/GeometriaFactory-Domain/`, que la consolidación de las unidades de entrega retiró, y el documento se renombró además con el sufijo del proyecto de código, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-01` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, todavía en estado `Sin verificar`: actualizarla es de la Fase I, no de esta carpeta

**Comando previsto:**

```bash
dotnet run --project samples/domain/01-basico
```

---

## 1. Objetivo del sample

Demostrar el camino más corto que un consumidor recorre contra esta biblioteca: configurar la única cuenta de administrador, dar de alta un alumno, habilitarlo fijándole la credencial provisoria y preguntar si la cuenta admite acceso. Al terminar, quien lo ejecuta sabe cómo se invoca una operación del dominio, cómo llega el rechazo tipado cuando la operación no procede y por qué la admisibilidad es una puerta única y no una comprobación repartida.

## 2. Prerequisites

- **.NET 10**, la plataforma que el `PRODUCT-INTAKE` declara para los seis proyectos de código de la solución de código.
- **Entorno de desarrollo contenido del repositorio**: el host no tiene el SDK.
- **Etapa `a` del plan de entrega cerrada.**
- **Linux**, la plataforma del entorno contenido.
- **Sin servicios externos**: el proyecto de código declara **0** dependencias salientes.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/domain/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá, y qué encontró al correr

**El sample está implementado y corre.** El árbol es el que declara la §5 del documento que gobierna
esta carpeta, sin desviarse: `Program.cs` con los cuatro actos, `Recorrido/` con un archivo por acto
y `tests/SalidaEsperada.cs` con el snapshot de §6 y su comparación.

```bash
dotnet run --project samples/domain/01-basico              # ejecuta el recorrido
dotnet run --project samples/domain/01-basico -- --verificar   # y lo compara contra §6
```

**No entra en `GeometriaFactory.sln` a propósito.** Si entrara, su ensamblado contaría en la
cobertura de `QG-03` y movería un número que mide otra cosa.

### Y lo primero que hizo fue encontrar una divergencia, que es para lo que sirve

**Seis de las diez líneas coinciden con el snapshot; cuatro no**, y las cuatro por el mismo motivo:
**el sistema emite los códigos de condición en inglés y toda la documentación los nombra en
castellano.**

| Lo que §6 declara | Lo que el sistema emite |
| --- | --- |
| `ADMINISTRADOR_YA_CONFIGURADO` | `ADMINISTRATOR_ALREADY_CONFIGURED` |
| `DATO_OBLIGATORIO_AUSENTE` | `REQUIRED_FIELD_MISSING` |
| `CUENTA_PENDIENTE` | `ACCOUNT_PENDING` |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | `PASSWORD_CHANGE_PENDING` |

**No es un defecto del sample y no se corrige acá.** La forma castellana aparece **21 veces en el
corpus vivo y la inglesa ninguna**; la forma inglesa es la que viaja **por el cable**, desde
`ConditionCode` del dominio hasta `ErrorCode` de la capa de contratos y la traducción de la Api.
Elegir cuál de las dos es la buena **cambia el contrato público de errores**, y eso es del Product
Owner. Queda declarado en
[`../../../SDD/Docs/Audit/Evaluacion-Del-Codigo-2026-08-27.md`](../../../SDD/Docs/Audit/Evaluacion-Del-Codigo-2026-08-27.md).

**Por eso el snapshot de `tests/SalidaEsperada.cs` NO se ajustó al código.** Ajustarlo habría hecho
pasar la verificación decidiendo en silencio que el código le gana a veintiún documentos, que es
exactamente lo que un snapshot existe para impedir. El sample sale **0** —el recorrido funciona, las
nueve operaciones se invocan y las excepciones son cero— y `--verificar` sale **1** con el diff a la
vista, hasta que alguien decida.
