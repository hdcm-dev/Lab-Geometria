# Seguridad de la cadena de suministro — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **1 secciones existen sólo en `GeometriaFactory-Visor`** —«Por qué la cadena de suministro importa acá y no en los otros dos»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Nota previa de cada proyecto de código

### 1.1 `GeometriaFactory-Web`

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal**, no se atribuye ninguna al intake, y no se nombra ningún producto comercial ni ninguna versión de herramienta.
**Y una diferencia con los cinco proyectos de código que no se despliegan.** Aquéllos declararon que su inventario y su firma se emiten «en las dos unidades desplegables». **Ésta es una de las dos**, y por lo tanto acá el documento tiene sujeto real: hay un artefacto que sale del repositorio y llega a un servidor de terceros.

### 1.2 `GeometriaFactory-Visor`

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal.** No se nombra ningún producto comercial ni ninguna versión de herramienta: la convención del corpus es nombrar por función, y la elección concreta pertenece al punto de control de la etapa `a` (`PD-01` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10).

## 2. Inventario de componentes

### 2.1 `GeometriaFactory-Web`

**Decisión de esta categoría: se emite inventario, y cubre las dos cadenas.** Es una unidad desplegable con dos cadenas de herramientas, y un inventario tomado sobre una sola de ellas dejaría fuera exactamente lo que más importa.

| Cadena | Qué contiene | Por qué no alcanza con la otra |
| --- | --- | --- |
| **Plataforma** | La biblioteca de componentes de interfaz, cuya versión la fuente deja **[A VERIFICAR]** y se ancla al crear el andamiaje; y los tipos de `GeometriaFactory-Contracts` compilados adentro, que **no tienen dependencias** | Es la única cadena que un inventario convencional del ecosistema de la plataforma vería |
| **Navegador** | **El motor de dibujo tridimensional, que queda dentro del bundle** por la puerta `PT-03` | Un inventario de la cadena de la plataforma **no lo vería**: no es una dependencia declarada de este proyecto de código, es código empaquetado dentro de un archivo de recursos estáticos que esta unidad transporta |

