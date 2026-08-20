# A1 — Las 34 filas sin clasificar, repartidas

**Producto:** Fábrica de Geometría
**Documento:** Clasificacion-Pendientes-A1.md
**Versión:** 1.0
**Fecha:** 2026-08-20
**Instrumento:** paso **A1** de `Plan-Cierre-De-Pendientes.md` §2.2
**Estado:** **Clasificación. Ningún punto abierto cerrado por este documento**

---

## 1. Qué hace este documento y qué no

**Reparte las 34 filas que la clasificación por palabra clave no pudo ubicar**, leyendo cada enunciado
y contrastándolo contra el árbol. **No cierra ninguna**: decir «esto ya está decidido en el código» es
el insumo de `A2`, no `A2`.

**El resultado corrige hacia abajo la estimación del plan.** Decía «como máximo 25 de 90 son tuyas»;
con las 34 repartidas, **son menos**.

---

## 2. Los cuatro grupos que se cierran leyendo, verificados sobre el árbol

| Grupo | Filas | Qué pide el punto | Dónde ya está, verificado |
|---|---|---|---|
| **G1 · Nombres definitivos de tipos y espacios de nombres** | **10** | «Los nombres definitivos … atados al punto de control de la etapa `a`» | `Producto/Norma-De-Nomenclatura.md`, con **1132 líneas de glosario**, y **23 espacios de nombres vivos** en `src/` |
| **G2 · Identificador del puerto de repositorio de cuentas** | **3** | «El intake nombra tres puertos y no éste» | **`Application/Ports/IAccountRepository.cs`**. El cuarto puerto existe y está nombrado |
| **G3 · Si el bundle se versiona o se ignora** | **3** | La decisión sobre `visor/dist/` | **`.gitignore:439`**, con su motivo escrito al lado: *«bundle NO se versiona (lo genera la canalización antes de publicar)»*, `AP-04` |
| **G4 · Navegador con capacidad 3D en el ejecutor** | **2** | Que el ejecutor provea navegador tridimensional | **`scripts/verify-stage-g.sh:71`**, con la imagen **anclada**: `mcr.microsoft.com/playwright:v1.48.0-jammy` |

**Subtotal: 18 de 34.** Las cuatro tienen cita literal a un archivo del árbol, de modo que por la
pregunta previa de `Master-Prompt.md` §8.1 **son trabajo propio y no detención**.

---

## 3. Las 16 restantes

| Clase | Filas | Cuáles | Desenlace |
|---|---|---|---|
| **Decididas en su propio caso de uso** | **9** | Tipos reconstruibles, la provisoria que «no se repite», los sellos, el área de la pieza volumétrica, la ambigüedad `RN-02012`, la fecha de última modificación | **Sus propios enunciados dicen «`CU-…` §10 **adopta**»**. La decisión está tomada y escrita; falta reflejarla en la fila. **Trabajo propio**, con la salvedad de §4 |
| **Requisito declarado por capacidad y no por versión** | **2** | La versión mínima de navegador | **La fuente decidió no fijar versión**: el requisito se declara por capacidad. Es una decisión tomada, no una omisión. **Trabajo propio** |
| **Formato de intercambio** | **1** | `PA-05` del Web, que remite a `05` §11 `PA-03` | **A leer en la 05**; el enunciado dice que **no es de ese proyecto decidirlo** |
| **Frecuencia del respaldo** | **3** | `PD-04`, `PA-07`, `PA-07` | **TUYA.** El propio enunciado dice que el intake la declara **«a definir por el docente»** y que **no es una omisión** |
| **Mutation score en el pipeline** | **1** | `PD-03` de la 09 | **TUYA.** Verificado: **no hay ninguna herramienta** en el árbol, y `CV-19` se reporta «sin medir» con su hueco declarado |

---

## 4. La salvedad sobre las 9 «decididas en su caso de uso»

**No las verifiqué una por una contra su `CU`.** Lo que verifiqué es que **su propio enunciado dice
que el caso de uso las adopta** —«`CU-06007` §10 adopta que la sostiene la impredecibilidad»,
«`CU-06002` §10 adopta la suma de los componentes»—, y localicé la definición del área en
`Definicion-Contrato-Del-Validador-De-Figuras.md`.

**Que la fila diga que el `CU` lo adopta no prueba que el `CU` lo diga.** Es exactamente la clase de
afirmación que este destino ya vio fallar —una fuente declarativa que quedó atrás—, así que **`A2`
tiene que abrir cada `CU` y citarlo**, y si alguno no lo dice, la fila pasa a la columna de decisiones
tuyas en lugar de cerrarse.

---

## 5. Reparto resultante, sobre las 90

| | Filas | Antes de A1 |
|---|---|---|
| **Trabajo propio** — se cierra leyendo y citando | **~57** | estimado 65 |
| **Decisiones tuyas** | **~19** | estimado 25 |
| **A confirmar en A2** | **~14** | las 9 de §4 más las 5 que dependen de abrir su fuente |

**La estimación de decisiones tuyas baja de 25 a ~19**, y las cuatro más claras son: **la frecuencia
del respaldo** (3 filas, que el intake manda al docente), **el mutation score** (1), y los valores
`[ASUNCIÓN]` de la familia `F3` original.

## 6. Lo que este documento no sabe

- **Si las 9 de §4 se sostienen.** Declarado en §4. Es lo primero que `A2` tiene que comprobar.
- **Si alguna de las 90 ya no aplica.** Sigue sin evaluarse por vigencia, igual que en el plan.
  **Retirar una puede seguir siendo tan válido como cerrarla.**
- **Si las filas repetidas son una decisión o varias.** `G1` son **10 filas** y probablemente **una
  sola** decisión —los nombres— repetida por proyecto de código. El recuento de §5 cuenta **filas**.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
|---|---|---|---|
| 1.0 | 2026-08-20 | Emisión inicial, paso **A1**. Reparte las **34** filas que la clasificación por palabra clave del plan no pudo ubicar. **18 se cierran leyendo**, en cuatro grupos con **cita literal a un archivo del árbol**: los nombres definitivos —`Norma-De-Nomenclatura.md` con 1132 líneas de glosario y 23 espacios de nombres vivos—, el puerto de cuentas —`IAccountRepository.cs`—, el bundle —`.gitignore:439` con su motivo—, y el navegador tridimensional —`verify-stage-g.sh:71`, con la imagen de Playwright **anclada**—. De las 16 restantes, **9 dicen que su propio caso de uso las adopta** y **4 son tuyas** —la frecuencia del respaldo, que el intake manda «a definir por el docente», y el mutation score, sin herramienta en el árbol—. **§4 declara la salvedad que decide el frente**: que una fila diga que el `CU` lo adopta **no prueba que el `CU` lo diga**, y `A2` tiene que abrir cada uno y citarlo. La estimación de decisiones del Product Owner **baja de 25 a ~19**. | Orquestador SDD |
