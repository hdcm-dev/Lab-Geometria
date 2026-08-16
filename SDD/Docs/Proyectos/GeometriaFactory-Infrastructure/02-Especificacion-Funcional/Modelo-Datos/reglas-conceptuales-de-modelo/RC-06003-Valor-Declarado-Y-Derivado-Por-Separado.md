# RC-06003 — El valor declarado y el derivado se guardan por separado

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-06003-Valor-Declarado-Y-Derivado-Por-Separado.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-02005`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md); [`RN-02008`](../../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §12 (entrada «valor declarado / valor derivado»), §17.1.P.11 punto 3, §17.3.P.10
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

De cada pieza se guardan **los dos valores**: el que el alumno declaró y el que el sistema derivó de las dimensiones. **Ninguno reemplaza al otro y ninguno se corrige.**

## 2. Justificación

Es la decisión de modelado que hace posible mostrar la discrepancia **sin recalcular en cada consulta**. Está declarada como decisión pre-tomada del producto y su motivo es directo: la verificación de valores es lo que el alumno tiene que ver, y verla exige tener los dos números a mano.

Guardar sólo el declarado obligaría a rehacer la derivación cada vez que alguien abre el trabajo. Guardar sólo el derivado sería **corregir el dato del alumno**, que es exactamente lo que el producto no hace: la discrepancia se señala, no se corrige y no se rechaza.

## 3. Ámbito de aplicación

- Alcanza al `Area` de toda pieza y al `Volumen` de las volumétricas.
- Alcanza también a la **advertencia**, que se guarda con los dos valores: sin ellos, el mensaje sería genérico y no le diría al alumno qué declaró contra qué dice la geometría.
- **No alcanza al texto original**, que conserva los valores tal como el alumno los escribió y que nunca se toca.
- **No fija cómo se deriva un valor**: eso es de [`CU-06002`](../../Casos-De-Uso/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md).

## 4. Consecuencia si se viola

Guardar un solo valor no produce rechazo del almacén: **produce un producto que ya no puede explicar la discrepancia**. En el caso de guardar sólo el derivado, además, el trabajo del alumno queda mostrado con un número que él no escribió, y eso contradice la conservación del dato original en su espíritu aunque el texto siga intacto.

## 5. CU afectados

- [`CU-06002`](../../Casos-De-Uso/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) — Verificar los valores: produce los dos.
- [`CU-06003`](../../Casos-De-Uso/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar: los conserva.

## 6. Pruebas que la verifican

`CU-06002` CA-04, que exige que el mensaje de la advertencia **exprese los dos valores** —declarada 36.00, derivada 54.00— y nunca un texto genérico, y CA-08, que compara el texto original antes y después de la verificación. Del lado del almacén, la recuperación del detalle de `CU-06003` CA-05 devuelve las piezas con sus observaciones, que llevan los dos valores.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
