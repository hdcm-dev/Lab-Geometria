# Cadena de suministro y seguridad de la construcción — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Supply-Chain-Seguridad.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §9; [`../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md); [`../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md`](../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md); [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3 y §5; [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) 1.0 §4; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §14, §15, §17.7.P.1, §17.7.P.4, §17.7.P.5, §17.7.P.8 y §17.7.P.11
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Entornos-Deploy.md`](Entornos-Deploy.md), [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Inventario de componentes](#1-inventario-de-componentes)
- [2. Firma del artefacto](#2-firma-del-artefacto)
- [3. Nivel de integridad de la construcción](#3-nivel-de-integridad-de-la-construcción)
- [4. Análisis de dependencias](#4-análisis-de-dependencias)
- [5. Análisis estático y dinámico](#5-análisis-estático-y-dinámico)
- [6. Política ante vulnerabilidades publicadas](#6-política-ante-vulnerabilidades-publicadas)
- [7. Por qué la cadena de suministro importa acá y no en los otros dos](#7-por-qué-la-cadena-de-suministro-importa-acá-y-no-en-los-otros-dos)
- [8. Control de cambios](#8-control-de-cambios)

---

**Nota previa sobre el origen de este documento.** Ninguna fuente del producto declara política de cadena de suministro; `Rules-Devops.md` §2.1 la exige para los ocho tipos D8. **Todo lo que este documento decide es decisión de esta categoría y va declarado como tal.** No se nombra ningún producto comercial ni ninguna versión de herramienta: la convención del corpus es nombrar por función, y la elección concreta pertenece al punto de control de la etapa `a` (`PD-01` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10).

## 1. Inventario de componentes

**Este es el único de los tres proyectos de código de nivel topológico 0 con componentes que inventariar, y el inventario es obligatorio acá por una razón que no aplica a los otros dos.**

| Hecho | Valor | Dónde está declarado |
| --- | --- | --- |
| Dependencias externas | **Existen.** El motor de dibujo tridimensional entra como dependencia declarada del manifiesto del paquete, más la cadena de herramientas de construcción | Intake §17.7.P.1 |
| Dónde termina el motor de dibujo | **Dentro del bundle**, no traído de una red de distribución externa. Es la puerta técnica `PT-03` | Intake §17.7.P.1 y §17.7.P.8 |
| Artefacto publicado externamente | **Ninguno**: `redistribuible` es false y no se publica | Intake §13 y §17.7.P.7 |
| Dónde termina el bundle | Dentro de la publicación del front, servido al navegador de cada alumno | `05` §5; intake §17.6.P.8 |

**Decisión de esta categoría: el stage de empaquetado emite el inventario de componentes del bundle**, a partir del archivo de bloqueo de dependencias, y se adjunta al informe de cierre de la etapa.

**El fundamento es que ningún otro inventario del producto lo vería.** `PT-03` exige que el motor de dibujo quede **dentro** del bundle; el bundle, a su vez, es un archivo de recursos estáticos dentro de la publicación del front. Un inventario tomado sobre las dependencias del anfitrión vería **un archivo**, no los componentes que ese archivo trae adentro. Es exactamente el punto ciego que un inventario de componentes existe para cerrar, y este proyecto de código es el único del producto que lo tiene.

**Alcance del inventario:** las dependencias que **terminan dentro del bundle**. Las de la cadena de herramientas de construcción se inventarían igual, y se distinguen de las anteriores, porque no llegan al navegador de nadie pero sí pueden comprometer la construcción.

## 2. Firma del artefacto

**No se firma, y hay que decir con precisión por qué, porque acá la respuesta es menos obvia que en los otros dos proyectos de código de nivel topológico 0.**

El bundle **sí es un archivo que se traslada** —se copia al anfitrión y se sube al hosting—, de modo que la pregunta «¿cómo sabe el que lo recibe que es el que se generó?» tiene sujeto. La respuesta que este producto ya tiene, y que no requiere firma:

| Garantía | Cómo se obtiene hoy |
| --- | --- |
| El artefacto corresponde al fuente | **No se traslada un artefacto guardado: se regenera.** El bundle no se versiona en el repositorio ([`Entornos-Deploy.md`](Entornos-Deploy.md) §2) y el flujo de trabajo del front lo **genera en su propio interior**, con un gate bloqueante que prohíbe tomarlo de un artefacto viejo (intake §17.6.P.8) |
| El artefacto no fue alterado a mano | `QG-09` y `CV-30`, con objetivo **0** ediciones manuales |
| El artefacto es reproducible | Métrica de `ADR-12006` §8: dos construcciones desde el mismo estado producen el mismo artefacto |

**Firmar lo que se regenera en cada publicación no agrega garantía**: el receptor —el proceso del hosting— no verifica firmas de recursos estáticos, y el productor y el consumidor del archivo son el mismo flujo de trabajo. La firma tendría sujeto si el bundle se distribuyera por un canal a terceros, que es justamente lo que `ADR-12006` §4 descartó.

**Lo que sí conviene declarar como límite:** la integridad del tramo final —la subida por FTP hasta el hosting— **no la garantiza este proyecto de código** y su riesgo está declarado en el producto: el intake §17.6.P.8 registra que la subida **no es transaccional** y que se despliega fuera del horario de uso. Es una preocupación de la categoría 09 de `GeometriaFactory-Web`.

## 3. Nivel de integridad de la construcción

**Nivel objetivo: el primero, declarado con su brecha.**

| Requisito del nivel objetivo | Estado hoy | Fundamento |
| --- | --- | --- |
| Construcción **automatizada y reproducible por guion** | **Cumplido**, y con una exigencia extra que los otros dos proyectos de código no tienen: la instalación de dependencias es **reproducible desde el archivo de bloqueo** y `ADR-12006` §8 exige que dos construcciones desde el mismo estado produzcan el mismo artefacto | Intake §17.7.P.8; `ADR-12006` §8 |
| **Procedencia** emitida del artefacto | **No cumplido.** Hoy no se emite ninguna | Decisión de esta categoría: se declara la brecha |

**La reproducibilidad exigida acá es más fuerte que la del nivel objetivo**, y no por ambición de esta categoría: `ADR-12006` la fija como métrica de validación. Es lo que hace que la resolución de `PA-05` sea segura: si el artefacto no fuera reproducible, regenerarlo en cada publicación en lugar de guardarlo sería un riesgo y no una propiedad.

**La elevación es de nivel producto** y sólo tiene sentido junto con la procedencia de los dos artefactos que se despliegan.

## 4. Análisis de dependencias

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

## 5. Análisis estático y dinámico

| Análisis | Estado | Fundamento |
| --- | --- | --- |
| Estático del fuente | **Existe**: la verificación de tipos del lenguaje fuente ocurre en el empaquetado, y su falla es falla de `QG-01` | Intake §17.7.P.1 y §17.7.P.8 |
| Estático **del artefacto generado** | **Existe, bloquea y es la verificación característica de este proyecto de código**: recuentos sobre el bundle —funciones expuestas, identificadores globales, ocurrencias de las tres formas de petición, claves escritas— | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §1 y §2; `QG-04`, `QG-05` y `QG-06` |
| **Dinámico** | **Existe, y es el único de los tres proyectos de código de nivel topológico 0 que lo tiene**: la medición sobre una página real, con un conductor que cuenta peticiones de red y lee el almacenamiento del navegador, **con los dos movimientos prendidos** | [`../08-Calidad-Y-Pruebas/Plan-Pruebas.md`](../08-Calidad-Y-Pruebas/Plan-Pruebas.md) §2 y §6 |
| Detección de secretos en las confirmaciones | Recomendada a nivel producto; este proyecto de código no maneja ninguno | [`Entornos-Deploy.md`](Entornos-Deploy.md) §5 |

**El análisis dinámico de este proyecto de código verifica ausencias, no vulnerabilidades**, y es una forma poco habitual de la técnica que conviene nombrar: no busca qué hace de más el bundle sobre una superficie expuesta —no tiene ninguna—, sino que **cuenta que no haga nada de lo que tiene prohibido**. Umbral cero, y **con la condición de medición declarada**, porque una medición de ausencia sin su condición no cuenta como medición ([`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../08-Calidad-Y-Pruebas/Criterios-Validacion.md) §3).

