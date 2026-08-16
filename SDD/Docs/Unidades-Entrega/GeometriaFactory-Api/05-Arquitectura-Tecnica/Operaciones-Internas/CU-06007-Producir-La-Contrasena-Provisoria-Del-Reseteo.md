# CU-06007 — Producir la contraseña provisoria del reseteo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md
**Versión:** 1.4
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md); [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14** §4 (F-26), §4.1 (**RN-06014**, RN-06012, RN-06013, RN-06015, **RN-06016**), §7 (CL-7), §9 (X-2 retirada), §17.1.P.2 · GeometriaFactory-Domain (INV-09); [`RN-02014`](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md); `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §6 y §8, que declaran que **RN-06014 no se ejerce allá** y que su mecanismo es de este proyecto de código; `Proyectos/GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-06008-Contrato-De-Reseteo-Y-Cambio-Obligatorio-De-Contrasena.md` §10 y **CA-10**
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

Producir el valor de la **contraseña provisoria** cuando el administrador **habilita** la cuenta de un alumno o **resetea su contraseña**, con las dos propiedades que la regla exige: **no es adivinable** y **no se repite** entre cuentas ni entre actos sobre la misma cuenta.

**Este contrato tiene desde `PRODUCT-INTAKE` 1.13 dos consumidores y un solo mecanismo.** **RN-06016** declara que habilitar una cuenta produce una contraseña provisoria con las mismas propiedades y el mismo tratamiento que la del reseteo, de modo que `GeometriaFactory-Application` `CU-06002` pide el valor por este mismo contrato, igual que `CU-04011`. **Nada de este contrato cambia por eso**, y ése es exactamente el punto: la invocación no lleva ningún dato de la cuenta ni del acto que la motiva, de modo que **no hay forma de que el valor dependa de cuál de los dos la pidió**. El nombre del documento se conserva por estabilidad de citación.

**Este contrato es el destinatario declarado de una delegación explícita.** Las tres capas de arriba resolvieron todo lo demás del reseteo y dejaron el mecanismo acá con nombre: `GeometriaFactory-Application` declara que RN-06014 **es la única de las dieciséis reglas sin tramo en su capa**, porque el valor le llega ya producido y ya derivado; `GeometriaFactory-Contracts` declara que **el contrato no declara mecanismo** y que producir un valor con esas dos propiedades es de `05-Arquitectura-Tecnica` y de este proyecto de código; y `GeometriaFactory-Domain` la enuncia como regla sin invariante, porque describe cómo se produce un valor y no una condición permanente sobre el estado.

El fundamento de la regla está registrado aguas arriba y no se reabre acá: si la contraseña la escribe el docente, **termina siendo la misma clave para toda la comisión**.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor (`GeometriaFactory-Application` `CU-06002` y `CU-04011`, por la composición de raíz) | Primario | Pide una contraseña provisoria para una **habilitación** o para un **reseteo**. Los dos piden lo mismo y reciben lo mismo |
| Fuente de aleatoriedad del sistema | Sistema | Provee el material impredecible del que sale el valor |

El administrador y el alumno son sujetos de la regla: uno acciona el reseteo y comunica el valor, el otro lo usa una sola vez para cambiarlo.

## 3. Precondiciones

- El consumidor ya resolvió la facultad del administrador y el acotamiento de la operación a cuentas de alumno. **Este contrato no autoriza nada** y **no sabe si lo que la motivó fue una habilitación o un reseteo.**
- **No hace falta ningún dato de la cuenta**, y es deliberado: pedirlo abriría la puerta a derivar el valor de él. La invocación no lleva correo, ni nombre, ni identificador, ni fecha.

## 4. Flujo principal

1. El consumidor pide una contraseña provisoria.
2. Se toma material impredecible de la fuente de aleatoriedad del sistema.
3. Se compone el valor en claro con ese material y **con nada más**: ningún dato de la cuenta, ninguna marca de tiempo y ningún contador entran en su composición.
4. Se devuelve el valor **en claro, una sola vez**. Es lo que la superficie le muestra al administrador para que se lo comunique al alumno.
5. El valor derivado que el producto guarda **no lo produce este contrato**: el consumidor pasa el valor en claro por `CU-06006`, exactamente como con la contraseña que el alumno elige.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se pide una provisoria para una cuenta que **ya fue reseteada antes** | Se produce un valor **nuevo y distinto del anterior**. Este contrato no consulta el valor previo —no existe en ninguna parte— y no lo necesita: la distinción la sostiene la impredecibilidad, no la comparación | Paso 4 |
| FA-02 | Se piden dos provisorias para **dos cuentas distintas** | Se producen dos valores distintos, por el mismo motivo | Paso 4 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` | La fuente de material impredecible del sistema no responde | Termina de forma **degradada** y **no devuelve ningún valor**. Es la condición más importante de este contrato: la alternativa —componer el valor con lo que haya a mano, un contador o la fecha— produciría una provisoria **adivinable**, que es exactamente lo que RN-06014 prohíbe, y lo haría en silencio. **Un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa** |

**Es la única condición de este contrato.** No escribe nada —no persiste— y no devuelve ningún valor parcial.

## 7. Postcondiciones

- **Éxito:** el consumidor recibe un valor en claro con las dos propiedades. **Este contrato no lo guarda, no lo registra en ninguna traza y no lo vuelve a producir**: no hay forma de recuperarlo si se pierde, y si se pierde se resetea de nuevo.
- **Fallo:** el consumidor recibe el código y **ningún valor**. La operación que pidió el valor —la habilitación o el reseteo— no ocurre.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Dos cuentas de alumno distintas | Se piden tres provisorias: una para cada cuenta y otra vez para la primera | Las **3 son distintas entre sí**. Es la propiedad que `GeometriaFactory-Contracts` `CU-06008` **CA-10** verifica del lado del contrato, ejercida acá sobre el mecanismo |
| CA-02 | Cualquier cuenta | Se piden 1000 provisorias | **No hay ninguna repetida.** El criterio expresa como prueba lo que la regla enuncia como propiedad |
| CA-03 | Una cuenta con correo, nombre, apellido y fecha de alta conocidos | Se pide una provisoria | El valor devuelto **no contiene ni se deriva** del correo, del nombre, del apellido, del identificador ni de la fecha. La invocación **no recibió ninguno de esos datos**, que es la forma estructural de garantizarlo |
| CA-04 | Dos provisorias pedidas en el mismo instante observable | Se comparan | Son distintas: **el momento no interviene en la composición** |
| CA-05 | Una fuente de aleatoriedad simulada que no responde | Se pide una provisoria | Devuelve `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` y **0 valores**. En particular **no** devuelve un valor compuesto por otro medio |
| CA-06 | Una provisoria producida, con el registro del servidor y el almacén observados | Se completa el reseteo | El valor en claro **no aparece en el registro ni en el almacén**: lo que se guarda es su forma derivada, que produce `CU-06006` |
| CA-07 | Una provisoria ya entregada al administrador | Se pide volver a obtener **ese mismo** valor | No hay forma: este contrato no lo conserva. El camino declarado es **resetear de nuevo**, que produce un valor nuevo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00001, NB-00002 |
| Reglas de negocio aplicables | [**RN-02014**](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) —**es el contrato que la ejerce**—, [RN-02012](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [RN-02013](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) por el circuito en el que se inserta, y [RN-02015](../../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md) por contraste: el estado de la cuenta **no interviene** acá, y no porque se lo ignore sino porque no llega |
| Mecanismo que provee | La producción del valor de la contraseña provisoria, delegada explícitamente por `GeometriaFactory-Application` §6, por `GeometriaFactory-Contracts` `CU-06008` §10 y por `GeometriaFactory-Domain` |
| Consumidor | `GeometriaFactory-Application` [`CU-00024`](../../02-Especificacion-Funcional/Casos-De-Uso/CU-00024-Resetear-La-Contrasena-De-Un-Alumno.md), por la composición de raíz |
| Historias de usuario a generar en 06 | US-06019, US-06020 |
| Componentes esperados en 05 | Productor de contraseñas provisorias, con su fuente de aleatoriedad y su alfabeto |
| Tests previstos en 08 | Unitarias sin base de datos: tres provisorias distintas, mil sin repetición, ausencia de derivación de datos de la cuenta, fuente no disponible que **no** produce valor, y una inspección de que el valor en claro no llega al registro ni al almacén |

## 10. Notas y supuestos

- **Cómo se sostiene «no se repite»: es una decisión derivada de esta categoría y se declara como tal.** Ninguna fuente dice **cómo**. Hay dos lecturas posibles y esta categoría adopta la primera con su fundamento:
  1. **La no repetición la sostiene la impredecibilidad**: con material impredecible suficiente, dos valores iguales son un suceso que no ocurre en la vida del producto. Es la lectura adoptada.
  2. **La no repetición se verifica contra un registro de las provisorias anteriores.** Se descarta, y no por costo: exigiría **conservar las provisorias**, y el producto no guarda ninguna contraseña en claro. Una regla que existe para que la clave no quede circulando no se puede hacer cumplir guardándola.
  La decisión no es bloqueante y está registrada como punto abierto en `Especificacion-Funcional.md` §11, para que el Product Owner la confirme o la reemplace.
- **La longitud y el alfabeto del valor son de `05-Arquitectura-Tecnica`**, y esta categoría no los fija. Sí declara la tensión que 05 tiene que resolver, porque nace del uso declarado y no de una preferencia: **la provisoria se comunica de viva voz o por escrito, del docente al alumno**, de modo que tiene que ser transcribible sin ambigüedad, y a la vez tiene que quedar lejos de lo adivinable. Un alfabeto que evite caracteres que se confunden entre sí resuelve las dos cosas a la vez. **Es una derivación de esta categoría a partir de F-26, no una cita**: ninguna fuente declara longitud, alfabeto ni forma.
- **La provisoria no vence por tiempo.** El producto no tiene vencimiento de credencial y esta categoría no lo inventa. Lo que la vuelve provisoria es la **marca** que la habilitación (RN-06016) o el reseteo (RN-06014) dejan sobre la cuenta, y que sólo el cambio efectivo hecho por la propia cuenta levanta (INV-09). Por eso el vocabulario del producto prohíbe llamarla «contraseña temporal».
- **Este contrato no deriva ni guarda.** El valor pasa por `CU-06006` para derivarse y por `CU-06005` para guardarse, exactamente como la contraseña que el alumno elige. **La frontera es la misma y eso es lo que hace que no haga falta un puerto nuevo**, tal como `GeometriaFactory-Application` §8 lo declara al mantener sus puertos en cuatro.
- **El administrador no conoce la contraseña definitiva.** Ve la provisoria una vez, se la comunica y ahí termina su participación: la nueva la elige el alumno.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.4 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
| 1.2 | 2026-08-10 | **Absorbe `PRODUCT-INTAKE` 1.13 §4.1 (RN-06016) y la precisión de F-04**: habilitar una cuenta produce una contraseña provisoria con las mismas propiedades y el mismo tratamiento que el reseteo. **Este contrato gana un segundo consumidor y no cambia nada más**, y §1 declara por qué eso no es una omisión: la invocación **no lleva ningún dato de la cuenta ni del acto**, de modo que el valor no puede depender de cuál de los dos la pidió. **§1** amplía el propósito a la habilitación; **§2** declara los dos consumidores; **§3** precisa que este contrato no sabe qué operación lo motivó. **Ningún flujo, código de terminación, postcondición ni criterio de aceptación cambia**, y las dos propiedades exigidas al valor siguen siendo las mismas. El **nombre del documento se conserva** por estabilidad de citación. Sube minor. |
| 1.3 | 2026-08-10 | **Cierra el hallazgo `C-11` (P3) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0, contra `PRODUCT-INTAKE` 1.14.** Dos frases atribuían al **reseteo** un origen único que **RN-06016** dejó de tener: la postcondición de fallo de **§7** decía «El reseteo no ocurre» y pasa a decir que no ocurre **la operación que pidió el valor —la habilitación o el reseteo—**; y la nota de **§10** decía que la marca la deja «el reseteo» y pasa a nombrar a las **dos** operaciones que la ponen (RN-06016 y RN-06014, INV-09). El cuerpo normativo ya era correcto desde la emisión 1.2 —**§3** declara los dos consumidores y el mecanismo único, y la tabla de actores dice que los dos piden y reciben lo mismo—, de modo que lo que se corrige es letra de la sección de notas y no contenido. **El nombre del archivo y el del título se conservan** por estabilidad de citación, con el alcance ampliado declarado en §3, tal como el propio informe recomienda. Se corrige además el recuento de la delegación de **§3**, que decía «la única de las **quince** reglas sin tramo» y son **dieciséis** (`C-02`), y la cabecera pasa a citar el intake **1.14**. **Ninguna entrada, ninguna salida, ninguna condición de error y ningún criterio de aceptación cambia.** Sube minor. |

## 17. Compatibilidad de la superficie pública

Cambiar la longitud o el alfabeto del valor es compatible mientras las dos propiedades se conserven. **Componer el valor con cualquier dato de la cuenta o del momento, devolverlo dos veces, conservarlo, registrarlo, o producir un valor por un camino alternativo cuando la fuente de aleatoriedad no responde son cambios incompatibles** y suben versión mayor: los cinco vacían RN-06014, y el último la vacía **en silencio**, que es la peor forma.
