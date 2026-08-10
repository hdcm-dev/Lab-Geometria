# CU-01 — Registrar el alta de un alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-01-Registrar-El-Alta-De-Un-Alumno.md
**Versión:** 1.4
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-02, con origen en RF-03, y F-04), §4.1 (RN-02), §17.1.P.2 (INV-01), §17.1.P.5, §6 (flujo 1)
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

Constituir un alumno con correo, nombre y apellido, con la cuenta en estado `Pendiente` y sin credencial derivada, de modo que exista el dueño al que después se le atribuyen trabajos. Es el contrato de uso que la capa de aplicación invoca cuando una persona **se auto-registra** en el laboratorio.

**Este caso de uso cubre uno solo de los dos caminos de alta del producto**, el del alumno que se registra (F-02, con origen en RF-03). El otro —la configuración de la cuenta de administrador en el primer arranque, que nace `Habilitado` porque es la que habilita a las demás— es **CU-12**, y ninguna de las reglas de este documento se le aplica.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Invoca la constitución del alumno con los datos del registro, habiendo resuelto antes la unicidad del correo |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa fuera del dominio el alumno ya constituido |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Verifica los datos obligatorios y fija el estado inicial |

El alumno y el administrador **no** son actores de este caso de uso: son los sujetos de las reglas que el dominio hace cumplir. El actor de la superficie pública de esta biblioteca es siempre el código que la consume.

## 3. Precondiciones

- Correo, nombre y apellido están presentes y no vacíos.
- El consumidor ya comprobó que el correo no está en uso por otra cuenta, porque el correo es único en todo el sistema (INV-01, RN-02) y esa comprobación exige el conjunto de alumnos.
- No se aporta credencial derivada: **el auto-registro del alumno no incluye contraseña** (PRODUCT-INTAKE §4, F-02). La cuenta la recibe **en el acto de habilitación**, con la contraseña provisoria que el sistema produce, y el alumno elige la suya cambiándola (F-04 precisada, **RN-16**). La configuración del administrador sí incluye la contraseña en el alta, y por eso es otro caso de uso.
- La fecha de alta la aporta el consumidor, porque el dominio no lee el reloj.
- **El papel con el que se constituye la cuenta es `Alumno`.** Este caso de uso no constituye cuentas de administrador: ésas son CU-12.

## 4. Flujo principal

1. La capa de aplicación solicita la constitución de un alumno con correo, nombre, apellido y fecha de alta, declarando que verificó la unicidad del correo.
2. El dominio verifica que correo, nombre y apellido estén presentes y no vacíos.
3. El dominio verifica que la unicidad del correo venga declarada como comprobada.
4. El dominio verifica que no se aporte credencial derivada.
5. El dominio fija el papel en `Alumno`.
6. El dominio fija el estado de cuenta en `Pendiente`, que es el estado inicial **del auto-registro**: la cuenta queda a la espera de un acto explícito de habilitación del administrador (CU-02).
7. El dominio deja la credencial derivada sin valor.
8. El dominio deja el conjunto de trabajos vacío.
9. El dominio devuelve el alumno constituido, con sus invariantes ya verificados.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El consumidor solicita la constitución de una cuenta con papel `Administrador` | **Este caso de uso no la admite**: la configuración del administrador es un camino de alta distinto, con su propio estado inicial, su propia credencial y su propia ventana de alta, y vive en **CU-12**. El dominio rechaza la solicitud en lugar de constituir una cuenta de administrador con las reglas del auto-registro, que la dejarían `Pendiente` y sin salida | Termina con el rechazo de §6 |
| FA-02 | El consumidor aporta los datos de alta con espacios alrededor | El dominio conserva los datos tal como los recibe: no normaliza el texto del correo ni del nombre. La normalización, si el producto la adopta, es decisión de 05 y afecta a cómo se compara la unicidad | Paso 2 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | Correo, nombre o apellido vacío o no provisto | Rechaza la constitución. No se produce ninguna instancia y no hay efecto parcial |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | El consumidor no declara haber comprobado que el correo esté libre | Rechaza la constitución: el dominio no admite constituir un alumno cuya unicidad de correo nadie verificó (INV-01) |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | Se aporta una credencial derivada en el **auto-registro** | Rechaza la constitución: en este camino la credencial se fija recién en el acto de habilitación (CU-02, con la fijación de CU-03). En la configuración del administrador la credencial sí se aporta, y eso es CU-12 |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Se solicita constituir la cuenta del **auto-registro** en un estado distinto de `Pendiente` | Rechaza la constitución. El estado inicial de este camino es siempre `Pendiente`; el del otro camino de alta es siempre `Habilitado` y lo fija CU-12 |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | Se solicita constituir una cuenta con papel `Administrador` por el auto-registro | Rechaza la constitución y remite a CU-12, que es el camino que la fuente declara para F-01 |

