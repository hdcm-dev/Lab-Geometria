# US-10 — Dar de baja exigiendo el correo escrito y declarando el arrastre antes del intento

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-10-Dar-De-Baja-Exigiendo-El-Correo-Escrito-Y-Declarando-El-Arrastre.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-04 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Panel-De-Cuentas`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **administrador**, quiero **que la baja de una cuenta me exija escribir su correo y me diga antes del intento qué se va a perder**, para **no borrar por accidente todos los trabajos de un alumno**.

## 2. Contexto

`RN-07` exige confirmación escrita y declara el arrastre de todos los trabajos. El caso de uso es [`CU-04`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-04-Administrar-Las-Cuentas-De-La-Comision.md) FA-02. `05` §10.3 declara qué hace esta pieza por `RN-07`: exigir el correo escrito **en la superficie** y **declarar antes del intento qué se va a arrastrar**.

## 3. Criterios de aceptación

- Given la operación de baja sobre una cuenta, When se la inicia, Then la superficie declara **antes del intento** que la baja elimina la cuenta **y todos sus trabajos**.
- Given esa confirmación, When el correo escrito no coincide con el de la cuenta, Then la baja **no procede**.
- Given la misma superficie, When el administrador busca una alternativa para un alumno que olvidó su contraseña, Then encuentra el **reseteo**, que conserva todo, y la superficie lo declara: es la fricción más cara que el producto tenía.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-01 |
| CU cubiertos | CU-04 FA-02 |
| Restricciones transversales que la alcanzan | RT-01, RT-03, RT-09 |
| Componente de `05` §3.1 | Superficies, Servicios de aplicación de front |
| Quién hace cumplir lo que esta historia sólo ofrece | La comparación del correo y el arrastre son de `GeometriaFactory-Application`, en una sola unidad de trabajo |
| BT derivadas | BT-08, BT-11, BT-13 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, con la baja confirmada y con la rechazada |

## 5. Prioridad y estimación

`Must` por derivar de `F-03`, `Must Have`, y porque el criterio de transición `d` → `e` exige que la baja **exija confirmación escribiendo el correo de la cuenta**.

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

**La baja y el reseteo viven en el mismo panel y hacen cosas opuestas**, y por eso la superficie declara lo que cada una cuesta antes del intento. US-30 es la otra mitad de esta distinción.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
