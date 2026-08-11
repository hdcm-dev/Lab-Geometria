# US-09 — Reemplazar la credencial derivada exigiendo la verificación de la vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** US-09-Reemplazar-La-Credencial-Derivada-Exigiendo-La-Vigente.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-02 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **orquestar el reemplazo de la credencial de una cuenta exigiendo que la vigente haya sido verificada**, para **que nadie cambie la contraseña de otro y para que el cambio lo haga siempre la propia persona**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el cambio de contraseña exigiendo la actual. El contrato de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Resolver-El-Ingreso-Y-La-Credencial-Del-Alumno.md), en su reemplazo. `02` §8 declara que esta operación **no se fusiona con el reseteo**: el sujeto es otro, la autorización es otra y la postcondición es opuesta.

## 3. Criterios de aceptación

- Given una cuenta cuya credencial vigente llega **ya verificada** y una credencial nueva **ya derivada**, When se pide el reemplazo, Then la credencial queda reemplazada.
- Given una solicitud de reemplazo sin la declaración de credencial vigente verificada, When se la procesa, Then se rechaza con su motivo y **la credencial no cambia**.
- Given cualquiera de los dos caminos, When se inspecciona lo que esta capa manipuló, Then **no aparece ninguna contraseña en claro**: llegan las dos ya derivadas.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-03 |
| RN e invariantes que ejerce | RN-01, RN-13 |
| Componente de `05` §3.1 | Orquestación del ingreso y la credencial |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Cambio de contraseña pendiente**, y es **la única excepción declarada**: éste es el camino que la marca deja pasar |
| BT derivadas | BT-10, BT-14 |
| Tests previstos en 08 | Prueba del rechazo sin credencial vigente verificada, con dobles |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que cambiar contraseña exigiendo la actual funcione y persista entre reinicios.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara el componente de `05` §3.1 y los puertos que consume
- [x] Declara qué comprobación de `02` §4 la alcanza
- [x] Las condiciones de rechazo que produce existen en el catálogo de las 36 de [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md)
- [x] Se puede verificar con dobles de los cuatro puertos, sin base de datos

## 7. Notas y supuestos

**Esta historia y US-32 son dos caras del mismo acto y se declaran aparte a propósito**: acá el reemplazo, allá el levantamiento de la marca. Separarlas hace visible que la marca **sólo** se levanta con el cambio efectivo hecho por la propia cuenta, que es lo que hace que la provisoria sea provisoria.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
