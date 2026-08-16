# Consolidación de casos de uso — decidida

**Documento:** Migracion-8.5-Consolidacion-Decidida.md
**Versión:** 1.2
**Fecha:** 2026-08-16
**Regla:** `Migracion-Rules.md` §4.3.2 paso 3
**Estado:** **Decidida. Se aplica sobre el árbol definitivo de la migración**
**Reemplaza a:** `Migracion-8.1-Deduplicacion-Propuesta.md`, que listaba 25 pares sin veredicto

---

## 1. Por qué los 25 pares no eran 25 decisiones

Agrupados por **capacidad** en lugar de por par, los casos de uso de `GeometriaFactory-Api` se ordenan
en cuatro capas, y el título de cada uno declara cuál:

| Capa de origen | Verbo que usa | Qué es en el modelo 8.x |
| --- | --- | --- |
| `Api` | «Exponer…» | El punto de acceso de la unidad de entrega |
| `Application` | «Orquestar…» | La coordinación interna |
| `Domain` | «Sostener…», «Constituir…» | El contrato de dominio |
| `Infrastructure` | «Guardar y recuperar…», «Derivar…» | La materialización técnica |

Los cuatro describen **la misma capacidad desde su capa**. En el modelo anterior era correcto: cada
proyecto de código tenía su categoría 02. En el 8.x la unidad de entrega es una sola y sus capas son
internas, de modo que **un caso de uso por capa es una vista, no un caso de uso**.

## 2. `GeometriaFactory-Api`: de 46 a 9 casos de uso

### 2.1 Salida 1 — son el mismo caso de uso

Nueve capacidades reales. Se conserva **una** por capacidad, que declara el flujo completo, y las
vistas por capa se absorben: el contrato de dominio y la orquestación pasan a ser pasos de su flujo,
y quedan citadas desde la arquitectura de la unidad.

| Capacidad de la unidad de entrega | Se absorben |
| --- | --- |
| Dar de alta una cuenta de alumno | `CU-00003` **(sólo A-02)**, `CU-04001`, `CU-02001` |
| Ingresar al laboratorio y sostener la sesión | `CU-00001`, `CU-00002`, `CU-00003` **(sólo A-05)**, `CU-04003`, `CU-02003`, `CU-02004` |
| Gobernar las cuentas de la comisión | `CU-00004`, `CU-04002`, `CU-02002` |
| Resetear la contraseña de un alumno | `CU-00005`, `CU-04011`, `CU-02013` |
| Configurar la cuenta de administrador en el primer arranque | `CU-00003` **(A-03 y A-17)**, `CU-04010`, `CU-02012` |
| Enviar un trabajo y ver sus observaciones | `CU-00006` **(A-10 y A-11)**, `CU-04004`, `CU-04005`, `CU-02005`, `CU-02006`, `CU-02007`, `CU-02008` |
| Eliminar un trabajo | `CU-00006` **(sólo A-12)**, `CU-04009` |
| Consultar el listado y el detalle de los trabajos | `CU-00007`, `CU-04006`, `CU-04007`, `CU-02009`, `CU-02011` |
| Dar desenlace a la revisión | `CU-00008`, `CU-04008`, `CU-02010` |

**Cuál sobrevive.** El que describe el flujo tal como lo ejecuta la persona, con el título de la
capacidad y no el de la capa: «Dar de alta una cuenta de alumno», no «Exponer el alta» ni «Orquestar
el alta». Ninguno de los tres sirve tal cual: **se escribe uno nuevo con el contenido de los tres**,
porque cada uno declara los actores, las precondiciones y los criterios de su capa y la unión no es
la suma de las partes.

### 2.1.1 Corrección: un documento de origen puede repartirse en más de una capacidad

La tabla de arriba suponía que cada documento de origen entra entero en una fila. **Al leer los tres
documentos del primer caso apareció que `CU-00003` no cumple eso**, y la propia tabla lo delataba: la
fila «Configurar la cuenta de administrador» no tenía ningún documento de la capa `Api`, cuando la
capacidad se ejerce por HTTP como todas las demás.

