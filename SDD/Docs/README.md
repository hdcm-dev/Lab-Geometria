# Fábrica de Geometría

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Versión del documento | 2.3 |
| Estado | Aprobado |
| Fecha | 2026-08-30 |
| Stack principal | C# sobre .NET 10 —Blazor Interactive Server en el front, ASP.NET Core en el servicio de datos—, Entity Framework Core sobre SQLite, y TypeScript con webpack en el visor |
| Composición | **2 unidades de entrega** y **7 proyectos de código** (ver §2) |
| Unidad de entrega principal | `GeometriaFactory-Api` |
| Documento | README raíz del producto |
| Refleja | `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **4.0**, derivado del intake **3.0** |

---

## Tabla de contenido

- [1. Identidad del producto](#1-identidad-del-producto)
- [2. Los dos ejes del producto](#2-los-dos-ejes-del-producto)
- [3. Stack y composición](#3-stack-y-composición)
- [4. Mapa de la documentación](#4-mapa-de-la-documentación)
- [5. Flujo de lectura recomendado por rol de intervención](#5-flujo-de-lectura-recomendado-por-rol-de-intervención)
- [6. Cómo contribuir y cómo regenerar la documentación](#6-cómo-contribuir-y-cómo-regenerar-la-documentación)
- [7. Estado actual y roadmap](#7-estado-actual-y-roadmap)
- [8. Lo que todavía no está decidido](#8-lo-que-todavía-no-está-decidido)
- [9. Glosario rápido](#9-glosario-rápido)
- [10. Contacto y responsables](#10-contacto-y-responsables)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Identidad del producto

En la Actividad 1 de la cátedra de Programación 2, el alumno construye una aplicación que modela figuras planas y volumétricas y las describe en un texto. Para ver ese resultado en tres dimensiones existe hoy una página suelta: el alumno copia el texto, lo pega ahí y mira. Esa cadena no tiene identidad, no tiene persistencia y no tiene entrega. El trabajo vive en un portapapeles.

Fábrica de Geometría cierra esa cadena dentro de un solo producto. El alumno se registra sin correo, carga su trabajo, lo envía, lo ve interpretado con sus advertencias y lo mira en tres dimensiones sin salir de la aplicación; el docente habilita cuentas, revisa lo que la comisión entregó y deja su desenlace. Hay un problema de fondo que el producto vuelve visible y que hoy pasa desapercibido: valores calculados que el programa del alumno emite mal en casos concretos y reproducibles, que el producto muestra como par de valor declarado y valor derivado sobre el trabajo del propio alumno.

La audiencia son dos personas concretas del aula —el alumno de la comisión y el docente— y no hay integradores externos, no hay áreas de auditoría y no hay clientes de terceros. Esa audiencia acotada explica buena parte de las decisiones técnicas del producto: una sola instancia, un solo curso, un solo administrador, sin versionado de rutas y sin escalera de ambientes.

El detalle vive en [`00-Contexto/Vision-Producto.md`](00-Contexto/Vision-Producto.md).

## 2. Los dos ejes del producto

Refleja [`PRODUCT-MANIFEST-Fabrica-De-Geometria.md`](../Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md) **2.2** §2.A, §2.B y §2.C, sin divergencias.

**El producto tiene dos ejes que no coinciden**: el de **entrega**, que dice qué se despliega, y el de **construcción**, que dice qué se compila. Confundirlos es el defecto que el modelo de dos ejes existe para hacer imposible.

### 2.1 Eje de entrega — las dos unidades

| Unidad de entrega | `tipo_unidad_entrega` (D8) | Rol | Integra con (runtime) | `redistribuible` |
| --- | --- | --- | --- | --- |
| `GeometriaFactory-Api` (principal) | `rest-api` | Host en el servidor propio: puntos de acceso, credencial firmada y preparación del almacén al arrancar. Sostiene el dato, las reglas y la única base de datos | — | false |
| `GeometriaFactory-Web` | `web-monolith` | Front en el hosting público; único punto de contacto del navegador | `GeometriaFactory-Api`, por HTTP con credencial firmada, servidor a servidor | false |

**El valor D8 y `redistribuible` son atributos de la unidad de entrega y de nada más.** Un proyecto de código no se entrega: se compila.

### 2.2 Eje de construcción — los siete proyectos de código

| Proyecto de código | Identidad de código | Rol en la arquitectura | Dependencias de compilación | Compone |
| --- | --- | --- | --- | --- |
| `GeometriaFactory-Api` | `GeometriaFactory.Api` | Host REST: puntos de acceso, autenticación y composición de raíz | `Application`, `Infrastructure`, `Contracts` | `GeometriaFactory-Api` |
| `GeometriaFactory-Application` | `GeometriaFactory.Application` | Casos de uso y los cuatro puertos | `Domain` | `GeometriaFactory-Api` |
| `GeometriaFactory-Domain` | `GeometriaFactory.Domain` | Entidades e invariantes; centro de la regla de dependencias | — | `GeometriaFactory-Api` |
| `GeometriaFactory-Infrastructure` | `GeometriaFactory.Infrastructure` | Adaptadores de los cuatro puertos, seguridad y validador de figuras | `Application`, `Domain` | `GeometriaFactory-Api` |
| `GeometriaFactory-Contracts` | `GeometriaFactory.Contracts` | Tipos de transferencia. **Único proyecto compartido** | — | `GeometriaFactory-Api` y `GeometriaFactory-Web` |
| `GeometriaFactory-Web` | `GeometriaFactory.Web` | Front: páginas y componentes. Hoja del grafo | `Contracts`, `Visor` | `GeometriaFactory-Web` |
| `GeometriaFactory-Visor` | `geometriafactory-visor` | Bundle del visor 3D; visualizador puro | — | `GeometriaFactory-Web` |

**Seis de los siete se construyen con un solo comando**, en `GeometriaFactory.sln`. `GeometriaFactory-Visor` **no tiene solución de código**: es un proyecto Node independiente, y por eso su identidad va en minúscula con guiones y su carpeta está en la raíz y no bajo `src/`, para que las dos cadenas de herramientas no compartan raíz.

**La arista `Web → Api` vive en el grafo de integración y no en el de compilación.** Es la distinción que los dos ejes hacen visible: el front llama al servicio de datos en tiempo de ejecución y no lo referencia al compilar.

**Dos unidades desplegables.** El front vive en el hosting público y el servicio de datos en el servidor propio, y esa partición responde a una restricción de red declarada en el intake §14, no a una preferencia de estilo. Las tres reglas de arquitectura de nivel producto se derivan de ahí: **RA-01**, ningún guion del navegador invoca el servicio de datos; **RA-02**, el bundle del visor es un visualizador puro, sin red, sin configuración y sin identidad; **RA-03**, todo lo que el navegador obtiene del backend pasa por el front, y ningún mensaje expone la dirección de un servicio interno.

El mapa completo, con el grafo, los contratos que cruzan fronteras y los riesgos de integración, está en [`Producto/Vista-Producto.md`](Producto/Vista-Producto.md).

## 3. Stack y composición

| Proyecto de código | Stack | Plataforma de ejecución |
| --- | --- | --- |
| `GeometriaFactory-Api` | ASP.NET Core sobre .NET 10, con credencial firmada | Linux en contenedor, en el servidor propio |
| `GeometriaFactory-Web` | ASP.NET Core sobre .NET 10 con Blazor Interactive Server y MudBlazor | Hosting público gratuito, con dominio y transporte seguro |
| `GeometriaFactory-Domain` | C# sobre .NET 10, sin dependencias | Linux, embebido en sus consumidores |
| `GeometriaFactory-Application` | C# sobre .NET 10, dependencia única del dominio | Linux, embebido en sus consumidores |
| `GeometriaFactory-Infrastructure` | C# sobre .NET 10, con Entity Framework Core sobre SQLite | Linux, embebido en el servicio de datos |
| `GeometriaFactory-Contracts` | C# sobre .NET 10, tipos de datos sin dependencias | Se carga en los **dos** procesos desplegables |
| `GeometriaFactory-Visor` | TypeScript transpilado con webpack; el motor de dibujo entra en el bundle y no por red de distribución | El navegador, con capacidad gráfica tridimensional requerida |

**La versión de plataforma del front está marcada para verificar** contra lo que soporte el hosting gratuito, y esa verificación es la puerta técnica `PT-01.a`. La matriz completa, con capacidades del navegador y restricciones justificadas, está en [`00-Contexto/Compatibilidad-Plataformas.md`](00-Contexto/Compatibilidad-Plataformas.md).

## 4. Mapa de la documentación

| Sección | Propósito | Responsable | Enlace |
| --- | --- | --- | --- |
| 00-Contexto (producto) | Visión, alcance, roadmap y compatibilidad de plataformas | AG-00000 | [00-Contexto](00-Contexto/) |
| 01-Necesidades-Negocio (producto) | Las nueve necesidades de negocio, `NB-00001` a `NB-00009` | AG-00010 | [01-Necesidades-Negocio](01-Necesidades-Negocio/) |
| Producto (producto) | Vista de producto, pipeline de producto y plan documental | AG-00050, AG-00090, AG-00110 | [Producto](Producto/) |
| Audit (producto) | Informes de auditoría independiente, uno por fase | Auditor independiente | [Audit](Audit/) |
| Unidades-Entrega/GeometriaFactory-Api | Documentación 02 a 11 de la unidad de entrega del servicio de datos, con los proyectos de código que la componen | AG-00020 a AG-00110 | [GeometriaFactory-Api](Unidades-Entrega/GeometriaFactory-Api/) |
| Unidades-Entrega/GeometriaFactory-Web | Documentación 02 a 11 de la unidad de entrega del front, con los proyectos de código que la componen | AG-00020 a AG-00110 | [GeometriaFactory-Web](Unidades-Entrega/GeometriaFactory-Web/) |

**La columna «Responsable» cita los roles con la forma vigente desde la migración 10.0 → 13.3**
(framework **12.0**), que hizo cumplir a la familia `AG` el ancho de cinco dígitos de
`Root-Rules.md` §9.2. El mapeo es el que el `CHANGELOG` del framework declara —`AG-00` → `AG-00000`,
`AG-01` → `AG-00010`, y así las catorce— y **se lee al revés para reconocer la forma anterior**.

**Los dos rangos se reescribieron y no se enumeraron**, que es una decisión y no un automatismo:
`AG-02 a AG-11` pasa a `AG-00020 a AG-00110`. **Son once y no diez**, y conviene decir cuál es el
undécimo: además de los diez titulares de categoría —`02` a `11`— el intervalo contiene **`AG-00031`**,
el subagente de fase que la 12.0 mapea desde `AG-03M`. **El rango nuevo lo dice mejor que el viejo**:
`AG-00031` está inequívocamente dentro de `AG-00020 a AG-00110`, y no estaba inequívocamente dentro de
`AG-02 a AG-11`. Enumerar los once habría cambiado lo que la fila dice
—de un rango a una lista— sobre un conjunto que no cambió, y el mapeo de la 12.0 es token a token.

**Y una ocurrencia de la forma anterior que queda deliberadamente sin tocar**: la fila **1.0** de §11
cita `AG-ROOT` como autor de aquella emisión. **Es registro histórico**: reescribirla haría decir a
una emisión del 2026-08-11 algo que no dijo, y es el mismo criterio por el que los informes de
`Audit/` están fuera del alcance de la migración. Se declara acá para que no se lea como un residuo
que el barrido no vio.

**El árbol cuelga de la unidad de entrega y no del proyecto de código** desde la migración 6.0 → 8.6 (framework 8.0). Los **siete** proyectos de código de §2 **no tienen árbol documental propio**: su contenido se consolidó dentro de la unidad de entrega que componen —`Domain`, `Application`, `Infrastructure` y `Contracts` en `GeometriaFactory-Api`; `Visor` en `GeometriaFactory-Web`—, con una subsección por proyecto de código dentro de cada documento. El inventario del eje de construcción vive en [`Producto/Vista-Producto.md`](Producto/Vista-Producto.md).

**La cadena de especificación se lee en este orden**: visión y alcance en 00, necesidades de negocio en 01, casos de uso y reglas en 02, experiencia de uso en 03, arquitectura y decisiones en 05, historias y tareas en 06, plan de trabajo en 07, calidad y pruebas en 08, canalización y publicación en 09, ejemplos verificables en 10, y cuerpo documental de entrega en 11. La categoría 04 no existe en ningún proyecto de código: ninguno usa modelos de lenguaje y su omisión está declarada en el manifiesto.

## 5. Flujo de lectura recomendado por rol de intervención

| Rol | Orden recomendado | Por qué |
| --- | --- | --- |
| Product Owner, que acá es el docente | 00 → 01 → 06 → 07 | Necesita ver la visión, las nueve necesidades de negocio y qué se comprometió para cada etapa antes de aprobar un punto de control |
| Desarrollador que retoma el producto | 00 → `Producto/Vista-Producto.md` → 02 → 05 → 10 → 11 | Necesita el mapa del producto entero antes de entrar por un proyecto de código, porque lo que le impone el resto no está escrito en el proyecto de código por el que entra |
| Auditor o QA | 02 → 08 → `Audit/` | Necesita los criterios verificables y la matriz de cobertura, y después el informe que ya los revisó, para no repetir lo verificado |
| Operador, cuando haya sistema que operar | `Producto/Pipeline-Producto.md` → 09 del proyecto de código que despliega → 11 | Necesita el orden de construcción y de salida antes que el procedimiento de una unidad suelta; el despliegue de este producto es conjunto |

**Cuatro roles y una advertencia común.** Ningún rol debería empezar por la categoría de un proyecto de código suelto. Este producto tiene siete proyectos de código que se condicionan entre sí, y la mayoría de los defectos que su propia auditoría registró nacieron de leer una parte y afirmar algo del todo.

## 6. Cómo contribuir y cómo regenerar la documentación

Esta documentación no se escribe a mano: la generan los subagentes del framework SDD, por fases, y cada fase se cierra con una auditoría independiente que no participó de su generación. La regeneración parte del intake y del manifiesto derivado, y respeta la detención obligatoria entre fases: no arranca la siguiente sin que la anterior haya devuelto aprobado.

Quien intervenga sobre el corpus tiene tres obligaciones que la auditoría de este producto verificó una y otra vez, y que conviene enunciar acá porque son las que más se rompieron: **abrir la fuente original antes de citarla**, nunca a través de otro documento; **contar sobre el instrumento** cada recuento que se afirme, en lugar de heredar el número de otro documento; y **declarar lo que no está decidido** en vez de resolverlo por conveniencia.

**Tres archivos satélite no se emiten, y se declara por qué en lugar de dejar el hueco.** `Root-Rules.md` §2.1 los pide cuando la unidad de entrega necesita comunicarse con integradores externos al equipo. Este producto no los tiene: el intake declara que la audiencia son dos personas del aula, **ninguna de las dos unidades de entrega es redistribuible** y no hay feed de paquetes.

| Archivo satélite | Estado | Fundamento |
| --- | --- | --- |
| `CHANGELOG.md` | No se emite | El repositorio ya lleva su bitácora de cambios en la raíz del código, declarada en el intake §16. Un segundo archivo acá sería una segunda fuente de verdad sobre lo mismo |
| `CONTRIBUTING.md` | No se emite | No hay aportes externos que guiar: `equipo_n` es 1. Lo que un contribuyente necesitaría vive en la `Guia-Contribucion` de cada proyecto de código, planificada en la categoría 11 |
| `LICENSE.md` | No se emite | Ninguna fuente declara licencia. Elegir una acá sería una decisión de producto tomada por un índice, y este documento no decide nada |

**La omisión de `CHANGELOG.md` queda registrada como apartamiento**, porque la regla lo declara obligatorio para el tipo `rest-api` de la unidad de entrega principal. El fundamento está arriba; la decisión de aceptarlo o revertirlo es del Product Owner.

## 7. Estado actual y roadmap

El producto está **especificado y en construcción**. La documentación está emitida y auditada por categoría, y el código va por la **etapa `e`** de las nueve del roadmap.

### 7.1 Documentación

| Categoría | Ámbito | Estado |
| --- | --- | --- |
| 00-Contexto | Producto | Aprobado |
| 01-Necesidades-Negocio | Producto | Aprobado |
| 02-Especificacion-Funcional | Las dos unidades de entrega | Aprobado |
| 03-UX-UI-DX | Las dos | Aprobado; validación visual de maqueta cerrada en `GeometriaFactory-Web` |
| 04-Prompts-AI | — | **Omitida por gating**: ninguna unidad usa modelos de lenguaje |
| 05-Arquitectura-Tecnica | Las dos | Aprobado |
| 06-Backlog-Tecnico | Las dos | Aprobado |
| 07-Plan-Sprint | Las dos | Aprobado |
| 08-Calidad-Y-Pruebas | Las dos | Aprobado |
| 09-Devops | Las dos | Aprobado |
| 10-Examples | Las dos | Aprobado, pasada de diseño |
| 11-Documentacion | Producto y las dos | **Planificado** |
| Vista y pipeline de producto | Producto | Aprobado |

**El árbol atravesó seis migraciones normativas cerradas** —6.0 → 8.6, 8.6 → 8.11, 8.11 → 9.9, 9.9 → 9.10, 9.10 → 9.12 y 9.12 → 10.0—, **y la séptima, 10.0 → 13.3, está en curso** ([`Audit/Plan-Migracion-10.0-a-13.3.md`](Audit/Plan-Migracion-10.0-a-13.3.md) 1.2). Sus informes están en [`Audit/`](Audit/) y la procedencia vigente la declara el manifiesto §1.1.

### 7.2 Construcción

| Etapa | Qué entregó | Estado |
| --- | --- | --- |
| `a` · Andamiaje | Esqueleto ambulante de las dos piezas desplegables, con PT-01 y PT-04 medidas | **Cerrada** |
| `b` · Cáscara de la pieza pública | Las once superficies alcanzables y el sistema visual portado | **Cerrada** |
| `c` · Administrador: alta inicial y sesión | Identidad del administrador, sesión y cambio de contraseña, persistidos | **Cerrada** |
| `d` · Alumno: ciclo de vida de la cuenta | Registro, habilitación con provisoria, primer ingreso y reseteo de credencial | **Cerrada** |
| `e` · Alta de trabajo y vista de trabajos | Alta, listado, reedición y eliminación de trabajos, y el listado de la comisión | **Cerrada** |
| `f` · Importación y validación | Interpretación del texto del alumno, la batería obligatoria de **diez** casos y el envío como única acción de guardado | **Cerrada** |
| `g` · Visualización 3D | El dibujo de las piezas reconstruidas y el árbol del texto | **Cerrada** |
| `h` · Circuito de revisión del administrador | Aprobación y rechazo con su comentario, y el desenlace visible para el alumno | **Cerrada** |
| `i` · Despliegue real | **Su puerta está escrita y la fase no ocurrió**: `scripts/verify-stage-i.sh` con sus siete criterios, y `Audit/Medicion-PT-05.md` en `SIN MEDIR` | **Planificada, no ejecutada** |

**El registro de cambios del código es [`../../changelog.md`](../../changelog.md)** y es la única fuente del avance de construcción: este README publica su resultado y no lo replica.

### 7.3 Magnitudes

**Este README dejó de replicar las magnitudes del producto**, y es deliberado: las que publicaba eran las anteriores a la consolidación de la migración —71 casos de uso, cuando la unidad `Api` tiene **nueve** y la `Web` **diez**— y quedaron afirmando durante días un recuento que ninguna fuente sostenía. Un índice que copia cifras de otro documento **las hereda viejas sin avisar**.

Las magnitudes vivas, cada una contada sobre su instrumento, están en [`Producto/Vista-Producto.md`](Producto/Vista-Producto.md); la composición, en el manifiesto §2; y el detalle por etapa, con sus hitos internos y demostrables, en [`00-Contexto/Roadmap-Producto.md`](00-Contexto/Roadmap-Producto.md), que es la única fuente del roadmap.

## 8. Lo que todavía no está decidido

Un producto que se entrega declarando lo que no está decidido vale más que uno que aparenta estar completo. Lo que sigue es el resumen de nivel producto; el detalle por proyecto de código vive en la sección de puntos abiertos de cada categoría 05 y 06.

| Punto abierto | Titular | Dónde está declarado |
| --- | --- | --- |
| Cuántas aristas de compilación tiene el producto: el manifiesto declara ocho en §2, dibuja siete en §3 y valida siete en §4 | Product Owner | [`Producto/Vista-Producto.md`](Producto/Vista-Producto.md) §3.1. **Evento de cierre:** **punto de control de la fase `i`**, que `PRODUCT-INTAKE` §10 y §15 declaran bloqueante y es el último que queda |
| ~~El **rechazo** del informe de auditoría de Fase B de `GeometriaFactory-Api`, emitido el 2026-08-11 con **diecisiete** hallazgos de recuento y de cita: falta la ronda 2 que lo levante.~~ **CERRADO el 2026-08-11.** La ronda 2 se emitió y su dictamen es **APROBADO**: verifica los diecisiete cerrados uno por uno sobre el instrumento y **levanta el rechazo**. Deja dos hallazgos nuevos, los dos P2 y los dos fuera de los 21 artefactos auditados, cerrados en esta misma tanda | Orquestador SDD | [`Audit/B-02-03-GeometriaFactory-Api-r2.md`](Audit/B-02-03-GeometriaFactory-Api-r2.md) §10 |
| ~~El nombre del cuarto puerto, el de repositorio de cuentas: el puerto existe y su identificador no está fijado.~~ **CERRADO.** El puerto se llama **`IAccountRepository`** y el glosario lo fija: `Norma-De-Nomenclatura.md` lo nombra en **doce** lugares, y `src/GeometriaFactory.Application/Ports/IAccountRepository.cs` existe desde la etapa `c`. **La fila sobrevivió al cierre porque nadie la volvió a leer** | — | Verificado contra el árbol el **2026-08-31** por [`Audit/Mesa-2026-08-31.md`](Audit/Mesa-2026-08-31.md) `M-01` |
| ~~El umbral numérico de fluidez del visor: ninguna fuente lo declara y ninguna categoría lo inventa.~~ **CERRADO el 2026-08-30**, con la escalada `E-03` de la mesa: pasa a **NO APLICA** con la figura de `ADR-14004`, porque **no tiene objeto** —no hay requisito de fluidez del que este número sea la cuantificación— y con su condición de reapertura declarada | — | `Unidades-Entrega/GeometriaFactory-Web/05-Arquitectura-Tecnica/` §11 |
| ~~El alcance de la colección de peticiones reproducible: la fuente lo declara en dos lugares con alcances distintos.~~ **CERRADO el 2026-08-12**: son los **ocho** escenarios, y así lo registran [`Producto/Pipeline-Producto.md`](Producto/Pipeline-Producto.md) y [`Handoff-Checkout.md`](Handoff-Checkout.md) `A-12`, los dos tachados. **Esta fila no se enteró** | — | Verificado el **2026-08-31**, `M-01` |
| Los umbrales rotulados como asunción —coberturas, latencias, caudal y arranque en frío— que condicionan gates de la canalización | Product Owner | `PRODUCT-INTAKE` §22, asunciones `A-3`, `A-4` y `A-5`, **las tres vigentes al 2026-08-31**. **Evento de cierre:** **punto de control de la fase `i`**, que `PRODUCT-INTAKE` §10 y §15 declaran bloqueante y es el último que queda |
| Las marcas para verificar heredadas de las fuentes: capacidades del hosting, versión de la biblioteca de componentes, construcción de la imagen en destino. **Ocho marcas `[A VERIFICAR]` vigentes al 2026-08-31** | Se resuelven **midiendo** | `PRODUCT-INTAKE` §22. **Evento de cierre:** la **fase `i` · Despliegue real**, que es cuando existe el destino sobre el que se mide. La etapa `a` cerró el 2026-08-13 sin resolverlas |
| **Los hallazgos que dejó la implementación de los dieciséis samples: catorce emitidos, y al 2026-08-31 quedan DOS vivos** —los apartamientos del código genérico sin forma de ADR, y dos códigos de facultad sin traducción—. Diez cerrados y dos retirados tras verificarlos contra el contrato | Product Owner, los dos | [`Audit/Reporte-Hallazgos-De-Los-Samples-2026-08-30.md`](Audit/Reporte-Hallazgos-De-Los-Samples-2026-08-30.md) §8. **Evento de cierre:** **punto de control de la fase `i`**, que `PRODUCT-INTAKE` §10 y §15 declaran bloqueante y es el último que queda |

**Ninguno de estos puntos impide seguir.** Los cuatro vivos están atados al **punto de control de la fase `i`**, que es el último que queda: la etapa `a` —a la que este párrafo los ataba— cerró el **2026-08-13**.

**Cada punto vivo declara ahora en qué evento se cierra**, y antes ninguno lo hacía. Sin evento **nada los puede vencer nunca**, que es peor que estar vencidos: una fila vencida se ve, una sin evento no. Es la misma no conformidad con `Root-Rules.md` §12.2 que la escalada `E-04` cerró para once filas de las categorías, y que estas cuatro nunca tuvieron porque no viven en una tabla con esa columna.

**Y tres filas se tacharon porque ya estaban cerradas** —el cuarto puerto, el umbral de fluidez y el alcance de la colección—. Las tres se conservan con su desenlace en lugar de retirarse: **un punto abierto que ya se cerró cuesta el mismo trabajo que uno real**, y quien lo tome merece encontrar por qué no estaba.

## 9. Glosario rápido

Veintiún términos para leer el resto sin tropezar. No reemplaza a los glosarios de categoría: el del dominio del cliente está en [`00-Contexto/Vision-Producto.md`](00-Contexto/Vision-Producto.md) §9, y cada proyecto de código lleva el suyo en su categoría 02.

| Término | Definición en una línea |
| --- | --- |
| Trabajo | Unidad que carga el alumno: nombre, fecha, descripción y el texto con el conjunto de piezas, con identificador propio y estado |
| Estado del trabajo | Conjunto cerrado de cuatro valores: `Borrador`, `Pendiente`, `Finalizado` y `Rechazado` |
| Enviar | La única acción de guardado del alumno: interpreta el texto y decide el estado; no hay una acción separada de guardar sin enviar |
| Pieza | Cada figura del conjunto raíz del trabajo; su identidad es su posición en ese conjunto, porque el dato no trae identificador propio |
| Componente | Figura plana que forma parte de una pieza: tapa, cara, base, lateral o lado |
| Observación | Lo que el producto emite al interpretar el texto del alumno; agrupa dos especies, la advertencia y el error de validación |
| Advertencia | Discrepancia entre un valor declarado y el derivado de las dimensiones; no impide que el trabajo pase a `Pendiente` |
| Error de validación | Defecto que impide interpretar el texto como figuras; deja el trabajo en `Borrador` con sus errores localizados |
| Valor declarado y valor derivado | El que trae el texto del alumno y el que el producto recalcula desde las dimensiones; el par es lo que hace visible el error de fórmula |
| Aprobar y rechazar | Las dos decisiones del administrador sobre un trabajo en `Pendiente`, y su facultad exclusiva |
| Comentario | Texto libre y opcional que el administrador deja al aprobar o al rechazar; no es una calificación ni una observación |
| Actividad 1 | Trabajo práctico de la cátedra que emite el dato que este producto consume; no forma parte del producto |
| Laboratorio | Nombre corriente con el que la cátedra nombra a este producto en uso |
| Etapa | Cada tramo en que el intake descompone la construcción, con su punto de control al cierre |
| Punto de control | Detención obligatoria al cerrar una etapa, a la espera del OK explícito del Product Owner |
| Puerta técnica | Verificación de viabilidad que condiciona la planificación; la que no pasa detiene lo que depende de ella |
| Proyecto de código | Unidad de compilación del producto, con su tipo, su rol y sus dependencias declaradas en el manifiesto |
| Unidad desplegable | Proceso que se despliega por separado; este producto tiene dos, el front y el servicio de datos |
| Puerto | Contrato que la capa de casos de uso define y que la de adaptadores implementa; la dependencia se invierte |
| Fachada del visor | Las seis funciones que el anfitrión puede invocar del bundle; es el único punto de extensión declarado del producto |
| Escenario | Cada uno de los ocho juegos de datos completos, `E-1` a `E-8`, que el intake transcribe y que el producto usa como material de prueba; no se inventan datos de prueba |

## 10. Contacto y responsables

| Rol | Responsable | Canal |
| --- | --- | --- |
| Product Owner | El docente de Programación 2, responsable de la cátedra y de la Actividad 1 | El punto de control de cada etapa, con OK explícito |
| Lead técnico | El mismo docente, asistido por agente de IA | El pull request de la etapa, que **es** el punto de control |
| Auditoría | Auditor independiente, invocado desde cero en cada fase, sin participación en la generación | Los informes de [`Audit/`](Audit/) |
| Dueño del problema | La cátedra de Programación 2 | Consultivo, a través del Product Owner |

## 11. Control de cambios

| Versión | Fecha | Descripción del cambio |
| --- | --- | --- |
| 2.3 | 2026-08-31 | **§8 se rehace contra el árbol, y tres de sus filas vivas resultaron falsas.** Las levantó el contraste del contrato de entrada de [`Audit/Mesa-2026-08-31.md`](Audit/Mesa-2026-08-31.md) (`M-01`), que abre la fuente antes de citarla: **el cuarto puerto** —se llama `IAccountRepository`, el glosario lo nombra doce veces y el archivo existe desde la etapa `c`—, **el umbral de fluidez del visor** —cerrado el 2026-08-30 con `ADR-14004`— y **el alcance de la colección de peticiones** —cerrado el **2026-08-12**, con dos documentos que lo registran tachado y esta fila sin enterarse—. Las tres se **tachan con su desenlace** y no se retiran: un punto abierto que ya se cerró cuesta el mismo trabajo que uno real, y quien lo tome merece encontrar por qué no estaba. **Los cuatro vivos declaran ahora en qué evento se cierran** (`M-05`), que es el punto de control de la fase `i`; ninguno lo declaraba, y sin evento nada los puede vencer nunca. Se corrige además el párrafo de cierre, que ataba los puntos al **punto de control de la etapa `a`**, cerrada el 2026-08-13. La fila de los hallazgos de los samples pasa de «los nueve» a **catorce emitidos y dos vivos**. Sube **minor**: corrige el inventario de lo que no está decidido y no cambia ninguna decisión. |
| 2.2 | 2026-08-30 | **§8 suma un punto abierto**: los **nueve hallazgos** que dejó la implementación de los dieciséis samples de las categorías `10-Examples`, emitidos en [`Audit/Reporte-Hallazgos-De-Los-Samples-2026-08-30.md`](Audit/Reporte-Hallazgos-De-Los-Samples-2026-08-30.md) 1.0. Son dos defectos del visor, tres huecos entre el código y su contrato, dos que se ven hacia afuera, y **el barrido de alcance de `ADR-08006`, que alcanzó el intake y los requerimientos técnicos y no llegó a la categoría 10** —verificado con `grep`: cero menciones a `10-Examples`, `ejemplo-0` y `samples/` en la observación que lo cerró—. **Ningún hallazgo se decide acá y ninguno impide seguir**: los dieciséis samples corren, y los que no coinciden con su §6 lo declaran por escrito, renglón por renglón. Sube **minor**: agrega una fila a un resumen y no cambia ninguna decisión ni ninguna magnitud del producto. |
| 2.1 | 2026-08-25 | **Cierra el hallazgo `C2-1` del audit independiente del corte del README raíz**, que fue **P1**: `Migracion-Rules.md` §4.3.1 no termina donde la emisión 2.0 lo citó. La oración siguiente del mismo párrafo dice que **«un documento cuyos recuentos también quedaron viejos no se reconecta a medias»**, porque reescribirle los identificadores y dejarle las cifras **«produce un documento que afirma cosas que nunca fueron ciertas, que es peor que uno viejo con su fecha declarada»** — y la 2.0 le puso fecha nueva, versión nueva e identificadores vigentes **a tres recuentos viejos**. Se corrigen contra su fuente, que es lo que la regla admite además de declararlos: **la cabecera** pasa de citar el manifiesto **2.2** e intake **2.0** a los vigentes **4.0** y **3.0**; **§7.1** pasa de «dos migraciones normativas» a **seis cerradas y la séptima en curso**; y **§7.2** deja de declarar `f` como «Siguiente» y `g` a `i…` como «No comenzadas» —el `changelog.md` tiene entrada propia para `f`, `g`, `h`, los prerrequisitos de `i` y su puerta— y publica las **ocho etapas cerradas** con la fase `i` **planificada y no ejecutada**. **Y el precedente estaba en este mismo documento**: la fila **1.5** reconectó punteros y **declaró lo que quedaba viejo**; la 2.0 no declaró nada, que es la diferencia que el audit señaló. Se cierran además **`C2-4`** —la nota bajo la Tabla A dice ahora **cuál es el undécimo del rango**, `AG-00031`, y por qué el rango nuevo lo dice mejor que el viejo— y se dejan registrados dos defectos que viven en el **mensaje de commit** de la 2.0 y que no se pueden editar allí: **`C2-2`**, el residuo de la forma anterior en este archivo es **14** y no 11 —la diferencia son las tres citas de `AG-ROOT`—, y **`C2-3`**, esos residuos caen en **dos** clases de exclusión de `SDD-Development-Guide.md` §VI.3.2 y no en una: **las filas de control de cambios** y **la declaración de la propia intervención**. Sube **minor**: corrige afirmaciones y no cambia ninguna decisión. |
| 2.0 | 2026-08-24 | **Migración normativa 10.0 → 13.3, fase M4, corte del README raíz** (`Audit/Plan-Migracion-10.0-a-13.3.md` **1.2** §4.3). **La columna «Responsable» del mapa de documentación pasa a la forma vigente de la familia `AG`**: cinco filas, con el mapeo que el `CHANGELOG` del framework declara para su versión **12.0** —`AG-00` → `AG-00000`, `AG-01` → `AG-00010`, `AG-05, AG-09, AG-11` → `AG-00050, AG-00090, AG-00110`, y los dos rangos `AG-02 a AG-11` → `AG-00020 a AG-00110`—. **`Root-Rules.md` §4.4 es la regla que lo exige** —la Tabla A y su columna— y §9.2 aporta la forma; la 12.0 lo declara **campo bloqueante nuevo** para todo destino conforme. **Los rangos se reescriben y no se enumeran**, con su motivo escrito bajo la tabla: enumerar habría cambiado lo que la fila dice sobre un conjunto que no cambió. **Y la cita `AG-ROOT` de la fila 1.0 de esta misma tabla queda sin tocar, declarada**: es registro histórico, y reescribirla le haría decir a una emisión del 2026-08-11 algo que no dijo. **El destino no renumera el resto del corpus**: `Migracion-Rules.md` §4.3.1 declara que la renumeración de una familia del conjunto normativo **la hace el framework**, y acota el trabajo del destino a la cita de su mapa de documentación. Estado previo archivado en [`_legacy/2026-08-24/README-v1.6.md`](_legacy/2026-08-24/README-v1.6.md). Sube **major**, con el mismo criterio que la 09 adoptó en la ronda 2 del corte anterior: el salto de la regla que lo gobierna es major, y la clasificación se lee de la numeración. |
| 1.6 | 2026-08-16 | **Reemisión sobre el modelo de dos ejes**, que cierra el hallazgo `N-01` del informe de migración 8.6 → 8.11. La 1.5 reconectó los punteros y **declaró que el contenido seguía siendo el del modelo anterior**; esta versión lo corrige en lugar de dejarlo declarado. **Cabecera**: la composición pasa de «7 proyectos de código» a **2 unidades de entrega y 7 proyectos de código**, el campo de proyecto principal pasa a **unidad de entrega principal**, y se declara que el documento refleja el `PRODUCT-MANIFEST` **2.2** —citaba el **1.3**—. **§2 se rehace entero**: de una tabla de siete proyectos de código con `Tipo D8` y `Redistribuible` —dos atributos que el modelo de dos ejes **no le asigna al proyecto de código**— pasa a **§2.1 el eje de entrega**, con las dos unidades, su D8, su integración en runtime y su `redistribuible`, y **§2.2 el eje de construcción**, con los siete proyectos de código, su identidad de código, sus dependencias de compilación y qué unidad compone cada uno, más la constancia de que la arista `Web → Api` vive en el grafo de integración y no en el de compilación. **§7 se parte en tres**: **7.1** documentación por categoría, con las dos migraciones normativas cerradas; **7.2 nueva**, el estado de construcción por etapa —`a` a `e` cerradas, `f` siguiente—, que reemplaza la afirmación «el producto está **especificado y todavía no construido**», falsa desde la etapa `c`; y **7.3**, donde este README **deja de replicar las magnitudes del producto** y remite a los documentos que las cuentan, con el fundamento: las que publicaba eran las anteriores a la consolidación —71 casos de uso, cuando `Api` tiene nueve y `Web` diez— y un índice que copia cifras las hereda viejas sin avisar. **§6** deja de predicar `redistribuible` y el tipo `rest-api` del proyecto de código y los predica de la unidad de entrega. Sube minor: ninguna decisión de producto cambia, y lo que se corrige es lo que el documento afirmaba de sí mismo. | Orquestador SDD |
| 1.5 | 2026-08-16 | **Reconexión del mapa de documentación al árbol vigente**, por la verificación mecánica de la fase M5 de la migración 8.6 → 8.11, que encontró **siete enlaces colgados** en §4 y **tres rutas colgadas** en §8. Los diez apuntaban a `Proyectos/<nombre-del-proyecto-de-código>/`, **un árbol que la migración 6.0 → 8.6 reemplazó por `Unidades-Entrega/`** al mover el nivel de aplicación del proyecto de código a la unidad de entrega (framework 8.0). **§4**: las siete filas por proyecto de código pasan a **dos filas por unidad de entrega**, con la constancia de que los siete proyectos de código **no tienen árbol documental propio** y de dónde quedó el contenido de cada uno. **§8**: las tres rutas de puntos abiertos se reescriben sobre la unidad de entrega que hoy los contiene, nombrando la subsección del proyecto de código donde viven. **Reconexión de punteros por `Migracion-Rules.md` §4.3.1, no reemisión**: no se archivó el estado anterior porque no se reescribió ningún cuerpo. **Este documento conserva desactualizaciones de contenido que esta reparación NO toca y que quedan declaradas como hallazgo del informe de migración 8.6 → 8.11**: la cabecera y §2 citan el `PRODUCT-MANIFEST` **1.3** cuando el vigente es el **2.1**, §2 declara `tipo_unidad_entrega` (D8) y `redistribuible` **por proyecto de código** contra el modelo de dos ejes, §7 afirma que el producto está «especificado y todavía no construido» cuando el código está en la etapa `e`, y sus magnitudes son las anteriores a la consolidación. Repararlas es reemitir el documento, no reconectarlo. Sube minor. |
| 1.4 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El recuento de §1 pasa de **15 códigos vivos sobre 18 emitidos** a **17 sobre 20**, con los **tres retirados intactos y ninguno reciclado**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. **Ninguna otra magnitud del producto, ningún proyecto de código y ninguna otra decisión cambia.** Sube minor. |
| 1.3 | 2026-08-11 | **Registra la promoción del estado documental del corpus**, hecha el mismo día sobre `SDD/Docs/` según la política de versionado de documentos de `Master-Prompt.md` 5.2 §5. **§7**, columna «Estado»: las once filas de categoría que decían `Propuesto` pasan a **`Aprobado`**; la de `11-Documentacion` **no cambia** y sigue en `Planificado`, y la de `04-Prompts-AI` sigue omitida por gating. **La constancia única de la promoción —alcance contado, fundamento, los ocho documentos no promovidos y lo que queda fuera de `SDD/Docs/`— vive en [`Handoff-Checkout.md`](Handoff-Checkout.md) §2, y este README no la repite**: sólo publica su resultado por categoría. La cabecera de este documento pasó a `Aprobado` por la misma promoción, y **eso no sube versión**: lo que la sube es la reescritura de la columna de §7. **Ninguna magnitud del producto, ningún punto abierto y ninguna decisión cambia.** Sube minor. |
| 1.2 | 2026-08-11 | **Absorbe la emisión de [`Audit/B-02-03-GeometriaFactory-Api-r2.md`](Audit/B-02-03-GeometriaFactory-Api-r2.md) 1.0, cuyo dictamen es APROBADO, y cierra con ella el hallazgo `N-02` (P2) de ese informe.** **§8**: el punto abierto del rechazo de la Fase B de `GeometriaFactory-Api` pasa a **CERRADO**, con la fila original tachada y conservada porque era correcta al escribirse; y el recuento de hallazgos de la ronda 1 pasa de **quince** a **diecisiete**, que es lo que suma su propio desglose —un P0, cinco P1, seis P2 y cinco P3— y lo que la ronda 2 verificó cerrado uno por uno. **§7**, fila de `02-Especificacion-Funcional`: se registra que el rechazo quedó levantado. **Búsqueda de propagación hecha con `grep` sobre todo el corpus vivo**: «quince hallazgos» sobre ese informe vivía en **tres** documentos de nivel producto —éste §8, [`Handoff-Checkout.md`](Handoff-Checkout.md) §6.1 `B-1` y [`Producto/Vista-Producto.md`](Producto/Vista-Producto.md) §1.1—, y «falta la ronda 2» en esos **tres mismos** lugares; los tres se corrigen en esta tanda. El recuento de informes de auditoría vivía además en **dos** lugares de `Handoff-Checkout.md` —§2.1 y §5— y pasa de 30 y 32 a **33**. **Ninguna magnitud del producto, ningún proyecto de código y ninguna decisión cambia.** Sube minor. |
| 1.1 | 2026-08-11 | **Absorbe la emisión de [`Audit/B-02-03-GeometriaFactory-Api-r1.md`](Audit/B-02-03-GeometriaFactory-Api-r1.md) y de [`Audit/B2-Maqueta-GeometriaFactory-Web-r2.md`](Audit/B2-Maqueta-GeometriaFactory-Web-r2.md), los dos del 2026-08-11.** **§7**, fila de `02-Especificacion-Funcional`: la Fase B pasa a estar auditada en **los siete** proyectos de código; ya no hay informe faltante, y lo que se declara es que el del proyecto de código principal llegó tarde y con dictamen rechazado. **§8**: el punto abierto deja de ser «falta el informe» —que dejó de ser cierto— y pasa a ser **«falta la ronda 2 que levante su rechazo»**, con el informe citado directamente. Se llega acá por la búsqueda de propagación que exige `B-02-03-GeometriaFactory-Api-r1.md` §10 para toda corrección de este tipo: este README y `Handoff-Checkout.md` §11 `D-3` eran los otros dos lugares vivos que afirmaban la ausencia del informe, y los dos se corrigen en la misma tanda. **Ninguna magnitud del producto, ningún proyecto de código y ninguna decisión cambia.** Sube minor. |
| 1.0 | 2026-08-11 | Emisión inicial, en la consolidación de la Fase H. Presenta la identidad del producto, la tabla de los **siete** proyectos de código con su tipo D8, rol y dependencias reflejando el `PRODUCT-MANIFEST` **1.3** sin divergencias, el stack y las plataformas por proyecto de código, el mapa de la documentación con las categorías de nivel producto y la carpeta de cada proyecto de código, **cuatro** flujos de lectura por rol de intervención, el proceso de regeneración con la declaración fundamentada de los **tres** archivos satélite que no se emiten, el estado por categoría con su fase de cierre, los **siete** puntos abiertos de nivel producto con su titular, un glosario rápido de **veintiún** términos y la tabla de responsables. **No decide nada y no replica el roadmap**: enlaza a `00-Contexto/Roadmap-Producto.md`. **Autor:** Arquitecto de Soluciones Senior + API Designer (AG-ROOT) |
