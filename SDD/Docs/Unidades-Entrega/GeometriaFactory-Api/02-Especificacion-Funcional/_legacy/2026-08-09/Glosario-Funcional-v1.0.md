> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `Glosario-Funcional.md` en su versión **1.0**, tomada el 2026-08-09 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-09
> - **Versión vigente:** [`Glosario-Funcional.md`](../../Glosario-Funcional.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# Glosario funcional — GeometriaFactory-Domain

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Domain
**Documento:** Glosario-Funcional.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-08
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `00-Contexto/Vision-Producto.md` §9 (glosario raíz de la cadena), §9.1, §9.2 y §9.3; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §12 y §12.1 (glosario del dominio del cliente y choque de vocabulario), §17.1
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de GeometriaFactory-Domain

---

## Tabla de contenido

- [1. Alcance de este glosario](#1-alcance-de-este-glosario)
- [2. Términos que esta categoría acuña](#2-términos-que-esta-categoría-acuña)
- [3. Términos con más de un referente](#3-términos-con-más-de-un-referente)
- [4. Términos referenciados y no redefinidos](#4-términos-referenciados-y-no-redefinidos)
- [5. Control de cambios](#5-control-de-cambios)

---

## 1. Alcance de este glosario

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Los términos que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2` (`Vision-Producto.md` §9.3).

## 2. Términos que esta categoría acuña

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Alumno | Entidad del dominio que representa a la persona de la comisión con identidad propia dentro del laboratorio y a la que pertenecen trabajos | `Definicion-Modelo-De-Dominio.md`, CU-01 a CU-04, CU-09, RN-01, RN-07 | — |
| Papel | Atributo del alumno que vale `Alumno` o `Administrador`. Es un conjunto cerrado de dos valores, sin permisos configurables | `Definicion-Modelo-De-Dominio.md`, CU-01, CU-02, CU-04, RN-01 | «Rol» en las fuentes técnicas. Se usa «papel» |
| Estado de cuenta | Atributo del alumno que vale `Pendiente`, `Habilitado` o `Bloqueado`, con transiciones declaradas | `Definicion-Modelo-De-Dominio.md`, CU-01 a CU-04, RN-01, RN-07 | — |
| Credencial derivada | Valor derivado de la contraseña del alumno, que el dominio recibe ya derivado y nunca en claro. Sin valor hasta el primer ingreso efectivo | `Definicion-Modelo-De-Dominio.md`, CU-03, CU-04 | «Hash de contraseña» en las fuentes técnicas |
| Admisibilidad de la cuenta | Resultado de evaluar si una cuenta admite acceso al laboratorio, con su motivo cuando no lo admite | `Definicion-Modelo-De-Dominio.md`, CU-04 | — |
| Baja de la cuenta | Operación destructiva e irreversible que elimina la cuenta y todos sus trabajos. No es un estado de cuenta | `Definicion-Modelo-De-Dominio.md`, CU-02, RN-07, RN-01 | «Baja física» en las fuentes |
| Estado del trabajo | Atributo del trabajo que vale `Borrador`, `Pendiente` o `Finalizado`, con transiciones declaradas | `Definicion-Modelo-De-Dominio.md`, CU-05, CU-08, CU-09, RN-04, RN-05 | — |
| Texto original | El texto que el alumno cargó, tal como lo emitió su programa. Se conserva íntegro y nunca se reescribe | `Definicion-Modelo-De-Dominio.md`, CU-05 a CU-08, RN-08 | «JSON original» en las fuentes técnicas. En esta categoría se dice «texto original», porque el dominio no conoce formatos de serialización |
| Posición de pieza | Lugar que una figura ocupa en el conjunto raíz del trabajo. **Es la identidad de la pieza**, porque el dato del alumno no trae identificador propio | `Definicion-Modelo-De-Dominio.md`, CU-06, CU-07, RN-09 | «Índice de figura» en las fuentes |
| Familia plana o volumétrica | Clasificación de una pieza que **se deriva de su tipo** y no se guarda como dato propio | `Definicion-Modelo-De-Dominio.md`, CU-06 | — |
| Especie de observación | Atributo de la observación que vale `Advertencia` o `Error de validación`, y que decide si la observación impide o no impide finalizar | `Definicion-Modelo-De-Dominio.md`, CU-07, CU-08, RN-05, RN-09 | «Severidad» en las fuentes técnicas |
| Consumidor de la biblioteca | El proyecto de código que usa la superficie pública de este dominio por referencia de proyecto de código: `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure`. **Es el actor primario de todos los casos de uso de esta categoría** | Los nueve CU | «Capa consumidora» |
| Sujeto de la regla | La persona sobre la que recae una regla que el dominio hace cumplir —el alumno, el administrador—, que **no** es actor de ningún caso de uso de esta categoría | Los nueve CU | — |

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

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Estado de cuenta de un alumno registrado y todavía no habilitado por el administrador | **«cuenta `Pendiente`»** o «estado de cuenta `Pendiente`» | Los dos valores se llaman igual y las dos máquinas de estado se describen en el mismo documento y se invocan desde casos de uso vecinos. Un subagente que lea una sección suelta no puede resolver a cuál apunta |
| Estado de un trabajo ya enviado por el alumno y todavía no finalizado | **«trabajo en estado `Pendiente`»** | Ídem |

La forma desnuda «`Pendiente`» no se usa en ninguna sección donde el sujeto no esté nombrado en la misma oración. Es la desambiguación más barata que resuelve el caso —entrada de glosario más forma calificada en las ocurrencias que colisionan— y no hace falta escalar a invariante de producto (`Vocabulario-Rules.md` §9.3).

### 3.4 Un caso que no es polisemia y no se corrige

**Observación** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1. La regla de uso que sí rige es de precisión: cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado corresponde «advertencia», y cuando se modela la entidad corresponde «observación». Calificar todas las ocurrencias del superordinado sería el falso positivo que `Vocabulario-Rules.md` §9.4 prohíbe.

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
| Etapa | `Vision-Producto.md` §9.2 |
| Puerta técnica | `Vision-Producto.md` §9.2 |
| Capacidad | `Vision-Producto.md` §9.2 |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Declara los trece términos que esta categoría acuña para el proyecto de código, los tres términos con más de un referente —trabajo, pieza y `Pendiente`— con la forma que corresponde a cada uno y la evidencia de colisión, el caso de superordinación de «observación» que deliberadamente no se corrige, y los veinte términos que se referencian del glosario raíz sin redefinirlos. |
