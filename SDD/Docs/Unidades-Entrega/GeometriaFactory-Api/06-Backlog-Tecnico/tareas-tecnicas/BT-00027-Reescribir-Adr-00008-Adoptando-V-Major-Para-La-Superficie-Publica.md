# BT-00027 — Reescribir `ADR-00008` adoptando `/v{MAJOR}/` para la superficie pública

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** BT-00027-Reescribir-Adr-00008-Adoptando-V-Major-Para-La-Superficie-Publica.md
**Versión:** 2.0
**Estado:** Done
**Fecha:** 2026-09-12
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Épica:** EP-T06 · Exposición pública y versionado del contrato
**Etapa del producto:** `k`
**Tipo:** docs
**Prioridad:** Alta
**Estimación:** **No aplica** — el producto no estima; ver §6

## 1. Descripción

Reescribir `ADR-00008` adoptando `/v{MAJOR}/` para la superficie pública.

## 2. Justificación

`Mesa-2026-09-12-ciclo-2.md` §4 ítem 1; `PRODUCT-INTAKE` **4.3** §17.1.P.3, fila «Versionado del contrato»; [`ADR-00008`](../../05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) vigente. La premisa que `ADR-00008` reescribe —«no hay clientes de terceros»— no vive sólo en esa ADR: también la sostiene [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md), que es **nivel Producto**. Por el criterio 6 de la DoR (`Definition-Of-Ready.md` §2.1), esta ficha declara que la decisión **alcanza fuera de esta unidad de entrega**: `ADR-08003` obliga también a `GeometriaFactory-Contracts`, que consolida la política de versionado por compilación compartida para las dos piezas desplegables.

## 3. Criterios de aceptación

