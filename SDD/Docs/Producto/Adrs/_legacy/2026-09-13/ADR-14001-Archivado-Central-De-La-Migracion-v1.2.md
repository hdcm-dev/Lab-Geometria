# ADR-14001 — El archivado de la migración es central y no por carpeta

**Producto:** Fábrica de Geometría
**Documento:** ADR-14001-Archivado-Central-De-La-Migracion.md
**Versión:** 1.2
**Estado:** Aceptado
**Fecha:** 2026-08-16
**Autor:** Orquestador SDD
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Cierra:** el hallazgo **M-03** de [`../../Audit/Informe-Migracion-6.0-a-8.6.md`](../../Audit/Informe-Migracion-6.0-a-8.6.md) 2.0 §5
**Trazabilidad upstream:** `Migracion-Rules.md` §6, criterio de archivado; `Root-Rules.md` §11
**Trazabilidad downstream:** las tres carpetas de `SDD/Docs/_legacy/` y `SDD/Intake/_legacy/`

---

## 1. Contexto

`Migracion-Rules.md` §6 exige que «el estado previo de cada documento migrado quede archivado en el
`_legacy/` **de su propia carpeta**» antes de sobrescribir. Es un criterio de aceptación
interpretativo, y su motivo es que el estado anterior quede al lado del documento que lo reemplaza,
donde quien lo lee lo va a buscar.

La migración 6.0 → 8.6 de este destino **no lo cumplió así**. Archivó en carpetas centrales:

| Carpeta | Qué conserva | Volumen |
| --- | --- | --- |
| `SDD/Docs/_legacy/2026-08-15-migracion-8.2/` | Las categorías de `GeometriaFactory-Contracts`, el proyecto de código que no quedó como unidad de entrega | 86 documentos |
| `SDD/Docs/_legacy/2026-08-16-consolidacion-8.5/` | Los casos de uso absorbidos por la consolidación de la unidad `GeometriaFactory-Api` | 32 documentos |
| `SDD/Intake/_legacy/2026-08-16/` | El intake 1.34 y el manifiesto 1.4 | 2 documentos |

## 2. Decisión

**Se conserva el archivado central y se declara como apartamiento**, en lugar de dispersar los
snapshots o de dejar el incumplimiento sin nombrar.

## 3. Motivo

**La migración movió árboles enteros, y la carpeta de origen de la mayoría de esos documentos dejó de
existir.** El criterio de la regla supone que el documento migrado sigue viviendo donde vivía, y que
lo único que cambia es su contenido. Acá cambió la estructura:
`Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/` no existe más, de modo que su
`_legacy/` no tendría dónde colgar.

**Y hay un motivo positivo, no sólo la imposibilidad.** Los 118 documentos de las dos carpetas de
`Docs/` **son el resultado de dos actos**, no de 118 decisiones sueltas: una migración estructural y
una consolidación de casos de uso. Un `_legacy/` por carpeta habría dispersado cada acto en veinte
lugares y habría perdido lo único que hace legible el archivo: **que se archivó junto porque se
decidió junto**. Las dos carpetas llevan la fecha y el motivo en su nombre, y un `README.md` que
declara, documento por documento, qué lo reemplaza y por qué.

## 4. Consecuencias

**A favor.** El archivo se lee como lo que es: dos actos fechados con su motivo. Quien busque «qué
decía el dominio del alta de un alumno» encuentra el documento y, en el mismo lugar, la decisión que
lo retiró.

**En contra, y es real.** Quien esté parado en
`Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Casos-De-Uso/` **no ve un
`_legacy/` al lado**, y tiene que saber que el archivo está en la raíz. Se mitiga con la cabecera de
cada caso de uso consolidado, que cita por ruta los documentos que absorbe, y con el `README.md` de
cada carpeta de archivo, que cita en la dirección inversa.

**Alcance del apartamiento.** Cubre **la migración 6.0 → 8.6 y sólo esa**. El archivado ordinario de
`Master-Prompt.md` §13 —el de un documento que sube de versión sin cambiar de lugar— **sigue siendo
por carpeta**, y este ADR no lo toca: ahí la carpeta de origen existe y el motivo de este apartamiento
no aplica.

## 5. Alternativas descartadas

| Alternativa | Por qué no |
| --- | --- |
| **Un `_legacy/` por carpeta de destino** | La mayoría de los documentos no tiene carpeta de origen viva. Habría obligado a inventar una correspondencia entre carpetas que la reestructuración deshizo, que es precisamente el tipo de inferencia que la regla de no invención prohíbe |
| **Un `_legacy/` por carpeta de origen, reconstruyendo el árbol viejo dentro de `_legacy/`** | Es lo que la carpeta `2026-08-15-migracion-8.2/` **sí hace** para `GeometriaFactory-Contracts`, porque ahí el árbol de origen era el objeto archivado. Extenderlo a los 32 casos de uso de la consolidación habría producido un árbol de veinte carpetas para 32 archivos, sin agregar nada |
| **No archivar y confiar en el historial de versiones** | `Migracion-Rules.md` lo declara P0: el estado previo no archivado detiene la cadena. El historial de versiones no es el archivo del método, y una migración no se audita con `git log` |

## 6. Estado del apartamiento

**Campos 4, 5 y 6 de `Root-Rules.md` §11.** Los tres los exige la normativa vigente; el **5** y el
**6** entraron con `Root-Rules.md` **6.1** (SDD 9.7), y el **4** faltaba desde la emisión.

