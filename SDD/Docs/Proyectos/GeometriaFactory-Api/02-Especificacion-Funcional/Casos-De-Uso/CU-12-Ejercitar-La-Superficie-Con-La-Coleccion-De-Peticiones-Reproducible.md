# CU-12 — Ejercitar la superficie con la colección de peticiones reproducible

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-12-Ejercitar-La-Superficie-Con-La-Coleccion-De-Peticiones-Reproducible.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13** §16.1 (qué hay en la carpeta de muestras para el tipo `rest-api`), §18 (**S-2**, con su reproducibilidad de cinco pasos o menos), §15 (regla de delivery: **no se inventan textos de prueba**), §20 (**los ocho escenarios**, E-1 a E-8), §21 (matriz de cobertura), §17.5.P.6, §10 (host de desarrollo sin herramientas)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y **`10-Examples`** de GeometriaFactory-Api

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

---

## 1. Propósito

Declarar la **colección de peticiones reproducible**, que es lo que el intake §16.1 asigna a este tipo de proyecto de código como contenido de su carpeta de muestras, y que §18 registra como la muestra **S-2** del producto.

No implementa ninguna capacidad: **demuestra**. Por eso no traza a ninguna necesidad de negocio, y el índice maestro §7.2 lo declara: asignarle las necesidades de las capacidades que ejercita las contaría dos veces. Lo que sí tiene es una obligación propia y verificable: **reproducirse en cinco pasos o menos, enteramente dentro del entorno de desarrollo contenido, y no inventar ningún texto de prueba**.

Su valor es el que ningún caso de uso suelto puede dar: recorrida entera, **la colección es la única forma de comprobar que la superficie ensamblada hace lo que las tres capas de adentro prometen**, sobre el dato real del alumno y sin ninguna pantalla de por medio.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Quien construye el producto | Primario | Ejecuta la colección contra el servicio real y compara lo que recibe con lo que la colección declara esperado |
| El servicio de este proyecto de código | Sistema | Responde. **Es la única pieza involucrada**: la colección no necesita la pieza pública ni el visor |
| Los ocho escenarios del intake §20 | Datos | Son los cuerpos. **Ninguno se inventó y ninguno se modificó** |

## 3. Precondiciones

- El servicio arranca desde el entorno de desarrollo contenido. **El host no tiene las herramientas y no va a tenerlas**: ningún paso de esta colección se ejecuta fuera de ese entorno.
- El almacén se puede llevar a su estado de primer arranque con el guion de reinicio que el repositorio declara.

## 4. Flujo principal

Los pasos se nombran por su papel, y las rutas y los nombres de guion salen del intake §16 y §18: **no se eligen acá**.

1. **Guion de reinicio del almacén**: deja el estado de primer arranque, sin ninguna cuenta y sin ningún trabajo.
2. **Guion de ejecución del servicio**: el servicio arranca, aplica las transformaciones sobre el almacén vacío y responde salud.
3. **Ejecutar la colección**, que recorre el circuito completo en el orden en que el producto se usa.

**Tres pasos, sobre un máximo de cinco.** Es la propiedad de reproducibilidad que el intake §18 exige a las tres muestras del producto.

El recorrido que la colección ejecuta, en orden:

| # | Qué ejercita | Puntos de acceso |
| --- | --- | --- |
| 1 | Configurar la cuenta de administrador en el primer arranque, y comprobar que **un segundo intento no procede** | A-03 |
| 2 | Registrar una cuenta de alumno, habilitarla —lo que devuelve su contraseña provisoria— y cambiarla con esa provisoria como vigente | A-02, A-01, A-06, A-07, A-05 |
| 3 | Canjear credenciales como alumno y como administrador | A-01 |
| 4 | Enviar **los ocho escenarios** como cuerpo, uno por trabajo | A-10 |
| 5 | Listar y abrir el detalle de cada uno, con los dos papeles | A-13, A-14 |
| 6 | Aprobar uno y rechazar otro, con comentario y sin comentario | A-15 |
| 7 | Forzar los caminos que las reglas prohíben, **contra la superficie y no contra una pantalla** | A-12, A-15, A-11 |
| 8 | Resetear la contraseña del alumno y comprobar que **queda confinado al cambio** | A-09, A-05, A-13 |

**El paso 7 es el que justifica que esta colección exista y no sea un recorrido feliz.** El intake declara bloqueante que la eliminación de un trabajo que no está en `Borrador` o que no pertenece al solicitante **se verifique forzando la petición contra esta superficie**, y una colección de peticiones es exactamente el instrumento para eso.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se quiere ejercitar sólo la parte del dato del alumno | Se ejecutan los pasos 1 a 5 del recorrido. Es lo que hace falta para ver, sin ninguna pantalla, **qué observaciones produce cada escenario y en qué estado queda cada trabajo** | Termina |
| FA-02 | La colección se ejecuta dos veces sin reiniciar el almacén | El paso 1 del recorrido responde con su negativa, porque el administrador ya está configurado, y el paso 2 con la suya, porque el correo ya está registrado. **Es un resultado legítimo y la colección lo declara**: reiniciar el almacén es el paso 1 del flujo principal por este motivo | Termina |

