# US-16 — No registrar la provisoria en ninguna traza

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-16-No-Registrar-La-Provisoria-En-Ninguna-Traza.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-07` y `A-09`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que la contraseña provisoria no aparezca en ningún registro del servidor ni en ninguna traza**, para **que una clave que sirve para entrar como el alumno no quede escrita en un archivo de diagnóstico**.

## 2. Contexto

`05` §7 declara que **ninguna respuesta lleva la provisoria fuera del cuerpo del reseteo**, y que el registro del servidor es la **contracara obligatoria** de `RA-03`: sin él, la prohibición de exponer se convierte en imposibilidad de diagnosticar; con él mal hecho, el secreto queda escrito. El contrato de uso es [`CU-05`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md).

## 3. Criterios de aceptación

- Given un reseteo exitoso, When se inspecciona el registro del servidor, Then la provisoria aparece exactamente **0** veces.
- Given una habilitación que produce provisoria, When se inspecciona el registro, Then ocurre lo mismo.
- Given cualquier respuesta de la superficie distinta del cuerpo del reseteo o del cambio de situación, When se la inspecciona, Then **la provisoria no está**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-05 |
| RN que ejerce | RN-14 en su parte de lo que **no** se hace con el valor |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión, Traductor de motivos y códigos |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-13, BT-17 |
| Tests previstos en 08 | Prueba de inspección sobre las respuestas de fallo de los quince puntos **y sobre el registro del servidor** |

## 5. Prioridad y estimación

`Must` porque `05` §7 declara `RA-03` como la regla que **se puede violar hacia afuera desde acá**: es la última vez que un dato del backend es tocado antes de salir del servidor propio.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza y el componente de `05` §3.1 que lo aloja
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**El registro estructurado de cada error y de cada intento de acceso rechazado sí es obligatorio** (`PRODUCT-INTAKE` §17.5.P.10): lo que esta historia acota es **qué no puede entrar en él**. Las dos cosas se sostienen juntas, y `GeometriaFactory-Infrastructure` declara la misma pareja desde su lado.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
