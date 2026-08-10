# Especificación funcional — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Especificacion-Funcional.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `00-Contexto/Alcance-Producto.md`; `01-Necesidades-Negocio/Necesidades-Negocio.md` y las necesidades NB-01 a NB-09; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14**, §17.3 íntegro —en particular §17.3.P.2, §17.3.P.3, §17.3.P.4, §17.3.P.5, §17.3.P.6, §17.3.P.10, §17.3.P.11 y §17.3.P.12—, §4, §4.1 (**las dieciséis reglas**, con **RN-16**), §4.2, §7, §10, §11 (RN-B3, RN-B5), §14 (RA-01 a RA-03), §20 y §21; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** §2, §3 y §5; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/` completo, cuyos **cuatro puertos** este proyecto de código implementa; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/` completo, cuyo modelo materializa
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Alcance funcional de este proyecto de código](#1-alcance-funcional-de-este-proyecto-de-código)
- [2. Documentos de esta categoría](#2-documentos-de-esta-categoría)
- [3. Los cuatro puertos que implementa y los dos mecanismos que provee](#3-los-cuatro-puertos-que-implementa-y-los-dos-mecanismos-que-provee)
- [4. Lo que esta capa hace y lo que no decide](#4-lo-que-esta-capa-hace-y-lo-que-no-decide)
- [5. Catálogo de casos de uso](#5-catálogo-de-casos-de-uso)
- [6. Reglas de negocio que esta capa hace cumplir](#6-reglas-de-negocio-que-esta-capa-hace-cumplir)
- [7. Matriz NB → CU → RN → US](#7-matriz-nb--cu--rn--us)
  - [7.1 Matriz](#71-matriz)
  - [7.2 Cobertura bidireccional](#72-cobertura-bidireccional)
  - [7.3 Historias de usuario previstas](#73-historias-de-usuario-previstas)
- [8. Criterio de recorte aplicado](#8-criterio-de-recorte-aplicado)
- [9. Omisiones declaradas](#9-omisiones-declaradas)
- [10. Numeración y nombres de archivo](#10-numeración-y-nombres-de-archivo)
- [11. Puntos abiertos](#11-puntos-abiertos)
- [12. Control de cambios](#12-control-de-cambios)

---

## 1. Alcance funcional de este proyecto de código

`GeometriaFactory-Infrastructure` es donde el producto **toca el mundo**. Implementa los cuatro puertos que declara `GeometriaFactory-Application`, provee los dos mecanismos de seguridad que las capas de adentro delegaron, y es **el proyecto de código que modela y ejerce la persistencia del producto**: el `PRODUCT-MANIFEST` §5 declara ese flag true acá y también en `GeometriaFactory-Api`, pero aquél **delega en éste** y sólo toma de configuración la ruta del archivo y dispara la aplicación de las transformaciones al arrancar (intake §17.5.P.4). Depende de `GeometriaFactory-Application` y de `GeometriaFactory-Domain`, y **no la referencia nadie más que la composición de raíz de `GeometriaFactory-Api`**. Es el nivel 2 del orden topológico.

Esta especificación tiene la forma de la variante `library` de la categoría: **cada caso de uso describe un contrato de uso de la superficie pública**, no un flujo de pantallas. El actor primario de los **diez** casos de uso es el código que consume la biblioteca; el alumno y el administrador aparecen como sujetos de las reglas, nunca como actores.

Tres rasgos distinguen a esta capa de las dos de adentro, y los tres recorren sus casos de uso:

1. **Acá vive el mecanismo, no la decisión.** Las capas de adentro declararon qué hace falta —guardar, interpretar, derivar, firmar, saber qué hora es— y acá se dice **con qué**. Un caso de uso de esta categoría que decidiera un estado, una autorización o una transición estaría mal ubicado.
2. **Acá está el riesgo del producto.** El intake declara con probabilidad alta y con impacto alto que **el validador se escribe sin leer el análisis**, porque el texto del alumno no es JSON estrictamente válido. Es el único riesgo de negocio cuya mitigación es una batería de pruebas, y esa batería vive acá. Por eso [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) es el documento de concepto central de este proyecto de código.
3. **Acá están las dos piezas sensibles.** La derivación de credenciales y la emisión del acceso firmado. Y una tercera que el resto de las capas delegó explícitamente: la **producción de la contraseña provisoria** —la de la habilitación y la del reseteo, que son la misma y con un solo mecanismo (RN-16)—, que `GeometriaFactory-Application` declara como la única de las dieciséis reglas sin tramo en su capa, y que `GeometriaFactory-Contracts` exige por sus propiedades sin declarar mecanismo.

Lo que **no** está acá, y dónde está: las entidades, los invariantes y las máquinas de estado, en `GeometriaFactory-Domain`; la orquestación, la autorización por pertenencia y la verificación de facultad, en `GeometriaFactory-Application`; los datos que cruzan la frontera del proceso, en `GeometriaFactory-Contracts`; los endpoints, el arranque y la configuración, en `GeometriaFactory-Api`; las páginas y el dibujo, en `GeometriaFactory-Web` y `GeometriaFactory-Visor`.

## 2. Documentos de esta categoría

| Documento | Propósito |
| --- | --- |
| `Especificacion-Funcional.md` | Este archivo: índice maestro, catálogos y matriz de trazabilidad |
| [`Definicion-Contrato-Del-Validador-De-Figuras.md`](Definicion-Contrato-Del-Validador-De-Figuras.md) | Documento de concepto central: las cuatro trampas del formato, las siete garantías, los ocho escenarios y la cobertura de la batería obligatoria |
| [`Modelo-Datos/Modelo-Conceptual.md`](Modelo-Datos/Modelo-Conceptual.md) | Las cinco entidades, sus atributos, las cuatro relaciones y los cuatro conjuntos cerrados |
| `Modelo-Datos/reglas-conceptuales-de-modelo/RC-XX-<Nombre>.md` | Siete reglas conceptuales de modelo, una por archivo |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña, con los términos de más de un referente |
| `Casos-De-Uso/CU-XX-<Nombre>.md` | Diez casos de uso, uno por archivo |
| [`README.md`](README.md) | Índice navegable de la sección, con el orden de lectura y las omisiones |

## 3. Los cuatro puertos que implementa y los dos mecanismos que provee

**Los cuatro puertos son los que `GeometriaFactory-Application` §3 declara**, y esta categoría no los redefine: los implementa. Los nombres de los tres primeros los declara el intake —`IRepositorioTrabajos`, `IValidadorFiguras` e `IRelojDelSistema`—; el cuarto, el **repositorio de cuentas**, **no lleva identificador declarado aguas arriba** y es un punto abierto que esta categoría **no reabre y no resuelve**.

| Puerto | Qué implementa acá | CU |
| --- | --- | --- |
| Repositorio de trabajos | Recuperar, resolver la consulta ya acotada, materializar y ejecutar el retiro | CU-03, CU-04 |
| Repositorio de cuentas | Recuperar, responder las dos preguntas sobre el conjunto, materializar y ejecutar el retiro | CU-05, CU-04 |
| Validación de figuras | Interpretar el texto, reconstruir las piezas y verificar los valores | CU-01, CU-02 |
| Reloj del sistema | Devolver el momento actual | CU-09 |

Y **dos mecanismos que no son puertos de la capa de aplicación** y que por eso conviene distinguir: no los declara nadie como contrato de inversión, sino que los consume la composición de raíz de `GeometriaFactory-Api` y, a través de ella, los casos de uso que los necesitan.

| Mecanismo | Qué provee | CU |
| --- | --- | --- |
| Credenciales | Derivar una contraseña, verificar una credencial y **producir la contraseña provisoria** de la habilitación y del reseteo | CU-06, CU-07 |
| Acceso firmado | Emitir y verificar el acceso, con sus cuatro reclamos | CU-08 |

**Y una responsabilidad que no es ni puerto ni mecanismo**: dejar el almacén en condiciones antes de que el servicio atienda su primera petición (CU-10). La invoca el arranque de `GeometriaFactory-Api` y no la invoca ningún caso de uso.

**El alcance de la unidad de trabajo es el que la capa de aplicación declara**: un caso de uso, una unidad de trabajo. Del lado de acá se expresa como una por operación.

## 4. Lo que esta capa hace y lo que no decide

Es la frontera que hace que el flag de autenticación valga true en este proyecto de código, y la que hay que dejar imposible de confundir, porque **acá están los dos mecanismos que el producto no puede permitirse mal hechos**.

**Enunciado en una línea: esta capa provee el mecanismo y no toma ninguna decisión de negocio.**

| Qué | Vive acá | Vive afuera |
| --- | --- | --- |
| Derivar una contraseña y verificar una credencial contra un valor derivado | **Sí** (CU-06) | — |
| **Producir la contraseña provisoria** de la habilitación y del reseteo, no adivinable y sin repetirse | **Sí** (CU-07). Es la delegación explícita de las tres capas de arriba | — |
| Emitir y verificar el acceso firmado, con su clave fuera del repositorio de código y de la imagen | **Sí** (CU-08) | — |
| Guardar y recuperar, conservando el texto original íntegro | **Sí** (CU-03, CU-05) | — |
| Interpretar el texto del alumno y emitir observaciones con posición y campo | **Sí** (CU-01, CU-02) | — |
| Decidir si una cuenta admite el acceso, y con qué motivo | **No.** Llega resuelto: una cuenta que no admite acceso **no llega a la emisión** | `GeometriaFactory-Domain` y `GeometriaFactory-Application` |
| Comprobar la pertenencia de un trabajo o la facultad de administrador | **No.** Cuando esta capa resuelve una consulta acotada, el recorte **ya venía decidido** | `GeometriaFactory-Application` |
| Decidir el estado del trabajo tras el envío | **No.** Se entrega el conjunto de observaciones y **el dominio resuelve** | `GeometriaFactory-Domain` |
| Comparar el correo escrito como confirmación de una baja | **No.** Llega resuelto | `GeometriaFactory-Application` |
| Traducir un motivo a respuesta de protocolo | **No.** Los códigos de esta capa son valores de enumeraciones cerradas | `GeometriaFactory-Api` |

Cinco precisiones que rigen en toda la categoría:

1. **El traslado del recorte no es una comprobación de autorización.** Que una consulta llegue acotada por dueño o por alcance es una decisión ya tomada afuera; acá se resuelve el pedido tal como viene. Duplicarla en el almacén crearía un segundo lugar donde la regla puede decir otra cosa.
2. **Las restricciones de unicidad del almacén sí son una segunda línea, y eso es deliberado.** La consulta previa del consumidor no es una garantía por sí sola, y `GeometriaFactory-Application` `CU-01` **FA-02** ya declara ese camino como flujo alternativo propio, con el mismo motivo de correo ocupado.
3. **Ninguna condición de error de esta capa deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.
4. **Ningún mensaje de esta capa incluye la dirección de un servicio interno, la ruta del almacén ni la clave de firma.** Es RA-03, que es regla de nivel producto, y su contracara es que **todo error que se muestre queda registrado del lado del servidor**.
5. **De las tres reglas de arquitectura del intake §14, sólo RA-03 tiene tramo acá. RA-01 y RA-02 no lo tienen, y se declara.** **RA-01** —ningún JavaScript del navegador llama a la API— no aplica porque esta capa **no tiene superficie de navegador**, no atiende peticiones y su único consumidor declarado es la composición de raíz de `GeometriaFactory-Api`. **RA-02** —el visor es visualizador puro, sin red, sin configuración y sin identidad— no aplica porque esta capa **no es el visor ni compone su bundle**; su contenido se respeta desde afuera, en la frontera que `Definicion-Contrato-Del-Validador-De-Figuras.md` §8 traza con la fachada y en `CU-01` **CA-11**, que exige **cero peticiones de red** originadas por el contrato del validador. No tener tramo no es incumplirlas: es no tener superficie donde puedan romperse.

## 5. Catálogo de casos de uso

| CU | Nombre | Contrato que describe | Estado |
| --- | --- | --- | --- |
| CU-01 | [Interpretar el texto original y reconstruir las piezas](Casos-De-Uso/CU-01-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) | Lectura tolerante del texto real del alumno, con la cantidad de figuras del conjunto raíz, las piezas con su posición y los errores de validación ubicados | Propuesto |
| CU-02 | [Verificar los valores declarados contra los derivados](Casos-De-Uso/CU-02-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) | La comparación con tolerancia y operador estricto, que señala y no corrige ni rechaza | Propuesto |
| CU-03 | [Guardar y recuperar los trabajos](Casos-De-Uso/CU-03-Guardar-Y-Recuperar-Los-Trabajos.md) | Materialización y consulta ya acotada, con el texto original conservado íntegro | Propuesto |
| CU-04 | [Ejecutar el borrado físico y el arrastre de la baja](Casos-De-Uso/CU-04-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) | La única operación destructiva del producto: todo o nada, sin borrado lógico | Propuesto |
| CU-05 | [Guardar y recuperar las cuentas de la comisión](Casos-De-Uso/CU-05-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) | Las cuentas con su marca, y las dos preguntas sobre el conjunto que ninguna entidad sola responde | Propuesto |
| CU-06 | [Derivar la contraseña y verificar una credencial](Casos-De-Uso/CU-06-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) | El único punto del producto donde la contraseña en claro se convierte en el valor guardado, y el único que la compara | Propuesto |
| CU-07 | [Producir la contraseña provisoria del reseteo](Casos-De-Uso/CU-07-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) | **La delegación explícita de RN-14**: un valor no adivinable y que no se repite | Propuesto |
| CU-08 | [Emitir el acceso firmado](Casos-De-Uso/CU-08-Emitir-El-Acceso-Firmado.md) | Los cuatro reclamos, la firma simétrica y la clave que vive fuera del repositorio de código y de la imagen | Propuesto |
| CU-09 | [Proveer el sello del reloj del sistema](Casos-De-Uso/CU-09-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) | El contrato más corto, y el que explica por qué la capa se puede probar entera con dobles | Propuesto |
| CU-10 | [Preparar el almacén al arrancar](Casos-De-Uso/CU-10-Preparar-El-Almacen-Al-Arrancar.md) | Crear, transformar el esquema y detener el arranque antes que confiar en un almacén equivocado | Propuesto |

**Diez casos de uso, sobre un mínimo de cinco para el tipo `library`.**

## 6. Reglas de negocio que esta capa hace cumplir

**Las reglas del producto viven en `GeometriaFactory-Domain` y acá se referencian, no se redactan.** Lo que esta tabla declara es dónde se ejerce cada una en esta capa.

**Catorce de las dieciséis tienen tramo acá y dos no lo tienen**, y el recuento cierra en dieciséis. **Tres tienen su tramo principal acá** —RN-08, RN-09 y **RN-14**—, y la consecuencia práctica es directa: **si acá se hacen mal, ninguna capa de más adentro puede repararlas**.

| RN | Enunciado en una línea | Dónde se ejerce en esta capa |
| --- | --- | --- |
| [RN-01](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md) | Administrador único y papeles fijos | CU-05: la restricción de unicidad del almacén, que impide el resultado aunque no explique el camino. CU-08 transporta el papel en el acceso, sin decidir qué habilita |
| [RN-02](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md) | El correo del alumno es único | CU-05: la segunda línea de la unicidad, con el motivo que la capa de aplicación ya declara recibir por esta vía |
| [RN-03](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md) | Un alumno sólo ve y opera sus propios trabajos | CU-03, **de forma negativa**: la consulta sin recorte declarado **no se resuelve**. Esta capa no comprueba pertenencia; lo que hace es no ofrecer el camino por el que la regla se rompería |
| [RN-04](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) | El alumno elimina sólo en borrador; el administrador, cualquier trabajo que ve | CU-04, **en su mitad de borrado físico**. La acotación por estado y por papel es de la capa de aplicación |
| [RN-05](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) | Un trabajo no pasa a estado `Pendiente` con errores de validación | CU-01 y CU-02 **producen el insumo**: la especie de cada observación. **El estado lo resuelve el dominio** y esta capa no lo decide |
| [RN-06](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) | Una cuenta `Pendiente` o `Bloqueado` no obtiene acceso | **Sin tramo acá.** La admisibilidad se resuelve antes y una cuenta no admitida **no llega** a CU-08. CU-05 guarda el estado, que es dato y no comprobación |
| [RN-07](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | La baja arrastra los trabajos y exige confirmación escrita | CU-04, **en su mitad de arrastre**, con el todo o nada de la unidad de trabajo. La confirmación escrita es de la capa de aplicación |
| [RN-08](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) | El texto original del alumno se conserva íntegro | **Tramo principal acá.** CU-01 no lo devuelve corregido y CU-03 rechaza toda escritura que lo reemplace (`RC-01`). Es la capa donde el texto se escribe y se conserva, y por lo tanto donde puede perderse |
| [RN-09](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Toda observación de error indica la posición de la pieza y el campo | **Tramo principal acá.** CU-01 produce el mensaje ubicado y reserva la posición de la figura no reconstruida (`RC-02`), y CU-02 emite la advertencia con sus dos valores |
| [RN-10](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md) | El desenlace es exclusivo del administrador y es terminal | **Sin tramo acá.** Esta capa guarda el estado y el comentario; quién puede cambiarlo y desde dónde lo deciden el dominio y la capa de aplicación |
| [RN-11](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) | El administrador no ve los trabajos en borrador | CU-03, **de forma negativa**, igual que RN-03: el predicado de alcance llega en el pedido y el borrador **no viaja** |
| [RN-12](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) | El reseteo conserva la cuenta y sus trabajos, y no es una baja | CU-05, que escribe la marca **sin tocar el estado ni los trabajos**, y CU-04 **por contraste**: el reseteo no pasa por el retiro (`RC-05`, `RC-07`) |
| [RN-13](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Con la provisoria sin cambiar, la cuenta no llega a ninguna otra parte | CU-05: **conserva la marca y la hace viajar**. Sin ese dato, la comprobación transversal de la capa de aplicación no tendría sobre qué decidir. La comprobación **no es de acá** |
| [**RN-14**](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-14-Provisoria-Producida-Por-El-Sistema.md) | La provisoria la produce el sistema: no es adivinable y no se repite | **Tramo principal, y único, acá: CU-07.** `GeometriaFactory-Application` §6 declara que es la única de las dieciséis **sin tramo en su capa**, `GeometriaFactory-Contracts` `CU-08` §10 la exige sin declarar mecanismo, y `RN-14` §3 nombra a este proyecto de código como el lugar de la generación |
| [RN-15](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-15-Reseteo-Independiente-Del-Estado-De-Cuenta.md) | Resetear no exige que la cuenta esté habilitada | CU-07 **de forma estructural**: la invocación **no recibe** el estado de la cuenta, de modo que no puede comprobarlo. Y CU-05, que escribe la marca sobre los tres estados sin alterarlos (`RC-07`) |
| [**RN-16**](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-16-Habilitar-Produce-La-Provisoria.md) | Habilitar una cuenta produce su contraseña provisoria y la deja con cambio de contraseña pendiente | CU-07, que **produce el valor también para la habilitación**: es el mismo mecanismo y el mismo valor que para el reseteo, y la invocación no lleva ningún dato del acto que la motiva, de modo que no puede distinguirlos (`CU-07` §3). Y CU-05, que **escribe la marca** con la credencial derivada provisoria, igual que en el reseteo (`RC-07`). **Quién habilita y cuándo lo decide la capa de aplicación**, no ésta |

## 7. Matriz NB → CU → RN → US

### 7.1 Matriz

| NB | CU de este proyecto de código | RN aplicables | US previstas en 06 |
| --- | --- | --- | --- |
| [NB-01](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) · Control de admisión y de bajas del laboratorio | CU-04, CU-05, CU-07 | RN-01, RN-02, RN-07, RN-12, RN-14, RN-15 | US-12, US-13, US-14, US-15, US-16, US-19, US-20 |
| [NB-02](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) · Identidad propia del alumno sin canal de correo | CU-05, CU-06, CU-07, CU-08 | RN-01, RN-13, RN-14 | US-14, US-17, US-18, US-19, US-20, US-21, US-22 |
| [NB-03](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) · Trabajo con dueño, estado y persistencia | CU-03, CU-04, CU-10 | RN-03, RN-04, RN-08 | US-08, US-09, US-10, US-11, US-12, US-24, US-25 |
| [NB-04](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) · Interpretación fiel del dato del alumno | CU-01 | RN-05, RN-08, RN-09 | US-01, US-02, US-03, US-04 |
| [NB-05](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) · Visibilidad del error de cálculo | CU-02 | RN-05, RN-09 | US-05, US-06, US-07 |
| [NB-06](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md) · Visualización del trabajo dentro del producto | CU-01 (parcial), CU-03 (parcial) | RN-09 | US-03, US-11 |
| [NB-07](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) · Revisión de la comisión desde un solo lugar | CU-03 (parcial) | RN-11 | US-10 |
| [NB-08](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md) · Alcance del laboratorio desde el aula | CU-10 (parcial) | — | US-24, US-25 |
| [NB-09](../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-09-Desenlace-Explicito-De-La-Entrega.md) · Desenlace explícito de la entrega | CU-03 (parcial), CU-04 | RN-04, RN-11 | US-09, US-13 |

### 7.2 Cobertura bidireccional

**De NB a CU. Las nueve necesidades reciben al menos un caso de uso en este proyecto de código.** Es una de las **dos** secciones del producto que lo pueden decir: `GeometriaFactory-Web` también declara la cobertura completa de las nueve en su índice maestro. **En los otros cuatro proyectos de código con documentación emitida hay al menos una necesidad sin caso de uso** —`GeometriaFactory-Domain` y `GeometriaFactory-Application` declaran explícitamente que no tocan NB-08—. No es un mérito: es una consecuencia de que acá viva el mecanismo de todo lo demás.

**Tres de las nueve quedan cubiertas parcialmente**, y conviene que se lea así:

- **NB-06.** Lo que esta capa aporta es la **identidad posicional** de la pieza y la entrega de sus componentes en el detalle, que es el dato con el que después se dibuja y se arma el árbol. El dibujo, el árbol y la sincronización son de `GeometriaFactory-Visor` y de `GeometriaFactory-Web`.
- **NB-07.** Lo que aporta es **resolver la consulta con el recorte ya aplicado**. La agrupación, el orden y el filtro tal como la persona los ejerce son decisiones de presentación de `GeometriaFactory-Web`.
- **NB-08.** Su dolor es de acceso y de despliegue, y esta capa **no atiende peticiones**. Lo único que aporta, y por eso es parcial, es que sus terminaciones degradadas y la detención del arranque de CU-10 **dejan al producto en un estado que la pieza pública puede declarar**, en lugar de servir datos en los que no se puede confiar. Lo demás es de `GeometriaFactory-Api`, `GeometriaFactory-Web` y `09-Devops`.

**De CU a NB. Nueve de los diez casos de uso trazan al menos a una necesidad de negocio, y uno no traza a ninguna**, lo cual se declara en vez de forzarle una:

| CU | NB que implementa |
| --- | --- |
| CU-01 | NB-04, NB-06 (parcial) |
| CU-02 | NB-05 |
| CU-03 | NB-03, NB-06 (parcial), NB-07 (parcial), NB-09 (parcial) |
| CU-04 | NB-01, NB-03, NB-09 |
| CU-05 | NB-01, NB-02 |
| CU-06 | NB-02 |
| CU-07 | NB-01, NB-02 |
| CU-08 | NB-02 |
| **CU-09** | **Ninguna.** Ver abajo |
| CU-10 | NB-03, NB-08 (parcial) |

**CU-09 no traza a ninguna necesidad de negocio, y es correcto que no lo haga.** Es un mecanismo transversal —devolver el momento actual— que ninguna necesidad pide y que existe por una razón de construcción: **que los sellos sean verificables en prueba**. Inventarle una traza sería peor que declarar la ausencia: haría creer que hay una necesidad de negocio detrás de una decisión de testabilidad. Su valor se mide en los casos de uso de la capa de aplicación que lo reemplazan por un doble.

### 7.3 Historias de usuario previstas

La numeración es una **previsión** de esta categoría, y la confirma la categoría 06 al redactarlas.

| US prevista | Contenido | CU de origen |
| --- | --- | --- |
| US-01 | Leer el texto real del alumno con tolerancia a comas finales y a las claves sinónimas | CU-01 |
| US-02 | Devolver la cantidad de figuras del conjunto raíz, incluidas las no reconstruidas | CU-01 |
| US-03 | Reconstruir las piezas con su posición, sus componentes y la posición reservada de las no reconstruidas | CU-01 |
| US-04 | Emitir el error de validación con posición de figura y campo | CU-01 |
| US-05 | Derivar el valor desde las dimensiones y los componentes | CU-02 |
| US-06 | Comparar con tolerancia absoluta y **operador estricto** | CU-02 |
| US-07 | Emitir la advertencia con el valor declarado y el derivado, sin corregir ninguno | CU-02 |
| US-08 | Conservar el texto original literal y rechazar toda escritura que lo reemplace | CU-03 |
| US-09 | Materializar el trabajo con sus piezas, componentes y observaciones en una unidad de trabajo | CU-03 |
| US-10 | Resolver la consulta con el recorte ya trasladado al pedido | CU-03 |
| US-11 | Excluir componentes y texto original del resultado de un listado | CU-03 |
| US-12 | Retirar físicamente un trabajo con todo lo que cuelga de él | CU-04 |
| US-13 | Arrastrar todos los trabajos de una cuenta dada de baja, todo o nada | CU-04 |
| US-14 | Sostener en el almacén la unicidad del correo y la del administrador | CU-05 |
| US-15 | Responder si un correo está registrado y si ya existe una cuenta con papel `Administrador` | CU-05 |
| US-16 | Conservar y transportar la marca de cambio de contraseña pendiente sin alterar el estado | CU-05 |
| US-17 | Derivar una contraseña sin guardarla ni registrarla en claro | CU-06 |
| US-18 | Verificar una credencial y distinguir el valor derivado ilegible de la contraseña equivocada | CU-06 |
| US-19 | Producir una contraseña provisoria no adivinable y sin repetirse | CU-07 |
| US-20 | Terminar sin producir valor cuando la fuente de aleatoriedad no responde | CU-07 |
| US-21 | Emitir el acceso firmado con sus cuatro reclamos | CU-08 |
| US-22 | Rechazar la emisión sin clave de firma, sin generar una al vuelo | CU-08 |
| US-23 | Proveer el sello por un puerto, para que las pruebas lo puedan fijar | CU-09 |
| US-24 | Aplicar las transformaciones de esquema al arrancar, sobre base inexistente | CU-10 |
| US-25 | Detener el arranque en lugar de operar sobre un almacén en el que no se puede confiar | CU-10 |

**Veinticinco historias previstas, US-01 a US-25, sin huecos.**

## 8. Criterio de recorte aplicado

- **Piso y techo.** El mínimo para `library` es de cinco casos de uso; el techo lo da la cobertura de lo que este proyecto de código implementa. Quedaron **diez**, y la causa es directa: cuatro puertos, dos mecanismos de seguridad y una responsabilidad de arranque no caben en menos sin fusionar contratos que se prueban de formas distintas.
- **Particiones.** **La interpretación se separó de la verificación de valores** —CU-01 frente a CU-02— por los mismos criterios con los que el dominio partió su reconstrucción de su registro de observaciones: trazan a necesidades distintas —NB-04 y NB-05—, sus observaciones son de **especies distintas** y esas especies tienen **efectos opuestos** sobre el estado del trabajo. Fusionarlos habría puesto en un solo contrato lo que bloquea y lo que no. **El retiro se separó del guardado** —CU-04 frente a CU-03 y CU-05— porque lo que hay que poder verificar del retiro es que **no queda nada**, y eso no es un caso más de la materialización: es la única operación destructiva del producto. **La producción de la provisoria se separó de la derivación** —CU-07 frente a CU-06— porque son propiedades distintas con pruebas distintas: una se prueba por no reversibilidad y la otra por **no repetición y no derivabilidad**, y porque CU-07 es el destinatario de una delegación explícita que conviene poder citar por su identificador. **La preparación del almacén se separó del guardado** —CU-10 frente a CU-03— porque su forma de terminación es propia: detiene el arranque.
- **Fusiones.** El guardado y la recuperación quedaron juntos en CU-03 para trabajos y en CU-05 para cuentas, porque comparten el almacén, la unidad de trabajo y las condiciones que los gobiernan, y se distinguen sólo por la dirección del dato. **La derivación y la verificación quedaron en CU-06** porque son la misma función mirada desde los dos lados: no se puede verificar sin saber cómo se derivó. **La emisión y la verificación del acceso quedaron en CU-08** por el mismo motivo, y porque las dos dependen de la misma clave.
- **Lo que no se convirtió en caso de uso.** El registro del lado del servidor de los errores que se muestran no recibió contrato propio: es una propiedad transversal que §4 declara una vez y que cada caso de uso ejerce. Tampoco lo recibieron el modo de diario ni el respaldo: el primero es un efecto de CU-10 y el segundo es una operación del docente que ninguna fuente asigna a este proyecto de código.

## 9. Omisiones declaradas

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido** | **Las dieciséis reglas del producto viven en `GeometriaFactory-Domain`**, las dieciséis con archivo propio allá, y son atemporales: redactarlas de nuevo acá crearía dos enunciados de la misma regla en la misma cadena documental. Esta categoría las **referencia** por identificador y con enlace, y §6 declara dónde se ejerce cada una. Es el mismo criterio que `GeometriaFactory-Application` §9 aplica |
| `Modelo-Datos/` | **Emitido, y no omitido** | Es la diferencia con los cinco proyectos de código anteriores y se declara con su fundamento. `GeometriaFactory-Domain` §7 y `GeometriaFactory-Application` §9 omiten estos artefactos con **dos** motivos: que la regla de la categoría los omite para `library`, y que su flag de persistencia es false. **Acá el segundo motivo no se cumple**: es el único `library` del producto con persistencia declarada true en el `PRODUCT-MANIFEST` §5, y el intake declara la persistencia «la responsabilidad central del proyecto de código» (§17.3.P.4). Omitirlos dejaría al producto **sin ningún documento que describa el dato guardado**. Se emiten, por lo tanto, como **apartamiento declarado de la guía del tipo**, con la misma forma con la que `GeometriaFactory-Domain` §6 declaró su apartamiento de la guía de «library con menos de diez». Si el orquestador decidiera que la guía del tipo manda sobre el flag, el contenido no se pierde: se muda al documento de concepto central |
| `_legacy/` | `2026-08-10/` | Conserva el estado **1.0** de los veintiséis documentos que la corrección del rechazo de `B-02-03-GeometriaFactory-Infrastructure-r1.md` llevó a 1.1. La emisión inicial no lo tenía, porque no había nada superado que archivar |

## 10. Numeración y nombres de archivo

1. **Los identificadores `CU-XX` de esta carpeta son locales al proyecto de código.** El `CU-05` de esta categoría no es el `CU-05` de `GeometriaFactory-Application` ni el de `GeometriaFactory-Domain`. La correspondencia se lee por §3 —qué puerto implementa cada uno— y por la matriz de §7.1, **nunca por número**.
2. **La serie es contigua de CU-01 a CU-10**, sin huecos, y su orden es el del recorrido del dato: primero lo que interpreta, después lo que guarda, después lo que protege y al final lo que prepara.
3. **Los identificadores `RC-XX` son propios de esta categoría** y de su carpeta de modelo de datos. **No son reglas de negocio y no compiten con las `RN-XX`**: una regla conceptual de modelo declara cómo el dato sobrevive, no qué decidió el negocio. La serie es contigua de RC-01 a RC-07.
4. **Las `RN-XX` que se citan conservan la numeración del intake y la de `GeometriaFactory-Domain`**, que son la misma. Dos de esos archivos llevan un slug que ya no describe del todo su enunciado y se citan igual por su ruta vigente, por la decisión de estabilidad de citación que ese proyecto de código declaró.
5. **Las `US-XX` de §7.3 son una previsión local** de esta categoría, no la numeración de las que previó `01-Necesidades-Negocio` ni la de los proyectos de código hermanos.
6. **Los `E-X` son los escenarios del intake §20** y se citan con su identificador de origen, sin renumerar. **`T1` a `T4` son las trampas del formato** que el intake declara en §17.3.P.11, y tampoco se renumeran.

## 11. Puntos abiertos

Quince, y ninguno bloqueante. **Nueve son propios de esta categoría y seis vienen declarados de aguas arriba y no se reabren.**

**El que era el primero de esta lista ya no está abierto.** Qué devuelve el validador ante el texto del escenario **E-8** lo resolvió el Product Owner y el `PRODUCT-INTAKE` **1.12** lo lleva a su texto vivo: §20.E-8 «Qué verificar» punto 5 y la fila «Dimensión no legible» de §21. El desenlace del envío es **error, no advertencia**: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-09. El fundamento es que una dimensión ilegible **no es un valor mal calculado sino un valor que no se pudo leer** —la diferencia con las advertencias de E-3 es que allá el sistema entiende lo que el alumno escribió y discrepa del resultado, y acá no lo entiende—, y es además el modo de falla **más probable** de los ocho escenarios, porque lo produce la configuración regional de la máquina y no un error del alumno. El resultado está declarado en `Definicion-Contrato-Del-Validador-De-Figuras.md` §6 y §7 y verificado por `CU-01` **CA-12**.

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| **Hasta dónde llega el conjunto de tipos reconstruibles** | Propio. Los seis que los escenarios ejercitan son los que la pieza que dibuja sabe dibujar; el análisis del que sale el intake menciona siete clases en `Ejemplo1` y diez en `Ejemplo2` y **ninguna fuente las enumera**, de modo que no se puede afirmar si alguna emite un tipo fuera de los seis | Product Owner, con la enumeración de las clases de la Actividad 1 |
| **Cómo se sostiene que la provisoria «no se repite»** | Propio. `CU-07` §10 adopta que la sostiene la impredecibilidad y **descarta** verificarla contra un registro de provisorias anteriores, porque exigiría conservarlas y el producto no guarda contraseñas en claro. **Es una decisión derivada, no una transcripción** | Product Owner, para confirmarla o reemplazarla |
| **Longitud y alfabeto de la contraseña provisoria** | Propio. Ninguna fuente los declara. `CU-07` §10 deja escrita la tensión que hay que resolver —transcribible de viva voz y a la vez lejos de lo adivinable— y **no la resuelve** | `05-Arquitectura-Tecnica` |
| **Vigencia exacta del acceso firmado** | Propio. El intake declara «corta» y «sin acceso de refresco», y no fija un número | `05-Arquitectura-Tecnica`, y Product Owner si quisiera fijarlo |
| **De dónde sale el valor derivado del área de una pieza volumétrica** | Propio. El intake la muestra dos veces como **suma de los componentes** —el cilindro de E-1 y el ortoedro de E-2— y una vez como fórmula —`6·l²` en el cubo de E-3—, y las dos formas coinciden en ese cubo. No hay contradicción declarada, pero tampoco hay una regla enunciada. `CU-02` §10 adopta la suma de componentes y lo declara. Detalle en `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | `05-Arquitectura-Tecnica`, al fijar la tabla de derivación por tipo |
| **Límite de tamaño del texto que se acepta** | Propio. Ninguna fuente lo declara, y el requerimiento no funcional declarado está medido sobre un texto de tres piezas. Un texto arbitrariamente grande no tiene hoy ningún corte declarado. Detalle en `Definicion-Contrato-Del-Validador-De-Figuras.md` §9 | Product Owner, y `05-Arquitectura-Tecnica` |
| **Zona horaria y precisión de los sellos** | Propio. Ninguna fuente las declara, y afectan a cómo se guardan las dos fechas del trabajo y la fecha de alta de la cuenta. Detalle en `Modelo-Datos/Modelo-Conceptual.md` §7, en `CU-09` §10 y en `RC-06` | `05-Arquitectura-Tecnica` |
| **Fecha de última modificación de la cuenta** | Propio. El modelo del dominio **no la declara** y el consumidor no la registra; este modelo no la incorpora por su cuenta. Si el Product Owner la quisiera, entraría por el dominio y no por acá. Detalle en `Modelo-Datos/Modelo-Conceptual.md` §7 | Product Owner |
| **La condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`** | Propio. **Ninguna fuente la enuncia**: la declara esta categoría porque devolver «0 advertencias» ante una verificación pedida sin reconstrucción sería indistinguible de un trabajo verificado sin discrepancias, y convertiría un defecto de orquestación en un resultado creíble. Detalle en `CU-02` §6 y §10, y en `../03-UX-UI-DX/DX-Error-Messages.md` §3.2 | Product Owner, para confirmarla o reemplazarla |
| Cuál función de derivación de clave se ancla, y con qué parámetros | El intake declara «PBKDF2 o Argon2» y no elige. `CU-06` declara la propiedad y no el mecanismo | `05-Arquitectura-Tecnica`, en la primera etapa |
| Identificador del puerto de repositorio de cuentas | Declarado abierto por `GeometriaFactory-Application` §11. Esta categoría **no lo reabre** y lo nombra en lenguaje de dominio | `05-Arquitectura-Tecnica` |
| Nombres de tipos y de espacios de nombres | Declarados abiertos aguas arriba y validados en el punto de control de la primera etapa | `05-Arquitectura-Tecnica` |
| Criterio de comparación de dos correos | Declarado abierto por `GeometriaFactory-Domain` y por `GeometriaFactory-Application`. **Acá se vuelve visible**, porque la restricción de unicidad del almacén lo materializa, y esta categoría **no lo resuelve** | `05-Arquitectura-Tecnica`, junto con la capa que ejerce la verificación |
| Frecuencia del respaldo | El intake la declara explícitamente «a definir por el docente». **No es una omisión de esta categoría**: es una decisión de operación que la fuente dejó abierta, y `Modelo-Datos/Modelo-Conceptual.md` §7 la registra sin resolverla | Product Owner, y `09-Devops` |
| Valores numéricos de los requerimientos no funcionales | Los 200 ms de la interpretación y los 30 segundos del arranque en frío están rotulados como asunción aguas arriba. Se usan como vigentes | Product Owner, y `08-Calidad-Y-Pruebas` |

**Y dos que quedaron resueltos aguas arriba y se registran para que nadie los vuelva a abrir**: los **sellos de tiempo del trabajo**, que el intake incorpora al modelo de datos con rótulo de decisión del Product Owner y que `RC-06` recoge; y **la tolerancia de 0.01 con operador estricto**, que el intake fija con su fundamento y que `CU-02` transcribe sin margen.

## 12. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial de la categoría para este proyecto de código, contra el `PRODUCT-INTAKE` **1.11** y el `PRODUCT-MANIFEST` **1.2**. Índice maestro de **diez** casos de uso, con la tabla de los cuatro puertos que implementa y los dos mecanismos que provee, la frontera entre el mecanismo y la decisión con sus cuatro precisiones, la tabla de las **quince** reglas de negocio referenciadas —**trece con tramo acá, dos sin él, y tres con su tramo principal acá**—, la matriz NB → CU → RN → US con la verificación bidireccional, la declaración de que **las nueve necesidades reciben caso de uso** y de que **CU-09 no traza a ninguna**, las veinticinco historias previstas, el criterio de recorte con sus particiones y fusiones, la omisión de las reglas de negocio y el **apartamiento declarado** por el que sí se emite el modelo de datos, las seis notas de numeración y los **diez puntos abiertos**, cinco propios y cinco heredados sin reabrir. |
| 1.1 | 2026-08-10 | Ronda 2 de auditoría: correcciones de `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` contra el `PRODUCT-INTAKE` **1.12**. **H-01**: §11 retira el punto abierto del desenlace del envío de `E-8` y declara en su lugar la decisión del Product Owner que 1.12 lleva a §20.E-8 punto 5 y a la fila «Dimensión no legible» de §21 —**error**, el trabajo queda en `Borrador` y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige RN-09—, con su fundamento y con la observación de que es el modo de falla más probable de los ocho escenarios. **H-02**: la trazabilidad upstream pasa a citar el `PRODUCT-INTAKE` **1.12**, que archiva 1.11. **H-04**: §11 incorpora los **seis** puntos abiertos que declaraban documentos subordinados y que el índice maestro no recogía —el valor derivado del área de una pieza volumétrica, el límite de tamaño del texto, la zona horaria y precisión de los sellos, la frecuencia del respaldo, la fecha de última modificación de la cuenta y la condición derivada `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`—; con la salida del de `E-8`, el recuento pasa de **diez** a **quince**, nueve propios y seis heredados. **H-06**: §4 gana una quinta precisión que declara **no aplicables RA-01 y RA-02** con su motivo, y deja RA-03 como la única de las tres con tramo acá. |
| 1.2 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**: habilitar una cuenta produce una contraseña provisoria con el mismo mecanismo que el reseteo, de modo que **`CU-07` gana un segundo consumidor** —`GeometriaFactory-Application` `CU-02`, además de `CU-11`— y ningún adaptador se agrega. §3 y §4 amplían las dos líneas que acotaban la producción de la provisoria al reseteo. **Los diez casos de uso, los cuatro adaptadores y los dos mecanismos no cambian de número ni de recorte.** Sube minor. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-02` (P0) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0 en las dos declaraciones vivas de este archivo que el informe no registra, contra `PRODUCT-INTAKE` 1.14.** La **cabecera de trazabilidad** y el tercer punto de **§1** decían **quince** reglas; son **dieciséis**, `RN-01` a `RN-16`, contadas sobre los archivos de `Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`. Las dos pasan a dieciséis, y el punto de §1 deja de atribuir la producción de la provisoria sólo al **reseteo**: desde **RN-16** la habilitación produce la misma provisoria con el mismo mecanismo, que es lo que `CU-07` ya declara en su §3 desde la emisión 1.2. La cabecera pasa a citar el intake **1.14**. **Levanta además un defecto de la misma familia que el informe no registra**: la tabla de correspondencia de **§6** **no tenía fila para `RN-16`** —quince filas, `RN-01` a `RN-15`—, de modo que la regla decimosexta no tenía declarado dónde se ejerce en esta capa pese a que `CU-07` declara desde su emisión 1.2 que la habilitación es su segundo consumidor. **Entra la fila de `RN-16`**, con su tramo en `CU-07` —el mismo mecanismo y el mismo valor que para el reseteo— y en `CU-05`, que escribe la marca; el recuento de la nota pasa a **catorce de las dieciséis con tramo acá y dos sin él**, y las tres con tramo principal acá siguen siendo RN-08, RN-09 y RN-14. Se corrige además la fila de omisión de **§9**, que ya decía «las dieciséis reglas» y a la vez «las **quince** con archivo propio allá», dentro de la misma celda. **Ningún caso de uso, ninguna condición y ningún otro recuento propio de esta categoría cambia.** Sube minor. |
