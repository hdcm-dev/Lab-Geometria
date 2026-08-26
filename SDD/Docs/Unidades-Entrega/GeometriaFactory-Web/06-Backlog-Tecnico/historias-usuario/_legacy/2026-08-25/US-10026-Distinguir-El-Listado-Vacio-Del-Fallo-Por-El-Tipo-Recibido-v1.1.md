> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10026-Distinguir-El-Listado-Vacio-Del-Fallo-Por-El-Tipo-Recibido.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10026-Distinguir-El-Listado-Vacio-Del-Fallo-Por-El-Tipo-Recibido.md`](../../US-10026-Distinguir-El-Listado-Vacio-Del-Fallo-Por-El-Tipo-Recibido.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10026 — Distinguir el listado vacío del fallo por el tipo recibido y no por el conteo

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10026-Distinguir-El-Listado-Vacio-Del-Fallo-Por-El-Tipo-Recibido.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Estado-Degradado-Y-Reconexion`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona que usa el laboratorio**, quiero **que la aplicación me diga si no tengo nada o si el servicio no respondió**, para **no confundir una lista vacía con una falla**.

## 2. Contexto

`NB-00008` pide alcance del laboratorio desde el aula, y `CL-2` del intake §7 declara este caso límite. El caso de uso es [`CU-10010`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md). `RT-07` de `02` §6 lo fija: **el listado vacío se distingue del fallo por el tipo recibido y no por el conteo**.

## 3. Criterios de aceptación

- Given un listado que responde con una colección vacía, When se lo muestra, Then la superficie declara el **vacío** con su mensaje propio.
- Given un listado que no obtiene respuesta, When se lo intenta mostrar, Then la superficie declara el **estado degradado**, con qué pasó, por qué y qué hacer.
- Given los dos casos, When se compara cómo se los distinguió, Then se los distinguió **por el tipo recibido** y no contando elementos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00008 |
| CU cubiertos | CU-10010 |
| Restricciones transversales que la alcanzan | RT-03, RT-06, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La respuesta de error neutra la declara el ensamblado de contratos |
| BT derivadas | BT-10013 |
| Tests previstos en 08 | Ejercicio del traductor con el camino de ausencia de respuesta, **sin red** |

## 5. Prioridad y estimación

`Must` porque `NB-00008` es una de las nueve necesidades y **ésta es la única parte de ella que la persona ve**; y porque `05` §4 declara que confundir los dos tramos es **el error de lectura más probable de toda la pieza**.

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

**Esta historia vive en la etapa `c` y no en la `a`**, y el motivo es que la `c` es la primera etapa con una llamada real al servicio de datos que puede fallar. En la `a` lo que hay es la página de salud, que es BT-10003 y no una historia.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
