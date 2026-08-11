# RN-08 — El texto original del alumno se conserva íntegro

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** RN-08-Texto-Original-Conservado-Integro.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.1 (enunciado de RN-08), §4 (F-20), §9 (X-4), §17.1.P.2 (reglas sin invariante asociado), §17.3.P.11 punto 2, §20 (los **ocho** escenarios); [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §1 y §5; `00-Contexto/Alcance-Producto.md` §5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Enunciado de la regla](#1-enunciado-de-la-regla)
- [2. Justificación](#2-justificación)
- [3. Ámbito de aplicación](#3-ámbito-de-aplicación)
- [4. Consecuencia si se viola](#4-consecuencia-si-se-viola)
- [5. CU afectados](#5-cu-afectados)
- [6. Pruebas que la verifican](#6-pruebas-que-la-verifican)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Enunciado de la regla

El texto que el alumno cargó se conserva íntegro, carácter por carácter, y el producto no lo reescribe, no lo normaliza y no lo corrige en ningún momento de la vida del trabajo.

## 2. Justificación

El formato de entrada es una premisa fija y el producto se adapta al dato, nunca al revés. El texto original es la única fuente fiel del trabajo del alumno, y editarlo desde el producto está declarado fuera del alcance (PRODUCT-INTAKE §4.1, §9 X-4 y §4 F-20). Conservarlo íntegro tiene además una consecuencia operativa declarada: permite reprocesar el trabajo si la interpretación mejora (§17.3.P.11 punto 2).

**Esta regla no tiene invariante asociado**, y el intake lo declara explícitamente: describe un comportamiento —no reescribir— y no una condición permanente sobre el estado (§17.1.P.2). En particular **no la expresa INV-04**, que enuncia otra cosa: que un trabajo `Finalizado` tiene el texto interpretado sin errores, y que sostiene a RN-05.

## 3. Ámbito de aplicación

- Se evalúa al constituir el trabajo y en cada reedición del borrador, que reemplaza el texto por otro texto del alumno pero nunca por una versión corregida por el producto.
- Se sostiene durante la interpretación: reconstruir las piezas no altera el texto.
- Se sostiene durante la verificación de valores: la discrepancia se señala como advertencia y el valor declarado no se corrige.
- Se sostiene en el envío, en el desenlace y después de él, en los cuatro estados del trabajo.

## 4. Consecuencia si se viola

Rechazo de la operación, con el código `TEXTO_ORIGINAL_ALTERADO`. La regla no admite compensación: un texto alterado ya no es el trabajo del alumno, y ninguna advertencia posterior lo repara.

## 5. CU afectados

- [CU-05](../Casos-De-Uso/CU-05-Crear-Y-Reeditar-Un-Trabajo.md) — Crear y reeditar un trabajo.
- [CU-06](../Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) — Reconstruir el conjunto de piezas del trabajo.
- [CU-07](../Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md) — Registrar las observaciones del trabajo.

## 6. Pruebas que la verifican

Pruebas unitarias de dominio previstas en 08: comparación carácter por carácter del texto conservado contra el texto aportado, con el escenario E-2 —que trae 2 comas finales y la clave `Tapas`— como caso principal, y con los **ocho** escenarios del intake como cobertura. El criterio de éxito de negocio es de `NB-04` §5: 0 caracteres del texto original modificados por el producto, verificado además en cada punto de control posterior por la regla de no regresión.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | **Corrige la atribución de INV-04.** La versión anterior declaraba que INV-04 expresaba esta regla, siguiendo lo que `PRODUCT-INTAKE` §21 afirmaba antes de su corrección; el intake 1.3 §17.1.P.2 transcribe los siete invariantes y deja ver que INV-04 enuncia que un trabajo `Finalizado` tiene el texto interpretado sin errores, y que sostiene a **RN-05**. Esta regla queda **sin invariante asociado**, junto con RN-07 y RN-09, por describir un comportamiento y no una condición permanente. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. §3 extiende el ámbito al envío, al desenlace y a los cuatro estados. |
| 1.2 | 2026-08-10 | **Cierra la parte del hallazgo `N-4`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 que alcanza a este archivo. Dos residuos de recuento: la **trazabilidad de cabecera** citaba «§20 (los siete escenarios)» y **§6** decía «con los siete escenarios del intake como cobertura». El intake §20 tiene **ocho** desde su versión 1.7, con **E-8** incorporado para `DIMENSION_NO_LEGIBLE`; contados `E-1` a `E-8` sobre la fuente viva. Los dos pasan a **ocho**. **El enunciado de la regla no cambia, ni su ámbito, ni su verificación**: la cobertura de prueba se amplía en un escenario, y E-8 conserva el texto original íntegro igual que los otros siete. Sube minor: corrige un recuento derivado. |
