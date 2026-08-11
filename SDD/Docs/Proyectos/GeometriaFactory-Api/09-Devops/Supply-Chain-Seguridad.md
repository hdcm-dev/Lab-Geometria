# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Platform Engineer (AG-09)
**Tipo de proyecto de código (D8):** `rest-api` · **Proyecto de código principal del producto**
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) 1.0; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §1, §2 y §3; [`../../GeometriaFactory-Infrastructure/09-Devops/Supply-Chain-Seguridad.md`](../../GeometriaFactory-Infrastructure/09-Devops/Supply-Chain-Seguridad.md) 1.0 §1 y §7; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §11, §13, §14, §17.5.P.1, §17.5.P.5, §17.5.P.7, §17.5.P.9 y §17.5.P.10
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Image-Docker.md`](Guia-Publicacion-Image-Docker.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. La superficie expuesta como preocupación de cadena de suministro](#7-la-superficie-expuesta-como-preocupación-de-cadena-de-suministro)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

**Y el rasgo que hace de éste el documento más cargado de la categoría en todo el producto.** Es la unidad desplegable que **sostiene el dato, las reglas de negocio y la única base de datos del producto** (intake §13), la que **embebe a tres proyectos de código**, la que **expone el único punto de entrada al servidor propio** y la que se despliega **construyendo en destino, sin registro intermedio**. Las cuatro cosas son afirmaciones de la fuente y las cuatro tienen consecuencia acá.

## 1. Inventario de componentes

**Decisión de esta categoría: el inventario de esta unidad se emite en el stage `imagen`, sobre lo que la imagen efectivamente lleva.** Es el inventario que más importa del producto: la imagen es lo que corre en el servidor donde vive el dato.

| Qué entra a la imagen | De dónde viene | Quién lo ancla |
| --- | --- | --- |
| El entorno de ejecución de la plataforma, **sin kit de desarrollo ni depurador** | La imagen base de ejecución, **sin linaje con la del contenedor de desarrollo** | Esta categoría, en el archivo de construcción; la versión se ancla en la etapa `a` |
| Las dependencias core de este proyecto de código, incluida la de **acceso firmado** | Intake §17.5.P.1 | El equipo, en la etapa `a` |
| **Las dependencias externas de `GeometriaFactory-Infrastructure`**, que son **tres** y de las cuales **dos son sensibles** | Intake §17.3.P.1 | `GeometriaFactory-Infrastructure`; ver [`../../GeometriaFactory-Infrastructure/09-Devops/Supply-Chain-Seguridad.md`](../../GeometriaFactory-Infrastructure/09-Devops/Supply-Chain-Seguridad.md) §1 |
| `GeometriaFactory-Application`, `GeometriaFactory-Domain` y `GeometriaFactory-Contracts`, **sin dependencias externas propias** | Intake §17.2.P.1, §17.1.P.1 y §17.4.P.1 | — |
| El **bundle del visor** | **No entra.** Viaja en la otra unidad desplegable | Intake §13 |

**La tercera fila es la que obliga a que el inventario se tome sobre la imagen y no sobre el archivo de proyecto de este proyecto de código.** La mayor parte de las dependencias externas que llegan al servidor propio **no las declara este proyecto de código**: las trae `GeometriaFactory-Infrastructure`, y dos de ellas son las piezas más sensibles del producto. Un inventario tomado sobre la superficie propia describiría lo que menos riesgo tiene.

**La quinta fila es una separación que conviene tener escrita.** El motor de dibujo tridimensional **nunca llega al servidor donde vive el dato**: queda dentro del bundle, que viaja en la publicación del front. Es una consecuencia de la topología del intake §14 y **reduce a la mitad la superficie de terceros de esta unidad**.

| Aspecto del inventario | Decisión |
| --- | --- |
| Cuándo se emite | En el stage `imagen`, sobre la imagen construida para medir `PT-04` |
| Dónde se adjunta | Al **informe de cierre** de la etapa |
| Formato y generador | **No se nombran.** Ninguna fuente los declara y su elección es de la etapa `a`, por la regla de anclaje de versiones. Ver `PD-02` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |
| Qué **no** cubre | Lo que el destino agregue al reconstruir. Ver §3 |

## 2. Firma del artefacto

**No se firma, y la brecha se declara en lugar de darse por cubierta.**

| Requisito | Estado | Motivo |
| --- | --- | --- |
| Firma de la imagen | **No cumplido, y además no tendría objeto en este canal.** El intake §17.5.P.7 declara que **la imagen no se publica en ningún registro**: se construye en destino. **No hay artefacto en tránsito que firmar**, porque lo que viaja es el código fuente desde el repositorio | Intake §17.5.P.7 |
| Registro público de transparencia | **No cumplido** | Lo mismo, y además exigiría infraestructura que el intake §10 no financia |
| Integridad de lo que sí viaja | **Parcialmente cumplido, y es lo que corresponde mirar acá.** Lo que llega al destino es **una etiqueta del repositorio**, y su integridad es la del propio repositorio | `05` §5; [`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4 |
| Integridad del origen | **Cumplido**: etiqueta por etapa cerrada, y reversión apoyada en ella | Intake §17.5.P.7 |

