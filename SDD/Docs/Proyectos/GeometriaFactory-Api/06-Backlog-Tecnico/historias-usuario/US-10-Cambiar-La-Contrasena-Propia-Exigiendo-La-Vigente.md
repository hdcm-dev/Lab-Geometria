# US-10 — Cambiar la contraseña propia exigiendo la vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-10-Cambiar-La-Contrasena-Propia-Exigiendo-La-Vigente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** `A-05`, bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código de `GeometriaFactory-Web`**, quiero **exponer el cambio de contraseña exigiendo la vigente**, para **que sólo el dueño de una cuenta pueda cambiar su propia clave**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el cambio de contraseña exigiendo la actual. El contrato de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md). `02` §8 declara por qué los cuatro puntos de alta y de credencial propia quedaron en un solo contrato de uso: **son los únicos que se ejercen sin acceso firmado o sin que el papel importe**.

## 3. Criterios de aceptación

- Given una sesión con acceso válido y la contraseña vigente correcta, When se pide el cambio, Then procede.
- Given una contraseña vigente equivocada, When se pide el cambio, Then se rechaza y **la contraseña no cambia**.
- Given el punto, When se inspecciona qué papel exige, Then **no exige ninguno en particular**: se ejerce sobre la propia cuenta, y el acceso identifica de quién es.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-03 |
| RN que ejerce | RN-13 en su excepción |
| Componente de `05` §3.1 | Superficie de acceso y credencial propia |
| ¿Decide qué se dice? | **No.** La verificación de la vigente es de `GeometriaFactory-Infrastructure` |
| Familia empobrecida | **No** |
| BT derivadas | BT-11, BT-16 |
| Tests previstos en 08 | Batería de integración con contraseña vigente correcta y equivocada |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que el cambio funcione y **persista entre reinicios**.

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

**Es el mismo punto de acceso que US-09**, y ésa es la consecuencia de `RN-16`: un solo camino para los tres cursos. Lo que las distingue es qué se presenta como contraseña vigente y de dónde viene la persona.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
