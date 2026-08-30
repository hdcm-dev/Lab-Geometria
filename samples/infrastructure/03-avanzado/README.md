# Sample `infrastructure/03-avanzado` — Los mecanismos que no guardan nada: credencial, provisoria, acceso firmado, reloj y arranque

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Nivel:** Avanzado
**Estado de esta carpeta:** **Implementado.** Corre en 0 y con 0 excepciones; **12 de 18 líneas coinciden con §6** y las otras 6 son divergencias declaradas (abajo). **Las dos inspecciones de umbral cero dan 0, medidas.**
**Documento que la gobierna:** [`ejemplo-03-avanzado-infraestructura.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-03-avanzado-infraestructura.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-03`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-03`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/infrastructure/03-avanzado
```

---

## 1. Objetivo del sample

Demostrar los cinco mecanismos que esta capa provee y que **ninguna otra puede proveer**: derivar una contraseña y verificar una credencial sin guardar nada en claro; producir la contraseña provisoria, no adivinable y sin repetirse; emitir el acceso firmado con sus **cuatro** reclamos; dar el sello del reloj **por un puerto**; y dejar el almacén en condiciones antes de la primera petición.

## 2. Prerequisites

- Los mismos ítems del sample `02-intermedio`, incluido el almacén en su estado de primer arranque.
- **Una clave de firma de prueba, tomada de configuración del entorno.** El sample no contiene ninguna clave.
- **La fuente de material impredecible del sistema**, disponible; el sample recorre también el caso en que no responde.

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/03-avanzado`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Los cinco actos de §5 contra los componentes reales, más las dos inspecciones de umbral cero. **Un solo doble en todo el sample**: el reloj fijado del acto 4, que es precisamente lo que el puerto existe para permitir. `UtcSystemClock` se ejercita igual, en la primera mitad de ese mismo acto.

**Ni la clave de firma, ni la contraseña, ni la ruta del almacén están escritas en el código.** Las tres se producen o se leen en tiempo de ejecución: la contraseña sale de la fábrica del producto, la clave de firma de `RandomNumberGenerator`, y la ruta de `ConnectionStrings__Store`. No es prolijidad — es la condición que hace que la primera inspección **pueda** dar cero.

**El acto 5 crea dos almacenes propios y los borra, con sus laterales.** SQLite en modo WAL deja tres archivos y no uno; la primera versión de este sample borró sólo el `.db` y dejó el `-wal` y el `-shm` en el directorio del almacén de trabajo, que es justo lo que se había comprometido a no ensuciar.

## 5. Las seis divergencias contra §6

Doce de las dieciocho líneas coinciden. Las seis que no dicen **una sola cosa**, y es lo que este sample vino a encontrar:

> **§6 le pide a esta capa seis rechazos tipados. La capa declara dos, y el sample sólo puede exhibir uno.**

| # | §6 espera | El árbol tiene |
| --- | --- | --- |
| `D-1` | `PLAINTEXT_PASSWORD_MISSING` | **no existe** — `Derive` devuelve `null` y no dice por qué |
| `D-2` | `RANDOMNESS_SOURCE_UNAVAILABLE` | **existe, pero la condición no es provocable** desde el sample |
| `D-3` | `SIGNING_KEY_MISSING` | **no existe** — `Issue` devuelve `null` |
| `D-4` | `INCOMPLETE_CLAIMS` | **no existe, y es el mismo `null` que `D-3`** |
| `D-5` | `MIGRATION_NOT_APPLICABLE` | **no existe** — el arranque se detiene, con la excepción del proveedor |
| — | `Rechazos tipados: 6` | `1` |

**No es el mismo hallazgo que el del sample `02`.** Allá los códigos existían con otro nombre y en otra capa. Acá **no existen**, y las fallas viajan como `null`. Los dos únicos códigos tipados de la capa entera son `UNREADABLE_PASSWORD_HASH` y `RANDOMNESS_SOURCE_UNAVAILABLE`, y ninguno de los dos cubre nada de lo de arriba.

**`D-3` y `D-4` juntas son la más seria, y no por el nombre que falta.** `Issue` devuelve **el mismo `null`** cuando falta la clave de firma y cuando faltan reclamos —y en otros tres casos más—. No son la misma clase de falla: reclamos incompletos es un pedido mal armado, que el llamador puede corregir; clave de firma ausente es un **despliegue mal configurado**, que nadie que pida un acceso puede arreglar. `scripts/store-path.sh` cuenta que este producto ya eligió detenerse en el arranque ante configuración faltante, por exactamente este motivo. Acá el arranque sigue y la falla aparece, mucho después, como un acceso que no se emite.

**`D-2` se midió por la mitad, y se dice cuál.** Hacer fallar al generador criptográfico del sistema operativo no está al alcance del sample, y fabricar la falla con un doble mediría el doble. Lo que sí se puede medir es la mitad que importa: la regla no es «avisar cuando la fuente falla», es **no componer la provisoria por otro medio**. Se cuenta sobre la fuente del componente —`new Random(`, la hora como semilla, el identificador de la cuenta— y el resultado es `0`.

**`D-1` tiene una huella de que el nombre estuvo pensado**: el comentario de `Verify` cita `CONTRASENA_EN_CLARO_AUSENTE`, en prosa, para una constante que nadie escribió.

## 6. Las dos inspecciones, y por qué su umbral cero no es un adorno

Un umbral cero sin condición de medición es un criterio mal escrito. Las dos declaran sobre qué cuentan, y **fallan en lugar de informar cero** si no encuentran qué medir: una medición que dio cero y una que no se hizo no son lo mismo, y confundirlas convierte el umbral en decoración.

- **Sobre la fuente del sample** — clave de firma, contraseña real y ruta del almacén: **0**. Se cuenta sobre los `.cs` copiados a la salida por el `csproj`.
- **Sobre la salida producida** — contraseña en claro y valor derivado: **0**. Se cuenta sobre lo que un operador vería en una consola o en un registro, no sobre el snapshot esperado.

## 7. Una cosa que el sample aprendió corriéndolo

**El acceso emitido con el reloj fijado volvía inválido**, y no por la firma: `ValidationParameters` trae `ValidateLifetime` con treinta segundos de tolerancia, así que un sello del futuro hace que el acceso todavía no haya empezado a valer. El acto 3 usa el reloj del sistema; el acto 4 fija el suyo para mostrar que se puede. Queda escrito donde pasa, porque leído por arriba «reloj fijado» suena a lo más seguro de las dos opciones.
