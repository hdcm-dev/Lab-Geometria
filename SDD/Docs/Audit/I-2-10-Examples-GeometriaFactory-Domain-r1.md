# Informe de Fase I — incremento 2 · `GeometriaFactory-Domain`, completo

**Producto:** Fábrica de Geometría
**Documento:** I-2-10-Examples-GeometriaFactory-Domain-r1.md
**Versión:** 1.0
**Fecha:** 2026-08-29
**Autor:** Orquestador de Fase I
**Nivel:** Unidad de entrega · `GeometriaFactory-Api`, proyecto de código `GeometriaFactory-Domain`
**Instrumento:** `Master-Prompt.md` §7, Fase I
**Incremento:** **2**, y **cierra el proyecto de código**
**Veredicto:** **APROBADO CON HALLAZGOS**, con la misma reserva de independencia que el incremento 1

---

## 0. El auditor tampoco fue independiente

Igual que en el incremento 1, y por lo mismo: el agente que implementó los dos samples es el que
escribe este informe. Lo que queda **no verificado por correlación** es si los snapshots describen el
comportamiento **correcto**; que el sistema los cumple es mecánico y se sostiene solo.

**Y hay un dato nuevo que hace la reserva más chica que la vez pasada.** Los dos samples
**coincidieron con su snapshot a la primera**, sin tocar el contrato. En el incremento 1 hubo que
corregir §6; acá no, y un snapshot escrito el 2026-08-11 que el código reproduce el 2026-08-29 sin
ajustes es evidencia de que describía algo real.

---

## 1. Qué cerró este incremento

| Sample | Contrato | Sonda | Resultado |
|---|---|---|---|
| `domain/02-intermedio` | `VER-02002` | `SD-02002` | **VERIFICADO** · 13 líneas, CONFORME |
| `domain/03-avanzado` | `VER-02003` | `SD-02003` | **VERIFICADO** · 13 líneas, CONFORME |

**Con esto `GeometriaFactory-Domain` es el primer proyecto de código con sus tres samples
implementados y sus tres contratos verificados.**

**El `03-avanzado` es un arnés y no un recorrido**, y conviene decir qué mide: sus actos `[9]`, `[10]`
y `[11]` **inspeccionan el proyecto de código** en vez de ejercitarlo. Lee el archivo de proyecto del
dominio y cuenta **0 dependencias salientes y 0 bibliotecas de persistencia o transporte**; corre el
mismo recorrido con **dos relojes distintos** y compara; y provoca **12 condiciones** verificando que
las 12 **vuelvan por valor** y ninguna por excepción, que es `ADR-02002` demostrado en vez de
afirmado.

---

## 2. El hallazgo del incremento: el renombre `F-03` no estaba cerrado

**Y lo encontró el sample, no una revisión.** Al escribir el `03-avanzado` contra su §6, tres de sus
líneas pedían códigos en la forma que `F-03` retiró:

```
[1] Trabajo ajeno: TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE
[7b] Desenlace sobre estado terminal: TRANSICION_DESDE_ESTADO_TERMINAL
[7c] Desenlace sin papel de administrador: DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR
```

**El glosario §6.8 los mapea a los tres.** No se habían renombrado porque **están sin comillas
invertidas, dentro de un bloque de salida**, y los cuatro tramos de `R-3` sólo barrieron tokens
entrecomillados.

**Medido sobre el corpus vivo: 73 ocurrencias en 12 documentos**, y **casi todas en `10-Examples`** —es
decir, **en los snapshots que los samples que faltan tienen que reproducir**. El tramo `R-3e` las
renombró, y por eso va en esta misma entrega: sin él, cada sample siguiente heredaba el mismo fallo.

**La declaración de `R-3d` —«`F-03` cerrado, cero pendientes»— era cierta para lo que medía y
demasiado ancha para lo que decía.** Queda corregida acá y en la norma.

---

## 3. Los pasos de la fase

| Paso | Qué pasó |
|---|---|
| **0** · precondición | Cumplida. Tres samples implementados, 494 casos en verde |
| **1** · pasada de ejecución | **Dos contratos a VERIFICADO**, con su salida real y su exit code |
| **2** · categoría 11 | **Ninguno afectado.** Los 15 artefactos siguen en `Planificado` |
| **3** · bitácora de eventualidades | **NO EJECUTADO** — sigue sin existir. Hallazgo `I-01` del incremento 1, sin cerrar |
| **4** · `AGENTS.md` | **NO EJECUTADO** — `Contrato-Agentes.md` sigue sin existir. Hallazgo `I-02`, sin cerrar |
| **5** · ensayo de entrega | Los comandos documentados de las dos carpetas corren y salen 0 |
| **6** · matriz de deriva | `SD-02002` y `SD-02003` a `Verificado`, **con cero deriva sensada**. Quedan **86** en `Sin verificar` |
| **7** · audit | Ver §0 |

---

## 4. Hallazgos

| # | Nivel | Hallazgo | Quién lo cierra |
|---|---|---|---|
| **`I-05`** | **P2** | **El barrido de `F-03` no alcanzaba las ocurrencias sin comillas invertidas**, y eran **73 en 12 documentos**, concentradas en los snapshots de `10-Examples`. **Reparado acá** con el tramo `R-3e` | Cerrado |
| `I-01` | P2 | No hay bitácora de eventualidades. **Sin cerrar desde el incremento 1** | La categoría 11 |
| `I-02` | P1 | `Contrato-Agentes.md` no existe y bloquea `AGENTS.md`. **Sin cerrar desde el incremento 1** | La categoría 11 |

**`I-05` importa más de lo que su nivel dice**, y es la misma clase de hallazgo que `I-03`: **es
predictivo**. Las 73 estaban esperando en los snapshots de los 16 samples que faltan, y cada una
habría hecho fallar su contrato de la misma forma.

---

## 5. Punto de continuación

**Quedan 16 samples**, y el orden que conviene es el del grafo de integración:

| Incremento | Proyecto de código | Samples | Qué hace falta además |
|---|---|---|---|
| 3 | `GeometriaFactory-Contracts` | 3 | Nada: son tipos, como el dominio |
| 4 | `GeometriaFactory-Application` | 3 | Nada |
| 5 | `GeometriaFactory-Infrastructure` | 3 | Un almacén de prueba |
| 6 | `GeometriaFactory-Api` | 3 | **El servicio corriendo**: sus comandos son `run.sh` contra la superficie real |
| 7 | `GeometriaFactory-Visor` | 3 | **Node y el empaquetado del bundle** |
| 8 | `GeometriaFactory-Web` | 1 | Datos de siembra |

**Los incrementos 3 y 4 no necesitan nada que no esté**, y son los que conviene hacer seguidos.

---

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-29 | Emisión inicial. **Incremento 2, que cierra `GeometriaFactory-Domain`**: sus tres samples implementados y sus tres contratos verificados. Los dos de esta entrega **coincidieron con su snapshot a la primera**, sin tocar el contrato. **El hallazgo del incremento es `I-05`**: el barrido de `F-03` no alcanzaba las ocurrencias **sin comillas invertidas** —73 en 12 documentos, casi todas en los snapshots de `10-Examples`—, y la declaración «`F-03` cerrado» de `R-3d` era cierta para lo que medía y demasiado ancha para lo que decía. Reparado con el tramo `R-3e` en esta misma entrega, porque sin él cada sample siguiente heredaba el mismo fallo. `I-01` e `I-02` siguen sin cerrar. | Orquestador de Fase I |
