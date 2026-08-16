# US-00026 — Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-00026-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00001 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** Ninguno: la composición de raíz no atiende peticiones
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que los cuatro puertos queden conectados con sus cuatro adaptadores en un solo lugar y que toda la configuración del despliegue entre por ahí**, para **que la frontera sea contable y que un puerto sin adaptador falle en construcción y no en la primera petición**.

## 2. Contexto

`02` §3 declara la **composición de la aplicación** como una de las cinco responsabilidades. `05` §2.1 descartó repartirla en módulos por área con un fundamento propio: **el defecto característico de esta capa es de omisión**, y un puerto sin adaptador **se detecta comparando contra una lista, no leyendo un módulo**. El contrato de uso es [`CU-00010`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00010-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md).

## 3. Criterios de aceptación

- Given la composición de raíz, When arranca el servicio, Then los **4 de 4** puertos resuelven a su adaptador, y hay **0** puertos sin adaptador o con más de uno.
- Given un puerto sin adaptador, When se intenta arrancar, Then **falla en construcción** y **no hay petición que responder**.
- Given la configuración del despliegue —ubicación del almacén, clave de firma, vigencia del acceso y límite de cuerpo—, When se la busca en el código, Then **entra sólo por acá** y **ningún componente la lee por su cuenta**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-00010` **no traza a ninguna necesidad**: conectar un puerto con su adaptador es **construcción, no capacidad**, y nadie la percibe |
| CU cubiertos | CU-00010 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Composición de raíz, **transversal** |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00002, BT-00008, BT-00010 |
| Tests previstos en 08 | Prueba de arranque que resuelve las cuatro dependencias y falla en construcción si falta alguna |

## 5. Prioridad y estimación

`Must` porque **todo lo demás es probable con dobles gracias a esta historia**, que es lo que las tres capas de adentro dan por sentado; y porque `05` §9 declara con probabilidad media el riesgo de que un puerto quede sin adaptador y **el fallo aparezca en la primera petición, en producción y sin nadie mirando**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap, o declara que su caso de uso no traza a ninguna y por qué
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza, o declara que no realiza ninguno, y el componente de `05` §3.1
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El nombre del cuarto puerto se fija en el punto de control de la etapa `a`, y no acá.** Lo declara `GeometriaFactory-Application` y su ADR correspondiente lo ató a ese punto de control; esta capa **conecta exactamente cuatro** puertos con cuatro adaptadores y no nombra ninguno por su cuenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
