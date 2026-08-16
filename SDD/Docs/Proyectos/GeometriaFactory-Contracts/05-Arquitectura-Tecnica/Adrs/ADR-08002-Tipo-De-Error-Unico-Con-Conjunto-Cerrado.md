# ADR-08002 — Un único tipo de error, con conjunto cerrado de diecisiete códigos

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ADR-08002-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Todo fallo que cruza la frontera entre las dos unidades desplegables tiene que llegar representado: el producto define el **fallo silencioso** como el problema que viene a eliminar, y la necesidad NB-00008 exige además que la indisponibilidad se presente como estado degradado explícito y no como una excepción sin manejar.

A la vez, RA-03 prohíbe que un mensaje exponga la dirección de un servicio interno. Cada tipo capaz de transportar texto libre hacia la unidad pública es un lugar donde esa prohibición se puede violar, de modo que **la cantidad de tipos de error es una superficie de riesgo**.

El conjunto de códigos tiene historia y conviene tenerla presente al decidir: creció de trece a catorce, a dieciséis y a diecisiete; **se achicó por primera vez a quince** cuando RN-08016 unificó los dos mecanismos de credencial inicial del producto y dos códigos perdieron su causa; y volvió a **diecisiete** cuando el Product Owner incorporó `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` (`PRODUCT-INTAKE` **1.29** §17.4 P.3), que **este proyecto de código emite formalmente** en [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §5.1. El catálogo de la categoría 03 enumera hoy **veinte identificadores emitidos —diecisiete vivos y tres retirados—**, y es la única tabla del proyecto de código donde están juntos ([`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §3.2).

Motivación upstream: NB-00002, NB-00004, NB-00008, NB-00009; RN-08003, RN-08009, RN-08010, RN-08011, RN-08013, RN-08015, RN-08016; RA-03.

## 2. Decisión

**Un único tipo de error para las ocho familias**, con cuatro campos —código, texto neutro, colección de detalles de ubicación y momento— y **un conjunto cerrado de diecisiete códigos vivos**.

Tres reglas que acompañan a la decisión:

1. **Un código se justifica por lo que el consumidor tiene que hacer**, no por la causa que lo produjo. Por eso `CONTRATO_TRABAJO_NO_ENCONTRADO` cubre tres causas —inexistente, ajeno y fuera del alcance del solicitante— con un solo código, y `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` cubre todas las operaciones bloqueadas y los **dos** orígenes de la marca.
2. **El conjunto se cierra por abajo con `CONTRATO_ERROR_NO_CLASIFICADO`**, que garantiza que ningún fallo llegue sin representación y evita tener que agregar un código ante cada situación nueva.
3. **Ningún identificador retirado se recicla.** Un identificador que salió del conjunto no vuelve a nombrar otra condición.

La fuente de verdad del conjunto es el contrato de uso [`CU-08006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) de la categoría 02; esta ADR no acuña ningún código.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Un tipo de error único con conjunto cerrado (**adoptada**) | Un solo lugar donde se puede filtrar una dirección de servicio; el consumidor trata el error igual venga de donde venga; el conjunto es enumerable e inspeccionable | El tipo tiene que servir a ocho familias, así que sus campos son genéricos; y el conjunto crece con el producto |
| Un tipo de error por familia de tipos | Cada familia declararía exactamente sus condiciones, con campos a medida | Multiplica por ocho los lugares donde RA-03 se puede violar, y obliga a la unidad pública a ocho tratamientos del mismo trabajo. Es la razón que declara [`CU-08006`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) §10 |
| Conjunto **abierto** de códigos, con cadena libre | Nunca hay que agregar un código; el servicio de datos informa lo que quiera | El consumidor no puede tratar exhaustivamente los casos y termina mostrando texto crudo, que es por donde se filtra una dirección de servicio. Y el conjunto deja de ser verificable |
| Un código por operación bloqueada por el cambio de contraseña pendiente | Le diría al consumidor exactamente qué operación rechazó | Información que el consumidor no usa: el trabajo que le queda es siempre el mismo, derivar al cambio de contraseña. Es el criterio que CA-07 y CA-08 de CU-08006 verifican |

## 5. Consecuencias positivas

1. **Una sola superficie que auditar** contra RA-03, y una sola prueba de inspección que la cubre (CA-01 de CU-08006).
2. El consumidor puede tratar el conjunto exhaustivamente, porque es cerrado y enumerable.
3. No hay camino por el que un fallo llegue sin representación al lado público.
4. La regla de no reciclado protege a un consumidor que quedó atrás de interpretar un código con la causa anterior.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que agregar o quitar un código sea un cambio incompatible**, aunque compile: el consumidor dejaría de cubrir todos los casos. Es la cláusula que declara §17 de CU-08006.
2. **Se acepta perder capacidad de distinguir causas** donde el conjunto colapsa varias en un código. Es deliberado y el contrato lo declara: para RN-08003, es precisamente el requisito —el trabajo ajeno tiene que ser indistinguible del inexistente—.
3. **Se acepta que el detalle de ubicación conserve una capacidad que hoy no se ejerce.** El tipo puede transportar índice de figura, pero desde el modelo vigente **ningún código del conjunto lo usa**: los defectos de interpretación viajan como observaciones del detalle. La capacidad se conserva porque el intake la exige «cuando corresponde», y se declara para que 05 no la elimine por parecer muerta ni 08 la busque donde no está.
4. **Se acepta que el conjunto lo gobierne la categoría 02.** Esta ADR fija el criterio, no la lista.

## 7. Implementación

- El tipo de error declara **exactamente cuatro campos** y **cero** capaces de transportar una dirección de servicio, una ruta de archivo de datos o un valor de secreto.
- El código es un valor de conjunto cerrado, no una cadena libre.
- El texto es **neutro**: no nombra el campo que falló cuando eso revelaría información —el canje de credenciales no dice cuál de los dos campos era incorrecto— y nunca contiene una dirección.
- **Las tres señales declaradas no son códigos de error** y no se cuentan entre los diecisiete: viven en la §6.1 de CU-08003, CU-08004 y CU-08005, y se catalogan precisamente para que no se traten como error.
- Un identificador retirado queda registrado como retirado en el catálogo de 03, tachado y rotulado, y no se reutiliza.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Tipos de error del ensamblado | Exactamente **1** | Inspección de la superficie pública |
| Campos del tipo de error | Exactamente **4**, y **0** capaces de transportar dirección, ruta o secreto | CA-01 de CU-08006 |
| Códigos vivos del conjunto cerrado | Exactamente **17**, sobre **20** identificadores emitidos | CA-09 de CU-08006 e inspección del catálogo de 03 §3.2 |
| Códigos producidos fuera del conjunto | Exactamente **0** | Prueba de integración que recorre las condiciones y compara contra el conjunto |
| Códigos para las operaciones bloqueadas por la marca | Exactamente **1**, para todas las operaciones y para los **2** orígenes | CA-07 y CA-08 de CU-08006 |
| Identificadores retirados reciclados | Exactamente **0** | Inspección del catálogo de 03 |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) §6, §8, §10 y §17.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.2, §3.2 y §3.3.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §14 (RA-03), §17.4.P.5 y §4.1 (RN-08009, RN-08013, RN-08015, RN-08016).
- ADR relacionadas: [`ADR-08004`](ADR-08004-Regla-De-Exposicion-De-La-Frontera.md), [`ADR-08003`](ADR-08003-Versionado-Por-Compilacion-Compartida.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el tipo de error único con conjunto cerrado de quince códigos vivos sobre dieciocho identificadores emitidos, las tres reglas que lo acompañan —código justificado por el trabajo del consumidor, cierre por abajo y no reciclado—, cuatro alternativas evaluadas y seis métricas de validación. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **5**. Sube minor. |
