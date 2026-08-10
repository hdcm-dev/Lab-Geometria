# CU-01 — Interpretar el texto original y reconstruir las piezas

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-01-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-04`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md); [`NB-03`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-03-Trabajo-Con-Dueno-Estado-Y-Persistencia.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11** §4 (F-09), §4.1 (RN-08, RN-09), §11 (RN-B3), §17.3.P.3, §17.3.P.6, §17.3.P.11 puntos 1 y 2, §20.E-1 a §20.E-7 y §21; implementa el puerto de validación de figuras que declara `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3 y consume su [`CU-05`](../../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md); alimenta [`CU-06`](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-06-Reconstruir-El-Conjunto-De-Piezas-Del-Trabajo.md) de GeometriaFactory-Domain
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Leer el texto que el alumno pegó tal como lo emite su programa, y devolver **cuántas figuras trae el conjunto raíz**, las piezas que se pudieron reconstruir con su posición y sus componentes, y las observaciones de especie **error de validación** que impidieron reconstruir alguna.

Es la mitad de riesgo del producto. El intake lo declara sin rodeos en su §11: **el defecto que más veces se repite es escribir este validador sin leer el análisis**, porque el texto del alumno **no es JSON estrictamente válido** y un lector estricto lo rechaza entero (RN-B3). Por eso este contrato nace con las cuatro trampas del formato ya declaradas, y no las descubre después.

Lo que este caso de uso **no** hace: no compara el valor declarado con el derivado —eso es `CU-02`—, no decide el estado del trabajo —eso lo resuelve el dominio con el conjunto de observaciones que recibe—, no guarda nada y **no hace red** (`PRODUCT-INTAKE` §17.3.P.3).

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor del puerto de validación de figuras (`GeometriaFactory-Application`) | Primario | Entrega el texto original y recibe el resultado de la interpretación |
| Texto original del trabajo | Sistema | El dato de entrada, conservado íntegro y nunca reescrito (RN-08) |

El alumno es el sujeto de la regla: es quien escribió el texto. **No es actor**: no invoca nada de esta capa.

## 3. Precondiciones

- El consumidor aporta el **texto original** del trabajo, tal como el alumno lo pegó.
- No hay ninguna otra precondición: este contrato **no consulta la base de datos, no abre conexiones y no lee configuración propia**. Es lo que permite ejercer las nueve pruebas obligatorias sin motor de persistencia (`PRODUCT-INTAKE` §14, §17.3.P.6).

## 4. Flujo principal

1. El consumidor entrega el texto original.
2. El adaptador lo lee con **tolerancia a comas finales y omisión de comentarios** (trampa **T2**). El texto que emite el programa del alumno trae comas antes del cierre de un array, y un lector estricto lo rechazaría entero.
3. Recorre el conjunto raíz y cuenta **cuántas figuras trae**, incluidas las que después no se puedan reconstruir. Ese número es el resultado que el dominio exige como precondición y que **no es derivable de las piezas adoptadas**, porque el conjunto admite huecos.
4. Por cada figura, lee su discriminante `Tipo` y reconstruye la pieza:
   - En el ortoedro, las bases se leen indistintamente de la clave **`Bases`** o de la clave **`Tapas`**, que son sinónimas (trampa **T1**). Es la línea que desbloquea el dibujo de todos los ortoedros que hoy el visualizador previo pierde.
   - Las caras del cubo se aceptan con `Tipo` **`Cuadrado`** o **`Rectangulo`** indistintamente (trampa **T3**): son la misma cara emitida por los dos ejemplos de la cátedra.
   - Las dimensiones se leen **por existencia del campo y no por veracidad de su valor**: un `0.00` presente es una dimensión legible y **no descarta la figura**.
5. Registra la posición de cada pieza en el conjunto raíz, que es su identidad, y sus componentes.
6. Por cada figura que no se pudo reconstruir, emite una observación de especie **error de validación** que indica **la posición de la figura y el campo** en el que está el defecto (RN-09), y **reserva esa posición**: la figura siguiente conserva la suya.
7. Devuelve la cantidad de figuras del conjunto raíz, las piezas reconstruidas y el conjunto de observaciones de error de validación. **El texto original se devuelve intacto o no se devuelve: nunca corregido** (RN-08).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El discriminante `Tipo` de una figura no pertenece al conjunto de tipos reconstruibles | Se emite una observación de error de validación con la **posición de esa figura** y el campo `Tipo`, se reserva su posición y **el recorrido sigue con las demás**: un defecto en un elemento no descarta el resto del análisis | Paso 4, con la figura siguiente |
| FA-02 | Una figura no trae el campo `Tipo` | Mismo tratamiento que FA-01: observación de error de validación con posición y campo `Tipo` | Paso 4, con la figura siguiente |
| FA-03 | El conjunto raíz está vacío | Se devuelve cantidad de figuras **0**, ninguna pieza y **una observación de error de validación** sobre el conjunto, sin posición de pieza porque no hay ninguna | Paso 7 |
| FA-04 | El texto no se puede leer **ni con la tolerancia** de T2 | Se devuelve cantidad de figuras 0, ninguna pieza y **una observación de error de validación** que declara que el texto no se pudo leer. **No es una condición de error de este contrato**: es un resultado, y el trabajo queda en `Borrador` por RN-05 | Paso 7 |
| FA-05 | Una figura de tipo reconstruible no expone el campo del que se lee una de sus dimensiones | Se emite observación de error de validación con la posición y el nombre del campo ausente, y se reserva la posición | Paso 4, con la figura siguiente |

