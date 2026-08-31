# Sample `application/01-basico` — La cuenta entra al laboratorio: alta, administrador, credencial y la guarda que corta primero

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Nivel:** Básico
**Estado de esta carpeta:** **Implementado.** Corre en 0 y **las 12 líneas coinciden con §6**, desde que el 2026-08-30 su documento pasó a 2.0. La línea que su contrato no cumplía era del documento y no del código: pedía que esta capa comprobara la marca de cambio de contraseña, que comprueba la guarda de la superficie HTTP. La comparación además dejó de estar detrás de una bandera: **el comando documentado ahora verifica**.
**Documento que la gobierna:** [`ejemplo-01-basico.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-01-basico-aplicacion.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-01`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-04001` de la `Matriz-Sensado-Deriva.md` de `GeometriaFactory-Api`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/application/01-basico
```

---

## 1. Objetivo del sample

Demostrar el camino de entrada al laboratorio tal como esta capa lo orquesta: constituir la cuenta de un alumno sin credencial y en situación pendiente, configurar la única cuenta de administrador mientras no exista ninguna, resolver la admisibilidad de un ingreso con su motivo, y reemplazar la credencial propia.

## 2. Prerequisites

- **.NET 10**; entorno de desarrollo contenido del repositorio; etapa `a` cerrada; Linux.
- **Sin servicios externos, sin base de datos y sin frontera de proceso**: los **cuatro** puertos se satisfacen con dobles que viven dentro del sample.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/application/01-basico`.
4. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá, y qué encontró

**El sample está implementado y corre.** Once de las doce líneas del snapshot de §6 coinciden.

```bash
dotnet run --project samples/application/01-basico              # ejecuta los cuatro actos
dotnet run --project samples/application/01-basico -- --verificar   # y lo compara contra §6
```

### La línea que no coincide, y por qué no se la ajustó

```
esperada: [4] Cuenta marcada pide listar sus trabajos: rechazado PASSWORD_CHANGE_PENDING
obtenida: [4] Cuenta marcada pide listar sus trabajos: PROCEDIÓ — la capa de aplicación no
          comprueba la marca
```

**`ConsultOwnWorksUseCase.ListAsync` no comprueba la marca de cambio pendiente.** Comprueba que haya
solicitante y lista. **Ningún caso de uso de la capa de aplicación la comprueba**: la comprobación
vive en `GeometriaFactory.Api/Endpoints/PendingPasswordChangeGuard.cs`, un intermediario de la capa
que expone.

**Y eso es exactamente la alternativa que `ADR-04004` descartó**, con su motivo escrito:

> *«Comprobaciones en la capa que expone, con esta capa confiando en lo ya verificado — se descarta
> porque desplazar la comprobación allá **la volvería inverificable con dobles**.»*

**Es lo que acaba de pasar.** Este sample intentó verificarla con dobles y no pudo, porque la
comprobación no está donde el ADR dice que está. El ADR predijo el síntoma y el sample lo produjo.

**Por eso el snapshot no se toca.** Ajustarlo dejaría el sample en verde y borraría la única
evidencia de que el código contradice una decisión aceptada. La línea queda fallando hasta que el
Product Owner decida si se mueve la guarda o se cambia el ADR.

### Un defecto propio, corregido y anotado

La primera versión del acto `[4]` **contaba esa línea como rechazo antes de mirar el resultado**, y
con eso el recuento final cuadraba con el snapshot **tapando** que la petición había procedido. Un
sample que fuerza su propio número deja de servir para lo único que sirve.

### Un desvío declarado de §5

El árbol de §5 enumera **dos dobles** y esta carpeta trae **tres**: el acto `[4]` ejerce la puerta a
través de una petición de listado, y esa petición entra por un caso de uso que declara el puerto de
trabajos en su constructor. Se **agrega** un archivo y no se renombra ni se quita ninguno de los que
§5 declara.
