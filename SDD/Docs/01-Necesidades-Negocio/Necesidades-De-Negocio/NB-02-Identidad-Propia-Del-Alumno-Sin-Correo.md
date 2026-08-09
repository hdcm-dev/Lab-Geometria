# NB-02 — Identidad propia del alumno sin canal de correo

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md |
| Versión | 1.1 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (promesa central: el trabajo tiene dueño), §4 (capacidades F-02, F-04 y F-05), §4.1 (regla RN-06), §4.2 (colisión de vocabulario de `Pendiente`), §6 (flujo 1), §9 (exclusiones X-1 y X-2), §11 (riesgo RN-B6); `Vision-Producto.md` §1, §3 y §8; `Alcance-Producto.md` §4.1 y §5 |
| Trazabilidad downstream | CU-04, CU-05, CU-06, CU-07 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

El trabajo del alumno no tiene hoy dueño. La cadena actual termina en un texto que vive en un portapapeles: no queda guardado, no tiene propietario y nadie puede afirmar de quién es (PRODUCT-INTAKE §1). La promesa central del producto es que ese trabajo quede guardado, tenga dueño, tenga estado y se entregue (PRODUCT-INTAKE §3), y el dueño requiere que el alumno tenga una identidad propia dentro del laboratorio: una cuenta que le pertenezca y una credencial que sólo él conozca.

La dificultad concreta es que el producto no dispone de canal de correo, por decisión declarada del Product Owner (PRODUCT-INTAKE §9, exclusiones X-1 y X-2). Todo el patrón habitual de alta —enviar una contraseña provisoria, confirmar una dirección, recuperar una clave olvidada— queda descartado. La necesidad, entonces, no es «tener cuentas», sino que el alumno llegue a tener una credencial propia **sin que ninguna credencial se transporte nunca**: el alumno se registra sin elegir contraseña y la establece él mismo en su primer ingreso efectivo, ya habilitado.

Esa forma de resolverlo tiene una consecuencia declarada y aceptada: sin correo no hay canal de recuperación, y un alumno que olvida su contraseña sólo se recupera por intervención del administrador, que da de baja la cuenta y la vuelve a dar de alta, perdiendo los trabajos que esa cuenta tenía (PRODUCT-INTAKE §11, RN-B6). La necesidad incluye, por eso, que el alumno pueda cambiar su contraseña cuando quiera presentando la vigente, que es la única forma que tiene de gestionar su credencial dentro del producto.

## 2. Ejemplo de uso desde la perspectiva del negocio

Una alumna entra por primera vez al laboratorio y se registra con su correo, su nombre y su apellido. No le piden ninguna contraseña. El producto le avisa que su cuenta quedó en estado `Pendiente`, a la espera de autorización. Ese mismo día intenta ingresar y el producto le dice, con todas las letras, que su cuenta `Pendiente` todavía no fue habilitada: no le devuelve un error confuso ni la deja pensando que se equivocó de dirección. Al día siguiente el docente la habilita. Ella vuelve a intentar ingresar y esta vez el producto le pide que establezca su contraseña. La establece, entra y ve su panel de trabajos vacío, listo para cargar el primero. En ningún momento recibió un correo del laboratorio, ni tuvo que esperar uno.

## 3. Impacto

- Si se resuelve: el trabajo pasa a tener dueño, que es la condición previa a cualquier entrega, a cualquier listado propio y a cualquier revisión del docente.
- Si se resuelve: el alumno recibe siempre una explicación de su situación —pendiente, habilitado o bloqueado— en lugar de un rechazo sin causa.
- Si se resuelve: la ausencia de correo deja de ser una carencia y pasa a ser una propiedad del circuito, porque ninguna credencial viaja por ningún canal.
- Si queda sin resolver: el producto no puede cumplir su promesa central, porque un trabajo sin dueño es exactamente el problema que se viene a resolver.
- Si queda sin resolver: no hay forma de separar los trabajos de un alumno de los de otro, y por lo tanto tampoco de revisarlos por alumno.
- Riesgo residual aceptado: sin canal de recuperación, olvidar la contraseña cuesta la cuenta y sus trabajos (PRODUCT-INTAKE §11, RN-B6).