## 6. Política ante vulnerabilidades publicadas

| Situación | Salida | Quién decide |
| --- | --- | --- |
| Vulnerabilidad publicada sobre el **motor de dibujo tridimensional** | Se evalúa la actualización de la versión anclada. **No se aplica como efecto colateral**: la regla de anclaje del intake exige documentar el cambio, y el propio intake §17.7.P.1 declara que si la versión adoptada es posterior a la del visualizador previo **se documenta el cambio de interfaz que exija** | El Product Owner, en el punto de control, con la constancia del cambio de interfaz si lo hubo |
| Vulnerabilidad publicada sobre una dependencia **de construcción** | No llega al navegador de nadie, pero puede comprometer la construcción. Se registra y se trata en el punto de control | El mismo |
| Vulnerabilidad que exigiera **traer una dependencia por red de distribución externa** para mitigarla | **No se admite**: violaría `PT-03`, que es puerta técnica bloqueante del producto | Nadie: la puerta no es negociable por esta categoría |
| Cualquier mitigación que introduzca una petición de red en el bundle | **No se admite**: violaría `RA-02` y, a través de ella, `RA-01`, que son reglas de nivel producto | Nadie |

**Las dos últimas filas son las que hay que leer con atención.** Son el caso en que la política de cadena de suministro podría entrar en conflicto con una regla de arquitectura, y la respuesta está fijada aguas arriba y no se decide acá: **`RA-01` y `RA-02` no se relajan por una vulnerabilidad**, y `PT-02` y `PT-03` no admiten carácter condicionado ([`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §3.1). Si una mitigación exigiera romperlas, lo que corresponde es elevar la decisión al Product Owner como cambio de alcance, no aplicarla.

**No se declara ningún acuerdo de nivel de servicio de remediación en horas o días**: el intake §10 declara «sin plazo; el avance se mide por etapas cerradas». El mecanismo que reemplaza al plazo es el punto de control bloqueante.

**Comunicación a integradores: no aplica.** No hay integradores externos y el intake §10 declara que **ninguna normativa de compliance aplica**.

## 7. Por qué la cadena de suministro importa acá y no en los otros dos

La sección existe para que la canalización de nivel producto no trate a los tres proyectos de código de nivel topológico 0 como si fueran el mismo caso:

| Preocupación | `GeometriaFactory-Domain` | `GeometriaFactory-Contracts` | `GeometriaFactory-Visor` |
| --- | --- | --- | --- |
| Dependencias externas | **0** | **0** | **Existen**, y una termina dentro del artefacto |
| Inventario de componentes propio | No se emite | No se emite | **Se emite**, porque ningún otro inventario del producto vería lo que hay adentro del bundle |
| Análisis de composición | Sin sujeto | Sin sujeto | **Con sujeto** |
| Análisis dinámico | Sin sujeto | Sin sujeto | **Con sujeto**: la medición de ausencias sobre una página real |
| El artefacto llega a un navegador de un tercero | No | No | **Sí**, servido desde el front al navegador de cada alumno |

**La última fila es la que justifica todo lo demás.** Los otros dos proyectos de código viven dentro de procesos que corren en máquinas del producto; este proyecto de código **entrega código que se ejecuta en la máquina del alumno**. Es la única superficie del producto donde una dependencia comprometida corre fuera del alcance de quien lo construyó, y por eso su cadena de suministro se verifica **sobre el artefacto generado** y no sobre lo que el manifiesto declara.

## 8. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que ninguna fuente del producto declara política de cadena de suministro y que todo lo de este documento es decisión de esta categoría. **Decide emitir el inventario de componentes del bundle** en el stage de empaquetado, con el fundamento de que `PT-03` mete el motor de dibujo **dentro** del artefacto y ningún inventario tomado sobre el anfitrión lo vería. Declara que no se firma, con la respuesta precisa de por qué acá la pregunta sí tiene sujeto: el artefacto **se regenera y no se traslada guardado**. Fija como objetivo el **primer nivel** de integridad con su brecha, señalando que la reproducibilidad exigida por `ADR-12006` es más fuerte que el nivel y que es lo que vuelve segura la resolución de `PA-05`. Declara el análisis de composición **obligatorio de ejecutar y registrar, sin umbral bloqueante inventado**, porque ninguna fuente da umbrales de severidad, y declara que **ninguna mitigación puede romper `RA-01`, `RA-02` ni `PT-03`**. Cierra con la comparación de los tres proyectos de código de nivel topológico 0 y con la razón de fondo: este es el único cuyo artefacto se ejecuta en la máquina del alumno. |
