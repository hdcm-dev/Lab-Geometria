# Glosario funcional — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Glosario-Funcional.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Glosario-Funcional.md` y `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Glosario-Funcional.md` (vocabulario de las dos categorías de las que este proyecto de código depende); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.12** §4.2, §12, §12.1, §17.3
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Validador](#31-validador)
  - [3.2 Repositorio](#32-repositorio)
  - [3.3 Derivado](#33-derivado)
  - [3.4 `Pendiente`](#34-pendiente)
  - [3.5 Tres casos que no son polisemia y no se corrigen](#35-tres-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código y que aparecen en más de uno de sus artefactos. Los que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, y los que acuñan las categorías 02 de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Adaptador | La implementación concreta de un puerto, que vive en este proyecto de código. **Un adaptador provee el mecanismo y no toma decisiones de negocio** | `Especificacion-Funcional.md` §3 y §4, los diez CU | «Implementación del puerto». **No se dice «el puerto» cuando el sujeto es la implementación** |
| Almacén | El archivo único donde el producto guarda todo lo que sobrevive al apagado del proceso. Se nombra así, y **no «base de datos»**, cuando el sujeto es el lugar donde vive el dato de esta instancia | `Especificacion-Funcional.md` §4, `Modelo-Datos/`, CU-03, CU-04, CU-05, CU-10 | «Archivo del almacén» cuando el sujeto es el archivo en su ruta |
| Trampa del formato | Cada uno de los cuatro rasgos del texto real del alumno que rompen a un lector ingenuo: `T1` clave sinónima, `T2` comas finales, `T3` cara con dos nombres, `T4` valores calculados erróneos. **El contrato nace sabiéndolas** | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-01, CU-02 | «Las cuatro trampas», en forma corta |
| Lectura tolerante | La forma en que este proyecto de código lee el texto del alumno: admitiendo comas finales, omitiendo comentarios y aceptando las claves sinónimas. **No es permisividad**: es adaptarse al dato, que es premisa fija del producto | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-01 | — |
| Existencia contra veracidad | El criterio con el que se lee una dimensión: se comprueba que **el campo esté**, no que su valor tenga sentido geométrico. Un `0.00` presente **no descarta la figura** | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-01, CU-02 | — |
| Operador estricto | La forma de la comparación de valores: se advierte cuando la diferencia absoluta es **mayor** que la tolerancia, y **no** cuando es mayor o igual | `Definicion-Contrato-Del-Validador-De-Figuras.md`, CU-02 | — |
| Posición reservada | La posición, dentro del rango de figuras del conjunto raíz, que ocupa una figura que **no se pudo reconstruir**. Admite observación aunque no haya pieza, y **no se compacta** | CU-01, CU-03, `RC-02` | — |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las no reconstruidas. **Referenciada** del modelo del dominio; acá se agrega que **es la que este proyecto de código produce** | CU-01, CU-03, `Modelo-Datos/` | — |
| Contraseña provisoria | El valor de credencial que **este proyecto de código produce** cuando el administrador resetea la cuenta de un alumno. **Referenciada** de la capa de aplicación; acá se agrega que es donde nace y que **no se conserva** | CU-07, `RC-07` | «clave provisoria». **No se dice «contraseña temporal»**: no vence por tiempo, la levanta la marca |
| Valor derivado de la credencial | Lo que el producto guarda en lugar de la contraseña. **No es el «valor derivado» de la geometría**: ver §3.3 | CU-05, CU-06, CU-07 | «credencial derivada», que es la forma que usa la categoría de dominio |
| Acceso firmado | Lo que se emite para que la pieza pública opere contra la pieza de datos: identificador, correo, papel y expiración, firmado con clave simétrica | CU-08 | «el acceso». **No se dice «sesión»**: la pieza de datos es sin estado |
| Clave de firma | El secreto con el que el acceso se firma. **Vive fuera del repositorio de código y fuera de la imagen** | CU-08 | — |
| Terminación degradada | La forma en que termina una operación que no se pudo completar por una causa que no depende de lo que se pidió. **Se declara en vez de fingir un resultado**, y este proyecto de código **no reintenta** | CU-01, CU-03, CU-04, CU-05, CU-07 | «estado degradado», que es la forma que usa la capa de aplicación |
| Arranque detenido | La forma de terminación propia de la preparación del almacén: el servicio **no atiende ninguna petición**. Es preferible a operar sobre un almacén en el que no se puede confiar | CU-10 | — |
| Transformación de esquema | Cada paso versionado que lleva el almacén de una forma a la siguiente, **aplicado al arrancar**. Se versiona con el código de su etapa y **una ya fusionada no se edita** | CU-10, `Modelo-Datos/` | «migración», que es la forma que usan las fuentes técnicas. Se admite cuando el sujeto es la herramienta |
| Regla conceptual de modelo | Cada una de las siete condiciones que el dato guardado tiene que cumplir, con identificador `RC-XX`. **No es una regla de negocio**: declara cómo el dato sobrevive, no qué decidió el negocio | `Modelo-Datos/`, los diez CU | — |
| Segunda línea | El papel que cumplen las restricciones de unicidad del almacén frente a la consulta previa del consumidor: **la verificación previa no es una garantía por sí sola** | `Especificacion-Funcional.md` §4, CU-05 | — |

## 3. Términos con más de un referente

Los cuatro términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en todos ellos los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Validador

Es la polisemia propia de este proyecto de código, y nace de que acá conviven el contrato y la cosa que lo implementa.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El **puerto** que la capa de aplicación declara | **«puerto de validación de figuras»**, siempre completo | Los dos referentes aparecen en la misma sección de `Especificacion-Funcional.md` §3 y en la §9 de CU-01 y CU-02 |
| El **adaptador** que lo implementa acá | **«validador de figuras»**, o «el validador» cuando el complemento ya está fijado en la misma oración | La categoría de aplicación ya declara «validador de figuras» como la forma corta usada cuando el sujeto es la implementación, y acá el sujeto es siempre la implementación |

### 3.2 Repositorio

Heredada de la categoría de aplicación, que la declaró primero, y **acá gana un tercer referente**.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El puerto por el que se guarda y se recupera | **Siempre calificado**: «puerto de repositorio de trabajos», «puerto de repositorio de cuentas» | Declarado en la categoría de aplicación §3.1 y conservado |
| El adaptador que lo implementa | **«adaptador del repositorio de trabajos»** o «de cuentas» | Los dos primeros conviven en `Especificacion-Funcional.md` §3 |
| El repositorio de código donde vive el árbol del producto | **No se nombra «repositorio» a secas.** Se dice «repositorio de código» | Aparece en CU-08 —donde se declara que la clave de firma no entra— junto con los otros dos referentes |

### 3.3 Derivado

Es la colisión que más caro sale, porque las dos cosas son números que el sistema calcula y las dos aparecen en el mismo caso de uso del reseteo.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El valor de `Area` o de `Volumen` que el sistema recalcula desde las dimensiones | **«valor derivado»**, o «`Area` derivada» y «`Volumen` derivado» con su campo | Declarado en el glosario raíz. Domina CU-01, CU-02 y `RC-03` |
| El valor con el que el producto guarda una contraseña | **«valor derivado de la credencial»** o **«credencial derivada»**, nunca «valor derivado» a secas | Los dos aparecen en la misma sección de `Especificacion-Funcional.md` §4 y en el vocabulario de CU-05, CU-06 y CU-07 |

Regla de uso, en una línea: **«derivado» a secas designa la geometría; la credencial se nombra siempre con su complemento.**

### 3.4 `Pendiente`

**Declarado en el glosario raíz** y en el intake §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica acá.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada | **«cuenta `Pendiente`»** | CU-05, `RC-07`, `Modelo-Datos/` |
| Estado de un **trabajo** enviado, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el trabajo está nombrado en la misma oración | CU-01, CU-02, CU-03, CU-04 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo, donde el atributo enunciado ya fija el referente, y los nombres de los códigos, que son identificadores literales del contrato.

### 3.5 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo, y su relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en el glosario raíz. **El comentario del administrador no es una observación**: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala.
- **«Puerto»** designa acá una sola cosa: el contrato que la capa de aplicación declara y que este proyecto de código implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica; es la misma resolución que la categoría de aplicación declaró.
- **«Transformación»** aparece sólo en la forma compuesta «transformación de esquema» y no colisiona con nada: este proyecto de código **no transforma el dato del alumno**, y esa prohibición se enuncia con otras palabras —conservar íntegro, no reescribir— precisamente para que no parezcan lo mismo.

## 4. Términos referenciados y no redefinidos

### 4.1 Del glosario raíz de 00

Ya declarados con la misma semántica con la que esta categoría los usa: Trabajo; Pieza en su referente del dominio, y en su segundo referente siempre calificado; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de GeometriaFactory-Domain

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Texto original | El texto que el alumno cargó, conservado íntegro |
| Posición de pieza | Lugar de la figura en el conjunto raíz; es la identidad de la pieza |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y **no se guarda** (`RC-04`) |
| Especie de observación | Atributo que vale `Advertencia` o `Error de validación` |
| Desenlace | Término colectivo de aprobar y rechazar |
| Alcance del administrador | Los tres estados de trabajo que no son `Borrador` |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

### 4.3 Del glosario funcional de GeometriaFactory-Application

| Término | Qué designa, en una línea |
| --- | --- |
| Puerto | Contrato que aquella capa declara y que **ésta implementa** |
| Los cuatro puertos, uno por uno | Repositorio de trabajos, repositorio de cuentas, validación de figuras y reloj del sistema |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública de aquella capa |
| Verificación de pertenencia y verificación de facultad | Las dos comprobaciones que **no se hacen acá** |
| Alcance de consulta | El recorte que el caso de uso traslada al pedido **antes** de pedir |
| Unidad de trabajo | El tramo dentro del cual las escrituras ocurren enteras o no ocurren |
| Motivo | El valor de la enumeración cerrada con el que se explica que algo no procede |
| Doble | Implementación de prueba de un puerto. **Acá es lo que se reemplaza**, no lo que se escribe |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja sobre la cuenta y que sólo el cambio efectivo levanta |
| Metadato de orquestación | Los sellos de alta, de modificación y de desenlace, que aquella capa aporta al materializar |
| Camino de alta | Cada una de las dos vías por las que nace una cuenta, con reglas opuestas |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara los diecisiete términos que esta categoría acuña —entre ellos las cuatro trampas del formato, la lectura tolerante, el criterio de existencia contra veracidad, el operador estricto, la posición reservada, la terminación degradada, el arranque detenido y la regla conceptual de modelo—, los cuatro términos con más de un referente con la forma que corresponde a cada uno y su evidencia de colisión —«validador», que es la polisemia propia de esta capa, «repositorio», que gana acá un tercer referente, «derivado», que es la que más caro sale, y `Pendiente`—, los tres casos que deliberadamente no se corrigen, y los términos que se referencian del glosario raíz y de los glosarios de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` sin redefinirlos. |
| 1.1 | 2026-08-10 | Actualización de la cita del `PRODUCT-INTAKE` de **1.11** a **1.12** en la trazabilidad upstream: 1.11 quedó archivada al resolver el Product Owner el desenlace del envío del escenario `E-8`. Corrige el hallazgo **H-02** del informe de auditoría `SDD/Docs/Audit/B-02-03-GeometriaFactory-Infrastructure-r1.md` (ronda 1). El delta entre 1.11 y 1.12 se revisó y sólo alcanza a `E-8`, que no toca lo que este documento declara: sin cambios de contenido. |
