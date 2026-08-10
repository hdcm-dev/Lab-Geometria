# 02 · Especificación funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`Especificacion-Funcional.md`](Especificacion-Funcional.md) (índice maestro de esta categoría); `01-Necesidades-Negocio/Necesidades-Negocio.md`; `00-Contexto/Vision-Producto.md`; y las categorías 02 de `GeometriaFactory-Contracts`, `GeometriaFactory-Application` e `GeometriaFactory-Infrastructure`
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Los doce casos de uso](#2-los-doce-casos-de-uso)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Artefactos omitidos y el que se emite](#4-artefactos-omitidos-y-el-que-se-emite)
- [5. Notas de uso de esta sección](#5-notas-de-uso-de-esta-sección)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Especificacion-Funcional.md`](Especificacion-Funcional.md) | Índice maestro: las cinco responsabilidades, la frontera entre lo que se decide y lo que se transporta, el catálogo, la tabla de las quince reglas, la matriz NB → CU → RN → US, el criterio de recorte, las omisiones y los doce puntos abiertos. **Es el punto de entrada** | Propuesto |
| [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) | Documento de concepto central: los dieciséis puntos de acceso, los diez códigos de respuesta, las dos traducciones, la tabla de los diecisiete códigos del contrato, las seis ausencias declaradas y lo que ninguna respuesta puede decir | Propuesto |
| [`Glosario-Funcional.md`](Glosario-Funcional.md) | Vocabulario que esta categoría acuña y los tres términos con más de un referente | Propuesto |
| `Casos-De-Uso/` | Doce casos de uso, uno por archivo | Propuesto |
| `README.md` | Este archivo: índice navegable, orden de lectura y omisiones | Propuesto |

No hay carpeta `_legacy/`: es la emisión inicial de la categoría para este proyecto de código.

## 2. Los doce casos de uso

| CU | Nombre | En una línea |
| --- | --- | --- |
| CU-01 | [Canjear credenciales por un acceso firmado](Casos-De-Uso/CU-01-Canjear-Credenciales-Por-Un-Acceso-Firmado.md) | El único punto cuya ruta declara una fuente, con su respuesta genérica y sus tres motivos |
| CU-02 | [Admitir la petición: acceso, papel y marca](Casos-De-Uso/CU-02-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md) | La guardia de once puntos, cuyo defecto característico es no alcanzar a alguno |
| CU-03 | [Exponer el alta de cuenta y la credencial propia](Casos-De-Uso/CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md) | Los cuatro puntos que se ejercen sin acceso o sin que el papel importe |
| CU-04 | [Exponer el gobierno de las cuentas de la comisión](Casos-De-Uso/CU-04-Exponer-El-Gobierno-De-Las-Cuentas-De-La-Comision.md) | Listado, situación y la única operación destructiva de la superficie |
| CU-05 | [Exponer el reseteo de la contraseña de un alumno](Casos-De-Uso/CU-05-Exponer-El-Reseteo-De-La-Contrasena-De-Un-Alumno.md) | El único punto que devuelve un valor de credencial, y no lo registra |
| CU-06 | [Exponer el envío y la eliminación de un trabajo](Casos-De-Uso/CU-06-Exponer-El-Envio-Y-La-Eliminacion-De-Un-Trabajo.md) | El texto que no se normaliza en el borde, y el envío que no verifica y **responde con éxito** |
| CU-07 | [Exponer el listado y el detalle de los trabajos](Casos-De-Uso/CU-07-Exponer-El-Listado-Y-El-Detalle-De-Los-Trabajos.md) | Los dos puntos de lectura, sin ningún parámetro para pedir de más |
| CU-08 | [Exponer el desenlace de la revisión](Casos-De-Uso/CU-08-Exponer-El-Desenlace-De-La-Revision.md) | La transición irreversible, y la terminalidad sostenida por ausencia |
| CU-09 | [Traducir el motivo del contrato a respuesta de protocolo](Casos-De-Uso/CU-09-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md) | Las dos traducciones, las tres reglas de asignación y los dos huecos declarados |
| CU-10 | [Componer la aplicación y conectar los puertos con sus adaptadores](Casos-De-Uso/CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md) | La factura de que todo lo demás se pruebe con dobles, pagada una sola vez |
| CU-11 | [Arrancar el servicio y dejar el almacén en condiciones](Casos-De-Uso/CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md) | El arranque que se detiene antes que atender sobre un almacén equivocado |
| CU-12 | [Ejercitar la superficie con la colección de peticiones reproducible](Casos-De-Uso/CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md) | La demostración ejecutable, con los ocho escenarios como cuerpo y en tres pasos |

## 3. Orden de lectura sugerido

1. [`Especificacion-Funcional.md`](Especificacion-Funcional.md) §1, §3 y §4: qué es esta capa, qué responsabilidades tiene y **qué decide y qué sólo transporta**. Sin §4, los doce casos de uso se leen como si acá se tomaran decisiones de negocio, que es exactamente lo que no pasa.
2. [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) **entero, y antes que cualquier caso de uso**, con especial atención a su **§2**: qué declara una fuente y qué es derivación de esta categoría. Leer §3 sin §2 hace creer que las dieciséis rutas están decididas, y **quince de ellas no lo están**.
3. **CU-02**, la guardia, antes que los puntos que gobierna. Un punto de acceso leído sin la guardia parece más abierto de lo que es.
4. Los casos de uso de puntos, en el orden del recorrido de la persona: **CU-03** y **CU-01** —el alta y la entrada—, después **CU-06** y **CU-07** —lo que el alumno hace—, después **CU-04**, **CU-05** y **CU-08** —lo que el administrador hace—.
5. **CU-09**, que se lee mejor después de haber visto qué puede fallar en cada punto. Su §10 es donde están los dos huecos del conjunto cerrado.
6. **CU-10** y **CU-11**, en ese orden: primero cómo se construye el proceso y después cómo arranca.
7. **CU-12** al final, que es el recorrido entero convertido en algo ejecutable.
8. [`Glosario-Funcional.md`](Glosario-Funcional.md), en particular §3.1 y §3.2, que resuelven las dos polisemias que más caro salen acá: «acceso» y «código».

## 4. Artefactos omitidos y el que se emite

| Artefacto | Situación |
| --- | --- |
| `Reglas-De-Negocio/RN-XX-<Nombre>.md` | **Omitido.** Las **quince** reglas del producto viven en `GeometriaFactory-Domain`, las quince con archivo propio allá, y acá se **referencian**. §6 del índice maestro declara, regla por regla, dónde se ejerce cada una en esta capa —**trece con tramo, dos sin él y dos que esta capa puede romper hacia afuera sola**— |
| `Modelo-Datos/Modelo-Conceptual.md` y sus `RC-XX` | **Omitidos**, y el motivo merece leerse porque el flag de persistencia de este proyecto de código vale **true**. Vale true porque acá se toma de configuración la ubicación del almacén y se disparan las transformaciones al arrancar, **no porque acá se modele el dato**: el intake lo resume en «delega en `GeometriaFactory.Infrastructure`». El modelo conceptual del producto ya está emitido allá, con sus cinco entidades y sus siete reglas conceptuales, y duplicarlo crearía dos descripciones del mismo dato guardado |
| `Definicion-<Concepto-Central>.md` | **Emitido**, y su concepto central es la **superficie HTTP**: es lo único que este proyecto de código existe hacia afuera, y es donde se decide lo que se puede romper sin que ninguna capa de adentro se entere |
| Sección opcional §17 de los casos de uso | **No se emite**, y se declara en lugar de omitirse en silencio. Los proyectos de código hermanos la llevan porque `Rules-Especificacion-Funcional.md` §4.3 la asigna al tipo `library`; **este proyecto de código es `rest-api`** y esta categoría no se apropia de una asignación que no le corresponde. El contenido equivalente —qué cambio de la superficie es incompatible— **sí está**, y vive en dos lugares: la política de cambios del ensamblado de contratos, que gobierna los tipos, y [`Definicion-Superficie-HTTP.md`](Definicion-Superficie-HTTP.md) §7, que declara las seis ausencias de la superficie y qué las repone |

## 5. Notas de uso de esta sección

- **Los identificadores `CU-XX` son locales a este proyecto de código.** No coinciden con los de `GeometriaFactory-Application`, ni con los de `GeometriaFactory-Contracts`, ni con los de `GeometriaFactory-Infrastructure`. La correspondencia se lee por §3, §7.1 y §7.4 del índice maestro, **nunca por número**.
- **Los `A-XX` no son casos de uso.** Son los dieciséis puntos de acceso, y un caso de uso puede describir más de uno. La correspondencia está en `Definicion-Superficie-HTTP.md` §3.
- **Quince de las dieciséis rutas son propuesta derivada de esta categoría**, rotuladas fila por fila. La única que declara una fuente es la del canje de credenciales. Leerlas como decididas es el error de lectura más probable de esta sección.
- **Esta categoría no agrega ningún código al conjunto cerrado del contrato.** Donde falta uno, el hueco se **declara** y se eleva, y mientras tanto se usa el genérico. Los dos huecos están en `CU-09` §10.
- **Esta categoría no toma decisiones de arquitectura**: las rutas definitivas, los nombres de tipos, la herramienta de configuración, el formato del archivo de la colección y los ADR pertenecen a `05-Arquitectura-Tecnica`; la estrategia de pruebas, a `08-Calidad-Y-Pruebas`; el despliegue, que el intake declara **manual y a cargo del docente**, a `09-Devops`. Lo que acá se declara como «tests previstos» es una previsión, no un plan.
- **Ningún dato de prueba se inventó.** Los escenarios se citan por el identificador del intake —`E-1` a `E-8`— sin renumerar, y es una regla de delivery del producto, no una preferencia de esta categoría.
- **Doce puntos abiertos**, ninguno bloqueante: **ocho propios** y **cuatro** heredados de aguas arriba que no se reabren. **Tres de los ocho son huecos de la superficie que esta categoría encontró y elevó al Product Owner**: cómo se identifica la cuenta al establecer la contraseña del primer ingreso, y los dos caminos para los que el conjunto cerrado de códigos no declara ninguno. Están en §11 del índice maestro.
- **Un residuo de forma de un documento hermano**, anotado para que se absorba y no para corregirlo desde acá: `GeometriaFactory-Infrastructure` §7.2 declara ser una de las **dos** secciones del producto que cubren las nueve necesidades, y con esta emisión son **tres**. Está en §7.2 y en §11 del índice maestro.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de los doce casos de uso y de los tres documentos transversales de la sección; orden de lectura de ocho pasos, **que insiste en leer §2 del documento de concepto central antes que su tabla de rutas**, porque quince de las dieciséis son propuesta derivada; las omisiones con su motivo, incluida la del modelo de datos pese al flag de persistencia en true y la de la sección opcional que la regla asigna a otro tipo; y las notas de uso, con los doce puntos abiertos y los tres huecos elevados al Product Owner. |
