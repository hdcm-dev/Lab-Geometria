# ADR-10007 — La dirección del servicio de datos viene de configuración, y el despliegue termina comprobando

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

Esta pieza es **una de las dos unidades desplegables del producto**, y la única que se publica en un hosting público de terceros. Tres hechos de la fuente condicionan su despliegue:

1. **El servidor de datos no tiene dirección estática.** La fuente registra la decisión del Product Owner de admitir la dirección directa —«la dirección dinámica realmente no cambia tanto»— con el servicio de nombres dinámico como recomendación. Sea cual sea, **la dirección real del servidor propio no se versiona**.
2. **La subida al hosting no es transaccional** (`R-03`). Puede dejar la aplicación a medias, y una subida que se reporta como exitosa habiendo dejado el producto caído es peor que una falla visible.
3. **El bundle del visor viaja adentro de esta unidad.** Si se toma de un artefacto viejo, la aplicación publicada dibuja con una versión que nadie construyó en ese flujo.

Motivación upstream: NB-00008; `RA-03`; `PRODUCT-INTAKE` §17.2.P.5 · GeometriaFactory-Web, §17.2.P.7 · GeometriaFactory-Web, §17.2.P.8 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web (`PT-01.a`, `PT-01.d`) y §17.7 P.8 (`PT-03`).

## 2. Decisión

**La dirección del servicio de datos se toma de configuración y nunca está embebida en el código.** Se inyecta al publicar desde secretos del repositorio, junto con las credenciales de la subida, y **la dirección real del servidor propio no se versiona**.

**El flujo de publicación no termina en la subida: termina comprobando que la dirección pública responde.** Una subida que deja la aplicación caída y se reporta como exitosa es un modo de falla peor que una falla visible.

**El bundle del visor se genera en el mismo flujo y nunca se toma de un artefacto viejo.**

La reversión es **volver a publicar desde la etiqueta anterior**, y el despliegue se hace **fuera del horario de uso**, que es el tratamiento acordado para una subida no transaccional.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Dirección desde configuración inyectada al publicar, con comprobación final (**adoptada**) | La dirección real no queda en el repositorio; cambiarla no exige recompilar el código fuente; una publicación rota se detecta en el mismo flujo | La comprobación final alarga el flujo, y una falla intermitente del hosting puede marcar en rojo un despliegue que sí funcionó |
| Dirección embebida en el código | Un parámetro menos que administrar y sin secretos que rotar | Publicaría la dirección del servidor propio en el repositorio, que es exactamente lo que `RA-03` y la fuente prohíben; y cambiarla exigiría recompilar. **Descartada por `PRODUCT-INTAKE` §17.2.P.5 · GeometriaFactory-Web y §17.2.P.11 · GeometriaFactory-Web punto 3** |
| Dirección configurable desde una superficie del producto | El docente podría cambiarla sin tocar el despliegue | **No hay superficies de configuración que la persona fije**, y un parámetro que la superficie no gobierna no se dibuja ni siquiera deshabilitado. Además pondría la dirección del servidor propio en el navegador. **Descartada por la categoría 03** |
| Terminar el flujo en la subida, sin comprobar | Flujo más corto y sin falsos rojos | Deja el modo de falla más caro sin detección: la aplicación caída reportada como desplegada. **Descartada por `PRODUCT-INTAKE` §17.2.P.8 · GeometriaFactory-Web** |

## 5. Consecuencias positivas

1. La dirección del servidor propio no aparece en el repositorio, y por lo tanto tampoco puede filtrarse desde ahí a un mensaje.
2. Un cambio de dirección —que la topología hace probable— se resuelve cambiando un secreto y volviendo a publicar, sin tocar el código fuente.
3. El modo de falla más caro del despliegue queda detectado dentro del propio flujo.
4. El bundle publicado es siempre el que se construyó en ese flujo, lo que hace que `PT-03` —el motor de dibujo dentro del bundle, sin red externa— siga valiendo sobre lo que efectivamente se sirvió.
5. La reversión es una operación conocida y ensayada: volver a publicar desde la etiqueta anterior, que existe porque hay una etiqueta por etapa.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la subida no sea transaccional** y que el remedio sea temporal —desplegar fuera del horario de uso— y no técnico. Es `R-03` y está aceptado aguas arriba.
2. **Se acepta que una intermitencia del hosting pueda marcar en rojo un despliegue correcto.** Es preferible al inverso, que es el modo de falla que esta decisión viene a cerrar.
3. **Se acepta depender de un hosting gratuito**, con el reciclado del proceso como riesgo sin mitigación en el código, a cambio de tener dominio público y transporte seguro donde la red de la facultad no bloquea.
4. **Se acepta que la dirección admita ser una dirección directa** y no sólo un nombre, con lo que eso implica si cambia. La fuente lo declara como decisión, y esta ADR no la reabre.

## 7. Implementación

- El componente **Cliente tipado del servicio de datos** de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 toma la dirección de configuración. Ningún otro componente la conoce, y ninguna superficie la muestra.
- El flujo de publicación recorre: obtención del código → preparación de las dos cadenas de herramientas → instalación reproducible y empaquetado del bundle con copia al directorio de recursos estáticos → publicación → inyección de la dirección desde secretos → subida → **comprobación de que la dirección pública responde**.
- Se dispara manualmente y por fusión a la rama principal, **restringido a los cambios de este proyecto de código y del visor**.
- Puertas bloqueantes: construcción **sin advertencias**; bundle generado en el mismo flujo; y la comprobación final.
- **`PT-01.d`** se mide con una llamada de salud que devuelve **datos reales** del servidor propio; si no pasa, la salida declarada es publicar el servicio de datos en un puerto convencional.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Apariciones de la dirección del servidor propio en el repositorio | Exactamente **0** | Inspección del árbol de fuentes y del historial |
| Flujos de publicación que terminan sin comprobar la dirección pública | Exactamente **0** | Inspección de la definición del flujo |
| `PT-01.a` · dirección pública responde tras publicar | **200** | Paso final del flujo |
| `PT-01.d` · salida hacia el servicio de datos | Una llamada de salud devuelve **datos reales** del servidor propio | Recorrido en la etapa `a` |
| Publicaciones que usan un bundle no generado en el mismo flujo | Exactamente **0** | Inspección de la definición del flujo |
| Advertencias de construcción | Exactamente **0** | Etapa de construcción del flujo, bloqueante |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §14 (`RA-03`), §17.2.P.5 · GeometriaFactory-Web, §17.2.P.7 · GeometriaFactory-Web, §17.2.P.8 · GeometriaFactory-Web, §17.2.P.10 · GeometriaFactory-Web, §17.2.P.12 · GeometriaFactory-Web y §17.7 P.8.
- [`../../03-UX-UI-DX/README.md`](../../03-UX-UI-DX/README.md) §7, que declara por qué no hay superficies de configuración.
- [`../../../GeometriaFactory-Visor/05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md`](ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md), por el carácter generado del bundle que esta unidad transporta.
- ADR relacionadas: [`ADR-10001`](ADR-10001-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-10006`](ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra que la dirección del servicio de datos viene de configuración inyectada al publicar y que la dirección real del servidor propio no se versiona, y que el flujo de publicación termina comprobando que la dirección pública responde en lugar de terminar en la subida. Evalúa cuatro alternativas, declara cuatro trade-offs incluida la subida no transaccional, y fija seis métricas de validación. |
