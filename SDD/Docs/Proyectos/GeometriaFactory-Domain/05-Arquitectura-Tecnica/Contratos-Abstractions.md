# Contrato de la superficie pública — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Contratos-Abstractions.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
- [3. Operaciones](#3-operaciones)
- [4. Elementos de datos](#4-elementos-de-datos)
  - [4.1 Entidades](#41-entidades)
  - [4.2 Conjuntos cerrados](#42-conjuntos-cerrados)
  - [4.3 Resultado de operación](#43-resultado-de-operación)
- [5. Manejo de errores](#5-manejo-de-errores)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara **qué expone `GeometriaFactory-Domain` a sus dos consumidores** —`GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, que lo referencian por proyecto de código (`PRODUCT-INTAKE` §14)— y con qué compromisos.

Los casos de uso que se materializan a través de este contrato son los **trece** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los del ensamblado de tipos de transferencia, que es otro proyecto de código y tiene su propio contrato. La duplicación aparente entre las entidades de acá y esos tipos es deliberada (`PRODUCT-INTAKE` §17.1.P.12).

## 2. Formato

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: no hay protocolo que describir.

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los declara abiertos y los ata al punto de control de la etapa `a` (`PRODUCT-INTAKE` §17.1.P.11); este documento nombra los elementos en lenguaje de dominio, igual que hace [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md).

## 3. Operaciones

Trece operaciones, una por caso de uso. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar, que es la contrapartida de [`ADR-05`](Adrs/ADR-05-Guarda-Unica-De-Admisibilidad.md) y de [`ADR-06`](Adrs/ADR-06-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md).

| Op | Caso de uso | Qué hace | Exige resuelto por el consumidor | Componente |
| --- | --- | --- | --- | --- |
| OP-01 | CU-01 | Constituye un alumno con cuenta `Pendiente`, sin credencial derivada y con papel `Alumno` | Unicidad del correo; momento del alta | Núcleo de entidades, Guardas de cuenta |
| OP-02 | CU-02 | Habilita, bloquea, rehabilita o da de baja una cuenta **de alumno** | Que quien opera es el administrador; el texto de confirmación en la baja; momento | Guardas de cuenta |
| OP-03 | CU-03 | Fija o reemplaza la credencial derivada de una cuenta | Que la credencial vigente fue verificada; la credencial nueva **ya derivada** | Guardas de cuenta |
| OP-04 | CU-04 | Responde si la cuenta admite acceso, y con qué motivos si no | Nada: es la puerta de entrada | Evaluador de admisibilidad |
| OP-05 | CU-05 | Constituye o reedita un trabajo con dueño, identidad propia y texto original íntegro | Pertenencia del trabajo; momento de creación y de última modificación | Núcleo de entidades, Máquina de estados |
| OP-06 | CU-06 | Adopta el conjunto de piezas y componentes reconstruido, con identidad posicional | La interpretación del texto, hecha afuera; la cantidad de figuras del conjunto raíz | Adopción de la interpretación |
| OP-07 | CU-07 | Adopta las observaciones del trabajo, comprobando que están bien formadas | La emisión de las observaciones, hecha afuera | Adopción de la interpretación |
| OP-08 | CU-08 | Resuelve el estado del trabajo en el envío: `Pendiente` si el texto verifica, `Borrador` si no | El resultado de la interpretación; momento | Máquina de estados |
| OP-09 | CU-09 | Resuelve si un alumno accede a un trabajo y qué puede hacer con él | Nada más que las entidades | Máquina de estados |
| OP-10 | CU-10 | Aplica el desenlace —aprobar o rechazar— sobre un trabajo en estado `Pendiente`, con comentario opcional | Que quien opera es el administrador; momento | Máquina de estados |
| OP-11 | CU-11 | Resuelve qué trabajos entran en el alcance del administrador y cuáles puede eliminar | Nada más que las entidades | Máquina de estados |
| OP-12 | CU-12 | Constituye la única cuenta de administrador, `Habilitado` y con credencial, mientras no exista ninguna | Que no existe ninguna cuenta con papel `Administrador`; la credencial **ya derivada**; momento | Guardas de cuenta |
| OP-13 | CU-13 | Resetea la contraseña de una cuenta de alumno: fija la provisoria **ya derivada** y pone la marca | Que quien opera es el administrador; la provisoria ya producida y derivada; momento | Guardas de cuenta |

**Trece operaciones sobre trece casos de uso.** OP-11 y OP-09 devuelven predicados y no aplican efecto: el dominio no ejecuta consultas, declara el criterio con el que la consulta se acota.

## 4. Elementos de datos

### 4.1 Entidades

Las cinco de [`../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`](../02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md) §2, con la semántica y las restricciones conceptuales que ese documento fija y que este contrato no redefine.

| Entidad | Qué expone | Qué no expone |
| --- | --- | --- |
| Alumno | Identificador, correo, nombre, apellido, papel, estado de cuenta, marca de cambio de contraseña pendiente, fecha de alta | El contenido de la credencial derivada, que es opaca: se comprueba su presencia, nunca su valor |
| Trabajo | Identificador, dueño, nombre, fecha declarada por el alumno, fecha de creación, fecha de última modificación, descripción, texto original, estado, conjunto de piezas, cantidad de figuras del conjunto raíz, observaciones, comentario del administrador | Ninguna operación de escritura libre sobre el estado ni sobre el texto original |
| Pieza | Posición, tipo, área declarada, área derivada, volumen declarado, volumen derivado, componentes | La familia plana o volumétrica, que **se deriva del tipo** y no se guarda |
| Componente | Posición, papel, tipo, dimensiones declaradas, área declarada | Ninguna corrección ni unificación de discriminantes |
| Observación | Especie, posición de pieza, campo, valor declarado, valor derivado | Ninguna relación con el comentario del administrador: no comparten campos |

### 4.2 Conjuntos cerrados

Agregar un valor a cualquiera de estos conjuntos es cambio **menor**; quitarlo es cambio **mayor** ([`ADR-03`](Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) §7).

| Conjunto | Valores | Cantidad |
| --- | --- | --- |
| Papel de la cuenta | `Alumno`, `Administrador` | 2 |
| Estado de cuenta | `Pendiente`, `Habilitado`, `Bloqueado` | 3 |
| Marca de cambio de contraseña pendiente | Puesta, levantada | 2 |
| Estado del trabajo | `Borrador`, `Pendiente`, `Finalizado`, `Rechazado`, con los dos últimos terminales | 4 |
| Especie de observación | Advertencia, error de validación | 2 |
| Desenlace de la revisión | Aprobar, rechazar | 2 |
| Tipo de pieza | `Cilindro`, `Cubo`, `Ortoedro`, `Rectangulo`, `Cuadrado`, `Circulo` | 6 |
| Tipo de componente | `Circulo`, `Cuadrado`, `Rectangulo`, `RectanguloDesarrollado` | 4 |
| Papel del componente | Tapa, cara, base, lateral, lado | 5 |

### 4.3 Resultado de operación

Toda operación que pueda rechazar devuelve un resultado con dos salidas posibles —efecto aplicado, o condición que lo impidió— según [`ADR-02`](Adrs/ADR-02-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md). Tres precisiones de forma:

1. **La admisibilidad devuelve varios motivos**, no uno: una cuenta puede ser no admisible por más de una causa a la vez.
2. **La adopción de la interpretación devuelve una colección de condiciones**, porque un conjunto de piezas puede estar mal formado en más de un lugar.
3. **Las demás operaciones devuelven una sola condición.**

## 5. Manejo de errores

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **42** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia.
- **Códigos reservados.** El catálogo registra **cinco identificadores retirados** —tres por renombre y dos por imposibilidad de su causa—, y ninguno se recicla. Un identificador retirado no vuelve a nombrar otra condición.
- **Sin excepciones para reglas de negocio.** Las excepciones quedan reservadas a defectos de programación del consumidor.
- **Sin texto de presentación.** El dominio devuelve códigos, no mensajes para una persona: la composición del mensaje es de la capa que expone, y la traducción a respuesta de protocolo, de `GeometriaFactory-Api`.
- **Sin dirección de servicio en ninguna condición** (RA-03). Es trivial acá porque el dominio no conoce ninguna, y se declara para que no deje de serlo.

## 6. Versionado del contrato

Aplica el criterio de [`ADR-03`](Adrs/ADR-03-Versionado-Y-Estabilidad-De-La-Superficie.md) §7, con estas concreciones sobre los elementos de este contrato:

| Cambio sobre este contrato | Clase |
| --- | --- |
| Quitar o renombrar una operación, o cambiar qué exige resuelto | Mayor |
| Quitar un valor de cualquiera de los nueve conjuntos cerrados de §4.2 | Mayor |
| Quitar un atributo de una entidad, o cambiar su semántica | Mayor |
| Perder un invariante, aunque ninguna firma cambie | Mayor |
| Agregar una operación, un atributo opcional o un valor a un conjunto cerrado | Menor |
| Agregar una condición al catálogo de 03 | Menor |
| Corregir una guarda para que cumpla el invariante que ya declaraba | Parche |

**Compatibilidad hacia atrás.** Los dos consumidores se compilan dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir a los dos consumidores en la misma etapa.

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-01 a CU-13, los trece |
| RN que cubre | RN-01 a RN-16, las dieciséis, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.2 |
| Invariantes que sostiene | INV-01 a INV-09, los nueve, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.3 |
| ADR que lo gobiernan | ADR-01, ADR-02, ADR-03, ADR-04, ADR-05, ADR-06 |
| Consumidores | `GeometriaFactory-Application` y `GeometriaFactory-Infrastructure`, por referencia de proyecto de código |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo; prueba de inspección de la superficie pública contra §4.2 y §5 |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara las trece operaciones de la superficie pública con lo que cada una exige resuelto por el consumidor, las cinco entidades con lo que exponen y lo que no, los nueve conjuntos cerrados con su cardinalidad, las tres formas del resultado de operación, el manejo de errores con la fuente única del catálogo y los cinco identificadores retirados, y el criterio de versionado aplicado elemento por elemento. |
