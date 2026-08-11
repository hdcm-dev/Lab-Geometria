# Definition of Done — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Definition-Of-Done.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15 y §17.4.P.3
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`

---

## Tabla de contenido

- [1. DoD por capa](#1-dod-por-capa)
  - [1.1 Historia de usuario](#11-historia-de-usuario)
  - [1.2 Tarea técnica](#12-tarea-técnica)
  - [1.3 Etapa](#13-etapa)
  - [1.4 Entrega del proyecto de código](#14-entrega-del-proyecto-de-código)
- [2. Excepciones admitidas](#2-excepciones-admitidas)
- [3. Vigencia](#3-vigencia)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. DoD por capa

**Por qué la tercera capa se llama «etapa» y la cuarta «entrega del proyecto de código».** El producto no tiene sprints —la unidad de planificación es la etapa— y este ensamblado **no se publica**: `redistribuible` es false y no viaja a ningún repositorio de paquetes. Llamar «release» a su cierre habría inventado un acto que no existe.

**Una particularidad de este proyecto de código que atraviesa las cuatro capas:** buena parte de sus criterios se cumplen **leyendo la superficie pública y no ejecutando nada**. Están escritos como recuentos para que se puedan verificar mecánicamente.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX`. **Se valida** leyendo la columna de test de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 para su `CU-XX`.
- [ ] Los `TC-XX` de **inspección de superficie** de esa historia están escritos y en verde. **Se valida** con la salida de la inspección.
- [ ] Los `TC-XX` de **integración** de esa historia están en verde, **o declarados diferidos por escrito** con la etapa en que se ejecutan. **Se valida** con el informe de cierre.
- [ ] La historia no introdujo ningún campo capaz de transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos ni una traza. **Se valida** con `TC-15` sobre la familia que la historia toca. **Es el criterio 6 de la DoR verificado del lado del cierre.**
- [ ] Todo código de error que la historia usa pertenece al conjunto cerrado de **quince**. **Se valida** con `TC-16`.
- [ ] La historia **no redactó ninguna regla de negocio**. **Se valida** leyendo su tabla de trazabilidad: las reglas se refieren por identificador a `GeometriaFactory-Domain`.
- [ ] Si la historia agregó o quitó un valor de un conjunto cerrado, el cambio está declarado como incompatible en el `§17` del contrato de uso. **Se valida** leyendo esa sección.
- [ ] El ensamblado compila sin advertencias. **Se valida** con `scripts/build.sh`.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección de la superficie pública o por prueba de integración, según lo exige la DoR §2 criterio 3.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** y no sólo tomada. **Se valida** leyendo el documento donde quedó.
- [ ] Si la tarea introdujo una arista nueva entre familias, su motivo está declarado y el grafo sigue siendo acíclico. **Se valida** con `CV-23`.
- [ ] Si la tarea es una puerta —`BT-02`, `BT-03`, `BT-08`, `BT-16`— la puerta se midió al menos una vez y su resultado quedó registrado. **Se valida** con la salida del pipeline o de la inspección.
- [ ] La construcción pasa. **Se valida** con `scripts/build.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **nueve** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-10` a `CV-14`, `CV-21` a `CV-23`— se cumplen. **Se valida** con `TC-15`, `TC-16`, `TC-20` y el informe de construcción.
- [ ] El criterio condicionado —`CV-09`— **se midió y se registró**, aunque no bloquee. **Se valida** con la presencia de la medición en el informe de cierre.
- [ ] **Todas** las inspecciones de superficie escritas hasta ese momento se reejecutan, y no sólo las de las familias que la etapa tocó. **Se valida** con `CV-15`.
- [ ] Ningún `TC-XX` que estaba en verde pasó a rojo sin justificación escrita. **Se valida** con `CV-16`.
- [ ] Si la etapa cambió un conjunto cerrado, el cambio está declarado incompatible y el despliegue conjunto está previsto. **Se valida** con `CV-18` y `CV-19`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada. **Se valida** comparándola contra el índice de la épica.
- [ ] Todo defecto cerrado generó al menos un `TC-XX`. **Se valida** con `CV-17`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**. **Se valida** con el informe de cierre (intake §15).

