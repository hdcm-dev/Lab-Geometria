> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md` en su versión **1.0**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md`](../../US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-02002 — Rechazar el alta con datos obligatorios ausentes

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-02002-Rechazar-El-Alta-Con-Datos-Obligatorios-Ausentes.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca de dominio (`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`)**, quiero **que el alta se rechace con una condición nombrada cuando falta alguno de los datos obligatorios**, para **que ninguna cuenta a medio constituir llegue al almacén y que el motivo del rechazo sea el mismo siempre que la causa lo sea**.

## 2. Contexto

La capacidad `F-02` del intake §4 fija los tres datos con los que el alumno se registra. [`CU-00021`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md) declara el rechazo como camino propio. La propiedad de terminación controlada de `05` §4 exige además que un rechazo no deje la entidad a medio modificar.

## 3. Criterios de aceptación

- Given una solicitud de alta a la que le falta uno de los datos obligatorios, When se procesa, Then la operación devuelve un rechazo tipado con la condición del catálogo de 03 que le corresponde, y **no constituye ninguna entidad**.
- Given una solicitud de alta con varios datos obligatorios ausentes, When se procesa, Then el rechazo nombra la condición y no se degrada a un texto genérico.
- Given un rechazo por dato ausente, When el consumidor lo recibe, Then llega como **valor de retorno** y no como excepción, según [`ADR-02002`](../../../../05-Arquitectura-Tecnica/Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md).

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-02001 |
| RN e invariantes que ejerce | RN-02002 |
| BT derivadas | BT-02007, BT-02008 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba unitaria por dato obligatorio ausente, más la prueba de inspección que verifica que la condición emitida figura en el catálogo de 03. |

## 5. Prioridad y estimación

`Must` por derivar de `F-02`, `Must Have` en `PRODUCT-INTAKE` §4, y porque un alta parcial rompe la terminación controlada que `05` §4 declara como propiedad del proyecto de código.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Cita por identificador toda regla e invariante que ejerce, sin volver a enunciarla
- [x] Las condiciones de rechazo que produce existen en el catálogo de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Sus tareas técnicas están identificadas y ninguna está bloqueada

## 7. Notas y supuestos

Qué se considera un correo bien formado **no se decide acá**: el dominio recibe el dato ya verificado por forma (`05` §3.1, entradas del núcleo de entidades). Esta historia trata la **ausencia** del dato, no su forma.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.3 previó con este mismo identificador y este mismo contenido. |
