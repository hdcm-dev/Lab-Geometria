# CU-12 — Configurar la cuenta de administrador en el primer arranque

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-12-Configurar-La-Cuenta-De-Administrador.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §1, §4, §5 y §7 (caso de uso previsto «configurar la cuenta de administrador en el primer arranque»); `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-01, con origen en RF-01 y RF-02), §4.1 (RN-01 y RN-02), §15 (etapa `c`: «configurar el administrador en el primer arranque, **entrar**, cambiar contraseña y salir, persistido»), §17.1.P.2 (INV-01 e INV-05), §17.1.P.5, §9 (X-3)
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

Constituir la **única** cuenta con papel `Administrador` de la instancia, en el primer arranque y sólo mientras no exista ninguna, con la cuenta ya `Habilitado` y con su credencial derivada ya fijada. Es el acto que crea la primera identidad del laboratorio, y por eso es el único camino de alta que no espera la habilitación de nadie: no hay ninguna cuenta anterior que pudiera darla.

Es el segundo de los **dos caminos de alta** del producto. El otro es el auto-registro del alumno, que es CU-01 y nace con la cuenta `Pendiente`.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Invoca la configuración, habiendo comprobado antes que no existe ninguna cuenta con papel `Administrador` y que el correo está libre |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Deriva la contraseña antes de que el valor llegue al dominio y materializa la cuenta constituida |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica los datos obligatorios, fija el papel y el estado, y adopta la credencial derivada |

El docente en su papel de administrador es el **sujeto** de la regla, no el actor del caso de uso.

## 3. Precondiciones

- Correo, nombre y apellido están presentes y no vacíos.
- El consumidor declara que **no existe ninguna cuenta con papel `Administrador`** en la instancia: el alta sólo es posible mientras no exista ninguna (RN-01, INV-05). El dominio no conoce el conjunto de cuentas y por eso exige la comprobación declarada.
- El consumidor declara que verificó que el correo no está en uso (RN-02, INV-01).
- Se aporta la credencial derivada, **ya derivada** y nunca en claro: a diferencia del auto-registro del alumno, la configuración del administrador incluye su contraseña, porque el guion de la etapa `c` exige entrar inmediatamente después de configurar.

## 4. Flujo principal

1. La capa de aplicación solicita configurar la cuenta de administrador con correo, nombre, apellido, credencial derivada y fecha de alta, declarando que no existe administrador y que el correo está libre.
2. El dominio verifica que correo, nombre y apellido estén presentes y no vacíos.
3. El dominio verifica que venga declarada la ausencia de administrador previo.
4. El dominio verifica que venga declarada la verificación de unicidad del correo.
5. El dominio verifica que la credencial derivada aportada no esté vacía.
6. El dominio fija el papel en `Administrador`.
7. El dominio fija el estado de cuenta en **`Habilitado`**, no en `Pendiente`: esta cuenta es la que habilita a las demás, y ninguna cuenta anterior podría habilitarla a ella.
8. El dominio adopta la credencial derivada aportada.
9. El dominio deja el conjunto de trabajos vacío y devuelve la cuenta constituida, que admite acceso desde ese mismo momento (CU-04).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El administrador cambia su contraseña inmediatamente después de entrar, como pide el guion de la etapa `c` | No es este caso de uso: el reemplazo de una credencial ya fijada es el flujo alternativo FA-01 de CU-03, que exige la credencial vigente verificada. Acá la credencial nace fijada, de modo que el camino de fijación por primera vez de CU-03 **no aplica** a esta cuenta | Termina el caso de uso; sigue CU-03 |
| FA-02 | Se solicita configurar una segunda cuenta de administrador | El dominio la rechaza. La comprobación de que no exista otra la ejerce la capa de aplicación sobre el conjunto de cuentas y la declara al invocar; el dominio rechaza toda invocación que no la traiga declarada o que la traiga en falso (RN-01, INV-05) | Termina con el rechazo de §6 |
| FA-03 | Se solicita constituir la cuenta de administrador con el estado `Pendiente` o `Bloqueado` | El dominio lo rechaza: dejaría a la instancia sin ninguna cuenta capaz de habilitar, y por INV-06 esa cuenta tampoco obtendría acceso. Es un estado terminal de hecho, sin salida | Termina con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | Correo, nombre o apellido vacío o no provisto | Rechaza la constitución. No se produce ninguna instancia y no hay efecto parcial |
| `ADMINISTRADOR_YA_CONFIGURADO` | El consumidor no declara la ausencia de administrador previo, o declara que ya existe uno | Rechaza la constitución: el alta del administrador sólo procede mientras no exista ninguno (RN-01) |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | El consumidor no declara haber comprobado que el correo esté libre | Rechaza la constitución (RN-02, INV-01) |
| `CONFIGURACION_SIN_CREDENCIAL` | No se aporta credencial derivada, o el valor aportado está vacío | Rechaza la constitución: una cuenta de administrador sin credencial no podría entrar, y no hay ninguna otra que pudiera resolverlo |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Se solicita constituirla en un estado distinto de `Habilitado` | Rechaza la constitución. En este camino el estado inicial es siempre `Habilitado`, por el mismo motivo por el que en el auto-registro es siempre `Pendiente`: cada camino tiene el suyo |

Los cinco rechazos terminan de forma controlada: el dominio no construye la entidad y devuelve la causa al consumidor. Ninguno es un código de protocolo: la traducción a respuesta pertenece a `GeometriaFactory-Api`.

## 7. Postcondiciones

- **Éxito:** existe la cuenta con papel `Administrador`, estado `Habilitado`, credencial derivada con valor, ningún trabajo y la fecha de alta recibida. La evaluación de admisibilidad de CU-04 la devuelve **admisible** sin ningún motivo pendiente, de modo que el guion de la etapa `c` —configurar, entrar, cambiar contraseña, salir— es recorrible de punta a punta.
- **Fallo:** no se constituye ninguna entidad y la instancia sigue sin administrador, de modo que este mismo caso de uso vuelve a estar disponible.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una instancia sin ninguna cuenta con papel `Administrador`, y los datos correo `docente@example.com`, nombre `Fernando`, apellido `Filipuzzi`, una credencial derivada de 64 caracteres y fecha de alta 2026-08-09 | La capa de aplicación solicita configurar la cuenta de administrador | El dominio devuelve una cuenta con papel `Administrador`, estado `Habilitado`, credencial derivada con valor y 0 trabajos |
| CA-02 | La cuenta de administrador recién configurada por CA-01 | La capa de aplicación consulta su admisibilidad por CU-04 | El dominio devuelve **admisible**, con 0 motivos: el administrador puede entrar inmediatamente después de configurarse, como exige el guion de la etapa `c` |
| CA-03 | Una instancia que ya tiene una cuenta con papel `Administrador` | La capa de aplicación solicita configurar otra | El dominio rechaza con el código `ADMINISTRADOR_YA_CONFIGURADO` y la instancia conserva 1 sola cuenta de administrador |
| CA-04 | Los datos de configuración completos, sin credencial derivada | La capa de aplicación solicita configurar la cuenta de administrador | El dominio rechaza con el código `CONFIGURACION_SIN_CREDENCIAL` |
| CA-05 | Los datos de configuración completos y una solicitud de constituirla con la cuenta `Pendiente` | La capa de aplicación solicita configurar la cuenta de administrador | El dominio rechaza con el código `ESTADO_INICIAL_NO_NEGOCIABLE`: una cuenta de administrador `Pendiente` dejaría a la instancia sin salida, porque nadie podría habilitarla |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, en su criterio de unicidad de la cuenta de administrador |
| Reglas de negocio aplicables | [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02](../Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) |
| Invariantes | INV-05, INV-01 e **INV-08**, que es el que expresa como condición permanente la propiedad que este caso de uso sostiene: la cuenta con papel `Administrador` está siempre `Habilitado`. Lo propuso esta categoría como candidato y `PRODUCT-INTAKE` §17.1.P.2 lo **adoptó**; el recorrido está en [`Definicion-Modelo-De-Dominio.md`](../Definicion-Modelo-De-Dominio.md) §4.2 |
| Historias de usuario a generar en 06 | US de configuración del administrador en el primer arranque, US de rechazo de la segunda configuración |
| Componentes esperados en 05 | Camino de alta propio en la entidad de alumno, distinto del auto-registro, con su estado inicial |
| Tests previstos en 08 | Pruebas unitarias puras de la configuración y de los cinco rechazos, más la prueba de recorrido de la etapa `c`: configurar y consultar admisibilidad en la misma batería, que es la que habría detectado el defecto |

## 10. Notas y supuestos

- **Por qué esta cuenta nace `Habilitado` y la del alumno no.** Las fuentes atan el estado inicial `Pendiente` al acto de auto-registro del alumno (RF-03, F-02), no a toda alta de cuenta. La configuración del administrador es un camino distinto y declarado por separado (F-01, con origen en RF-01 y RF-02), y el guion de la etapa `c` exige **entrar** inmediatamente después de configurar. Si esta cuenta naciera `Pendiente`, la única transición que la sacaría de ahí es que un administrador la habilite, y no hay ninguno: la instancia quedaría inutilizable en el primer arranque.
- **RN-01 e INV-05 no dicen nada sobre el estado inicial.** Dicen que existe exactamente un administrador y que su alta sólo es posible mientras no exista ninguno. Se citan acá por lo que sí declaran —la unicidad y la ventana de alta—, no como fundamento del estado.
- La unicidad de la cuenta de administrador y la del correo se afirman sobre el conjunto de cuentas, que el dominio no conoce: las ejerce `GeometriaFactory-Application` con el puerto de repositorio y las declara al invocar.
- El dominio **no maneja secretos**: la credencial llega ya derivada (PRODUCT-INTAKE §17.1.P.5).
- La cuenta de administrador **no admite ninguna de las cuatro operaciones de CU-02** —ni habilitación, ni bloqueo, ni rehabilitación, ni baja—, que las rechaza con `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` (RN-01, F-03). Por eso este caso de uso se ejerce una sola vez en la vida de la instancia y su resultado no se revierte.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. Nace de la corrección del **P0** que la auditoría de `GeometriaFactory-Application` detectó y que el informe `B-02-03-GeometriaFactory-Application-r1.md` reporta: la versión anterior de esta categoría no tenía caso de uso para la capacidad **F-01** y resolvía la configuración del administrador como un flujo alternativo de CU-01, que fija el estado inicial en `Pendiente` para toda cuenta. Con eso la cuenta del administrador nacía `Pendiente`, no podía obtener acceso por INV-06 y no había ninguna otra cuenta que pudiera habilitarla: la instancia quedaba inutilizable en el primer arranque. Este documento separa el segundo camino de alta, con su estado inicial `Habilitado`, su credencial fijada en el acto y su ventana de alta única. `NB-01` §7 ya lo preveía como caso de uso propio. |
| 1.1 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-02**. §9 remitía a §4.3 de `Definicion-Modelo-De-Dominio.md` a buscar el invariante candidato INV-08, que vive en **§4.2**: la remisión se escribió con la numeración previa a la inserción de esa subsección. Alcanzado además por **H-01**: §10 citaba el código retirado `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` y pasa a citar el que lo reemplaza, con las cuatro operaciones en lugar de sólo la baja. |
| 1.2 | 2026-08-09 | Corrección de una afirmación que quedó falsa aguas arriba, alcanzada al propagar `PRODUCT-INTAKE` **1.7**. §9 declaraba a **INV-08** como «invariante candidato… que no viene del intake», y §17.1.P.2 lo incorpora rotulado «**adoptado**». La fila de invariantes pasa a declararlo vigente, junto con INV-05 e INV-01. **Ningún flujo, código ni criterio de aceptación de este caso de uso cambia**, y el reseteo de contraseña de F-26 no lo toca: no procede sobre la cuenta de administrador (CU-13 §6). |
| 1.3 | 2026-08-09 | **Cierra la parte del hallazgo `F26-27`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0 que alcanza a este archivo: una **línea en blanco partía la tabla** de este control de cambios y dejaba fuera de ella las filas que la seguían. Se retira, **sin tocar el texto de ninguna fila**. **Ninguna sección de este contrato de uso se toca**, y ningún flujo, código de rechazo, postcondición ni criterio de aceptación cambia. Sube minor: repara el renderizado de una tabla. |
## 17. Compatibilidad de la superficie pública

Los dos caminos de alta son parte del contrato y no se fusionan: fusionarlos reintroduce el defecto que este documento corrige. Agregar un dato obligatorio a la configuración es un cambio incompatible para `GeometriaFactory-Application`, que la invoca por referencia de proyecto de código.