**El desplazamiento que este canal produce, dicho sin suavizar.** En un modelo con registro, la firma protegería la imagen entre quien la construye y quien la corre. Acá **quien la construye es quien la corre**, de modo que la pregunta de confianza se desplaza al eslabón anterior: **que lo que el destino trae del repositorio sea lo que la etapa cerró**. Lo que hoy sostiene eso es la etiqueta y el control de acceso del propio repositorio, y **no hay una comprobación criptográfica declarada**. Es la brecha, y queda escrita.

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido en la canalización**: `scripts/build.sh`, `scripts/test.sh` y el archivo de construcción multietapa son los mismos en la máquina de quien construye y en el pipeline | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

**Y una brecha propia de este canal, que ninguna otra unidad del producto tiene.** La imagen que la canalización verifica **no es la imagen que corre**: la del servidor propio se construye ahí, en otro momento y sobre otra máquina. Dos consecuencias que se declaran en lugar de disimularse:

| Consecuencia | Qué implica |
| --- | --- |
| **El inventario del §1 describe la imagen verificada, no exactamente la desplegada** | Si entre una y otra cambió algo que la construcción resuelve —una versión no anclada, un repositorio de paquetes que devuelve otra cosa—, las dos imágenes pueden diferir. **La regla de anclaje de versiones del intake es lo único que hoy lo acota**, y por eso acá no es una preferencia de estilo sino el mecanismo principal |
| **La reproducibilidad no está verificada entre las dos máquinas** | Ninguna fuente exige compararlas y esta categoría **no declara que sean idénticas**. Lo que declara es que la única garantía disponible es el anclaje explícito de toda versión |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**. **La elevación es de nivel producto.**

## 4. Análisis de dependencias

| Comprobación | Umbral o criterio | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Anclaje explícito de **toda** versión que entra a la imagen | Toda versión **fijada explícitamente**; un cambio mayor **se documenta, nunca es efecto colateral** | Revisión de los archivos de proyecto y del de construcción, en la etapa `a` y en cada cambio. Acá es **el mecanismo principal**, por §3 | Bloqueante como regla del intake, encabezado de la Parte C |
| Contenido de la imagen final | **Sólo el entorno de ejecución**, sin kit de desarrollo ni depurador, y **sin linaje con la imagen del contenedor de desarrollo** | Inspección del archivo de construcción | **Bloqueante**: Definition of Done §1.4 |
| Puertos publicados hacia el enrutador | **Uno**, y es el único punto de entrada al servidor propio | Inspección del archivo de composición | `05` §5 |
| Configuración de intercambio declarada en el producto | **1** sola | `QG-10`, con `TC-29`, en el stage `build` | **Bloqueante** |
| Actualización automática de dependencias | **No se declara ninguna.** Contradiría la regla de anclaje, y acá además haría divergir la imagen verificada de la desplegada | — | — |

