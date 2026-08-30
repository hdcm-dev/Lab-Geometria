# Sample `infrastructure/02-intermedio` — El almacén: guardar, recuperar con el recorte ya decidido, retirar y arrastrar

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Nivel:** Intermedio
**Estado de esta carpeta:** **Implementado.** Corre en 0 y con 0 excepciones; **9 de 14 líneas coinciden con §6** y las otras 5 son divergencias declaradas (abajo).
**Documento que la gobierna:** [`ejemplo-02-intermedio-infraestructura.md`](../../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/10-Examples/ejemplo-02-intermedio-infraestructura.md) 1.0, del que este README es la copia corta de §1, §3 y §4
**Contrato de verificación:** `VER-02`, declarado en la §9 de ese documento
**Sonda de sensado:** `SD-02`, en estado `Sin verificar`

**Comando previsto:**

```bash
dotnet run --project samples/infrastructure/02-intermedio
```

---

## 1. Objetivo del sample

Demostrar la otra mitad de esta capa: la que **sí abre el almacén**. Materializar un trabajo con sus piezas, sus componentes y sus observaciones; resolver la consulta **con el recorte ya trasladado al pedido**; devolver el listado **sin componentes ni texto original** frente al detalle que sí los lleva; retirar físicamente un trabajo; y arrastrar todos los trabajos de una cuenta dada de baja, todo o nada.

## 2. Prerequisites

- Los mismos cuatro ítems del sample `01-basico`.
- **Un almacén en su estado de primer arranque**, obtenido con el guion de reinicio del repositorio. **La ruta del almacén llega de configuración y no está escrita en el sample.**

## 3. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/02-intermedio`.
5. Comparar la salida con §6 del documento que gobierna esta carpeta.

## 4. Qué hay acá

Los cinco actos de §5, contra **SQLite de verdad** con las migraciones del producto aplicadas por `StorePreparation`. **Sin dobles**: los dos adaptadores del almacén son los del producto.

**Las interpretaciones no se recalculan.** `Escenarios/Interpretaciones.cs` trae congelada la salida que el sample `01-basico` produjo sobre estos mismos tres textos. Este sample mira **qué se guarda y cómo se recupera**; si el intérprete corriera acá, una diferencia en la salida no diría cuál de las dos capas se movió.

**Corre sobre un archivo propio y lo borra al terminar.** Es un apartamiento declarado del paso 3 de la §3: dos de los cinco actos son destructivos, y el 2026-08-15 este producto ya perdió la cuenta de administrador del Product Owner con una rutina destructiva y una base de trabajo compartiendo ruta. La llave `ConnectionStrings__Store` **tiene que estar declarada** —sin ella el sample se detiene—; lo que no se hereda es el nombre del archivo. El motivo largo está en `Almacen.cs`.

## 5. Las cinco divergencias contra §6

Nueve de las catorce líneas coinciden. Las cinco que no, coinciden entre sí en algo: **§6 nombra códigos que no existen en el árbol**, y en cuatro de los cinco casos la conducta que describe sí se cumple, en otra capa y con otro nombre.

| # | §6 espera | El árbol tiene | Dónde |
| --- | --- | --- | --- |
| `D-1` | `QUERY_WITHOUT_DECLARED_SCOPE` | **nada, y a propósito**: el puerto no declara ninguna operación de listado sin recorte, de modo que la condición no tiene camino que la produzca | `IWorkRepository`, dicho por escrito |
| `D-2` | `PARTIAL_DELETION_NOT_ALLOWED` | `DELETION_WITHOUT_WORK_CASCADE` | Dominio, `Account.AdmitDeletion` |
| `D-3` | `ADMINISTRATOR_UNIQUENESS_VIOLATED` | `ADMINISTRATOR_ALREADY_CONFIGURED` | Dominio, `Account.ConfigureAdministrator` |
| `D-4` | `WRITE_REWRITES_ORIGINAL_JSON` | `ORIGINAL_JSON_ALTERED` | Dominio, `Work.Edit` |
| — | `Rechazos tipados: 5` | `4` | consecuencia directa de `D-1` |

**`D-1` es la que más dice, y es a favor del producto.** §6 llama a esa línea «la razón de ser de este sample»: quería ver el adaptador rechazando una consulta sin recorte. El producto hizo algo más fuerte —que la consulta sin recorte **no se pueda pedir**— y esa decisión está escrita en el puerto desde antes. Un rechazo en tiempo de ejecución se puede alcanzar y hay que probarlo; una operación que no existe no compila. El sample mide la segunda y lo dice, en lugar de fabricar el rechazo para que el snapshot cierre.

**`D-2`, `D-3` y `D-4` son el mismo movimiento tres veces.** Las tres condiciones se corrieron del adaptador hacia el dominio, y al mudarse se quedaron con el nombre de lo que impiden y no con el de lo que evitan. Ninguna conducta falta: las tres se ejercitan y las tres rechazan.

**El snapshot se transcribió sin tocar una coma.** `tests/SalidaEsperada.cs` lleva las catorce líneas de §6 tal cual, con la lista de divergencias aparte. Reescribirlo para que diera CONFORME habría convertido al sample en una copia de sí mismo.

## 6. Dos cosas que el sample aprendió sobre el adaptador

Ninguna es un defecto; las dos son consecuencias del diseño que sólo se ven corriéndolo, y por eso quedan escritas donde pasan.

- **Leer y retirar van en la misma unidad de trabajo.** Las observaciones y los componentes son colecciones poseídas con clave sombra —existen en el esquema y no en el tipo del dominio—, así que un trabajo leído en otro contexto llega sin ellas y el adaptador no tiene qué borrar. El servicio cumple la condición sola, porque lee y escribe dentro del alcance de una petición.
- **Un contexto de larga vida no es «una unidad de trabajo por operación».** `ADR-06002` dice una por operación, y un contexto que sobrevive a varias hace estallar la segunda con el seguimiento de identidad de EF. El sample abre uno por acto.