## 6. Excepciones y errores

Este contrato no produce condiciones propias: **las provoca a propósito** y compara lo recibido con lo esperado. Lo que declara es qué se espera de cada escenario, y **cada expectativa es la que su propia fuente declara, no una que esta categoría calcule**.

| Escenario | Qué ejercita | Resultado esperado del envío |
| --- | --- | --- |
| **E-1** | El texto semilla de tres piezas | Éxito. **3** piezas, **2** advertencias, estado `Pendiente` |
| **E-2** | Las dos trampas juntas: la clave `"Tapas"` y las comas finales | Éxito. **1** pieza, **1** advertencia de volumen —declarado 343.00 contra derivado 1029.00—, estado `Pendiente` |
| **E-3** | El cubo con caras `Cuadrado` y el área declarada 36.00 | Éxito. **1** advertencia de área —declarada 36.00 contra derivada 54.00—, estado `Pendiente` |
| **E-4** | El mismo cubo con caras `Rectangulo` y el área declarada 54.00 | Éxito. **0** observaciones, estado `Pendiente`. **Es el criterio negativo**, más difícil de acertar que el positivo |
| **E-5** | El tipo desconocido en la segunda figura | Éxito. **1** observación de error de validación, con **índice de figura 1** y campo `Tipo`, estado `Borrador` |
| **E-6** | La dimensión en `0.00` | Éxito. La figura **se interpreta y no se descarta**; estado `Pendiente` |
| **E-7** | La cobertura de los seis tipos, con la clave `"Bases"` | Éxito. Se interpreta con `Bases` igual que con `Tapas`, y las **6** piezas se reconstruyen. Estado `Pendiente`. **El recuento de observaciones no se declara acá**: ninguna fuente lo declara para este escenario, y esta categoría **no lo calcula** |
| **E-8** | La dimensión no legible, con la coma decimal de la cultura de la máquina | Éxito. Observación de **error de validación** localizada por índice de figura y campo, estado `Borrador`, según el `PRODUCT-INTAKE` **1.12** §20.E-8 punto 5 |

**Qué de esta tabla está declarado y qué se deriva, para que nadie lo lea todo con el mismo peso.** Los recuentos de piezas y de observaciones y los pares de valores declarado y derivado **salen de la sección «qué verificar» del escenario correspondiente en el intake §20**, sin cambio. El **estado resultante** lo declara el propio escenario en **E-1, E-2, E-5, E-6 y E-8**; en **E-3, E-4 y E-7** el escenario no lo nombra y acá se **deriva de RN-05** —no hay error de interpretación, de modo que el trabajo pasa a estado `Pendiente`—. Es una derivación de una regla enunciada, no un cálculo sobre los datos, y se declara para distinguirla de los recuentos.

**Los ocho responden con éxito, y ninguno con fallo.** Es la propiedad más importante que esta colección demuestra: **el estado del trabajo no es el código de respuesta**. Dos de los ocho quedan en `Borrador` y los ocho se guardaron.

## 7. Postcondiciones