Los cinco errores terminan de forma controlada: el dominio no construye la entidad y devuelve la causa al consumidor, que decide qué informar hacia afuera. Ninguno de los códigos es un código de protocolo: la traducción a respuesta pertenece a `GeometriaFactory-Api`.

## 7. Postcondiciones

- **Éxito:** existe un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor, ningún trabajo y la fecha de alta recibida. La cuenta **no admite acceso** hasta que un administrador la habilite (INV-06, CU-02), y eso es correcto en este camino porque ese administrador ya existe: lo constituyó CU-12 en el primer arranque.
- **Fallo:** no se constituye ninguna entidad. El dominio no deja estado intermedio, porque no guarda nada: la materialización es posterior y externa.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Los datos de registro correo `ana@example.com`, nombre `Ana`, apellido `Rossi` y fecha de alta 2026-08-09, con la unicidad del correo declarada como verificada | La capa de aplicación solicita constituir el alumno | El dominio devuelve un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor y 0 trabajos |
| CA-02 | Los datos de registro con apellido vacío y correo `ana@example.com` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `DATO_OBLIGATORIO_AUSENTE` y no devuelve ninguna entidad |
| CA-03 | Los datos de registro completos, sin declarar la verificación de unicidad del correo | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `UNICIDAD_DE_CORREO_NO_VERIFICADA` |
| CA-04 | Los datos de registro completos más una credencial derivada de 64 caracteres | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` |
| CA-05 | Los datos de un **auto-registro de alumno** y una solicitud de constituirlo con la cuenta en estado `Habilitado` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `ESTADO_INICIAL_NO_NEGOCIABLE`. El criterio es del auto-registro y no alcanza a la configuración del administrador, que por CU-12 CA-01 **sí** nace `Habilitado` |
| CA-06 | Los datos de registro completos con papel `Administrador` | La capa de aplicación solicita constituir la cuenta por este camino | El dominio rechaza con el código `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` y no constituye una cuenta de administrador `Pendiente` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02, y NB-01 en su criterio de admisión explícita |
| Reglas de negocio aplicables | [RN-02](../Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), y [RN-01](../Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) **sólo** en cuanto al conjunto cerrado de dos papeles: la unicidad del administrador y su ventana de alta se ejercen en CU-12, no acá |
| Invariantes | INV-01. **INV-05 no se cita como fundamento de este camino**: dice que existe exactamente un administrador y que su alta sólo es posible mientras no exista ninguno, y no dice nada sobre el estado inicial de una cuenta |
| Historias de usuario a generar en 06 | US de auto-registro del alumno, US de verificación de los datos obligatorios, US de unicidad del correo |
| Componentes esperados en 05 | Entidad de alumno del modelo de dominio, su conjunto cerrado de estados de cuenta y **los dos caminos de alta con su estado inicial propio** |
| Tests previstos en 08 | Pruebas unitarias puras sin dobles sobre la constitución y sobre los cinco rechazos, dentro de la batería de dominio que debe completarse en menos de 10 segundos (PRODUCT-INTAKE §17.1.P.10) |

## 10. Notas y supuestos

- **La unicidad del correo es un invariante del sistema que el dominio no puede verificar solo** (INV-01): se afirma sobre el conjunto de alumnos y una entidad no conoce a ese conjunto. El dominio la exige declarada y `GeometriaFactory-Application` la ejerce con el puerto de repositorio. Se declara acá para que 05 la ubique en la capa que sí puede resolverla.
- Este caso de uso no envía ni prepara ninguna comunicación: el producto no tiene canal de correo (`Alcance-Producto.md` §5, exclusión X-1).
- La fecha de alta llega como dato porque el reloj es un puerto de `GeometriaFactory-Application` (PRODUCT-INTAKE §17.2.P.11 punto 3).
- **El estado inicial `Pendiente` es de este camino y no del producto entero.** Las fuentes lo atan al acto de auto-registro: la capacidad que lo declara es F-02, «registro de alumno con correo, nombre y apellido, sin elegir contraseña», y el flujo 1 de PRODUCT-INTAKE §6 lo recorre —el alumno se registra, el sistema le avisa que su cuenta quedó pendiente de autorización, y el docente la habilita después—. La lectura de que ese estado inicial pertenece al auto-registro y no a toda alta de cuenta es de esta categoría: ninguna fuente accesible transcribe una tabla de transiciones de cuenta de la que copiarla. El otro camino, F-01, tiene su propio estado inicial y vive en CU-12.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Incorpora **INV-01 y RN-02**, la unicidad del correo, que la versión anterior declaraba como nota al pie por no tener enunciado disponible: pasa a precondición, a paso del flujo principal, al código de rechazo `UNICIDAD_DE_CORREO_NO_VERIFICADA` y al criterio CA-03. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |
| 1.2 | 2026-08-09 | **Corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`. Este caso de uso fijaba el estado inicial `Pendiente` para **toda** cuenta y resolvía la configuración del administrador como su flujo alternativo FA-01, que retornaba al paso 5 y atravesaba el paso 6: la cuenta del administrador nacía `Pendiente`, no obtenía acceso por INV-06 y no había ninguna otra cuenta que pudiera habilitarla, de modo que la instancia quedaba inutilizable en el primer arranque. Era una **generalización de alcance**: las fuentes atan el estado inicial `Pendiente` al auto-registro del alumno (RF-03, F-02), y la configuración del administrador es otro camino (F-01, con origen en RF-01 y RF-02), cuyo guion de la etapa `c` exige entrar inmediatamente después de configurar. El documento se **acota al auto-registro**: §1 lo declara, §3 y el paso 6 lo precisan, **FA-01 deja de constituir cuentas de administrador** y remite a **CU-12**, que se emite con este cambio; las causas de `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` y de `ESTADO_INICIAL_NO_NEGOCIABLE` se acotan a este camino; se suma el código `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` con su criterio CA-06; CA-05 declara que su criterio no alcanza al otro camino; y §9 **retira la cita de INV-05 y acota la de RN-01**, que sostenían algo que ninguna de las dos dice: hablan de la unicidad del administrador y de su ventana de alta, no del estado inicial. |
| 1.3 | 2026-08-09 | Correcciones de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`. **H-04**: §6 decía «los cuatro errores» sobre una tabla que pasó a tener cinco al sumarse `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`. **H-06**: §10 entrecomillaba «se registra (RF-03)» como si fuera transcripción de una fuente, y no es localizable; se reapoya el fundamento en la capacidad F-02 y en el flujo 1 de §6, que sí lo dicen, y se declara que la lectura es de esta categoría. |

## 17. Compatibilidad de la superficie pública

La constitución del alumno es superficie pública de la biblioteca hacia `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, por referencia de proyecto de código. Agregar un dato obligatorio al alta es un cambio incompatible y rompe la compilación de los consumidores, que es la señal más temprana posible (PRODUCT-INTAKE §17.2.P.3). El versionado es SemVer 2.0.0 y no se publica en ningún feed (§17.1.P.7).
