# CU-05 — Guardar y recuperar las cuentas de la comisión

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** CU-05-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-01`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-01-Control-De-Admision-Al-Laboratorio.md); [`NB-02`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-02-Identidad-Propia-Del-Alumno-Sin-Correo.md); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.11** §4.1 (RN-01, RN-02, RN-12, RN-13), §17.1.P.2 (INV-01, INV-05, INV-08, INV-09), §17.3.P.4; implementa el puerto de repositorio de cuentas de `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3, cuyo identificador es punto abierto declarado allá
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Infrastructure

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

Materializar y recuperar las cuentas del laboratorio, y responder las **dos preguntas sobre el conjunto** que la capa de aplicación necesita para ejercer sus reglas de alta: si un correo ya está registrado, y si ya existe una cuenta con papel `Administrador`.

Ninguna de las dos preguntas se puede contestar mirando una entidad sola, y ése es el motivo por el que este puerto existe. La segunda es la que sostiene la ventana de alta del administrador, que se cierra con la primera configuración y no vuelve a abrirse.

Lo que este caso de uso **no** hace: no deriva contraseñas ni las compara —eso es `CU-06`—, no produce la contraseña provisoria —eso es `CU-07`— y no decide transiciones de estado de cuenta.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Consumidor del puerto de repositorio de cuentas (`GeometriaFactory-Application`) | Primario | Recupera, pregunta por el conjunto y materializa |
| Almacén de datos | Sistema | El archivo único de SQLite |

## 3. Precondiciones

- El almacén existe y su esquema está al día, por `CU-10`.
- La credencial derivada, cuando viaja, llega **ya derivada**: el valor en claro **nunca atraviesa este contrato**.

## 4. Flujo principal

1. El consumidor pide una operación sobre cuentas, dentro de **una única unidad de trabajo**.
2. Si es una **recuperación por correo o por identificador**, se devuelve la cuenta con su papel, su estado, su credencial derivada si la tiene, su fecha de alta y su **marca de cambio de contraseña pendiente**.
3. Si es la pregunta **«¿este correo ya está registrado?»**, se responde por sí o por no sobre el conjunto de cuentas.
4. Si es la pregunta **«¿ya existe una cuenta con papel `Administrador`?»**, se responde por sí o por no.
5. Si es una **materialización**, se escribe la cuenta con todos sus atributos, **incluida la marca**. La unicidad del correo y la unicidad del administrador se sostienen también acá, con la restricción del almacén, y no sólo con la consulta previa del consumidor.
6. La unidad de trabajo se cierra entera o no se cierra.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La recuperación por correo no encuentra ninguna cuenta | Se devuelve **nada encontrado**. No es una condición de error de este contrato: quién lo traduce —y **sin distinguirlo hacia afuera**, para no revelar qué correos están registrados— es el consumidor | Termina la operación |
| FA-02 | Se materializa una cuenta que ya existe, cambiando su estado, su credencial derivada o su marca | Se actualiza la cuenta. **El correo y el papel no se reemplazan por esta vía**: son la identidad de la cuenta | Paso 6 |
| FA-03 | Se materializa la marca de cambio de contraseña pendiente sobre una cuenta `Bloqueado` | Se escribe la marca y **el estado de la cuenta no cambia**: la marca no es un estado de cuenta y convive con los tres sin reemplazar a ninguno | Paso 6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del caso de uso |
| --- | --- | --- |
| `CORREO_YA_REGISTRADO` | La materialización colisionó con una cuenta que ya ocupa ese correo | Termina sin escribir nada. **Es la segunda línea de la unicidad del correo**, y por eso existe: el consumidor consulta antes, pero la verificación previa no es una garantía por sí sola, y `GeometriaFactory-Application` `CU-01` **FA-02** declara explícitamente ese flujo alternativo: el puerto de repositorio rechaza la materialización por una colisión que la consulta previa no vio, y el caso de uso devuelve el mismo motivo. **No se informa el estado ni el papel de la cuenta que ocupa el correo** |
| `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` | La materialización habría dejado **dos** cuentas con papel `Administrador` en la instancia | Termina sin escribir nada. Sostiene el invariante de administrador único desde el almacén. **No sustituye a la ventana de alta**, que la resuelve el consumidor: acá se impide el resultado, no se explica el camino |
| `ALMACEN_NO_DISPONIBLE` | El archivo del almacén no está alcanzable | Termina de forma **degradada**, sin escribir nada. Tiene su entrada en `CU-03` §6, con la misma causa |

## 7. Postcondiciones

