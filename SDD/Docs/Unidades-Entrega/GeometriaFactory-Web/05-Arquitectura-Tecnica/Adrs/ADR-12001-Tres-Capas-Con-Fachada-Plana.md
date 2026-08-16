# ADR-12001 — Tres capas con fachada plana, y el motor de dibujo confinado a la capa interna

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ADR-12001-Tres-Capas-Con-Fachada-Plana.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

`PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Visor declara **tres capas obligatorias** y dice explícitamente que son «el motivo por el que existe la fachada»: el componente anfitrión, que vive en `GeometriaFactory-Web` y no conoce el motor de dibujo ni los nombres internos del bundle; la fachada externa, que expone funciones planas y **no contiene lógica de dibujo**; y el servicio del visor, que tiene la escena, las mallas, el árbol y la disposición y **no conoce al anfitrión**.

El punto de extensión declarado del producto **es** ese contrato de fachada (`PRODUCT-INTAKE` §18), y `tiene_extensibilidad` es true sólo en este proyecto de código (`PRODUCT-MANIFEST` §5). Lo que la extensión compra es concreto: **el motor de dibujo tridimensional es reemplazable sin tocar ninguna página**.

Motivación upstream: NB-00006, y NB-00004 en su parte de piezas efectivamente dibujadas; capacidades F-11, F-13 y F-25; RA-02.

## 2. Decisión

Se adopta un **estilo de microkernel con fachada plana en tres capas**, con la regla de dependencias estricta y unidireccional:

1. La **capa 1**, el componente anfitrión, invoca sólo las seis funciones y **no conoce nada del interior**. Vive fuera de este proyecto de código.
2. La **capa 2**, la fachada, resuelve el identificador de instancia y traduce invocaciones en órdenes. **No contiene lógica de dibujo.**
3. La **capa 3**, el servicio de dibujo y su lector de texto, tiene todo el conocimiento gráfico y **no conoce al anfitrión**. El motor de dibujo tridimensional queda confinado acá y **nunca aparece en la superficie pública**.

## 3. Estado

**Propuesto** desde 2026-08-10. Las tres capas y sus dos alternativas descartadas vienen del intake; lo que esta ADR agrega es el registro formal, la partición interna de las capas 2 y 3 en cuatro componentes y las métricas de verificación.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Tres capas con fachada plana (**adoptada**) | El motor de dibujo es reemplazable sin tocar ninguna página; la fachada se puede ejercer entera sin backend; el anfitrión se prueba contra seis funciones y no contra una escena | Una capa de indirección más; toda capacidad nueva del motor que el anfitrión necesite tiene que pasar por la fachada |
| Exponer el servicio de dibujo directamente al anfitrión | Una capa menos; acceso inmediato a todo lo que el motor ofrece | Ata las páginas a los nombres internos del motor y **lo vuelve irreemplazable**, que es exactamente lo contrario del punto de extensión declarado. Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Visor |
| Portar el archivo del visualizador previo tal cual | Costo casi nulo; ya funciona | Arrastraría **527 de 1101 líneas** de código inactivo —el **48 %**— y dos controles inoperantes. Descartada por `PRODUCT-INTAKE` §17.2.P.2 · GeometriaFactory-Visor |
| Un componente web autónomo en lugar de una fachada de funciones | El anfitrión lo usaría como un elemento más de la página, sin invocaciones explícitas | Movería a la capa 3 decisiones de presentación que son del anfitrión —ubicación, tamaño, estilo del elemento de dibujo— y haría que el ciclo de vida lo gobernara el navegador y no el anfitrión, que es quien sabe cuándo hay que liberar |

## 5. Consecuencias positivas

1. **El motor de dibujo se puede reemplazar sin tocar ninguna página**, que es el valor concreto del punto de extensión.
2. La fachada se ejerce **entera sin ninguna pieza del backend**, que es la propiedad que el visualizador previo ya tenía y que el intake exige no perder.
3. El anfitrión se prueba contra seis funciones, y no contra una escena tridimensional.
4. La liberación de recursos tiene un solo dueño —la capa 3— y una sola puerta —`destruir`—, que es lo que hace medible el recorrido de ida y vuelta de `PT-02`.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta una capa de indirección.** Toda capacidad del motor que el anfitrión necesite tiene que atravesar la fachada, y eso hace más caro exponerla. Es deliberado: es el costo de que el motor sea reemplazable.
2. **Se acepta reescribir el port en lugar de copiar el archivo original**, con el costo de trabajo que implica (`PRODUCT-INTAKE` §17.2.P.12 · GeometriaFactory-Visor).
3. **Se acepta que la capa 1 no sea de este proyecto de código.** El anfitrión vive en `GeometriaFactory-Web` y su categoría 03 documenta la superficie donde la escena queda embebida; acá sólo se declara el contrato entre las dos.
4. **Se acepta que agregar una función a la fachada sea un cambio menor y no un evento raro.** Ya pasó una vez, con la sexta función, y el proceso está declarado en [`../Extensibilidad.md`](../Extensibilidad.md) §5.

## 7. Implementación

- Los seis componentes de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1, con el grafo acíclico que esa sección declara.
- **El archivo de fachada no importa el motor de dibujo.** Es verificable por inspección y es la forma concreta en que «la capa 2 no contiene lógica de dibujo» se comprueba.
- **La capa 3 no recibe ninguna referencia al anfitrión**: no toca la página más allá del elemento de dibujo que se le entregó.
- El bundle expone **un solo nombre propio** en el objeto global del navegador, sin identificadores globales sueltos.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Referencias al motor de dibujo en la capa 2 | Exactamente **0** | Inspección del archivo de fachada |
| Referencias al anfitrión en la capa 3 | Exactamente **0** | Inspección de los módulos de la capa 3 |
| Nombres propios expuestos en el objeto global | Exactamente **1**, con **0** identificadores globales sueltos | Inspección del bundle generado |
| Recorridos de ida y vuelta sin degradación | **10**, con los dos movimientos prendidos | Puerta técnica `PT-02` |
| Recorrido completo de la fachada sin backend | **6 de 6** funciones, con **0** servicios disponibles | Sample S-1 |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.2.P.2 · GeometriaFactory-Visor, §17.2.P.10 · GeometriaFactory-Visor, §17.2.P.11 · GeometriaFactory-Visor punto 3, §17.2.P.12 · GeometriaFactory-Visor, §18 y §15 (`PT-02`).
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §5 (`tiene_extensibilidad`).
- [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §1, §3.1 y §3.2.
- ADR relacionadas: [`ADR-12002`](ADR-12002-Superficie-De-Seis-Funciones-Planas.md), [`ADR-12004`](ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el estilo de tres capas con fachada plana, la regla de dependencias unidireccional, el confinamiento del motor de dibujo a la capa interna, cuatro alternativas evaluadas —incluido el componente web autónomo, que esta categoría agrega y descarta— y cinco métricas verificables por inspección. |
