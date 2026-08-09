# CU-03 — Fijar y reemplazar la credencial derivada del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.7 §4 (F-04, F-05 y **F-26**), §4.1 (RN-06, **RN-12**, **RN-13**), §17.1.P.2 (INV-06, **INV-09**), §17.1.P.5, §6 (flujo 1), §7 (**CL-7** reescrito), §9 (X-1 vigente, **X-2 retirada**)
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Fijar por primera vez la credencial derivada de un alumno habilitado, y reemplazarla más adelante, sin que el dominio conozca nunca la contraseña en claro. El camino de fijación es el del **auto-registro**: la cuenta del administrador nace con su credencial ya fijada (CU-12) y por eso sólo usa el de reemplazo. Es el contrato de uso que sostiene la promesa de que **ninguna credencial se transporta**: el alumno la establece en su primer ingreso efectivo y el producto no tiene canal de correo por el que enviarla.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita fijar o reemplazar la credencial derivada del alumno |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Deriva la contraseña antes de que el valor llegue al dominio y materializa el resultado |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza la operación según el estado de la cuenta y la presencia previa de credencial |

El alumno es el sujeto de la regla. El dominio **no maneja secretos**: la contraseña llega ya derivada (PRODUCT-INTAKE §17.1.P.5).

## 3. Precondiciones

- El alumno existe y su estado de cuenta es `Habilitado`.
- El valor de credencial que se aporta ya está derivado; el dominio no recibe texto en claro.
- Para el reemplazo, el consumidor ya comprobó la credencial vigente contra la que el alumno presentó. Esa comparación exige el mecanismo de derivación y por eso vive en la infraestructura.

## 4. Flujo principal — fijación en el primer ingreso efectivo

1. La capa de aplicación solicita fijar la credencial derivada de un alumno.
2. El dominio comprueba que el estado de cuenta sea `Habilitado`.
3. El dominio comprueba que la credencial derivada no tenga todavía valor.
4. El dominio comprueba que el valor aportado no esté vacío.
5. El dominio fija la credencial derivada con el valor aportado.
6. El dominio devuelve el alumno, que a partir de acá admite acceso (CU-04).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El alumno cambia su contraseña estando ya dentro del laboratorio | El dominio admite el reemplazo cuando la credencial derivada ya tiene valor y el estado es `Habilitado`, y exige que el consumidor declare que verificó la credencial vigente. El valor anterior se reemplaza y no se conserva historial: ninguna fuente declara historial de credenciales | Paso 5, con reemplazo en lugar de fijación |
| FA-02 | El administrador resuelve el caso del alumno que olvidó su contraseña | **Este caso de uso no es el camino**: el administrador le fija una contraseña provisoria por [CU-13](CU-13-Resetear-La-Contrasena-De-Una-Cuenta-De-Alumno.md), que conserva la cuenta y todos sus trabajos. Lo que vuelve acá es el paso siguiente, el cambio obligatorio de FA-04. **La salida anterior —dar de baja y volver a dar de alta— dejó de ser la declarada**: `PRODUCT-INTAKE` 1.7 retira la exclusión X-2 y reescribe el caso límite CL-7 sobre el reseteo, porque aquella salida eliminaba todos los trabajos del alumno por RN-07 | Termina el caso de uso sin efecto; el camino sigue en CU-13 y vuelve por FA-04 |
| FA-03 | El administrador cambia su contraseña después de configurarse, como pide el guion de la etapa `c` | Entra por el reemplazo de FA-01 y no por la fijación: su credencial ya tiene valor desde CU-12, de modo que el camino de fijación por primera vez le devolvería `CREDENCIAL_YA_FIJADA` | Paso 5, con reemplazo |
| FA-04 | **El cambio obligatorio**: la cuenta tiene la marca de cambio de contraseña pendiente puesta y reemplaza su credencial provisoria | Es el mismo reemplazo de FA-01, con la misma exigencia de credencial vigente verificada —la vigente es la provisoria—, y con **un efecto adicional: el dominio levanta la marca**. Es el único acto que la levanta, y sólo lo ejerce la propia cuenta: la contraseña nueva la elige el alumno y el administrador no la conoce (RN-13, INV-09). El estado de cuenta y los trabajos no se tocan | Paso 5, con reemplazo y con la marca levantada |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | El estado de cuenta es `Pendiente` o `Bloqueado` | Rechaza la operación y conserva la credencial derivada como estaba |
| `CREDENCIAL_YA_FIJADA` | Se solicita fijar por primera vez una credencial que ya tiene valor | Rechaza la operación: el camino correcto es el reemplazo de FA-01 |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Se solicita reemplazar sin que el consumidor declare la verificación de la credencial vigente | Rechaza la operación |
| `VALOR_DERIVADO_VACIO` | El valor de credencial aportado está vacío | Rechaza la operación |

Los cuatro rechazos dejan al alumno exactamente como estaba.

## 7. Postcondiciones

