> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06019-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06019-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md`](../../US-06019-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06019 — Producir una contraseña provisoria no adivinable y sin repetirse

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06019-Producir-Una-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el sistema produzca la contraseña provisoria, no el administrador**, y que **no sea adivinable y no se repita**, para **que una clave escrita a mano no termine siendo la misma para toda la comisión**.

## 2. Contexto

`RN-06014` lo declara, y es **la única de las dieciséis reglas cuyo tramo principal y único está en esta capa**: `GeometriaFactory-Application` §6 declara que no tiene tramo allá, `GeometriaFactory-Contracts` la exige por sus propiedades **sin declarar mecanismo**, y la propia regla nombra a este proyecto de código como el lugar de la generación. `RN-06016` le suma un **segundo consumidor** —la habilitación— **sin agregar mecanismo**. El contrato de uso es [`CU-06007`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md).

## 3. Criterios de aceptación

- Given dos producciones consecutivas sobre la misma cuenta, When se comparan los valores, Then son **distintos**; y lo mismo entre cuentas distintas: **0** provisorias repetidas.
- Given una provisoria producida, When se intenta derivarla del nombre, del correo o de la fecha, Then **no es derivable de ninguno de los tres**.
- Given la invocación, When se inspecciona qué recibe, Then **no lleva ningún dato del acto que la motiva**: no puede distinguir una habilitación de un reseteo, ni **recibe el estado de la cuenta**, de modo que no puede comprobarlo.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-06007 |
| RN que ejerce | **RN-06014, con tramo principal y único acá**; RN-06015 de forma estructural; RN-06016 |
| Componente de `05` §3.1 | Mecanismo de credenciales |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No.** Quién habilita y cuándo lo decide la capa de aplicación |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06014, BT-06025 |
| Tests previstos en 08 | Prueba de **dos** provisorias distintas sobre la misma cuenta, y prueba de no derivabilidad |

## 5. Prioridad y estimación

`Must` por `RN-06014` y `RN-06016`, y porque el criterio de transición `d` → `e` exige que **dos reseteos consecutivos sobre la misma cuenta produzcan provisorias distintas y que ninguna sea derivable del nombre, del correo ni de la fecha**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y, si toca el almacén, las reglas conceptuales de modelo que materializa
- [x] Declara que no toma ninguna decisión de negocio
- [x] Toda condición que produce existe en el catálogo de las 17 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md), declarada como resultado o como fallo
- [x] Declara el camino en que el mecanismo se detiene en lugar de cumplir a medias, cuando puede fallar
- [x] Declara si toca el almacén y, en consecuencia, dónde vive su prueba

## 7. Notas y supuestos

**Cómo se sostiene que «no se repite» es una decisión derivada y no una transcripción.** `CU-06007` §10 adopta que la sostiene la **impredecibilidad** y **descarta** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas y el producto no guarda contraseñas en claro. Es `PA-06` de [`../Product-Backlog.md`](../../../Product-Backlog.md) §6, elevado con BT-06025.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
