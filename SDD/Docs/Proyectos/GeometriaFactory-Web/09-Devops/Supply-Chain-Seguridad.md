# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8, §9 y §11; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §1, §2 y §3; [`../05-Arquitectura-Tecnica/Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md`](../05-Arquitectura-Tecnica/Adrs/ADR-10003-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md`](../05-Arquitectura-Tecnica/Adrs/ADR-10006-Aislamiento-Del-Visor-Tras-Su-Fachada.md) 1.0; [`../../GeometriaFactory-Visor/09-Devops/Supply-Chain-Seguridad.md`](../../GeometriaFactory-Visor/09-Devops/Supply-Chain-Seguridad.md) 1.0; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §14, §17.6.P.1, §17.6.P.5, §17.6.P.8, §17.6.P.9 y §17.7.P.1
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. Las tres reglas de arquitectura como preocupación de cadena de suministro](#7-las-tres-reglas-de-arquitectura-como-preocupación-de-cadena-de-suministro)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.

**Y una diferencia con los cinco proyectos de código que no se despliegan.** Aquéllos declararon que su inventario y su firma se emiten «en las dos unidades desplegables». **Ésta es una de las dos**, y por lo tanto acá el documento tiene sujeto real: hay un artefacto que sale del repositorio y llega a un servidor de terceros.

## 1. Inventario de componentes

**Decisión de esta categoría: se emite inventario, y cubre las dos cadenas.** Es una unidad desplegable con dos cadenas de herramientas, y un inventario tomado sobre una sola de ellas dejaría fuera exactamente lo que más importa.

| Cadena | Qué contiene | Por qué no alcanza con la otra |
| --- | --- | --- |
| **Plataforma** | La biblioteca de componentes de interfaz, cuya versión la fuente deja **[A VERIFICAR]** y se ancla al crear el andamiaje; y los tipos de `GeometriaFactory-Contracts` compilados adentro, que **no tienen dependencias** | Es la única cadena que un inventario convencional del ecosistema de la plataforma vería |
| **Navegador** | **El motor de dibujo tridimensional, que queda dentro del bundle** por la puerta `PT-03` | Un inventario de la cadena de la plataforma **no lo vería**: no es una dependencia declarada de este proyecto de código, es código empaquetado dentro de un archivo de recursos estáticos que esta unidad transporta |

**La segunda fila es la razón de esta decisión, y no es original de acá**: [`../../GeometriaFactory-Visor/09-Devops/Supply-Chain-Seguridad.md`](../../GeometriaFactory-Visor/09-Devops/Supply-Chain-Seguridad.md) ya declaró que emite inventario **por el mismo motivo**, con su alcance acotado al bundle. Esta categoría **no lo duplica**: el inventario del bundle lo produce el proyecto de código que lo empaqueta, y el de esta unidad **lo incorpora** en el paso 4 del flujo, cuando el bundle entra a los recursos estáticos.

| Aspecto del inventario | Decisión |
| --- | --- |
| Cuándo se emite | En el flujo de publicación, sobre el estado que efectivamente se sube |
| Qué cubre | Las dos cadenas, con el inventario del bundle incorporado y no recalculado |
| Dónde se adjunta | Al **informe de cierre** de la etapa, junto con el registro del flujo |
| Formato y generador | **No se nombran.** Ninguna fuente los declara y su elección es de la etapa `a`, por la regla de anclaje de versiones. Ver `PD-03` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

## 2. Firma del artefacto

**No se firma, y la brecha se declara en lugar de darse por cubierta.**

| Requisito | Estado | Motivo |
| --- | --- | --- |
| Firma del artefacto publicado | **No cumplido** | El canal de entrega es una **subida por FTP a un hosting gratuito de terceros**, y **ninguna fuente declara un mecanismo por el que quien recibe pueda verificar una firma**. Una firma emitida acá no tendría verificador: el navegador que consume el front verifica el transporte, no la autoría del despliegue |
| Registro público de transparencia | **No cumplido** | Lo mismo, y además exigiría infraestructura que el intake §10 no financia |
| Integridad del origen | **Cumplido** | Etiqueta por etapa cerrada, y reversión apoyada en ella ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4; `05` §5) |
| Integridad del artefacto respecto de su fuente | **Cumplido para el bundle**, que es la parte generada: `QG-02` exige que se genere en el mismo flujo y `QG-09` de `GeometriaFactory-Visor` prohíbe editarlo a mano | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 |

**Lo que la ausencia de firma deja abierto, dicho sin suavizar**: quien reciba el contenido del hosting no tiene modo de comprobar que fue este flujo el que lo puso ahí. La mitigación disponible **no es criptográfica sino de acceso**: las credenciales del canal viven como secreto del repositorio, con alcance mínimo, y no entran al árbol de fuentes (intake §17.6.P.5).

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** El flujo de publicación está versionado en el repositorio (intake §16) y los pasos 1 a 5 se reproducen con los guiones del repositorio dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero.

