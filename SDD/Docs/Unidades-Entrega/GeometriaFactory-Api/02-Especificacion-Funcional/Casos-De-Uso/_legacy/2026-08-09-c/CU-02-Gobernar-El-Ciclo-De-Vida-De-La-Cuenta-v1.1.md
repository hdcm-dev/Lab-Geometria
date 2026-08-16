> **Artefacto archivado — estado `Superado`**
>
> Copia archivada de `CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md` en su versión **1.1**, tomada el 2026-08-09 por el orquestador SDD **antes** de despachar la corrección, según `Master-Prompt.md` §8.
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.1
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md`](../../CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md)
>
> El cuerpo que sigue **no se modifica**. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# CU-02 — Gobernar el ciclo de vida de la cuenta del alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** CU-02-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §1, §4 y §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.1 y §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-03), §4.1 (RN-01 y RN-07), §17.1.P.2 (INV-05), §17.1.P.5, §7 (CL-6), §9 (X-3), §11 (RN-B6)
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

Sostener las cuatro operaciones que el administrador ejerce sobre una cuenta de alumno —habilitar, bloquear, rehabilitar y dar de baja— como transiciones verificables del dominio, admitiendo únicamente las que la máquina de estados declara. Las cuatro forman un solo contrato de uso porque son el mismo acto de admisión visto en cuatro momentos de la vida de la cuenta (`NB-01` §5, criterio de cobertura de las cuatro operaciones).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Capa de casos de uso del producto (`GeometriaFactory-Application`) | Primario | Solicita la transición de estado o la baja sobre un alumno ya constituido |
| Capa de infraestructura (`GeometriaFactory-Infrastructure`) | Secundario | Materializa fuera del dominio el resultado de la transición |
| Modelo de dominio de `GeometriaFactory-Domain` | Sistema | Admite o rechaza la transición según la máquina de estados |

El administrador es el **sujeto** de la regla, no el actor del caso de uso: quien invoca la superficie pública de esta biblioteca es el código consumidor.

## 3. Precondiciones

- El alumno existe y su estado de cuenta pertenece al conjunto `Pendiente`, `Habilitado`, `Bloqueado`.
- La operación solicitada pertenece al conjunto habilitar, bloquear, rehabilitar, dar de baja.
- Para la baja, el consumidor ya obtuvo del administrador la confirmación escrita del correo de la cuenta (RN-07). Esa comprobación es del consumidor: el dominio expresa la exigencia, no la interfaz que la recoge.

## 4. Flujo principal

1. La capa de aplicación solicita al alumno una transición de estado de cuenta.
2. El dominio lee el estado actual del alumno.
3. El dominio comprueba que el par estado actual y transición solicitada figure en la tabla de transiciones admitidas.
4. El dominio aplica la transición y deja el nuevo estado como estado actual.
5. El dominio devuelve el alumno con su nuevo estado.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La operación solicitada es la baja de la cuenta | El dominio exige que la baja arrastre los trabajos del alumno **en cualquier estado en que estén**, incluidos los terminales: la cuenta y sus trabajos desaparecen juntos, y no se admite una baja que deje trabajos huérfanos (RN-07). El dominio expresa esa condición como parte de la operación; la eliminación efectiva del dato la ejecuta la infraestructura | Termina el caso de uso: no hay estado posterior porque la cuenta deja de existir |
| FA-02 | Se solicita habilitar una cuenta que ya está `Habilitado` | El dominio trata la operación como sin efecto y devuelve el alumno sin cambio de estado, en lugar de rechazarla: la operación es idempotente respecto del estado | Paso 5 |
| FA-03 | Se solicita bloquear una cuenta `Pendiente` | Transición no declarada por las fuentes. El dominio la rechaza y no la infiere | Paso 3, con el rechazo de §6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del dominio |
| --- | --- | --- |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | El par estado actual y transición solicitada no figura en la tabla de transiciones | Rechaza la operación y conserva el estado actual sin modificar |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Se solicita la baja declarando que los trabajos del alumno se conservan | Rechaza la operación: la baja arrastra los trabajos, y esa consecuencia está aceptada por escrito aguas arriba |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | Se solicita dar de baja la cuenta con papel `Administrador` | Rechaza la operación: la instancia tiene un único administrador y quedaría sin él (INV-05, RN-01) |

Los tres rechazos son terminaciones controladas: el alumno queda exactamente como estaba antes de la solicitud.

## 7. Postcondiciones

- **Éxito de una transición:** el alumno tiene el nuevo estado y ningún otro atributo cambió.
- **Éxito de una baja:** la operación queda declarada como baja con arrastre de los trabajos del alumno, para que el consumidor la materialice como una sola unidad.
- **Fallo:** el estado de cuenta se conserva sin cambios y no hay efecto parcial.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita habilitarlo | El dominio devuelve el alumno con cuenta `Habilitado` |
| CA-02 | Un alumno con cuenta `Habilitado` | La capa de aplicación solicita bloquearlo y luego rehabilitarlo | El dominio devuelve el alumno con cuenta `Bloqueado` y después `Habilitado`, y admite las 4 operaciones declaradas |
| CA-03 | Un alumno con cuenta `Pendiente` | La capa de aplicación solicita bloquearlo | El dominio rechaza con el código `TRANSICION_DE_CUENTA_NO_ADMITIDA` y la cuenta sigue en `Pendiente` |
| CA-04 | Un alumno con cuenta `Bloqueado` y 3 trabajos, uno de ellos en estado `Finalizado` | La capa de aplicación solicita darlo de baja conservando los trabajos | El dominio rechaza con el código `BAJA_SIN_ARRASTRE_DE_TRABAJOS` |
| CA-05 | Un alumno con papel `Administrador` | La capa de aplicación solicita darlo de baja | El dominio rechaza con el código `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 |
| Reglas de negocio aplicables | [RN-01](../../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-07](../../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [RN-06](../../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) —este caso de uso es donde el estado de cuenta cambia, y de ese estado depende que la cuenta obtenga o no acceso— |
| Invariantes | INV-05, INV-06 |
| Historias de usuario a generar en 06 | US de habilitación, US de bloqueo y rehabilitación, US de baja con arrastre |
| Componentes esperados en 05 | Máquina de transiciones de estado de cuenta dentro de la entidad de alumno |
| Tests previstos en 08 | Pruebas unitarias de la tabla de transiciones, incluidas las inadmisibles, y del rechazo de la baja sin arrastre |

## 10. Notas y supuestos

- La confirmación escrita del correo antes de la baja es una exigencia de negocio que el dominio **declara** y que la interfaz del producto **recoge**; el detalle de esa interacción pertenece a la categoría 03 del proyecto de código de la pieza pública, no a esta.
- La baja es física y no un estado: por eso no aparece en la máquina de estados como destino, sino como salida del ciclo de vida.
- La pérdida de los trabajos de la cuenta dada de baja es un riesgo residual declarado y aceptado aguas arriba (`Vision-Producto.md` §8, RG-06). Alcanza también a los trabajos en estado `Finalizado` y `Rechazado`: la terminalidad de esos dos estados impide que cambien de estado o de contenido (INV-07), no que la baja de la cuenta los arrastre.
- La eliminación de **un** trabajo, sea por su dueño o por el administrador, no es esta operación: vive en CU-09.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe `PRODUCT-INTAKE` 1.3 y la resolución de la ambigüedad de los invariantes. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. Precisa que la baja arrastra los trabajos **en cualquier estado**, incluidos los dos terminales que el modelo de estados nuevo introduce, y distingue ese arrastre de la terminalidad de INV-07. Cita el enunciado de RN-01 y RN-07 de §4.1 y el de INV-05 de §17.1.P.2. Se califican las ocurrencias de `Pendiente` según `Vision-Producto.md` §9.2. **Correcciones de la ronda r1 del audit**: hallazgo **P3-01**, §9 suma **RN-06** e INV-06, que ya listaban a este caso de uso porque es acá donde el estado de cuenta cambia; hallazgo **P3-04**, la sección opcional se numera §17, como fija `Rules-Especificacion-Funcional.md` §4.3. |

## 17. Compatibilidad de la superficie pública

Agregar un estado de cuenta al conjunto cerrado, o una transición nueva, es un cambio de alcance de este caso de uso y sube la versión mayor del documento. Quitar una transición admitida es un cambio incompatible para `GeometriaFactory-Application`, que la invoca por referencia de proyecto de código.
