> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-06020-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-06020-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md`](../../US-06020-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-06020 — Terminar sin producir valor cuando la fuente de aleatoriedad no responde

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-06020-Terminar-Sin-Producir-Valor-Cuando-La-Aleatoriedad-No-Responde.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-06003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la producción de la provisoria se detenga y lo diga cuando la fuente de material impredecible no responde**, para **que nunca se produzca una provisoria adivinable con el reseteo pareciendo haber funcionado**.

## 2. Contexto

`05` §9 declara este riesgo con impacto **muy alto** y lo enuncia con todas las letras: si el valor se compusiera por un contador, la fecha o el correo, se produciría una provisoria adivinable **y el reseteo parecería haber funcionado**. Y agrega la asimetría que lo hace grave: **un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa**. El contrato de uso es [`CU-06007`](../../../../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md).

## 3. Criterios de aceptación

- Given una fuente de material impredecible que no responde, When se pide la provisoria, Then se devuelve la condición correspondiente y **no se produce ningún valor**.
- Given esa terminación, When se inspecciona el efecto, Then **no hay ninguno**: la cuenta queda como estaba y no se escribió ninguna marca.
- Given esa misma situación, When se busca en el mecanismo un camino alternativo para componer el valor, Then **no hay ninguno**: el atajo está escrito como prohibido.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-06007 |
| RN que ejerce | RN-06014 |
| Componente de `05` §3.1 | Mecanismo de credenciales |
| Reglas conceptuales de modelo | — |
| ¿Toma alguna decisión de negocio? | **No** |
| ¿Toca el almacén? | **No** |
| BT derivadas | BT-06014, BT-06021 |
| Tests previstos en 08 | Prueba con la fuente de aleatoriedad indisponible, comprobando que no se produce valor |

## 5. Prioridad y estimación

`Must` porque es la materialización de la propiedad estructural que `05` §2 declara para toda la capa: **cuando un mecanismo no puede cumplir su promesa, se detiene y lo dice**; no la cumple a medias, no compone un valor por otro medio y no cae hacia un sustituto.

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

**Es una de las historias cuyo entregable es una terminación y no un efecto.** Su valor se mide por lo que **no** ocurre, y por eso su criterio central se expresa como una ausencia con umbral cero.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
