# Entornos y despliegue — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Entornos-Deploy.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Deploy Engineer (AG-09)
**Tipo de proyecto de código (D8):** `web-monolith`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §5, §8 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) 1.0; [`../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md`](../05-Arquitectura-Tecnica/Adrs/ADR-03-Credencial-De-Sesion-En-El-Estado-Del-Circuito.md) 1.0; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) 1.0 §2 y §2.2; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.21** §10, §13, §14, §16, §17.6.P.4, §17.6.P.5, §17.6.P.7, §17.6.P.8, §17.6.P.9 y §17.6.P.12
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Guia-Publicacion-Front-Ftp.md`](Guia-Publicacion-Front-Ftp.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes de este proyecto de código](#1-ambientes-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. El bundle no se versiona: el tramo que le toca al anfitrión](#2-el-bundle-no-se-versiona-el-tramo-que-le-toca-al-anfitrión)
- [3. Provisión](#3-provisión)
- [4. Configuración](#4-configuración)
- [5. Secretos, y la dirección que no se versiona](#5-secretos-y-la-dirección-que-no-se-versiona)
- [6. Promoción](#6-promoción)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Ambientes de este proyecto de código

**Dos, y son los que la fuente declara.** No hay un tercero ni un cuarto, y decirlo es más útil que dibujar una escalera de cuatro ambientes que nadie provisiona.

| Ambiente | Destino | Quién lo aprueba | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| **Desarrollo** | El contenedor de desarrollo, en la máquina de quien construye. Ahí corre el front en local, contra el servicio de datos que también corre en local | Nadie: no hay promoción hacia él | No aplica |
| **Hosting público** | La única unidad desplegable de este proyecto de código: la publicación en el hosting gratuito, con dominio público y transporte seguro | El Product Owner, en el punto de control de la etapa | **Sin acuerdo de nivel de servicio.** El intake §17.6.P.12 declara el hosting gratuito con el **reciclado del proceso como riesgo sin mitigación en el código** (`R-06`) |

**La segunda fila no declara un SLO y no es un olvido.** El intake §17.6.P.12 acepta depender de un hosting gratuito a cambio de tener dominio público y transporte seguro donde la red de la facultad no bloquea, y `PT-01.c` mide **20 minutos** de navegación continua sin que el proceso recicle el circuito. Un acuerdo de disponibilidad mensual sobre una infraestructura que ninguna de las partes controla sería un número sin quien lo sostenga.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `web-monolith` el modelo `DEV` / `QA` / `STAGING` / `PROD`, y §2.2 admite agregar ambientes pero **no quitar ninguno sin un ADR que lo justifique**. Acá hay **dos** y no cuatro. El apartamiento se declara con sus tres fundamentos, todos verificables:

| Fundamento | Dónde se verifica |
| --- | --- |
| **El presupuesto declarado es cero.** El intake §10 declara «sin presupuesto monetario asignado» y enumera las **tres** piezas de infraestructura de costo cero: el hosting gratuito, el servidor domiciliario ya existente y el trabajo del docente más agente IA. Un `QA` y un `STAGING` son dos hostings más | Intake §10 |
| **El aprobador sería el mismo en los cuatro.** `equipo_n` es 1: la misma persona construye, publica y aprueba. Una escalera de cuatro ambientes con un solo aprobador es ceremonia, no control | Intake §2; [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) §4 |
| **Lo que un `STAGING` compraría, acá lo compra el paso 8 del flujo.** Un ambiente de ensayo existe para descubrir que la publicación quedó rota antes de que la vea un usuario; el intake §17.6.P.8 resuelve ese mismo problema haciendo que **el flujo no termine en la subida, sino comprobando que la dirección pública responde** | Intake §17.6.P.8; `QG-03` |

**El ADR que sostiene el apartamiento es [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md)**, que en su §4 evalúa cuatro alternativas de despliegue y adopta la comprobación final como sustituto del ensayo previo, y en su §6 acepta por escrito los dos trade-offs que ese apartamiento implica: la subida no transaccional y que una intermitencia del hosting pueda marcar en rojo un despliegue correcto.

**Lo que este apartamiento cuesta, y se declara en lugar de disimularse.** Sin ambiente de ensayo, **la primera vez que una publicación se ejerce en condiciones reales es en producción**. El producto lo compensa con tres cosas y ninguna es un cuarto ambiente: la comprobación final del flujo, la publicación **fuera del horario de uso** y la reversión por republicación desde la etiqueta anterior.

## 2. El bundle no se versiona: el tramo que le toca al anfitrión

[`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2 cerró el punto abierto `PA-05` de aquel proyecto de código: **el bundle generado no se versiona en el repositorio; se ignora, y lo genera la canalización antes de publicar.** Esta categoría **adopta la decisión y no la reabre**, y con eso queda cerrado también el `PA-07` de `05` §11 de este proyecto de código, que preguntaba lo mismo desde este lado y lo derivaba a 09.

