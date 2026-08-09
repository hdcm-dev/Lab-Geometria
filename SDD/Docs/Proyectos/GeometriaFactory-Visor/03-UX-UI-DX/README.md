# 03 · UX / UI / DX — GeometriaFactory-Visor

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** README.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX

**Trazabilidad upstream:** `../02-Especificacion-Funcional/` completa, con `Definicion-Contrato-De-Fachada.md` como documento de concepto central y `CU-01` a `CU-07` como contratos de uso —en el orden de lectura `CU-01` a `CU-05`, después `CU-07` y por último el transversal `CU-06`, `Especificacion-Funcional.md` §3.2—; `../../../00-Contexto/Vision-Producto.md` §3 y §9; `../../../00-Contexto/Alcance-Producto.md` §4.1 y §4.2; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §2.3; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §14, §16.1, §17.7, §18 y §20 E-1 y E-7; `Rules-UX-UI-DX.md` §1.2 fila `library`, §1.5, §2.1, §2.2, §4 y §6
**Trazabilidad downstream:** 05-Arquitectura-Tecnica, 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples (sample S-1), 11-Documentacion; Fase B2 de validación visual de maqueta

---

## Tabla de contenido

- [1. Punto de entrada](#1-punto-de-entrada)
- [2. Documentos vigentes](#2-documentos-vigentes)
- [3. Artefactos omitidos, con su motivo](#3-artefactos-omitidos-con-su-motivo)
- [4. Artefactos de la Fase B2](#4-artefactos-de-la-fase-b2)
- [5. Cómo leer esta sección](#5-cómo-leer-esta-sección)
- [6. Autoverificación contra los criterios de aceptación de la categoría](#6-autoverificación-contra-los-criterios-de-aceptación-de-la-categoría)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Punto de entrada

`GeometriaFactory-Visor` es un proyecto de código de tipo **`library`**, nivel 0 del orden topológico del producto y sin dependencias. Su `tiene_ui_final` es **false**, de modo que esta categoría se emite en **variante DX** y con **cero wireframes**, que es el mínimo que `Rules-UX-UI-DX.md` §2.2 fija para el tipo.

Lo que esta sección documenta es una superficie que se consume **por código**: **seis funciones planas**, siete garantías y siete códigos de condición. Eran cinco funciones hasta el 2026-08-09, cuando el Product Owner agregó `establecerMovimiento` al cerrar la validación visual de la Fase B2; **las otras dos cifras no se movieron**, porque la sexta función no acuña garantía ni código. Y hay una razón por la que no es un trámite: `tiene_extensibilidad` es **true**, y el punto de extensión declarado del producto **es** el contrato de esta fachada (PRODUCT-INTAKE §18). Esta documentación es lo que permite reemplazar el motor de dibujo tridimensional sin tocar ninguna página, porque es la que hace que ningún anfitrión conozca nada del interior.

El punto de entrada es [`DX-Developer-Experience.md`](DX-Developer-Experience.md).

## 2. Documentos vigentes

| Documento | Propósito | Variante | Estado |
| --- | --- | --- | --- |
| [`DX-Developer-Experience.md`](DX-Developer-Experience.md) | Marco DX: dos roles de intervención, onboarding en tres tramos con objetivo verificable, quick-start de cinco pasos, plan Diátaxis con dueño por modo, principios de diagnóstico, seis métricas DX y lazo de retroalimentación. Documenta además la **sexta función**, `establecerMovimiento`, con su firma, su retorno y su única condición, el gobierno del **movimiento automático de la escena** —por las opciones de `inicializar` al nacer y por la sexta función con la instancia viva— y que su bucle de dibujo no origina peticiones | DX | Propuesto |
| [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) | Recorrido de integración de la primera hora: dibujar en 5 minutos, ejercer el contrato entero en 30 —incluido cómo se pasan las dos opciones de movimiento automático y cómo se cambian en vivo con `establecerMovimiento`, dentro de las seis funciones—, modificar el interior sin cambiar el contrato en 60. Incluye diagnóstico de catorce síntomas frecuentes | DX | Propuesto |
| [`DX-Error-Messages.md`](DX-Error-Messages.md) | Catálogo de trece entradas `E-VIS-01` a `E-VIS-13`, derivadas de los **siete** códigos de condición del contrato de fachada —que no cambian—, con la acción sugerida siempre del lado del anfitrión, y seis situaciones que **no** son entradas del catálogo | DX | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Los diecinueve términos que **esta categoría** acuña, el término polisémico «recorrido» resuelto con forma calificada obligatoria, y las tres listas de términos referenciados y no redefinidos | DX | Propuesto |
| `README.md` | Este índice | Ambas | Propuesto |

Un solo archivo por nombre lógico, ninguno con sufijo de versión en el nombre y ninguna versión superada: la carpeta `_legacy/` no existe todavía y no corresponde crearla.

## 3. Artefactos omitidos, con su motivo

Un artefacto omitido **no vuelve**. Los cinco de esta tabla no se van a emitir en ninguna fase posterior, y esa es la diferencia con los tres artefactos de línea de base de §4, que sí se emitieron —en la carpeta del proyecto de código donde hubo maqueta—.

| Artefacto | Motivo de la omisión |
| --- | --- |
| `Experiencia-De-Uso.md` | **Omitido por no haber UI final.** `Rules-UX-UI-DX.md` §2.1 lo declara obligatorio para los tipos con interfaz visible al usuario final y lo omite explícitamente para `library`. Este proyecto de código no tiene pantallas propias ni usuarios propios: no hay persona que recorra una superficie suya, y las once secciones del §4.2 —flujos de usuario, accesibilidad de pantalla, internacionalización de la interfaz— no tienen sujeto acá. La experiencia del rol de intervención vive en `DX-Developer-Experience.md`, que es su equivalente en variante DX |
| `wireframes-<superficie>.md` | **Omitidos por no haber superficies que dibujar.** El mínimo que `Rules-UX-UI-DX.md` §2.2 fija para `library` es **cero**. No hay pantalla, modal, ventana ni pestaña de este proyecto de código: hay seis funciones. La superficie donde la escena queda embebida pertenece al proyecto de código anfitrión y se documenta en su propia categoría 03 |
| `DX-Portal-Developers.md` | **Omitido porque `tiene_portal_developers` es false.** No hay portal hospedado ni lo va a haber: el artefacto no se publica —`redistribuible` = false, PRODUCT-INTAKE §17.7— y sus dos únicos consumidores son internos al producto. Un portal para una comunidad que no existe sería documentación sin lector |
| `DX-Operability.md` | **Omitido por no ser `worker-service`.** `Rules-UX-UI-DX.md` §2.1 lo declara obligatorio para ese tipo. Este proyecto de código no es un servicio en segundo plano: no tiene proceso, no tiene operador y no emite registros de operación. Su artefacto es un archivo que se sirve como recurso estático |
| `representacion-<concepto>.md` | **No aplica.** El artefacto es condicional: existe cuando hay una representación visual o estructural reutilizada entre varios artefactos de la categoría. Acá no hay ninguna, porque no hay wireframes que la compartan |

## 4. Artefactos de la Fase B2

`requiere_maqueta` es **true** para este proyecto de código, y la Fase B2 **ya corrió y quedó aprobada**. Corrió, eso sí, de una forma que esta sección anticipaba de otro modo y que conviene dejar declarada: **este proyecto de código no tuvo maqueta propia, por decisión del Product Owner**, y su validación se integró en la maqueta del proyecto de código `GeometriaFactory-Web`, que es el anfitrión real de la escena. Tiene sentido: la fachada no dibuja ninguna superficie propia, de modo que lo único que se puede mirar de ella es la escena embebida en su anfitrión.

Consecuencia sobre los tres artefactos de línea de base, que esta sección declaraba previstos **para esta carpeta**:

| Artefacto | Estado | Dónde quedó |
| --- | --- | --- |
| `Linea-Base-Visual.md` | **Emitido, fuera de esta carpeta** | En la categoría 03 de `GeometriaFactory-Web`, que es donde corrió la maqueta |
| `Contrato-Datos-Maqueta.md` | **Emitido, fuera de esta carpeta** | Ídem |
| `Bitacora-Validacion-Maqueta.md` | **Emitido, fuera de esta carpeta** | Ídem, con el registro de la validación de la fachada dentro de esa misma maqueta |

**No son artefactos omitidos ni artefactos faltantes: son artefactos emitidos en otra carpeta**, la del proyecto de código donde hubo maqueta. La titularidad documental de la categoría 03 sigue rigiendo (`Rules-UX-UI-DX.md` §1.5); lo que cambia es de qué proyecto de código es esa categoría 03. Esta carpeta no los duplica: duplicarlos crearía dos líneas de base para una sola maqueta.

**Lo que sí llegó a esta carpeta es la retroalimentación.** Los tres documentos DX de §2 absorbieron lo que la validación visual dejó sobre la fachada —la capacidad **F-25** de movimiento automático de la escena y el cero como dimensión legible— **sin subir versión**, porque `Master-Prompt.md` §5 lo admite mientras el documento está en estado `Propuesto`, y con el motivo declarado en su control de cambios. En una segunda ronda, del **2026-08-09**, absorbieron también la **decisión del Product Owner** que cerró esa misma validación: la **sexta función** de la fachada, `establecerMovimiento(id, opciones)`, que prende y apaga los dos movimientos **sobre una instancia viva y sin reconstruirla**, con contrato de uso en el `CU-07` nuevo.

**Y la frontera que esa función hace visible, declarada acá para que ninguna categoría aguas abajo la corra:** el **control visible**, la **consulta de la preferencia de movimiento reducido** del sistema y la **conservación de la elección** de quien mira son del **componente anfitrión**, no del archivo de guion. Si el bundle consultara la preferencia violaría **G-3** —leer configuración propia— y si guardara la elección violaría **G-2** —persistir—. La fachada recibe el estado deseado, lo aplica y devuelve el **estado efectivo de los dos** movimientos.

**Constancia sobre el aspecto del resaltado.** `CU-03` §10 difiere a esta categoría cómo se ve el resaltado de una pieza —color, contorno, opacidad—. Esta categoría **no fija valores visuales**, por dos razones que conviven: la variante DX no dibuja, y el anti-patrón de wireframe con detalle visual sigue rigiendo. Lo que sí queda fijado acá es la propiedad de contrato que el integrador consume: **hay a lo sumo un resaltado por instancia y corresponde al índice pedido**. Los valores concretos se resolvieron sobre la maqueta de `GeometriaFactory-Web` y quedaron registrados en la `Linea-Base-Visual.md` de esa categoría 03.

## 5. Cómo leer esta sección

1. Empezar por `DX-Developer-Experience.md` §1.3, que son los tres enunciados que ordenan todo lo demás. El primero —**todo pasa por las seis funciones**— es el que sostiene el punto de extensión.
2. Ejecutar `Guia-Onboarding-Developer.md` de arriba hacia abajo, una sola vez. Es el modo tutorial: enseña ejerciendo, y sus tres tramos cierran con objetivos que se verifican mirando.
3. Volver a `DX-Error-Messages.md` cuando aparezca una condición. Está escrito para entrar por una entrada suelta: cada una nombra su código, su función y qué le queda por hacer al anfitrión.
4. Tener `Glosario-UX.md` a mano al entrar por una sección suelta. Es donde se resuelve por qué el sustantivo «recorrido» va siempre calificado —«de integración» o «de ida y vuelta»—, con qué alcance rige esa invariante y en qué secciones se verificó la colisión, y qué términos son de esta categoría y cuáles se referencian de 02 y del glosario raíz.

**Advertencia para las categorías aguas abajo.** Esta categoría **no decide** la elección del motor de dibujo tridimensional ni su versión, que son de 05-Arquitectura-Tecnica; ni la maqueta, que es de la Fase B2; ni el pipeline, que es de 09; ni la materialización del sample S-1, que es de 10-Examples. Y no documenta nada que el archivo de guion no haga: no hay red, no hay configuración, no hay autorización, no hay persistencia y no hay validación de reglas de dominio. Si un párrafo de acá sugiriera que el visor obtiene algo por su cuenta, estaría documentando una violación de `RA-01` y `RA-02`, y sería un defecto.

## 6. Autoverificación contra los criterios de aceptación de la categoría

Verificación ítem por ítem de `Rules-UX-UI-DX.md` §6. Los criterios de la variante UX/UI se declaran **no aplicables con su motivo**, en lugar de darse por cumplidos.

| # | Criterio | Resultado | Evidencia o motivo |
| --- | --- | --- | --- |
| 1 | Variante declarada en cada cabecera y coherente con el tipo D8 | Cumple | Los cinco documentos declaran `Variante: DX`, que es la que §1.2 asigna a `library` |
| 2 | `Experiencia-De-Uso.md` con sus once secciones | **No aplicable** (UX/UI) | Obligatorio sólo para tipos con UI final. `tiene_ui_final` = false. Omisión declarada en §3 |
| 3 | Al menos un `wireframes-<superficie>.md` por superficie clave, con nueve secciones | **No aplicable** (UX/UI) | El mínimo de wireframes para `library` es cero (§2.2). No hay superficies propias |
| 4 | `DX-Developer-Experience.md` con las nueve secciones del §4.2.3, con Diátaxis y tramos 5/30/60 verificables | Cumple | Las nueve secciones están, en orden; §2 trae los tres tramos con objetivo verificable y §4 el plan Diátaxis con dueño por modo |
| 5 | Accesibilidad con WCAG 2.2 AA como piso | **No aplicable** (UX/UI) | Ninguna accesibilidad se declara acá, porque esta categoría no produce ninguna superficie visual. La accesibilidad de la superficie donde la escena queda embebida pertenece a la categoría 03 del proyecto de código anfitrión, en variante UX/UI, y a la Fase B2. El criterio es condicional —«toda accesibilidad declarada»— y acá no hay antecedente que lo dispare |
| 6 | Cada wireframe enumera vacío, cargando, con datos y error | **No aplicable** (UX/UI) | No hay wireframes |
| 7 | Cada `dx-` doc con quick-start verificable, ejecutable y reproducible | Cumple | `DX-Developer-Experience.md` §3, cinco pasos dentro del entorno de desarrollo contenido con resultado observable en cada uno; `Guia-Onboarding-Developer.md` §3.1 lo ejecuta con la secuencia neutral de invocaciones de §3.2. `DX-Error-Messages.md` y `Glosario-UX.md` enlazan al mismo quick-start en lugar de duplicarlo |
| 8 | Trazabilidad upstream y downstream en cada artefacto | Cumple | Cabecera con secciones concretas en los cinco documentos, y tabla de trazabilidad en `DX-Developer-Experience.md` §8, `DX-Error-Messages.md` §7 y `Guia-Onboarding-Developer.md` §6 |
| 9 | Ningún archivo con sufijo de versión; versión en la cabecera; slug en Título-Con-Guiones | Cumple | Cinco archivos, ninguno con sufijo; los cinco declaran `Versión: 1.0` |
| 10 | Un solo archivo por nombre lógico; superadas en `_legacy/` | Cumple | No hay versiones superadas; `_legacy/` no existe y no corresponde crearla |
| 11 | Existe `Glosario-UX.md` y su tabla no está vacía | Cumple | Diecinueve términos acuñados en §2, en cuatro tablas |
| 12 | Todo término presente en más de un artefacto está en el glosario, con sus referentes | Cumple | Regla de inclusión declarada en `Glosario-UX.md` §1 y columna «artefactos donde aparece» en cada fila |
| 13 | El glosario no duplica términos de `Glosario-Funcional.md` de 02; los reusados se referencian | Cumple | `Glosario-UX.md` §4.1 referencia los **veinticuatro** términos de 02 sin redefinir ninguno —eran veinte hasta que la capacidad F-25 y la sexta función sumaron movimiento automático, órbita de la cámara, giro de las figuras y estado efectivo del movimiento—; §4.2 y §4.3 hacen lo mismo con el glosario raíz y con los demás documentos del producto. El conteo sigue el valor que `Glosario-Funcional.md` fija |
| 14 | Ninguna polisemia con contextos disjuntos reportada como defecto ni corregida calificando todo | Cumple, **tras corrección** | `Glosario-UX.md` §3.3 adopta sin reabrirla la resolución de `Glosario-Funcional.md` §3.3 sobre «escena», «malla», «árbol» e «instancia», y el audit r1 §4.4 confirmó que las once polisemias evaluadas y descartadas no debían corregirse. La única desambiguación nueva, «recorrido», estaba **declarada y no cumplida** (**H-03**) y su evidencia de colisión no era verificable (**H-04**): en esta ronda se calificaron las veinte ocurrencias de sustantivo desnudo, se declaró el alcance de la invariante —uso, no mención ni forma verbal— y se reemplazó la evidencia por las dos secciones donde los dos sentidos conviven en la misma tabla. La afirmación anterior de esta fila y la de §5 punto 4 eran falsas y quedan corregidas |
| 15 | Sin menciones a stacks concretos, productos comerciales ni protocolos del dominio fuente | Cumple | Se usa el vocabulario neutral que ya fijó 02: archivo de guion, motor de dibujo tridimensional, capacidad gráfica tridimensional, elemento de dibujo, entorno de desarrollo contenido. Las únicas cadenas literales son las **seis** funciones y los siete códigos, que son nombres del contrato, y los dos guiones de construcción declarados por PRODUCT-INTAKE §16 |
| 16 | Con `requiere_maqueta` = true: nombre canónico de superficie por wireframe y estados de la maqueta | **No aplicable** (UX/UI) | No hay wireframes que declaren superficie. Lo que la maqueta demostró se fijó sobre la superficie del anfitrión, en la maqueta de `GeometriaFactory-Web` (§4) |
| 17 | Con maqueta ya aprobada: artefactos retroalimentados y los tres documentos de la fase presentes | **Cumple, con constancia** | La Fase B2 corrió y quedó aprobada, y los tres documentos DX de §2 absorbieron su retroalimentación —capacidad F-25 y el cero como dimensión legible— con el motivo declarado en su control de cambios. Los tres documentos de la fase **no están en esta carpeta y no faltan**: este proyecto de código no tuvo maqueta propia y se validó dentro de la maqueta de `GeometriaFactory-Web`, que es donde se emitieron. §4 lo declara |
| 18 | Tabla de contenido en todo documento de más de tres secciones de primer nivel | Cumple | Los cinco documentos superan las tres secciones y los cinco traen tabla de contenido con anclas de primer y segundo nivel, inmediatamente después de la cabecera |

**Resumen: 12 criterios cumplidos —uno de ellos, el 17, con la constancia de que los tres documentos de la fase viven en la categoría 03 de `GeometriaFactory-Web`— y 6 declarados no aplicables por ser de la variante UX/UI.**

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Índice de los cuatro documentos vigentes de la categoría en variante DX, declaración de las cinco omisiones con su motivo —incluidos `Experiencia-De-Uso.md` y los wireframes por no haber UI final, y `DX-Portal-Developers.md` por no haber portal—, declaración de los tres artefactos de maqueta como **previstos para la Fase B2** y no como omitidos, constancia sobre el aspecto del resaltado que `CU-03` §10 difiere a esta categoría, guía de lectura y autoverificación ítem por ítem contra los dieciocho criterios de aceptación de `Rules-UX-UI-DX.md` §6. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-07**: §3 decía «Los tres de esta tabla» sobre una tabla de cinco filas; pasa a «Los cinco», coherente con el propio control de cambios. **H-06**: §2 decía «once síntomas» y la tabla de §4 de la guía tiene **diez**; el conteo queda corregido. **H-03 y H-04**: el criterio 14 de §6 declaraba cumplida una invariante léxica que estaba incumplida, y §5 punto 4 afirmaba que el sustantivo «recorrido» nunca aparecía desnudo; las dos afirmaciones eran falsas y se reescriben declarando la corrección aplicada y su alcance. **H-05**: el criterio 13 de §6 decía «diecinueve términos de 02», que acuña **veinte**. La declaración de los tres artefactos de maqueta como **previstos para la Fase B2** no se toca: el audit la dio por correcta sin reserva. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código por no tener maqueta propia. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **§4** pasa de «Artefactos previstos para la Fase B2» a «Artefactos de la Fase B2» —la tabla de contenido acompaña el cambio de ancla— y declara que la fase **ya corrió y quedó aprobada**, que este proyecto de código **no tuvo maqueta propia por decisión del Product Owner** y que sus tres artefactos de línea de base —`Linea-Base-Visual.md`, `Contrato-Datos-Maqueta.md` y `Bitacora-Validacion-Maqueta.md`— se emitieron en la categoría 03 de `GeometriaFactory-Web`: no están omitidos ni faltan, están **emitidos en otra carpeta**, y esta no los duplica; la constancia sobre el aspecto del resaltado se reubica al final de la sección y remite a esa línea de base. **§3** ajusta la frase que contrastaba con §4. **§2** actualiza los propósitos de los tres documentos DX: el gobierno del movimiento automático de la escena y el conteo de síntomas de la guía, que pasa de **diez** a **doce**, y las **seis** situaciones que no son entradas del catálogo. **§6** actualiza el criterio 16 y el criterio 17, que pasa de «no aplicable todavía» a **cumple con constancia**, y el resumen pasa a **12 cumplidos y 6 no aplicables**. Ningún código de condición nuevo y ninguna función nueva: la lista de `Definicion-Contrato-De-Fachada.md` §6 sigue cerrada en siete y la superficie pública, en cinco funciones. |
| 1.0 | 2026-08-09 | Alineación con la **sexta función de la fachada**, `establecerMovimiento(id, opciones)`, que el Product Owner decidió al cerrar la validación visual de la **Fase B2** y que `Definicion-Contrato-De-Fachada.md` §4.6 acuña, con contrato de uso en el **`CU-07` nuevo** y consolidación en el intake **1.6**. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`; sin snapshot y sin `_legacy/`. **(a)** **§1** declara la superficie en **seis funciones planas**, y deja constancia de que las **siete** garantías y los **siete** códigos **no se movieron**: la sexta función no acuña ni garantía ni código. **§3** —motivo de la omisión de wireframes—, **§5 punto 1** y el **criterio 15** de §6 recuentan lo mismo. **(b)** **§2** actualiza los propósitos: `DX-Developer-Experience.md` documenta la función nueva con su firma, su retorno y su única condición; la guía cambia el estado del movimiento **en vivo** en lugar de reconstruir la instancia y pasa de **doce** a **catorce** síntomas; el catálogo pasa de **doce** a **trece** entradas, `E-VIS-01` a `E-VIS-13`, **derivadas de los mismos siete códigos**. **(c)** **§4** registra la segunda ronda de absorción de la Fase B2 y suma la **frontera entre el bundle y el anfitrión**: el control visible, la consulta de la preferencia de movimiento reducido y la conservación de la elección son del anfitrión, porque hacerlas la fachada violaría G-3 y G-2. **(d)** El **criterio 13** de §6 pasa de **veinte** a **veinticuatro** términos del glosario funcional de 02, que es el valor que `Glosario-Funcional.md` fijó al sumar los cuatro del movimiento automático; el **criterio 11**, que cuenta los **diecinueve** términos propios de esta categoría, **no cambia**. La cabecera pasa a `CU-01` a `CU-07` con su orden de lectura. El resumen de §6 sigue en **12 cumplidos y 6 no aplicables**. |
| 1.0 | 2026-08-09 | Corrección absorbida de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, **sin subir versión** por `Master-Prompt.md` §5. **`AB2-10`**: la fecha de cabecera decía 2026-08-08 y el documento tiene entradas de control de cambios fechadas 2026-08-09; pasa a **2026-08-09**, que es cuando se lo tocó por última vez. Ningún contenido cambia. |
