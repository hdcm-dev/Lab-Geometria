# CU-01 — Registrar el alta de una cuenta

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** CU-01-Registrar-El-Alta-De-Una-Cuenta.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md) §5 (circuito de alta sin correo, alta de punta a punta); [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md) §5 (admisión explícita); `00-Contexto/Vision-Producto.md` §9.1; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-02), §4.1 (RN-02), §6 (flujo 1), §17.2.P.2 y §17.2.P.11 punto 3 (el reloj es un puerto); orquesta [`CU-01` de GeometriaFactory-Domain](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md)
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

Orquestar el **auto-registro de un alumno**: verificar sobre el conjunto de cuentas que el correo esté libre, tomar el sello de alta del puerto de reloj, pedirle al dominio que constituya la cuenta y entregarla al puerto de repositorio para que se materialice. Es el contrato que el consumidor invoca cuando una persona se registra en el laboratorio.

**Este caso de uso no constituye la cuenta del administrador.** El producto tiene **dos caminos de alta** con reglas opuestas —estado inicial, credencial y ventana de alta—, y el dominio los declara en dos casos de uso distintos: el auto-registro es su CU-01 y la configuración del administrador es su CU-12. Esta capa espeja esa partición: la configuración del administrador es [CU-10](CU-10-Configurar-La-Cuenta-De-Administrador.md).

Este caso de uso **no fija credencial**. Desde **RN-16** la credencial inicial la produce el sistema **al habilitar la cuenta** (CU-02), no el alumno al entrar: el alumno recibe una contraseña provisoria y la **cambia** en su primer ingreso efectivo, que es CU-03. La cuenta nace `Pendiente` y sin credencial derivada.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor de los casos de uso (`GeometriaFactory-Api`) | Primario | Invoca el alta con los datos que recibió del registro |
| Puerto de repositorio de cuentas | Sistema | Responde si el correo está libre y materializa la cuenta constituida |
| Puerto de reloj del sistema | Sistema | Provee el sello de alta, para que sea verificable en prueba |
| Modelo de dominio (`GeometriaFactory-Domain`) | Sistema | Constituye la cuenta, verifica los datos obligatorios y fija el papel y el estado inicial del auto-registro |

El alumno es **sujeto de la regla**, no actor: quien ejerce el contrato es el código consumidor.

## 3. Precondiciones

- El consumidor aporta correo, nombre y apellido.
- El consumidor **no** aporta credencial: el auto-registro no incluye contraseña.
- Los dos puertos que este caso de uso consume —repositorio de cuentas y reloj del sistema— están provistos por la composición de raíz.

## 4. Flujo principal

1. El consumidor solicita el alta con el correo, el nombre y el apellido.
2. El caso de uso consulta al puerto de repositorio de cuentas si ese correo ya está registrado (RN-02, INV-01).
3. El correo está libre: el caso de uso toma el sello de alta del puerto de reloj.
4. El caso de uso invoca la constitución del alumno en el dominio, declarando que la unicidad del correo fue verificada y sin aportar credencial ni estado.
5. El dominio fija el papel en `Alumno`, fija el estado inicial del auto-registro en `Pendiente` y devuelve la cuenta constituida sin credencial derivada.
6. El caso de uso entrega la cuenta al puerto de repositorio dentro de una única unidad de trabajo y devuelve el resultado, que informa que la cuenta quedó pendiente de habilitación.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El consumidor pide constituir por esta vía una cuenta con papel `Administrador` | El caso de uso propaga el rechazo del dominio con el motivo `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` y remite a CU-10. **No hay flujo de administrador en este contrato**: sus reglas son opuestas a las del auto-registro | Termina el caso de uso |
| FA-02 | El puerto de repositorio rechaza la materialización por una colisión de correo que la consulta del paso 2 no vio | El caso de uso no materializa nada y devuelve el motivo `CORREO_YA_REGISTRADO`. La verificación previa no es una garantía por sí sola: la unicidad efectiva la sostiene también la capa que guarda | Termina el caso de uso |
| FA-03 | El consumidor aporta los datos con espacios alrededor | El caso de uso los entrega al dominio tal como los recibe, que los conserva sin normalizar. Cómo se comparan dos correos es un punto abierto declarado y no se decide acá | Paso 2 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CORREO_YA_REGISTRADO` | El correo aportado ya pertenece a una cuenta | No constituye nada y devuelve el motivo. No se informa el estado ni el papel de la cuenta que lo ocupa |
| `DATO_OBLIGATORIO_AUSENTE` | El dominio rechaza la constitución por correo, nombre o apellido vacío | Propaga el motivo del dominio sin traducirlo. No hay efecto parcial |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | El consumidor aporta una credencial derivada en el auto-registro | Propaga el rechazo del dominio: en este camino la credencial se fija recién en CU-03 |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | El consumidor pide constituir la cuenta en un estado distinto de `Pendiente` | Propaga el rechazo del dominio. El estado inicial de este camino no se elige: lo fija el dominio |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | Se pide constituir una cuenta con papel `Administrador` por el auto-registro | Propaga el rechazo del dominio y remite a CU-10 |

Los cinco terminan de forma controlada y sin escritura: la unidad de trabajo no se abre hasta el paso 6.

## 7. Postcondiciones

- **Éxito:** existe una cuenta con correo, nombre, apellido, papel `Alumno`, sello de alta y estado `Pendiente`, sin credencial derivada, a la espera del acto explícito de habilitación del administrador.
- **Fallo:** no existe ninguna cuenta nueva y ningún dato quedó a medio escribir.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un repositorio de cuentas sin ninguna cuenta con el correo `ana.perez@ejemplo.edu` y un reloj fijado en 2026-03-15 | El consumidor solicita el alta de `ana.perez@ejemplo.edu`, «Ana», «Pérez» | El caso de uso devuelve la cuenta constituida con papel `Alumno`, estado `Pendiente`, sin credencial derivada y con sello de alta 2026-03-15 |
| CA-02 | Un repositorio con una cuenta cuyo correo es `ana.perez@ejemplo.edu` | El consumidor solicita el alta de `ana.perez@ejemplo.edu` con otro nombre | El caso de uso devuelve el motivo `CORREO_YA_REGISTRADO` y el repositorio sigue con 1 cuenta |
| CA-03 | Un repositorio de cuentas vacío | El consumidor solicita por esta vía el alta de `docente@ejemplo.edu` con papel `Administrador` | El caso de uso devuelve el motivo `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, el repositorio sigue vacío y el camino correcto es CU-10 |
| CA-04 | Un repositorio vacío y un doble de reloj fijado en 2026-03-15 | El consumidor solicita el alta aportando una credencial derivada | El caso de uso devuelve el motivo `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` y el repositorio sigue vacío |
| CA-05 | Un repositorio vacío y un doble de reloj fijado en 2026-03-15 | El consumidor solicita el alta con el nombre vacío | El caso de uso devuelve el motivo `DATO_OBLIGATORIO_AUSENTE` y el repositorio sigue vacío |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-02 en sus criterios de circuito sin correo y de alta de punta a punta; NB-01 en su criterio de admisión explícita |
| Reglas de negocio aplicables | [RN-02](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-06](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-06-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md) |
| Casos de uso de dominio orquestados | [CU-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-El-Alta-De-Un-Alumno.md) |
| Puertos que consume | Repositorio de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-01, US-02 |
| Componentes esperados en 05 | Caso de uso de auto-registro con su resultado tipado; contrato del puerto de repositorio de cuentas; contrato del puerto de reloj |
| Tests previstos en 08 | Unitarias con repositorio simulado: correo libre, correo ocupado, papel de administrador pedido por esta vía, credencial aportada, dato obligatorio ausente y sello de alta tomado del reloj |