- **Éxito en recuperación o pregunta:** el consumidor recibe la respuesta. Nada cambió.
- **Éxito en materialización:** la cuenta queda escrita con todos sus atributos, incluida la marca.
- **Fallo:** el almacén queda exactamente como estaba.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | Un almacén con una cuenta cuyo correo es el mismo que se va a materializar | Se materializa la cuenta nueva | Devuelve `CORREO_YA_REGISTRADO` y **no queda ninguna cuenta nueva**. La respuesta **no incluye** el estado ni el papel de la cuenta que ocupa el correo |
| CA-02 | Un almacén vacío | Se pregunta si ya existe una cuenta con papel `Administrador` | Responde **no** |
| CA-03 | Un almacén con la cuenta de administrador ya configurada | Se pregunta lo mismo | Responde **sí**, y una materialización de una segunda cuenta con ese papel devuelve `UNICIDAD_DE_ADMINISTRADOR_VIOLADA` |
| CA-04 | Una cuenta de alumno en estado `Bloqueado` | Se materializa sobre ella la marca de cambio de contraseña pendiente | La cuenta queda **`Bloqueado` y marcada**. El estado no cambió: la marca no es un estado de cuenta |
| CA-05 | Una cuenta con marca puesta | Se recupera | El resultado trae la marca. Sin ella, la comprobación transversal del consumidor no tendría sobre qué decidir |
| CA-06 | Una cuenta con credencial derivada | Se recupera | El resultado trae la credencial **derivada** y **en ningún caso un valor en claro**, que nunca entró al almacén |
| CA-07 | Un correo que no existe en el almacén | Se recupera por correo | Devuelve **nada encontrado**, y **no** un código de error: distinguirlo revelaría qué correos están registrados y esa distinción es del consumidor, que tampoco la expone |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | NB-01, NB-02 |
| Reglas de negocio aplicables | [RN-02](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02-Correo-Del-Alumno-Unico.md), [RN-01](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-01-Administrador-Unico-Y-Papeles-Fijos.md), [RN-12](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-12-Reseteo-Conserva-La-Cuenta-Y-Sus-Trabajos.md) y [RN-13](../../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-13-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md), las dos últimas por la conservación y el transporte de la marca |
| Reglas conceptuales de modelo | [`RC-07`](../Modelo-Datos/reglas-conceptuales-de-modelo/RC-07-La-Marca-No-Es-Un-Estado-De-Cuenta.md) |
| Puerto que implementa | Repositorio de cuentas, cuyo **identificador es un punto abierto declarado aguas arriba** y que esta categoría no reabre |
| Consumidor | `GeometriaFactory-Application`, sus CU-01, CU-02, CU-03, CU-07, CU-10 y CU-11 |
| Historias de usuario a generar en 06 | US-14, US-15, US-16 |
| Componentes esperados en 05 | Adaptador del repositorio de cuentas, con las dos restricciones de unicidad del almacén |
| Tests previstos en 08 | Pruebas de integración contra SQLite real: colisión de correo en la materialización, rechazo del segundo administrador, conservación de la marca sobre cuenta bloqueada y ausencia de valor en claro en toda la fila |

## 10. Notas y supuestos

- **El criterio con el que dos correos se consideran el mismo es un punto abierto declarado aguas arriba**, en `GeometriaFactory-Domain` y en `GeometriaFactory-Application`. Esta categoría **no lo reabre y no lo resuelve**: la restricción de unicidad del almacén se define con el criterio que fije `05-Arquitectura-Tecnica`, y ése es exactamente el lugar donde el criterio se vuelve una decisión de implementación. Queda anotado como punto abierto en `Especificacion-Funcional.md` §11.
- **La marca de cambio de contraseña pendiente es un atributo de la cuenta, no un estado.** Convive con `Pendiente`, `Habilitado` y `Bloqueado` sin reemplazar a ninguno, y el reseteo procede sobre los tres.
- **La cuenta con papel `Administrador` nace `Habilitado` y no admite baja.** Este contrato no lo decide: lo hace cumplir el dominio y lo ejerce el consumidor. Lo que el almacén sostiene es la unicidad, que es una condición sobre el conjunto.
- **Este contrato no autentica.** No compara credenciales y no emite accesos: guarda un valor ya derivado y lo devuelve tal cual.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |

## 17. Compatibilidad de la superficie pública

Agregar un atributo a la cuenta es compatible mientras la marca, el papel y el estado conserven su significado. **Dejar caer alguna de las dos restricciones de unicidad, permitir reemplazar el correo o el papel por la vía de la materialización, o guardar un valor de contraseña en claro son cambios incompatibles** y suben versión mayor.
