# Backlog técnico — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Backlog-Tecnico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **seis** componentes en tres capas), §3.3 (qué se porta y qué no), §5 (pipeline y puertas), §6 (vista de datos), §8 (los **ocho** NFR), §9 (los **seis** riesgos), §10.2 (las **siete** garantías) y §11 (los **cinco** puntos abiertos); las **seis** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md) y [`../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../../../05-Arquitectura-Tecnica/Flujo-Ejecucion.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) (garantías y códigos de condición); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15 (puertas `PT-02` y `PT-03`), §16 y §16.1, §17.7.P.1 a P.11, §18 y §20
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas`, `09-Devops` y `10-Examples` de GeometriaFactory-Visor

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones y cadena de construcción](#21-ep-t01--fundaciones-y-cadena-de-construcción)
  - [2.2 EP-T02 · Capa 2, fachada y registro de instancias](#22-ep-t02--capa-2-fachada-y-registro-de-instancias)
  - [2.3 EP-T03 · Capa 3, lectura, dibujo y movimiento](#23-ep-t03--capa-3-lectura-dibujo-y-movimiento)
  - [2.4 EP-T04 · Puertas, sample e inspección del bundle](#24-ep-t04--puertas-sample-e-inspección-del-bundle)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **dieciocho** tareas técnicas viven **inline**, porque el proyecto de código está por debajo del umbral de treinta.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) §3.1, de una ADR, de un NFR de su §8, de una puerta técnica del intake §15 o de un punto abierto de `05` §11. Las cuatro que cierran un punto abierto son BT-12003, BT-12009, BT-12017 y BT-12018.

**Dos particularidades de este backlog técnico, que conviene decir antes de leerlo.**

1. **Varias tareas se verifican sobre el bundle generado y no sobre el código fuente.** `05` §9 declara que la causa más probable de que aparezca una petición de red no es la comodidad del programador sino **una dependencia que la haga por dentro**, y por eso la verificación se hace sobre el artefacto que se sirve. BT-12013 y BT-12016 son de esa clase.
2. **Una parte del trabajo es decidir qué del visualizador previo no se porta.** `05` §3.3 declara qué se conserva y qué no, y el motivo de cada exclusión; ese documento es fuente de las tareas de la capa 3 y no una nota de contexto.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones y cadena de construcción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto del bundle exista, que su construcción sea reproducible y que produzca en la etapa `a` un archivo **vacío pero real** |
| Alcance | Estructura del proyecto, instalación reproducible de dependencias, empaquetado, copia al directorio de recursos estáticos del anfitrión, guion de ciclo corto y la decisión de versionar o ignorar el artefacto generado |
| Fuente upstream | `PRODUCT-INTAKE` §15 (etapa `a`), §16 y §17.7.P.8; `05` §5 y §11 `PA-05` |
| Momento | Etapa `a` |
| BT contenidas | BT-12001, BT-12002, BT-12003 |

### 2.2 EP-T02 · Capa 2, fachada y registro de instancias

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que exista una única puerta al interior, con las **seis** funciones planas, el registro que resuelve el identificador y los **siete** códigos de condición tomados de su fuente única |
| Alcance | Fachada plana, registro de instancias e incorporación de los códigos de condición |
| Fuente upstream | `05` §3.1 (fachada plana, registro de instancias); [`ADR-12001`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md), [`ADR-12002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §6 |
| Momento | Antes de comprometer la etapa `g` |
| BT contenidas | BT-12004, BT-12005, BT-12006 |