- **Éxito de la fijación:** el alumno tiene credencial derivada con valor y sigue en estado `Habilitado`.
- **Éxito del reemplazo:** el alumno tiene la credencial derivada nueva y ningún otro atributo cambió. **Con una sola excepción declarada**: si la marca de cambio de contraseña pendiente estaba puesta, el reemplazo la levanta (FA-04). Los dos efectos son un solo acto y no hay camino por el que uno ocurra sin el otro.
- **Fallo:** la credencial derivada conserva su valor anterior, tenga o no valor.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Habilitado` y credencial derivada sin valor | La capa de aplicación solicita fijar la credencial con un valor derivado no vacío | El dominio devuelve el alumno con credencial derivada con valor y cuenta `Habilitado` |
| CA-02 | Un alumno con cuenta `Pendiente` y credencial derivada sin valor | La capa de aplicación solicita fijar la credencial | El dominio rechaza con el código `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` |
| CA-03 | Un alumno con cuenta `Habilitado` y credencial derivada ya fijada | La capa de aplicación solicita fijarla por primera vez | El dominio rechaza con el código `CREDENCIAL_YA_FIJADA` |
| CA-04 | Un alumno con cuenta `Habilitado` y credencial derivada ya fijada | La capa de aplicación solicita reemplazarla sin declarar la verificación de la vigente | El dominio rechaza con el código `CREDENCIAL_VIGENTE_NO_VERIFICADA` y 0 cambios de credencial se aplican |
| CA-05 | Un alumno reseteado: cuenta `Habilitado`, credencial provisoria y **marca de cambio de contraseña pendiente puesta** | La capa de aplicación solicita el reemplazo declarando verificada la provisoria | El dominio devuelve el alumno con la credencial nueva y **la marca levantada**, y la cuenta vuelve a ser admisible en CU-04 |
| CA-06 | El mismo alumno reseteado, con la marca puesta | La capa de aplicación solicita el reemplazo **sin** declarar la verificación de la vigente | El dominio rechaza con el código `CREDENCIAL_VIGENTE_NO_VERIFICADA`, la credencial no cambia y **la marca sigue puesta**: 0 caminos levantan la marca sin un cambio efectivo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 |
| Reglas de negocio aplicables | [RN-06](../Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), que es la que hace que una cuenta que no está `Habilitado` no llegue a tener credencial útil; y [RN-13](../Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), que es la que hace que el reemplazo de FA-04 levante la marca y que nadie más pueda levantarla. La máquina de estados de cuenta está en [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §5.1, y la de la marca en §5.3 |
| Invariantes | INV-06, por la condición de estado; **INV-09**, por el levantamiento de la marca en FA-04 |
| Historias de usuario a generar en 06 | US de establecimiento de contraseña en el primer ingreso, US de cambio de contraseña exigiendo la vigente, **US-27** de cambio obligatorio que levanta la marca |
| Componentes esperados en 05 | Atributo de credencial derivada de la entidad de alumno, con su condición de estado |
| Tests previstos en 08 | Pruebas unitarias de fijación, de reemplazo y de los cuatro rechazos, sin dobles y sin infraestructura; más el par del cambio obligatorio: reemplazo con la marca puesta que la levanta, y reemplazo rechazado que la deja puesta |

## 10. Notas y supuestos

- El dominio **no deriva** la contraseña ni la compara: la derivación de clave vive en `GeometriaFactory-Infrastructure` (PRODUCT-INTAKE §17.3.P.5). Este caso de uso modela la condición, no el mecanismo.
- **Sí hay recuperación de contraseña olvidada, y no es autónoma.** `PRODUCT-INTAKE` 1.7 §9 **retira la exclusión X-2**: el administrador resetea la contraseña por CU-13 y la cuenta cambia la provisoria por acá. Lo que sigue excluido es la recuperación **por correo**, que es lo que impide X-1: el producto no tiene canal de correo (`Alcance-Producto.md` §5). La redacción anterior de esta nota citaba las dos exclusiones juntas y quedó falsa en su primera mitad.
- Ninguna credencial de sesión es observable desde el navegador: eso lo sostiene la pieza pública del producto y no este proyecto de código (`NB-02` §5, quinto criterio).

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. §9 deja de declarar que ninguna regla lo restringe y pasa a citar **RN-06**, cuyo enunciado el intake anterior no transcribía. Se califican las ocurrencias de `Pendiente` y de los demás estados de cuenta según `Vision-Producto.md` §9.2. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |
| 1.2 | 2026-08-09 | Alcanzado por la **corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`. Con los dos caminos de alta separados, §1 declara que la fijación por primera vez es la del auto-registro y se suma **FA-03**, que ubica el cambio de contraseña del administrador de la etapa `c` en el reemplazo de FA-01: su credencial nace fijada por CU-12, de modo que el camino de fijación le devolvería `CREDENCIAL_YA_FIJADA`. |
| 1.3 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-07**. §5 listaba los flujos alternativos en orden FA-01, FA-03, FA-02, porque FA-03 se insertó en la versión anterior a continuación del flujo con el que se relaciona en lugar de al final de la tabla. Se restituye el orden correlativo sin cambiar ningún contenido ni ninguna numeración. |

| 1.4 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` **1.7**, que incorpora la capacidad **F-26**, las reglas **RN-12** y **RN-13** y el invariante **INV-09**. Se suma **FA-04, el cambio obligatorio**: el reemplazo hecho por una cuenta con la marca de cambio de contraseña pendiente puesta **levanta la marca**, y es el único acto que la levanta. §7 declara el efecto adicional como parte del mismo acto, sin camino parcial; §8 suma **CA-05** y **CA-06**, que verifican el levantamiento y que un rechazo deja la marca puesta; §9 refiere RN-13 e INV-09. **Dos afirmaciones de la versión anterior quedaron falsas y se corrigen**: FA-02 declaraba que la única salida ante una contraseña olvidada era dar de baja y volver a dar de alta, y §10 declaraba que no hay recuperación de contraseña olvidada citando X-1 y X-2 juntas. El intake **retira X-2** y reescribe **CL-7** sobre el reseteo; lo que sigue excluido es la recuperación autónoma por correo, que es X-1. |
## 17. Compatibilidad de la superficie pública

La operación recibe un valor ya derivado y no un texto en claro. Cambiar esa premisa haría que el dominio maneje secretos y contradiría §17.1.P.5 del intake: sería un cambio incompatible y de alcance, no una evolución menor.
