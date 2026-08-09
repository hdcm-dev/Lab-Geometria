# NB-06 — Visualización del trabajo dentro del producto

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-06-Visualizacion-Dentro-Del-Producto.md |
| Versión | 1.2 |
| Estado | Propuesto |
| Fecha | 2026-08-09 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE 1.5 §1 (idea y problema), §3 (diferenciadores D-3 y D-4), §4 (capacidades F-11, F-13 y F-25), §4.1 (regla RN-11), §6 (flujos 2, 2.1 y 3), §17.7 P.10 (el movimiento automático no altera la disposición), §20 (escenarios E-1 y E-7); `Vision-Producto.md` §1, §3 y §9; `Alcance-Producto.md` §4.1, §4.2 y §8; `Roadmap-Producto.md` §3 y §5.2 |
| Trazabilidad downstream | CU-15, CU-16, CU-17, CU-28 (previstas en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

Ver la figura y trabajar sobre ella están hoy cortados en dos. El alumno modela en su programa, copia el texto y sale del laboratorio a una página suelta para mirarlo en tres dimensiones; lo que ve ahí no está asociado a nada, no queda guardado y no se puede volver a abrir (PRODUCT-INTAKE §1). Ese corte es lo que hace que la previsualización no sirva para revisar una entrega: cuando el alumno cierra la página, la imagen se va con ella.

Además, la visualización actual no muestra todo lo que el alumno modela. Ningún ortoedro generado por la aplicación se dibuja, porque la página espera una clave que el programa del alumno no emite, y falla sin mensaje (PRODUCT-INTAKE §1). El alumno modela una pieza, la ve desaparecer y no tiene forma de saber si el error está en su modelo o en la herramienta.

La necesidad, entonces, es que el alumno y el docente vean el trabajo cargado dentro del producto, con las figuras dibujadas y con la estructura del texto desplegada al lado, de modo que se pueda pasar de un elemento del árbol a la pieza que le corresponde. Es una necesidad de comprensión, no de estética: la previsualización existe para que el alumno se dé cuenta de si modeló lo que quería modelar antes de entregar, y para que el docente vea exactamente lo mismo que vio el alumno.

Hay una parte de esa comprensión que depende de poder mirar la figura desde más de un lado sin pelear con el ratón: una pieza quieta y vista desde un solo ángulo esconde caras, y esconde justamente la diferencia entre lo que el alumno modeló y lo que creía haber modelado. De ahí la capacidad **F-25**, que el Product Owner incorporó al mirar la maqueta de la Fase B2: un movimiento automático **opcional** de la escena, con dos gobiernos independientes —la **órbita de la cámara** alrededor del conjunto y el **giro de cada pieza sobre su eje**—, que se detienen mientras la persona arrastra y arrancan apagados si el sistema declara preferencia de movimiento reducido. Es **comodidad de lectura, no capacidad de entrega**: sin ella el trabajo se carga, se valida, se ve y se resuelve igual, y por eso el intake la declara `Should Have` y no `Must Have`. Conviene además conservar su procedencia, que no es uniforme: la órbita de la cámara **ya existe en la visualización que la cátedra usa hoy** y se porta al producto, mientras que el **giro de las piezas no existe** y es capacidad nueva.

## 2. Ejemplo de uso desde la perspectiva del negocio

Un alumno termina de pegar el texto de su trabajo y pide previsualizarlo sin salir del laboratorio. Sus tres piezas aparecen dibujadas, ortoedro incluido, y al costado se despliega la estructura de lo que cargó, que puede abrir y cerrar por partes. Elige una pieza en el árbol y la reconoce resaltada en la escena; entiende, por fin, que la figura que creía haber modelado como un prisma alargado le quedó cúbica. Corrige su programa, vuelve, carga la nueva salida, esta vez las piezas quedan como esperaba y lo envía. Más tarde el docente abre ese trabajo, que ya está a su disposición porque dejó de ser un borrador, y ve exactamente lo mismo: las mismas piezas, el mismo árbol y las mismas advertencias.

## 3. Impacto

- Si se resuelve: desaparece el corte entre modelar y ver, y la previsualización pasa a estar asociada a un trabajo con dueño y con estado.
- Si se resuelve: los ortoedros se dibujan, y con eso desaparece el fallo silencioso que hoy deja al alumno sin explicación.
- Si se resuelve: el alumno detecta antes de entregar que modeló algo distinto de lo que quería.
- Si se resuelve: el docente revisa mirando lo mismo que vio el alumno, sin pedirle capturas ni explicaciones.
- Si se resuelve: la escena se puede mirar desde todos sus lados sin pelear con el ratón, y las caras que un ángulo fijo esconde dejan de esconderse (F-25).
- Si queda sin resolver **la parte de F-25**: nadie deja de entregar por eso. La lectura de la figura queda más incómoda y más dependiente de que la persona sepa arrastrar, que es exactamente el peso que tiene una capacidad `Should Have` de comodidad de lectura.
- Si queda sin resolver: la revisión del docente se limita a leer texto, y se pierde el motivo original por el que existía una visualización.
- Si queda sin resolver: el alumno sigue saliendo del laboratorio a una página aparte, y lo que ve ahí sigue sin quedar registrado en ningún lado.

## 4. Problema específico que resuelve

- Ver el trabajo en tres dimensiones obliga hoy a salir del laboratorio a una página suelta.
- Lo que se ve en esa página no queda asociado a ningún trabajo ni a ningún alumno.
- Los ortoedros generados por la aplicación del alumno no se dibujan, y su ausencia no produce ningún mensaje.
- No hay forma de relacionar un elemento de la estructura del texto con la pieza que le corresponde en la escena.
- Dos previsualizaciones del mismo trabajo pueden ubicar las piezas de forma distinta y confundir a quien compara.
- El docente no puede ver lo mismo que vio el alumno sin pedirle que se lo muestre en su pantalla.
- Una escena quieta y mirada desde un solo ángulo esconde caras, y esconder una cara es esconder la diferencia entre lo que el alumno modeló y lo que creía haber modelado.

## 5. Criterios de éxito

| Criterio | Métrica | Target | Plazo |
| --- | --- | --- | --- |
| Piezas efectivamente dibujadas del escenario semilla | Piezas del escenario semilla del intake que se dibujan, ortoedro incluido, sobre las 3 que lo componen | 3 de 3 | Punto de control de la etapa `g` |
| Cobertura de los tipos de figura dibujables | Tipos de figura que la visualización dibuja, sobre los 6 que el escenario de cobertura del intake ejercita | 6 de 6 | Punto de control de la etapa `g` |
| Continuidad de uso | Recorridos de ida y vuelta entre trabajos que no degradan la visualización, sobre los 10 recorridos del guion de demostración | 10 de 10 | Punto de control de la etapa `g` |
| Disposición estable entre procesados | Procesados del mismo trabajo que producen la misma disposición de las piezas, sobre los 2 del guion de demostración. Se predica de la **posición** de cada pieza, derivada de su índice, **no de su orientación en un instante** | 2 de 2 | Punto de control de la etapa `g` |
| Sincronización entre el árbol y la escena | Piezas de la escena que responden a la selección de su elemento en el árbol, sobre el total de piezas del trabajo | 100 % | Punto de control de la etapa `g` |
| Vista única para los dos papeles | Elementos que el administrador ve al abrir un trabajo de los que tiene a la vista, sobre los 4 que ve el alumno: datos, texto, escena y árbol | 4 de 4 | Punto de control de la etapa `g` |
| Salidas del producto para ver el trabajo | Páginas fuera del producto que la persona necesita abrir para ver su trabajo en tres dimensiones | 0 | Punto de control de la etapa `g` |
| Gobierno independiente del movimiento automático | Movimientos de la escena que la persona enciende y apaga por separado, sobre los 2 declarados: la órbita de la cámara alrededor del conjunto y el giro de cada pieza sobre su eje | 2 de 2 | Punto de control de la etapa `g` |
| Movimiento automático que no estorba ni se impone | Movimientos que se detienen mientras la persona arrastra **y** que arrancan apagados cuando el sistema declara preferencia de movimiento reducido, sobre los 2 declarados | 2 de 2 | Punto de control de la etapa `g` |

Origen de cada criterio: el primero, el tercero, el cuarto, el quinto y el sexto derivan de la transición `g` a `h` de `Roadmap-Producto.md` §5.2, que a su vez traza a PRODUCT-INTAKE §15 y §17 —el quinto, de su criterio «el árbol y la escena se sincronizan por índice de pieza»—; el segundo, de PRODUCT-INTAKE §20 (escenario E-7); el séptimo, de PRODUCT-INTAKE §3 (diferenciador D-4) y §1; el **octavo** y el **noveno**, de PRODUCT-INTAKE 1.5 §4 (capacidad F-25), que enuncia los dos gobiernos independientes, la suspensión durante el arrastre y el arranque apagado bajo preferencia de movimiento reducido. Ninguno depende de la asunción A-2 del intake.

La precisión del cuarto criterio —disposición de la **posición**, no de la orientación— no es un cambio de target: es la lectura que PRODUCT-INTAKE 1.5 §17.7 P.10 declara al incorporar F-25, y evita que el movimiento automático parezca contradecir un criterio que ya existía. El criterio, su métrica y su target de 2 de 2 no cambian, y `Roadmap-Producto.md` §5.2 recibe la misma precisión.

Los criterios octavo y noveno tienen el plazo de la etapa `g` porque es la etapa sobre cuya superficie vive la capacidad, **no porque sean bloqueantes**: F-25 es `Should Have` y ni la transición `g` → `h` de `Roadmap-Producto.md` §5.2 ni los criterios de aceptación de `Alcance-Producto.md` §8 la incorporan. Si la capacidad se difiere, se difieren con ella estos dos criterios y ninguno de los otros siete se toca, que es el mismo mecanismo que §9 declara para F-13.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Comprometió la previsualización dentro del producto como capacidad del alcance y valida el punto de control de la etapa `g`, que cierra el alcance comprometido |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que el alumno pueda verificar visualmente lo que modeló, que es el objetivo original de la Actividad 1 |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Integra la visualización y el árbol dentro del producto y demuestra el escenario semilla con el ortoedro dibujado |
| Alumno de la comisión | Beneficiario | Ve su trabajo dibujado sin salir del laboratorio y se da cuenta antes de entregar si modeló otra cosa |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Abre cualquier trabajo y ve exactamente lo mismo que vio el alumno, sin pedirle nada |

## 7. Trazabilidad a CU

| NB | CU prevista | Estado |
| --- | --- | --- |
| NB-06 | CU-15 previsualizar el trabajo en tres dimensiones | a generar |
| NB-06 | CU-16 explorar la estructura del trabajo como árbol colapsable | a generar |
| NB-06 | CU-17 sincronizar el árbol y la escena por índice de pieza | a generar |
| NB-06 | CU-28 gobernar el movimiento automático de la escena: encender y apagar por separado la órbita de la cámara y el giro de las piezas | a generar |

## 8. Dependencias con otras NB

- Depende de: NB-04, porque la visualización dibuja las piezas que la interpretación reconstruyó a partir del texto del alumno.
- Es prerequisito de: NB-07 en su parte de revisión visual, porque el administrador abre el trabajo con la misma visualización y el mismo árbol que ve el alumno.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: la NB agrupa F-11, declarada Must Have, y F-13 y F-25, declaradas Should Have, de modo que la prioridad de la NB es la de su capacidad más alta, y así queda declarado. El agrupamiento no es una decisión de esta categoría: el intake ya asigna F-11 y F-13 a la misma etapa `g` (PRODUCT-INTAKE §15), y `Roadmap-Producto.md` §3 las mantiene en la misma épica candidata, junto con F-25, cuya etapa ese mismo apartado fundamenta.

La parte correspondiente a **F-13** —disposición estable y sincronización— se identifica en el **cuarto** criterio de §5, «disposición estable entre procesados», en el **quinto**, «sincronización entre el árbol y la escena», y en CU-17, para que la categoría 07 pueda diferirla sin tocar el resto de la NB. La parte correspondiente a **F-25** se identifica en el **octavo** criterio, «gobierno independiente del movimiento automático», en el **noveno**, «movimiento automático que no estorba ni se impone», y en CU-28, con el mismo propósito. De los cuatro criterios restantes, el primero, el segundo, el tercero y el séptimo pertenecen a F-11, y el sexto lo comparte la NB con F-12; ninguno de esos cinco se difiere.

**Por qué F-25 encaja en esta necesidad y no en una propia.** Comparte con F-11 el dolor central —ver el trabajo dentro del producto para darse cuenta de si se modeló lo que se quería— y comparte su superficie, su etapa y su beneficiario. No apunta a una métrica de negocio distinta ni a un público distinto, que son los dos criterios de partición que esta categoría aplica en `Necesidades-Negocio.md` §3.2: una NB propia para el movimiento automático habría separado un ajuste de lectura de la vista que lo contiene, sin ningún dolor independiente que lo justificara. Encaja, entonces, por fusión, sin forzarlo. Lo que **no** hace es cambiar la prioridad de la NB ni la de ninguna capacidad: F-25 sigue siendo `Should Have`, decidida por el Product Owner, y esta categoría sólo la deriva y la traza.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de ver el trabajo dentro del producto a partir de las capacidades F-11 y F-13 del intake, con seis criterios de éxito trazados a su sección de origen, tres casos de uso previstos y la declaración de agregación de prioridades de §9. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgos **H-04** y **H-03**. H-04: §9 localizaba la parte de F-13 en «los criterios cuarto y quinto», y el quinto de entonces era la vista única para los dos papeles, que pertenece a F-11 y F-12; además la sincronización entre el árbol y la escena, que es la otra mitad de F-13, no tenía criterio propio. §5 suma un quinto criterio de sincronización derivado del criterio «el árbol y la escena se sincronizan por índice de pieza» de la transición `g` a `h` de `Roadmap-Producto.md` §5.2, y §9 pasa a identificar la parte de F-13 por nombre además de por ordinal. Los criterios pasan de seis a siete y la nota de origen de §5 se renumera en consecuencia. H-03: la ocurrencia de «observación» de §2 pasa a «advertencia», por el glosario raíz (`Vision-Producto.md` §9.1). Las dos correcciones se absorben **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
| 1.1 | 2026-08-08 | Ajusta §2 y el sexto criterio de §5 a la regla **RN-11** que `PRODUCT-INTAKE` 1.3 incorpora en §4.1: el administrador **no ve los trabajos en estado `Borrador`**, de modo que la vista compartida se predica sobre los trabajos que él tiene a la vista, y el ejemplo pasa por el envío antes de que el docente abra el trabajo. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Los siete criterios, sus targets, la prioridad y las dependencias no cambian. |
| 1.2 | 2026-08-09 | Vincula la capacidad **F-25**, movimiento automático de la escena, que el Product Owner incorpora en `PRODUCT-INTAKE` 1.5 §4 a partir de la **validación visual de la Fase B2**, aprobada tras cuatro iteraciones. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§1** incorpora el párrafo que articula el dolor de la capacidad y conserva sus dos precisiones informativas: la órbita de la cámara existe en la visualización actual y se porta, el giro de las piezas es capacidad nueva, y el conjunto es comodidad de lectura y no capacidad de entrega. **§3** suma el impacto de resolverla y el de diferirla, que no impide entregar. **§4** suma el problema del ángulo fijo que esconde caras. **§5** pasa de siete a nueve criterios, con el **octavo** —gobierno independiente de los dos movimientos, 2 de 2— y el **noveno** —suspensión durante el arrastre y arranque apagado bajo preferencia de movimiento reducido, 2 de 2—, los dos derivados de la capacidad y con plazo en el punto de control de la etapa `g`; el **cuarto** criterio recibe una precisión, no un cambio de target: la disposición determinista se predica de la posición derivada del índice y no de la orientación en un instante, según `PRODUCT-INTAKE` 1.5 §17.7 P.10, de modo que el movimiento automático no lo contradiga. Se declara además que los dos criterios nuevos no son bloqueantes: la transición `g` → `h` del roadmap y los criterios de aceptación del producto no los incorporan. **§7** prevé **CU-28**, gobernar el movimiento automático de la escena. **§9** declara por qué F-25 encaja en esta NB por fusión y no merece NB propia, e identifica su parte por ordinal y por nombre para que la categoría 07 pueda diferirla sin tocar el resto. La prioridad de la NB, la de sus capacidades, sus dependencias y los targets de los siete criterios anteriores no cambian. | Analista de Negocio Senior (AG-01) |
