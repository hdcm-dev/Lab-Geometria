# CU-04008 — Dar desenlace a un trabajo

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-04008-Dar-Desenlace-A-Un-Trabajo.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00009`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00009-Desenlace-Explicito-De-La-Entrega.md) §5 (cobertura de los desenlaces, facultad exclusiva del administrador, terminalidad, carácter opcional del comentario, devolución visible para el alumno); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-21, F-23), §4.1 (RN-04010, RN-04011), §4.2 (modelo de estados y sus tres consecuencias aceptadas), §6 (flujo 2.1), §7 (CL-10, CL-11), §17.2.P.5; orquesta [`CU-02010` de GeometriaFactory-Domain](CU-02010-Resolver-El-Desenlace-Del-Trabajo.md)
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

Orquestar la decisión del administrador sobre un trabajo en estado `Pendiente` —aprobarlo, que lo pasa a `Finalizado`, o rechazarlo, que lo pasa a `Rechazado`—, con un comentario escrito opcional, verificando que quien la ejerce tenga la facultad y dejando que el dominio haga cumplir la terminalidad de los dos estados de cierre. Es el contrato que convierte una entrega depositada en una entrega con respuesta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Solicita el desenlace declarando la identidad y el papel de quien lo decide |
| Puerto de repositorio de trabajos | Sistema | Recupera el trabajo y materializa el estado resultante y el comentario |
| Puerto de reloj del sistema | Sistema | Provee la fecha del desenlace |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Admite el desenlace sólo desde estado `Pendiente` y fija el estado terminal |

El administrador es el sujeto de la regla y el alumno el destinatario de la respuesta.

## 3. Precondiciones

- El consumidor aporta la identidad y el papel de quien decide.
- El desenlace pertenece al conjunto aprobar, rechazar.
- El trabajo existe.

## 4. Flujo principal

1. El consumidor solicita el desenlace de un trabajo, con o sin comentario.
2. El caso de uso verifica que el papel de quien decide sea `Administrador` (RN-04010). Si no lo es, termina en FA-01.
3. El caso de uso recupera el trabajo por el puerto de repositorio y comprueba que esté dentro del alcance del administrador, es decir que no esté en `Borrador` (RN-04011).
4. El caso de uso toma el sello del desenlace del puerto de reloj e invoca el desenlace en el dominio.
5. El dominio admite el desenlace desde estado `Pendiente`, fija el estado terminal y adopta el comentario si lo hay.
6. El caso de uso materializa el resultado en una única unidad de trabajo y devuelve el estado terminal.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Quien solicita no tiene papel `Administrador` | El caso de uso devuelve no procede con el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, sin tocar el trabajo. La facultad no se delega, ni siquiera sobre el trabajo propio | Termina el caso de uso |
| FA-02 | El desenlace llega **sin** comentario | Procede igual: el comentario es opcional en los dos desenlaces. El alumno ve el estado y sabe que no fue aceptado, aunque no tenga el motivo por escrito | Paso 4 |
| FA-03 | El trabajo ya tiene desenlace | El caso de uso propaga el rechazo del dominio con el motivo `TRANSICION_DESDE_ESTADO_TERMINAL`: no se corrige una aprobación ni se revisa un rechazo. Corregir un rechazo significa cargar un trabajo nuevo | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | El papel declarado no es `Administrador` | No recupera ni modifica el trabajo |
| `DESENLACE_FUERA_DE_PENDIENTE` | El trabajo no está en estado `Pendiente` | Propaga el rechazo del dominio y conserva el estado actual |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | El trabajo está en `Borrador` | Termina sin efecto: un borrador no se aprueba ni se rechaza, y el administrador ni siquiera lo ve |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | El trabajo ya está en `Finalizado` o en `Rechazado` | Propaga el rechazo del dominio: el trabajo no cambia de estado ni de contenido |
| `DESENLACE_DESCONOCIDO` | El desenlace no es aprobar ni rechazar | Termina sin tocar el trabajo |

Ninguno deja escritura parcial.

## 7. Postcondiciones

- **Éxito:** el trabajo está en `Finalizado` o en `Rechazado`, con la fecha del desenlace y con el comentario si lo hubo, y no admite ninguna transición posterior.
- **Fallo:** el trabajo queda exactamente como estaba, con su estado y su comentario anteriores.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un trabajo del alumno A en estado `Pendiente` y un reloj fijado en 2026-04-10 | El administrador lo aprueba con el comentario «Muy bien resuelto» | El caso de uso devuelve el trabajo en `Finalizado`, con fecha 2026-04-10 y el comentario «Muy bien resuelto» |
| CA-02 | Un trabajo del alumno A en estado `Pendiente` | El administrador lo rechaza sin comentario | El caso de uso devuelve el trabajo en `Rechazado` y sin comentario, y no exige ninguno |
| CA-03 | Un trabajo del alumno A en estado `Pendiente` | El alumno A intenta aprobarlo | El caso de uso devuelve el motivo `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y el trabajo sigue en estado `Pendiente` |
| CA-04 | Un trabajo del alumno A en `Rechazado` | El administrador intenta aprobarlo | El caso de uso devuelve el motivo `TRANSICION_DESDE_ESTADO_TERMINAL` y el trabajo sigue en `Rechazado` |
| CA-05 | Un trabajo del alumno A en `Borrador` | El administrador intenta aprobarlo | El caso de uso devuelve el motivo `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` y el trabajo sigue en `Borrador` |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-00009 |
| Reglas de negocio aplicables | [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02010](../Reglas-De-Negocio/RN-02010-Desenlace-Exclusivo-Del-Administrador-Y-Terminalidad.md), [RN-02011](../Reglas-De-Negocio/RN-02011-El-Administrador-No-Ve-Los-Borradores.md), [RN-02005](../Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) |
| Casos de uso de dominio orquestados | [CU-02010](CU-02010-Resolver-El-Desenlace-Del-Trabajo.md), [CU-02011](CU-02011-Resolver-El-Alcance-Del-Administrador-Sobre-Un-Trabajo.md) |
| Puertos que consume | Repositorio de trabajos, reloj del sistema |
| Historias de usuario a generar en 06 | US-04023, US-04024, US-04025 |
| Componentes esperados en 05 | Caso de uso de desenlace con su enumeración cerrada de dos decisiones y su comentario opcional |
| Tests previstos en 08 | Unitarias con dobles: aprobación con comentario, rechazo sin comentario, desenlace intentado por un alumno, desenlace sobre un estado terminal y sobre un borrador |

