# EXP-0001 · Actuación 010 — Resolución provisoria y lote al Product Owner

**Expediente:** [EXP-0001](../README.md)
**Tipo:** `resolucion`
**Fecha:** 2026-09-13
**Autor:** Orquestador de migración normativa SDD
**Foliada:** 010 · **Forma del lote:** `Master-Prompt.md` §8.1 y §7.0 (lote de la fase), `Mesa-Rules.md` §7.1

---

## 1. Resolución provisoria

La migración normativa de `Lab-Geometria` de SDD 13.7 a 13.16 queda **parcial y declarada** (`Migracion-Rules.md` §4.6): plan 1.2, mesa 1.1 e informe de M6 emitidos en `SDD/Docs/Audit/`; siete propuestas con texto exacto verificadas; procedencia en 13.7. **Se completa con un solo acto del Product Owner** —los puntos A y B de abajo— seguido de `propuestas/aplicar-propuestas.sh` en sus tres fases, un audit de cierre y M5.

## 2. El lote, con lo que rige si no se responde

```text
LOTE DE LA FASE — EXP-0001, migración 13.7 → 13.16 · 2026-09-13

A · ¿Aprobás el plan de migración 1.2?
  QUÉ PASÓ         Master-Prompt-Migracion.md §5 detiene M1 hasta la aprobación del plan. El plan lleva los
                   30 veredictos de la mesa y no tiene filas sin resolver.
  ORIGEN DEL HECHO  ajeno a la corrida · calculado contra b9675d8: la detención es de la norma, no de un estado
                   que la corrida dejó a medias.
  OPCIONES         (1) aprobar; (2) aprobar con reservas sobre filas concretas; (3) no aprobar.
  PROPUESTA        (1). Alternativa razonable: (2) sobre PM-06, si preferís que las 23 filas no derivables se
                   deriven a mano antes de escribir.
  SI NO RESPONDÉS  nada se escribe en SDD/Intake ni en SDD/Docs; el destino sigue en 13.7 con este expediente.
  QUÉ NECESITO     «aprobado» o la lista de filas con reserva.

B · ¿Aprobás el diff del intake (P-01, 4.6 → 5.0) y, con él, la re-derivación del manifiesto (P-02)?
  QUÉ PASÓ         Master-Prompt.md §13 caso (b): el intake sólo se reescribe con tu aprobación explícita, que no
                   se delega (dictamen D-1; actuación 008). Diff de estructura: ninguna sección se mueve, se parte ni
                   se colapsa; cambian §13.2, §13.3, §16.1, §16, §17.2, §19, §23 y la trazabilidad. Batería: vacía.
  ORIGEN DEL HECHO  ajeno a la corrida · calculado contra b9675d8.
  OPCIONES         (1) aprobar; (2) aprobar con cambios; (3) no aprobar.
  PROPUESTA        (1). Alternativa razonable: (2) si querés otro nombre para el perfil npm o para el párrafo del visor.
  SI NO RESPONDÉS  el intake sigue en 4.6 y las propuestas quedan en el expediente.
  QUÉ NECESITO     una actuación de aprobación con fecha (una línea alcanza); después:
                   bash SDD/Expedientes/0001-Migracion-Normativa-A-13.16/propuestas/aplicar-propuestas.sh --aprobacion NNN --fase intake
                   … --fase manifiesto  (confirmación de M3)   … --fase docs  (M4)

C · visor.bundle.js no existe: el artefacto es geometriafactory-visor.js (intake l.496, 1154, 1272, 1381)
  QUÉ PASÓ         webpack.config.js:12, scripts/build-visor.sh:28 y los samples nombran geometriafactory-visor.js.
                   No es del salto 13.16 y la mesa no lo metió en P-01 (veredicto 12).
  ORIGEN DEL HECHO  ajeno a la corrida · calculado contra b9675d8.
  OPCIONES         (1) corregir las cuatro menciones por caso (a) de §13 con esta respuesta; (2) dejarlo como deuda.
  PROPUESTA        (1). Alternativa razonable: (2) si preferís tocarlo vos en la próxima edición del intake.
  SI NO RESPONDÉS  rige (2): DD-5 de Mesa-2026-09-13.md §8.
  QUÉ NECESITO     «corregí» o «déjalo».

D · Un hueco que la clasificación §4.8 eleva: Product-Backlog.md de GeometriaFactory-Api :749, PA-09
  QUÉ PASÓ         «La construcción de la imagen en destino desde el repositorio [A VERIFICAR]»: la regla del backlog
                   6.0 no trata el tema y el mecanismo lo eleva (E-015). Es el espejo de 05 §11 PA-08, que sí es
                   hueco del ciclo (Rules-Arquitectura 6.0 lo trata).
  ORIGEN DEL HECHO  ajeno a la corrida · calculado contra b9675d8.
  OPCIONES         (1) tratarlo como hueco del ciclo, por espejo de PA-08; (2) tratarlo aparte.
  PROPUESTA        (1). Alternativa razonable: ninguna con efecto distinto: la fila sigue abierta hasta que PT-04 se
                   registre como hecho en 05.
  SI NO RESPONDÉS  rige (1).
  QUÉ NECESITO     nada, salvo que quieras (2).

E · Un segundo hueco que la clasificación §4.8 eleva: Pipeline-CI-CD.md de GeometriaFactory-Api :597, PD-04
  QUÉ PASÓ         «La frecuencia del respaldo del almacén, que el intake declara ‘a definir por el docente’»: Rules-Devops
                   6.0 no trata el respaldo (E-015 v3, 0 ocurrencias a inicio de palabra) y el mecanismo lo eleva. La fila
                   ya te nombra como quien lo cierra, y Mesa-2026-09-12.md §8 lleva el mecanismo de respaldo (E-05, RN-B8)
                   como deuda tuya.
  ORIGEN DEL HECHO  ajeno a la corrida · calculado contra b9675d8.
  OPCIONES         (1) tratarlo como hueco del ciclo, por espejo de 05 §11 PA-07 (que Rules-Arquitectura 6.0 sí trata);
                   (2) tratarlo aparte, junto con la deuda de Mesa-2026-09-12 §8.
  PROPUESTA        (1). Alternativa razonable: (2), si vas a decidir el respaldo en la misma pasada.
  SI NO RESPONDÉS  rige (1).
  QUÉ NECESITO     nada, salvo que quieras (2).
```

**Ninguna otra pregunta sobrevivió a la mesa.** Los puntos D y E no son preguntas de producto: son los dos huecos que el mecanismo de `Migracion-Rules.md` §4.8 eleva por construcción, con su default declarado. D-2 (`ADR-14003`), D-3, D-4, D-5 y D-6 se resolvieron leyendo el conjunto; la mesa descartó preguntar el valor de `API_BASE_URL`.

## 3. Continuación fuera de este expediente

Migrar `RPI.VideoControl` a 13.16 (actuación 001, P-2): expediente propio, cuando el Product Owner lo disponga.
