# Medición de `PT-05` — el acceso desde la red de la facultad

**Producto:** Fábrica de Geometría
**Documento:** Medicion-PT-05.md
**Versión:** 1.4
**Estado:** **SIN MEDIR** — el formulario existe, la medición no se hizo
**Fecha:** —
**Autor:** —
**Nivel:** Producto
**Puerta técnica:** `PT-05`, fase `i` (`Roadmap-Producto.md` §2.2)
**Regla que valida:** `RN-B1`

---

## 1. Qué mide, y por qué su resultado se registra igual si sale mal

**`PT-05` es la única puerta técnica de la fase `i`** —los demás criterios de la transición `i` → `j…` están en `Roadmap-Producto.md` §5.2— y valida `RN-B1`: *«los alumnos no pueden alcanzar la
aplicación desde la red de la facultad»*, impacto **Alto** — «sin acceso el laboratorio no existe».

**El criterio de transición `i` → `j…` no exige que la medición dé bien.** Exige que **se documente
sea cual sea**: si el acceso no funciona, el número se registra igual y la topología se revisa. Una
puerta que sólo admitiera el resultado bueno no mediría un riesgo, **lo escondería**.

**Este documento existe antes de la medición a propósito.** Un formulario en blanco declara que la
pregunta está hecha y sin responder; no tenerlo se lee como que nadie la hizo. `verify-stage-i.sh`
comprueba que exista **y que su estado ya no diga `SIN MEDIR`**, de modo que el formulario vacío no
pasa la puerta.

