# NB-00006 — Visualización del trabajo dentro del producto

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Documento | NB-00006-Visualizacion-Dentro-Del-Producto.md |
| Versión | 1.5 |
| Estado | Aprobado |
| Fecha | 2026-08-11 |
| Autor | Analista de Negocio Senior (AG-01) |
| Trazabilidad upstream | PRODUCT-INTAKE **1.19** §1 (idea y problema), §3 (diferenciadores D-3 y D-4), §4 (capacidades F-11, F-13 y F-25, **las tres `Must Have`**), §4.1 (regla RN-11), §6 (flujos 2, 2.1 y 3), §15 (etapa `g` y puerta técnica `PT-02`), §17.7 P.3 (la sexta función de la fachada, que gobierna los dos movimientos), §17.7 P.8 (lo que `PT-02` mide antes de comprometer la etapa `g`, con la sincronización por índice y la disposición determinista entre sus propiedades), §17.7 P.10 (el movimiento automático no altera la disposición), §20 (escenarios E-1 y E-7); `Vision-Producto.md` §1, §3 y §9; `Alcance-Producto.md` §4.1 —donde viven las tres capacidades, F-25 desde su promoción del intake 1.7 y F-13 desde la del 1.19— y §8; `Roadmap-Producto.md` §3 y §5.2 |
| Trazabilidad downstream | `CU-00007`, `CU-02006`, `CU-04006` en `GeometriaFactory-Api`; `CU-10007`, `CU-12001`, `CU-12002`, `CU-12003`, `CU-12004`, `CU-12005`, `CU-12006`, `CU-12007` en `GeometriaFactory-Web` (emitidos en 02-Especificacion-Funcional); 06-Backlog-Tecnico, 07-Plan-Sprint, 08-Calidad-Y-Pruebas |

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

Hay una parte de esa comprensión que depende de poder mirar la figura desde más de un lado sin pelear con el ratón: una pieza quieta y vista desde un solo ángulo esconde caras, y esconde justamente la diferencia entre lo que el alumno modeló y lo que creía haber modelado. De ahí la capacidad **F-25**, que el Product Owner incorporó al mirar la maqueta de la Fase B2: un movimiento automático de la escena, con dos gobiernos independientes —la **órbita de la cámara** alrededor del conjunto y el **giro de cada pieza sobre su eje**—, que se detienen mientras la persona arrastra. Lo opcional es que la persona los prenda o los apague, **no que el producto los tenga**: desde `PRODUCT-INTAKE` 1.7 la capacidad es **`Must Have`**. El motivo de la promoción no es que la comodidad de lectura haya pasado a ser indispensable, sino que **la órbita ya existe en la visualización que la cátedra usa hoy**, de modo que diferirla no sería postergar una mejora sino retirar algo que el alumno ya tiene. Quién decide el estado inicial de cada movimiento es el **anfitrión**, que consulta la preferencia de movimiento reducido del sistema y le pasa al visor **dos valores de verdad**; el visor no consulta nada. Conviene además conservar su procedencia, que no es uniforme: la órbita de la cámara **ya existe en la visualización que la cátedra usa hoy** y se porta al producto, mientras que el **giro de las piezas no existe** y es capacidad nueva.

## 2. Ejemplo de uso desde la perspectiva del negocio

Un alumno termina de pegar el texto de su trabajo y pide previsualizarlo sin salir del laboratorio. Sus tres piezas aparecen dibujadas, ortoedro incluido, y al costado se despliega la estructura de lo que cargó, que puede abrir y cerrar por partes. Elige una pieza en el árbol y la reconoce resaltada en la escena; entiende, por fin, que la figura que creía haber modelado como un prisma alargado le quedó cúbica. Corrige su programa, vuelve, carga la nueva salida, esta vez las piezas quedan como esperaba y lo envía. Más tarde el docente abre ese trabajo, que ya está a su disposición porque dejó de ser un borrador, y ve exactamente lo mismo: las mismas piezas, el mismo árbol y las mismas advertencias.

## 3. Impacto