Lo que sí le toca a esta categoría es **el tramo de esa decisión que ocurre en su directorio**. Aquella §2.2 declaró cuatro exigencias operativas y una quedó como acción pendiente, con la fecha de su lectura. Ésta es su verificación desde este lado:

| Exigencia | Estado al **2026-08-11**, leído sobre el archivo `.gitignore` del repositorio | Quién la aplica |
| --- | --- | --- |
| El control de versiones **excluye el bundle copiado** bajo `src/GeometriaFactory.Web/wwwroot/js/` | **No cumplido todavía.** El archivo no excluye esa ruta. La línea que excluiría el directorio de recursos estáticos entero **está comentada**, y así debe seguir: ese directorio también contiene recursos escritos a mano, y excluirlo entero sacaría del repositorio archivos que sí deben estar | **Esta categoría lo asigna a `BT-01`**, etapa `a` |
| El control de versiones **excluye el directorio de salida del empaquetado** en `visor/` | **No cumplido todavía.** El archivo no excluye esa ruta; **sí excluye** el directorio de dependencias del ecosistema del navegador | El mismo `BT-01`, en el mismo acto |
| El flujo de publicación **genera el bundle en su propio interior** | **Cumplido por diseño**: es el paso 4 de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1, y `QG-02` lo hace bloqueante | Esta categoría |
| Toda ejecución local que necesite el bundle **lo construye antes** | Declarado por el intake §17.7.P.8 para el guion propio del bundle | Quien ejecuta |