- El ADR declara el cambio de premisa respecto de `ADR-00008` vigente («sin versionado de rutas y despliegue conjunto») y adopta `/v{MAJOR}/` para la superficie pública, con el `MAJOR` compartido con el SemVer del producto (`D-03`)
- el documento anterior pasa a `Superado` o queda reemplazado por la ADR con el siguiente identificador libre al momento de escribirla (`ADR-00010` a la fecha: `ADR-00009` la ocupa la autenticación de personas) con el enlace cruzado en los dos sentidos
- la premisa «no hay clientes de terceros» se reescribe o se declara superada en el **conjunto medido** con `git grep -n -i "no hay clientes de terceros" -- SDD ':!*/_legacy/*' ':!SDD/Docs/Audit/*'` (fuera de `_legacy/` y de `Audit/`, que son registros), que a la fecha de esta ficha devuelve exactamente estos dieciséis lugares en once documentos:
  - `SDD/Docs/README.md` (l.40)
  - `SDD/Docs/Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md` (l.15, l.27)
  - `SDD/Docs/Producto/Contratos-Inter-Unidad/Contratos-Abstractions.md` (l.178)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` (l.58, l.211)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/03-UX-UI-DX/DX-Developer-Experience.md` (l.31)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md` (l.31)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md` (l.15, l.44) — la ADR que esta tarea reescribe
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/05-Arquitectura-Tecnica/Contratos-REST.md` (l.206)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/Estrategia-Calidad.md` (l.77)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/08-Calidad-Y-Pruebas/README.md` (l.86)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Estrategia-Versionado.md` (l.34)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/Guia-Publicacion-Image-Docker.md` (l.40)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Api/09-Devops/README.md` (l.72)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/Guia-Publicacion-Front-Ftp.md` (l.39)
  - `SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/09-Devops/README.md` (l.63)
  - `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` (l.794, l.798, l.1176, l.1934) — la fila «Versionado del contrato» (l.794) ya asienta que la premisa «deja de serlo»; las demás ocurrencias son transcripciones anteriores o registro de control de cambios y quedan igual de alcanzadas
  
  Un documento queda cubierto por el criterio cuando reescribe la premisa, la declara superada, o remite a `ADR-00008`/al intake §17.1.P.3 sin repetirla como cierta. `ADR-08003` es de **nivel Producto** (fuera de esta unidad de entrega): esta ficha no la reescribe, pero **declara la reescritura como consecuencia obligada** que la categoría de Producto debe ejecutar, por el criterio 6 de la DoR

## 4. Dependencias

Ninguna.

## 5. Tipo

`docs`.

## 6. Estimación

**No aplica.** El producto no estima: planifica por etapas con punto de control bloqueante. `PA-01` de [`../Product-Backlog.md`](../Product-Backlog.md) §6 quedó **cerrado por lectura** el 2026-08-25, y el fundamento está en su §4.1 (ver también [`../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md`](../historias-usuario/US-00001-Canjear-Correo-Y-Contrasena-Por-Un-Acceso-Firmado.md) §5.b).

## 7. Trazabilidad a US

| Aspecto | Contenido |
| --- | --- |
| US que la consumen | **Infraestructura compartida**: habilita a las ocho tareas restantes de esta épica |
| CU upstream | — (sin CU: exposición pública, `PRODUCT-INTAKE` 4.3) |
| Puntos de acceso que toca | Ninguno |
| Fuente de arquitectura | `ADR-00008` (a reescribir); [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) (nivel Producto, misma premisa) |
| Alcance fuera de la unidad (criterio 6 de la DoR) | **Sí**: `ADR-08003` es de nivel Producto y obliga también a `GeometriaFactory-Contracts` |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-09-12 | **Extracción a archivo individual** de la fila BT-00027 de [`../Backlog-Tecnico.md`](../Backlog-Tecnico.md) v3.0 §3.1, por cruce del umbral de treinta `BT` que fija `Rules-Backlog-Tecnico.md` §3.3 para el bloque `00xxx` (`GeometriaFactory-Api`, 35 tareas con la apertura de la fase `k`). **Contenido transpuesto sin reescritura** desde la fila del catálogo (§3.1), la épica EP-T06 de §2.1 y la fila correspondiente de la matriz de trazabilidad (§4.1). Autocorrección de la misma corrida (`Master-Prompt.md` §8.1), `ORIGEN DEL HECHO: de la corrida`, base `9167e68`. Evidencia en [`../../../../Audit/Apertura-Fase-k-2026-09-12.md`](../../../../Audit/Apertura-Fase-k-2026-09-12.md). |
| 1.1 | 2026-09-12 | **Evaluación de DoR** (`Definition-Of-Ready.md` §2.1). Cumple los seis criterios sin corrección: el criterio 1 se sostiene en `ADR-00008` vigente, citada en §2 y en §7 («Fuente de arquitectura»), que **es** una de las fuentes admitidas por el criterio (una ADR); el criterio 2 se sostiene en la misma ADR, que es lo que esta tarea reescribe; los criterios 3, 5 y 6 se verifican por inspección directa de la ficha. Pasa a **Ready**. Evidencia en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md). |
| 1.2 | 2026-09-12 | **Corrección de hueco, detectada por el orquestador contra el árbol.** La ficha reescribía la premisa «no hay clientes de terceros» sólo en `ADR-00008`, pero la misma premisa vive en más lugares del árbol vivo. Se agrega un criterio de aceptación con el **conjunto medido** por `git grep -n -i "no hay clientes de terceros" -- SDD ':!*/_legacy/*' ':!SDD/Docs/Audit/*'` (dieciséis lugares en once documentos, listados en §3, fuera de `_legacy/` y de `Audit/`, que son registros) y el comando al lado, para que la tarea no cierre habiendo reescrito una sola fuente y dejado el resto contradiciéndola. Se agrega además, en §2 y en §7, la cita a [`ADR-08003`](../../../../Producto/Adrs/ADR-08003-Versionado-Por-Compilacion-Compartida.md) —**nivel Producto**, una de las fuentes que sostiene la misma premisa— y se declara, por el criterio 6 de la DoR, que la decisión **alcanza fuera de esta unidad de entrega** (obliga también a `GeometriaFactory-Contracts`). Reevaluación de los seis criterios de `Definition-Of-Ready.md` §2.1 con la corrección aplicada: **1: Sí** (`ADR-00008` vigente, §2; una ADR); **2: Sí** (§7, infraestructura compartida sostenida por la misma ADR); **3: Sí** (los tres criterios de §3 son verificables por inspección, y el tercero fija el conjunto medido con el comando de verificación); **4: N/A** (§7: «Puntos de acceso que toca: Ninguno»); **5: Sí** (§4: «Ninguna», sin dependencias); **6: Sí, con declaración** (`ADR-08003` es nivel Producto: se declara que la decisión alcanza fuera de la unidad, §7). Los seis dan: **sigue Ready**. Evidencia y corrección en [`../../../../Audit/DoR-Tramo-k-2026-09-12.md`](../../../../Audit/DoR-Tramo-k-2026-09-12.md), sección BT-00027 reevaluada. |
| 1.3 | 2026-09-12 | **Corrección de identificador.** El criterio 2 de §3 nombraba `ADR-00009` como la ADR que reemplazaría a `ADR-00008`; ese identificador quedó ocupado por [`ADR-00009`](../../05-Arquitectura-Tecnica/Adrs/ADR-00009-La-Api-Autentica-Personas-No-Aplicaciones.md) (la API autentica personas, no aplicaciones; **Aceptado**, 2026-09-12). El criterio pasa a decir «el siguiente identificador libre al momento de escribirla (`ADR-00010` a la fecha)», verificado con `ls 05-Arquitectura-Tecnica/Adrs/`. **Ningún otro criterio, fuente ni dependencia cambia.** Sigue **Ready**. Entra además a la lista de §3 una constancia: la premisa «no hay clientes de terceros» que esta tarea reescribe se lee ahora con `ADR-00009`, que descarta modelar terceros —quien usa la API es una persona con uno de los dos papeles, a través de la aplicación que sea—; la reescritura sigue siendo de esta tarea. |
| 2.0 | 2026-09-12 | **Cierre: pasa a `Done`.** Rama `fase-k/bt-00027-adr-versionado-rutas` sobre la base `cfb11f7` (`main`); commits `43e7ed2` (ADR-00010), `b53d000` (ADR-00008 superada), `1074eea` (trece documentos del conjunto medido), `a83eb75` (`Estrategia-Versionado.md`), `72e9581` (intake 4.5), `3c0d77d` (índices de 05), `659d91b` (`changelog.md`), `f0a3fa9` (evidencia). Evidencia completa, con comando y salida, en [`../../../../Audit/BT-00027-Cierre-2026-09-12.md`](../../../../Audit/BT-00027-Cierre-2026-09-12.md). **Criterio 1 — cumple**: [`ADR-00010`](../../05-Arquitectura-Tecnica/Adrs/ADR-00010-Version-En-La-Ruta-Solo-Major-Para-La-Superficie-Publica.md) §1 declara el cambio de premisa (hay aplicaciones propias además del front que no compilan contra el ensamblado, `ADR-00009`) y §2.1 adopta `/v{MAJOR}/` con el `MAJOR` del producto (`D-03`, MinVer, remitido a `BT-00033`); verificado con `grep -n "MAJOR" …/ADR-00010-*.md`. **Criterio 2 — cumple**: el identificador libre era `ADR-00010` (`ls 05-Arquitectura-Tecnica/Adrs/ | grep ADR-0000` → nueve archivos); `ADR-00008` 1.1 pasa a `Superado parcialmente por ADR-00010` en cabecera y §3, con aviso de lectura que dice qué se deroga (regla 1, convenciones de §7 que la aplican, dos primeras métricas de §8) y qué subsiste (reglas 2 a 5, ausencia declarada); enlace cruzado en los dos sentidos verificado con `grep -c "ADR-00010" ADR-00008-*.md` (5) y `grep -c "ADR-00008" ADR-00010-*.md` (22). **Criterio 3 — cumple**: el `grep` del criterio, remedido sobre la base antes de empezar, devolvió **23 líneas en 17 archivos** (la lista de la ficha decía «dieciséis lugares en once documentos» y enumeraba dieciséis documentos; contando la propia ficha, que se excluye, son dieciséis documentos y 19 líneas fuera de la ficha; la diferencia con la lista escrita es la fila 4.4 del intake, l.1934, agregada después de la 1.2). Después: 47 líneas, todas clasificadas en el §1 de la evidencia —reescritas con la fecha desde la que rige o negadas (14), avisos de lectura (2), la ADR nueva (3), registro conservado bajo aviso en las dos ADR superadas (4), control de cambios (20), la propia ficha (4)—; ninguna afirma la premisa como cierta. `ADR-08003` (nivel Producto) recibe aviso de lectura y control de cambios sin reescritura del cuerpo, y la reescritura queda declarada como consecuencia obligada de la categoría de Producto. El intake: l.794 y l.798 ya negaban la premisa desde la 4.3 y no se tocan; l.1176 (§17.2.P.3 · Contracts) era la única transcripción viva restante y se reescribe en la **4.5** por delegación expresa de la entrada 4.4; l.1934-1936 son control de cambios. Verificaciones de enlaces (21 archivos, 0 rotos), de ancho de tablas (todos los pares coinciden) e índices de 05 (`ADR-00008` y `ADR-00010` en `Decisiones-Arquitectura.md` 2.2 y `README.md` 2.2) en la evidencia. **Alcance respetado**: sin `src/`, sin rutas (`BT-00032`), sin evento de etiqueta ni herramienta (`BT-00033`), sin política de deprecación (`BT-00035`). Sube major: la ficha cambia de estado de vida. |
