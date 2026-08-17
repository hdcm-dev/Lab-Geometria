# US-04032 — Levantar la marca con el cambio efectivo hecho por la propia cuenta, y sólo con él

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** US-04032-Levantar-La-Marca-Con-El-Cambio-Hecho-Por-La-Propia-Cuenta.md
**Versión:** 1.1
**Estado:** Aprobada
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-04003 Ciclo de vida de la cuenta de alumno
**Etapa del producto:** `d`
**Prioridad MoSCoW:** Must
**Estimación:** Sin fijar (ver [`../Product-Backlog.md`](../Product-Backlog.md) §4.1)

## 1. Historia

Como **código consumidor de la biblioteca**, quiero **que la marca de cambio de contraseña pendiente se levante únicamente cuando la propia cuenta reemplaza su credencial**, para **que la provisoria sea efectivamente provisoria y no una clave permanente que el administrador conoce**.

## 2. Contexto

`RN-04013` declara que al cambiarla la marca se levanta y la cuenta opera con normalidad, y que **la contraseña nueva la elige el alumno y el administrador no la conoce**. El contrato de uso es [`CU-00022`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00022-Ingresar-Al-Laboratorio-Y-Sostener-La-Sesion.md), en su reemplazo, que `02` §4 declara **el único lugar donde la marca se levanta**.

## 3. Criterios de aceptación

- Given una cuenta con la marca puesta, When ella misma reemplaza su credencial presentando la provisoria como vigente, Then la marca **queda levantada** y la cuenta opera con normalidad.
- Given esa misma cuenta, When cualquier otra operación del sistema se ejerce sobre ella —incluida una del administrador—, Then la marca **no se levanta**: sólo la levanta el cambio hecho por la propia cuenta.
- Given una cuenta sin la marca, When reemplaza su credencial, Then el reemplazo procede y **no hay marca que levantar**.

## 4. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| NB upstream | NB-00002 |
| CU cubiertos | CU-04003 |
| RN e invariantes que ejerce | RN-04013, RN-04016; INV-09 |
| Componente de `05` §3.1 | Orquestación del ingreso y la credencial, Guarda de autorización |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Comprobación de `02` §4 que la alcanza | **Cambio de contraseña pendiente**, en su **única excepción declarada** |
| BT derivadas | BT-04010, BT-04014 |
| Tests previstos en 08 | Prueba de que ninguna otra operación levanta la marca |

## 5. Prioridad y estimación

`Must` por `RN-04013` e `INV-09`, y porque el criterio de transición `d` → `e` exige que la cuenta con la contraseña reseteada **se autentique y no obtenga sesión de trabajo** hasta cambiar la contraseña, y que recién al cambiarla opere con normalidad.

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

**La marca tiene dos orígenes y una sola salida.** La ponen la **habilitación** de CU-04002 —por `RN-04016`— y el **reseteo** de CU-04011 —por `RN-04014`—, y la levanta únicamente este reemplazo. `02` §4 corrigió en su versión 1.6 la afirmación anterior de que la ponía sólo el reseteo, y esta historia refleja el estado vigente.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. Confirma y redacta la historia que [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §7.3 previó con este mismo identificador. |
