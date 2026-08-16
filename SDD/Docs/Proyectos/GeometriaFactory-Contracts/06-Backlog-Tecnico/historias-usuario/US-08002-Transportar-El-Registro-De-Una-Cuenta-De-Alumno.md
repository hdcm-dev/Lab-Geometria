# US-08002 — Transportar el registro de una cuenta de alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** US-08002-Transportar-El-Registro-De-Una-Cuenta-De-Alumno.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-08003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **extremo que compila contra el contrato (`GeometriaFactory-Api` y `GeometriaFactory-Web`)**, quiero **el tipo con el que viaja el registro de un alumno, con su correo, su nombre y su apellido y sin contraseña**, para **que el alumno se registre por su cuenta sin elegir contraseña y sin ningún canal de correo de por medio**.

## 2. Contexto

La capacidad `F-02` del intake §4 declara el registro con correo, nombre y apellido, **sin elegir contraseña**. El contrato de uso es [`CU-08002`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08002-Contrato-De-Administracion-De-Cuentas.md). `RN-08016` cierra además la escritura anónima de contraseña: la credencial la produce la habilitación.

## 3. Criterios de aceptación

- Given los tres datos del alumno, When se arma la solicitud de registro, Then el tipo los transporta y **no tiene campo de contraseña**.
- Given un registro cuyo correo ya está usado, When el servicio responde, Then el rechazo viaja como respuesta de error con su código propio del conjunto cerrado, y no como un campo del resultado del registro.
- Given la inspección de esta familia, When se busca cualquier tipo de establecimiento anónimo de contraseña, Then no existe ninguno, por `RN-08016`.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00001, NB-00002 |
| CU cubiertos | CU-08002 |
| Familia de tipos de `05` §3.1 | Familia de cuentas |
| Restricciones transversales de `02` §6 | RT-01 |
| RN que refiere por identificador | RN-08002, RN-08016 |
| BT derivadas | BT-08010 |
| Etapa del producto | `d`, según [`../../../../00-Contexto/Roadmap-Producto.md`](../../../../00-Contexto/Roadmap-Producto.md) §2.1 |
| Tests previstos en 08 | Prueba de integración del registro admitido y del rechazado por correo usado. |

## 5. Prioridad y estimación

`Must` por derivar de `F-02`, `Must Have` en `PRODUCT-INTAKE` §4.

**Estimación: sin fijar.** Ninguna fuente da base para puntos de historia ni para tallas, y el intake declara sin plazo calendario: el avance se mide por etapas cerradas. El fundamento completo está en [`../Product-Backlog.md`](../Product-Backlog.md) §4.1 y el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un contrato de uso de 02 en su tabla de trazabilidad
- [x] Declara la necesidad de negocio y la etapa del roadmap en la que se ejerce
- [x] Tiene criterios de aceptación en Given/When/Then, con al menos un camino feliz y un caso de borde
- [x] Declara la familia de tipos de `05` §3.1 que la sostiene
- [x] Ninguna regla de negocio se redacta acá: las refiere por identificador a `GeometriaFactory-Domain`
- [x] Se refinó contra la regla de exposición de `05` §3.2 y ningún campo que introduce puede transportar una dirección de servicio, una ruta de datos ni un secreto

## 7. Notas y supuestos

**El registro de cuenta sigue siendo anónimo por diseño** y así debe seguir: es como el alumno entra al laboratorio. Lo que `RN-08016` elimina es la escritura anónima **de contraseña**, y el intake lo precisó explícitamente el 2026-08-09 porque su redacción anterior decía «ninguna escritura anónima en el sistema», que era falso.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 previó con este mismo identificador y esta misma pertenencia a necesidades de negocio. |
