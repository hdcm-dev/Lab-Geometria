# ADR-02 — Un único tipo de error, con conjunto cerrado de quince códigos

**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Todo fallo que cruza la frontera entre las dos unidades desplegables tiene que llegar representado: el producto define el **fallo silencioso** como el problema que viene a eliminar, y la necesidad NB-08 exige además que la indisponibilidad se presente como estado degradado explícito y no como una excepción sin manejar.

A la vez, RA-03 prohíbe que un mensaje exponga la dirección de un servicio interno. Cada tipo capaz de transportar texto libre hacia la unidad pública es un lugar donde esa prohibición se puede violar, de modo que **la cantidad de tipos de error es una superficie de riesgo**.

El conjunto de códigos tiene historia y conviene tenerla presente al decidir: creció de trece a catorce, a dieciséis y a diecisiete, y **se achicó por primera vez a quince** cuando RN-16 unificó los dos mecanismos de credencial inicial del producto y dos códigos perdieron su causa. El catálogo de la categoría 03 enumera hoy **dieciocho identificadores emitidos —quince vivos y tres retirados—**, y es la única tabla del proyecto de código donde están juntos ([`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §3.2).

Motivación upstream: NB-02, NB-04, NB-08, NB-09; RN-03, RN-09, RN-10, RN-11, RN-13, RN-15, RN-16; RA-03.

## 2. Decisión

**Un único tipo de error para las ocho familias**, con cuatro campos —código, texto neutro, colección de detalles de ubicación y momento— y **un conjunto cerrado de quince códigos vivos**.

Tres reglas que acompañan a la decisión:

1. **Un código se justifica por lo que el consumidor tiene que hacer**, no por la causa que lo produjo. Por eso `CONTRATO_TRABAJO_NO_ENCONTRADO` cubre tres causas —inexistente, ajeno y fuera del alcance del solicitante— con un solo código, y `CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO` cubre todas las operaciones bloqueadas y los **dos** orígenes de la marca.
2. **El conjunto se cierra por abajo con `CONTRATO_ERROR_NO_CLASIFICADO`**, que garantiza que ningún fallo llegue sin representación y evita tener que agregar un código ante cada situación nueva.
3. **Ningún identificador retirado se recicla.** Un identificador que salió del conjunto no vuelve a nombrar otra condición.

La fuente de verdad del conjunto es el contrato de uso [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) de la categoría 02; esta ADR no acuña ningún código.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Un tipo de error único con conjunto cerrado (**adoptada**) | Un solo lugar donde se puede filtrar una dirección de servicio; el consumidor trata el error igual venga de donde venga; el conjunto es enumerable e inspeccionable | El tipo tiene que servir a ocho familias, así que sus campos son genéricos; y el conjunto crece con el producto |
| Un tipo de error por familia de tipos | Cada familia declararía exactamente sus condiciones, con campos a medida | Multiplica por ocho los lugares donde RA-03 se puede violar, y obliga a la unidad pública a ocho tratamientos del mismo trabajo. Es la razón que declara [`CU-06`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) §10 |
| Conjunto **abierto** de códigos, con cadena libre | Nunca hay que agregar un código; el servicio de datos informa lo que quiera | El consumidor no puede tratar exhaustivamente los casos y termina mostrando texto crudo, que es por donde se filtra una dirección de servicio. Y el conjunto deja de ser verificable |
| Un código por operación bloqueada por el cambio de contraseña pendiente | Le diría al consumidor exactamente qué operación rechazó | Información que el consumidor no usa: el trabajo que le queda es siempre el mismo, derivar al cambio de contraseña. Es el criterio que CA-07 y CA-08 de CU-06 verifican |

## 5. Consecuencias positivas

1. **Una sola superficie que auditar** contra RA-03, y una sola prueba de inspección que la cubre (CA-01 de CU-06).
2. El consumidor puede tratar el conjunto exhaustivamente, porque es cerrado y enumerable.
3. No hay camino por el que un fallo llegue sin representación al lado público.
4. La regla de no reciclado protege a un consumidor que quedó atrás de interpretar un código con la causa anterior.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que agregar o quitar un código sea un cambio incompatible**, aunque compile: el consumidor dejaría de cubrir todos los casos. Es la cláusula que declara §17 de CU-06.
2. **Se acepta perder capacidad de distinguir causas** donde el conjunto colapsa varias en un código. Es deliberado y el contrato lo declara: para RN-03, es precisamente el requisito —el trabajo ajeno tiene que ser indistinguible del inexistente—.
3. **Se acepta que el detalle de ubicación conserve una capacidad que hoy no se ejerce.** El tipo puede transportar índice de figura, pero desde el modelo vigente **ningún código del conjunto lo usa**: los defectos de interpretación viajan como observaciones del detalle. La capacidad se conserva porque el intake la exige «cuando corresponde», y se declara para que 05 no la elimine por parecer muerta ni 08 la busque donde no está.
4. **Se acepta que el conjunto lo gobierne la categoría 02.** Esta ADR fija el criterio, no la lista.

## 7. Implementación

- El tipo de error declara **exactamente cuatro campos** y **cero** capaces de transportar una dirección de servicio, una ruta de archivo de datos o un valor de secreto.
- El código es un valor de conjunto cerrado, no una cadena libre.
- El texto es **neutro**: no nombra el campo que falló cuando eso revelaría información —el canje de credenciales no dice cuál de los dos campos era incorrecto— y nunca contiene una dirección.
- **Las tres señales declaradas no son códigos de error** y no se cuentan entre los quince: viven en la §6.1 de CU-03, CU-04 y CU-05, y se catalogan precisamente para que no se traten como error.
- Un identificador retirado queda registrado como retirado en el catálogo de 03, tachado y rotulado, y no se reutiliza.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Tipos de error del ensamblado | Exactamente **1** | Inspección de la superficie pública |
| Campos del tipo de error | Exactamente **4**, y **0** capaces de transportar dirección, ruta o secreto | CA-01 de CU-06 |
| Códigos vivos del conjunto cerrado | Exactamente **15**, sobre **18** identificadores emitidos | CA-09 de CU-06 e inspección del catálogo de 03 §3.2 |
| Códigos producidos fuera del conjunto | Exactamente **0** | Prueba de integración que recorre las condiciones y compara contra el conjunto |
| Códigos para las operaciones bloqueadas por la marca | Exactamente **1**, para todas las operaciones y para los **2** orígenes | CA-07 y CA-08 de CU-06 |
| Identificadores retirados reciclados | Exactamente **0** | Inspección del catálogo de 03 |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md) §6, §8, §10 y §17.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.2, §3.2 y §3.3.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §14 (RA-03), §17.4.P.5 y §4.1 (RN-09, RN-13, RN-15, RN-16).
- ADR relacionadas: [`ADR-04`](ADR-04-Regla-De-Exposicion-De-La-Frontera.md), [`ADR-03`](ADR-03-Versionado-Por-Compilacion-Compartida.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el tipo de error único con conjunto cerrado de quince códigos vivos sobre dieciocho identificadores emitidos, las tres reglas que lo acompañan —código justificado por el trabajo del consumidor, cierre por abajo y no reciclado—, cuatro alternativas evaluadas y seis métricas de validación. |
