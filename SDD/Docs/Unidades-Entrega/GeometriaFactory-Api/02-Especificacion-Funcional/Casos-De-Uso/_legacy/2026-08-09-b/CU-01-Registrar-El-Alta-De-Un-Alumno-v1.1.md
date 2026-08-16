> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `CU-01-Registrar-El-Alta-De-Un-Alumno.md` en su versión **1.1**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-01-Registrar-El-Alta-De-Un-Alumno.md`](../../CU-02001-Registrar-El-Alta-De-Un-Alumno.md)
>
> El cuerpo que sigue **no se modifica**. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-01 — Registrar el alta de un alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-01-Registrar-El-Alta-De-Un-Alumno.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5; `00-Contexto/Vision-Producto.md` §9.1; `00-Contexto/Alcance-Producto.md` §4.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-02), §4.1 (RN-01 y RN-02), §17.1.P.2 (INV-01 e INV-05), §17.1.P.5, §6 (flujo 1)
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

Constituir un alumno con correo, nombre y apellido, con la cuenta en estado `Pendiente` y sin credencial derivada, de modo que exista el dueño al que después se le atribuyen trabajos. Es el contrato de uso que la capa de aplicación invoca cuando una persona se registra en el laboratorio.

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
- No se aporta credencial derivada: el registro no incluye contraseña (PRODUCT-INTAKE §4, F-02).
- La fecha de alta la aporta el consumidor, porque el dominio no lee el reloj.
- El papel con el que se constituye el alumno es `Alumno`.

## 4. Flujo principal

1. La capa de aplicación solicita la constitución de un alumno con correo, nombre, apellido y fecha de alta, declarando que verificó la unicidad del correo.
2. El dominio verifica que correo, nombre y apellido estén presentes y no vacíos.
3. El dominio verifica que la unicidad del correo venga declarada como comprobada.
4. El dominio verifica que no se aporte credencial derivada.
5. El dominio fija el papel en `Alumno`.
6. El dominio fija el estado de cuenta en `Pendiente`.
7. El dominio deja la credencial derivada sin valor.
8. El dominio deja el conjunto de trabajos vacío.
9. El dominio devuelve el alumno constituido, con sus invariantes ya verificados.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El consumidor solicita la constitución del alumno con papel `Administrador` | El dominio admite un único alumno con ese papel por instancia, y su alta sólo es posible mientras no exista ninguno (INV-05, RN-01). La comprobación de que no exista otro **no** se resuelve acá, porque el dominio no conoce el conjunto de cuentas: la ejerce la capa de aplicación y la declara al invocar | Paso 5 del flujo principal, con papel `Administrador` |
| FA-02 | El consumidor aporta los datos de alta con espacios alrededor | El dominio conserva los datos tal como los recibe: no normaliza el texto del correo ni del nombre. La normalización, si el producto la adopta, es decisión de 05 y afecta a cómo se compara la unicidad | Paso 2 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `DATO_OBLIGATORIO_AUSENTE` | Correo, nombre o apellido vacío o no provisto | Rechaza la constitución. No se produce ninguna instancia y no hay efecto parcial |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | El consumidor no declara haber comprobado que el correo esté libre | Rechaza la constitución: el dominio no admite constituir un alumno cuya unicidad de correo nadie verificó (INV-01) |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | Se aporta una credencial derivada en el registro | Rechaza la constitución: la credencial se fija recién en el primer ingreso efectivo (CU-03) |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Se solicita constituir el alumno con la cuenta en un estado distinto de `Pendiente` | Rechaza la constitución. El estado inicial es siempre `Pendiente` |

Los cuatro errores terminan de forma controlada: el dominio no construye la entidad y devuelve la causa al consumidor, que decide qué informar hacia afuera. Ninguno de los códigos es un código de protocolo: la traducción a respuesta pertenece a `GeometriaFactory-Api`.

## 7. Postcondiciones

- **Éxito:** existe un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor, ningún trabajo y la fecha de alta recibida.
- **Fallo:** no se constituye ninguna entidad. El dominio no deja estado intermedio, porque no guarda nada: la materialización es posterior y externa.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Los datos de registro correo `ana@example.com`, nombre `Ana`, apellido `Rossi` y fecha de alta 2026-08-09, con la unicidad del correo declarada como verificada | La capa de aplicación solicita constituir el alumno | El dominio devuelve un alumno con papel `Alumno`, cuenta `Pendiente`, credencial derivada sin valor y 0 trabajos |
| CA-02 | Los datos de registro con apellido vacío y correo `ana@example.com` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `DATO_OBLIGATORIO_AUSENTE` y no devuelve ninguna entidad |
| CA-03 | Los datos de registro completos, sin declarar la verificación de unicidad del correo | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `UNICIDAD_DE_CORREO_NO_VERIFICADA` |
| CA-04 | Los datos de registro completos más una credencial derivada de 64 caracteres | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` |
| CA-05 | Los datos de registro completos y una solicitud de constituirlo con la cuenta en estado `Habilitado` | La capa de aplicación solicita constituir el alumno | El dominio rechaza con el código `ESTADO_INICIAL_NO_NEGOCIABLE` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02, y NB-01 en su criterio de admisión explícita |
| Reglas de negocio aplicables | [RN-01](../../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02](../../../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) |
| Invariantes | INV-01, INV-05 |
| Historias de usuario a generar en 06 | US de constitución del alumno, US de verificación de los datos obligatorios, US de unicidad del correo |
| Componentes esperados en 05 | Entidad de alumno del modelo de dominio y su conjunto cerrado de estados de cuenta |
| Tests previstos en 08 | Pruebas unitarias puras sin dobles sobre la constitución y sobre los cuatro rechazos, dentro de la batería de dominio que debe completarse en menos de 10 segundos (PRODUCT-INTAKE §17.1.P.10) |

## 10. Notas y supuestos

- **La unicidad del correo es un invariante del sistema que el dominio no puede verificar solo** (INV-01): se afirma sobre el conjunto de alumnos y una entidad no conoce a ese conjunto. El dominio la exige declarada y `GeometriaFactory-Application` la ejerce con el puerto de repositorio. Se declara acá para que 05 la ubique en la capa que sí puede resolverla.
- Este caso de uso no envía ni prepara ninguna comunicación: el producto no tiene canal de correo (`Alcance-Producto.md` §5, exclusión X-1).
- La fecha de alta llega como dato porque el reloj es un puerto de `GeometriaFactory-Application` (PRODUCT-INTAKE §17.2.P.11 punto 3).

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Incorpora **INV-01 y RN-02**, la unicidad del correo, que la versión anterior declaraba como nota al pie por no tener enunciado disponible: pasa a precondición, a paso del flujo principal, al código de rechazo `UNICIDAD_DE_CORREO_NO_VERIFICADA` y al criterio CA-03. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Corrección de la ronda r1 del audit, hallazgo P3-04**: la sección opcional de compatibilidad se numera §17 y no §12, que es el número que `Rules-Especificacion-Funcional.md` §4.3 le asigna a la variante `library`. |

## 17. Compatibilidad de la superficie pública

La constitución del alumno es superficie pública de la biblioteca hacia `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, por referencia de proyecto de código. Agregar un dato obligatorio al alta es un cambio incompatible y rompe la compilación de los consumidores, que es la señal más temprana posible (PRODUCT-INTAKE §17.2.P.3). El versionado es SemVer 2.0.0 y no se publica en ningún feed (§17.1.P.7).
