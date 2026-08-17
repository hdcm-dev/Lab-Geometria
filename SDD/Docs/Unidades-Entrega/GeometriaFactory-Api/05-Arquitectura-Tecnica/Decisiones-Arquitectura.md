# Decisiones de arquitectura — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Decisiones-Arquitectura.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `rest-api` · **Unidad de entrega principal del producto**
**Proyectos de código que la componen:** `GeometriaFactory-Api`, `GeometriaFactory-Domain`, `GeometriaFactory-Application`, `GeometriaFactory-Infrastructure` y `GeometriaFactory-Contracts`
**Trazabilidad upstream:** [`../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`](../../../../Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md) **2.1**
**Consolida a:** los documentos homónimos de las capas que componen la unidad, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase.** Cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Ninguna de las nueve secciones es común a las cuatro capas**, y es el único documento del inventario
donde eso pasa. No es un defecto: cada capa organiza **su** catálogo de decisiones por las categorías
que le aplican, y las categorías no coinciden porque las decisiones no coinciden.

Lo que sí conviene leer junto son las secciones de **categorías vacías**: el host declara **una**
categoría de decisión sin usar y `GeometriaFactory-Infrastructure` declara **dos**. Son declaraciones
de ausencia, y sólo tienen sentido contra el catálogo completo de la unidad.

---

## 1. Índice de ADR

### 1.1 `GeometriaFactory-Api`

Cada decisión vive en un archivo individual bajo [`Adrs/`](Adrs/). Este documento **no contiene el cuerpo de ninguna**: es el índice navegable.

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-00001](Adrs/ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) | Host delgado, con la composición de raíz como único lugar de ensamblado | Estilo | Propuesto | 2026-08-10 |
| [ADR-00002](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) | El formato de intercambio y su configuración, fijados para los dos extremos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-00003](Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) | Credencial firmada, papel exigido por punto y una guardia transversal sin excepciones sueltas | Seguridad | Propuesto | 2026-08-10 |
| [ADR-00004](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) | Dos traducciones en orden, con una tabla única y sin inventar códigos | Comunicación | Propuesto | 2026-08-10 |
| [ADR-00005](Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) | Sin paginación, con su condición de reingreso declarada | Comunicación | Propuesto | 2026-08-10 |
| [ADR-00006](Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) | Composición de raíz única: ciclos de vida y configuración en un solo lugar | Persistencia | Propuesto | 2026-08-10 |
| [ADR-00007](Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) | Arranque en dos fases, y un punto de salud que no exige acceso | Despliegue | Propuesto | 2026-08-10 |
| [ADR-00008](Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md) | Sin versionado de rutas, con despliegue conjunto como regla operativa | Despliegue | Propuesto | 2026-08-10 |

**Ocho ADR, ninguna superada y ninguna rechazada.**

### 1.2 `GeometriaFactory-Infrastructure`

