# US-10006 — Cambiar la contraseña propia presentando la vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-10006-Cambiar-La-Contrasena-Propia-Presentando-La-Vigente.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Credencial-Propia`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta habilitada**, quiero **cambiar mi contraseña presentando la vigente**, para **elegir una clave que sólo yo conozca sin que nadie más pueda cambiármela**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el cambio de contraseña exigiendo la actual. El caso de uso es [`CU-10003`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10003-Establecer-Y-Cambiar-La-Contrasena-Propia.md) y la superficie es `Credencial-Propia`, alojada en el shell de trabajo cuando el cambio es voluntario.

## 3. Criterios de aceptación

- Given una sesión abierta, When la persona presenta su contraseña vigente y escribe la nueva dos veces, Then el cambio procede y la sesión sigue siendo válida.
- Given una contraseña vigente equivocada, When confirma, Then la superficie muestra el rechazo y **la contraseña no cambia**.
- Given cualquiera de los dos caminos, When se inspecciona el navegador, Then **ninguna de las contraseñas escritas queda ahí**: la solicitud la arma el servidor de esta pieza.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-10003 |
| Restricciones transversales que la alcanzan | RT-01, RT-02, RT-03 |
| Componente de `05` §3.1 | Superficies, Sesión y estado del circuito, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | La verificación de la contraseña vigente es de `GeometriaFactory-Infrastructure` |
| BT derivadas | BT-10011, BT-10013, BT-10014 |
| Tests previstos en 08 | Paso del guion de la etapa `c`, con la comprobación de persistencia entre reinicios |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que el cambio de contraseña **persista entre reinicios**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

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

**Los tres cursos de esta superficie son el mismo formulario y el mismo contrato** desde `PRODUCT-INTAKE` 1.13 (`RN-10016`): el cambio voluntario de esta historia, el primer ingreso y el cambio obligado tras un reseteo. Lo que los distingue es de dónde se llega y si hay salida, y eso es US-10007 y US-10028.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
