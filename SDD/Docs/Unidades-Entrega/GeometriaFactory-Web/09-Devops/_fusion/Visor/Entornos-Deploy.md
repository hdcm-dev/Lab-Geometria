# Entornos y canales — GeometriaFactory-Visor

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Entornos-Deploy.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Visor/Arquitectura-Proyecto-Codigo.md) 1.0 §5 y §11 (punto abierto `PA-05`); [`../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) 1.0; [`../08-Calidad-Y-Pruebas/Criterios-Validacion.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Criterios-Validacion.md) 1.0 §6; [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Estrategia-Testing.md) 1.1 §7; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.20** §10, §13, §16, §16.1, §17.2.P.7 · GeometriaFactory-Web, §17.2.P.8 · GeometriaFactory-Web, §17.2.P.4 · GeometriaFactory-Visor, §17.2.P.5 · GeometriaFactory-Visor, §17.2.P.7 · GeometriaFactory-Visor, §17.2.P.8 · GeometriaFactory-Visor, §17.2.P.9 · GeometriaFactory-Visor y §18
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes y canales de este proyecto de código](#1-ambientes-y-canales-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. Resolución de `PA-05`: el bundle se ignora en el repositorio](#2-resolución-de-pa-05-el-bundle-se-ignora-en-el-repositorio)
  - [2.1 Alternativa considerada y descartada](#21-alternativa-considerada-y-descartada)
  - [2.2 Qué exige esta decisión para quedar cumplida](#22-qué-exige-esta-decisión-para-quedar-cumplida)
- [3. Provisión](#3-provisión)
- [4. Configuración](#4-configuración)
- [5. Secretos](#5-secretos)
- [6. Promoción](#6-promoción)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Ambientes y canales de este proyecto de código

**Ninguno propio de despliegue y ninguno de publicación.** Las afirmaciones que lo sostienen:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: su artefacto es un archivo de guion generado que **se copia al directorio de recursos estáticos de `GeometriaFactory-Web` y viaja dentro del despliegue de esa unidad** | `05` §5, primera fila |
| **No se publica** en ningún repositorio de paquetes: `redistribuible` es false | Intake §17.2.P.7 · GeometriaFactory-Visor; `05` §5 |
| En tiempo de ejecución **no hay entorno de la cadena de herramientas**: hay un archivo servido como recurso estático | `05` §5, tercera fila; intake §17.2.P.9 · GeometriaFactory-Visor |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor de la canalización | Nadie: no hay promoción hacia él | No aplica |
| Navegador con capacidad gráfica tridimensional | Donde el bundle **se ejecuta**, servido desde el front. No es un ambiente que este proyecto de código provisione ni opere | — | Sin ella el visor **no es soportado** (intake §17.2.P.9 · GeometriaFactory-Visor) |

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único. **Acá no hay feed**, y el ADR que lo justifica es anterior a esta categoría: [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §4 evaluó publicar el bundle en un repositorio público de paquetes y lo descartó porque el intake lo descarta explícitamente, `redistribuible` es false y no hay integradores externos: **sería un portal para una comunidad que no existe**.

**El caso de este proyecto de código tiene además un rasgo que los otros dos de nivel topológico 0 no tienen**: su artefacto sí es un archivo que se entrega —se copia al anfitrión— y por eso la categoría emite [`Guia-Publicacion-Bundle-Visor.md`](../../Guia-Publicacion-Bundle-Visor.md), que documenta esa entrega interna con la estructura que `Rules-Devops.md` §4.5 exige. **Entrega no es publicación**, y el documento lo declara en su primera sección para que nadie lea un canal donde no lo hay.

## 2. Resolución de `PA-05`: el bundle se ignora en el repositorio

`05` §11 registra el punto abierto `PA-05` —si el bundle generado **se versiona en el repositorio o se ignora**— y declara que **lo cierra la categoría 09, al emitirse**. [`ADR-12006`](../../../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §6, punto 4, acepta explícitamente que la decisión quede abierta y que la elección pertenezca a 09. **Se cierra acá.**

**Decisión: el bundle generado no se versiona en el repositorio. Se ignora, y lo genera la canalización antes de publicar.**

El intake §17.2.P.7 · GeometriaFactory-Visor admite las dos formas y le pone condición a cada una: si se versiona, se versiona como salida reproducible; **si se ignora, `scripts/build.sh` lo genera antes de publicar**. Los cuatro fundamentos de la elección, todos verificables abriendo la fuente que se cita:

| Fundamento | Dónde se verifica |
| --- | --- |
| **La condición que el intake pone para ignorar ya está cumplida por el diseño de la canalización del front.** Sus pasos son: obtener el código, preparar las dos cadenas de herramientas, **instalación reproducible de dependencias en `visor/`, el empaquetador genera el bundle y lo copia a los recursos estáticos**, publicación del front, inyección de configuración desde secretos, subida y verificación de que la dirección pública responde | Intake §17.2.P.8 · GeometriaFactory-Web, pasos del flujo de trabajo |
| **Hay un gate bloqueante del producto que prohíbe usar un bundle versionado.** El mismo §17.2.P.8 · GeometriaFactory-Web declara entre los quality gates bloqueantes del front que el **bundle se genera en el mismo flujo de trabajo, nunca tomado de un artefacto viejo**. Un bundle versionado en el repositorio sería, por definición, un artefacto anterior a esa ejecución: quedaría en el repositorio sin que nadie lo consuma | Intake §17.2.P.8 · GeometriaFactory-Web, quality gates |
| **Versionarlo abre la puerta al único defecto que este proyecto de código tiene prohibido sin excepción.** El intake §13 y §17.2.P.7 · GeometriaFactory-Visor declaran que el bundle **nunca se edita a mano**; `QG-09` lo rechaza en revisión y `CV-30` lo declara bloqueante. Un archivo generado, versionado y visible en cada revisión es exactamente el que alguien corrige «en el acto» cuando tiene apuro | Intake §13 y §17.2.P.7 · GeometriaFactory-Visor; `08` `Estrategia-Calidad.md` §3 y `Criterios-Validacion.md` §6 |
| **No perjudica al sample ni al trabajo local**, que es la única objeción seria a ignorarlo. El intake §18 declara la reproducción de **S-1** en cinco pasos o menos y su primer paso **ya es construir el bundle** con el guion propio; el mismo guion existe para el ciclo corto de trabajo sobre el visor | Intake §18 y §17.2.P.8 · GeometriaFactory-Visor |

**La decisión no toca ninguna decisión de arquitectura.** `ADR-12006` §2 mantiene todo lo suyo: versionado semántico sin publicación, artefacto tratado como salida reproducible y nunca editado a mano. Lo que 09 elige es **dónde no vive el archivo**, que es exactamente lo que la ADR le dejó.

### 2.1 Alternativa considerada y descartada

| Alternativa | A favor | En contra |
| --- | --- | --- |
| **Ignorarlo y generarlo** (adoptada) | La canalización del front ya lo genera y tiene un gate que prohíbe tomarlo de un artefacto viejo; el archivo generado no aparece en ninguna revisión, de modo que nadie lo edita; no hay diferencia posible entre el artefacto y el fuente | Exige que el guion de construcción esté disponible antes de abrir el sample o el front en local. El intake ya lo declara como primer paso de S-1 |
| Versionarlo como salida reproducible | Permitiría abrir el sample sin construir nada, y dejaría en el historial la evidencia de cada cambio del artefacto | El artefacto versionado **no lo usaría nadie**: el gate del front obliga a regenerarlo. Y cada cambio del fuente produciría una diferencia enorme e ilegible en la revisión, que es donde `QG-09` tiene que poder ver si alguien lo editó a mano. **La evidencia que aportaría es la que vuelve inservible a la revisión que la buscaría** |

### 2.2 Qué exige esta decisión para quedar cumplida

Consecuencias operativas concretas, y se declaran porque una decisión de esta clase sin sus consecuencias es una intención:

| Exigencia | Estado hoy | Quién la aplica |
| --- | --- | --- |
| El control de versiones **excluye el directorio de salida del empaquetado** en `visor/` y **el bundle copiado** bajo los recursos estáticos del anfitrión | **No cumplido todavía**: el archivo `.gitignore` del repositorio, leído el 2026-08-11, no excluye ninguna de las dos rutas —sí excluye el directorio de dependencias del ecosistema del navegador— | El equipo, en la etapa `a`, al poner en pie la cadena de construcción (`BT-12001` de `08` `Plan-Pruebas.md` §2) |
| El guion propio del bundle existe y produce **un archivo vacío pero real** desde la etapa `a` | Declarado como criterio de entrada de la categoría 08 | El mismo, con `BT-12001` y `BT-12002` |
| Toda ejecución local que necesite el bundle **lo construye antes**: el sample S-1 y el front en local | Declarado por el intake §18 para S-1 | Quien ejecuta |
| La canalización del front **no se modifica por esta decisión** | Ya genera el bundle en su interior | Categoría 09 de `GeometriaFactory-Web` |

**La primera fila es una acción pendiente y no un incumplimiento de este documento.** Se declara con la fecha de la lectura para que la próxima categoría que la verifique sepa contra qué estado se escribió.

## 3. Provisión

**No hay infraestructura declarativa atribuible a este proyecto de código**: no provisiona servidor, red ni almacenamiento. Lo que necesita del entorno es lo que declara §4 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md): el contenedor de desarrollo para construir, y **un navegador con capacidad gráfica tridimensional más un conductor** para medir. Ese segundo requisito **no es un ambiente que se provisione con infraestructura declarativa**: es una capacidad del ejecutor, y su ausencia deja a este proyecto de código sin sus gates principales (`PD-02` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10).

La infraestructura del producto —imagen del backend y flujo de trabajo de publicación del front— pertenece a los dos proyectos de código que se despliegan y **no se describe acá**.

## 4. Configuración

**Ninguna, y es prohibición explícita y no ausencia.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración propia en ejecución | **Ninguna.** El bundle **no lee configuración propia** | Intake §17.2.P.3 · GeometriaFactory-Visor; `RA-02` del intake §14 |
| Persistencia | **Ninguna, y es prohibición explícita**: no guarda estado entre páginas ni escribe en el almacenamiento del navegador. `QG-05` lo verifica con umbral **0** | Intake §17.2.P.4 · GeometriaFactory-Visor; `08` `Estrategia-Calidad.md` §3 |
| Identidad y papel del usuario | **Ninguno, y también es prohibición explícita**: el bundle no sabe quién es el usuario ni qué papel tiene, y no participa de ninguna decisión de autorización | Intake §17.2.P.5 · GeometriaFactory-Visor |
| Preferencia de movimiento reducido del entorno | **La lee el anfitrión, no el bundle.** El anfitrión le pasa dos valores de verdad por la fachada | Intake, control de cambios 1.7, decisión (c); `08` `Estrategia-Testing.md` §5 |

**Estas cuatro filas son la razón por la que este proyecto de código no puede tener ambientes.** Un ambiente se distingue de otro por su configuración, y acá no hay ninguna: el mismo bundle, byte por byte, sirve para el sample sin backend, para el front en local y para el front publicado. **Es el mismo artefacto en los tres lugares**, y eso es lo que `RA-02` compra.

## 5. Secretos

**Ninguno, en ninguno de los dos momentos**, y la afirmación es más fuerte que en los otros dos proyectos de código de nivel topológico 0:

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** La instalación de dependencias se hace desde el registro público del ecosistema, sin credencial, y no hay publicación que autenticar | Intake §17.2.P.7 · GeometriaFactory-Visor: no se publica |
| Ejecución | **Ninguno, y es imposible que los haya**: el bundle no hace red —`QG-04`, umbral **0**— y no lee configuración. No tiene por dónde recibir un secreto ni a dónde mandarlo | Intake §17.2.P.3 · GeometriaFactory-Visor y §17.2.P.5 · GeometriaFactory-Visor |

**La contribución de este proyecto de código a la seguridad del producto es una ausencia**, y así lo declara [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/_fusion/Visor/Estrategia-Calidad.md) §1: no hacer red es lo que hace **imposible** violar `RA-01` desde el navegador. Desde esta categoría, la consecuencia práctica es que **un stage de este proyecto de código que pidiera una credencial sería la señal de que algo se salió de su alcance**.

**No se declara ninguna frecuencia de rotación**: no hay secreto propio. Los del producto pertenecen a la categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api`.

## 6. Promoción

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| **Momento de medición → compromiso de la etapa `g`** | `PT-02` y `PT-03` pasadas enteras | El mismo | El registro de la medición, **con la condición de cada ausencia** |
| Bundle construido → bundle en los recursos estáticos del anfitrión | El stage de copia | Automático dentro de la construcción | La constancia de que el bundle corresponde al fuente que lo generó |
| Bundle en el anfitrión → bundle servido en el hosting | La publicación del front, que un cambio bajo `visor/` dispara | Categoría 09 de `GeometriaFactory-Web` | La verificación de que la dirección pública responde, que el intake §17.2.P.8 · GeometriaFactory-Web declara como cierre obligatorio de ese flujo |

**La última fila no la ejecuta este proyecto de código**, y se declara igual porque es donde su artefacto termina. El intake §17.2.P.8 · GeometriaFactory-Web declara que ese flujo **no termina en la subida, termina comprobando que la dirección pública responde**, con el fundamento de que una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que este proyecto de código **no tiene ambientes ni canales propios** y registra el apartamiento del modelo `preview` / `stable` apoyado en `ADR-12006` §4. **Cierra el punto abierto `PA-05`**: el bundle **se ignora en el repositorio y lo genera la canalización**, con cuatro fundamentos verificables —la canalización del front ya lo genera; el gate bloqueante del front prohíbe tomarlo de un artefacto viejo; versionarlo abre la puerta a la edición manual que el producto prohíbe sin excepción; y no perjudica al sample, cuyo primer paso ya es construirlo—, con la alternativa evaluada y **cuatro exigencias operativas**, una de ellas declarada como acción pendiente con la fecha de lectura del estado del repositorio. Declara la ausencia de configuración, de persistencia, de identidad y de secretos como **prohibiciones explícitas** y no como carencias, y por qué eso hace que el mismo artefacto sirva en los tres lugares donde se usa. |