**La puerta que comprueba este formulario se reescribió el 2026-09-14** (`DD-R8-1` de [`Mesa-2026-09-13-ciclo-2.md`](Mesa-2026-09-13-ciclo-2.md) §7, PR #226): mide los seis criterios vigentes del roadmap, y su `I-3` es este formulario. Hasta esa reescritura medía la topología vieja y quedaba en rojo por construcción.

## 2. La medición

| Campo | Valor |
|---|---|
| **Fecha y hora** | — |
| **Desde dónde se midió** | — (red de la facultad: aula, laboratorio o wifi; decirlo) |
| **Quién midió** | — (`RN-B1` pide **un alumno de verdad**, no el docente desde su equipo) |
| **Dirección pública del front usada** | — (desde el 2026-09-06, un dominio propio publicado por túnel) |
| **Dirección del servicio de datos usada** | — (ídem) |
| **Revisión que corría** | — (la versión del sello del front, que es la misma que informa `/salud`) |
| **¿El front cargó?** | — |
| **¿El circuito funcionó de punta a punta?** | — |
| **Tiempo hasta la primera pantalla** | — |
| **Tiempo aceptable** | **A definir por el Product Owner antes de la visita** (`E-5`, default: «el formulario queda con umbral a definir por el PO; la medición no se bloquea») |
| **Caudal observado** | — (peticiones por minuto durante la visita). `A-5` declara **20 por minuto** para la API, y es **el único umbral de `A-5` que sigue abierto** (`PRODUCT-INTAKE` §22): esta medición es la que se le delegó |
| **Resultado** | — (**funciona** / **no funciona** / **funciona con reservas**, y cuáles). **Qué es «con reservas» lo define el Product Owner** (`E-5`) |
| **Salida de la puerta** | — (la de `PUBLIC_URL=… API_URL=… scripts/verify-stage-i.sh`, pegada tal cual) |

**La dirección y su fecha se registran juntas, y no es redundante.** `ADR-14003` **1.1** declara que
la dirección del servicio de datos viaja como **IP pública dinámica que se actualiza a mano**: una
puerta en verde sobre una dirección que puede cambiar **no es una garantía permanente**, y sin la
fecha no se sabe hasta cuándo valió.

### 2.1 Receta, para que la medición se pueda repetir

Mesa del 2026-09-14, `R-16`: la puerta no tenía receta. Éstos son los pasos, en orden. Cada uno se anota en la tabla de arriba o en §3.

1. **Desde la red de la facultad** —aula, laboratorio o wifi— y **con el equipo de un alumno**, abrir la dirección pública del front. Anotar la hora y **el tiempo hasta la primera pantalla**.
2. Anotar la versión del sello de versión, al pie de la pantalla de ingreso.
3. El alumno **se registra**. El docente **lo habilita** desde el panel de cuentas y le pasa la provisoria.
4. El alumno **entra con la provisoria y elige su contraseña**.
5. El alumno **carga un trabajo, lo envía y lo ve en tres dimensiones**.
6. El docente **lo aprueba con un comentario**, y el alumno ve el desenlace.
7. Si algo falla, **se anota dónde y con qué mensaje, y se sigue** con lo que se pueda: el resultado se registra sea cual sea.
8. Desde cualquier equipo con `git` y `curl`, correr la puerta y pegar su salida en la tabla.

**Los pasos 3 a 6 son también el criterio 4 de la transición** —el circuito completo sobre el despliegue real—, así que una sola visita puede dejar asentados los dos.

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
| 1.4 | 2026-09-14 | **Mesa del 2026-09-14, `R-16` y `E-5` por default.** §2 suma la revisión que corría, el **tiempo aceptable** y la definición de «con reservas» —las dos **a definir por el Product Owner**—, el **caudal observado** contra los 20 por minuto de `A-5` y la salida de la puerta. **§2.1 nueva: la receta.** §1 deja de decir que la puerta no certifica: se reescribió (`DD-R8-1`, PR #226). Las direcciones pasan a ser dominios por túnel. **El estado sigue en `SIN MEDIR`**: la medición no se hizo. Estado anterior en `_legacy/2026-09-14/`. |
| 1.3 | 2026-09-13 | **Parche PR-05 de [`Mesa-2026-09-13-ciclo-2.md`](Mesa-2026-09-13-ciclo-2.md).** Tercera corrección de la misma cita (1.1: 1.8 → 1.9; 1.2: 1.9 → 1.10): esta vez **se quita el número** (`Root-Rules.md` §10 R1) para que no vuelva a envejecer. §1 deja de afirmar que `PT-05` es «lo único que la fase `i` hace» y declara que la puerta no certifica hasta `DD-R8-1`. La medición sigue `SIN MEDIR`. | Orquestador de reanudación SDD |
| 1.2 | 2026-09-12 | **Cita corregida por dato derivado** (`Root-Rules.md` §10): la cabecera citaba `Roadmap-Producto.md` **1.9** y el roadmap está en **1.10** desde la reapertura de la fila `i` por el evento de `Rules-Contexto.md` §3.5 (séptima reanudación, salida `A`). **§2.2 no cambió**: `PT-05` sigue siendo lo único que la fase `i` hace y su criterio de medición no varía con el cambio de topología. **Sigue `SIN MEDIR`**: esta intervención no fabrica una medición — item diferido, evento de cierre «una persona en la red de la facultad corre el formulario», ciclo de origen: mesa · producto · base `5c95dab`. | Orquestador de reanudación SDD |
| 1.1 | 2026-08-27 | **Parche `P-05` de la mesa de evaluación del 2026-08-27** ([`Mesa-2026-08-27.md`](Mesa-2026-08-27.md), hallazgo **H-06**, ancla **E2**, nivel **P3**). La cabecera citaba `Roadmap-Producto.md` **1.8** y el roadmap está en **1.9** desde la reparación de `D-04` del 2026-08-23. **§2.2 no cambió entre las dos versiones** —lo que la 1.9 corrigió es el recuento del criterio `F-1` de §5.2—, de modo que **el formulario no cambia y sigue en `SIN MEDIR`**: lo que se corrige es la cita, que es un dato derivado (`Root-Rules.md` §10). | Orquestador de reanudación SDD |
| 1.0 | 2026-08-18 | Emisión del formulario, **antes de la medición y sin ningún dato inventado**. Declara qué mide `PT-05`, por qué su resultado se registra sea cual sea, y los campos que `ADR-14003` **1.1** exige —la dirección usada **y su fecha**—. Estado **`SIN MEDIR`**, que es lo que `verify-stage-i.sh` comprueba para que un formulario vacío no pase la puerta. | Orquestador SDD |
