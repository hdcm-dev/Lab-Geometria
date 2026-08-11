# Estrategia de testing — GeometriaFactory-Contracts

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Contracts
**Documento:** Estrategia-Testing.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero QA / SDET Senior (AG-08)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`Estrategia-Calidad.md`](Estrategia-Calidad.md) §2 y §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.6 §6; [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1, §4, §5 y §8; [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §17.4.P.6, §18 (sample S-2), §20 y §22
**Trazabilidad downstream:** [`Casos-Prueba-Referenciales.md`](Casos-Prueba-Referenciales.md), [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md), [`Plan-Pruebas.md`](Plan-Pruebas.md)

---

## Tabla de contenido

- [1. Pirámide de testing deseada](#1-pirámide-de-testing-deseada)
- [2. Cobertura mínima: por qué no es por líneas](#2-cobertura-mínima-por-qué-no-es-por-líneas)
- [3. Tooling](#3-tooling)
- [4. Especificaciones Given-When-Then](#4-especificaciones-given-when-then)
- [5. Mocks y fixtures](#5-mocks-y-fixtures)
- [6. Datos de prueba](#6-datos-de-prueba)
- [7. Ambiente de testing](#7-ambiente-de-testing)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Pirámide de testing deseada

`Rules-Calidad-Y-Pruebas.md` §2.2 fija para el tipo `library` la distribución **80 / 15 / 5**. **Este proyecto de código se aparta de ella por completo, y el apartamiento está declarado aguas arriba, no decidido acá**: el intake §17.4.P.6 declara que «no tiene pruebas propias: son tipos sin comportamiento» y que «se ejercitan íntegramente desde las pruebas de integración que golpean la API real»; `05` §5 lo materializa declarando que su pipeline tiene etapas `restore` → `build` y **no tiene etapa de `test`**.

| Nivel | Qué cubre acá | Porcentaje objetivo | Justificación |
| --- | --- | --- | --- |
| Unit | — | **0 %** | No hay comportamiento que aislar. Un test unitario sobre un tipo sin lógica en sus descriptores de acceso no verifica nada, que es el anti-patrón «test sin assert» con otra forma |
| Integración | Cada tipo de transferencia ejercitado contra el servicio real por la batería de integración que vive en `GeometriaFactory-Api` | **60 %** | Es el gate que el intake declara como equivalente de la cobertura: **100 % de los tipos ejercitados** |
| Inspección de superficie | Recuentos sobre la superficie pública: campos prohibidos, campos de la respuesta de sesión, carga útil del listado, conjunto cerrado de códigos, referencias hacia el dominio | **40 %** | Es donde se verifica lo que este proyecto de código **decide**: qué cruza la frontera y qué no. Cinco de los nueve quality gates se comprueban acá |
| E2E y snapshot | — | **0 %** | No aplica: el ensamblado no es unidad de despliegue y no tiene proceso |

**Los porcentajes son de esfuerzo y no de cantidad de aserciones.** El reparto 60/40 declara que la inspección de superficie no es un anexo de la batería de integración sino la mitad de la verificación de este proyecto de código.

**Dónde viven materialmente las pruebas de integración.** No en este proyecto de código: en la batería que golpea el servicio real, que pertenece a `GeometriaFactory-Api`. Esta estrategia **declara qué tiene que verificar cada una sobre los tipos**, y la matriz de [`Matriz-Cobertura-Pruebas.md`](Matriz-Cobertura-Pruebas.md) §6 es el instrumento con el que se comprueba que ningún tipo quedó sin ejercitar. **Es una dependencia declarada y no un hueco**: este proyecto de código es de nivel topológico 0 y su verificación efectiva ocurre en nivel 3.

## 2. Cobertura mínima: por qué no es por líneas

El intake §17.4.P.6 declara **«cobertura mínima: no aplica como gate propio»** y fija como **«gate equivalente y bloqueante»** que **el 100 % de los DTOs esté ejercitado por al menos una prueba de integración** [ASUNCIÓN, §22 asunción `A-4`, cuya columna «Si el Product Owner la cambia» declara que **«Cambia la forma del gate, no su carácter bloqueante»**].

La partición de esta categoría es por las **ocho familias de tipos** de `05` §3.1, que son sus componentes.

| Familia de tipos | Contrato de uso | Métrica de cobertura | Umbral |
| --- | --- | --- | --- |
| Familia de sesión | CU-01 | Tipos ejercitados por integración; campos de la respuesta de sesión | 100 % ejercitados; exactamente **4** campos |
| Familia de cuentas | CU-02 | Tipos ejercitados por integración | 100 % |
| Familia de trabajo | CU-03 | Tipos ejercitados por integración | 100 % |
| Familia de listado | CU-04 | Tipos ejercitados; ocurrencias prohibidas en la proyección | 100 %; **0** de texto original, **0** de componentes, **0** de comentario |
| Familia de detalle | CU-05 | Tipos ejercitados; separación del comentario respecto de las observaciones | 100 %; **0** campos compartidos |
| Familia de desenlace | CU-07 | Tipos ejercitados; conjunto cerrado del desenlace | 100 %; exactamente **2** valores |
| Familia de reseteo | CU-08 | Tipos ejercitados; campos de la solicitud | 100 %; exactamente **1** campo en la solicitud |
| Familia de error | CU-06, y transversalmente las otras siete | Tipos ejercitados; conjunto cerrado de códigos; campos capaces de filtrar | 100 %; **15** códigos vivos; **0** campos de filtración |
| **Ensamblado completo** | Los ocho | Tipos ejercitados; referencias hacia el dominio; advertencias | **100 %** [ASUNCIÓN]; **0** referencias; **0** advertencias |

**No hay mutation score**, y su ausencia se declara en lugar de omitirse: `Rules-Calidad-Y-Pruebas.md` §2.2 lo pide para `library`, pero la prueba de mutación necesita lógica que mutar y acá no la hay. Mutar un tipo sin comportamiento produce mutantes que ninguna prueba puede matar y un puntaje sin significado. **Es la única exigencia de §2.2 que este proyecto de código no cumple, y el motivo es estructural.**

**La cobertura no se reporta como número global único.** El informe es por familia, y una familia al 100 % no compensa a otra al 80 %.

## 3. Tooling

Nombrado por función, según la convención de las categorías 03 y 05 de este proyecto de código.

| Propósito | Herramienta, por su función |
| --- | --- |
| Integración contra el servicio real | La batería de integración del producto, que levanta el servicio en proceso y lo golpea por su protocolo, según declara el intake §17.4.P.6. Se ejecuta con `scripts/test.sh` |
| Inspección de superficie pública | Comprobación reproducible sobre el ensamblado y sobre su archivo de proyecto. Dos de ellas ya están escritas y publicadas en [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §3, para `DXC-01` y `DXC-09` |
| Construcción | `scripts/build.sh`, cuyo gate es «sin advertencias» y no «sin errores» (`DXC-09`) |
| Colección de peticiones de ejemplo | El sample **S-2** del intake §18: alta de trabajo, envío que verifica y envío que no, y aprobación y rechazo, con los cuerpos de **E-2** y **E-5** |

## 4. Especificaciones Given-When-Then

Los criterios de aceptación de los **ocho** contratos de uso ya están escritos en Given/When/Then en la sección `§8` de cada uno, y las **veintidós** historias los heredan con la exigencia de la DoR §1 criterio 3.

**Decisión de esta categoría: no se adopta un juego de archivos de escenario ejecutables.** Los criterios viven en los contratos de uso y en las historias; cada `TC-XX` los transcribe citando su origen. Un juego paralelo abriría una segunda fuente de verdad sobre el mismo criterio.

**La forma característica de los criterios de este proyecto de código es el recuento**, y conviene decirlo porque cambia cómo se escriben las aserciones: «declara exactamente cuatro campos y **0** que puedan transportar una dirección», «las 3 respuestas traen el **mismo** código», «**0 campos** permiten distinguir los dos casos». Una aserción de este proyecto de código que no termine en un número es, casi siempre, una aserción mal escrita.

## 5. Mocks y fixtures

**Sin dobles en la batería de integración**: golpea el servicio real, que es lo que le da sentido. Los dobles que existan del lado del servicio son decisión de la categoría 08 de `GeometriaFactory-Api` y no se deciden acá.

Fixtures que esta categoría declara, todos **cuerpos de petición y de respuesta**:

| Fixture | Qué contiene | De dónde sale |
| --- | --- | --- |
| Cuerpos del sample S-2 | Alta de trabajo, envío que verifica, envío que no verifica, aprobación y rechazo | Intake §18, que los declara con los cuerpos de `E-2` y `E-5` |
| Respuesta de sesión de referencia | Los **cuatro** campos y ninguno más | `CU-01`; restricción `RT-10` |
| Colección de respuestas de error | Una por cada uno de los **quince** códigos vivos | `03` §3.2, que es la única tabla de todo el proyecto de código donde los **dieciocho** identificadores emitidos están enumerados juntos |
| Proyección de listado de referencia | Un elemento de listado sin texto original, sin componentes y sin comentario | `CU-04`; restricción `RT-04` |

**Regla de duplicación:** los cuerpos de petición se derivan de los del sample S-2 y no se copian. Un segundo cuerpo equivalente con otro valor de geometría es un hallazgo, porque los datos de geometría del producto son los ocho escenarios y no se inventan.

## 6. Datos de prueba

**Los datos de geometría de este producto son reales y no se sustituyen por datos sintéticos.** El intake §20 transcribe **ocho** escenarios `E-1` a `E-8` con sus payloads completos y su procedencia; §21 los cruza contra la batería obligatoria de **diez** casos de prueba —los **nueve** de la fuente técnica más el **décimo** que esa misma sección agregó el 2026-08-09 para la dimensión no legible—.

**Qué le toca a este proyecto de código, que es una parte y no el todo.** El ensamblado **no interpreta el texto**: lo transporta como cadena, sin interpretarlo (restricción `RT-03`, intake §17.4.P.11 punto 2). De los ocho escenarios, entonces, lo que este proyecto de código verifica es que **el texto viaje íntegro en las dos direcciones** y que **el resultado de la interpretación quepa en sus tipos**.

| Escenario | Qué verifica de este proyecto de código | Fuente |
| --- | --- | --- |
| `E-2` | Es uno de los dos cuerpos del sample **S-2**. Su texto **no es JSON estrictamente válido** —lleva dos comas finales— y por eso es el mejor caso para verificar que el campo de texto original es **una sola cadena que viaja sin interpretarse** | Intake §18 y §20.E-2 |
| `E-5` | Es el otro cuerpo del sample **S-2**. Su resultado trae una observación de severidad `Error` con **índice de figura 1** y **campo `Tipo`**: es el escenario con el que se verifica que el detalle de ubicación transporta los dos datos | Intake §18 y §20.E-5 |
| `E-1` | Resultado con **3 piezas y 2 advertencias**: el detalle transporta la colección de piezas con sus componentes y las observaciones con su par de valores | §20.E-1, punto 5 |
| `E-3` y `E-4` | El par de valores declarado 36.00 y derivado 54.00 en `E-3`, y **cero observaciones** en `E-4`: verifican que la observación lleva los dos valores en campos propios y que una colección vacía también es un caso del contrato | §20.E-3 punto 2 y §20.E-4 punto 4 |
| `E-6` | Una figura con una dimensión en `0.00` que **se interpreta**: su pieza viaja en el detalle como cualquier otra | §20.E-6, punto 1 |
| `E-7` | Seis piezas que cubren los seis tipos, tres volumétricos y tres planos: es el juego más ancho para el detalle | §20.E-7, punto 1 |
| `E-8` | El desenlace del envío **es error** [DECISIÓN 2026-08-09]: el resultado del envío trae estado `Borrador` y la observación localizada por índice y campo. Verifica que el contrato transporta ese desenlace sin ambigüedad | §20.E-8, punto 5 |

**Los ocho escenarios están alcanzados.** Ninguno se sustituye y ninguno se reescribe: si el intake cambia uno, el cambio baja acá como corrección con su fila de control de cambios.

**Datos que no son de geometría** —correos, nombres, momentos— se usan con valores evidentemente ficticios y se declaran como tales en el `TC-XX`. El intake no los fija y no hay nada que sustituir.

## 7. Ambiente de testing

| Aspecto | Decisión |
| --- | --- |
| Dónde corre | Dentro del contenedor de desarrollo (intake, encabezado de la Parte C) |
| Etapa de pruebas propia | **Ninguna.** El pipeline de este proyecto de código es `restore` → `build` (`05` §5) |
| Dónde corre la verificación efectiva | En la batería de integración que golpea el servicio real, que pertenece a `GeometriaFactory-Api` y que exige su base de datos efímera y su servicio levantado. **Esas condiciones no se declaran acá**: son de esa categoría |
| Secretos | **Ninguno productivo.** Las credenciales de los cuerpos de prueba son ficticias y viajan en claro por diseño del canje, siempre servidor a servidor (`RT-11`) |
| Aislamiento | Cada prueba de integración parte del estado que su propia preparación establece. La política concreta la fija la categoría 08 de `GeometriaFactory-Api` |
| Duración | **No se declara ninguna.** Ninguna fuente da un tiempo de ejecución para la batería de integración, y esta categoría no lo inventa |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **`H-01`.** §6 afirmaba que §21 del intake cruza la batería obligatoria contra **nueve** casos de prueba; §21 la cruza contra **diez** —los nueve de la fuente técnica más el décimo que esa misma sección agregó el 2026-08-09—. Es el mismo defecto que el informe registró en `GeometriaFactory-Visor` (`H-05`) y que **también estaba acá**. **`H-02`.** §2 cita ahora la fila `A-4` de §22 completa, con su columna «Si el Product Owner la cambia», que es la que sostiene el carácter bloqueante de `QG-05`. Ningún dato de prueba ni umbral cambia. Corrige contra [`../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md`](../../../Audit/E-08-Calidad-Siete-Proyectos-r1.md) 1.0 y contra el texto vivo del intake **1.20**. |
| 1.0 | 2026-08-11 | Emisión inicial. Declara el apartamiento completo de la pirámide 80/15/5 de `Rules-Calidad-Y-Pruebas.md` §2.2, con el fundamento de que el apartamiento viene declarado del intake §17.4.P.6 y de `05` §5 y no se decide acá; el reparto 60/40 entre integración e inspección de superficie; la cobertura por las **ocho** familias de tipos en lugar de por líneas, con el gate de 100 % de tipos ejercitados rotulado [ASUNCIÓN]; la ausencia declarada de mutation score con su motivo estructural; el tooling nombrado por función; los cuatro fixtures; el uso de los **ocho** escenarios reales del intake §20 con la precisión de qué parte de cada uno le toca a un ensamblado que transporta y no interpreta; y el ambiente, con la constancia de que no se declara ningún tiempo de ejecución que ninguna fuente dé. |