**Los cinco terminan en un resultado, no en una negativa.** Es la distinción que gobierna todo este contrato: un texto que el alumno escribió mal es el caso normal del producto, no una avería del adaptador.

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `TEXTO_ORIGINAL_AUSENTE` | Se invocó la interpretación sin texto: nulo o cadena vacía | Termina sin interpretar. **No se confunde con el conjunto raíz vacío de FA-03**, que sí es un texto y sí produce observación: acá no llegó ningún texto y el defecto es de la invocación, no del alumno |
| `INTERPRETACION_NO_DISPONIBLE` | El adaptador no puede completar la interpretación por una causa que no depende del texto recibido | Termina de forma **degradada** y lo declara. **No inventa observaciones, no devuelve un conjunto vacío como si fuera un resultado y no informa figuras que no contó.** Es el código que `GeometriaFactory-Application` `CU-05` §6 declara recibir por este puerto, y el que hace que el trabajo quede en `Borrador` con su texto intacto |

**Ninguna de las dos escribe nada** —este contrato no persiste— y **ninguna modifica el texto original**.

## 7. Postcondiciones

- **Éxito:** el consumidor recibe la cantidad de figuras del conjunto raíz, el conjunto de piezas reconstruidas con su posición y sus componentes, y el conjunto de observaciones de error de validación, cada una con su posición y su campo. El texto original queda intacto.
- **Éxito con figuras no reconstruidas:** igual que el anterior, con las posiciones de esas figuras **reservadas** y dentro del rango declarado.
- **Fallo:** el consumidor recibe el código y nada más. Nada quedó escrito y el texto no cambió.

## 8. Criterios de aceptación

Los seis primeros son escenarios del intake, transcriptos por su identificador y con el resultado que el propio intake declara en su §20 y en la matriz de su §21. **No se inventó ningún dato de prueba**: es la regla de delivery 5 de §15 del intake.

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El texto del escenario **E-2**, `Ortoedro(7,7,21)` tal como lo emite el programa: con la clave `"Tapas"` y **dos comas finales** | Se interpreta | El texto **se lee pese a las comas finales** (T2), se reconoce el tipo `Ortoedro`, **las bases se leen desde `Tapas`** (T1) y la estructura reconstruida tiene **1 pieza, 2 bases y 4 laterales**. 0 observaciones de error de validación |
| CA-02 | El texto del escenario **E-3**, `Cubo(3)` de `Ejemplo1`, con caras `"Tipo":"Cuadrado"` | Se interpreta | Las caras se interpretan (T3) y la pieza se reconstruye entera. 0 observaciones de error de validación |
| CA-03 | El texto del escenario **E-4**, el mismo cubo de lado 3 emitido por `Ejemplo2`, con caras `"Tipo":"Rectangulo"` | Se interpreta | Las caras se interpretan **igual que las `Cuadrado` de CA-02** (T3). 0 observaciones de error de validación |
| CA-04 | El texto del escenario **E-5**: un cubo válido en la posición 0 y una figura con `"Tipo":"Piramide"` en la posición 1 | Se interpreta | Se devuelve cantidad de figuras del conjunto raíz **2**, **1 pieza reconstruida** —la de la posición 0— y **1 observación de especie error de validación que indica posición 1 y campo `Tipo`**. La posición 1 queda reservada. El índice reportado es **1 y no 0**, que es lo que verifica que se calcula y no se informa siempre el primero |
| CA-05 | El texto del escenario **E-6**: una única figura `Rectangulo` con `"Largo": 0.00` | Se interpreta | La figura **se interpreta y no se descarta**: la comparación es **por existencia del campo y no por veracidad del valor**. Se devuelve 1 pieza y **0 observaciones de error de validación** |
| CA-06 | El texto del escenario **E-7**, con los seis tipos: `Cilindro`, `Cubo`, `Ortoedro` —con clave `"Bases"`—, `Rectangulo`, `Cuadrado` y `Circulo` | Se interpreta | Se reconstruyen **6 piezas**, una por tipo, con las tres figuras planas **como piezas del conjunto raíz** y no como componentes. El ortoedro se lee con `Bases`, que T1 acepta igual que `Tapas`. 0 observaciones de error de validación |
| CA-07 | El texto del escenario **E-1**, el JSON semilla de tres piezas: `Cilindro(3,3)`, `Cubo(3)` y `Ortoedro(7,7,21)` | Se interpreta | Se devuelve cantidad de figuras **3** y **3 piezas** en las posiciones 0, 1 y 2. El cilindro se reconstruye con **2 tapas `Circulo` y 1 `Lado` de tipo `RectanguloDesarrollado`**. 0 observaciones de error de validación: las advertencias de este escenario son de `CU-02` |
| CA-08 | El texto de E-1 y un adaptador sin latencia añadida | Se interpreta | La interpretación completa resuelve en **menos de 200 ms**, medida sin acceso a base de datos. El valor está rotulado como asunción aguas arriba y se usa como vigente |
| CA-09 | Un texto cualquiera de los escenarios anteriores | Se interpreta y se compara el texto devuelto con el recibido | Son **idénticos carácter por carácter**: este contrato no reescribe, no reordena y no normaliza el texto del alumno (RN-08) |
| CA-10 | Un texto que no se puede leer ni con tolerancia a comas finales | Se interpreta | Se devuelven **0 figuras, 0 piezas y 1 observación de error de validación**, y **no** el código `INTERPRETACION_NO_DISPONIBLE`: un texto ilegible es un resultado del producto, no una avería del adaptador |
| CA-11 | Cualquiera de los textos anteriores, con la pestaña de red de un entorno de prueba observada | Se interpreta | **0 peticiones de red originadas por este contrato.** Recibe texto y devuelve observaciones (`PRODUCT-INTAKE` §17.3.P.3) |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-04, y NB-03 en cuanto el trabajo que no verifica se conserva |
| Reglas de negocio aplicables | [RN-08](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md), [RN-09](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-09-Observacion-De-Error-Con-Posicion-Y-Campo.md), [RN-05](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) por el efecto de las observaciones que emite |
| Puerto que implementa | Validación de figuras, en su mitad de interpretación y reconstrucción |
| Consumidor | `GeometriaFactory-Application` [`CU-05`](../../../GeometriaFactory-Application/02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-E-Interpretar-Su-Texto.md) |
| Escenarios del intake que lo ejercitan | E-1, E-2, E-3, E-4, E-5, E-6, E-7 (§20), con la matriz de §21 |
| Historias de usuario a generar en 06 | US-01, US-02, US-03, US-04 |
| Componentes esperados en 05 | Adaptador del puerto de validación de figuras, con la lectura tolerante y la tabla de tipos reconstruibles |
| Tests previstos en 08 | Los ocho casos de la batería obligatoria que corresponden a la interpretación, con los textos de E-1 a E-7 como fixtures y **sin motor de persistencia**. La cobertura mínima del validador es la más alta del producto |

