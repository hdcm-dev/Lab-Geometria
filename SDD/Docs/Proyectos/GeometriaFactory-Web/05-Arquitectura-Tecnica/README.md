# 05 · Arquitectura técnica — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Tipo de proyecto de código (D8):** `web-monolith`

---

## 1. Punto de entrada

`GeometriaFactory-Web` es la **pieza pública** del producto: el front desplegado en el hosting público, **el único punto de contacto del navegador** y el **anfitrión del bundle del visor**. Es una de las dos unidades desplegables y nivel 1 del orden topológico; compila contra `GeometriaFactory-Contracts` y contra el bundle de `GeometriaFactory-Visor`, y habla con el servicio de datos **en tiempo de ejecución, servidor a servidor**.

Lo que hay que haber entendido antes de tocar esta sección: **este proyecto de código es el único del producto que puede violar las tres reglas de arquitectura**, porque es el único que sirve al navegador. Los otros seis las sostienen por construcción o no las alcanzan. El punto de entrada es [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md), y dentro de él, §10.4.

## 2. Documentos de esta sección

| Documento | Propósito |
| --- | --- |
| [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) | Documento maestro: estilo, las cuatro vistas mínimas, cross-cutting, catorce NFR, siete riesgos, trazabilidad de las trece restricciones transversales, de las dieciséis reglas y de las tres reglas de arquitectura, y siete puntos abiertos |
| [`Decisiones-Arquitectura.md`](Decisiones-Arquitectura.md) | Índice de las siete ADR, con la correspondencia contra las cinco decisiones que la regla exige para `web-monolith` y las dos categorías que quedan vacías |
| [`Adrs/`](Adrs/) | Las siete decisiones, una por archivo |

## 3. ADR vigentes

| ADR | Título | Categoría | Estado |
| --- | --- | --- | --- |
| [ADR-01](Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md) | Render en el servidor con circuito interactivo, y una sola salida hacia el servicio de datos | Estilo | Propuesto |
| [ADR-02](Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) | Sin estado propio y sin persistencia, y por qué se omite el modelo de datos lógico | Persistencia | Propuesto |
| [ADR-03](Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) | La credencial de sesión vive en el estado del circuito, y las rutas acotan sin hacer cumplir | Seguridad | Propuesto |
| [ADR-04](Adrs/ADR-04-Tres-Capas-De-Presentacion.md) | Tres capas de presentación: ninguna superficie llega sola al servicio de datos | Estilo | Propuesto |
| [ADR-05](Adrs/ADR-05-Estado-Degradado-Como-Superficie.md) | Un traductor único de condiciones, y el estado degradado como superficie y no como error | Comunicación | Propuesto |
| [ADR-06](Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md) | El visor se opera sólo por sus seis funciones, y es esta pieza la que consulta el entorno | Comunicación | Propuesto |
| [ADR-07](Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) | La dirección del servicio de datos viene de configuración, y el despliegue termina comprobando | Despliegue | Propuesto |

Ninguna superada, ninguna rechazada.

## 4. NFR vigentes

Los catorce, con su objetivo numérico y su mecanismo, están en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §8. En una línea: las **cuatro** mediciones de `PT-01` —**200** en la dirección pública, semáforo del transporte, **20 minutos** de estabilidad y una llamada de salud con datos reales—, **100 %** de los pasos del guion acumulativo, **0** peticiones del navegador hacia el servicio de datos, **1** sola salida, **0** apariciones de la credencial en el navegador, **0** mensajes que expongan una dirección de servicio sobre los **quince** códigos vivos, **0** tráfico de circuito durante la interacción con la escena, **0** instancias del visor no liberadas tras **10** recorridos, **0** invocaciones al interior del bundle sobre sus **seis** funciones, la línea de base de **11** superficies, **73** componentes, **74** estados y **24** rutas sostenida por **61** filas de sensado de deriva, y **0** advertencias de construcción.

**No hay cobertura de líneas, y la fuente lo declara así**: este proyecto de código no tiene proyecto de pruebas propio, y su verificación es el guion de demostración acumulativo más la batería de integración del servicio que consume.

## 5. Orden de lectura sugerido

1. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.4, las tres reglas de arquitectura y cómo las trata esta pieza. Son media página y es lo primero: **una superficie o un componente que las viole es un defecto, no una alternativa**. La categoría 03 hace la misma recomendación con su §2.4, y por el mismo motivo.
2. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §2.2 — las cuatro decisiones de `GeometriaFactory-Contracts` y de `GeometriaFactory-Visor` que esta pieza hereda y no reabre. Sin eso, el aislamiento del visor se lee como una precaución y no como una obligación heredada.
3. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §3 — los ocho componentes en tres capas, y sobre todo **§3.2**, cuyas cinco precisiones son las que hacen auditable lo demás.
4. [`Adrs/ADR-01`](Adrs/ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md) y [`ADR-03`](Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) — juntas, porque la segunda sólo es posible por la primera.
5. [`Adrs/ADR-06`](Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md) — la decisión con más superficie de contacto con otro proyecto de código.
6. [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10 — la trazabilidad, para consultar por restricción transversal, por regla o por regla de arquitectura.

## 6. Artefactos omitidos y su motivo

| Artefacto | Estado | Motivo |
| --- | --- | --- |
| `Modelo-Datos-Logico.md` | **Omitido, con ADR** | La regla lo marca **obligatorio** para `web-monolith`, y se omite igual como **decisión técnica declarada**: `tiene_persistencia` es false y es deliberado. La categoría 02 lo pidió explícitamente en su §9 —«corresponde una ADR en 05-Arquitectura-Tecnica que la registre»— y esa ADR es [`ADR-02`](Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md). El modelo lógico del producto le corresponde a la categoría 05 de `GeometriaFactory-Infrastructure` |
| `contratos-<area>.md` | **Omitido** | La regla lo exige para `web-monolith` **sólo si expone API externa**, y este proyecto de código **no expone contrato a nadie**: es hoja del grafo de dependencias y punto de entrada del usuario final (`PRODUCT-INTAKE` §14). Los contratos que **consume** están emitidos en los proyectos de código que los producen: [`Contracts`](../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Contratos-Abstractions.md) y [`Visor`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Contratos-Abstractions.md) |
| `Flujo-Ejecucion.md` | **Omitido** | La regla lo admite para `web-monolith` **sólo si hay orquestación compleja**, y no la hay: cada superficie hace una o pocas solicitudes y compone el resultado. La secuencia más larga de la pieza es el ciclo de vida de la instancia del visor, y vive entera en [`ADR-06`](Adrs/ADR-06-Aislamiento-Del-Visor-Tras-Su-Fachada.md) §7 y en `CU-07`. El flujo de ejecución del dibujo, que sí es una canalización, ya lo emitió [`GeometriaFactory-Visor`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Flujo-Ejecucion.md) |
| `Extensibilidad.md` | **Omitido** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión del producto es el contrato de la fachada del visor, y esta pieza es su **consumidor**, no su dueño: cómo crece esa fachada lo declara [`Extensibilidad.md`](../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Extensibilidad.md) §5 de `GeometriaFactory-Visor` |
| `_legacy/` | **No existe** | Es la primera emisión de esta categoría en este proyecto de código: no hay ninguna versión superada que archivar |

## 7. Lo que esta sección resolvió de lo que aguas arriba quedó abierto

Dos puntos llegaron a esta categoría por nombre, y conviene decir qué pasó con cada uno:

| Punto que llegó | Qué hizo esta sección |
| --- | --- |
| La **ADR que registre la omisión del modelo conceptual y del modelo lógico**, pedida por la categoría 02 §9 | **Resuelto.** [`ADR-02`](Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) la emite, con la aclaración de que la omisión **no es la que la regla admite** para el tipo D8: contradice su valor por defecto y por eso lleva ADR |
| El **formato de intercambio y su configuración**, derivado por la Fase C de `GeometriaFactory-Contracts` a las categorías 05 de `GeometriaFactory-Api` y de ésta | **No resuelto, y con fundamento.** No se puede decidir de un solo lado: los dos extremos tienen que coincidir o el contrato deja de ser el mismo. Esta categoría declara que la decisión pertenece a la categoría 05 de `GeometriaFactory-Api`, que es el productor, y que esta pieza la adopta. Sigue como `PA-03` |

## 8. Relación con la Fase B2 y con la categoría 03

La maqueta de este proyecto de código **se ejecutó y quedó aprobada por el Product Owner**, tras cuatro iteraciones. Eso condiciona a esta sección de una manera concreta que conviene tener presente:

- **Los nombres canónicos de las once superficies no se cambian.** Cambiarlos invalidaría filas de la matriz de sensado de deriva.
- **La sección 5 de cada wireframe es la lista de estados que hay que sostener.** Un estado que la implementación no reproduzca es una deriva, no una decisión de arquitectura.
- **La disposición de cuatro partes de la vista de trabajo viene decidida aguas arriba y probada en el aula**: esta sección no la rediseña.
- **Las 61 filas de la matriz de sensado de deriva nacieron todas en `Sin verificar`**, y se mueven al cierre de cada sprint de codificación. Ninguna afirma nada sobre el sistema construido: declaran qué tendría que ser cierto.

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Índice navegable de la sección: los tres documentos vigentes, las siete ADR con su estado, los NFR en una línea, el orden de lectura de seis pasos, los cuatro artefactos omitidos con su motivo —incluido el modelo lógico, omitido con ADR contra el valor por defecto de la regla—, el destino de los dos puntos abiertos que llegaron por nombre a esta categoría, y la relación con la Fase B2 aprobada y con la línea de base visual que la codificación tiene que sostener. |