**La segunda fila es la razón de esta decisión, y no es original de acá**: [`../../GeometriaFactory-Visor/09-Devops/Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md) ya declaró que emite inventario **por el mismo motivo**, con su alcance acotado al bundle. Esta categoría **no lo duplica**: el inventario del bundle lo produce el proyecto de código que lo empaqueta, y el de esta unidad **lo incorpora** en el paso 4 del flujo, cuando el bundle entra a los recursos estáticos.

| Aspecto del inventario | Decisión |
| --- | --- |
| Cuándo se emite | En el flujo de publicación, sobre el estado que efectivamente se sube |
| Qué cubre | Las dos cadenas, con el inventario del bundle incorporado y no recalculado |
| Dónde se adjunta | Al **informe de cierre** de la etapa, junto con el registro del flujo |
| Formato y generador | **No se nombran.** Ninguna fuente los declara y su elección es de la etapa `a`, por la regla de anclaje de versiones. Ver `PD-03` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10 |

### 2.2 `GeometriaFactory-Visor`

**Este es el único de los tres proyectos de código de nivel topológico 0 con componentes que inventariar, y el inventario es obligatorio acá por una razón que no aplica a los otros dos.**

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Existen.** El motor de dibujo tridimensional entra como dependencia declarada del manifiesto del paquete, más la cadena de herramientas de construcción | Intake §17.2.P.1 · GeometriaFactory-Visor |
| Dónde termina el motor de dibujo | **Dentro del bundle**, no traído de una red de distribución externa. Es la puerta técnica `PT-03` | Intake §17.2.P.1 · GeometriaFactory-Visor y §17.2.P.8 · GeometriaFactory-Visor |
| Artefacto publicado externamente | **Ninguno**: `redistribuible` es false y no se publica | Intake §13 y §17.2.P.7 · GeometriaFactory-Visor |
| Dónde termina el bundle | Dentro de la publicación del front, servido al navegador de cada alumno | `05` §5; intake §17.2.P.8 · GeometriaFactory-Web |

**Decisión de esta categoría: el stage de empaquetado emite el inventario de componentes del bundle**, a partir del archivo de bloqueo de dependencias, y se adjunta al informe de cierre de la etapa.

**El fundamento es que ningún otro inventario del producto lo vería.** `PT-03` exige que el motor de dibujo quede **dentro** del bundle; el bundle, a su vez, es un archivo de recursos estáticos dentro de la publicación del front. Un inventario tomado sobre las dependencias del anfitrión vería **un archivo**, no los componentes que ese archivo trae adentro. Es exactamente el punto ciego que un inventario de componentes existe para cerrar, y este proyecto de código es el único del producto que lo tiene.

**Alcance del inventario:** las dependencias que **terminan dentro del bundle**. Las de la cadena de herramientas de construcción se inventarían igual, y se distinguen de las anteriores, porque no llegan al navegador de nadie pero sí pueden comprometer la construcción.

## 3. Firma del artefacto

### 3.1 `GeometriaFactory-Web`

**No se firma, y la brecha se declara en lugar de darse por cubierta.**

| Requisito | Estado | Motivo |
| --- | --- | --- |
| Firma del artefacto publicado | **No cumplido** | El canal de entrega es una **subida por FTP a un hosting gratuito de terceros**, y **ninguna fuente declara un mecanismo por el que quien recibe pueda verificar una firma**. Una firma emitida acá no tendría verificador: el navegador que consume el front verifica el transporte, no la autoría del despliegue |
| Registro público de transparencia | **No cumplido** | Lo mismo, y además exigiría infraestructura que el intake §10 no financia |
| Integridad del origen | **Cumplido** | Etiqueta por etapa cerrada, y reversión apoyada en ella ([`Estrategia-Versionado.md`](Estrategia-Versionado.md) §4; `05` §5) |
| Integridad del artefacto respecto de su fuente | **Cumplido para el bundle**, que es la parte generada: `QG-02` exige que se genere en el mismo flujo y `QG-09` de `GeometriaFactory-Visor` prohíbe editarlo a mano | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.2 |

**Lo que la ausencia de firma deja abierto, dicho sin suavizar**: quien reciba el contenido del hosting no tiene modo de comprobar que fue este flujo el que lo puso ahí. La mitigación disponible **no es criptográfica sino de acceso**: las credenciales del canal viven como secreto del repositorio, con alcance mínimo, y no entran al árbol de fuentes (intake §17.2.P.5 · GeometriaFactory-Web).

### 3.2 `GeometriaFactory-Visor`

**No se firma, y hay que decir con precisión por qué, porque acá la respuesta es menos obvia que en los otros dos proyectos de código de nivel topológico 0.**

El bundle **sí es un archivo que se traslada** —se copia al anfitrión y se sube al hosting—, de modo que la pregunta «¿cómo sabe el que lo recibe que es el que se generó?» tiene sujeto. La respuesta que este producto ya tiene, y que no requiere firma:

| Garantía | Cómo se obtiene hoy |
| --- | --- |
| El artefacto corresponde al fuente | **No se traslada un artefacto guardado: se regenera.** El bundle no se versiona en el repositorio ([`Entornos-Deploy.md`](Entornos-Deploy.md) §2) y el flujo de trabajo del front lo **genera en su propio interior**, con un gate bloqueante que prohíbe tomarlo de un artefacto viejo (intake §17.2.P.8 · GeometriaFactory-Web) |
| El artefacto no fue alterado a mano | `QG-09` y `CV-30`, con objetivo **0** ediciones manuales |
| El artefacto es reproducible | Métrica de `ADR-12006` §8: dos construcciones desde el mismo estado producen el mismo artefacto |

**Firmar lo que se regenera en cada publicación no agrega garantía**: el receptor —el proceso del hosting— no verifica firmas de recursos estáticos, y el productor y el consumidor del archivo son el mismo flujo de trabajo. La firma tendría sujeto si el bundle se distribuyera por un canal a terceros, que es justamente lo que `ADR-12006` §4 descartó.

**Lo que sí conviene declarar como límite:** la integridad del tramo final —la subida por FTP hasta el hosting— **no la garantiza este proyecto de código** y su riesgo está declarado en el producto: el intake §17.2.P.8 · GeometriaFactory-Web registra que la subida **no es transaccional** y que se despliega fuera del horario de uso. Es una preocupación de la categoría 09 de `GeometriaFactory-Web`.

## 4. Nivel de integridad de la construcción

### 4.1 `GeometriaFactory-Web`

**Nivel objetivo: el primero, declarado con su brecha y no como alcanzado.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido.** El flujo de publicación está versionado en el repositorio (intake §16) y los pasos 1 a 5 se reproducen con los guiones del repositorio dentro del contenedor de desarrollo | Intake §16 y encabezado de la Parte C |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

No se fija un nivel más alto por el mismo motivo que en el resto del producto: los niveles superiores exigen infraestructura de construcción con garantías propias, y el intake §10 declara el producto **sin presupuesto monetario asignado**, con las tres piezas de infraestructura de costo cero.

**Una precisión que sólo corresponde a las dos unidades desplegables.** Acá la elevación de nivel **sí tendría sujeto**, porque hay un artefacto que sale del repositorio; en las cinco bibliotecas del producto no lo tenía. Que igual no se eleve es una consecuencia del presupuesto declarado y no una omisión de análisis. **La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia del artefacto del servidor propio.

### 4.2 `GeometriaFactory-Visor`

**Nivel objetivo: el primero, declarado con su brecha.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido**, y con una exigencia extra que los otros dos proyectos de código no tienen: la instalación de dependencias es **reproducible desde el archivo de bloqueo** y `ADR-12006` §8 exige que dos construcciones desde el mismo estado produzcan el mismo artefacto | Intake §17.2.P.8 · GeometriaFactory-Visor; `ADR-12006` §8 |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

**La reproducibilidad exigida acá es más fuerte que la del nivel objetivo**, y no por ambición de esta categoría: `ADR-12006` la fija como métrica de validación. Es lo que hace que la resolución de `PA-05` sea segura: si el artefacto no fuera reproducible, regenerarlo en cada publicación en lugar de guardarlo sería un riesgo y no una propiedad.

**La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia de los dos artefactos que se despliegan.

## 5. Análisis de dependencias

### 5.1 `GeometriaFactory-Web`

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

### 5.2 `GeometriaFactory-Visor`

**Acá el análisis de composición sí tiene sujeto**, y es lo que distingue a este documento de sus dos hermanos de nivel topológico 0.

| Comprobación | Umbral | Cuándo corre | Carácter |
| --- | --- | --- | --- |
| Instalación **reproducible** desde el archivo de bloqueo, sin resolución libre de versiones | Sin desvíos | Stage `instalar` | Bloqueante por construcción |
| Análisis de composición sobre las dependencias que **terminan dentro del bundle** | Ninguna vulnerabilidad crítica ni alta sin excepción **declarada por escrito y aprobada en el punto de control** | Stage `instalar`, tras el inventario de §1 | **Decisión de esta categoría**, ver el párrafo siguiente |
| **0** dependencias traídas de una red de distribución externa en tiempo de ejecución | 0 | `TC-12019`, medición de `PT-03` | **Bloqueante, y detiene la planificación de la etapa `g`** |
| **0** peticiones originadas por el bundle, **incluidas las que una dependencia haga por dentro** | 0, con los dos movimientos prendidos | `TC-12016` y `TC-12018`, sobre el **bundle generado** | **Bloqueante, sin gradación** |
| Actualización automática de dependencias | **No admitida sin decisión registrada** | — | La regla de anclaje de versiones del intake lo impide: un cambio de versión mayor **se documenta, nunca es efecto colateral de una actualización** |

**Sobre el carácter del análisis de composición.** Ninguna fuente del producto declara umbrales de severidad, de modo que esta categoría **no lo declara bloqueante por sí sola**: lo declara **obligatorio de ejecutar y de registrar**, y su resultado entra al punto de control de la etapa. Un hallazgo crítico o alto no puede quedar en silencio; qué se hace con él lo decide el Product Owner en el punto de control, que el intake §15 declara bloqueante. **Es el mismo tratamiento que la Fase E dio a lo que se mide y se registra sin bloquear automáticamente**, y se adopta acá por la misma razón: **el umbral no lo da ninguna fuente**.

**La cuarta fila es la más importante de la tabla, y no es un gate de dependencias al uso.** `RQ-01` de [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §4 declara que la petición de red puede aparecer por dos causas —comodidad de quien escribe, o **una dependencia que la haga por dentro**— y que la segunda tiene probabilidad **media**, más alta que la primera. Por eso la inspección corre **sobre el bundle generado y no sólo sobre la fuente**: una petición hecha desde adentro de una dependencia no aparece en el código propio.

## 6. Análisis estático y dinámico

### 6.1 `GeometriaFactory-Web`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático | **Existe y bloquea**: el gate de construcción es «sin advertencias» | Intake §17.2.P.8 · GeometriaFactory-Web; `QG-01` |
| Estático de estructura | **Existe, bloquea, y es la verificación característica de este proyecto de código**: `QG-06` sobre el árbol de fuentes y las dependencias de guion, `QG-08` sobre el traductor de condiciones y `QG-09` sobre las invocaciones al bundle | [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3 |
| **Dinámico** | **Existe, y acá sí tiene sujeto**, a diferencia de las cinco bibliotecas del producto: `QG-05`, `QG-07` y `QG-10` se miden **sobre el producto corriendo en un navegador**, contando peticiones, leyendo el almacenamiento y observando el tráfico del circuito | `Estrategia-Calidad.md` §3; `Plan-Pruebas.md` §3 |
| Condición de las mediciones de ausencia | **`QG-05` se mide con los dos movimientos automáticos prendidos.** Un conteo con los movimientos apagados daría cero sin haber ejercitado el bucle de dibujo | `Estrategia-Calidad.md` §3, `QG-05` |
| Detección de secretos en las confirmaciones | **Recomendada, y acá con sujeto propio**: este proyecto de código es el que administra los **dos** secretos del producto que viven en el repositorio | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**La tercera fila es lo que hace de este proyecto de código el único con análisis dinámico real del producto.** Y trae una dependencia de ejecutor que conviene no perder: sin **navegador con capacidad gráfica tridimensional y un conductor capaz de contar peticiones y leer el almacenamiento**, `QG-05`, `QG-07` y `QG-10` no se pueden medir en la canalización y quedan como medición manual registrada. Está declarado como `PD-04` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, y es el mismo requisito que `GeometriaFactory-Visor` registró del otro lado de la fachada.

### 6.2 `GeometriaFactory-Visor`

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático del fuente | **Existe**: la verificación de tipos del lenguaje fuente ocurre en el empaquetado, y su falla es falla de `QG-01` | Intake §17.2.P.1 · GeometriaFactory-Visor y §17.2.P.8 · GeometriaFactory-Visor |
| Estático **del artefacto generado** | **Existe, bloquea y es la verificación característica de este proyecto de código**: recuentos sobre el bundle —funciones expuestas, identificadores globales, ocurrencias de las tres formas de petición, claves escritas— | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §1 y §2; `QG-04`, `QG-05` y `QG-06` |
| **Dinámico** | **Existe, y es el único de los tres proyectos de código de nivel topológico 0 que lo tiene**: la medición sobre una página real, con un conductor que cuenta peticiones de red y lee el almacenamiento del navegador, **con los dos movimientos prendidos** | [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2 y §6 |
| Detección de secretos en las confirmaciones | Recomendada a nivel producto; este proyecto de código no maneja ninguno | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**El análisis dinámico de este proyecto de código verifica ausencias, no vulnerabilidades**, y es una forma poco habitual de la técnica que conviene nombrar: no busca qué hace de más el bundle sobre una superficie expuesta —no tiene ninguna—, sino que **cuenta que no haga nada de lo que tiene prohibido**. Umbral cero, y **con la condición de medición declarada**, porque una medición de ausencia sin su condición no cuenta como medición ([`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3).

