# E-017 · Jurado de Mesa-2026-09-13, íntegro

Origen: cinco jueces con funciones objetivo (Evidencia, Impacto, Costo y beneficio, Coherencia histórica, Riesgo e irreversibilidad), despachados por AG-00970 con el mandato de `Mesa-Rules.md` §6.4, sobre E-019, E-018 y E-021. Votos: P = PROCEDE, N = NO_PROCEDE, I = INSUFICIENTE. Veto de riesgo no usado.

| # | Ítem / grupo | Ev | Imp | C/B | Coh | Rgo | Veredicto |
|---|---|---|---|---|---|---|---|
| 1 | Ciclo de origen: VER-01/02/03, LEC-03, TRZ-01/02, FOR-01, ORQ-A3, REF-07 | P: E-006 falla medido (111/113, 52/113 fuera del documento) y v4 reproducible | P: sin esto PM-06 escribe un origen falso y queda P1 por Root-Rules §12.2 | P: el script ya existe, falta ajustar el criterio | P: no reabre nada; aplica Migracion-Rules §4.9 | P: sale del historial y se regenera | PROCEDE 5-0 |
| 2 | Clasificación §4.8: VER-04, FOR-02 | P: con 0 elevados el criterio da 0=0 | P: el criterio del plan §6 no mide nada | P: el paso 3 se puede ejecutar | P | P | PROCEDE 5-0 |
| 3 | PM-07 / PM-08: VER-05, REF-N2 | P | P | P | P: PD-VER-01/02 «Decidido 2026-09-12» son registro | I: el panel no fija el universo «>118» con número | PROCEDE 4-1 |
| 4 | VER-06 (E-004 no prueba §3.6) | P: probado fallando | P: PM-05 sin cierre | P | P | P | PROCEDE 5-0 |
| 5 | REQ-01 (barrido incompleto) | P: E1 confirmado | P: PM-01 deja residuos vivos | P | P | P | PROCEDE 5-0 |
| 6 | D-4: ARQ-06, LEC-05 | P | P | P | P | I: 11-Documentacion como deuda previa no medido con comando | PROCEDE 4-1 |
| 7 | Forma de §16.1: REQ-06, ARQ-05 | P: `library` no es D8 de ninguna UE | P | P | P | P | PROCEDE 5-0 |
| 8 | ARQ-05 `[A VERIFICAR]` vs REF-06 | I: el enlace roto prueba falta de documento, no de UE | P: el hueco es real | N: §2.C ya lo asigna | N: precedente intake 2.0 l.1984 | N: degrada lo derivable | NO_PROCEDE 3-1-1 |
| 9 | Puntos 2 y 3 del visor: REQ-03, REF-05, LEC-06 | P | P: el perfil absorbe sólo el punto 1 | P | P: intake 4.0 ya los decidió | P | PROCEDE 5-0 |
| 10 | ARQ-03 (ADR nuevo ahora) | N: la decisión existe, falta su forma | P: Intake-Rules §4 lo manda | N: sería invención (§4.1) | N: es de la fase 05 | N | NO_PROCEDE 4-1 |
| 11 | Arista del visor y grafo único: ARQ-01, ARQ-02, FOR-03, REQ-04, REF-08 | P | P: el manifiesto afirma que el visor no participa | P | P: aplica ADR-10008 | P | PROCEDE 5-0 (P2) |
| 12 | `visor.bundle.js`: REQ-05, ARQ-04, REF-08 | P | P | P | P: no es diff estructural | I: el intake no se escribe sin su autor | PROCEDE 4-1 (hecho y vía) |
| 13 | PM-04 fila Visor→Web: OPE-01, OPE-03→P2, OPE-04, REF-04, ORQ-A5 | P: build 553/553 sin Node y MSB3073 | P: la fila afirma «lo genera la canalización» | P | P: la etapa Node es un ambiente | P | PROCEDE 5-0 |
| 14 | FOR-04 (caminos a visor/dist en PM-05) | P | I | N: los samples no son proyectos de §13.2 | N: código fuera (§2.2) | N | NO_PROCEDE 3-1-1 |
| 15 | D-2 / ADR-14003: OPE-05, REF-01 | P: ADR l.9 y changelog l.1097 | I: el secreto no vive en ningún repo | P | P: Mesa-09-12 §8 ya lo asignó | P | PROCEDE REF-01 4-1; la pregunta NO_PROCEDE (ancla C) |
| 16 | FOR-05 (tres resultados) | P | P | P | P | N: el plan ya condiciona el contador | PROCEDE 4-1 |
| 17 | D-1: LEC-02, REF-02 | P: MP §13 l.1783 y MP-Mig l.242 | P | P | P: la actuación 001 §3 ya lo dice | P | PROCEDE 5-0 |
| 18 | D-6: REQ-02, LEC-05, REF-03 | P: §13 «dos y ningún otro» | P | P | P: precedente intake 3.0 | P | PROCEDE 5-0 |
| 19 | D-3 + TRZ-05 | P: colisión medida en 0 | P | P | I: tocar filas cerradas roza el registro | P | PROCEDE 4-1 |
| 20 | Inventario de diferidos: TRZ-04, ORQ-A4 | P | P | P | P | I: las 16 menciones en prosa sin clasificar | PROCEDE 4-1 |
| 21 | Deuda de cabeceras: ORQ-A1, TRZ-06 | P: Mesa-08-27 §8, 456 ocurrencias | P: P1 por §12.2 si se deja pasar | I: 456 celdas | P: la deuda nombra esta M4 | P | PROCEDE 4-1 |
| 22 | LEC-06 | P | P (P3) | P | P | P | PROCEDE 5-0, autocorrección |
| 23 | LEC-01 | P | P | P | P: «sin push» ya está en restricciones duras | P | PROCEDE 5-0 |
| 24 | LEC-04 | P: lista cerrada §2.1 | P | P | P | P | PROCEDE 5-0 |
| 25 | OPE-02 | P | P | N: fuera del salto | N: canales tocan la topología | P: mismo documento que PM-04 | PROCEDE 3-2, parcial |
| 26 | OPE-06 | P | P | P | P | P | PROCEDE 5-0 |
| 27 | Ejemplos de Contracts: REF-N1, ORQ-A2 | P: E-010 | P | P | P | P | PROCEDE 5-0 |
| 28 | FOR-06 | P | P (P3) | I: el plan ya dice «por carpeta» | P | P | PROCEDE 4-1 |
| 29 | D-5: TRZ-03, LEC-06 | P | P | P | I: agrega columna a tablas aprobadas | P: §12.2 punto 5 lo exige | PROCEDE 4-1 |
| 30 | ORQ-A5 | P: E-012 | P | P | P | P | PROCEDE 5-0 |

Dictámenes D-1 a D-6, deuda DD-1 a DD-6 y «ninguna escalada»: transcriptos en `SDD/Docs/Audit/Mesa-2026-09-13.md` §6.1, §8 y §9. Homogeneidad: 16 de 30 filas 5-0 (53 %).
