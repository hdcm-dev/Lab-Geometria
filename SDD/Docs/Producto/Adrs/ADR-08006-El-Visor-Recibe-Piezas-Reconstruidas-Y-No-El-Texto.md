# ADR-08006 — El visor recibe piezas reconstruidas y no el texto del alumno

**Producto:** Fábrica de Geometría
**Documento:** ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md
**Versión:** 1.1
**Estado:** Aceptado
**Fecha:** 2026-08-16
**Autor:** Product Owner (la decisión) · Orquestador SDD (la redacción)
**Nivel:** Producto
**Tipo:** **Decisión de arquitectura que cambia un contrato declarado**
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14 (`RA-02`), §17.7.P.2 y §20.E-7; `Requerimientos-Tecnicos.md` §8.3
**Trazabilidad downstream:** [`../../Unidades-Entrega/GeometriaFactory-Web/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../../Unidades-Entrega/GeometriaFactory-Web/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md); [`../../Unidades-Entrega/GeometriaFactory-Web/03-UX-UI-DX/Wireframes-Envio-De-Trabajo.md`](../../Unidades-Entrega/GeometriaFactory-Web/03-UX-UI-DX/Wireframes-Envio-De-Trabajo.md); `Definicion-Superficie-HTTP.md`; la etapa `g`

---

## 1. Contexto

El texto que el alumno pega **no es JSON estrictamente válido** y trae cuatro trampas de formato que
un lector ingenuo no sobrevive: la clave sinónima del ortoedro, las comas finales, la cara del cubo
con dos nombres y los valores calculados erróneos. La etapa `f` construyó el lector que las tolera,
en `GeometriaFactory-Infrastructure`, con su batería obligatoria de diez casos.

**Y hasta esta decisión había un segundo lector.** `Definicion-Contrato-De-Fachada.md` declara
`cargarJson(id, texto)`: el bundle recibía **el mismo texto del alumno** y lo leía por su cuenta para
saber de dónde sacar cada dimensión. El intake lo declaraba explícitamente y aclaraba que **no es
duplicar la validación**, porque el backend decide si el trabajo verifica y el bundle sólo necesita
dibujar.

**Es cierto que no duplica la validación, y aun así deja un hueco.** Son **dos implementaciones de la
misma tolerancia**, escritas en dos lenguajes, mantenidas por separado, y **nada verifica que
coincidan**: un texto que el bundle dibuja y el validador rechaza —o al revés— es posible, y ninguna
prueba del producto lo cruzaría. El día que las dos lecturas se separen, el alumno ve tres figuras
dibujadas y un rechazo que habla de dos.

## 2. Decisión

**El visor deja de recibir el texto del alumno y pasa a recibir las piezas ya reconstruidas.**

| | Antes | Desde esta decisión |
| --- | --- | --- |
| Qué recibe la fachada | El texto del alumno, opaco | **Las piezas reconstruidas**, con su posición, su tipo, sus dimensiones y sus componentes |
| Quién tolera el formato | El validador **y** el bundle | **Sólo el validador**, en C# |
| Quién dibuja y rota | El bundle | **El bundle**, igual que antes |
| Quién habla con el servicio de datos | La pieza pública | **La pieza pública**, igual que antes |

**El bundle sigue sin tocar el backend, y eso no cambia.** `RA-02` se conserva entero: el bundle no
hace red, no tiene identidad, no lee configuración y no pide su dato por su cuenta. **Lo recibe de su
anfitrión**, que es lo que siempre hizo; lo único distinto es la forma de ese dato.

**Quién lo decidió y por qué.** El Product Owner, el 2026-08-16, con este fundamento: el bundle es un
visualizador y su trabajo es dibujar y rotar, no interpretar; interpretar el formato del alumno es
del laboratorio, y tenerlo en dos lados es tener dos verdades sobre el mismo texto.

## 3. Consecuencias, incluidas las que cuestan

### 3.1 Lo que mejora

- **Una sola tolerancia, con una sola batería.** Las cuatro trampas del formato viven en un solo
  lugar y las verifican los diez casos obligatorios. El hueco de las dos lecturas **se cierra por
  construcción**, no con una prueba de coherencia entre ellas.
- **La condición `DIMENSION_NO_LEGIBLE` de la fachada pierde su razón de ser.** Era el código con el
  que el bundle enumeraba la pieza cuya dimensión no pudo leer —el escenario `E-8`, `"3,50"` como
  cadena—. Con las piezas ya reconstruidas, **esa pieza no le llega**: el validador la retuvo y emitió
  su error de validación con posición y campo. La frontera que `Definicion-Contrato-Del-Validador-De-Figuras.md`
  §8 describía en dos mitades **pasa a tener una sola**.
- **El bundle se simplifica**: deja de tener tabla de claves sinónimas y de tolerar comas finales.

### 3.2 Lo que cuesta, y hay que decirlo

- **La previsualización pasa a exigir una llamada al servicio de datos.** Hoy el wireframe declara
  que dibuja **sin ninguna llamada**, con la sola precondición de que el área de texto no esté vacía.
  Con esta decisión, previsualizar necesita que alguien reconstruya las piezas, y quien sabe hacerlo
  está del otro lado. **Es la contrapartida directa de la decisión y no un efecto imprevisto.**
- **Hace falta un punto de acceso que interprete sin guardar.** Previsualizar no puede escribir: el
  producto tiene **una sola acción de guardado** y es enviar. El punto nuevo se declara en
  `Definicion-Superficie-HTTP.md` antes de escribirlo.
- **`E-7` deja de ejercitar lo que ejercitaba.** El escenario prueba el visor **sin backend**, con el
  JSON pegado a mano en una página estática, y con eso cubría el mapeo de los seis tipos y la clave
  `Bases`. La propiedad de dibujar sin backend **se conserva** —se pega el DTO en lugar del texto—,
  pero lo que el bundle deja de ejercitar ahí es **la tolerancia**, que ya no es suya.
- **`RT` §8.3 pide no perder la propiedad de `tools_json_figure_viewer`**: que cualquiera pegue el
  texto y vea el dibujo, sin instalar nada. Con esta decisión, **pegar el texto crudo en la página de
  prueba del bundle ya no dibuja**. Sigue dibujando quien pegue el DTO.

**Los dos últimos puntos son cambios sobre afirmaciones del intake y de los requerimientos
técnicos, y este ADR no los reescribe**: se elevan aguas arriba como hallazgo, que es lo que
corresponde cuando una decisión de construcción alcanza un documento del Product Owner.

## 4. Alternativas consideradas

| Alternativa | Por qué no se eligió |
| --- | --- |
| **Dejarlo como estaba** y cerrar el hueco con una prueba de coherencia entre los dos lectores sobre los ocho escenarios | Verifica que hoy coinciden y no impide que mañana no. El defecto que evita es real; la causa —dos implementaciones de la misma tolerancia— queda en pie |
| **Prevalidar en C# y recién entonces habilitar el dibujo**, con el bundle leyendo el texto igual | Suma una llamada al servicio de datos **y conserva los dos lectores**: paga el costo de esta decisión sin cobrar su beneficio |
| **Que el bundle consulte al servicio de datos** | Contradice `RA-02` de frente: el bundle no hace red y no tiene identidad. **Nunca estuvo en consideración** |

## 5. Qué queda por hacer, y en qué orden

1. **Declarar antes de escribir**, que es lo que este ADR habilita: la firma nueva en
   `Definicion-Contrato-De-Fachada.md`, el punto de acceso en `Definicion-Superficie-HTTP.md` y la
   interacción en `Wireframes-Envio-De-Trabajo.md`.
2. **Elevar al Product Owner** el alcance sobre `§20.E-7`, `§20.E-8` y `RT` §8.3.
3. **Construir**, en la etapa `g`: el tipo de transferencia de las piezas, el punto de acceso que
   interpreta sin guardar, la fachada con su firma nueva y el bloque de previsualización.

**Nada de la etapa `f` se deshace.** El validador, su batería y el circuito de envío quedan como
están: esta decisión cambia **a quién más le sirve** lo que ese validador ya produce.

## 6. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial. Registra la decisión del Product Owner de que **el visor reciba las piezas reconstruidas y no el texto del alumno**, con su fundamento —el bundle dibuja y rota, interpretar es del laboratorio— y con sus dos consecuencias caras declaradas: la previsualización pasa a exigir una llamada al servicio de datos, y `E-7` y `RT` §8.3 dejan de ejercitar en el bundle una tolerancia que ya no es suya. Declara que **`RA-02` se conserva entero**: el bundle sigue sin hacer red y recibe su dato del anfitrión. Enumera las tres alternativas descartadas con su motivo. | Product Owner (decisión) · Orquestador SDD |
| 1.1 | 2026-08-16 | **Las tres escrituras aguas arriba quedaron aplicadas**, y con eso §5 punto 2 se cumple: el intake pasa a **2.2** con los dos puntos de escenario reescritos, y `RT` §8.3 lleva sus dos frases al día con autorización explícita del Product Owner sobre su carpeta. **§3.2 se matiza con lo que la lectura completa mostró**: la fuente **ya contemplaba** que el bundle recibiera «el texto **o la estructura**», de modo que esta decisión elige entre dos opciones que estaban abiertas y no contradice esa fila; lo que sí invierte es la asignación de «interpretar para dibujar», que §8.3 le daba al bundle. La constancia está en [`../../Audit/Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md`](../../Audit/Observacion-Alcance-Aguas-Arriba-De-ADR-08006.md) **4.0** §2.3. Ninguna decisión cambia. | Orquestador SDD |