Cada decisión vive en un archivo individual bajo [`Adrs/`](Adrs/). Este documento **no contiene el cuerpo de ninguna**: es el índice navegable.

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-06001](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Un adaptador por puerto, sin repositorio genérico y sin adaptador único | Estilo | Propuesto | 2026-08-10 |
| [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) | Archivo único, escritor único y una unidad de trabajo por operación | Persistencia | Propuesto | 2026-08-10 |
| [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) | Criterio de comparación de dos correos, y el índice que lo sostiene | Persistencia | Propuesto | 2026-08-10 |
| [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | Derivación de clave anclada, con sus parámetros versionados junto al valor guardado | Seguridad | Propuesto | 2026-08-10 |
| [ADR-06005](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | La contraseña provisoria: no adivinable, sin repetirse y transcribible de viva voz | Seguridad | Propuesto | 2026-08-10 |
| [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) | Lectura tolerante en un solo lugar, y una tabla de derivación por tipo | Estilo | Propuesto | 2026-08-10 |
| [ADR-06007](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) | Transformaciones de esquema al arrancar, con linaje inmutable y arranque detenido | Despliegue | Propuesto | 2026-08-10 |

**Siete ADR, ninguna superada y ninguna rechazada.** El mínimo para el tipo `library` es de **tres**; el número real lo determina el conjunto de decisiones que hubo que registrar, y acá pesan dos cosas que ninguna otra biblioteca del producto tiene: la persistencia y los dos mecanismos de seguridad.

## 2. Las cinco decisiones que el tipo `rest-api` exige, y dónde está cada una

### 2.1 `GeometriaFactory-Api`

La guía de la categoría fija un mínimo de **cinco** ADR para este tipo, con sus temas nombrados. Las cinco están, y esta tabla declara cuál las cubre para que la correspondencia no haya que deducirla.

| Tema exigido por el tipo | ADR que lo cubre |
| --- | --- |
| **Estilo** | [ADR-00001](Adrs/ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) |
| **Persistencia** | [ADR-00006](Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), que es donde la persistencia de este proyecto de código realmente vive: **toma de configuración la ubicación del almacén y fija los ciclos de vida**, porque **el modelo del dato lo tiene `GeometriaFactory-Infrastructure`** |
| **Autenticación** | [ADR-00003](Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) |
| **Paginación** | [ADR-00005](Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md), que la registra **como decisión de no paginar, con su condición de reingreso medible**. Una ausencia sin sustituto sería un pendiente; con sustituto es una decisión |
| **Manejo de errores** | [ADR-00004](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) |

Las **tres** restantes las agrega esta categoría porque el producto las necesitaba y ninguna fuente las tenía resueltas: el **formato de intercambio** ([ADR-00002](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md)), que dos proyectos de código reasignaron acá; el **arranque en dos fases** ([ADR-00007](Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md)); y la **política de versionado de la frontera** ([ADR-00008](Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md)).

## 3. La categoría de decisión que queda vacía, y por qué

### 3.1 `GeometriaFactory-Api`

| Categoría | Por qué no hay ninguna ADR |
| --- | --- |
| **Extensibilidad** | `tiene_extensibilidad` es **false** en este proyecto de código (`PRODUCT-MANIFEST` §5). El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de otro proyecto de código. Y hacia esta superficie **no hay extensión posible por diseño**: su único cliente legítimo está declarado, y agregar otro rompe `RA-01` |

**Observabilidad no tiene ADR propia, y no es un hueco.** Es el único proyecto de código del producto con `tiene_observabilidad_critica` en true, pero lo que el intake declara —registro estructurado de cada error y de cada intento de acceso rechazado, y ningún mensaje con direcciones de servicios internos— **no es una decisión abierta sino la contracara obligatoria de `RA-03`**, y vive donde corresponde: centralizada en [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §7 y exigida por [`ADR-00004`](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) §2 punto 6, con su métrica en §8. Las métricas numéricas que el flag habilita están en la tabla de NFR.

## 4. Qué decisión gobierna cada preocupación

### 4.1 `GeometriaFactory-Api`

Tabla de consulta rápida, para no tener que abrir las ocho.

| Si la pregunta es… | La respuesta está en |
| --- | --- |
| Qué puede y qué no puede hacer un punto de acceso | [ADR-00001](Adrs/ADR-00001-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) |
| Cómo se nombran los campos al serializar, y qué pasa con los nulos y con los conjuntos cerrados | [ADR-00002](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) |
| Qué pasa con un cuerpo que excede el límite de tamaño | [ADR-00002](Adrs/ADR-00002-Formato-De-Intercambio-Y-Su-Configuracion.md) §2 punto 6: **se rechaza, nunca se trunca** |
| Qué puntos quedan fuera de la guardia, y cuántos | [ADR-00003](Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md): **exactamente cuatro** |
| Cuánto dura una credencial firmada | [ADR-00003](Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) §2 punto 5, con el número abierto |
| Qué código de respuesta le toca a cada código del contrato | [ADR-00004](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) y la tabla de [`Contratos-REST.md`](Contratos-REST.md) §5 |
| Qué se hace cuando el conjunto cerrado no tiene código para un camino | [ADR-00004](Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md): el genérico, **y se declara el hueco** |
| Por qué el listado no se pagina, y cuándo habría que paginarlo | [ADR-00005](Adrs/ADR-00005-Sin-Paginacion-Con-Condicion-De-Reingreso-Declarada.md) |
| Dónde se conectan los cuatro puertos con sus adaptadores | [ADR-00006](Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) |
| Qué pasa si falta la clave de firma o el volumen del almacén | [ADR-00006](Adrs/ADR-00006-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) y [ADR-00007](Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md): **el servicio no arranca** |
| Qué responde el punto de salud, y qué no responde | [ADR-00007](Adrs/ADR-00007-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) |
| Qué reemplaza al versionado de rutas | [ADR-00008](Adrs/ADR-00008-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md): el **despliegue conjunto** |

### 4.2 `GeometriaFactory-Infrastructure`

Tabla de consulta rápida, para no tener que abrir las siete.

| Si la pregunta es… | La respuesta está en |
| --- | --- |
| Cuántos adaptadores hay y quién los conecta | [ADR-06001](Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) |
| Qué pasa si el almacén está tomado, o si una baja se interrumpe | [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) |
| Con qué zona horaria y con qué precisión se guardan los sellos | [ADR-06002](Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) §2 punto 4 |
| Cuándo dos correos son el mismo correo | [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) |
| Cómo se nombra el adaptador del puerto que todavía no tiene nombre | [ADR-06003](Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6 |
| Qué se guarda junto al valor derivado de una credencial, y qué pasa si falta la clave de firma | [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) |
| Cuánto dura un acceso firmado | [ADR-06004](Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) §2 punto 5, con el número abierto |
| Cuántos caracteres tiene una provisoria y de dónde salen | [ADR-06005](Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) |
| De dónde sale el área derivada de una pieza volumétrica | [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) y [`Flujo-Ejecucion.md`](Flujo-Ejecucion.md) §5 |
| Quién impone el límite de tamaño del texto | [ADR-06006](Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3: el borde del proceso, y rechaza, nunca trunca |
| Qué pasa si el esquema del almacén no corresponde | [ADR-06007](Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) |

## 5. Qué es este documento

### 5.1 `GeometriaFactory-Domain`

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Domain`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a estado `Superado por ADR-YY` sin reescribirse.

### 5.2 `GeometriaFactory-Application`

Índice navegable de las decisiones de arquitectura de `GeometriaFactory-Application`. **No contiene el cuerpo de ninguna decisión**: cada una vive en su archivo individual bajo [`Adrs/`](Adrs/), es inmutable una vez aceptada y, si evoluciona, se registra en una ADR nueva y la anterior pasa a `Superado por ADR-YY` sin reescribirse.

## 6. ADR vigentes

### 6.1 `GeometriaFactory-Domain`

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-02001](Adrs/ADR-02001-Modelo-De-Dominio-Rico-Con-Invariantes-Explicitas.md) | Modelo de dominio rico con invariantes explícitas y cero dependencias | Estilo | Propuesto | 2026-08-10 |
| [ADR-02002](Adrs/ADR-02002-Superficie-Publica-De-Guardas-Y-Resultados-Tipados.md) | La superficie pública son guardas con resultado tipado, no excepciones | Estilo | Propuesto | 2026-08-10 |
| [ADR-02003](Adrs/ADR-02003-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por versionado semántico sin publicación, y estabilidad de la superficie | Despliegue | Propuesto | 2026-08-10 |
| [ADR-02004](Adrs/ADR-02004-Frontera-De-Autenticacion-Y-Autorizacion.md) | Frontera de autenticación: el dominio modela la condición y no el mecanismo | Seguridad | Propuesto | 2026-08-10 |
| [ADR-02005](Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) | Puerta única de admisibilidad para las guardas de acceso de la cuenta | Seguridad | Propuesto | 2026-08-10 |
| [ADR-02006](Adrs/ADR-02006-El-Dominio-No-Lee-El-Reloj-Ni-El-Conjunto.md) | El dominio no lee el reloj ni el conjunto de entidades: los dos entran por parámetro | Estilo | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna está superada y ninguna rechazada.

### 6.2 `GeometriaFactory-Application`

| ADR | Título | Categoría | Estado | Fecha |
| --- | --- | --- | --- | --- |
| [ADR-04001](Adrs/ADR-04001-Casos-De-Uso-Con-Inversion-De-Dependencias.md) | Casos de uso con inversión de dependencias, con una sola dependencia saliente | Estilo | Propuesto | 2026-08-10 |
| [ADR-04002](Adrs/ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) | Cuatro puertos, y qué significa que el cuarto no tenga nombre declarado | Comunicación | Propuesto | 2026-08-10 |
| [ADR-04003](Adrs/ADR-04003-Versionado-Y-Estabilidad-De-La-Superficie.md) | Versionado por compilación compartida y estabilidad de la superficie de dos caras | Despliegue | Propuesto | 2026-08-10 |
| [ADR-04004](Adrs/ADR-04004-Orden-Fijo-De-Las-Cuatro-Comprobaciones.md) | Orden fijo de las cuatro comprobaciones de autorización, en un único componente | Seguridad | Propuesto | 2026-08-10 |
| [ADR-04005](Adrs/ADR-04005-Un-Caso-De-Uso-Una-Unidad-De-Trabajo.md) | Un caso de uso, una unidad de trabajo: el alcance lo fija esta capa | Persistencia | Propuesto | 2026-08-10 |
| [ADR-04006](Adrs/ADR-04006-Resultado-Tipado-Y-Catalogo-Cerrado-De-Condiciones.md) | Resultado tipado hacia arriba, con el catálogo de treinta y seis condiciones como conjunto cerrado | Estilo | Propuesto | 2026-08-10 |

**Seis ADR**, sobre el mínimo de tres que la regla de la categoría fija para el tipo `library`. Ninguna superada, ninguna rechazada.

## 7. Por qué son seis y no tres

### 7.1 `GeometriaFactory-Domain`

El mínimo de tres para `library` cubre estilo, superficie pública y estrategia de versionado, y son ADR-02001, ADR-02002 y ADR-02003. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-02004 | El `PRODUCT-MANIFEST` §5 declara que corregir `tiene_auth` a true en este proyecto de código tiene por efecto que **la categoría 05 emita su ADR de autenticación**, que con el valor anterior se habría omitido |
| ADR-02005 | La categoría 02 tomó una decisión derivada —concentrar la guarda de INV-09 en la admisibilidad— y la declaró como tal. Una decisión de esa clase enterrada como viñeta del documento maestro es el anti-patrón que la regla de la categoría nombra primero |
| ADR-02006 | Que el momento y la unicidad entren por parámetro es lo que hace reproducible la batería del dominio y lo que sostiene las cero dependencias. Sin ADR, se lee como un detalle de firma en vez de como la decisión que es |

### 7.2 `GeometriaFactory-Application`

El mínimo de tres cubre estilo, superficie pública y estrategia de versionado, y acá son ADR-04001, ADR-04002 y ADR-04003 —la superficie pública de este proyecto de código tiene **dos caras**, y la de abajo son los puertos—. Las otras tres tienen origen declarado:

| ADR | Por qué existe |
| --- | --- |
| ADR-04004 | El flag `tiene_auth` es **true** en este proyecto de código, y el `PRODUCT-MANIFEST` §5 declara explícitamente que el efecto de esa corrección «es que la categoría 05 de esos dos proyectos de código emite su ADR de autenticación, que con el valor anterior se habría omitido». Además cierra la dependencia de disciplina que [`GeometriaFactory-Domain ADR-02005`](Adrs/ADR-02005-Guarda-Unica-De-Admisibilidad.md) §6 declaró que el dominio no podía cerrar: dejarla como viñeta habría enterrado la decisión que sostiene `INV-09` |
| ADR-04005 | El intake declara la persistencia de este proyecto de código como «no aplica directamente», pero le asigna **el alcance de la unidad de trabajo** (§17.1.P.4 · GeometriaFactory-Application). Es la única decisión de persistencia que esta capa toma, y sin ella el límite de consistencia quedaría sin dueño entre el dominio —que no abre unidades— y el adaptador —que no sabe qué operaciones forman un acto— |
| ADR-04006 | La categoría 03 catalogó **36** condiciones y ninguna fuente declaraba quién puede acuñar una nueva. Un catálogo que crece desde varios lugares deja de ser cerrado, y aguas abajo `GeometriaFactory-Api` tiene que traducir cada condición a una respuesta de protocolo |

## 8. Cobertura de las categorías de decisión

### 8.1 `GeometriaFactory-Domain`

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-02001, ADR-02002, ADR-02006 | — |
| Persistencia | **Ninguna** | El flag `tiene_persistencia` es false y el intake declara «no aplica» en §17.1.P.4 · GeometriaFactory-Domain. No hay decisión de persistencia que tomar acá |
| Comunicación | **Ninguna** | Este proyecto de código no expone protocolos ni cruza fronteras de proceso (`PRODUCT-INTAKE` §17.1.P.3 · GeometriaFactory-Domain) |
| Seguridad | ADR-02004, ADR-02005 | — |
| Observabilidad | **Ninguna** | El intake declara «sin observabilidad propia» en §17.1.P.10 · GeometriaFactory-Domain: no registra ni instrumenta |
| Despliegue | ADR-02003 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción y el versionado |
| Extensibilidad | **Ninguna** | El flag `tiene_extensibilidad` es false. El punto de extensión del producto es el contrato de la fachada del visor, no este proyecto de código |

Las cuatro categorías sin ADR se declaran vacías con su motivo, en lugar de omitirse, para que nadie las complete más adelante con decisiones inventadas.

### 8.2 `GeometriaFactory-Application`

| Categoría | ADR que la cubre | Observación |
| --- | --- | --- |
| Estilo | ADR-04001, ADR-04006 | — |
| Persistencia | ADR-04005 | No hay persistencia propia: lo que la ADR gobierna es el **alcance** de la unidad de trabajo y la forma de las dos lecturas del puerto de repositorio |
| Comunicación | ADR-04002 | Los cuatro puertos son la única frontera del proyecto de código. No hay comunicación entre procesos: §17.1.P.3 · GeometriaFactory-Application declara «no aplica» hacia afuera |
| Seguridad | ADR-04004 | Autorización, no autenticación: acá no se comparan contraseñas ni se emiten accesos |
| Observabilidad | **Ninguna** | `tiene_observabilidad_critica` es false y §17.1.P.10 · GeometriaFactory-Application no declara observabilidad propia. Esta capa no instrumenta: la correlación la lleva `GeometriaFactory-Api`, que es quien tiene petición que correlacionar |
| Despliegue | ADR-04003 | No hay unidad de despliegue propia; lo que la ADR gobierna es la construcción, el versionado y la asimetría de las dos caras |
| Extensibilidad | **Ninguna** | `tiene_extensibilidad` es false en el `PRODUCT-MANIFEST` §5. El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de `GeometriaFactory-Visor` |

Las dos categorías sin ADR se declaran vacías con su motivo, para que nadie las complete más adelante con decisiones inventadas.

## 9. Las dos categorías de decisión que quedan vacías, y por qué

### 9.1 `GeometriaFactory-Infrastructure`

| Categoría | Por qué no hay ninguna ADR |
| --- | --- |
| **Comunicación** | Este proyecto de código **no se comunica con nadie por un protocolo**. No expone puntos de acceso, no abre conexiones y no cruza la frontera del proceso: su único consumidor es la composición de raíz de `GeometriaFactory-Api`, por referencia de proyecto de código. La decisión de comunicación que le concierne —qué frontera hay y qué pasa por ella— ya la tomó `GeometriaFactory-Application` en su ADR-06002, y acá se implementa |
| **Observabilidad** | El flag `tiene_observabilidad_critica` es **false** en este proyecto de código (`PRODUCT-MANIFEST` §5), y el intake no declara métricas ni trazas para él. Lo único que sí declara —**registro del lado del servidor de todo error que se muestre al usuario**— no es una decisión de arquitectura de esta capa sino la contracara obligatoria de `RA-03`, y vive centralizada en [`Arquitectura-Unidad-Entrega.md`](Arquitectura-Unidad-Entrega.md) §7 |

**Extensibilidad tampoco tiene ADR, y tampoco es un hueco**: `tiene_extensibilidad` es false acá. El punto de extensión declarado del producto es el contrato de la fachada del visor, que es de otro proyecto de código.

## 10. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con las capas juntas. Los documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
