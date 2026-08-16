# Definition of Ready — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Definition-Of-Ready.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) 1.7 §3, §4 y §6; [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md) (las **36** condiciones); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §8 y §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Criterios DoR para historias de usuario](#1-criterios-dor-para-historias-de-usuario)
- [2. Criterios DoR para tareas técnicas](#2-criterios-dor-para-tareas-técnicas)
- [3. Excepciones admitidas](#3-excepciones-admitidas)
- [4. Aprobador](#4-aprobador)
- [5. Qué no es esta DoR](#5-qué-no-es-esta-dor)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Criterios DoR para historias de usuario

Siete criterios, todos respondibles con sí o no. Los tres últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto**, de las que [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §2.1 enumera.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Application/Arquitectura-Proyecto-Codigo.md) §3.1 que la sostiene** y los **puertos** que consume, de los cuatro de `02` §3.
5. **Declara cuál de las cuatro comprobaciones de `02` §4 la alcanza, o declara que ninguna la alcanza y por qué.** Una historia que no diga nada de la cuarta comprobación **no está lista**: es el camino por el que `INV-09` se pierde, y `Domain ADR-04005` §6 ya declaró que el dominio no puede impedirlo.
6. **Toda condición de rechazo que la historia produce existe en el catálogo de las 36** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md). Una historia que necesite una condición nueva **no está lista**: el catálogo es cerrado y se compara en las dos direcciones.
7. **Se puede verificar con dobles de los cuatro puertos, sin base de datos y sin frontera de proceso.** Si no se puede, o la historia está mal ubicada o algún componente está consultando por su cuenta, que es el primer riesgo de `05` §9.

## 2. Criterios DoR para tareas técnicas

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11 o una regla de delivery del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero dependencias salientes de más, cero pruebas que tocan la base, cero componentes cargados en el listado— el criterio se expresa con **umbral cero y con la condición en la que se mide**.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias de `05` §3.2: **ningún orquestador depende de otro orquestador**, y la guarda no lee conjuntos ni escribe.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas. Y si el punto abierto que cierra **es de otro proyecto de código**, lo declara: la tarea acompaña y no decide.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Tarea que **acompaña** un punto abierto cuya titularidad es de otro proyecto de código —BT-04020 y BT-04021— | El criterio 2 de §2 se cumple declarando de quién es la decisión y cuál es el plazo, en lugar de una historia consumidora | El Product Owner |
| Historia cuya verificación depende de uno de los dos valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-05` del backlog se cierre con BT-04018. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que agrega una operación que lee o escribe | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un camino que ejerza una capacidad sin resolver antes la marca es el riesgo de impacto **muy alto** de `05` §9, y una condición acuñada aguas abajo rompe la cobertura del catálogo en las dos direcciones | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son dos cosas:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15), que es donde se cierran los nombres de la etapa `a`, incluido el del cuarto puerto.
2. Las **puertas medidas** del pipeline, que no dependen de que alguien las revise: cero dependencias salientes de más, cero advertencias de construcción y **cero pruebas de esta capa que toquen la base de datos real**. La última es la más dura y es propia de este proyecto de código (`PRODUCT-INTAKE` §17.2.P.8).

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona la cobertura alcanzada, ni los 500 ms medidos, ni la matriz de las cuatro comprobaciones completa, que son condiciones de cierre.

**Tampoco redacta reglas ni invariantes.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; lo que esta DoR exige es que la historia los **cite por identificador** y declare el tramo que esta capa ejerce, que es lo que `02` §6 y `05` §10.2 ya repartieron.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **siete** criterios de entrada para las historias —tres de ellos propios de este proyecto de código: cuál de las cuatro comprobaciones la alcanza, que toda condición de rechazo exista en el catálogo cerrado de 36, y que la historia se pueda verificar con dobles y sin base de datos— y **cinco** para las tareas técnicas, incluido el que exige que toda tarea de indagación que cierre un punto abierto ajeno declare de quién es la decisión. Declara cuatro casos de excepción, uno de ellos negativo y sin excepción posible; el aprobador con la constancia de que el filtro más duro es la puerta de cero pruebas que tocan la base real; y la delimitación contra la Definition of Done, que vive en 08 y todavía no está emitida. |
