# Glosario funcional — GeometriaFactory-Application

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Glosario-Funcional.md
**Versión:** 1.3
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9, §9.1, §9.2 y §9.3 (glosario raíz de la cadena); `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Glosario-Funcional.md` (vocabulario que acuña la categoría de la que este proyecto de código depende); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.2, §12, §12.1, §17.2
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Repositorio](#31-repositorio)
  - [3.2 Pieza](#32-pieza)
  - [3.3 `Pendiente`](#33-pendiente)
  - [3.4 Rol](#34-rol)
  - [3.5 Trabajo, y la forma «unidad de trabajo»](#35-trabajo-y-la-forma-unidad-de-trabajo)
  - [3.6 Dos casos que no son polisemia y no se corrigen](#36-dos-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código y que aparecen en más de uno de sus artefactos. Los que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, y los que acuña la categoría 02 de `GeometriaFactory-Domain` se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Puerto | Contrato que esta capa declara y que otra capa implementa. Es la frontera del proyecto de código: lo que el caso de uso necesita, expresado sin decir quién lo provee ni con qué mecanismo | `Especificacion-Funcional.md` §3, los once CU | «Interfaz de salida». Se dice **puerto** |
| Puerto de repositorio de trabajos | Puerto por el que el caso de uso recupera un trabajo, resuelve una consulta ya acotada, materializa el resultado y ejecuta el retiro | `Especificacion-Funcional.md` §3, CU-04002, CU-04004 a CU-04009 | — |
| Puerto de repositorio de cuentas | Puerto por el que el caso de uso recupera una cuenta, pregunta si un correo ya está registrado o si ya existe una cuenta con papel `Administrador`, y materializa el resultado. **Su identificador no está declarado aguas arriba**: es punto abierto | `Especificacion-Funcional.md` §3 y §11, CU-04001, CU-04002, CU-04003, CU-04007, CU-04010 | — |
| Puerto de validación de figuras | Puerto por el que el caso de uso entrega el texto original y recibe la cantidad de figuras del conjunto raíz, las piezas reconstruidas y las observaciones | `Especificacion-Funcional.md` §3, CU-04005 | «Validador de figuras», que es la forma corta usada cuando el sujeto es la implementación |
| Puerto de reloj del sistema | Puerto por el que el caso de uso obtiene el sello, **para que los sellos de alta, de modificación y de desenlace sean verificables en prueba** | `Especificacion-Funcional.md` §3, CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010 | «Reloj» en forma corta |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública de esta capa por referencia de proyecto de código: `GeometriaFactory-Api`. **Es el actor primario de los once casos de uso** | Los once CU | «Capa consumidora». Se distingue del «consumidor de la biblioteca» de la categoría de dominio, que puede ser también esta capa |
| Verificación de pertenencia | Comprobación de que el trabajo pedido es del alumno solicitante, ejercida sobre el dato recuperado y antes de escribir. **No la reemplaza ningún papel** | `Especificacion-Funcional.md` §4, CU-04004, CU-04005, CU-04006, CU-04009 | «Autorización por pertenencia» |
| Verificación de facultad | Comprobación de que quien pide una operación reservada tiene el papel `Administrador` | `Especificacion-Funcional.md` §4, CU-04002, CU-04007, CU-04008, CU-04011 | — |
| Contraseña provisoria | Valor de credencial que **el sistema produce** cuando el administrador resetea la contraseña de la cuenta de un alumno, y que el administrador le comunica fuera del producto. Es **provisoria** porque no sirve para nada más que para cambiarla: mientras esté vigente, la cuenta está confinada al cambio | `Especificacion-Funcional.md` §4, CU-04011, CU-04003 | «clave provisoria». **No se dice «contraseña temporal»**, que sugiere un vencimiento por tiempo que el producto no tiene |
| Marca de cambio de contraseña pendiente | Atributo que el reseteo deja sobre la cuenta y que la confina a cambiar su propia contraseña. **La pone únicamente CU-04011 y la levanta únicamente el cambio efectivo de CU-04003 FA-05**, hecho por la propia cuenta (INV-09) | `Especificacion-Funcional.md` §3 y §4, CU-04011, CU-04003, CU-04002 | «marca», en forma corta cuando el complemento ya está fijado. No es un estado de cuenta: convive con `Habilitado` sin reemplazarlo |
| Reseteo de contraseña | Operación por la que el administrador reemplaza la credencial derivada de un alumno por una provisoria y deja la marca, **cualquiera sea el estado de esa cuenta**. **No es una baja**: conserva la cuenta, su estado de habilitación y todos sus trabajos (RN-04012) | `Especificacion-Funcional.md` §5 y §8, CU-04011, CU-04002 | «reseteo». **No se dice «recuperación»**, que es lo que el producto sigue sin tener: no hay canal de correo y no hay camino autónomo |
| Cambio forzado | El reemplazo de credencial que hace una cuenta marcada, y que es lo único que levanta la marca | `Especificacion-Funcional.md` §4, CU-04003 | — |
| Alcance de consulta | Recorte que el caso de uso traslada al puerto antes de pedir: por dueño en el caso del alumno, por estado distinto de `Borrador` en el del administrador. **No es un filtro aplicado después sobre un conjunto mayor** | `Especificacion-Funcional.md` §3 y §4, CU-04006, CU-04007 | — |
| Unidad de trabajo | Tramo dentro del cual las escrituras de un caso de uso ocurren enteras o no ocurren. El alcance declarado es **un caso de uso, una unidad de trabajo** | `Especificacion-Funcional.md` §3, CU-04001, CU-04002, CU-04004, CU-04005, CU-04008, CU-04009 | — |
| Motivo | Valor de la enumeración cerrada con la que un caso de uso explica por qué una operación no procede. **No es un código de protocolo**: su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` | Los once CU | «Código de rechazo» en la categoría de dominio |
| Doble | Implementación de prueba de un puerto, que hace ejercitable un caso de uso entero sin base de datos ni frontera de proceso | `Especificacion-Funcional.md` §1 y §4, CU-04001, CU-04005, CU-04007, CU-04008, CU-04010 | «Repositorio simulado», «validador doble», «reloj fijado» |
| Metadato de orquestación | Dato que esta capa aporta al materializar y que **el modelo del dominio no declara como atributo**: los sellos de alta, de modificación y de desenlace. Se distingue de la «Fecha» que el alumno declara en su trabajo, que sí es del dominio | `Especificacion-Funcional.md` §3 y §11, CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010 | «Sello», en la forma corta de cada uno |
| Camino de alta | Cada una de las dos formas en que se constituye una cuenta, con estado inicial, credencial y ventana de alta opuestos: el auto-registro del alumno y la configuración del administrador. Cada uno tiene caso de uso propio acá y en el dominio | `Especificacion-Funcional.md` §5 y §8, CU-04001, CU-04010, CU-04003 | — |

## 3. Términos con más de un referente

Los términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en todos ellos los sentidos aparecen en el mismo contexto de lectura —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Repositorio

Es la polisemia propia de este proyecto de código y no existía en la categoría de dominio, porque allá no hay puertos.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El puerto por el que esta capa guarda y recupera | **Siempre calificado**: «puerto de repositorio de trabajos», «puerto de repositorio de cuentas», o «el puerto de repositorio» cuando el complemento ya está fijado en la misma oración | Los dos referentes conviven en la cadena documental del producto y el segundo aparece en documentos que esta categoría cita |
| El repositorio de código donde vive el árbol del producto | **No se nombra «repositorio» a secas en esta categoría.** Se dice «repositorio de código» | Declarado aguas arriba: el nombre del repositorio de código no es un plano de identidad del producto |

### 3.2 Pieza

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo, cuya identidad es su posición | **«pieza», forma desnuda.** Es el referente del dominio y el que domina esta categoría | `Vision-Producto.md` §9.1 y §9.2 declaran los dos referentes y la forma de cada uno |
| Cada uno de los dos artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» | `Vision-Producto.md` §9.2 declara la forma calificada obligatoria |

En los artefactos de esta categoría el segundo referente aparece sólo en notas de ubicación de responsabilidades, y ahí va calificado.

### 3.3 `Pendiente`

**Declarado en el glosario raíz**, `Vision-Producto.md` §9.2, y en PRODUCT-INTAKE §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica acá, que es donde los dos referentes conviven con más densidad, porque los mismos casos de uso tocan cuentas y trabajos.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | CU-04001, CU-04002, CU-04003 |
| Estado de un **trabajo** enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el sujeto es el trabajo y está nombrado en la misma oración | CU-04004 a CU-04009 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo —«`Borrador`, `Pendiente`, `Finalizado` o `Rechazado`»—, donde el atributo enunciado ya fija el referente y calificar cada valor sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica; y los nombres de los motivos, que son identificadores literales del contrato.

### 3.4 Rol

Los dos referentes conviven en la misma tabla —la de actores de los once casos de uso—, de modo que colisionan y se desambiguan. El segundo referente no lo eligió este proyecto de código: lo impone `Rules-Especificacion-Funcional.md` §4.2 punto 2, que fija el encabezado de esa tabla.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Atributo de la cuenta que vale `Alumno` o `Administrador` | **«papel»**, siempre. Nunca «rol», ni siquiera citando fuentes técnicas que lo llaman así | `Especificacion-Funcional.md` §4 y §6, CU-04001, CU-04002, CU-04007, CU-04008, CU-04009, CU-04010 |
| Función que un actor cumple dentro de un caso de uso | **«rol»**, y **sólo** como encabezado de la columna de la tabla de actores | §2 «Actores» de los once casos de uso |

La forma «rol» no se usa en prosa en ningún artefacto de esta categoría. Es la misma resolución que declara el glosario de `GeometriaFactory-Domain` §3.4, y se conserva idéntica.

### 3.5 Trabajo, y la forma «unidad de trabajo»

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La unidad que el alumno carga y entrega en el laboratorio | **«trabajo», forma desnuda.** Es el único referente admitido para la palabra sola, y **no es una «unidad de entrega»** en el sentido normativo: es un registro de datos, no algo que se despliegue | `Vision-Producto.md` §9.1 y PRODUCT-INTAKE §12.1 declaran la resolución |
| El tramo transaccional de un caso de uso | **Siempre en la forma compuesta «unidad de trabajo»**, nunca «trabajo» a secas. La forma compuesta se usa completa incluso cuando el contexto parece bastar | Los dos aparecen en el mismo párrafo de varios casos de uso: «el trabajo se materializa en una única unidad de trabajo» |
| El flujo de trabajo del administrador, en la fórmula «no forma parte de su flujo de trabajo» | **Siempre en la forma compuesta «flujo de trabajo»**, que viene de la formulación de la regla aguas arriba | Aparece en las mismas secciones que el referente del dominio, en CU-04007 y CU-04009 |
| El esfuerzo de construcción del producto | **No se nombra «trabajo».** Se dice «tarea» o «etapa» | `Vision-Producto.md` §9.1, entrada «Trabajo» |

### 3.6 Dos casos que no son polisemia y no se corrigen

- **Observación** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo, y su relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1. La regla que sí rige es de precisión: cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado corresponde «advertencia», y cuando se habla del conjunto que el validador devuelve corresponde «observación». **El comentario del administrador no es una observación**: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala.
- **Puerto** designa acá una sola cosa: el contrato que esta capa declara y otra implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica (`Vocabulario-Rules.md` §9.1).

## 4. Términos referenciados y no redefinidos

### 4.1 Del glosario raíz de 00

Ya declarados en `00-Contexto/Vision-Producto.md` §9 con la misma semántica con la que esta categoría los usa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 |
| Pieza, en su referente del dominio | `Vision-Producto.md` §9.1 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Componente | `Vision-Producto.md` §9.1 |
| Observación | `Vision-Producto.md` §9.1 |
| Advertencia | `Vision-Producto.md` §9.1 |
| Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, como única acción de guardado | `Vision-Producto.md` §9.1 |
| Aprobar / Rechazar | `Vision-Producto.md` §9.1 |
| Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio | `Vision-Producto.md` §9.1 |
| Actividad 1 | `Vision-Producto.md` §9.1 |
| Punto de control | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Etapa | `Vision-Producto.md` §9.2 |
| Capacidad | `Vision-Producto.md` §9.2 |

### 4.2 Del glosario de GeometriaFactory-Domain

Ya declarados en `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Glosario-Funcional.md` §2. Esta categoría los usa con la misma semántica y no los redefine.

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno | La persona de la comisión con identidad propia dentro del laboratorio, a la que pertenecen trabajos |
| Papel | Atributo que vale `Alumno` o `Administrador` |
| Estado de cuenta | Atributo que vale `Pendiente`, `Habilitado` o `Bloqueado` |
| Credencial derivada | Valor derivado de la contraseña, que nunca llega en claro |
| Admisibilidad de la cuenta | Resultado de evaluar si una cuenta admite acceso, con su motivo |
| Baja de la cuenta | Operación destructiva que elimina la cuenta y todos sus trabajos |
| Texto original | El texto que el alumno cargó, conservado íntegro |
| Posición de pieza | Lugar de la figura en el conjunto raíz; es la identidad de la pieza |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y no se guarda |
| Especie de observación | Atributo que vale `Advertencia` o `Error de validación` |
| Desenlace | Término colectivo de aprobar y rechazar |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las que no se pudieron reconstruir; es el rango de posiciones válidas del trabajo |
| Alcance del administrador | Los tres estados de trabajo que no son `Borrador` |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.3 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial. Declara los doce términos que esta categoría acuña —los cuatro puertos, el consumidor, las dos verificaciones, el alcance de consulta, la unidad de trabajo, el motivo y el doble—, los cinco términos con más de un referente con la forma que corresponde a cada uno y su evidencia de colisión —entre ellos «repositorio», que es la polisemia propia de esta capa, y la forma compuesta «unidad de trabajo», que colisiona con el trabajo del alumno en el mismo párrafo—, los dos casos que deliberadamente no se corrigen, y los treinta y un términos que se referencian del glosario raíz y del glosario de `GeometriaFactory-Domain` sin redefinirlos. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **H-01**: los recuentos de artefactos pasan de nueve a diez casos de uso y §2 da de alta **«camino de alta»**, que la partición de CU-04001 y CU-04010 acuñó y que aparece en más de un artefacto. **H-06**: §2 da de alta **«metadato de orquestación»**, con la distinción frente a la «Fecha» que el alumno declara, y las entradas de los puertos de reloj y de repositorio de cuentas se ajustan. **H-03**: §4.2 suma **«cantidad de figuras del conjunto raíz»** como término **referenciado** del modelo del dominio, que es donde está declarado, y la entrada del puerto de validación lo incorpora. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. §2 da de alta cinco términos que la capacidad acuña y que aparecen en más de un artefacto: **«contraseña provisoria»**, **«marca de cambio de contraseña pendiente»**, **«reseteo de contraseña»** y **«cambio forzado»**, y la entrada de la verificación de facultad suma CU-04011. Dos alias quedan **prohibidos** con su motivo: «contraseña temporal», que sugiere un vencimiento por tiempo que el producto no tiene, y «recuperación», que nombra lo que el producto sigue sin tener —X-2 retiró la exclusión de la recuperación asistida por el administrador, **no** la de la recuperación autónoma por correo, que X-1 impide—. Los recuentos de artefactos pasan de diez a **once** casos de uso. Sube minor: agrega términos sin redefinir ninguno. |
| 1.2 | 2026-08-09 | Absorbe las dos decisiones del Product Owner sobre **F-26** que `CU-04011` 1.2 aplica. §2 corrige dos definiciones **sin dar de alta ni de baja ningún término**: **contraseña provisoria** deja de atribuirle al administrador la elección del valor —**lo produce el sistema**, para que no termine siendo la misma clave para toda la comisión— y **reseteo de contraseña** explicita que procede **cualquiera sea el estado de la cuenta**. Ningún alias se agrega ni se prohíbe y los recuentos de artefactos no cambian. |
