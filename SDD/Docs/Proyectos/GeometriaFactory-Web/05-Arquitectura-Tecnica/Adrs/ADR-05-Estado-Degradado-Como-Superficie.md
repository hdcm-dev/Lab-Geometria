# ADR-05 — Un traductor único de condiciones, y el estado degradado como superficie y no como error

**Proyecto de código:** GeometriaFactory-Web
**Documento:** ADR-05-Estado-Degradado-Como-Superficie.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

Esta pieza recibe del servicio de datos dos clases de cosas distintas cuando algo no sale: una **respuesta de error del contrato**, con uno de los **diecisiete** códigos vivos que `GeometriaFactory-Contracts` declara, o **ninguna respuesta**, porque el servicio de datos no está. Y tiene una tercera fuente de interrupción que no viene del servicio de datos en absoluto: **el circuito se cortó**.

Las tres terminan en la pantalla, y confundirlas es el error de lectura más probable de toda la pieza. La categoría 03 lo dice sin rodeos al recomendar leer temprano la tabla que separa los dos tramos de la superficie de degradación, «aunque nada esté fallando».

Hay además una obligación que sólo se puede cumplir en un lugar: `RA-03` exige que **ningún mensaje mostrado incluya la dirección de un servicio interno, un nombre de archivo de datos ni una traza de la implementación**. Si los mensajes se compusieran en once superficies, habría once lugares donde eso se puede violar; si se componen en uno, hay uno.

Motivación upstream: NB-08; `RA-03`; `PRODUCT-INTAKE` §7 (`CL-2`), §17.6.P.5 y §17.6.P.10; restricciones transversales `RT-03` y `RT-07` de la categoría 02.

## 2. Decisión

**Un único componente traduce condiciones a presentación**, y es el único lugar por el que un mensaje llega a la persona. Traduce los **diecisiete** códigos vivos del contrato y también el camino de ausencia de respuesta, y garantiza que ninguno lleve dirección de servicio, ruta de datos ni traza.

**El estado degradado es una superficie, no un error.** Se documenta y se implementa una vez, con sus **dos tramos independientes** —el servicio de datos que no responde, y el circuito que se cortó—, y las otras diez superficies la referencian en lugar de reproducirla.

**Nunca una excepción sin manejar y nunca una pantalla rota.** Toda interrupción tiene un estado declarado, y el conjunto de estados que cada superficie puede mostrar está fijado en la sección 5 de su wireframe.

Y la regla que hace posible distinguir sin adivinar: **el listado vacío se distingue del fallo por el tipo recibido y no por el conteo.**

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Traductor único, con el estado degradado como superficie (**adoptada**) | `RA-03` se verifica en un solo punto; los dos tramos de degradación no se confunden; una superficie nueva hereda el tratamiento sin escribirlo | El traductor concentra el conocimiento de los diecisiete códigos, y una condición nueva del contrato obliga a tocarlo |
| Cada superficie compone sus propios mensajes | Cada superficie diría exactamente lo que su contexto necesita | Once lugares donde se puede filtrar una dirección de servicio, y once redacciones que van a divergir. Es el mismo fundamento por el que `GeometriaFactory-Contracts` descartó un tipo de error por familia. **Descartada por esta categoría** |
| Mostrar el mensaje que venga del servicio de datos, tal cual | Cero traducción y cero desincronización | El texto del contrato es **neutro y para diagnóstico**, no para una persona en una superficie; y confiar en que nunca traiga una ruta o una traza es apostar `RA-03` a la disciplina del otro lado. **Descartada por esta categoría** |
| Tratar el corte de circuito y la indisponibilidad del servicio de datos con el mismo aviso | Un solo mensaje que escribir | Son dos cosas distintas con dos acciones distintas: una se resuelve sola al reconectar, la otra no depende de la persona. Mezclarlas produce el aviso que no sirve para ninguna de las dos. **Descartada por la categoría 03**, que las separó en dos tramos |

## 5. Consecuencias positivas

