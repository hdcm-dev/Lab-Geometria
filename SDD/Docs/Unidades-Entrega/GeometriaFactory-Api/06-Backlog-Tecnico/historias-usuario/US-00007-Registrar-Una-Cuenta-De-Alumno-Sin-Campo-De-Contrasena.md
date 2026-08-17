# US-00007 — Registrar una cuenta de alumno sin campo de contraseña

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-00007-Registrar-Una-Cuenta-De-Alumno-Sin-Campo-De-Contrasena.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-00003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-02`, **fuera de la guardia**: el registro es anónimo por diseño
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el registro de una cuenta de alumno sin ningún campo de contraseña**, para **que el alumno entre al laboratorio por su cuenta y la credencial inicial la produzca la habilitación**.

## 2. Contexto

`F-02` del intake §4 es `Must Have`, y `PRODUCT-INTAKE` **1.15** §4.1 precisa que lo que `RN-00016` elimina es la escritura anónima **de credencial**, no la de cuenta: **el registro es anónimo por diseño y así debe seguir**, porque es como el alumno entra al laboratorio. El contrato de uso es [`CU-00021`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md).

## 3. Criterios de aceptación

- Given los datos de alta, When se los envía al punto de registro, Then la cuenta queda constituida y el punto **no exige acceso firmado**.
- Given ese punto, When se inspecciona su solicitud, Then **no tiene ningún campo de contraseña**.
- Given un correo ya registrado, When se lo envía, Then la respuesta **no declara la situación ni el papel** de la cuenta que lo ocupa.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-00003 |
| RN que ejerce | RN-00002, RN-00016 en su efecto de que ningún punto fije una contraseña sin credencial |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La unicidad la resuelven la capa de aplicación y el almacén |
| Familia empobrecida | **Sí**: correo ya registrado sin declarar situación ni papel es la tercera de las tres |
| BT derivadas | BT-00014, BT-00016 |
| Tests previstos en 08 | Batería de integración, y la comparación de dos correos ocupados por cuentas en situaciones distintas |

## 5. Prioridad y estimación

`Must` por derivar de `F-02`, `Must Have`, y porque el criterio de transición `d` → `e` exige que un alumno se registre **sin elegir contraseña**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

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

**Es uno de los cuatro puntos que no exigen acceso firmado, y ninguno de los cuatro fija una contraseña sobre una cuenta existente.** Esa es la propiedad que hay que poder comprobar sobre la tabla de `05` §3.4, y la que hizo que `A-04` quedara **retirado y no reciclado**.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
