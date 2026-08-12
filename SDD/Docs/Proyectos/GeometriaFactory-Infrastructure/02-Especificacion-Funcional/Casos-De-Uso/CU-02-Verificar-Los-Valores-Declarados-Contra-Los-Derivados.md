# CU-02 — Verificar los valores declarados contra los derivados

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-02-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-05`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md); [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §3 (D-2), §4 (F-10), §4.1 (RN-05, RN-08), §7 (CL-4), §17.3.P.10, §17.3.P.11 punto 1, §20.E-1 a §20.E-6 y §21; implementa la segunda mitad del puerto de validación de figuras de `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3; alimenta [`CU-07`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-07-Registrar-Las-Observaciones-Del-Trabajo.md) de GeometriaFactory-Domain
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Recalcular el `Area` y el `Volumen` de cada pieza reconstruida a partir de sus dimensiones y de sus componentes, compararlos con los valores que el alumno declaró, y **emitir una advertencia por cada discrepancia**.

Es el mayor valor didáctico del producto y su rasgo diferenciador: el alumno ve sobre su propio trabajo que su cubo declara 36.00 donde la geometría dice 54.00. **Y es deliberado que no bloquee**: la discrepancia se señala, no se corrige y no se rechaza. Que el trabajo se apruebe o se rechace después es decisión del administrador y no del validador (`PRODUCT-INTAKE` §7, CL-4).

Lo que este caso de uso **no** hace: no corrige el valor declarado, no reescribe el texto original (RN-08) y no decide el estado del trabajo. Una advertencia **no impide** que el trabajo pase a estado `Pendiente` (RN-05).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor del puerto de validación de figuras (`GeometriaFactory-Application`) | Primario | Recibe las advertencias junto con el resto del resultado de la interpretación |
| Conjunto de piezas reconstruidas por `CU-01` | Sistema | La entrada de la verificación: cada pieza con sus dimensiones, sus componentes y sus valores declarados |

El alumno es el sujeto de la regla: es quien declaró los valores.

## 3. Precondiciones

- `CU-01` ya reconstruyó el conjunto de piezas. La verificación **opera sobre piezas reconstruidas**, no sobre el texto.
- Una figura que `CU-01` no pudo reconstruir **no llega acá**: su posición está reservada y su observación ya existe, de especie error de validación.

## 4. Flujo principal

