# CU-02 — Gobernar las cuentas de la comisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-02-Gobernar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5 (admisión explícita, cobertura de las cuatro operaciones, protección de la operación destructiva, advertencia previa a la baja); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-03), §4.1 (RN-01, RN-06, RN-07), §7 (CL-6), §17.2.P.5; orquesta [`CU-02` de GeometriaFactory-Domain](../../CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md)
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

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca la operación aportando la identidad y el papel de quien la solicita |
| Puerto de repositorio de cuentas | Sistema | Recupera la cuenta y materializa el resultado de la transición o la baja |
| Puerto de repositorio de trabajos | Sistema | Retira los trabajos de la cuenta dada de baja |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Admite o rechaza la transición según la máquina de estados de la cuenta |

El administrador es el sujeto de la regla. La **verificación de facultad** se ejerce en esta capa y no ocultando un control en la pantalla.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien solicita la operación.
- La operación pertenece al conjunto habilitar, bloquear, rehabilitar, dar de baja.
- La cuenta destino existe.

## 4. Flujo principal

1. El consumidor solicita una operación sobre una cuenta, declarando quién la pide.
2. El caso de uso verifica que el papel de quien la pide sea `Administrador` (RN-01). Si no lo es, termina en FA-01.
3. El caso de uso recupera la cuenta destino por el puerto de repositorio de cuentas.
4. El caso de uso invoca la transición en el dominio, que la admite o la rechaza según la máquina de estados.
5. El caso de uso materializa el estado resultante por el puerto de repositorio, en una única unidad de trabajo.
6. El caso de uso devuelve el estado de cuenta resultante.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien solicita no tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, sin recuperar la cuenta destino ni evaluar la transición | Termina el caso de uso |
| FA-02 | La operación solicitada es dar de baja | El caso de uso exige que el consumidor aporte el correo escrito como confirmación y lo compara con el de la cuenta destino (RN-07). Si coinciden, retira por el puerto de repositorio de trabajos **todos** los trabajos de esa cuenta, cualquiera sea su estado, y recién después la cuenta, todo en la misma unidad de trabajo | Paso 6 |
| FA-03 | La baja se solicita sobre la cuenta con papel `Administrador` | El caso de uso propaga el rechazo del dominio con el motivo `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA`: la instancia quedaría sin administrador (RN-01, INV-05) | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | Quien solicita no tiene papel `Administrador` | No recupera ni modifica nada. Es una negativa por facultad y no por pertenencia: acá la existencia de la cuenta destino no se oculta, porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que no tiene |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | El correo escrito como confirmación no es el de la cuenta destino | No retira ningún trabajo ni la cuenta. La unidad de trabajo no se abre |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | El dominio rechaza el par estado actual y transición | Propaga el motivo y conserva el estado actual |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | Se pide dar de baja la cuenta con papel `Administrador` | Propaga el rechazo del dominio |
| `CUENTA_INEXISTENTE` | El puerto de repositorio no encuentra la cuenta destino | Termina sin efecto |

Ninguno deja efecto parcial: la baja escribe todo o no escribe nada.

## 7. Postcondiciones

- **Éxito, transición:** la cuenta queda en el estado resultante y su credencial derivada no cambia.
- **Éxito, baja:** no queda ninguna cuenta con ese correo ni ningún trabajo cuyo dueño fuera esa cuenta, en ningún estado.
- **Fallo:** la cuenta y sus trabajos quedan exactamente como estaban.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Una cuenta `Pendiente` de `ana.perez@ejemplo.edu` y un solicitante con papel `Administrador` | El consumidor solicita habilitarla | El caso de uso devuelve la cuenta en estado `Habilitado` |
| CA-02 | Una cuenta `Pendiente` y un solicitante con papel `Alumno` | El consumidor solicita habilitarla | El caso de uso devuelve el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y la cuenta no cambia de estado |
| CA-03 | Una cuenta de `ana.perez@ejemplo.edu` con 3 trabajos: 1 en `Borrador`, 1 en `Pendiente` y 1 en `Finalizado` | El administrador solicita la baja escribiendo `ana.perez@ejemplo.edu` como confirmación | El caso de uso retira la cuenta y los 3 trabajos, y el repositorio de trabajos queda con 0 trabajos de esa cuenta |
| CA-04 | La misma cuenta con sus 3 trabajos | El administrador solicita la baja escribiendo `ana.perez@ejemplo.com` como confirmación | El caso de uso devuelve el motivo `CONFIRMACION_DE_BAJA_NO_COINCIDE` y siguen existiendo la cuenta y sus 3 trabajos |
| CA-05 | La cuenta con papel `Administrador` | El administrador solicita darla de baja escribiendo su propio correo | El caso de uso devuelve el motivo `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01 |
| Reglas de negocio aplicables | [RN-01](../../../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-04](../../../Reglas-De-Negocio/RN-02004-Eliminacion-Acotada-Al-Borrador.md), [RN-06](../../../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), [RN-07](../../../Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) |
| Casos de uso de dominio orquestados | [CU-02](../../CU-02002-Gobernar-El-Ciclo-De-Vida-De-La-Cuenta.md) |
| Puertos que consume | Repositorio de cuentas, repositorio de trabajos |
| Historias de usuario a generar en 06 | US-04, US-05, US-06 |
| Componentes esperados en 05 | Caso de uso de gobierno de cuentas; contrato de retiro por dueño en el puerto de repositorio de trabajos |
| Tests previstos en 08 | Unitarias con dobles: las cuatro operaciones admitidas, la negativa por facultad, la confirmación que no coincide, el arrastre sobre los cuatro estados de trabajo y la baja rechazada del administrador |

## 10. Notas y supuestos

- **El arrastre de los trabajos es de esta capa**, porque exige recorrer el conjunto de trabajos de una cuenta y el dominio no ejecuta consultas. El dominio aporta el rechazo `BAJA_SIN_ARRASTRE_DE_TRABAJOS`, que rechaza toda baja que declare conservarlos; **este caso de uso no puede alcanzarlo por construcción**, porque el flujo alternativo FA-02 siempre declara el arrastre. Se nombra acá para que su ausencia en §6 no se lea como olvido.
- **Este caso de uso no consume el puerto de reloj.** Las cuatro operaciones cambian el estado de la cuenta y el modelo del dominio no declara una fecha de última modificación para esa entidad, de modo que no hay metadato de orquestación que registrar. Si el Product Owner resuelve incorporarla, este caso de uso pasa a consumir el reloj y la fila del puerto lo declara.
- La advertencia previa que le muestra al administrador qué se elimina es una decisión de presentación y vive en `03-UX-UI-DX`; acá vive la exigencia de la confirmación escrita.
- Una cuenta `Bloqueado` conserva sus trabajos: la baja es la única operación destructiva.
- La eliminación de **un** trabajo por parte del administrador no es este caso de uso: es CU-09.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-07**: §9 suma RN-04, que el índice maestro ya declaraba ejercida acá en el arrastre de la baja. **H-05**: §10 declara explícitamente que este caso de uso **no consume el puerto de reloj** y por qué, que es la lectura que el índice maestro atribuía mal. **H-14**: §10 nombra `BAJA_SIN_ARRASTRE_DE_TRABAJOS` y declara que es inalcanzable por construcción, en lugar de aludirlo sin nombrarlo. |

## 17. Compatibilidad de la superficie pública

Agregar una operación al conjunto es compatible mientras las cuatro existentes conserven su semántica. Quitar la confirmación escrita de la baja, o dejar de arrastrar los trabajos, contradicen RN-07 y son cambios de alcance.
