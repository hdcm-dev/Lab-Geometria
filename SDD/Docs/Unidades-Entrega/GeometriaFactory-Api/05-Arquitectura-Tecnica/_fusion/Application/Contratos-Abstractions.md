# Contrato de la superficie pública — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Contratos-Abstractions.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
- [3. Operaciones: la cara de arriba](#3-operaciones-la-cara-de-arriba)
- [4. Puertos: la cara de abajo](#4-puertos-la-cara-de-abajo)
- [5. Las cuatro comprobaciones contra cada operación](#5-las-cuatro-comprobaciones-contra-cada-operación)
- [6. Manejo de errores](#6-manejo-de-errores)
- [7. Versionado del contrato](#7-versionado-del-contrato)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara **qué expone `GeometriaFactory-Application` y a quién**, y con qué compromisos. La superficie es **de dos caras**, y ésa es la particularidad que hay que entender antes que nada:

- **Hacia arriba**, expone sus **once** casos de uso a `GeometriaFactory-Api`.
- **Hacia abajo**, expone los **cuatro** puertos que `GeometriaFactory-Infrastructure` implementa. La dependencia se invierte: esta capa declara lo que necesita y otra lo provee (`PRODUCT-INTAKE` §14 y §17.1.P.1 · GeometriaFactory-Application).

Los casos de uso que se materializan a través de este contrato son los once de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §5, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los tipos de transferencia de `GeometriaFactory-Contracts`, que es otro proyecto de código y tiene su propio contrato.

## 2. Formato

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: no hay protocolo que describir (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application declara «no aplica» hacia afuera del proceso).

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los declara abiertos y los ata al punto de control de la etapa `a`; este documento nombra los elementos en lenguaje de dominio, igual que hacen las categorías 02 y 03 de este proyecto de código. Los **tres** identificadores de puerto que el intake sí declara se transcriben en §4 y son la única cita de identificadores de código de esta cadena.

## 3. Operaciones: la cara de arriba

Once operaciones, una por caso de uso. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar; la columna «Puertos» declara qué frontera consume cada una.

| Op | Caso de uso | Qué hace | Exige resuelto por el consumidor | Puertos | Componente |
| --- | --- | --- | --- | --- | --- |
| OP-01 | CU-04001 | Registra el alta de una cuenta de alumno por auto-registro: correo libre, cuenta `Pendiente` y **sin** credencial | La identidad de quien pide, que acá es anónima por diseño | Reloj, Repositorio de cuentas | Alta de cuentas |
| OP-02 | CU-04002 | Gobierna la cuenta de un alumno: habilitar, bloquear, rehabilitar y dar de baja. **Habilitar y rehabilitar producen además la contraseña provisoria** | Que quien opera es el administrador; el texto de confirmación en la baja; la provisoria **ya producida y ya derivada** | Repositorio de cuentas, Repositorio de trabajos | Gobierno de cuentas |
| OP-03 | CU-04003 | Resuelve el ingreso: admisibilidad de la cuenta con su motivo, fijación de la credencial derivada dentro de la habilitación y su reemplazo por la propia cuenta | La credencial **ya derivada**; para el reemplazo, que la vigente fue verificada afuera | Reloj, Repositorio de cuentas | Ingreso y credencial |
| OP-04 | CU-04004 | Carga y reedita un trabajo propio, con dueño y texto original íntegro, y sólo en `Borrador` | La identidad del solicitante | Reloj, Repositorio de trabajos | Trabajo |
| OP-05 | CU-04005 | Envía el trabajo: interpreta su texto por el puerto, incorpora piezas y observaciones y deja que el dominio resuelva el estado. **Es la única acción de guardado** | La identidad del solicitante | Reloj, Repositorio de trabajos, Validación de figuras | Trabajo |
| OP-06 | CU-04006 | Consulta los trabajos propios del alumno: listado acotado al dueño y **sin componentes**, y detalle con desenlace y comentario | La identidad del solicitante | Repositorio de trabajos | Consulta |
| OP-07 | CU-04007 | Revisa los trabajos de la comisión: listado **sin borradores**, con dueño para agrupar y filtrar, y detalle equivalente al del alumno | Que quien opera es el administrador | Repositorio de trabajos, Repositorio de cuentas | Consulta |
| OP-08 | CU-04008 | Da desenlace a un trabajo: aprobar o rechazar desde estado `Pendiente`, con comentario opcional y terminalidad | Que quien opera es el administrador | Reloj, Repositorio de trabajos | Desenlace |
| OP-09 | CU-04009 | Elimina un trabajo, con los **dos alcances opuestos**: el alumno sólo en `Borrador`, el administrador en todo lo que ve | La identidad y el papel del solicitante | Repositorio de trabajos | Trabajo |
| OP-10 | CU-04010 | Configura la cuenta de administrador: única, con papel `Administrador`, `Habilitado` y con credencial, **sólo mientras no exista ninguna** | La credencial **ya derivada** | Reloj, Repositorio de cuentas | Alta de cuentas |
| OP-11 | CU-04011 | Resetea la contraseña de un alumno: fija la provisoria, la devuelve una vez y pone la marca, **conservando la cuenta, su estado cualquiera sea y todos sus trabajos** | Que quien opera es el administrador; la provisoria **ya producida y ya derivada** | Reloj, Repositorio de cuentas | Gobierno de cuentas |

**Once operaciones sobre once casos de uso.** OP-06 y OP-07 no aplican efecto: devuelven proyecciones ya acotadas por el predicado que la propia operación entrega al puerto.

## 4. Puertos: la cara de abajo

Los cuatro son los de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §3, y este contrato no los redefine: declara qué se les pide y qué operaciones los consumen.

| Puerto | Identificador declarado | Qué le pide esta capa | Operaciones que lo consumen |
| --- | --- | --- | --- |
| Repositorio de trabajos | `IWorkRepository` | Recuperar un trabajo, resolver una consulta **ya acotada** por dueño o por alcance, materializar el resultado y ejecutar el retiro. Ofrece **dos** formas de lectura: la proyección de listado —sin texto original, sin componentes y sin comentario— y el detalle completo | OP-02, OP-04, OP-05, OP-06, OP-07, OP-08, OP-09 |
| Validación de figuras | `IFigureValidator` | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | OP-05 |
| Reloj del sistema | `ISystemClock` | Los sellos de alta, de modificación y de desenlace, **para que sean verificables en prueba** | OP-01, OP-03, OP-04, OP-05, OP-08, OP-10, OP-11 |
| Repositorio de cuentas | **Sin identificador declarado**, ver [`ADR-04002`](../../Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) §2 | Recuperar una cuenta por su correo, responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`, y materializar el resultado, **incluida la marca de cambio de contraseña pendiente** | OP-01, OP-02, OP-03, OP-07, OP-10, OP-11 |

**Dos precisiones sobre lo que viaja por los puertos**, tomadas de la categoría 02 y no redefinidas acá:

1. **Los sellos de alta, de modificación y de desenlace son metadatos de orquestación de esta capa**, distintos de la «Fecha» que el alumno declara en su trabajo. El modelo del dominio no los declara como atributos y la discrepancia está elevada al Product Owner.
2. **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas adoptadas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción, de modo que OP-05 —único consumidor del puerto de validación— es quien la hace viajar.

**Nada más cruza estas cuatro fronteras.** En particular, la **producción** de la contraseña provisoria no abre puerto: el valor llega a esta capa ya producido y ya derivado, del mismo lado desde el que llega la contraseña que el alumno elige.

## 5. Las cuatro comprobaciones contra cada operación

Las once filas están, sin agrupar. Son las comprobaciones de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §4, ejercidas en el orden fijo de [`ADR-04004`](../../Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md): la cuarta primero, después pertenencia, después facultad, después alcance.

| Op | Cambio de contraseña pendiente | Pertenencia | Facultad | Alcance del administrador |
| --- | --- | --- | --- | --- |
| OP-01 | Sí | — | — | — |
| OP-02 | Sí | — | **Sí** | — |
| OP-03 | Sí, **con la única excepción declarada**: el reemplazo de FA-05, que es lo que la levanta | — | — | — |
| OP-04 | Sí | **Sí** | — | — |
| OP-05 | Sí | **Sí** | — | — |
| OP-06 | Sí | **Sí** | — | — |
| OP-07 | Sí | — | **Sí** | **Sí** |
| OP-08 | Sí | — | **Sí** | **Sí** |
| OP-09 | Sí | **Sí** | — | **Sí** |
| OP-10 | Sí | — | — | — |
| OP-11 | Sí | — | **Sí** | — |

**La primera columna es «Sí» en las once, y ésa es exactamente la propiedad que `INV-09` exige**: una cuenta con la marca puesta no ejerce ninguna capacidad, ni siquiera las que su papel y su pertenencia admitirían. La única celda con excepción es la de OP-03, y la excepción está acotada al reemplazo de la propia credencial.

## 6. Manejo de errores

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **36** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Application/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia ([`ADR-04006`](../../Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md)).
- **Resultado tipado, no excepción.** Toda condición prevista viaja como valor de retorno con su código estable. Las excepciones quedan reservadas a defectos de programación del consumidor.
- **Las tres negativas de autorización no se confunden.** La de pertenencia oculta la existencia del recurso y el consumidor la traduce a «no encontrado» y **nunca** a «no autorizado»; la de facultad sí admite ser explícita; la de alcance del administrador es propia y distinta de las dos.
- **Una sola negativa de facultad**, aunque el dominio declare dos códigos para la misma: esta capa corta con su propia verificación antes de invocarlo.
- **Sin texto de presentación.** Esta capa devuelve códigos y, cuando corresponde, índice de figura y campo. La composición del mensaje es de quien expone y la traducción a respuesta de protocolo, de `GeometriaFactory-Api`.
- **Sin dirección de servicio, ruta de datos ni traza en ninguna condición** (`RA-03`). Es trivial acá porque esta capa no conoce ninguna de las tres, y se declara para que no deje de serlo.
- **Las observaciones del trabajo no son condiciones de error de esta capa**: son datos del trabajo, con su especie y su ubicación. Y **el comentario del administrador no es una observación**: no comparten ni un campo.

## 7. Versionado del contrato

Aplica el criterio de [`ADR-04003`](../../Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) §7, con estas concreciones sobre los elementos de este contrato:

| Cambio sobre este contrato | Cara | Clase |
| --- | --- | --- |
| Quitar o renombrar una operación, o cambiar su postcondición | Arriba | Mayor |
| Cambiar qué exige resuelto una operación antes de invocarla | Arriba | Mayor |
| Cambiar la columna de una comprobación en la tabla de §5 | Arriba | Mayor |
| Quitar, renombrar o cambiar la firma de una operación de un puerto | Abajo | Mayor |
| **Agregar** una operación a un puerto existente, o agregar un puerto | Abajo | **Mayor** |
| Quitar una condición del catálogo de 03, o reciclar su identificador | Las dos | Mayor |
| Agregar una operación a la cara de arriba | Arriba | Menor |
| Agregar una condición al catálogo de 03 | Las dos | Menor |
| Corregir un orquestador para que ejerza la comprobación que ya declaraba | Ninguna | Parche |

**Compatibilidad hacia atrás.** Los consumidores de las dos caras se compilan dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir a los dos lados en la misma etapa.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-04001 a CU-04011, los **once** |
| CU de dominio que orquesta | Los **trece** de `GeometriaFactory-Domain`, con el reparto de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Application/Especificacion-Funcional.md) §7.4. Ninguno queda sin orquestar |
| RN que cubre | RN-04001 a RN-04016, las **dieciséis**, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.2. **Quince** tienen tramo acá; RN-04014 no |
| Invariantes | INV-01 a INV-09, los **nueve**, con el aporte de esta capa declarado en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.3 |
| ADR que lo gobiernan | ADR-04001, ADR-04002, ADR-04003, ADR-04004, ADR-04005, ADR-04006 |
| Consumidores | `GeometriaFactory-Api`, por la cara de arriba; `GeometriaFactory-Infrastructure`, por la de abajo. Los dos por referencia de proyecto de código |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo de 36; matriz comprobación contra prueba para las cuatro negativas; matriz puerto contra doble; prueba del arrastre de la baja como testigo de la unidad de trabajo |

## 9. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara la superficie de dos caras: las once operaciones de la cara de arriba con lo que cada una exige resuelto y los puertos que consume, y los cuatro puertos de la cara de abajo con lo que se les pide. Emite la tabla de las cuatro comprobaciones contra cada operación, con las once filas, el manejo de errores con la fuente única del catálogo de 36 condiciones, y el criterio de versionado aplicado elemento por elemento con la asimetría de las dos caras. |
| 1.1 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](../../../../../Producto/Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`—. Acá son **3 ocurrencias**, las de la tabla de la cara de salida. **Ninguna operación, ninguna precondición y ningún contrato cambian**: cambia el idioma del identificador y nada más. **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-04001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
