# ADR-04 — Motor de dibujo empaquetado dentro del bundle y aislado tras la capa 3

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ADR-04-Motor-De-Dibujo-Empaquetado-Y-Aislado.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

El motor de dibujo tridimensional es la única dependencia externa real de este proyecto de código. `PRODUCT-INTAKE` §17.7.P.1 declara dos cosas sobre él: que **entra como dependencia declarada del proyecto y no desde una red de distribución externa**, terminando dentro del bundle porque el front debe funcionar sin acceso a redes externas; y que **la versión que se adopte se ancla y se registra**, documentando el cambio de interfaz que exija si es posterior a la del visualizador previo.

Ese último punto no es teórico: el visualizador previo **reimplementa la cámara orbital a mano** porque su versión no la trae, de modo que una versión posterior probablemente ofrezca la capacidad de otra forma.

La puerta técnica **PT-03** mide exactamente esto: el motor dentro del bundle y la página funcionando sin acceso a redes externas. Se mide antes de comprometer la etapa `g` (`PRODUCT-INTAKE` §15).

Motivación upstream: NB-06, NB-08 en su arista de funcionamiento desde la red del aula; `PT-03`; RA-02.

## 2. Decisión

**El motor de dibujo tridimensional se empaqueta dentro del bundle y queda confinado a la capa 3.** Tres consecuencias declaradas:

1. **Ninguna dependencia se trae desde una red de distribución externa en tiempo de ejecución.** El artefacto que se sirve es autosuficiente.
2. **El motor no aparece nunca en la superficie pública**: ni sus tipos, ni sus nombres, ni sus objetos. Lo que cruza la fachada son valores propios de este contrato.
3. **La versión del motor se ancla explícitamente** y se registra en el momento en que se introduce. Un cambio de versión mayor es una decisión que se documenta, nunca el efecto colateral de una actualización.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Motor empaquetado y confinado a la capa 3 (**adoptada**) | La página funciona sin acceso a redes externas; el motor es reemplazable; el artefacto es reproducible | El bundle pesa más; una actualización del motor obliga a regenerar y volver a desplegar el artefacto |
| Traer el motor desde una red de distribución externa | Bundle mucho más liviano; caché compartida entre sitios | **Falla PT-03**: la página dejaría de funcionar sin acceso a la red externa, y el producto se usa desde una red que bloquea lo que no espera. Descartada por `PRODUCT-INTAKE` §17.7.P.1 |
| Empaquetarlo pero dejarlo accesible al anfitrión | El anfitrión podría hacer cosas que la fachada no expone | Rompe la capa 2 de ADR-01 y vuelve al motor irreemplazable, que es lo contrario del punto de extensión |
| No anclar la versión, tomar siempre la última compatible | Menos mantenimiento declarado | Contradice la regla de anclaje de versiones que el producto declara para los seis proyectos de código, y expone a que un cambio de interfaz del motor rompa la capa 3 sin decisión de nadie |

## 5. Consecuencias positivas

1. **La página funciona sin acceso a redes de distribución externas**, que es `PT-03` y una condición de uso real: el laboratorio se usa desde una red que bloquea lo que no espera.
2. El motor es **reemplazable**: sustituirlo toca sólo la capa 3, y ninguna página cambia.
3. El artefacto es **reproducible**: se genera desde dependencias ancladas.
4. Un cambio de interfaz del motor queda acotado a un componente y no se propaga.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta el peso del motor dentro del bundle**, a cambio de funcionar sin acceso a redes externas. El intake lo declara explícitamente como trade-off aceptado.
2. **Se acepta regenerar y volver a desplegar el artefacto** ante cualquier actualización del motor.
3. **Se acepta que la versión del motor sea un punto abierto** hasta la etapa que implementa la capa 3, con el riesgo de retrabajo que registra [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §9 con probabilidad alta.
4. **Se acepta que este proyecto de código no nombre el motor en su documentación.** La convención está declarada en [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §2.2, y tiene la contrapartida de que hay que abrir el intake para saber cuál es.

## 7. Implementación

- El motor entra como **dependencia declarada del proyecto**, con versión anclada, y el empaquetador lo incluye en el artefacto.
- **Sólo la capa 3 lo referencia.** La fachada no lo importa, y es verificable por inspección del archivo de fachada.
- La cadena de herramientas corre **dentro del contenedor de desarrollo**, y en tiempo de ejecución no hay ninguna: hay un archivo servido como recurso estático.
- El artefacto se copia al directorio de recursos estáticos del anfitrión como paso final de la construcción.
- Si la versión anclada es posterior a la del visualizador previo, **el cambio de interfaz que exija se documenta** en el momento de anclarla.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Dependencias traídas de una red externa en tiempo de ejecución | Exactamente **0** | Puerta técnica `PT-03`: la página funciona sin acceso a redes externas |
| Referencias al motor fuera de la capa 3 | Exactamente **0** | Inspección del archivo de fachada y de los módulos de la capa 2 |
| Dependencias con versión anclada | **100 %** | Inspección del manifiesto de dependencias del proyecto |
| Reproducibilidad del artefacto | Dos construcciones desde el mismo estado producen el mismo artefacto | Comparación de dos construcciones |
| Ediciones manuales del artefacto generado | Exactamente **0** | Regla declarada: el bundle nunca se edita a mano |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.7.P.1, §17.7.P.7, §17.7.P.8, §17.7.P.9, §17.7.P.11 punto 2, §17.7.P.12 y §15 (`PT-02`, `PT-03`); encabezado de la Parte C, regla de anclaje de versiones.
- [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §2.2 y §5.
- ADR relacionadas: [`ADR-01`](ADR-01-Tres-Capas-Con-Fachada-Plana.md), [`ADR-06`](ADR-06-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el empaquetado del motor de dibujo dentro del bundle y su confinamiento a la capa 3, con sus tres consecuencias declaradas, cuatro alternativas evaluadas y cinco métricas ancladas en la puerta técnica `PT-03`. |
