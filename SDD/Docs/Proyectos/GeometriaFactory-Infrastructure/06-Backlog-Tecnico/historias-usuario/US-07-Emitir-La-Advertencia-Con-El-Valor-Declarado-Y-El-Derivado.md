# US-07 — Emitir la advertencia con el valor declarado y el derivado, sin corregir ninguno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-07-Emitir-La-Advertencia-Con-El-Valor-Declarado-Y-El-Derivado.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-05 Interpretación y verificación del dato del alumno
**Etapa del producto:** `f`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que cada advertencia traiga el valor que el alumno declaró y el que la geometría deriva, sin corregir ninguno de los dos**, para **que el alumno vea la discrepancia sobre su propio trabajo, que es el mayor valor didáctico del servicio**.

## 2. Contexto

El intake §3 declara este diferenciador: el sistema **señala** las discrepancias **sin corregirlas ni rechazarlas**. `RN-08` prohíbe reescribir el texto original y `RC-03` exige guardar el valor declarado y el derivado **por separado**. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md).

## 3. Criterios de aceptación

- Given una discrepancia por encima de la tolerancia, When se emite la observación, Then es de especie **advertencia** y trae **los dos valores**.
- Given esa advertencia, When se compara el texto original antes y después, Then **no cambió**: el valor declarado no se corrige.
- Given un texto con advertencias y sin errores, When el dominio resuelve el estado, Then **el trabajo pasa a estado `Pendiente`**: la advertencia no bloquea, y esta capa **no decide ese estado**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-05 |
| CU cubiertos | CU-02 |
| RN que ejerce | RN-05 en su parte de insumo, RN-08, RN-09 |
| Componente de `05` §3.1 | Motor de verificación de valores |
| Reglas conceptuales de modelo | `RC-03` |
| ¿Toma alguna decisión de negocio? | **No.** El estado lo resuelve el dominio |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-17, BT-18, BT-19 |
| Tests previstos en 08 | Casos 5, 6 y 7 de la batería, con `E-3`, `E-4`, `E-2` y `E-6` |

## 5. Prioridad y estimación

`Must` por derivar de `F-10`, `Must Have`, y porque el criterio de transición `f` → `g` exige que un cubo del primer ejemplo produzca advertencia **con los dos valores expresados** y que el mismo cubo del segundo **no** produzca ninguna.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Una dimensión en cero no descarta la figura** —caso 7 de la batería, escenario `E-6`—: el cero es un valor legible, y lo que produce una condición es la **ausencia** del dato, nunca el valor que trae.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