- Si se resuelve: desaparece el corte entre modelar y ver, y la previsualización pasa a estar asociada a un trabajo con dueño y con estado.
- Si se resuelve: los ortoedros se dibujan, y con eso desaparece el fallo silencioso que hoy deja al alumno sin explicación.
- Si se resuelve: el alumno detecta antes de entregar que modeló algo distinto de lo que quería.
- Si se resuelve: el docente revisa mirando lo mismo que vio el alumno, sin pedirle capturas ni explicaciones.
- Si se resuelve: la escena se puede mirar desde todos sus lados sin pelear con el ratón, y las caras que un ángulo fijo esconde dejan de esconderse (F-25).
- Si queda sin resolver **la parte de F-25**: nadie deja de entregar por eso, pero el producto llega al aula **con menos de lo que la cátedra ya tenía**, porque la órbita existe hoy en la herramienta que los alumnos usan. Ése, y no la comodidad, es el motivo de que desde el intake 1.7 sea `Must Have` y de que la transición `g` → `h` no cierre sin ella.
- Si queda sin resolver **la parte de F-13**: no hay dónde diferirla. Sus dos propiedades —la sincronización por índice y la disposición determinista— están entre lo que la puerta técnica `PT-02` mide **antes** de comprometer la etapa `g` (PRODUCT-INTAKE §17.7 P.8), y una puerta que no pasa detiene la planificación de la etapa. Ése, y no la ambición, es el motivo de que desde el intake **1.19** sea `Must Have`.
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

Origen de cada criterio: el primero, el tercero, el cuarto, el quinto y el sexto derivan de la transición `g` a `h` de `Roadmap-Producto.md` §5.2, que a su vez traza a PRODUCT-INTAKE §15 y §17 —el quinto, de su criterio «el árbol y la escena se sincronizan por índice de pieza»—; el segundo, de PRODUCT-INTAKE §20 (escenario E-7); el séptimo, de PRODUCT-INTAKE §3 (diferenciador D-4) y §1; el **octavo** y el **noveno**, de PRODUCT-INTAKE §4 (capacidad F-25), que enuncia los dos gobiernos independientes, la suspensión durante el arrastre y —desde 1.7— que el estado inicial de cada movimiento lo fija el anfitrión con dos valores de verdad, tras consultar él la preferencia de movimiento reducido. Ninguno depende de la asunción A-2 del intake.

La precisión del cuarto criterio —disposición de la **posición**, no de la orientación— no es un cambio de target: es la lectura que PRODUCT-INTAKE §17.7 P.10 declara al incorporar F-25, y evita que el movimiento automático parezca contradecir un criterio que ya existía. El criterio, su métrica y su target de 2 de 2 no cambian, y `Roadmap-Producto.md` §5.2 recibe la misma precisión.

Los criterios octavo y noveno tienen el plazo de la etapa `g` porque es la etapa sobre cuya superficie vive la capacidad, y desde `PRODUCT-INTAKE` 1.7 **son además bloqueantes**: promovida F-25 a `Must Have`, la transición `g` → `h` de `Roadmap-Producto.md` §5.2 incorpora el gobierno independiente de los dos movimientos como **séptimo criterio**, incorporado allá el 2026-08-09 con la versión 1.4 de ese documento; y `Alcance-Producto.md` §8 incorpora el criterio de aceptación de producto correspondiente.

**El mecanismo de diferimiento que este apartado describía ya no aplica a ningún criterio de esta NB** [REESCRITO 2026-08-11 contra `PRODUCT-INTAKE` **1.19**]. Hasta esta versión el apartado declaraba que el mecanismo seguía aplicando al cuarto y al quinto criterio, por ser F-13 `Should Have`. **El Product Owner promovió F-13 a `Must Have`**, y el fundamento de la promoción es exactamente que ese diferimiento nunca fue practicable: los dos criterios están en la transición `g` → `h` de `Roadmap-Producto.md` §5.2 desde la emisión inicial de este documento, y PRODUCT-INTAKE §17.7 P.8 los incluye entre lo que `PT-02` mide **antes** de comprometer la etapa `g`. Diferirlos habría dejado la puerta sin pasar y la etapa sin planificar. Los **nueve** criterios de §5 son hoy bloqueantes y ninguno tiene salida de diferimiento; `Alcance-Producto.md` §8 recoge el criterio de aceptación de producto de F-13 desde su versión 1.6.

## 6. Stakeholders involucrados

| Rol | Nivel | Qué pide o aporta |
| --- | --- | --- |
| Docente de Programación 2 (TUP), responsable de la cátedra y de la Actividad 1, en su papel de Product Owner | Propietario | Comprometió la previsualización dentro del producto como capacidad del alcance y valida el punto de control de la etapa `g`, que cierra el alcance comprometido |
| Cátedra de Programación 2, como dueño del problema | Propietario | Necesita que el alumno pueda verificar visualmente lo que modeló, que es el objetivo original de la Actividad 1 |
| El mismo docente, en su papel de equipo de desarrollo (una persona, asistida por un agente de IA) | Implementador | Integra la visualización y el árbol dentro del producto y demuestra el escenario semilla con el ortoedro dibujado |
| Alumno de la comisión | Beneficiario | Ve su trabajo dibujado sin salir del laboratorio y se da cuenta antes de entregar si modeló otra cosa |
| El mismo docente, en su papel de administrador del laboratorio | Beneficiario y operador | Abre cualquier trabajo y ve exactamente lo mismo que vio el alumno, sin pedirle nada |

