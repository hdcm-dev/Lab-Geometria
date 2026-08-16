# Criterios de validación — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Criterios-Validacion.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) 1.1; [`Estrategia-Calidad.md`](Estrategia-Calidad.md) 1.1 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §11; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §15, §17.5.P.6, §17.5.P.8, §17.5.P.10, §21 y §22
**Trazabilidad downstream:** [`Definition-Of-Done.md`](Definition-Of-Done.md); `09-Devops`

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Criterios funcionales](#2-criterios-funcionales)
- [3. Criterios no funcionales](#3-criterios-no-funcionales)
- [4. Criterios de regresión](#4-criterios-de-regresión)
- [5. Criterios de calidad de código](#5-criterios-de-calidad-de-código)
- [6. Excepciones documentadas](#6-excepciones-documentadas)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito

Define qué significa que `GeometriaFactory-Api` está **validado**. Es el **proyecto de código principal del producto** y una de sus dos unidades de entrega, de modo que acá «validado» quiere decir **que el servicio puede atender a la pieza pública sin perder ninguna decisión tomada adentro y sin exponer nada de lo que la topología protege**.

Los momentos en que se aplican estos criterios son el **punto de control de cada etapa**, que el intake §15 declara bloqueante, y el **momento en que el artefacto se construye y arranca**. **No incluyen el despliegue**: el intake §17.5.P.8 lo declara manual y del Product Owner.

**Un criterio de este documento se cumple o no se cumple; no hay cumplimiento parcial.** Cuando uno no se cumple, la salida es la de §6 y nunca el silencio.

## 2. Criterios funcionales

| Id | Criterio | Cómo se comprueba | Umbral |
| --- | --- | --- | --- |
| CV-01 | Los **doce** casos de uso tienen al menos un caso de verificación pasado, y cada criterio Given-When-Then de sus historias está cubierto | [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §2 | **12 de 12** |
| CV-02 | **Los quince puntos de acceso están ejercidos**, que es lo que `Rules-Calidad-Y-Pruebas.md` §2.2 exige para el tipo `rest-api` | Matriz §5 | **15 de 15** |
| CV-03 | Exactamente **4** puntos quedan fuera de la guardia y **11** bajo ella, verificado **en las dos direcciones** | `TC-00007` | **4 + 11 = 15**, sin gradación |
| CV-04 | Las **treinta** historias de usuario tienen su caso de verificación | Matriz §2, columna de historias | **30 de 30** |
| CV-05 | Las **dieciséis** reglas de negocio tienen verificado el tramo que esta capa ejerce; las **tres** sin tramo tienen verificado que **esta capa no deshaga lo que otra decidió** | Matriz §4 | **16 de 16**, con **13** con tramo y **3** sin él |
| CV-06 | Los **nueve** invariantes tienen verificado lo que esta capa aporta a cada uno | Matriz §6 | **9 de 9** |
| CV-07 | **16 de 17** códigos del contrato tienen traducción declarada y **1** está declarado **sin destino con su motivo**; hay **0** inventados y **0** renombrados | `TC-00024` y `TC-00027`, en las dos direcciones | **14 + 1 = 15**, con **0** y **0** |
| CV-08 | Las **tres** familias empobrecidas dan respuestas **indistinguibles en cuerpo y en código** | `TC-00025` | **3 de 3**, sin gradación |
| CV-09 | Los **ocho** escenarios del intake §20 están ejercitados **como cuerpo de petición**, sin sustituirlos por datos sintéticos | `TC-00017`, `TC-00019`, `TC-00022`, `TC-00035` y la batería del validador que corre desde acá | **8 de 8** |
| CV-10 | Un envío cuyo texto **no verifica** responde con **éxito** y no con un código de fallo | `TC-00017`, con `E-5` y `E-8` | **3 de 3** envíos exitosos con estados distintos |

## 3. Criterios no funcionales

Uno por cada NFR de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §8. Los cinco primeros llevan su rótulo **[ASUNCIÓN]** porque así vienen del intake.

| Id | Criterio | Umbral | Cómo se mide | Carácter |
| --- | --- | --- | --- | --- |
| CV-11 | Latencia del listado, medida **en el servidor** | **Percentil 99 por debajo de 500 ms** **[ASUNCIÓN, `A-5`]** | `TC-00034` | **Condicionado** |
| CV-12 | Caudal sostenido | **20 peticiones por minuto** **[ASUNCIÓN, `A-5`]** | `TC-00034` | **Condicionado** |
| CV-13 | Arranque en frío: aplica transformaciones y responde salud | **Menos de 30 segundos** **[ASUNCIÓN, `A-5`]** | `TC-00033` | **Condicionado** |
| CV-14 | Cobertura del proyecto de código, **por componente y no como número global** | **75 %** de líneas y **70 %** de ramas **[ASUNCIÓN, `A-3`]** | Informe de cobertura por componente | **Condicionado** |
| CV-15 | Forma de la pirámide de pruebas | **60 %** integración y **40 %** unitarias **[ASUNCIÓN en cuanto al reparto]** | `TC-00037` | **Condicionado.** **La inversión no es asunción** y no queda en suspenso |
| CV-16 | Puntos de acceso fuera de la guardia | **4** sobre **15**, ni uno más | `TC-00007` | **Bloqueante, sin gradación** |
| CV-17 | Puntos que fijan una contraseña sobre una cuenta existente sin credencial | **0** | `TC-00010` | **Bloqueante** |
| CV-18 | Códigos del contrato con traducción declarada, en las dos direcciones | 16 con destino, 1 sin él, 0 inventados, 0 renombrados | `TC-00024`, `TC-00027` | **Bloqueante** |
| CV-19 | Respuestas indistinguibles de las tres familias empobrecidas | **3 de 3** | `TC-00025` | **Bloqueante, sin gradación** |
| CV-20 | Respuestas que exponen dirección, ruta, secreto o traza, sobre los quince puntos **y** sobre el registro del servidor | **0** | `TC-00026` | **Bloqueante** |
| CV-21 | Configuraciones de intercambio declaradas en el producto | **1**, compartida por los dos extremos | `TC-00029` | **Bloqueante** |
| CV-22 | Caracteres de diferencia entre el texto enviado y el guardado, y truncamientos silenciosos | **0** y **0** | `TC-00019` | **Bloqueante, sin gradación** |
| CV-23 | Puertos conectados a su adaptador | **4 de 4**, con fallo **en construcción** si falta alguno | `TC-00028` | **Bloqueante** |
| CV-24 | Peticiones atendidas con la preparación del almacén incompleta | **0** | `TC-00031` | **Bloqueante** |
| CV-25 | Eliminaciones fuera de alcance aceptadas **al forzar la petición** | **0** | `TC-00020` | **Bloqueante.** Es el **único** criterio del producto que la fuente exige ejercer forzando la petición contra esta superficie |
| CV-26 | Advertencias de construcción | **0** | Etapa `build`; intake §17.5.P.8 | **Bloqueante** |
| CV-27 | Pasos de la colección de peticiones reproducible, y datos de prueba inventados en ella | **5 o menos**, y **0** | `TC-00035` | **Bloqueante al cierre de la etapa que la incorpora** |

**No hay criterio de disponibilidad, y es correcto que no lo haya.** El intake declara «sin SLO»: el servidor es domiciliario y su caída se responde con **estado degradado en el front**, no con redundancia.

**No se declara ningún tiempo de ejecución de la batería.** Los tres tiempos de este proyecto de código —`CV-11`, `CV-12` y `CV-13`— son **del servicio** y vienen del intake con su rótulo.

## 4. Criterios de regresión

| Id | Criterio | Umbral |
| --- | --- | --- |
| CV-28 | La batería completa —unitaria y de integración— se ejecuta entera al cerrar cada etapa | 100 % de los `TC-XX` escritos hasta ese momento |
| CV-29 | **Ningún caso de verificación que pasaba en la etapa anterior deja de pasar** sin justificación escrita | 0 regresiones sin justificar |
| CV-30 | **`TC-00007` se ejecuta en todas las etapas que agregan un punto de acceso**, y `TC-00025` y `TC-00026` en todas las que agregan una respuesta de fallo | Presentes en cada una. Son los tres cuyo resultado **cambia al crecer la superficie** |
| CV-31 | **La batería del validador que corre desde acá pasa entera** en toda etapa posterior a la `f` | **10 de 10** en cada ejecución. Ver §6 sobre el recuento |
| CV-32 | Todo defecto cerrado generó al menos un `TC-XX` nuevo o extendió uno existente | 1 caso por defecto cerrado, como mínimo |

**La regla de no regresión es acumulativa por diseño.** El intake §15, regla de delivery 1, la declara: al cerrar cada etapa deben seguir pasando los guiones de todas las anteriores, **sin correcciones**.

## 5. Criterios de calidad de código

| Id | Criterio | Umbral | Carácter |
| --- | --- | --- | --- |
| CV-33 | Cobertura por componente cumplida, con los **ocho** reportados por separado | Tabla de [`Estrategia-Testing.md`](Estrategia-Testing.md) §2 | **Condicionado**, por depender de `CV-14` |
| CV-34 | Mutation score | **60 %**, piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija **para el tipo `library`**; la fila `rest-api`, que es la de este proyecto de código, **no pide mutation score**. **Ninguna fuente del producto lo declara.** Se adopta igual, con más exigencia que la que la guía pide | **No exigible todavía**: la herramienta no está elegida ni corre. **La composición de raíz queda exenta** con su fundamento |
| CV-35 | El análisis estático no introduce advertencias nuevas | 0 advertencias nuevas | **Bloqueante**, por `CV-26` |
| CV-36 | Ningún caso de verificación está deshabilitado sin motivo escrito en su fila | 0 deshabilitados sin motivo | **Bloqueante** |
| CV-37 | Ninguna prueba de la batería de integración usa dobles: golpea **la superficie real contra el almacén real** | 0 dobles en integración | **Bloqueante**. Doblar algo ahí la convierte en otra cosa |
| CV-38 | Ninguna prueba usa el almacén de desarrollo ni el de producción: cada una **crea y descarta el suyo** | 0 usos del almacén compartido | **Bloqueante** |
| CV-39 | Ninguna prueba deja un secreto real en el repositorio: la clave de firma de prueba es **evidentemente ficticia** y llega por configuración | 0 secretos reales | **Bloqueante** |
| CV-40 | Los casos de verificación citan los puntos por su identificador `A-XX` **y no por su ruta**, mientras la forma de las rutas siga validándose en el punto de control de la etapa `a` | 0 citas por ruta | **Bloqueante hasta el cierre de la etapa `a`** |

## 6. Excepciones documentadas

**Un criterio no cumplido no se acepta en silencio.** Las cuatro únicas salidas admitidas:

| Situación | Salida admitida | Quién la aprueba |
| --- | --- | --- |
| Criterio **condicionado** —`CV-11` a `CV-15`, `CV-33`— no alcanzado | Se registra la medición y su distancia al umbral en el informe de cierre, y **no bloquea**, porque el umbral es un valor rotulado [ASUNCIÓN] sin confirmar | Nadie: es el tratamiento declarado, no una excepción concedida |
| Criterio **no exigible todavía** —`CV-34`— | Se reporta «sin medir» con el hueco citado. **No se reporta un número inventado** | — |
| **Puerta técnica que no pasa** —`PT-04`— | **No hay excepción.** El intake §15 declara que detiene la planificación de las etapas que dependen de ella y **no se arrastra como deuda** | El Product Owner decide la salida, no la excepción |
| Criterio **bloqueante** no cumplido | Se abre una tarea técnica en [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md) con la remediación, y la etapa **no cierra** hasta que se cumpla o hasta que el Product Owner acepte la excepción por escrito | El Product Owner, con constancia escrita |

**Sobre `CV-31` y el recuento de la batería del validador.** El intake **1.20** escribe «incluidas las **diez** pruebas del validador» en §17.5.P.8 —y «las **diez** pruebas del validador pasan» en §17.3.P.8—, y su §21 tiene **diez** filas, la décima incorporada con `E-8` bajo el rótulo **[DECISIÓN 2026-08-09]**. **Hasta 1.19 los dos gates escribían nueve**; la Fase C de `GeometriaFactory-Infrastructure` ya había resuelto la lectura en **diez**, este documento la heredó, y la fuente lo confirmó en 1.20. **Cerrar la etapa con nueve casos no es una excepción admitida.**

**Lo que tampoco es una excepción admitida:** agregar un punto de acceso sin declarar si queda dentro de la guardia; enriquecer una respuesta de una familia empobrecida «para que sea más útil»; truncar un cuerpo en lugar de rechazarlo; dar por verificada la eliminación fuera de alcance **sin forzar la petición**; declarar cumplido un NFR de umbral cero por no haber observado lo contrario; o dejar un secreto real en una prueba.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** La nota de §6 sobre `CV-31` afirmaba en presente que el intake escribe «nueve» en §17.3.P.8 y §17.5.P.8; el intake **1.20** dice **diez** en los dos. Reescrita contra el texto vivo, con el nueve ubicado **hasta 1.19**. **`H-08`.** `CV-34` atribuía a `Rules-Calidad-Y-Pruebas.md` §2.2 un piso de mutación que esa tabla fija para el tipo **`library`**: la fila `rest-api`, que es la de este proyecto de código, no pide mutation score. Queda declarado como exigencia adoptada por encima de la guía. Ningún umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara **cuarenta** criterios de validación numerados `CV-01` a `CV-40`, repartidos en funcionales, no funcionales, de regresión y de calidad de código, cada uno con su umbral y su forma de medición. Distingue cuatro caracteres —bloqueante, condicionado, **puerta técnica sin excepción posible** y no exigible— y ata los condicionados a los valores rotulados **[ASUNCIÓN]** del intake §22, precisando que **la inversión de la pirámide no es asunción** aunque su reparto sí lo sea. Incorpora el criterio de **15 de 15 puntos ejercidos**, que es lo que hace verificable la exigencia de la regla para el tipo `rest-api`, y el de **4 + 11 = 15** sobre la guardia. Declara que no hay criterio de disponibilidad y que los tres tiempos son del servicio y no de la suite. Su §6 declara las cuatro salidas admitidas, el tratamiento del recuento de la batería del validador y **seis** situaciones que explícitamente no son excepción. |
| 1.2 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
