# CU-11 — Arrancar el servicio y dejar el almacén en condiciones

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md), [`NB-08`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §7 (CL-8), §14 (RA-03), §17.5.P.3 (punto de salud), §17.5.P.4 (**aplicar las migraciones al arrancar**), §17.5.P.8 (PT-04), §17.5.P.9, §17.5.P.10 (arranque en frío), §17.5.P.12 (ventana de indisponibilidad); `Proyectos/GeometriaFactory-Infrastructure/.../CU-10-Preparar-El-Almacen-Al-Arrancar.md`, cuya forma de terminación propia es el **arranque detenido**
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Api

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)

---

## 1. Propósito

Declarar el arranque del servicio: **disparar la preparación del almacén** antes de atender la primera petición, y exponer **A-16**, el punto por el que se responde por el estado del servicio.

Es la responsabilidad que el intake §17.5.P.4 asigna a este proyecto de código en una línea —**aplicar las migraciones al arrancar**— y es una de las dos por las que su flag de persistencia vale true, aunque **el dato lo modele y lo guarde otro**: acá se toma de configuración la ubicación y se dispara la transformación; la transformación la ejecuta el adaptador.

Su forma de terminación es propia y no existe en ningún otro caso de uso de esta categoría: **el arranque detenido**. Cuando el almacén no queda en condiciones, el servicio **no atiende ninguna petición**. Es preferible a atender sobre un almacén en el que no se puede confiar, y es lo que hace que la pieza pública pueda declarar estado degradado en vez de mostrar datos que parecen buenos.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| El proceso de este proyecto de código | Primario | Dispara la preparación y decide si atiende o se detiene |
| Adaptador de preparación del almacén | Sistema | Crea el almacén si no existe y aplica las transformaciones de esquema pendientes |
| `GeometriaFactory-Web` | Consumidor | Consulta el punto de salud desde su propia página de salud |
| Comprobación del despliegue | Consumidor | Consulta el punto de salud para decidir si el contenedor está sano |
| Operador del despliegue | Sistema | El docente, que **despliega a mano** y es el primero que ve un arranque detenido |

## 3. Precondiciones

- La composición terminó y la configuración está tomada (CU-10).
- La ubicación del almacén apunta, en producción, a un **volumen persistente** y nunca al interior de la imagen.

## 4. Flujo principal

1. El proceso dispara la preparación del almacén contra el adaptador.
2. El adaptador crea el almacén si no existe y aplica las transformaciones de esquema pendientes.
3. El almacén queda al día.
4. El servicio empieza a atender peticiones.
5. Llega una petición a **A-16** y se responde `200` con el estado del servicio.

**El punto de salud no exige acceso firmado**, y es una de las cuatro excepciones declaradas de la guardia de CU-02. El motivo es directo: lo consultan la página de salud de la pieza pública y la comprobación del despliegue, y ninguna de las dos tiene credenciales que canjear.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El almacén **no existe** todavía | Se crea y se le aplican todas las transformaciones desde el principio. **Es el camino del primer arranque del laboratorio**, y que funcione solo es criterio de aceptación de una etapa del producto | Paso 3 |
| FA-02 | El almacén existe y ya está al día | No hay ninguna transformación que aplicar y el arranque sigue. **Aplicar transformaciones al arrancar no significa transformar en cada arranque** | Paso 3 |
| FA-03 | Se reemplaza la versión del servicio | El proceso anterior se detiene y el nuevo arranca aplicando lo que falte. **Hay una ventana de indisponibilidad declarada y aceptada**: sin un intermediario que superponga versiones, no hay despliegue sin corte | Paso 1 |
| FA-04 | El servicio está atendiendo y el almacén deja de estar disponible **después** del arranque | **No es un caso de este contrato**: el arranque ya ocurrió. Cada petición afectada termina de forma degradada por CU-09, y el punto de salud lo refleja | Termina fuera de este contrato |

## 6. Excepciones y errores

| Condición | Respuesta | Qué ocurre |
| --- | --- | --- |
| **El esquema encontrado no corresponde al linaje de transformaciones conocido** | El servicio **no atiende** | Arranque detenido. **No se aplica un esquema por aproximación y no se descarta el almacén**: el segundo atajo deja el servicio impecable y **sin los trabajos de nadie**. Causa frecuente declarada aguas abajo: una transformación ya fusionada que se editó |
| **La ubicación configurada del almacén no admite escritura** | El servicio **no atiende** | Arranque detenido. **No se cae hacia una ruta alternativa dentro de la imagen** |
| El servicio está atendiendo y el almacén no responde | `503` en **A-16** | El punto de salud **declara que el servicio no está en condiciones**, en lugar de responder que está sano porque el proceso vive |

