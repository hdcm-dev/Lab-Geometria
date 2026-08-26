> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10018-Abrir-El-Trabajo-Y-Encontrar-Los-Mismos-Cuatro-Elementos.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10018-Abrir-El-Trabajo-Y-Encontrar-Los-Mismos-Cuatro-Elementos.md`](../../US-10018-Abrir-El-Trabajo-Y-Encontrar-Los-Mismos-Cuatro-Elementos.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10018 — Abrir el trabajo y encontrar los mismos cuatro elementos que ve el administrador

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10018-Abrir-El-Trabajo-Y-Encontrar-Los-Mismos-Cuatro-Elementos.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10007 Visualización del trabajo
**Etapa del producto:** `g`
**Superficie de 03:** `Vista-De-Trabajo`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno dueño de un trabajo, y como administrador que lo revisa**, quiero **abrirlo y encontrar exactamente los mismos elementos**, para **que el docente revise lo mismo que el alumno entregó y no una versión distinta**.

## 2. Contexto

`NB-00006` pide visualización dentro del producto y `NB-00007` revisión desde un solo lugar. El caso de uso es [`CU-10007`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10007-Abrir-Un-Trabajo-Y-Explorarlo-En-Escena-Y-Arbol.md), **emitido como caso de uso propio y único para los dos papeles** porque esa identidad es un criterio de éxito de negocio —«4 de 4 elementos»— y dos casos de uso la habrían duplicado y habrían admitido que divergieran (`02` §3.1).

## 3. Criterios de aceptación

- Given un trabajo interpretado, When lo abre su dueño, Then encuentra sus datos, su texto, la escena y el árbol.
- Given ese mismo trabajo, When lo abre el administrador, Then encuentra **exactamente lo mismo**: el administrador entra como actor secundario del mismo caso de uso y no como segundo actor primario.
- Given cualquiera de los dos, When se recorre la escena, Then **no hay tráfico de circuito hacia el servidor** y el texto del trabajo viajó **una sola vez**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00006, NB-00005, NB-00007, NB-00009, NB-00004 |
| CU cubiertos | CU-10007 |
| Restricciones transversales que la alcanzan | RT-04, RT-05, RT-10, RT-11, RT-13 |
| Componente de `05` §3.1 | Anfitrión del visor, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La visibilidad la decide el dominio; el dibujo es del bundle |
| BT derivadas | BT-10016, BT-10017, BT-10018 |
| Tests previstos en 08 | Paso del guion de la etapa `g`, y el conteo de tráfico de circuito durante la interacción |

## 5. Prioridad y estimación

`Must` por derivar de `F-11`, `Must Have`, y porque el criterio de transición `g` → `h` exige que **el administrador abra cualquier trabajo que ve y encuentre exactamente lo mismo que vio el alumno**.

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

**El anfitrión del visor es un componente de esta arquitectura y no del bundle** (`05` §2.2): la capa 1 del contrato de fachada **vive en este proyecto de código**, y su ciclo de vida —incluida la liberación— es responsabilidad de acá.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
