# ADR-06006 — Lectura tolerante en un solo lugar, y una tabla de derivación por tipo

**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Estilo

---

## 1. Contexto

El intake declara, en su registro de riesgos de negocio, que **el defecto que más veces se repite es escribir el validador sin leer el análisis**, con probabilidad alta si no se controla e impacto alto: «la aplicación no sirve para el dato que existe» (`RN-B3`). Es el único riesgo de negocio cuya mitigación declarada es una batería de pruebas.

La categoría 02 ya escribió esa mitigación como contrato: **cuatro trampas del formato** `T1` a `T4`, **siete garantías** `G-1` a `G-7`, los **ocho** escenarios y la cobertura de la batería. Lo que dejó abierto y derivó explícitamente a esta categoría es una cosa concreta: **de dónde sale el valor derivado del área de una pieza volumétrica**. El intake la muestra dos veces como **suma de los componentes** —el cilindro de `E-1` y el ortoedro de `E-2`— y una vez como **fórmula** —el cubo de `E-3`—, y las dos formas **coinciden** en ese cubo. No hay contradicción declarada, pero tampoco hay una regla enunciada, y `CU-06002` §10 adoptó la suma de componentes dejando dicho que la tabla por tipo la fija esta categoría.

Motivación upstream: NB-00004, NB-00005, NB-00006; RN-06005, RN-06008, RN-06009; INV-04; `PRODUCT-INTAKE` §11 (RN-B3), §17.3.P.3, §17.3.P.10, §17.3.P.11 punto 1, §20 y §21.

## 2. Decisión

**La lectura tolerante ocurre en un solo lugar, y el valor derivado sale de una tabla por tipo cuya forma canónica es la suma de los componentes.** Cuatro decisiones:

1. **Un solo punto de lectura del texto.** Las **cuatro** tolerancias `T1` a `T4` se aplican en el motor de interpretación y en ningún otro lugar del producto de datos. Un segundo lector con sus propias tolerancias es la forma segura de que las dos se separen.
2. **El área derivada de una pieza volumétrica es la suma de las áreas declaradas de sus componentes**, y la fórmula por tipo se usa **sólo** donde no hay componentes que sumar: las figuras planas como piezas del conjunto raíz, y el volumen. La tabla completa está en [`../Flujo-Ejecucion.md`](../Flujo-Ejecucion.md) §5. **La razón de preferir la suma no es de gusto: es que el escenario semilla no cierra sin ella.** El intake declara que el área del cilindro de `E-1` da **113.09** por suma de componentes contra **113.10** declarada, con una diferencia de **exactamente 0.01**, y sobre ese caso funda la decisión del operador estricto. Con la fórmula la diferencia sería **cero** y el fundamento del operador se quedaría sin caso: el número medido del intake sólo se obtiene sumando los componentes, cuyas áreas vienen ya redondeadas a dos decimales.
   **El área declarada de cada componente se toma tal cual y no se verifica contra sus dimensiones.** Ninguna fuente declara esa verificación, y hacerla produciría observaciones que los escenarios documentados no traen: `E-4` exige **cero** observaciones en total.
3. **El motor no impone límite de tamaño al texto.** El corte pertenece al **borde del proceso**, y fijar dos límites distintos —uno en el borde y otro acá— permitiría que un texto entrara y no se interpretara. Lo que esta ADR sí exige del borde es que **rechace, nunca trunque**: un truncamiento rompe `RN-06008` en silencio.
4. **El texto original no se modifica nunca, ni siquiera cuando la interpretación falla** (`G-1`). Un texto que el alumno escribió mal **es un resultado, no una avería**: produce observaciones y no la condición degradada (`G-7`, `CU-06001` CA-10).

**Lo que esta ADR no reabre**, porque el intake ya lo fijó y lo transcribe sin margen: la tolerancia de **0.01** con operador **estricto** —se advierte cuando la diferencia absoluta es **mayor** que 0.01, no mayor o igual—, y el desenlace del envío del escenario `E-8`, que es **error de validación** con el trabajo en `Borrador`.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Suma de las áreas declaradas de los componentes, con fórmula sólo donde no hay componentes (**adoptada**) | Es **la única vía que reproduce el número medido del intake**: los 113.09 del cilindro de `E-1` y la diferencia de exactamente 0.01 sobre la que se funda el operador estricto; el cubo de `E-3` y el ortoedro de `E-2` dan lo mismo por las dos vías, de modo que ningún escenario documentado cambia | Depende de que los componentes estén completos: una pieza con un componente que no se pudo leer deriva un área menor de la real. Y arrastra el redondeo a dos decimales de cada componente |
| Fórmula por tipo como forma canónica | No depende de los componentes; una pieza con componentes incompletos deriva igual, y no arrastra redondeos | **Descartada.** Daría **113.10** para el cilindro de `E-1`, o sea diferencia **cero** con lo declarado, y el caso sobre el que el intake funda el operador estricto **dejaría de existir**. Además obligaría a decidir una fórmula de área para tipos que ninguna fuente documenta como pieza raíz |
| Derivar por las dos vías y advertir cuando difieren, o verificar además el área declarada de cada componente | Detectaría más errores del alumno | **Descartada.** Produciría observaciones que los escenarios documentados no traen —`E-4` exige **cero** en total— y en `E-1` agregaría una advertencia por el cilindro que el escenario, rotulado como medido, declara que no existe |
| Dos lectores del texto, uno tolerante y otro estricto, para distinguir el texto malformado del texto con errores | Separaría el defecto de forma del defecto de contenido | **Descartada.** El texto del alumno **no es estrictamente válido y eso es un hecho del producto**: un lector estricto rechazaría todos los textos reales, y la distinción que produciría no la usa ninguna regla |
| Fijar acá un límite de tamaño del texto | Cierra un punto abierto | **Descartada.** El límite del borde y el del motor tienen que ser el mismo, y el borde es quien puede rechazar antes de leer. Fijar dos es peor que fijar uno |

