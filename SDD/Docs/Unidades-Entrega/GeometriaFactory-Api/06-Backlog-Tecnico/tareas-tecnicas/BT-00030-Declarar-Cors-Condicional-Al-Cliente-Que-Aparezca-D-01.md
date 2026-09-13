# BT-00030 — Declarar CORS, condicional a que aparezca un cliente propio de navegador desde otro origen

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00030-Declarar-Cors-Condicional-Al-Cliente-Que-Aparezca-D-01.md
**Versión:** 1.3
**Estado:** Ready
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** indagación
**Prioridad:** Media
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Declarar CORS, condicional a que aparezca un cliente propio que sea JavaScript de navegador desde otro origen.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 5; `05` §9.1 (`GeometriaFactory-Api`), riesgo **«Que se agregue un punto de acceso pensado para el navegador, o se configure el intercambio de origen cruzado»**, mitigado hoy por «las tres ausencias declaradas de la superficie de 02» y por que «el único cliente legítimo esté declarado en el manifiesto y en el grafo»; la condición es la que [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) y `PRODUCT-INTAKE` **4.4** §17.1.P.3 (fila «CORS») dejan: que aparezca un cliente propio que sea JavaScript de navegador desde otro origen (`D-01` cerrado: el cliente es cualquier aplicación propia, y su forma no cambia la autenticación)

## 3. Criterios de aceptación

- Mientras no aparezca un cliente propio que sea JavaScript de navegador desde otro origen, el documento de arquitectura declara **explícitamente por qué CORS no aplica hoy** (no hay cliente de navegador de otro origen conocido)
- si aparece un cliente propio que sea JavaScript de navegador desde otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: la etapa `k`, que cierra en su punto de control, con la declaración de por qué CORS no aplica o con la política por origen explícito si ese cliente apareció**

## 4. Dependencias

Ninguna.

## 5. Tipo

`indagación`. Si aparece un cliente propio que sea JavaScript de navegador desde otro origen, se agrega una política por origen explícito (OWASP CORS Cheat Sheet) antes de habilitar cualquier origen. **Caja temporal: la etapa `k`, que cierra en su punto de control, con la declaración de por qué CORS no aplica o con la política por origen explícito si ese cliente apareció**.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: condicionada a un cliente propio de navegador desde otro origen (`ADR-00009`), y no duplica esa decisión |
| CU upstream | — (sin CU: condicionado a que aparezca un cliente propio de navegador desde otro origen) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `05` §9.1 (`GeometriaFactory-Api`), riesgo de CORS/navegador; [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) y `PRODUCT-INTAKE` **4.4** §17.1.P.3 fila «CORS» (la condición) |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00030 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1) y **corrección de fuente**. §2 y §7 citaban sólo el ítem diferido `D-01`, que no es punto abierto de `05` §11 y no es fuente admitida por el criterio 1 en la letra. Existe una fuente admitida que sostiene la misma tarea: `05` §9.1 ya trae el riesgo «que se agregue un punto de acceso pensado para el navegador, o se configure el intercambio de origen cruzado», con probabilidad baja e impacto muy alto, mitigado hoy por las tres ausencias declaradas de la superficie de 02. Se agrega esa cita en §2 y en §7, conservando `D-01` como condición adicional (quién es el cliente, no si CORS aplica hoy). **Ningún criterio de aceptación cambia**: la tarea sigue siendo de tipo `indagación` con caja temporal hasta que `D-01` cierre, que es la excepción de DoR §3.1 para este tipo de tarea. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Corrección del criterio 6, detectada por el orquestador contra el árbol.** El «Sí» de v1.1 descansaba en una equivalencia no admitida por la letra del criterio 6 de `Definition-Of-Ready.md` §2.1: «caja temporal expresada en etapas o en el punto de control que la cierra», y «hasta que `D-01` cierre» no fija techo, porque `D-01` es un ítem diferido «sin evento ocurrido todavía» (`Mesa-2026-09-12-ciclo-2.md`). Las demás indagaciones de la unidad anclan la caja a una etapa o a una puerta (`BT-00005`/`07`/`09`/`10`: «la etapa `a`»; `BT-00025`: «antes de fijar la puerta en 09»; `BT-00026`: «antes de la etapa de despliegue real»). Se corrige la caja, en los dos lugares donde aparecía (criterios de aceptación y §5), a: «la etapa `k`, que cierra en su punto de control, con `D-01` cerrado o con la declaración de por qué CORS no aplica». El primer criterio de aceptación ya obligaba a esa declaración mientras `D-01` siga abierto, así que la corrección no cambia ningún comportamiento exigido: sólo repara la letra del criterio 6. Sigue **Ready**. Evidencia y corrección en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md), fila del criterio 6 de BT-00030. |
| 1.3 | 2026-09-12 | **Propagación de [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md)** (la API autentica personas, no aplicaciones; **Aceptado**): `D-01` **cerró** —el cliente es cualquier aplicación propia que actúe en nombre de una de las dos personas, sin tercera clase de identidad ni de cliente— y esta ficha hablaba de `D-01` como abierto en el título, §1, §2, §3, §5 y §7. La condición pasa a ser la única que dispara CORS: **que aparezca un cliente propio que sea JavaScript de navegador desde otro origen**; sin mención a terceros. La caja temporal (criterio 6 de la DoR) sigue anclada a «la etapa `k`, que cierra en su punto de control». §4 retira la dependencia de `BT-00028`, que pasó a `Descartada`: queda **sin dependencias**. El archivo conserva su nombre (con `D-01`) para no romper los enlaces del catálogo, del Mini-Plan y de la auditoría. Reevaluación de los criterios 5 y 6 de `Definition-Of-Ready.md` §2.1: **5: Sí** (sin dependencias); **6: Sí** (caja en etapa y punto de control). Sigue **Ready**. |
