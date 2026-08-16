# ADR-02006 — El dominio no lee el reloj ni el conjunto de entidades: los dos entran por parámetro

**Proyecto de código:** GeometriaFactory-Domain
**Documento:** ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

Dos cosas que el modelo necesita no están dentro del alcance de una entidad:

- **El momento.** El trabajo lleva fecha de creación y fecha de última modificación, y la cuenta lleva fecha de alta. [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1 y §2.2 declaran que las tres **las aporta el consumidor** y que el dominio no lee ningún reloj. `GeometriaFactory-Application` es quien tiene el reloj como puerto (§7 de ese mismo documento).
- **El conjunto.** INV-01 —el correo del alumno es único en todo el sistema— se afirma sobre el conjunto de alumnos, y una entidad no conoce a ese conjunto. La unicidad efectiva la resuelve el consumidor con el puerto de repositorio.

La tercera cosa que no está: **la fecha que el alumno escribe en su trabajo no es ninguna de las anteriores**. Es dato del alumno y no del reloj, y el modelo la distingue explícitamente de la fecha de creación y de la de última modificación.

Motivación upstream: RN-02002, RN-02007, RN-02008; INV-01; `PRODUCT-INTAKE` §17.1.P.1 · GeometriaFactory-Domain (sin dependencias), §17.1.P.11 · GeometriaFactory-Application punto 3 (el reloj como puerto de la capa de aplicación) y §17.1.P.4 · GeometriaFactory-Infrastructure (sellos de tiempo del trabajo).

## 2. Decisión

**El momento y la unicidad ya resuelta entran al dominio por parámetro de la operación que los necesita.** El proyecto de código no obtiene el momento por su cuenta, no consulta conjuntos de entidades y no declara ningún puerto propio para hacerlo: el puerto vive en `GeometriaFactory-Application`, que es su dueño.

Cuando una operación exige unicidad, su contrato declara que **la unicidad ya fue verificada por el consumidor** y el dominio no la vuelve a comprobar. Lo que el dominio sí hace es rechazar la operación si el consumidor declara que la unicidad no se cumple.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Momento y unicidad por parámetro (**adoptada**) | Conserva las cero dependencias; las pruebas son reproducibles porque el tiempo es un dato; ninguna operación depende del entorno | Cada operación que necesita el momento lo lleva en su firma, lo que engrosa el contrato; el consumidor tiene que acordarse de aportarlo |
| Un puerto de reloj declarado en el dominio | El dominio expresaría por sí mismo que necesita el tiempo | Duplicaría el puerto que `GeometriaFactory-Application` ya declara, y el intake asigna ese puerto explícitamente a la capa de aplicación (§17.1.P.11 · GeometriaFactory-Application punto 3) |
| Leer el reloj del sistema dentro del dominio | Firmas más cortas | Mete una dependencia de entorno en el nivel 0 y hace irreproducibles las pruebas de todo lo que dependa de fechas; es el defecto que ADR-02001 protege |
| Un puerto de repositorio en el dominio para verificar la unicidad | INV-01 se ejercería entero acá | Obliga al dominio a conocer el concepto de consulta, que es exactamente lo que `Definicion-Modelo-De-Dominio.md` §7 pone del otro lado de la frontera |

## 5. Consecuencias positivas

1. Las pruebas del dominio son **reproducibles y deterministas**: dos corridas con el mismo dato dan el mismo resultado, sin importar cuándo se ejecuten.
2. Las cero dependencias de [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) se conservan.
3. La zona horaria y la precisión del momento se deciden en un solo lugar del producto, el dueño del reloj, y no en cada entidad.
4. El contrato deja explícito, operación por operación, qué necesita resuelto de antemano, que es información útil para la capa de aplicación.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que INV-01 no se ejerza entero en este proyecto de código.** El dominio declara la condición; el consumidor la resuelve. Está declarado en `Definicion-Modelo-De-Dominio.md` §4.1, primera precisión de ubicación.
2. **Se acepta que el consumidor pueda mentir.** Si declara que la unicidad se verificó y no lo hizo, el dominio no lo detecta. La mitigación no es arquitectónica sino de prueba: 08 verifica el camino completo desde la capa de aplicación.
3. **Se acepta que las firmas crezcan.** Toda operación que constituye una entidad con sello de tiempo lo lleva en su firma.
4. **Se acepta que el orden de dos operaciones con el mismo momento sea indistinguible.** Ninguna regla del producto depende del orden temporal fino, de modo que no hace falta resolverlo.

## 7. Implementación

- Las operaciones de constitución y de modificación de [`../Contratos-Abstractions.md`](../Contratos-Abstractions.md) §3 llevan el momento como parámetro explícito.
- Ninguna operación obtiene el momento por su cuenta: es verificable por inspección de la superficie pública.
- El contrato distingue con nombres distintos la **fecha declarada por el alumno**, la **fecha de creación** y la **fecha de última modificación**, para que no se confundan.
- La unicidad se expresa como un dato de entrada del contrato de alta, y no como una consulta.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Lecturas del reloj del sistema dentro del proyecto de código | Exactamente **0** | Inspección de la superficie pública y del cuerpo de las operaciones en revisión |
| Puertos declarados por este proyecto de código | Exactamente **0** | Inspección: los puertos son de `GeometriaFactory-Application` |
| Reproducibilidad de la batería | **100 %** de las pruebas con el mismo resultado en dos corridas separadas en el tiempo | Segunda corrida de la batería en la etapa de `test` |
| Operaciones con sello de tiempo que lo reciben por parámetro | **100 %** | Inspección de las firmas del contrato |

## 9. Referencias

- [`../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2.1, §2.2, §4.1 y §7.
- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` 1.15 §17.1.P.1 · GeometriaFactory-Domain, §17.1.P.11 · GeometriaFactory-Application punto 3 y §17.1.P.4 · GeometriaFactory-Infrastructure.
- ADR relacionadas: [`ADR-02001`](ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md), [`ADR-02002`](ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra que el momento y la unicidad ya resuelta entran por parámetro, que el dominio no declara puertos propios, las cuatro alternativas evaluadas —incluido el puerto de reloj propio, descartado por duplicar el de la capa de aplicación— y cuatro métricas de validación. |
