# US-06 — Cambiar la contraseña propia presentando la vigente

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-06-Cambiar-La-Contrasena-Propia-Presentando-La-Vigente.md
**Versión:** 1.0
**Estado:** Propuesta
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-03 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Credencial-Propia`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **persona con cuenta habilitada**, quiero **cambiar mi contraseña presentando la vigente**, para **elegir una clave que sólo yo conozca sin que nadie más pueda cambiármela**.

## 2. Contexto

`F-05` del intake §4 declara `Must Have` el cambio de contraseña exigiendo la actual. El caso de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md) y la superficie es `Credencial-Propia`, alojada en el shell de trabajo cuando el cambio es voluntario.

## 3. Criterios de aceptación

- Given una sesión abierta, When la persona presenta su contraseña vigente y escribe la nueva dos veces, Then el cambio procede y la sesión sigue siendo válida.
- Given una contraseña vigente equivocada, When confirma, Then la superficie muestra el rechazo y **la contraseña no cambia**.
- Given cualquiera de los dos caminos, When se inspecciona el navegador, Then **ninguna de las contraseñas escritas queda ahí**: la solicitud la arma el servidor de esta pieza.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-03 |
| Restricciones transversales que la alcanzan | RT-01, RT-02, RT-03 |
| Componente de `05` §3.1 | Superficies, Sesión y estado del circuito, Cliente tipado |
| Quién hace cumplir lo que esta historia sólo ofrece | La verificación de la contraseña vigente es de `GeometriaFactory-Infrastructure` |
| BT derivadas | BT-11, BT-13, BT-14 |
| Tests previstos en 08 | Paso del guion de la etapa `c`, con la comprobación de persistencia entre reinicios |

## 5. Prioridad y estimación

`Must` por derivar de `F-05`, `Must Have`, y porque el criterio de transición `c` → `d` exige que el cambio de contraseña **persista entre reinicios**.

**Estimación: sin fijar**, por [`../Product-Backlog.md`](../Product-Backlog.md) §4.1.

## 6. DoR check

- [x] Declara al menos un caso de uso de 02
- [x] Declara la necesidad de negocio y la etapa del roadmap
- [x] Criterios en Given/When/Then, con camino feliz y caso de borde
- [x] Declara la superficie de 03 que la aloja y el componente de `05` §3.1 que la sostiene
- [x] Declara qué restricciones transversales de `02` §6 la alcanzan
- [x] Toda condición que presenta es uno de los quince códigos vivos del contrato, o el camino de ausencia de respuesta
- [x] Ninguna afirmación depende de que la pieza pública haga cumplir una regla
- [x] Se puede maquetar y validar sin servicio de datos

## 7. Notas y supuestos

**Los tres cursos de esta superficie son el mismo formulario y el mismo contrato** desde `PRODUCT-INTAKE` 1.13 (`RN-16`): el cambio voluntario de esta historia, el primer ingreso y el cambio obligado tras un reseteo. Lo que los distingue es de dónde se llega y si hay salida, y eso es US-07 y US-28.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