1. Por cada pieza reconstruida, se toma su valor **declarado** de `Area` y, si es volumétrica, el de `Volumen`.
2. Se calcula el valor **derivado** desde sus dimensiones y sus componentes.
3. Se comparan los dos con **tolerancia absoluta de 0.01**, nunca por igualdad exacta de punto flotante. El número no es una asunción: sale de que el emisor redondea a dos decimales.
4. **El operador es estricto**: se emite advertencia cuando la diferencia absoluta es **mayor** que 0.01, y **no** cuando es mayor o igual.
5. Por cada discrepancia se emite una observación de especie **advertencia**, con la posición de la pieza, el campo —`Area` o `Volumen`—, **el valor declarado y el valor derivado**, los dos.
6. Se devuelven las advertencias junto con el resultado de `CU-01`. **Los valores declarado y derivado viajan por separado y se guardan por separado**, que es lo que hace posible mostrar la discrepancia sin recalcular en cada consulta.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El valor declarado coincide con el derivado dentro de la tolerancia | **No se emite ninguna observación.** Es el criterio negativo, más difícil de acertar que el positivo: un validador que advirtiera siempre pasaría E-3 y fallaría E-4 | Paso 6, con la pieza siguiente |
| FA-02 | La diferencia absoluta es **exactamente 0.01** | **No se emite advertencia**, por el operador estricto del paso 4 | Paso 6, con la pieza siguiente |
| FA-03 | La pieza es plana y no declara `Volumen` | Se verifica sólo el `Area`. La ausencia de `Volumen` en una figura plana no es un defecto y no produce observación | Paso 6, con la pieza siguiente |
| FA-04 | Una dimensión de la pieza vale `0.00` | Se deriva igual y se compara igual: el cero es un valor y la comparación es por existencia del campo, no por su verosimilitud geométrica. A lo sumo produce **advertencia**, nunca un error de validación | Paso 6, con la pieza siguiente |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` | Se pidió verificar sin que la reconstrucción de `CU-01` haya ocurrido | Termina sin verificar y sin emitir ninguna observación. **No se infiere un conjunto vacío**: un conjunto vacío es un resultado legítimo de la interpretación y devolver «0 advertencias» acá lo haría indistinguible de un trabajo verificado sin discrepancias |

**Es la única condición de este contrato, y es una decisión derivada** —ninguna fuente la enuncia—: se declara como tal en §10 y queda registrada como punto abierto propio de la categoría en `Especificacion-Funcional.md` §11.

## 7. Postcondiciones

- **Éxito:** el consumidor recibe el conjunto de advertencias, cada una con posición, campo, valor declarado y valor derivado. Ninguna pieza fue modificada y **ningún valor declarado fue corregido**.
- **Éxito sin discrepancias:** el conjunto de advertencias está vacío, y eso es un resultado, no un fallo.
- **Fallo:** el consumidor recibe el código y nada más.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Las tres piezas del escenario **E-1** ya reconstruidas | Se verifican sus valores | Se emiten exactamente **2 advertencias**: área del cubo, declarada 36.00 contra derivada 54.00, y volumen del ortoedro, declarado 343.00 contra derivado 1029.00 |
| CA-02 | El mismo escenario **E-1** | Se verifica el **cilindro** | **No produce ninguna observación**: su área declarada es 113.10 y la suma de sus componentes da 113.09, con una diferencia de **exactamente 0.01**, que el operador estricto no advierte. Con «mayor o igual» este escenario daría 3 advertencias y el caso de prueba canónico del producto fallaría |
| CA-03 | El mismo escenario **E-1** | Se verifica el **área del ortoedro** | **No produce observación**: `2·49 + 4·147 = 686.00` coincide con lo declarado |
| CA-04 | El cubo del escenario **E-3**, de `Ejemplo1` | Se verifica | **1 advertencia de área**: declarada 36.00, derivada 54.00. El mensaje expresa **los dos valores**, nunca un texto genérico. `Volumen` declarado 27.00 coincide con `3³` y **no** produce observación |
| CA-05 | El cubo del escenario **E-4**, de `Ejemplo2`, con área declarada 54.00 | Se verifica | **Cero observaciones en total.** Comparado con CA-04, es la prueba de que la verificación mide la geometría y no la forma del texto |
| CA-06 | El ortoedro del escenario **E-2** | Se verifica | **1 advertencia de volumen**: derivado `7·7·21 = 1029.00` contra declarado 343.00. El `Area` derivada `2·49 + 4·147 = 686.00` coincide con la declarada y **no** produce observación |
| CA-07 | La figura del escenario **E-6**, un `Rectangulo` con `"Largo": 0.00` y `"Area": 0.00` | Se verifica | Se produce **a lo sumo una advertencia** por el valor derivado, y **nunca** una observación de especie error de validación. La figura no se descarta |
| CA-08 | Cualquier pieza con discrepancia | Se verifica y se compara el texto original antes y después | El texto original es **idéntico**: la discrepancia se señala y el valor del alumno **no se corrige** (RN-08) |
| CA-09 | Dos piezas cuyas diferencias absolutas son 0.010 y 0.011 respectivamente | Se verifican | La primera **no** produce advertencia y la segunda **sí**. Es el criterio que ancla el operador estricto en una prueba y no sólo en la prosa |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-05, y NB-04 en cuanto la verificación opera sobre el dato fiel |
| Reglas de negocio aplicables | [RN-05](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) —una advertencia **no** impide el paso a estado `Pendiente`—, [RN-08](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) por la ubicación de la observación |
| Puerto que implementa | Validación de figuras, en su mitad de verificación de valores |
| Consumidor | `GeometriaFactory-Application` [`CU-05`](../../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md) |
| Escenarios del intake que lo ejercitan | E-1, E-2, E-3, E-4, E-6 (§20), con la matriz de §21 |
| Historias de usuario a generar en 06 | US-05, US-06, US-07 |
| Componentes esperados en 05 | Adaptador de verificación de valores, con la tabla de derivación por tipo y el comparador de tolerancia |
| Tests previstos en 08 | Los cuatro casos de la batería obligatoria que corresponden a la verificación de valores —área del cubo de `Ejemplo1`, volumen del ortoedro, dimensión en cero y el escenario semilla completo—, más la prueba del operador estricto de CA-09 |

## 10. Notas y supuestos

- **La tolerancia de 0.01 no es una asunción.** Sale de que el emisor redondea a dos decimales, y el intake la separa explícitamente de las asunciones numéricas de §22.
- **El operador estricto sí es una decisión, y está tomada.** `PRODUCT-INTAKE` §17.3.P.10 la declara con su fundamento: sobre el escenario semilla decide si el producto devuelve las **dos** advertencias documentadas o tres. Toda implementación y todo caso de prueba lo usan.
- **De dónde sale el valor derivado del área de una pieza volumétrica.** El intake lo muestra en dos lugares: el área del cilindro de E-1 se contrasta contra **la suma de sus componentes** (§17.3.P.10) y la del ortoedro de E-2 contra `2·49 + 4·147` (§20.E-2), que es la misma suma. En el cubo de E-3 el intake escribe la derivación como `6·l²`, que **coincide** con la suma de sus seis componentes declarados de 9.00: no hay contradicción entre las dos formas de escribirlo. El volumen, en cambio, se deriva de las dimensiones —`7·7·21`, `3³`—. **Este contrato adopta la suma de componentes**, y como ninguna fuente enuncia la regla, el punto queda registrado como abierto en `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 y en `Especificacion-Funcional.md` §11.
- **Advertencia y error de validación son dos especies de la misma noción y no se confunden.** Sólo el error de validación impide el paso a estado `Pendiente`. Este contrato **nunca** emite errores de validación: los emite `CU-01`.
- **La condición `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` nació como decisión derivada de esta categoría y hoy la enuncia la fuente.** Ninguna fuente la enunciaba: se declaró acá porque la alternativa —devolver un conjunto vacío de advertencias— sería indistinguible de un trabajo verificado sin discrepancias, y eso convertiría un defecto de orquestación en un resultado creíble. **El Product Owner la confirmó tal como está** en `PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5 (2026-08-12), adoptando ese mismo fundamento y agregando el del otro extremo: del lado del visor, una escena vacía sin motivo es el **fallo silencioso** que el producto viene a eliminar. **El enunciado, la fila de §6 y los criterios de aceptación no cambian**; lo que cambia es que deja de ser derivación. El punto abierto que la registraba en `Especificacion-Funcional.md` §11 queda **cerrado con su desenlace y su fecha**.
- **La partición frente a `CU-01` es la misma que el dominio hizo entre su `CU-06` y su `CU-07`**: trazan a necesidades distintas —NB-04 y NB-05—, con métricas distintas, y sus observaciones son de especies distintas con efectos opuestos sobre el estado del trabajo.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-04**: el cierre de §6 afirmaba que la condición `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` estaba declarada en `Especificacion-Funcional.md` §11 y §11 no la contenía; la afirmación se vuelve verdadera al incorporarse el punto a §11, y la remisión se reescribe para nombrar el registro que ahora existe. La nota de §10 sobre el valor derivado del área de una pieza volumétrica declara dónde queda registrado ese punto abierto —`Definicion-Contrato-Del-Validador-De-Figuras.md` §9 y `Especificacion-Funcional.md` §11—, que era el segundo de los seis que el índice maestro no recogía. **H-02**: la trazabilidad upstream cita el `PRODUCT-INTAKE` **1.12**. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Absorbe la decisión (c) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.3 P.11 punto 5): se **confirma** la condición `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` tal como esta capa la había declarado, con su fundamento —cuando ninguna pieza se pudo reconstruir corresponde una condición propia, y no una lista vacía de observaciones ni una escena en blanco—. **El enunciado no cambia**: lo que cambia es que deja de ser derivación y pasa a estar enunciada por la fuente. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |

## 17. Compatibilidad de la superficie pública

Agregar un campo a la advertencia es compatible mientras conserve la posición, el campo y **los dos valores**. **Cambiar la tolerancia, cambiar el operador de estricto a no estricto, corregir el valor declarado o convertir una advertencia en error de validación son cambios incompatibles** y suben versión mayor: los cuatro cambian el resultado de escenarios declarados del producto, y el tercero contradice además RN-08.
