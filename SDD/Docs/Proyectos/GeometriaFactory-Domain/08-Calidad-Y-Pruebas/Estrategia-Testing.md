# Estrategia de testing — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Estrategia-Testing.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §4 y §8; [`../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md); [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §17.1.P.6, §20 (los **ocho** escenarios `E-1` a `E-8`), §21 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md); `09-Devops` y `11-Documentacion`

---

## Tabla de contenido

- [1. Pirámide de testing deseada](#1-pirámide-de-testing-deseada)
- [2. Cobertura mínima por capa](#2-cobertura-mínima-por-capa)
- [3. Tooling](#3-tooling)
- [4. Especificaciones Given-When-Then](#4-especificaciones-given-when-then)
- [5. Mocks y fixtures](#5-mocks-y-fixtures)
- [6. Datos de prueba](#6-datos-de-prueba)
- [7. Ambiente de testing](#7-ambiente-de-testing)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Pirámide de testing deseada

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5** entre unitario, integración y extremo a extremo con snapshot. Este proyecto de código la adopta con **una redistribución declarada**, porque no tiene con qué integrar ni qué recorrer de punta a punta.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | Las guardas, las transiciones y la constitución de las cinco entidades, sin dobles | **90 %** | El intake §17.1.P.6 declara «pruebas unitarias puras y sin dobles». Cero dependencias salientes significa que todo lo que hay que probar es entrada y salida de una operación |
| Integración | Composición de dos o más de los cinco componentes de `05` §3.1 dentro del mismo proyecto de código: por ejemplo, adoptar la interpretación y después enviar | **10 %** | Es lo único que califica como integración acá: no hay base de datos, no hay red y no hay marco de aplicación con el que integrar |
| E2E y snapshot | — | **0 %** | **No aplica y se declara así en lugar de omitirse.** El proyecto de código no es unidad de despliegue, no tiene proceso propio ni interfaz (`05` §4 y §5). Un recorrido de punta a punta del producto pasa por `GeometriaFactory-Api`, y ahí es donde vive |

**El apartamiento es de reparto, no de rigor.** Los cinco puntos que la regla asigna a snapshot y extremo a extremo se reasignan a integración interna; el piso unitario **sube** de 80 a 90. No se baja ninguna exigencia, de modo que no hace falta la ADR que §2.2 exige para bajar cobertura.

**Contra la pirámide invertida**: acá sería imposible construirla, porque no hay nada que recorrer. **Contra la pirámide aplanada** —un número global de cobertura sin distinguir capas— la defensa es §2 de este documento, que reporta por componente y nunca como número único.

**Dos clases de prueba que no son un nivel de la pirámide y conviene nombrar aparte**, porque no ejecutan lógica de negocio sino que revisan el proyecto de código sobre sí mismo:

- **Prueba de inspección.** Comprueba una propiedad estructural del proyecto de código: cero dependencias salientes, el conjunto de códigos emitidos contra el catálogo, ninguna operación que obtenga el momento por su cuenta. Se cuentan dentro del nivel unitario porque corren en el mismo ejecutor y con el mismo costo.
- **Prueba basada en propiedades.** Sobre invariantes que valen para todo valor admisible; ver §4.

## 2. Cobertura mínima por capa

La partición no es en capas de despliegue —no las hay— sino en los **cinco componentes** de [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §3.1. El piso global lo fija el intake §17.1.P.6 y es **90 % de líneas y 85 % de ramas** [ASUNCIÓN del intake §22, asunción `A-3`].

| Componente | Líneas | Ramas | Mutation score | Fundamento del valor |
| --- | --- | --- | --- | --- |
| Núcleo de entidades | 90 % | 85 % | 60 % | Piso del intake §17.1.P.6 |
| Guardas de cuenta | 95 % | 90 % | 60 % | Sube sobre el piso: es el componente donde el P0 del producto y su reincidencia se abrieron (`05` §9, segundo riesgo) |
| Evaluador de admisibilidad | 100 % | 100 % | 60 % | Es la **puerta única** de `INV-06` y de `INV-09` ([`ADR-02005`](../05-Arquitectura-Tecnica/Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md)). Una rama sin cubrir acá es una guarda que nadie ejerce |
| Máquina de estados del trabajo | 95 % | 90 % | 60 % | Sostiene cinco de los nueve invariantes (`05` §10.3) |
| Adopción de la interpretación | 90 % | 85 % | 60 % | Piso del intake |
| **Proyecto de código completo** | **90 %** | **85 %** | **60 %** | Intake §17.1.P.6 [ASUNCIÓN] y `Rules-Calidad-Y-Pruebas.md` §2.2 para el mutation score |

**De dónde sale cada número, sin mezclarlos.** El 90/85 global es del intake y viene rotulado **[ASUNCIÓN]**: es el valor que el Product Owner tiene pendiente de confirmar. El **mutation score de 60 %** no lo declara ninguna fuente del producto: es el piso que `Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` y esta categoría lo adopta como tal; **no se le atribuye al intake**. Los tres valores por encima del piso —95, 95 y 100— los sube esta categoría con el fundamento declarado en la columna, que es lo que §2.2 admite («los porcentajes son piso, no techo»).

**La cobertura no se reporta como número global único.** El informe de la etapa `test` se emite por componente, y un 90 % global con el evaluador de admisibilidad en 70 % es un incumplimiento aunque el promedio cierre.

## 3. Tooling

Se nombran por función y no por producto, que es la convención que las categorías 03 y 05 de este proyecto de código ya siguen. La elección concreta y su anclaje de versión son de la etapa `a` (intake, encabezado de la Parte C: regla de anclaje de versiones).

| Nivel o propósito | Herramienta, por su función |
| --- | --- |
| Unit e integración interna | Marco de pruebas unitarias de la plataforma objetivo, ejecutado por `scripts/test.sh` |
| Aserciones | Biblioteca de aserciones del mismo marco. Sin marcos de dobles: el intake §17.1.P.6 declara «sin dobles» |
| Cobertura por líneas y ramas | Recolector de cobertura de la plataforma, con informe por componente |
| Mutation score | Marco de pruebas de mutación de la plataforma. **Su incorporación al pipeline es un hueco declarado**, ver [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6 |
| Pruebas basadas en propiedades | Marco de generación de casos de la plataforma, sólo donde §4 lo declara |
| Inspección estructural | El propio marco de pruebas, leyendo el archivo de proyecto y el conjunto de códigos emitidos |

**No se nombra ningún producto comercial**, y no porque falte la decisión sino porque el intake la ata a la etapa `a` y el nombre no cambia nada de esta estrategia.

## 4. Especificaciones Given-When-Then

**Los criterios de aceptación de las veintisiete historias ya están escritos en Given/When/Then**: la Definition of Ready lo exige como criterio 3, con al menos un camino feliz y un caso de borde ([`../06-Backlog-Tecnico/Definition-Of-Ready.md`](../06-Backlog-Tecnico/Definition-Of-Ready.md) §1).

Decisión de esta categoría: **no se adopta un marco de especificaciones ejecutables con archivos de escenario separados.** Los criterios viven en las historias, y cada `TC-XX` de [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md) los transcribe en sus pasos citando la historia de origen. Un juego de archivos de escenario paralelo a las historias abriría una segunda fuente de verdad sobre el mismo criterio, que es el defecto que este corpus tiene documentado como el que más veces volvió.

**Dónde sí se usan pruebas basadas en propiedades**, que son la otra forma de especificación de esta estrategia:

| Propiedad | Enunciado |
| --- | --- |
| Terminación controlada | Para toda operación y todo estado inicial admisible, o el efecto se aplica entero o la entidad queda como estaba (`05` §4, última viñeta) |
| Conjunto cerrado de condiciones | Para toda invocación que rechaza, el código devuelto pertenece a las **42** condiciones del catálogo |
| Indistinguibilidad | Para todo trabajo ajeno y todo trabajo inexistente, el resultado de `CU-02009` es el mismo (`RN-02003`, `INV-02`) |
| Terminalidad | Para todo trabajo en `Finalizado` o en `Rechazado` y toda transición, el resultado es rechazo (`INV-07`) |

## 5. Mocks y fixtures

**Política de dobles: ninguno.** El intake §17.1.P.6 declara «pruebas unitarias puras y sin dobles», y este proyecto de código lo permite porque no tiene dependencias que aislar. Lo que en otros proyectos de código exigiría un doble —el reloj y la unicidad del correo— acá **entra por parámetro** ([`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)): la prueba pasa el momento y la afirmación de unicidad como valores, y por eso es reproducible sin fijar el reloj del entorno.

Fixtures que sí existen, todos como **constructores de entidad** compartidos:

| Fixture | Qué construye | Por qué se centraliza |
| --- | --- | --- |
| Cuenta de alumno en cada uno de sus tres estados | `Pendiente`, `Habilitado`, `Bloqueado`, con y sin la marca de cambio de contraseña pendiente | Seis combinaciones que aparecen en `CU-02002`, `CU-02003`, `CU-02004` y `CU-02013` |
| Cuenta de administrador | Única, `Habilitado`, con credencial derivada | `INV-05` e `INV-08` la exigen en esa forma y sólo en esa |
| Trabajo en cada uno de sus cuatro estados | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado` | Las transiciones y la terminalidad se prueban contra los cuatro |
| Resultados de interpretación de los escenarios del intake | Los conjuntos de piezas y observaciones que corresponden a `E-1` a `E-8`, ver §6 | Es el material que hace comparables las pruebas de este proyecto de código con las de `GeometriaFactory-Infrastructure` |

**Regla de duplicación:** un caso de prueba que necesite una variante de un fixture la deriva del constructor compartido y no lo copia. Un segundo constructor equivalente es un hallazgo de revisión.

## 6. Datos de prueba

**Los datos de prueba de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos, provenientes de la aplicación de escritorio de los alumnos y de los dos ejemplos de la cátedra, cada uno con su procedencia y su estado declarado —`medido`, `derivado` o `reconstruido`—. §21 los cruza contra la batería obligatoria de **diez** casos de prueba —los **nueve** de la fuente técnica más el **décimo** que esa misma sección agregó el 2026-08-09 para la dimensión no legible—.

**Cómo los usa este proyecto de código, que es la parte que hay que decir con precisión.** El dominio **no interpreta el texto del alumno**: la interpretación es de `GeometriaFactory-Infrastructure` y la reconstrucción de piezas le llega ya producida ([`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §1). De cada escenario, entonces, lo que entra acá **no es el texto sino su resultado**: el conjunto de piezas y de observaciones que el escenario declara en su bloque «Qué verificar».

| Escenario | Qué aporta a las pruebas de este proyecto de código | Fuente del valor |
| --- | --- | --- |
| `E-1` | Conjunto de 3 piezas y 2 advertencias, sin errores. El trabajo **pasa a `Pendiente` al enviarlo** | §20.E-1, punto 6 de «Qué verificar» |
| `E-2` | 1 pieza con 2 bases y 4 laterales, 1 advertencia de volumen y ningún error. **Pasa a `Pendiente` con la advertencia asociada** | §20.E-2, puntos 4, 6 y 7 |
| `E-3` | Advertencia de área con el par declarado 36.00 y derivado 54.00. Es el caso insignia de `ADVERTENCIA_SIN_LOS_DOS_VALORES` | §20.E-3, punto 2 |
| `E-4` | **Cero observaciones en total.** Es el criterio negativo: el envío pasa a `Pendiente` sin ninguna observación que adoptar | §20.E-4, punto 4 |
| `E-5` | Observación de severidad **`Error`** con **índice de figura 1** y **campo `Tipo`**; la primera pieza, válida, se interpreta igual. El trabajo **queda en `Borrador`** | §20.E-5, puntos 1 a 4 |
| `E-6` | Una figura que **se interpreta** y produce a lo sumo una advertencia; el trabajo pasa a `Pendiente` | §20.E-6, puntos 1 a 3 |
| `E-7` | Conjunto de 6 piezas que cubre los seis tipos, tres volumétricos y tres planos. Ejercita la derivación de familia de `US-02012` | §20.E-7, puntos 1 y 3 |
| `E-8` | **El desenlace del envío es error, no advertencia** [DECISIÓN 2026-08-09]: el trabajo **queda en `Borrador`** y no pasa a `Pendiente`, con el mensaje localizado por índice de figura y campo que exige `RN-02009` | §20.E-8, punto 5 |

**Regeneración y versionado.** Los ocho escenarios **no se regeneran**: son datos declarados por el intake con su procedencia. Un fixture de este proyecto de código que cambie un valor de un escenario es un defecto, no una actualización. Si el intake cambia un escenario, el cambio baja acá como una corrección con su fila de control de cambios.

**Lo que no se inventa.** Ningún caso de prueba de este proyecto de código introduce un payload de figuras que no esté en §20. Donde hace falta un dato que ningún escenario da —un correo, un nombre de alumno, un momento— se usa un valor evidentemente ficticio y se declara como tal en el `TC-XX`: son datos de identidad, no datos de geometría, y el intake no los fija.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo, porque el equipo anfitrión no tiene el kit de desarrollo instalado (intake, encabezado de la Parte C, y §17.1.P.9) |
| Aislamiento entre pruebas | Total y por construcción: no hay estado compartido entre invocaciones, no hay caché y no hay registro estático (`05` §4). Ninguna prueba depende del orden de ejecución |
| Paralelismo | Admitido. `05` §4 declara que la batería puede correr en paralelo porque ninguna prueba comparte estado |
| Base de datos | **Ninguna.** `tiene_persistencia` es false |
| Variables de entorno y secretos | **Ninguno.** El proyecto de código no lee configuración (`05` §7) y la contraseña llega ya derivada |
| Reloj | **No se fija ni se simula.** El momento entra por parámetro, de modo que la prueba lo elige ([`ADR-02006`](../05-Arquitectura-Tecnica/Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md)) |
| Duración | La batería completa en menos de **10 segundos** [ASUNCIÓN del intake §17.1.P.10]. **Ningún otro tiempo de ejecución se declara acá**: ninguna fuente da otro |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** §6 afirmaba que §21 del intake cruza los ocho escenarios contra la batería obligatoria de **nueve** casos de prueba; §21 los cruza contra **diez**. Es el mismo defecto que el informe registró en `GeometriaFactory-Visor` (`H-05`) y que **también estaba acá**. Ningún dato de prueba, fixture ni umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara la pirámide objetivo con su apartamiento del reparto de `Rules-Calidad-Y-Pruebas.md` §2.2 y su justificación —el nivel unitario sube de 80 a 90 y los niveles de extremo a extremo y snapshot se declaran no aplicables—, la cobertura mínima por los cinco componentes de `05` §3.1 con el origen de cada número separado, el tooling nombrado por función, la decisión de no adoptar archivos de escenario ejecutables con su fundamento, la política de cero dobles que el intake declara, los cuatro fixtures compartidos, el uso de los **ocho** escenarios reales del intake §20 —con la precisión de que a este proyecto de código le entra el resultado de la interpretación y no el texto— y el ambiente de testing, incluida la constancia de que no se declara ningún tiempo de ejecución que ninguna fuente dé. |