## 7. Trazabilidad a CU

| NB | Casos de uso emitidos | Estado |
| --- | --- | --- |
| NB-00006 | `CU-00007`, `CU-02006`, `CU-04006` en `GeometriaFactory-Api`; `CU-10007`, `CU-12001`, `CU-12002`, `CU-12003`, `CU-12004`, `CU-12005`, `CU-12006`, `CU-12007` en `GeometriaFactory-Web` previsualizar el trabajo en tres dimensiones | Emitidos |
| NB-00006 | `CU-00007`, `CU-02006`, `CU-04006` en `GeometriaFactory-Api`; `CU-10007`, `CU-12001`, `CU-12002`, `CU-12003`, `CU-12004`, `CU-12005`, `CU-12006`, `CU-12007` en `GeometriaFactory-Web` explorar la estructura del trabajo como árbol colapsable | Emitidos |
| NB-00006 | `CU-00007`, `CU-02006`, `CU-04006` en `GeometriaFactory-Api`; `CU-10007`, `CU-12001`, `CU-12002`, `CU-12003`, `CU-12004`, `CU-12005`, `CU-12006`, `CU-12007` en `GeometriaFactory-Web` sincronizar el árbol y la escena por índice de pieza | Emitidos |
| NB-00006 | `CU-00007`, `CU-02006`, `CU-04006` en `GeometriaFactory-Api`; `CU-10007`, `CU-12001`, `CU-12002`, `CU-12003`, `CU-12004`, `CU-12005`, `CU-12006`, `CU-12007` en `GeometriaFactory-Web` gobernar el movimiento automático de la escena: encender y apagar por separado la órbita de la cámara y el giro de las piezas | Emitidos |

## 8. Dependencias con otras NB

- Depende de: NB-00004, porque la visualización dibuja las piezas que la interpretación reconstruyó a partir del texto del alumno.
- Es prerequisito de: NB-00007 en su parte de revisión visual, porque el administrador abre el trabajo con la misma visualización y el mismo árbol que ve el alumno.

## 9. Prioridad MoSCoW

**Must Have.** Se deriva de PRODUCT-INTAKE §4: la NB agrupa F-11, F-13 y F-25, **las tres declaradas `Must Have`** —F-25 desde el intake 1.7 y F-13 desde el 1.19—, de modo que la prioridad de la NB coincide hoy con la de todas sus capacidades y no sólo con la de la más alta. La regla de agregación no cambió; lo que cambió es que esta NB ya no la necesita. El agrupamiento no es una decisión de esta categoría: el intake ya asigna F-11 y F-13 a la misma etapa `g` (PRODUCT-INTAKE §15), y `Roadmap-Producto.md` §3 las mantiene en la misma épica candidata, junto con F-25, cuya etapa ese mismo apartado fundamenta.

La parte correspondiente a **F-13** —disposición estable y sincronización— se identifica en el **cuarto** criterio de §5, «disposición estable entre procesados», en el **quinto**, «sincronización entre el árbol y la escena», y en CU-17. La parte correspondiente a **F-25** se identifica en el **octavo** criterio, «gobierno independiente del movimiento automático», en el **noveno**, «movimiento automático que no estorba ni se impone», y en CU-28. De los cuatro criterios restantes, el primero, el segundo, el tercero y el séptimo pertenecen a F-11, y el sexto lo comparte la NB con F-12.

**Esa identificación por ordinal y por nombre se conserva, pero su propósito cambió** [ACTUALIZADO 2026-08-11]. Se escribió para que la categoría 07 pudiera **diferir** la parte de F-13 y la de F-25 sin tocar el resto de la NB, y ese propósito **caducó** con las dos promociones: los nueve criterios son bloqueantes y ninguno se difiere. Lo que la identificación sigue sirviendo es para **trazar**: saber qué criterio y qué caso de uso responden por cada capacidad, que es lo que 06 y 08 necesitan para verificarlas por separado.

