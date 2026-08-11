# Definition of Ready — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Definition-Of-Ready.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2 y §6; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §8 y §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §2.2 y §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Visor

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

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.
2. **Declara su necesidad de negocio y su momento del producto**: la etapa `a`, el momento de medición de `PT-02` y `PT-03`, o la etapa `g`.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 que la sostiene**, y su capa.
5. **Declara qué garantías del contrato de fachada ejerce**, de las **siete** de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2, y ninguna de sus afirmaciones contradice a otra garantía.
6. **Todo código de condición que la historia usa es uno de los siete** de ese mismo documento, §6. Una historia que necesite un código nuevo **no está lista**: un código sólo puede nacer allá, y un curso nuevo se agrega como fila de curso y no como código.
7. **Ninguna persona, papel, servicio ni credencial interviene como actor ni condiciona un flujo.** Nombrarlos para declarar qué queda fuera del contrato es obligatorio; que intervengan es un defecto por definición (`02` §2).

## 2. Criterios DoR para tareas técnicas

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, una puerta técnica del intake §15 o un punto abierto de su §11.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR o la puerta que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia**, el criterio se expresa con umbral cero y con la condición en que se mide. Un criterio de ausencia sin condición de medición **no está listo**: mediría el caso fácil (`02` §6).
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias entre capas: la capa 1 no conoce el interior, la capa 2 no contiene lógica de dibujo y la capa 3 no conoce al anfitrión.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el momento de medición que la cierra**, no en horas.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el resultado esperado en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende del umbral de fluidez, que no existe | El criterio 3 de §1 se cumple con verificación **cualitativa declarada**, junto con `PT-02`, hasta que `PA-04` del backlog se cierre. Es la salida que `05` §8 declara, y no habilita a inventar un número | El Product Owner, o 08 al fijar su guion de medición |
| Historia que introduce comportamiento en la capa 3 | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Perder una garantía es un cambio mayor aunque las seis firmas no se toquen (`05` §10.2), y acuñar un código aguas abajo desincroniza 02, 03 y 08 (`05` §9, sexto riesgo) | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son dos cosas, y en este proyecto de código la segunda es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **dos puertas técnicas medidas sobre el artefacto generado**, `PT-02` y `PT-03`. No dependen de que alguien las revise: se miden, y una puerta que no pasa **detiene la planificación de la etapa `g`** en lugar de arrastrarse como deuda (`Roadmap-Producto.md` §2.2).

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5 y las dos puertas técnicas. Esta DoR habla de **cuándo empezar**: no menciona los diez recorridos sin degradación, ni la medición de peticiones con los movimientos prendidos, ni la página integradora funcionando, que son condiciones de cierre.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **siete** criterios de entrada para las historias —tres de ellos propios de este proyecto de código: las garantías que ejerce, el conjunto cerrado de siete códigos y la prohibición de que una persona o un servicio intervengan como actor— y **cinco** para las tareas técnicas, incluido el que exige que todo criterio de ausencia declare su condición de medición. Declara tres casos de excepción, uno de ellos negativo y sin excepción posible; el aprobador con la constancia de que el filtro más duro son las dos puertas medidas sobre el artefacto generado; y la delimitación contra la Definition of Done, que vive en 08 y todavía no está emitida. |
