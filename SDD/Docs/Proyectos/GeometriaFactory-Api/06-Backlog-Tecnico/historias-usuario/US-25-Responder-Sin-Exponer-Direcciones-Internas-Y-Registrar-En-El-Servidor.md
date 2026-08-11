# US-25 — Responder sin exponer direcciones de servicios internos, y registrar del lado del servidor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-25-Responder-Sin-Exponer-Direcciones-Internas-Y-Registrar-En-El-Servidor.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Ninguno propio: es transversal a los quince
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que ninguna respuesta de esta superficie lleve la dirección de un servicio interno, la ruta del almacén, la clave de firma o una traza, y que todo error quede registrado del lado del servidor**, para **no exponer la topología y a la vez poder diagnosticar**.

## 2. Contexto

`RA-03` es regla de nivel producto, y `05` §10.4 declara que **acá es donde se puede violar hacia afuera**: es **la última vez que un dato del backend es tocado antes de salir del servidor propio**. Su contracara obligatoria es el registro: **sin él, la prohibición de exponer se convierte en imposibilidad de diagnosticar**, y el operador que despliega a mano se queda sin nada que mirar. El contrato de uso es [`CU-09`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md).

## 3. Criterios de aceptación

- Given las respuestas de fallo de los **quince** puntos, When se las inspecciona, Then **0** llevan dirección de servicio interno, ruta del almacén, clave de firma, contraseña, provisoria fuera del cuerpo del reseteo ni traza de implementación.
- Given cada uno de esos errores, When se mira el registro del servidor, Then quedó **registrado de forma estructurada**, junto con todo intento de acceso rechazado.
- Given el registro, When se lo inspecciona, Then **tampoco** contiene los secretos de la lista anterior.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-08 |
| CU cubiertos | CU-09 |
| RN que ejerce | — directamente; ejerce `RA-03`, que es regla de arquitectura del producto |
| Componente de `05` §3.1 | Traductor de motivos y códigos |
| ¿Decide qué se dice? | **Decide cómo se dice** |
| Familia empobrecida | **No**, pero comparte con ellas el criterio de decir menos de lo que el servicio sabe |
| BT derivadas | BT-13 |
| Tests previstos en 08 | **Prueba de inspección sobre las respuestas de fallo de los quince puntos y sobre el registro del servidor** |

## 5. Prioridad y estimación

`Must` porque `05` §10.4 declara que ésta es **la única de las siete Fases C del producto donde las tres reglas de arquitectura tienen tratamiento**, y `RA-03` es la que se viola hacia afuera desde acá.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap, o declara que su caso de uso no traza a ninguna y por qué
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el punto de acceso que la realiza, o declara que no realiza ninguno, y el componente de `05` §3.1
- [x] Declara si su punto está bajo la guardia, y si no lo está, cuál de las cuatro ausencias declaradas es
- [x] Toda condición que transporta es uno de los quince códigos vivos del contrato, con su destino declarado
- [x] Declara que no decide qué se dice
- [x] Declara si su respuesta pertenece a una de las tres familias deliberadamente empobrecidas

## 7. Notas y supuestos

**`GeometriaFactory-Infrastructure` sostiene la misma pareja desde su lado** —cinco cosas que no entran en un mensaje ni en una traza, más el texto del alumno— y **es de disciplina y no de ignorancia**, porque esa capa **conoce** los secretos. Acá pasa lo mismo, un escalón más afuera.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
