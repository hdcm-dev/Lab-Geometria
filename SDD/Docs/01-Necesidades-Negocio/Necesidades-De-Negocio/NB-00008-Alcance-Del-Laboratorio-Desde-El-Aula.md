# NB-00008 — Alcance del laboratorio desde el aula

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md |
| Versión | 1.1 |
| Estado | Aprobado |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §4 (capacidad F-14), §7 (casos límite CL-2, CL-8 y CL-9), §10 (restricciones de red, de servidor propio y de despliegue manual), §11 (riesgos RN-B1, RN-B2 y RN-B4), §15 (puertas técnicas y dónde se miden); `Vision-Producto.md` §7 y §8; `Alcance-Producto.md` §2.2, §4.2 y §6.2; `Compatibilidad-Plataformas.md` §1.2 y §5 |
| Trazabilidad downstream | `CU-00009`, `CU-00011`, `CU-06010` en `GeometriaFactory-Api`; `CU-10010` en `GeometriaFactory-Web` (emitidos en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

---

## Tabla de contenido

- [1. Descripción de la necesidad](#1-descripción-de-la-necesidad)
- [2. Ejemplo de uso desde la perspectiva del negocio](#2-ejemplo-de-uso-desde-la-perspectiva-del-negocio)
- [3. Impacto](#3-impacto)
- [4. Problema específico que resuelve](#4-problema-específico-que-resuelve)
- [5. Criterios de éxito](#5-criterios-de-éxito)
- [6. Stakeholders involucrados](#6-stakeholders-involucrados)
- [7. Trazabilidad a CU](#7-trazabilidad-a-cu)
- [8. Dependencias con otras NB](#8-dependencias-con-otras-nb)
- [9. Prioridad MoSCoW](#9-prioridad-moscow)
- [10. Control de cambios](#10-control-de-cambios)

---

## 1. Descripción de la necesidad

El escenario de uso previsto del laboratorio es el aula, y la red desde la que trabajan los alumnos bloquea el acceso al servidor del docente. Es la restricción que ordena la forma entera del producto y también el riesgo de mayor impacto declarado: si los alumnos no alcanzan el laboratorio desde la facultad, el laboratorio no existe (PRODUCT-INTAKE §11, RN-B1). La necesidad de negocio no es de infraestructura: es que el producto esté efectivamente disponible en el único momento y lugar donde se lo va a usar.

De esa restricción se sigue la segunda mitad del problema. El lugar público al que sí se llega desde la facultad no conserva los datos, así que las dos piezas desplegables del producto viven separadas y una de ellas —la pieza de datos— depende de un servidor domiciliario sin dirección fija, cuyo despliegue ejecuta el docente a mano (PRODUCT-INTAKE §10). Eso implica que el laboratorio puede quedar sin datos durante una clase por un corte de luz o de conexión, o porque cambió la dirección del servidor (PRODUCT-INTAKE §7, CL-8 y CL-9). No hay alta disponibilidad ni la va a haber: es un laboratorio de aula.

Lo que el negocio sí necesita es que esa indisponibilidad **nunca se le presente al alumno como un error sin explicación**. Si el servicio de datos no responde, la persona tiene que ver un estado explícito que le diga que el laboratorio no tiene los datos en este momento, y no una pantalla rota (PRODUCT-INTAKE §7, CL-2). La necesidad tiene además un componente de oportunidad: la verificación de que la facultad alcanza el producto, y las mediciones de viabilidad del lugar público, valen mucho más medidas temprano que al final, porque su resultado puede cambiar decisiones que ya se tomaron (PRODUCT-INTAKE §15).

## 2. Ejemplo de uso desde la perspectiva del negocio

Es la primera clase en la que se usa el laboratorio. Los alumnos abren la dirección del laboratorio desde las máquinas del aula y entran: la comisión entera trabaja durante las dos horas sin que nadie quede afuera. A mitad de la clase siguiente se corta la luz en la casa del docente. Los alumnos que estaban cargando trabajos no ven una pantalla rota: ven un aviso explícito de que el laboratorio no está pudiendo acceder a los datos en este momento. La clase pierde el laboratorio ese día, cosa que el docente ya sabía que podía pasar y que aceptó; nadie pierde tiempo tratando de averiguar si el problema es suyo.

## 3. Impacto

- Si se resuelve: el laboratorio está disponible en el escenario de uso para el que fue construido, que es la clase.
- Si se resuelve: la premisa que ordena la partición del producto queda verificada en campo y deja de ser una suposición.
- Si se resuelve: una caída del servidor propio se presenta como una situación comprensible y acotada, no como una falla del producto.
- Si queda sin resolver: todo lo construido en las otras ocho necesidades queda inaccesible para sus destinatarios, y el valor entregado es cero.
- Si queda sin resolver tarde: descubrir el bloqueo al final obliga a rehacer decisiones ya tomadas, con un costo que crece con cada etapa cerrada.
- Riesgo residual aceptado: no hay alta disponibilidad, y una clase puede quedarse sin laboratorio por un corte en el servidor del docente (PRODUCT-INTAKE §11, RN-B4).

## 4. Problema específico que resuelve

- La red desde la que trabajan los alumnos bloquea el acceso al servidor donde viven los datos.
- El lugar público al que sí se llega no conserva los datos, de modo que ninguna de las dos piezas desplegables resuelve el problema por sí sola.
- No se sabe todavía si el lugar público sostiene el producto, y esa incógnita condiciona decisiones que se toman antes de resolverla.
- Un cambio de dirección del servidor propio deja al laboratorio apuntando a la nada hasta que el docente vuelva a publicarlo.
- Una caída del servicio de datos podría presentarse al alumno como una pantalla rota, sin explicación.
- Una verificación de acceso hecha al final resulta mucho más cara de atender que la misma verificación hecha temprano.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Acceso verificado desde la red de la facultad | Intentos de acceso al laboratorio desde la red de la facultad que resultan exitosos, sobre los intentos de la verificación de campo | 100 % | Punto de control de la etapa `i`, con la verificación de campo declarada |
| Mediciones de viabilidad ejecutadas temprano | Mediciones de viabilidad documentadas antes de construir sobre el lugar público, sobre las 5 declaradas: las 4 partes de la primera puerta técnica más la de construcción y arranque del servicio de datos | 5 de 5 | Punto de control de la etapa `a` |
| Estabilidad de la sesión en el lugar público | Minutos de navegación continua sin que el proceso recicle la sesión de la persona | ≥ 20 min | Punto de control de la etapa `a` |
| Indisponibilidad explicada, nunca rota | Fallos del servicio de datos que se le presentan a la persona como error sin explicación en lugar de como estado degradado explícito | 0 | Punto de control de la etapa `a`, y en cada punto de control posterior por la regla de no regresión |
| Despliegue verificado de las dos piezas desplegables | Piezas desplegables del producto verificadas en su destino real, sobre las 2 que lo componen: la pieza pública y la pieza de datos | 2 de 2 | Punto de control de la etapa `i` |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §11 (RN-B1), §15 (puerta técnica de acceso desde la facultad) y §4 (F-14); el segundo y el tercero, de PRODUCT-INTAKE §15 y de la transición `a` a `b` de `Roadmap-Producto.md` §5.2 —el intake declara explícitamente que los 20 minutos no son una asunción sino un valor tomado de las fuentes—; el cuarto, de PRODUCT-INTAKE §7 (CL-2 y CL-8) y §11 (RN-B4); el quinto, de PRODUCT-INTAKE §4 (F-14) y §10. Ninguno depende de la asunción A-2 del intake.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Declaró la restricción de red como premisa del producto y aceptó por escrito la ausencia de alta disponibilidad; valida el punto de control de la etapa `i` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que el laboratorio esté disponible en la clase, que es el único escenario de uso previsto |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Ejecuta las mediciones de viabilidad en la primera etapa y documenta su resultado antes de construir encima |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Ejecuta el despliegue a mano y vuelve a publicar cuando cambia la dirección del servidor propio |
| Alumno de la comisión | Beneficiario | Alcanza el laboratorio desde las máquinas del aula y, cuando los datos no están, recibe un aviso explícito en lugar de una pantalla rota |

## 7. Trazabilidad a CU

| NB | Casos de uso emitidos | Estado |
| --- | --- | --- |
| NB-00008 | `CU-00009`, `CU-00011`, `CU-06010` en `GeometriaFactory-Api`; `CU-10010` en `GeometriaFactory-Web` verificar el acceso al laboratorio desde la red de la facultad | Emitidos |
| NB-00008 | `CU-00009`, `CU-00011`, `CU-06010` en `GeometriaFactory-Api`; `CU-10010` en `GeometriaFactory-Web` presentar el estado degradado cuando el servicio de datos no responde | Emitidos |

## 8. Dependencias con otras NB

- Depende de: sin dependencias. No consume el resultado de ninguna otra NB: verifica y sostiene la disponibilidad del producto entero.
- Es prerequisito de: ninguna NB del alcance comprometido. La verificación de acceso desde la facultad se sitúa en la etapa `i`, posterior al cierre del alcance comprometido, y las mediciones de viabilidad de la etapa `a` condicionan decisiones de construcción, no el contenido de las otras necesidades.

## 9. Prioridad MoSCoW

**Should Have.** Se deriva de PRODUCT-INTAKE §4, donde la capacidad F-14 está declarada Should Have; es la única capacidad que esta NB agrupa, de modo que no hay agregación de prioridades distintas. La prioridad menor corresponde al despliegue real verificado, que el intake ubica en la etapa `i`; las mediciones de viabilidad de la etapa `a` que esta NB también recoge no son una capacidad del alcance sino condiciones declaradas de la planificación (PRODUCT-INTAKE §15).

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de que el laboratorio esté disponible en el aula a partir de la capacidad F-14 y de los riesgos RN-B1, RN-B2 y RN-B4 del intake, con cinco criterios de éxito trazados a su sección de origen y dos casos de uso previstos. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgo **H-01**: las ocurrencias de «pieza» que designan a un artefacto desplegable pasan a la forma calificada que exige el glosario raíz (`Vision-Producto.md` §9.2), «pieza desplegable», «pieza pública» y «pieza de datos», para que no colisionen con el referente del dominio —cada figura del trabajo— que NB-00006 §5 cuenta en su primer criterio. Alcanza a §1, §4 y al quinto criterio de §5, incluido su título. Se retiran además los tres usos de «mitad» que designaban ese mismo referente, sinónimo informal que el glosario prohíbe; se conservan los dos usos corrientes de la palabra, que no designan una pieza desplegable. Ningún target, plazo ni trazabilidad cambia. La corrección se absorbe **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
| 1.1 | 2026-08-08 | Corre el plazo de sus dos criterios de despliegue de la etapa `h` a la **`i`**, y con ellos las referencias de §6, §8 y §9, porque `PRODUCT-INTAKE` 1.3 insertó el circuito de revisión del administrador como etapa `h` y desplazó los pendientes a `i…`. La puerta técnica de acceso desde la facultad sigue atada al despliegue real y no a la letra (`PRODUCT-INTAKE` §15). **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Ninguna métrica, target, prioridad ni dependencia cambia. |