### 2.3 EP-T03 · Capa 3, lectura, dibujo y movimiento

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la escena exista, que se dibuje lo que el texto trae, que la disposición sea determinista y que los dos movimientos automáticos vivan en el bucle de dibujo sin que el anfitrión conozca el interior |
| Alcance | Lector del texto, servicio de dibujo, motor confinado a la capa 3, disposición por índice, movimientos y liberación de recursos |
| Fuente upstream | `05` §3.1 (lector del texto, servicio de dibujo, motor de dibujo), §3.3, §4 y §6; [`ADR-12004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md), [`ADR-12005`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md); `05` §11 `PA-01` y `PA-02` |
| Momento | Antes de comprometer la etapa `g`, salvo BT-12011 y BT-12017, que son de la etapa `g` |
| BT contenidas | BT-12007, BT-12008, BT-12009, BT-12010, BT-12011, BT-12012, BT-12017 |

### 2.4 EP-T04 · Puertas, sample e inspección del bundle

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las dos puertas técnicas del proyecto de código se puedan medir, que el punto de extensión tenga su demostración y que la superficie del bundle generado sea la declarada y ninguna otra |
| Alcance | `PT-02`, `PT-03`, la página integradora sin backend, la inspección del bundle generado y el umbral de fluidez |
| Fuente upstream | `PRODUCT-INTAKE` §15 (puertas), §16.1 y §18 (sample `S-1`); `05` §8 (NFR de dependencias externas y de superficie pública) y §11 `PA-03`; [`ADR-12003`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md), [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) |
| Momento | `PT-02` y `PT-03` antes de comprometer la etapa `g`; el sample y el umbral, en la etapa `g` |
| BT contenidas | BT-12013, BT-12014, BT-12015, BT-12016, BT-12018 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Momento | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-12001 | Crear el proyecto del bundle con su cadena de construcción reproducible | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.7.P.8; `05` §5 | Ninguna | Las etapas de instalación reproducible de dependencias, empaquetado y copia al directorio de recursos estáticos del anfitrión corren de punta a punta; el bundle se genera **sin errores**; en la etapa `a` el archivo es **vacío pero real** | **Infraestructura compartida**: la sostiene `05` §5. Habilita a las 14 |
| BT-12002 | Guion de construcción propio del bundle, para el ciclo corto de trabajo | devops | EP-T01 | `a` | Media | Sin fijar | `05` §5, fila de ciclo corto de trabajo | BT-12001 | Un guion genera **sólo** el bundle, sin encadenar la construcción del resto del producto; el guion general sigue encadenando los dos | **Infraestructura compartida**: es lo que hace barato iterar sobre la capa 3 |
| BT-12003 | Decidir si el bundle generado se versiona en el repositorio o se ignora | indagación | EP-T01 | `a` | Media | Sin fijar | `05` §11 `PA-05`; `PRODUCT-INTAKE` §17.7.P.7 | BT-12001 | Queda decidido y registrado: si se versiona, se versiona **como salida reproducible**; si se ignora, el guion de construcción lo genera antes de publicar. En los dos casos **el artefacto nunca se edita a mano**. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la decisión es de 09 y este backlog la eleva con su plazo |
| BT-12004 | Construir la fachada plana con las seis funciones | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Fachada plana»; [`ADR-12002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | BT-12001 | Las **seis** funciones existen con los nombres que `PRODUCT-INTAKE` §17.7.P.3 fija; la capa 2 **no contiene lógica de dibujo**; toda condición se informa por su código y ninguna operación deja la instancia en estado indeterminado (garantía `G-7`) | US-12001, US-12002, US-12003, US-12007, US-12009, US-12010, US-12012 |
| BT-12005 | Construir el registro de instancias con su invalidación | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Registro de instancias» | BT-12004 | Cada identificador resuelve a su instancia viva; al liberarla el identificador **queda invalidado** y toda invocación posterior informa `INSTANCIA_DESCONOCIDA`; dos instancias vivas no comparten escena, ni selección, ni disposición (garantía `G-4`) | US-12001, US-12009, US-12010, US-12011 |
| BT-12006 | Incorporar los siete códigos de condición desde su fuente única | feature | EP-T02 | Antes de `g` | Alta | Sin fijar | [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §6; `05` §7 y §9, sexto riesgo | BT-12004 | Los códigos son exactamente **siete**; **ninguno se acuña aguas abajo**; un curso nuevo se agrega como fila de curso y **no** como código; el catálogo de 03 puede crecer sin que crezca el conjunto de códigos, y esa distinción queda escrita | US-12003, US-12005, US-12006, US-12009, US-12010, US-12011 |
| BT-12007 | Construir el lector del texto con las variantes de clave del emisor | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Lector del texto»; `PRODUCT-INTAKE` §17.7.P.11 punto 4 | BT-12001 | Obtiene piezas, componentes y dimensiones tolerando las variantes de clave del emisor real; lo que produce `DIMENSION_NO_LEGIBLE` es la **ausencia** de la clave o del componente, **nunca el valor que trae**; el cero es una dimensión legible | US-12004, US-12005, US-12006, US-12007 |
| BT-12008 | Construir el servicio de dibujo | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §3.1, componente «Servicio de dibujo»; [`Flujo-Ejecucion.md`](../../../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) | BT-12005, BT-12007, BT-12009 | Escena, mallas, disposición, selección, encuadre y bucle de dibujo funcionan; la capa 3 **no conoce al anfitrión**; se dibujan los **seis** tipos dibujables, tres volumétricos y tres planos | US-12001, US-12004, US-12006, US-12010 |
| BT-12009 | Anclar la versión del motor de dibujo y confinarlo a la capa 3 | indagación | EP-T03 | Antes de `g` | Alta | Sin fijar | [`ADR-12004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md); `05` §11 `PA-01`; `PRODUCT-INTAKE` §17.7.P.1 | BT-12001 | La versión queda anclada y registrada según la regla de anclaje del producto; si es posterior a la del visualizador previo, **se documenta el cambio de interfaz que exija**; el motor **nunca se expone al anfitrión**. **Caja temporal: antes de comprometer la etapa `g`** | US-12004 |
| BT-12010 | Derivar la disposición de cada pieza de su índice | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | [`ADR-12005`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md); `05` §3.3, última fila de lo que no se porta | BT-12008 | Dos procesados del mismo texto producen la **misma posición** de cada pieza; **el ordenamiento aleatorio del visualizador previo se reemplaza** y no queda ningún rastro suyo; la comparación es de posición y no de orientación | US-12008 |
| BT-12011 | Construir el gobierno de los dos movimientos automáticos en el bucle de dibujo | feature | EP-T03 | `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §4 (`F-25`) y §17.7.P.3 (sexta función); `05` §4 y §6 | BT-12004, BT-12008, BT-12010 | Los dos movimientos se prenden y se apagan **por separado** sobre una instancia viva, sin reconstruirla y sin perder la selección; se detienen mientras la persona arrastra y mientras la superficie no está visible, **sin cambiar el estado gobernado**; el estado de los movimientos **sobrevive a la carga de otro texto** | US-12002, US-12012, US-12013 |
| BT-12012 | Liberar recursos y cortar el bucle al destruir la instancia | feature | EP-T03 | Antes de `g` | Alta | Sin fijar | `05` §4, última viñeta; `05` §9, tercer riesgo; [`ADR-12001`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | BT-12005, BT-12008 | La destrucción libera los recursos gráficos y **corta el bucle**; **un bucle que sobreviviera a la destrucción es la forma de degradación que hay que descartar**, y se mide con los dos movimientos prendidos | US-12011 |
| BT-12013 | Medir la puerta `PT-03` sobre el bundle generado | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.7.P.8; `05` §8, fila de dependencias externas; [`ADR-12004`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | BT-12001, BT-12008, BT-12009 | El motor de dibujo queda **dentro** del bundle; la página funciona **sin acceso a redes de distribución externas**; exactamente **0** dependencias traídas de una red externa en tiempo de ejecución. **Una puerta que no pasa detiene la planificación de la etapa `g`** y no se arrastra como deuda | **Infraestructura compartida**: la puerta condiciona la etapa entera |
| BT-12014 | Medir la puerta `PT-02` sobre una página del anfitrión | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §15 y §17.7.P.8; `05` §5, fila de puertas bloqueantes | BT-12004, BT-12005, BT-12008, BT-12012 | El bundle carga en una página del anfitrión; la creación de instancia arma la escena; la carga del texto dibuja las **tres** figuras de `E-1` **incluido el ortoedro**; **diez** recorridos de ida y vuelta no degradan; el árbol y la escena **se sincronizan por índice**. Los recorridos se miden **con los dos movimientos prendidos** | US-12001, US-12004, US-12009, US-12011 |
| BT-12015 | Construir la página integradora sin backend, que es el sample `S-1` | feature | EP-T04 | `g` | Alta | Sin fijar | `PRODUCT-INTAKE` §16.1 y §18; [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md); [`Extensibilidad.md`](../../../05-Arquitectura-Tecnica/Extensibilidad.md) | BT-12004 a BT-12012 | Un archivo carga el bundle y un texto **pegado a mano** y dibuja, con **0** servicios del backend disponibles; recorre las **seis** funciones; es el material con el que se verifican las **seis** propiedades transversales juntas | US-12014 |
| BT-12016 | Inspeccionar la superficie del bundle generado | devops | EP-T04 | Antes de `g` | Alta | Sin fijar | `05` §8, filas de cero red y de superficie pública del bundle; `05` §9, primer riesgo | BT-12001, BT-12004 | Exactamente **6** funciones expuestas, bajo **1** nombre propio en el objeto global del navegador y **0** identificadores globales sueltos; **0** ocurrencias de las tres formas de petición de red, **en el código fuente y en el bundle generado**; **0** claves escritas en el almacenamiento del navegador | US-12001, US-12014 |
| BT-12017 | Fijar los nombres internos de funciones, de clases y de campos | indagación | EP-T03 | `g` | Media | Sin fijar | `05` §11 `PA-02` | BT-12004, BT-12008 | Los nombres internos quedan decididos y registrados. **Los nombres de las seis funciones de la fachada no entran en esta tarea**: los fija `PRODUCT-INTAKE` §17.7.P.3 y no están abiertos. **Caja temporal: la etapa `g`** | **Infraestructura compartida**: ninguna historia la consume por separado |
| BT-12018 | Resolver el umbral numérico de fluidez, o dejarlo declaradamente cualitativo | indagación | EP-T04 | `g` | Media | Sin fijar | `05` §8, cierre; `05` §11 `PA-03` | BT-12011, BT-12014 | O bien el Product Owner fija un umbral, o bien 08 fija su guion de medición cualitativo junto con `PT-02`. **Ninguna de las dos salidas es inventar un número acá**: `05` §8 se niega explícitamente a hacerlo porque se propagaría a 08 como si fuera del producto. **Caja temporal: antes de cerrar la etapa `g`** | **Infraestructura compartida**: condiciona el guion de medición de 08 |

**Siete tareas se justifican como infraestructura compartida** —BT-12001, BT-12002, BT-12003, BT-12013, BT-12017, BT-12018 y, por su naturaleza de puerta, también BT-12016, que además declara dos historias consumidoras— y las once restantes declaran al menos una historia que las consume.

## 4. Trazabilidad BT ↔ US ↔ CU

Las dieciocho filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Visor/Especificacion-Funcional.md) §3.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-12001 | Infraestructura compartida (habilita a las 14) | CU-12001 a CU-12007 | `05` §5 |
| BT-12002 | Infraestructura compartida | — (ciclo de trabajo) | `05` §5 |
| BT-12003 | Infraestructura compartida | — (artefacto generado) | `05` §11 `PA-05` |
| BT-12004 | US-12001, US-12002, US-12003, US-12007, US-12009, US-12010, US-12012 | CU-12001, CU-12002, CU-12003, CU-12004, CU-12007 | ADR-12002, `05` §3.1 |
| BT-12005 | US-12001, US-12009, US-12010, US-12011 | CU-12001, CU-12003, CU-12004, CU-12005 | `05` §3.1, registro de instancias |
| BT-12006 | US-12003, US-12005, US-12006, US-12009, US-12010, US-12011 | CU-12001 a CU-12005, CU-12007 | Definicion-Contrato-De-Fachada §6 |
| BT-12007 | US-12004, US-12005, US-12006, US-12007 | CU-12002 | `05` §3.1, lector del texto |
| BT-12008 | US-12001, US-12004, US-12006, US-12010 | CU-12001, CU-12002, CU-12004 | `05` §3.1, servicio de dibujo |
| BT-12009 | US-12004 | CU-12002 | ADR-12004, `05` §11 `PA-01` |
| BT-12010 | US-12008 | CU-12002 | ADR-12005 |
| BT-12011 | US-12002, US-12012, US-12013 | CU-12001, CU-12007 | `PRODUCT-INTAKE` §17.7.P.3, `05` §4 |
| BT-12012 | US-12011 | CU-12005 | ADR-12001, `05` §4 |
| BT-12013 | Infraestructura compartida | — (puerta `PT-03`) | `05` §8, ADR-12004 |
| BT-12014 | US-12001, US-12004, US-12009, US-12011 | CU-12001, CU-12002, CU-12003, CU-12005 | `05` §5, puertas bloqueantes |
| BT-12015 | US-12014 | CU-12006 | ADR-12006, `PRODUCT-INTAKE` §16.1 y §18 |
| BT-12016 | US-12001, US-12014 | CU-12001, CU-12006 | `05` §8, ADR-12002, ADR-12003 |
| BT-12017 | Infraestructura compartida | CU-12001 a CU-12007 | `05` §11 `PA-02` |
| BT-12018 | Infraestructura compartida | — (guion de medición) | `05` §11 `PA-03` |

**Cobertura inversa: los siete casos de uso tienen al menos una tarea técnica que los realiza.** CU-12001 en BT-12004, BT-12005, BT-12006, BT-12008, BT-12011, BT-12014 y BT-12016; CU-12002 en BT-12004, BT-12006, BT-12007, BT-12008, BT-12009, BT-12010 y BT-12014; CU-12003 en BT-12004, BT-12005, BT-12006 y BT-12014; CU-12004 en BT-12004, BT-12005, BT-12006 y BT-12008; CU-12005 en BT-12005, BT-12006, BT-12012 y BT-12014; CU-12006 en BT-12015 y BT-12016; CU-12007 en BT-12004, BT-12006 y BT-12011. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

**Cobertura de las siete garantías de `05` §10.2.** `G-1` en BT-12013 y BT-12016; `G-2` en BT-12016; `G-3` en BT-12004 y BT-12011; `G-4` en BT-12005 y BT-12008; `G-5` en BT-12006 y BT-12007; `G-6` en BT-12010; `G-7` en BT-12004 y BT-12012. **Las siete tienen tarea técnica.** Perder cualquiera de ellas es un cambio mayor aunque las seis firmas no se toquen (`05` §10.2), y por eso ninguna queda sin trabajo que la sostenga.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Visor`. Declara **cuatro** épicas técnicas alineadas con las tres capas y con las puertas, y **dieciocho** tareas técnicas inline —por debajo del umbral de treinta— cada una con tipo, fuente upstream por identificador, momento, dependencias, criterios de aceptación verificables y las historias que la consumen. Declara las dos particularidades del proyecto de código: que varias tareas se verifican **sobre el bundle generado** y no sobre el código fuente, y que decidir qué del visualizador previo no se porta es trabajo y no contexto. Convierte en trabajo los cuatro puntos abiertos de la categoría 05 que lo admiten —versión del motor, nombres internos, umbral de fluidez y versionado del artefacto generado— y deja constancia de que ninguna de las salidas de BT-12018 consiste en inventar un número. Emite la matriz BT ↔ US ↔ CU con sus dieciocho filas, la cobertura inversa sobre los siete casos de uso y la cobertura de las **siete** garantías. |
| 1.1 | 2026-08-11 | **Cierra el hallazgo `D-06-02`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **§4**: la enumeración de cobertura inversa omitía **BT-12011** en la entrada de **CU-12001**, pese a que la fila de BT-12011 de esa misma matriz declara «CU-12001, CU-12007». La omisión no afectaba la cobertura —los siete casos de uso tenían y tienen al menos una tarea técnica— pero sí la exhaustividad de una enumeración que se lee como completa. Se agrega BT-12011 y se declara explícitamente que la enumeración **es exhaustiva** y que incluye las filas de alcance general, que en este proyecto de código son BT-12001, BT-12006 y BT-12017. **Se recontó la matriz entera**, reconstruyendo el diccionario inverso `CU → {BT}` desde las dieciocho filas: ésta era la única discrepancia. Ninguna tarea técnica, dependencia ni criterio cambia. Sube minor. |
