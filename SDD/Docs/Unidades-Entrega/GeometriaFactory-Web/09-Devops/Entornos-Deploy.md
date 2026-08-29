# Entornos y despliegue — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Entornos-Deploy.md
**Versión:** 3.3
**Estado:** Propuesto
**Fecha:** 2026-08-26
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** **3 secciones existen sólo en `GeometriaFactory-Visor`** —«Ambientes y canales de este proyecto de código», «Resolución de `PA-05`: el bundle se ignora en el repositorio», «Secretos»—, y son las que el portal no podía declarar porque describen el componente empaquetado que viaja adentro.

---

## 1. Ambientes de este proyecto de código

### 1.1 `GeometriaFactory-Web`

**Dos, y son los que la fuente declara.** No hay un tercero ni un cuarto, y decirlo es más útil que dibujar una escalera de cuatro ambientes que nadie provisiona.

| Ambiente | Destino | Quién lo aprueba | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| **Desarrollo** | El contenedor de desarrollo, en la máquina de quien construye. Ahí corre el front en local, contra el servicio de datos que también corre en local | Nadie: no hay promoción hacia él | No aplica |
| **Hosting público** | La única unidad desplegable de este proyecto de código: la publicación en el hosting gratuito, con dominio público y transporte seguro | El Product Owner, en el punto de control de la etapa | **Sin acuerdo de nivel de servicio.** El intake §17.2.P.12 · GeometriaFactory-Web declara el hosting gratuito con el **reciclado del proceso como riesgo sin mitigación en el código** (`R-06`) |

**La segunda fila no declara un SLO y no es un olvido.** El intake §17.2.P.12 · GeometriaFactory-Web acepta depender de un hosting gratuito a cambio de tener dominio público y transporte seguro donde la red de la facultad no bloquea, y `PT-01.c` mide **20 minutos** de navegación continua sin que el proceso recicle el circuito. Un acuerdo de disponibilidad mensual sobre una infraestructura que ninguna de las partes controla sería un número sin quien lo sostenga.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `web-monolith` el modelo `DEV` / `QA` / `STAGING` / `PROD`, y §2.2 admite agregar ambientes pero **no quitar ninguno sin un ADR que lo justifique**. Acá hay **dos** y no cuatro. El apartamiento se declara con sus tres fundamentos, todos verificables:

| Fundamento | Dónde se verifica |
| --- | --- |
| **El presupuesto declarado es cero.** El intake §10 declara «sin presupuesto monetario asignado» y enumera las **tres** piezas de infraestructura de costo cero: el hosting gratuito, el servidor domiciliario ya existente y el trabajo del docente más agente IA. Un `QA` y un `STAGING` son dos hostings más | Intake §10 |
| **El aprobador sería el mismo en los cuatro.** `equipo_n` es 1: la misma persona construye, publica y aprueba. Una escalera de cuatro ambientes con un solo aprobador es ceremonia, no control | Intake §2; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 |
| **Lo que un `STAGING` compraría, acá lo compra el paso 8 del flujo.** Un ambiente de ensayo existe para descubrir que la publicación quedó rota antes de que la vea un usuario; el intake §17.2.P.8 · GeometriaFactory-Web resuelve ese mismo problema haciendo que **el flujo no termine en la subida, sino comprobando que la dirección pública responde** | Intake §17.2.P.8 · GeometriaFactory-Web; `QG-10003` |

**El ADR que sostiene el apartamiento es [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md)**, que en su §4 evalúa cuatro alternativas de despliegue y adopta la comprobación final como sustituto del ensayo previo, y en su §6 acepta por escrito los dos trade-offs que ese apartamiento implica: la subida no transaccional y que una intermitencia del hosting pueda marcar en rojo un despliegue correcto.

**Lo que este apartamiento cuesta, y se declara en lugar de disimularse.** Sin ambiente de ensayo, **la primera vez que una publicación se ejerce en condiciones reales es en producción**. El producto lo compensa con tres cosas y ninguna es un cuarto ambiente: la comprobación final del flujo, la publicación **fuera del horario de uso** y la reversión por republicación desde la etiqueta anterior.

## 2. El bundle no se versiona: el tramo que le toca al anfitrión

### 2.1 `GeometriaFactory-Web`