## 10. Notas y supuestos

- **Las cuatro trampas se declaran acá y no se descubren después.** T1, la clave sinónima del ortoedro; T2, las comas finales; T3, la cara del cubo con dos nombres; y T4, que los valores calculados erróneos **se señalan y no se rechazan**, que es de `CU-02`. Las cuatro están en `PRODUCT-INTAKE` §17.3.P.11 punto 1 y ejercitadas por los escenarios de §20.
- **La familia plana o volumétrica no viaja reconstruida como dato guardado**: se deriva del `Tipo`. Es decisión pre-tomada aguas arriba y su consecuencia sobre el almacén está en `RC-04`.
- **Una figura no reconstruida reserva su posición.** No se compacta el conjunto: si se compactara, la posición que la observación designa dejaría de coincidir con la figura que el alumno escribió, y RN-09 dejaría de servirle para encontrarla.
- **Un error de validación no es una condición de error de este contrato.** Es un resultado, es una entidad del dominio y es lo que el alumno tiene que ver. Confundirlos es el defecto que la §1.2 del catálogo de errores de la categoría 03 previene.
- **El escenario E-8 no es de este contrato**, y conviene decirlo porque su payload sí es interpretable acá: su texto es JSON sintácticamente válido y lo que falla es la lectura de un valor, `"3,50"` como cadena. El propio intake lo declara «el borde del visor, no el del validador» (§20) y su punto 4 dice que ese escenario **no prescribe el desenlace del envío**. Qué devuelve **este** contrato ante ese texto no está declarado por ninguna fuente y queda como punto abierto en `Especificacion-Funcional.md` §11.
- **`RectanguloDesarrollado` no tiene escenario propio** y aparece sólo como componente `Lado` del cilindro, en E-1 y en E-7. Así lo emite el programa, y el intake declara en §21 que no se agregó un escenario que lo use como pieza suelta porque ninguna fuente lo documenta como salida real.
- El tiempo de CA-08 se toma de la asunción de requerimientos no funcionales declarada aguas arriba, pendiente de confirmación del Product Owner; se usa como valor vigente.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |

## 17. Compatibilidad de la superficie pública

Agregar un tipo reconstruible es compatible: el conjunto de tipos crece y ningún texto que antes se interpretaba deja de interpretarse. **Dejar de aceptar una clave sinónima, dejar de tolerar las comas finales, descartar una figura por el valor de una dimensión o compactar las posiciones son cambios incompatibles** y suben versión mayor: los cuatro rompen textos que hoy se interpretan, y los tres primeros contradicen T1, T2 y el criterio de existencia contra veracidad.
