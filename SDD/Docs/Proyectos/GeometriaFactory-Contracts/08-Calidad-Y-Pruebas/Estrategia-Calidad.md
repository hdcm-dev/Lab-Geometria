# Estrategia de calidad — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Estrategia-Calidad.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.6 §3 y §6 (las **once** restricciones transversales); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3.1 y §3.2; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §5, §8, §9 y §11; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §17.4.P.6, §17.4.P.8, §17.4.P.10 y §22
**Trazabilidad downstream:** [`Estrategia-Testing.md`](Estrategia-Testing.md), [`Plan-Pruebas.md`](Plan-Pruebas.md), [`Criterios-Validacion.md`](Criterios-Validacion.md), [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops` y `11-Documentacion`

---

## Tabla de contenido

- [1. Definición de calidad para este proyecto de código](#1-definición-de-calidad-para-este-proyecto-de-código)
- [2. Atributos de calidad priorizados](#2-atributos-de-calidad-priorizados)
- [3. Quality gates](#3-quality-gates)
  - [3.1 Qué significa que un gate esté condicionado](#31-qué-significa-que-un-gate-esté-condicionado)
- [4. Roles de calidad dentro del equipo](#4-roles-de-calidad-dentro-del-equipo)
- [5. Cadencia de revisión](#5-cadencia-de-revisión)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Definición de calidad para este proyecto de código

`GeometriaFactory-Contracts` tiene calidad cuando **ningún dato que la regla de exposición prohíbe puede cruzar la frontera de servicio**, cuando los **ocho** contratos de uso transportan exactamente lo que declaran, y cuando todo fallo llega representado por uno de los **diecisiete** códigos vivos del conjunto cerrado.

La definición no habla de comportamiento, y no es un descuido: **este proyecto de código no tiene comportamiento**. Son tipos de transferencia planos ([`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §1). Su arquitectura es de exposición, y su calidad se mide con la misma vara: **qué se expone, qué no se expone y qué se puede diagnosticar**.

De ahí una consecuencia que hay que decir de entrada: **su pipeline no tiene etapa de pruebas propias** (`05` §5, coherente con el intake §17.4.P.6). Lo que reemplaza a la cobertura de líneas es la **inspección de la superficie pública** y la **batería de integración que golpea el servicio real**, y las dos son verificaciones tan mecánicas como un porcentaje.

## 2. Atributos de calidad priorizados

Clasificación ISO/IEC 25010. Los dos valores rotulados **[ASUNCIÓN]** vienen así desde el intake: el de `QG-05` desde §17.4.P.6, que la fila `A-4` de §22 respalda, y el de `QG-06` desde §17.4.P.10, que §22 no enumera. **Ninguno de los dos es compromiso de valor hasta que el Product Owner los confirme**; en `QG-05` lo que queda en duda es **la forma del gate y no su carácter bloqueante**, según la propia columna de `A-4` (ver §3.1).

| Atributo ISO 25010 | Prioridad | Métrica y origen |
| --- | --- | --- |
| Seguridad | **Crítica** | **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza de la implementación, en los tipos de las **ocho** familias (`05` §8; `RA-03` del intake §14) |
| Adecuación funcional | **Crítica** | 100 % de los **ocho** contratos de uso con al menos un caso de prueba por criterio de aceptación; **100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración [ASUNCIÓN del intake §17.4.P.6] |
| Fiabilidad | **Alta** | Conjunto cerrado de **17** códigos vivos sobre **20** identificadores emitidos, con **0** códigos producidos fuera del conjunto y **0** identificadores retirados reciclados (`05` §8; `03` §3.2) |
| Compatibilidad | **Alta** | Un cambio incompatible **rompe la compilación antes que el tiempo de ejecución**, porque los dos extremos compilan contra el mismo ensamblado (intake §17.4.P.3). La contracara operativa es el despliegue conjunto |
| Eficiencia de desempeño | **Media**, y sólo de carga útil | **0** ocurrencias del texto original, **0** de componentes de pieza y **0** del comentario del administrador en la proyección de listado [ASUNCIÓN derivada del intake §17.4.P.10]. Es el único atributo de rendimiento que este proyecto de código puede empeorar (`05` §8, cierre) |
| Mantenibilidad | **Alta** | **0** referencias hacia `GeometriaFactory-Domain` (quality gate bloqueante del intake §17.4.P.8) y **0** advertencias de construcción |
| Usabilidad | **No aplica como atributo de interfaz** | `tiene_ui_final` es false. Su equivalente es la experiencia del desarrollador, en [`../03-UX-UI-DX/DX-Developer-Experience.md`](../03-UX-UI-DX/DX-Developer-Experience.md) |
| Portabilidad | **Baja** | Plataforma única, cargada en los **dos** procesos desplegables del producto (intake §17.4.P.9) |