El motivo es que los documentos de la capa `Api` **no agrupan por capacidad sino por perfil de
autenticación**: `CU-00003` §1 declara que sus cuatro puntos están juntos porque «se ejercen sin
acceso firmado, o sin que el papel importe». Ese criterio es correcto para un contrato de superficie
y transversal a las capacidades:

| Punto de `CU-00003` | Qué expone | Capacidad a la que va |
| --- | --- | --- |
| **A-02** | Registrar una cuenta de alumno, sin contraseña | Dar de alta una cuenta de alumno |
| **A-03** | Configurar la cuenta de administrador, sólo si no existe | Configurar la cuenta de administrador |
| **A-17** | Responder si ya existe administrador, de sólo lectura | Ídem: es la misma ventana mirada desde afuera |
| **A-05** | Cambiar la contraseña propia exigiendo la vigente | Ingresar al laboratorio y sostener la sesión |
| ~~A-04~~ | Retirado por `PRODUCT-INTAKE` 1.13. El identificador no se recicla | — |

**El recuento no cambia: siguen siendo nueve capacidades.** Lo que cambia es que el reparto es de
**punto de acceso a capacidad**, no de documento a capacidad. Se verificó que `CU-00003` es el único
caso: los otros siete documentos de la capa `Api` cubren un solo grupo de puntos de una sola
capacidad —`CU-00001` A-01; `CU-00004` A-06 a A-08; `CU-00005` A-09; `CU-00006` A-10 a A-12;
`CU-00007` A-13 y A-14; `CU-00008` A-15— y `CU-00002` es transversal por diseño, que es por lo que ya
estaba en la fila de la sesión.

### 2.1.2 Corrección: dos filas que eran una capacidad, y una que estaba escondida adentro

El mismo reparto de punto de acceso a capacidad, aplicado a los trabajos, corrige el corte en dirección
contraria: **dos filas de la tabla original eran la misma capacidad**, y la que faltaba estaba escondida
como tercer punto de una de ellas.

La tabla original decía «Cargar, reeditar y eliminar un trabajo» y «Enviar un trabajo e interpretar su
texto», que es **el corte por capa disfrazado de corte por capacidad**: cargar, interpretar y resolver
el estado no son tres cosas que el alumno haga, sino tres tramos de lo que pasa cuando aprieta enviar.
Las fuentes lo dicen con todas las letras: `CU-02008` §1 y `CU-04005` §1 declaran que el alumno tiene
**una sola acción de guardado, enviar**, y `CU-04004` §1 declara que la resolución del estado no ocurre
en la carga. Los tres puntos de acceso lo confirman:

| Punto de `CU-00006` | Qué expone | Capacidad a la que va |
| --- | --- | --- |
| **A-10** | Envío de un trabajo nuevo | Enviar un trabajo y ver sus observaciones |
| **A-11** | Reenvío de uno que quedó en `Borrador` | Ídem: es el mismo acto, repetido |
| **A-12** | Eliminación, con **dos alcances y un solo punto** | Eliminar un trabajo |

**La eliminación sí es otra capacidad**, y la tabla original la tenía como cola de una fila en lugar de
como fila propia: la ejercen **dos actores distintos con reglas opuestas** —el alumno sólo lo propio y
sólo en `Borrador`; el administrador cualquiera que ve y **nunca** en `Borrador`—, no interpreta ningún
texto y no resuelve ningún estado.

**El recuento no cambia: siguen siendo nueve capacidades.** Cambia dónde está el corte.

### 2.2 Salida 3 — no son casos de uso de la unidad de entrega

Dieciséis documentos describen operaciones **internas** que ninguna persona ejecuta. Su lugar es la
arquitectura de la unidad de entrega, no su especificación funcional.

