# ADR-12002 — La superficie pública son seis funciones planas, siete garantías y siete códigos

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-12002-Superficie-De-Seis-Funciones-Planas.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

Toda la superficie pública de este proyecto de código son **seis funciones planas**, que `PRODUCT-INTAKE` §17.2.P.3 · GeometriaFactory-Visor declara: `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento`.

Eran cinco hasta el 2026-08-09. La sexta la acuñó [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §4.6 por decisión del Product Owner tomada al cerrar la validación visual de la Fase B2, y el intake **la consolidó en su versión 1.6**, de modo que la fuente vuelve a ser única. El motivo de la sexta es arquitectónico y conviene tenerlo presente: prender o apagar los movimientos con la escena andando, dentro de las cinco originales, exigía **reconstruir la instancia**, lo que pierde la selección de pieza y produce un parpadeo.

Junto con las seis funciones, el contrato declara **siete garantías** transversales, **siete prohibiciones** y **siete códigos de condición** con lista cerrada. **La sexta función no acuñó garantía ni código**: la única condición que puede informar, `INSTANCIA_DESCONOCIDA`, ya existía y pasó a presentarse en cinco funciones.

Motivación upstream: NB-00006, NB-00004 parcial; capacidades F-11, F-13 y F-25; RA-02.

## 2. Decisión

**La superficie pública son seis funciones planas y nada más**, expuestas bajo un solo nombre propio en el objeto global del navegador, sin identificadores globales sueltos.

Cuatro decisiones de forma la acompañan:

1. **Instancias identificadas, no instancia global.** `inicializar` devuelve un identificador **opaco**; las otras cinco lo exigen. Es lo que sostiene la garantía G-4 de aislamiento entre instancias.
2. **Las siete garantías son parte del contrato**, no detalles de implementación: perder cualquiera es cambio mayor aunque las seis firmas no se toquen.
3. **Los siete códigos de condición son la lista cerrada**, y su fuente única es §6 del contrato de fachada. Un **curso** nuevo se agrega como fila de curso; un **código** nuevo sólo puede nacer en la categoría 02, y nunca aguas abajo.
4. **El resultado de dibujo no lleva observaciones.** Se llama así, y no «resultado de la interpretación», precisamente para que no se confunda con el que emite el backend, que sí lleva observaciones y decide si el trabajo puede finalizar.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Seis funciones planas con instancias identificadas (**adoptada**) | Superficie mínima y enumerable; dos escenas vivas en la misma página; el identificador opaco deja libre la representación interna | Cinco de las seis funciones llevan un parámetro de identificador; el anfitrión tiene que conservarlo |
| Una instancia global única, sin identificador | Firmas más cortas; el anfitrión no conserva nada | Rompe **G-4**: no habría dos escenas vivas en la misma página, y `destruir` sería ambiguo. Además obligaría al bundle a tener estado global, que es lo contrario de lo que su versión anterior corrigió |
| Reconstruir la instancia para cambiar el movimiento, en lugar de agregar la sexta función | Cinco funciones en lugar de seis | **Pierde la selección de pieza y produce un parpadeo**, y obliga al anfitrión a reponer la selección. Es exactamente el problema que la decisión del Product Owner del 2026-08-09 resolvió |
| Un código de condición por función en lugar de por condición | Cada entrada de catálogo tendría su código propio | Multiplicaría los códigos sin agregar información: `INSTANCIA_DESCONOCIDA` significa lo mismo en las cinco funciones que lo informan, y el anfitrión hace lo mismo en las cinco. Es la distinción entre unidad de contrato —la condición— y unidad de catálogo —la función— que la categoría 03 ya declara |

## 5. Consecuencias positivas

1. La superficie es **enumerable de un vistazo**: seis funciones, siete garantías, siete códigos.
2. El anfitrión puede gobernar los dos movimientos **sin reconstruir la instancia** y sin perder la selección.
3. La lista cerrada de códigos permite que 03 crezca en entradas de diagnóstico —de doce a trece— **sin** que el contrato crezca, y esa distinción queda escrita.
4. El identificador opaco deja libre la representación interna: cambiarla no es cambio de contrato.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que agregar una función sea posible y ocurra.** Ya ocurrió una vez. Es cambio menor —no rompe a ningún anfitrión escrito contra las anteriores— pero cada una amplía la superficie que hay que sostener.
2. **Se acepta que el anfitrión tenga trabajo que la fachada no le quita**: dibujar los controles de movimiento, consultar la preferencia de movimiento reducido del sistema, conservar la elección y presentar el árbol a partir de la estructura que `cargarJson` devuelve.
3. **Se acepta que el resultado de dibujo conserve la enumeración de las piezas no dibujadas aunque el anfitrión no la use.** Es la garantía G-5 y es lo que cierra el problema original de NB-00006.
4. **Se acepta que el contrato lo gobierne la categoría 02.** Esta ADR fija el criterio de crecimiento, no la lista.

## 7. Implementación

- Las seis funciones, con la semántica que fija [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §4, y su resumen operativo en [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3.
- El identificador de instancia **deja de ser válido en cuanto `destruir` retorna, y no se reutiliza** para una instancia nueva.
- **`establecerMovimiento` es idempotente**: fijar el estado que ya estaba no cambia nada. Y el movimiento **no nombrado conserva el que tenía**, a diferencia de `inicializar`, donde lo ausente arranca apagado.
- Los nombres de las **funciones internas**, de las clases y de los campos del resultado **no se fijan acá**; los de las seis funciones de la fachada **sí están fijados** por el intake.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Funciones expuestas | Exactamente **6** | Inspección del bundle generado |
| Identificadores globales sueltos | Exactamente **0**, bajo **1** nombre propio | Inspección del bundle generado |
| Códigos de condición | Exactamente **7** | Inspección contra §6 del contrato de fachada |
| Garantías verificadas | **7 de 7** con al menos una prueba cada una | Matriz garantía contra prueba en 08 |
| Piezas no dibujadas enumeradas | **100 %**, con **0** desapariciones sin registro | Escenarios E-1 y E-7 |
| Identificador de instancia reutilizado tras `destruir` | Exactamente **0** veces | Prueba de ciclo de vida |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.2.P.2 · GeometriaFactory-Visor, §17.2.P.3 · GeometriaFactory-Visor, §17.2.P.11 · GeometriaFactory-Visor punto 3, §18.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §5, fundamento de `tiene_extensibilidad` con la enumeración de las seis funciones.
- [`../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2, §4, §5.1, §5.2, §6 y §7.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §3.2, por la distinción entre unidad de contrato y unidad de catálogo.
- ADR relacionadas: [`ADR-12001`](ADR-12001-Tres-Capas-Con-Fachada-Plana.md), [`ADR-12003`](ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la superficie de seis funciones planas con instancias identificadas, las siete garantías como parte del contrato, la lista cerrada de siete códigos con la distinción entre curso y código, la separación entre resultado de dibujo y resultado de la interpretación del backend, cuatro alternativas evaluadas y seis métricas de validación. |
