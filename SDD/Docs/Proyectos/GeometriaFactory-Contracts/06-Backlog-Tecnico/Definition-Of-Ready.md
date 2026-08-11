# Definition of Ready — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Definition-Of-Ready.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.2 (regla de exposición) y §9 (riesgos); [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.6 §5 y §6; [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Contracts

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

Siete criterios, todos respondibles con sí o no. El sexto y el séptimo son propios de este proyecto de código y no aparecen en la DoR de los otros dos de nivel 0: derivan de que acá lo que se decide es **qué cruza la frontera y qué no**.

1. **Traza a un contrato de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.
2. **Declara su necesidad de negocio y su etapa** del producto.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara la familia de tipos** de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1 que la sostiene.
5. **No redacta ninguna regla de negocio.** Las refiere por identificador a `GeometriaFactory-Domain`, como hace la categoría 02 en su §5. Una historia que enuncie una regla no está lista.
6. **Se refinó contra la regla de exposición** de `05` §3.2: ningún campo que la historia introduce puede transportar el hash de la contraseña, la clave de firma, una dirección de servicio interno, una ruta de archivo de datos ni una traza de la implementación.
7. **Todo código de error que la historia usa pertenece al conjunto cerrado** de [`../05-Arquitectura-Tecnica/Contratos-Abstractions.md`](../05-Arquitectura-Tecnica/Contratos-Abstractions.md) §5.1. Una historia que necesite un código nuevo no está lista: primero se da de alta donde el conjunto se declara.

## 2. Criterios DoR para tareas técnicas

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: una familia de tipos de `05` §3.1, una ADR, un NFR de su §8, una restricción transversal de `02` §6 o un punto abierto de su §11.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR o la puerta que la sostiene.
3. **Sus criterios de aceptación son verificables por inspección de la superficie pública o por prueba de integración.** En este proyecto de código no hay pruebas propias (`05` §5), de modo que un criterio que dependa de una prueba unitaria propia está mal formulado.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, lo que incluye no introducir una arista nueva entre familias sin declarar su motivo.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas**, no en horas.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el resultado esperado en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia de la fase `i…` | Los criterios 3 y 7 de §1 se difieren hasta que esa fase se planifique con la plantilla completa, como declara el roadmap §2.1. Alcanza hoy únicamente a **US-10** | El Product Owner, al planificar la fase `i…` |
| Historia que introduce un campo nuevo en un tipo existente | Ninguno: **no se admite excepción al criterio 6**. `05` §9 declara que agregar un campo de diagnóstico es la forma habitual en que ese defecto entra, y que entra sin que nadie lo note porque compila | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar a la etapa, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona es el **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15). Para este proyecto de código hay además un segundo filtro que no depende de nadie: **la compilación de los dos extremos**. Un cambio incompatible rompe la compilación de `GeometriaFactory-Api` y de `GeometriaFactory-Web` antes que el tiempo de ejecución, y ése es el mecanismo del versionado del producto ([`ADR-03`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Versionado-Por-Compilacion-Compartida.md)).

**Lo que ese filtro no atrapa está declarado y conviene tenerlo presente al aprobar**: `Vista-Producto.md` §7 registra como riesgo `RI-01` que los dos extremos se configuren distinto **sin romper ninguna compilación**, y lo califica como el único modo de falla del contrato que la compilación compartida no atrapa.

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que hace las veces de criterio de cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona la matriz de tipos ejercitados, ni el despliegue conjunto, ni la documentación al día, que son condiciones de cierre.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **siete** criterios de entrada para las historias —dos de ellos propios de este proyecto de código, la regla de exposición y el conjunto cerrado de códigos— y **cinco** para las tareas técnicas, todos respondibles con sí o no; tres casos de excepción, uno de ellos negativo y sin excepción posible; el aprobador con la constancia de que el segundo filtro real es la compilación de los dos extremos y con el modo de falla que ese filtro **no** atrapa; y la delimitación contra la Definition of Done, que vive en 08 y todavía no está emitida. |
