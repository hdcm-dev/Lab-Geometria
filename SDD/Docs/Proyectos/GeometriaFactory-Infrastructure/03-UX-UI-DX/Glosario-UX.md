# Glosario de la sección 03 — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Glosario-UX.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena); `02-Especificacion-Funcional/Glosario-Funcional.md` §2, §3 y §4; `02-Especificacion-Funcional/Especificacion-Funcional.md` §3, §4 y §11; `02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md` §2, §4 y §8; `02-Especificacion-Funcional/Modelo-Datos/`; §6 de CU-01 a CU-10; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Glosario-Funcional.md` §2 y `03-UX-UI-DX/Glosario-UX.md` §2; `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Glosario-Funcional.md` §2; `Vocabulario-Rules.md` §2, §4 y §9
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas`, `09-Devops` y `11-Documentacion` de GeometriaFactory-Infrastructure

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
  - [3.1 Error](#31-error)
  - [3.2 Atajo](#32-atajo)
  - [3.3 Tres casos que no son polisemia y no se corrigen](#33-tres-casos-que-no-son-polisemia-y-no-se-corrigen)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) declara lo que la categoría 02 acuña para este proyecto de código, incluidas las cuatro trampas del formato, la lectura tolerante, el operador estricto, la terminación degradada y el arranque detenido.
- Los glosarios de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` declaran el vocabulario de las dos capas de las que este proyecto de código depende.

