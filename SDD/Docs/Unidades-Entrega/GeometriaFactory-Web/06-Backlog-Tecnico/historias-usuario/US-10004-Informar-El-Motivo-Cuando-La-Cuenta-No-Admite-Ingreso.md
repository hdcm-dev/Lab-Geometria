# US-10004 — Informar el motivo cuando la cuenta no admite ingreso

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** US-10004-Informar-El-Motivo-Cuando-La-Cuenta-No-Admite-Ingreso.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Épica:** EP-10003 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Ingreso`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta que todavía no fue habilitada o que fue bloqueada**, quiero **que la pantalla me diga en qué situación está mi cuenta**, para **saber si tengo que esperar al docente o hablar con él, en lugar de recibir un rechazo mudo**.

## 2. Contexto

`RN-10006` fija que una cuenta `Pendiente` o `Bloqueado` no obtiene acceso. El caso de uso es [`CU-10002`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10002-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md). `05` §10.3 declara qué hace esta pieza por `RN-10006`: mostrar el motivo de la situación de la cuenta al intentar ingresar, **sin sesión**.

## 3. Criterios de aceptación

- Given una cuenta en estado `Pendiente`, When la persona intenta ingresar con credenciales correctas, Then la superficie muestra el motivo explícito de que todavía no fue habilitada y **no se abre ninguna sesión**.
- Given una cuenta `Bloqueado`, When intenta ingresar, Then el motivo es distinguible del anterior.
- Given un correo o una contraseña equivocados, When intenta ingresar, Then el mensaje **no declara cuál de los dos campos falló**, y es indistinguible entre los dos casos.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-10002 |
| Restricciones transversales que la alcanzan | RT-03, RT-07 |
| Componente de `05` §3.1 | Traductor de condiciones a presentación, Superficies |
| Quién hace cumplir lo que esta historia sólo ofrece | La admisibilidad la resuelve el dominio y la traducción a respuesta de protocolo, `GeometriaFactory-Api` |
| BT derivadas | BT-10013 |
| Tests previstos en 08 | Comparación de las dos respuestas de credencial inválida, que deben ser indistinguibles |

## 5. Prioridad y estimación

`Must` por `RN-10006`, y porque el criterio de transición `d` → `e` exige que un alumno cuya cuenta está en estado `Pendiente` reciba **un aviso explícito** de que todavía no fue habilitada.

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

**Dos mensajes con propósitos opuestos conviven en la misma superficie**: el de situación de cuenta, que **sí** dice qué pasa, y el de credencial inválida, que **deliberadamente dice menos** de lo que el servicio sabe. Confundirlos convertiría la superficie de ingreso en un modo de averiguar qué correos existen.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
