# US-06018 — Verificar una credencial y distinguir el valor derivado ilegible de la contraseña equivocada

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** US-06018-Verificar-Una-Credencial-Y-Distinguir-El-Derivado-Ilegible.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la verificación de una credencial distinga una contraseña equivocada de un valor derivado que no se puede leer**, para **no tratar un problema del almacén como si fuera un error de la persona**.

## 2. Contexto

`02` §8 declara que la derivación y la verificación quedaron en el mismo contrato de uso porque **son la misma función mirada desde los dos lados**: no se puede verificar sin saber cómo se derivó. El contrato de uso es [`CU-06006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md).

## 3. Criterios de aceptación

- Given una contraseña correcta y su valor derivado, When se verifica, Then el veredicto es afirmativo.
- Given una contraseña equivocada, When se verifica, Then el veredicto es negativo, y es un **resultado** y no un fallo.
- Given un valor derivado que **no se puede leer** —parámetros ausentes o incompatibles—, When se verifica, Then se devuelve una condición **distinta** de la contraseña equivocada, y es un **fallo**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-06006 |
| RN que ejerce | — directamente; su veredicto alimenta la admisibilidad, que resuelve el dominio |
| Componente de `05` §3.1 | Mecanismo de credenciales |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** El veredicto no es una decisión de acceso |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06003, BT-06013, BT-06021 |
| Tests previstos en 08 | Prueba con un valor derivado ilegible, comprobando que la condición es distinta |

## 5. Prioridad y estimación

`Must` porque la distinción entre **resultado** y **fallo** es la que gobierna las 17 condiciones de esta capa, y confundirlos es el segundo riesgo de `05` §9, con probabilidad **alta**.

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

**El mensaje que llega a la persona no se decide acá.** La respuesta genérica de credenciales inválidas **sin declarar cuál campo falló** es una de las tres familias deliberadamente empobrecidas de `GeometriaFactory-Api`; lo que esta historia produce es un veredicto tipado que esa capa traduce.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