[`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §2 cerró el punto abierto `PA-05` de aquel proyecto de código: **el bundle generado no se versiona en el repositorio; se ignora, y lo genera la canalización antes de publicar.** Esta categoría **adopta la decisión y no la reabre**, y con eso queda cerrado también el `PA-07` de `05` §11 de este proyecto de código, que preguntaba lo mismo desde este lado y lo derivaba a 09.

Lo que sí le toca a esta categoría es **el tramo de esa decisión que ocurre en su directorio**. Aquella §2.2 declaró cuatro exigencias operativas y una quedó como acción pendiente, con la fecha de su lectura. Ésta es su verificación desde este lado:

| Exigencia | Estado al **2026-08-11**, leído sobre el archivo `.gitignore` del repositorio | Quién la aplica |
| --- | --- | --- |
| El control de versiones **excluye el bundle copiado** bajo `src/GeometriaFactory.Web/wwwroot/js/` | **No cumplido todavía.** El archivo no excluye esa ruta. La línea que excluiría el directorio de recursos estáticos entero **está comentada**, y así debe seguir: ese directorio también contiene recursos escritos a mano, y excluirlo entero sacaría del repositorio archivos que sí deben estar | **Esta categoría lo asigna a `BT-10001`**, etapa `a` |
| El control de versiones **excluye el directorio de salida del empaquetado** en `visor/` | **No cumplido todavía.** El archivo no excluye esa ruta; **sí excluye** el directorio de dependencias del ecosistema del navegador | El mismo `BT-10001`, en el mismo acto |
| El flujo de publicación **genera el bundle en su propio interior** | **Cumplido por diseño**: es el paso 4 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, y `QG-10002` lo hace bloqueante | Esta categoría |
| Toda ejecución local que necesite el bundle **lo construye antes** | Declarado por el intake §17.2.P.8 · GeometriaFactory-Visor para el guion propio del bundle | Quien ejecuta |

**Las dos primeras filas son una sola acción y esta categoría la cierra asignándola.** [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §2.2 la había dejado en «el equipo, en la etapa `a`, al poner en pie la cadena de construcción»; acá queda con dueño y con tarea: **`BT-10001` de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md)**, «crear el proyecto del front con su flujo de publicación», que es la tarea de la etapa `a` que pone en pie este flujo. Queda registrada como `PD-02` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10.

**Por qué la exclusión importa y no es higiene.** El intake §13 y §17.2.P.7 · GeometriaFactory-Visor declaran que el bundle **nunca se edita a mano**; el `QG-12009` de `GeometriaFactory-Visor` lo rechaza en revisión. Un archivo generado, versionado y visible en cada revisión es exactamente el que alguien corrige «en el acto» cuando tiene apuro, y además produce en cada cambio del fuente una diferencia ilegible que **inutiliza la revisión que tendría que detectarlo**.

**El estado se declara con su fecha de lectura**, para que la próxima categoría que lo verifique sepa contra qué estado se escribió este documento.

## 3. Provisión

### 3.b La aprobación de `plan` antes de `apply` — **ítem propio**

**Esta subsección realiza el ítem 2.b de `Rules-Devops.md` §4.4**, que desde la regla **6.0** lo pide
separado de la herramienta por ser **política de proceso** y no consecuencia de la herramienta. La
numeración local es `3.b` porque la provisión de esta unidad vive en §3; **el ítem de la regla que
realiza es el 2.b**, y se lee contra él.

| Aspecto | Decisión |
| --- | --- |
| **Aprobación de `plan` antes de `apply`** | **No aplica**, y no está diferida |
| **Por qué** | §3.1 declara que **no hay infraestructura declarativa**: el hosting es un servicio de terceros que se contrata y se configura por fuera del repositorio. Sin herramienta no hay `plan` ni `apply` |
| **Qué ocupa su lugar** | El pull request por etapa, y **las dos puertas bloqueantes que el flujo de publicación corre antes de subir** —`QG-01` y `QG-02` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1—, que es lo más parecido a un `plan` aprobado que este despliegue tiene: se verifica antes de tocar el destino |
| **Qué lo reabriría** | Que el front pase a un hosting provisionado con código |
| **Apartamiento que lo autoriza** | [`ADR-14004`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md), que declara «no aplica» como tercera salida frente a un ítem obligatorio **sin objeto**, con sus tres partes obligatorias |
| **Estado de ese apartamiento** | **`Aceptado`** — aprobado por el Product Owner el **2026-08-26**. La declaración deja de apoyarse en un instrumento sin aceptar, y el apartamiento **cuenta como decisión y no como omisión** |

**No se difiere**: está contestado con su motivo y su condición de reapertura, y por eso no lleva la
forma de `Root-Rules.md` §12.2.

### 3.1 `GeometriaFactory-Web`

**No hay infraestructura declarativa, y la ausencia es de la fuente y no de esta categoría.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Herramienta declarativa de infraestructura | **Ninguna.** El hosting es un servicio gratuito de terceros que se contrata y se configura por fuera del repositorio; no se provisiona con código | Intake §10, tres piezas de infraestructura de costo cero |
| Qué sí vive en el repositorio | **El flujo de publicación**, `.github/workflows/deploy-front-ftp.yml`, que el árbol del intake §16 declara. Es la única pieza de automatización de despliegue de este proyecto de código, y **está versionada** | Intake §16 |
| Dependencias de infraestructura en ejecución | **Una**: el servicio de datos, por dirección tomada de configuración. **Ninguna base de datos, ningún almacén de secretos propio y ningún servicio adicional** | `05` §5 |
| Estado del proceso | En memoria del servidor del hosting, y **se pierde en cada reciclado**. No hay volumen, no hay caché externa y no hay sesión persistente | Intake §17.2.P.4 · GeometriaFactory-Web; [`ADR-10002`](../05-Arquitectura-Tecnica/Adrs/ADR-10002-Sin-Estado-Propio-Y-Sin-Persistencia.md) |

**La cuarta fila es la que hace posible a la primera.** Un front sin estado propio no necesita provisión: no hay volumen que crear, ni base que migrar, ni caché que calentar. El intake §17.2.P.4 · GeometriaFactory-Web lo declara sin rodeos —«el front no guarda estado propio: es exactamente el problema que la topología evita»— y esa decisión es la que permite que el despliegue entero sea copiar archivos y comprobar que responden.

### 3.2 `GeometriaFactory-Visor`

**No hay infraestructura declarativa atribuible a este proyecto de código**: no provisiona servidor, red ni almacenamiento. Lo que necesita del entorno es lo que declara §4 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md): el contenedor de desarrollo para construir, y **un navegador con capacidad gráfica tridimensional más un conductor** para medir. Ese segundo requisito **no es un ambiente que se provisione con infraestructura declarativa**: es una capacidad del ejecutor, y su ausencia deja a este proyecto de código sin sus gates principales (`PD-02` de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10).

La infraestructura del producto —imagen del backend y flujo de trabajo de publicación del front— pertenece a los dos proyectos de código que se despliegan y **no se describe acá**.

## 4. Configuración

### 4.1 `GeometriaFactory-Web`

Configuración de doce factores: **fuera del código, en variables inyectadas al publicar**.

| Valor de configuración | De dónde sale | Quién lo conoce |
| --- | --- | --- |
| **Dirección del servicio de datos** | Del almacén de secretos del repositorio, inyectada en el paso 6 del flujo. **Nunca embebida en el código** | Únicamente el componente que hace de cliente tipado del servicio de datos. **Ningún otro componente la conoce y ninguna superficie la muestra** ([`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §7) |
| Credenciales del canal de publicación | Del mismo almacén, usadas en el paso 7 | El flujo de publicación |
| Configuración del bundle | **Ninguna, y es prohibición explícita.** `RA-02`: el bundle no lee configuración propia | [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](Entornos-Deploy.md) §4 |
| Configuración que la persona pueda fijar | **Ninguna.** No hay superficies de configuración en el producto, y un parámetro que la superficie no gobierna **no se dibuja ni siquiera deshabilitado** | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §4, tercera alternativa descartada |

