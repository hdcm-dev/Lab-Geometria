# US-04 — Rechazar toda petición sin acceso, con acceso vencido o con firma que no corresponde

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-04-Rechazar-Toda-Peticion-Sin-Acceso-Vencido-O-Con-Firma-Ajena.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Punto de acceso:** Los **once** puntos bajo la guardia
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que ninguna petición a un punto que exige acceso se atienda sin un acceso válido**, para **que la única puerta del servidor propio no se pueda cruzar sin identidad**.

## 2. Contexto

`02` §3 declara la **admisión de la petición** como una de las cinco responsabilidades de esta capa, y `05` §3.1 la aloja en un componente **transversal a los once puntos que exigen acceso**. El contrato de uso es [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md).

## 3. Criterios de aceptación

- Given una petición a un punto bajo la guardia **sin** acceso, When se la recibe, Then se rechaza y **no se ejerce ningún caso de uso**.
- Given un acceso **vencido** o con **firma que no corresponde**, When se lo presenta, Then se rechaza igual, y los dos casos son distinguibles entre sí en el registro del servidor.
- Given ese rechazo, When se lo inspecciona, Then queda **registrado del lado del servidor** junto con todo intento de acceso rechazado.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-02 |
| RN que ejerce | RN-01 en el transporte del papel |
| Componente de `05` §3.1 | Guardia de admisión |
| ¿Decide qué se dice? | **No.** El mecanismo de verificación es de `GeometriaFactory-Infrastructure`; **exigirlo en cada punto es de acá** |
| Familia empobrecida | **No** |
| BT derivadas | BT-10, BT-11, BT-12 |
| Tests previstos en 08 | Batería de integración sobre los once puntos bajo la guardia |

## 5. Prioridad y estimación

`Must` porque **un puerto publicado hacia el enrutador es el único punto de entrada al servidor propio**, y todo lo que esta capa no proteja queda expuesto.

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

**Los cuatro puntos que no exigen acceso son ausencias declaradas y contables**: canje, registro de cuenta, configuración del administrador y salud. `05` §3.4 los enumera uno por uno **para que la ausencia sea contable**, y BT-12 verifica que sean exactamente cuatro.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
