# CU-10 — Configurar la cuenta de administrador

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-10-Configurar-La-Cuenta-De-Administrador.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5 (unicidad de la cuenta de administrador); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-01), §4.1 (RN-01, RN-02), §15 (etapa `c`: configurar el administrador en el primer arranque, entrar, cambiar contraseña y salir), §17.2.P.2, §17.2.P.5, §17.2.P.11 punto 3; orquesta [`CU-12` de GeometriaFactory-Domain](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-12-Configurar-La-Cuenta-De-Administrador.md)
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

Orquestar la configuración de la **única** cuenta con papel `Administrador` de la instancia, en el primer arranque y sólo mientras no exista ninguna: comprobar sobre el conjunto de cuentas que no haya administrador y que el correo esté libre, tomar el sello de alta del puerto de reloj, pedirle al dominio que la constituya con su credencial derivada ya aportada, y materializarla.

Es el **segundo camino de alta** del producto. El otro es el auto-registro del alumno, que es [CU-01](CU-01-Registrar-El-Alta-De-Una-Cuenta.md) y deja la cuenta `Pendiente` y sin credencial. Los dos caminos tienen reglas opuestas, y por eso son dos contratos y no uno con un flujo alternativo.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca la configuración con los datos del primer arranque y la credencial ya derivada |
| Puerto de repositorio de cuentas | Sistema | Responde si ya existe una cuenta con papel `Administrador` y si el correo está libre, y materializa la cuenta constituida |
| Puerto de reloj del sistema | Sistema | Provee el sello de alta, para que sea verificable en prueba |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Constituye la cuenta con papel `Administrador`, estado `Habilitado` y credencial derivada adoptada |

El docente en su papel de administrador es el sujeto de la regla.

## 3. Precondiciones

- El consumidor aporta correo, nombre, apellido y la credencial **ya derivada**: la contraseña en claro no atraviesa esta capa.
- Los dos puertos que este caso de uso consume —repositorio de cuentas y reloj del sistema— están provistos por la composición de raíz.

## 4. Flujo principal

1. El consumidor solicita configurar la cuenta de administrador.
2. El caso de uso consulta al puerto de repositorio de cuentas si ya existe alguna cuenta con papel `Administrador` (RN-01, INV-05).
3. No existe ninguna: el caso de uso consulta si el correo aportado ya está registrado (RN-02, INV-01).
4. El correo está libre: el caso de uso toma el sello de alta del puerto de reloj.
5. El caso de uso invoca la configuración en el dominio, declarando la ausencia de administrador previo y la verificación de unicidad del correo, y aportando la credencial derivada.
6. El dominio fija el papel en `Administrador`, fija el estado en `Habilitado` y adopta la credencial derivada.
7. El caso de uso materializa la cuenta por el puerto de repositorio, en una única unidad de trabajo, y devuelve la cuenta configurada.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Ya existe una cuenta con papel `Administrador` | El caso de uso devuelve no procede con el motivo `ADMINISTRADOR_YA_CONFIGURADO`, sin consultar el correo ni escribir nada. La ventana de alta se cierra con la primera configuración y no vuelve a abrirse | Termina el caso de uso |
| FA-02 | El administrador cambia su contraseña inmediatamente después de entrar, como pide el guion de la etapa `c` | No es este caso de uso: es el reemplazo de CU-03, que exige la credencial vigente verificada. Acá la credencial nace fijada, de modo que el camino de fijación por primera vez **no aplica** a esta cuenta | Termina el caso de uso; sigue CU-03 |
| FA-03 | El puerto de repositorio rechaza la materialización por una colisión que las consultas de los pasos 2 y 3 no vieron | El caso de uso no materializa nada y devuelve el motivo correspondiente. Las comprobaciones previas no son la garantía por sí solas | Termina el caso de uso |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `ADMINISTRADOR_YA_CONFIGURADO` | Ya existe una cuenta con papel `Administrador` | No constituye nada. Es el motivo que el dominio también devuelve si la ausencia no se le declara |
| `CORREO_YA_REGISTRADO` | El correo aportado ya pertenece a una cuenta | No constituye nada y no informa el papel ni el estado de la cuenta que lo ocupa |
| `DATO_OBLIGATORIO_AUSENTE` | El dominio rechaza la constitución por correo, nombre o apellido vacío | Propaga el motivo del dominio. No hay efecto parcial |
| `CONFIGURACION_SIN_CREDENCIAL` | No se aporta credencial derivada, o el valor está vacío | Propaga el rechazo del dominio: una cuenta de administrador sin credencial no podría entrar, y no hay ninguna otra que pudiera resolverlo |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | El consumidor pide constituirla en un estado distinto de `Habilitado` | Propaga el rechazo del dominio. En este camino el estado inicial no se elige, igual que en el auto-registro, y es el opuesto |

Los cinco terminan de forma controlada y sin escritura: la unidad de trabajo no se abre hasta el paso 7.

## 7. Postcondiciones

