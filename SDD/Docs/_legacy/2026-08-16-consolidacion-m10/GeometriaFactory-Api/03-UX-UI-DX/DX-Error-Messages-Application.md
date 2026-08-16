# Catálogo de condiciones de error de los casos de uso

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Application
**Documento:** DX-Error-Messages.md
**Versión:** 1.8
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** DX Lead (AG-03)
**Variante:** DX
**Trazabilidad upstream:** §6 de los once casos de uso de `02-Especificacion-Funcional/Casos-De-Uso/` (CU-04001 a CU-04011), de donde se deriva cada entrada, con sus §3, §5, §7, §8, §9 y §10; `02-Especificacion-Funcional/Especificacion-Funcional.md` §3 (los cuatro puertos, los metadatos de orquestación y la cantidad de figuras del conjunto raíz), §4 (**las cuatro comprobaciones**, sus **cinco** precisiones y la equivalencia de la negativa de facultad), §6 y §11; `02-Especificacion-Funcional/Glosario-Funcional.md` §2 y §3; RN-04001 a RN-04016 y las §6 de los doce casos de uso de `Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/`, más **RN-04012** y **RN-04013** del `PRODUCT-INTAKE` **1.14** §4.1 y el invariante **INV-09** de su §17.1.P.2 · GeometriaFactory-Domain; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `01-Necesidades-Negocio/Necesidades-Negocio.md` §2 (NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.10**, §4 (F-26), §4.1, §4.2, §15, §17.1.P.2 · GeometriaFactory-Domain, §17.1.P.3 · GeometriaFactory-Application, §17.1.P.5 · GeometriaFactory-Application, §17.1.P.10 · GeometriaFactory-Application, §17.1.P.11 · GeometriaFactory-Application, §17.1.P.12 · GeometriaFactory-Application
**Trazabilidad downstream:** `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `11-Documentacion` de GeometriaFactory-Application

---

## Tabla de contenido

- [1. Principios de redacción de errores](#1-principios-de-redacción-de-errores)
  - [1.1 Qué pasó, por qué pasó, qué hacer](#11-qué-pasó-por-qué-pasó-qué-hacer)
  - [1.2 Una condición de error no es una observación, y el comentario tampoco](#12-una-condición-de-error-no-es-una-observación-y-el-comentario-tampoco)
  - [1.3 Qué emite esta capa y qué compone el consumidor](#13-qué-emite-esta-capa-y-qué-compone-el-consumidor)
  - [1.4 Un mismo motivo con dos causas opuestas: los dos caminos de alta](#14-un-mismo-motivo-con-dos-causas-opuestas-los-dos-caminos-de-alta)
- [2. Taxonomía](#2-taxonomía)
  - [2.1 Las categorías en uso](#21-las-categorías-en-uso)
  - [2.2 Las dos categorías que el proyecto de código hermano declaró vacías](#22-las-dos-categorías-que-el-proyecto-de-código-hermano-declaró-vacías)
  - [2.3 Forma de terminación](#23-forma-de-terminación)
  - [2.4 Las tres negativas de autorización](#24-las-tres-negativas-de-autorización)
  - [2.5 Lo que esta capa produce y lo que el dominio rechaza sin que acá ocurra](#25-lo-que-esta-capa-produce-y-lo-que-el-dominio-rechaza-sin-que-acá-ocurra)
- [3. Catálogo](#3-catálogo)
  - [3.1 CU-04001 Registrar el alta de una cuenta](#31-cu-01-registrar-el-alta-de-una-cuenta)
  - [3.2 CU-04002 Gobernar las cuentas de la comisión](#32-cu-02-gobernar-las-cuentas-de-la-comisión)
  - [3.3 CU-04003 Resolver el ingreso y la credencial del alumno](#33-cu-03-resolver-el-ingreso-y-la-credencial-del-alumno)
  - [3.4 CU-04004 Cargar y reeditar un trabajo propio](#34-cu-04-cargar-y-reeditar-un-trabajo-propio)
  - [3.5 CU-04005 Enviar un trabajo e interpretar su texto](#35-cu-05-enviar-un-trabajo-e-interpretar-su-texto)
  - [3.6 CU-04006 Consultar los trabajos propios del alumno](#36-cu-06-consultar-los-trabajos-propios-del-alumno)
  - [3.7 CU-04007 Revisar los trabajos de la comisión](#37-cu-07-revisar-los-trabajos-de-la-comisión)
  - [3.8 CU-04008 Dar desenlace a un trabajo](#38-cu-08-dar-desenlace-a-un-trabajo)
  - [3.9 CU-04009 Eliminar un trabajo](#39-cu-09-eliminar-un-trabajo)
  - [3.10 CU-04010 Configurar la cuenta de administrador](#310-cu-10-configurar-la-cuenta-de-administrador)
  - [3.11 CU-04011 Resetear la contraseña de un alumno](#311-cu-11-resetear-la-contraseña-de-un-alumno)
- [4. Tono y voz](#4-tono-y-voz)
- [5. Localización](#5-localización)
- [6. Control de cambios](#6-control-de-cambios)
- [7. Cobertura y trazabilidad](#7-cobertura-y-trazabilidad)
  - [7.1 Recuento](#71-recuento)
  - [7.2 Verificación mecánica de cobertura](#72-verificación-mecánica-de-cobertura)
  - [7.3 Tabla de cobertura](#73-tabla-de-cobertura)
  - [7.4 Trazabilidad del artefacto](#74-trazabilidad-del-artefacto)

---

## 1. Principios de redacción de errores

### 1.1 Qué pasó, por qué pasó, qué hacer

Las tres partes son obligatorias en cada entrada y se corresponden con las tres columnas del catálogo: **mensaje** dice qué pasó, **causa probable** dice por qué pasó, **acción sugerida** dice qué hacer al respecto.

La tercera parte tiene acá dos destinatarios, y distinguirlos es lo que la hace accionable:

> El diagnóstico dice **qué hacer del lado del consumidor** cuando la negativa nace de lo que el consumidor pidió, y **qué corregir del lado del adaptador del puerto** cuando nace de lo que un puerto devolvió. Confundirlos manda a corregir la capa equivocada.

Cinco reglas de redacción que ninguna entrada incumple:

1. **Lenguaje plano y sin culpar a nadie.** El enunciado describe la comprobación que se negó, no la torpeza de quien invocó.
2. **Nada genérico.** No hay «operación inválida» ni «error interno». Una negativa dice qué comprobación se negó. Es la misma exigencia que RN-04009 le impone al producto frente al alumno, aplicada frente al consumidor.
3. **Nada que la regla oculte se filtra.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` es deliberadamente indistinguible de la inexistencia (RN-04003), y la cuenta inexistente en la consulta de admisibilidad no se distingue hacia afuera para no revelar qué correos están registrados (CU-04003 §6 y §10). El tratamiento completo está en §2.4.
4. **Ningún motivo es un código de protocolo.** El motivo es un valor de una enumeración cerrada; la traducción a respuesta pertenece a `GeometriaFactory-Api` (`Glosario-Funcional.md` §2).
5. **Ninguna condición deja efecto parcial.** El alcance transaccional declarado es un caso de uso, una unidad de trabajo (`Especificacion-Funcional.md` §3), y por eso cada entrada puede afirmar sin excepción que el repositorio de cuentas o el de trabajos quedan como estaban.

### 1.2 Una condición de error no es una observación, y el comentario tampoco

Es la distinción que sostiene todo lo demás, y confundirla lleva a modelar mal tres cosas a la vez. Las tres nociones son distintas y ninguna es especie de otra:

| Noción | Qué es | Cuántas hay | Quién la produce | Se guarda |
| --- | --- | --- | --- | --- |
| **Condición de error del caso de uso** | Una comprobación que impide una operación ilegítima o imposible. Es lo que este catálogo enumera, y se identifica por un **motivo** | Una por invocación negada, y no sobrevive a la invocación | El caso de uso, al negarse, o el dominio, cuyo rechazo el caso de uso propaga | No |
| **Observación** | Entidad del dominio con dos especies, advertencia y error de validación, que el producto emite **al interpretar el texto del alumno** y al verificar sus valores | Varias por trabajo, tantas como defectos | El validador de figuras, detrás del puerto de validación; el caso de uso la incorpora al trabajo (CU-04005 §4 paso 5) | Sí, como entidad |
| **Comentario** | Texto libre y opcional que el administrador deja al aprobar o al rechazar. **No es una observación y no es una calificación** | A lo sumo uno por trabajo | Una persona | Sí, como atributo del trabajo |

Consecuencia práctica, y es la que más veces se equivoca: un trabajo que vuelve en `Borrador` porque su texto trajo un error de validación **no produjo ninguna condición de este catálogo**. Es el resultado declarado del envío (CU-04005 FA-01), el estado lo resolvió el dominio y el caso de uso lo devolvió con sus observaciones localizadas. Traducirlo hacia afuera como fallo sería un defecto del consumidor.

En el sentido inverso: `OBSERVACION_MAL_FORMADA` y `CONJUNTO_DE_PIEZAS_MAL_FORMADO` **sí** son condiciones de este catálogo, aunque hablen de observaciones y de piezas. Lo que se niega no es la observación ni la pieza en sí: es un conjunto mal formado que llegó del validador, y que el alumno no debe ver.

### 1.3 Qué emite esta capa y qué compone el consumidor

Esta capa emite un **motivo**, no un texto. No produce mensajes para personas, no los formatea y no los traduce: no cruza ninguna frontera de proceso y sus contratos son referencias de proyecto de código dentro de la misma solución de código (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application).

La columna «mensaje» de este catálogo es el **enunciado canónico en lenguaje plano** de cada condición: la base sobre la que la capa que expone compone lo que una persona lee. No es una cadena que la biblioteca produzca ni un recurso que exista en el código.

### 1.4 Un mismo motivo con dos causas opuestas: los dos caminos de alta

El producto tiene **dos caminos de alta de cuenta**, y no son variantes de uno solo: son dos contratos con reglas opuestas. Entenderlo es condición para leer bien §3.1 y §3.10, y para no buscar en uno lo que está en el otro.

| Rasgo | Auto-registro del alumno (CU-04001) | Configuración del administrador (CU-04010) |
| --- | --- | --- |
| Estado inicial que impone el dominio | `Pendiente` | `Habilitado` |
| Credencial derivada en el alta | **Prohibida.** Se fija en el primer ingreso efectivo, por CU-04003 | **Obligatoria.** La cuenta nace con credencial fijada |
| Ventana de alta | Abierta siempre: una vez por alumno | Abierta **sólo mientras no exista ningún administrador**. Se cierra con la primera configuración y no vuelve a abrirse |
| Papel que constituye | `Alumno` | `Administrador` |
| Veces que se ejerce | Una por alumno | **Una sola en la vida de la instancia** |

El fundamento de que la cuenta del administrador nazca `Habilitado` lo declara el dominio y esta capa no lo redacta de nuevo: si naciera `Pendiente`, la única transición que la sacaría de ahí es que un administrador la habilite, y no hay ninguno; la instancia quedaría inutilizable en el primer arranque (CU-04010 §10).

**Consecuencia sobre el catálogo.** El motivo `ESTADO_INICIAL_NO_NEGOCIABLE` aparece en los dos caminos con **causas opuestas**: en CU-04001 rechaza constituir la cuenta del auto-registro en un estado distinto de `Pendiente`; en CU-04010, en un estado distinto de `Habilitado`. No es una inconsistencia y no hay que unificarlo: el enunciado del motivo es «el estado inicial de este camino no se elige», y cuál es ese estado lo fija el camino.

Por eso es **el único motivo del catálogo que lleva fila completa en dos subsecciones de §3** en lugar de una entrada única con nota, y las dos filas se leen juntas, con remisión mutua. **Es la misma forma que adoptó el proyecto de código hermano** en su propia categoría 03 para el mismo motivo, y se conserva idéntica: la consistencia entre proyectos de código hermanos vale más que la economía de una fila. Las otras ocho condiciones declaradas en más de un caso de uso conservan la misma causa en todos y siguen con entrada única.

## 2. Taxonomía

### 2.1 Las categorías en uso

| Categoría | Qué agrupa | Cuántas condiciones |
| --- | --- | --- |
| **Entrada inválida** | El dato que llega está ausente, vacío, no admitido en este camino, o no pertenece a un conjunto cerrado declarado | 14 |
| **Recurso ausente** | Lo que la operación referencia no existe, no existe **para quien lo pide**, o todavía no tiene valor | 3 |
| **Conflicto de estado** | La operación es legítima, pero el estado actual de la cuenta, del trabajo o del conjunto de cuentas no la admite | 12 |
| **Conflicto de facultad** | La operación es legítima y el estado la admitiría, pero el papel declarado por **quien pide** no la ejerce, o el papel de la **cuenta destino** no admite la operación | 3 |
| **Conflicto de alcance** | La operación es legítima y el papel la ejerce, pero el trabajo pedido está fuera de lo que ese papel ve | 1 |
| **Error transitorio** | Un puerto no pudo completar lo que se le pidió, por una causa que no depende de lo que el consumidor pidió | 1 |
| **Error interno** | Un adaptador de puerto devolvió algo que el contrato no admite. No es un defecto del caso de uso ni del consumidor | 2 |

Dos categorías se agregan a la enumeración de referencia y conviene justificarlas, porque son exactamente las que esta capa existe para ejercer:

- **Conflicto de facultad** se declara aparte porque no se resuelve mirando el dato ni el estado, sino el papel de quien pide. Confundirla con un conflicto de estado llevaría a buscar el remedio en una transición que no existe.
- **Conflicto de alcance** se declara aparte de las otras dos porque su remedio también es distinto: no hay dato que corregir ni papel que cambiar, hay un trabajo que simplemente no forma parte del flujo de trabajo del administrador. Fundirla con «conflicto de estado» haría creer que existe una transición que lo trae al alcance, y no existe (RN-04011).

**Una divergencia deliberada de clasificación con el proyecto de código hermano, declarada para que no se lea como descuido.** El motivo `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` está clasificado allá como conflicto de facultad y acá como **entrada inválida**. El fundamento es uno solo, y es que el referente cambia con la capa: en el dominio el papel llega como pretensión de constituir una entidad reservada; acá el papel es **un dato del pedido de alta**, no la facultad de quien pide. Nadie está ejerciendo una facultad que no tiene, y **CU-04001 no verifica facultad ni pertenencia** —el auto-registro lo ejerce una persona que todavía no tiene cuenta (CU-04001 §10)—: lo que se rechaza es un valor del pedido, exactamente como en `PAPEL_NO_RECONOCIDO`, que esta capa clasifica igual.

Y lo que esta divergencia **no** invoca, escrito acá para que nadie lo reponga: **no hay correspondencia uno a uno entre la categoría de conflicto de facultad y la negativa por facultad de §2.4**. La categoría tiene tres miembros —`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` y `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`— y las negativas de autorización son cuatro, de las cuales sólo la primera de esas tres es una. Son cosas de distinto orden: la categoría es taxonómica y la negativa es una de las tres comprobaciones de autorización. La clasificación de este motivo se sostiene por el referente del papel y por nada más.

### 2.2 Las dos categorías que el proyecto de código hermano declaró vacías

El proyecto de código hermano las declaró vacías con su motivo, porque el dominio no ejecuta entrada ni salida. **Acá no están vacías, y la diferencia es informativa**: es la primera capa del producto que depende de algo que puede no responder.

| Categoría | Condiciones | Por qué existen acá y no en el dominio |
| --- | --- | --- |
| **Error transitorio** | `INTERPRETACION_NO_DISPONIBLE` | Esta capa **depende de puertos**, y un puerto puede no poder completar lo que se le pidió. La terminación es degradada y declarada: el trabajo queda en `Borrador` con su texto intacto (CU-04005 §6). Aun así, **esta capa no reintenta**: devuelve el estado degradado y quien decida reintentar es el consumidor |
| **Error interno** | `CONJUNTO_DE_PIEZAS_MAL_FORMADO`, `OBSERVACION_MAL_FORMADA` | Son los dos casos en que el motivo no denuncia lo que el consumidor pidió sino **lo que un adaptador devolvió**. El caso de uso no los puede corregir y no los puede mostrar: un conjunto mal formado es un defecto del validador y no un resultado que el alumno deba ver (CU-04005 §6). Los dos son **condiciones agregadas**, y su relación con los ocho rechazos del dominio que agrupan está en §2.5 |

Ninguna otra condición pertenece a estas dos categorías, y una falla no declarada tampoco: su lugar es una prueba que falla, no una entrada acá.

### 2.3 Forma de terminación

Dimensión ortogonal a la categoría, y hay que leerla junto con ella porque cambia lo que el consumidor tiene que hacer:

| Forma | Qué significa | Dónde aparece |
| --- | --- | --- |
| **Negativa sin escritura** | El caso de uso se niega a una operación de escritura. No abre la unidad de trabajo, o la cierra sin efecto; el repositorio queda exactamente como estaba | CU-04001, CU-04002, CU-04003 en sus operaciones sobre la credencial, CU-04004, CU-04005, CU-04008, CU-04009, CU-04010 |
| **Motivo de resultado** | La operación es una consulta y **siempre devuelve un resultado**; el motivo es la razón por la que ese resultado es «no admisible» o «no procede». No es una excepción de programa y no modifica nada | CU-04003 en la consulta de admisibilidad, CU-04006, CU-04007 |
| **Terminación degradada** | La operación no se completó por una causa que no depende del pedido, y el caso de uso lo declara en vez de fingir un resultado. Es la forma de una sola condición: `INTERPRETACION_NO_DISPONIBLE` | CU-04005 |

La diferencia importa: ante una negativa sin escritura el consumidor corrige la invocación; ante un motivo de resultado **informa** o **encamina** a la operación que corresponde; ante una terminación degradada informa que el servicio no está disponible y **no** presenta el trabajo como interpretado. `CREDENCIAL_NO_ESTABLECIDA` es el ejemplo canónico del segundo caso: no es un fallo, es la situación esperada del primer ingreso efectivo del alumno.

### 2.4 Las tres negativas de autorización

Esta es la sección que justifica que `tiene_auth` valga true en este proyecto de código, y la que hay que dejar imposible de confundir. Las **cuatro** comprobaciones transversales de `Especificacion-Funcional.md` §4 producen cuatro negativas, y **confundir las dos primeras es el error más caro que un consumidor puede cometer contra esta capa**: confirmar que un recurso ajeno existe habilita averiguar por tanteo qué identificadores existen.

| Negativa | Motivo | Qué se preguntó | ¿Oculta la existencia del recurso? | Traducción del consumidor |
| --- | --- | --- | --- | --- |
| **Pertenencia** | `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | ¿Este trabajo es del alumno que lo pide? | **Sí, deliberadamente.** El trabajo ajeno y el identificador inexistente comparten motivo por diseño | «No encontrado», y **nunca** «no autorizado» |
| **Facultad** | `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | ¿Quien pide esta operación reservada tiene el papel `Administrador`? | **No, y no tiene por qué.** No hay recurso ajeno cuya existencia proteger: se preguntó por una facultad, no por un recurso | Explícita: la operación requiere la facultad de administrador |
| **Alcance** | `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | ¿Este trabajo entra en lo que el administrador ve? | **No.** Expresa que el trabajo está fuera de su flujo de trabajo, no que no exista | Explícita: los trabajos en `Borrador` no forman parte de la revisión |
| **Cambio de contraseña pendiente** | `CAMBIO_DE_CONTRASENA_PENDIENTE` | ¿La cuenta que pide fue reseteada por el administrador y todavía no cambió su clave? | **No, y no debe.** La persona sabe perfectamente que le resetearon la clave: ocultarlo la dejaría sin saber qué hacer | Explícita, y **con el camino**: hay que cambiar la contraseña antes de cualquier otra cosa |

Las **cinco** precisiones que rigen en toda la categoría, transcriptas de `Especificacion-Funcional.md` §4 porque son el insumo directo de este catálogo:

1. **El papel no reemplaza a la pertenencia.** Son dos comprobaciones distintas: un alumno autenticado no debe poder leer el trabajo de otro cambiando el identificador de la petición, y ningún papel resuelve eso.
2. **La negativa por pertenencia y la negativa por facultad no se confunden.** La primera oculta la existencia del recurso; la segunda no tiene nada que ocultar.
3. **La comprobación se hace sobre el dato recuperado y antes de escribir.** No se resuelve ocultando un control en la pantalla, y por eso es verificable con dobles sin base de datos.
4. **El trabajo ajeno y el identificador inexistente comparten motivo por diseño.** Distinguirlos permitiría averiguar por tanteo qué identificadores existen.
5. **La cuarta comprobación corta antes que las otras tres y tiene una sola excepción.** Una cuenta marcada por un reseteo no ejerce ninguna capacidad —ni las que su papel y su pertenencia admitirían— salvo cambiar su propia contraseña, que es el reemplazo de CU-04003 FA-05. La marca la pone únicamente CU-04011 y la levanta únicamente ese cambio (INV-09). **Para el consumidor esto tiene una consecuencia operativa**: ante `CAMBIO_DE_CONTRASENA_PENDIENTE` no hay dato que corregir ni papel que cambiar, hay una sola ruta a la que llevar a la persona.

**Una sola negativa de facultad, y dos motivos del dominio detrás.** El dominio declara dos motivos distintos para la misma negativa —uno en su resolución de desenlace y otro en la de alcance del administrador— y esta capa emite uno solo: corta con su propia verificación **antes** de invocar al dominio, de modo que ninguno de los dos llega a producirse (`Especificacion-Funcional.md` §4, CU-04008 §10, CU-04009 §10). Quien lea las dos capas no debe leer tres negativas de facultad donde hay una.

**Procedimiento de decisión**, para el consumidor que tiene que traducir un motivo y para quien escribe un caso de uso nuevo:

1. **¿La pregunta fue por un recurso concreto que puede ser de otra persona?** Si es sí, la negativa oculta: mismo motivo para el ajeno y para el inexistente, y traducción a «no encontrado». Termina acá.
2. **¿La pregunta fue por una facultad, sin recurso ajeno de por medio?** Entonces la negativa puede ser explícita: no hay nada que ocultar, y ocultarla sólo haría más difícil el diagnóstico.
3. **¿La cuenta que pide está marcada como con cambio de contraseña pendiente?** Entonces ninguna de las dos preguntas anteriores llega a hacerse: la negativa es explícita y encamina al cambio.
3. **¿La pregunta fue por un recurso que el papel sí puede ver en general, pero éste en particular queda fuera de su alcance?** Entonces la negativa es explícita y **no oculta**: el administrador ve todo lo que no es borrador, y decirle que un borrador está fuera de su alcance no le revela nada que no supiera.

**Traducciones prohibidas.** Ninguna de estas cuatro es admisible en `GeometriaFactory-Api` ni en ninguna superficie aguas abajo, y la métrica que las cuenta tiene objetivo cero ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §6):

| Traducción prohibida | Por qué | Qué corresponde |
| --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` → «no autorizado» | Confirma que el recurso existe y que es de otro. Es exactamente lo que RN-04003 impide | «No encontrado» |
| Devolver una respuesta distinta para el trabajo ajeno y para el identificador inexistente | La distinción por sí sola permite el tanteo, aunque los dos textos sean vagos | Una sola respuesta, indistinguible |
| Distinguir hacia afuera la cuenta inexistente de la cuenta que no admite ingreso | Revela qué correos están registrados (CU-04003 §6 y §10) | No admisible, sin distinguir el motivo hacia afuera |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` → «no encontrado» | El error simétrico, y también es un defecto: oculta lo que no hace falta ocultar y deja al integrador sin diagnóstico | Explícita, tal como el motivo la declara |

**Cómo se sostiene esto sin confiar en la buena memoria.** La indistinguibilidad es verificable: CA-03 de CU-04006 exige que el motivo devuelto para el detalle de un trabajo ajeno sea **el mismo** que para un identificador inexistente, y CA-03 de CU-04009 lo exige para la eliminación. Las dos son pruebas unitarias con repositorio simulado, y son las que impiden que una refactorización reintroduzca la distinción sin que nadie se dé cuenta.

### 2.5 Lo que esta capa produce y lo que el dominio rechaza sin que acá ocurra

Los once casos de uso orquestan trece casos de uso del dominio, y **el dominio declara rechazos que esta capa no puede producir**. Su ausencia del catálogo no es un olvido: la 02 los nombra uno por uno en sus §10 para que no se lea así, y acá se reúnen en una sola tabla porque para quien implementa la capa es información operativa. **Ninguna fila de esta tabla es una condición de este catálogo**, y por eso ninguna entra en los recuentos de §7.

| Rechazo del dominio | Origen | Por qué acá no ocurre | Dónde está declarado |
| --- | --- | --- | --- |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | Dominio, auto-registro y configuración | **Inalcanzable por construcción.** Los dos caminos de alta consultan el correo antes y declaran siempre la verificación al invocar | CU-04001 §10 |
| `BAJA_SIN_ARRASTRE_DE_TRABAJOS` | Dominio, ciclo de vida de la cuenta | **Inalcanzable por construcción.** El flujo alternativo de la baja siempre declara el arrastre | CU-04002 §10 |
| `REEDICION_FUERA_DE_BORRADOR` | Dominio, creación y reedición del trabajo | **Equivalente**, no ausente: es la misma negativa que `OPERACION_FUERA_DE_BORRADOR`. Esta capa corta antes, con la resolución de acceso del dominio | CU-04004 §10 |
| `ENVIO_SIN_INTERPRETACION` | Dominio, gobierno del estado del trabajo | **Inalcanzable por construcción.** El envío interpreta siempre antes de invocar al dominio | CU-04005 §10 |
| `DESENLACE_NO_ADMITIDO_EN_ESTE_CONTRATO` | Dominio, gobierno del estado del trabajo | **Inalcanzable por construcción.** El envío no ofrece aprobar ni rechazar: eso es CU-04008 | CU-04005 §10 |
| `TIPO_DE_PIEZA_DESCONOCIDO`, `FAMILIA_DECLARADA_CONTRADICE_AL_TIPO`, `POSICION_DE_PIEZA_INVALIDA`, `RECONSTRUCCION_SOBRE_TRABAJO_TERMINAL` | Dominio, reconstrucción del conjunto de piezas | **Agregados deliberadamente** en `CONJUNTO_DE_PIEZAS_MAL_FORMADO`. Los cuatro son defectos del validador o de la orquestación, y ninguno es un resultado que el alumno deba ver | CU-04005 §6 |
| `ESPECIE_DE_OBSERVACION_DESCONOCIDA`, `ERROR_SIN_UBICACION`, `ADVERTENCIA_SIN_LOS_DOS_VALORES`, `OBSERVACION_SOBRE_PIEZA_INEXISTENTE` | Dominio, registro de las observaciones | **Agregados deliberadamente** en `OBSERVACION_MAL_FORMADA`, por el mismo criterio y de forma simétrica | CU-04005 §6 |
| `DESENLACE_SIN_PAPEL_DE_ADMINISTRADOR`, `ALCANCE_SIN_PAPEL_DE_ADMINISTRADOR` | Dominio, desenlace y alcance del administrador | **No llegan a producirse.** Esta capa corta antes con su propia verificación de facultad, y emite un motivo único por los dos | `Especificacion-Funcional.md` §4, CU-04008 §10, CU-04009 §10 |
| `OPERACION_DESCONOCIDA` | Dominio, acceso del alumno y alcance del administrador | **Inalcanzable por construcción.** Cada resolución se consulta con una operación fija; lo que sí puede llegar mal es el papel, y eso es `PAPEL_NO_RECONOCIDO` | CU-04009 §10 |
| `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` | Dominio, reseteo de la contraseña de una cuenta de alumno | **No llega a producirse.** Esta capa corta antes con su propio acotamiento a cuentas de alumno y emite `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`; el cierre es el mismo y su fuente es `RN-04015`, que lo ancla en INV-08 | CU-04011 §6 y §10 |
| `RESETEO_CON_ARRASTRE_DE_TRABAJOS` | Dominio, reseteo de la contraseña de una cuenta de alumno | **Inalcanzable por construcción.** La invocación de esta capa **nunca declara** efecto sobre los trabajos ni sobre el estado de la cuenta: el reseteo no es una baja y no dispara RN-04007 (RN-04012) | CU-04011 §7 y §10 |

Dos consecuencias para quien implementa:

1. **Una condición agregada esconde varias causas del dominio, y eso es deliberado.** Al depurar `CONJUNTO_DE_PIEZAS_MAL_FORMADO` o `OBSERVACION_MAL_FORMADA`, el motivo fino que hay que mirar es el que devolvió el dominio, y está en las tablas de la 02 de `GeometriaFactory-Domain`. Este catálogo no lo repite porque no es lo que esta capa emite.
2. **Un rechazo inalcanzable que aparece en ejecución es un defecto de esta capa, no del consumidor.** Si el dominio devuelve `ENVIO_SIN_INTERPRETACION`, el caso de uso saltó un paso propio. Es la mejor señal temprana que ofrece esta frontera.

## 3. Catálogo

Treinta y seis condiciones, derivadas una por una de la §6 de los once casos de uso. **El número no cambió con la emisión 1.6 y la composición sí**: entró `HABILITACION_SIN_CREDENCIAL_PROVISORIA` en CU-04002 y salió `CREDENCIAL_NO_ESTABLECIDA`, que declaraban CU-04003 y CU-04011. Ninguna se inventó y ninguna quedó afuera; el recuento y la verificación mecánica están en §7.

**Diez** condiciones se declaran en más de un caso de uso. **Nueve conservan la misma causa en todos** y llevan una sola entrada, en el caso de uso donde aparecen primero, con la nota de sus apariciones restantes. La undécima, `ESTADO_INICIAL_NO_NEGOCIABLE`, lleva **fila completa en §3.1 y en §3.10** porque sus dos causas son opuestas según el camino de alta: el motivo está en §1.4. Es la única fila excedente del catálogo: 37 filas de tabla para 36 condiciones.

### 3.1 CU-04001 Registrar el alta de una cuenta

Es el **auto-registro del alumno**, uno de los dos caminos de alta (§1.4). Forma de terminación: negativa sin escritura. En los cinco casos no se constituye ninguna cuenta y la unidad de trabajo no se abre.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CORREO_YA_REGISTRADO` | Conflicto de estado | El correo aportado ya pertenece a una cuenta | La consulta de unicidad lo encontró ocupado, o el puerto de repositorio rechazó la materialización por una colisión que esa consulta no vio (CU-04001 FA-02) | Informar que el correo está ocupado y **no informar el estado ni el papel de la cuenta que lo ocupa**. La verificación previa no es una garantía por sí sola: la unicidad efectiva la sostiene también la capa que guarda, y por eso este motivo llega por dos caminos (RN-04002). Esta condición vuelve a declararse en CU-04010, con la misma causa |
| `DATO_OBLIGATORIO_AUSENTE` | Entrada inválida | Falta un dato obligatorio del alta: correo, nombre o apellido | El dominio rechazó la constitución porque uno de los tres llegó vacío | Completar el dato antes de invocar. Esta capa **propaga el motivo del dominio sin traducirlo**: no lo infiere ni lo deja en blanco. Esta condición vuelve a declararse en CU-04004, sobre el nombre y la fecha del trabajo, y en CU-04010, con la misma causa que acá |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | Entrada inválida | El **auto-registro** no admite credencial derivada | El consumidor aportó una credencial derivada junto con los datos del auto-registro | Registrar sin credencial: en este camino se fija recién en el primer ingreso efectivo, por CU-04003. **En la configuración del administrador la credencial sí se aporta**, y eso es CU-04010: el motivo está acotado a este camino (§1.4) |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta del auto-registro en un estado distinto de `Pendiente` | Invocar sin pedir estado: lo fija el dominio. Toda cuenta de alumno nace `Pendiente` y sólo el administrador la habilita, con acto explícito (CU-04002). **Mismo motivo, causa opuesta en CU-04010**, donde el estado impuesto es `Habilitado`: ver §3.10 y §1.4 |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | Entrada inválida | El auto-registro no constituye cuentas con papel `Administrador` | Se pidió constituir un administrador por la vía del alumno | Usar CU-04010, que es el camino declarado para la configuración del administrador. Constituirlo acá lo dejaría `Pendiente` y sin salida, porque ninguna otra cuenta podría habilitarlo (§1.4). **Sobre su categoría**, que diverge de la del proyecto de código hermano, ver §2.1 |

### 3.2 CU-04002 Gobernar las cuentas de la comisión

Forma de terminación: negativa sin escritura. Ninguna deja efecto parcial: la baja escribe todo o no escribe nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | Conflicto de facultad | La operación requiere el papel `Administrador` | El papel declarado por quien pide no es `Administrador` | Comprobar el papel antes de invocar. **Es una negativa por facultad y no por pertenencia**: la existencia de la cuenta destino no se oculta, porque quien pregunta no está pidiendo un recurso ajeno sino ejerciendo una facultad que no tiene (§2.4). El caso de uso no recupera ni modifica nada. Esta condición vuelve a declararse en CU-04007 y en CU-04008 |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | Entrada inválida | El correo escrito como confirmación no es el de la cuenta destino | Se solicitó la baja con un correo de confirmación distinto | Volver a pedirle al administrador que escriba el correo exacto de la cuenta. La confirmación escrita es exigencia de RN-04007 y protege la única operación destructiva del producto: no se retira ningún trabajo ni la cuenta, y la unidad de trabajo no se abre |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | Conflicto de estado | La transición pedida no está admitida desde el estado actual de la cuenta | El dominio rechazó el par estado actual y transición | Encadenar las transiciones declaradas por la máquina de estados de la cuenta, que vive en `GeometriaFactory-Domain`. Esta capa **propaga el motivo y conserva el estado actual**: no infiere transiciones intermedias |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | Conflicto de facultad | La cuenta con papel `Administrador` no se da de baja | Se pidió dar de baja al administrador de la instancia | No hay camino: la instancia quedaría sin administrador (RN-04001) y su alta ya no puede repetirse, porque la ventana se cerró con la primera configuración (§1.4). Esta capa propaga el rechazo del dominio |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | Entrada inválida | Habilitar o rehabilitar exige la credencial derivada provisoria | El puerto de producción de la provisoria o el de derivación no entregaron el valor, y la transición se invocó sin él | Producir la provisoria, derivarla y aportarla en la misma invocación. Desde **RN-04016** fijar la credencial y poner la marca son efectos del mismo acto que habilitar, y admitir la transición sin credencial dejaría la cuenta `Habilitado` sin nada con que autenticarse |
| `CUENTA_INEXISTENTE` | Recurso ausente | No hay ninguna cuenta con el identificador o el correo pedido | El puerto de repositorio de cuentas no la encontró | Verificar el dato con el que se invocó. **Acá no oculta nada**, porque la operación ya exigió la facultad de administrador y el administrador gobierna todas las cuentas de la comisión. Esta condición vuelve a declararse en CU-04003, donde su tratamiento hacia afuera **sí** es distinto: ver §3.3 |

### 3.3 CU-04003 Resolver el ingreso y la credencial del alumno

Dos formas conviven: la consulta de admisibilidad es **motivo de resultado** —siempre devuelve un resultado, y el motivo explica por qué no es admisible— y las operaciones sobre la credencial son negativas sin escritura, que dejan la cuenta exactamente como estaba.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `CUENTA_PENDIENTE` | Conflicto de estado | La cuenta está registrada y todavía no fue habilitada | El estado de cuenta es `Pendiente` (RN-04006) | **No es un fallo y no se responde con un rechazo genérico**: informar con todas las letras que la cuenta espera la habilitación del administrador, que es lo que el producto promete al alumno sin canal de correo. No emitir acceso |
| `CUENTA_BLOQUEADA` | Conflicto de estado | La cuenta está bloqueada | El estado de cuenta es `Bloqueado` (RN-04006) | Informar el motivo y no emitir acceso. La rehabilitación es un acto explícito del administrador, por CU-04002. Una cuenta bloqueada conserva sus trabajos: la baja es la única operación destructiva |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | Conflicto de estado | La credencial derivada sólo se fija o se reemplaza con la cuenta habilitada | Se intentó fijar o reemplazar sobre una cuenta `Pendiente` o `Bloqueado` | Habilitar o rehabilitar la cuenta primero, por CU-04002. Esta capa propaga el rechazo del dominio y conserva la credencial como estaba |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | Entrada inválida | El reemplazo exige declarar verificada la credencial vigente | Se pidió el reemplazo sin esa declaración | Verificar la credencial vigente en la capa que sí puede compararla —`GeometriaFactory-Infrastructure`— y **declararlo al invocar**. Esta capa no compara credenciales: exige que la verificación se declare, que es la forma en que la regla se hace exigible sin conocer el mecanismo |
| `CREDENCIAL_YA_FIJADA` | Conflicto de estado | La credencial derivada ya tiene valor | Se pidió fijar por primera vez algo que ya está fijado | Usar el camino de reemplazo, declarando verificada la credencial vigente. El valor anterior se reemplaza y no se conserva historial. Es el motivo que recibe siempre la cuenta del administrador si se intenta fijarle credencial, porque nace con una |
| `VALOR_DERIVADO_VACIO` | Entrada inválida | El valor de credencial derivada llegó vacío | Se invocó la fijación o el reemplazo con un valor sin contenido | Aportar el valor **ya derivado**. Esta capa no deriva la contraseña y nunca la conoce en claro; conserva la credencial como estaba. Esta condición vuelve a declararse en CU-04011, con la misma causa: allá el valor vacío es el de la contraseña provisoria |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | Conflicto de estado | La cuenta tiene que cambiar su contraseña antes de hacer cualquier otra cosa | La cuenta fue reseteada por el administrador en CU-04011 y todavía no cambió la provisoria (RN-04013, INV-09) | **No es un fallo y no se responde con un rechazo genérico**: encaminar al cambio de contraseña, que es la única ruta disponible para esa cuenta. **Es la cuarta comprobación transversal de `Especificacion-Funcional.md` §4** y por lo tanto la puede devolver cualquier caso de uso; su entrada vive acá porque acá está su única excepción, el reemplazo de FA-05, que es lo que la levanta. No hay dato que corregir ni papel que cambiar |

**La cuenta inexistente en la consulta de admisibilidad.** `CUENTA_INEXISTENTE` tiene su entrada única en §3.2, pero su tratamiento acá es distinto y es una de las reglas de ocultamiento del producto: cuando el puerto de repositorio no encuentra el correo, el caso de uso devuelve **no admisible sin distinguir el motivo hacia afuera**, para no revelar qué correos están registrados (CU-04003 §6, CA-05). Es el mismo criterio con el que un trabajo ajeno es indistinguible de uno inexistente, aplicado a la cuenta.

### 3.4 CU-04004 Cargar y reeditar un trabajo propio

Forma de terminación: negativa sin escritura. Ninguna deja escritura parcial: la unidad de trabajo se abre recién al materializar.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | Recurso ausente | El trabajo no existe para quien lo pide | El solicitante no es el dueño del trabajo, **o el identificador no existe** | Traducirlo a «no encontrado» y **nunca** a «no autorizado». Los dos casos comparten motivo por diseño: es lo que impide averiguar por tanteo qué identificadores existen (RN-04003). Ver §2.4, incluida la tabla de traducciones prohibidas. Esta condición vuelve a declararse en CU-04005, CU-04006 y CU-04009 |
| `OPERACION_FUERA_DE_BORRADOR` | Conflicto de estado | El dueño no reedita ni elimina un trabajo fuera de `Borrador` | Se pidió reeditar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado` | Informar la acotación al borrador (RN-04004). **Es un motivo distinto del anterior porque acá la existencia del trabajo ya está admitida para su dueño**: quien pregunta es el dueño y no hay nada que ocultarle. **Ver** un trabajo propio sí procede en los cuatro estados. Esta condición vuelve a declararse en CU-04009, sobre la eliminación, y es la misma negativa que el dominio llama `REEDICION_FUERA_DE_BORRADOR` (§2.5) |
| `TEXTO_ORIGINAL_ALTERADO` | Entrada inválida | El texto original no admite versiones corregidas | El consumidor aportó como texto original una versión corregida del que pegó el alumno | Conservar el texto tal como el alumno lo pegó (RN-04008). El producto no edita el dato del alumno, y es justamente lo que hace posible reprocesar el mismo trabajo cuando el validador mejora. La reedición cambia los datos del trabajo y el texto que el alumno **vuelve a pegar**, nunca el texto ya guardado |
| `TRABAJO_SIN_DUENO` | Entrada inválida | El trabajo no trae la identidad del alumno solicitante | El consumidor invocó la carga sin declarar quién la pide | Aportar la identidad del solicitante, que el consumidor ya autenticó. **Un trabajo sin dueño no es un trabajo**, y la pertenencia es lo único que después va a acotar quién lo ve |

**El dato obligatorio ausente en la carga.** `DATO_OBLIGATORIO_AUSENTE` tiene su entrada única en §3.1 y vuelve a declararse acá con otro alcance: lo que falta es **el nombre o la fecha del trabajo**, y la fecha en cuestión es la que **declara el alumno**, no un sello del reloj. Esta capa propaga el rechazo del dominio y no materializa nada.

### 3.5 CU-04005 Enviar un trabajo e interpretar su texto

Conviven las tres formas de terminación, y es el único caso de uso donde eso pasa. **Ninguna condición modifica el texto original**, ni siquiera cuando la interpretación falla.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ENVIO_FUERA_DE_BORRADOR` | Conflicto de estado | Sólo se envía un trabajo en `Borrador` | Se pidió enviar un trabajo en **estado `Pendiente`** | No reenviar. Esta capa propaga el rechazo del dominio y conserva el estado actual. Enviar es la **única acción de guardado** del alumno, y un trabajo que ya salió de sus manos no vuelve al envío. **El motivo está acotado al estado `Pendiente`**: los dos estados de cierre devuelven el de la fila siguiente |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | Conflicto de estado | De un trabajo `Finalizado` o `Rechazado` no sale ninguna transición | Se pidió enviar un trabajo que ya tuvo desenlace | No hay camino de vuelta: los dos estados de cierre no cambian de estado ni de contenido (RN-04010). **Corregir un rechazo significa cargar un trabajo nuevo.** El dominio devuelve **este** motivo y no el anterior para los dos estados de cierre, y no los distingue entre sí; el criterio CA-07 lo ancla. Esta condición vuelve a declararse en CU-04008, sobre un desenlace nuevo |
| `INTERPRETACION_NO_DISPONIBLE` | Error transitorio | El puerto de validación de figuras no pudo completar la interpretación | El adaptador que implementa el puerto no respondió o no pudo resolver | Informar que la interpretación no está disponible y **no presentar el trabajo como interpretado**. El caso de uso termina de forma controlada: el trabajo queda en `Borrador` con su texto intacto y se devuelve el estado degradado. **No se inventan observaciones y no se pasa a estado `Pendiente`.** Esta capa no reintenta: si corresponde reintentar, lo decide el consumidor |
| `CONJUNTO_DE_PIEZAS_MAL_FORMADO` | Error interno | El conjunto de piezas que devolvió el validador no es adoptable | El dominio lo rechazó por posición inválida —repetida, negativa o fuera del rango declarado—, tipo de pieza desconocido, familia que contradice al tipo, o reconstrucción sobre un trabajo terminal | **Corregir el adaptador del puerto de validación, no la invocación.** Es una **condición agregada** que reúne cuatro rechazos del dominio (§2.5); el motivo fino está en la 02 del dominio. El caso de uso no materializa nada. Atención a la causa más frecuente: **la posición se valida contra la cantidad de figuras del conjunto raíz**, que el validador declara, y no contra la cantidad de piezas adoptadas |
| `OBSERVACION_MAL_FORMADA` | Error interno | El conjunto de observaciones que devolvió el validador no es adoptable | El dominio lo rechazó por especie desconocida, error sin ubicación, advertencia sin los dos valores u observación sobre una posición inexistente | **Corregir el adaptador del puerto de validación, no la invocación.** Es la condición agregada **simétrica** a la anterior, y reúne otros cuatro rechazos del dominio (§2.5). Un conjunto mal formado es un defecto del validador y no un resultado que el alumno deba ver. **Una posición reservada no es una posición inexistente**: la de una figura que no se pudo reconstruir sí pertenece al rango declarado y sí admite observación |

**La negativa por pertenencia en el envío.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` tiene su entrada única en §3.4 y vuelve a declararse acá con una precisión propia que conviene no perder: cuando el solicitante no es el dueño, el caso de uso devuelve el motivo **sin invocar al validador**. El criterio de aceptación CA-05 lo verifica contando 0 invocaciones del validador doble, y es la prueba de que la comprobación ocurre antes y no después.

**El dato que hace comprobable todo lo demás.** El puerto de validación devuelve, además de las piezas y las observaciones, **la cantidad de figuras del conjunto raíz**, incluidas las que no se pudieron reconstruir, y este caso de uso la hace viajar hasta el dominio (CU-04005 §4 pasos 3 y 4). **No es derivable de las piezas adoptadas**, porque ésas admiten huecos, y sin ella el dominio no tiene rango contra el cual validar la posición de una observación. Las dos condiciones agregadas de este caso de uso dependen de ese rango: sin él, `OBSERVACION_MAL_FORMADA` no tendría contra qué evaluar «posición inexistente» y la posición reservada de una figura no reconstruida dejaría de ser comprobable.

### 3.6 CU-04006 Consultar los trabajos propios del alumno

Forma de terminación: motivo de resultado. Las dos son consultas y no modifican nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `SOLICITANTE_NO_DECLARADO` | Entrada inválida | La consulta no trae la identidad del alumno solicitante | El consumidor pidió el listado o el detalle sin declarar quién lo pide | Aportar la identidad del solicitante, ya autenticada por la capa externa. El caso de uso **termina sin consultar el repositorio de trabajos**: un listado sin dueño declarado sería el listado de todos, y ése es exactamente el resultado que la separación entre alumnos viene a impedir |

**La negativa por pertenencia en la consulta.** `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, con entrada única en §3.4, es acá donde su traducción queda declarada con más precisión: el consumidor la traduce a «no encontrado» y **nunca** a «no autorizado», porque confirmar que el recurso existe pero es ajeno ya sería informar de más (CU-04006 §6). CA-03 exige que el motivo sea el mismo que devolvería para un identificador inexistente.

**Un listado vacío no es una condición de error.** El alumno sin ningún trabajo recibe 0 trabajos y ningún motivo (CU-04006 FA-03, CA-05). Tratarlo como error es un defecto del consumidor.

### 3.7 CU-04007 Revisar los trabajos de la comisión

Forma de terminación: motivo de resultado. Las tres son consultas y no modifican nada.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | Conflicto de alcance | El trabajo está en `Borrador` y no entra en el alcance del administrador | Se pidió el detalle de un borrador | Excluirlo de la vista de revisión. **A diferencia de la negativa por pertenencia, ésta no oculta la existencia del trabajo**: expresa que está fuera de su flujo de trabajo (RN-04011). El recorte se traslada al puerto y no se aplica después sobre un conjunto mayor, de modo que en el listado el borrador ni siquiera aparece ni se cuenta. Esta condición vuelve a declararse en CU-04008 y en CU-04009 |
| `TRABAJO_INEXISTENTE` | Recurso ausente | El identificador no corresponde a ningún trabajo | El identificador pedido no existe | Verificar el identificador. **Acá no hay recurso ajeno que proteger**: el administrador ve todo lo que no es borrador, y por eso este motivo es distinto del de pertenencia y no lo reemplaza. Comparar con `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, que sí oculta, es la mejor forma de entender §2.4 |

**La negativa por facultad en la revisión.** `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, con entrada única en §3.2, se declara acá con una precisión propia: el caso de uso **no consulta el repositorio de trabajos** cuando el papel no es `Administrador`, y CA-03 lo verifica contando 0 consultas. La consulta del alumno sobre sus propios trabajos es CU-04006, y encaminar hacia allí es lo que corresponde.

### 3.8 CU-04008 Dar desenlace a un trabajo

Forma de terminación: negativa sin escritura. En los cinco casos el trabajo queda exactamente como estaba, con su estado y su comentario anteriores.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `DESENLACE_FUERA_DE_PENDIENTE` | Conflicto de estado | Sólo se aprueba o se rechaza un trabajo en estado `Pendiente` | El trabajo está en otro estado | Esperar a que el trabajo sea enviado y su texto verifique. Esta capa propaga el rechazo del dominio y conserva el estado actual. Un trabajo en estado `Pendiente` es, por RN-04005, uno cuyo texto no trajo errores de validación: es la precondición de todo desenlace |
| `DESENLACE_DESCONOCIDO` | Entrada inválida | El desenlace pedido no es aprobar ni rechazar | Llegó un tercer valor | Usar uno de los dos. Aprobar lleva a `Finalizado` y rechazar a `Rechazado`; los dos admiten comentario opcional y los dos son terminales. El caso de uso termina sin tocar el trabajo |

**Las otras tres negativas de este caso de uso** tienen entrada única en otras secciones y se declaran acá con su precisión propia:

- `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` (§3.2): **la facultad no se delega, ni siquiera sobre el trabajo propio.** Un alumno que intente aprobar su propio trabajo recibe esta negativa, y el caso de uso no recupera ni modifica el trabajo (CA-03). Se verifica acá y no en la pantalla: un alumno que fuerce la petición contra el servicio de datos tiene que ser rechazado igual.
- `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` (§3.7): un borrador no se aprueba ni se rechaza, **y el administrador ni siquiera lo ve** (CA-05). Este caso de uso comprueba el alcance **antes** que el desenlace, y por eso devuelve este motivo y no el de estado.
- `TRANSICION_DESDE_ESTADO_TERMINAL` (§3.5): el trabajo ya tuvo desenlace. No se corrige una aprobación ni se revisa un rechazo; lo único que un trabajo terminal admite es que el administrador lo elimine, por CU-04009.

### 3.9 CU-04009 Eliminar un trabajo

Forma de terminación: negativa sin escritura. Ninguna deja el trabajo a medio retirar: o se va entero con sus piezas y sus observaciones, o no se toca.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `PAPEL_NO_RECONOCIDO` | Entrada inválida | El papel declarado no pertenece al conjunto cerrado de dos valores | El consumidor declaró un papel distinto de `Alumno` o `Administrador` | Declarar uno de los dos. El caso de uso **termina sin evaluar ninguna de las dos resoluciones**, porque elegir la resolución por el papel es la única decisión propia de esta capa acá, y sin papel válido no hay resolución que elegir (RN-04001, papeles fijos) |

**Las tres negativas restantes de este caso de uso**, con entrada única en otras secciones, y que juntas explican por qué los dos alcances conviven en un solo contrato:

- `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` (§3.4): el alumno pide eliminar un trabajo ajeno, o un identificador que no existe. **No se retira nada** y el consumidor lo traduce a «no encontrado», nunca a «no autorizado». CA-03 exige que el motivo sea el mismo que para un identificador inexistente.
- `OPERACION_FUERA_DE_BORRADOR` (§3.4): el alumno pide eliminar un trabajo propio en estado `Pendiente`, `Finalizado` o `Rechazado`. Es un motivo distinto del anterior porque acá la existencia ya está admitida para su dueño. **Un trabajo `Rechazado` queda como registro del intento**, y sólo el administrador puede quitarlo.
- `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` (§3.7): el administrador pide eliminar un borrador. Los borradores no forman parte de su flujo de trabajo, **ni para verlos ni para quitarlos**.

Los dos alcances son opuestos y por eso conviven: al alumno lo acotan la pertenencia y el borrador; al administrador lo acota exactamente lo contrario, todo menos el borrador.

### 3.10 CU-04010 Configurar la cuenta de administrador

Es la **configuración del administrador en el primer arranque**, el otro camino de alta (§1.4). Forma de terminación: negativa sin escritura. En los cinco casos no se constituye ninguna cuenta y la instancia sigue sin administrador, de modo que este mismo contrato vuelve a estar disponible.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `ADMINISTRADOR_YA_CONFIGURADO` | Conflicto de estado | Ya existe una cuenta con papel `Administrador` | Se pidió configurar un administrador habiendo uno | No hay camino: la instancia tiene exactamente uno y **la ventana de alta se cierra con la primera configuración y no vuelve a abrirse** (RN-04001). El caso de uso no consulta siquiera el correo. Es también el motivo que el dominio devuelve si la ausencia de administrador no se le declara |
| `CONFIGURACION_SIN_CREDENCIAL` | Entrada inválida | La configuración del administrador exige credencial derivada | No se aportó credencial derivada, o el valor llegó vacío | Aportar la credencial **ya derivada**: la contraseña en claro no atraviesa esta capa. **Es lo opuesto al auto-registro**, donde la credencial está prohibida (§1.4): una cuenta de administrador sin credencial no podría entrar, y no hay ninguna otra cuenta que pudiera resolverlo |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | Entrada inválida | El estado inicial de **este camino** no se elige | Se pidió constituir la cuenta de administrador en un estado distinto de `Habilitado` | Invocar sin pedir estado: lo fija el dominio. **Mismo motivo, causa opuesta en CU-04001**, donde el estado impuesto es `Pendiente`: ver §3.1 y §1.4. Una cuenta de administrador `Pendiente` o `Bloqueado` dejaría a la instancia sin salida, porque no obtendría acceso y nadie podría habilitarla |

**Las dos negativas que este caso de uso comparte con el auto-registro**, con entrada única en §3.1 y la misma causa acá:

- `CORREO_YA_REGISTRADO`: el correo del administrador ya pertenece a otra cuenta. No se constituye nada y **no se informa el papel ni el estado de la cuenta que lo ocupa**.
- `DATO_OBLIGATORIO_AUSENTE`: falta el correo, el nombre o el apellido. Esta capa propaga el motivo del dominio.

**Un criterio de este caso de uso que conviene conocer aunque no produzca ninguna condición.** CA-02 encadena la configuración con la consulta de admisibilidad de CU-04003 y exige que devuelva admisible **con 0 motivos**: el administrador entra inmediatamente después de configurarse. Es la prueba de que el primer arranque es recorrible de punta a punta, y el defecto que la partición en dos caminos de alta vino a cerrar.

### 3.11 CU-04011 Resetear la contraseña de un alumno

Es el **reseteo de contraseña por el administrador** (F-26 del `PRODUCT-INTAKE` 1.7). Forma de terminación: negativa sin escritura. Ninguna deja efecto parcial: el reseteo escribe credencial, marca y sello, o no escribe nada. **En ningún caso se retira un trabajo**: resetear no es dar de baja y no dispara RN-04007.

| Motivo | Categoría | Mensaje | Causa probable | Acción sugerida |
| --- | --- | --- | --- | --- |
| `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` | Conflicto de facultad | El reseteo se ejerce sobre cuentas con papel `Alumno` | Se pidió resetear la contraseña de la cuenta con papel `Administrador` | No hay camino por acá: el administrador administra su propia credencial por el reemplazo de CU-04003, declarando verificada la vigente. El acotamiento es una **decisión derivada** de esta capa, declarada con su fundamento en CU-04011 §10: un reseteo sobre sí mismo dejaría al único administrador confinado por INV-09, con la instancia sin gobierno y sin ninguna otra cuenta que pudiera resolverlo |

**Las tres negativas que este caso de uso comparte con otros**, con entrada única donde aparecen primero y la misma causa acá:

- `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` (§3.2): quien pide el reseteo no tiene el papel. No se recupera la cuenta destino ni se toca ninguna credencial.
- `CUENTA_INEXISTENTE` (§3.2): el puerto no encuentra la cuenta destino. **Acá tampoco oculta nada**, por el mismo motivo que en CU-04002: la operación ya exigió la facultad de administrador.
- `VALOR_DERIVADO_VACIO` (§3.3): la contraseña provisoria llegó vacía. **Desde la emisión 1.6 también lo declara CU-04002**, en la habilitación, por la misma causa. Esta capa nunca la conoce en claro. **Desde que la provisoria la produce el sistema y no la escribe el administrador, esta condición ya no puede nacer de lo que escriba una persona**, sino de un defecto de quien la produce; se conserva catalogada igual, porque suponerla imposible es como se termina escribiendo una credencial vacía.

**Y dos negativas que este caso de uso dejó de declarar, escritas acá para que nadie las reponga.**

**`CREDENCIAL_NO_ESTABLECIDA` salió del catálogo entero con la emisión 1.6.** Figuraba acá sobre la cuenta destino que nunca había fijado credencial, y en §3.3 como motivo de resultado del primer ingreso. **RN-04016** (`PRODUCT-INTAKE` 1.13 §4.1) hace que habilitar produzca y fije la contraseña provisoria: ninguna cuenta llega a estar habilitada sin credencial, y el reseteo sobre una cuenta `Pendiente` sin credencial simplemente la fija. **No es un rechazo que se relaje: es una causa que dejó de existir**, y el identificador **no se recicla**. Quien busque el encaminamiento del primer ingreso encuentra `CAMBIO_DE_CONTRASENA_PENDIENTE` en §3.3.

**`CUENTA_NO_HABILITADA_PARA_CREDENCIAL` dejó de declararse acá con la emisión 1.2.** Figuraba en CU-04011 sobre la cuenta destino `Pendiente` o `Bloqueado`. **El Product Owner resolvió que el reseteo no exige que la cuenta esté habilitada** —es una operación sobre la credencial, no toca el estado de la cuenta, y el administrador resetea y habilita en el orden que quiera—, de modo que la condición **no se relajó ni se renombró: dejó de existir para este caso de uso**. Sigue vigente en CU-04003, donde la cuenta que fija o reemplaza **su propia** credencial sí tiene que estar habilitada, y su entrada de §3.3 no cambia.

**Un criterio de este caso de uso que conviene conocer aunque no produzca ninguna condición.** Este caso de uso **no invoca el reemplazo de credencial del dominio, sino su operación de reseteo**, que no exige que se declare verificada la credencial vigente y no exige estado `Habilitado`. La versión anterior de esta nota describía lo contrario —el reemplazo sostenido por la verificación de facultad en lugar de por una comparación de contraseñas—, y la corrección está en CU-04011 §10, para que nadie la lea como un atajo ni la reponga: el administrador no conoce la contraseña del alumno y no la conocerá, y lo que autoriza la operación de este lado sigue siendo la facultad.

## 4. Tono y voz

Coherente con la guía de estilo del producto: español rioplatense neutro técnico, sin marketing y sin emojis.

| Regla | Sí | No |
| --- | --- | --- |
| Describir la comprobación, no juzgar a quien invocó | «La operación requiere el papel `Administrador`» | «Olvidaste comprobar el papel» |
| Nombrar la entidad y el estado con el vocabulario del dominio | «Sólo se envía un trabajo en `Borrador`» | «El registro está en un estado no editable» |
| Decir la acción en imperativo, y del lado que corresponde | «Corregir el adaptador del puerto de validación» | «El sistema debería haber validado antes» |
| Calificar siempre `Pendiente` | «cuenta `Pendiente`», «trabajo en estado `Pendiente`» | «pendiente» a secas |
| No prometer lo que esta capa no hace | «Informar que la interpretación no está disponible» | «Reintentar en unos segundos» |
| No confesar la pertenencia | «No encontrado» | «No tenés permiso sobre ese trabajo» |
| Nombrar el camino de alta cuando la regla es opuesta en el otro | «El **auto-registro** no admite credencial derivada» | «El alta no admite credencial derivada» |

Dos excepciones declaradas a la regla de calificación de `Pendiente`, que no son defectos: **los nombres de los motivos son identificadores literales del contrato** y no se califican ni se traducen —`CUENTA_PENDIENTE` se escribe así, y su enunciado en prosa sí califica—, y las enumeraciones del conjunto cerrado de estados, donde el atributo enunciado ya fija el referente. Es la excepción que `Glosario-Funcional.md` §3.3 ya declara, y calificarla sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

## 5. Localización

**Esta capa no localiza nada.** Política, en tres reglas:

1. **Los motivos son identificadores estables**, en mayúsculas y sin acentos, y **no se traducen nunca**. Son parte de la superficie pública: renombrar uno es un cambio incompatible para los consumidores y rompe su compilación, que es la señal más temprana posible (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Application). La §17 de cada caso de uso declara qué cambio sobre la enumeración es compatible: **agregar un motivo lo es si el consumidor tiene un camino por defecto**; quitar o resignificar uno, no.
2. **El texto que una persona lee no se compone acá.** La traducción de un motivo a mensaje y a respuesta de protocolo pertenece a `GeometriaFactory-Api` y a la superficie que lo muestra. Esa traducción está sujeta a la tabla de traducciones prohibidas de §2.4, que no es una recomendación de estilo sino una regla del producto.
3. **Un solo idioma en el producto v1**: español rioplatense. No hay compromiso de traducción y no hay catálogo de recursos que mantener. Si alguna vez lo hubiera, viviría en la capa que compone el mensaje y no acá.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.8 | 2026-08-11 | **Unificación de nomenclatura del reseteo: se resetea la contraseña de la cuenta, no la cuenta.** Corrección pedida por el Product Owner —«ese resetear cuenta hay que corregirlo por resetear clave de cuenta de usuario alumno»— y corregida primero en la fuente, `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.28**: leído literal, «resetear la cuenta» sugiere darla de baja y volver a darla de alta, que es exactamente el remedio que **F-26** vino a reemplazar. Acá se reescriben **1** ocurrencia a «resetear / reseteo **de la contraseña** de la cuenta» y «cuenta **con la contraseña reseteada**». El caso de la **cuenta de administrador** se reescribe como «resetear **la contraseña de** la cuenta de administrador», que sigue sin admitirse (**INV-08**, **RN-04015**): no se cambia el sujeto a «de alumno», que invertiría el sentido de la regla. No cambia ninguna regla ni su verificación, y **no se toca ningún identificador** de código de error ni de regla —`RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` y `CONTRATO_RESETEO_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` se conservan tal cual—. |
| 1.0 | 2026-08-09 | Emisión inicial. Cataloga las 27 condiciones distintas derivadas de la §6 de los nueve casos de uso de entonces, sobre 37 filas declaradas. Declara la distinción entre condición de error, observación y comentario; la taxonomía con «conflicto de facultad» y «conflicto de alcance» agregadas y justificadas; la forma de terminación como dimensión ortogonal; y la §2.4 con las tres negativas de autorización, su tabla de traducciones prohibidas y su procedimiento de decisión. |
| 1.0 | 2026-08-09 | **Correcciones de la ronda r1 del audit**, absorbidas sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. **Alineación con el 02 corregido**: el catálogo pasa de **27 a 34 condiciones distintas** sobre **48 filas y diez casos de uso**, con §3.10 nueva para CU-04010, la configuración del administrador. Altas: `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`, `ESTADO_INICIAL_NO_NEGOCIABLE`, `CONFIGURACION_SIN_CREDENCIAL`, `CREDENCIAL_YA_FIJADA`, `VALOR_DERIVADO_VACIO`, `TEXTO_ORIGINAL_ALTERADO` y `CONJUNTO_DE_PIEZAS_MAL_FORMADO`; `TRANSICION_DESDE_ESTADO_TERMINAL` pasa a declararse también en CU-04005 y su entrada se muda a §3.5, que es donde aparece primero; `ADMINISTRADOR_YA_CONFIGURADO` se muda de §3.1 a §3.10; `ENVIO_FUERA_DE_BORRADOR` acota su causa al estado `Pendiente`; `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` se acota al auto-registro. **§1.4 nueva**: los dos caminos de alta con sus cinco rasgos opuestos, y el tratamiento del motivo con **causas opuestas**, que lleva fila completa en dos subsecciones con remisión mutua, **adoptando la misma forma que el proyecto de código hermano**. **§2.5 nueva**: los rechazos del dominio que esta capa no puede producir —inalcanzables por construcción, equivalentes o agregados—, con su lugar de declaración en la 02 y las dos consecuencias para quien implementa. §3.5 transmite además la **cantidad de figuras del conjunto raíz** y por qué no es derivable de las piezas adoptadas. **H-08**: la fila de `DATO_OBLIGATORIO_AUSENTE` en §3.4 pasa a nota de prosa, y el preámbulo de §3 declara que la única fila excedente es la del motivo con causas opuestas: 35 filas para 34 condiciones. **H-09**: «Control de cambios» vuelve a §6 y «Cobertura y trazabilidad» pasa a §7, unificando la convención con `Guia-Onboarding-Developer.md`. **H-10**: «dentro de la misma solución de código» en §1.3. §2.1 declara además la **divergencia deliberada de clasificación** de `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` respecto del proyecto de código hermano, con su motivo. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, capacidad **F-26** con sus reglas **RN-04012** y **RN-04013** y el invariante **INV-09**. El catálogo pasa de **34 a 36 condiciones distintas** sobre **55 filas y once casos de uso**, con **§3.11 nueva** para CU-04011, el reseteo de contraseña por el administrador. Altas: `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`, con la **decisión derivada** que lo funda, y `CAMBIO_DE_CONTRASENA_PENDIENTE`, cuya entrada vive en §3.3 porque ahí está su única excepción. **§2.4** pasa de tres a **cuatro negativas de autorización**, con la precisión 5 nueva y un tercer paso en el procedimiento de decisión. **§2.1** actualiza dos recuentos de categoría —conflicto de estado de 11 a 12, conflicto de facultad de 2 a 3— y la nota de divergencia deja de declarar dos miembros donde ahora hay tres. **§3.3** declara que `CREDENCIAL_NO_ESTABLECIDA`, `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` y `VALOR_DERIVADO_VACIO` reaparecen en CU-04011, y que la primera **cambia de forma de terminación** entre los dos. §7 rehace los cuatro recuentos y la verificación mecánica en las dos direcciones. Sube minor: agrega dos condiciones y una sección de catálogo, sin cambiar la semántica de ninguna existente. |
| 1.0 | 2026-08-09 | **Corrección de la ronda r2 del audit, hallazgo H-18**, absorbida sin subir versión por `Master-Prompt.md` §5, con el documento en estado `Propuesto`. §2.1 **retira el argumento auxiliar inválido** con el que cerraba la divergencia de clasificación de `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO`: no existe la «correspondencia uno a uno» invocada entre la categoría de conflicto de facultad, que tiene dos miembros, y la negativa por facultad de §2.4, que es una sola. **La clasificación no cambia y el fundamento principal se conserva**, reforzado: el referente del papel cambia con la capa —acá es un dato del pedido de alta y no la facultad de quien pide, y CU-04001 no verifica facultad ni pertenencia—, con el paralelo explícito a `PAPEL_NO_RECONOCIDO`, que esta capa clasifica igual. Se agrega en su lugar la declaración de lo que la divergencia **no** se apoya, para que una ronda posterior no reponga el argumento retirado ni revierta por él la clasificación entera. |
| 1.2 | 2026-08-09 | **Absorbe dos decisiones del Product Owner sobre F-26**, que `CU-04011` 1.2 aplica. **Decisión A: resetear no exige que la cuenta esté habilitada**; **decisión B: la contraseña provisoria la produce el sistema y no la escribe el administrador**. **Baja de una aparición, no de una condición**: `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` **sale de CU-04011** —la causa dejó de existir, no se relajó ni se renombró— y vuelve a ser exclusiva de CU-04003, donde la cuenta que fija o reemplaza **su propia** credencial sí tiene que estar habilitada; su entrada de §3.3 **no cambia**. **§3.11** pasa de cinco negativas compartidas a **cuatro**, suma la nota que declara la negativa retirada para que no se reponga, precisa `VALOR_DERIVADO_VACIO` —ya no puede nacer de lo que escriba una persona— y **corrige la nota final**, que describía este caso de uso invocando el **reemplazo** del dominio sostenido por la verificación de facultad: CU-04011 pasa a invocar la **operación de reseteo** del dominio, que no exige credencial vigente verificada ni estado `Habilitado`. **§7 rehace los recuentos**: filas de condición de 55 a **54**, condiciones compartidas de doce a **once**, reapariciones de 19 a **18** y la fila de CU-04011 de seis a cinco. **Las condiciones distintas catalogadas siguen siendo 36 y las filas de tabla de §3, 37**: no se dio de baja ninguna entrada del catálogo. **Autor:** DX Lead (AG-03)|
| 1.3 | 2026-08-09 | **Cierra los hallazgos `F26-18` y las filas de este archivo del `F26-20`** del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, contra `PRODUCT-INTAKE` **1.10**. **`F26-18`**: **§2.5** se define como el conjunto de rechazos del dominio que esta capa **no puede producir**, y no tenía ninguno de `CU-02013` pese a que `CU-04011` lo orquesta desde su versión 1.2: entran `OPERACION_NO_APLICABLE_A_LA_CUENTA_DE_ADMINISTRADOR` —no llega a producirse, porque esta capa corta antes con `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`— y `RESETEO_CON_ARRASTRE_DE_TRABAJOS` —inalcanzable por construcción, porque la invocación nunca declara efecto sobre los trabajos—, y el recuento de §7.1 pasa de **16 a 18**. **`F26-20`**: §2.5 decía «los once casos de uso orquestan **doce** casos de uso del dominio», que son **trece**; §3 decía «**Doce** condiciones se declaran en más de un caso de uso» y su §7.1 ya declaraba **once**, con la undécima —no la duodécima— llevando fila completa por causas opuestas; el cuadre decía «36 + 19 = 55» y es **36 + 18 = 54**; **§7.3** omitía `CU-04011` en dos filas, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` y `VALOR_DERIVADO_VACIO`, que ese caso de uso sí declara en su §6; y **§7.4** citaba «CU-04001 a **CU-04010**» y «RN-04001 a **RN-04011**», que son `CU-04001` a `CU-04011` y `RN-04001` a `RN-04016`. Se anotan además las reglas nuevas donde corresponden en §7.3: `RN-04014` sobre `VALOR_DERIVADO_VACIO` y `RN-04015` sobre `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO`. **Ninguna entrada del catálogo se agrega ni se quita: las condiciones distintas siguen siendo 36 y las filas de §3, 37.** Sube minor. |
| 1.4 | 2026-08-10 | **Cierra la parte del hallazgo `F26-25` que alcanza a este archivo**, del informe de auditoría `SDD/Docs/Audit/F26-Propagacion-r1.md` 1.0, reabierta por `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0, que lo había declarado **no verificado**. **Verificado acá con `git show a2d5b22`**: ese commit cambió **dos pasajes** de este documento **sin fila propia y sin subir versión** —§2.5, «los diez casos de uso orquestan doce casos de uso del dominio» pasó a «los **once**», y la fila de `ESTADO_INICIAL_NO_NEGOCIABLE`, «ninguna de las **once** reglas enuncia con qué estado nace una cuenta» pasó a «las **trece**»—, y ninguna fila posterior los menciona. De los cuatro archivos que `F26-25` nombraba, los otros tres ya habían dejado su constancia; **éste era el que faltaba**. **No se reescribe ninguna fila histórica y no se toca ninguno de los dos pasajes**: los dos cambios son reales y siguen vigentes —esta capa tiene hoy once casos de uso, y las reglas del producto son **quince** desde el `PRODUCT-INTAKE` 1.10, de modo que el «trece» de esa fila quedó a su vez desactualizado y **también se corrige acá a quince**, contado sobre `RN-04001` a `RN-04016` en `GeometriaFactory-Domain`—. **Ninguna condición de error se agrega, se retira ni cambia de taxonomía**, y los recuentos de §7.1 no se mueven. Sube minor: repone una constancia omitida y corrige un recuento derivado. |
| 1.5 | 2026-08-10 | **Cierra la parte del hallazgo de forma que alcanza a este archivo**, detectada al propagar `SDD/Docs/Audit/F26-Propagacion-r2.md` 1.0 contra `PRODUCT-INTAKE` **1.11**. **§6 Control de cambios**: la fila **1.2** de este documento estaba **fuera de la tabla**, al final del archivo, detrás de §7.4, donde el renderizador la absorbía como una fila más de la tabla de trazabilidad del artefacto —dos columnas— y quedaba además **después** de la 1.3 y de la 1.4. Se reincorpora a la tabla de §6 en su lugar cronológico, entre la corrección de la ronda r2 y la 1.3, **sin alterar una palabra de lo que dice**; sus **cuatro celdas** se acomodan a las tres columnas de la tabla y el autor pasa a leerse dentro de la celda de cambios, con la misma forma que ya usó `GeometriaFactory-Web` `Wireframes-Panel-De-Cuentas.md` 1.3 al cerrar `F26-27`. **Ninguna condición de error se agrega, se retira ni cambia de taxonomía**, y ningún recuento de §7 se mueve. Sube minor: repara el renderizado de una tabla y ordena su historial. |
| 1.6 | 2026-08-10 | **Propagación del `PRODUCT-INTAKE` 1.13**, regla **RN-04016** y precisión de **F-04**: habilitar una cuenta produce y fija su contraseña provisoria y la deja con cambio de contraseña pendiente. **El recuento de condiciones distintas no cambia —siguen siendo 36— y la composición sí.** **§3.2** da de alta `HABILITACION_SIN_CREDENCIAL_PROVISORIA` y suma la reaparición de `VALOR_DERIVADO_VACIO`, con lo que CU-04002 pasa de 5 a **7** filas. **§3.3** retira `CREDENCIAL_NO_ESTABLECIDA`, cuya causa —cuenta habilitada sin credencial— dejó de ser posible, y CU-04003 pasa de 9 a **8** filas. **§3.11** retira la aparición de ese mismo motivo en el reseteo y pasa de 5 a **4** filas, con la nota que declara los **dos** motivos que este caso de uso dejó de declarar y por qué ninguno se recicla. **§2.1** actualiza los recuentos por categoría —entrada inválida **14**, recurso ausente **3**—. **§7.1** deja las filas declaradas en **54**, las condiciones compartidas en **10** y las reapariciones en **18**; **§7.2** rehace las tres filas de CU-04002, CU-04003 y CU-04011, con el total invariante en 54. **§7.3** reemplaza la fila de cobertura del motivo retirado por la del motivo nuevo. **0 condiciones inventadas y 0 condiciones de los casos de uso sin entrada.** Sube minor. |
| 1.7 | 2026-08-10 | **Cierra el hallazgo `C-08` (P2) del informe de auditoría `SDD/Docs/Audit/Coherencia-Corpus-r1.md` 1.0.** La cabecera de trazabilidad declaraba derivarse del `PRODUCT-INTAKE` **1.7**, versión archivada, y pasa a declarar la **1.14**, vigente. La **1.7** es la versión cuya letra sobre **RN-04013** e **INV-09** fue precisada en la 1.8 y corregida en la 1.14, que es exactamente el punto donde el corpus más se equivocó. Se revisó el cuerpo antes de mover la cabecera y **no arrastra ninguna decisión de las versiones intermedias**: no queda en él ningún recuento de «quince reglas» ni de «diecisiete códigos», ninguna cita a la exclusión **X-2** como vigente y ninguna afirmación de que la marca de cambio de contraseña pendiente la ponga únicamente el reseteo. **Ningún contenido normativo de este documento cambia: la corrección es de trazabilidad.** Sube minor. |

## 7. Cobertura y trazabilidad

### 7.1 Recuento

| Magnitud | Valor |
| --- | --- |
| Casos de uso de los que deriva el catálogo | 11 (CU-04001 a CU-04011) |
| Filas de condición declaradas en la §6 de los once casos de uso | 54 |
| Condiciones declaradas en más de un caso de uso | 10 (`CORREO_YA_REGISTRADO`, `DATO_OBLIGATORIO_AUSENTE`, `ESTADO_INICIAL_NO_NEGOCIABLE`, `FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_INEXISTENTE`, `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` y `VALOR_DERIVADO_VACIO`, que desde la emisión 1.6 comparten **CU-04002, CU-04003 y CU-04011**). **`CUENTA_NO_HABILITADA_PARA_CREDENCIAL` dejó de ser compartida** en la 1.2 y vuelve a ser exclusiva de CU-04003; **`CREDENCIAL_NO_ESTABLECIDA` salió del catálogo** en la 1.6 |
| Reapariciones, sobre esas diez | 18 |
| **Condiciones distintas catalogadas** | **36** |
| Filas de tabla en §3 | 37. La única excedente es `ESTADO_INICIAL_NO_NEGOCIABLE`, con fila completa en §3.1 y §3.10 por causas opuestas (§1.4) |
| Condiciones inventadas por esta categoría | **0** |
| Condiciones de los casos de uso sin entrada en el catálogo | **0** |
| Rechazos del dominio sin condición propia acá, declarados en §2.5 | **18**, ninguno de ellos condición de este catálogo. Los dos últimos son los de `CU-02013`, que entraron con la orquestación del reseteo |

Cuadre: 36 + 18 = 54.

### 7.2 Verificación mecánica de cobertura

La verificación se hizo en las dos direcciones, caso de uso por caso de uso, y su resultado se deja escrito para que una revisión posterior la pueda repetir sin rehacerla:

| CU | Filas en su §6 | Entradas nuevas en §3 | Condiciones ya catalogadas que reaparecen | Suma |
| --- | --- | --- | --- | --- |
| CU-04001 | 5 | 5 | 0 | 5 |
| CU-04002 | 7 | 6 | 1 (`VALOR_DERIVADO_VACIO`) | 7 |
| CU-04003 | 8 | 7 | 1 (`CUENTA_INEXISTENTE`) | 8 |
| CU-04004 | 5 | 4 | 1 (`DATO_OBLIGATORIO_AUSENTE`) | 5 |
| CU-04005 | 6 | 5 | 1 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`) | 6 |
| CU-04006 | 2 | 1 | 1 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`) | 2 |
| CU-04007 | 3 | 2 | 1 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`) | 3 |
| CU-04008 | 5 | 2 | 3 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`, `TRANSICION_DESDE_ESTADO_TERMINAL`) | 5 |
| CU-04009 | 4 | 1 | 3 (`TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`, `OPERACION_FUERA_DE_BORRADOR`, `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR`) | 4 |
| CU-04010 | 5 | 2 | 3 (`CORREO_YA_REGISTRADO`, `DATO_OBLIGATORIO_AUSENTE`, `ESTADO_INICIAL_NO_NEGOCIABLE`) | 5 |
| CU-04011 | 4 | 1 | 3 (`FACULTAD_DE_ADMINISTRADOR_REQUERIDA`, `CUENTA_INEXISTENTE`, `VALOR_DERIVADO_VACIO`) | 4 |
| **Total** | **54** | **36** | **18** | **54** |

`ESTADO_INICIAL_NO_NEGOCIABLE` se cuenta como entrada nueva en CU-04001 y como reaparición en CU-04010, igual que las otras ocho repetidas: **la segunda fila de tabla de §3.10 no altera el recuento de condiciones distintas**, sólo el de filas de tabla.

Las dos comprobaciones que cierran la verificación:

- **De caso de uso a catálogo.** Ninguna de las 54 filas quedó sin entrada: 36 dieron entrada nueva y 18 son reapariciones de una condición ya catalogada, cada una anotada con su caso de uso adicional.
- **De catálogo a caso de uso.** Ninguna de las 36 entradas de §3 existe sin una fila que la respalde en la §6 del caso de uso que la titula. **No hay ninguna condición inventada por esta categoría**, y en particular no se agregó ninguna a partir de los flujos alternativos: se recorrieron las **veintiuna citas de motivo** que aparecen en las §5 de los once casos de uso y todas corresponden a un motivo ya declarado en la §6 del mismo caso de uso. Tampoco se agregó ninguna a partir de §2.5: los dieciséis rechazos del dominio que esa sección enumera **no son condiciones de este catálogo** y no entran en ningún recuento.

Las apariciones adicionales no se catalogan dos veces, pero **sí llevan su precisión propia** cuando el caso de uso agrega una: la negativa por pertenencia que no invoca al validador (§3.5), la negativa por facultad que no consulta el repositorio de trabajos (§3.7), la facultad que no se delega ni sobre el trabajo propio y el alcance comprobado antes que el estado (§3.8), el tratamiento distinto de la cuenta inexistente en la consulta de admisibilidad (§3.3), el otro alcance del dato obligatorio ausente (§3.4), las dos negativas compartidas entre los dos caminos de alta (§3.10) y las cuatro que el reseteo comparte con CU-04002 y CU-04003, entre ellas `CREDENCIAL_NO_ESTABLECIDA`, que **cambia de forma de terminación** —motivo de resultado en CU-04003, negativa sin escritura en CU-04011— (§3.11).

### 7.3 Tabla de cobertura

| Motivo | CU que lo declara | Regla de negocio | Categoría | Forma de terminación |
| --- | --- | --- | --- | --- |
| `CORREO_YA_REGISTRADO` | CU-04001, CU-04010 | RN-04002 | Conflicto de estado | Negativa sin escritura |
| `DATO_OBLIGATORIO_AUSENTE` | CU-04001, CU-04004, CU-04010 | — | Entrada inválida | Negativa sin escritura |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | CU-04001 | — | Entrada inválida | Negativa sin escritura |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | CU-04001, CU-04010 | — (causas opuestas, §1.4) | Entrada inválida | Negativa sin escritura |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | CU-04001 | RN-04001 | Entrada inválida (§2.1) | Negativa sin escritura |
| `FACULTAD_DE_ADMINISTRADOR_REQUERIDA` | CU-04002, CU-04007, CU-04008, CU-04011 | RN-04001, RN-04010 | Conflicto de facultad | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `CONFIRMACION_DE_BAJA_NO_COINCIDE` | CU-04002 | RN-04007 | Entrada inválida | Negativa sin escritura |
| `TRANSICION_DE_CUENTA_NO_ADMITIDA` | CU-04002 | — | Conflicto de estado | Negativa sin escritura |
| `CUENTA_DE_ADMINISTRADOR_NO_ADMITE_BAJA` | CU-04002 | RN-04001 | Conflicto de facultad | Negativa sin escritura |
| `CUENTA_INEXISTENTE` | CU-04002, CU-04003, CU-04011 | — | Recurso ausente | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `CUENTA_PENDIENTE` | CU-04003 | RN-04006 | Conflicto de estado | Motivo de resultado |
| `CUENTA_BLOQUEADA` | CU-04003 | RN-04006 | Conflicto de estado | Motivo de resultado |
| `HABILITACION_SIN_CREDENCIAL_PROVISORIA` | CU-04002 | RN-04016, RN-04014 | Entrada inválida | Negativa sin escritura |
| `CUENTA_NO_HABILITADA_PARA_CREDENCIAL` | CU-04003 | RN-04006 | Conflicto de estado | Negativa sin escritura |
| `CAMBIO_DE_CONTRASENA_PENDIENTE` | CU-04003, y **cualquiera** por la comprobación transversal de §4 | RN-04013, INV-09 | Conflicto de estado | Negativa sin escritura |
| `RESETEO_ACOTADO_A_CUENTAS_DE_ALUMNO` | CU-04011 | RN-04015, RN-04001 | Conflicto de facultad | Negativa sin escritura |
| `CREDENCIAL_VIGENTE_NO_VERIFICADA` | CU-04003 | — | Entrada inválida | Negativa sin escritura |
| `CREDENCIAL_YA_FIJADA` | CU-04003 | — | Conflicto de estado | Negativa sin escritura |
| `VALOR_DERIVADO_VACIO` | CU-04003, CU-04011 | RN-04014 | Entrada inválida | Negativa sin escritura |
| `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE` | CU-04004, CU-04005, CU-04006, CU-04009 | RN-04003 | Recurso ausente | Negativa sin escritura y motivo de resultado, según el caso de uso |
| `OPERACION_FUERA_DE_BORRADOR` | CU-04004, CU-04009 | RN-04004 | Conflicto de estado | Negativa sin escritura |
| `TEXTO_ORIGINAL_ALTERADO` | CU-04004 | RN-04008 | Entrada inválida | Negativa sin escritura |
| `TRABAJO_SIN_DUENO` | CU-04004 | RN-04003 | Entrada inválida | Negativa sin escritura |
| `ENVIO_FUERA_DE_BORRADOR` | CU-04005 | RN-04005 | Conflicto de estado | Negativa sin escritura |
| `TRANSICION_DESDE_ESTADO_TERMINAL` | CU-04005, CU-04008 | RN-04010 | Conflicto de estado | Negativa sin escritura |
| `INTERPRETACION_NO_DISPONIBLE` | CU-04005 | RN-04008 | Error transitorio | Terminación degradada |
| `CONJUNTO_DE_PIEZAS_MAL_FORMADO` | CU-04005 | RN-04009 | Error interno | Negativa sin escritura |
| `OBSERVACION_MAL_FORMADA` | CU-04005 | RN-04009, RN-04005 | Error interno | Negativa sin escritura |
| `SOLICITANTE_NO_DECLARADO` | CU-04006 | RN-04003 | Entrada inválida | Motivo de resultado |
| `TRABAJO_FUERA_DEL_ALCANCE_DEL_ADMINISTRADOR` | CU-04007, CU-04008, CU-04009 | RN-04011, RN-04004 | Conflicto de alcance | Motivo de resultado y negativa sin escritura, según el caso de uso |
| `TRABAJO_INEXISTENTE` | CU-04007 | — | Recurso ausente | Motivo de resultado |
| `DESENLACE_FUERA_DE_PENDIENTE` | CU-04008 | RN-04010, RN-04005 | Conflicto de estado | Negativa sin escritura |
| `DESENLACE_DESCONOCIDO` | CU-04008 | RN-04010 | Entrada inválida | Negativa sin escritura |
| `PAPEL_NO_RECONOCIDO` | CU-04009 | RN-04001 | Entrada inválida | Negativa sin escritura |
| `ADMINISTRADOR_YA_CONFIGURADO` | CU-04010 | RN-04001 | Conflicto de estado | Negativa sin escritura |
| `CONFIGURACION_SIN_CREDENCIAL` | CU-04010 | RN-04006 | Entrada inválida | Negativa sin escritura |

Tres notas sobre las columnas, para que nadie las complete con atribuciones inventadas:

| Caso | Situación |
| --- | --- |
| `ESTADO_INICIAL_NO_NEGOCIABLE` sin regla de negocio | **Ninguna de las dieciséis reglas enuncia con qué estado nace una cuenta.** La atribución a RN-04001 se retiró aguas arriba: ese enunciado habla de la unicidad del administrador y de la ventana en la que su alta es posible, no del estado inicial. El origen está en el modelo de estados de cuenta del dominio y en los dos caminos de alta |
| RN-04008 sin condición que la haga cumplir por rechazo | Tiene una, `TEXTO_ORIGINAL_ALTERADO`, desde la corrección de esta ronda. Su otra mitad sigue siendo un **comportamiento** y no una comprobación: el texto no se reescribe, ni siquiera cuando la interpretación falla, y `INTERPRETACION_NO_DISPONIBLE` la cita como garantía y no como violación |
| Columnas con guion | No son un vacío a completar: hay condiciones que sostienen una precondición del contrato sin que ninguna regla de negocio las enuncie por separado, como `CREDENCIAL_VIGENTE_NO_VERIFICADA`, `CREDENCIAL_YA_FIJADA` o `TRABAJO_INEXISTENTE`. Inventarles una regla sería el defecto contrario al que este catálogo evita |

### 7.4 Trazabilidad del artefacto

**Quick-start: no aplicable en este documento, y el motivo es explícito.** El criterio de `Rules-UX-UI-DX.md` §6 pide un quick-start verificable en cada documento `dx-`; acá no corresponde porque este artefacto es del modo **reference** y se consulta por motivo, no se recorre de principio a fin: no hay una secuencia de pasos que produzca un primer resultado. El quick-start del proyecto de código es único y vive en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) §3, con su compromiso de verificación por punto de control en §3.2, y su recorrido guiado en [`Guia-Onboarding-Developer.md`](Guia-Onboarding-Developer.md) §2 y §3. Duplicarlo acá crearía una segunda fuente de verdad sobre pasos ejecutables, que es lo que se desincroniza primero. **No se da por cumplido: se declara no aplicable.**

| Dimensión | Referencia |
| --- | --- |
| Rol de intervención | Integrador por casos de uso, implementador de puertos y mantenedor de la capa ([`DX-Developer-Experience.md`](DX-Developer-Experience.md) §1.1) |
| Superficie pública que se documenta | Las 36 condiciones de error de los once contratos de uso, y las cuatro comprobaciones transversales de `Especificacion-Funcional.md` §4 |
| CU origen | CU-04001 a CU-04011, §6 de cada uno |
| Reglas de negocio relevantes | RN-04001 a RN-04016 de `GeometriaFactory-Domain`, con la correspondencia de §7.3 |
| Necesidades de negocio | NB-00001, NB-00002, NB-00003, NB-00004, NB-00005, NB-00009 |
| Wireframes asociados | N/A. `tiene_ui_final` == false |
| US a generar en 06 | US del catálogo mantenido junto al código; US de traducción de motivo a respuesta en `GeometriaFactory-Api`, con la tabla de traducciones prohibidas de §2.4 como criterio de aceptación; US de la indistinguibilidad de `TRABAJO_INEXISTENTE_PARA_EL_SOLICITANTE`; US del recorrido del primer arranque, que encadena CU-04010 con la admisibilidad de CU-04003 |
| Tests previstos en 08 | Una prueba unitaria con dobles por condición, **ninguna tocando la base de datos real**; dos pruebas de indistinguibilidad derivadas de CA-03 de CU-04006 y CA-03 de CU-04009; y la prueba de recorrido del primer arranque derivada de CA-02 de CU-04010 |
| Catálogo de diseño aplicado | N/A para variante DX |
| Configuración dirigida por esquema, primer arranque, acceso de operador único, identidad de versión | N/A. Ninguna de las cuatro extensiones aplica a este proyecto de código. **La configuración del administrador de CU-04010 no es la extensión de primer arranque**: acá es un contrato de uso, y la superficie de aprovisionamiento, si la hubiera, viviría en la categoría 03 de la pieza pública |
| Validación visual de maqueta y línea de base | N/A. `requiere_maqueta` == false |