| Campo | Valor |
| --- | --- |
| **4 · Disparadores que superarían la decisión** | Este apartamiento queda superado cuando **el framework declare cómo se archiva una migración estructural** —el caso en que la carpeta de origen de un documento deja de existir—, o cuando **una migración posterior de este destino vuelva a archivar de forma central**, porque entonces el archivado central deja de ser una excepción de una corrida y pasa a ser el patrón del producto |
| **5 · Estado** | **`vigente`** |
| **6 · Saltos de versión que sobrevivió** | **3** — sobrevivió **8.11 → 9.9**, **9.12 → 10.0** y **10.0 → 13.3**. **Revisado por la fase M4 de la migración 10.0 → 13.3, el 2026-08-25**, con resultado **no contemplado**: el campo 4 se contrastó contra las entradas **10.1 a 13.3** del `CHANGELOG.md` del framework y ninguna cumple el disparador. El incremento es de **+2** y no de +1 porque **la migración 9.12 → 10.0 no corrió esta revisión** —ni su plan ni su informe nombran la palabra «apartamiento»—, de modo que ese salto le pasó por encima sin contarse (`Audit/Plan-Migracion-10.0-a-13.3.md` §5.1) |

**El campo 4 se derivó del alcance que este ADR ya declaraba**, y no se inventó: su §4 acota el
apartamiento a «la migración 6.0 → 8.6 y sólo esa», y deja dicho que el archivado ordinario sigue
siendo por carpeta. La condición de superación es la lectura directa de ese límite. **La derivación
la aprobó el Product Owner** el 2026-08-17, en la batería de la fase M1 de la migración 8.11 → 9.9
(`Audit/Plan-Migracion-8.11-a-9.9.md` §3.1), después de que el orquestador se negara a redactarlo por
su cuenta: escribirle un disparador plausible habría sido la invención que `Migracion-Rules.md` §4.1
tipifica como P0.

**Por qué el resultado de la revisión fue «no contemplado».** Se leyeron las entradas del `CHANGELOG`
del framework de la 8.12 a la 9.9, una por una: **ninguna toca el criterio de archivado de una
migración estructural**. La vigente sigue sin decir nada del caso, de modo que el apartamiento **se
preserva con su texto literal** —incluido su fundamento original— y su contador se incrementa.
Reescribir el fundamento contra la normativa nueva produciría un ADR que dice haber decidido algo que
en su fecha nadie decidió.

**Qué pasa si llega a 2.** `Migracion-Rules.md` §4.7 declara que un apartamiento que sobrevive **dos o
más saltos** sin ser contemplado ya demostró que no es de un producto, y se declara **candidato a
regla del framework** en el informe de la migración que lo detecte. Con el contador en **3**, **ya lo es**, y el informe de M6 lo declara. **Lo va a reportar el número y no la memoria de nadie.**

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-17 | **Revisión de apartamientos de la migración 8.11 → 9.9, fase M4** (`Migracion-Rules.md` **3.7** §4.7, que la SDD 9.7 agregó al método). Entra **§6, el estado del apartamiento**, con los campos **4**, **5** y **6** que `Root-Rules.md` **6.1** §11 exige: el disparador que lo superaría, el estado **`vigente`** y el contador de saltos sobrevividos en **1**. El control de cambios pasa a §7. **Resultado de la revisión: no contemplado** —se leyeron las entradas del `CHANGELOG` del framework de la 8.12 a la 9.9 contra el disparador, y ninguna alcanza el caso—, de modo que el apartamiento **se preserva con su texto literal y su fundamento original**: §4.1 prohíbe re-fundamentarlo contra la normativa nueva. **El campo 4 no existía** y se derivó del alcance que el propio ADR declaraba, con aprobación del Product Owner en la batería de M1 (`Audit/Plan-Migracion-8.11-a-9.9.md` §3.1); el orquestador se negó a redactarlo por su cuenta. Sube **minor**: no cambia la decisión, agrega los campos que la vuelven evaluable en el salto siguiente. |
| 1.0 | 2026-08-16 | Emisión inicial. Declara como apartamiento el archivado central de la migración 6.0 → 8.6, con su motivo, su alcance acotado a esa migración y las tres alternativas descartadas. Cierra el hallazgo **M-03** del informe de migración. |
| 1.2 | 2026-08-25 | **Revisión de apartamientos de la migración 10.0 → 13.3, fase M4** (`Migracion-Rules.md` **3.19** §4.7). **Resultado: no contemplado.** Ninguna entrada de la 10.1 a la 13.3 declara **cómo se archiva una migración estructural**, que es el disparador del campo 4, y ninguna migración posterior de este destino volvió a archivar de forma central. El apartamiento **se preserva con su texto literal** y **sólo cambia su contador**, que sube **+2** —el salto 9.12 → 10.0, que **no corrió esta revisión**, y éste—. **Con el contador en 3 cruza el umbral de dos saltos**: `Migracion-Rules.md` §4.7 declara que un apartamiento que sobrevive dos o más ya demostró que **no es de un producto**, y queda declarado **candidato a regla del framework** en el informe de M6. **Lo levantó el audit de M6 como P0**: el plan mandaba a M4 escribir este campo y M4 no lo tocó, de modo que el contador seguía diciendo 1. |
