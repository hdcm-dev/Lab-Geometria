# US-26 — Conectar cada puerto con su adaptador y tomar de configuración lo que el despliegue provee

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** US-26-Conectar-Cada-Puerto-Con-Su-Adaptador-Y-Tomar-La-Configuracion.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + API Product Owner (AG-06)
**Épica:** EP-01 Esqueleto ambulante y verificación de viabilidad
**Etapa del producto:** `a`
**Punto de acceso:** Ninguno: la composición de raíz no atiende peticiones
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que los cuatro puertos queden conectados con sus cuatro adaptadores en un solo lugar y que toda la configuración del despliegue entre por ahí**, para **que la frontera sea contable y que un puerto sin adaptador falle en construcción y no en la primera petición**.

## 2. Contexto

`02` §3 declara la **composición de la aplicación** como una de las cinco responsabilidades. `05` §2.1 descartó repartirla en módulos por área con un fundamento propio: **el defecto característico de esta capa es de omisión**, y un puerto sin adaptador **se detecta comparando contra una lista, no leyendo un módulo**. El contrato de uso es [`CU-10`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md).

## 3. Criterios de aceptación

- Given la composición de raíz, When arranca el servicio, Then los **4 de 4** puertos resuelven a su adaptador, y hay **0** puertos sin adaptador o con más de uno.
- Given un puerto sin adaptador, When se intenta arrancar, Then **falla en construcción** y **no hay petición que responder**.
- Given la configuración del despliegue —ubicación del almacén, clave de firma, vigencia del acceso y límite de cuerpo—, When se la busca en el código, Then **entra sólo por acá** y **ningún componente la lee por su cuenta**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | **Ninguna.** `02` §7.2 declara que `CU-10` **no traza a ninguna necesidad**: conectar un puerto con su adaptador es **construcción, no capacidad**, y nadie la percibe |
| CU cubiertos | CU-10 |
| RN que ejerce | — |
| Componente de `05` §3.1 | Composición de raíz, **transversal** |
| ¿Decide qué se dice? | **No** |
| Familia empobrecida | **No** |
| BT derivadas | BT-02, BT-08, BT-10 |
| Tests previstos en 08 | Prueba de arranque que resuelve las cuatro dependencias y falla en construcción si falta alguna |

## 5. Prioridad y estimación

`Must` porque **todo lo demás es probable con dobles gracias a esta historia**, que es lo que las tres capas de adentro dan por sentado; y porque `05` §9 declara con probabilidad media el riesgo de que un puerto quede sin adaptador y **el fallo aparezca en la primera petición, en producción y sin nadie mirando**.

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

**El nombre del cuarto puerto se fija en el punto de control de la etapa `a`, y no acá.** Lo declara `GeometriaFactory-Application` y su ADR correspondiente lo ató a ese punto de control; esta capa **conecta exactamente cuatro** puertos con cuatro adaptadores y no nombra ninguno por su cuenta.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador y este mismo contenido. |
