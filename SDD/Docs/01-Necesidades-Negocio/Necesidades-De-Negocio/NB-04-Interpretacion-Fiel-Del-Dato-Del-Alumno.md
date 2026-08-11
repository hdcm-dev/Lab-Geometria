# NB-04 — Interpretación fiel del dato del alumno, con el error localizado

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-04-Interpretacion-Fiel-Del-Dato-Del-Alumno.md |
| Versión | 1.2 |
| Estado | Aprobado |
| Fecha | 2026-08-08 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE §1 (idea y problema), §3 (diferenciadores D-1 y D-3), §4 (capacidades F-09 y F-22), §4.1 (reglas RN-05, RN-08 y RN-09), §4.2 (modelo de estados del trabajo), §6 (flujos 2 y 4), §7 (caso límite CL-3), §12 (glosario del dominio: «enviar»), §9 (exclusión X-4), §10 (formato de entrada no negociable), §11 (riesgo RN-B3), §15 (regla de delivery de datos de prueba reales), §20 y §21 (escenarios de datos); `Vision-Producto.md` §3 y §8; `Alcance-Producto.md` §4.1, §7 y §8 |
| Trazabilidad downstream | CU-12, CU-13, CU-23 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

El dato que produce el alumno es el que es, y no se negocia: lo emite su Actividad 1 con el formato que tiene, y el producto se adapta al dato y nunca al revés (PRODUCT-INTAKE §10). Ese texto tiene particularidades conocidas y verificadas —una coma final antes de cerrar, una clave que se llama de una manera cuando el consumidor esperaba otra, y caras que a veces se nombran de dos formas distintas para la misma figura— y hoy la página de visualización disponible se atraganta con ellas. El resultado es el peor posible desde el punto de vista didáctico: la figura simplemente no aparece, y nadie le dice al alumno por qué. Ningún ortoedro generado por la aplicación se dibuja, y falla en silencio (PRODUCT-INTAKE §1).

La necesidad de la cátedra tiene dos caras. La primera es que el texto real del alumno se interprete sin pedirle una sola corrección: si el producto rechazara el dato que los alumnos efectivamente producen, no serviría para el dato que existe, y ese es el riesgo que las fuentes señalan como el que más veces se repite (PRODUCT-INTAKE §11, RN-B3). La segunda es que, cuando el texto de verdad no se puede interpretar, el alumno reciba una indicación precisa de **en qué figura y en qué campo** está el problema, en lugar de un mensaje genérico o de un silencio.

De esa segunda cara se deriva el límite entre lo que queda a medio hacer y lo que llega a ser una entrega, y el producto lo resuelve con **una sola acción**: el alumno envía, y el resultado de interpretar su texto es lo que decide el estado. Si el texto verifica, el trabajo queda entregado, en estado `Pendiente`; si no verifica, queda en `Borrador` con sus errores localizados, y el alumno corrige y vuelve a enviar cuantas veces haga falta (PRODUCT-INTAKE §4, F-22, y §4.1, RN-05). No hay una acción separada de guardar sin enviar, y esa unificación es deliberada: evita que el alumno crea que entregó algo que el producto nunca pudo leer. Y en ningún caso el producto reescribe el texto del alumno: el original se conserva íntegro porque es la única fuente fiel de su trabajo (PRODUCT-INTAKE §4.1, RN-08, y §9, exclusión X-4).

## 2. Ejemplo de uso desde la perspectiva del negocio

Un alumno pega en su trabajo la salida exacta de su programa, con sus comas finales y con la clave que su código emite para las bases del ortoedro. El producto la interpreta sin quejarse y sin pedirle que arregle nada: reconstruye sus piezas y sus componentes, y el ortoedro —que en la página vieja nunca se veía— aparece. Otro alumno, en cambio, dejó a medio hacer una figura de un tipo que su programa todavía no sabe describir. El producto no le dice «error de formato»: le dice que el problema está en la tercera figura, en el campo del tipo. Su trabajo queda en estado `Borrador` con el texto que pegó; corrige su programa, vuelve, pega la nueva salida, vuelve a enviar y esta vez el trabajo queda entregado, a la espera de la revisión del docente.

## 3. Impacto

- Si se resuelve: el producto sirve para el dato que existe, y no para un dato ideal que ningún alumno produce.
- Si se resuelve: desaparece el fallo silencioso, que es exactamente lo que el producto viene a eliminar.
- Si se resuelve: el alumno deja de adivinar dónde falla su salida y recibe la ubicación exacta del defecto, que es información de valor didáctico sobre su propio programa.
- Si se resuelve: queda establecido el límite entre lo que quedó a medio hacer y lo que es una entrega, que es lo que le da sentido al estado del trabajo.
- Si se resuelve: el alumno no puede creer que entregó algo que el producto nunca pudo leer, porque el estado se lo dice.
- Si queda sin resolver: la entrega no se puede exigir, porque no habría forma de afirmar que un trabajo entregado es interpretable.
- Si queda sin resolver: no hay sobre qué construir la verificación de valores ni la visualización, porque las dos operan sobre el trabajo ya interpretado.

## 4. Problema específico que resuelve

