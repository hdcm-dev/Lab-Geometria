# Sample `application/02-intermedio` — Los ocho trabajos del alumno: carga, envío interpretado, consulta y retiro

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Nivel:** Intermedio
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 14 líneas coinciden con §6**, desde que el 2026-08-30 su documento pasó a 2.0. La línea que faltaba era del documento: nombraba el código de la segunda barrera sobre un recorrido que se detiene en la primera. La comparación además dejó de estar detrás de una bandera: **el comando documentado ahora verifica**.
**Documento que la gobierna:** [`ejemplo-02-intermedio-aplicacion.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-aplicacion.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-04002` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/application/02-intermedio
```

---

## 1. Objetivo del sample

Demostrar el ciclo del trabajo tal como esta capa lo orquesta, con los **ocho** escenarios reales del `PRODUCT-INTAKE` §20 recorridos uno por uno: cargar el trabajo con su texto original íntegro, enviarlo interpretando su texto **por el puerto** y no acá, consultar lo propio con los cuatro estados distinguibles, y retirarlo sólo desde `Borrador`.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`, sin agregados.
- **Un prerequisito de datos:** el doble del puerto de validación devuelve, para cada escenario, **el resultado de interpretación que la sección «qué verificar» de ese escenario declara** en el `PRODUCT-INTAKE` §20. **No se compone ningún resultado nuevo.**

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/02-intermedio`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá, y qué encontró

**El sample está implementado y corre. Trece de las catorce líneas del snapshot coinciden.**

```bash
dotnet run --project samples/application/02-intermedio
dotnet run --project samples/application/02-intermedio -- --verificar   # y la comparación
```

### La línea que no coincide

```
esperada: [Reedicion] Trabajo fuera de Borrador: rechazado EDIT_OUTSIDE_DRAFT
obtenida: [Reedicion] Trabajo fuera de Borrador: rechazado OPERATION_OUTSIDE_DRAFT
```

**Los dos códigos existen y describen la misma situación desde dos guardas distintas.**
`Work.Edit()` rechaza con `EDIT_OUTSIDE_DRAFT`, y `LoadAndEditOwnWorkUseCase.EditAsync` **nunca
llega a llamarlo**: antes resuelve el acceso con `ResolveStudentAccess(…, Edit)`, que rechaza con
`OPERATION_OUTSIDE_DRAFT`.

**Y ese orden es deliberado.** `ADR-04004` decide que las comprobaciones se ejercen **en un orden
fijo y antes de cualquier escritura**, de modo que la guarda de acceso corta primero **por diseño**.
El código hace lo que el ADR manda; lo que quedó desactualizado es **§6 de este contrato**, que
esperaba el código del dominio.

**No se ajusta el snapshot y no se toca el código.** Es una decisión del Product Owner con dos
salidas legítimas: corregir §6 al código que la guarda emite —lo más probable, porque el orden es
el que el ADR fija—, o hacer que la capa **transporte el código específico del dominio** cuando lo
haya. La segunda no es absurda: `EDIT_OUTSIDE_DRAFT` dice **qué** se intentaba, y
`OPERATION_OUTSIDE_DRAFT` sólo dice que no procedía.

### Dos desvíos declarados de §5

El árbol enumera **tres dobles** y esta carpeta trae **cuatro**: `ConsultOwnWorksUseCase` declara el
puerto de cuentas en su constructor —para poner el correo y el nombre de la persona dueña en el
detalle— y sin ese doble los actos `[E-7]` y `[Consulta]` no se pueden recorrer. **Se agrega un
archivo y no se renombra ni se quita ninguno de los que §5 declara.**
