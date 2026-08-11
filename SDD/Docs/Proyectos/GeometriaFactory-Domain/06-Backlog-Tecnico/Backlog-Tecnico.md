# Backlog técnico — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Backlog-Tecnico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1 (los **cinco** componentes), §5 (etapas del pipeline), §8 (los **seis** NFR), §9 (los **cinco** riesgos) y §11 (los **cuatro** puntos abiertos); las **seis** ADR de [`../05-Arquitectura-Tecnica/Adrs/`](../05-Arquitectura-Tecnica/Adrs/); [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) 1.5 (las **42** condiciones); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §15 (etapas y reglas de delivery), §16 (estructura de repositorio) y §17.1.P.6 a P.11
**Trazabilidad downstream:** [`Product-Backlog.md`](Product-Backlog.md), `07-Plan-Sprint`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Cómo se lee este backlog](#1-cómo-se-lee-este-backlog)
- [2. Épicas técnicas y sus tareas](#2-épicas-técnicas-y-sus-tareas)
  - [2.1 EP-T01 · Fundaciones del proyecto de código](#21-ep-t01--fundaciones-del-proyecto-de-código)
  - [2.2 EP-T02 · Superficie pública y resultados tipados](#22-ep-t02--superficie-pública-y-resultados-tipados)
  - [2.3 EP-T03 · Guardas de cuenta y admisibilidad](#23-ep-t03--guardas-de-cuenta-y-admisibilidad)
  - [2.4 EP-T04 · Trabajo, estados y adopción](#24-ep-t04--trabajo-estados-y-adopción)
  - [2.5 EP-T05 · Verificación y puertas](#25-ep-t05--verificación-y-puertas)
- [3. Detalle de las tareas técnicas](#3-detalle-de-las-tareas-técnicas)
- [4. Trazabilidad BT ↔ US ↔ CU](#4-trazabilidad-bt--us--cu)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Cómo se lee este backlog

Las **dieciséis** tareas técnicas viven **inline** en este documento y no en archivos individuales, porque el proyecto de código está por debajo del umbral de treinta que fija la regla de la categoría. Cada una declara su fuente upstream por identificador, sus criterios de aceptación, sus dependencias, su tipo y las historias que la consumen.

**Ninguna tarea inventa alcance.** Cada una nace de un componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1, de una ADR, de un NFR de su §8, de un punto abierto de su §11 o de una regla de delivery del intake §15. Las cuatro que cierran un punto abierto —BT-02, BT-03, BT-15 y BT-16— son la parte de este backlog que convierte en trabajo lo que las categorías anteriores dejaron declarado sin resolver, en lugar de resolverlo por su cuenta.

**Estimación: sin fijar**, por el fundamento de [`Product-Backlog.md`](Product-Backlog.md) §4.1. Lo que ordena las tareas es la **etapa** y las dependencias de §3, no un tamaño relativo.

## 2. Épicas técnicas y sus tareas

### 2.1 EP-T01 · Fundaciones del proyecto de código

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que el proyecto de código exista, compile con cero dependencias salientes y cierre en su punto de control las dos decisiones que el intake dejó abiertas para la etapa `a` |
| Alcance | Estructura del proyecto y de su proyecto de pruebas, nombres, herramienta de versión y las dos puertas de construcción |
| Fuente upstream | `PRODUCT-INTAKE` §16 (estructura de repositorio), §17.1.P.7 y P.11; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md); `05` §5, §8 y §11 |
| Etapa | `a` |
| BT contenidas | BT-01, BT-02, BT-03, BT-04, BT-05 |

### 2.2 EP-T02 · Superficie pública y resultados tipados

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que la superficie pública sea la que declara el contrato de abstracciones, con rechazo tipado y con el catálogo de condiciones cerrado en las dos direcciones |
| Alcance | Núcleo de entidades, forma de las guardas, catálogo de condiciones, entrada del momento y de la unicidad por parámetro |
| Fuente upstream | `05` §3.1 (núcleo de entidades), [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md), [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md), `05` §8 fila del catálogo de condiciones |
| Etapa | `c` a `f`, según la historia que la consuma |
| BT contenidas | BT-06, BT-07, BT-08, BT-09 |

### 2.3 EP-T03 · Guardas de cuenta y admisibilidad

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las reglas de la cuenta se ejerzan en su componente y que la admisibilidad sea una puerta única |
| Alcance | Guardas de cuenta y evaluador de admisibilidad, con `INV-06` e `INV-09` ejercidos en un solo lugar |
| Fuente upstream | `05` §3.1 (guardas de cuenta, evaluador de admisibilidad), [`ADR-04`](../05-Arquitectura-Tecnica/Adrs/ADR-04-Frontera-De-Autenticacion-Y-Autorizacion.md), [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) |
| Etapa | `c` y `d` |
| BT contenidas | BT-10, BT-11 |

### 2.4 EP-T04 · Trabajo, estados y adopción

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que las transiciones del trabajo y la adopción del conjunto de piezas vivan cada una en su componente, sin que ninguna regla se ejerza dos veces |
| Alcance | Máquina de estados del trabajo y adopción de la interpretación |
| Fuente upstream | `05` §3.1 (máquina de estados del trabajo, adopción de la interpretación), [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); `PRODUCT-INTAKE` §4.2 (modelo de estados) |
| Etapa | `e`, `f` y `h` |
| BT contenidas | BT-12, BT-13 |

### 2.5 EP-T05 · Verificación y puertas

| Aspecto | Contenido |
| --- | --- |
| Objetivo | Que los nueve invariantes queden ejercidos, que las puertas medibles del proyecto de código estén definidas y que los dos valores rotulados como asunción se confirmen antes de volverse bloqueantes |
| Alcance | Matriz invariante contra prueba, puertas de cobertura y de tiempo, y el criterio de comparación de correos |
| Fuente upstream | `05` §8 (NFR de ejercicio de los invariantes y de cobertura), `05` §11 PA-02; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §9; `PRODUCT-INTAKE` §22 asunciones `A-3` y `A-5` |
| Etapa | `a` la definición, `d` a `h` la ejecución acumulativa |
| BT contenidas | BT-14, BT-15, BT-16 |

## 3. Detalle de las tareas técnicas

| BT | Título | Tipo | Épica | Etapa | Prioridad | Estimación | Fuente upstream | Dependencias | Criterios de aceptación | US que la consumen |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| BT-01 | Crear el proyecto de código y su proyecto de pruebas, sin dependencias salientes | feature | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §16 y §17.1.P.1; [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Ninguna | El proyecto de código compila dentro del artefacto de agrupación; el archivo de proyecto declara **0** referencias a otros proyectos de código del producto y **0** a bibliotecas de persistencia, transporte o serialización; el proyecto de pruebas existe y corre vacío | **Infraestructura compartida**: la sostiene [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md). Habilita a las 27 |
| BT-02 | Fijar los nombres de tipos y de espacios de nombres, y validarlos en el punto de control | indagación | EP-T01 | `a` | Alta | Sin fijar | `PRODUCT-INTAKE` §17.1.P.11 (punto abierto de la etapa `a`); `05` §11 PA-01 | BT-01 | Existe una propuesta de nombres para las cinco entidades y para los espacios de nombres; el Product Owner la acepta o la corrige **en el punto de control de la etapa `a`**; la decisión queda registrada. **Caja temporal: la etapa `a`**, y no se arrastra a la `c` | **Infraestructura compartida**: ninguna historia la consume por separado, todas dependen de que los nombres estén fijados. `05` §9 la declara como riesgo de retrabajo, no de corrección |
| BT-03 | Elegir y anclar la herramienta que calcula la versión | indagación | EP-T01 | `a` | Media | Sin fijar | `PRODUCT-INTAKE` §17.1.P.7; `05` §11 PA-04 | BT-01 | La herramienta está elegida y su versión anclada según la regla de anclaje de versiones del producto; el cálculo de la versión a partir de las convenciones de mensaje de confirmación produce un resultado reproducible. **Caja temporal: la etapa `a`** | **Infraestructura compartida**: la exige la estrategia de versionado del intake §17.1.P.7 |
| BT-04 | Puerta bloqueante de cero dependencias salientes | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de dependencias salientes; `05` §9, primer riesgo | BT-01 | La inspección del archivo de proyecto es parte de la revisión y **bloquea la fusión** si aparece una dependencia; la puerta se mide en cada etapa, no sólo en la `a` | **Infraestructura compartida**: sostiene la propiedad que justifica el estilo entero ([`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md)) |
| BT-05 | Puerta de construcción con cero advertencias | devops | EP-T01 | `a` | Alta | Sin fijar | `05` §8, fila de advertencias de construcción; `PRODUCT-INTAKE` §17.1.P.8 | BT-01 | El guion de construcción termina en 0 y **sin advertencias**; la condición es bloqueante para fusionar | **Infraestructura compartida**: puerta declarada del pipeline |
| BT-06 | Construir el núcleo de entidades con las cinco entidades del modelo | feature | EP-T02 | `c` | Alta | Sin fijar | `05` §3.1, componente «Núcleo de entidades»; [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) | BT-01, BT-02 | Las cinco entidades quedan constituibles con sus atributos y su semántica; el valor declarado y el derivado de cada pieza se guardan **por separado**; la posición de la pieza es su identidad y el conjunto **admite huecos y no se renumera** (`05` §6) | US-01, US-09, US-11, US-12, US-24 |
| BT-07 | Fijar la forma de la superficie pública: guardas con resultado tipado | feature | EP-T02 | `c` | Alta | Sin fijar | [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md); [`Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) | BT-01, BT-02 | Toda condición prevista viaja como **valor de retorno** con su código estable, nunca como excepción de control de flujo; las excepciones quedan reservadas a defectos de programación del consumidor; ninguna operación deja una entidad a medio modificar | US-02, US-07 y, por herencia de forma, las 27 |
| BT-08 | Cerrar el catálogo de las 42 condiciones en las dos direcciones | feature | EP-T02 | `f` | Alta | Sin fijar | `05` §8, fila de cobertura del catálogo; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) | BT-07 | **100 %** de las 42 condiciones alcanzadas por al menos una prueba, y **0** condiciones producidas por la biblioteca que no figuren en el catálogo; la comparación se hace en las dos direcciones. Los **cinco** identificadores retirados no se reciclan | US-02, US-13, US-14, US-16 |
| BT-09 | Hacer que el momento y la unicidad entren por parámetro | feature | EP-T02 | `d` | Alta | Sin fijar | [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); `05` §7, filas de configuración y de zona horaria | BT-06 | Ninguna operación obtiene el momento por su cuenta ni consulta conjuntos de entidades; la inspección lo verifica; las pruebas son reproducibles sin fijar el reloj del entorno | US-03, US-09 |
| BT-10 | Construir las guardas de cuenta | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Guardas de cuenta» | BT-06, BT-07 | Papeles, ventana de alta del administrador, ciclo de vida y credencial derivada quedan ejercidos en este componente; **las guardas no invocan al evaluador de admisibilidad** (`05` §3.2) | US-01, US-04, US-05, US-06, US-24, US-25, US-26 |
| BT-11 | Construir el evaluador de admisibilidad como puerta única | feature | EP-T03 | `c` | Alta | Sin fijar | `05` §3.1, componente «Evaluador de admisibilidad»; [`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) | BT-06, BT-10 | `INV-06` e `INV-09` se ejercen **en un solo lugar** y no repetidos en cada operación; el resultado trae el motivo de la no admisión; ninguna otra operación del proyecto de código vuelve a comprobar esas dos condiciones | US-06, US-08, US-26, US-27 |
| BT-12 | Construir la máquina de estados del trabajo | feature | EP-T04 | `e` | Alta | Sin fijar | `05` §3.1, componente «Máquina de estados del trabajo»; `PRODUCT-INTAKE` §4.2 | BT-06, BT-07 | Las transiciones del modelo de estados quedan ejercidas, con envío, desenlace, terminalidad y quién elimina en qué estado; una transición no admitida devuelve su condición y no cambia nada | US-05, US-10, US-15, US-16, US-17, US-18, US-19, US-20, US-21, US-22, US-23 |
| BT-13 | Construir la adopción de la interpretación | feature | EP-T04 | `f` | Alta | Sin fijar | `05` §3.1, componente «Adopción de la interpretación» | BT-06, BT-12 | El conjunto de piezas, sus componentes y las observaciones se incorporan comprobando que están bien formados; un conjunto mal formado se rechaza **entero** y el trabajo queda como estaba | US-10, US-11, US-13, US-14, US-16 |
| BT-14 | Armar la matriz de ejercicio de los nueve invariantes | docs | EP-T05 | `d` | Alta | Sin fijar | `05` §8, fila de ejercicio de los invariantes; `05` §9, segundo riesgo | BT-10, BT-11, BT-12 | **100 %** de los nueve invariantes con al menos una prueba que verifique su violación rechazada, **sin dobles de prueba**; la matriz se entrega a 08 y se revisa al cerrar cada etapa | US-08, US-17, US-23, US-25, US-27 |
| BT-15 | Confirmar los dos valores rotulados como asunción y fijar la puerta de cobertura | indagación | EP-T05 | `d` | Media | Sin fijar | `05` §8, filas de tiempo de la batería y de cobertura; `05` §11 PA-02; `PRODUCT-INTAKE` §22 asunciones `A-3` y `A-5` | BT-05, BT-14 | El Product Owner confirma o corrige los dos valores **sobre su propio documento**; hasta entonces se usan como vigentes y la puerta **no se declara bloqueante** en 09. **Caja temporal: antes de fijar la puerta en 09** | **Infraestructura compartida**: condiciona la puerta del pipeline de todas las historias |
| BT-16 | Decidir el criterio de comparación de dos correos | indagación | EP-T05 | `d` | Media | Sin fijar | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §9, punto abierto; `RN-02`, `INV-01` | BT-06 | Queda decidido si dos correos se comparan tal cual o normalizados, **y dónde** se normaliza; la decisión se toma junto con la capa que ejerce la verificación y no acá sola; el dominio sigue conservando el dato como lo recibe. **Caja temporal: antes de cerrar la etapa `d`** | US-03 |

**Seis tareas se justifican como infraestructura compartida** —BT-01, BT-02, BT-03, BT-04, BT-05 y BT-15— y las otras diez declaran al menos una historia consumidora. Ninguna tarea queda sin una cosa ni la otra.

## 4. Trazabilidad BT ↔ US ↔ CU

Las dieciséis filas están, una por tarea técnica, sin agrupar. Los casos de uso son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.

| BT | US que la consumen | CU upstream | Fuente de arquitectura |
| --- | --- | --- | --- |
| BT-01 | Infraestructura compartida (habilita a las 27) | CU-01 a CU-13 | ADR-01 |
| BT-02 | Infraestructura compartida | CU-01 a CU-13 | `05` §11 PA-01 |
| BT-03 | Infraestructura compartida | — (no realiza ningún caso de uso: es la estrategia de versionado) | `05` §11 PA-04 |
| BT-04 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-01 |
| BT-05 | Infraestructura compartida | — (puerta de construcción) | `05` §8, ADR-03 |
| BT-06 | US-01, US-09, US-11, US-12, US-24 | CU-01, CU-05, CU-06, CU-07, CU-12 | `05` §3.1, núcleo de entidades |
| BT-07 | US-02, US-07 | CU-01, CU-03 | ADR-02, Contratos-Abstractions |
| BT-08 | US-02, US-13, US-14, US-16 | CU-01, CU-07, CU-08 | `05` §8, catálogo de condiciones |
| BT-09 | US-03, US-09 | CU-01, CU-05 | ADR-06 |
| BT-10 | US-01, US-04, US-05, US-06, US-24, US-25, US-26 | CU-01, CU-02, CU-03, CU-12, CU-13 | `05` §3.1, guardas de cuenta |
| BT-11 | US-06, US-08, US-26, US-27 | CU-02, CU-03, CU-04, CU-13 | ADR-05 |
| BT-12 | US-05, US-10, US-15, US-16, US-17, US-18, US-19, US-20, US-21, US-22, US-23 | CU-02, CU-05, CU-08, CU-09, CU-10, CU-11 | `05` §3.1, máquina de estados |
| BT-13 | US-10, US-11, US-13, US-14, US-16 | CU-05, CU-06, CU-07, CU-08 | `05` §3.1, adopción de la interpretación |
| BT-14 | US-08, US-17, US-23, US-25, US-27 | CU-04, CU-08, CU-11, CU-12, CU-03 | `05` §8, ejercicio de los invariantes |
| BT-15 | Infraestructura compartida | — (puerta de cobertura y de tiempo) | `05` §11 PA-02 |
| BT-16 | US-03 | CU-01 | `02` §9, punto abierto |

**Cobertura inversa: los trece casos de uso tienen al menos una tarea técnica que los realiza.** CU-01 en BT-06, BT-07, BT-08, BT-09, BT-10 y BT-16; CU-02 en BT-10, BT-11 y BT-12; CU-03 en BT-07, BT-10, BT-11 y BT-14; CU-04 en BT-11 y BT-14; CU-05 en BT-06, BT-09, BT-12 y BT-13; CU-06 en BT-06 y BT-13; CU-07 en BT-06, BT-08 y BT-13; CU-08 en BT-08, BT-12, BT-13 y BT-14; CU-09 en BT-12; CU-10 en BT-12; CU-11 en BT-12 y BT-14; CU-12 en BT-06, BT-10 y BT-14; CU-13 en BT-10 y BT-11. **La enumeración es exhaustiva**: incluye las filas de alcance general —las que declaran un rango de casos de uso— junto con las específicas, y se reconstruyó desde la matriz fila por fila en lugar de escribirse a mano.

## 5. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial del backlog técnico de `GeometriaFactory-Domain`. Declara **cinco** épicas técnicas con su objetivo, su alcance, su fuente upstream y la etapa en la que corren, y **dieciséis** tareas técnicas inline —por debajo del umbral de treinta— cada una con tipo, fuente upstream por identificador, dependencias, criterios de aceptación verificables y las historias que la consumen. Convierte en trabajo los cuatro puntos abiertos que las categorías 02 y 05 dejaron declarados —nombres de tipos y espacios de nombres, herramienta de cálculo de versión, los dos valores rotulados como asunción y el criterio de comparación de correos— en lugar de resolverlos por su cuenta. Emite la matriz BT ↔ US ↔ CU con sus dieciséis filas y su cobertura inversa sobre los trece casos de uso. La estimación queda sin fijar, con el fundamento de `Product-Backlog.md` §4.1. |
| 1.1 | 2026-08-11 | **Cierra el hallazgo `D-06-02`** del informe de auditoría [`../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md`](../../../Audit/D-06-07-Backlog-Siete-Proyectos-r1.md) 1.0. **§4**: la enumeración de cobertura inversa atribuía a **CU-08** las tareas BT-08, BT-12 y BT-13, y omitía **BT-14**, que la fila de esa misma matriz declara sobre «CU-04, CU-08, CU-11, CU-12, CU-03». La omisión no afectaba la cobertura —la afirmación «al menos una tarea técnica» era y sigue siendo verdadera para los trece casos de uso— pero sí la exhaustividad de una enumeración que se lee como completa y lo es para todas las demás filas. Se agrega BT-14 a la entrada de CU-08 y se declara explícitamente que la enumeración **es exhaustiva** y que incluye las filas de alcance general, para que la propiedad quede afirmada y verificable en vez de supuesta. **Se recontó la matriz entera**, reconstruyendo el diccionario inverso `CU → {BT}` desde las dieciséis filas: ésta era la única discrepancia. Ninguna tarea técnica, dependencia ni criterio cambia. Sube minor. |
