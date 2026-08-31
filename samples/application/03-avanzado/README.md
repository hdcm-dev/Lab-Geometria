# Sample `application/03-avanzado` — El administrador: gobierno de las cuentas, revisión de la comisión, desenlace y reseteo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Nivel:** Avanzado
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 15 líneas coinciden con §6**, desde que el 2026-08-30 su documento pasó a 2.0. La línea que faltaba era del documento: nombraba un código del dominio donde correspondía el de la capa de aplicación. La comparación además dejó de estar detrás de una bandera: **el comando documentado ahora verifica**.
**Documento que la gobierna:** [`ejemplo-03-avanzado-aplicacion.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-aplicacion.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-04003` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/application/03-avanzado
```

---

## 1. Objetivo del sample

Demostrar todo lo que sólo el administrador puede hacer y **por qué esta capa es la que lo hace cumplir**: habilitar, bloquear, rehabilitar y dar de baja con confirmación escrita y arrastre; recorrer la entrega de la comisión sin ver un solo borrador; aprobar y rechazar desde `Pendiente`; y resetear la contraseña de un alumno sin perder ni la cuenta ni sus trabajos.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`.
- **Un prerequisito propio:** el sample **no** parte del estado que dejaron los otros dos: lo reconstruye desde cero con los **cuatro** dobles de puerto, para que se pueda ejecutar solo.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/03-avanzado`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá, y qué encontró

**El sample está implementado y corre. Catorce de las quince líneas coinciden.**

```bash
dotnet run --project samples/application/03-avanzado              # los cuatro actos
dotnet run --project samples/application/03-avanzado -- --verificar   # y la comparación
```

### La línea que no coincide, y es la misma divergencia que el `02-intermedio`

```
esperada: [2] Listado ... pedido por un alumno: rechazado SCOPE_REQUIRES_ADMINISTRATOR_ROLE
obtenida: [2] Listado ... pedido por un alumno: rechazado ADMINISTRATOR_ROLE_REQUIRED
```

**Los dos códigos existen.** El del dominio —`SCOPE_REQUIRES_ADMINISTRATOR_ROLE`, en `Work.cs`— y el
de esta capa —`ADMINISTRATOR_ROLE_REQUIRED`, en `ApplicationConditionCode`—. `ReviewCommissionWorksUseCase`
**corta con el suyo antes de tocar el repositorio**, y su propio comentario lo declara:

> *«FA-01: **sin consultar el repositorio**. CA-03 lo mide sobre el repositorio y no sobre la
> respuesta.»*

**Es deliberado y es la tercera vez que aparece la misma forma** —después de `EDIT_OUTSIDE_DRAFT` en
el `02-intermedio`—: **la capa tiene su propio código para una condición que el dominio también
nombra**, y el suyo llega primero porque su guarda corta antes. No es un defecto puntual: es un
patrón, y merece decidirse una vez para las tres.

**No se ajusta el snapshot y no se toca el código.**

### Tres defectos propios, corregidos y anotados

1. **El acto `[2]` contaba cuatro trabajos donde el contrato pide tres**, porque los trabajos extra
   del acto `[3]` se agregaban al principio. Ahora se agregan **después** del `[2]`, que es el que
   los cuenta.
2. **El acto `[4]` reseteaba sobre una cuenta que el `[1]` acababa de dar de baja**, y devolvía
   `ACCOUNT_NOT_FOUND`. Hacen falta **dos** cuentas bloqueadas: la que se da de baja y la que se
   resetea.
3. **El acto `[3]` pedía el desenlace sobre un trabajo terminal** cuando quería probar la falta de
   papel, y la guarda de terminalidad cortaba antes. Ahora usa uno en `Pendiente`.

**Los tres son del sample y ninguno del sistema**, y quedan escritos porque los tres producían una
salida plausible: un número que cierra, un rechazo que suena razonable y un código que existe.
