# NB-05 — Visibilidad del error de cálculo sobre el trabajo propio

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-05-Visibilidad-Del-Error-De-Calculo.md |
| Versión | 1.1 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (diferenciador D-2), §4 (capacidad F-10), §4.1 (regla RN-05), §4.2 (modelo de estados del trabajo), §6 (flujo 2), §7 (caso límite CL-4), §8 (métrica de valor didáctico entregado), §20 (escenarios E-1 a E-4), §22 (asunción A-2); `Vision-Producto.md` §1, §3, §5 y §9; `Alcance-Producto.md` §3 y §8 |
| Trazabilidad downstream | CU-14 (prevista en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

Los ejemplos de la propia cátedra arrastran dos errores de fórmula verificados numéricamente: el área del cubo se calcula sobre cuatro caras en lugar de seis, y el volumen del ortoedro ignora una de sus dimensiones (PRODUCT-INTAKE §1). Como consecuencia, un cubo de arista 3 declara un área de 36.00 donde la geometría dice 54.00, y un ortoedro de 7 por 7 por 21 declara un volumen de 343.00 donde la geometría dice 1029.00. Ese error está presente en el cien por ciento de esas figuras y **nada en la cadena actual se lo señala al alumno**. El alumno entrega un valor equivocado convencido de que está bien, y el defecto sobrevive a la actividad.

La necesidad de la cátedra es que el producto haga visible esa discrepancia sobre el trabajo del propio alumno, que es el único lugar donde tiene valor didáctico. No se trata de corregirla ni de rechazar el trabajo: el producto recalcula los valores desde las dimensiones que el propio texto declara, compara y **señala** la diferencia, mostrando los dos números. La corrección la hace el alumno en su programa, que es donde está el error.

El carácter no bloqueante es deliberado y está declarado: una discrepancia de valor **no impide que el trabajo pase a estado `Pendiente`** (PRODUCT-INTAKE §7, CL-4, y §4.1, RN-05). Que después el administrador lo apruebe o lo rechace es una decisión suya, y no la toma la verificación de valores. Si bloqueara, el alumno con el error lo viviría como un rechazo del producto en lugar de como información sobre su código, y además quedaría fuera de la entrega justamente el caso que más interesa observar. Esta es la capacidad que el intake identifica como el mayor valor didáctico del producto (PRODUCT-INTAKE §3, diferenciador D-2).

## 2. Ejemplo de uso desde la perspectiva del negocio

Una alumna carga el trabajo con las tres piezas del ejemplo con el que trabajó toda la comisión: un cilindro, un cubo y un ortoedro. El producto se lo acepta y, junto al trabajo, le muestra dos advertencias: en el cubo, el área declarada es 36.00 y la que se desprende de sus dimensiones es 54.00; en el ortoedro, el volumen declarado es 343.00 y el derivado es 1029.00. Las dos aparecen con los dos números a la vista. Nadie le impide entregar: el trabajo pasa a estado `Pendiente` igual, con sus dos advertencias, y queda a la espera de la revisión del docente. La alumna vuelve a su programa, mira la fórmula del área del cubo, cuenta las caras y entiende sola dónde estaba el error. Un compañero que ya había corregido su programa carga el mismo cubo y no recibe ninguna advertencia.

## 3. Impacto

- Si se resuelve: el error de fórmula se hace visible sobre el trabajo propio del alumno, que es donde tiene valor didáctico.
- Si se resuelve: la cátedra obtiene evidencia concreta de qué defectos de cálculo aparecen en la comisión y con qué frecuencia.
- Si se resuelve: el alumno recibe información sobre su código sin que nadie le corrija el trabajo por él.
- Si queda sin resolver: el producto se limita a guardar y mostrar, y pierde el diferenciador que las fuentes declaran como su mayor valor didáctico.
- Si queda sin resolver: los defectos de fórmula siguen sin señalarse y sobreviven a la actividad, exactamente como hoy.
- Riesgo a controlar: una verificación mal construida que advirtiera siempre daría por buena la advertencia esperada y dejaría pasar el caso en que no debe advertir nada.

## 4. Problema específico que resuelve

- El alumno no recibe ninguna señal cuando el valor que su programa calcula no se corresponde con las dimensiones que él mismo declara.
- Los dos defectos verificados de los ejemplos de la cátedra sobreviven a la actividad sin hacerse visibles.
- Una corrección automática privaría al alumno del aprendizaje y alteraría su entrega.
- Un rechazo del trabajo dejaría fuera de la entrega justamente el caso que interesa observar.
- Sin los dos valores a la vista, la advertencia no explica nada: el alumno necesita ver el declarado y el derivado juntos.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Valor didáctico efectivamente entregado | Advertencias de valor declarado contra derivado que el producto muestra, por alumno que cargue un cubo del primer ejemplo de la cátedra o un ortoedro | ≥ 1 por alumno | Primera entrega de la cursada |
| Carácter no bloqueante | Trabajos que quedan impedidos de pasar a estado `Pendiente` por tener advertencias de valor | 0 | Punto de control de la etapa `f` |
| Advertencia explicativa | Advertencias que se muestran con los 2 valores expresados, el declarado y el derivado, sobre el total de advertencias emitidas | 100 % | Punto de control de la etapa `f` |
| Criterio negativo de la verificación | Advertencias emitidas sobre el mismo cubo emitido por el segundo ejemplo de la cátedra, cuyo valor declarado sí se corresponde con sus dimensiones | 0 | Punto de control de la etapa `f` |
| Cobertura sobre el escenario semilla | Advertencias emitidas sobre el escenario de tres piezas del intake, sobre las 2 esperadas: el área del cubo y el volumen del ortoedro | 2 de 2 | Punto de control de la etapa `f` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §8; el segundo, de PRODUCT-INTAKE §7 (CL-4), §4 (F-10) y §4.1 (RN-05); el tercero, de PRODUCT-INTAKE §3 (D-2) y §20 (escenario E-3); el cuarto, de PRODUCT-INTAKE §20 (escenario E-4) y de `Alcance-Producto.md` §8; el quinto, de PRODUCT-INTAKE §20 (escenario E-1). **El target del primer criterio está rotulado como asunción A-2 en PRODUCT-INTAKE §22 y está pendiente de confirmación del Product Owner**; se usa como valor vigente hasta que la confirmación llegue. Los otros cuatro están declarados en las fuentes y no son asunciones.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Decidió que la discrepancia se señale y no se corrija ni se rechace, y confirma el target de la métrica de valor didáctico |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que el error de fórmula deje de sobrevivir a la actividad sin hacerse visible |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye la verificación de valores y demuestra tanto el caso que advierte como el que no debe advertir |
| Alumno de la comisión | Beneficiario | Descubre el error de fórmula sobre su propio trabajo, con los dos valores a la vista, y sin que nadie se lo corrija |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Obtiene evidencia concreta de qué defectos de cálculo aparecen en la comisión |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-05 | CU-14 verificar los valores declarados contra los derivados y emitir las advertencias | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-04, porque los valores sólo se pueden recalcular sobre un trabajo cuyas piezas y dimensiones ya fueron interpretadas.
- Es prerequisito de: ninguna otra NB. Es una hoja de la cadena de dependencias.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4, donde la capacidad F-10 está declarada Must Have; es la única capacidad que esta NB agrupa, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de hacer visible el error de cálculo sobre el trabajo propio a partir de la capacidad F-10 del intake, con cinco criterios de éxito trazados a su sección de origen —incluido el criterio negativo del segundo ejemplo de la cátedra y uno con target rotulado como asunción A-2— y un caso de uso previsto. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgo **H-03**: las trece ocurrencias de «observación» de §2, §3, §4, §5 y §7 pasan a **«advertencia»**, que es el término específico que el glosario raíz reserva a la discrepancia entre valor declarado y derivado (`Vision-Producto.md` §9.1). «Observación» queda como superordinado de «advertencia» y «error de validación», y esta NB no lo necesita porque su enunciado se refiere siempre a la especie que no impide finalizar. Alcanza a los cinco títulos de criterio de §5 y al enunciado de CU-14; ningún target, plazo ni trazabilidad cambia. La corrección se absorbe **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
| 1.1 | 2026-08-08 | Alinea el carácter no bloqueante de la advertencia con el modelo de estados que `PRODUCT-INTAKE` 1.3 declara en §4.2: el corte pasa del cierre al envío, de modo que lo que una advertencia no impide es que el trabajo **pase a estado `Pendiente`**, y no «guardar ni finalizar». Alcanza a §1, a §2 y al segundo criterio de §5 con su nota de origen, y se declara que aprobar o rechazar es decisión del administrador y no de la verificación de valores. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Ningún target ni prioridad cambia. |
