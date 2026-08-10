# ADR-03 — La credencial de sesión vive en el estado del circuito, y las rutas acotan sin hacer cumplir

**Proyecto de código:** GeometriaFactory-Web
**Documento:** ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior (AG-05)
**Categoría:** Seguridad

---

## 1. Contexto

Esta pieza recibe las credenciales del formulario y las canjea contra el servicio de datos. Lo que hay que decidir es **dónde queda la credencial de sesión resultante** y **qué hacen las rutas con el papel de quien entró**.

La topología ya resolvió la mitad: como la llamada al servicio de datos la hace el servidor de esta pieza ([`ADR-01`](ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md)), **el navegador no necesita la credencial para nada**. Lo único que necesita es poder decir «soy la misma persona que abrió este circuito».

La otra mitad es más delicada y la categoría 02 la dejó escrita: **la pieza pública no puede ser la última defensa de ninguna regla, porque el navegador no es confiable.** Ocultar un botón, no armar una ruta o no ofrecer una acción son decisiones de presentación legítimas y necesarias, pero **no hacen cumplir nada**. Confundir esas dos cosas es lo que produce productos donde forzar una solicitud sin pasar por la pantalla alcanza para saltarse una regla.

Hay un cuarto guardián que no es de papel sino de estado de cuenta: una cuenta con **cambio de contraseña pendiente** se autentica y **no obtiene sesión de trabajo**, y mientras la marca esté puesta no hay ninguna otra ruta a la que ir.

Motivación upstream: NB-01, NB-02; `RN-01`, `RN-03`, `RN-06`, `RN-11`, `RN-13`, `RN-16`; `INV-02`, `INV-03`, `INV-06`, `INV-09`; `PRODUCT-INTAKE` §17.6.P.4, §17.6.P.5 y §4.1; restricciones transversales `RT-02`, `RT-09` y `RT-12` de la categoría 02.

## 2. Decisión

**La credencial de sesión vive en el estado del circuito, del lado del servidor, y nunca llega al navegador.** El navegador conserva únicamente una marca de sesión que **no la transporta** y que no es legible por guion. Que la credencial no aparezca en el navegador es **criterio de aceptación verificable con las herramientas de desarrollo**, no una aspiración.

**El encaminamiento tiene cuatro guardianes**, y los cuatro **acotan lo que se ofrece sin hacer cumplir nada**:

1. **Aprovisionamiento resuelto.** Mientras no exista la cuenta de administrador, cualquier ruta pedida desvía al aprovisionamiento inicial; una vez que existe, esa ruta **deja de armar formulario para siempre** y desvía de forma neutra, sin explicar por qué.
2. **Sesión.** Ninguna ruta del panel es accesible sin sesión.
3. **Papel.** Un alumno con sesión no alcanza ninguna ruta de administrador, y **ninguna barra lateral muestra el destino del otro papel, ni siquiera deshabilitado**.
4. **Cambio de contraseña pendiente.** Con la marca puesta, la única ruta alcanzable es el cambio de la propia contraseña, y se llega **sin sesión de trabajo**, en el shell de acceso y sin barra lateral.

**La verificación de pertenencia y de papel la hace el servicio de datos en cada solicitud**, y esta pieza no la reemplaza. Varios criterios de aceptación de 02 lo verifican **forzando la solicitud sin pasar por la pantalla**, que es la única forma de comprobar que el acotamiento de acá no se estaba usando como si fuera una defensa.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Credencial en el estado del circuito, del lado del servidor (**adoptada**) | La credencial no está al alcance de ningún guion del navegador; no hay que decidir cuánto dura en el navegador ni cómo se revoca ahí; es verificable con las herramientas de desarrollo | Se pierde con el reciclado del proceso del hosting, y hay que volver a entrar |
| Credencial en almacenamiento del navegador | Sobreviviría al reciclado y evitaría re-autenticar | La pone al alcance de cualquier guion que se agregue después, en un producto donde ni siquiera los datos se guardan ahí. **Rompe el criterio de aceptación de que la credencial no aparece en el navegador.** Descartada por esta categoría |
| Credencial en una marca de sesión legible por guion | Simple de implementar y sobrevive a la recarga de la página | Es la misma exposición que la anterior con otra forma; además la haría viajar en cada pedido del navegador, incluidos los de recursos estáticos. Descartada por esta categoría |
| Confiar en los guardianes de ruta como control de acceso, sin verificación del otro lado | Menos ida y vuelta y menos código repetido | **El navegador no es confiable**: forzar la solicitud sin pasar por la pantalla saltearía todo. Es exactamente el defecto que 02 previno con sus criterios de aceptación. Descartada por `PRODUCT-INTAKE` §17.6.P.5 y por la categoría 02 §5 |

## 5. Consecuencias positivas

