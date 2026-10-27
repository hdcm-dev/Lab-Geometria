# ADR-10010 — La ayuda de usuario acompaña, sin resolver el trabajo

**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ADR-10010-La-Ayuda-De-Usuario-Acompana-Sin-Resolver-El-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado (por delegación: pedido del Product Owner del 2026-10-27, citado en §1)
**Fecha:** 2026-10-27
**Autor:** Mesa de lógica de decisión del 2026-10-27, primer caso de aplicación de `Knowledge-Logica-De-Decision`
**Categoría:** Experiencia de uso
**Tipo:** Decisión consecuente: **ratifica y acota** la fila «Ayuda y documentación» de `../../03-UX-UI-DX/Experiencia-De-Uso.md` §2.1

---

## 1. Contexto

La fila de heurísticas «Ayuda y documentación» de `Experiencia-De-Uso.md` §2.1 decía: «El producto no lleva manual: la ayuda es contextual y mínima». La mesa DocAsCode quiso sumar ayuda de usuario como piloto del sistema de documentación, leyó esa fila como un veto y elevó la superación al Product Owner (filas L3 y L4 de su lote).

El Product Owner respondió el 2026-10-27, literal: «Geometria es un proyecto academico, lo pense para que mis alumnos hicieran un json desde un programa en c# y luego importaran y vieran las geometrias en ese servicio — con esto mis alumnos tenian que hacer las clases, entender que debian respetar los nombres de los atributos, la jerarquia de objetos y clases, etc. — si hay un ADR que te traba, tenes permiso de modificarlo […] la documentación es un aditicional no una decisión que implica rehacer el programa […] podría darse una ADR Consecuente que ratifique esa decisión».

**Rango verificado:** la fila es una heurística de Nielsen aplicada al producto. No es un ADR ni una restricción dura, así que se actualiza en el mismo cambio y esta decisión deja asentado el porqué.

## 2. Decisión

1. **Se ratifica** la ayuda contextual y mínima de cada superficie: el requisito bajo el campo, la leyenda del estado vacío y el texto de la confirmación escrita.
2. **Se acota «sin manual»**: el producto **puede** llevar ayuda de usuario, como páginas de ayuda o una guía de uso, que explique **cómo usar el servicio**: entrar, importar, leer los errores de interpretación y ver las geometrías.
3. **Límite didáctico:** **la ayuda no entrega el JSON ni las clases resueltas**. Puede describir el formato de intercambio y sus reglas, que ya son públicas en el contrato, y mostrar ejemplos que no coincidan con un trabajo práctico vigente. Nunca da la solución de lo que el alumno tiene que construir en C#: las clases, los nombres de atributos que tiene que deducir, la jerarquía de objetos ni el JSON de un trabajo.

## 3. Estado

Aprobado por delegación del Product Owner (§1). Se avisa en el cierre de la mesa de lógica de decisión (`Y01-Aplicacion`).

## 4. Alternativas consideradas

| Alternativa | Por qué no |
| --- | --- |
| Mantener «sin manual» y no sumar ayuda | Deja sin objeto la piloto de documentación, y el objetivo académico no lo exige |
| Superar la fila sin límite | Pone en riesgo el objetivo: el alumno podría copiar la solución en vez de construirla |

## 5. Consecuencias positivas

- Habilita la ayuda de usuario como piloto del sistema de documentación.
- El objetivo académico queda protegido por un límite escrito y verificable.

## 6. Consecuencias negativas y trade-offs

- Cada página de ayuda tiene que revisarse contra el límite del punto 2.3. La revisión pasa a formar parte del criterio de aceptación de esa documentación.

## 7. Implementación

- La fila «Ayuda y documentación» de `Experiencia-De-Uso.md` §2.1 cita esta decisión (versión 1.5 de ese documento).
- No cambia el código, el contrato, el esquema ni las capas.
- **Reversión, en una línea:** se revierte el commit que agrega esta ADR y la fila vuelve a la 1.4.

## 8. Métricas de validación

- Ninguna página de ayuda contiene una clase C# ni un JSON que resuelva un trabajo práctico vigente (revisión de la documentación antes de publicarla).

## 9. Referencias

- `../../03-UX-UI-DX/Experiencia-De-Uso.md` §2.1, fila «Ayuda y documentación».
- `Knowledge-Logica-De-Decision` §3.2 (IA.Knowledge.Base): una decisión propia del destino se supera, acota o ratifica con una decisión consecuente.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-10-27 | Emisión inicial. Ratifica la ayuda contextual, acota «sin manual» y fija el límite didáctico. Es la lección `DEC-001` de la mesa de lógica de decisión. |
