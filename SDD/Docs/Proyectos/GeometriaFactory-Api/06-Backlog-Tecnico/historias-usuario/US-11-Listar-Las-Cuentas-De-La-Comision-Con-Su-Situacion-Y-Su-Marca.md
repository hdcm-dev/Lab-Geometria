# US-11 — Listar las cuentas de la comisión con su situación y su marca

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-11-Listar-Las-Cuentas-De-La-Comision-Con-Su-Situacion-Y-Su-Marca.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-03 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Punto de acceso:** `A-06`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **obtener la lista de cuentas de la comisión con su situación y su marca de cambio de contraseña pendiente**, para **que el panel del administrador pueda mostrar en qué estado está cada alumno**.

## 2. Contexto

`NB-01` pide control de admisión y `F-03` del intake §4 lo declara `Must Have`. El contrato de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md). La **marca** viaja porque `GeometriaFactory-Infrastructure` la conserva y la hace viajar como atributo propio de la cuenta.

## 3. Criterios de aceptación

- Given un acceso con papel `Administrador`, When se pide el listado, Then vienen las cuentas con su **situación** y su **marca**.
- Given un acceso con papel `Alumno`, When se pide el listado, Then se rechaza en la guardia, **antes** de ejercer ningún caso de uso.
- Given el resultado, When se inspecciona qué campos trae, Then son los del tipo de transferencia del ensamblado de contratos: esta capa **no agrega ni recorta campos**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-04 |
| RN que ejerce | RN-01 |
| Componente de `05` §3.1 | Superficie de gobierno de la comisión |
| ¿Decide qué se dice? | **No.** Los tipos son del ensamblado de contratos |
| Familia empobrecida | **No** |
| BT derivadas | BT-11, BT-17 |
| Tests previstos en 08 | Batería de integración con acceso de papel insuficiente |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have`, y porque sin el listado el administrador no tiene desde dónde ejercer las otras cuatro operaciones del panel.

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

**Esta superficie no incorpora paginación**, y la decisión está registrada **con condición de reingreso declarada**: cuando la medición del percentil deje de cumplirse, entra paginación, y **es un cambio del ensamblado de contratos** que los dos extremos compilan juntos.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
