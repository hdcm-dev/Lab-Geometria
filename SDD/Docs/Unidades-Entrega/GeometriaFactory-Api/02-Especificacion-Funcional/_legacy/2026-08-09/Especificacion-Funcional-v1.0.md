> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Especificacion-Funcional.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`Especificacion-Funcional.md`](../../Especificacion-Funcional.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Especificación funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Especificacion-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §1, §3, §9; `00-Contexto/Alcance-Producto.md` §4.1, §5, §8; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2, §4 y §5.3, y las necesidades NB-01 a NB-06; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.1 íntegro, §13 y §14 (composición), §4, §6, §7, §12, §20 y §21
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `06-Backlog-Tecnico` de GeometriaFactory-Domain; `08-Calidad-Y-Pruebas`

---

## Tabla de contenido

- [1. Alcance funcional de este proyecto de código](#1-alcance-funcional-de-este-proyecto-de-código)
- [2. Documentos de esta categoría](#2-documentos-de-esta-categoría)
- [3. Catálogo de casos de uso](#3-catálogo-de-casos-de-uso)
- [4. Catálogo de reglas de negocio](#4-catálogo-de-reglas-de-negocio)
- [5. Matriz NB → CU → RN → US](#5-matriz-nb--cu--rn--us)
  - [5.1 Matriz](#51-matriz)
  - [5.2 Cobertura bidireccional](#52-cobertura-bidireccional)
  - [5.3 Historias de usuario previstas](#53-historias-de-usuario-previstas)
- [6. Criterio de recorte aplicado](#6-criterio-de-recorte-aplicado)
- [7. Omisiones declaradas](#7-omisiones-declaradas)
- [8. Numeración y su relación con la numeración de 01](#8-numeración-y-su-relación-con-la-numeración-de-01)
- [9. Puntos abiertos](#9-puntos-abiertos)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Alcance funcional de este proyecto de código

`GeometriaFactory-Domain` contiene las entidades e invariantes del dominio y es el centro de la regla de dependencias: no depende de nada y lo consumen `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure` por referencia de proyecto de código (PRODUCT-INTAKE §13 y §17.1.P.1).

Por eso esta especificación tiene una forma particular y deliberada, que es la de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los nueve casos de uso es el código que consume la biblioteca. El alumno y el administrador aparecen como **sujetos de las reglas** que el dominio hace cumplir, nunca como actores.

Lo que no está acá, y dónde está: la interpretación del texto del alumno, el cálculo de los valores derivados, la persistencia, la derivación de la contraseña y la emisión del acceso pertenecen a `GeometriaFactory-Infrastructure`; los datos que cruzan la frontera del proceso, a `GeometriaFactory-Contracts`; el dibujo, a `GeometriaFactory-Visor`. La tabla completa de fronteras está en [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) §7.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) | Documento de concepto central: las cinco entidades, sus invariantes y las dos máquinas de estado |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Nueve casos de uso, uno por archivo |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | Siete reglas de negocio, una por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

## 3. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-01 | [Registrar el alta de un alumno](../../Casos-De-Uso/CU-02001-Registrar-El-Alta-De-Un-Alumno.md) | Constituir un alumno en estado `Pendiente`, sin credencial derivada | Propuesto |
| CU-02 | [Gobernar el ciclo de vida de la cuenta del alumno](../../Casos-De-Uso/CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) | Habilitar, bloquear, rehabilitar y dar de baja | Propuesto |
| CU-03 | [Fijar y reemplazar la credencial derivada](../../Casos-De-Uso/CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) | Fijar la credencial en el primer ingreso efectivo y reemplazarla después | Propuesto |
| CU-04 | [Evaluar la admisibilidad de la cuenta](../../Casos-De-Uso/CU-02004-Evaluar-La-Admisibilidad-De-La-Cuenta.md) | Responder si la cuenta admite acceso y con qué motivo si no lo admite (INV-06) | Propuesto |
| CU-05 | [Crear y reeditar un trabajo](../../Casos-De-Uso/CU-02005-Crear-Y-Reeditar-Un-Trabajo.md) | Constituir el trabajo con dueño, identidad propia y texto original íntegro | Propuesto |
| CU-06 | [Reconstruir el conjunto de piezas del trabajo](../../Casos-De-Uso/CU-02006-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) | Incorporar piezas y componentes con identidad posicional y valores separados | Propuesto |
| CU-07 | [Registrar las observaciones del trabajo](../../Casos-De-Uso/CU-02007-Registrar-Las-Observaciones-Del-Trabajo.md) | Incorporar advertencias y errores de validación bien formados | Propuesto |
| CU-08 | [Gobernar el estado del trabajo](../../Casos-De-Uso/CU-02008-Gobernar-El-Estado-Del-Trabajo.md) | Enviar y finalizar, con la regla que separa guardar de entregar | Propuesto |
| CU-09 | [Resolver el acceso de un alumno a un trabajo](../../Casos-De-Uso/CU-02009-Resolver-El-Acceso-Del-Alumno-A-Un-Trabajo.md) | Pertenencia del trabajo y acotación de la eliminación al borrador | Propuesto |

Nueve casos de uso, sobre un mínimo de cinco para el tipo `library`.

## 4. Catálogo de reglas de negocio

| RN | Enunciado en una línea | CU afectados | Estado |
| --- | --- | --- | --- |
| RN-01 | [Administrador único y papeles fijos](../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | CU-01, CU-02, CU-04 | Propuesto |
| RN-03 | [Un trabajo ajeno es indistinguible de uno inexistente](../../Reglas-De-Negocio/RN-02003-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | CU-09 | Propuesto |
| RN-04 | [La eliminación de un trabajo está acotada al borrador](../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md) | CU-08, CU-09 | Propuesto |
| RN-05 | [Un trabajo no se finaliza con errores de validación](../../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | CU-07, CU-08 | Propuesto |
| RN-07 | [La baja arrastra los trabajos y exige confirmación escrita](../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | CU-02 | Propuesto |
| RN-08 | [El texto original del alumno se conserva íntegro](../../Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | CU-05, CU-06, CU-07 | Propuesto |
| RN-09 | [Toda observación de error indica la posición de la pieza y el campo](../../Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | CU-06, CU-07 | Propuesto |

Los invariantes INV-02, INV-04, INV-05 e INV-06 no llevan archivo propio: son propiedades permanentes del modelo y viven en [`Definicion-Modelo-De-Dominio.md`](Definicion-Modelo-De-Dominio.md) §4, que es el documento de concepto central. Cada regla de negocio declara el invariante que materializa.

## 5. Matriz NB → CU → RN → US

### 5.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| NB-01 · Control de admisión y de bajas del laboratorio | CU-01, CU-02, CU-04 | RN-01, RN-07 | US-01, US-02, US-03, US-04, US-07 |
| NB-02 · Identidad propia del alumno sin canal de correo | CU-01, CU-03, CU-04 | RN-01 | US-01, US-05, US-06, US-07 |
| NB-03 · Trabajo con dueño, estado y persistencia | CU-05, CU-08, CU-09 | RN-03, RN-04, RN-05, RN-08 | US-08, US-09, US-14, US-15, US-16, US-17 |
| NB-04 · Interpretación fiel del dato del alumno | CU-05, CU-06, CU-07, CU-08 | RN-05, RN-08, RN-09 | US-09, US-10, US-11, US-13, US-15 |
| NB-05 · Visibilidad del error de cálculo | CU-07, CU-08 | RN-05 | US-12, US-15 |
| NB-06 · Visualización del trabajo dentro del producto | CU-06 (parcial: identidad posicional) | RN-09 | US-10 |
| NB-07 · Revisión de la comisión desde un solo lugar | — | — | — |
| NB-08 · Alcance del laboratorio desde el aula | — | — | — |

### 5.2 Cobertura bidireccional

**De CU a NB.** Los nueve casos de uso trazan al menos a una necesidad de negocio; no hay ninguno huérfano.

| CU | NB que implementa |
| --- | --- |
| CU-01 | NB-02, NB-01 |
| CU-02 | NB-01 |
| CU-03 | NB-02 |
| CU-04 | NB-01, NB-02 |
| CU-05 | NB-03, NB-04 |
| CU-06 | NB-04, NB-06 |
| CU-07 | NB-05, NB-04 |
| CU-08 | NB-03, NB-04, NB-05 |
| CU-09 | NB-03 |

**De NB a CU.** Seis de las ocho necesidades reciben al menos un caso de uso en este proyecto de código. Las dos restantes **no las toca este proyecto de código**, y esto es una alerta explícita y no un silencio:

| NB sin CU acá | Por qué | Dónde se cubre |
| --- | --- | --- |
| NB-07 · Revisión de la comisión desde un solo lugar | Lo que la necesidad pide son listados agrupados y filtrados por alumno y una vista del trabajo. El dominio no ofrece consultas: las consultas que sí importan viven en los adaptadores, y la vista en la pieza pública | 02 de `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure`, `GeometriaFactory-Api` y `GeometriaFactory-Web` |
| NB-08 · Alcance del laboratorio desde el aula | Su dolor no es funcional sino de acceso: mediciones de viabilidad, despliegue y estado degradado. Este proyecto de código no atiende peticiones ni abre conexiones (PRODUCT-INTAKE §17.1.P.10) | 02 de `GeometriaFactory-Web` y `GeometriaFactory-Api`; 09-Devops |

NB-06 queda cubierta **parcialmente**: lo que este proyecto de código aporta es la identidad posicional de la pieza, que es lo que después permite seleccionarla y resaltarla y lo que sostiene una disposición determinista. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.

### 5.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas. Es el mismo mecanismo con el que `01-Necesidades-Negocio` previó las CU.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-01 | Constituir un alumno en estado `Pendiente` sin credencial | CU-01 |
| US-02 | Rechazar el alta con datos obligatorios ausentes | CU-01 |
| US-03 | Habilitar, bloquear y rehabilitar una cuenta | CU-02 |
| US-04 | Dar de baja una cuenta arrastrando sus trabajos | CU-02 |
| US-05 | Fijar la credencial derivada en el primer ingreso efectivo | CU-03 |
| US-06 | Reemplazar la credencial derivada exigiendo la vigente | CU-03 |
| US-07 | Evaluar la admisibilidad de la cuenta y devolver su motivo | CU-04 |
| US-08 | Constituir un trabajo con dueño, identidad propia y texto original | CU-05 |
| US-09 | Reeditar un borrador descartando la interpretación anterior | CU-05 |
| US-10 | Reconstruir el conjunto de piezas con identidad posicional | CU-06 |
| US-11 | Derivar la familia plana o volumétrica desde el tipo | CU-06 |
| US-12 | Registrar advertencias con el valor declarado y el derivado | CU-07 |
| US-13 | Registrar errores de validación con posición de pieza y campo | CU-07 |
| US-14 | Enviar un trabajo desde `Borrador` a `Pendiente` | CU-08 |
| US-15 | Finalizar con advertencias y rechazar la finalización con errores | CU-08 |
| US-16 | Resolver la pertenencia de un trabajo a su dueño | CU-09 |
| US-17 | Acotar la eliminación al estado `Borrador` | CU-09 |

## 6. Criterio de recorte aplicado

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de las necesidades de negocio que este proyecto de código toca. Quedaron nueve.
- **Fusiones.** Las cuatro operaciones del administrador sobre una cuenta —habilitar, bloquear, rehabilitar y dar de baja— quedaron en un solo caso de uso, CU-02, porque `NB-01` §5 las trata como un único conjunto de cobertura y son el mismo acto de admisión en cuatro momentos. La pertenencia y la acotación de la eliminación quedaron en CU-09 porque las dos responden la misma pregunta: si la operación procede.
- **Particiones.** La reconstrucción de las piezas (CU-06) se separó del registro de observaciones (CU-07) porque trazan a necesidades distintas con métricas distintas, que es la misma partición que `01-Necesidades-Negocio` §3.2 justifica entre NB-04 y NB-05, y porque tienen carácter opuesto: un error de interpretación impide entregar y una advertencia de valor no.
- **Lo que no se convirtió en caso de uso.** Todo lo que exige conocer el conjunto de entidades —unicidad del correo, listados, agrupaciones— no está acá: el dominio verifica lo que puede verificar sobre una entidad.

## 7. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos/Modelo-Conceptual.md` | **Omitido** | La regla de la categoría lo omite para `library`, y el flag `tiene_persistencia` de este proyecto de código es false. El intake declara «no aplica» en §17.1.P.4: el dominio no conoce el motor de persistencia. El vocabulario, la semántica y los elementos del concepto viven en `Definicion-Modelo-De-Dominio.md`, que es el documento de concepto central de este proyecto de código |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | **Omitido** | Dependen del modelo conceptual, que está omitido, y la regla las omite para `library`. Las restricciones de integridad del dominio están declaradas como invariantes en `Definicion-Modelo-De-Dominio.md` §4 y como reglas de negocio en `Reglas-De-Negocio/` |
| `Casos-De-Uso/_legacy/` y `Reglas-De-Negocio/_legacy/` | No existen | Emisión inicial: no hay ninguna versión superada que archivar |

## 8. Numeración y su relación con la numeración de 01

Dos aclaraciones que evitan una lectura equivocada de la trazabilidad:

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** `01-Necesidades-Negocio` §5.3 previó veintidós casos de uso a nivel producto; esta categoría se emite por proyecto de código, de modo que `CU-01` de `GeometriaFactory-Domain` no es el mismo artefacto que el `CU-01` que previó el catálogo de necesidades. La correspondencia entre unos y otros es la matriz de §5.1, que traza por necesidad de negocio y no por número.
2. **Los identificadores `RN-XX` conservan la numeración del intake**, para no romper la trazabilidad con las fuentes. Por eso la serie de esta carpeta es RN-01, RN-03, RN-04, RN-05, RN-07, RN-08 y RN-09: **falta RN-02 y falta RN-06**, y la causa está declarada en §9. Renumerarlas de forma contigua habría hecho que `RN-05` de esta carpeta dejara de ser la `RN-05` que citan el intake, los escenarios y las nueve pruebas obligatorias.

## 9. Puntos abiertos

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| Enunciado de **INV-01** | El intake nombra el rango «INV-01 a INV-06» pero no transcribe su enunciado. **No se inventa acá**. Está registrado en `Definicion-Modelo-De-Dominio.md` §4.2 | Product Owner, con la respuesta viviendo en PRODUCT-INTAKE §17.1 |
| Enunciado de **INV-03** | Aparece siempre junto a INV-02 bajo el rótulo común de «verificación de pertenencia», sin declarar qué lo distingue. **No se inventa acá** | Product Owner, con la respuesta viviendo en PRODUCT-INTAKE §17.1 |
| Enunciados de **RN-02** y **RN-06** | El intake declara que la fuente funcional tiene reglas RN-01 a RN-09, pero sólo transcribe siete. Esta categoría no da de alta las dos que faltan | Product Owner. Si sus enunciados alcanzan al dominio, esta categoría emite los archivos correspondientes sin renumerar los existentes |
| Nombres de tipos y de espacios de nombres | Declarados abiertos por el intake (§17.1.P.11) y validados en el punto de control de la etapa `a`. **No es ambigüedad de esta categoría**: acá los conceptos se nombran en lenguaje de dominio | 05-Arquitectura-Tecnica y la codificación de la etapa `a` |

Ninguno de los cuatro bloquea la emisión de esta categoría: los nueve casos de uso y las siete reglas se sostienen en enunciados declarados.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Índice maestro de nueve casos de uso y siete reglas de negocio, con la matriz NB → CU → RN → US, la verificación bidireccional de cobertura, las dos necesidades de negocio que este proyecto de código no toca con su justificación, el criterio de recorte con sus fusiones y particiones, las omisiones del modelo conceptual y de las reglas conceptuales con su motivo, la aclaración de las dos numeraciones y los cuatro puntos abiertos. |