## 10. Notas y supuestos

- **La unicidad del correo se verifica acá y no en el dominio**, porque exige conocer el conjunto de cuentas y el dominio verifica sobre una entidad. El dominio rechaza constituir un alumno cuya unicidad nadie declaró haber verificado con el motivo `UNICIDAD_DE_CORREO_NO_VERIFICADA`, que **este caso de uso no puede alcanzar por construcción**: el paso 4 declara siempre la verificación que el paso 2 hizo. Se nombra acá para que la ausencia en §6 no se lea como olvido.
- **El estado inicial no lo decide esta capa.** El dominio lo fija, y lo fija distinto en cada camino de alta: `Pendiente` en el auto-registro y `Habilitado` en la configuración del administrador. RN-01 e INV-05 **no fundamentan ningún estado inicial**: declaran la unicidad del administrador y la ventana en la que su alta es posible.
- **El sello de alta es un metadato de orquestación** que esta capa aporta al materializar. No se confunde con la «Fecha» que el alumno declara en su trabajo, que es dato del alumno; el modelo del dominio declara la fecha de alta del alumno y la recibe del consumidor, sin leer el reloj.
- **El criterio de comparación de dos correos —tal cual o normalizados— es un punto abierto declarado aguas arriba** y no se reabre acá: lo resuelve `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación.
- La derivación de la contraseña y la emisión del acceso no ocurren en esta capa.
- Este caso de uso **no verifica pertenencia ni facultad**: el auto-registro lo ejerce una persona que todavía no tiene cuenta. La protección del alta del administrador vive en CU-10.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r1 del audit, hallazgo H-01**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. El caso de uso **se acota al auto-registro del alumno** y deja de absorber la configuración del administrador, que pasa a [CU-10](CU-10-Configurar-La-Cuenta-De-Administrador.md), espejando la partición que `GeometriaFactory-Domain` resolvió entre su CU-01 y su CU-12 nuevo. Se retiran el flujo alternativo que constituía la cuenta del administrador, la postcondición que la declaraba habilitada, su criterio de aceptación y la atribución del estado inicial a RN-01 e INV-05, que no lo fundamentan. §6 suma `ESTADO_INICIAL_NO_NEGOCIABLE` y `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, propagados del dominio, y pierde `ADMINISTRADOR_YA_CONFIGURADO`, que se muda a CU-10. **H-12**: §3 dice «los dos puertos» y no «los tres», que contaba al modelo de dominio como puerto. **H-14**: §10 declara que `UNICIDAD_DE_CORREO_NO_VERIFICADA` es inalcanzable por construcción, para que su ausencia en §6 no se lea como olvido. **H-06**: la fecha de alta se declara **metadato de orquestación** de esta capa y se nombra «sello de alta», distinta de la «Fecha» que el alumno declara y que el modelo del dominio sí modela. El nombre de archivo se conserva por estabilidad de citación. |

## 17. Compatibilidad de la superficie pública

Agregar datos opcionales al alta es compatible. Dejar de verificar la unicidad del correo antes de invocar al dominio contradice RN-02. Admitir por esta vía la constitución de una cuenta con papel `Administrador` reabriría el defecto que la partición en dos caminos de alta cerró.
