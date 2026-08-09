# Glosario funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Glosario-Funcional.md
**Versión:** 1.3
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena), §9.1, §9.2 y §9.3; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4.2 (colisión de vocabulario de `Pendiente`), §12 y §12.1 (glosario del dominio del cliente y choque de vocabulario), §17.1
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Trabajo](#31-trabajo)
  - [3.2 Pieza](#32-pieza)
  - [3.3 `Pendiente`](#33-pendiente)
  - [3.4 Rol](#34-rol)
  - [3.5 Dos casos que no son polisemia y no se corrigen](#35-dos-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Los términos que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2` (`Vision-Producto.md` §9.3).

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Alumno | Entidad del dominio que representa a la persona de la comisión con identidad propia dentro del laboratorio y a la que pertenecen trabajos | `Definicion-Modelo-De-Dominio.md`, CU-01 a CU-04, CU-09, RN-01, RN-02, RN-06, RN-07 | — |
| Papel | Atributo del alumno que vale `Alumno` o `Administrador`. Es un conjunto cerrado de dos valores, sin permisos configurables. **Determina cuál de los dos caminos de alta constituye la cuenta** | `Definicion-Modelo-De-Dominio.md`, CU-01, CU-02, CU-04, CU-10, CU-11, CU-12, RN-01, RN-10 | «Rol» en las fuentes técnicas. **Se usa «papel»**, y «rol» queda reservado al encabezado normativo de la tabla de actores: ver §3.4 |
| Estado de cuenta | Atributo del alumno que vale `Pendiente`, `Habilitado` o `Bloqueado`, con transiciones declaradas y **con un valor inicial que depende del camino de alta**. **No se confunde con el estado del trabajo**, que es otro conjunto cerrado: ver §3.3 | `Definicion-Modelo-De-Dominio.md`, CU-01 a CU-04, CU-12, RN-01, RN-06, RN-07 | — |
| Credencial derivada | Valor derivado de la contraseña del alumno, que el dominio recibe ya derivado y nunca en claro. Sin valor hasta el primer ingreso efectivo en el auto-registro; con valor desde el alta en la configuración del administrador | `Definicion-Modelo-De-Dominio.md`, CU-03, CU-04, CU-12, RN-06 | «Hash de contraseña» en las fuentes técnicas |
| Admisibilidad de la cuenta | Resultado de evaluar si una cuenta admite acceso al laboratorio, con su motivo cuando no lo admite | `Definicion-Modelo-De-Dominio.md`, CU-04, RN-06 | — |
| Camino de alta | Cada una de las dos formas en que se constituye una cuenta, con estado inicial y tratamiento de la credencial propios: el **auto-registro del alumno** (CU-01), que nace `Pendiente` y sin credencial, y la **configuración del administrador** en el primer arranque (CU-12), que nace `Habilitado` y con credencial | `Definicion-Modelo-De-Dominio.md`, CU-01, CU-03, CU-12, RN-01, RN-02 | — |
| Baja de la cuenta | Operación destructiva e irreversible que elimina la cuenta y todos sus trabajos, cualquiera sea el estado de cada uno. No es un estado de cuenta | `Definicion-Modelo-De-Dominio.md`, CU-02, RN-01, RN-07 | «Baja física» en las fuentes |
| Texto original | El texto que el alumno cargó, tal como lo emitió su programa. Se conserva íntegro y nunca se reescribe | `Definicion-Modelo-De-Dominio.md`, CU-05 a CU-08, RN-08 | «JSON original» en las fuentes técnicas. En esta categoría se dice «texto original», porque el dominio no conoce formatos de serialización |
| Posición de pieza | Lugar que una figura ocupa en el conjunto raíz del trabajo. **Es la identidad de la pieza**, porque el dato del alumno no trae identificador propio | `Definicion-Modelo-De-Dominio.md`, CU-06, CU-07, RN-09 | «Índice de figura» en las fuentes |
| Familia plana o volumétrica | Clasificación de una pieza que **se deriva de su tipo** y no se guarda como dato propio | `Definicion-Modelo-De-Dominio.md`, CU-06 | — |
| Especie de observación | Atributo de la observación que vale `Advertencia` o `Error de validación`, y que decide si el trabajo pasa a estado `Pendiente` o queda en `Borrador` | `Definicion-Modelo-De-Dominio.md`, CU-07, CU-08, RN-05, RN-09 | «Severidad» en las fuentes técnicas |
| Desenlace | Cada una de las dos decisiones que el administrador aplica sobre un trabajo en estado `Pendiente` y que lo llevan a un estado terminal. Es el término colectivo de «aprobar» y «rechazar», que el glosario raíz declara por separado | `Definicion-Modelo-De-Dominio.md`, CU-10, CU-11, RN-10, RN-11 | — |
| Alcance del administrador | Conjunto de estados de trabajo sobre los que el administrador ve y opera: los tres que no son `Borrador` | `Definicion-Modelo-De-Dominio.md`, CU-11, RN-04, RN-11 | — |
| Consumidor de la biblioteca | El proyecto de código que usa la superficie pública de este dominio por referencia de proyecto de código: `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure`. **Es el actor primario de todos los casos de uso de esta categoría** | Los doce CU | «Capa consumidora» |
| Sujeto de la regla | La persona sobre la que recae una regla que el dominio hace cumplir —el alumno, el administrador—, que **no** es actor de ningún caso de uso de esta categoría | Los doce CU | — |

## 3. Términos con más de un referente

Los tres términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los tres, los sentidos aparecen en el mismo contexto de lectura —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Trabajo

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La unidad que el alumno carga y entrega en el laboratorio: nombre, fecha, descripción y texto original, con dueño y estado | **«trabajo», forma desnuda**. Es el único referente admitido en esta categoría | El término normativo «unidad de entrega» designa a otra cosa, y las fuentes del producto usaron una vez uno por el otro |
| Las dos piezas desplegables del producto | **No se nombran «trabajo» en ningún caso.** Se nombran «pieza pública» y «pieza de datos», o «unidad de entrega» en contexto normativo | `Vision-Producto.md` §9.3 y PRODUCT-INTAKE §12.1 declaran la resolución |
| El esfuerzo de construcción del producto | **No se nombra «trabajo».** Se dice «tarea» o «etapa» | `Vision-Producto.md` §9.1, entrada «Trabajo» |

### 3.2 Pieza

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo, cuya identidad es su posición | **«pieza», forma desnuda.** Es el referente del dominio y el que domina esta categoría | Los dos referentes conviven en la misma cadena documental y el segundo aparece en documentos que esta categoría cita |
| Cada uno de los dos artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» | `Vision-Producto.md` §9.2 declara la forma calificada obligatoria |

En los artefactos de esta categoría el segundo referente aparece sólo en notas de ubicación de responsabilidades, y ahí va calificado.

### 3.3 `Pendiente`

**Declarado en el glosario raíz**, `Vision-Producto.md` §9.2, y en PRODUCT-INTAKE §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica en los artefactos de esta categoría, que es donde los dos referentes conviven con más densidad.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | `Definicion-Modelo-De-Dominio.md` §2.1 y §5.1, CU-01 a CU-04, RN-01, RN-06 |
| Estado de un **trabajo** enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el sujeto es el trabajo y está nombrado en la misma oración | `Definicion-Modelo-De-Dominio.md` §2.2 y §5.2, CU-05, CU-07 a CU-11, RN-04, RN-05, RN-10, RN-11 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo —«`Borrador`, `Pendiente`, `Finalizado` o `Rechazado`»—, donde el atributo enunciado ya fija el referente y calificar cada valor sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica; y los nombres de los códigos de rechazo, que son identificadores literales del contrato.

### 3.4 Rol

Los dos referentes conviven **en la misma tabla y hasta en la misma celda** —la tabla de actores de los doce casos de uso—, de modo que colisionan en el sentido de `Vocabulario-Rules.md` §9.2 y se desambiguan. El segundo referente no lo eligió este proyecto de código: viene impuesto por `Rules-Especificacion-Funcional.md` §4.2 punto 2, que fija el encabezado de esa tabla.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Atributo del alumno que vale `Alumno` o `Administrador` | **«papel»**, siempre. Nunca «rol», ni siquiera citando fuentes técnicas que lo llaman así | `Definicion-Modelo-De-Dominio.md` §2.1, CU-01, CU-02, CU-04, CU-10, CU-11, RN-01, RN-10 |
| Función que un actor cumple dentro de un caso de uso | **«rol»**, y **sólo** como encabezado de la columna de la tabla de actores, que es donde la regla lo impone | §2 «Actores» de los doce casos de uso |

La forma «rol» no se usa en prosa en ningún artefacto de esta categoría: en cuanto se sale del encabezado normativo, el término es «papel». La única ocurrencia en prosa es la transcripción literal del enunciado de INV-02 en `Definicion-Modelo-De-Dominio.md` §4.1 —«a un rol de alumno»—, que se conserva porque es cita del intake y se declara acá para que no se lea como una tercera acepción.

### 3.5 Dos casos que no son polisemia y no se corrigen

- **Observación** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1. La regla de uso que sí rige es de precisión: cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado corresponde «advertencia», y cuando se modela la entidad corresponde «observación».
- **Comentario** designa acá una sola cosa: el texto libre y opcional del administrador. No es una observación, no es una calificación, y no tiene relación con los comentarios que el validador tolera **dentro** del texto del alumno, que son sintaxis del dato de entrada y no aparecen en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica (`Vocabulario-Rules.md` §9.1).

## 4. Términos referenciados y no redefinidos

Los siguientes términos ya están declarados en `00-Contexto/Vision-Producto.md` §9 con la misma semántica con la que esta categoría los usa. Se referencian y no se redefinen; ninguna definición de §2 los pisa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 |
| Pieza, en su referente del dominio | `Vision-Producto.md` §9.1 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Componente | `Vision-Producto.md` §9.1 |
| Observación | `Vision-Producto.md` §9.1 |
| Advertencia | `Vision-Producto.md` §9.1 |
| Error de validación | `Vision-Producto.md` §9.1 |
| **Estado del trabajo**, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| **Enviar**, como única acción de guardado | `Vision-Producto.md` §9.1 |
| **Aprobar / Rechazar** | `Vision-Producto.md` §9.1 |
| **Comentario** | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio | `Vision-Producto.md` §9.1 |
| Actividad 1 | `Vision-Producto.md` §9.1 |
| `Describir()` | `Vision-Producto.md` §9.1 |
| Tapa | `Vision-Producto.md` §9.1 |
| Rectángulo desarrollado | `Vision-Producto.md` §9.1 |
| Coma final | `Vision-Producto.md` §9.1 |
| Fallo silencioso | `Vision-Producto.md` §9.1 |
| Punto de control | `Vision-Producto.md` §9.1 |
| Hito interno / hito demostrable | `Vision-Producto.md` §9.1 |
| **`Pendiente`, forma calificada obligatoria** | `Vision-Producto.md` §9.2 |
| Etapa | `Vision-Producto.md` §9.2 |
| Puerta técnica | `Vision-Producto.md` §9.2 |
| Capacidad | `Vision-Producto.md` §9.2 |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los trece términos que esta categoría acuña para el proyecto de código, los tres términos con más de un referente —trabajo, pieza y `Pendiente`— con la forma que corresponde a cada uno y la evidencia de colisión, el caso de superordinación de «observación» que deliberadamente no se corrige, y los veinte términos que se referencian del glosario raíz sin redefinirlos. |
| 1.1 | 2026-08-09 | Absorbe el vocabulario que `Vision-Producto.md` 1.1 §9 dio de alta con el circuito de revisión. Sube minor y archiva el estado anterior por `Master-Prompt.md` §5. **§2 devuelve al glosario raíz** el término «estado del trabajo», que esta categoría había acuñado y que ahora está declarado aguas arriba con sus cuatro valores, y da de alta **«desenlace»** y **«alcance del administrador»**, que sí acuña esta categoría. **§3.3 deja de declarar la polisemia de `Pendiente` por cuenta propia** y pasa a referenciar la declaración del glosario raíz §9.2, agregando sólo cómo se aplica acá y los dos usos que deliberadamente **no** se califican. **§3.4** suma «comentario» como segundo caso que no es polisemia, con sus contextos disjuntos verificados. **§4** suma los cuatro términos nuevos del glosario raíz y la entrada de `Pendiente`. **Corrección de la ronda r1 del audit, hallazgo P3-10**: «rol» tenía dos referentes conviviendo en la misma tabla y el glosario declaraba sólo uno, como alias descartado. Se agrega **§3.4** con los dos referentes y la forma que corresponde a cada uno —«papel» para el atributo del alumno, «rol» sólo como encabezado normativo de la tabla de actores que fija `Rules-Especificacion-Funcional.md` §4.2—, y la entrada «Papel» de §2 remite a ella. Los casos descartados pasan a §3.5. |
| 1.2 | 2026-08-09 | Alcanzado por la **corrección del P0** reportado por `B-02-03-GeometriaFactory-Application-r1.md`. §2 da de alta **«camino de alta»**, que es el concepto que la corrección hizo explícito —el auto-registro del alumno y la configuración del administrador, con estado inicial y credencial propios—, y precisa las entradas «papel», «estado de cuenta» y «credencial derivada», que quedaban ambiguas sobre cuál de los dos describían. Las listas de artefactos incorporan CU-12. |
| 1.3 | 2026-08-09 | Corrección de la ronda r3 del audit, informe `B-02-03-GeometriaFactory-Domain-r3.md`, hallazgo **H-07**. §3.4 conservaba dos «once casos de uso» en prosa viva —uno en el párrafo de evidencia de colisión y otro en la celda de ocurrencias—, que la emisión de CU-12 dejó desactualizados. |
