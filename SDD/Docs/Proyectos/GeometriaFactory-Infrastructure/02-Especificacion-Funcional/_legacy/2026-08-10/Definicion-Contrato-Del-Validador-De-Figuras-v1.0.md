# Definición del contrato del validador de figuras

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Definicion-Contrato-Del-Validador-De-Figuras.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11** §1, §3 (D-1, D-2, D-3), §4 (F-09, F-10), §4.1 (RN-05, RN-08, RN-09), §7 (CL-3, CL-4), §10 («formato de entrada no negociable»), §11 (**RN-B3**), §12, §14, §17.3.P.3, §17.3.P.6, §17.3.P.10, §17.3.P.11 punto 1, §20 completo (**E-1 a E-8**) y §21 (matriz de cobertura); `Proyectos/GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`, para la frontera entre lo que interpreta y lo que dibuja
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Por qué este documento existe](#1-por-qué-este-documento-existe)
- [2. Las cuatro trampas del formato](#2-las-cuatro-trampas-del-formato)
- [3. Qué devuelve el validador](#3-qué-devuelve-el-validador)
- [4. Las garantías del contrato](#4-las-garantías-del-contrato)
- [5. Los tipos que reconstruye](#5-los-tipos-que-reconstruye)
- [6. Los ocho escenarios, uno por uno](#6-los-ocho-escenarios-uno-por-uno)
- [7. Cobertura contra la batería obligatoria](#7-cobertura-contra-la-batería-obligatoria)
- [8. La frontera con el visor](#8-la-frontera-con-el-visor)
- [9. Puntos abiertos de este contrato](#9-puntos-abiertos-de-este-contrato)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Por qué este documento existe

El intake declara, en su registro de riesgos de negocio, que **el defecto que más veces se repite es escribir el validador sin leer el análisis**, y le pone probabilidad alta si no se controla y impacto alto: *«la aplicación no sirve para el dato que existe»* (RN-B3). Su mitigación declarada es la batería obligatoria de nueve casos de prueba con datos verificados, y los escenarios de la Parte D como fixtures.

Este documento es esa mitigación puesta por escrito **antes** de que alguien empiece a leer texto. Reúne en un solo lugar lo que hay que saber para no cometer el defecto: qué tiene el dato real que un lector estricto rechaza, qué devuelve el contrato, qué garantiza, y qué escenario prueba cada cosa.

**La premisa que ordena todo lo demás:** el texto lo produce el alumno con su programa y **su formato es el que está**. El servicio se adapta al dato, nunca al revés.

## 2. Las cuatro trampas del formato

Las cuatro están declaradas en `PRODUCT-INTAKE` §17.3.P.11 punto 1, y el contrato **nace sabiéndolas**.

| Id | Trampa | Qué hace un lector ingenuo | Qué hace este contrato |
| --- | --- | --- | --- |
| **T1** | El ortoedro emite la clave **`"Tapas"`** para lo que el visualizador previo exige como **`"Bases"`** | Busca `Bases`, no la encuentra y no dibuja el ortoedro. **Falla en silencio** | Acepta las **dos claves como sinónimas**. Es la línea que desbloquea el dibujo de todos los ortoedros generados por la aplicación |
| **T2** | El texto trae **comas finales**: una coma antes del cierre de un array o de un objeto | Rechaza el texto entero como inválido, y el trabajo del alumno no entra nunca | Lee **con tolerancia a comas finales y omisión de comentarios**. El texto del alumno **no es JSON estrictamente válido** y eso es un hecho del producto, no un error a corregir |
| **T3** | Las caras del cubo llegan con `Tipo` **`Cuadrado`** en un ejemplo de la cátedra y **`Rectangulo`** en el otro | Acepta una y descarta la otra, según cuál haya mirado primero | Acepta **las dos**: son la misma cara emitida por dos programas distintos |
| **T4** | Los valores calculados que trae el texto **son incorrectos en dos casos reproducibles**: el área del cubo usa `4·l²` en lugar de `6·l²`, y el volumen del ortoedro ignora el largo | Rechaza el trabajo por «datos inconsistentes», o —peor— **corrige el número** | **Los señala y no los rechaza ni los corrige.** Es el mayor valor didáctico del producto: el alumno ve sobre su propio trabajo que su cubo declara 36.00 donde la geometría dice 54.00 |

**Una quinta cosa que no es trampa de formato pero se equivoca igual de seguido:** el criterio de **existencia contra veracidad**. Una dimensión presente con valor `0.00` está presente, y la figura **no se descarta**. Descartarla sería aplicar un juicio que ninguna regla pidió, y dejaría al alumno sin ver su propio error.

## 3. Qué devuelve el validador

El contrato devuelve **tres cosas y no dos**, y la tercera es la que se olvida:

| Lo que devuelve | Qué es | Por qué importa |
| --- | --- | --- |
| **La cantidad de figuras del conjunto raíz** | Cuántas figuras trae el texto, **incluidas las que no se pudieron reconstruir** | **No es derivable de las piezas**, porque el conjunto admite huecos. Es el rango de posiciones válidas: sin él, el dominio no tiene contra qué comprobar que la posición de una observación existe |
| **Las piezas reconstruidas** | Cada una con su **posición** en el conjunto raíz, su tipo, sus dimensiones, sus componentes y sus dos valores —declarado y derivado— | La posición es la identidad de la pieza: el texto del alumno no trae identificador |
| **Las observaciones** | Cada una con su **especie** —`Advertencia` o `Error de validación`—, su **posición** y su **campo** | Es lo que el alumno ve. Sólo el error de validación impide que el trabajo pase a estado `Pendiente` |

**Y una cosa que no devuelve: el estado del trabajo.** El contrato entrega el conjunto de observaciones y **el dominio resuelve el estado**. Un validador que decidiera el estado tendría dentro una regla de negocio que no le pertenece.

## 4. Las garantías del contrato

Siete, y todas son verificables:

| Id | Garantía | Dónde se verifica |
| --- | --- | --- |
| G-1 | **El texto original no se modifica**, ni siquiera cuando la interpretación falla | `CU-01` CA-09 |
| G-2 | **Un defecto en una figura no descarta el resto del análisis**: el recorrido sigue | `CU-01` CA-04 |
| G-3 | **La posición de una figura no reconstruida queda reservada** y no se compacta | `CU-01` CA-04, `CU-03` CA-08 |
| G-4 | **Ningún mensaje es genérico**: todo error de validación indica posición y campo | `CU-01` CA-04 |
| G-5 | **Un valor calculado erróneo produce advertencia, nunca rechazo** | `CU-02` CA-01, CA-04, CA-06 |
| G-6 | **El contrato no hace red y no lee configuración propia**: recibe texto y devuelve observaciones | `CU-01` CA-11 |
| G-7 | **Un texto que el alumno escribió mal es un resultado, no una avería**: produce observaciones, no la condición degradada | `CU-01` CA-10 |

**G-7 es la que más veces se rompe al implementar**, y por eso tiene criterio propio: un texto ilegible tiene que devolver observaciones y no `INTERPRETACION_NO_DISPONIBLE`, porque el segundo dejaría al alumno sin saber qué corregir.

## 5. Los tipos que reconstruye

| Familia | Tipos | Dónde aparecen |
| --- | --- | --- |
| Volumétricos | `Cilindro`, `Cubo`, `Ortoedro` | Como piezas del conjunto raíz, en E-1, E-2, E-7 |
| Planos | `Rectangulo`, `Cuadrado`, `Circulo` | Como **piezas del conjunto raíz** en E-7 y en E-6, y como **componentes** en todos los demás |
| Componente sin forma de pieza | `RectanguloDesarrollado` | **Sólo** como componente `Lado` del cilindro, en E-1 y en E-7 |

**`RectanguloDesarrollado` no tiene escenario propio**, y el intake declara por qué: aparece sólo como componente, así lo emite el programa, y no se agregó un escenario que lo use como pieza suelta porque **ninguna fuente lo documenta como salida real**. Su nombre es además una trampa clásica para el consumidor del dato: es la superficie lateral del cilindro **desenrollada**, con `Ancho = 2πr` y `Largo = altura`.

**Un tipo fuera de este conjunto produce un error de validación con posición y campo `Tipo`**, y ése es el escenario E-5. Hasta dónde llega el conjunto es un punto abierto, y está en §9.

## 6. Los ocho escenarios, uno por uno

Los ocho están transcriptos completos en `PRODUCT-INTAKE` §20, con su procedencia y su estado. Acá se declara **qué le toca a este contrato en cada uno**.

| Escenario | Qué es | Qué ejercita **acá** | Resultado esperado en este contrato |
| --- | --- | --- | --- |
| **E-1** | El JSON semilla del visualizador previo: tres piezas, `Cilindro(3,3)`, `Cubo(3)` y `Ortoedro(7,7,21)` | El camino completo, y **el operador estricto de la tolerancia** | **3 piezas y 2 advertencias.** Área del cubo 36.00 contra 54.00; volumen del ortoedro 343.00 contra 1029.00. El **área del cilindro no advierte**, porque su diferencia es exactamente 0.01. El **área del ortoedro tampoco**: 686.00 coincide |
| **E-2** | `Ortoedro(7,7,21)` **tal como lo emite el programa** | **T1 y T2 juntas en el mismo texto**, más T4 | Lectura exitosa pese a las dos comas finales; bases leídas desde `Tapas`; **1 pieza, 2 bases y 4 laterales**; área sin observación y **1 advertencia de volumen** |
| **E-3** | `Cubo(3)` de `Ejemplo1`, caras `Cuadrado` | **T3 por el lado de `Ejemplo1`** y **T4 en su forma más visible** | Caras interpretadas; **1 advertencia de área**, declarada 36.00 contra derivada 54.00; volumen sin observación |
| **E-4** | `Cubo(3)` de `Ejemplo2`, caras `Rectangulo` | **T3 por el otro lado**, y el **criterio negativo** | Caras interpretadas igual que en E-3, y **cero observaciones en total**. Es el escenario que un validador que advirtiera siempre haría fallar |
| **E-5** | Un cubo válido en la posición 0 y un tipo desconocido en la 1 | **RN-09 y RN-05 juntas** | **1 observación de error de validación con posición 1 y campo `Tipo`**; la pieza de la posición 0 se reconstruye igual; el trabajo queda en `Borrador` |
| **E-6** | Un `Rectangulo` con `"Largo": 0.00` | **Existencia contra veracidad** | La figura **se interpreta y no se descarta**; a lo sumo una advertencia por el valor derivado, **nunca** un error de validación |
| **E-7** | Los seis tipos dibujables, con `"Bases"` en el ortoedro | **La cobertura del mapeo de tipos**, y T1 por su otra clave | **6 piezas**, con las tres figuras planas como piezas del conjunto raíz. Cero errores de validación |
| **E-8** | Un ortoedro válido y un cubo con `"Largo": "3,50"` como cadena | **Nada declarado para este contrato** | **No hay resultado declarado.** El intake lo llama «el borde del visor, no el del validador» y su propio texto dice que **no prescribe el desenlace del envío**. Es un punto abierto: ver §9 |

**Los siete primeros son los que `PRODUCT-INTAKE` §17.3.P.6 declara como entrada de la batería de este proyecto de código.** El octavo entró después, por el contrato de la pieza que dibuja.

## 7. Cobertura contra la batería obligatoria

La batería obligatoria del producto tiene **nueve casos de prueba**, y la matriz de `PRODUCT-INTAKE` §21 los cruza contra el escenario que ejercita a cada uno. Acá se agrega la columna que a esta categoría le falta: **qué caso de uso lo cubre y con qué criterio de aceptación**.

| # | Caso de prueba obligatorio | Escenario | CU de esta categoría | Criterio |
| --- | --- | --- | --- | --- |
| 1 | Ortoedro con clave `Tapas` (T1) | E-2 | CU-01 | CA-01 |
| 2 | Texto con comas finales (T2) | E-2 | CU-01 | CA-01 |
| 3 | Cubo de `Ejemplo1`, caras `Cuadrado` (T3) | E-3 | CU-01 | CA-02 |
| 4 | Cubo de `Ejemplo2`, caras `Rectangulo` (T3) | E-4 | CU-01 | CA-03 |
| 5 | Área de `Cubo(3)` de `Ejemplo1` | E-3 | CU-02 | CA-04 |
| 6 | Volumen de `Ortoedro(7,7,21)` | E-2, E-1 | CU-02 | CA-06, CA-01 |
| 7 | Dimensión en `0` | E-6 | CU-01 y CU-02 | CA-05 y CA-07 |
| 8 | `Tipo` desconocido | E-5 | CU-01 | CA-04 |
| 9 | JSON semilla completo | E-1 | CU-01 y CU-02 | CA-07 y CA-01 |

**Nueve casos, todos con criterio de aceptación en esta categoría. Ninguno queda sin cubrir y ninguno se inventó.**

Tres precisiones sobre la matriz, para que nadie las lea como huecos:

1. **La matriz de §21 tiene diez filas y no nueve.** La décima —«dimensión no legible en el visor», E-8— **no pertenece a la batería obligatoria** de este proyecto de código: entró el 2026-08-09 con el contrato de la pieza que dibuja y su lugar de verificación es la etapa de visualización.
2. **E-7 no respalda ninguno de los nueve**, aunque §17.3.P.6 lo declare como entrada de este proyecto de código. Acá se usa igual, y su valor es real: es el único texto que ejercita el mapeo completo de los seis tipos y las figuras planas como piezas del conjunto raíz. Se declara como cobertura **adicional** y no como parte de la batería.
3. **Los cuatro casos que verifican valores son de `CU-02` y los cinco de estructura son de `CU-01`**, con dos —el 7 y el 9— que tocan a los dos. Es la partición que este proyecto de código heredó del dominio.

## 8. La frontera con el visor

Es la frontera que más se confunde, porque los dos leen el mismo texto y los dos toleran las mismas claves. **Y no es duplicar la validación.**

| | Este contrato | La fachada del visor |
| --- | --- | --- |
| Qué decide | **Si el trabajo verifica**, y emite observaciones con posición y campo | **Qué puede dibujar**, y enumera lo que no dibujó |
| Qué produce | Piezas, valores derivados y observaciones | Mallas |
| Qué le basta saber | El texto entero, con sus valores | **De dónde sacar una dimensión** para construir la malla |
| Dónde corre | En la pieza de datos, sin red | En el navegador, sin red y sin identidad |
| Sobre el `0.00` | **Lo interpreta** y a lo sumo advierte | **Lo dibuja**: el cero es una dimensión legible |

El intake lo declara sin ambigüedad: el bundle tolera las mismas claves que el backend y **eso no es duplicar la validación**, porque el backend decide si el trabajo es válido y el bundle sólo necesita saber de dónde sacar una dimensión.

**La consecuencia práctica** está en el escenario E-8: la condición `DIMENSION_NO_LEGIBLE` es de la fachada y no de acá, y el propio escenario declara que **decidir si el trabajo pasa a estado `Pendiente` es del validador, no del bundle**. Que la fachada no haya podido dibujar una pieza no dice nada sobre si el trabajo verifica.

## 9. Puntos abiertos de este contrato

| Punto | Situación | Quién lo resuelve |
| --- | --- | --- |
| **Qué devuelve este contrato ante el texto de E-8** | El texto de E-8 es JSON sintácticamente válido y lo que falla es la lectura de un valor: `"3,50"` llega como cadena porque el emisor escribe la coma decimal de su cultura. **Ninguna fuente declara qué hace el validador con él**: §20 lo declara borde del visor y su punto 4 dice explícitamente que no prescribe el desenlace del envío. Las dos lecturas posibles tienen consecuencia visible para el alumno: **error de validación** —el trabajo queda en `Borrador` y el alumno corrige su programa— o **figura no reconstruida con su posición reservada**, que es un error de validación igual pero acotado a esa pieza. **Esta categoría no elige** y lo eleva | Product Owner, y `08-Calidad-Y-Pruebas` al fijar el resultado esperado |
| **Hasta dónde llega el conjunto de tipos reconstruibles** | Los seis de §5 son los que los escenarios ejercitan y los que la pieza que dibuja sabe dibujar. El análisis del que sale el intake menciona **siete clases en `Ejemplo1` y diez en `Ejemplo2`**, y **ninguna fuente las enumera**, de modo que no se puede afirmar cuáles son ni si alguna emite un tipo que no está en los seis. Un tipo fuera del conjunto produce error de validación, que es un resultado correcto pero puede no ser el deseado si la clase existe en la actividad | Product Owner, con la enumeración de las clases de la Actividad 1 |
| Cuál es el valor derivado del área de una pieza volumétrica | El intake lo muestra dos veces como **suma de los componentes** —el cilindro de E-1 y el ortoedro de E-2— y una vez como fórmula —`6·l²` en el cubo de E-3—, y las dos formas **coinciden** en ese cubo. No hay contradicción declarada, pero tampoco hay una regla enunciada. `CU-02` §10 adopta la suma de componentes y lo declara | `05-Arquitectura-Tecnica`, al fijar la tabla de derivación por tipo |
| El límite de tamaño del texto que se acepta | Ninguna fuente lo declara, y el requerimiento no funcional declarado está medido sobre un texto de tres piezas. Un texto arbitrariamente grande no tiene hoy ningún corte declarado | Product Owner, y `05-Arquitectura-Tecnica` |

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Documento de concepto central del proyecto de código. Declara las cuatro trampas del formato con lo que hace un lector ingenuo y lo que hace este contrato, las tres cosas que devuelve y la que no, las siete garantías con su criterio de verificación, el conjunto de tipos que reconstruye, los ocho escenarios del intake con lo que a este contrato le toca en cada uno, la cobertura completa de la batería obligatoria de nueve casos contra los criterios de aceptación de `CU-01` y `CU-02`, la frontera con la pieza que dibuja y los cuatro puntos abiertos. |