- **Ejecutada entera:** quedan ocho trabajos con los estados que sus escenarios determinan, una cuenta de alumno con la marca puesta por el reseteo, y **una comparación completa entre lo esperado y lo recibido**.
- **En cualquier punto:** el almacén se puede devolver a su estado de primer arranque con el guion del paso 1, y la colección **vuelve a dar lo mismo**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un clon limpio del repositorio de código y el entorno de desarrollo contenido construido | Se ejecuta la colección | Se llega al primer resultado en **3 pasos**, y los 3 ocurren **dentro** del entorno contenido: **0 pasos** en el host |
| CA-02 | Los cuerpos de la colección | Se comparan con los escenarios del intake §20 | Son **idénticos**: **0 textos inventados** y **0 textos modificados**, incluidas las comas finales y las claves de **E-2** |
| CA-03 | La colección ejecutada entera | Se cuentan sus respuestas de envío | **8** envíos, **8** respuestas de éxito, **6** trabajos en `Pendiente` y **2** en `Borrador` —los de **E-5** y **E-8**— |
| CA-04 | La colección ejecutada dos veces con reinicio del almacén entre las dos | Se comparan los dos resultados | Son **iguales**: la colección es reproducible |
| CA-05 | El paso 7 del recorrido | Se ejecuta | Fuerza **al menos** la eliminación de un trabajo que no está en `Borrador` por su dueño, la eliminación de un trabajo ajeno y el desenlace pedido por un alumno, y **las 3** son rechazadas por el servicio, no por una pantalla |
| CA-06 | El paso 8 del recorrido | Se ejecuta | Después del reseteo, el listado pedido por el alumno responde con el rechazo de la guardia, y **después** del cambio de contraseña la misma petición funciona |
| CA-07 | Los archivos de la colección | Se inspeccionan | **0 apariciones** de una clave de firma, de una contraseña real y de la dirección de un servidor de producción. Lo que contienen son datos de prueba declarados del producto |
| CA-08 | La colección | Se recorre contra la tabla de puntos de acceso | Ejercita **13 de los 15** puntos —A-01 a A-03, A-05 a A-07 y A-09 a A-15—. Los **2** que no ejercita, A-08 y A-16, se declaran en §10 con su motivo. 13 + 2 = 15. El punto **A-04 se retiró** de la superficie con `PRODUCT-INTAKE` 1.13 y por eso ni se ejercita ni se cuenta |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | **Ninguna**, y el índice maestro §7.2 lo declara con su motivo: esta colección no implementa, demuestra |
| Reglas de negocio aplicables | Las que el paso 7 fuerza: [RN-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-03-Trabajo-Ajeno-Indistinguible-De-Inexistente.md), [RN-04](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-04-Eliminacion-Acotada-Al-Borrador.md) y [RN-10](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-10-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md); y las que el paso 8 demuestra: [RN-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) |
| Escenarios que usa | **E-1 a E-8** del intake §20, **los ocho**, sin renumerar y sin modificar |
| Puntos de acceso que ejercita | **13 de 16**. Los tres que no, en §10 |
| Muestra del producto | **S-2**, declarada en el intake §18 |
| Historias de usuario a generar en 06 | US-30 |
| Componentes esperados en 05 | Ninguno de tiempo de ejecución. La forma del archivo de la colección es de `05-Arquitectura-Tecnica` y su contenido vive en `10-Examples` |
| Tests previstos en 08 | La colección **no reemplaza a las pruebas de integración** y no se cuenta como cobertura: es una demostración ejecutable a mano. Lo que sí comparte con ellas son los datos |

## 10. Notas y supuestos

- **El alcance de la colección es una derivación declarada, y su motivo es un residuo de la fuente.** El intake la describe en dos lugares que **no dicen lo mismo**: §16.1 habla de «los escenarios E-1 a E-7 como cuerpo» y §18, en la muestra S-2, nombra «los cuerpos de E-2 y E-5». **Las dos descripciones son anteriores a que E-8 entrara al intake**, y ninguna de las dos se actualizó. Esta categoría adopta **los ocho**, y el fundamento es que **E-8 es el modo de falla que el propio intake llama el más probable de todos**, porque lo produce la configuración regional de la máquina del alumno y no un error de programación: dejarlo fuera de la única demostración ejecutable del proyecto de código principal sería dejar sin demostrar justamente lo que más va a pasar. **Está elevado al Product Owner** en el índice maestro §11, para que confirme el alcance y actualice §16.1 y §18.
- **Los dos puntos de acceso que la colección no ejercita, y una precisión sobre un tercero.** **A-08**, la baja física de una cuenta: ejercitarla dejaría la colección sin el alumno con el que sigue el recorrido, y su verificación —que no quede ningún trabajo— vive en las pruebas de integración. **A-16**, el punto de salud: lo ejercita el **paso 2 del flujo principal**, que espera a que responda antes de seguir, de modo que queda ejercitado **fuera** del archivo de la colección. Y la precisión: **A-12 se ejercita, pero sólo en sus caminos rechazados** —el paso 7 lo fuerza como alumno sobre un trabajo que no está en `Borrador` y sobre uno ajeno—; **no se ejercita su camino de éxito**, porque borraría los trabajos que los pasos siguientes usan. Los tres puntos se declaran para que la cuenta de CA-08 cierre y para que nadie los reponga sin pensar en el orden del recorrido.
- **No se inventa ningún texto de prueba, y es una regla de delivery del producto**, no una preferencia de esta categoría. El intake §15 lo declara para todo guion que involucre el JSON del alumno.
- **La colección no necesita ninguna otra pieza del producto.** No hay pantalla, no hay circuito y no hay visor: se ejecuta contra el servicio y nada más. Es la contracara exacta de la otra muestra del producto, que ejercita el visor **sin backend**; entre las dos, las dos mitades del producto quedan demostrables por separado.
- **Que la colección exista no reemplaza el guion de demostración de cada etapa**, que se recorre en el navegador delante del cliente. Son dos cosas distintas: aquélla demuestra el producto y ésta demuestra la superficie.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-16) y la precisión de F-04**, que retiran el punto de acceso **A-04** de la superficie. **§4** rehace el guion 2 de la colección: la habilitación devuelve la provisoria y el cambio se hace por **A-05**, con esa provisoria como vigente, en lugar de establecerla por A-04. **§8** actualiza **CA-08**: la cobertura pasa de **14 de 16** a **13 de 15**, con los mismos dos puntos no ejercitados y su mismo motivo. La cabecera cita el intake **1.13**. **La reproducibilidad de cinco pasos, los ocho escenarios de datos y la prohibición de inventar textos de prueba no cambian.** Sube minor. |
