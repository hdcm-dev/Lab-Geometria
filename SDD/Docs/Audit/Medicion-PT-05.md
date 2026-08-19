# Medición de `PT-05` — el acceso desde la red de la facultad

**Producto:** Fábrica de Geometría
**Documento:** Medicion-PT-05.md
**Versión:** 1.0
**Estado:** **SIN MEDIR** — el formulario existe, la medición no se hizo
**Fecha:** —
**Autor:** —
**Nivel:** Producto
**Puerta técnica:** `PT-05`, fase `i` (`Roadmap-Producto.md` **1.8** §2.2)
**Regla que valida:** `RN-B1`

---

## 1. Qué mide, y por qué su resultado se registra igual si sale mal

**`PT-05` es lo único que la fase `i` hace**, y valida `RN-B1`: *«los alumnos no pueden alcanzar la
aplicación desde la red de la facultad»*, impacto **Alto** — «sin acceso el laboratorio no existe».

**El criterio de transición `i` → `j…` no exige que la medición dé bien.** Exige que **se documente
sea cual sea**: si el acceso no funciona, el número se registra igual y la topología se revisa. Una
puerta que sólo admitiera el resultado bueno no mediría un riesgo, **lo escondería**.

**Este documento existe antes de la medición a propósito.** Un formulario en blanco declara que la
pregunta está hecha y sin responder; no tenerlo se lee como que nadie la hizo. `verify-stage-i.sh`
comprueba que exista **y que su estado ya no diga `SIN MEDIR`**, de modo que el formulario vacío no
pasa la puerta.

## 2. La medición

| Campo | Valor |
|---|---|
| **Fecha y hora** | — |
| **Desde dónde se midió** | — (red de la facultad: aula, laboratorio o wifi; decirlo) |
| **Quién midió** | — (`RN-B1` pide **un alumno de verdad**, no el docente desde su equipo) |
| **Dirección pública del front usada** | — |
| **Dirección del servicio de datos usada** | — |
| **¿El front cargó?** | — |
| **¿El circuito funcionó de punta a punta?** | — |
| **Tiempo hasta la primera pantalla** | — |
| **Resultado** | — (**funciona** / **no funciona** / **funciona con reservas**, y cuáles) |

**La dirección y su fecha se registran juntas, y no es redundante.** `ADR-14003` **1.1** declara que
la dirección del servicio de datos viaja como **IP pública dinámica que se actualiza a mano**: una
puerta en verde sobre una dirección que puede cambiar **no es una garantía permanente**, y sin la
fecha no se sabe hasta cuándo valió.

## 3. Qué se observó

*A completar. Lo que se vio, incluido lo que no se esperaba.*

## 4. Qué se decide con esto

*A completar.*

- **Si funciona:** `RN-B1` queda medida y la fase `i` puede cerrar por este criterio.
- **Si no funciona:** **la topología se revisa**, y esa revisión es trabajo de la fase `i`, no de
  `j…`. El roadmap funda la dependencia `j…` → `i` exactamente en esto: si `PT-05` obligara a mover
  la topología, las tres capacidades diferidas se habrían construido sobre una que va a cambiar.

## 5. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-18 | Emisión del formulario, **antes de la medición y sin ningún dato inventado**. Declara qué mide `PT-05`, por qué su resultado se registra sea cual sea, y los campos que `ADR-14003` **1.1** exige —la dirección usada **y su fecha**—. Estado **`SIN MEDIR`**, que es lo que `verify-stage-i.sh` comprueba para que un formulario vacío no pase la puerta. | Orquestador SDD |
