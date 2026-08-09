> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `CU-07-Registrar-Las-Observaciones-Del-Trabajo.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-07-Registrar-Las-Observaciones-Del-Trabajo.md`](../../CU-07-Registrar-Las-Observaciones-Del-Trabajo.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-07 — Registrar las observaciones del trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-07-Registrar-Las-Observaciones-Del-Trabajo.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-05`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) §1, §4 y §5; [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) §5 (localización del defecto); `00-Contexto/Vision-Producto.md` §9.1 (observación, advertencia, error de validación); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-4), §17.1.P.11 punto 3, §17.2.P.11 punto 2, §17.3.P.10 (tolerancia 0.01), §20.E-1, §20.E-3, §20.E-4, §20.E-5
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

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
- [12. Compatibilidad de la superficie pública](#12-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Incorporar al trabajo las observaciones que produjeron la interpretación de su texto y la verificación de sus valores, cada una con su especie, su ubicación y, cuando corresponde, el valor declarado y el derivado. Es el contrato que sostiene el mayor valor didáctico del producto: la discrepancia se **señala**, no se corrige ni se rechaza.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Entrega al trabajo las observaciones que obtuvo de la interpretación y de la verificación de valores |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Produce esas observaciones detrás del puerto del validador de figuras |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica que cada observación esté bien formada y la adopta |

El alumno es el sujeto: es quien ve la advertencia sobre su propio trabajo. El actor del contrato es el código consumidor.

## 3. Precondiciones

- El trabajo existe y su conjunto de piezas ya fue reconstruido, salvo en las observaciones que expresan que la reconstrucción no fue posible.
- Cada observación llega con su especie declarada: `Advertencia` o `Error de validación`.
- Las advertencias de discrepancia de valor llegan con el valor declarado y el derivado, y la comparación que las produjo se resolvió con tolerancia absoluta de 0.01, nunca por igualdad exacta.

## 4. Flujo principal

1. La capa de aplicación entrega al trabajo el conjunto de observaciones.
2. El dominio descarta las observaciones de una verificación anterior, si las hubiera.
3. El dominio verifica que la especie de cada observación pertenezca al conjunto cerrado.
4. El dominio verifica que toda observación de especie error de validación indique la posición de la pieza y el campo (RN-09).
5. El dominio verifica que toda advertencia de discrepancia de valor traiga el valor declarado y el derivado, los dos.
6. El dominio adopta el conjunto de observaciones.
7. El dominio deja disponible, para el consumidor, si el trabajo tiene al menos una observación de especie error de validación, que es la condición que gobierna la finalización (CU-08).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La verificación no encuentra ninguna discrepancia | El dominio adopta un conjunto vacío de observaciones. Es el caso del escenario E-4, el mismo cubo emitido por `Ejemplo2`, cuyo valor declarado sí se corresponde con sus dimensiones: **el criterio negativo importa tanto como el positivo** | Paso 6 |
| FA-02 | La observación no es atribuible a ninguna figura, por ejemplo un conjunto raíz vacío | El dominio la adopta sin posición de pieza, con el campo que el consumidor indique. Sigue siendo de especie error de validación | Paso 6 |
| FA-03 | El mismo trabajo se reprocesa después de una mejora del validador | El dominio reemplaza el conjunto de observaciones entero. El texto original nunca cambió, y eso es lo que hace posible reprocesar | Paso 2 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `ESPECIE_DE_OBSERVACION_DESCONOCIDA` | La especie no es `Advertencia` ni `Error de validación` | Rechaza el conjunto entero. La especie es lo que decide el efecto sobre la finalización y no admite un tercer valor |
| `ERROR_SIN_UBICACION` | Una observación de especie error de validación no indica posición de pieza ni campo, siendo atribuible a una figura | Rechaza el conjunto: un mensaje genérico es exactamente lo que el producto viene a eliminar (RN-09) |
| `ADVERTENCIA_SIN_LOS_DOS_VALORES` | Una advertencia de discrepancia no trae el valor declarado o no trae el derivado | Rechaza el conjunto: sin los dos números la advertencia no explica nada |
| `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | La posición de pieza indicada no existe en el conjunto de piezas del trabajo | Rechaza el conjunto |

## 7. Postcondiciones

- **Éxito:** el trabajo tiene el conjunto de observaciones adoptado, cada una bien formada, y expone si hay al menos un error de validación. El texto original y el conjunto de piezas no cambiaron.
- **Fallo:** el conjunto de observaciones anterior se conserva y el trabajo queda sin modificar.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo con las 3 piezas del escenario E-1 y las observaciones de su verificación | La capa de aplicación las entrega al trabajo | El dominio adopta 2 observaciones, las 2 de especie advertencia: área de la pieza 1, declarada 36.00 contra derivada 54.00, y volumen de la pieza 2, declarado 343.00 contra derivado 1029.00 |
| CA-02 | Un trabajo con el cubo del escenario E-4, de área declarada 54.00 y derivada 54.00 | La capa de aplicación entrega el resultado de la verificación | El dominio adopta 0 observaciones |
| CA-03 | Un trabajo con el texto del escenario E-5 y su observación de tipo desconocido | La capa de aplicación la entrega al trabajo | El dominio adopta 1 observación de especie error de validación, con posición de pieza 1 y campo `Tipo`, y expone que el trabajo tiene al menos un error de validación |
| CA-04 | Una observación de especie error de validación sin posición de pieza y sin campo, atribuible a la pieza 0 | La capa de aplicación la entrega al trabajo | El dominio rechaza con el código `ERROR_SIN_UBICACION` |
| CA-05 | Una advertencia de área con valor declarado 36.00 y sin valor derivado | La capa de aplicación la entrega al trabajo | El dominio rechaza con el código `ADVERTENCIA_SIN_LOS_DOS_VALORES` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-05, y NB-04 en su criterio de localización del defecto |
| Reglas de negocio aplicables | [RN-05](../Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md), [RN-08](../Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-09](../Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) |
| Invariantes | INV-04 |
| Historias de usuario a generar en 06 | US de registro de advertencias de valor, US de registro de errores de validación con ubicación |
| Componentes esperados en 05 | Entidad de observación con su especie cerrada y su ubicación |
| Tests previstos en 08 | Pruebas unitarias con los escenarios E-1, E-3, E-4 y E-5; el caso de E-4 es la prueba del criterio negativo, que un validador que advirtiera siempre no pasaría |

## 10. Notas y supuestos

- La **comparación** entre valor declarado y derivado la hace el validador de figuras, no el dominio. Acá entra su resultado. La tolerancia absoluta de 0.01 está declarada en el intake y no es una asunción: sale de que el emisor redondea a dos decimales.
- Una advertencia nunca impide guardar ni finalizar. Esa consecuencia es de RN-05 y se ejerce en CU-08.
- «Observación» es acá el término superordinado y es el correcto, porque la entidad modelada es una y su especie es un atributo. Cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado, el término que corresponde es «advertencia».

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |

## 12. Compatibilidad de la superficie pública

El conjunto de especies es cerrado y de dos valores. Agregar una tercera especie cambiaría el efecto sobre la finalización y es un cambio de alcance del modelo, no una evolución menor: exige decisión del Product Owner y revisión de RN-05.
