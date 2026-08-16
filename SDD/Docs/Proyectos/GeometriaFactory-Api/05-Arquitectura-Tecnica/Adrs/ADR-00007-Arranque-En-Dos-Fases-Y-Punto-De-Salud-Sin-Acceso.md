# ADR-00007 — Arranque en dos fases, y un punto de salud que no exige acceso

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Despliegue

---

## 1. Contexto

El intake declara como responsabilidad propia y explícita de este proyecto de código **aplicar las transformaciones de esquema al arrancar** y tomar de configuración la ruta del almacén, que en producción apunta a un volumen persistente (`PRODUCT-INTAKE` §17.5.P.4). `GeometriaFactory-Infrastructure` decidió que esa preparación **detenga el arranque** antes que operar sobre un almacén en el que no se puede confiar. Lo que esta ADR resuelve es **dónde encaja ese disparo en el arranque del servicio** y qué pasa con el punto de salud mientras tanto.

La pregunta no es de forma. El intake declara la puerta de imagen del pipeline en términos exactos: la imagen se construye, arranca desde el contenedor de desarrollo, **aplica las transformaciones sobre un almacén vacío y responde salud**. Y declara un requerimiento numérico: **arranque en frío por debajo de 30 segundos**, «para que la comprobación del despliegue sirva de algo». Un punto de salud que responda antes de que el almacén esté listo hace que esa puerta pase sin haber comprobado nada.

Motivación upstream: NB-00003, NB-00008; `PRODUCT-INTAKE` §17.5.P.3, §17.5.P.4, §17.5.P.8, §17.5.P.10.

## 2. Decisión

**El arranque tiene dos fases y el servicio no escucha hasta que las dos terminaron.** Cinco reglas:

