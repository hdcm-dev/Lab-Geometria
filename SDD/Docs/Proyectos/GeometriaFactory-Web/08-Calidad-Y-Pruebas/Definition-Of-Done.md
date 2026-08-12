# Definition of Done — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Definition-Of-Done.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) **1.2**; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15 y §17.6.P.8
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`, que materializa sus criterios mecánicos como pasos del flujo de publicación

---

## Tabla de contenido

- [1. DoD por capa](#1-dod-por-capa)
  - [1.1 Historia de usuario](#11-historia-de-usuario)
  - [1.2 Tarea técnica](#12-tarea-técnica)
  - [1.3 Etapa](#13-etapa)
  - [1.4 Publicación](#14-publicación)
- [2. Excepciones admitidas](#2-excepciones-admitidas)
- [3. Vigencia](#3-vigencia)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. DoD por capa

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**. Llamarla sprint habría creado una unidad que ninguna fuente tiene.

**Y por qué la cuarta se llama «publicación» y no «release».** Este proyecto de código **sí se entrega**, a diferencia de las bibliotecas del producto: se publica en el hosting público por el flujo de publicación. Pero **no se versiona como paquete redistribuible** —`redistribuible` es false—, de modo que lo que se declara terminado no es una versión liberada sino **una publicación que quedó en pie**.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están ejecutados y **pasan**. **Se valida** con el registro del guion de la etapa.
- [ ] **Si la historia introduce una acotación, existe un caso que la verifica forzando la solicitud sin pasar por la pantalla.** **Se valida** con los seis casos de `CV-05`. Una historia que sólo demuestre que el control no se dibuja **no está terminada**: eso acota lo que se ofrece y no prueba nada.
- [ ] La superficie que la historia declaró en su Definition of Ready criterio 4 **tiene sus filas de la matriz de sensado verificadas**, con estado y fecha. **Se valida** leyendo [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md).
- [ ] **Ninguna condición que la historia presenta está fuera de los diecisiete códigos vivos** ni del camino de ausencia de respuesta, y ninguna expone dirección, ruta ni traza. **Se valida** con `TC-31`.
- [ ] La historia **no introdujo ninguna petición del navegador hacia el servicio de datos** ni ninguna salida nueva. **Se valida** con `TC-29` y `TC-30`.
- [ ] Si la historia toca la escena, **no introdujo invocaciones al interior del bundle** ni tráfico de circuito durante la interacción. **Se valida** con `TC-32` y `TC-33`.
- [ ] La construcción termina **sin advertencias**. **Se valida** con la etapa de construcción del flujo de publicación.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por ejecución observada o por medición de la puerta que la tarea nombra.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia** —cero peticiones del navegador, cero salidas nuevas, cero invocaciones al interior del bundle, cero apariciones de la credencial—, el criterio se midió **con umbral cero y en la condición declarada**. **Se valida** con el `TC-XX` de inspección correspondiente. En particular, **el conteo de peticiones se hace con los dos movimientos automáticos prendidos**: sin esa condición no cuenta.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde. **Se valida** leyendo ese documento.
- [ ] Si la tarea es una **puerta técnica**, se midió y su resultado quedó registrado, **y si no pasó, la salida declarada se ejecutó en lugar de arrastrarse como deuda**. **Se valida** con el informe de la medición.
- [ ] La construcción pasa entera y el bundle **se generó en el mismo flujo**, no se tomó de un artefacto viejo. **Se valida** con el registro del flujo de publicación.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **once** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **El guion de demostración de la etapa y los de todas las anteriores pasan al 100 %, sin correcciones.** **Se valida** con `TC-35` y `CV-23`. Ejecutar sólo el de la etapa en curso **no cumple**.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-13`, `CV-14` a `CV-18`, `CV-20` a `CV-22`, `CV-31` a `CV-35`— se cumplen. **Se valida** con el registro del flujo y con `TC-29` a `TC-33`.
- [ ] `CV-13` **se cumple**, y no sólo se midió: es **bloqueante** aunque su forma de puerta siga rotulada [ASUNCIÓN], porque el intake §22 `A-4` declara que lo que puede cambiar es la forma del gate y no su carácter. **Se valida** con la presencia de la medición **y de su resultado en verde** en el informe de cierre.
- [ ] **Las filas de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) que la etapa toca están verificadas, con estado y fecha, y ninguna deriva mayor queda abierta.** **Se valida** recorriendo la matriz. Una deriva mayor se resuelve **corrigiendo lo construido o actualizando la línea de base con aprobación humana explícita**, nunca por omisión.
- [ ] Toda deriva **menor** quedó registrada aunque no bloquee. **Se valida** con `CV-27`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada: ninguna fila dice `Pendiente` para un elemento que la etapa cerró. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-28`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Publicación

Se aplica cada vez que el flujo de publicación corre hacia el hosting público.

- [ ] La construcción terminó **sin advertencias** y el bundle se generó **en el mismo flujo**. **Se valida** con el registro del flujo (intake §17.6.P.8).
- [ ] La dirección del servicio de datos se inyectó desde los secretos y **la dirección real del servidor propio no quedó versionada**. **Se valida** por inspección del repositorio y del registro del flujo.
- [ ] **El flujo no terminó en la subida: terminó comprobando que la dirección pública responde.** **Se valida** con el paso final del flujo. Es la única forma de que una subida no transaccional que deja la aplicación caída no se reporte como exitosa.
- [ ] La etiqueta de la etapa existe y permite volver a cualquier demostración ya aprobada. **Se valida** con el registro de la etiqueta.
- [ ] Si la publicación no dejó la aplicación en pie, **se volvió a publicar desde la etiqueta anterior**. **Se valida** con el registro de la reversión.
- [ ] La publicación se hizo **fuera del horario de uso**, porque la subida no es transaccional. **Se valida** con la hora registrada del flujo.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| **`CV-13` no alcanzado** | **Nada.** No es condicionado: lo rotulado [ASUNCIÓN] es la **forma de la puerta**, y el intake §22 `A-4` deja a salvo su carácter bloqueante | El Product Owner, con constancia escrita, como en cualquier criterio bloqueante | La medición, su distancia al umbral y la remediación, en el informe de cierre |
| Cobertura de líneas **no exigible** | El criterio `CV-30` se declara «no aplica» mientras no exista proyecto de pruebas propio | — | El fundamento del intake §17.6.P.6 |
| Deriva **menor** | Se registra y **no bloquea** el cierre | — | La fila de la matriz, con su estado y su fecha |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| **Puerta técnica que no pasa** | **No se admite excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella y **no se arrastra como deuda**. Lo que se ejecuta es la salida que la puerta declara | El Product Owner decide la salida, no la excepción | La medición y la salida ejecutada |
| **Deriva mayor sin resolver** | **No se admite.** Se corrige lo construido o se actualiza la línea de base con aprobación humana explícita | — | — |
| **Guion ejecutado sólo para la etapa en curso** | **No se admite.** Es la regla de no-regresión acumulativa del intake §15, y **no es la parte rotulada [ASUNCIÓN]** | — | — |
| **Acotación dada por verificada mirando la pantalla** | **No se admite.** Esta pieza no hace cumplir reglas: si no se forzó la solicitud, no se verificó nada | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Web`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código, sin contradecirlos.
- **Los umbrales de deriva de [`Matriz-Sensado-Deriva.md`](Matriz-Sensado-Deriva.md) no se cambian desde este documento.** Esta DoD exige que la matriz se verifique y que ninguna deriva mayor quede abierta; qué constituye deriva mayor lo declara la matriz, y cambiarlo requiere aprobación humana sobre la línea de base.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-02`.** `CV-13` pasa de criterio condicionado a **bloqueante**: se lo agrega a la lista de bloqueantes de la DoD de etapa y su fila de flexibilización deja de conceder nada. Ningún umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y **publicación**—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release, dado que este proyecto de código sí se entrega pero no se versiona como paquete redistribuible. Cada criterio responde «cómo se valida» con una operación concreta. Incorpora como criterio de historia que **toda acotación se verifique forzando la solicitud**, como criterio de tarea técnica que las propiedades de ausencia se midan **en su condición declarada** —el conteo de peticiones con los dos movimientos prendidos—, y como criterio de etapa que **ninguna deriva mayor quede abierta**. Su §1.4 declara los criterios de la publicación, incluida la comprobación de que la dirección pública responde. Declara **ocho** casos de excepción, **cuatro** de ellos sin excepción posible, y la vigencia como fuente canónica, con la constancia de que los umbrales de deriva **no se cambian desde este documento**. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
