> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Necesidades-Negocio.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`Necesidades-Negocio.md`](../../Necesidades-Negocio.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Necesidades de Negocio — Fábrica de Geometría

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | Necesidades-Negocio.md |
| Versión | 1.0 |
| Cantidad de NB | 8 |
| Versión del catálogo de NB | 1.0 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §2 (audiencia y stakeholders), §3 (propuesta de valor), §4 (alcance funcional pretendido con MoSCoW), §6 (flujos típicos), §7 (casos límite), §8 (métricas de éxito), §9 (exclusiones), §10 (restricciones), §11 (riesgos), §15 (descomposición y delivery), §20 y §21 (escenarios de datos), §22 (asunciones); `00-Contexto/Vision-Producto.md`, `00-Contexto/Alcance-Producto.md`, `00-Contexto/Roadmap-Producto.md`, `00-Contexto/Compatibilidad-Plataformas.md` |
| Trazabilidad downstream | CU-01 a CU-22 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Propósito y alcance de esta categoría](#1-propósito-y-alcance-de-esta-categoría)
- [2. Catálogo de necesidades de negocio](#2-catálogo-de-necesidades-de-negocio)
- [3. Criterio de recorte aplicado](#3-criterio-de-recorte-aplicado)
  - [3.1 Fusiones](#31-fusiones)
  - [3.2 Particiones](#32-particiones)
  - [3.3 Capacidades que no reciben NB propia](#33-capacidades-que-no-reciben-nb-propia)
- [4. Mapa de dependencias entre NB](#4-mapa-de-dependencias-entre-nb)
- [5. Trazabilidad agregada](#5-trazabilidad-agregada)
  - [5.1 De capacidad del intake a NB](#51-de-capacidad-del-intake-a-nb)
  - [5.2 De métrica de negocio a NB](#52-de-métrica-de-negocio-a-nb)
  - [5.3 De NB a caso de uso previsto](#53-de-nb-a-caso-de-uso-previsto)
  - [5.4 Posición en la cadena de trazabilidad](#54-posición-en-la-cadena-de-trazabilidad)
- [6. Valores pendientes de confirmación](#6-valores-pendientes-de-confirmación)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Propósito y alcance de esta categoría

Este documento es el punto de entrada al catálogo de necesidades de negocio de **Fábrica de Geometría**. La categoría es de **nivel producto**: se genera una sola vez para el producto entero, a partir del `PRODUCT-INTAKE` único y de los cuatro documentos de `00-Contexto/` ya emitidos.

Cada necesidad articula qué problema concreto del negocio se resuelve, para quién, con qué criterio de éxito verificable y con qué prioridad. Ninguna necesidad origina una prioridad, un target ni una exclusión: todo se deriva del intake y traza a su sección de origen. Los flujos del sistema paso a paso no viven acá: pertenecen a la categoría 02, que desarrolla los casos de uso que cada NB declara previstos.

Las necesidades están redactadas desde las dos personas que usan el producto —el **alumno de la comisión** y el **docente en su papel de administrador del laboratorio**—, que son los beneficiarios declarados en `Vision-Producto.md` §2. El vocabulario del dominio no se redefine acá: los términos usados están declarados en `Vision-Producto.md` §9, que es el glosario raíz de la cadena.

## 2. Catálogo de necesidades de negocio

| ID | Necesidad | Prioridad MoSCoW | CU previstas | Estado | Enlace |
| --- | --- | --- | --- | --- | --- |
| NB-01 | Control de admisión y de bajas del laboratorio | Must Have | CU-01, CU-02, CU-03 | Propuesto | [NB-01](Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) |
| NB-02 | Identidad propia del alumno sin canal de correo | Must Have | CU-04, CU-05, CU-06, CU-07 | Propuesto | [NB-02](Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| NB-03 | Trabajo con dueño, estado y persistencia | Must Have | CU-08, CU-09, CU-10, CU-11 | Propuesto | [NB-03](Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) |
| NB-04 | Interpretación fiel del dato del alumno, con el error localizado | Must Have | CU-12, CU-13 | Propuesto | [NB-04](Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) |
| NB-05 | Visibilidad del error de cálculo sobre el trabajo propio | Must Have | CU-14 | Propuesto | [NB-05](Necesidades-De-Negocio/NB-05-Visibilidad-Del-Error-De-Calculo.md) |
| NB-06 | Visualización del trabajo dentro del producto | Must Have | CU-15, CU-16, CU-17 | Propuesto | [NB-06](Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md) |
| NB-07 | Revisión de la comisión desde un solo lugar | Must Have | CU-18, CU-19, CU-20 | Propuesto | [NB-07](Necesidades-De-Negocio/NB-07-Revision-De-La-Comision-En-Un-Solo-Lugar.md) |
| NB-08 | Alcance del laboratorio desde el aula | Should Have | CU-21, CU-22 | Propuesto | [NB-08](Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md) |

Siete necesidades son Must Have y una es Should Have. Las prioridades no se deciden en esta categoría: se derivan de PRODUCT-INTAKE §4 y se justifican en §9 de cada archivo. Dos NB agrupan capacidades de prioridades distintas —NB-06 con F-11 y F-13, NB-07 con F-12 y F-15— y en ambos casos la prioridad de la NB es la de su capacidad más alta, declarada como tal en su §9.

## 3. Criterio de recorte aplicado

El recorte parte de las capacidades declaradas en PRODUCT-INTAKE §4 y de las tres métricas de negocio de PRODUCT-INTAKE §8, y aplica la regla de fusión y de división de la categoría: se fusionan las capacidades que comparten dolor central y se distinguen sólo por el ejemplo; se parte lo que apunta a métricas distintas con públicos distintos.

Las doce capacidades Must Have del intake se agrupan en seis necesidades (NB-01 a NB-06 más NB-07), la capacidad Should Have F-13 se absorbe en NB-06 por compartir etapa y dolor con F-11, la Could Have F-15 se absorbe en NB-07 por la misma razón, y la Should Have F-14 recibe necesidad propia porque su dolor —que el laboratorio esté disponible en el aula— es de naturaleza distinta y tiene su propio riesgo declarado.

### 3.1 Fusiones

| NB | Capacidades fusionadas | Dolor central compartido |
| --- | --- | --- |
| NB-01 | F-01, F-03 | El docente no tiene ninguna forma de decidir quién está adentro del laboratorio. F-01 y F-03 son el mismo acto de admisión en dos momentos: el primer arranque y la vida de la cursada |
| NB-02 | F-02, F-04, F-05 | El trabajo del alumno no tiene dueño, y la ausencia de canal de correo condiciona las tres capacidades por igual: ninguna credencial se transporta |
| NB-03 | F-06, F-07, F-08 | El trabajo del alumno se pierde al cerrar la página. Cargar, guardar como borrador y listar son el mismo dolor visto en tres momentos del mismo trabajo |
| NB-06 | F-11, F-13 | Ver el trabajo obliga hoy a salir a una página suelta. F-13 es la precisión de esa misma vista, no un dolor nuevo; el intake ya las asigna a la misma etapa `g` |
| NB-07 | F-12, F-15 | El docente no puede ver el estado de la entrega de su comisión. F-15 es el mismo dato agregado, no un dolor distinto |

### 3.2 Particiones

| Partición | Qué se separó | Fundamento |
| --- | --- | --- |
| NB-01 frente a NB-02 | El control de admisión que ejerce el administrador se separó de la identidad propia que obtiene el alumno | Públicos distintos —docente y alumno— y criterios de éxito distintos: NB-01 mide unicidad del administrador y protección de la baja; NB-02 mide alta sin correo de punta a punta. Una sola NB con las cinco capacidades habría mezclado dos beneficiarios con dolores independientes |
| NB-04 frente a NB-05 | Interpretar el texto del alumno se separó de verificar sus valores calculados | Métricas distintas: NB-04 mide aceptación del dato real y localización del error, con target de cobertura de escenarios; NB-05 mide valor didáctico entregado, con target por alumno derivado de PRODUCT-INTAKE §8. Además tienen carácter opuesto: un error de interpretación impide entregar, una advertencia de valor no |
| NB-06 frente a NB-07 | Ver el trabajo dentro del producto se separó de revisar la comisión | Públicos distintos en su motivo: el alumno previsualiza para darse cuenta de si modeló lo que quería; el administrador revisa para recorrer la entrega de toda la comisión. Comparten la vista, y esa relación queda declarada como dependencia de NB-07 sobre NB-06 |
| NB-08 frente al resto | La disponibilidad del laboratorio en el aula se separó de toda capacidad funcional | Es la única necesidad cuyo dolor no es funcional sino de acceso, y tiene su propio riesgo declarado de impacto alto en PRODUCT-INTAKE §11. Fusionarla con cualquier otra habría escondido el riesgo que motiva la forma entera del producto |

### 3.3 Capacidades que no reciben NB propia

| Capacidad | Prioridad declarada | Tratamiento |
| --- | --- | --- |
| F-13 | Should Have | Absorbida en NB-06, identificada en su cuarto criterio —disposición estable entre procesados—, en su quinto —sincronización entre el árbol y la escena— y en CU-17 |
| F-15 | Could Have | Absorbida en NB-07, identificada en su sexto criterio y en CU-20 |
| F-16, F-17 | Could Have | Sin NB en este catálogo. El intake las declara además en la exclusión X-6 como candidatas de la etapa `h`, y no articula un dolor de negocio propio para ellas más allá de la propuesta de la que provienen. Reciben NB cuando el Product Owner planifique la etapa `h` |
| F-18 a F-21 | Won't Have v1 | Sin NB, por definición: son las capacidades excluidas del producto (exclusiones X-1 a X-5). Redactar una NB sobre ellas contradiría `Alcance-Producto.md` §5 |

## 4. Mapa de dependencias entre NB

El grafo es acíclico y ninguna necesidad depende de más de tres. La cadena principal es lineal, y las dos ramas finales convergen en la revisión.

```text
NB-01 ──> NB-02 ──> NB-03 ──> NB-04 ──┬──> NB-05
  │                   │               │
  │                   │               └──> NB-06 ──┐
  │                   │                            │
  └────────────────── └────────────────────────────┴──> NB-07

NB-08  (sin dependencias; verifica y sostiene la disponibilidad del producto entero)
```

| NB | Depende de | Cantidad | Es prerequisito de |
| --- | --- | --- | --- |
| NB-01 | — | 0 | NB-02, NB-07 |
| NB-02 | NB-01 | 1 | NB-03 |
| NB-03 | NB-02 | 1 | NB-04, NB-07 |
| NB-04 | NB-03 | 1 | NB-05, NB-06 |
| NB-05 | NB-04 | 1 | — |
| NB-06 | NB-04 | 1 | NB-07 |
| NB-07 | NB-01, NB-03, NB-06 | 3 | — |
| NB-08 | — | 0 | — |

Verificación de aciclicidad: existe un orden topológico completo —NB-01, NB-02, NB-03, NB-04, NB-05, NB-06, NB-07, y NB-08 en cualquier posición— en el que toda dependencia apunta hacia atrás. No hay ninguna arista de retorno.

El orden de las necesidades coincide con el orden de las etapas del intake, y no por casualidad: PRODUCT-INTAKE §15 corta las etapas en vertical por lo que la persona puede hacer al terminar cada una, que es la misma unidad con la que se recortaron estas necesidades.

## 5. Trazabilidad agregada

### 5.1 De capacidad del intake a NB

| Capacidad | Prioridad (PRODUCT-INTAKE §4) | NB que la recoge | Etapa que la entrega |
| --- | --- | --- | --- |
| F-01 | Must Have | NB-01 | `c` |
| F-02 | Must Have | NB-02 | `d` |
| F-03 | Must Have | NB-01 | `d` |
| F-04 | Must Have | NB-02 | `d` |
| F-05 | Must Have | NB-02 | `c` |
| F-06 | Must Have | NB-03 | `e` |
| F-07 | Must Have | NB-03 | `e` |
| F-08 | Must Have | NB-03 | `e` |
| F-09 | Must Have | NB-04 | `f` |
| F-10 | Must Have | NB-05 | `f` |
| F-11 | Must Have | NB-06 | `g` |
| F-12 | Must Have | NB-07 | `e` |
| F-13 | Should Have | NB-06 | `g` |
| F-14 | Should Have | NB-08 | `h` |
| F-15 | Could Have | NB-07 | `h` |
| F-16, F-17 | Could Have | Sin NB, ver §3.3 | `h` |
| F-18 a F-21 | Won't Have v1 | Sin NB, ver §3.3 | No aplica |

Las doce capacidades Must Have están cubiertas: ninguna quedó sin necesidad que la articule. La correspondencia entre capacidad y etapa se lee de `Alcance-Producto.md` §4.1 y de `Roadmap-Producto.md` §3, y no se decide acá.

### 5.2 De métrica de negocio a NB

| Métrica de `Vision-Producto.md` §6 | NB donde baja a criterio de éxito |
| --- | --- |
| Avance del producto: 7 de 7 etapas cerradas con OK explícito | No baja a una NB en particular. Es una métrica del proceso de construcción y se verifica en el punto de control de cada etapa, que es el plazo declarado de la mayoría de los criterios de este catálogo |
| Cierre del circuito didáctico: ≥ 80 % de trabajos en estado `Finalizado` | NB-03, quinto criterio |
| Valor didáctico entregado: ≥ 1 advertencia por alumno que cargue un cubo del primer ejemplo o un ortoedro | NB-05, primer criterio |

Las tres métricas del intake son de nivel producto y no alcanzan para cubrir los criterios de éxito de ocho necesidades. Los criterios restantes se derivaron de los casos límite de PRODUCT-INTAKE §7, de los flujos de §6, de los escenarios de datos de §20 y de los criterios verificables de transición de `Roadmap-Producto.md` §5.2, que a su vez trazan a los criterios de aceptación de etapa del intake. Cada criterio declara su origen en la nota al pie de §5 de su archivo.

### 5.3 De NB a caso de uso previsto

| NB | CU previstas | Estado de las CU |
| --- | --- | --- |
| NB-01 | CU-01, CU-02, CU-03 | a generar |
| NB-02 | CU-04, CU-05, CU-06, CU-07 | a generar |
| NB-03 | CU-08, CU-09, CU-10, CU-11 | a generar |
| NB-04 | CU-12, CU-13 | a generar |
| NB-05 | CU-14 | a generar |
| NB-06 | CU-15, CU-16, CU-17 | a generar |
| NB-07 | CU-18, CU-19, CU-20 | a generar |
| NB-08 | CU-21, CU-22 | a generar |

Veintidós casos de uso previstos, todos con estado `a generar`. La numeración es una previsión de esta categoría y la confirma la categoría 02 al redactarlos.

### 5.4 Posición en la cadena de trazabilidad

```text
PRODUCT-INTAKE -> 00-Contexto -> NB (este catálogo) -> CU -> US -> BT -> Sprint -> Test -> Pipeline
```

| Eslabón | Documento | Qué consume de este catálogo |
| --- | --- | --- |
| CU | 02-Especificacion-Funcional | Las 22 CU previstas de §5.3 y el problema de negocio que cada una debe resolver |
| US y BT | 06-Backlog-Tecnico | La prioridad MoSCoW de §2 para ordenar el backlog, y el mapa de dependencias de §4 para ordenar el trabajo |
| Sprint | 07-Plan-Sprint | El mismo mapa de dependencias, que coincide con el orden estrictamente secuencial de las etapas declarado en `Roadmap-Producto.md` §4. Por `equipo_n = 1` la categoría 07 emite únicamente `Mini-Plan.md` |
| Test | 08-Calidad-Y-Pruebas | Los criterios de éxito de §5 de cada NB, que son input directo de los criterios de aceptación |

## 6. Valores pendientes de confirmación

Dos criterios de éxito de este catálogo toman su target de la asunción A-2 de PRODUCT-INTAKE §22, declarada completa y utilizable pero **pendiente de confirmación del Product Owner**: el quinto criterio de NB-03 (≥ 80 % de trabajos en estado `Finalizado`) y el primero de NB-05 (≥ 1 advertencia por alumno que cargue un cubo del primer ejemplo o un ortoedro). Los dos se usan como valores vigentes hasta que la confirmación llegue, y los dos declaran esa condición en su archivo.

Todos los demás criterios de éxito de este catálogo son de recuento o binarios y están declarados en las fuentes del intake, de modo que un cambio de A-2 alcanza sólo a esos dos.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del catálogo. Ocho necesidades de negocio derivadas de las veintiuna capacidades de PRODUCT-INTAKE §4 y de las tres métricas de §8, con el criterio de recorte, sus cinco fusiones y sus cuatro particiones justificadas, el mapa de dependencias acíclico con su verificación, la trazabilidad agregada de capacidad a NB, de métrica a NB y de NB a caso de uso previsto, y la declaración de los dos targets pendientes de confirmación. | Analista de Negocio Senior (AG-01) |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgos **H-03** y **H-04**. H-03: las tres ocurrencias de «observación» de §3.2, §5.2 y §6 pasan a **«advertencia»**, término específico del glosario raíz (`Vision-Producto.md` §9.1); en particular el nombre de la métrica de §5.2 se alinea literalmente con el de `Vision-Producto.md` §6, para que la misma métrica no cambie de nombre al cruzar de categoría. H-04: §3.3 localizaba la parte de F-13 en «los criterios cuarto y quinto» de NB-06, y el quinto de entonces no era de F-13; la fila pasa a identificar los dos criterios por nombre, ya con el criterio de sincronización que NB-06 §5 incorporó. Ningún target, prioridad ni dependencia cambia. Las dos correcciones se absorben **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. | Analista de Negocio Senior (AG-01) |
