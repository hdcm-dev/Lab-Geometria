# RC-07 — La marca de cambio de contraseña pendiente no es un estado de cuenta, y el comentario no es una observación

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** RC-07-La-Marca-No-Es-Un-Estado-De-Cuenta.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`RN-12`](../../../../Reglas-De-Negocio/RN-02012-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md); [`RN-13`](../../../../Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md); [`RN-15`](../../../../Reglas-De-Negocio/RN-02015-Reseteo-Independiente-Del-Estado-De-Cuenta.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11** §17.1.P.2 (**INV-09**) y §17.3.P.4 («Ampliación del 2026-08-08 por el circuito de revisión»); `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`
**Trazabilidad downstream:** `05-Arquitectura-Tecnica` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

---

## 1. Enunciado

Dos atributos del modelo que no son lo que su vecino sugiere, y que por eso comparten regla:

1. **La marca de cambio de contraseña pendiente es un atributo propio de la cuenta y no un valor de su estado.** Convive con `Pendiente`, `Habilitado` y `Bloqueado` sin reemplazar a ninguno, y **el reseteo no cambia el estado de la cuenta**.
2. **El comentario del administrador es un campo del trabajo y no una entidad aparte.** Es texto libre y nulable, con la fecha y el identificador de quien lo dejó, **sin historial**: un trabajo recibe a lo sumo uno. **No se confunde con la observación**, que es lo que el validador emite sobre la geometría y que sí es entidad propia, con varias filas por trabajo.

## 2. Justificación

**Sobre la marca.** El reseteo procede sobre los tres estados de cuenta porque **opera sobre la credencial y no es una transición de la máquina de estados**. Si la marca fuera un cuarto estado, resetear una cuenta bloqueada la desbloquearía y resetear una pendiente la habilitaría: el administrador tendría que acordarse de una secuencia, que es justamente lo que la regla vino a evitar. Modelarla como atributo aparte es lo que permite que resetear y habilitar se hagan **en cualquier orden y terminen igual**.

**Sobre el comentario.** Un campo alcanza porque los dos estados de cierre son terminales: de `Finalizado` y de `Rechazado` no sale ninguna transición, de modo que no puede haber un segundo comentario que historiar. Y no es una observación porque lo escribe **una persona**, no lleva nota ni escala y no habla de la geometría.

## 3. Ámbito de aplicación

- La marca alcanza a la entidad **cuenta**. **La pone únicamente el reseteo del administrador y la levanta únicamente el cambio efectivo hecho por la propia cuenta**, que es lo que enuncia el invariante que la sostiene.
- El comentario alcanza a la entidad **trabajo**, en sus estados `Finalizado` y `Rechazado`.
- **El almacén no comprueba ninguna de las dos guardas.** Quién puede poner la marca, quién puede levantarla y desde qué estado se comenta son decisiones del dominio y de la capa de aplicación. Lo que el modelo de datos sostiene es que **existan como atributos separados**, que es la condición para que esas decisiones se puedan tomar.
- El **estado `Rechazado`** pertenece al conjunto cerrado de estados del trabajo desde la ampliación del circuito de revisión, junto con `Borrador`, `Pendiente` y `Finalizado`.

## 4. Consecuencia si se viola

Modelar la marca como un cuarto estado no produce rechazo: produce que un reseteo sobre una cuenta bloqueada la desbloquee, y con eso vacía la regla que declara el reseteo independiente del estado.

Modelar el comentario como entidad con historial tampoco produce rechazo: produce un lugar donde guardar varios comentarios para un trabajo que, por ser terminal, no puede recibir un segundo. Y lo peor: lo acerca a la observación, que es la confusión que el vocabulario del producto prohíbe expresamente.

## 5. CU afectados

- [`CU-05`](../../../../Casos-De-Uso/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) — Guardar y recuperar las cuentas: es donde la marca se escribe, se conserva y viaja.
- [`CU-03`](../../../../Casos-De-Uso/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) — Guardar y recuperar los trabajos: es donde el comentario vive como campo.
- [`CU-07`](../../../../Casos-De-Uso/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) — **por contexto**: es la mitad del reseteo que produce el valor, y la que explica por qué la provisoria es provisoria por la marca y no por un vencimiento.

## 6. Pruebas que la verifican

`CU-05` CA-04 —la marca puesta sobre una cuenta `Bloqueado`, que **queda `Bloqueado` y marcada**— y CA-05 —la marca que viaja en la recuperación—. Del lado del trabajo, `CU-04` CA-02, que verifica que el comentario del trabajo rechazado desaparece con la baja de la cuenta **junto con el trabajo**, porque es un campo suyo y no una entidad con vida propia.

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