**La segunda fila es un control de superficie de ataque escrito como control de empaquetado.** Una imagen que llevara el kit de desarrollo al servidor domiciliario multiplicaría lo que un acceso indebido puede hacer ahí, y el intake §17.5.P.9 lo prohíbe con esas palabras. La Definition of Done §1.4 lo verifica **por inspección del archivo de construcción**, que es donde se ve el linaje.

**La quinta fila tiene acá un motivo extra respecto del resto del producto.** En las bibliotecas, una actualización automática rompería la regla de anclaje; acá, además, **haría que la imagen desplegada dejara de corresponder a la verificada**, que es la brecha declarada en §3.

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «en 0 **y sin advertencias**» | Intake §17.5.P.8; `QG-01` |
| Estático de superficie | **Existe, bloquea y es la verificación característica de este proyecto de código**: `QG-05` sobre los **quince** puntos en las dos direcciones, `QG-06` sobre los **quince** códigos del contrato, `QG-08` sobre las respuestas y el registro del servidor, y `QG-10` sobre la composición de raíz | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| **Dinámico** | **Existe, y es el más completo del producto**: la batería de integración **golpea la superficie real por su protocolo contra el almacén real**, y `QG-12` exige verificar **forzando la petición** y no por la interfaz | Intake §17.5.P.6; `Estrategia-Calidad.md` §1 |
| Dinámico sobre el artefacto empaquetado | **Existe**: el stage `imagen` arranca la imagen, aplica las transformaciones sobre un almacén vacío y comprueba salud | `PT-04`; `QG-13` |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con el sujeto más sensible**: el intake §17.5.P.5 declara que la clave de firma va **como secreto del repositorio, nunca en el archivo del flujo de trabajo** | [`Entornos-Deploy.md`](Entornos-Deploy.md) §6 |

