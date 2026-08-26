> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md`](../../US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-04019 — Devolver el detalle con piezas y componentes, y el listado sin componentes

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04019-Devolver-El-Detalle-Con-Piezas-Y-Componentes-Y-El-Listado-Sin-Componentes.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04004 Gestión del trabajo
**Etapa del producto:** `e`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que el detalle traiga las piezas con su identidad posicional y sus componentes, y que el listado no los traiga**, para **tener con qué dibujar y armar el árbol sin volver pesada la pantalla más cargada del producto**.

## 2. Contexto

`NB-00006` pide visualización dentro del producto, y `02` §7.2 declara que **lo que esta capa aporta a esa necesidad es la entrega de las piezas con su identidad posicional y sus componentes en el detalle**. El contrato de uso es [`CU-00028`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00028-Consultar-El-Listado-Y-El-Detalle-De-Los-Trabajos.md). `PRODUCT-INTAKE` §17.1.P.10 · GeometriaFactory-Application declara que las consultas de listado **nunca cargan los componentes**.

## 3. Criterios de aceptación

- Given un trabajo interpretado, When se pide su detalle, Then vienen sus piezas con su **posición** y sus componentes.
- Given el listado del alumno y el de la comisión, When se los resuelve, Then los componentes cargados son exactamente **0** en los dos.
- Given un conjunto de piezas con huecos —posiciones sin pieza reconstruida—, When se lo devuelve, Then las posiciones **no se renumeran**: la posición es la identidad de la pieza.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00006 (parcial) |
| CU cubiertos | CU-04006 |
| RN e invariantes que ejerce | RN-04003, RN-04009 |
| Componente de `05` §3.1 | Orquestación de la consulta |
| Puertos que consume | Repositorio de trabajos |
| Comprobación de `02` §4 que la alcanza | Pertenencia, y cambio de contraseña pendiente antes que ella |
| BT derivadas | BT-04007, BT-04016 |
| Tests previstos en 08 | Inspección de la proyección devuelta, comprobando que la colección de componentes no viene materializada |

## 5. Prioridad y estimación

`Must` porque `05` §8 fija el NFR de **0** componentes cargados en los dos listados y porque sin las piezas con su posición no hay con qué sincronizar el árbol y la escena en la etapa `g`.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**El dibujo, el árbol y la sincronización no son de esta capa**: son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`. Por eso `NB-00006` figura como cobertura **parcial** en `02` §7.2, y por eso esta historia vive en la etapa `e` y no en la `g`.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