- **Éxito:** existe una cuenta con papel `Administrador`, estado `Habilitado`, credencial derivada con valor, sello de alta y ningún trabajo. La consulta de admisibilidad de CU-03 la devuelve admisible sin ningún motivo pendiente, de modo que el guion del primer arranque —configurar, entrar, cambiar contraseña y salir— es recorrible de punta a punta.
- **Fallo:** no se constituye ninguna cuenta y la instancia sigue sin administrador, de modo que este mismo caso de uso vuelve a estar disponible.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un repositorio de cuentas sin ninguna cuenta con papel `Administrador` y un reloj fijado en 2026-03-01 | El consumidor solicita configurar `docente@ejemplo.edu`, «Fernando», «Filipuzzi», con una credencial derivada no vacía | El caso de uso devuelve la cuenta con papel `Administrador`, estado `Habilitado`, credencial derivada con valor, sello de alta 2026-03-01 y 0 trabajos |
| CA-02 | La cuenta configurada por CA-01 | El consumidor consulta su admisibilidad por CU-03 | El caso de uso devuelve admisible, con 0 motivos: el administrador entra inmediatamente después de configurarse |
| CA-03 | Un repositorio con 1 cuenta con papel `Administrador` | El consumidor solicita configurar `otro.docente@ejemplo.edu` | El caso de uso devuelve el motivo `ADMINISTRADOR_YA_CONFIGURADO` y el repositorio sigue con 1 sola cuenta con ese papel |
| CA-04 | Un repositorio sin administrador y un reloj fijado en 2026-03-01 | El consumidor solicita la configuración sin credencial derivada | El caso de uso devuelve el motivo `CONFIGURACION_SIN_CREDENCIAL` y el repositorio sigue sin administrador |
| CA-05 | Un repositorio sin administrador pero con una cuenta cuyo correo es `docente@ejemplo.edu` | El consumidor solicita configurar `docente@ejemplo.edu` | El caso de uso devuelve el motivo `CORREO_YA_REGISTRADO` y no constituye nada |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, en su criterio de unicidad de la cuenta de administrador |
| Reglas de negocio aplicables | [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-02](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) |
| Casos de uso de dominio orquestados | [CU-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-12-Configurar-La-Cuenta-De-Administrador.md) |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-03, US-28 |
| Componentes esperados en 05 | Caso de uso de configuración con su comprobación de ventana de alta sobre el conjunto de cuentas |
| Tests previstos en 08 | Unitarias con repositorio simulado: configuración exitosa, segunda configuración rechazada, correo ocupado, credencial ausente, y la prueba de recorrido del primer arranque que encadena la configuración con la consulta de admisibilidad |

## 10. Notas y supuestos

- **Por qué esta cuenta nace `Habilitado` y la del alumno no.** Las fuentes atan el estado inicial `Pendiente` al acto de auto-registro del alumno, no a toda alta de cuenta. Si esta cuenta naciera con el estado `Pendiente`, la única transición que la sacaría de ahí es que un administrador la habilite, y no hay ninguno: la instancia quedaría inutilizable en el primer arranque. El fundamento lo declara el dominio y esta capa no lo redacta de nuevo.
- **RN-01 e INV-05 no fundamentan el estado inicial**: declaran la unicidad del administrador y la ventana en la que su alta es posible. Se citan por eso.
- **La ventana de alta se comprueba acá**, porque exige conocer el conjunto de cuentas, y se le declara al dominio al invocar. Es la misma división de trabajo que la unicidad del correo, y el dominio la protege con dos rechazos propios: `ADMINISTRADOR_YA_CONFIGURADO`, que §6 propaga, y `UNICIDAD_DE_CORREO_NO_VERIFICADA`, que **este caso de uso no puede alcanzar por construcción**, porque el paso 5 declara siempre la verificación que el paso 3 hizo. Se nombra acá, igual que en CU-01 §10, para que su ausencia en §6 no se lea como olvido: es inalcanzable en **los dos caminos de alta**.
- **El sello de alta es un metadato de orquestación** que esta capa aporta al materializar, distinto de la «Fecha» que el alumno declara en su trabajo.
- La derivación de la contraseña no es de esta capa: la credencial llega derivada. La emisión del acceso tampoco.
- La cuenta de administrador **no admite baja**, y por eso este contrato se ejerce una sola vez en la vida de la instancia.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial, **derivada de la corrección de la ronda r1 del audit, hallazgo H-01**. Nace al partirse CU-01 en los dos caminos de alta que `GeometriaFactory-Domain` separó entre su CU-01 y su CU-12: el auto-registro del alumno y la configuración del administrador, con estado inicial, credencial y ventana de alta opuestos. Recoge los tres motivos que el dominio dio de alta —`ADMINISTRADOR_YA_CONFIGURADO`, `CONFIGURACION_SIN_CREDENCIAL` y el `ESTADO_INICIAL_NO_NEGOCIABLE` acotado a este camino— y el criterio de recorrido del primer arranque, que encadena la configuración con la admisibilidad. |

## 17. Compatibilidad de la superficie pública

Admitir una segunda configuración, constituir esta cuenta sin credencial o dejarla en un estado que no admite acceso son cambios que contradicen RN-01 y el guion del primer arranque, y exigen decisión del Product Owner.
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-17**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. §10 **nombra** `UNICIDAD_DE_CORREO_NO_VERIFICADA` y declara que es inalcanzable por construcción también en este camino de alta, donde antes se lo aludía sin nombrarlo. La declaración de inalcanzabilidad queda ahora en los dos caminos, que es donde el rechazo del dominio se evita. |
