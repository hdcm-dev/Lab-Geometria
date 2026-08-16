# ADR-02003 — Versionado por versionado semántico sin publicación, y estabilidad de la superficie

**Proyecto de código:** GeometriaFactory-Domain
**Documento:** ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain declara versionado semántico y convenciones de mensaje de confirmación **sin excepciones**, que la versión la calcula una herramienta que se ancla en la etapa `a`, que la biblioteca **no se publica en ningún repositorio de paquetes** y que el modelo de ramas es una rama por etapa con etiqueta al fusionar. `redistribuible` es false (`PRODUCT-MANIFEST` §2).

La tensión que hay que resolver es que un versionado semántico sin publicación puede leerse como un trámite vacío: si nadie consume el paquete por su versión, la pregunta es qué gobierna esa versión. Y la hay: los dos consumidores del dominio —`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`— compilan contra él, y el producto declara que un cambio incompatible sube major en el registro de cambios del producto aunque no se publique en ningún feed (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Contracts, dicho allá para el ensamblado de contratos y aplicable por analogía declarada acá).

Además hay una asimetría propia: el intake deja **abiertos los nombres de tipos y de espacios de nombres** hasta el punto de control de la etapa `a` (§17.1.P.11 · GeometriaFactory-Domain). Antes de ese punto no hay superficie estable que versionar.

Motivación upstream: `PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain, §17.1.P.8 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain y §15 (puntos de control y ramas por etapa).

## 2. Decisión

Se adopta **versionado semántico con convenciones de mensaje de confirmación, sin publicación en ningún repositorio de paquetes**. Lo que la versión gobierna es la **compatibilidad de compilación de los dos consumidores del dominio**, y el criterio de qué constituye un cambio mayor lo declara §7 de esta ADR.

La superficie pública **empieza a ser estable en el punto de control de la etapa `a`**, cuando se fijan los nombres. Todo lo anterior a ese punto es prehistoria de versionado y no genera cambio mayor.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Versionado semántico sin publicación (**adoptada**) | Da un vocabulario común para declarar qué rompe y qué no; se apoya en las etiquetas por etapa que el producto ya tiene | La versión no la verifica ningún consumidor externo: su valor depende de la disciplina de quien la asigna |
| Versión única del producto, sin versión propia del proyecto de código | Un solo número que seguir; menos ceremonia en un producto de un solo equipo | Pierde la capacidad de declarar que un cambio del dominio es incompatible sin que lo sea el producto entero, que es justamente la señal que los consumidores necesitan |
| Publicación en un repositorio de paquetes interno | Los consumidores se atarían a una versión resuelta y no a la carpeta | El intake lo descarta explícitamente («no se publica en ningún feed») y agregaría infraestructura a un producto que las fuentes declaran básico |
| Sin versionado, sólo etiquetas de etapa | Cero ceremonia | Deja sin nombre la diferencia entre un cambio que rompe a los consumidores y uno que no; y el producto ya exige convenciones de mensaje de confirmación, que sin versionado quedan sin efecto |

## 5. Consecuencias positivas

1. Un cambio incompatible del dominio queda declarado como tal antes de que lo descubra la compilación de un consumidor.
2. Las etiquetas por etapa permiten volver a cualquier demostración ya aprobada, que es la política de reversión que el intake declara (§17.1.P.8 · GeometriaFactory-Domain).
3. El criterio de §7 hace verificable, y no opinable, qué sube major.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la versión no la verifique ninguna herramienta de resolución de dependencias.** Es una convención sostenida por disciplina y por revisión.
2. **Se acepta que la superficie no sea estable antes del punto de control de la etapa `a`.** Es el precio de dejar abiertos los nombres, que es lo que el intake decidió.
3. **Se acepta depender de una herramienta todavía no elegida** para calcular la versión: el intake la ancla en la etapa `a` y esta ADR no la nombra.

## 7. Implementación

Qué constituye cada clase de cambio en este proyecto de código:

| Clase | Qué la produce |
| --- | --- |
| **Mayor** | Quitar o renombrar un tipo, una operación o un atributo de la superficie pública; cambiar qué recibe una operación; **quitar un valor de un conjunto cerrado** —los cuatro estados del trabajo, los tres estados de cuenta, los dos papeles, las dos especies de observación—; y **perder cualquiera de los nueve invariantes**, aunque ninguna firma cambie |
| **Menor** | Agregar un tipo, una operación o un atributo opcional; **agregar un valor a un conjunto cerrado**, que obliga al consumidor a contemplarlo pero no rompe su compilación; agregar una condición de error al catálogo |
| **Parche** | Corregir el comportamiento de una guarda para que cumpla el invariante que ya declaraba, sin cambiar la superficie |

- Una rama por etapa a partir de la principal, con etiqueta al fusionar (`PRODUCT-INTAKE` §17.1.P.7 · GeometriaFactory-Domain).
- El registro de cambios del producto recibe la fila de todo cambio mayor de este proyecto de código.
- **Los ADR de esta sección no se versionan en su archivo**: una decisión que evoluciona se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY`.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Advertencias de construcción | Exactamente **0** | `scripts/build.sh` termina en 0 y sin advertencias, puerta bloqueante para fusionar |
| Etapas con etiqueta al fusionar | **100 %** | Inspección de etiquetas contra la lista de etapas cerradas |
| Cambios mayores sin fila en el registro de cambios del producto | Exactamente **0** | Revisión del pull request de la etapa, que **es** el punto de control |
| Invariantes vigentes tras un cambio mayor | **9 de 9** verificados por prueba antes de fusionar | Batería del dominio en la etapa de `test` |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.7 · GeometriaFactory-Domain, §17.1.P.8 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Domain, §17.1.P.7 · GeometriaFactory-Contracts y §15.
- `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` 1.2 §2.
- [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §6, que aplica este criterio a cada elemento del contrato.
- ADR relacionadas: [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-02002`](ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el versionado semántico sin publicación, declara qué gobierna la versión en ausencia de consumidores externos, fija el criterio de cambio mayor, menor y parche —incluida la pérdida de un invariante como cambio mayor sin cambio de firma—, evalúa cuatro alternativas y declara cuatro métricas. |
