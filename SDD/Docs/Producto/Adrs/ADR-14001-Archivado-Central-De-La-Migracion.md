# ADR-14001 — El archivado de la migración es central y no por carpeta

**Producto:** Fábrica de Geometría
**Documento:** ADR-14001-Archivado-Central-De-La-Migracion.md
**Versión:** 1.0
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

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Declara como apartamiento el archivado central de la migración 6.0 → 8.6, con su motivo, su alcance acotado a esa migración y las tres alternativas descartadas. Cierra el hallazgo **M-03** del informe de migración. |
