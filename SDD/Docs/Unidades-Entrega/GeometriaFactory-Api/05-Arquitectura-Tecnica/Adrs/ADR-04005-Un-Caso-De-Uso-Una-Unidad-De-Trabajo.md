# ADR-04005 — Un caso de uso, una unidad de trabajo: el alcance lo fija esta capa

**Proyecto de código:** GeometriaFactory-Application
**Documento:** ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Persistencia

---

## 1. Contexto

Este proyecto de código declara su persistencia como «no aplica directamente» y su flag `tiene_persistencia` es false: no abre conexiones, no conoce el motor y no escribe. Pero **sí decide dos cosas sobre la persistencia**, y no decidirlas acá las dejaría sin dueño:

1. **Dónde empieza y dónde termina la atomicidad.** El dominio no abre ni cierra unidades de trabajo —lo declaró la Fase C del nivel 0— y el adaptador no sabe qué operaciones forman un acto. Si el alcance lo fijara el adaptador, cada llamada al repositorio sería su propia transacción, y la baja de una cuenta podría dejar la cuenta dada de baja con la mitad de sus trabajos todavía en pie.
2. **Qué forma tiene la consulta.** Las consultas de listado **nunca cargan los componentes de las piezas**, que es una decisión de modelado con efecto directo en el tiempo de respuesta del listado del administrador.

El caso testigo es `RN-04007`: la baja arrastra **todos** los trabajos de la cuenta, en cualquier estado, y exige la confirmación escrita del correo. Son tres efectos —comparar, retirar, cambiar la situación— que o pasan juntos o no pasa ninguno.