### 1.4 Entrega del proyecto de código

Se aplica cuando las **siete** etapas comprometidas que este proyecto de código toca —`a`, `c`, `d`, `e`, `f`, `g` y `h`— están cerradas.

- [ ] Los **veinticinco** criterios de [`Criterios-Validacion.md`](Criterios-Validacion.md) están evaluados uno por uno, con su resultado registrado.
- [ ] **8 de 8** contratos de uso, **21 de 21** historias comprometidas y **16 de 16** reglas con caso de prueba en verde. **Se valida** con los recuentos de la matriz. `US-10` y `TC-11` quedan declarados fuera del tramo comprometido.
- [ ] **100 %** de los tipos ejercitados por al menos una prueba de integración contra el servicio real. **Se valida** con `TC-21`. **Criterio bloqueante**: el intake §17.4.P.6 lo llama «el gate equivalente y bloqueante», y §22 `A-4` declara que un cambio del Product Owner «cambia la forma del gate, no su carácter bloqueante». Lo que puede cambiar es **cómo** se expresa la condición.
- [ ] **15** códigos vivos, **18** emitidos, **3** retirados y **0** reciclados. **Se valida** con `TC-16`.
- [ ] **0** referencias hacia `GeometriaFactory-Domain` y **0** campos de filtración. **Se valida** con `TC-20` y `TC-15`.
- [ ] Los **ocho** escenarios del intake §20 siguen siendo el material de los casos de prueba que los usan. **Se valida** con `CV-07`.
- [ ] Los dos valores rotulados **[ASUNCIÓN]** están confirmados, o su continuidad como asunción está declarada. **Se valida** leyendo el intake §22 —fila `A-4`— y §17.4.P.10, más el estado de `BT-18`. **Que el de `QG-05` siga sin confirmar no lo vuelve condicionado**: el gate bloquea igual y lo sujeto a confirmación es su forma.
- [ ] No queda ningún punto abierto de `05` §11 sin desenlace declarado. **Se valida** leyendo esa tabla, que hoy declara **tres abiertos y uno resuelto**.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre de la etapa | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6 | La medición y su distancia al umbral |
| Prueba de integración **no ejecutable todavía** | Se difiere **por escrito**, con la etapa en que se ejecuta. La inspección de superficie correspondiente **sí se ejecuta y no se difiere** | El Product Owner, en el punto de control | La declaración de diferimiento en el informe de cierre |
| Deuda técnica que una etapa no alcanza a cerrar | Se difiere **una sola vez**, y nunca si es de los bloqueantes de §1.3 | El Product Owner | Una `BT-XX` nueva con la etapa en que se cierra |
| Campo nuevo que la revisión rechaza por la regla de exposición | **No se admite excepción.** Es el defecto que entra sin que nadie lo note porque compila | — | — |
| Código retirado que se quiere reponer | **No se admite.** Contradice `CA-09` de `CU-06` y describe situaciones que `RN-16` volvió imposibles | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Contracts`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) habla de **cuándo empezar**; este documento, de cuándo está terminado. Los dos criterios de exposición y de conjunto cerrado aparecen en los dos, y no es duplicación: allá son condición de entrada de una historia y acá son condición de cierre verificada sobre lo construido.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** Los tres puntos que trataban a `CV-08` como criterio condicionado pasan a tratarlo como **bloqueante**, por la fila `A-4` del intake §22 y por §17.4.P.6. Se aclara además que **que la asunción siga sin confirmar no vuelve condicionado al gate**: lo sujeto a confirmación es su forma. Ningún umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y entrega del proyecto de código—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release. Cada criterio responde «cómo se valida» con una operación concreta, y la mayoría con un recuento sobre la superficie pública. Distingue en las cuatro capas entre las inspecciones de superficie, que no se difieren, y las pruebas de integración, que sí pueden diferirse por escrito mientras `GeometriaFactory-Api` no exista. Declara **cinco** casos de excepción, dos de ellos sin excepción posible, y la vigencia como fuente canónica con la delimitación frente a la Definition of Ready. |