**Las dos primeras filas son una sola acción y esta categoría la cierra asignándola.** [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §2.2 la había dejado en «el equipo, en la etapa `a`, al poner en pie la cadena de construcción»; acá queda con dueño y con tarea: **`BT-01` de [`../06-Backlog-Tecnico/Backlog-Tecnico.md`](../06-Backlog-Tecnico/Backlog-Tecnico.md)**, «crear el proyecto del front con su flujo de publicación», que es la tarea de la etapa `a` que pone en pie este flujo. Queda registrada como `PD-02` en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10.

**Por qué la exclusión importa y no es higiene.** El intake §13 y §17.7.P.7 declaran que el bundle **nunca se edita a mano**; el `QG-09` de `GeometriaFactory-Visor` lo rechaza en revisión. Un archivo generado, versionado y visible en cada revisión es exactamente el que alguien corrige «en el acto» cuando tiene apuro, y además produce en cada cambio del fuente una diferencia ilegible que **inutiliza la revisión que tendría que detectarlo**.

**El estado se declara con su fecha de lectura**, para que la próxima categoría que lo verifique sepa contra qué estado se escribió este documento.

## 3. Provisión

**No hay infraestructura declarativa, y la ausencia es de la fuente y no de esta categoría.**

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Herramienta declarativa de infraestructura | **Ninguna.** El hosting es un servicio gratuito de terceros que se contrata y se configura por fuera del repositorio; no se provisiona con código | Intake §10, tres piezas de infraestructura de costo cero |
| Qué sí vive en el repositorio | **El flujo de publicación**, `.github/workflows/deploy-front-ftp.yml`, que el árbol del intake §16 declara. Es la única pieza de automatización de despliegue de este proyecto de código, y **está versionada** | Intake §16 |
| Dependencias de infraestructura en ejecución | **Una**: el servicio de datos, por dirección tomada de configuración. **Ninguna base de datos, ningún almacén de secretos propio y ningún servicio adicional** | `05` §5 |
| Estado del proceso | En memoria del servidor del hosting, y **se pierde en cada reciclado**. No hay volumen, no hay caché externa y no hay sesión persistente | Intake §17.6.P.4; [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) |

**La cuarta fila es la que hace posible a la primera.** Un front sin estado propio no necesita provisión: no hay volumen que crear, ni base que migrar, ni caché que calentar. El intake §17.6.P.4 lo declara sin rodeos —«el front no guarda estado propio: es exactamente el problema que la topología evita»— y esa decisión es la que permite que el despliegue entero sea copiar archivos y comprobar que responden.

## 4. Configuración

Configuración de doce factores: **fuera del código, en variables inyectadas al publicar**.

| Valor de configuración | De dónde sale | Quién lo conoce |
| --- | --- | --- |
| **Dirección del servicio de datos** | Del almacén de secretos del repositorio, inyectada en el paso 6 del flujo. **Nunca embebida en el código** | Únicamente el componente que hace de cliente tipado del servicio de datos. **Ningún otro componente la conoce y ninguna superficie la muestra** ([`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §7) |
| Credenciales del canal de publicación | Del mismo almacén, usadas en el paso 7 | El flujo de publicación |
| Configuración del bundle | **Ninguna, y es prohibición explícita.** `RA-02`: el bundle no lee configuración propia | [`../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md`](../../GeometriaFactory-Visor/09-Devops/Entornos-Deploy.md) §4 |
| Configuración que la persona pueda fijar | **Ninguna.** No hay superficies de configuración en el producto, y un parámetro que la superficie no gobierna **no se dibuja ni siquiera deshabilitado** | [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §4, tercera alternativa descartada |

**Sólo hay dos valores de configuración en todo este proyecto de código**, y los dos son secretos. No hay mapa de variables por ambiente porque **no hay dos ambientes con configuración distinta**: en desarrollo el front apunta al servicio de datos que corre en local, y en el hosting apunta al del servidor propio; es el mismo parámetro con otro valor.

**La última fila es una decisión de producto y no de esta categoría.** Ofrecer una pantalla donde el docente escriba la dirección del servidor propio habría puesto **la dirección de un servicio interno en el navegador**, que es lo que `RA-03` prohíbe. La categoría 03 la descartó por eso y [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §4 lo registra.

## 5. Secretos, y la dirección que no se versiona

| Secreto, nombrado por su función | Dónde vive | Quién lo usa | Rotación |
| --- | --- | --- | --- |
| **Dirección base del servicio de datos** | Almacén de secretos del repositorio. **La dirección real del servidor propio no se versiona** | El paso 6 del flujo, que la inyecta en la publicación | **Cuando la dirección cambia.** Ver más abajo |
| **Credenciales del canal de publicación** | El mismo almacén | El paso 7 del flujo | **No se declara ninguna frecuencia**: ninguna fuente la da, y un plazo en días inventado acá sería un compromiso sin dueño |

**Ningún secreto entra al repositorio, y ninguno de los dos aparece en este documento ni en ningún otro de esta cadena.** El intake §17.6.P.5 lo declara: la dirección y las credenciales viven como secretos del repositorio y se inyectan en la publicación, y **la dirección real del servidor propio no se versiona**. Esta categoría los nombra **por su función** y declara dónde vive el valor, que es todo lo que un documento de especificación puede decir de un secreto.

**Qué pasa cuando la dirección del servidor propio cambia**, que la topología hace probable —el intake §10 declara que el servidor domiciliario **no tiene dirección estática**, y que el Product Owner admite apuntar a la dirección directa con el servicio de nombres dinámico como recomendación—:

| Paso | Qué se hace | Fundamento |
| --- | --- | --- |
| 1 | **Nada se recompila y nada se revierte.** El código no la contiene | [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) §5, consecuencia positiva 2 |
| 2 | Se actualiza el valor del secreto | El mismo |
| 3 | Se vuelve a publicar el front, **con el flujo entero**, incluida la comprobación final del paso 8 | [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 |
| 4 | Se rehace la comprobación de `PT-01.d`: una llamada de salud que devuelva **datos reales** del servidor propio | Intake §17.6.P.10, fila `PT-01.d` |

**Cómo se entera alguien de que cambió, que es la parte incómoda.** Nada la detecta automáticamente: **ninguna fuente declara un vigilante, un servicio de monitoreo ni una alerta**, y esta categoría no inventa uno. Lo que sí está declarado es qué se ve cuando ocurre: el front entra en **estado degradado**, que es una superficie del producto ([`ADR-05`](../05-Arquitectura-Tecnica/Adrs/ADR-05-Estado-Degradado-Como-Superficie.md)), y **ese mensaje nunca incluye la dirección del servicio interno** (`QG-08`, `RA-03`). El síntoma es visible y el diagnóstico no se filtra: es exactamente el reparto que `RA-03` pide.

**Si el Product Owner adopta el servicio de nombres dinámico** —que la fuente declara como recomendación y no como decisión—, el valor del secreto pasa a ser un nombre estable y los cuatro pasos de arriba dejan de ejecutarse ante cada cambio de dirección. **Esta categoría no lo adopta por su cuenta**: la fuente registra la decisión del Product Owner de admitir la dirección directa, y cambiarla es suya.

## 6. Promoción

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Rama principal → publicación en el hosting | El flujo de publicación, disparado por fusión con cambios en las rutas del filtro, o a mano | El mismo, con el registro del flujo | El registro del flujo, con la respuesta de la dirección pública y **la hora** |
| **Cambio incompatible del contrato → etapa cerrada** | La constancia de que **las dos unidades desplegables salieron desde el mismo estado del repositorio** | El mismo, con constancia escrita | La constancia en el informe de cierre. Es el `QG-08` de `GeometriaFactory-Contracts`, que bloquea la **publicación de la etapa** |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, más `QG-04` y `QG-11` en verde |

**La tercera fila es la obligación que esta unidad recibe de otro proyecto de código**, y no la inventa esta categoría: el intake §17.4.P.3 declara que las dos unidades se despliegan juntas ante un cambio de contrato. Su tratamiento operativo entero, incluidas las tres decisiones derivadas y el hallazgo de que **el desfase de momentos es irreducible mientras un extremo se despliegue a mano**, está en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §3.2.

**La publicación se hace fuera del horario de uso.** No es una recomendación: el intake §17.6.P.8 lo declara como tratamiento de una subida **no transaccional** (`R-03`), y la Definition of Done §1.4 lo exige con la hora registrada.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara los **dos** ambientes que la fuente sostiene y registra el **apartamiento del modelo de cuatro** que `Rules-Devops.md` §2.2 fija para el tipo `web-monolith`, con tres fundamentos verificables —presupuesto cero, aprobador único y la comprobación final del flujo como sustituto del ensayo previo—, el ADR que lo sostiene y **lo que el apartamiento cuesta**, declarado en lugar de disimulado. Resuelve el tramo de este lado de la decisión de que **el bundle no se versiona**: verifica el estado del archivo de exclusión del repositorio **con su fecha de lectura**, declara que la línea que excluiría el directorio de recursos estáticos entero debe seguir comentada, y **asigna la acción pendiente a `BT-01` de la etapa `a`**, cerrando `PA-07` de `05` §11. Declara la ausencia de infraestructura declarativa con el front sin estado propio como su causa, los **dos** únicos valores de configuración —los dos secretos, nombrados por su función—, y el procedimiento de **cuatro pasos ante un cambio de la dirección del servidor propio**, con la constancia de que **nada lo detecta automáticamente** y de que el síntoma visible es el estado degradado, que no expone la dirección. |