Ninguna entrada de §2 pisa a ninguna de esas fuentes. Lo único que se acuña acá es el vocabulario de **quien interviene sobre este proyecto de código** y del **recorrido de implementación y de despliegue**.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —salvo en las enumeraciones del conjunto cerrado y en los identificadores literales de los códigos—, «pieza» va desnuda en su referente del dominio y calificada en su referente de artefacto desplegable, **«repositorio» a secas no se escribe**, **«derivado» a secas designa la geometría**, «trabajo» no es «unidad de entrega», y **la palabra «proyecto» a secas no se usa**.

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 03 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Superficie pública de la capa de infraestructura | El conjunto de lo que se implementa contra este proyecto de código. **No es propia**: tiene la forma de los contratos que otra capa declaró, más dos mecanismos y una responsabilidad de arranque | Los tres | «Superficie pública» a secas cuando el proyecto de código está nombrado |
| Condición de error del adaptador | Cada una de las **17** situaciones catalogadas en las que un contrato de esta capa no puede hacer lo que se le pidió. Se identifica por un **código**. **No es un resultado**: ver §3.1 | Los tres | «Condición de error» cuando el proyecto de código está nombrado |
| Resultado que no es una condición | Cada una de las **siete** situaciones que parecen un fallo y son el funcionamiento normal del producto: el error de validación, el texto ilegible, cero advertencias, nada encontrado, el conjunto vacío, la credencial que no coincide y el acceso vencido | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| **Atajo prohibido** | Cada una de las tres salidas que un implementador apurado tomaría cuando algo del mundo no responde, y que **no fallan**: dejan el sistema funcionando y equivocado. Tienen métrica propia, con objetivo **cero** | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md`, `DX-Developer-Experience.md` | «Los tres atajos». **No se dice «mitigación»**: no hay nada que mitigar, hay algo que no se hace |
| Falla hacia el lado seguro | La propiedad que las condiciones de los atajos prohibidos sostienen: **cuando el mecanismo no puede cumplir su promesa, se detiene y lo dice; no la cumple a medias** | `DX-Error-Messages.md`, `Guia-Onboarding-Developer.md` | — |
| Defecto que no falla | El patrón que agrupa a los atajos prohibidos y a las tres reglas cuyo tramo principal vive acá: **se rompen produciendo algo válido**, de modo que ninguna prueba los encuentra si no está escrita a propósito | `Guia-Onboarding-Developer.md` §7, `DX-Developer-Experience.md` §1.4 | — |
| Operador del despliegue | El rol de intervención de quien arranca el contenedor de la pieza de datos **a mano**. **Existe acá y no en las capas de adentro**, y es a quien le hablan seis de las diecisiete condiciones | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «El operador» cuando el rol ya está nombrado. Acá lo encarna el docente |
| Implementador de adaptadores | El rol de intervención de quien escribe la implementación de un puerto que la capa de aplicación declaró | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Mantenedor de la capa | El rol de intervención de quien sostiene este proyecto de código y vuelve sobre él sin el contexto de la etapa en que lo escribió. Acá lo encarnan una persona y un agente de IA | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Rol de intervención | Quién interviene sobre este proyecto de código, como tipo. **No es la persona objetivo del producto**, que es el alumno o el administrador | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | El término de la variante DX |
| Diagnóstico accionable | La tercera parte obligatoria de toda entrada del catálogo: qué hacer al respecto. Acá dice además **de qué lado hacerlo**, porque la mitad de las condiciones se corrigen en el despliegue y no en el código | `DX-Error-Messages.md`, `DX-Developer-Experience.md` | «Acción sugerida», que es el nombre de la columna |
| Categoría de error | Cada uno de los siete grupos en que el catálogo ordena las condiciones. **Dos están vacías acá** —conflicto de facultad y conflicto de alcance— y su vacío es informativo | `DX-Error-Messages.md` | «Taxonomía» para el conjunto |
| Categoría vacía | Grupo de la taxonomía sin ninguna condición en este proyecto de código, **declarado con su motivo** en lugar de omitido | `DX-Error-Messages.md` §2.2 | — |
| Tramo de onboarding | Cada uno de los tres cortes del recorrido —5 minutos, 30 minutos, 1 hora—, cada uno con un objetivo **verificable** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Quick-start | La secuencia mínima y reproducible que produce el primer resultado exitoso. Ocurre entera **dentro del entorno de desarrollo contenido** | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| Primer resultado exitoso | Acá, **la batería del validador en verde sobre los textos reales de los escenarios**, sin almacén, sin red y sin secreto | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | — |
| TTFS | *Time-to-first-success*: tiempo desde abrir el repositorio de código hasta el primer resultado exitoso | `DX-Developer-Experience.md` | — |
| TTFV | *Time-to-first-value*: tiempo hasta haber corrido la batería obligatoria y saber qué prueba cada caso | `DX-Developer-Experience.md` | — |
| Modo de documentación | Cada uno de los cuatro modos de Diátaxis, con su ubicación declarada en un artefacto concreto de la cadena | `DX-Developer-Experience.md`, `Guia-Onboarding-Developer.md` | «Modo Diátaxis» |

## 3. Términos con más de un referente

Los dos términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los dos, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Error

Es la colisión central de esta sección, igual que en las dos capas hermanas, **y acá tiene un referente más que ninguna de las dos**: el defecto del despliegue.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La situación en la que un contrato de esta capa no puede hacer lo que se le pidió. No se guarda en ninguna parte | **«condición de error del adaptador»**, o **«código»** cuando se nombra el identificador. La forma desnuda «error» **no se usa** para este referente | `DX-Error-Messages.md` §1.2 y §3 hablan de varios referentes en la misma sección |
| El defecto que impide interpretar el texto del alumno, que es **una de las dos especies de observación** | **«error de validación», siempre completo.** Es entidad del dominio, es un **resultado**, y **no está en el catálogo** | Declarado en el glosario raíz. `DX-Error-Messages.md` §1.2 pone los dos referentes en la misma tabla |
| Lo que falta o no responde **en el despliegue**: el volumen sin montar, la clave sin proveer, el esquema divergente | **«defecto del despliegue»**, y las categorías del catálogo se llaman «error transitorio» y «error interno» sólo como nombre de categoría, donde el encabezado ya fija el referente | `DX-Developer-Experience.md` §1.1 y `Guia-Onboarding-Developer.md` §4 hablan de los tres referentes en la misma sección |
| Una falla no declarada del proyecto de código | **«defecto».** No es una condición del catálogo: su lugar es una prueba que falla | `DX-Error-Messages.md` §2.1 |

Regla de uso, en una línea: **«error» a secas no se escribe en esta sección**. Las formas son «condición de error del adaptador», «error de validación», «defecto del despliegue» y «defecto».

Y la distinción que completa el cuadro: **el error de validación es lo que el alumno tiene que ver, y ninguna de las otras tres lo es.** Confundirlos produce un producto que le grita al alumno por hacer bien su trabajo.

### 3.2 Atajo

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada una de las tres salidas prohibidas de §2, que dejan el sistema funcionando y equivocado | **«atajo prohibido»**, o «el atajo» cuando la condición ya está nombrada en la misma oración | `Guia-Onboarding-Developer.md` §7 y `DX-Error-Messages.md` §2.4 usan los dos referentes en la misma sección |
| Un camino de lectura abreviado dentro de la documentación | **No se nombra «atajo».** Se dice «orden de lectura» o «punto de entrada» | Los dos aparecerían en las mismas secciones de orden de lectura, y por eso el segundo se evita |

### 3.3 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es exactamente el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. Lo que sí colisiona es «error», y está resuelto en §3.1. **El comentario del administrador no es una observación.**
- **Los nombres de los códigos son identificadores literales del contrato** y no se califican ni se traducen. Se escriben en mayúsculas y sin acentos, y la misma excepción alcanza a las enumeraciones del conjunto cerrado de estados.
- **«Migración»** aparece en las fuentes técnicas del producto con el sentido de transformación de esquema, y esta sección usa **«transformación de esquema»** en prosa. **No es una polisemia a corregir**: es una elección de forma de la categoría 02, que admite «migración» cuando el sujeto es la herramienta, y los contextos no se cruzan.

## 4. Términos referenciados y no redefinidos

Los siguientes aparecen en los artefactos de esta sección con la misma semántica con la que ya están declarados aguas arriba. Se referencian y no se redefinen; ninguna entrada de §2 los pisa.

### 4.1 Del glosario raíz de 00

Trabajo; Pieza en sus dos referentes, el segundo siempre calificado; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de 02 de este proyecto de código

Todos declarados en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) §2 y §3. **Esta sección los usa sin excepción con la misma semántica y no redefine ninguno.**

| Término | Qué designa, en una línea |
| --- | --- |
| Adaptador | La implementación concreta de un puerto, que vive en este proyecto de código |
| Almacén | El archivo único donde el producto guarda lo que sobrevive al apagado del proceso |
| Trampa del formato | Cada uno de los cuatro rasgos del texto real del alumno que rompen a un lector ingenuo |
| Lectura tolerante | Admitir comas finales, omitir comentarios y aceptar las claves sinónimas |
| Existencia contra veracidad | Comprobar que el campo esté, no que su valor tenga sentido geométrico |
| Operador estricto | Advertir cuando la diferencia es **mayor** que la tolerancia, y no cuando es mayor o igual |
| Posición reservada | La posición de una figura que no se pudo reconstruir, que no se compacta |
| Cantidad de figuras del conjunto raíz | Cuántas trae el texto interpretado; **no es derivable de las piezas adoptadas** |
| Contraseña provisoria | El valor que este proyecto de código **produce** cuando el administrador resetea |
| Valor derivado de la credencial | Lo que el producto guarda en lugar de la contraseña. **No es el «valor derivado» de la geometría** |
| Acceso firmado, Clave de firma | Lo que se emite para operar contra la pieza de datos, y el secreto con el que se firma |
| Terminación degradada | La forma de terminar de una operación que no se pudo completar por causa del mundo |
| Arranque detenido | La forma propia de la preparación del almacén: el servicio no atiende ninguna petición |
| Transformación de esquema | Cada paso versionado que lleva el almacén de una forma a la siguiente |
| Regla conceptual de modelo | Cada una de las siete condiciones que el dato guardado tiene que cumplir. **No es una regla de negocio** |
| Segunda línea | El papel de las restricciones de unicidad del almacén frente a la consulta previa del consumidor |
| Validador, con sus dos referentes | `Glosario-Funcional.md` §3.1 |
| Repositorio, con sus tres referentes | `Glosario-Funcional.md` §3.2 |
| Derivado, con sus dos referentes | `Glosario-Funcional.md` §3.3 |

### 4.3 De los glosarios de GeometriaFactory-Domain y GeometriaFactory-Application

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Texto original, Posición de pieza, Especie de observación | El vocabulario del trabajo interpretado |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y **no se guarda** |
| Desenlace, Alcance del administrador | Las dos nociones que gobiernan el cierre del circuito |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |
| Puerto, y los cuatro puertos por su nombre | Los contratos que la capa de aplicación declara y **ésta implementa** |
| Verificación de pertenencia, verificación de facultad | Las dos comprobaciones que **no se hacen acá** |
| Alcance de consulta, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación. **El doble es lo que acá se reemplaza**, no lo que se escribe |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja sobre la cuenta y que sólo el cambio efectivo levanta |
| Metadato de orquestación | Los sellos de alta, de modificación y de desenlace, que aquella capa aporta al materializar |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara los diecinueve términos que esta categoría acuña —entre ellos el **atajo prohibido**, la **falla hacia el lado seguro**, el **defecto que no falla**, el **operador del despliegue** que las capas hermanas declaran no aplicable, el **resultado que no es una condición** y la **categoría vacía**—; los dos términos con más de un referente con su forma obligatoria y su evidencia de colisión —«error», que acá tiene **un referente más que en las capas hermanas**, el defecto del despliegue, y «atajo»—; los tres casos que deliberadamente no se corrigen; y los términos referenciados del glosario raíz, del glosario funcional de 02 de este proyecto de código y de los de `GeometriaFactory-Domain` y `GeometriaFactory-Application`, que no se redefinen. |
