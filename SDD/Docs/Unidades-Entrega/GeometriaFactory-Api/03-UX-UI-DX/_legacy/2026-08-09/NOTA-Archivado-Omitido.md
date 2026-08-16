# Nota de archivado omitido — 03-UX-UI-DX de GeometriaFactory-Domain

**Fecha:** 2026-08-09
**Autor:** Orquestador SDD
**Alcance:** los cinco documentos de esta categoría, en su versión **1.0**

---

## Qué falta acá y por qué

Esta carpeta **no contiene los snapshots** de la versión 1.0 de `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md`, `DX-Error-Messages.md`, `Glosario-UX.md` y `README.md`, que la política de deprecación de `Master-Prompt.md` §5 exige conservar al pasar los documentos a 1.1.

**El estado 1.0 de esos cinco archivos se perdió.** No es recuperable: la carpeta `SDD/` no está bajo seguimiento de control de versiones en el repositorio destino, de modo que no existe otra copia.

## De quién es el error

**Del orquestador, no del subagente.** `Master-Prompt.md` §8 asigna el snapshot al orquestador y lo declara **anterior** a la construcción del despacho, con un fundamento explícito: un subagente puede abortar después de haber editado y antes de haber archivado, y una fase que despacha varios subagentes produciría carpetas de archivado parciales de distintos momentos.

En este caso el orquestador despachó la alineación de esta categoría —derivada de la corrección del hallazgo P0 registrado en `SDD/Docs/Audit/B-02-03-GeometriaFactory-Application-r1.md`— **sin tomar el snapshot primero**, y trasladó al subagente la decisión de cuándo tomarlo. El subagente editó los cinco documentos, informó correctamente que ya lo había hecho y **no archivó nada por su cuenta**, que es lo que su despacho le indicaba.

## Por qué no se reconstruyó

Fabricar los snapshots copiando el contenido vigente y rotulándolo `1.0` habría producido cinco archivos que afirman preservar una versión cuyo contenido nadie verificó. `Master-Prompt.md` §5.1 lo prohíbe expresamente: «etiquetar con una versión un archivo cuyo contenido no se verificó es una afirmación sin evidencia y D9 la prohíbe». Un archivado falso es peor que un archivado ausente, porque el ausente se nota y el falso no.

## Qué sí se conserva

La **trazabilidad narrativa** del cambio, en las filas de control de cambios de los cinco documentos vivos, que declaran qué cambió al pasar a 1.1, con qué alcance por sección y citando el informe de auditoría que originó la corrección. Lo que se perdió es el texto literal anterior, no el registro de qué se hizo ni por qué.

## Alcance de la pérdida

Acotado a esta categoría y a esta transición. Los demás archivados del producto están completos: al 2026-08-09 hay snapshots con su bloque de archivado en `00-Contexto/`, `01-Necesidades-Negocio/`, `GeometriaFactory-Contracts/` y las tres carpetas de `GeometriaFactory-Domain/02-Especificacion-Funcional/`.

## Qué se corrigió del procedimiento

El orquestador vuelve a tomar el snapshot **antes** de construir cualquier despacho sobre un entregable existente, sin delegar el momento en el subagente, que es lo que §8 fija y lo que se había respetado en todas las intervenciones anteriores del producto.
