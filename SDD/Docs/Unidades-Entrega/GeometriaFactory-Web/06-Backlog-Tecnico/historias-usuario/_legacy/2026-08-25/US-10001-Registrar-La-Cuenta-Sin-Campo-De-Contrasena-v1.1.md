> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `US-10001-Registrar-La-Cuenta-Sin-Campo-De-Contrasena.md` en su versión **1.1**, tomada el 2026-08-25 por el orquestador de migración normativa **antes** de aplicar el corte de la categoría 06 de la fase M4 (`Master-Prompt.md` §5 y §8).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-25
> - **Versión vigente:** [`US-10001-Registrar-La-Cuenta-Sin-Campo-De-Contrasena.md`](../../US-10001-Registrar-La-Cuenta-Sin-Campo-De-Contrasena.md)
>
> El cuerpo que sigue **no se modifica**. Lo único que se tocó son **los enlaces relativos**, reescritos dos niveles para que sigan resolviendo desde esta ubicación.

---

# US-10001 — Registrar la cuenta con correo, nombre y apellido, sin campo de contraseña

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10001-Registrar-La-Cuenta-Sin-Campo-De-Contrasena.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10004 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Registro-De-Cuenta`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../../../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno de la comisión**, quiero **registrarme con mi correo, mi nombre y mi apellido, sin elegir contraseña**, para **entrar al laboratorio por mi cuenta y quedar esperando que el docente me habilite, sin ningún correo de por medio**.

## 2. Contexto

`NB-00002` pide identidad propia del alumno sin canal de correo y `F-02` del intake §4 la declara `Must Have`. El caso de uso es [`CU-10001`](../../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10001-Registrar-La-Cuenta-De-Alumno.md) y la superficie es `Registro-De-Cuenta`. **El registro es anónimo por diseño y debe seguir siéndolo**: `PRODUCT-INTAKE` 1.15 §4.1 precisa que lo que `RN-10016` elimina es la escritura anónima **de credencial**, no la de cuenta.

## 3. Criterios de aceptación

- Given la superficie de registro, When la persona la completa con correo, nombre y apellido, Then el registro procede y la persona recibe el aviso de que su cuenta queda esperando habilitación.
- Given esa misma superficie, When se inspecciona el formulario, Then **no tiene ningún campo de contraseña**: la credencial inicial la produce la habilitación.
- Given el envío del formulario, When se cuenta desde dónde sale la solicitud, Then sale del **servidor de esta pieza** y no del navegador.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-10001 |
| Restricciones transversales que la alcanzan | RT-01, RT-03, RT-06, RT-07 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | La unicidad del correo la resuelve `GeometriaFactory-Application` y la sostiene el almacén |
| BT derivadas | BT-10008, BT-10011, BT-10013 |
| Tests previstos en 08 | Paso del guion de demostración de la etapa `d`, y conteo de peticiones del navegador |

## 5. Prioridad y estimación

`Must` por derivar de `F-02`, `Must Have`, y porque el criterio de transición `d` → `e` exige que un alumno se registre con correo, nombre y apellido **sin elegir contraseña**.

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

**El aviso de cuenta esperando habilitación es parte de esta superficie y no del ingreso.** El criterio de transición `d` → `e` exige además que un alumno cuya cuenta está en estado `Pendiente` reciba un aviso explícito al intentar ingresar, y eso es US-10004.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