**Ningún mensaje de arranque detenido incluye la ruta del almacén**, ni en la respuesta —que no existe— ni en el registro que el operador lee. Es RA-03, y acá tiene una consecuencia práctica incómoda que conviene declarar: **el operador tiene que poder diagnosticar sin que el mensaje le diga la ruta**, y por eso el diagnóstico dice **qué revisar** —el montaje del volumen, el linaje de transformaciones— y no **dónde estaba mirando el proceso**.

## 7. Postcondiciones

- **Arranque con éxito:** el almacén está al día, el servicio atiende y el punto de salud responde `200`.
- **Arranque detenido:** el servicio **no atiende ninguna petición**, el almacén **queda como estaba** y la condición está registrada.
- **En ningún caso** el servicio atiende con el almacén a medio transformar.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una ubicación de almacén vacía, sin ningún almacén creado | Se arranca el servicio | El almacén queda creado y al día **sin ningún paso manual**, y el punto de salud responde `200` |
| CA-02 | Un almacén ya al día | Se arranca el servicio | Arranca igual y **0** transformaciones se aplican |
| CA-03 | Un almacén cuyo esquema no corresponde al linaje conocido | Se arranca el servicio | **No atiende ninguna petición**, y el almacén **conserva todos sus datos**: 0 descartes y 0 aproximaciones |
| CA-04 | Una ubicación de almacén no escribible | Se arranca el servicio | **No atiende ninguna petición**, y **0 almacenes** aparecen dentro de la imagen |
| CA-05 | El punto A-16 | Se invoca **sin** acceso firmado | Responde `200`: es uno de los cuatro puntos que no lo exigen |
| CA-06 | El servicio atendiendo, con el almacén caído | Se invoca A-16 | Responde `503`, y **no** `200` por el solo hecho de que el proceso siga vivo |
| CA-07 | Los mensajes de las dos condiciones de arranque detenido, en el registro que el operador lee | Se inspeccionan | **0 apariciones** de la ruta del almacén, y cada uno dice **qué revisar del lado del despliegue** |
| CA-08 | La imagen del servicio construida con el archivo de construcción del repositorio | Se arranca desde el entorno de desarrollo contenido | Aplica las transformaciones sobre un almacén vacío y **responde salud**. Es la puerta técnica que el intake declara para este proyecto de código |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-03, porque sin almacén al día no hay persistencia; **NB-08**, que es donde esta capa tiene su tramo propio: el punto de salud y el arranque que se detiene son lo que permite que la indisponibilidad se presente como estado degradado explícito |
| Reglas de negocio aplicables | **Ninguna directamente.** Lo que este contrato sostiene es la condición para que todas se puedan ejercer. La que más se le acerca es la conservación íntegra del texto, **por lo que este contrato se niega a hacer**: descartar el almacén ante un esquema divergente lo perdería entero |
| Regla de arquitectura del producto | **RA-03**, con su tensión propia declarada en §6: diagnosticar sin decir la ruta |
| Punto de acceso | A-16 |
| Historias de usuario a generar en 06 | US-27, US-28, US-29 |
| Componentes esperados en 05 | Disparo de la preparación en el arranque; punto de salud; y la decisión de qué informa el punto de salud, que es de aquella categoría |
| Tests previstos en 08 | Los ocho criterios, **de los cuales cuatro son de arranque** y se ejercen construyendo el proceso, no invocándolo. La verificación de la imagen es puerta bloqueante y vive también en `09-Devops` |

## 10. Notas y supuestos

- **El arranque en frío tiene un umbral declarado aguas arriba y rotulado como asunción**, para que la comprobación del despliegue sirva de algo. Se usa como valor vigente y **esta categoría no lo cambia**.
- **La ventana de indisponibilidad en cada reemplazo de versión está aceptada por escrito**, a cambio de no montar un intermediario que superponga versiones. No es un defecto a corregir en esta categoría.
- **Detener el arranque es preferible a atender**, y conviene poder decir por qué en una línea: un servicio que atiende sobre un almacén equivocado **le devuelve a la comisión datos que parecen buenos**, y nadie se entera hasta que alguien busca su trabajo. Un servicio detenido se nota en el primer minuto.
- **Qué informa exactamente el punto de salud no está declarado por ninguna fuente**, más allá de que existe y de quién lo consume. Esta categoría declara **cuándo tiene que decir que no está en condiciones** —cuando el almacén no responde— y **no fija su forma**, que es de `05-Arquitectura-Tecnica`.
- **El respaldo del almacén no es de esta categoría.** El intake lo declara explícitamente «a definir por el docente», y `GeometriaFactory-Infrastructure` lo registra como punto abierto de operación. Acá se nombra sólo para decir que **el arranque no lo reemplaza**: un arranque detenido protege lo que hay, y no lo respalda.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
