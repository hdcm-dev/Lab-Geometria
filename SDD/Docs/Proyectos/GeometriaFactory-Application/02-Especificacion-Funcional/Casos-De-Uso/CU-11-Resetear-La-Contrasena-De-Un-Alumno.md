# CU-11 — Resetear la contraseña de un alumno

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-11-Resetear-La-Contrasena-De-Un-Alumno.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5 (las operaciones del administrador sobre la cuenta); [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §5 (custodia de la credencial, laboratorio sin canal de correo); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.7**, §4 (F-26), §4.1 (RN-12, RN-13), §7 (CL-7 reescrito), §9 (X-2 retirada), §17.1.P.2 (INV-09), §17.2.P.5; orquesta [`CU-03` de GeometriaFactory-Domain](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

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

Orquestar el **reseteo de la contraseña de un alumno** por parte del administrador: reemplazar la credencial derivada de la cuenta por una **provisoria**, que el administrador fija y le comunica, y dejar la cuenta marcada como **con cambio de contraseña pendiente**, de modo que su próximo ingreso no llegue a ninguna otra parte del sistema hasta que la persona elija una contraseña nueva (RN-12, RN-13, INV-09).

Es la operación que cierra el agujero que el intake declaraba hasta 1.6: **hasta entonces el único remedio de un olvido de contraseña era dar de baja y volver a dar de alta, y la baja arrastra todos los trabajos** (RN-07). Acá **no se arrastra nada**: la cuenta conserva su estado de habilitación, su papel, su identidad y todos sus trabajos con sus estados y comentarios.

**Este caso de uso no deriva la contraseña ni la comunica.** Recibe el valor provisorio ya derivado y no conoce el valor en claro; el canal por el que el administrador se lo comunica al alumno es del aula y no del producto.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Solicita el reseteo aportando la identidad y el papel de quien lo pide, la cuenta destino y el valor provisorio **ya derivado** |
| Puerto de repositorio de cuentas | Sistema | Recupera la cuenta destino y materializa la credencial provisoria y la marca de cambio pendiente |
| Puerto de reloj del sistema | Sistema | Provee el sello de modificación de la cuenta, que es un metadato de orquestación de esta capa |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Admite o rechaza el reemplazo de la credencial derivada |

El administrador es el sujeto de la regla y el alumno la padece. La **verificación de facultad** se ejerce en esta capa y no ocultando un control en la pantalla.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien solicita la operación.
- El consumidor aporta el valor de la contraseña provisoria **ya derivado**. El valor en claro no atraviesa esta capa.
- La cuenta destino existe, tiene papel `Alumno`, está en estado `Habilitado` y tiene credencial derivada.

## 4. Flujo principal

1. El consumidor solicita el reseteo de la contraseña de una cuenta, declarando quién lo pide y aportando el valor provisorio derivado.
2. El caso de uso verifica que el papel de quien lo pide sea `Administrador` (RN-01). Si no lo es, termina en FA-01.
3. El caso de uso recupera la cuenta destino por el puerto de repositorio de cuentas.
4. El caso de uso verifica que la cuenta destino tenga papel `Alumno`. Si no lo tiene, termina en FA-02.
5. El caso de uso toma el sello de modificación del puerto de reloj e invoca el **reemplazo** de la credencial derivada en el dominio, declarando la verificación de la vigente **por facultad del administrador y no por conocimiento de la credencial vigente** (§10).
6. El caso de uso deja la cuenta marcada como **con cambio de contraseña pendiente** y materializa cuenta, credencial provisoria, marca y sello por el puerto de repositorio, en una única unidad de trabajo.
7. El caso de uso devuelve que procede, con el estado de la cuenta —que **no cambió**— y la marca puesta.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien solicita no tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, sin recuperar la cuenta destino ni tocar ninguna credencial | Termina el caso de uso |
| FA-02 | La cuenta destino tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`. El administrador administra su propia credencial por CU-03, en el camino de reemplazo (§10) | Termina el caso de uso |
| FA-03 | La cuenta destino ya está marcada como con cambio de contraseña pendiente | El reseteo **procede igual** y fija una provisoria nueva: es el caso del alumno que perdió también la provisoria antes de usarla. La marca ya puesta no se duplica ni se levanta | Paso 7 |
| FA-04 | La cuenta destino está habilitada y **todavía no tiene credencial derivada** | El caso de uso propaga el rechazo del dominio con el motivo `CREDENCIAL_NO_ESTABLECIDA`: no hay nada que resetear y el camino ya abierto es que el alumno la fije en su primer ingreso efectivo, por CU-03 FA-02 | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | Quien solicita no tiene papel `Administrador` | No recupera ni modifica nada. Es una negativa por facultad y no por pertenencia |
| `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` | La cuenta destino tiene papel `Administrador` | No toca la credencial ni pone marca alguna |
| `CUENTA_INEXISTENTE` | El puerto de repositorio no encuentra la cuenta destino | Termina sin efecto. **Acá no oculta nada**, porque la operación ya exigió la facultad de administrador |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | La cuenta destino está `Pendiente` o `Bloqueado` | Propaga el rechazo del dominio y conserva la credencial como estaba |
| `CREDENCIAL_NO_ESTABLECIDA` | La cuenta está habilitada y todavía no tiene credencial derivada | Propaga el rechazo: el camino correcto es la fijación que el alumno ejerce por CU-03 |
| `VALOR_DERIVADO_VACIO` | El valor provisorio aportado está vacío | Propaga el rechazo del dominio y conserva la credencial como estaba |

Ninguno deja efecto parcial: el reseteo escribe credencial, marca y sello, o no escribe nada.

## 7. Postcondiciones

- **Éxito:** la cuenta tiene la credencial provisoria como credencial derivada vigente, queda marcada como con cambio de contraseña pendiente y su sello de modificación es el del reloj. **Su estado de habilitación, su papel, su identidad y todos sus trabajos —con sus estados y sus comentarios— quedan exactamente como estaban** (RN-12).
- **Éxito, y es la mitad que importa:** mientras la marca esté puesta, la cuenta **no ejerce ninguna otra capacidad del sistema** (RN-13, INV-09). La marca la levanta **únicamente** el cambio efectivo que hace la propia cuenta, en CU-03.
- **Fallo:** la cuenta, su credencial, su marca y sus trabajos quedan exactamente como estaban.
- **En ningún caso** se retira un trabajo: el reseteo **no es una baja** y no dispara RN-07.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta `Habilitado` de `ana.perez@ejemplo.edu` con credencial derivada, **3 trabajos** —1 en `Borrador`, 1 en `Rechazado` y 1 en `Finalizado`, con sus comentarios— y un solicitante con papel `Administrador`; reloj fijado en 2026-03-20 | El consumidor solicita el reseteo con un valor provisorio derivado no vacío | El caso de uso devuelve que procede; la cuenta queda `Habilitado`, con la credencial provisoria, con la marca de cambio pendiente y sello 2026-03-20, y **sigue teniendo los 3 trabajos con sus estados y sus comentarios** |
| CA-02 | La misma cuenta ya reseteada | El consumidor invoca cualquier otro caso de uso en nombre de esa cuenta —listar sus trabajos, cargar uno, enviarlo— | Todos devuelven el motivo `CAMBIO_DE_CONTRASENA_PENDIENTE` y **ninguno lee ni escribe nada** (INV-09) |
| CA-03 | La misma cuenta ya reseteada | La propia cuenta reemplaza su credencial por CU-03 FA-03, declarando verificada la provisoria | La marca se levanta y los mismos casos de uso de CA-02 vuelven a proceder |
| CA-04 | Una cuenta y un solicitante con papel `Alumno` | El consumidor solicita el reseteo | El caso de uso devuelve `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y la credencial de la cuenta destino no cambia |
| CA-05 | La cuenta con papel `Administrador` | El administrador solicita resetearse a sí mismo | El caso de uso devuelve `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y no pone ninguna marca |
| CA-06 | Una cuenta `Bloqueado` con credencial derivada | El administrador solicita el reseteo | El caso de uso devuelve `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` y la credencial no cambia |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | [RN-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), **que ya tienen archivo propio en `GeometriaFactory-Domain`** (§10); [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-07](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-07-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) **por contraste**: el reseteo no la dispara |
| Casos de uso de dominio orquestados | [CU-03](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-03-Fijar-Y-Reemplazar-La-Credencial-Derivada.md), en su camino de reemplazo |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-29, US-30, US-31 |
| Componentes esperados en 05 | Caso de uso de reseteo de credencial; marca de cambio de contraseña pendiente en el puerto de repositorio de cuentas; guardia transversal de la marca |
| Tests previstos en 08 | Unitarias con dobles: reseteo que conserva los tres trabajos y el estado de habilitación; negativa por facultad; reseteo sobre la cuenta de administrador; reseteo sobre cuenta bloqueada y sobre cuenta sin credencial; valor provisorio vacío; y la batería de INV-09, que ejerce cada uno de los otros casos de uso con la marca puesta |

## 10. Notas y supuestos

- **[DECISIÓN DERIVADA] El reseteo está acotado a cuentas con papel `Alumno`.** El intake enuncia F-26 sobre los alumnos del administrador —«el administrador ve a todos sus alumnos y desde el mismo panel… les resetea la clave»— y no declara el reseteo de la cuenta de administrador. Se acota explícitamente por dos motivos: el administrador **ya tiene** camino propio para su credencial, el reemplazo de CU-03; y un reseteo sobre sí mismo lo dejaría confinado por INV-09, con el laboratorio sin gobierno hasta que cambie la clave y sin ninguna otra cuenta que pudiera resolverlo, porque RN-01 declara administrador único. **Fundamento, no fuente**: el intake no lo prohíbe, esta capa lo decide y lo rotula.
- **[DECISIÓN DERIVADA] El reseteo exige la cuenta en estado `Habilitado` y con credencial ya establecida.** El dominio sólo admite fijar o reemplazar sobre una cuenta habilitada, y RN-12 declara que el reseteo **conserva** el estado de habilitación, no que lo cambie: sobre una cuenta `Pendiente` o `Bloqueado` el remedio es habilitarla o rehabilitarla por CU-02, y sobre una cuenta sin credencial el camino ya abierto es la fijación del primer ingreso efectivo. Los dos motivos que se propagan **ya existen** en el catálogo de esta capa y no se inventa ninguno.
- **La declaración de credencial vigente verificada la aporta acá la facultad del administrador y no el conocimiento de la contraseña.** El dominio exige que el reemplazo declare verificada la vigente (CU-03 FA-04, motivo `CREDENCIAL_VIGENTE_NO_VERIFICADA`), y el administrador **no conoce la contraseña del alumno ni la conocerá**: lo que sostiene el reemplazo acá es la verificación de facultad de §4 del índice maestro, ejercida antes de invocar al dominio. Es la única invocación del reemplazo en la que la declaración no nace de una comparación de credenciales, y por eso se declara acá y no se deja inferir.
- **La contraseña nueva la elige el alumno y el administrador no la conoce** (RN-13). Lo único que el administrador conoce es la provisoria, y su vida útil termina en el primer ingreso: INV-09 es lo que hace que la provisoria sea provisoria.
- **El reseteo no es una baja.** No retira ningún trabajo, no exige confirmación escrita del correo y no toca RN-07. La confirmación que sí corresponde —para que el administrador no resetee por accidente la cuenta equivocada— es una decisión de presentación y vive en `03-UX-UI-DX` de `GeometriaFactory-Web`.
- **RN-12 y RN-13 ya tienen archivo en `GeometriaFactory-Domain`.** Entraron en el `PRODUCT-INTAKE` 1.7, esa categoría las redactó, y acá se referencian por enlace como las once anteriores. El punto abierto que el índice maestro §11 declaraba queda cerrado.
- **La exigencia de que la provisoria sea distinta de la contraseña nueva no está declarada aguas arriba** y esta categoría **no la inventa**. Si el producto la adopta, es una exigencia de forma de la contraseña y vive donde viven las demás: `05-Arquitectura-Tecnica` y la capa que compara credenciales.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, por la capacidad **F-26** del `PRODUCT-INTAKE` **1.7**, que retira la exclusión X-2 y reescribe el caso límite CL-7. Declara el contrato del reseteo con la verificación de facultad, el acotamiento a cuentas de alumno y a cuenta habilitada con credencial, la marca de cambio de contraseña pendiente de INV-09, la conservación íntegra de la cuenta y de sus trabajos de RN-12, los dos motivos nuevos y los cuatro propagados, y las dos decisiones derivadas con su fundamento. |
| 1.1 | 2026-08-09 | **Reconciliación con lo que `GeometriaFactory-Domain` ya emitió.** §9 y §10 declaraban que **RN-12 y RN-13 todavía no tenían archivo** en esa categoría y las citaban contra el intake; los dos archivos existen —`RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md` y `RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md`— y las citas pasan a ser enlaces, como las de las once anteriores. **Punto abierto cerrado**, y el índice maestro lo retira de su §11. Ningún flujo, motivo ni criterio de aceptación de este caso de uso cambia: la precisión de RN-13 en el `PRODUCT-INTAKE` 1.8 alcanza al ingreso de la cuenta reseteada, que es CU-03, y no al acto de resetear. |

## 17. Compatibilidad de la superficie pública

Agregar el reseteo al conjunto de operaciones del administrador es **compatible** con lo que había: no altera la semántica de las cuatro de CU-02 ni la de las dos de CU-03. Lo que **no** es compatible es levantar la marca de cambio pendiente por cualquier vía que no sea el cambio efectivo hecho por la propia cuenta, ni admitir que una cuenta marcada ejerza cualquier otra capacidad: las dos contradicen INV-09 y son cambios de alcance.
