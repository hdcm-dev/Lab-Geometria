> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-00004-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-00004-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md`](../../US-00004-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-00004 — Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00004-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00002 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Los **once** puntos bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que ninguna petición a un punto que exige acceso se atienda sin un acceso válido**, para **que la única puerta del servidor propio no se pueda cruzar sin identidad**.

## 2. Contexto

`02` §3 declara la **admisión de la petición** como una de las cinco responsabilidades de esta capa, y `05` §3.1 la aloja en un componente **transversal a los once puntos que exigen acceso**. El contrato de uso es [`CU-00022`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md).

## 3. Criterios de aceptación

- Given una petición a un punto bajo la guardia **sin** acceso, When se la recibe, Then se rechaza y **no se ejerce ningún caso de uso**.
- Given un acceso **vencido** o con **firma que no corresponde**, When se lo presenta, Then se rechaza igual, y los dos casos son distinguibles entre sí en el registro del servidor.
- Given ese rechazo, When se lo inspecciona, Then queda **registrado del lado del servidor** junto con todo intento de acceso rechazado.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-00002 |
| RN que ejerce | RN-00001 en el transporte del papel |
| Componente de `05` §3.1 | Guardia de admisión |
| ¿Decide qué se dice? | **No.** El mecanismo de verificación es de `GeometriaFactory-Infrastructure`; **exigirlo en cada punto es de acá** |
| Familia empobrecida | **No** |
| BT derivadas | BT-00010, BT-00011, BT-00012 |
| Tests previstos en 08 | Batería de integración sobre los once puntos bajo la guardia |

## 5. Prioridad y estimación

`Must` porque **un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio**, y todo lo que esta capa no proteja queda expuesto.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los diecisiete códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**Los cuatro puntos que no exigen acceso son ausencias declaradas y contables**: canje, registro de cuenta, configuración del administrador y salud. `05` §3.4 los enumera uno por uno **para que la ausencia sea contable**, y BT-00012 verifica que sean exactamente cuatro.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