**Sólo hay dos valores de configuración en todo este proyecto de código**, y los dos son secretos. No hay mapa de variables por ambiente porque **no hay dos ambientes con configuración distinta**: en desarrollo el front apunta al servicio de datos que corre en local, y en el hosting apunta al del servidor propio; es el mismo parámetro con otro valor.

**La última fila es una decisión de producto y no de esta categoría.** Ofrecer una pantalla donde el docente escriba la dirección del servidor propio habría puesto **la dirección de un servicio interno en el navegador**, que es lo que `RA-03` prohíbe. La categoría 03 la descartó por eso y [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §4 lo registra.

### 4.2 `GeometriaFactory-Visor`

**Ninguna, y es prohibición explícita y no ausencia.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Configuración propia en ejecución | **Ninguna.** El bundle **no lee configuración propia** | Intake §17.2.P.3 · GeometriaFactory-Visor; `RA-02` del intake §14 |
| Persistencia | **Ninguna, y es prohibición explícita**: no guarda estado entre páginas ni escribe en el almacenamiento del navegador. `QG-12005` lo verifica con umbral **0** | Intake §17.2.P.4 · GeometriaFactory-Visor; `08` `Estrategia-Calidad.md` §3 |
| Identidad y papel del usuario | **Ninguno, y también es prohibición explícita**: el bundle no sabe quién es el usuario ni qué papel tiene, y no participa de ninguna decisión de autorización | Intake §17.2.P.5 · GeometriaFactory-Visor |
| Preferencia de movimiento reducido del entorno | **La lee el anfitrión, no el bundle.** El anfitrión le pasa dos valores de verdad por la fachada | Intake, control de cambios 1.7, decisión (c); `08` `Estrategia-Testing.md` §5 |

**Estas cuatro filas son la razón por la que este proyecto de código no puede tener ambientes.** Un ambiente se distingue de otro por su configuración, y acá no hay ninguna: el mismo bundle, byte por byte, sirve para el sample sin backend, para el front en local y para el front publicado. **Es el mismo artefacto en los tres lugares**, y eso es lo que `RA-02` compra.

## 5. Secretos, y la dirección que no se versiona

### 5.1 `GeometriaFactory-Web`

| Secreto, nombrado por su función | Dónde vive | Quién lo usa | Rotación |
| --- | --- | --- | --- |
| **Dirección base del servicio de datos** | Almacén de secretos del repositorio. **La dirección real del servidor propio no se versiona** | El paso 6 del flujo, que la inyecta en la publicación | **Cuando la dirección cambia.** Ver más abajo |
| **Credenciales del canal de publicación** | El mismo almacén | El paso 7 del flujo | **No se declara ninguna frecuencia**: ninguna fuente la da, y un plazo en días inventado acá sería un compromiso sin dueño |

**Ningún secreto entra al repositorio, y ninguno de los dos aparece en este documento ni en ningún otro de esta cadena.** El intake §17.2.P.5 · GeometriaFactory-Web lo declara: la dirección y las credenciales viven como secretos del repositorio y se inyectan en la publicación, y **la dirección real del servidor propio no se versiona**. Esta categoría los nombra **por su función** y declara dónde vive el valor, que es todo lo que un documento de especificación puede decir de un secreto.

**Qué pasa cuando la dirección del servidor propio cambia**, que la topología hace probable —el intake §10 declara que el servidor domiciliario **no tiene dirección estática**, y que el Product Owner admite apuntar a la dirección directa con el servicio de nombres dinámico como recomendación—:

| Paso | Qué se hace | Fundamento |
| --- | --- | --- |
| 1 | **Nada se recompila y nada se revierte.** El código no la contiene | [`ADR-10007`](../05-Arquitectura-Tecnica/Adrs/ADR-10007-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §5, consecuencia positiva 2 |
| 2 | Se actualiza el valor del secreto | El mismo |
| 3 | Se vuelve a publicar el front, **con el flujo entero**, incluida la comprobación final del paso 8 | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 |
| 4 | Se rehace la comprobación de `PT-01.d`: una llamada de salud que devuelva **datos reales** del servidor propio | Intake §17.2.P.10 · GeometriaFactory-Web, fila `PT-01.d` |

**Cómo se entera alguien de que cambió, que es la parte incómoda.** Nada la detecta automáticamente: **ninguna fuente declara un vigilante, un servicio de monitoreo ni una alerta**, y esta categoría no inventa uno. Lo que sí está declarado es qué se ve cuando ocurre: el front entra en **estado degradado**, que es una superficie del producto ([`ADR-10005`](../05-Arquitectura-Tecnica/Adrs/ADR-10005-Estado-Degradado-Como-Superficie.md)), y **ese mensaje nunca incluye la dirección del servicio interno** (`QG-10008`, `RA-03`). El síntoma es visible y el diagnóstico no se filtra: es exactamente el reparto que `RA-03` pide.

**Si el Product Owner adopta el servicio de nombres dinámico** —que la fuente declara como recomendación y no como decisión—, el valor del secreto pasa a ser un nombre estable y los cuatro pasos de arriba dejan de ejecutarse ante cada cambio de dirección. **Esta categoría no lo adopta por su cuenta**: la fuente registra la decisión del Product Owner de admitir la dirección directa, y cambiarla es suya.

## 6. Promoción

### 6.1 `GeometriaFactory-Web`

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Rama principal → publicación en el hosting | El flujo de publicación, disparado por fusión con cambios en las rutas del filtro, o a mano | El mismo, con el registro del flujo | El registro del flujo, con la respuesta de la dirección pública y **la hora** |
| **Cambio incompatible del contrato → etapa cerrada** | La constancia de que **las dos unidades desplegables salieron desde el mismo estado del repositorio**, **primero el backend** (intake §17.2.P.7 · GeometriaFactory-Web desde 1.22) | El mismo, con constancia escrita | La constancia en el informe de cierre. Es el `QG-08008` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa** |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, más `QG-10004` y `QG-10011` en verde |

**La tercera fila es la obligación que esta unidad recibe de otro proyecto de código**, y no la inventa esta categoría: el intake §17.2.P.3 · GeometriaFactory-Contracts declara que las dos unidades se despliegan juntas ante un cambio de contrato. Su tratamiento operativo entero, incluidas las tres decisiones derivadas y el hallazgo de que **el desfase de momentos es irreducible mientras un extremo se despliegue a mano**, está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2.

**La publicación se hace fuera del horario de uso.** No es una recomendación: el intake §17.2.P.8 · GeometriaFactory-Web lo declara como tratamiento de una subida **no transaccional** (`R-03`), y la Definition of Done §1.4 lo exige con la hora registrada.

### 6.2 `GeometriaFactory-Visor`

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| **Momento de medición → compromiso de la etapa `g`** | `PT-02` y `PT-03` pasadas enteras | El mismo | El registro de la medición, **con la condición de cada ausencia** |
| Bundle construido → bundle en los recursos estáticos del anfitrión | El stage de copia | Automático dentro de la construcción | La constancia de que el bundle corresponde al fuente que lo generó |
| Bundle en el anfitrión → bundle servido en el hosting | La publicación del front, que un cambio bajo `visor/` dispara | Categoría 09 de `GeometriaFactory-Web` | La verificación de que la dirección pública responde, que el intake §17.2.P.8 · GeometriaFactory-Web declara como cierre obligatorio de ese flujo |

**La última fila no la ejecuta este proyecto de código**, y se declara igual porque es donde su artefacto termina. El intake §17.2.P.8 · GeometriaFactory-Web declara que ese flujo **no termina en la subida, termina comprobando que la dirección pública responde**, con el fundamento de que una subida que deja la aplicación caída y se reporta como exitosa es peor que una falla visible.

## 7. Ambientes y canales de este proyecto de código

### 7.1 `GeometriaFactory-Visor`

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

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo `preview` / `stable` sobre feed único. **Acá no hay feed**, y el ADR que lo justifica es anterior a esta categoría: [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §4 evaluó publicar el bundle en un repositorio público de paquetes y lo descartó porque el intake lo descarta explícitamente, `redistribuible` es false y no hay integradores externos: **sería un portal para una comunidad que no existe**.

**El caso de este proyecto de código tiene además un rasgo que los otros dos de nivel topológico 0 no tienen**: su artefacto sí es un archivo que se entrega —se copia al anfitrión— y por eso la categoría emite [`Guia-Publicacion-Bundle-Visor.md`](Guia-Publicacion-Bundle-Visor.md), que documenta esa entrega interna con la estructura que `Rules-Devops.md` §4.5 exige. **Entrega no es publicación**, y el documento lo declara en su primera sección para que nadie lea un canal donde no lo hay.

## 8. Resolución de `PA-05`: el bundle se ignora en el repositorio

### 8.1 `GeometriaFactory-Visor`

`05` §11 registra el punto abierto `PA-05` —si el bundle generado **se versiona en el repositorio o se ignora**— y declara que **lo cierra la categoría 09, al emitirse**. [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) §6, punto 4, acepta explícitamente que la decisión quede abierta y que la elección pertenezca a 09. **Se cierra acá.**

**Decisión: el bundle generado no se versiona en el repositorio. Se ignora, y lo genera la canalización antes de publicar.**

El intake §17.2.P.7 · GeometriaFactory-Visor admite las dos formas y le pone condición a cada una: si se versiona, se versiona como salida reproducible; **si se ignora, `scripts/build.sh` lo genera antes de publicar**. Los cuatro fundamentos de la elección, todos verificables abriendo la fuente que se cita:

| Fundamento | Dónde se verifica |
| --- | --- |
| **La condición que el intake pone para ignorar ya está cumplida por el diseño de la canalización del front.** Sus pasos son: obtener el código, preparar las dos cadenas de herramientas, **instalación reproducible de dependencias en `visor/`, el empaquetador genera el bundle y lo copia a los recursos estáticos**, publicación del front, inyección de configuración desde secretos, subida y verificación de que la dirección pública responde | Intake §17.2.P.8 · GeometriaFactory-Web, pasos del flujo de trabajo |
| **Hay un gate bloqueante del producto que prohíbe usar un bundle versionado.** El mismo §17.2.P.8 · GeometriaFactory-Web declara entre los quality gates bloqueantes del front que el **bundle se genera en el mismo flujo de trabajo, nunca tomado de un artefacto viejo**. Un bundle versionado en el repositorio sería, por definición, un artefacto anterior a esa ejecución: quedaría en el repositorio sin que nadie lo consuma | Intake §17.2.P.8 · GeometriaFactory-Web, quality gates |
| **Versionarlo abre la puerta al único defecto que este proyecto de código tiene prohibido sin excepción.** El intake §13 y §17.2.P.7 · GeometriaFactory-Visor declaran que el bundle **nunca se edita a mano**; `QG-12009` lo rechaza en revisión y `CV-12030` lo declara bloqueante. Un archivo generado, versionado y visible en cada revisión es exactamente el que alguien corrige «en el acto» cuando tiene apuro | Intake §13 y §17.2.P.7 · GeometriaFactory-Visor; `08` `Estrategia-Calidad.md` §3 y `Criterios-Validacion.md` §6 |
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

## 9. Secretos

### 9.1 `GeometriaFactory-Visor`

**Ninguno, en ninguno de los dos momentos**, y la afirmación es más fuerte que en los otros dos proyectos de código de nivel topológico 0:

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** La instalación de dependencias se hace desde el registro público del ecosistema, sin credencial, y no hay publicación que autenticar | Intake §17.2.P.7 · GeometriaFactory-Visor: no se publica |
| Ejecución | **Ninguno, y es imposible que los haya**: el bundle no hace red —`QG-12004`, umbral **0**— y no lee configuración. No tiene por dónde recibir un secreto ni a dónde mandarlo | Intake §17.2.P.3 · GeometriaFactory-Visor y §17.2.P.5 · GeometriaFactory-Visor |

**La contribución de este proyecto de código a la seguridad del producto es una ausencia**, y así lo declara [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §1: no hacer red es lo que hace **imposible** violar `RA-01` desde el navegador. Desde esta categoría, la consecuencia práctica es que **un stage de este proyecto de código que pidiera una credencial sería la señal de que algo se salió de su alcance**.

**No se declara ninguna frecuencia de rotación**: no hay secreto propio. Los del producto pertenecen a la categoría 09 de `GeometriaFactory-Web` y de `GeometriaFactory-Api`.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 3.3 | 2026-08-29 | **Tramo `R-4` · renumerado de `QG` y `CV` al mapa de bloques del destino**, decidido por el Product Owner el 2026-08-29 al **retirar el `ADR-14005`** en lugar de aceptarlo. **9 línea(s)** pasan de `QG-NN` a `QG-<bloque>NNN`, con el bloque **deducido de la línea o de la sección y nunca inventado** — `00` Api, `02` Domain, `04` Application, `06` Infrastructure, `08` Contracts, `10` Web, `12` Visor. Con esto las dos familias **dejan de necesitar apartamiento**: cumplen [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) y `Root-Rules.md` §9.1 y §9.2. Las referencias cuyo bloque no estaba en el texto **conservan la forma vieja a propósito** y quedan inventariadas en [`../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md`](../../../Audit/Inventario-Renumerado-R-4-2026-08-29.md). Se respeta §4.1: no se tocan las filas de control de cambios ni lo que está entre «…». |
| 3.1 | 2026-08-24 | **Ronda 3 del corte 09 de la migración 10.0 → 13.3**, sobre el re-audit independiente, que pasó de RECHAZADO a **APROBADO CON HALLAZGOS**: el P0 y los cinco P1 quedaron cerrados y aparecieron cuatro P2 y tres P3. **§3.b suma el estado del apartamiento en el que se apoya** (**P3**): `ADR-14004` está **`Propuesto`** y la emisión anterior lo citaba como si ya autorizara. |
| 3.2 | 2026-08-26 | **El apartamiento en el que se apoya §3.b pasa a `Aceptado`.** El Product Owner aprobó [`ADR-14004`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md) el **2026-08-26**, sin modificar su contenido. La fila que declaraba que la figura «no aplica» se sostenía sobre un instrumento **todavía no aprobado** deja de hacer falta: con la aceptación, **el apartamiento cuenta como decisión y no como omisión** (`Root-Rules.md` §11), que es la diferencia que ese ADR existe para producir. **Nada más cambia**: el ítem sigue contestado igual, con su motivo y su condición de reapertura. Sube **minor**. |
| 3.0 | 2026-08-24 | **Ronda 2 del corte 09 de la migración 10.0 → 13.3**, que repara lo que el **audit independiente** de la ronda 1 levantó. **El veredicto fue RECHAZADO**, con un **P0**: `Migracion-Rules.md` §6 lista «estado previo no archivado» entre los hallazgos que **detienen la cadena**, y la ronda 1 no archivó. La justificación que había invocado —el precedente de editar en el lugar de la migración anterior— **la refuta el propio `ADR-14001` §4**, que acota su apartamiento a «la migración 6.0 → 8.6 y sólo esa» y declara que el archivado de un documento que **sube de versión sin cambiar de lugar sigue siendo por carpeta**. El estado previo queda en `_legacy/2026-08-24/`. **Y §3.b suma la cita de [`ADR-14004`](../../../Producto/Adrs/ADR-14004-Item-Obligatorio-Sin-Objeto-Se-Declara-No-Aplica.md)**, el apartamiento que autoriza la figura «no aplica» y que la ronda 1 usó sin declarar; **P2** del audit. **Y sube MAJOR y no minor, corrigiendo el criterio de la fila anterior.** La ronda 1 bumpeó minor con el argumento de que partir una sección no cambia ninguna decisión; el propio destino había bumpeado **major** cinco días antes por la misma operación, con el argumento de que **cambia la estructura de la sección para corresponder con la de la regla**. Los dos razonamientos se sostienen por separado, pero convivir sin declararlo dejaba la serie midiendo con dos varas. **Se adopta el criterio anterior**, que es el que ya estaba escrito. |
| 2.1 | 2026-08-24 | **Migración normativa 10.0 → 13.3, fase M4** (`Audit/Plan-Migracion-10.0-a-13.3.md` 1.0 §4.2). Entra **§3.b, la aprobación de `plan` antes de `apply` como ítem propio** —numerada `3.b` porque la provisión de esta unidad vive en §3, y declarando que **el ítem de la regla que realiza es el 2.b** de `Rules-Devops.md` **6.0** §4.4—. Se declara **no aplica**, con el fundamento que §3.1 ya traía: el hosting es un servicio de terceros que se configura por fuera del repositorio. Y se nombra lo que ocupa su lugar: **las dos puertas bloqueantes que el flujo corre antes de subir**, `QG-01` y `QG-02`, que es lo más parecido a un `plan` aprobado que este despliegue tiene. Sube **minor**. |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
