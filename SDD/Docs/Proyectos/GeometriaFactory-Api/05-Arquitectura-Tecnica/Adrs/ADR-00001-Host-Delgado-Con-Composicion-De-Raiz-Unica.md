# ADR-00001 — Host delgado, con la composición de raíz como único lugar de ensamblado

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

`GeometriaFactory-Api` es el **proyecto de código principal** del producto y el único que ensambla a los demás. El intake ya fijó su forma —«host delgado: endpoints que traducen petición a caso de uso y resultado a tipo de transferencia, más la composición de raíz que conecta puertos con adaptadores»— y descartó dos alternativas: lógica en los puntos de acceso y servicio de fachada que devuelva vistas armadas (`PRODUCT-INTAKE` §17.5.P.2).

Lo que ninguna fuente resuelve es **qué significa «delgado» de forma verificable**. Sin un criterio, cualquier cosa que se agregue a un punto de acceso se justifica como «conveniencia del borde», y la propiedad que las tres capas de adentro compraron —que la autorización, el estado y la interpretación se prueban sin HTTP— se pierde de a poco y sin que nadie lo decida.

Hay además un dato del intake que ordena la decisión: la pirámide de pruebas de este proyecto de código es **60 % de integración y 40 % unitarias**, **invertida a propósito**, «porque lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo». Un host que además decidiera cosas necesitaría pruebas unitarias que ese reparto no tiene.

Motivación upstream: NB-00003, NB-00008; RN-00003, RN-00004, RN-00013; `PRODUCT-INTAKE` §17.5.P.2, §17.5.P.6, §17.5.P.11.

## 2. Decisión

**Un punto de acceso hace exactamente cuatro cosas y ninguna más**: recibe la petición y la convierte en la invocación de **a lo sumo un** caso de uso, invoca, convierte el resultado en un tipo de transferencia del ensamblado de contratos, y elige el código de respuesta pidiéndoselo al traductor.

De ahí salen cuatro prohibiciones verificables:

1. **Ningún punto de acceso comprueba pertenencia ni facultad sobre el dato recuperado.** Lo que exige es el **papel declarado en el acceso**, y eso no es autorizar: duplicar la comprobación acá crearía un segundo lugar donde la regla puede decir otra cosa.
2. **Ningún punto de acceso abre una unidad de trabajo, ni encadena dos casos de uso.** Una petición ejerce a lo sumo uno.
3. **Ningún punto de acceso agrega ni recorta campos de un tipo de transferencia.** Los tipos son del ensamblado de contratos y esta capa no los modifica.
4. **Ningún punto de acceso elige su código de respuesta por su cuenta.** Lo pide al traductor, que aplica la tabla única.

**Y una obligación estructural: hay una sola composición de raíz, y es el único lugar donde el grafo de dependencias se construye.** No hay registro automático por convención ni módulos de composición por área ([`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md)).

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Host delgado con las cuatro prohibiciones verificables (**adoptada**) | Cada prohibición se comprueba por inspección; la pirámide invertida tiene sentido porque no hay lógica propia que probar unitariamente; las capas de adentro conservan la propiedad de probarse sin HTTP | Obliga a que casi todo camino de prueba pase por integración, que es más lenta de correr |
| Lógica en los puntos de acceso | Menos capas y menos traducción; cada punto se lee entero | **Descartada por el intake §17.5.P.2**: haría inseparable la verificación de pertenencia de la capa de transporte y volvería obligatoria una prueba de integración para cada regla de negocio |
| Servicio de fachada que devuelva vistas ya armadas | Menos viajes desde el front | **Descartada por el intake §17.5.P.2**: el front arma sus vistas en el servidor del hosting; una fachada agregaría un salto sin quitar ninguno |
| Host delgado, pero con la comprobación de pertenencia duplicada acá «por defensa en profundidad» | Un punto de acceso mal invocado fallaría antes | **Descartada.** Dos lugares que comprueban lo mismo terminan diciendo cosas distintas, y el criterio de verificación de `RN-00004` que la fuente exige —**forzar la petición contra esta superficie**— dejaría de probar lo que quiere probar: probaría el borde, no la regla |

## 5. Consecuencias positivas

1. La pirámide invertida del intake queda justificada: no hay lógica propia que probar unitariamente, y lo que hay es cableado.
2. `RN-00004` conserva su criterio de verificación tal como la fuente lo declara: forzar la petición contra esta superficie prueba la regla de adentro, no una copia de acá.
3. Un punto de acceso nuevo es barato de revisar: cuatro prohibiciones y una lista de quince.
4. Las tres capas de adentro conservan la propiedad de probarse sin frontera de proceso, que es lo que sus tres Fases C compraron.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la mayoría del esfuerzo de prueba sea de integración**, con lo que eso cuesta en tiempo de corrida. Es lo que el intake ya decidió al fijar la pirámide.
2. **Se acepta que un error de la capa de aplicación se manifieste primero acá**, en una prueba de integración, y que el diagnóstico exija bajar una capa.
3. **Se acepta no tener defensa en profundidad sobre la pertenencia.** La comprobación vive en un solo lugar, y esa unicidad es la propiedad, no la carencia.

## 7. Implementación

- Los ocho componentes son los de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1, y §3.4 declara qué punto de acceso aloja cada superficie.
- **Convención impuesta:** un punto de acceso que necesite más de un caso de uso es señal de que el recorte de la categoría 02 está mal, y ése no se reabre acá.
- **Convención impuesta:** ninguna superficie depende de otra superficie.
- El único artefacto de este proyecto de código que no es código de producción es la **colección de peticiones reproducible**, que vive en el árbol de muestras del repositorio y **no tiene componente**.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Casos de uso invocados por petición | **A lo sumo 1** | Inspección de los quince puntos |
| Comprobaciones de pertenencia o de facultad sobre el dato dentro de un punto de acceso | Exactamente **0** | Inspección en revisión |
| Unidades de trabajo abiertas por esta capa | Exactamente **0** | Inspección en revisión |
| Campos agregados o recortados sobre un tipo de transferencia | Exactamente **0** | Prueba de integración que compara el cuerpo contra el tipo declarado |
| Códigos de respuesta elegidos fuera del traductor | Exactamente **0** | Inspección en revisión |
| Reparto de la pirámide de pruebas | **60 %** integración, **40 %** unitarias [ASUNCIÓN del intake] | Recuento en el informe de 08 |
| Cobertura del proyecto de código | **75 %** de líneas y **70 %** de ramas [ASUNCIÓN del intake] | Informe de cobertura, bloqueante |
| Advertencias de construcción | Exactamente **0** | Etapa de `build`, puerta bloqueante |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §17.5.P.2, §17.5.P.6 y §17.5.P.11.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, §4 y §8.
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md`](../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md).
- ADR relacionadas: [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), [`ADR-00004`](ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md), [`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Convierte «host delgado» en cuatro cosas que un punto de acceso hace y cuatro prohibiciones verificables, con la unicidad de la composición de raíz como obligación estructural. Evalúa cuatro alternativas, dos de ellas ya descartadas por el intake, declara tres trade-offs —incluido el de renunciar a defensa en profundidad sobre la pertenencia— y fija ocho métricas de validación. |
