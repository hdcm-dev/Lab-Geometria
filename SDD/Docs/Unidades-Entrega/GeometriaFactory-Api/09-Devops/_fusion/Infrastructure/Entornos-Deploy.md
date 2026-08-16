# Entornos y canales — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Entornos-Deploy.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Ingeniero DevOps Senior + Release Engineer (AG-09)
**Tipo de proyecto de código (D8):** `library`
**Trazabilidad upstream:** [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/_fusion/Infrastructure/Arquitectura-Proyecto-Codigo.md) §5, §8 y §11; [`../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md); [`../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`../08-Calidad-Y-Pruebas/Estrategia-Calidad.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Calidad.md) 1.1 §3; [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **1.22** §10, §13, §14, §16, §17.1.P.3 · GeometriaFactory-Infrastructure, §17.1.P.4 · GeometriaFactory-Infrastructure, §17.1.P.5 · GeometriaFactory-Infrastructure, §17.1.P.7 · GeometriaFactory-Infrastructure y §17.1.P.9 · GeometriaFactory-Infrastructure
**Trazabilidad downstream:** [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md), [`Supply-Chain-Seguridad.md`](Supply-Chain-Seguridad.md); `Producto/Pipeline-Producto.md`

---

## Tabla de contenido

- [1. Ambientes y canales de este proyecto de código](#1-ambientes-y-canales-de-este-proyecto-de-código)
  - [1.1 Apartamiento declarado del modelo de la categoría](#11-apartamiento-declarado-del-modelo-de-la-categoría)
- [2. Dónde viaja este ensamblado](#2-dónde-viaja-este-ensamblado)
- [3. Provisión](#3-provisión)
- [4. Configuración, y el respaldo que no se fija acá](#4-configuración-y-el-respaldo-que-no-se-fija-acá)
- [5. Secretos: la clave de firma que se recibe y no se busca](#5-secretos-la-clave-de-firma-que-se-recibe-y-no-se-busca)
- [6. Promoción](#6-promoción)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Ambientes y canales de este proyecto de código

**Ninguno propio de despliegue y ninguno de publicación.** Las afirmaciones que lo sostienen son de la fuente:

| Afirmación | Dónde está declarada |
| --- | --- |
| No tiene unidad de despliegue propia: **viaja embebido en la unidad desplegable del servidor propio, por la vía de `GeometriaFactory-Api`** | `05` §5, primera fila |
| No se publica en ningún repositorio de paquetes: `redistribuible` es false | `05` §5, última fila; intake §13 |
| **Tres dependencias de infraestructura, y son las únicas**: el sistema de archivos donde vive el almacén, la fuente de material impredecible del sistema y la clave de firma provista desde afuera. **Ninguna es un servicio de red** | `05` §5, tercera fila |

| Ambiente o canal | Destino | Aprobador | Ventana o acuerdo de nivel de servicio |
| --- | --- | --- | --- |
| Contenedor de desarrollo | La máquina de quien construye, y el ejecutor del pipeline. Ahí corre la batería y el stage de verificación de transformaciones, **sobre almacenes desechables** | Nadie: no hay promoción hacia él | No aplica |

**La tercera fila de la primera tabla es la que hace corta a la segunda.** Este proyecto de código **no habla por red con nada**: el intake §17.1.P.3 · GeometriaFactory-Infrastructure lo declara —«No aplica: no expone endpoints. Consume el sistema de archivos donde vive el archivo […] y nada más», con la elisión del nombre del motor de almacenamiento marcada, por la convención del corpus de no nombrar stacks en prosa; y **el validador de figuras no hace red**—. Un ambiente se distingue de otro por qué servicios alcanza; acá no hay ninguno que alcanzar.

### 1.1 Apartamiento declarado del modelo de la categoría

`Rules-Devops.md` §2.2 fija para el tipo `library` el modelo de canales `preview` / `stable` sobre feed único, y admite apartarse con un ADR que lo justifique. **Acá no hay feed**: el intake §17.1.P.7 · GeometriaFactory-Infrastructure declara la estrategia idéntica a §17.1.P.7 · GeometriaFactory-Domain, sin publicación, y §13 lo generaliza al producto entero.

**Y falta el instrumento que la regla nombra, así que se declara en lugar de darse por cubierto.** Las otras tres bibliotecas del producto anclan este mismo apartamiento en su `ADR-06003`; **este proyecto de código no tiene ninguna ADR sobre publicación ni sobre canales** —sus siete, `ADR-06001` a `ADR-06007`, tratan adaptadores, almacén, comparación de correos, derivación de clave, contraseña provisoria, lectura tolerante y transformaciones—, de modo que la cita al intake **sustituye** al ADR que `Rules-Devops.md` §2.2 pide y no lo reemplaza formalmente. El apartamiento es sustantivamente correcto —no hay feed, y no lo hay por decisión del producto—; lo que falta es el instrumento. **Queda registrado como `PD-05`** en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, con la categoría 05 de este proyecto de código como dueña.

**Declarar acá un `DEV`, un `QA` y un `PROD` sería duplicar los ambientes de `GeometriaFactory-Api` con otro nombre y otro dueño**, que es el anti-patrón que `Rules-Devops.md` §4.8 nombra. El ambiente de ejecución donde este ensamblado termina es el del servidor propio, y su dueño es la categoría 09 de `GeometriaFactory-Api`.

**Y una precisión que este proyecto de código sí tiene y ninguna otra biblioteca del producto**: aunque no tenga ambientes propios, **es el que impone más restricciones sobre el ambiente ajeno**. El almacén va a un **volumen persistente y nunca dentro de la imagen**, el modo de diario está declarado, la concurrencia de escritura es de **escritor único** y las transformaciones se aplican **al arrancar** (intake §17.1.P.4 · GeometriaFactory-Infrastructure). Todo eso condiciona cómo se arma la unidad desplegable, y está recogido en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §9.

## 2. Dónde viaja este ensamblado

| Destino | Cómo llega | Quién es dueño de ese despliegue |
| --- | --- | --- |
| El proceso del **servidor propio** | Embebido en la imagen del backend, construida desde `deploy/Dockerfile` multietapa (intake §16), por la vía de `GeometriaFactory-Api` | Categoría 09 de `GeometriaFactory-Api` |
| El proceso del **hosting público** | **No llega.** El front no lo referencia, y no podría: `05` §5 declara que **nadie más que la composición de raíz de `GeometriaFactory-Api` lo referencia** | — |

**La segunda fila tiene una consecuencia de seguridad que conviene decir explícita.** Este ensamblado contiene la derivación de contraseñas y la emisión de accesos firmados. Que **no llegue al proceso del hosting** significa que esas dos piezas **nunca se despliegan en la máquina de terceros**: viven sólo en el servidor propio, que es donde vive el dato. Es una propiedad de la topología del intake §14 y no una decisión de esta categoría, pero es la que hace que un compromiso del hosting no exponga la capacidad de emitir accesos.

## 3. Provisión

**No hay infraestructura declarativa atribuible a este proyecto de código**: no provisiona servidor, red ni almacenamiento.

Lo que sí hace, y es lo que lo distingue, es **exigir tres cosas del ambiente que lo hospeda**, todas de `05` §5 y del intake §17.1.P.4 · GeometriaFactory-Infrastructure:

| Exigencia sobre el ambiente ajeno | Detalle | Quién la satisface |
| --- | --- | --- |
| Un **volumen persistente** donde viva el archivo del almacén, **nunca una ruta dentro de la imagen** | Si el archivo quedara dentro de la imagen, cada reemplazo de versión borraría el trabajo de la comisión | Categoría 09 de `GeometriaFactory-Api`, en el archivo de composición |
| La **ubicación del almacén tomada de configuración**, provista por `GeometriaFactory-Api` | Este proyecto de código **no la busca**: la recibe | La misma |
| La **clave de firma provista desde afuera**, por variable de entorno o archivo montado | Ver §5 | La misma |

**Las tres son restricciones y no provisiones.** Este proyecto de código no crea el volumen ni escribe el archivo de composición; lo que hace es **fallar de manera declarada si alguna de las tres no está**, que es preferible a arreglárselas solo. `QG-12` lo mide en la tercera: **0** emisiones de acceso sin clave de firma y **0** claves generadas al vuelo.

## 4. Configuración, y el respaldo que no se fija acá

| Aspecto | Decisión | Fundamento |
| --- | --- | --- |
| Ubicación del almacén | **Configurable, y la configuración la provee `GeometriaFactory-Api`.** En producción, un volumen persistente | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `05` §5 |
| Modo de diario | **Declarado por la fuente**, no elegido acá | Intake §17.1.P.4 · GeometriaFactory-Infrastructure |
| Concurrencia de escritura | **Escritor único.** No es una configuración: es una propiedad del motor que el producto acepta como trade-off | Intake §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.12 · GeometriaFactory-Infrastructure; [`ADR-06002`](../../../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) |
| Versionado del esquema | **Transformaciones aplicadas automáticamente al arrancar**, sobre almacén inexistente o desactualizado | Intake §17.1.P.4 · GeometriaFactory-Infrastructure y §17.1.P.11 · GeometriaFactory-Infrastructure punto 3 |
| Multi-inquilino | **No.** Una instancia, un curso, un administrador | Intake §17.1.P.4 · GeometriaFactory-Infrastructure; `INV-05` |
| Variables de entorno del pipeline | **Ninguna.** Los cuatro stages leen el repositorio, crean almacenes desechables y escriben informes y recuentos | Decisión de esta categoría, derivada de [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §2.1 |

**El respaldo del almacén, que es lo único de operación que la fuente dejó abierto y dirigió acá.** El intake §17.1.P.4 · GeometriaFactory-Infrastructure lo declara como **copia del archivo con el diario activo, consistente**, y su **frecuencia «a definir por el docente»**; `PA-07` de `05` §11 lo registra como punto abierto y lo dirige a esta categoría junto con el Product Owner.

**Esta categoría no inventa una frecuencia.** Lo que sí aporta, porque le corresponde, es qué condiciones tiene que cumplir el respaldo para servir de algo:

| Condición | Fundamento |
| --- | --- |
| Se copia **el archivo con el diario activo**, y la copia es consistente. No se copia el archivo a mano mientras el proceso escribe | Intake §17.1.P.4 · GeometriaFactory-Infrastructure |
| **El respaldo es el único mecanismo del producto para volver atrás sobre datos.** Volver a una etiqueta revierte el código y no el almacén, y el guion de restablecimiento **deja el almacén vacío** | `05` §5; [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §7 |
| La copia **vive fuera del volumen que respalda**, o no protege del modo de falla más probable de un servidor domiciliario | **Decisión de esta categoría**, declarada como tal |
| **No se declara ninguna frecuencia, ninguna retención y ningún destino concreto** | Ninguna fuente los da, y el intake §10 declara «sin plazo». Un número puesto acá se propagaría como si fuera del producto |

**El punto abierto queda registrado como `PD-04`** en [`Pipeline-CI-CD.md`](Pipeline-CI-CD.md) §10, con el Product Owner como quien lo cierra.

## 5. Secretos: la clave de firma que se recibe y no se busca

**Acá viven las dos piezas sensibles del producto** —la derivación de la contraseña y la emisión del acceso firmado (intake §17.1.P.5 · GeometriaFactory-Infrastructure)— y sin embargo **este proyecto de código no custodia ningún secreto**.

| Secreto, nombrado por su función | Dónde vive | Cómo llega | Qué pasa si no llega |
| --- | --- | --- | --- |
| **Clave de firma del acceso** | **Fuera del repositorio y fuera de la imagen**: variable de entorno o archivo montado. Se genera o se provee en el primer arranque | La provee `GeometriaFactory-Api` desde la configuración del ambiente | **Falla con la condición declarada.** `05` §5: este proyecto de código **la recibe y no la busca**; `QG-12` mide **0** claves generadas al vuelo |

**La cuarta columna es la decisión de diseño que esta categoría subraya.** Un adaptador que, ante la falta de la clave, generara una al vuelo **arrancaría bien y emitiría accesos que nadie más puede verificar**: el producto funcionaría hasta el primer reinicio y después dejaría de reconocer sus propios accesos, sin ningún error visible en el momento de la falla. Por eso la ausencia es una condición declarada y no un valor por defecto.

| Momento | Secretos | Fundamento |
| --- | --- | --- |
| Construcción | **Ninguno.** El restaurador toma dependencias de repositorios públicos; no hay publicación que autenticar | Intake §17.1.P.7 · GeometriaFactory-Infrastructure |
| Prueba | **Ninguno real.** Las contraseñas de los casos son ficticias, y los almacenes son desechables | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../../../08-Calidad-Y-Pruebas/Estrategia-Testing.md) |
| Ejecución | **Uno, recibido y no custodiado**: la clave de firma | Intake §17.1.P.5 · GeometriaFactory-Infrastructure; `05` §5 |

**Ningún secreto entra al repositorio, ni en la integración continua.** El intake §17.1.P.5 · GeometriaFactory-Infrastructure lo declara sin excepción. **No se declara ninguna frecuencia de rotación**: ninguna fuente la da, y el gobierno del valor pertenece a la categoría 09 de `GeometriaFactory-Api`, que es la que lo provee al ambiente.

**Y una regla de higiene que alcanza al pipeline y no sólo al producto**: `QG-13` mide **0** mensajes o trazas con un secreto, la ruta del almacén o el texto del alumno. Eso incluye la salida de los cuatro stages: un registro de ejecución que imprimiera la ruta del almacén desechable o un fragmento de un escenario estaría produciendo, en la canalización, lo que el gate prohíbe en el producto.

## 6. Promoción

| Transición | Trigger | Aprobador | Registro |
| --- | --- | --- | --- |
| Rama de etapa → rama principal | Fusión del pull request | Product Owner, con OK explícito | Informe de cierre (intake §15) |
| Etapa fusionada → etapa cerrada | Etiqueta al fusionar | El mismo | La etiqueta, más el registro del linaje de transformaciones aplicado |

**Ninguna transición de este proyecto de código alcanza a un acto de despliegue.** Lo que se despliega es la unidad que lo embebe, y su promoción la gobierna la categoría 09 de `GeometriaFactory-Api`.

**Pero una de sus obligaciones sí sobrevive a la promoción**, y es la única del producto que lo hace: **el linaje de transformaciones**. Una etapa cerrada deja aplicado en todo almacén existente un linaje que ninguna etiqueta posterior deshace. Por eso el registro de la segunda fila no es ceremonia: es el único rastro de qué esquema quedó en el almacén de la comisión.

## 7. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial. Declara que este proyecto de código **no tiene ambientes ni canales propios** y registra el apartamiento del modelo `preview` / `stable`, con la precisión de que **es el que más restricciones impone sobre el ambiente ajeno** aunque no tenga uno propio. Reemplaza la tabla de ambientes por la de **dónde viaja el ensamblado**, con la consecuencia de seguridad de que la derivación de contraseñas y la emisión de accesos **nunca se despliegan en la máquina de terceros**. Declara las **tres** exigencias sobre el ambiente que lo hospeda como restricciones y no como provisiones. Trata el **respaldo del almacén** que la fuente dejó abierto: **no inventa frecuencia, retención ni destino**, y aporta las condiciones que el respaldo debe cumplir, incluida una decisión propia declarada como tal —la copia vive fuera del volumen que respalda—. Declara el único secreto **recibido y no custodiado**, con el fundamento de por qué generar una clave al vuelo sería peor que fallar, y una regla de higiene que alcanza a la salida del pipeline. |
| 1.1 | 2026-08-11 | **Corrección de dos hallazgos de la auditoría `F-09-Devops-Siete-Proyectos-r1.md`.** **`H-02` (P3):** la cita del intake §17.1.P.3 · GeometriaFactory-Infrastructure en §1 fundía dos oraciones con punto y coma y suprimía sin marca el nombre del motor de almacenamiento; se transcribe literal con la **elisión marcada** y el motivo de la elisión declarado. **`H-04` (P3):** §1.1 apartaba el modelo de canales de `Rules-Devops.md` §2.2 reconociendo que la regla pide un ADR y sustituyéndolo por una cita al intake; se declara expresamente que **ninguna de las siete ADR de este proyecto de código cubre publicación ni canales** y se registra la ausencia como **`PD-05`** con dueño, en lugar de darla por cubierta. Trazabilidad upstream del intake a **1.22**, cuya §17.2.P.7 · GeometriaFactory-Web no alcanza a este documento y cuyas §17.3.x no cambiaron. |