## 5. Consecuencias positivas

1. La mitigación del riesgo `RN-B3` queda materializada en un componente con cobertura propia del **95 %**, que es el número más alto del producto.
2. La tabla de derivación por tipo cierra el punto abierto que la categoría 02 derivó acá, con un criterio que ningún escenario documentado contradice.
3. El escenario `E-4` —el criterio negativo, **cero observaciones**— sigue siendo verificable: un validador que advirtiera siempre lo haría fallar, y la suma de componentes coincide ahí con lo declarado.
4. `RN-06009` queda sostenida donde se produce: cada observación lleva posición y campo, y la posición de una figura no reconstruida **queda reservada y no se compacta** (`G-3`, `RC-06002`).
5. El motor no hace red y no lee configuración propia, lo que lo hace ejecutable en la batería unitaria y le da al producto su reflejo estructural de `RA-02`.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que una pieza con un componente ilegible derive un área menor de la real**, y que el redondeo a dos decimales de cada componente se arrastre a la suma. Lo segundo es justamente lo que el escenario semilla documenta, y la tolerancia de 0.01 con operador estricto existe para absorberlo. Lo primero queda cubierto igual, porque el componente ilegible produce su propia observación con posición y campo: el alumno ve la causa, no sólo el efecto.
   **Se acepta también no verificar el área declarada de cada componente**, con la consecuencia de que un error dentro de un componente sólo se ve si mueve el total de la pieza.
2. **Se acepta depender de un límite de tamaño que fija otro proyecto de código**, con la exigencia escrita de que rechace y no trunque.
3. **Se acepta que la tabla de derivación por tipo cubra sólo los seis tipos que los escenarios ejercitan.** Un tipo fuera del conjunto produce error de validación con posición y campo, que es un resultado correcto aunque pueda no ser el deseado; el alcance del conjunto sigue siendo punto abierto del Product Owner.
4. **Se acepta que el valor declarado y el derivado se guarden por separado** (`RC-06003`), con el costo de dos columnas por magnitud, a cambio de que la advertencia pueda mostrar los dos números.

## 7. Implementación

- El motor de interpretación y el motor de verificación de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 son los únicos lugares donde el texto del alumno se lee y donde un valor se deriva.
- El orden de los pasos, con sus transformaciones, está en [`../Flujo-Ejecucion.md`](../Flujo-Ejecucion.md); la tabla de derivación por tipo es su §5.
- **Convención impuesta:** ninguna observación es genérica. Todo error de validación indica **posición y campo** (`G-4`).
- **Convención impuesta:** un defecto en una figura **no descarta el resto del análisis** (`G-2`), y una dimensión presente con valor cero **no descarta la figura** (`E-6`): existencia no es veracidad.
- **Convención impuesta:** el motor no decide el estado del trabajo. Entrega el conjunto de observaciones con su especie y **el dominio resuelve**.
- La condición `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO` protege el orden de los dos motores y **es derivación de la categoría 02**, que esta ADR hereda sin reabrir.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Casos de la batería del validador que pasan | **10 de 10**, con los ocho escenarios como entrada | Etapa de `test` del pipeline, con la matriz de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §10.5 |
| Advertencias del escenario `E-1` | Exactamente **2**, no 3 | Caso 9 de la batería: es el que verifica el operador estricto |
| Observaciones del escenario `E-4` | Exactamente **0** | Caso 4 de la batería: el criterio negativo |
| Cobertura de líneas de los dos motores | **95 %** [ASUNCIÓN del intake] | Informe de cobertura acotado, bloqueante |
| Tiempo de interpretación del texto de `E-1` | Menos de **200 ms**, sin almacén [ASUNCIÓN del intake] | Medición en la batería unitaria |
| Peticiones de red originadas por los dos motores | Exactamente **0** | Inspección de dependencias, y `CU-06001` CA-11 |
| Textos ilegibles que devuelven la condición degradada en lugar de una observación | Exactamente **0** | `CU-06001` CA-10 |
| Posiciones compactadas tras una figura no reconstruida | Exactamente **0** | Casos 8 y 10 de la batería, sobre `E-5` y `E-8` |
| Lugares del producto de datos donde se aplican las tolerancias del formato | Exactamente **1** | Inspección en revisión |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §11 (RN-B3), §17.3.P.3, §17.3.P.10, §17.3.P.11 punto 1, §20 (los ocho escenarios) y §21 (matriz de cobertura).
- [`../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md) completo, y en particular §2, §4, §6, §7 y §9.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) §10, que adopta la suma de componentes y deriva la tabla a esta categoría.
- ADR relacionadas: [`ADR-06001`](ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), [`ADR-06002`](ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. **Cierra el punto abierto del valor derivado del área que la categoría 02 derivó acá**: la forma canónica es la suma de los componentes y la fórmula por tipo se usa sólo donde no hay componentes que sumar. Fija además el punto único de lectura tolerante, la ausencia de límite propio de tamaño con la exigencia de que el borde rechace y no trunque, y la garantía de que un texto mal escrito es un resultado y no una avería. No reabre la tolerancia de 0.01 con operador estricto ni el desenlace de `E-8`, que el intake ya fijó. Evalúa cinco alternativas, declara cuatro trade-offs y fija nueve métricas de validación. |
