# ADR-14002 — Las familias propias del intake conservan su ancho de origen

**Producto:** Fábrica de Geometría
**Documento:** ADR-14002-Familias-Propias-Del-Intake-Con-Ancho-De-Origen.md
**Versión:** 1.2
**Estado:** Aceptado
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Cierra:** el hallazgo **M-09** de [`../../Audit/Informe-Migracion-6.0-a-8.6.md`](../../Audit/Informe-Migracion-6.0-a-8.6.md) 2.0 §5, en la parte que no era determinable
**Trazabilidad upstream:** `Root-Rules.md` §9.2, ancho de cinco dígitos y familias alcanzadas
**Trazabilidad downstream:** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` 2.1

---

## 1. Contexto

`Root-Rules.md` §9.2 fija **cinco dígitos uniformes** para los identificadores y enumera las familias
alcanzadas —`NB`, `CU`, `RN`, `RC`, `ADR`, `US`, `BT`, `EP`, `EST` y equivalentes— con **dos únicas
exclusiones**: `AG-XX`, que designa un rol del catálogo del framework, y el ordinal de iteración.

El salto 6.0 → 8.6 **alcanza la forma de los identificadores**, y `Migracion-Rules.md` §4.3.1 exige
un árbol de migración con una fila por identificador alcanzado. **Ese árbol se construyó sobre
`SDD/Docs/` y no incluyó las familias que el intake acuña**, que quedaron con su ancho de origen.

Las familias, con su volumen medido en el árbol vivo sin `Audit/`:

| Familia | Qué cataloga | Ocurrencias | Tiene numeración de destino |
| --- | --- | --- | --- |
| `RN` | Reglas de negocio | 388 | **Sí**: `RN-02001` a `RN-02016` |
| `INV` | Invariantes del dominio | 559 | No |
| `F` | Funcionalidades del alcance MoSCoW | 1634 | No |
| `E` | Escenarios del anexo de ejemplos | 1143 | No |
| `A` | Asunciones declaradas | 912 | No |
| `RA` | Reglas de arquitectura del producto | 447 | No |
| `X` | Exclusiones | 205 | No |
| `R` | Riesgos | 174 | No |
| `CL` | Casos límite | 158 | No |
| `CP` | Restricciones de compatibilidad | 14 | No |
| `RF` | Requisitos de las fuentes | 10 | No |

## 2. Decisión

**Se parte en dos, y las dos partes se deciden distinto.**

1. **`RN` se renumera**, a `RN-02001` a `RN-02016`, con sus 377 citas reconectadas desde registro.
2. **Las diez familias restantes conservan su ancho de origen**, y ese apartamiento se declara acá.

## 3. Motivo

**`RN` no era una elección de numeración: era una inconsistencia.** El árbol migrado **ya numeraba
estas mismas reglas** como `RN-02001` a `RN-02016`, con archivo propio por regla. Hasta esta decisión
convivían `RN-15` y `RN-02015` **para la misma regla**, sin nada en el texto que dijera que eran la
misma. No había dos formas legítimas: había una forma vigente y una cita que no la usaba. Por eso se
renumera y no se declara apartamiento.

**Las otras diez son otro caso, y la diferencia es exactamente ésa: no tienen destino.** Ninguna
existe en el árbol con cinco dígitos, de modo que renumerarlas no reconecta nada con nada: **elige un
número nuevo**. Y eso trae tres consecuencias que ningún beneficio compensa:

- **Más de 5000 ocurrencias reescritas** sin que ninguna referencia resuelva mejor de lo que resuelve
  hoy: `F-26` resuelve hoy, y `F-00026` resolvería igual.
- **El intake es documento humano**, y sus catálogos son del Product Owner. `RN` se toca porque el
  método ya le había asignado otro número; el resto es renumerar por conformidad de forma.
- **`E`, `A`, `X`, `R` y `CL` no son catálogos del producto en el sentido de §9.2**: son las
  colecciones internas de un documento —escenarios de ejemplo, asunciones, exclusiones, riesgos y
  casos límite— que sólo existen dentro del intake y que ninguna categoría genera ni referencia como
  artefacto. Están más cerca de las dos exclusiones que la regla ya declara que de `CU` o `NB`.

## 4. Consecuencias

**A favor.** El intake conserva la numeración con la que el Product Owner lo escribió y lo lee, y las
5000 ocurrencias no se tocan. La única inconsistencia real —dos números para la misma regla— quedó
cerrada.

**En contra, y es real.** El producto queda con **dos anchos conviviendo**: cinco dígitos en las
familias que el framework genera, dos en las que el intake acuña. Un lector que vea `F-26` y
`CU-00026` en la misma página tiene que saber que el ancho no es un dato del referente. Se mitiga con
esta declaración y con que las familias del intake **no se mezclan** con las generadas: `F`, `E`, `A`,
`X`, `R`, `CL`, `CP`, `RF` y `RA` no existen en ninguna otra numeración del producto, de modo que no
hay ambigüedad posible sobre a cuál se refiere una cita.

**Qué lo reabre.** Si alguna de estas familias pasa a tener artefacto propio generado —un archivo por
invariante, por ejemplo, como lo tienen hoy las reglas de negocio— **deja de aplicar este
apartamiento** y esa familia se renumera, por el mismo motivo por el que `RN` se renumeró.

## 5. Alternativas descartadas

| Alternativa | Por qué no |
| --- | --- |
| **Renumerar las once familias** | Más de 5000 ocurrencias, sobre el documento del Product Owner, sin que ninguna referencia resuelva mejor. El costo es cierto y el beneficio no |
| **No renumerar ninguna, `RN` incluida** | Habría dejado vivo el único defecto real: dos números para la misma regla, sin nada que lo dijera. Un apartamiento no sirve para tapar una inconsistencia; sirve para declarar una diferencia deliberada |
| **Dejar el hallazgo abierto sin decidir** | Es lo que venía pasando y es lo que `Root-Rules.md` §11 existe para evitar: un apartamiento sin declarar se evalúa como omisión y no como decisión |

## 6. Estado del apartamiento

**Campos 4, 5 y 6 de `Root-Rules.md` §11.** El **5** y el **6** entraron con `Root-Rules.md` **6.1**
(SDD 9.7); el **4** ya estaba, bajo el título «Qué lo reabre» de §4.

| Campo | Valor |
| --- | --- |
| **4 · Disparadores que superarían la decisión** | Declarado en **§4, «Qué lo reabre»**: que alguna de estas familias **pase a tener artefacto propio generado** —un archivo por invariante, como lo tienen hoy las reglas de negocio—. Esa familia se renumera entonces, por el mismo motivo por el que `RN` se renumeró |
| **5 · Estado** | **`vigente`** |
| **6 · Saltos de versión que sobrevivió** | **3** — sobrevivió **8.11 → 9.9**, **9.12 → 10.0** y **10.0 → 13.3**. **Revisado por la fase M4 de la migración 10.0 → 13.3, el 2026-08-25**, con resultado **no contemplado**: el campo 4 se contrastó contra las entradas **10.1 a 13.3** del `CHANGELOG.md` del framework y ninguna cumple el disparador. El incremento es de **+2** y no de +1 porque **la migración 9.12 → 10.0 no corrió esta revisión** —ni su plan ni su informe nombran la palabra «apartamiento»—, de modo que ese salto le pasó por encima sin contarse (`Audit/Plan-Migracion-10.0-a-13.3.md` §5.1) |

**Por qué el resultado de la revisión fue «no contemplado».** Se leyeron las entradas del `CHANGELOG`
del framework de la 8.12 a la 9.9, una por una, contra el disparador del campo 4: **ninguna le da
artefacto propio generado** a `F`, `E`, `A`, `X`, `R`, `CL`, `CP`, `RF` ni `RA`. El disparador no se
cumplió, la vigente sigue sin decir nada del caso, y el apartamiento **se preserva con su texto
literal** —incluido su fundamento original— con su contador incrementado.

**Qué pasa si llega a 2.** `Migracion-Rules.md` §4.7 declara que un apartamiento que sobrevive **dos o
más saltos** sin ser contemplado ya demostró que no es de un producto, y se declara **candidato a
regla del framework** en el informe de la migración que lo detecte. Con el contador en **3**, **ya lo es**, y el informe de M6 lo declara.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-17 | **Revisión de apartamientos de la migración 8.11 → 9.9, fase M4** (`Migracion-Rules.md` **3.7** §4.7, que la SDD 9.7 agregó al método). Entra **§6, el estado del apartamiento**, con los campos **4**, **5** y **6** que `Root-Rules.md` **6.1** §11 exige: el disparador que lo superaría, el estado **`vigente`** y el contador de saltos sobrevividos en **1**. El control de cambios pasa a §7. **Resultado de la revisión: no contemplado** —se leyeron las entradas del `CHANGELOG` del framework de la 8.12 a la 9.9 contra el disparador, y ninguna alcanza el caso—, de modo que el apartamiento **se preserva con su texto literal y su fundamento original**: §4.1 prohíbe re-fundamentarlo contra la normativa nueva. **El campo 4 ya estaba**, bajo el título «Qué lo reabre» de §4, y se cita en lugar de duplicarse. Sube **minor**: no cambia la decisión, agrega los campos que la vuelven evaluable en el salto siguiente. |
| 1.0 | 2026-08-16 | Emisión inicial. Parte el hallazgo M-09 en dos: `RN` se renumera porque tenía numeración de destino y convivían dos números para la misma regla; las diez familias restantes conservan su ancho y el apartamiento se declara acá, con su motivo, su mitigación y la condición que lo reabre. |
| 1.2 | 2026-08-25 | **Revisión de apartamientos de la migración 10.0 → 13.3, fase M4** (`Migracion-Rules.md` **3.19** §4.7). **Resultado: no contemplado.** El disparador del campo 4 es que alguna de estas familias **pase a tener artefacto propio generado**, y no ocurrió: la **12.0** toca `Root-Rules` §9 —hace cumplir el ancho a la familia `AG` y suma tres familias a la tabla de exclusiones— pero **no le da artefacto propio** a `F`, `E`, `A`, `X`, `R`, `CL`, `CP`, `RF` ni `RA`. Se preserva con su texto literal y **sólo cambia el contador**, que sube **+2** por el mismo motivo que su hermano. **Con el contador en 3 cruza el umbral** y queda declarado candidato a regla del framework. **Levantado por el audit de M6 como P0.** |
