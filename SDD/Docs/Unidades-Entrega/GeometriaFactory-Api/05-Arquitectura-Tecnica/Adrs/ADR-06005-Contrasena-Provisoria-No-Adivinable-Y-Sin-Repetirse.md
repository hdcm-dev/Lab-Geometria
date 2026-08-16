# ADR-06005 — La contraseña provisoria: no adivinable, sin repetirse y transcribible de viva voz

**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

`RN-06014` declara que **la contraseña provisoria la produce el sistema, no la escribe el administrador**, y le exige dos propiedades: **no es adivinable** y **no se repite** entre cuentas ni entre reseteos de la misma cuenta. `RN-06016` le suma un segundo consumidor —habilitar una cuenta produce la misma provisoria, con el mismo mecanismo— y con eso el producto pasa a tener **un solo** mecanismo de credencial inicial en lugar de dos.

Esta es la delegación más explícita de todo el corpus. `GeometriaFactory-Application` declara que `RN-06014` es **la única de las dieciséis reglas sin tramo en su capa**; `GeometriaFactory-Contracts` exige las propiedades del valor sin declarar mecanismo; `GeometriaFactory-Api` declara que el valor llega producido y sólo dice **lo que no hace con él**. Los tres apuntan acá.

Y acá la fuente se termina: **ninguna declara la longitud ni el alfabeto**. La categoría 02 dejó escrita la tensión que hay que resolver —el valor tiene que ser **transcribible de viva voz** porque el administrador se lo comunica al alumno en persona, y a la vez tiene que estar **lejos de lo adivinable**— y la derivó a esta categoría sin resolverla.

Motivación upstream: NB-00001, NB-00002; RN-06014, RN-06015, RN-06016; INV-09; `PRODUCT-INTAKE` §4 (F-26, F-04 precisada), §4.1, §17.1.P.5 · GeometriaFactory-Infrastructure.

## 2. Decisión

**La provisoria se produce íntegramente desde la fuente de material impredecible del sistema, con longitud fija de doce caracteres sobre un alfabeto de letras y dígitos del que se quitan los pares que se confunden al dictarlos.** Cinco reglas, y las cinco son exigibles:

1. **Ningún carácter proviene de otra fuente.** Ni del correo, ni del nombre, ni de la fecha, ni de un contador, ni de la identidad de la cuenta. La invocación **no recibe** ningún dato de la cuenta ni del acto que la motiva, de modo que no puede distinguir la habilitación del reseteo ni derivar el valor de nada.
2. **Longitud fija de doce caracteres**, sobre un alfabeto de letras mayúsculas, minúsculas y dígitos **sin los caracteres que se confunden al dictarlos** —el cero con la o mayúscula, el uno con la ele minúscula y la i mayúscula—. **Sin signos de puntuación**, porque su nombre hablado es ambiguo y su escritura depende de la disposición del teclado.
3. **Si la fuente de material impredecible no responde, no se produce valor.** Termina en `FUENTE_DE_ALEATORIEDAD_NO_DISPONIBLE` como terminación degradada, y el camino declarado es **volver a intentar el reseteo**, que produce un valor nuevo.
4. **El valor se devuelve una sola vez, al consumidor, y no se registra en ninguna traza.** No se guarda en claro: lo que se guarda es su valor derivado, por el mecanismo de [`ADR-06004`](ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md).
5. **La producción no recibe el estado de la cuenta.** Es la forma estructural de `RN-06015`: no puede comprobarlo, de modo que resetear no puede exigir que la cuenta esté habilitada.

**«No se repite» lo sostiene la impredecibilidad, no un registro de valores anteriores.** Esta ADR **hereda** la lectura que `CU-06007` §10 adoptó y no la reabre: conservar las provisorias anteriores para compararlas exigiría guardar contraseñas en claro, que es exactamente lo que el producto no hace. Lo que la longitud y el alfabeto compran es que esa lectura sea sostenible: con doce caracteres del alfabeto elegido, dos producciones consecutivas iguales no son un riesgo operativo.

**La longitud y el alfabeto son derivación de esta categoría, rotulada como tal.** Ninguna fuente los declara, y siguen elevados al Product Owner en `PA-06` de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §11 junto con el criterio de no repetición.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Doce caracteres de un alfabeto alfanumérico sin caracteres ambiguos (**adoptada**) | Resuelve la tensión declarada: se dicta sin deletrear cada carácter y a la vez es larga; ninguna de las dos propiedades de `RN-06014` depende de conservar nada | Es una decisión derivada que ninguna fuente respalda; el Product Owner puede quererla más corta |
| Palabras del idioma unidas, del estilo de las frases de paso | Muy fácil de dictar y de escribir | **Descartada.** Un diccionario reduce el espacio de valores de una forma que no se ve, y en un producto donde el administrador conoce la provisoria de todos sus alumnos la propiedad de no adivinable importa entre pares y no sólo frente a un extraño |
| Alfabeto con signos de puntuación | Más valores por carácter, con lo cual alcanza una longitud menor | **Descartada.** El canal declarado es la voz: el administrador se la comunica al alumno en persona. Un signo de puntuación se dicta mal, se escribe distinto según el teclado y termina en un ingreso fallido que parece un problema del sistema |
| Componer el valor con un dato de la cuenta y un sufijo aleatorio | Sería reconocible y fácil de asociar a su dueño en el momento de comunicarlo | **Descartada, y es el atajo que la categoría 03 declara explícitamente prohibido.** Vuelve la provisoria parcialmente derivable, que es lo que `RN-06014` prohíbe por escrito |
| Conservar un registro de provisorias emitidas para garantizar que no se repiten | Verificaría la propiedad literalmente | **Descartada por `CU-06007` §10 y no reabierta acá.** Exigiría guardar contraseñas en claro o un valor que las identifique, y crearía un objeto nuevo cuyo único propósito es sostener una prueba |
| Mecanismos distintos para la habilitación y para el reseteo | Cada uno podría ajustarse a su caso | **Descartada por `RN-06016`**: es el mismo mecanismo y el mismo valor, y la invocación no lleva ningún dato que permita distinguirlos |