| Documento | Qué describe | Destino |
| --- | --- | --- |
| `CU-06003`, `CU-06005` | Guardar y recuperar trabajos y cuentas | Persistencia, en `05-Arquitectura-Tecnica` |
| `CU-06004` | Borrado físico y arrastre de la baja | Ídem |
| `CU-06006`, `CU-06007`, `CU-06008` | Derivar contraseña, producir provisoria, emitir acceso firmado | Seguridad, en `05` |
| `CU-06001`, `CU-06002` | Interpretar el texto y verificar valores derivados | Motor de interpretación, en `05` |
| `CU-06009`, `CU-06010` | Sello del reloj, preparar el almacén | Puertos y arranque, en `05` |
| `CU-00009` | Traducir el motivo del contrato a respuesta de protocolo | Contrato de error, en `05` y en `Producto/Contratos-Inter-Unidad/` |
| `CU-00010`, `CU-00011` | Componer la aplicación, arrancar el servicio | Composición y despliegue, en `05` y `09` |
| `CU-00012` | Ejercitar la superficie con la colección reproducible | **Categoría 10**: es un sample, no un caso de uso |

## 3. `GeometriaFactory-Web`: de 17 a 10 casos de uso

### 3.1 Salida 2 — son casos de uso distintos y se conservan

Los diez de `CU-10001` a `CU-10010` describen lo que una persona hace con el portal. Ninguno es vista
de otro: se conservan los diez, sin cambios.

### 3.2 Salida 3 — el contrato del componente no es especificación funcional

Los siete de `CU-12001` a `CU-12007` son la **API del visor**: inicializar una instancia, cargar el
texto y dibujar, seleccionar una pieza, redimensionar, destruir y liberar recursos. Es el contrato de
un componente interno, y quien lo «ejecuta» es el portal, no una persona.

Lo que el usuario efectivamente hace con el visor ya está declarado en `CU-10007`, «abrir un trabajo
y explorarlo en escena y árbol». Los siete pasan a la arquitectura de la unidad de entrega, como
contrato del componente empaquetado.

## 4. El resultado

| | Antes | Después |
| --- | --- | --- |
| `GeometriaFactory-Api` | 46 | **9** |
| `GeometriaFactory-Web` | 17 | **10** |
| **Total** | **63** | **19** |

Diecinueve casos de uso para nueve necesidades de negocio es una proporción que se lee. Sesenta y
tres no lo era, y no porque estuvieran mal escritos: estaban escritos para un modelo en el que cada
capa compilada tenía su propia categoría 02.

## 5. Lo que esta consolidación no hace, y hay que decirlo

**No borra nada.** Los cuarenta y cuatro documentos absorbidos o reubicados conservan su contenido: el
que se absorbe queda citado desde el caso de uso que lo reemplaza, y el que se reubica va a la
categoría que le corresponde. `Migracion-Rules.md` §4.3.2 lo exige.

**No se aplica sobre el árbol de la primera migración.** Se aplica sobre el definitivo, en la fase M4
de `Master-Prompt-Migracion.md` 2.0, donde cada caso de uso consolidado lo redacta el subagente
titular de la categoría con el contenido de los tres o cuatro que absorbe. Escribir uno nuevo con el
contenido de varios es trabajo de redacción, no de mudanza de archivos.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Consolidación decidida. Reemplaza la propuesta de 25 pares por el agrupamiento en nueve capacidades más dieciséis operaciones internas, con su veredicto y su destino. |
| 1.2 | 2026-08-16 | §2.1.2 nueva: dos filas de trabajos eran la misma capacidad —cargar, interpretar y resolver el estado son tramos de **una sola acción de guardado**, que las fuentes declaran— y la eliminación, que estaba como cola de una fila, pasa a fila propia por tener **dos actores con reglas opuestas**. El corte se rehace sobre A-10 y A-11 por un lado y A-12 por el otro. El recuento de nueve capacidades no cambia. |
| 1.1 | 2026-08-16 | §2.1.1 nueva: el reparto es de punto de acceso a capacidad, no de documento a capacidad. `CU-00003` se reparte en tres filas, verificado como caso único. La tabla §2.1 lo refleja. El recuento de nueve capacidades no cambia. |
