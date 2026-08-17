# ADR-08004 — Regla de exposición: lista cerrada de lo que nunca cruza la frontera

**Producto:** Fábrica de Geometría
**Documento:** ADR-08004-Regla-De-Exposicion-De-La-Frontera.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

Este ensamblado es el único tipo de dato que cruza la frontera entre las dos unidades desplegables, de modo que **es donde se decide qué se expone y qué no** (`PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Contracts). Es la decisión central del proyecto de código: no tiene comportamiento, así que lo único que decide es la forma de la frontera.

Tres reglas de arquitectura de nivel producto caen sobre esa frontera (`PRODUCT-INTAKE` §14):

- **RA-01**, ningún JavaScript del navegador invoca el servicio de datos. Es lo que sostiene las tres propiedades de la topología: sin contenido mixto, sin restricción de origen cruzado y sin exposición de la dirección del servidor propio.
- **RA-02**, el visor es un visualizador puro, sin red, sin configuración y sin identidad. Es lo que hace imposible violar RA-01 desde el navegador. No alcanza a este proyecto de código, y se nombra para declarar por qué no.
- **RA-03**, todo llega al navegador a través de la unidad pública y ningún mensaje expone direcciones de servicios internos.

La categoría 02 ya declaró la consecuencia de RA-01 sobre este ensamblado, en su restricción transversal `RT-11`: **ningún tipo habilita a que el navegador invoque el servicio de datos**, y todas las solicitudes las arma el servidor de la unidad pública, **incluidas las que llevan credenciales en claro**.

Motivación upstream: NB-00002, NB-00008; RN-08006, RN-08013, RN-08014, RN-08016; RA-01 y RA-03; INV-09.

## 2. Decisión

Se declara una **lista cerrada de lo que nunca cruza la frontera**, verificable por inspección de la superficie pública:

| Nunca cruza | Fundamento |
| --- | --- |
| El hash de la contraseña, en ninguna de sus formas | `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Contracts |
| La clave de firma | `PRODUCT-INTAKE` §17.1.P.5 · GeometriaFactory-Contracts |
| Cualquier dirección de servicio interno, en un campo o dentro de un texto | RA-03 |
| Rutas de archivos de datos y trazas de la implementación | CU-08006 §7 |
| Una condición que impida operar, como **campo** de la respuesta de sesión | `RT-10` de la categoría 02 |

Y dos decisiones de forma que la acompañan:

1. **La respuesta de sesión declara exactamente cuatro campos y ninguno más.** Las tres condiciones que impiden operar —cuenta no habilitada, cuenta que no estableció contraseña y cuenta con cambio de contraseña pendiente— viajan como **respuesta de error con código propio** y no como campo de la respuesta de sesión.
2. **Ningún tipo se diseña para que lo consuma el navegador.** Todas las solicitudes las arma el servidor de la unidad pública y viajan servidor a servidor.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Lista cerrada de prohibiciones, verificable por inspección (**adoptada**) | Se puede comprobar mecánicamente sobre la superficie pública; no depende del criterio de quien agrega un campo | Es una lista y las listas envejecen: una categoría de dato nueva no está prohibida hasta que alguien la agregue |
| Prohibición genérica —«no exponer datos sensibles»— | No envejece; cubre lo que no se anticipó | No es verificable: cada campo nuevo se discute de cero, y el defecto entra por la interpretación más laxa |
| Marcar los campos permitidos en lugar de los prohibidos, con lista blanca | Cierra por construcción: lo no listado no viaja | Obliga a enumerar todos los campos de las ocho familias en un segundo lugar que se desincroniza del primero; y la superficie pública **ya es** esa enumeración |
| Devolver la condición que impide operar como campo de la respuesta de sesión | Un solo viaje: el consumidor sabe en la misma respuesta qué pasa | Obliga a emitir una respuesta de sesión a una cuenta que por INV-09 no ejerce ninguna capacidad, que es contradictorio y observable. Descartada por `RT-10` y por la precisión de RN-08013 del intake |

## 5. Consecuencias positivas

1. **Una sola prueba de inspección** cubre la prohibición entera sobre el tipo de error, y es CA-01 de CU-08006.
2. RA-01 queda sostenida desde la forma del contrato y no sólo desde la conducta del código: no hay tipo pensado para el navegador.
3. La cuenta que no puede operar recibe un error con código, que es tratable, en lugar de una sesión que no sirve.
4. La unidad pública nunca recibe material con el que exponer la topología del producto, ni siquiera por accidente en un texto de diagnóstico.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que las credenciales en claro viajen en la solicitud** entre las dos unidades. Es inevitable —hay que canjearlas en algún lado— y la mitigación es de topología, no de contrato: el viaje es servidor a servidor y nunca lo origina el navegador.
2. **Se acepta un viaje adicional** en los caminos donde la condición se descubre al canjear: el consumidor recibe el error y encamina, en lugar de recibir todo junto.
3. **Se acepta que la lista pueda envejecer.** La mitigación es que su verificación es una prueba de inspección que se ejecuta en cada etapa, de modo que un campo nuevo que la viole aparece como fallo y no como omisión.
4. **Se acepta que este proyecto de código no pueda verificar RA-01 por sí mismo.** Lo que puede es no ofrecer ningún tipo que la facilite; la verificación estructural pertenece a 05 y 09 de los proyectos de código que despliegan.

## 7. Implementación

- Las ocho familias de [`../Arquitectura-Proyecto-Codigo.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 se revisan contra la lista de §2, campo por campo.
- El tipo de error declara **cuatro** campos y ninguno capaz de transportar lo prohibido.
- La respuesta de sesión declara **cuatro** campos y ninguno más.
- La solicitud de reseteo lleva **sólo el identificador de cuenta**: no lleva campo de contraseña, porque la provisoria la produce el sistema (RN-08014).
- **No existe ningún tipo de establecimiento anónimo de contraseña.** RN-08016 lo eliminó: el establecimiento del primer ingreso usa la solicitud de cambio de contraseña, con la provisoria como vigente. El registro de cuenta **sigue siendo anónimo por diseño** y su solicitud es un tipo de este ensamblado; lo que desapareció es la escritura anónima de credencial y no toda escritura anónima.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Campos capaces de transportar dirección, ruta de datos o secreto | Exactamente **0** en las ocho familias | Prueba de inspección de superficie pública (CA-01 de CU-08006) |
| Campos de la respuesta de sesión | Exactamente **4** | Inspección de la superficie pública |
| Condiciones que impiden operar transportadas como campo de sesión | Exactamente **0** de las tres | CA-02, CA-05 y CA-06 de CU-08001 |
| Campos de contraseña en la solicitud de reseteo | Exactamente **0** | Inspección de la familia de reseteo |
| Tipos de establecimiento anónimo de contraseña | Exactamente **0** | Inspección de la superficie pública |
| Textos de error que contienen una dirección | Exactamente **0** | CA-04 de CU-08006, con el servicio de datos detenido |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §14 (RA-01, RA-02, RA-03), §17.1.P.5 · GeometriaFactory-Contracts, §17.1.P.5 · GeometriaFactory-Api y §4.1 (RN-08013, RN-08014, RN-08016).
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../_legacy/2026-08-15-migracion-8.2/GeometriaFactory-Contracts/02-Especificacion-Funcional/Especificacion-Funcional.md) §2 y §6 (`RT-01`, `RT-02`, `RT-10`, `RT-11`).
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md`](../Contratos-Inter-Unidad/CU-08006-Contrato-De-Respuesta-De-Error.md) §7 y §8; [`CU-08001`](../Contratos-Inter-Unidad/CU-08001-Contrato-De-Canje-De-Credenciales-Y-Sesion.md); [`CU-08008`](../Contratos-Inter-Unidad/CU-08008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md).
- ADR relacionadas: [`ADR-08001`](ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md), [`ADR-08002`](ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la lista cerrada de cinco categorías de dato que nunca cruzan la frontera, las dos decisiones de forma que la acompañan, el tratamiento de RA-01 y RA-03 sobre este proyecto de código y la declaración de por qué RA-02 no lo alcanza, cuatro alternativas evaluadas y seis métricas de validación. |
