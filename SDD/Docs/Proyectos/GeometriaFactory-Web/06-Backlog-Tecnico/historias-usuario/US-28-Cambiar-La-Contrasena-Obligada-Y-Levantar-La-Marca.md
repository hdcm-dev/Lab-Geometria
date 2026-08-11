# US-28 — Cambiar la contraseña obligada tras un reseteo y levantar la marca

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** US-28-Cambiar-La-Contrasena-Obligada-Y-Levantar-La-Marca.md
**Versión:** 1.0
**Estado:** Aprobada
**Fecha:** 2026-08-10
**Autor:** Scrum Master (AG-06)
**Épica:** EP-04 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Superficie de 03:** `Credencial-Propia`, en su curso de cambio forzado
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **alumno al que le resetearon la contraseña**, quiero **cambiarla en el primer ingreso y recién ahí poder usar el laboratorio**, para **que la clave que el docente me comunicó deje de servir en cuanto elijo la mía**.

## 2. Contexto

`RN-13` declara que mientras la provisoria no se cambie la cuenta **se autentica y no obtiene sesión de trabajo**, y que al cambiarla la marca se levanta. El caso de uso es [`CU-03`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-03-Establecer-Y-Cambiar-La-Contrasena-Propia.md) FA-04. Es una de las **tres** historias que `02` §3.2 describió por contenido: «cambio forzado que levanta la marca».

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When la persona ingresa con la provisoria, Then llega al formulario de cambio **en el shell de acceso y sin barra lateral**, y no a ninguna otra ruta.
- Given ese formulario, When la persona cambia su contraseña, Then la marca **se levanta** y a partir de ahí opera con normalidad.
- Given el curso de cambio forzado, When se busca una salida del formulario, Then **no hay ninguna**: es lo único que la cuenta puede hacer.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-02 |
| CU cubiertos | CU-03 FA-04 |
| Restricciones transversales que la alcanzan | RT-02, RT-12 |
| Componente de `05` §3.1 | Superficies, Armazón y encaminamiento, Sesión y estado del circuito |
| Quién hace cumplir lo que esta historia sólo ofrece | `GeometriaFactory-Application` con su cuarta comprobación transversal, y `GeometriaFactory-Api` con su guardia sobre todos los puntos salvo uno |
| BT derivadas | BT-07, BT-13, BT-14 |
| Tests previstos en 08 | Paso del guion de la etapa `d`, sobre una cuenta reseteada |

## 5. Prioridad y estimación

`Must` por derivar de `F-26` y `F-04`, `Must Have`, y porque el criterio de transición `d` → `e` exige que la cuenta reseteada **se autentique y no obtenga sesión de trabajo**, y que recién al cambiarla opere con normalidad.

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

**Los tres cursos de `Credencial-Propia` son el mismo formulario de tres campos** desde `RN-16`; lo que distingue a éste es **de dónde se llega y que no hay salida**. `02` §3.1 declara que lo que sí es decisión propia y por eso lleva criterio de aceptación es **el confinamiento**, y que ése no vive en el formulario sino en el guardián: es US-29.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Numera y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 había repartido por necesidad de negocio con este identificador, y que su §3.2 dejó a la categoría 06 para redactar. |
