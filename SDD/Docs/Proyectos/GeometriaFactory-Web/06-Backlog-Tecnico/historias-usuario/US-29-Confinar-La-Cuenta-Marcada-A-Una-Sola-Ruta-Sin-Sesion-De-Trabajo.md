# US-29 — Confinar la cuenta con cambio pendiente a una sola ruta, sin sesión de trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-29-Confinar-La-Cuenta-Marcada-A-Una-Sola-Ruta-Sin-Sesion-De-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-04 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Credencial-Propia` y los dos shells
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **producto**, quiero **que una cuenta con cambio de contraseña pendiente no alcance ninguna ruta que no sea el cambio de su propia contraseña, y que llegue ahí sin sesión de trabajo**, para **que una clave que el administrador conoce no sirva para operar como el alumno**.

## 2. Contexto

`RT-12` de `02` §6 lo declara con todas las letras, y `05` §10.2 lo asigna al **cuarto guardián** del armazón. Los casos de uso son [`CU-02`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-02-Iniciar-Y-Cerrar-Sesion-Sin-Exponer-La-Credencial.md) FA-07 y [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md) FA-05. Es una de las **tres** historias que `02` §3.2 describió por contenido: «confinamiento de la cuenta reseteada».

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When intenta alcanzar cualquier ruta del panel, Then el **cuarto guardián** la desvía al cambio de su propia contraseña.
- Given ese desvío, When se inspecciona la sesión, Then **no hay sesión de trabajo**: el canje reconoce la provisoria y **no emite sesión**.
- Given esa misma cuenta, When alguien fuerza una solicitud sin pasar por la pantalla, Then **el servicio de datos la rechaza igual**: esta pieza **acota lo que se ofrece** y quien lo hace cumplir es el servicio de datos, que verifica la marca en cada solicitud.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-02 FA-07, CU-03 FA-05 |
| Restricciones transversales que la alcanzan | RT-02, RT-09, RT-12 |
| Componente de `05` §3.1 | Armazón y encaminamiento, con su **cuarto guardián**; Sesión y estado del circuito |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application`, con la cuarta comprobación que **corta antes que las otras tres**, y `GeometriaFactory-Api`, que garantiza que ningún punto quede fuera de la guardia |
| BT derivadas | BT-07, BT-14 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, con una ruta pedida por dirección directa desde una cuenta marcada |

## 5. Prioridad y estimación

`Must` por `RN-13` e `INV-09`, y porque el criterio de transición `d` → `e` exige que **cualquier ruta que la cuenta reseteada intente termine en el cambio de contraseña**.

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

**El cuarto guardián se completa en la etapa `d` y no en la `b`**, aunque el armazón se construya allá: hasta la `d` no existe la marca sobre la que decidir. BT-07 lo declara explícitamente.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
