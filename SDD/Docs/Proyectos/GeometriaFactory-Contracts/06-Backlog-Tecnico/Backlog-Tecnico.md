# Backlog técnico — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Backlog-Tecnico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (las **ocho** familias de tipos), §3.2 (la regla de exposición), §5 (etapas del pipeline y puertas), §8 (los **siete** NFR), §9 (los **seis** riesgos), §10.2 (las **once** restricciones transversales) y §11 (los **cuatro** puntos abiertos); las **cinco** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) 1.0 §5.1 (los **diecisiete** códigos vivos); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15, §16 y §17.4.P.3 a P.11
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Contracts

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones del ensamblado](#21-ep-t01--fundaciones-del-ensamblado)
  - [2.2 EP-T02 · Familia de error y regla de exposición](#22-ep-t02--familia-de-error-y-regla-de-exposición)
  - [2.3 EP-T03 · Familias de sesión, cuentas y reseteo](#23-ep-t03--familias-de-sesión-cuentas-y-reseteo)
  - [2.4 EP-T04 · Familias del trabajo](#24-ep-t04--familias-del-trabajo)
  - [2.5 EP-T05 · Verificación por integración y despliegue conjunto](#25-ep-t05--verificación-por-integración-y-despliegue-conjunto)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **dieciocho** tareas técnicas viven **inline** en este documento, porque el proyecto de código está por debajo del umbral de treinta que fija la regla de la categoría.

**Este backlog técnico tiene una particularidad que conviene decir antes que nada**: el proyecto de código **no tiene comportamiento** y su pipeline **no tiene etapa de pruebas propias** (`05` §5). En consecuencia, buena parte de sus tareas no construyen lógica sino que **declaran forma y cierran puertas de inspección**. Una tarea cuyo criterio de aceptación sea «hay exactamente cero campos de tal clase» no es una tarea vacía acá: es el único mecanismo con el que este proyecto de código sostiene lo que decide.

**Ninguna tarea inventa alcance.** Cada una nace de una familia de tipos de `05` §3.1, de una ADR, de un NFR de su §8, de una restricción transversal de `02` §6 o de un punto abierto de `05` §11. Las cuatro que cierran un punto abierto son BT-08004, BT-08005, BT-08017 y BT-08018.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones del ensamblado

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el ensamblado exista, compile sin dependencias y **sin ninguna referencia hacia `GeometriaFactory-Domain`**, y que sus dos decisiones abiertas de forma queden encaminadas |
| Alcance | Estructura del proyecto, las dos puertas de construcción, los nombres por familia y el campo de momento |
| Fuente upstream | `PRODUCT-INTAKE` §16, §17.4.P.1 y P.8; [`ADR-08001`](../05-Arquitectura-Tecnica/Adrs/ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md); `05` §5, §8 y §11 |
| Etapa | `a`, y los nombres por familia en la etapa que implementa cada una |
| BT contenidas | BT-08001, BT-08002, BT-08003, BT-08004, BT-08005 |

### 2.2 EP-T02 · Familia de error y regla de exposición

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que exista **un solo** tipo con el que un fallo cruza la frontera, con conjunto cerrado, y que la regla de exposición tenga mecanismo y no sólo declaración |
| Alcance | Tipo de error único, los diecisiete códigos vivos, la regla de no reciclado y la prueba de inspección de superficie pública |
| Fuente upstream | [`ADR-08002`](../05-Arquitectura-Tecnica/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md), [`ADR-08004`](../05-Arquitectura-Tecnica/Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md); `05` §3.2 y §8; [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §5.1 |
| Etapa | `c`, y la inspección se repite en cada etapa posterior |
| BT contenidas | BT-08006, BT-08007, BT-08008, BT-08009 |

### 2.3 EP-T03 · Familias de sesión, cuentas y reseteo

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las tres familias de la identidad tengan su forma declarada, con la respuesta de sesión acotada y la solicitud de reseteo sin campo de contraseña |
| Alcance | Familia de sesión, familia de cuentas y familia de reseteo, con la única arista adicional del grafo |
| Fuente upstream | `05` §3.1 (familias de sesión, cuentas y reseteo); [`ADR-08004`](../05-Arquitectura-Tecnica/Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md); `02` §6 `RT-01` y `RT-10` |
| Etapa | `c` y `d` |
| BT contenidas | BT-08009, BT-08010, BT-08011 |

### 2.4 EP-T04 · Familias del trabajo

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el trabajo, su listado, su detalle y su desenlace tengan cada uno su familia, y que el reparto entre listado y detalle no se pueda invertir aguas abajo |
| Alcance | Familia de trabajo, familia de listado, familia de detalle y familia de desenlace |
| Fuente upstream | `05` §3.1 (familias de trabajo, listado, detalle y desenlace), §6; [`ADR-08005`](../05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md); `02` §6 `RT-03`, `RT-04`, `RT-08` y `RT-09` |
| Etapa | `e`, `f`, `g` y `h` |
| BT contenidas | BT-08012, BT-08013, BT-08014, BT-08015 |

### 2.5 EP-T05 · Verificación por integración y despliegue conjunto

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la ausencia de pruebas propias no sea una ausencia de verificación, y que la contrapartida del versionado por compilación compartida quede escrita para 09 |
| Alcance | Matriz tipo contra prueba de integración, adopción del formato de intercambio, regla operativa de despliegue conjunto y confirmación de los valores rotulados como asunción |
| Fuente upstream | `05` §5 (sin etapa de pruebas), §8, §11 PA-03 y PA-04; [`ADR-08003`](../05-Arquitectura-Tecnica/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md); `02` §6 `RT-06` y `RT-07` |
| Etapa | `c` a `h`, de forma acumulativa |
| BT contenidas | BT-08016, BT-08017, BT-08018 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-08001 | Crear el ensamblado de tipos, sin dependencias | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.4.P.1; [`ADR-08001`](../05-Arquitectura-Tecnica/Adrs/ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) | Ninguna | El ensamblado compila dentro del artefacto de agrupación y **no declara ninguna biblioteca de serialización**; las etapas del pipeline son `restore` → `build`, sin etapa de pruebas, según `05` §5 | **Infraestructura compartida**: la sostiene [`ADR-08001`](../05-Arquitectura-Tecnica/Adrs/ADR-08001-Tipos-De-Transferencia-Planos-Sin-Dependencias.md). Habilita a las 22 |
| BT-08002 | Puerta bloqueante de cero referencias hacia `GeometriaFactory-Domain` | devops | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.4.P.8; `05` §8 y §9, primer riesgo | BT-08001 | La inspección del archivo de proyecto arroja exactamente **0** referencias hacia el dominio; una referencia de ese tipo **se rechaza en revisión**; la puerta se mide en cada etapa | **Infraestructura compartida**: es la vía por la que el intake declara que el acoplamiento vuelve |
| BT-08003 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de advertencias de construcción; `PRODUCT-INTAKE` §17.4.P.8 | BT-08001 | La etapa de construcción termina sin advertencias; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-08004 | Fijar los nombres de los tipos, de sus campos y de los espacios de nombres, por familia | indagación | EP-T01 | `c` en adelante | Alta | Sin fijar | `05` §11 PA-01 | BT-08001 | Cada familia entra a su etapa con sus nombres decididos y registrados; la decisión se toma **en el punto de control de la etapa que implementa la familia** y no antes. **Caja temporal: la etapa de cada familia** | **Infraestructura compartida**: todas las historias dependen de que los nombres de su familia estén fijados |
| BT-08005 | Fijar la zona horaria y la precisión del campo de momento del tipo de error | indagación | EP-T01 | `a` o `c` | Media | Sin fijar | `05` §7, fila de momento; `05` §11 PA-02 | BT-08001, BT-08006 | La zona horaria y la precisión quedan decididas y registradas, junto con la elección del formato de intercambio; **ninguna fuente las declara**, de modo que la decisión se origina acá y se documenta como tal. **Caja temporal: antes de cerrar la etapa `c`** | US-08014, US-08016 |
| BT-08006 | Construir el tipo de error único con sus cuatro campos | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.1, familia de error; `05` §7; [`ADR-08002`](../05-Arquitectura-Tecnica/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md) | BT-08001 | El tipo tiene exactamente cuatro campos —código, texto neutro, colección de detalles de ubicación y momento—; **las siete familias restantes dependen de él y él de ninguna** (`05` §3.1) | US-08014, US-08015, US-08016 |
| BT-08007 | Declarar el conjunto cerrado de diecisiete códigos vivos, con la regla de no reciclado | feature | EP-T02 | `c` | Alta | Sin fijar | [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §5.1; [`ADR-08002`](../05-Arquitectura-Tecnica/Adrs/ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md); `05` §8 | BT-08006 | Los códigos vivos son exactamente **diecisiete**, sobre **veinte** identificadores emitidos; los **tres** retirados **no se reciclan**; **0** códigos se producen fuera del conjunto | US-08014, US-08015, US-08016 |
| BT-08008 | Prueba de inspección de superficie pública para los campos prohibidos | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.2 (regla de exposición); [`ADR-08004`](../05-Arquitectura-Tecnica/Adrs/ADR-08004-Regla-De-Exposicion-De-La-Frontera.md); `05` §8 y §9, segundo riesgo | BT-08001, BT-08006 | Hay exactamente **0** campos capaces de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos o una traza de la implementación, en los tipos de las **ocho** familias; la inspección se repite en cada etapa que agregue un campo | US-08001, US-08002, US-08003, US-08004, US-08005, US-08007, US-08014, US-08016, US-08021, US-08022 |
| BT-08009 | Construir la familia de sesión con su respuesta de cuatro campos | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, familia de sesión; `05` §8, fila de campos de la respuesta de sesión; `02` §6 `RT-10` | BT-08006, BT-08008 | La respuesta de sesión tiene exactamente **4** campos y **0** que transporten una condición que impida operar: ésas viajan como respuesta de error con código propio | US-08001 |
| BT-08010 | Construir la familia de cuentas | feature | EP-T03 | `d` | Alta | Sin fijar | `05` §3.1, familia de cuentas | BT-08006, BT-08008 | Transporta el registro, el listado de cuentas, el cambio de situación, la confirmación escrita de la baja y el cambio de contraseña; **no hay ningún tipo de establecimiento anónimo de contraseña**, por `RN-08016` | US-08002, US-08003, US-08004, US-08005, US-08022 |
| BT-08011 | Construir la familia de reseteo | feature | EP-T03 | `d` | Alta | Sin fijar | `05` §3.1, familia de reseteo; `02` §3.1, criterio de recorte de `CU-08008` | BT-08010 | La solicitud lleva el identificador de cuenta **y nada más**; el resultado declara la situación conservada, el cambio pendiente y la provisoria producida; la **única arista adicional** del grafo —reseteo hacia cuentas— queda con su motivo registrado y el grafo sigue siendo acíclico | US-08021, US-08022 |
| BT-08012 | Construir la familia de trabajo | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, familia de trabajo; `02` §6 `RT-03` y `RT-08` | BT-08006, BT-08008 | Transporta el envío, la eliminación y el estado, con el texto original **como cadena no interpretada**; la solicitud de eliminación es **única** para los dos papeles; el estado pertenece al conjunto cerrado de cuatro | US-08006, US-08007, US-08019 |
| BT-08013 | Construir la familia de listado con su carga útil acotada | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, familia de listado; [`ADR-08005`](../05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md); `05` §8, fila de carga útil del listado | BT-08012 | **0** ocurrencias del texto original, **0** de componentes de pieza y **0** del comentario del administrador en la proyección; el alcance varía según el papel y excluye los trabajos en `Borrador` para el administrador | US-08008, US-08009, US-08019, US-08020 |
| BT-08014 | Construir la familia de detalle, con el comentario como bloque propio | feature | EP-T04 | `f` | Alta | Sin fijar | `05` §3.1, familia de detalle; `05` §6; `02` §6 `RT-09` | BT-08012 | Transporta piezas, componentes, observaciones y el texto original; **el comentario del administrador viaja como bloque propio y no comparte ni un campo con las observaciones** | US-08011, US-08012, US-08013, US-08015, US-08018, US-08020 |
| BT-08015 | Construir la familia de desenlace | feature | EP-T04 | `h` | Alta | Sin fijar | `05` §3.1, familia de desenlace; `02` §3.1, criterio de recorte de `CU-08007` | BT-08006, BT-08012 | Transporta el desenlace como conjunto cerrado de **dos** valores, el estado terminal alcanzado y el comentario opcional; declara **dos** códigos de rechazo propios; **ningún tipo permite salir de un estado terminal** | US-08017, US-08018 |
| BT-08016 | Armar la matriz tipo contra prueba de integración | docs | EP-T05 | `c` | Alta | Sin fijar | `05` §5 (sin etapa de pruebas propias); `05` §8, primera fila; `02` §6 `RT-07` | BT-08009 a BT-08015 | **100 %** de los tipos de transferencia con al menos una prueba de integración que golpea el servicio real; la matriz se entrega a 08 y se revisa al cerrar cada etapa | **Infraestructura compartida**: es el gate equivalente que reemplaza a la cobertura de líneas |
| BT-08017 | Adoptar el formato de intercambio que fijan los dos extremos | feature | EP-T05 | `c` | Alta | Sin fijar | `05` §11 PA-03; [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §5 y §6 | BT-08001, BT-08005 | Los tipos son **serializables sin comportamiento** —sin lógica en los descriptores de acceso, sin campos calculados y sin ciclos entre tipos—; la configuración de intercambio **no se decide acá**: se adopta la que el productor ya fijó para los dos extremos. **Caja temporal: antes de cerrar la etapa `c`** | **Infraestructura compartida**: condiciona a las 22 |
| BT-08018 | Confirmar los dos valores rotulados como asunción | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, primeras dos filas; `05` §11 PA-04; `PRODUCT-INTAKE` §22 asunción `A-4` | BT-08016 | El Product Owner confirma o corrige los dos valores **sobre su propio documento**; hasta entonces se usan como vigentes y la puerta **no se declara bloqueante** en 09. **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida**: condiciona la puerta del pipeline |

**Siete tareas se justifican como infraestructura compartida** —BT-08001, BT-08002, BT-08003, BT-08004, BT-08016, BT-08017 y BT-08018— y las otras once declaran al menos una historia consumidora.

## 4. Trazabilidad BT ↔ US ↔ CU

Las dieciocho filas están, una por tarea técnica, sin agrupar. Los contratos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-08001 | Infraestructura compartida (habilita a las 22) | CU-08001 a CU-08008 | ADR-08001 |
| BT-08002 | Infraestructura compartida | — (puerta de construcción) | `PRODUCT-INTAKE` §17.4.P.8, `05` §8 |
| BT-08003 | Infraestructura compartida | — (puerta de construcción) | `05` §8 |
| BT-08004 | Infraestructura compartida | CU-08001 a CU-08008 | `05` §11 PA-01 |
| BT-08005 | US-08014, US-08016 | CU-08006 | `05` §11 PA-02 |
| BT-08006 | US-08014, US-08015, US-08016 | CU-08006 | `05` §3.1, familia de error; ADR-08002 |
| BT-08007 | US-08014, US-08015, US-08016 | CU-08006 | Contratos-Abstractions §5.1; ADR-08002 |
| BT-08008 | US-08001, US-08002, US-08003, US-08004, US-08005, US-08007, US-08014, US-08016, US-08021, US-08022 | CU-08001, CU-08002, CU-08003, CU-08006, CU-08008 | ADR-08004, `05` §3.2 |
| BT-08009 | US-08001 | CU-08001 | `05` §3.1, familia de sesión |
| BT-08010 | US-08002, US-08003, US-08004, US-08005, US-08022 | CU-08002, CU-08008 | `05` §3.1, familia de cuentas |
| BT-08011 | US-08021, US-08022 | CU-08008 | `05` §3.1, familia de reseteo |
| BT-08012 | US-08006, US-08007, US-08019 | CU-08003, CU-08004 | `05` §3.1, familia de trabajo |
| BT-08013 | US-08008, US-08009, US-08019, US-08020 | CU-08004 | ADR-08005 |
| BT-08014 | US-08011, US-08012, US-08013, US-08015, US-08018, US-08020 | CU-08005, CU-08007 | `05` §3.1, familia de detalle |
| BT-08015 | US-08017, US-08018 | CU-08007 | `05` §3.1, familia de desenlace |
| BT-08016 | Infraestructura compartida | CU-08001 a CU-08008 | `05` §8, tipos ejercitados por integración |
| BT-08017 | Infraestructura compartida | CU-08001 a CU-08008 | `05` §11 PA-03 |
| BT-08018 | Infraestructura compartida | — (puerta de verificación) | `05` §11 PA-04 |

**Cobertura inversa: los ocho contratos de uso tienen al menos una tarea técnica que los realiza.** CU-08001 en BT-08008 y BT-08009; CU-08002 en BT-08008 y BT-08010; CU-08003 en BT-08008 y BT-08012; CU-08004 en BT-08012 y BT-08013; CU-08005 en BT-08014; CU-08006 en BT-08005, BT-08006, BT-08007 y BT-08008; CU-08007 en BT-08014 y BT-08015; CU-08008 en BT-08008, BT-08010 y BT-08011. Las cuatro tareas transversales —BT-08001, BT-08004, BT-08016 y BT-08017— alcanzan a los ocho.

**Cobertura de las once restricciones transversales de `02` §6.** `RT-01` en BT-08008; `RT-02` en BT-08006 y BT-08007; `RT-03` en BT-08012; `RT-04` en BT-08013; `RT-05` en BT-08002; `RT-06` en BT-08017 y en la regla operativa de despliegue conjunto que 09 tiene que materializar; `RT-07` en BT-08016; `RT-08` en BT-08012 y BT-08015; `RT-09` en BT-08014; `RT-10` en BT-08009; `RT-11` en BT-08008. **Las once tienen tarea técnica.**

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Contracts`. Declara **cinco** épicas técnicas y **dieciocho** tareas técnicas inline —por debajo del umbral de treinta— cada una con tipo, fuente upstream por identificador, dependencias, criterios de aceptación verificables y las historias que la consumen. Declara por qué buena parte de las tareas de este proyecto de código son de forma y de inspección y no de lógica: el ensamblado no tiene comportamiento y su pipeline no tiene etapa de pruebas propias. Convierte en trabajo los cuatro puntos abiertos de la categoría 05 —nombres por familia, zona horaria y precisión del momento, adopción del formato de intercambio y confirmación de los dos valores rotulados como asunción—. Emite la matriz BT ↔ US ↔ CU con sus dieciocho filas, la cobertura inversa sobre los ocho contratos de uso y la cobertura de las **once** restricciones transversales. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **3**. Sube minor. |
