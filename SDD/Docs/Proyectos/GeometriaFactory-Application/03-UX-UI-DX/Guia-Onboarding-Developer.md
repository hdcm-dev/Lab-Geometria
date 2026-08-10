# Guía de onboarding — la primera hora contra los casos de uso y los puertos

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** `02-Especificacion-Funcional/Especificacion-Funcional.md` §1, §3, §4, §6, §7.4, §8 y §11; CU-01 §4, §5, §6, §8 y §10; CU-02 §5, §6 y §10; CU-03 §5, §6 y §10; CU-04 §4, §5, §6, §8 y §10; CU-05 §4, §5, §6, §8 y §10; CU-06 §4, §6, §8 y §10; CU-07 §4, §5, §6 y §10; CU-08 §5, §6 y §10; CU-09 §4, §5, §6 y §10; CU-10 §1, §6 y §10; **CU-11 §1, §4, §5, §6 y §10**; `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §3; RN-01 a RN-11 de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`, más **RN-12** y **RN-13** del `PRODUCT-INTAKE` **1.7** §4.1 y el invariante **INV-09** de su §17.1.P.2; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `00-Contexto/Alcance-Producto.md` §4.4; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §17.2.P.1, §17.2.P.2, §17.2.P.4, §17.2.P.5, §17.2.P.6, §17.2.P.8, §17.2.P.10, §17.2.P.11, §17.2.P.12, §4.1, §4.2 y §16
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Audiencia y prerrequisitos](#1-audiencia-y-prerrequisitos)
- [2. Instalación o acceso](#2-instalación-o-acceso)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 El resultado que se busca](#31-el-resultado-que-se-busca)
  - [3.2 Los pasos](#32-los-pasos)
  - [3.3 El primer ejemplo con sentido de aplicación](#33-el-primer-ejemplo-con-sentido-de-aplicación)
  - [3.4 Leer una negativa](#34-leer-una-negativa)
  - [3.5 Las tres negativas, en diez minutos](#35-las-tres-negativas-en-diez-minutos)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Control de cambios](#6-control-de-cambios)
- [7. La inversión de dependencias, en la práctica](#7-la-inversión-de-dependencias-en-la-práctica)
  - [7.1 Los cuatro puertos y qué le pide esta capa a cada uno](#71-los-cuatro-puertos-y-qué-le-pide-esta-capa-a-cada-uno)
  - [7.2 Por qué el reloj es un puerto](#72-por-qué-el-reloj-es-un-puerto)
  - [7.3 Dónde va algo nuevo](#73-dónde-va-algo-nuevo)

---

## 1. Audiencia y prerrequisitos

Esta guía está escrita para tres lectores concretos, y no para un integrador hipotético:

- **El mantenedor que vuelve sobre este proyecto de código sin el contexto de la etapa en que lo escribió.** Es el caso más frecuente en un equipo de una persona.
- **El agente de IA que construye por etapas** y que necesita, en cada arranque, reconstruir por qué una negativa existe antes de tocarla.
- **Quien escribe una de las dos capas vecinas**: `GeometriaFactory-Api`, que invoca los casos de uso, o `GeometriaFactory-Infrastructure`, que implementa los puertos. Son los únicos consumidores de esta superficie pública, y no hay integradores externos.

Prerrequisitos:

| Prerrequisito | Detalle |
| --- | --- |
| El repositorio abierto en el entorno de desarrollo contenido del propio repositorio | Todo el ciclo ocurre adentro. El host no tiene las herramientas y no va a tenerlas (`Alcance-Producto.md` §4.4) |
| Nada más | Sin base de datos, sin red, sin servicio levantado y sin credencial de acceso. La dependencia core única de esta capa es `GeometriaFactory-Domain` (`PRODUCT-INTAKE` §17.2.P.1) y su persistencia está declarada como «no aplica directamente» (§17.2.P.4) |
| Haber leído dos secciones de la especificación funcional | `Especificacion-Funcional.md` §3, la tabla de los cuatro puertos, y §4, las **cuatro** comprobaciones. Sin ellas los once casos de uso se leen mal, porque los dos rasgos que los recorren están enunciados una sola vez ahí |

Conocimiento previo que **no** se supone: el estilo de casos de uso con inversión de dependencias. Es lo que la §7 de esta guía enseña, y es lo que hay que entender antes de tocar nada, porque quien no lo entienda va a intentar consultar datos desde acá.

Vocabulario mínimo para no perderse en la primera media hora. Los términos están definidos en `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz, en [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) y en [`Glosario-UX.md`](Glosario-UX.md); acá sólo se enumeran para que el lector sepa qué buscar: puerto, doble, motivo, unidad de trabajo, alcance de consulta, verificación de pertenencia, verificación de facultad, camino de alta, metadato de orquestación, cantidad de figuras del conjunto raíz, trabajo, pieza, observación, advertencia, error de validación, texto original, desenlace y comentario.

Tres convenciones que conviene conocer desde el primer minuto porque se cruzan en cada documento:

- **`Pendiente` va siempre calificado**, «cuenta `Pendiente`» o «trabajo en estado `Pendiente`», porque nombra dos estados distintos (`Vision-Producto.md` §9.2).
- **«Repositorio» a secas no se escribe acá.** Es la polisemia propia de esta capa: se dice «puerto de repositorio de trabajos» o «puerto de repositorio de cuentas», y «repositorio de código» para el árbol del producto (`Glosario-Funcional.md` §3.1).
- **«Trabajo» es lo que carga el alumno; el tramo transaccional es siempre «unidad de trabajo»**, en su forma compuesta completa, incluso cuando el contexto parecería bastar (`Glosario-Funcional.md` §3.5).

## 2. Instalación o acceso

No hay instalación: este proyecto de código no se publica en ningún feed y se compila dentro de `GeometriaFactory.sln` (`PRODUCT-INTAKE` §17.2.P.7). El acceso es abrir el repositorio.

```bash
# Dentro del entorno de desarrollo contenido, que el propio repositorio
# define en `.devcontainer/`.

# 1. Script de construcción de la solución de código.
#    Criterio de éxito: termina en 0 y sin advertencias.
./scripts/build.sh

# 2. Script de pruebas del repositorio.
#    Criterio de éxito: pasa entero.
./scripts/test.sh
```

Verificable en un vistazo: si el paso 1 termina en 0 y sin advertencias y el paso 2 pasa entero, el prerrequisito está cumplido y se pasa a §3. Si alguno falla, la §4 tiene los arranques que fallan de verdad.

## 3. Primer ejemplo ejecutable

### 3.1 El resultado que se busca

El primer resultado exitoso de este proyecto de código es **la batería de la capa de aplicación en verde, sin haber preparado nada externo**. Vale la pena detenerse en por qué eso es posible: las pruebas son unitarias con **dobles** —repositorio simulado, validador doble, reloj fijado—, y no hay base de datos que preparar, ni servicio que levantar, ni credencial de acceso que configurar.

Eso no es una comodidad: es la propiedad que justifica el diseño entero. Se renunció a consultar la base con proyecciones ad-hoc desde el caso de uso, y lo que se compró con esa renuncia fue poder ejercer cada contrato de punta a punta con dobles (`PRODUCT-INTAKE` §17.2.P.12). La puerta de calidad propia y bloqueante lo dice sin rodeos: **ninguna prueba de esta capa toca la base de datos real; si una lo hace, está mal ubicada y pertenece a integración** (§17.2.P.8).

### 3.2 Los pasos

```bash
# 3. Comando de prueba del ecosistema, acotado al proyecto de prueba de este
#    proyecto de código. Criterio de éxito: verde, y sin haber preparado
#    ninguna base de datos, ningún servicio y ninguna credencial de acceso.
dotnet test tests/GeometriaFactory.Application.Tests
```

Los pasos se nombran por su papel —entorno de desarrollo contenido, script de construcción, script de pruebas, comando de prueba del ecosistema— y conservan su forma literal porque se tienen que poder ejecutar. Las rutas y el nombre del proyecto de prueba salen de `PRODUCT-INTAKE` §16 y §17.2.P.6, y no se eligen acá.

### 3.3 El primer ejemplo con sentido de aplicación

El segundo resultado, que es el que explica la capa, es **ver un caso de uso entero resolviendo con dobles**. El más ilustrativo es el más pesado: CU-05, el envío. Se transcribe su criterio de aceptación CA-01 tal como el caso de uso lo declara:

| Given | When | Then |
| --- | --- | --- |
| Un trabajo en `Borrador` del alumno A con el texto semilla de 3 piezas —cilindro, cubo y ortoedro—, y un validador doble que devuelve 3 piezas y 2 advertencias: área declarada 36.00 contra derivada 54.00 en el cubo, y volumen declarado 343.00 contra derivado 1029.00 en el ortoedro | El alumno A envía el trabajo | El caso de uso devuelve el trabajo en estado `Pendiente` con 3 piezas y 2 advertencias, y ninguna de las dos lo bloquea |

Tres cosas ocurrieron ahí, y las tres son la capa entera en miniatura:

1. **El texto no se interpretó acá.** El caso de uso se lo entregó al puerto de validación de figuras y recibió la cantidad de figuras del conjunto raíz, las piezas reconstruidas y las observaciones. Por eso un doble alcanza para ejercerlo: la lógica de tolerancia del formato vive en la implementación, en `GeometriaFactory-Infrastructure`.
2. **El estado no lo decidió el caso de uso.** Le entregó al dominio el conjunto de observaciones y el dominio resolvió: sin errores de validación pasa a estado `Pendiente`, con errores queda en `Borrador` (RN-05). Las advertencias señalan y no bloquean, y eso es deliberado.
3. **Nada tocó una base de datos.** Ni siquiera el sello: el sello de modificación salió del puerto de reloj, fijado por la prueba.

Y el contraste, que es CA-05 del mismo caso de uso y el ejemplo canónico de la comprobación de pertenencia:

| Given | When | Then |
| --- | --- | --- |
| Un trabajo en `Borrador` del alumno A | El alumno B lo envía | El caso de uso devuelve el motivo `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` y **el validador doble registra 0 invocaciones** |

Ese cero es lo que hay que mirar: la comprobación ocurrió **antes** de invocar al validador y antes de escribir nada. La pertenencia se verifica sobre el dato recuperado, no sobre lo que declara la petición (CU-04 §10).

**Los nombres de tipos y de espacios de nombres son un punto abierto declarado**, que se resuelve en `05-Arquitectura-Tecnica` y se valida en el punto de control de la etapa `a` (`Especificacion-Funcional.md` §11). Por eso acá los conceptos se nombran en lenguaje de dominio y no se propone ninguna firma: la que valga la va a fijar 05, y esta guía la va a citar entonces.

### 3.4 Leer una negativa

Con el motivo a la vista, el recorrido de lectura es siempre el mismo, y es lo que hay que automatizar en la cabeza:

1. **El motivo** dice qué comprobación se negó: `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`.
2. **El catálogo** ([`DX-Error-Messages.md`](DX-Error-Messages.md)) dice qué pasó, por qué pasó y qué hacer —y de qué lado hacerlo, que acá no siempre es el del consumidor.
3. **El caso de uso** que lo declara (§6 de CU-04, CU-05, CU-06 o CU-09) dice cuál es la respuesta: no procede, sin escritura y sin invocar al validador.
4. **La regla de negocio** (§9 del mismo caso de uso) dice por qué la comprobación existe: RN-03, que vive en `GeometriaFactory-Domain` y que esta capa **ejerce** sin volver a enunciarla.

Cuatro saltos, todos con enlace. Si en alguna negativa la cadena se corta, es un defecto de esta sección.

Y la observación que le da sentido al ejercicio: **el caso de uso no arregló nada.** No consultó de más, no reintentó y no corrigió el pedido. Devolvió el motivo y dejó el repositorio como estaba. Quien tiene que decidir qué hacer —y sobre todo **cómo traducirlo hacia afuera**— es el consumidor.

### 3.5 Las tres negativas, en diez minutos

Es el tramo que más rinde de la primera media hora, y el que se evalúa en el tramo de 30 minutos de [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §2. La tabla completa está en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.4; lo que hay que retener es esto:

| Se preguntó por | Negativa | ¿Oculta que el recurso existe? | Se traduce a |
| --- | --- | --- | --- |
| Un recurso que puede ser de otra persona | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | **Sí, deliberadamente** | «No encontrado», **nunca** «no autorizado» |
| Una facultad | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | No, y no tiene por qué | Un mensaje explícito |
| Un recurso fuera del alcance del papel | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | No | Un mensaje explícito |

La frase que resume todo y conviene poder recitar: **el papel no reemplaza a la pertenencia, y la pertenencia no se confiesa.**

Por qué importa tanto: confirmar que un trabajo ajeno existe permite averiguar por tanteo qué identificadores existen. Por eso el trabajo ajeno y el identificador inexistente comparten motivo **por diseño**, y por eso hay dos criterios de aceptación —CA-03 de CU-06 y CA-03 de CU-09— cuyo único propósito es verificar que los dos motivos son el mismo.

Un cuarto caso, de la misma familia y fácil de olvidar: **la cuenta inexistente en la consulta de admisibilidad tampoco se distingue hacia afuera**, para no revelar qué correos están registrados (CU-03 §6 y §10, CA-05).

Dos distinciones más que conviene fijar antes de seguir, porque confundirlas es el otro error caro de esta sección:

| No son lo mismo | Diferencia | Dónde está declarado |
| --- | --- | --- |
| Condición de error y observación | La **condición de error** es una comprobación que impide una operación y no se guarda en ninguna parte. La **observación** es una entidad del dominio, con dos especies, que el validador produce al interpretar el texto del alumno y que el caso de uso incorpora al trabajo. Un trabajo que vuelve en `Borrador` por un error de validación **no produjo ninguna condición de error** | [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.2; [`Glosario-UX.md`](Glosario-UX.md) §3.1 |
| Observación y comentario | La observación la emite el producto y hay tantas como defectos; el **comentario** lo escribe el administrador, hay a lo sumo uno por trabajo y **no es una observación ni una calificación** | `Vision-Producto.md` §9.1; CU-08 §10 |

## 4. Diagnóstico de problemas frecuentes en la primera hora

| Síntoma | Causa probable | Qué hacer |
| --- | --- | --- |
| Un comando del quick-start no existe en el host | Se está ejecutando fuera del entorno de desarrollo contenido. El host no tiene las herramientas y no va a tenerlas | Abrir el repositorio de código en el entorno contenido que él mismo define y repetir desde el paso 0 |
| `./scripts/build.sh` termina en 0 pero con advertencias | La puerta de calidad exige 0 **y sin advertencias** para fusionar (§17.2.P.8) | Tratar la advertencia como bloqueante: no es un aviso, es un criterio de la etapa |
| Una prueba de esta capa necesita preparar una base de datos | La prueba está mal ubicada. La puerta de calidad propia de la capa es que **ninguna prueba de acá toca la base de datos real** (§17.2.P.8) | Moverla a `tests/GeometriaFactory.Integration.Tests`, que pertenece a la Api, o revisar por qué el caso de uso no pasa por un puerto |
| Se busca dónde el caso de uso arma la consulta | No la arma. Le pide al puerto una consulta **ya acotada** por dueño o por alcance, y el cómo vive en `GeometriaFactory-Infrastructure` | `Especificacion-Funcional.md` §3. Si aparece la tentación de traer todo y filtrar en memoria, ver la fila siguiente |
| Se filtra en memoria el resultado de una consulta | Es el patrón que la separación entre alumnos viene a impedir: da el mismo resultado visible y deja de ser una garantía | Trasladar el recorte al pedido (CU-06 §10, CU-07 §10). Un borrador que llega a esta capa y se descarta acá **ya viajó** |
| Se busca dónde se guarda el trabajo | Acá no se guarda: se **entrega** al puerto de repositorio dentro de una única unidad de trabajo. La persistencia está declarada como «no aplica directamente» (§17.2.P.4) | Buscar en `GeometriaFactory-Infrastructure`. El alcance transaccional es un caso de uso, una unidad de trabajo |
| Se busca dónde se interpreta el texto del alumno | No está acá. La interpretación y el cálculo del valor derivado los hace el validador de figuras, detrás del puerto de validación de figuras | CU-05 §10. El caso de uso **entrega** el texto y **recibe** piezas y observaciones |
| Se busca la comparación de contraseñas o la emisión del acceso | Esta capa no autentica: **autoriza**. El valor de credencial llega ya derivado y el valor en claro nunca la atraviesa | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.3, la tabla de la frontera |
| Se busca el identificador del puerto de repositorio de cuentas y no aparece en el intake | Es correcto: el intake nombra tres puertos y no éste. Está declarado como **punto abierto** y esta sección no lo reabre | `Especificacion-Funcional.md` §3 y §11. Hasta que 05 lo fije, se lo nombra en lenguaje de dominio |
| Se busca el nombre exacto de un tipo o de un espacio de nombres | Es el otro punto abierto declarado: se fija en 05 y se valida en el punto de control de la etapa `a` | `PRODUCT-INTAKE` §17.2.P.11; `Especificacion-Funcional.md` §11 |
| Un envío devuelve el trabajo en `Borrador` y se lo interpreta como error | No es un error y **no produjo ninguna condición del catálogo**: es el resultado declarado del envío cuando hay al menos una observación de especie error de validación (CU-05 FA-01). Las advertencias no lo impiden | No traducirlo a fallo hacia afuera: el alumno corrige por CU-04 y vuelve a enviar cuantas veces haga falta |
| Un envío devuelve `INTERPRETACION_NO_DISPONIBLE` y se lo trata como dato inválido | No lo es: el puerto de validación no pudo completar la interpretación. El trabajo queda en `Borrador` con su texto intacto y el estado es **degradado**, no interpretado | Informar que la interpretación no está disponible. **No inventar observaciones y no pasar a estado `Pendiente`.** Esta capa no reintenta |
| Un envío devuelve `OBSERVACION_MAL_FORMADA` y se busca qué corrigió mal el alumno | No es del alumno ni del consumidor: es un **defecto del adaptador del validador**, que devolvió un conjunto que el contrato no admite | Corregir el adaptador en `GeometriaFactory-Infrastructure`. Un conjunto mal formado no es un resultado que el alumno deba ver (CU-05 §6) |
| Una reedición rebota con `OPERACION_FUERA_DE_BORRADOR` y se lo confunde con la negativa de pertenencia | Son motivos distintos a propósito: acá **la existencia del trabajo ya está admitida para su dueño**, y lo que se niega es la operación, no el acceso | CU-04 FA-03. **Ver** un trabajo propio sí procede en los cuatro estados; lo que se acota al borrador es operarlo |
| Se busca cómo corregir un trabajo `Rechazado` y todo rebota | `Rechazado` es terminal por decisión aceptada por escrito. Corregir un rechazo significa cargar un trabajo nuevo | CU-08 FA-03. El rechazado queda como registro del intento y sólo el administrador puede quitarlo, por CU-09 |
| Un alta rechaza con `CORREO_YA_REGISTRADO` aunque la consulta previa dijo que estaba libre | Es el camino declarado de CU-01 FA-03: **la verificación previa no es una garantía por sí sola**, y la unicidad efectiva la sostiene también la capa que guarda | No materializar nada y devolver el motivo. Sin informar el estado ni el papel de la cuenta que ocupa el correo |
| Dos correos que parecen el mismo se tratan como distintos | El criterio con el que dos correos se consideran el mismo es un **punto abierto declarado y no bloqueante**, que viene del dominio y esta categoría no reabre | `Especificacion-Funcional.md` §11. Lo resuelve 05 junto con la capa que ejerce la verificación |
| Se busca dónde se enuncia una regla de negocio y no está | Las **trece** reglas viven en `GeometriaFactory-Domain` y acá se **ejercen**, no se redactan. **Las trece tienen archivo allá**, incluidas RN-12 y RN-13, que entraron con el `PRODUCT-INTAKE` 1.7 | `Especificacion-Funcional.md` §6 dice, regla por regla, dónde se ejerce cada una en esta capa |
| Se busca en CU-01 el alta del administrador y no está | Son **dos caminos de alta con reglas opuestas** —estado inicial, credencial y ventana de alta—, y por eso son dos contratos: el auto-registro del alumno es CU-01 y la configuración del administrador es CU-10 | [`DX-Error-Messages.md`](DX-Error-Messages.md) §1.4, con la tabla de los cinco rasgos opuestos |
| Un alta rechaza con `ESTADO_INICIAL_NO_NEGOCIABLE` y la causa parece contradecir a la del otro camino | No se contradicen: el enunciado es «el estado inicial de **este** camino no se elige», y cuál es ese estado lo fija el camino. `Pendiente` en el auto-registro, `Habilitado` en la configuración del administrador | Es el único motivo con fila completa en dos subsecciones del catálogo, con remisión mutua ([`DX-Error-Messages.md`](DX-Error-Messages.md) §3.1 y §3.10) |
| Se busca la cantidad de figuras del conjunto raíz y se la intenta derivar contando piezas | **No es derivable**: el conjunto de piezas adoptadas admite huecos, porque la posición de una figura no reconstruida queda reservada. La produce el validador al interpretar | §7.1 de esta guía. Sin ese dato el dominio no tiene rango contra el cual validar la posición de una observación |
| Un envío rechaza con `CONJUNTO_DE_PIEZAS_MAL_FORMADO` o con `OBSERVACION_MAL_FORMADA` y se busca el motivo fino | Son **condiciones agregadas**: cada una reúne cuatro rechazos del dominio, y esta capa emite uno solo porque ninguno de los ocho es un resultado que el alumno deba ver | El motivo fino está en la 02 de `GeometriaFactory-Domain`. La agregación está declarada en [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.5 |
| Se busca en el catálogo un rechazo que el dominio declara y no aparece | Puede ser **inalcanzable por construcción**, equivalente a otro de esta capa, o estar agregado. Ninguna de las tres es un olvido, y las tres están declaradas | [`DX-Error-Messages.md`](DX-Error-Messages.md) §2.5, con las dieciséis filas y su lugar de declaración en la 02 |

## 5. Próximos pasos

Los cuatro modos de documentación, con el orden en que conviene visitarlos después de esta guía. El plan completo está en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §4.

| Modo | Ir a | Cuándo |
| --- | --- | --- |
| Tutorial | Esta guía | Es la que se acaba de recorrer. No hay otra |
| How-to | [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/), los once contratos de uso | Cuando hay que invocar una operación concreta y saber qué resolver antes |
| Reference | [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3 y §4 para los puertos y las comprobaciones; [`DX-Error-Messages.md`](DX-Error-Messages.md) para las condiciones de error; [`Glosario-UX.md`](Glosario-UX.md) y [`../02-Especificacion-Funcional/Glosario-Funcional.md`](../02-Especificacion-Funcional/Glosario-Funcional.md) para el vocabulario | Cuando hace falta un dato puntual |
| Explanation | [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.2, §1.3 y §1.4; `Especificacion-Funcional.md` §1, §4 y §8; la §7 de esta guía | Cuando la pregunta es «por qué está así» |

Punto de entrada recomendado de la sección de especificación funcional: su [`README.md`](../02-Especificacion-Funcional/README.md) propone un orden de lectura que este onboarding no duplica. Y para el lector que llega desde el dominio, la tabla de §7.4 del índice de 02 dice qué caso de uso de `GeometriaFactory-Domain` orquesta cada uno de los once de acá.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial (ver fila siguiente para las correcciones absorbidas). Recorrido de la primera hora para el mantenedor, para el agente de IA y para quien escribe una de las dos capas vecinas, sin integradores externos. Declara los prerrequisitos reducidos a abrir el repositorio en el entorno de desarrollo contenido más dos secciones de lectura previa, el primer resultado exitoso como la batería de la capa en verde sin preparar nada externo, el primer ejemplo con sentido de aplicación tomado de CA-01 y CA-05 de CU-05 con sus tres lecciones, el recorrido de cuatro saltos para leer una negativa, el resumen de las tres negativas de autorización con la regla de que la pertenencia no se confiesa, las dos distinciones que separan condición de error, observación y comentario, dieciocho diagnósticos de la primera hora y el enlace explícito a los cuatro modos de Diátaxis. Las seis secciones obligatorias de `Rules-UX-UI-DX.md` §4.2.4 conservan su numeración 1 a 6; el contenido propio sobre la inversión de dependencias va al final como §7 y no desplaza a ninguna. Cita los tres puntos abiertos declarados por 02 —el identificador del puerto de repositorio de cuentas, los nombres de tipos y el criterio de comparación de correos— sin reabrir ninguno. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **Alineación con el 02 corregido**: los recuentos pasan a **diez casos de uso**; §7.1 actualiza la tabla de puertos —el repositorio de cuentas suma CU-10 y la pregunta por el administrador existente, el validador suma la **cantidad de figuras del conjunto raíz**, el reloj pasa a hablar de **sellos** y **pierde CU-02**, con el motivo declarado— y suma los dos datos que viajan por los puertos y se malinterpretan. §1 amplía el vocabulario mínimo con «camino de alta», «metadato de orquestación» y «cantidad de figuras del conjunto raíz». §3.3 incorpora la cantidad de figuras al primer ejemplo. §4 pasa de dieciocho a **veintitrés diagnósticos**, con los dos caminos de alta, el motivo con causas opuestas, la cantidad de figuras que no se deriva, las dos condiciones agregadas y los rechazos del dominio que acá no ocurren. **H-11**: la primera fila de §4 califica «repositorio de código», que es la única sección donde los dos referentes conviven. Suma un cuarto punto abierto citado y no reabierto: los sellos, que el modelo del dominio no declara como atributos. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-16**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. Se retiran dos residuos de la nomenclatura anterior a los **sellos**. El más grave está en §7.2: **transcribía el criterio CA-01 de CU-01 como «fecha de alta 2026-03-15» cuando el criterio dice «sello de alta»**, y una transcripción que no coincide con el original es peor que una paráfrasis porque el lector la toma por literal; pasa a citarse sin comillas de transcripción y con el término vigente. El otro está en §3.3, tercera lección del primer ejemplo, que atribuía al reloj una «fecha de modificación». Se alinean además la consecuencia práctica de §7.2 y la cita del intake §17.2.P.11 punto 3, que conserva su redacción literal —«las fechas de alta y de modificación»— con la aclaración de que es lo que esta capa llama sellos. **No se tocó** ninguna mención a la fecha de alta del alumno ni a la «Fecha» que el alumno declara en su trabajo: las dos son atributos que el modelo del dominio sí declara. En la misma operación, la cabecera de trazabilidad suma **CU-10**, que el cuerpo ya citaba en §4 y en §7.1 desde la ronda r1 y que la cabecera no listaba. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26**. Los recuentos pasan a **once casos de uso**, con CU-11 incorporado a la trazabilidad upstream; las comprobaciones de `Especificacion-Funcional.md` §4 pasan de tres a **cuatro**, con la del cambio de contraseña pendiente; y §7 declara que las reglas son **trece** y que RN-12 y RN-13 se citan contra el intake porque su archivo aguas arriba todavía no existe. |
| 1.2 | 2026-08-09 | **Reconciliación con lo que `GeometriaFactory-Domain` ya emitió.** §4 declaraba, en el diagnóstico de la regla que no aparece, que **RN-12 y RN-13 todavía no tenían archivo allá** y que acá se citaban contra el intake: **las trece tienen archivo**, y la fila pasa a decirlo. Ni el recorrido, ni el recuento de diagnósticos, ni el how-to cambian. |

## 7. La inversión de dependencias, en la práctica

Este es el tramo de una hora y el que más rinde a largo plazo. La pregunta que responde es: **qué hace exactamente que esta capa se pueda probar entera sin nada, y qué la rompería.**

El enunciado, una vez: esta capa **declara** qué necesita y otra capa lo provee. Un caso de uso que mencionara el motor de persistencia, el mecanismo de acceso o el protocolo de transporte estaría mal ubicado (`Especificacion-Funcional.md` §1).

### 7.1 Los cuatro puertos y qué le pide esta capa a cada uno

La tabla es la de `Especificacion-Funcional.md` §3, y se transcribe acá porque es el corazón de este tramo. **Lo que hay que leer es la columna del medio**: es la única forma correcta de pensar un puerto, porque enuncia una necesidad y no una implementación.

| Puerto | Qué le pide esta capa | Casos de uso que lo consumen | Qué NO le pide |
| --- | --- | --- | --- |
| Repositorio de trabajos | Recuperar un trabajo, resolver una consulta **ya acotada** por dueño o por alcance, materializar el resultado y ejecutar el retiro | CU-02, CU-04, CU-05, CU-06, CU-07, CU-08, CU-09 | Una consulta ad-hoc que el caso de uso arme; los componentes de las piezas en un listado |
| Repositorio de cuentas | Recuperar una cuenta por su correo, responder si un correo ya está registrado y **si ya existe una cuenta con papel `Administrador`**, y materializar el resultado | CU-01, CU-02, CU-03, CU-07, CU-10 | Comparar credenciales; decidir si el correo es «el mismo» según un criterio que esta capa no fijó |
| Validación de figuras | Interpretar el texto original y devolver **la cantidad de figuras del conjunto raíz**, las piezas reconstruidas y las observaciones, con su especie y su ubicación | CU-05 | Decidir el estado del trabajo; corregir el texto del alumno; emitir un conjunto de piezas o de observaciones mal formado |
| Reloj del sistema | Los sellos de alta, de modificación y de desenlace | CU-01, CU-03, CU-04, CU-05, CU-08, CU-10 | Nada más: es el puerto más chico y el más instructivo. Ver §7.2. **CU-02 no lo consume**: el modelo del dominio no declara fecha de última modificación de la cuenta, de modo que sus cuatro operaciones no registran ningún sello |

**Dos datos que viajan por esos puertos y que conviene reconocer, porque los dos se malinterpretan:**

- **Los sellos son metadatos de orquestación de esta capa**, no atributos del dominio: el modelo declara la fecha de alta del alumno —que recibe del consumidor— y la «Fecha» que el alumno declara en su trabajo, y nada más. La discrepancia está elevada al Product Owner y declarada como punto abierto (`Especificacion-Funcional.md` §3 y §11).
- **La cantidad de figuras del conjunto raíz** entra por el puerto de validación junto con las piezas y las observaciones, y CU-05 la hace viajar hasta el dominio. **No es derivable de las piezas adoptadas**, porque la posición de una figura que no se pudo reconstruir queda reservada y el conjunto admite huecos. Es el rango contra el que el dominio valida la posición de cada observación: sin ella, RN-09 deja de ser comprobable. CU-05 es el único orquestador de la reconstrucción y del registro de observaciones, de modo que es el único que puede aportarla.

**El identificador del puerto de repositorio de cuentas es un punto abierto declarado.** El intake nombra tres —`IRepositorioTrabajos`, `IValidadorFiguras` e `IRelojDelSistema`— y no éste, que la orquestación de las cuentas y la verificación de unicidad del correo necesitan. No es una regla nueva ni una decisión de alcance: es un nombre, y su definición se difiere a `05-Arquitectura-Tecnica` y al punto de control de la primera etapa (`Especificacion-Funcional.md` §11). Esta guía no lo reabre y lo nombra en lenguaje de dominio.

### 7.2 Por qué el reloj es un puerto

Es la decisión que más sorprende y la que mejor explica el estilo, así que conviene entenderla antes que ninguna otra. Está declarada como decisión pre-tomada en `PRODUCT-INTAKE` §17.2.P.11 punto 3, y el motivo es explícito: **para que las fechas de alta y de modificación sean verificables en prueba** —que es lo que esta capa llama, desde la corrección de la ronda r1, los **sellos** de alta, de modificación y de desenlace—.

Sin puerto de reloj, un criterio de aceptación como CA-01 de CU-01 —que exige devolver la cuenta «con sello de alta 2026-03-15»— no se puede escribir sin trucos. Con puerto de reloj se escribe en una línea: el doble devuelve ese sello y la prueba compara. La misma lógica vale para el validador y para los dos repositorios: **si algo no se puede fijar desde la prueba, es porque no pasó por un puerto**, y esa es la señal más barata de que un caso de uso está mal construido.

Consecuencia práctica para quien escribe un caso de uso nuevo: **el sello nunca se toma del ambiente**. Se pide al puerto, aunque parezca ceremonia para una línea de código.

### 7.3 Dónde va algo nuevo

Ante una capacidad nueva, en este orden:

1. **¿Es una condición sobre una entidad, que tiene que ser verdadera siempre?**
   Entonces no va acá: es un invariante y va como guarda de la entidad en `GeometriaFactory-Domain`. Esta capa **ejerce** las reglas sobre el pedido concreto, no las enuncia (`Especificacion-Funcional.md` §6). Termina acá.
2. **¿Exige conocer un conjunto de entidades, o el momento actual, o un dato que hay que interpretar?**
   Entonces sí es de esta capa, y va como **caso de uso apoyado en un puerto**. Es el caso de la unicidad del correo, que exige el conjunto de cuentas y por eso se verifica acá y no en el dominio (CU-01 §10).
3. **¿Necesita un puerto que todavía no existe?**
   Antes de declararlo, verificar que la necesidad se pueda enunciar **sin nombrar el mecanismo**. Si el enunciado no se puede escribir sin decir «base de datos», «HTTP» o «archivo», lo que hace falta no es un puerto nuevo sino repensar qué se le está pidiendo. Un puerto nuevo se declara acá y se implementa en `GeometriaFactory-Infrastructure`; su identificador, como el del repositorio de cuentas, lo fija 05.
4. **¿Es un mecanismo —guardar, autenticar, derivar, transportar, serializar, interpretar—?**
   Entonces acá va **la orquestación** y afuera va el mecanismo. Nunca las dos cosas en el mismo lugar, y nunca el mecanismo acá.
5. **Si la capacidad entra:** sube a [`../02-Especificacion-Funcional/`](../02-Especificacion-Funcional/) como caso de uso con su identificador `CU-XX`, declara qué puertos consume, sus condiciones de error aparecen en su §6, y **recién entonces** entran al catálogo de [`DX-Error-Messages.md`](DX-Error-Messages.md). El catálogo no inventa condiciones: las deriva.

La prueba de humo de todo el procedimiento, que se puede aplicar sin leer nada más: **si el caso de uso nuevo no se puede ejercer entero con dobles, algo se coló que no pasó por un puerto.**