Motivación upstream: NB-00001, NB-00003, NB-00007, NB-00009; RN-04004, RN-04007, RN-04011, RN-04012; INV-03; `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Application y §17.1.P.10 · GeometriaFactory-Application.

## 2. Decisión

**Cada caso de uso abre a lo sumo una unidad de trabajo y no reparte su efecto entre varias.** El **alcance** lo fija esta capa; el **mecanismo** —cómo se abre, cómo se confirma, cómo se revierte— es del adaptador que implementa el puerto de repositorio, y esta capa no lo nombra.

**Ningún caso de uso invoca a otro caso de uso** para componer un efecto mayor: si dos efectos tienen que ser atómicos, viven en el mismo caso de uso.

Y sobre la forma de la consulta: **las consultas de listado no materializan los componentes de las piezas**, y **el predicado de alcance se traslada a la consulta** en lugar de aplicarse después de traerla.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Un caso de uso, una unidad de trabajo, con el alcance fijado acá (**adoptada**) | El acto de negocio y el límite de consistencia coinciden; el arrastre de la baja es atómico por construcción; el adaptador queda libre de decidir qué forma un acto | Un caso de uso que crezca demasiado arrastra una unidad de trabajo larga, y esta capa no tiene forma de medir cuánto dura |
| Que el alcance lo fije el adaptador, una transacción por llamada al repositorio | El adaptador es quien conoce el motor y podría optimizar | La baja podría quedar a medias, y `RN-04007` dejaría de valer sin que ninguna prueba de esta capa lo notara. El límite de consistencia dejaría de ser una decisión de diseño y pasaría a ser un efecto secundario |
| Que el alcance lo fije quien atiende la petición, una transacción por petición | Un solo lugar para todo el producto, alineado con el ciclo de la petición | Ataría el límite de consistencia al protocolo: una operación invocada fuera de una petición no tendría alcance. Además dejaría a esta capa sin poder declarar la atomicidad que sus postcondiciones prometen |
| Componer efectos invocando un caso de uso desde otro | Reúso obvio: la baja podría invocar al retiro de trabajos | Produciría unidades de trabajo anidadas, o dos unidades donde la postcondición promete una. Es la vía por la que el arrastre parcial vuelve |

## 5. Consecuencias positivas

1. `RN-04007` se materializa como propiedad estructural y no como cuidado del programador: la baja es atómica porque el caso de uso es la unidad.
2. `RN-04012` se verifica por el mismo camino y en sentido contrario: el reseteo **no** dispara ningún retiro, y su postcondición declara todos los trabajos conservados.
3. El adaptador queda libre de decidir qué forma un acto, que es información que no tiene.
4. El listado del administrador no arrastra los componentes de cada pieza, lo que coincide con la proyección que `GeometriaFactory-Contracts` separó del detalle y evita que el contrato y la capa de aplicación se contradigan.
5. El predicado de alcance trasladado a la consulta hace que un borrador **no se traiga**, en lugar de traerse y filtrarse: `RN-04011` deja de depender de un filtro en memoria.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la baja de una cuenta con muchos trabajos abra una unidad de trabajo larga**, y que esta capa no pueda medir cuánto dura porque no conoce el motor. La medición, si hace falta, es de `GeometriaFactory-Infrastructure` y de 08.
2. **Se acepta que no haya reúso entre casos de uso.** El retiro de trabajos aparece en el caso de uso de eliminación y, como arrastre, en el de gobierno de cuentas. Se acepta la repetición del acto a cambio de que la postcondición de cada caso de uso sea verdadera por sí sola.
3. **Se acepta que el puerto de repositorio tenga que ofrecer la proyección de listado ya recortada**, en lugar de que el caso de uso arme la consulta. Es la contrapartida de la inversión de dependencias, y se paga en la forma del puerto.

## 7. Implementación

- Los **seis** orquestadores de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 abren a lo sumo una unidad de trabajo cada uno; ninguno invoca a otro.
- La unidad se abre **después** de que la guarda de autorización autorizó y **antes** de la primera escritura: la comprobación se hace sobre el dato recuperado y antes de escribir.
- El caso de uso de gobierno de cuentas es el testigo: comparación del correo escrito, retiro de todos los trabajos y cambio de situación, en la misma unidad.
- El puerto de repositorio de trabajos ofrece **dos** formas de lectura distintas: la proyección de listado —sin texto original, sin componentes y sin comentario— y el detalle completo. No es una optimización: es la forma del contrato.
- Convención impuesta al adaptador: la unidad de trabajo es **un contexto por operación**, según `PRODUCT-INTAKE` §17.1.P.4 · GeometriaFactory-Application, que del lado de esta capa se expresa como un caso de uso, una transacción.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Unidades de trabajo por caso de uso | **A lo sumo 1** | Inspección de los seis orquestadores |
| Casos de uso que invocan a otro caso de uso | Exactamente **0** | Inspección de dependencias entre orquestadores |
| Trabajos huérfanos tras una baja interrumpida | Exactamente **0** | Prueba del arrastre con doble que falla a mitad del retiro, comprobando que nada quedó aplicado |
| Trabajos retirados por un reseteo | Exactamente **0** | Prueba de `RN-04012`, con la cuenta y todos sus trabajos conservados |
| Componentes de pieza materializados en las consultas de listado | Exactamente **0**, en el listado del alumno y en el de la comisión | Inspección de la proyección devuelta, y prueba que comprueba que la colección de componentes no viene materializada |
| Borradores traídos y filtrados en memoria en el listado de la comisión | Exactamente **0**: no se traen | Inspección del predicado que el caso de uso entrega al puerto |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §17.1.P.4 · GeometriaFactory-Application, §17.1.P.10 · GeometriaFactory-Application y §4.1 (RN-04004, RN-04007, RN-04011, RN-04012).
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, última precisión.
- [`../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §7, donde el dominio deriva la atomicidad al puerto de repositorio que esta capa declara.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md`](../../../../Producto/Adrs/ADR-08005-Proyeccion-De-Listado-Separada-Del-Detalle.md), la decisión equivalente del otro lado de la frontera.
- ADR relacionadas: [`ADR-04001`](ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-04002`](ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra que el alcance de la unidad de trabajo lo fija esta capa y el mecanismo el adaptador, con el arrastre de la baja como caso testigo, evalúa cuatro alternativas, declara tres trade-offs, fija la forma de las dos lecturas del puerto de repositorio y seis métricas de validación. |
