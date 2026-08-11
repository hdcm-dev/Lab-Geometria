# ADR-04 — Orden fijo de las cuatro comprobaciones de autorización, en un único componente

**Proyecto de código:** GeometriaFactory-Application
**Documento:** ADR-04-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

Esta capa **autoriza y no autentica**: quién es la persona llega ya resuelto desde afuera, y lo que acá se decide es si ese pedido concreto procede. Son **cuatro** comprobaciones, declaradas en [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4: pertenencia, facultad, alcance del administrador y **cambio de contraseña pendiente**.

Hay un antecedente que pesa y que viene del nivel 0. [`Domain ADR-05`](../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) decidió que la admisibilidad es la puerta única de las guardas de acceso de la cuenta, y **declaró en su §6 que el dominio no puede impedir que exista un camino que ejerza una capacidad sin resolver la admisión**. Esa dependencia de disciplina cae acá: si el camino existiera, tendría que volver a comprobar la marca, y esa comprobación no sería del dominio. Es exactamente lo que esta ADR viene a cerrar.

El orden importa por dos motivos distintos, y conviene no confundirlos. El primero es de corrección: `INV-09` enuncia que una cuenta con la marca puesta no ejerce **ninguna** capacidad, ni siquiera las que su papel y su pertenencia admitirían, de modo que comprobar la marca después de la pertenencia devolvería la negativa equivocada. El segundo es de filtración: la negativa por pertenencia oculta la existencia del recurso y la negativa por facultad no tiene nada que ocultar, así que ejercerlas en el orden equivocado permitiría averiguar por tanteo qué identificadores existen.

Motivación upstream: NB-01, NB-02, NB-03, NB-07, NB-09; RN-01, RN-03, RN-06, RN-10, RN-11, RN-13, RN-15, RN-16; INV-02, INV-03, INV-06, INV-07, INV-09; `PRODUCT-INTAKE` §17.2.P.5 y §17.1.P.2.

## 2. Decisión

**Las cuatro comprobaciones se ejercen en un único componente —la guarda de autorización— y en un orden fijo**, sobre el dato ya recuperado y **antes** de cualquier escritura:

1. **Cambio de contraseña pendiente.** Corta antes que las otras tres. Tiene **una sola excepción declarada**: el reemplazo de la propia credencial de `CU-03` FA-05, que es lo único que la cuenta marcada alcanza y lo único que levanta la marca.
2. **Pertenencia**, cuando el pedido es sobre un trabajo y quien pide es un alumno.
3. **Facultad**, cuando la operación es reservada al papel `Administrador`.
4. **Alcance del administrador**, que excluye los trabajos en `Borrador` de lo que el administrador ve y opera.

**Ninguna otra comprobación de autorización vive fuera de ese componente**, y **ningún orquestador vuelve a comprobar lo que la guarda ya comprobó**.

Además: **la negativa por pertenencia y la negativa por facultad no se colapsan y no se intercambian.** La primera responde con el motivo de trabajo inexistente para el solicitante —que el consumidor traduce a «no encontrado» y **nunca** a «no autorizado»—; la segunda sí admite ser explícita, porque no hay recurso ajeno cuya existencia proteger.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Orden fijo en un único componente (**adoptada**) | Cierra la dependencia de disciplina que el nivel 0 declaró que no podía cerrar; un solo lugar que auditar; el invariante se verifica con una prueba por comprobación y una por el orden | Depende de que ningún orquestador se saltee la guarda, y eso no lo garantiza el compilador |
| Las cuatro comprobaciones repartidas en cada orquestador | No hay componente transversal que recordar; cada caso de uso se lee entero | **Once** lugares que mantener sincronizados. Es la misma forma en que se abrieron el P0 y el P1 del nivel 0, con la diferencia de que allá eran cuatro operaciones y acá serían once |
| Comprobaciones en la capa que expone, con esta capa confiando en lo ya verificado | La capa que expone tiene la petición entera y podría cortar antes, sin recuperar el dato | La autorización por pertenencia dejaría de ser verificable sin infraestructura, que es la propiedad que `PRODUCT-INTAKE` §17.2.P.5 y §17.2.P.6 exigen. Además el front **no puede ser la última defensa de ninguna regla**, y quien atiende la petición tampoco tiene la entidad recuperada |
| Comprobaciones sin orden fijo, resueltas por conjunto de motivos | Devolvería todas las causas de una vez, que a veces es más útil | Rompe `INV-09`: una cuenta con la marca puesta recibiría además motivos que revelan qué existe y qué no. El orden **es** parte de la corrección, no una optimización |

**La tercera alternativa merece una precisión.** No se descarta por desconfiar de la capa que expone, sino porque desplazar la comprobación allá la volvería inverificable con dobles. Nada impide que la capa que expone **también** acote lo que ofrece: eso es una decisión de presentación legítima, y `GeometriaFactory-Web` la toma. Lo que no puede es reemplazar a ésta.

## 5. Consecuencias positivas

1. `INV-09` queda con una puerta cerrada del lado que el nivel 0 declaró que no podía cerrar, y con una sola prueba que verifica que la cuarta comprobación corta antes que las otras tres.
2. Las tres negativas de autorización quedan distinguibles y auditables en un solo lugar, que es lo que la categoría 03 pide en su tabla de traducciones prohibidas.
3. Agregar una capacidad nueva al producto no obliga a agregarle cuatro comprobaciones: le basta con pasar por la guarda.
4. La verificación de pertenencia se prueba con dobles y sin base, que es la razón declarada de que `tiene_auth` valga true en este proyecto de código.
5. Aguas abajo, la capa que expone traduce **un solo código** para todas las operaciones bloqueadas por la marca, en lugar de uno por operación —que es lo que el contrato del producto ya decidió con su código único de cambio de contraseña requerido—.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta una dependencia de disciplina hacia adentro.** El compilador no impide que un orquestador nuevo se saltee la guarda. La mitigación es de inspección: la verificación estructural de §7 y el NFR de las cuatro comprobaciones ejercitadas.
2. **Se acepta que la marca se comprueba antes de saber si el recurso existe**, de modo que una cuenta marcada que pide un trabajo ajeno recibe el motivo de la marca y no el de inexistencia. Es deliberado: revelar cualquier otra cosa a una cuenta que no debe ejercer ninguna capacidad sería exactamente lo que `INV-09` prohíbe.
3. **Se acepta que la excepción del reemplazo de credencial quede declarada y no derivada.** Es una excepción a una regla de orden, y las excepciones a reglas de orden son el lugar donde vuelven los defectos: por eso se escribe en la decisión y no en una nota.

## 7. Implementación

- El componente **Guarda de autorización** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que ejerce las cuatro comprobaciones.
- La guarda **no lee el conjunto y no escribe**: trabaja sobre la entidad ya recuperada por el orquestador, que es lo que la hace ejercitable con dobles.
- **Una sola negativa de facultad.** El dominio declara dos códigos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador— y esta capa emite uno solo, porque corta con su propia verificación **antes** de invocar al dominio. Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.
- El **alcance del administrador** se traslada a la consulta y no se aplica después de traerla: un borrador no se filtra en memoria, no se trae.
- Verificación estructural sugerida a 08: recorrer los seis orquestadores y comprobar que ninguno ejerce una comprobación de autorización por su cuenta, y que ninguno invoca al dominio antes de pasar por la guarda.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Componentes que ejercen comprobaciones de autorización | Exactamente **1** | Inspección de los ocho componentes |
| Comprobaciones ejercitadas con prueba de su negativa, sin base de datos | **4 de 4** | Matriz comprobación contra prueba en 08 |
| Pruebas que verifican que la cuarta comprobación corta antes que las otras tres | Al menos **1**, con una cuenta marcada que pide una operación que su papel y su pertenencia admitirían | Prueba dedicada |
| Códigos distintos para las operaciones bloqueadas por la marca | Exactamente **1** para todas ellas | Prueba que pide tres operaciones distintas con la marca puesta y comprueba el mismo motivo |
| Orígenes de la marca que producen el mismo motivo | **2 de 2** —habilitación y reseteo— | Prueba con una cuenta recién habilitada y una reseteada |
| Negativas por pertenencia que se traducen a «no autorizado» | Exactamente **0** | Prueba que pide un trabajo ajeno y comprueba el motivo, más la tabla de traducciones prohibidas de 03 |
| Reemplazo de la propia credencial con la marca puesta | Procede en **100 %** de los casos, y levanta la marca | Prueba de la única capacidad admitida |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4, con sus cinco precisiones.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.4, la tabla de traducciones prohibidas.
- [`../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md`](../../../GeometriaFactory-Domain/05-Arquitectura-Tecnica/Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) §6 punto 1, que es la dependencia de disciplina que esta ADR cierra.
- [`../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md`](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Contrato-De-Respuesta-De-Error.md), por el código único de las operaciones bloqueadas por la marca.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §17.2.P.5, §17.1.P.2 (INV-02, INV-03, INV-09) y §4.1 (RN-03, RN-13, RN-16).
- ADR relacionadas: [`ADR-01`](ADR-01-Casos-De-Uso-Con-Inversion-De-Dependencias.md), [`ADR-06`](ADR-06-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el orden fijo de las cuatro comprobaciones en un único componente como cierre de la dependencia de disciplina que la Fase C de `GeometriaFactory-Domain` declaró que el dominio no podía cerrar, evalúa cuatro alternativas con una precisión sobre la tercera, declara tres trade-offs y fija siete métricas de validación. |