**La tercera fila es la que hace de este proyecto de código el que más superficie verifica del producto, y `QG-12` es su caso extremo.** `Estrategia-Calidad.md` §3 lo declara: es **el único criterio de verificación del producto que la fuente exige ejercer forzando la petición**, y no mirando una pantalla. Desde la cadena de suministro, la lectura es que **la comprobación de que un control existe no puede hacerse sobre el cliente que respeta el control**.

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre una de las **dos bibliotecas sensibles** que embebe —derivación de clave o emisión de acceso firmado— | Se ancla la versión corregida en `GeometriaFactory-Infrastructure` y **se despliega esta unidad**: es la única forma de que la corrección llegue al servidor. Los accesos vigentes caducan solos, porque la vigencia es **corta** y **no hay acceso de refresco** | El equipo ancla; el Product Owner despliega |
| Vulnerabilidad sobre la **imagen base de ejecución** | Se ancla la versión corregida en el archivo de construcción y se vuelve a desplegar. **La reconstrucción en destino la trae**, y ése es el único caso donde ese canal juega a favor | El equipo, con constancia |
| Vulnerabilidad sobre el **entorno del servidor propio**, fuera de la imagen | **Fuera del alcance de esta cadena.** Es la máquina del Product Owner | El Product Owner |
| Exposición de la **clave de firma** | Se rota el valor en el ambiente y se reinicia el servicio. **El valor no está en el repositorio ni en la imagen**, de modo que la rotación no exige reconstruir | Intake §17.5.P.5 |
| Exposición de la **dirección del servidor propio** | Se revisa por dónde se filtró: `QG-08` mide **0** respuestas que la expongan, sobre los **quince** puntos **y** sobre el registro del servidor. Es `RA-03`, y `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 lo ubica **en el último tramo antes de salir del servidor propio** | El equipo, con constancia |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

**Y dos riesgos aceptados por escrito que esta categoría transcribe y no reabre.** El intake §17.5.P.5 declara que **el tramo entre el front y este servicio viaja en claro si ese salto es HTTP plano**, con el túnel saliente como salida **documentada y no adoptada**; y registra la **nota de seguridad sobre el flujo de credenciales**, aceptado porque el intermediario es el propio front del mismo sistema, el tramo hacia el navegador es seguro y el alcance es un laboratorio de aula. Las dos son decisiones del Product Owner registradas aguas arriba.

## 7. La superficie expuesta como preocupación de cadena de suministro

Esta sección existe porque acá, además de dependencias, **hay algo que ninguna otra unidad del producto tiene: un puerto abierto hacia afuera en la máquina donde vive el dato**.

| Propiedad | Por qué es de cadena de suministro y no sólo de diseño |
| --- | --- |
| **Un solo punto de entrada al servidor propio** | `05` §5 lo declara: todo lo que este proyecto de código no exponga **no existe para nadie de afuera**. La superficie de ataque de la máquina **es exactamente la lista de quince puntos**, y por eso un punto nuevo es una decisión de exposición y no una funcionalidad |
| **Exactamente 4 puntos fuera de la guardia, ni uno más** | `QG-05`, medido **en las dos direcciones**. `05` §9 declara el riesgo: un punto nuevo fuera de la guardia hace que una regla del producto deje de valer **y nada falla** |
| **El punto de salud no exige acceso y no diagnostica** | [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) §2, regla 4: responde por el estado del servicio y **no dice dónde está el almacén, ni con qué esquema, ni qué ruta se configuró**. Es el punto que **cualquiera puede llamar**, y por eso es el que menos puede contar |
| **Ningún mensaje expone dirección, ruta, secreto ni traza** | `QG-08`, con umbral **0**, sobre los quince puntos **y sobre el registro del servidor** |
| **Tres familias empobrecidas indistinguibles** | `QG-07`, **3 de 3** en cuerpo y en código. Es lo que impide que la superficie revele la existencia de un recurso ajeno, y `Estrategia-Calidad.md` §3 declara que **ninguna capa de adentro puede repararlo** |

**Las cinco comparten la propiedad que las hace un problema de esta categoría y no sólo de la 05**: **su incumplimiento no produce ningún fallo**. Un punto agregado fuera de la guardia responde bien; una respuesta más informativa se ve mejor; un mensaje con la dirección adentro ayuda a diagnosticar. **Las cinco se miden con recuentos y ninguna con un juicio**, y por eso corren en cada pull request que toca la superficie, que es la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 materializa como trigger propio.

**Y la advertencia que cierra el documento**: esas cinco reglas protegen **la única máquina del producto donde vive el trabajo de la comisión**. El intake §11 registra desde el negocio que su caída es un riesgo aceptado con estado degradado; **su exposición indebida no está aceptada por nadie**, y no tiene ninguna mitigación posterior.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría. Decide emitir el **inventario en el stage `imagen`, sobre lo que la imagen lleva y no sobre la superficie propia**, con el fundamento de que **la mayor parte de las dependencias externas del servidor propio las trae `GeometriaFactory-Infrastructure`**, y deja escrito que el motor de dibujo **nunca llega al servidor donde vive el dato**. Declara que **no se firma y que en este canal la firma no tendría objeto**, porque lo que viaja es el código y no un artefacto: la pregunta de confianza **se desplaza al eslabón anterior**, y esa brecha queda escrita. Declara una brecha propia del canal —**la imagen verificada no es la imagen que corre**— con sus dos consecuencias, y que **la regla de anclaje de versiones es acá el mecanismo principal y no una preferencia de estilo**. Declara el análisis dinámico más completo del producto y `QG-12` como su caso extremo. Cierra con la sección propia: **la superficie expuesta es acá la preocupación de cadena de suministro que importa**, con **cinco** propiedades que comparten que su incumplimiento **no produce ningún fallo**, sobre la única máquina del producto donde vive el trabajo de la comisión. |