**Una precisión que sólo corresponde a las dos unidades desplegables.** Acá la elevación de nivel **sí tendría sujeto**, porque hay un artefacto que sale del repositorio; en las cinco bibliotecas del producto no lo tenía. Que igual no se eleve es una consecuencia del presupuesto declarado y no una omisión de análisis. **La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia del artefacto del servidor propio.

## 4. Análisis de dependencias

| Comprobación | Umbral | Cómo se ejecuta | Carácter |
| --- | --- | --- | --- |
| Bibliotecas de guion agregadas que consulten servicios por su cuenta | **0** | `QG-06`, con `TC-10030`, inspección del árbol de fuentes y de las dependencias de guion | **Bloqueante** |
| Salidas hacia el servicio de datos | Exactamente **1**, el cliente tipado | El mismo gate | **Bloqueante** |
| Dependencias traídas de una red de distribución externa en tiempo de ejecución | **0** | Puerta `PT-03`, medida del lado de `GeometriaFactory-Visor`: **el motor de dibujo queda dentro del bundle** | **Detiene la planificación de la etapa `g`** |
| Instalación de dependencias del navegador | **Reproducible desde el archivo de bloqueo**, no resolución libre | Paso 3 del flujo | Bloqueante por construcción |
| Versión de la biblioteca de componentes de interfaz | **Anclada y registrada** al crear el andamiaje | `BT-10002`, etapa `a`. Está **[A VERIFICAR]** en la fuente y **no se inventa acá** | Bloqueante como tarea de la etapa `a` |
| Actualización automática de dependencias | **No se declara ninguna.** Contradiría la regla de anclaje del intake, que prohíbe que una versión cambie **como efecto colateral de una actualización** | — | — |

**La primera fila es el gate de dependencias más importante del producto, y no se lee como tal a primera vista.** `QG-06` no mide una vulnerabilidad: mide que **nadie agregó una biblioteca de guion que hable con un servicio por su cuenta**. Es la puerta por la que `RA-01` se rompería sin que nadie lo note, porque una biblioteca así no viola ninguna regla del lenguaje ni rompe ninguna compilación. En este proyecto de código, **agregar una dependencia del navegador es un acto de seguridad de la topología antes que un acto de construcción**.

**La regla de anclaje de versiones del producto rige en las dos cadenas**: el intake, en el encabezado de su Parte C, declara que toda versión se fija explícitamente y que un cambio de versión mayor **se documenta, nunca es efecto colateral de una actualización**.

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «sin advertencias» | Intake §17.6.P.8; `QG-01` |
| Estático de estructura | **Existe, bloquea, y es la verificación característica de este proyecto de código**: `QG-06` sobre el árbol de fuentes y las dependencias de guion, `QG-08` sobre el traductor de condiciones y `QG-09` sobre las invocaciones al bundle | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| **Dinámico** | **Existe, y acá sí tiene sujeto**, a diferencia de las cinco bibliotecas del producto: `QG-05`, `QG-07` y `QG-10` se miden **sobre el producto corriendo en un navegador**, contando peticiones, leyendo el almacenamiento y observando el tráfico del circuito | `Estrategia-Calidad.md` §3; `Plan-Pruebas.md` §3 |
| Condición de las mediciones de ausencia | **`QG-05` se mide con los dos movimientos automáticos prendidos.** Un conteo con los movimientos apagados daría cero sin haber ejercitado el bucle de dibujo | `Estrategia-Calidad.md` §3, `QG-05` |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con sujeto propio**: este proyecto de código es el que administra los **dos** secretos del producto que viven en el repositorio | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**La tercera fila es lo que hace de este proyecto de código el único con análisis dinámico real del producto.** Y trae una dependencia de ejecutor que conviene no perder: sin **navegador con capacidad gráfica tridimensional y un conductor capaz de contar peticiones y leer el almacenamiento**, `QG-05`, `QG-07` y `QG-10` no se pueden medir en la canalización y quedan como medición manual registrada. Está declarado como `PD-04` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, y es el mismo requisito que `GeometriaFactory-Visor` registró del otro lado de la fachada.

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la biblioteca de componentes de interfaz | Se ancla la versión corregida y se registra en el punto de control de la etapa. **Si la versión corregida exige una plataforma que el hosting no soporta**, el conflicto es con `PT-01.a` y la salida declarada es bajar la versión objetivo del front, no la del backend | El equipo, y el Product Owner si hay que bajar la versión objetivo |
| Vulnerabilidad sobre el motor de dibujo tridimensional | **No se remedia acá**: viaja dentro del bundle y su anclaje pertenece a `GeometriaFactory-Visor`. Lo que esta unidad tiene que hacer es **volver a publicar** para que el bundle corregido llegue al hosting | Categoría 09 de `GeometriaFactory-Visor`, y esta categoría publica |
| Vulnerabilidad sobre la plataforma de ejecución del hosting | **No está bajo control del producto**: el hosting es un servicio gratuito de terceros. Lo que sí está bajo control es la versión objetivo del front | El Product Owner, con constancia |
| Exposición de las credenciales del canal de publicación | **Rotación inmediata del secreto y republicación.** El valor no está en el repositorio, de modo que la rotación no exige tocar código | Intake §17.6.P.5 |
| Exposición de la dirección del servidor propio | Rotación del secreto, republicación y **revisión de por dónde se filtró**: `QG-08` mide **0** mensajes que la expongan, sobre los **diecisiete** códigos vivos y el camino de ausencia de respuesta | Es `RA-03`; `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos, y el intake §10 declara que **ninguna normativa de compliance aplica**: es un laboratorio de aula con cuentas creadas para la materia.

**Y un riesgo aceptado por escrito que esta categoría no reabre.** El intake §17.5.P.5 declara que el tramo entre el front y el servicio de datos **viaja en claro si ese salto es HTTP plano**, con el túnel saliente como salida **documentada y no adoptada**. Es decisión del Product Owner registrada aguas arriba; esta categoría la transcribe y **no la suaviza ni la agrava**.

## 7. Las tres reglas de arquitectura como preocupación de cadena de suministro

Esta sección existe porque el riesgo característico de este proyecto de código **no entra por una dependencia vulnerable**, y decirlo sin ofrecer dónde sí está dejaría el documento vacío.

[`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §2 lo declara: **este proyecto de código no puede delegar la seguridad de la topología**, no porque maneje secretos sino porque **es el único punto de contacto del navegador**. Si acá aparece una petición del navegador hacia el servicio de datos, la partición del producto deja de existir.