**Por qué F-25 encaja en esta necesidad y no en una propia.** Comparte con F-11 el dolor central —ver el trabajo dentro del producto para darse cuenta de si se modeló lo que se quería— y comparte su superficie, su etapa y su beneficiario. No apunta a una métrica de negocio distinta ni a un público distinto, que son los dos criterios de partición que esta categoría aplica en `Necesidades-Negocio.md` §3.2: una NB propia para el movimiento automático habría separado un ajuste de lectura de la vista que lo contiene, sin ningún dolor independiente que lo justificara. Encaja, entonces, por fusión, sin forzarlo. Lo que **no** hace es cambiar la prioridad de la NB, que ya era `Must Have` por F-11. La prioridad de F-25 la decide el Product Owner —`Should Have` en el intake 1.5, `Must Have` en 1.7— y esta categoría sólo la deriva y la traza.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Articula la necesidad de ver el trabajo dentro del producto a partir de las capacidades F-11 y F-13 del intake, con seis criterios de éxito trazados a su sección de origen, tres casos de uso previstos y la declaración de agregación de prioridades de §9. |
| 1.0 | 2026-08-08 | Corrección de la ronda 1 de auditoría, hallazgos **H-04** y **H-03**. H-04: §9 localizaba la parte de F-13 en «los criterios cuarto y quinto», y el quinto de entonces era la vista única para los dos papeles, que pertenece a F-11 y F-12; además la sincronización entre el árbol y la escena, que es la otra mitad de F-13, no tenía criterio propio. §5 suma un quinto criterio de sincronización derivado del criterio «el árbol y la escena se sincronizan por índice de pieza» de la transición `g` a `h` de `Roadmap-Producto.md` §5.2, y §9 pasa a identificar la parte de F-13 por nombre además de por ordinal. Los criterios pasan de seis a siete y la nota de origen de §5 se renumera en consecuencia. H-03: la ocurrencia de «observación» de §2 pasa a «advertencia», por el glosario raíz (`Vision-Producto.md` §9.1). Las dos correcciones se absorben **sin subir versión** por `Master-Prompt.md` §5, que declara que las correcciones del audit de la propia fase de emisión se resuelven dentro de la versión en curso mientras el documento esté en estado `Propuesto`. |
| 1.1 | 2026-08-08 | Ajusta §2 y el sexto criterio de §5 a la regla **RN-11** que `PRODUCT-INTAKE` 1.3 incorpora en §4.1: el administrador **no ve los trabajos en estado `Borrador`**, de modo que la vista compartida se predica sobre los trabajos que él tiene a la vista, y el ejemplo pasa por el envío antes de que el docente abra el trabajo. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por otras categorías (`Master-Prompt.md` §5). Los siete criterios, sus targets, la prioridad y las dependencias no cambian. |
| 1.2 | 2026-08-09 | Vincula la capacidad **F-25**, movimiento automático de la escena, que el Product Owner incorpora en `PRODUCT-INTAKE` 1.5 §4 a partir de la **validación visual de la Fase B2**, aprobada tras cuatro iteraciones. **Sube minor y archiva el estado anterior** porque el documento ya es citado como insumo por cinco proyectos de código (`Master-Prompt.md` §5). **§1** incorpora el párrafo que articula el dolor de la capacidad y conserva sus dos precisiones informativas: la órbita de la cámara existe en la visualización actual y se porta, el giro de las piezas es capacidad nueva, y el conjunto es comodidad de lectura y no capacidad de entrega. **§3** suma el impacto de resolverla y el de diferirla, que no impide entregar. **§4** suma el problema del ángulo fijo que esconde caras. **§5** pasa de siete a nueve criterios, con el **octavo** —gobierno independiente de los dos movimientos, 2 de 2— y el **noveno** —suspensión durante el arrastre y arranque apagado bajo preferencia de movimiento reducido, 2 de 2—, los dos derivados de la capacidad y con plazo en el punto de control de la etapa `g`; el **cuarto** criterio recibe una precisión, no un cambio de target: la disposición determinista se predica de la posición derivada del índice y no de la orientación en un instante, según `PRODUCT-INTAKE` 1.5 §17.7 P.10, de modo que el movimiento automático no lo contradiga. Se declara además que los dos criterios nuevos no son bloqueantes: la transición `g` → `h` del roadmap y los criterios de aceptación del producto no los incorporan. **§7** prevé **CU-28**, gobernar el movimiento automático de la escena. **§9** declara por qué F-25 encaja en esta NB por fusión y no merece NB propia, e identifica su parte por ordinal y por nombre para que la categoría 07 pueda diferirla sin tocar el resto. La prioridad de la NB, la de sus capacidades, sus dependencias y los targets de los siete criterios anteriores no cambian. **Autor:** Analista de Negocio Senior (AG-01) |
| 1.3 | 2026-08-09 | **Fila repuesta el 2026-08-09 al cerrar el hallazgo `F26-05`** de `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0: la versión 1.3 se emitió **sin fila de control de cambios**, de modo que su único cambio real no estaba descrito en ninguna parte del propio archivo. Lo que la 1.3 cambió, verificado contra el árbol, es la **promoción de F-25 a `Must Have`** que el Product Owner hizo en `PRODUCT-INTAKE` 1.7, en cuatro lugares: **§1**, donde el párrafo de F-25 pasa a declarar que lo opcional es que la persona prenda o apague los movimientos, **no que el producto los tenga**, con el fundamento de que la órbita ya existe en la visualización que la cátedra usa hoy y de que diferirla sería retirar algo que el alumno ya tiene; **§3**, donde el impacto de dejar F-25 sin resolver pasa de «nadie deja de entregar por eso» a que el producto llegaría al aula con menos de lo que la cátedra ya tenía, y declara que la transición `g` → `h` no cierra sin ella; **§5**, donde los criterios octavo y noveno pasan a ser **bloqueantes** y el mecanismo de diferimiento deja de aplicarles; y **§9**, donde la NB pasa a agrupar dos capacidades `Must Have` y se registra que la prioridad de F-25 la decide el Product Owner. La cabecera pasó a citar el intake 1.7. |
| 1.4 | 2026-08-09 | **Cierra los hallazgos `F26-05`, `F26-04` en su parte declarativa y `F26-26`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.9**. **Sube minor y archiva el estado anterior** por `Master-Prompt.md` §5. **§10**: se repone la fila 1.3, que faltaba. **§5 (`F26-04`)**: la afirmación de que la transición `g` → `h` de `Roadmap-Producto.md` §5.2 incorpora el gobierno independiente de los dos movimientos **era verdadera en la intención y falsa en el instrumento** —esa sección tenía seis criterios y ninguno lo mencionaba—; el criterio se agregó allá el 2026-08-09 con la versión 1.4 del roadmap, y este apartado pasa a citarlo como el séptimo criterio de esa transición y a remitir además al criterio de aceptación de producto que `Alcance-Producto.md` §8 incorporó. **Cabecera (`F26-26`)**: la trazabilidad upstream pasa del intake 1.7 al **1.9**, declara la prioridad vigente de cada capacidad agrupada, suma §17.7 P.3 —la sexta función de la fachada, que es por donde se gobiernan los dos movimientos— y corrige la remisión a `Alcance-Producto.md`, porque F-25 dejó de estar en su §4.2 y pasó a §4.1 con el resto del alcance comprometido. Ningún criterio de éxito, ningún target, ninguna dependencia y ninguna CU prevista cambia. |
| 1.5 | 2026-08-11 | **Absorbe la promoción de F-13 a `Must Have`**, decidida por el Product Owner y registrada en `PRODUCT-INTAKE` **1.19** §4 y en su control de cambios. **Cabecera**: la trazabilidad upstream pasa al intake **1.19**, declara las tres capacidades agrupadas como `Must Have`, suma **§15** y **§17.7 P.8** —lo que `PT-02` mide antes de comprometer la etapa `g`, que es la fuente del fundamento— y corrige la remisión a `Alcance-Producto.md`, porque F-13 dejó de estar en su §4.2 y pasó a §4.1, igual que F-25 en su momento. **§3**: entra el impacto de dejar la parte de F-13 sin resolver, que no es un impacto de comodidad sino de puerta: `PT-02` mide sus dos propiedades y una puerta que no pasa detiene la planificación de la etapa. **§5**: se reescribe el párrafo del **mecanismo de diferimiento**, que declaraba que seguía aplicando al cuarto y al quinto criterio por ser F-13 `Should Have`. Ese fundamento era el que la promoción derribó, y no se corrige sustituyendo una prioridad por otra: el apartado pasa a declarar que **ningún criterio de esta NB tiene ya salida de diferimiento** y por qué el de F-13 nunca la tuvo en los hechos —los dos criterios están en la transición `g` → `h` del roadmap desde la emisión inicial de este documento—. **§9**: la NB pasa a agrupar **tres** capacidades `Must Have` y se declara que la regla de agregación de prioridades, que se conserva escrita, ya no la necesita; y la identificación por ordinal y por nombre de las partes de F-13 y de F-25 se conserva con su **propósito corregido**: nació para diferir y hoy sirve para trazar. Ningún criterio de éxito, ningún target, ninguna dependencia y ninguna CU prevista cambia. Sube minor. |
