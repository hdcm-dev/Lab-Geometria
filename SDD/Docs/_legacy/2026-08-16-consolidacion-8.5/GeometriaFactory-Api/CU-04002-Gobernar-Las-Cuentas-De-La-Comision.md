# CU-04002 — Gobernar las cuentas de la comisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-04002-Gobernar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5 (admisión explícita, cobertura de las cuatro operaciones, protección de la operación destructiva, advertencia previa a la baja); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.13**, §4 (F-03, **F-04** precisada, F-26), §4.1 (RN-04001, RN-04006, RN-04007, RN-04012, RN-04013, RN-04014, **RN-04016**), §17.1.P.2 (**INV-09**), §7 (CL-6, CL-7 reescrito), §17.2.P.5; orquesta [`CU-02002`](CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) y la fijación de [`CU-02003`](CU-02003-Fijar-Y-Reemplazar-La-Credencial-Derivada.md) de GeometriaFactory-Domain
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

Orquestar las cuatro operaciones que el administrador ejerce sobre una cuenta de alumno —habilitar, bloquear, rehabilitar y dar de baja—, verificando en cada una que quien las pide tenga la facultad, y arrastrando en la baja todos los trabajos de esa cuenta dentro de la misma unidad de trabajo. Las cuatro forman un solo contrato porque son el mismo acto de admisión en cuatro momentos de la vida de la cuenta.

**Desde `PRODUCT-INTAKE` 1.13, habilitar y rehabilitar producen además la contraseña provisoria de la cuenta.** **RN-04016** declara que habilitar una cuenta produce una provisoria con el mismo mecanismo y el mismo tratamiento que el reseteo, la fija y deja la cuenta con **cambio de contraseña pendiente** (INV-09). Este caso de uso pasa entonces a **consumir el puerto de producción de la contraseña provisoria y el puerto de reloj** en esas dos operaciones, y a devolver el valor en claro **una sola vez** para que el administrador se lo comunique al alumno. Bloquear y dar de baja no cambian.

**La quinta operación que el administrador ejerce desde el mismo panel —el reseteo de la contraseña (F-26)— sigue sin ser de este caso de uso, y es CU-04011.** Lo que la separa ya no es el tratamiento de la provisoria, que desde 1.13 es el mismo, sino que **el reseteo no es una transición de la máquina de estados de la cuenta** (RN-04015) y que procede sobre las tres situaciones sin alterarlas. Se declara la frontera para que la ausencia no se lea como olvido.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca la operación aportando la identidad y el papel de quien la solicita |
| Puerto de repositorio de cuentas | Sistema | Recupera la cuenta y materializa el resultado de la transición o la baja |
| Puerto de repositorio de trabajos | Sistema | Retira los trabajos de la cuenta dada de baja |
| Puerto de producción de la contraseña provisoria | Sistema | **Produce el valor en claro** de la provisoria al habilitar o rehabilitar, no adivinable y sin repetirse (RN-04014, RN-04016). Es el mismo puerto que consume CU-04011 |
| Puerto de derivación de contraseña | Sistema | Deriva la provisoria antes de que el valor llegue al dominio: el dominio nunca la conoce en claro |
| Puerto de reloj del sistema | Sistema | Provee el sello de modificación de la cuenta cuando la operación escribe credencial |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Admite o rechaza la transición según la máquina de estados de la cuenta |

El administrador es el sujeto de la regla. La **verificación de facultad** se ejerce en esta capa y no ocultando un control en la pantalla.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien solicita la operación.
- La operación pertenece al conjunto habilitar, bloquear, rehabilitar, dar de baja.
- La cuenta destino existe.

## 4. Flujo principal