| Regla | Qué la rompería desde acá | Con qué recuento se mide | Qué pasa si se rompe |
| --- | --- | --- | --- |
| **`RA-01`** · ningún guion del navegador invoca el servicio de datos | Una biblioteca de guion agregada que consulte por su cuenta, o una llamada de red escrita en el navegador | `QG-05` (**0** peticiones, con los movimientos prendidos) y `QG-06` (**1** salida, **0** bibliotecas) | Reaparecen las tres propiedades que la topología cierra: contenido mixto, intercambio entre orígenes y **exposición de la dirección del servidor propio** |
| **`RA-02`** · el bundle es un visualizador puro | Invocar el interior del bundle en lugar de su fachada, o pasarle configuración por un camino lateral | `QG-09` (**0** invocaciones al interior; **6 de 6** funciones como única vía; **0** accesos al elemento de dibujo fuera del anfitrión) | `RI-04` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7: el bundle adquiere capacidades y `RA-02` deja de ser cierta sin que nadie lo note |
| **`RA-03`** · nada expone direcciones de servicios internos | Un mensaje de error que incluya la dirección, la ruta del almacén o una traza | `QG-08` (**0** sobre los **diecisiete** códigos vivos **y** sobre el camino de ausencia de respuesta) | `RI-05`: la violación directa de `RA-03`, en el último tramo antes de llegar a la persona |

**Las tres comparten las mismas tres propiedades**, y son las que las hacen un problema de cadena de suministro y no de funcionalidad:

| Propiedad | Por qué importa acá |
| --- | --- |
| **Compilan, se publican y se ven bien** | Ninguna herramienta de análisis de composición las detectaría: no son dependencias vulnerables, son código correcto que hace algo que el producto prohíbe |
| **Se verifican con un recuento, no con un juicio** | Los tres gates tienen umbral **0**, y `QG-09` además **6 de 6** |
| **Una medición de ausencia sin su condición no cuenta** | `QG-05` medido con los movimientos apagados daría cero sin haber ejercitado el bucle de dibujo. Es el criterio que el producto aplica en las dos puntas de la fachada |

**La conclusión operativa para el pipeline** es que las tres comprobaciones más valiosas de este proyecto de código corren **en cada pull request**, sobre el producto ejecutándose en un navegador, y **no en un stage periódico de análisis de dependencias**. Es la cadencia que [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3 materializa.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría, con la diferencia de que **acá el documento tiene sujeto real**: es una de las dos unidades desplegables. Decide emitir **inventario sobre las dos cadenas**, incorporando el del bundle en lugar de recalcularlo, con el fundamento de que un inventario de la cadena de la plataforma **no vería el motor de dibujo**. Declara que **no se firma**, con la brecha explícita y la constancia de que una firma emitida acá **no tendría verificador**, y con la mitigación de acceso que sí existe. Fija como objetivo el **primer nivel** de integridad de la construcción con su brecha, precisando que acá la elevación sí tendría sujeto y que igual no se eleva por el presupuesto declarado. Declara que **agregar una dependencia del navegador es un acto de seguridad de la topología**, que este es el único proyecto de código del producto con **análisis dinámico real** y la dependencia de ejecutor que eso implica, y la política ante vulnerabilidades **sin plazos en horas ni días**. Cierra con la sección propia: **las tres reglas de arquitectura son acá la preocupación de cadena de suministro que importa**, compilan, se ven bien, se miden con recuentos y no cuentan si se miden sin su condición. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **2**. Sube minor. |
