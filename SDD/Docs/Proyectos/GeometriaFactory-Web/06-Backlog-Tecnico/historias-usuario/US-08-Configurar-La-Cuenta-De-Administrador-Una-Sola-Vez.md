# US-08 — Configurar la cuenta de administrador una sola vez en la vida de la instancia

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-08-Configurar-La-Cuenta-De-Administrador-Una-Sola-Vez.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-03 Identidad del administrador y sesión
**Etapa del producto:** `c`
**Superficie de 03:** `Aprovisionamiento-Inicial`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **docente que arranca el laboratorio por primera vez**, quiero **configurar mi cuenta de administrador con mi correo y mi contraseña**, para **poder gobernar la comisión desde el primer día**, y como **producto**, dejar de ofrecer ese formulario para siempre.

## 2. Contexto

`RN-01` declara que existe **exactamente un** administrador y que su alta sólo es posible mientras no exista ninguno. `02` §3.1 declara que este acto **no recibió caso de uso propio**: se absorbió en [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) FA-03, porque un caso de uso propio para un formulario que se usa **una vez en la vida del laboratorio** habría sido un sub-flujo. La superficie sí es propia: `Aprovisionamiento-Inicial`.

## 3. Criterios de aceptación

- Given una instancia sin administrador, When el docente completa el aprovisionamiento, Then la cuenta queda configurada y habilitada, y puede ingresar.
- Given una instancia que **ya tiene** administrador, When se pide la ruta de aprovisionamiento, Then la aplicación **deja de armar el formulario** y desvía al ingreso.
- Given cualquiera de los dos casos, When se inspecciona el mensaje mostrado, Then **no revela nada** de la cuenta de administrador existente.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-04 FA-03 y FA-04 |
| Restricciones transversales que la alcanzan | RT-01, RT-03, RT-09 |
| Componente de `05` §3.1 | Superficies, Armazón y encaminamiento, con su guardián de aprovisionamiento resuelto |
| Quién hace cumplir lo que esta historia sólo ofrece | `INV-05` es del dominio y la ventana de alta la ejerce `GeometriaFactory-Application` |
| BT derivadas | BT-07, BT-08, BT-11 |
| Tests previstos en 08 | Paso del guion de la etapa `c`, con la ruta pedida por dirección directa después del aprovisionamiento |

## 5. Prioridad y estimación

`Must` por derivar de `F-01`, `Must Have`, y porque el criterio de transición `c` → `d` exige que el administrador se configure en el primer arranque y **sólo** mientras no exista ninguno.

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

**El guardián de aprovisionamiento resuelto es uno de los cuatro del armazón** (`05` §3.1). Que la superficie deje de armarse **acota lo que se ofrece**; quien impide un segundo administrador es la capa de aplicación con el dominio.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
