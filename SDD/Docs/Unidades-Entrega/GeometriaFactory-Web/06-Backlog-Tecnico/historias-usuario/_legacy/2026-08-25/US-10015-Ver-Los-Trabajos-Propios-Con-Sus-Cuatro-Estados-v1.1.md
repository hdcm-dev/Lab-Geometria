> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10015-Ver-Los-Trabajos-Propios-Con-Sus-Cuatro-Estados.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10015-Ver-Los-Trabajos-Propios-Con-Sus-Cuatro-Estados.md`](../../US-10015-Ver-Los-Trabajos-Propios-Con-Sus-Cuatro-Estados.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10015 — Ver los trabajos propios con sus cuatro estados distinguibles

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10015-Ver-Los-Trabajos-Propios-Con-Sus-Cuatro-Estados.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10005 Gestión del trabajo
**Etapa del producto:** `e`
**Superficie de 03:** `Panel-De-Trabajos-Del-Alumno`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno**, quiero **ver mis trabajos con su estado en un solo lugar**, para **saber qué entregué, qué me falta y qué me respondieron**.

## 2. Contexto

`NB-00003` pide trabajo con dueño y estado, y `F-08` del intake §4 lo declara `Must Have`. El caso de uso es [`CU-10006`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10006-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md), y la representación de fila con su insignia es una de las **tres** que la categoría 03 declara reutilizadas.

## 3. Criterios de aceptación

- Given un alumno con trabajos en los cuatro estados, When abre su panel, Then los ve todos y **cada estado es distinguible**, comunicado por al menos dos canales y nunca sólo por color.
- Given un listado sin trabajos, When se lo muestra, Then la superficie declara el vacío **como vacío** y no como fallo: la distinción se hace **por el tipo recibido y no por el conteo**.
- Given el listado, When se busca en él el comentario del administrador o el texto original, Then **no están**: el listado usa la proyección y no el detalle.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00003, NB-00009 |
| CU cubiertos | CU-10006 |
| Restricciones transversales que la alcanzan | RT-06, RT-07 |
| Componente de `05` §3.1 | Superficies, Representaciones reutilizadas, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | El alcance del listado lo decide `GeometriaFactory-Application` con la verificación de pertenencia |
| BT derivadas | BT-10009, BT-10011 |
| Tests previstos en 08 | Paso del guion de la etapa `e`, con listado poblado y listado vacío |

## 5. Prioridad y estimación

`Must` por derivar de `F-08`, `Must Have`, y porque sin el listado propio el alumno no tiene dónde ver el desenlace de US-10017.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los diecisiete códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**Que el listado no lleve el comentario ni el texto es una decisión heredada del ensamblado de contratos** y esta pieza la consume **sin invertirla**: pedirlos obligaría a traer el detalle de cada fila, que es el riesgo que `05` §9 registra con probabilidad **alta**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
