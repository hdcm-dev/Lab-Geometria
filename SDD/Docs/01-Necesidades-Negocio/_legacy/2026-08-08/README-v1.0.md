> **Artefacto archivado — estado `Superado`**
>
> Esta es una **copia archivada** del documento `README.md` en su versión **1.0**, tomada el 2026-08-08 por el orquestador SDD antes de que la versión vigente la superara (`Master-Prompt.md` §5 y §5.1).
>
> - **Estado:** `Superado`
> - **Versión que preserva:** 1.0
> - **Fecha de archivado:** 2026-08-08
> - **Versión vigente:** [`README.md`](../../README.md)
>
> El cuerpo que sigue **no se modifica**: un registro que se corrige después deja de ser un registro. Este archivo no se renombra, no se reenlaza y no vuelve a tocarse.

---

# 01 · Necesidades de Negocio

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | README.md |
| Versión | 1.0 |
| Estado | Propuesto |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | `Necesidades-Negocio.md` (índice maestro de esta categoría); `00-Contexto/Vision-Producto.md` §2 (stakeholders), `00-Contexto/README.md` §4 |
| Trazabilidad downstream | 02-Especificacion-Funcional, 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Las ocho necesidades de negocio](#2-las-ocho-necesidades-de-negocio)
- [3. Mapa de dependencias](#3-mapa-de-dependencias)
- [4. Orden de lectura sugerido](#4-orden-de-lectura-sugerido)
- [5. RACI por necesidad](#5-raci-por-necesidad)
- [6. Notas de uso de esta sección](#6-notas-de-uso-de-esta-sección)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Qué hay en esta carpeta

| Documento | Propósito |
| --- | --- |
| `Necesidades-Negocio.md` | Índice maestro: catálogo, criterio de recorte con sus fusiones y particiones, mapa de dependencias y trazabilidad agregada. **Es el punto de entrada** |
| `Necesidades-De-Negocio/NB-XX-<Nombre>.md` | Una necesidad por archivo, con sus diez secciones obligatorias |
| `README.md` | Este archivo: índice navegable, orden de lectura y RACI |

Este `README.md` existe porque el catálogo tiene más de cinco necesidades. No sustituye al índice maestro: lo complementa con el orden de lectura y con la asignación de responsabilidades.

## 2. Las ocho necesidades de negocio

| NB | Título | Impacto | Prioridad MoSCoW | Estado | Enlace |
| --- | --- | --- | --- | --- | --- |
| NB-01 | Control de admisión y de bajas del laboratorio | La comisión queda delimitada y el docente sabe quiénes están habilitados, bloqueados o fuera. Sin ella ninguna capacidad posterior tiene fundamento | Must Have | Propuesto | [NB-01](../../Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) |
| NB-02 | Identidad propia del alumno sin canal de correo | El trabajo pasa a tener dueño, que es la condición previa a toda entrega. Ninguna credencial se transporta por ningún canal | Must Have | Propuesto | [NB-02](../../Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) |
| NB-03 | Trabajo con dueño, estado y persistencia | El esfuerzo de la Actividad 1 deja de terminar en un portapapeles y se convierte en una entrega verificable con estado e historial | Must Have | Propuesto | [NB-03](../../Necesidades-De-Negocio/NB-00003-Trabajo-Con-Dueno-Estado-Y-Persistencia.md) |
| NB-04 | Interpretación fiel del dato del alumno, con el error localizado | El producto sirve para el dato que existe y no para uno ideal; desaparece el fallo silencioso y el alumno recibe figura y campo del defecto | Must Have | Propuesto | [NB-04](../../Necesidades-De-Negocio/NB-00004-Interpretacion-Fiel-Del-Dato-Del-Alumno.md) |
| NB-05 | Visibilidad del error de cálculo sobre el trabajo propio | El error de fórmula se hace visible sobre el trabajo del propio alumno, que es el único lugar donde tiene valor didáctico | Must Have | Propuesto | [NB-05](../../Necesidades-De-Negocio/NB-00005-Visibilidad-Del-Error-De-Calculo.md) |
| NB-06 | Visualización del trabajo dentro del producto | Desaparece el corte entre modelar y ver, y los ortoedros se dibujan por primera vez | Must Have | Propuesto | [NB-06](../../Necesidades-De-Negocio/NB-00006-Visualizacion-Dentro-Del-Producto.md) |
| NB-07 | Revisión de la comisión desde un solo lugar | La revisión pasa de ser una tarea alumno por alumno a una sola sesión de trabajo del docente | Must Have | Propuesto | [NB-07](../../Necesidades-De-Negocio/NB-00007-Revision-De-La-Comision-En-Un-Solo-Lugar.md) |
| NB-08 | Alcance del laboratorio desde el aula | El producto está disponible en el único escenario de uso previsto. Sin ella, el valor entregado por las otras siete es cero | Should Have | Propuesto | [NB-08](../../Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) |

## 3. Mapa de dependencias

| NB | Depende de | Cantidad | Es prerequisito de |
| --- | --- | --- | --- |
| NB-01 | — | 0 | NB-02, NB-07 |
| NB-02 | NB-01 | 1 | NB-03 |
| NB-03 | NB-02 | 1 | NB-04, NB-07 |
| NB-04 | NB-03 | 1 | NB-05, NB-06 |
| NB-05 | NB-04 | 1 | — |
| NB-06 | NB-04 | 1 | NB-07 |
| NB-07 | NB-01, NB-03, NB-06 | 3 | — |
| NB-08 | — | 0 | — |

El grafo es acíclico y ninguna necesidad supera las tres dependencias. La verificación del orden topológico está en `Necesidades-Negocio.md` §4 y no se repite acá.

## 4. Orden de lectura sugerido

Las dependencias son fuertes: siete de las ocho necesidades forman una cadena, y leerlas fuera de orden obliga a volver atrás.

1. **`Necesidades-Negocio.md`** — primero siempre. Da el criterio de recorte y la trazabilidad agregada, y sin él las ocho necesidades se leen como una lista sin fundamento.
2. **NB-01 y NB-02** — la identidad: quién entra al laboratorio y cómo obtiene su credencial. Son la raíz de la cadena.
3. **NB-03** — el trabajo como unidad con dueño, estado y persistencia. Todo lo que sigue opera sobre él.
4. **NB-04**, y a continuación **NB-05** y **NB-06**, que son sus dos ramas: la verificación de valores y la visualización.
5. **NB-07** — la revisión, que converge sobre NB-01, NB-03 y NB-06.
6. **NB-08** — se puede leer suelta: no depende de ninguna otra y su dolor es de acceso, no funcional. Conviene leerla temprano si el lector viene de la categoría 09.

## 5. RACI por necesidad

Los cinco stakeholders del producto están declarados en `Vision-Producto.md` §2 y en `00-Contexto/README.md` §4. Product Owner, equipo de desarrollo y administrador del laboratorio **son la misma persona en papeles distintos**, y por eso el RACI se expresa por papel y no por nombre: es la separación de papeles la que da sentido a la revisión.

| NB | Propietario (aprueba) | Implementador (construye) | Beneficiario (valida el valor) | Revisor |
| --- | --- | --- | --- | --- |
| NB-01 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Docente, en su papel de administrador del laboratorio | Product Owner, en el punto de control de las etapas `c` y `d` |
| NB-02 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión | Product Owner, en el punto de control de la etapa `d` |
| NB-03 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión | Product Owner, en el punto de control de la etapa `e` |
| NB-04 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión | Product Owner, en el punto de control de la etapa `f` |
| NB-05 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión, y la cátedra como dueño del problema | Product Owner, en el punto de control de la etapa `f` |
| NB-06 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión y administrador del laboratorio | Product Owner, en el punto de control de la etapa `g` |
| NB-07 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Docente, en su papel de administrador del laboratorio | Product Owner, en los puntos de control de las etapas `e` y `g` |
| NB-08 | Docente, en su papel de Product Owner | Docente, en su papel de equipo de desarrollo | Alumno de la comisión, que accede desde el aula | Product Owner, en los puntos de control de las etapas `a` y `h` |

La consulta permanente en las ocho necesidades es la **cátedra de Programación 2**, como dueño del problema.

## 6. Notas de uso de esta sección

- **Autoridad.** Ninguna necesidad origina una prioridad, un target ni una exclusión. Todo se deriva de `PRODUCT-INTAKE` y de `00-Contexto/`, y traza a su sección de origen.
- **Nivel de abstracción.** Una necesidad enuncia un problema del negocio. Los flujos paso a paso pertenecen a la categoría 02 y las historias de usuario a la 06; ninguna de las dos formas aparece en estos archivos.
- **Vocabulario.** Los términos del dominio no se redefinen acá: están declarados en `Vision-Producto.md` §9, que es el glosario raíz de la cadena. La palabra «proyecto» a secas no se usa en ninguna documentación de este producto.
- **Valores pendientes de confirmación.** Dos targets provienen de la asunción A-2 del intake y esperan confirmación del Product Owner; están identificados en `Necesidades-Negocio.md` §6.
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera.

## 7. Control de cambios

| Versión | Fecha | Cambios | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial del índice de la sección. Enumera las ocho necesidades con su impacto, prioridad, estado y enlace, reproduce el mapa de dependencias, fija el orden de lectura por la cadena de dependencias y asigna el RACI por papel, con la aclaración de que propietario, implementador y beneficiario operador son la misma persona en papeles distintos. | Analista de Negocio Senior (AG-01) |