1. `RA-03` tiene **un** punto de verificación en toda la pieza, y ese punto es inspeccionable sin levantar el servicio de datos.
2. Los dos tramos de degradación se leen separados, que es lo que la categoría 03 pidió explícitamente en su orden de lectura.
3. Una condición nueva del contrato tiene un solo lugar donde recibir mensaje, y si no lo recibe se nota, porque el traductor cubre el conjunto cerrado.
4. La distinción de vacío contra fallo por el tipo recibido evita el defecto de mostrar «no hay trabajos» cuando lo que pasa es que no se pudieron traer.
5. El estado degradado, al ser superficie, tiene nombre canónico, mapa de estados y filas propias en la matriz de sensado de deriva: se puede verificar como cualquier otra.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que el traductor concentre el conocimiento de los diecisiete códigos**, y que agregar una condición al contrato obligue a tocarlo. Es deliberado: que haya que tocar un archivo es exactamente la señal que se quiere.
2. **Se acepta que el mensaje mostrado no sea el del contrato.** El de acá es para una persona; el del contrato es neutro y para diagnóstico. La correspondencia entre los dos vive en el traductor.
3. **Se acepta perder detalle diagnóstico en la superficie.** Cuando algo falla, la persona no ve qué servicio ni por qué camino, y eso hace más difícil que ella misma reporte con precisión. Se acepta porque la alternativa es exponer la topología, que es lo que la partición del producto protege.

## 7. Implementación

- El componente **Traductor de condiciones a presentación** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que produce mensajes para la persona.
- **No habla con el servicio de datos**: recibe el tipo de error ya traído, o la señal de que no hubo respuesta. Por eso se ejercita entero sin red.
- Cada mensaje dice **qué pasó, por qué pasó y qué hacer**, que son las tres partes que la categoría 03 exige.
- Los **dos tramos** de la superficie de degradación se tratan por separado: la indisponibilidad del servicio de datos deja el armazón intacto y avisa en el área de contenido; el corte de circuito se superpone en el borde superior.
- **El repliegue del transporte no se anuncia**: no es una degradación del laboratorio y avisarlo sería alarmar sin darle a nadie nada que hacer.
- Verificación sugerida a 08: recorrer los diecisiete códigos y el camino de ausencia de respuesta, y comprobar en los dieciséis casos que el texto mostrado no contiene dirección, ruta ni traza.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Códigos vivos del contrato con mensaje de superficie declarado | **15 de 15**, más el camino de ausencia de respuesta | Matriz código contra mensaje en 08 |
| Mensajes que exponen dirección de servicio, ruta de datos o traza | Exactamente **0** | Inspección del traductor sobre los dieciséis caminos |
| Componentes que producen mensajes para la persona | Exactamente **1** | Inspección del árbol de fuentes |
| Excepciones sin manejar que llegan a la pantalla | Exactamente **0** | Recorrido con el servicio de datos apagado y con el circuito cortado |
| Superficies que distinguen vacío de fallo por el conteo | Exactamente **0** de **11** | Recorrido con el servicio de datos apagado sobre las superficies con colección |
| Tramos de degradación tratados por separado | **2 de 2** | Recorrido de los dos casos, comprobando que el aviso y el lugar son distintos |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §7 (`CL-2`), §14 (`RA-03`), §17.6.P.5 y §17.6.P.10.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §6 (`RT-03`, `RT-07`) y [`CU-10`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md).
- [`../../03-UX-UI-DX/Wireframes-Estado-Degradado-Y-Reconexion.md`](../../03-UX-UI-DX/Wireframes-Estado-Degradado-Y-Reconexion.md) §1, la tabla que separa los dos tramos; [`../../03-UX-UI-DX/Experiencia-De-Uso.md`](../../03-UX-UI-DX/Experiencia-De-Uso.md) §4.1 y §8.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md`](../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-02-Tipo-De-Error-Unico-Con-Conjunto-Cerrado.md), el conjunto cerrado de diecisiete códigos vivos que este traductor cubre.
- ADR relacionadas: [`ADR-02`](ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md), [`ADR-04`](ADR-04-Tres-Capas-De-Presentacion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra el traductor único de condiciones como único punto por el que un mensaje llega a la persona —y por lo tanto único punto de verificación de `RA-03`— y el estado degradado como superficie con sus dos tramos independientes. Evalúa cuatro alternativas, declara tres trade-offs incluida la pérdida deliberada de detalle diagnóstico, y fija seis métricas de validación sobre los quince códigos vivos más el camino de ausencia de respuesta. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. Se cierran con su fila, su desenlace y su fecha los puntos abiertos que estas decisiones resolvían. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **6**. Sube minor. |