## 7. Política ante vulnerabilidades publicadas

### 7.1 `GeometriaFactory-Web`

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad sobre la biblioteca de componentes de interfaz | Se ancla la versión corregida y se registra en el punto de control de la etapa. **Si la versión corregida exige una plataforma que el hosting no soporta**, el conflicto es con `PT-01.a` y la salida declarada es bajar la versión objetivo del front, no la del backend | El equipo, y el Product Owner si hay que bajar la versión objetivo |
| Vulnerabilidad sobre el motor de dibujo tridimensional | **No se remedia acá**: viaja dentro del bundle y su anclaje pertenece a `GeometriaFactory-Visor`. Lo que esta unidad tiene que hacer es **volver a publicar** para que el bundle corregido llegue al hosting | Categoría 09 de `GeometriaFactory-Visor`, y esta categoría publica |
| Vulnerabilidad sobre la plataforma de ejecución del hosting | **No está bajo control del producto**: el hosting es un servicio gratuito de terceros. Lo que sí está bajo control es la versión objetivo del front | El Product Owner, con constancia |
| Exposición de las credenciales del canal de publicación | **Rotación inmediata del secreto y republicación.** El valor no está en el repositorio, de modo que la rotación no exige tocar código | Intake §17.2.P.5 · GeometriaFactory-Web |
| Exposición de la dirección del servidor propio | Rotación del secreto, republicación y **revisión de por dónde se filtró**: `QG-08` mide **0** mensajes que la expongan, sobre los **diecisiete** códigos vivos y el camino de ausencia de respuesta | Es `RA-03`; `RI-05` de [`../../../Producto/Vista-Producto.md`](../../../Producto/Vista-Producto.md) §7 |

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días.** El intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el **punto de control bloqueante** de la etapa en curso.