- El texto real del alumno es rechazado hoy por el consumidor disponible, por particularidades de formato conocidas y verificadas.
- Los ortoedros generados por la aplicación del alumno no se dibujan, y el alumno no recibe ninguna explicación.
- Los mensajes de error genéricos obligan al alumno a buscar a ciegas en su propia salida.
- No hay criterio explícito de qué se puede entregar y qué no.
- Un alumno podría dar por entregado un trabajo cuyo texto el producto nunca llegó a interpretar.
- Un producto que reescribiera el texto del alumno perdería la única fuente fiel de su trabajo.
- Una interpretación construida sin mirar el dato real terminaría rechazando lo que los alumnos efectivamente producen.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Cobertura de la batería obligatoria con datos verificados | Casos de prueba obligatorios que pasan, sobre los 9 declarados | 9 de 9 | Punto de control de la etapa `f` |
| Aceptación del dato real sin pedir correcciones | Escenarios de datos del intake que se interpretan sin que el alumno modifique una sola coma de su salida, sobre los **8** declarados —`E-1` a `E-8` de PRODUCT-INTAKE §20, con `E-8` incorporado el 2026-08-09— | **8 de 8** | Punto de control de la etapa `f` |
| Localización del defecto | Errores de interpretación reportados sin indicar índice de figura y campo | 0 | Punto de control de la etapa `f` |
| Límite entre lo que no verifica y la entrega | Trabajos que pasan a estado `Pendiente` teniendo errores de interpretación sin resolver | 0 | Punto de control de la etapa `f` |
| Acción única de guardado | Acciones de guardado que el alumno tiene disponibles sobre su trabajo, sobre la 1 declarada: enviar | 1 de 1 | Punto de control de la etapa `f` |
| Conservación del original | Caracteres del texto original del alumno modificados por el producto | 0 | Punto de control de la etapa `f`, y en cada punto de control posterior por la regla de no regresión |

Origen de cada criterio: el primero deriva de PRODUCT-INTAKE §11 (RN-B3) y §15 (regla de delivery de datos de prueba reales), y de la transición `f` a `g` de `Roadmap-Producto.md` §5.2; el segundo, de PRODUCT-INTAKE §20 y §21 y de §4 (F-09); el tercero, de PRODUCT-INTAKE §4 (F-09) y §7 (CL-3); el cuarto, de PRODUCT-INTAKE §7 (CL-3) y §4.1 (RN-05); el quinto, de PRODUCT-INTAKE §4 (F-22), §12 (entrada «enviar») y de la transición `f` a `g` de `Roadmap-Producto.md` §5.2; el sexto, de PRODUCT-INTAKE §4.1 (RN-08) y §9 (X-4). Ninguno depende de la asunción A-2 del intake.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Declaró el formato de entrada como no negociable y prohibió que el producto reescriba el texto del alumno; valida el punto de control de la etapa `f` |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que la herramienta funcione con el material que sus alumnos ya producen |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Construye la interpretación contra los escenarios de datos verificados del intake, sin inventar datos de prueba |
| Alumno de la comisión | Beneficiario | Entrega su salida tal como la produce su programa y, cuando algo falla, recibe la figura y el campo exactos |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Revisa trabajos entregados sabiendo que todos ellos se interpretaron sin errores |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-04 | CU-12 interpretar el texto del trabajo y reportar los errores con figura y campo | a generar |
| NB-04 | CU-13 resolver el estado del trabajo según el resultado de la interpretación | a generar |
| NB-04 | CU-23 enviar un trabajo | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-03, porque la interpretación se aplica sobre un trabajo ya cargado y guardado.
- Es prerequisito de: NB-05 (la verificación de valores opera sobre el trabajo interpretado), NB-06 (la visualización dibuja las piezas reconstruidas por la interpretación) y NB-09 (el desenlace se ejerce sobre trabajos en estado `Pendiente`, y a ese estado sólo se llega por un envío que verifica).

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: las dos capacidades que esta NB agrupa —F-09 y F-22— están declaradas Must Have, las dos con la misma prioridad, de modo que no hay agregación de prioridades distintas.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de interpretar el dato real del alumno y de localizar el defecto a partir de la capacidad F-09 del intake, con cinco criterios de éxito trazados a su sección de origen y dos casos de uso previstos. |
| 1.1 | 2026-08-08 | Absorbe el circuito de revisión del administrador incorporado por el Product Owner en `PRODUCT-INTAKE` 1.3. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Incorpora la capacidad **F-22**, el envío como única acción de guardado, cuyo dolor es el mismo que esta NB ya articulaba —dónde está el límite entre lo que no verifica y lo que es una entrega— y que ahora se resuelve con una sola acción. **§1** reescribe el tercer párrafo sobre el envío y el par `Borrador` / `Pendiente`; **§2**, **§3** y **§4** ajustan el ejemplo y las viñetas. **§5** reescribe el cuarto criterio, que pasa de «trabajos que se finalizan» a «trabajos que pasan a estado `Pendiente`», y suma un quinto criterio de acción única de guardado. **§7** suma CU-23, enviar un trabajo, y renombra CU-13. **§8** declara a NB-09 como dependiente. **§9** declara la agregación de F-09 y F-22, las dos Must Have. |
| 1.2 | 2026-08-09 | **Cierra la fila de `F26-20` que alcanza a este archivo**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **§5**, primer criterio: los escenarios de datos del intake pasan de **siete a ocho**, contados `E-1` a `E-8` en PRODUCT-INTAKE §20; el octavo, `E-8`, lo incorporó el Product Owner el 2026-08-09 para la condición `DIMENSION_NO_LEGIBLE`, que era la única del contrato de fachada sin escenario propio. La métrica y el sentido del criterio no cambian: cambia el denominador, que estaba desactualizado. **Sube minor y archiva el estado anterior** por `Master-Prompt.md` §5. Ninguna otra métrica, target, dependencia ni CU prevista cambia. |