1. **Fase uno: construir el grafo de dependencias.** Si algo falta, **falla en construcción** y no hay servicio. No hay petición que responder ([`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md)).
2. **Fase dos: disparar la preparación del almacén.** La ejecuta `GeometriaFactory-Infrastructure`; **esta capa la dispara y espera su resultado**. Si no se pudo completar, **el arranque se detiene** y ninguna petición se atiende.
3. **El punto de salud no exige acceso firmado, y tiene que ser así.** Lo consume la página de salud del front y la comprobación del despliegue, y **tiene que poder responder cuando nadie puede autenticarse**. Es uno de los cuatro puntos fuera de la guardia, y su ausencia de credencial está declarada en la tabla de la superficie.
4. **El punto de salud responde por el estado del servicio, no por el de sus dependencias en detalle.** Dice si el servicio puede atender o no; **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró**. Es `RA-03` en el punto más tentador de todos: el que existe para diagnosticar.
5. **No hay modo de sólo lectura ni arranque parcial.** Un servicio que atiende sobre un almacén en el que no se puede confiar es peor que uno que no arranca: el segundo se nota en el despliegue, el primero se nota cuando alguien busca su trabajo y no está.

**El reemplazo de versión es detener y arrancar, con ventana de indisponibilidad.** Sin proxy inverso no hay despliegue con solapamiento, y el intake lo acepta por escrito. Esta ADR **no lo reabre**: lo registra, porque es lo que hace que la fase dos ocurra en cada reemplazo y no sólo la primera vez.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Dos fases, sin escuchar hasta terminar, con salud sin acceso (**adoptada**) | La puerta de imagen del pipeline comprueba lo que dice comprobar; un despliegue mal configurado no acepta ni un trabajo; el requerimiento de arranque en frío es medible de punta a punta | El servicio tarda más en responder por primera vez, y una transformación mal escrita deja el servicio caído hasta que alguien intervenga |
| Escuchar primero y preparar el almacén en segundo plano | Arranque aparente más rápido y comprobación de salud que responde enseguida | **Descartada.** La puerta de imagen pasaría **sin haber comprobado nada**, y el servicio aceptaría peticiones contra un almacén a medio preparar |
| Preparar el almacén a demanda, en la primera petición que lo necesite | No hay fase dos que espere | **Descartada.** Convierte un fallo de despliegue en un fallo de la primera persona que use el sistema, y en un despliegue manual y domiciliario eso significa que se entera el alumno |
| Punto de salud que exija acceso firmado | Menos superficie sin credencial | **Descartada.** La página de salud del front y la comprobación del despliegue tienen que poder consultarlo **sin credencial**, y una salud que exige autenticarse no sirve cuando lo que falló es la autenticación |
| Punto de salud que informe el estado de cada dependencia con detalle | Diagnóstico inmediato sin abrir el registro | **Descartada por `RA-03`.** Sería el lugar más cómodo para exponer la ruta del almacén, y es un punto **sin credencial**. El diagnóstico vive en el registro del lado del servidor, que es donde tiene que estar |
| Arrancar en modo de sólo lectura si el esquema no corresponde | El listado seguiría funcionando | **Descartada.** Un producto cuya única acción de guardado no funciona no está degradado, está roto, y presentarlo como funcionando retrasa el diagnóstico |

## 5. Consecuencias positivas

1. La puerta de imagen del pipeline comprueba exactamente lo que declara: transformaciones aplicadas sobre un almacén vacío **y** salud respondida.
2. El requerimiento de **30 segundos** de arranque en frío es medible de punta a punta, desde el arranque del contenedor hasta la primera respuesta de salud.
3. Un despliegue sin volumen montado, sin clave de firma o con un esquema que no corresponde **no acepta ni un solo trabajo**.
4. `NB-00008` recibe acá su tramo propio: el punto de salud y el arranque que se detiene son lo que permite que la pieza pública declare un estado degradado explícito en lugar de fallar sin explicación.
5. `RA-03` se sostiene en el punto sin credencial, que es donde más barato sería romperla.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que el servicio tarde más en responder por primera vez**, y que el requerimiento de arranque en frío incluya la preparación del almacén.
2. **Se acepta que una transformación mal escrita deje el servicio caído** hasta que una persona intervenga. Es el precio de no tener ningún camino automático que pueda perder datos.
3. **Se acepta la ventana de indisponibilidad de cada reemplazo de versión**, ya aceptada por la fuente a cambio de no montar un proxy inverso.
4. **Se acepta que el punto de salud diagnostique poco**, y se compensa con el registro estructurado del lado del servidor, que es donde el operador tiene que mirar.

## 7. Implementación

- El componente de arranque y salud de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que dispara la preparación y el único que aloja el punto de salud.
- **Convención impuesta:** ninguna operación de superficie dispara la preparación del almacén.
- **Convención impuesta:** el punto de salud no lleva ni ruta, ni esquema, ni versión de dependencia en su respuesta.
- La ruta del almacén llega por la composición de raíz; **esta capa la toma de configuración y `GeometriaFactory-Infrastructure` la recibe** ([`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md)).
- La reversión declarada es volver a la etiqueta de la etapa anterior y reconstruir; el guion de restablecimiento del almacén **reproduce el estado de primer arranque** y no es un camino de producción.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Arranque en frío | **Menos de 30 segundos** hasta responder salud, con transformaciones aplicadas [ASUNCIÓN del intake] | Medición desde el arranque del contenedor |
| Peticiones atendidas con la preparación incompleta | Exactamente **0** | Prueba de arranque fallido contra el punto de salud y contra un punto de superficie |
| Respuestas de salud que incluyen ruta, esquema o versión de dependencia | Exactamente **0** | Prueba de inspección sobre la respuesta de salud |
| Puntos de acceso que disparan la preparación del almacén | Exactamente **0** | Inspección en revisión |
| Aplicación de transformaciones sobre almacén vacío en la puerta de imagen | **1 de 1** exitosa | Etapa de imagen del pipeline |
| Consultas de salud que fallan por falta de credencial | Exactamente **0** | Prueba sin cabecera de autorización |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §14 (RA-03), §17.5.P.3, §17.5.P.4, §17.5.P.8 y §17.5.P.10.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-00011-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00011-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md).
- [`../../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md`](../../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md), que es la decisión que esta ADR dispara.
- ADR relacionadas: [`ADR-00003`](ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md), [`ADR-00006`](ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), [`ADR-00008`](ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Fija el arranque en dos fases sin escuchar hasta terminar, con el punto de salud sin credencial y sin detalle de dependencias, y sin modo de sólo lectura. Es lo que hace que la puerta de imagen del pipeline compruebe lo que declara y que el requerimiento de arranque en frío sea medible de punta a punta. Evalúa seis alternativas, declara cuatro trade-offs y fija seis métricas de validación. |