**Seguridad y adecuación funcional empatan en el primer lugar, y el orden entre ellas importa poco porque se verifican en el mismo lugar**: la superficie pública. Un campo de más es a la vez un defecto de exposición y un defecto de contrato.

## 3. Quality gates

| Id | Condición | Cómo se verifica | Consecuencia si no se cumple |
| --- | --- | --- | --- |
| QG-01 | El ensamblado compila **sin advertencias** | Etapa `build` del pipeline | **Bloquea la fusión** (intake §17.4.P.8) |
| QG-02 | **0** referencias hacia `GeometriaFactory-Domain` | Inspección del archivo de proyecto, y la comprobación reproducible que `03` §3 declara para `DXC-01` | Bloquea la fusión y **se rechaza en revisión**. Es la vía por la que el intake declara que el acoplamiento vuelve |
| QG-03 | **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza | Prueba de inspección de superficie pública, que es `CA-01` de `CU-06` (`TC-15`) | **Se rechaza aunque compile** (`DXC-05`) |
| QG-04 | El conjunto cerrado tiene exactamente **17** códigos vivos y se producen **0** códigos fuera de él | Prueba de inspección del conjunto, que es `CA-09` de `CU-06` (`TC-16`) | Se rechaza aunque compile (`DXC-03`) |
| QG-05 | **100 %** de los tipos de transferencia ejercitados por al menos una prueba de integración **[ASUNCIÓN del intake §17.4.P.6, sobre la forma del gate y no sobre su carácter]** | Matriz tipo contra prueba de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6, sobre `BT-16` | **Bloquea la fusión, y no es condicionado.** Lo sujeto a confirmación es **la forma**, ver §3.1 |
| QG-06 | La proyección de listado no lleva texto original, ni componentes de pieza, ni comentario **[ASUNCIÓN derivada del intake §17.4.P.10]** | Inspección de la superficie de la familia de listado (`TC-09`) | **Condicionado**, ver §3.1 |
| QG-07 | La respuesta de sesión declara exactamente **4** campos y **0** que transporten una condición que impida operar | Inspección de la superficie pública, restricción `RT-10` (`TC-01`, `TC-02`) | Se rechaza aunque compile (`DXC-14`) |
| QG-08 | Ante un cambio incompatible, **las dos unidades desplegables se despliegan juntas** | Revisión del pull request de la etapa, contra `RT-06` | Bloquea la publicación de la etapa. Su detección tardía es `DXC-08` |
| QG-09 | **Ningún tipo permite salir de un estado terminal** y ningún tipo habilita a que el navegador invoque el servicio de datos | Inspección de la superficie, restricciones `RT-08` y `RT-11` (`TC-18`, `TC-22`) | Se rechaza aunque compile (`DXC-10`) |

**La mitad de los gates de este proyecto de código son de la clase «se rechaza aunque compile».** No es una rareza: es lo que `03` §3.1 llama la clase que más caro se paga, y por eso están declarados como gates y no dejados a criterio de quien revisa.

### 3.1 Qué significa que un gate esté condicionado

**En este proyecto de código queda condicionado un solo gate: `QG-06`.** Su rótulo es **[ASUNCIÓN derivada del intake §17.4.P.10]**, y lo que la asunción pone en duda es **qué se verifica** —qué campos quedan fuera de la proyección de listado—, no la forma de expresar la puerta. [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) `BT-18` declara el tratamiento y esta estrategia lo adopta: **el valor se usa como vigente y la puerta no se declara bloqueante en `09-Devops` hasta que el Product Owner lo confirme sobre su propio documento**.

Condicionado no es opcional: la verificación se hace igual y el resultado se registra. Lo que queda en suspenso es la consecuencia automática.