1. El consumidor solicita una operación sobre una cuenta, declarando quién la pide.
2. El caso de uso verifica que el papel de quien la pide sea `Administrador` (RN-04001). Si no lo es, termina en FA-01.
3. El caso de uso recupera la cuenta destino por el puerto de repositorio de cuentas.
4. Si la operación es **habilitar** o **rehabilitar**, el caso de uso pide la contraseña provisoria al puerto de producción, la deriva por el puerto de derivación y toma el sello del puerto de reloj (RN-04014, RN-04016).
5. El caso de uso invoca la transición en el dominio, aportando la credencial derivada cuando corresponde; el dominio la admite o la rechaza según la máquina de estados y, en la habilitación, fija la credencial y pone la marca de cambio de contraseña pendiente.
6. El caso de uso materializa el estado resultante —y, cuando corresponde, la credencial, la marca y el sello— por el puerto de repositorio, en una única unidad de trabajo.
7. El caso de uso devuelve el estado de cuenta resultante y, si la operación fue habilitar o rehabilitar, **el valor en claro de la provisoria**, una sola vez, para que el consumidor se lo muestre al administrador.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien solicita no tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, sin recuperar la cuenta destino ni evaluar la transición | Termina el caso de uso |
| FA-04 | La operación es **habilitar o rehabilitar** una cuenta que ya tiene la marca puesta por un reseteo anterior | El caso de uso procede igual: produce una provisoria nueva, la fija y **deja la marca puesta**. No hay estado intermedio que distinguir y la marca no se acumula | Paso 7 |
| FA-05 | La operación es **habilitar** una cuenta que ya está `Habilitado` | El dominio la trata como sin efecto y este caso de uso **no pide provisoria al puerto**: producir una sin que nadie la haya pedido dejaría al alumno fuera de su propia cuenta. Para eso está el reseteo de CU-04011, que es explícito | Paso 7 |
| FA-02 | La operación solicitada es dar de baja | El caso de uso exige que el consumidor aporte el correo escrito como confirmación y lo compara con el de la cuenta destino (RN-04007). Si coinciden, retira por el puerto de repositorio de trabajos **todos** los trabajos de esa cuenta, cualquiera sea su estado, y recién después la cuenta, todo en la misma unidad de trabajo | Paso 6 |
| FA-03 | La baja se solicita sobre la cuenta con papel `Administrador` | El caso de uso propaga el rechazo del dominio con el motivo `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`: la instancia quedaría sin administrador (RN-04001, INV-05) | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | Quien solicita no tiene papel `Administrador` | No recupera ni modifica nada. Es una negativa por facultad y no por pertenencia: acá la existencia de la cuenta destino no se oculta, porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que no tiene |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | El correo escrito como confirmación no es el de la cuenta destino | No retira ningún trabajo ni la cuenta. La unidad de trabajo no se abre |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | El dominio rechaza el par estado actual y transición | Propaga el motivo y conserva el estado actual |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | Se pide dar de baja la cuenta con papel `Administrador` | Propaga el rechazo del dominio |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | Se invoca la transición de habilitación en el dominio sin aportar la credencial derivada provisoria, porque el puerto de producción o el de derivación no la entregaron | Propaga el rechazo del dominio y conserva el estado actual. **No hay camino por el que una cuenta quede `Habilitado` sin credencial** (RN-04016) |
| `VALOR_DERIVADO_VACIO` | El valor derivado de la provisoria llegó vacío | Propaga el rechazo del dominio y conserva la credencial y el estado como estaban. Desde que la provisoria la produce el sistema, esta condición **no puede nacer de lo que escriba una persona** sino de un defecto de quien la produce |
| `CUENTA_INEXISTENTE` | El puerto de repositorio no encuentra la cuenta destino | Termina sin efecto |

Ninguno deja efecto parcial: la baja escribe todo o no escribe nada.

## 7. Postcondiciones