## 4. Problema específico que resuelve

- El texto que produce el alumno no puede atribuirse hoy a nadie.
- Sin canal de correo, el alumno no tiene forma de recibir una credencial inicial.
- Un alumno que todavía no fue autorizado no sabe si el problema es su cuenta, su clave o la herramienta.
- No existe forma de que el alumno cambie su credencial cuando sospecha que dejó de ser secreta.
- Una credencial que se enviara por algún canal quedaría expuesta fuera del producto.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Circuito de alta sin correo | Correos que el producto envía en todo el ciclo de registro, habilitación y primer ingreso de un alumno | 0 | Punto de control de la etapa `d`, y en cada punto de control posterior por la regla de no regresión |
| Alta de punta a punta | Recorridos completos del guion de alta —registro, habilitación, establecimiento de contraseña y acceso al panel— que terminan con el alumno dentro de su panel de trabajos, sobre los recorridos ejecutados en el punto de control | 1 de 1 | Punto de control de la etapa `d` |
| Explicación al alumno no habilitado | Intentos de ingreso de una cuenta `Pendiente` que reciben aviso explícito de que la cuenta todavía no fue habilitada, sobre el total de intentos | 100 % | Punto de control de la etapa `d` |
| Custodia de la credencial vigente | Cambios de contraseña aceptados sin que se presente la contraseña vigente | 0 | Punto de control de la etapa `c`, y verificado sobre cuentas de alumno en la etapa `d` |
| Credencial no observable | Credenciales de sesión observables desde el navegador de la persona | 0 | Punto de control de la etapa `c` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §9 (X-1) y §4 (F-04); el segundo y el tercero, de PRODUCT-INTAKE §6 (flujo 1) y de la transición `d` a `e` de `Roadmap-Producto.md` §5.2; el cuarto, de PRODUCT-INTAKE §4 (F-05) y de la transición `c` a `d` del mismo roadmap; el quinto, de la misma transición `c` a `d`. Ninguno depende de la asunción A-2 del intake.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Decidió el circuito sin correo y aceptó por escrito su consecuencia sobre la recuperación de contraseña; valida el punto de control de la etapa `d` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que el trabajo entregado sea atribuible a un alumno concreto de la comisión |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye el circuito de identidad y lo demuestra de punta a punta en el punto de control |
| Alumno de la comisión | Beneficiario | Obtiene una cuenta propia y una credencial que elige él mismo, y sabe siempre en qué situación está su cuenta |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Es quien habilita la cuenta y quien resuelve, dando de baja y volviendo a dar de alta, el caso del alumno que olvidó su contraseña |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-02 | CU-04 registrar una cuenta de alumno con correo, nombre y apellido | a generar |
| NB-02 | CU-05 establecer la contraseña en el primer ingreso efectivo | a generar |
| NB-02 | CU-06 iniciar y cerrar sesión | a generar |
| NB-02 | CU-07 cambiar la contraseña exigiendo la vigente | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-01, porque la habilitación de una cuenta de alumno la ejerce el administrador, que existe recién cuando NB-01 está resuelta.
- Es prerequisito de: NB-03, porque un trabajo sólo tiene dueño si el alumno tiene identidad propia.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: las tres capacidades que esta NB agrupa —F-02, F-04 y F-05— están declaradas Must Have, todas con la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de identidad propia del alumno sin canal de correo a partir de las capacidades F-02, F-04 y F-05 del intake, con cinco criterios de éxito trazados a su sección de origen y cuatro casos de uso previstos. |
| 1.1 | 2026-08-08 | Califica la forma desnuda de `Pendiente` en §2 —dos ocurrencias— y en el tercer criterio de §5 —dos ocurrencias—, según la entrada de forma calificada obligatoria que `Vision-Producto.md` §9.2 incorporó al declarar el modelo de estados del trabajo de `PRODUCT-INTAKE` 1.3: el término nombra a la vez un estado de cuenta y uno de trabajo, y esta NB usa el primero. La cabecera suma RN-06 y §4.2 como origen. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Ningún criterio, target, prioridad ni trazabilidad cambia: es una calificación léxica por ocurrencia. |
