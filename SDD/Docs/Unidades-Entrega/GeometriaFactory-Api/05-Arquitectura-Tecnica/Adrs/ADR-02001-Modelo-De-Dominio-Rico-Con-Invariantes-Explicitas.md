# ADR-02001 — Modelo de dominio rico con invariantes explícitas y cero dependencias

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

`GeometriaFactory-Domain` es el nivel 0 del orden topológico del producto y el centro de la regla de dependencias (`PRODUCT-MANIFEST` §3). El producto tiene **dieciséis** reglas de negocio y **nueve** invariantes, y su pieza con más reglas verificadas —el validador de figuras— vive fuera de este proyecto de código, detrás de un puerto (`PRODUCT-INTAKE` §14).

Lo que motiva la decisión: `PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain exige que los invariantes y las transiciones de estado se puedan probar **sin infraestructura**, y §17.1.P.1 · GeometriaFactory-Domain declara la condición estructural que lo permite, «`Domain` sin dependencias». Aguas abajo, la regla de no-regresión acumulativa del producto (`PRODUCT-INTAKE` §15) hace que el costo de ejercer esas pruebas se pague en cada etapa, de modo que su velocidad es una propiedad arquitectónica y no un detalle.

Motivación upstream: NB-00001, NB-00002, NB-00003, NB-00004, NB-00005 y NB-00009, que son las seis necesidades a las que trazan los trece casos de uso de la categoría 02; RN-02001 a RN-02016; INV-01 a INV-09.

## 2. Decisión

Se adopta un **modelo de dominio rico**: las entidades sostienen sus propias invariantes y las transiciones de estado son operaciones de la entidad, no de un servicio externo. El proyecto de código declara **cero dependencias salientes** —ni hacia otros proyectos de código del producto, ni hacia bibliotecas de persistencia, transporte o serialización— y esa ausencia es una puerta bloqueante de construcción, no una recomendación.

Los agregados son **dos y no uno**: la cuenta y el trabajo. Ninguna invariante liga el estado de una cuenta con el estado de un trabajo, de modo que no hay frontera de consistencia que justifique unirlos.

## 3. Estado

**Propuesto** desde 2026-08-10. Deriva de una decisión ya tomada aguas arriba (`PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain), de modo que lo que esta ADR agrega es su registro formal, la partición en dos agregados y la puerta de verificación.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Modelo de dominio rico con dos agregados (**adoptada**) | Los nueve invariantes se prueban sin infraestructura; cada agregado carga sólo lo que su operación necesita; la biblioteca compila y se prueba sin ningún otro proyecto de código | Más tipos y más ceremonia que un modelo anémico; obliga a que el consumidor resuelva antes lo que exige conocer el conjunto |
| Modelo anémico, con la lógica en los servicios de aplicación | Menos tipos; toda la lógica junta y fácil de recorrer | Los invariantes se irían al nivel 1, y probarlos exigiría la capa de aplicación: se pierde exactamente la propiedad que el intake declara como motivo (§17.1.P.2 · GeometriaFactory-Domain) |
| Entidades del proveedor de persistencia como modelo de dominio | Un solo juego de tipos entre dominio y base; menos mapeo | Obliga a referenciar una biblioteca de persistencia desde el nivel 0 y viola la regla de dependencias hacia adentro (`PRODUCT-INTAKE` §17.1.P.2 · GeometriaFactory-Domain) |
| Un agregado único que abarque cuenta y trabajo | Una sola puerta de consistencia, imposible de saltear | Ninguna de las nueve invariantes cruza las dos entidades, así que no compra consistencia; y cargaría la cuenta entera en cada operación sobre un trabajo |

## 5. Consecuencias positivas

1. Los **nueve** invariantes son verificables con pruebas unitarias puras y sin dobles, que es lo que `PRODUCT-INTAKE` §17.1.P.6 · GeometriaFactory-Domain declara como estrategia de este proyecto de código.
2. La regla de no-regresión acumulativa se ejerce barata: la batería no levanta base de datos ni servidor.
3. Un cambio de proveedor de persistencia o de marco de transporte no toca este proyecto de código.
4. La partición en dos agregados deja explícito qué se carga en cada operación, y evita el bloqueo de una cuenta entera por una operación sobre un trabajo.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta la duplicación aparente entre entidades y tipos de transferencia.** Es deliberada y es lo que impide que un cambio de dominio rompa el contrato que cruza la frontera de proceso (`PRODUCT-INTAKE` §17.1.P.12 · GeometriaFactory-Domain).
2. **Se renuncia a anotar las entidades con atributos de mapeo y de serialización.** El costo se paga en el proyecto de código que persiste, que tiene que declarar la correspondencia por su cuenta.
3. **Se acepta que INV-01 no se pueda ejercer entero acá.** La unicidad del correo se afirma sobre el conjunto de alumnos, que una entidad no conoce: el dominio declara la condición y exige que el consumidor la haya resuelto.
4. **Dos agregados obligan a que la atomicidad la establezca el consumidor** cuando una operación toca los dos, como la baja de una cuenta que arrastra sus trabajos (RN-02007).

## 7. Implementación

- Un archivo de proyecto **sin ninguna referencia** a otro proyecto de código del producto ni a paquetes de persistencia, transporte o serialización.
- Las cinco entidades de [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2, con sus atributos declarados como estado propio y no público de escritura libre.
- Los cinco componentes de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1, con el grafo acíclico que esa misma sección declara.
- Los nombres de tipos y de espacios de nombres **no se fijan acá**: quedan abiertos hasta el punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11 · GeometriaFactory-Domain).

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Dependencias salientes | Exactamente **0** | Inspección del archivo de proyecto, bloqueante en revisión |
| Tiempo de la batería del dominio | Menos de **10 segundos** [ASUNCIÓN del intake, §17.1.P.10 · GeometriaFactory-Domain] | Duración total reportada en la etapa de `test` |
| Cobertura | **90 %** de líneas y **85 %** de ramas [ASUNCIÓN del intake, §17.1.P.6 · GeometriaFactory-Domain] | Informe de cobertura del pipeline |
| Invariantes ejercitados | **9 de 9**, cada uno con al menos una prueba de violación rechazada | Matriz invariante contra prueba en 08 |
| Dobles de prueba en la batería del dominio | Exactamente **0** | Inspección de la batería: si hace falta un doble, hay una dependencia que no debería existir |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.1 · GeometriaFactory-Domain, §17.1.P.2 · GeometriaFactory-Domain, §17.1.P.6 · GeometriaFactory-Domain, §17.1.P.10 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain y §17.1.P.12 · GeometriaFactory-Domain; §14; §15.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §2, §3 y §5.
- [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2, §4 y §5.
- ADR relacionadas: [`ADR-02002`](ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md), [`ADR-02005`](ADR-02005-Guarda-Unica-De-Admisibilidad.md), [`ADR-02006`](ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el modelo de dominio rico con cero dependencias salientes, la partición en dos agregados con su fundamento, las cuatro alternativas evaluadas y las cinco métricas de validación. |
