# Definition of Done — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Definition-Of-Done.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`Criterios-Validacion.md`](Criterios-Validacion.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5, que declara que la DoD vive acá; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.5.P.7 y §17.5.P.8
**Trazabilidad downstream:** [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md), que **referencia** esta DoD y no la redefine; `09-Devops`, que materializa sus criterios mecánicos como etapas del pipeline

---

## Tabla de contenido

- [1. DoD por capa](#1-dod-por-capa)
  - [1.1 Historia de usuario](#11-historia-de-usuario)
  - [1.2 Tarea técnica](#12-tarea-técnica)
  - [1.3 Etapa](#13-etapa)
  - [1.4 Entrega del artefacto](#14-entrega-del-artefacto)
- [2. Excepciones admitidas](#2-excepciones-admitidas)
- [3. Vigencia](#3-vigencia)
- [4. Control de cambios](#4-control-de-cambios)

---

## 1. DoD por capa

**Por qué la tercera capa se llama «etapa» y no «sprint».** Este producto no tiene sprints: la unidad de planificación es la **etapa**.

**Y por qué la cuarta se llama «entrega del artefacto» y no «release».** Este proyecto de código **sí produce un artefacto entregable** —la imagen que corre en el servidor propio— pero `redistribuible` es false y **no se publica en ningún registro**: se construye en destino. Además **el despliegue no es del agente**: el intake §17.5.P.8 lo declara manual y del Product Owner. Lo que esta DoD declara terminado es **el artefacto entregado**, no el despliegue realizado.

Cada criterio responde a «¿cómo se valida?» con una operación concreta.

### 1.1 Historia de usuario

- [ ] Todos los criterios Given/When/Then de la historia están cubiertos por al menos un `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md). **Se valida** leyendo la columna de tests de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2.
- [ ] Esos `TC-XX` están escritos y **pasan**. **Se valida** con la salida de `scripts/test.sh`.
- [ ] **Si la historia agrega o modifica un punto de acceso, quedó declarado si está dentro de la guardia o si es una de las cuatro exenciones con su motivo, y `TC-07` se reejecutó.** **Se valida** con la tabla de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §5. **Una historia que agregue un punto y no reejecute `TC-07` no está terminada**: es el primer riesgo de `05` §9, y es un defecto de **omisión** que no se ve leyendo el punto nuevo.
- [ ] **Si la historia agrega una respuesta de fallo, `TC-24`, `TC-25`, `TC-26` y `TC-27` se reejecutaron.** **Se valida** con esos cuatro casos. Ninguna familia empobrecida puede haberse enriquecido.
- [ ] **Ninguna condición que la historia presenta está fuera del conjunto cerrado de quince códigos**, y ninguna se acuñó, renombró ni tradujo a texto acá. **Se valida** con `TC-27`.
- [ ] **Si la historia introduce una propiedad de ausencia** —cero exposiciones, cero truncamientos, cero eliminaciones fuera de alcance—, se midió **con umbral cero y en la condición declarada**. **Se valida** con el `TC-XX` correspondiente.
- [ ] **Si la historia introduce una acotación, existe un caso que la verifica forzando la petición.** **Se valida** con `TC-20` para la eliminación, que es la que la fuente exige así.
- [ ] La construcción termina en 0 y sin advertencias. **Se valida** con la salida de `scripts/build.sh`.
- [ ] La cobertura del componente que la historia toca no bajó respecto de la medición anterior. **Se valida** comparando el informe de cobertura por componente.

### 1.2 Tarea técnica

- [ ] Los criterios de aceptación que la tarea declara en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) se cumplen uno por uno. **Se valida** por inspección, por prueba automatizada o por medición de la puerta que la tarea nombra.
- [ ] Si la propiedad que la tarea sostiene es una **ausencia**, el criterio se midió **con umbral cero y en la condición declarada**, y no se dio por cumplido por no haberse observado lo contrario. **Se valida** con el `TC-XX` de inspección correspondiente.
- [ ] Si la tarea es de tipo indagación, la decisión que produjo está **registrada** en el documento que corresponde. **Se valida** leyendo ese documento.
- [ ] Si la tarea es una **puerta técnica**, se midió y su resultado quedó registrado, **y si no pasó, la salida declarada se ejecutó en lugar de arrastrarse como deuda**. **Se valida** con el informe de la medición.
- [ ] Si la tarea toca la composición de raíz, **la resolución de los cuatro puertos se verifica en el arranque y falta alguno falla en construcción**. **Se valida** con `TC-28`.
- [ ] La construcción y las dos baterías —unitaria y de integración— pasan enteras. **Se valida** con `scripts/build.sh` y `scripts/test.sh`.

### 1.3 Etapa

- [ ] Todas las historias de la épica de la etapa cumplen §1.1, y todas sus tareas técnicas cumplen §1.2. **Se valida** recorriendo el índice de la épica en [`../06-Backlog-Tecnico/Product-Backlog.md`](../06-Backlog-Tecnico/Product-Backlog.md) §3.
- [ ] Los **doce** criterios de salida de [`Plan-Pruebas.md`](Plan-Pruebas.md) §3 se cumplen. **Se valida** con esa lista.
- [ ] **`TC-07` cierra con 4 y 11 sobre los quince puntos, en las dos direcciones.** **Se valida** con la tabla de la matriz §5.
- [ ] **`TC-25` da 3 de 3 comparaciones idénticas.** **Se valida** con ese caso.
- [ ] **A partir de la etapa `f`: la batería del validador que corre desde acá pasa entera, 10 de 10.** **Se valida** con `CV-31`. **Nueve casos no cumplen**, y el motivo está en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6.
- [ ] Los criterios bloqueantes de [`Criterios-Validacion.md`](Criterios-Validacion.md) —`CV-16` a `CV-27`, `CV-35` a `CV-40`— se cumplen. **Se valida** con el informe del pipeline y con los casos nombrados.
- [ ] Los criterios condicionados —`CV-11` a `CV-15`, `CV-33`— **se midieron y se registraron**, aunque no bloqueen. **Se valida** con la presencia de la medición en el informe de cierre. Registrar «sin medir» cuando la medición era posible **no cumple**.
- [ ] Las dos baterías completas —y no sólo lo que la etapa tocó— corren y pasan. **Se valida** con `CV-28`.
- [ ] Ningún caso de verificación que pasaba dejó de pasar sin justificación escrita. **Se valida** con `CV-29`.
- [ ] [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) está actualizada, **incluida su tabla de quince puntos de acceso**. **Se valida** comparando la matriz contra el índice de la épica.
- [ ] Todo defecto cerrado en la etapa generó al menos un `TC-XX`. **Se valida** con `CV-32`.
- [ ] El punto de control de la etapa tiene el **OK explícito del Product Owner**, con constancia escrita. **Se valida** con el informe de cierre (intake §15, reglas de delivery 2 y 3).

### 1.4 Entrega del artefacto

Se aplica cada vez que el artefacto del servidor propio se construye para entregarse.

- [ ] **`PT-04` pasa**: la imagen se construye con su archivo de construcción **multietapa**, arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. **Se valida** con la medición registrada de la puerta.
- [ ] La imagen final lleva **sólo el entorno de ejecución**, sin kit de desarrollo ni depurador, y **no tiene linaje con la imagen del contenedor de desarrollo**. **Se valida** por inspección del archivo de construcción.
- [ ] **Ningún secreto entra al repositorio ni a la imagen.** La clave de firma y la ubicación del almacén llegan por variable de entorno o archivo montado. **Se valida** por inspección del repositorio, del archivo de construcción y del de composición.
- [ ] El almacén apunta a un **volumen persistente** y no a una ruta dentro de la imagen. **Se valida** por inspección del archivo de composición.
- [ ] La etiqueta de la etapa existe y permite **volver a cualquier demostración ya aprobada**. **Se valida** con el registro de la etiqueta.
- [ ] **El artefacto queda entregado, no desplegado.** El archivo de construcción y el de composición se entregan; **el despliegue lo ejecuta el Product Owner**. **Se valida** con la constancia de la entrega en el informe de cierre.
- [ ] La reversión está disponible: **volver a la etiqueta anterior y reconstruir**. **Se valida** con la etiqueta previa existente.

## 2. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba | Qué queda registrado |
| --- | --- | --- | --- |
| Criterio **condicionado** no alcanzado | Deja de bloquear el cierre, porque su umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado en [`Criterios-Validacion.md`](Criterios-Validacion.md) §6 | La medición y su distancia al umbral |
| Mutation score **no exigible todavía** | `CV-34` se reporta «sin medir» hasta que la herramienta corra. **La composición de raíz queda exenta** con su fundamento | — | El hueco de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §8 |
| Deuda técnica que una etapa no alcanza a cerrar | El criterio se difiere **una sola vez**, y sólo si no es de los bloqueantes de §1.3 | El Product Owner, en el punto de control | Una `BT-XX` nueva, con la etapa en que se cierra |
| **Puerta técnica que no pasa** | **No se admite excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella | El Product Owner decide la salida | La medición y la salida ejecutada |
| **Punto de acceso agregado sin declarar su ubicación respecto de la guardia** | **No se admite.** Es el primer riesgo de `05` §9 y **nada falla cuando ocurre** | — | — |
| **Familia empobrecida enriquecida** | **No se admite.** La respuesta más informativa es la tentadora, y **ninguna capa de adentro puede repararla** | — | — |
| **Cuerpo truncado en lugar de rechazado** | **No se admite.** Rompe `RN-08` en silencio | — | — |
| **Eliminación fuera de alcance dada por verificada sin forzar la petición** | **No se admite.** Es el único criterio del producto que la fuente exige ejercer así | — | — |
| **Batería del validador cerrada con nueve casos** | **No se admite.** Tiene **diez**, y el décimo cubre `E-8`. El intake **1.20** lo dice así en §17.5.P.8 | — | — |

## 3. Vigencia

**Este documento es la fuente canónica de la Definition of Done de `GeometriaFactory-Api`.**

- [`../07-Plan-Sprint/Mini-Plan.md`](../07-Plan-Sprint/Mini-Plan.md) y cualquier plan de etapa **referencian** esta DoD y no la redefinen. Una lista de criterios de cierre escrita en un plan es un hallazgo, y el que rige es éste.
- [`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §5 declara que la DoD vive en esta categoría y que hasta su emisión regían los criterios de transición del roadmap §5, que son de nivel producto. **Con esta emisión ese interinato termina**: los criterios del roadmap siguen valiendo a nivel producto y esta DoD los complementa a nivel de proyecto de código.
- **Esta DoD no declara terminado ningún despliegue.** El despliegue es manual y del Product Owner, y ningún criterio de este documento se cumple ejecutándolo.
- **El conjunto cerrado de códigos no se amplía desde este documento.** Es del ensamblado de contratos; esta DoD exige que se respete, no lo define.
- Todo cambio en los criterios de §1 se registra en §4 y se comunica en el punto de control de la etapa siguiente.
- La DoD **no habla de cuándo empezar**: eso es la Definition of Ready, y las dos no se solapan.

## 4. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** El caso de excepción «batería del validador cerrada con nueve casos» describía la redacción del gate del intake sin decir que ya está corregida; ahora cita el intake **1.20** §17.5.P.8, que dice **diez**. No se admite igual, y el umbral de la batería sigue siendo **10 de 10**. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la DoD en **cuatro** capas —historia, tarea técnica, **etapa** y **entrega del artefacto**—, con el fundamento de por qué la tercera no se llama sprint y la cuarta no se llama release ni despliegue. Cada criterio responde «cómo se valida» con una operación concreta. Incorpora como criterio de historia que **todo punto de acceso nuevo declare su ubicación respecto de la guardia y reejecute `TC-07`**, y que toda respuesta de fallo nueva reejecute los cuatro casos de traducción. Su §1.4 declara los criterios del artefacto, incluidos `PT-04`, la ausencia de secretos en la imagen y el volumen persistente, con la constancia de que **el artefacto queda entregado, no desplegado**. Declara **nueve** casos de excepción, **seis** de ellos sin excepción posible, y la vigencia como fuente canónica. |