**Comunicación a integradores: no aplica.** No hay integradores externos, y el intake §10 declara que **ninguna normativa de compliance aplica**: es un laboratorio de aula con cuentas creadas para la materia.

**Y un riesgo aceptado por escrito que esta categoría no reabre.** El intake §17.1.P.5 · GeometriaFactory-Api declara que el tramo entre el front y el servicio de datos **viaja en claro si ese salto es HTTP plano**, con el túnel saliente como salida **documentada y no adoptada**. Es decisión del Product Owner registrada aguas arriba; esta categoría la transcribe y **no la suaviza ni la agrava**.

### 7.2 `GeometriaFactory-Visor`

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad publicada sobre el **motor de dibujo tridimensional** | Se evalúa la actualización de la versión anclada. **No se aplica como efecto colateral**: la regla de anclaje del intake exige documentar el cambio, y el propio intake §17.2.P.1 · GeometriaFactory-Visor declara que si la versión adoptada es posterior a la del visualizador previo **se documenta el cambio de interfaz que exija** | El Product Owner, en el punto de control, con la constancia del cambio de interfaz si lo hubo |
| Vulnerabilidad publicada sobre una dependencia **de construcción** | No llega al navegador de nadie, pero puede comprometer la construcción. Se registra y se trata en el punto de control | El mismo |
| Vulnerabilidad que exigiera **traer una dependencia por red de distribución externa** para mitigarla | **No se admite**: violaría `PT-03`, que es puerta técnica bloqueante del producto | Nadie: la puerta no es negociable por esta categoría |
| Cualquier mitigación que introduzca una petición de red en el bundle | **No se admite**: violaría `RA-02` y, a través de ella, `RA-01`, que son reglas de nivel producto | Nadie |