**`QG-05` no es condicionado, y conviene decir por qué.** Su rótulo **[ASUNCIÓN]** viene de §17.4.P.6, y la fuente declara ahí mismo que **«el gate equivalente y bloqueante es que el 100 % de los DTOs esté ejercitado por al menos una prueba de integración»**. El intake §22 lo confirma en la fila `A-4`, cuya columna «Si el Product Owner la cambia» dice **«Cambia la forma del gate, no su carácter bloqueante»**. Lo que está en duda es **cómo se expresa** la puerta —cobertura de líneas, recuento de DTOs ejercitados, u otra forma—, no si detiene la fusión. Condicionarlo suspendería justamente el carácter que la fuente puso a salvo. **`09-Devops` lo materializa como gate bloqueante desde la primera etapa que lo alcanza**; si el Product Owner cambia la forma, cambia la condición medida y el gate sigue bloqueando.

## 4. Roles de calidad dentro del equipo

`equipo_n` es **1** (intake §2).

| Papel | Quién | Qué le corresponde |
| --- | --- | --- |
| AG-08, calidad y pruebas | La única persona del equipo, en este papel | Los casos de prueba, la matriz tipo contra prueba, la DoD y la declaración de validación |
| Product Owner | El docente de la cátedra, que es también quien ejecuta | El OK del punto de control de cada etapa y la confirmación de los dos valores rotulados [ASUNCIÓN] |
| Revisión mecánica | El pipeline y la revisión del pull request | `QG-01` y `QG-02` los da el pipeline; los siete restantes son de revisión de superficie, y por eso su enunciado es una condición contable y no un juicio |

**La particularidad de este proyecto de código es que la revisión pesa más que la ejecución.** Cinco de los nueve gates se comprueban leyendo la superficie pública y no corriendo nada, porque no hay nada que correr. Escribirlos como recuentos —cero campos, cuatro campos, diecisiete códigos— es lo que impide que dependan de que alguien se acuerde.

## 5. Cadencia de revisión

| Momento | Qué se revisa | Qué produce |
| --- | --- | --- |
| Al abrir la rama de cada etapa | Qué familias de tipos entran en alcance y qué `TC-XX` les corresponden | El alcance de testing de la etapa, en [`Plan-Pruebas.md`](Plan-Pruebas.md) §5 |
| **En cada pull request que agrega o cambia un campo** | Los cinco gates de superficie: `QG-03`, `QG-04`, `QG-07`, `QG-09` y la carga útil de `QG-06` | La constancia de la inspección en el pull request de la etapa |
| Al cerrar cada etapa | La matriz tipo contra prueba entera y el estado de cada `TC-XX` | Matriz actualizada |
| Ante todo cambio del conjunto cerrado de códigos | Si el cambio es incompatible, y si obliga al despliegue conjunto | La declaración de incompatibilidad en el `§17` del contrato de uso afectado |

**La cadencia por cambio de superficie es propia de este proyecto de código**, y existe porque su defecto característico entra de a un campo por vez y **compila** (`05` §9, segundo riesgo). Revisarlo sólo al cerrar la etapa lo detectaría tarde.

**No se declara ninguna frecuencia calendaria**: el intake declara «sin plazo calendario; el avance se mide por etapas cerradas».

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** `QG-05` estaba **condicionado**, y no correspondía: el intake §17.4.P.6 lo llama «el gate **equivalente y bloqueante**», y §22 declara en la fila `A-4` que un cambio del Product Owner «cambia la forma del gate, **no su carácter bloqueante**». Vuelve a **bloqueante**, con **la forma —y sólo la forma— sujeta a confirmación**. §3.1 se reescribe entera: el único condicionado de este proyecto de código es `QG-06`. **`H-08`.** §3.1 atribuía `QG-06` a la asunción `A-4` de §22, que no lo enumera; su respaldo real es §17.4.P.10, «[ASUNCIÓN derivada de RT §7.2]», que es lo que la propia celda del gate ya citaba bien. Ningún umbral cambia: lo que cambia es la consecuencia de no alcanzarlo. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la definición de calidad de un proyecto de código sin comportamiento, cuyos atributos críticos son la seguridad de exposición y la adecuación funcional del contrato; los ocho atributos ISO 25010 con su métrica de origen; los **nueve** quality gates, cinco de ellos de la clase «se rechaza aunque compile» y dos condicionados por depender de un valor rotulado [ASUNCIÓN] en el intake §22; el reparto de papeles con la constancia de que la revisión de superficie pesa más que la ejecución; y una cadencia que suma la revisión **por cambio de superficie**, con el fundamento de que el defecto característico de este proyecto de código entra de a un campo y compila. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **4**. Sube minor. |