## 10. Notas y supuestos

- **La verificación de facultad se ejerce acá**, no en la pantalla: un alumno que fuerce la petición contra el servicio de datos tiene que ser rechazado igual, y eso es lo que este caso de uso hace verificable con dobles.
- Un trabajo en estado `Pendiente` que llega a `Pendiente` sin errores de validación es la precondición de todo desenlace; que sus advertencias se aprueben o se rechacen es una decisión del administrador y no del validador.
- **El comentario no es una observación ni una calificación**: lo escribe una persona, es texto libre, hay a lo sumo uno por trabajo y no lleva nota ni escala.
- El retiro de un trabajo con desenlace no es este caso de uso: la terminalidad impide cambiarlo, no eliminarlo, y la eliminación es CU-04009.
- **El sello del desenlace es un metadato de orquestación** que esta capa aporta al materializar, con el mismo carácter que los de CU-04004: el modelo del dominio no declara una fecha de desenlace y la discrepancia está elevada al Product Owner.
- **`DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` del dominio no llega a producirse.** Este caso de uso corta antes con su propia verificación de facultad, y la equivalencia entre los dos motivos está declarada en `Especificacion-Funcional.md` §4.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-07**: §9 suma RN-04001, que es la regla de la verificación de facultad que este caso de uso ejerce y que el índice maestro ya le asignaba. **H-06**: la fecha del desenlace pasa a llamarse **sello** y se declara metadato de orquestación en §10. **H-13**: §10 declara que `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR` del dominio no llega a producirse porque esta capa corta antes, y remite a la equivalencia del índice. |

## 17. Compatibilidad de la superficie pública

Hacer obligatorio el comentario, admitir un tercer desenlace o permitir una transición desde un estado terminal son cambios de alcance que contradicen RN-04010 y las consecuencias que el Product Owner aceptó al fijar el modelo de estados.