- **Éxito, habilitación o rehabilitación:** la cuenta queda `Habilitado`, con **credencial derivada nueva**, la **marca de cambio de contraseña pendiente** puesta y el sello del reloj; y el resultado transporta **el valor en claro de la provisoria, una sola vez**. Lo que se guarda es la forma derivada: el valor en claro **no se persiste en ninguna parte** y esta capa no lo conserva después de devolverlo.
- **Éxito, bloqueo:** la cuenta queda en el estado resultante y su credencial derivada y su marca no cambian.
- **Éxito, baja:** no queda ninguna cuenta con ese correo ni ningún trabajo cuyo dueño fuera esa cuenta, en ningún estado.
- **Fallo:** la cuenta y sus trabajos quedan exactamente como estaban.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta `Pendiente` de `ana.perez@ejemplo.edu` y un solicitante con papel `Administrador` | El consumidor solicita habilitarla | El caso de uso devuelve la cuenta en estado `Habilitado`, con **1 contraseña provisoria en claro**, la marca de cambio de contraseña pendiente puesta y **0 provisorias persistidas en claro** |
| CA-06 | Dos cuentas `Pendiente` distintas y un solicitante con papel `Administrador` | El consumidor habilita las dos, y después bloquea y rehabilita la primera | Las **3** provisorias devueltas son distintas entre sí, y el bloqueo devuelve **0** provisorias: sólo habilitar y rehabilitar producen una (RN-04014, RN-04016) |
| CA-07 | Una cuenta `Pendiente` y un puerto de producción de provisoria que falla | El consumidor solicita habilitarla | El caso de uso devuelve el motivo `HABILITACION_SIN_CREDENCIAL_PROVISORIA`, la cuenta sigue `Pendiente` y **0 cuentas** quedan `Habilitado` sin credencial derivada |
| CA-02 | Una cuenta `Pendiente` y un solicitante con papel `Alumno` | El consumidor solicita habilitarla | El caso de uso devuelve el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y la cuenta no cambia de estado |
| CA-03 | Una cuenta de `ana.perez@ejemplo.edu` con 3 trabajos: 1 en `Borrador`, 1 en `Pendiente` y 1 en `Finalizado` | El administrador solicita la baja escribiendo `ana.perez@ejemplo.edu` como confirmación | El caso de uso retira la cuenta y los 3 trabajos, y el repositorio de trabajos queda con 0 trabajos de esa cuenta |
| CA-04 | La misma cuenta con sus 3 trabajos | El administrador solicita la baja escribiendo `ana.perez@ejemplo.com` como confirmación | El caso de uso devuelve el motivo `CONFIRMACION_DE_BAJA_NO_COINCIDE` y siguen existiendo la cuenta y sus 3 trabajos |
| CA-05 | La cuenta con papel `Administrador` | El administrador solicita darla de baja escribiendo su propio correo | El caso de uso devuelve el motivo `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00001 |
| Reglas de negocio aplicables | [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02004](../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-02006](../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-02007](../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md), [RN-02014](../Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md), [RN-02016](../Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) |
| Casos de uso de dominio orquestados | [CU-02002](CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos, **producción de la contraseña provisoria**, **derivación de contraseña** y **reloj del sistema** |
| Historias de usuario a generar en 06 | US-04004, US-04005, US-04006 |
| Componentes esperados en 05 | Caso de uso de gobierno de cuentas; contrato de retiro por dueño en el puerto de repositorio de trabajos |
| Tests previstos en 08 | Unitarias con dobles: las cuatro operaciones admitidas, la negativa por facultad, la confirmación que no coincide, el arrastre sobre los cuatro estados de trabajo y la baja rechazada del administrador; más las de **RN-04016**: habilitación que devuelve provisoria y deja la marca (CA-01), tres provisorias distintas y ninguna en el bloqueo (CA-06), y habilitación sin provisoria disponible que se rechaza (CA-07) |

## 10. Notas y supuestos

- **La negativa por facultad de este caso de uso ya tiene código propio en el contrato.** `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` viajaba hasta hoy en el código genérico del conjunto cerrado, porque el único código de facultad estaba acotado al desenlace. El Product Owner incorporó `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` (`PRODUCT-INTAKE` **1.29** §17.4 P.3 y §17.2 P.5) y `GeometriaFactory-Contracts` lo emite. **Este caso de uso no cambia**: su motivo, su tabla de §6 y sus criterios de aceptación quedan como están, y lo que cambia es que aguas abajo la negativa deja de ser indistinguible de una falla.
- **El arrastre de los trabajos es de esta capa**, porque exige recorrer el conjunto de trabajos de una cuenta y el dominio no ejecuta consultas. El dominio aporta el rechazo `BAJA_SIN_ARRASTRE_DE_TRABAJOS`, que rechaza toda baja que declare conservarlos; **este caso de uso no puede alcanzarlo por construcción**, porque el flujo alternativo FA-02 siempre declara el arrastre. Se nombra acá para que su ausencia en §6 no se lea como olvido.
- **Este caso de uso consume el puerto de reloj desde la versión 1.2, y sólo en dos de sus cuatro operaciones.** Hasta `PRODUCT-INTAKE` 1.12 no lo consumía, con el fundamento que sigue: las cuatro operaciones cambiaban el estado de la cuenta y el modelo del dominio no declara una fecha de última modificación para esa entidad. **Habilitar y rehabilitar escriben ahora credencial** (RN-04016), y el sello de modificación es el mismo metadato de orquestación que CU-04003 y CU-04011 registran al escribirla. Bloquear y dar de baja siguen sin consumirlo. El fundamento original, para la parte que sigue valiendo: Las cuatro operaciones cambian el estado de la cuenta y el modelo del dominio no declara una fecha de última modificación para esa entidad, de modo que no hay metadato de orquestación que registrar. Si el Product Owner resuelve incorporarla, este caso de uso pasa a consumir el reloj y la fila del puerto lo declara.
- La advertencia previa que le muestra al administrador qué se elimina es una decisión de presentación y vive en `03-UX-UI-DX`; acá vive la exigencia de la confirmación escrita.
- Una cuenta `Bloqueado` conserva sus trabajos: la baja es la única operación destructiva.
- La eliminación de **un** trabajo por parte del administrador no es este caso de uso: es CU-04009.
- **La baja dejó de ser el remedio del olvido de contraseña.** Hasta el `PRODUCT-INTAKE` 1.6 el único camino declarado era dar de baja y volver a dar de alta, con el arrastre de FA-02 como consecuencia aceptada; desde 1.7 el remedio es el **reseteo** de CU-04011, que conserva la cuenta y todos sus trabajos (RN-04012, CL-7 reescrito, exclusión X-2 retirada). Quien lea FA-02 no debe seguir leyéndola como la salida de un olvido.
- **Dos de las cuatro operaciones ponen la marca de cambio de contraseña pendiente, y ninguna la levanta.** Habilitar y rehabilitar la ponen (**RN-04016**); bloquear la conserva tal cual estaba; la baja se lleva la cuenta entera, marca incluida. La levanta **únicamente** CU-04003 FA-05, el cambio efectivo hecho por la propia cuenta, y nada más (INV-09).

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-07**: §9 suma RN-04004, que el índice maestro ya declaraba ejercida acá en el arrastre de la baja. **H-05**: §10 declara explícitamente que este caso de uso **no consume el puerto de reloj** y por qué, que es la lectura que el índice maestro atribuía mal. **H-14**: §10 nombra `BAJA_SIN_ARRASTRE_DE_TRABAJOS` y declara que es inalcanzable por construcción, en lugar de aludirlo sin nombrarlo. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. **§1** declara la frontera con **CU-04011**: el reseteo de contraseña se ejerce desde el mismo panel del administrador pero **no es este caso de uso**, y se enumeran los cuatro rasgos que lo separan de las cuatro operaciones de acá. **§10** suma dos notas: la baja **dejó de ser el remedio del olvido de contraseña** —CL-7 reescrito y X-2 retirada—, de modo que FA-02 ya no se lee así; y las cuatro operaciones **no tocan la marca** de cambio de contraseña pendiente. Sube minor: declara una frontera nueva y corrige la lectura de un flujo alternativo, sin cambiar ningún flujo, motivo ni criterio de aceptación. |
| 1.2 | 2026-08-10 | **Propagación del `PRODUCT-INTAKE` 1.13**, regla **RN-04016** y precisión de **F-04**: habilitar una cuenta **produce su contraseña provisoria**, la fija y la deja con cambio de contraseña pendiente, con el mismo mecanismo y el mismo tratamiento que el reseteo. **§1** declara el cambio y reescribe la frontera con CU-04011, que ya no se sostiene en el tratamiento de la provisoria —es el mismo— sino en que el reseteo no es una transición de la máquina de estados (RN-04015). **§2** suma tres actores de sistema: el puerto de **producción de la contraseña provisoria**, el de **derivación** y el de **reloj**. **§4** suma el paso 4 y pasa a **siete** pasos, con el valor en claro devuelto una sola vez. **§5** suma **FA-04** —habilitar sobre una cuenta ya marcada— y **FA-05** —habilitar lo ya habilitado **no** produce provisoria nueva, porque dejaría al alumno fuera de su cuenta—. **§6** suma `HABILITACION_SIN_CREDENCIAL_PROVISORIA` y `VALOR_DERIVADO_VACIO`, y los motivos pasan de cinco a **siete**. **§7** parte la postcondición de éxito y declara que el valor en claro **no se persiste**. **§8** rehace CA-01 y suma **CA-06** y **CA-07**. **§9** suma RN-04014 y RN-04016, tres puertos y tres pruebas previstas. **§10** corrige las dos notas que quedaron falsas: la que decía que este caso de uso **no consume el puerto de reloj** —lo consume en dos de las cuatro operaciones— y la que decía que **ninguna de las cuatro toca la marca** —dos la ponen—. Sube minor. |
| 1.3 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |

## 17. Compatibilidad de la superficie pública

Agregar una operación al conjunto es compatible mientras las cuatro existentes conserven su semántica. Quitar la confirmación escrita de la baja, o dejar de arrastrar los trabajos, contradicen RN-04007 y son cambios de alcance.

El **reseteo** de CU-04011 no entra a este conjunto: agregarlo acá obligaría a un solo contrato a mezclar transiciones de estado con escritura de credencial, que es la fusión que §8 del índice maestro desaconseja con el mismo criterio con el que separó los dos caminos de alta.