## 5. Consecuencias positivas

1. `RN-06014` queda con sus dos propiedades sostenidas por construcción, y con una prueba directa para cada una.
2. `RN-06016` no agrega mecanismo: el segundo consumidor usa el mismo, y el producto tiene **un solo** camino de credencial inicial.
3. `RN-06015` queda sostenida estructuralmente: la producción no puede comprobar un estado que no recibe.
4. El canal real —la voz— queda contemplado en la decisión, con lo cual el modo de falla más probable de la operación deja de ser la transcripción.
5. El atajo más caro del producto queda cerrado con una condición propia y con su fundamento escrito, en lugar de con una recomendación.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que un reseteo pueda no completarse** cuando la fuente de material impredecible no responde. Es preferible por lejos: un reseteo que no se completa es recuperable; una provisoria adivinable no se nota hasta que alguien la usa.
2. **Se acepta que la propiedad de no repetición se sostenga por impredecibilidad y no por comprobación**, con la consecuencia de que la prueba correspondiente verifica que dos producciones consecutivas difieren, no que ninguna se repitió nunca.
3. **Se acepta que la longitud y el alfabeto sean decisión derivada**, y que el Product Owner pueda reemplazarlos. Si lo hace, cambia una constante y su prueba, no la forma del mecanismo.
4. **Se acepta perder los valores del alfabeto que se quitaron.** Doce caracteres compensan de sobra la reducción, y el costo de un carácter ambiguo se paga en cada comunicación.

## 7. Implementación

- El mecanismo de credenciales de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único lugar donde una provisoria existe.
- **Convención impuesta:** la operación de producción **no declara ningún parámetro**. Si recibiera uno, alguien terminaría derivando el valor de él.
- **Convención impuesta:** la provisoria no entra en ningún mensaje, en ningún registro ni en ninguna traza. Viaja en el resultado y en ningún otro lado.
- Quien la fija como credencial es la escritura de la cuenta, que además **pone la marca de cambio de contraseña pendiente**; la marca es atributo propio y **no es un estado de cuenta** (`RC-06007`).
- El único lugar donde la marca se levanta es el reemplazo de la propia credencial por la cuenta autenticada, y eso ocurre en la capa de aplicación, no acá.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Provisorias iguales en dos producciones consecutivas sobre la misma cuenta | Exactamente **0** | Prueba que produce dos y compara |
| Caracteres de la provisoria que provienen de un dato de la cuenta o del reloj | Exactamente **0** | Prueba que produce con la misma cuenta y con el reloj fijo, y comprueba que los valores difieren |
| Parámetros que la operación de producción recibe | Exactamente **0** | Inspección de la superficie |
| Caracteres ambiguos en el alfabeto | Exactamente **0** | Inspección del alfabeto declarado |
| Longitud de la provisoria | Exactamente **12** caracteres | Prueba sobre veinte producciones |
| Apariciones de una provisoria en un mensaje, un registro o una traza | Exactamente **0** | Prueba de inspección del registro del servidor tras un reseteo y una habilitación |
| Valores producidos cuando la fuente de material impredecible no responde | Exactamente **0** | Prueba con la fuente sustituida por un doble que falla |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4 (F-26 y F-04 precisada), §4.1 (RN-06014, RN-06015, RN-06016) y §17.1.P.5 · GeometriaFactory-Infrastructure.
- [`../../02-Especificacion-Funcional/Casos-De-Uso/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md`](../Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) §3 y §10.
- [`../../03-UX-UI-DX/DX-Error-Messages.md`](../../03-UX-UI-DX/DX-Error-Messages.md) §2.4 y §3.7.
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../_fusion/Application/Arquitectura-Proyecto-Codigo.md) §10.2, fila de RN-06014, que es la delegación explícita que esta ADR recibe.
- ADR relacionadas: [`ADR-06004`](ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. **Cierra el punto abierto de longitud y alfabeto que la categoría 02 derivó a esta categoría**, resolviendo la tensión declarada entre transcribible de viva voz y lejos de lo adivinable: doce caracteres alfanuméricos sin caracteres ambiguos, producidos íntegramente desde la fuente de material impredecible, sin parámetros de entrada y sin registro. Hereda sin reabrir la lectura de `CU-06007` §10 sobre cómo se sostiene la no repetición. Evalúa seis alternativas, declara cuatro trade-offs y fija siete métricas de validación. |
