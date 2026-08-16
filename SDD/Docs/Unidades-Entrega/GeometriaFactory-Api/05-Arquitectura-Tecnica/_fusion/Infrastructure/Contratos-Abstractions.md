# Contrato de la superficie pública — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Contratos-Abstractions.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-13
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)

---

## Tabla de contenido

- [1. Alcance del contrato](#1-alcance-del-contrato)
- [2. Formato](#2-formato)
- [3. Operaciones](#3-operaciones)
- [4. Esquemas de datos: qué cruza cada frontera](#4-esquemas-de-datos-qué-cruza-cada-frontera)
- [5. Manejo de errores](#5-manejo-de-errores)
- [6. Versionado del contrato](#6-versionado-del-contrato)
- [7. Trazabilidad](#7-trazabilidad)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Alcance del contrato

Este documento declara **qué expone `GeometriaFactory-Infrastructure` y a quién**, y con qué compromisos. La superficie tiene una particularidad que hay que entender antes que nada: **es de una sola cara y de tres clases distintas**.

- **Cuatro adaptadores** que implementan contratos **que declara otro proyecto de código**: los puertos de `GeometriaFactory-Application`. Esta capa **no los define**: los cumple.
- **Dos mecanismos** que no son puertos de nadie y que esta capa **sí define**: credenciales y acceso firmado.
- **Una responsabilidad de arranque** que no es puerto ni mecanismo: dejar el almacén en condiciones antes de la primera petición.

**El único consumidor es la composición de raíz de `GeometriaFactory-Api`.** Nadie más referencia este proyecto de código: así lo declara el intake §14 y así lo refleja el grafo de dependencias del `PRODUCT-MANIFEST` §3.

Los casos de uso que se materializan a través de este contrato son los **diez** de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/_fusion/Infrastructure/Especificacion-Funcional.md) §5, sin excepción: cada uno describe un contrato de uso de esta superficie y no un flujo de pantallas.

**Este contrato no cruza ninguna frontera de proceso.** Los datos que salen del proceso son los tipos de transferencia de `GeometriaFactory-Contracts`, que es otro proyecto de código y tiene su propio contrato.

## 2. Formato

**Contrato de superficie de biblioteca, declarado en prosa estructurada.** No hay descripción formal de servicio, ni esquema de mensajes, ni definición de procedimiento remoto: el intake declara «no aplica» en comunicación e integración para este proyecto de código, porque **no expone puntos de acceso** (§17.1.P.3 · GeometriaFactory-Infrastructure).

**Los nombres de tipos, de operaciones y de espacios de nombres no se fijan acá.** El intake los ata al punto de control de la etapa `a`; este documento nombra los elementos en lenguaje de dominio, igual que hacen las categorías 02 y 03 de este proyecto de código. Los **tres** identificadores de puerto que el intake sí declara —`IWorkRepository`, `IFigureValidator` e `ISystemClock`— se citan en §3 y son la única cita de identificadores de código de esta cadena; el cuarto **no tiene identificador declarado** y esta categoría no lo inventa ([`ADR-06003`](../../Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6).

## 3. Operaciones

Las **siete** filas de superficie están, agrupadas por clase y sin agrupar dentro de cada clase. La columna «Exige resuelto» declara qué tiene que haber resuelto el consumidor **antes** de invocar.

### Los cuatro adaptadores de puerto

| Op | Frontera | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-01 | Puerto de repositorio de trabajos (`IWorkRepository`) | Recuperar un trabajo; resolver una consulta **ya acotada** por dueño o por alcance, en sus **dos** formas —proyección de listado sin texto original, sin componentes y sin comentario, y detalle completo—; materializar el resultado; ejecutar el retiro | El recorte, declarado en el pedido. **Sin recorte no hay consulta**; y la pertenencia y la facultad, ya comprobadas | CU-06003, CU-06004 | ADR-06001, ADR-06002 |
| OP-02 | Puerto de repositorio de cuentas (**sin identificador declarado**) | Recuperar una cuenta por su correo; responder si un correo ya está registrado y si ya existe una cuenta con papel `Administrador`; materializar el resultado **incluida la marca de cambio de contraseña pendiente**; ejecutar el retiro con arrastre | La credencial **ya derivada**, cuando la haya; la facultad, ya comprobada; el correo de confirmación, ya comparado | CU-06005, CU-06004 | ADR-06001, ADR-06003 |
| OP-03 | Puerto de validación de figuras (`IFigureValidator`) | Interpretar el texto original y devolver **tres cosas**: la cantidad de figuras del conjunto raíz, las piezas reconstruidas con su posición y las observaciones con su especie, su posición y su campo | Nada más que el texto. **No recibe identidad, ni estado, ni configuración** | CU-06001, CU-06002 | ADR-06006 |
| OP-04 | Puerto de reloj del sistema (`ISystemClock`) | Devolver el momento actual, en tiempo universal coordinado | Nada | CU-06009 | ADR-06002 |

### Los dos mecanismos

| Op | Mecanismo | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-05 | Credenciales | Derivar una contraseña; verificar una credencial contra un valor derivado; y **producir la contraseña provisoria** de la habilitación y del reseteo | Para las dos primeras, la contraseña en claro. **Para la tercera, nada: la producción no recibe ningún parámetro** | CU-06006, CU-06007 | ADR-06004, ADR-06005 |
| OP-06 | Acceso firmado | Emitir un acceso con sus **cuatro** reclamos —identificador, correo, papel y expiración— y verificar uno recibido | Los cuatro reclamos, **completos**; la admisibilidad de la cuenta, **ya resuelta**: una cuenta que no admite acceso no llega acá | CU-06008 | ADR-06004 |

### La responsabilidad de arranque

| Op | Responsabilidad | Qué ofrece | Exige resuelto por el consumidor | CU | ADR |
| --- | --- | --- | --- | --- | --- |
| OP-07 | Preparación del almacén | Crear el almacén si no existe, aplicar el linaje de transformaciones si está desactualizado, y **detener el arranque** antes que operar sobre un almacén en el que no se puede confiar | La ubicación del almacén, provista por configuración. **Esta capa la recibe y no la busca** | CU-06010 | ADR-06007 |

**Siete filas de superficie sobre diez casos de uso**, y la diferencia no es un hueco: `CU-06001` y `CU-06002` comparten la frontera del puerto de validación —son los dos motores de un mismo pipeline—, y `CU-06003` con `CU-06004` y `CU-06005` con `CU-06004` comparten las dos fronteras de repositorio, porque el retiro es una operación más de cada una aunque sea el caso de uso que se verifica por ausencia.

## 4. Esquemas de datos: qué cruza cada frontera

**Nada cruza estas fronteras que no esté en esta tabla.** En particular, la **producción** de la contraseña provisoria no abre puerto nuevo: el valor sale por el mecanismo y llega a la capa de aplicación **ya producido y ya derivado**, del mismo lado desde el que llega la contraseña que el alumno elige.

| Frontera | Entra | Sale | Lo que **nunca** cruza |
| --- | --- | --- | --- |
| Repositorio de trabajos | Identidad de un trabajo, o un pedido de consulta **con su recorte**; entidades a materializar | Trabajo completo, proyección de listado, o nada encontrado | El conjunto completo de trabajos de la comisión: no hay operación que lo devuelva |
| Repositorio de cuentas | Correo, identidad de cuenta, entidades a materializar | Cuenta con su estado, su papel y **su marca**; respuesta de las dos preguntas sobre el conjunto | La contraseña en claro, y el valor derivado hacia arriba de esta frontera **en ningún caso salvo el que la propia verificación consume** |
| Validación de figuras | **Sólo el texto original** | Cantidad de figuras del conjunto raíz, piezas y observaciones | El estado del trabajo: el motor no lo decide. Y ninguna petición de red sale de esta frontera |
| Reloj del sistema | Nada | Un momento | Nada más: es el contrato más corto de la capa, y que sea trivial es la prueba de que la inversión está bien hecha |
| Credenciales | Contraseña en claro, o valor derivado a verificar, o nada | Valor derivado, veredicto, o provisoria en claro **una sola vez** | La provisoria hacia una traza o un registro; la contraseña en claro hacia adentro del producto |
| Acceso firmado | Cuatro reclamos, o un acceso a verificar | Acceso firmado, o veredicto | La clave de firma, ni una parte de ella |
| Preparación del almacén | Ubicación del almacén | Almacén preparado, o arranque detenido | La ruta del almacén, dentro de cualquier mensaje |

**Dos precisiones tomadas de la categoría 02 y no redefinidas acá:**

1. **La cantidad de figuras del conjunto raíz la produce el validador**, incluidas las figuras que no pudo reconstruir, y **no es derivable de las piezas**, que admiten huecos. El dominio la exige como precondición de la reconstrucción.
2. **Los sellos de creación y de última modificación son metadatos que produce el consumidor por el puerto de reloj**, distintos de la `Fecha` que el alumno declara en su trabajo. Los tres tiempos no se confunden (`RC-06006`).

## 5. Manejo de errores

- **El conjunto de condiciones es cerrado y su fuente única es la categoría 03**: las **17** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Infrastructure/DX-Error-Messages.md). Este contrato no acuña ninguna y no las transcribe: las referencia.
- **Código, no texto y no excepción.** Esta capa emite un código estable de una enumeración cerrada. No produce mensajes para personas, no los formatea y no los traduce.
- **Ningún código es un código de protocolo.** Su traducción pertenece a `GeometriaFactory-Api`, y una sola condición tiene destinatario declarado aguas arriba: `INTERPRETACION_NO_DISPONIBLE`, que `GeometriaFactory-Application` `CU-06005` §6 espera por el puerto de validación.
- **Dos categorías de conflicto están vacías, y no es un hueco**: facultad y alcance. **Esta capa no autoriza** y no recibe la identidad del solicitante para comprobar nada. Quien busque acá una negativa de autorización está buscando en la capa equivocada.
- **Cuatro condiciones son de terminación degradada y dos detienen el arranque.** Esta capa **no reintenta**: reintentar, si corresponde, lo decide el consumidor.
- **Ninguna condición deja efecto parcial.** Todas las escrituras ocurren dentro de una unidad de trabajo que se cierra entera o no se cierra.
- **Ninguna condición lleva un secreto, la ruta del almacén ni el texto del alumno**, y **todas quedan registradas del lado del servidor**. Es `RA-03` ejercida por disciplina y no por ignorancia, porque esta capa **sí conoce** las tres cosas que no puede decir.
- **Siete cosas que parecen fallos y son resultados**, y ninguna tiene entrada en el catálogo: una figura que no se pudo reconstruir, un texto que no se pudo leer ni con la tolerancia, una verificación sin discrepancias, una recuperación que no encontró nada, una consulta con alcance que devuelve el conjunto vacío, una credencial que no coincide y un acceso vencido o con firma que no corresponde.

## 6. Versionado del contrato

Aplica el criterio general del producto —versionado semántico, sin publicación en ningún repositorio de paquetes, con una rama y una etiqueta por etapa— con estas concreciones sobre los elementos de este contrato.

| Cambio sobre este contrato | Clase |
| --- | --- |
| Quitar o renombrar una operación de un adaptador, o cambiar su postcondición | Mayor |
| Cambiar qué exige resuelto una operación antes de invocarla | Mayor |
| Cambiar lo que cruza una frontera en la tabla de §4 | Mayor |
| Quitar una condición del catálogo de 17, o reciclar su identificador | Mayor |
| Cambiar la forma de terminación de una condición existente | Mayor |
| **Editar una transformación de esquema ya fusionada** | **Prohibido**, no versionado. Entra una transformación nueva ([`ADR-06007`](../../Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md)) |
| Agregar una operación a un adaptador existente, o agregar una condición al catálogo | Menor |
| Agregar una transformación de esquema al linaje | Menor |
| Cambiar los parámetros de la función de derivación de clave | Menor, **porque los parámetros viajan con el valor derivado** ([`ADR-06004`](../../Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md)): las credenciales existentes siguen verificándose |
| Corregir un adaptador para que cumpla lo que ya declaraba | Parche |

**Compatibilidad hacia atrás.** El único consumidor se compila dentro del mismo artefacto de agrupación, de modo que un cambio incompatible **rompe la compilación antes de romper el tiempo de ejecución**. No hay deprecación gradual ni convivencia de dos versiones: la política es corregir los dos lados en la misma etapa.

**La excepción son los datos ya guardados, y es la que importa.** El esquema del almacén **sobrevive al despliegue** y no se recompila: un cambio del modelo lógico que la compilación no detecta se detecta al arrancar, con el linaje de transformaciones, y termina en arranque detenido si no cierra.

## 7. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| CU que lo consumen | CU-06001 a CU-06010, los **diez** |
| Puertos que implementa | Los **cuatro** que declara [`GeometriaFactory-Application`](../Application/Contratos-Abstractions.md) §4. Ninguno queda sin adaptador y no hay adaptador sin puerto |
| RN que cubre | RN-06001 a RN-06016, las **dieciséis**, con el reparto de [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.2. **Catorce** tienen tramo acá; RN-06006 y RN-06010 no. **Tres** lo tienen principal: RN-06008, RN-06009 y RN-06014 |
| Invariantes | INV-01 a INV-09, los **nueve**, con el aporte de esta capa declarado en [`Arquitectura-Proyecto-Codigo.md`](Arquitectura-Proyecto-Codigo.md) §10.3 |
| ADR que lo gobiernan | ADR-06001 a ADR-06007, las **siete** |
| Consumidores | **Uno solo**: la composición de raíz de `GeometriaFactory-Api`, por referencia de proyecto de código |
| Documentos hermanos | [`Modelo-Datos-Logico.md`](../../Modelo-Datos-Logico.md), para lo que cruza hacia el almacén; [`Flujo-Ejecucion.md`](../../Flujo-Ejecucion.md), para lo que ocurre dentro del puerto de validación |
| Tests previstos en 08 | Una prueba por operación en su camino de efecto aplicado y al menos una por condición del catálogo de 17; matriz puerto contra adaptador; batería de 10 casos del validador sin almacén; pruebas de integración contra el almacén real para los dos repositorios y para la preparación |

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara la superficie de una cara y tres clases —cuatro adaptadores de puertos que declara otro proyecto de código, dos mecanismos propios y una responsabilidad de arranque—, las siete operaciones con lo que cada una exige resuelto, la tabla de lo que cruza y lo que nunca cruza cada frontera, el manejo de errores con la fuente única del catálogo de 17 condiciones y las dos categorías vacías, y el criterio de versionado con la excepción de los datos ya guardados, que sobreviven al despliegue y no se recompilan. |
| 1.1 | 2026-08-13 | **Tramo `R-2` del plan de renombre de [`Norma-De-Nomenclatura.md`](../../../../../Producto/Norma-De-Nomenclatura.md) 1.4 §8, ejecutado contra el glosario de su §6 y no por criterio propio.** **Acto 1 · el renombre** de los **tres puertos declarados** de su §6.3 —`IRepositorioTrabajos` ⟶ `IWorkRepository`, `IValidadorFiguras` ⟶ `IFigureValidator` e `IRelojDelSistema` ⟶ `ISystemClock`—. Acá son **6 ocurrencias**: las tres de §2 y las tres de la tabla de operaciones de §3. **Ninguna operación y ningún contrato cambian.** **Cuadre `V-4` en las dos direcciones, contra la lista escrita antes de editar:** 64 ocurrencias candidatas medidas en 13 documentos con el instrumento de la norma §2.1, **63 renombradas y 1 no renombrada** —la cita textual de la línea de trazabilidad upstream de `RC-06001-Texto-Original-Escrito-Una-Sola-Vez.md`, que atribuye al `PRODUCT-INTAKE` **1.12** las palabras «`JsonOriginal` conservado íntegro y nunca reescrito» y que **renombrar falsificaría**—. `V-6` cuadró los tres nombres de archivo de `Ports/`. **Esta fila queda fuera del cuadre**, por el punto 4 de `V-4`: al describir lo que hizo reintroduce los identificadores viejos. |