1. La credencial de sesión no tiene ninguna vía por la que salir al navegador, y eso se comprueba mirando, no argumentando.
2. `RA-01` y esta decisión se sostienen mutuamente: como la solicitud la arma el servidor, la credencial no necesita viajar; y como no viaja, no hay tentación de armar la solicitud en el navegador.
3. `INV-09` tiene su tratamiento de superficie: el cuarto guardián deja a la cuenta marcada sin ninguna otra ruta, y el shell de acceso —sin barra lateral— **dice lo mismo con la forma**, porque una barra lateral prometería destinos que no existen.
4. La distinción entre acotar y hacer cumplir queda escrita, de modo que 06 y 08 no pueden confundirlas al derivar historias y pruebas.
5. El aprovisionamiento inicial, que se usa una vez en la vida de la instancia, queda cerrado para siempre por un guardián y no por un cuidado.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que la sesión se pierda cuando el proceso del hosting recicla.** Es el costo directo de no guardar la credencial en el navegador, y se paga con el estado «sesión no restablecible» diseñado en 03.
2. **Se acepta que las mismas comprobaciones existan de los dos lados**: acá como acotación de lo que se ofrece, allá como verificación real. No es duplicación redundante: son dos cosas distintas con el mismo enunciado, y esta ADR las nombra distinto para que no se colapsen.
3. **Se acepta que el guardián de papel oculte destinos en lugar de mostrarlos deshabilitados.** Un control deshabilitado informa que existe algo que no se puede usar; acá la ruta **no se arma**, porque para el otro papel no existe.
4. **Se acepta que el desvío del aprovisionamiento ya resuelto sea neutro y no explique por qué.** Explicar revelaría que la instancia ya tiene administrador, que no es información que un anónimo necesite.

## 7. Implementación

- El componente **Sesión y estado del circuito** de [`../Arquitectura-Proyecto-Codigo.md`](../Arquitectura-Proyecto-Codigo.md) §3.1 es el único que custodia la credencial; el **Armazón y encaminamiento** es el único que aplica los cuatro guardianes.
- El **cliente tipado** adjunta la credencial del lado del servidor. Ninguna superficie la ve.
- Las credenciales en claro del canje, del cambio de contraseña y del reseteo **también** viajan servidor a servidor, según la regla de exposición que `GeometriaFactory-Contracts` declara para su frontera.
- El cuarto guardián se aplica **antes** que el de papel: una cuenta marcada no llega a ninguna ruta de su papel. Es la contraparte de superficie del orden fijo que `GeometriaFactory-Application` ejerce del otro lado.
- Verificación sugerida a 08: forzar la solicitud de un recurso ajeno y de una ruta del otro papel **sin pasar por la pantalla**, y comprobar que la negativa la produce el servicio de datos.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Apariciones de la credencial de sesión en el navegador | Exactamente **0** | Inspección del almacenamiento, de las marcas de sesión y del contenido servido, con las herramientas de desarrollo, en la etapa `c` |
| Guardianes de ruta aplicados | **4 de 4**, cada uno con al menos un recorrido que lo ejercita | Guion de demostración de la etapa correspondiente |
| Rutas del panel alcanzables sin sesión | Exactamente **0** | Recorrido pidiendo cada ruta sin sesión |
| Rutas de administrador alcanzables por un alumno con sesión | Exactamente **0** | Recorrido con sesión de alumno |
| Rutas alcanzables por una cuenta con la marca de cambio pendiente, distintas del cambio de su propia contraseña | Exactamente **0** | Recorrido con una cuenta recién habilitada y con una recién reseteada: **2 de 2** orígenes de la marca |
| Negativas producidas por esta pieza en lugar de por el servicio de datos, al forzar la solicitud sin pasar por la pantalla | Exactamente **0** | Prueba que fuerza la solicitud de un recurso ajeno y de una ruta del otro papel |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.16** §4.1 (`RN-13`, `RN-16`), §17.1.P.2 (`INV-09`), §17.6.P.4 y §17.6.P.5.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §2, §5 y §6 (`RT-02`, `RT-09`, `RT-12`).
- [`../../03-UX-UI-DX/Experiencia-De-Uso.md`](../../03-UX-UI-DX/Experiencia-De-Uso.md) §3.2, los dos shells y los tres destinos por papel.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md`](../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-04-Regla-De-Exposicion-De-La-Frontera.md), por la regla de exposición de la frontera y por el tratamiento de las credenciales en claro.
- ADR relacionadas: [`ADR-01`](ADR-01-Render-En-El-Servidor-Con-Circuito-Interactivo.md), [`ADR-02`](ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Registra la custodia de la credencial de sesión en el estado del circuito del lado del servidor y los cuatro guardianes de ruta, con la distinción explícita entre **acotar lo que se ofrece** y **hacer cumplir**, que es la decisión que la categoría 02 declaró y que esta ADR lleva al plano arquitectónico. Evalúa cuatro alternativas, declara cuatro trade-offs y fija seis métricas de validación, incluida la de forzar la solicitud sin pasar por la pantalla. |