**Las dos últimas filas son las que hay que leer con atención.** Son el caso en que la política de cadena de suministro podría entrar en conflicto con una regla de arquitectura, y la respuesta está fijada aguas arriba y no se decide acá: **`RA-01` y `RA-02` no se relajan por una vulnerabilidad**, y `PT-02` y `PT-03` no admiten carácter condicionado ([`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1). Si una mitigación exigiera romperlas, lo que corresponde es elevar la decisión al Product Owner como cambio de alcance, no aplicarla.

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el punto de control bloqueante.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

## 8. Las tres reglas de arquitectura como preocupación de cadena de suministro

### 8.1 `GeometriaFactory-Web`

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

## 9. Por qué la cadena de suministro importa acá y no en los otros dos

### 9.1 `GeometriaFactory-Visor`

La sección existe para que la canalización de nivel producto no trate a los tres proyectos de código de nivel topológico 0 como si fueran el mismo caso:

| Preocupación | `GeometriaFactory-Domain` | `GeometriaFactory-Contracts` | `GeometriaFactory-Visor` |
| --- | --- | --- | --- |
| Dependencias externas | **0** | **0** | **Existen**, y una termina dentro del artefacto |
| Inventario de componentes propio | No se emite | No se emite | **Se emite**, porque ningún otro inventario del producto vería lo que hay adentro del bundle |
| Análisis de composición | Sin sujeto | Sin sujeto | **Con sujeto** |
| Análisis dinámico | Sin sujeto | Sin sujeto | **Con sujeto**: la medición de ausencias sobre una página real |
| El artefacto llega a un navegador de un tercero | No | No | **Sí**, servido desde el front al navegador de cada alumno |

**La última fila es la que justifica todo lo demás.** Los otros dos proyectos de código viven dentro de procesos que corren en máquinas del producto; este proyecto de código **entrega código que se ejecuta en la máquina del alumno**. Es la única superficie del producto donde una dependencia comprometida corre fuera del alcance de quien lo construyó, y por eso su cadena de suministro se verifica **sobre el artefacto generado** y no sobre lo que el manifiesto declara.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
