# US-04001 — Constituir una cuenta de alumno en estado `Pendiente` y sin credencial

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04001-Constituir-Una-Cuenta-De-Alumno-Pendiente-Y-Sin-Credencial.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar el auto-registro de un alumno con su correo, su nombre y su apellido, resolviendo antes la unicidad del correo sobre el conjunto de cuentas**, para **que el alumno entre al laboratorio por su cuenta y quede esperando la habilitación del administrador, sin ningún canal de correo de por medio**.

## 2. Contexto

`NB-00002` pide identidad propia del alumno sin canal de correo, y `F-02` del intake §4 la declara `Must Have`. El contrato de uso es [`CU-00021`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md). Lo que esta capa aporta y el dominio no puede aportar es **la verificación de la unicidad sobre el conjunto**, que llega por el puerto de repositorio de cuentas (`05` §10.3 `INV-01`).

## 3. Criterios de aceptación

- Given un correo que el puerto de repositorio de cuentas responde como no registrado y los datos de alta completos, When se orquesta el auto-registro, Then la cuenta queda constituida con papel `Alumno`, en estado `Pendiente` y **sin credencial derivada**.
- Given una solicitud de auto-registro que pretende el papel `Administrador`, When se la procesa, Then se rechaza: el segundo camino de alta es CU-04010 y no éste (`RN-04001`).
- Given una cuenta recién constituida por este camino, When se consulta su marca de cambio de contraseña pendiente, Then **no está puesta**: la marca la ponen la habilitación y el reseteo, nunca el alta.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002, NB-00001 |
| CU cubiertos | CU-04001 |
| RN e invariantes que ejerce | RN-04001, RN-04002, RN-04006; INV-01 |
| Componente de `05` §3.1 | Orquestación del alta de cuentas |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Ninguna**, y es la única situación en que corresponde: el solicitante es anónimo por diseño, porque es como el alumno entra al laboratorio (`PRODUCT-INTAKE` 1.15 §4.1, precisión de RN-04016) |
| BT derivadas | BT-04012, BT-04007, BT-04009 |
| Tests previstos en 08 | Prueba unitaria con doble del puerto de repositorio de cuentas, sin base de datos. El caso concreto lo fija 08 |

## 5. Prioridad y estimación

`Must` porque su capacidad de origen, `F-02`, está declarada `Must Have` en `PRODUCT-INTAKE` §4, y porque toda la etapa `d` cuelga de que exista una cuenta de alumno.

**Estimación: sin fijar**, por el fundamento de [`../Product-Backlog.md`](../Product-Backlog.md) §4.1; el punto abierto es `PA-01` de su §6.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza —ninguna— y por qué
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**La unicidad la resuelve esta capa y la sostiene el almacén como segunda línea.** La consulta previa no es una garantía por sí sola, y por eso `GeometriaFactory-Infrastructure` declara el índice único como camino declarado y no como accidente. El **criterio de comparación de dos correos** sigue abierto y no se decide acá: es `PA-07` de [`../Product-Backlog.md`](../Product-Backlog.md) §6, acompañado por BT-04021.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
