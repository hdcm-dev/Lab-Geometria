# Glosario funcional — GeometriaFactory-Api

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** Glosario-Funcional.md
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

**Las cuatro secciones son comunes a las cuatro capas**, y la consolidación es una **unión de
vocabulario**. Un término que dos capas definen distinto **no se unifica**: las dos definiciones
quedan, con su capa nombrada, porque una polisemia con contextos disjuntos no es un defecto
(`Vocabulario-Rules.md` §10) y borrar una de las dos sí lo sería.

---

## 1. Alcance de este glosario

### 1.1 `GeometriaFactory-Api`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Todo lo demás se **referencia** en §4, sin redefinirlo:

- `00-Contexto/Vision-Producto.md` §9 es el **glosario raíz de la cadena**.
- Los glosarios funcionales de los cuatro proyectos de código que este ensambla o transporta declaran el vocabulario de sus capas, y esta categoría lo usa con la misma semántica.

Rigen sin excepción las resoluciones de vocabulario del producto: **`Pendiente` va siempre calificado** —«cuenta `Pendiente`» o «trabajo en estado `Pendiente`»—, salvo en las enumeraciones del conjunto cerrado y en los identificadores literales; **«trabajo» no es «unidad de entrega»**; y **la palabra «proyecto» a secas no se usa**.

Una precisión de vocabulario propia de esta categoría, que conviene fijar antes de la tabla: **acá se dice «punto de acceso» y no «endpoint»**, y **«código de respuesta» y no «status»**. No es purismo idiomático: es que los dos términos ingleses arrastran connotaciones de otras herramientas, y el producto tiene ya tres referentes distintos para la palabra «código».

### 1.2 `GeometriaFactory-Domain`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código, y que aparecen en más de uno de sus artefactos. Los términos que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2` (`Vision-Producto.md` §9.3).

### 1.3 `GeometriaFactory-Application`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código y que aparecen en más de uno de sus artefactos. Los que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, y los que acuña la categoría 02 de `GeometriaFactory-Domain` se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

### 1.4 `GeometriaFactory-Infrastructure`

Acá se declaran únicamente los términos que **esta** categoría acuña para **este** proyecto de código y que aparecen en más de uno de sus artefactos. Los que ya declara `00-Contexto/Vision-Producto.md` §9, que es el glosario raíz de la cadena, y los que acuñan las categorías 02 de `GeometriaFactory-Domain` y de `GeometriaFactory-Application` se **referencian** en §4 y no se redefinen.

La resolución del choque de vocabulario del intake rige acá sin excepción: «proyecto de código» designa la unidad de compilación, **la palabra «proyecto» a secas no se usa**, y las dos unidades de la Actividad 1 que emiten el dato se nombran `Ejemplo1` y `Ejemplo2`.

## 2. Términos que esta categoría acuña

### 2.1 `GeometriaFactory-Api`

| Término canónico | Definición operativa | Artefactos donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| **Punto de acceso** | Cada una de las **quince** entradas de la superficie, identificada por `A-XX`, con su verbo, su ruta, el papel que exige y sus códigos de respuesta. **No es un caso de uso**: un caso de uso puede describir más de uno | `Definicion-Superficie-HTTP.md`, los doce casos de uso | «El punto» cuando ya está identificado. **No se dice «endpoint»** |
| **Superficie HTTP** | El conjunto de los quince puntos de acceso más las reglas que los gobiernan. **Es todo lo que este proyecto de código existe hacia afuera** | Los tres documentos y los casos de uso | «La superficie» cuando el proyecto de código está nombrado |
| **Código de respuesta** | El número con el que una petición termina. Son **diez** en toda la superficie | `Definicion-Superficie-HTTP.md`, los casos de uso | **No se dice «status» ni «status code»** |
| **Código del contrato** | Cada uno de los **diecisiete** identificadores del conjunto cerrado que declara el ensamblado de contratos. **Esta categoría no agrega ninguno** | `Definicion-Superficie-HTTP.md`, `CU-00009`, los casos de uso de puntos | «El código del conjunto cerrado». Ver §3.2 |
| **Las dos traducciones** | El par de conversiones que toda terminación fallida atraviesa: de motivo interno a código del contrato, y de código del contrato a código de respuesta | `Definicion-Superficie-HTTP.md` §5, `CU-00009` | — |
| **Guardia de admisión** | La condición que las peticiones de los once puntos que exigen acceso atraviesan antes de llegar a un caso de uso: acceso, papel y marca | `CU-00002`, `Definicion-Superficie-HTTP.md` | «La guardia» cuando ya está nombrada |
| **Papel exigido** | El papel que un punto de acceso comprueba sobre el acceso firmado. **No es la autorización completa**, que se hace sobre el dato recuperado y vive en otra capa | `Definicion-Superficie-HTTP.md` §3, `CU-00002` | — |
| **Composición de raíz** | El único lugar del producto donde los puertos se encuentran con sus adaptadores y donde entra la configuración del despliegue | `CU-00010`, `Especificacion-Funcional.md` §3 | «La composición» |
| **Arranque detenido** | La forma de terminación del arranque cuando el almacén no queda en condiciones: **el servicio no atiende ninguna petición**. El término lo acuñó `GeometriaFactory-Infrastructure` y acá se usa con la misma semántica | `CU-00011` | — |
| **Ruta propuesta** | Una ruta de la tabla de puntos de acceso que **esta categoría deriva** y que ninguna fuente declara. Va rotulada fila por fila | `Definicion-Superficie-HTTP.md` §3 | «Propuesta derivada» |
| **Colección de peticiones** | La demostración ejecutable del proyecto de código: la muestra que recorre la superficie con los ocho escenarios como cuerpo, en tres pasos | `CU-00012` | «La colección». Es la muestra **S-2** del producto |
| **Señal que no es un fallo** | Un resultado que otro producto trataría como error y que acá viaja en una respuesta exitosa: el texto que no verifica y el listado vacío. Son **dos** | `Definicion-Superficie-HTTP.md` §5, `CU-00006`, `CU-00007` | «Señal declarada», que es como la nombra el ensamblado de contratos |
| **Hueco del conjunto cerrado** | Un camino que las capas de adentro distinguen y para el que **el contrato no declara código propio**, de modo que sólo queda el genérico. Son **dos**, y están elevados al Product Owner | `CU-00009` §10, `Definicion-Superficie-HTTP.md` §9 | — |

### 2.2 `GeometriaFactory-Domain`

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Alumno | Entidad del dominio que representa a la persona de la comisión con identidad propia dentro del laboratorio y a la que pertenecen trabajos | `Definicion-Modelo-De-Dominio.md`, CU-02001 a CU-02004, CU-02009, RN-02001, RN-02002, RN-02006, RN-02007 | — |
| Papel | Atributo del alumno que vale `Alumno` o `Administrador`. Es un conjunto cerrado de dos valores, sin permisos configurables. **Determina cuál de los dos caminos de alta constituye la cuenta** | `Definicion-Modelo-De-Dominio.md`, CU-02001, CU-02002, CU-02004, CU-02010, CU-02011, CU-02012, RN-02001, RN-02010 | «Rol» en las fuentes técnicas. **Se usa «papel»**, y «rol» queda reservado al encabezado normativo de la tabla de actores: ver §3.4 |
| Estado de cuenta | Atributo del alumno que vale `Pendiente`, `Habilitado` o `Bloqueado`, con transiciones declaradas y **con un valor inicial que depende del camino de alta**. **No se confunde con el estado del trabajo**, que es otro conjunto cerrado: ver §3.3 | `Definicion-Modelo-De-Dominio.md`, CU-02001 a CU-02004, CU-02012, RN-02001, RN-02006, RN-02007 | — |
| Credencial derivada | Valor derivado de la contraseña del alumno, que el dominio recibe ya derivado y nunca en claro. En el auto-registro, **sin valor mientras la cuenta está `Pendiente` y con valor desde el acto de habilitación** (RN-02016); con valor desde el alta en la configuración del administrador | `Definicion-Modelo-De-Dominio.md`, CU-02002, CU-02003, CU-02004, CU-02012, RN-02006, RN-02016 | «Hash de contraseña» en las fuentes técnicas |
| Admisibilidad de la cuenta | Resultado de evaluar si una cuenta admite acceso al laboratorio, con su motivo cuando no lo admite | `Definicion-Modelo-De-Dominio.md`, CU-02004, RN-02006 | — |
| Camino de alta | Cada una de las dos formas en que se constituye una cuenta, con estado inicial y tratamiento de la credencial propios: el **auto-registro del alumno** (CU-02001), que nace `Pendiente` y sin credencial, y la **configuración del administrador** en el primer arranque (CU-02012), que nace `Habilitado` y con credencial | `Definicion-Modelo-De-Dominio.md`, CU-02001, CU-02003, CU-02012, RN-02001, RN-02002 | — |
| Baja de la cuenta | Operación destructiva e irreversible que elimina la cuenta y todos sus trabajos, cualquiera sea el estado de cada uno. No es un estado de cuenta, y **no es el reseteo de contraseña**: ver esa entrada | `Definicion-Modelo-De-Dominio.md`, CU-02002, RN-02001, RN-02007, RN-02012 | «Baja física» en las fuentes |
| Contraseña provisoria | Contraseña que **el sistema produce** al **habilitar** una cuenta de alumno (RN-02016) o al **resetear su contraseña** (RN-02014), que el administrador le comunica por fuera del producto y que la cuenta está obligada a reemplazar antes de ejercer cualquier otra capacidad. El dominio nunca la conoce ni la produce: llega ya derivada | `Definicion-Modelo-De-Dominio.md`, CU-02002, CU-02013, CU-02003, CU-02004, RN-02012, RN-02013, RN-02016 | — |
| Marca de cambio de contraseña pendiente | Atributo del alumno, puesto o levantado, que declara que su credencial vigente es una contraseña provisoria. La ponen **las dos** operaciones que producen una contraseña provisoria —la **habilitación** (CU-02002, RN-02016) y el **reseteo** (CU-02013, RN-02014)— y la levanta **únicamente** el reemplazo hecho por la propia cuenta. Se nombra **siempre con la palabra «marca»**, porque `Pendiente` a secas ya nombra un estado de cuenta y un estado de trabajo: ver §3.3 | `Definicion-Modelo-De-Dominio.md`, CU-02002, CU-02013, CU-02003, CU-02004, RN-02012, RN-02013, RN-02016 | «Cambio de contraseña pendiente» como nombre del atributo. **No** se dice «cuenta pendiente» para nombrarla |
| Reseteo de contraseña | Operación **conservadora** del administrador sobre una cuenta de alumno: **el sistema le produce** una contraseña provisoria (RN-02014) y el dominio le pone la marca, sin cambiar su estado de cuenta, su papel, su identidad ni ninguno de sus trabajos, y **cualquiera sea el estado de esa cuenta**. **Resetear no es dar de baja** | `Definicion-Modelo-De-Dominio.md`, CU-02013, CU-02002, RN-02012, RN-02014, RN-02015 | — |
| Texto original | El texto que el alumno cargó, tal como lo emitió su programa. Se conserva íntegro y nunca se reescribe | `Definicion-Modelo-De-Dominio.md`, CU-02005 a CU-02008, RN-02008 | «JSON original» en las fuentes técnicas. En esta categoría se dice «texto original», porque el dominio no conoce formatos de serialización |
| Posición de pieza | Lugar que una figura ocupa en el conjunto raíz del trabajo. **Es la identidad de la pieza**, porque el dato del alumno no trae identificador propio | `Definicion-Modelo-De-Dominio.md`, CU-02006, CU-02007, RN-02009 | «Índice de figura» en las fuentes |
| Familia plana o volumétrica | Clasificación de una pieza que **se deriva de su tipo** y no se guarda como dato propio | `Definicion-Modelo-De-Dominio.md`, CU-02006 | — |
| Especie de observación | Atributo de la observación que vale `Advertencia` o `Error de validación`, y que decide si el trabajo pasa a estado `Pendiente` o queda en `Borrador` | `Definicion-Modelo-De-Dominio.md`, CU-02007, CU-02008, RN-02005, RN-02009 | «Severidad» en las fuentes técnicas |
| Desenlace | Cada una de las dos decisiones que el administrador aplica sobre un trabajo en estado `Pendiente` y que lo llevan a un estado terminal. Es el término colectivo de «aprobar» y «rechazar», que el glosario raíz declara por separado | `Definicion-Modelo-De-Dominio.md`, CU-02010, CU-02011, RN-02010, RN-02011 | — |
| Alcance del administrador | Conjunto de estados de trabajo sobre los que el administrador ve y opera: los tres que no son `Borrador` | `Definicion-Modelo-De-Dominio.md`, CU-02011, RN-02004, RN-02011 | — |
| Consumidor de la biblioteca | El proyecto de código que usa la superficie pública de este dominio por referencia de proyecto de código: `GeometriaFactory-Application` o `GeometriaFactory-Infrastructure`. **Es el actor primario de todos los casos de uso de esta categoría** | Los trece CU | «Capa consumidora» |
| Sujeto de la regla | La persona sobre la que recae una regla que el dominio hace cumplir —el alumno, el administrador—, que **no** es actor de ningún caso de uso de esta categoría | Los trece CU | — |

### 2.3 `GeometriaFactory-Application`

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Puerto | Contrato que esta capa declara y que otra capa implementa. Es la frontera del proyecto de código: lo que el caso de uso necesita, expresado sin decir quién lo provee ni con qué mecanismo | `Especificacion-Funcional.md` §3, los once CU | «Interfaz de salida». Se dice **puerto** |
| Puerto de repositorio de trabajos | Puerto por el que el caso de uso recupera un trabajo, resuelve una consulta ya acotada, materializa el resultado y ejecuta el retiro | `Especificacion-Funcional.md` §3, CU-04002, CU-04004 a CU-04009 | — |
| Puerto de repositorio de cuentas | Puerto por el que el caso de uso recupera una cuenta, pregunta si un correo ya está registrado o si ya existe una cuenta con papel `Administrador`, y materializa el resultado. **Su identificador no está declarado aguas arriba**: es punto abierto | `Especificacion-Funcional.md` §3 y §11, CU-04001, CU-04002, CU-04003, CU-04007, CU-04010 | — |
| Puerto de validación de figuras | Puerto por el que el caso de uso entrega el texto original y recibe la cantidad de figuras del conjunto raíz, las piezas reconstruidas y las observaciones | `Especificacion-Funcional.md` §3, CU-04005 | «Validador de figuras», que es la forma corta usada cuando el sujeto es la implementación |
| Puerto de reloj del sistema | Puerto por el que el caso de uso obtiene el sello, **para que los sellos de alta, de modificación y de desenlace sean verificables en prueba** | `Especificacion-Funcional.md` §3, CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010 | «Reloj» en forma corta |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública de esta capa por referencia de proyecto de código: `GeometriaFactory-Api`. **Es el actor primario de los once casos de uso** | Los once CU | «Capa consumidora». Se distingue del «consumidor de la biblioteca» de la categoría de dominio, que puede ser también esta capa |
| Verificación de pertenencia | Comprobación de que el trabajo pedido es del alumno solicitante, ejercida sobre el dato recuperado y antes de escribir. **No la reemplaza ningún papel** | `Especificacion-Funcional.md` §4, CU-04004, CU-04005, CU-04006, CU-04009 | «Autorización por pertenencia» |
| Verificación de facultad | Comprobación de que quien pide una operación reservada tiene el papel `Administrador` | `Especificacion-Funcional.md` §4, CU-04002, CU-04007, CU-04008, CU-04011 | — |
| Contraseña provisoria | Valor de credencial que **el sistema produce** cuando el administrador resetea la contraseña de la cuenta de un alumno, y que el administrador le comunica fuera del producto. Es **provisoria** porque no sirve para nada más que para cambiarla: mientras esté vigente, la cuenta está confinada al cambio | `Especificacion-Funcional.md` §4, CU-04011, CU-04003 | «clave provisoria». **No se dice «contraseña temporal»**, que sugiere un vencimiento por tiempo que el producto no tiene |
| Marca de cambio de contraseña pendiente | Atributo que el reseteo deja sobre la cuenta y que la confina a cambiar su propia contraseña. **La pone únicamente CU-04011 y la levanta únicamente el cambio efectivo de CU-04003 FA-05**, hecho por la propia cuenta (INV-09) | `Especificacion-Funcional.md` §3 y §4, CU-04011, CU-04003, CU-04002 | «marca», en forma corta cuando el complemento ya está fijado. No es un estado de cuenta: convive con `Habilitado` sin reemplazarlo |
| Reseteo de contraseña | Operación por la que el administrador reemplaza la credencial derivada de un alumno por una provisoria y deja la marca, **cualquiera sea el estado de esa cuenta**. **No es una baja**: conserva la cuenta, su estado de habilitación y todos sus trabajos (RN-04012) | `Especificacion-Funcional.md` §5 y §8, CU-04011, CU-04002 | «reseteo». **No se dice «recuperación»**, que es lo que el producto sigue sin tener: no hay canal de correo y no hay camino autónomo |
| Cambio forzado | El reemplazo de credencial que hace una cuenta marcada, y que es lo único que levanta la marca | `Especificacion-Funcional.md` §4, CU-04003 | — |
| Alcance de consulta | Recorte que el caso de uso traslada al puerto antes de pedir: por dueño en el caso del alumno, por estado distinto de `Borrador` en el del administrador. **No es un filtro aplicado después sobre un conjunto mayor** | `Especificacion-Funcional.md` §3 y §4, CU-04006, CU-04007 | — |
| Unidad de trabajo | Tramo dentro del cual las escrituras de un caso de uso ocurren enteras o no ocurren. El alcance declarado es **un caso de uso, una unidad de trabajo** | `Especificacion-Funcional.md` §3, CU-04001, CU-04002, CU-04004, CU-04005, CU-04008, CU-04009 | — |
| Motivo | Valor de la enumeración cerrada con la que un caso de uso explica por qué una operación no procede. **No es un código de protocolo**: su traducción hacia afuera del proceso pertenece a `GeometriaFactory-Api` | Los once CU | «Código de rechazo» en la categoría de dominio |
| Doble | Implementación de prueba de un puerto, que hace ejercitable un caso de uso entero sin base de datos ni frontera de proceso | `Especificacion-Funcional.md` §1 y §4, CU-04001, CU-04005, CU-04007, CU-04008, CU-04010 | «Repositorio simulado», «validador doble», «reloj fijado» |
| Metadato de orquestación | Dato que esta capa aporta al materializar y que **el modelo del dominio no declara como atributo**: los sellos de alta, de modificación y de desenlace. Se distingue de la «Fecha» que el alumno declara en su trabajo, que sí es del dominio | `Especificacion-Funcional.md` §3 y §11, CU-04001, CU-04003, CU-04004, CU-04005, CU-04008, CU-04010 | «Sello», en la forma corta de cada uno |
| Camino de alta | Cada una de las dos formas en que se constituye una cuenta, con estado inicial, credencial y ventana de alta opuestos: el auto-registro del alumno y la configuración del administrador. Cada uno tiene caso de uso propio acá y en el dominio | `Especificacion-Funcional.md` §5 y §8, CU-04001, CU-04010, CU-04003 | — |

### 2.4 `GeometriaFactory-Infrastructure`

| Término canónico | Definición operativa | Artefactos de 02 donde aparece | Sinónimos y alias |
| --- | --- | --- | --- |
| Adaptador | La implementación concreta de un puerto, que vive en este proyecto de código. **Un adaptador provee el mecanismo y no toma decisiones de negocio** | `Especificacion-Funcional.md` §3 y §4, los diez CU | «Implementación del puerto». **No se dice «el puerto» cuando el sujeto es la implementación** |
| Almacén | El archivo único donde el producto guarda todo lo que sobrevive al apagado del proceso. Se nombra así, y **no «base de datos»**, cuando el sujeto es el lugar donde vive el dato de esta instancia | `Especificacion-Funcional.md` §4, `Modelo-Datos/`, CU-06003, CU-06004, CU-06005, CU-06010 | «Archivo del almacén» cuando el sujeto es el archivo en su ruta |
| Trampa del formato | Cada uno de los cuatro rasgos del texto real del alumno que rompen a un lector ingenuo: `T1` clave sinónima, `T2` comas finales, `T3` cara con dos nombres, `T4` valores calculados erróneos. **El contrato nace sabiéndolas** | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-06001, CU-06002 | «Las cuatro trampas», en forma corta |
| Lectura tolerante | La forma en que este proyecto de código lee el texto del alumno: admitiendo comas finales, omitiendo comentarios y aceptando las claves sinónimas. **No es permisividad**: es adaptarse al dato, que es premisa fija del producto | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-06001 | — |
| Existencia contra veracidad | El criterio con el que se lee una dimensión: se comprueba que **el campo esté**, no que su valor tenga sentido geométrico. Un `0.00` presente **no descarta la figura** | `Definicion-Contrato-Del-Validador-De-Figuras.md` §2, CU-06001, CU-06002 | — |
| Operador estricto | La forma de la comparación de valores: se advierte cuando la diferencia absoluta es **mayor** que la tolerancia, y **no** cuando es mayor o igual | `Definicion-Contrato-Del-Validador-De-Figuras.md`, CU-06002 | — |
| Posición reservada | La posición, dentro del rango de figuras del conjunto raíz, que ocupa una figura que **no se pudo reconstruir**. Admite observación aunque no haya pieza, y **no se compacta** | CU-06001, CU-06003, `RC-06002` | — |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las no reconstruidas. **Referenciada** del modelo del dominio; acá se agrega que **es la que este proyecto de código produce** | CU-06001, CU-06003, `Modelo-Datos/` | — |
| Contraseña provisoria | El valor de credencial que **este proyecto de código produce** cuando el administrador resetea la contraseña de la cuenta de un alumno. **Referenciada** de la capa de aplicación; acá se agrega que es donde nace y que **no se conserva** | CU-06007, `RC-06007` | «clave provisoria». **No se dice «contraseña temporal»**: no vence por tiempo, la levanta la marca |
| Valor derivado de la credencial | Lo que el producto guarda en lugar de la contraseña. **No es el «valor derivado» de la geometría**: ver §3.3 | CU-06005, CU-06006, CU-06007 | «credencial derivada», que es la forma que usa la categoría de dominio |
| Acceso firmado | Lo que se emite para que la pieza pública opere contra la pieza de datos: identificador, correo, papel y expiración, firmado con clave simétrica | CU-06008 | «el acceso». **No se dice «sesión»**: la pieza de datos es sin estado |
| Clave de firma | El secreto con el que el acceso se firma. **Vive fuera del repositorio de código y fuera de la imagen** | CU-06008 | — |
| Terminación degradada | La forma en que termina una operación que no se pudo completar por una causa que no depende de lo que se pidió. **Se declara en vez de fingir un resultado**, y este proyecto de código **no reintenta** | CU-06001, CU-06003, CU-06004, CU-06005, CU-06007 | «estado degradado», que es la forma que usa la capa de aplicación |
| Arranque detenido | La forma de terminación propia de la preparación del almacén: el servicio **no atiende ninguna petición**. Es preferible a operar sobre un almacén en el que no se puede confiar | CU-06010 | — |
| Transformación de esquema | Cada paso versionado que lleva el almacén de una forma a la siguiente, **aplicado al arrancar**. Se versiona con el código de su etapa y **una ya fusionada no se edita** | CU-06010, `Modelo-Datos/` | «migración», que es la forma que usan las fuentes técnicas. Se admite cuando el sujeto es la herramienta |
| Regla conceptual de modelo | Cada una de las siete condiciones que el dato guardado tiene que cumplir, con identificador `RC-XX`. **No es una regla de negocio**: declara cómo el dato sobrevive, no qué decidió el negocio | `Modelo-Datos/`, los diez CU | — |
| Segunda línea | El papel que cumplen las restricciones de unicidad del almacén frente a la consulta previa del consumidor: **la verificación previa no es una garantía por sí sola** | `Especificacion-Funcional.md` §4, CU-06005 | — |

## 3. Términos con más de un referente

### 3.1 `GeometriaFactory-Api`

Los tres siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los tres, los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Ninguno se declara acá por analogía con otro.

### 3.1 Acceso

Es la colisión central de esta categoría, y la única que aparece en los doce casos de uso.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El valor firmado con el que la pieza pública opera contra este servicio | **«acceso firmado»**, o «el acceso» cuando ya está nombrado en la misma oración | `CU-00001` §4 y `CU-00002` §4 hablan de los tres referentes en la misma sección |
| Una entrada de la superficie | **«punto de acceso»**, siempre completo, o su identificador `A-XX` | `Definicion-Superficie-HTTP.md` §3 |
| La posibilidad de que una cuenta entre al sistema, que el dominio resuelve | **«admisibilidad»**, que es el término de la capa que la resuelve. **No se dice «acceso» para este referente** | `CU-00001` §4 pasos 2 y 3 |

Regla de uso, en una línea: **«acceso» a secas designa el valor firmado**, y los otros dos referentes se escriben siempre completos.

### 3.2 Código

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El número con el que una petición termina | **«código de respuesta»**, siempre completo | La tabla de `Definicion-Superficie-HTTP.md` §6 tiene los dos en columnas contiguas |
| El identificador del conjunto cerrado del ensamblado de contratos | **«código del contrato»**, o el identificador literal en mayúsculas | Ídem |
| El texto fuente del producto | **«repositorio de código»**, «solución de código», «proyecto de código»: **nunca «código» a secas** | `CU-00010` §6 nombra el repositorio de código y los códigos del contrato en la misma tabla |

Regla de uso: **«código» a secas no se escribe en esta categoría.**

### 3.3 Punto

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Una entrada de la superficie | **«punto de acceso»** | `CU-00011` habla del punto de salud y del punto de control en secciones vecinas |
| La detención obligatoria al cerrar una etapa, a la espera del OK explícito | **«punto de control»**, término del glosario raíz, siempre completo | Ídem |
| Un asunto sin resolver | **«punto abierto»**, siempre completo | `Especificacion-Funcional.md` §11 |

### 3.4 Dos casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo, que es el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica.

- **«Salud»** tiene un solo referente en esta categoría: el estado del servicio que A-16 informa. No colisiona con nada.
- **Los identificadores `A-XX` y `CU-XX` conviven sin ambigüedad** porque tienen prefijos distintos y porque `Especificacion-Funcional.md` §10 declara que **no son la misma serie**: un caso de uso puede describir más de un punto de acceso, y dos casos de uso nunca comparten uno.

### 3.2 `GeometriaFactory-Domain`

Los tres términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en los tres, los sentidos aparecen en el mismo contexto de lectura —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Trabajo

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La unidad que el alumno carga y entrega en el laboratorio: nombre, fecha, descripción y texto original, con dueño y estado | **«trabajo», forma desnuda**. Es el único referente admitido en esta categoría | El término normativo «unidad de entrega» designa a otra cosa, y las fuentes del producto usaron una vez uno por el otro |
| Las dos piezas desplegables del producto | **No se nombran «trabajo» en ningún caso.** Se nombran «pieza pública» y «pieza de datos», o «unidad de entrega» en contexto normativo | `Vision-Producto.md` §9.3 y PRODUCT-INTAKE §12.1 declaran la resolución |
| El esfuerzo de construcción del producto | **No se nombra «trabajo».** Se dice «tarea» o «etapa» | `Vision-Producto.md` §9.1, entrada «Trabajo» |

### 3.2 Pieza

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo, cuya identidad es su posición | **«pieza», forma desnuda.** Es el referente del dominio y el que domina esta categoría | Los dos referentes conviven en la misma cadena documental y el segundo aparece en documentos que esta categoría cita |
| Cada uno de los dos artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» | `Vision-Producto.md` §9.2 declara la forma calificada obligatoria |

En los artefactos de esta categoría el segundo referente aparece sólo en notas de ubicación de responsabilidades, y ahí va calificado.

### 3.3 `Pendiente`

**Declarado en el glosario raíz**, `Vision-Producto.md` §9.2, y en PRODUCT-INTAKE §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica en los artefactos de esta categoría, que es donde los dos referentes conviven con más densidad.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | `Definicion-Modelo-De-Dominio.md` §2.1 y §5.1, CU-02001 a CU-02004, RN-02001, RN-02006 |
| Estado de un **trabajo** enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el sujeto es el trabajo y está nombrado en la misma oración | `Definicion-Modelo-De-Dominio.md` §2.2 y §5.2, CU-02005, CU-02007 a CU-02011, RN-02004, RN-02005, RN-02010, RN-02011 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo —«`Borrador`, `Pendiente`, `Finalizado` o `Rechazado`»—, donde el atributo enunciado ya fija el referente y calificar cada valor sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica; y los nombres de los códigos de rechazo, que son identificadores literales del contrato.

### 3.4 Rol

Los dos referentes conviven **en la misma tabla y hasta en la misma celda** —la tabla de actores de los trece casos de uso—, de modo que colisionan en el sentido de `Vocabulario-Rules.md` §9.2 y se desambiguan. El segundo referente no lo eligió este proyecto de código: viene impuesto por `Rules-Especificacion-Funcional.md` §4.2 punto 2, que fija el encabezado de esa tabla.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Atributo del alumno que vale `Alumno` o `Administrador` | **«papel»**, siempre. Nunca «rol», ni siquiera citando fuentes técnicas que lo llaman así | `Definicion-Modelo-De-Dominio.md` §2.1, CU-02001, CU-02002, CU-02004, CU-02010, CU-02011, RN-02001, RN-02010 |
| Función que un actor cumple dentro de un caso de uso | **«rol»**, y **sólo** como encabezado de la columna de la tabla de actores, que es donde la regla lo impone | §2 «Actores» de los trece casos de uso |

La forma «rol» no se usa en prosa en ningún artefacto de esta categoría: en cuanto se sale del encabezado normativo, el término es «papel». La única ocurrencia en prosa es la transcripción literal del enunciado de INV-02 en `Definicion-Modelo-De-Dominio.md` §4.1 —«a un rol de alumno»—, que se conserva porque es cita del intake y se declara acá para que no se lea como una tercera acepción.

### 3.5 Dos casos que no son polisemia y no se corrigen

- **Observación** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo. La relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1. La regla de uso que sí rige es de precisión: cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado corresponde «advertencia», y cuando se modela la entidad corresponde «observación».
- **Comentario** designa acá una sola cosa: el texto libre y opcional del administrador. No es una observación, no es una calificación, y no tiene relación con los comentarios que el validador tolera **dentro** del texto del alumno, que son sintaxis del dato de entrada y no aparecen en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica (`Vocabulario-Rules.md` §9.1).

### 3.3 `GeometriaFactory-Application`

Los términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en todos ellos los sentidos aparecen en el mismo contexto de lectura —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Repositorio

Es la polisemia propia de este proyecto de código y no existía en la categoría de dominio, porque allá no hay puertos.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El puerto por el que esta capa guarda y recupera | **Siempre calificado**: «puerto de repositorio de trabajos», «puerto de repositorio de cuentas», o «el puerto de repositorio» cuando el complemento ya está fijado en la misma oración | Los dos referentes conviven en la cadena documental del producto y el segundo aparece en documentos que esta categoría cita |
| El repositorio de código donde vive el árbol del producto | **No se nombra «repositorio» a secas en esta categoría.** Se dice «repositorio de código» | Declarado aguas arriba: el nombre del repositorio de código no es un plano de identidad del producto |

### 3.2 Pieza

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| Cada figura del conjunto raíz del trabajo, cuya identidad es su posición | **«pieza», forma desnuda.** Es el referente del dominio y el que domina esta categoría | `Vision-Producto.md` §9.1 y §9.2 declaran los dos referentes y la forma de cada uno |
| Cada uno de los dos artefactos del producto que se despliegan por separado | **Siempre calificado**: «pieza pública», «pieza de datos», «piezas desplegables» | `Vision-Producto.md` §9.2 declara la forma calificada obligatoria |

En los artefactos de esta categoría el segundo referente aparece sólo en notas de ubicación de responsabilidades, y ahí va calificado.

### 3.3 `Pendiente`

**Declarado en el glosario raíz**, `Vision-Producto.md` §9.2, y en PRODUCT-INTAKE §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica acá, que es donde los dos referentes conviven con más densidad, porque los mismos casos de uso tocan cuentas y trabajos.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada por el administrador | **«cuenta `Pendiente`»** | CU-04001, CU-04002, CU-04003 |
| Estado de un **trabajo** enviado, con el texto interpretado sin errores, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el sujeto es el trabajo y está nombrado en la misma oración | CU-04004 a CU-04009 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo —«`Borrador`, `Pendiente`, `Finalizado` o `Rechazado`»—, donde el atributo enunciado ya fija el referente y calificar cada valor sería el falso positivo que `Vocabulario-Rules.md` §9.1 tipifica; y los nombres de los motivos, que son identificadores literales del contrato.

### 3.4 Rol

Los dos referentes conviven en la misma tabla —la de actores de los once casos de uso—, de modo que colisionan y se desambiguan. El segundo referente no lo eligió este proyecto de código: lo impone `Rules-Especificacion-Funcional.md` §4.2 punto 2, que fija el encabezado de esa tabla.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Atributo de la cuenta que vale `Alumno` o `Administrador` | **«papel»**, siempre. Nunca «rol», ni siquiera citando fuentes técnicas que lo llaman así | `Especificacion-Funcional.md` §4 y §6, CU-04001, CU-04002, CU-04007, CU-04008, CU-04009, CU-04010 |
| Función que un actor cumple dentro de un caso de uso | **«rol»**, y **sólo** como encabezado de la columna de la tabla de actores | §2 «Actores» de los once casos de uso |

La forma «rol» no se usa en prosa en ningún artefacto de esta categoría. Es la misma resolución que declara el glosario de `GeometriaFactory-Domain` §3.4, y se conserva idéntica.

### 3.5 Trabajo, y la forma «unidad de trabajo»

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| La unidad que el alumno carga y entrega en el laboratorio | **«trabajo», forma desnuda.** Es el único referente admitido para la palabra sola, y **no es una «unidad de entrega»** en el sentido normativo: es un registro de datos, no algo que se despliegue | `Vision-Producto.md` §9.1 y PRODUCT-INTAKE §12.1 declaran la resolución |
| El tramo transaccional de un caso de uso | **Siempre en la forma compuesta «unidad de trabajo»**, nunca «trabajo» a secas. La forma compuesta se usa completa incluso cuando el contexto parece bastar | Los dos aparecen en el mismo párrafo de varios casos de uso: «el trabajo se materializa en una única unidad de trabajo» |
| El flujo de trabajo del administrador, en la fórmula «no forma parte de su flujo de trabajo» | **Siempre en la forma compuesta «flujo de trabajo»**, que viene de la formulación de la regla aguas arriba | Aparece en las mismas secciones que el referente del dominio, en CU-04007 y CU-04009 |
| El esfuerzo de construcción del producto | **No se nombra «trabajo».** Se dice «tarea» o «etapa» | `Vision-Producto.md` §9.1, entrada «Trabajo» |

### 3.6 Dos casos que no son polisemia y no se corrigen

- **Observación** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo, y su relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en `Vision-Producto.md` §9.1. La regla que sí rige es de precisión: cuando el enunciado se refiere a una discrepancia entre valor declarado y derivado corresponde «advertencia», y cuando se habla del conjunto que el validador devuelve corresponde «observación». **El comentario del administrador no es una observación**: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala.
- **Puerto** designa acá una sola cosa: el contrato que esta capa declara y otra implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica (`Vocabulario-Rules.md` §9.1).

### 3.4 `GeometriaFactory-Infrastructure`

Los cuatro términos siguientes se verificaron contra el criterio de `Vocabulario-Rules.md` §9.1 y §9.2: en todos ellos los sentidos aparecen en el **mismo contexto de lectura** —la sección— y por eso se desambiguan. Los términos cuyos contextos son disjuntos no se corrigen, y no se declara ninguno acá por analogía.

### 3.1 Validador

Es la polisemia propia de este proyecto de código, y nace de que acá conviven el contrato y la cosa que lo implementa.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El **puerto** que la capa de aplicación declara | **«puerto de validación de figuras»**, siempre completo | Los dos referentes aparecen en la misma sección de `Especificacion-Funcional.md` §3 y en la §9 de CU-06001 y CU-06002 |
| El **adaptador** que lo implementa acá | **«validador de figuras»**, o «el validador» cuando el complemento ya está fijado en la misma oración | La categoría de aplicación ya declara «validador de figuras» como la forma corta usada cuando el sujeto es la implementación, y acá el sujeto es siempre la implementación |

### 3.2 Repositorio

Heredada de la categoría de aplicación, que la declaró primero, y **acá gana un tercer referente**.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El puerto por el que se guarda y se recupera | **Siempre calificado**: «puerto de repositorio de trabajos», «puerto de repositorio de cuentas» | Declarado en la categoría de aplicación §3.1 y conservado |
| El adaptador que lo implementa | **«adaptador del repositorio de trabajos»** o «de cuentas» | Los dos primeros conviven en `Especificacion-Funcional.md` §3 |
| El repositorio de código donde vive el árbol del producto | **No se nombra «repositorio» a secas.** Se dice «repositorio de código» | Aparece en CU-06008 —donde se declara que la clave de firma no entra— junto con los otros dos referentes |

### 3.3 Derivado

Es la colisión que más caro sale, porque las dos cosas son números que el sistema calcula y las dos aparecen en el mismo caso de uso del reseteo.

| Referente | Forma que corresponde | Evidencia de colisión |
| --- | --- | --- |
| El valor de `Area` o de `Volumen` que el sistema recalcula desde las dimensiones | **«valor derivado»**, o «`Area` derivada» y «`Volumen` derivado» con su campo | Declarado en el glosario raíz. Domina CU-06001, CU-06002 y `RC-06003` |
| El valor con el que el producto guarda una contraseña | **«valor derivado de la credencial»** o **«credencial derivada»**, nunca «valor derivado» a secas | Los dos aparecen en la misma sección de `Especificacion-Funcional.md` §4 y en el vocabulario de CU-06005, CU-06006 y CU-06007 |

Regla de uso, en una línea: **«derivado» a secas designa la geometría; la credencial se nombra siempre con su complemento.**

### 3.4 `Pendiente`

**Declarado en el glosario raíz** y en el intake §4.2. Se referencia y no se redefine; esta subsección declara únicamente cómo se aplica acá.

| Referente | Forma que corresponde | Dónde aparece acá |
| --- | --- | --- |
| Estado de una **cuenta** registrada y todavía no habilitada | **«cuenta `Pendiente`»** | CU-06005, `RC-06007`, `Modelo-Datos/` |
| Estado de un **trabajo** enviado, a la espera de revisión | **«trabajo en estado `Pendiente`»**, o «estado `Pendiente`» cuando el trabajo está nombrado en la misma oración | CU-06001, CU-06002, CU-06003, CU-06004 |

La forma desnuda no se usa. **Dos usos que no se califican, y no son defecto**: la enumeración del conjunto cerrado de valores de un atributo, donde el atributo enunciado ya fija el referente, y los nombres de los códigos, que son identificadores literales del contrato.

### 3.5 Tres casos que no son polisemia y no se corrigen

Se declaran para que una revisión posterior no los levante como hallazgo.

- **«Observación»** tiene un solo referente y dos especies: es un término **superordinado**, no ambiguo, y su relación con «advertencia» y «error de validación» es de hiperonimia, ya declarada en el glosario raíz. **El comentario del administrador no es una observación**: lo escribe una persona, hay a lo sumo uno por trabajo y no lleva nota ni escala.
- **«Puerto»** designa acá una sola cosa: el contrato que la capa de aplicación declara y que este proyecto de código implementa. No tiene relación con ningún sentido de infraestructura de red, que no aparece en ningún artefacto de esta categoría. Los contextos son disjuntos y por eso no se califica; es la misma resolución que la categoría de aplicación declaró.
- **«Transformación»** aparece sólo en la forma compuesta «transformación de esquema» y no colisiona con nada: este proyecto de código **no transforma el dato del alumno**, y esa prohibición se enuncia con otras palabras —conservar íntegro, no reescribir— precisamente para que no parezcan lo mismo.

## 4. Términos referenciados y no redefinidos

### 4.1 `GeometriaFactory-Api`

### 4.1 Del glosario raíz de 00

Trabajo; Pieza; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; **Punto de control**; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 De los glosarios funcionales de los cuatro proyectos de código que este ensambla o transporta

| Término | Qué designa, en una línea | Dónde está declarado |
| --- | --- | --- |
| Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta | `GeometriaFactory-Domain` |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja y que sólo el cambio efectivo levanta | `GeometriaFactory-Domain` |
| Desenlace, Terminalidad, Alcance del administrador | Las nociones que gobiernan el cierre del circuito | `GeometriaFactory-Domain` |
| Puerto, y los cuatro puertos | Los contratos que la capa de aplicación declara | `GeometriaFactory-Application` |
| Verificación de pertenencia, verificación de facultad | Las dos comprobaciones que **no se hacen acá** | `GeometriaFactory-Application` |
| Alcance de consulta, Unidad de trabajo, Motivo, Doble | El vocabulario de la orquestación | `GeometriaFactory-Application` |
| Contrato de uso, Tipo de transferencia, Conjunto cerrado de códigos | El vocabulario del ensamblado que cruza la frontera | `GeometriaFactory-Contracts` |
| Adaptador, Almacén, Transformación de esquema | El vocabulario de la capa que toca el mundo | `GeometriaFactory-Infrastructure` |
| Terminación degradada | La forma de terminar de una operación que el mundo no dejó completar | `GeometriaFactory-Infrastructure` |
| Trampa del formato, Lectura tolerante, Operador estricto | El vocabulario del dato real del alumno | `GeometriaFactory-Infrastructure` |
| Contraseña provisoria | El valor que el reseteo produce, y que esta superficie devuelve **una sola vez** | `GeometriaFactory-Infrastructure` |
| Acceso firmado, Clave de firma | Lo que se emite para operar contra este servicio, y el secreto con el que se firma | `GeometriaFactory-Infrastructure` |

**Los seis términos normativos del framework** —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. Dos precisiones que este proyecto de código vuelve pertinentes:

- **Este proyecto de código sí es una unidad de entrega**, y es una de las dos del producto: se despliega de forma independiente, como contenedor en el servidor propio. La otra es la pieza pública. **El trabajo del alumno no lo es**, y el intake lo declara expresamente.
- **«Proyecto» a secas no se usa**, por el choque de vocabulario que el intake declara: la palabra designa tanto una unidad de compilación como los ejemplos de la cátedra que emiten el dato.

### 4.2 `GeometriaFactory-Domain`

Los siguientes términos ya están declarados en `00-Contexto/Vision-Producto.md` §9 con la misma semántica con la que esta categoría los usa. Se referencian y no se redefinen; ninguna definición de §2 los pisa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 |
| Pieza, en su referente del dominio | `Vision-Producto.md` §9.1 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Componente | `Vision-Producto.md` §9.1 |
| Observación | `Vision-Producto.md` §9.1 |
| Advertencia | `Vision-Producto.md` §9.1 |
| Error de validación | `Vision-Producto.md` §9.1 |
| **Estado del trabajo**, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| **Enviar**, como única acción de guardado | `Vision-Producto.md` §9.1 |
| **Aprobar / Rechazar** | `Vision-Producto.md` §9.1 |
| **Comentario** | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio | `Vision-Producto.md` §9.1 |
| Actividad 1 | `Vision-Producto.md` §9.1 |
| `Describir()` | `Vision-Producto.md` §9.1 |
| Tapa | `Vision-Producto.md` §9.1 |
| Rectángulo desarrollado | `Vision-Producto.md` §9.1 |
| Coma final | `Vision-Producto.md` §9.1 |
| Fallo silencioso | `Vision-Producto.md` §9.1 |
| Punto de control | `Vision-Producto.md` §9.1 |
| Hito interno / hito demostrable | `Vision-Producto.md` §9.1 |
| **`Pendiente`, forma calificada obligatoria** | `Vision-Producto.md` §9.2 |
| Etapa | `Vision-Producto.md` §9.2 |
| Puerta técnica | `Vision-Producto.md` §9.2 |
| Capacidad | `Vision-Producto.md` §9.2 |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá.

### 4.3 `GeometriaFactory-Application`

### 4.1 Del glosario raíz de 00

Ya declarados en `00-Contexto/Vision-Producto.md` §9 con la misma semántica con la que esta categoría los usa.

| Término | Dónde está declarado |
| --- | --- |
| Trabajo | `Vision-Producto.md` §9.1 |
| Pieza, en su referente del dominio | `Vision-Producto.md` §9.1 |
| Pieza, en su segundo referente, siempre calificado | `Vision-Producto.md` §9.2 |
| Componente | `Vision-Producto.md` §9.1 |
| Observación | `Vision-Producto.md` §9.1 |
| Advertencia | `Vision-Producto.md` §9.1 |
| Error de validación | `Vision-Producto.md` §9.1 |
| Estado del trabajo, con sus cuatro valores y la terminalidad de dos de ellos | `Vision-Producto.md` §9.1 |
| Enviar, como única acción de guardado | `Vision-Producto.md` §9.1 |
| Aprobar / Rechazar | `Vision-Producto.md` §9.1 |
| Comentario | `Vision-Producto.md` §9.1 |
| Valor declarado / valor derivado | `Vision-Producto.md` §9.1 |
| Laboratorio | `Vision-Producto.md` §9.1 |
| Actividad 1 | `Vision-Producto.md` §9.1 |
| Punto de control | `Vision-Producto.md` §9.1 |
| `Pendiente`, forma calificada obligatoria | `Vision-Producto.md` §9.2 |
| Etapa | `Vision-Producto.md` §9.2 |
| Capacidad | `Vision-Producto.md` §9.2 |

### 4.2 Del glosario de GeometriaFactory-Domain

Ya declarados en `Unidades-Entrega/GeometriaFactory-Api/02-Especificacion-Funcional/Glosario-Funcional.md` §2. Esta categoría los usa con la misma semántica y no los redefine.

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno | La persona de la comisión con identidad propia dentro del laboratorio, a la que pertenecen trabajos |
| Papel | Atributo que vale `Alumno` o `Administrador` |
| Estado de cuenta | Atributo que vale `Pendiente`, `Habilitado` o `Bloqueado` |
| Credencial derivada | Valor derivado de la contraseña, que nunca llega en claro |
| Admisibilidad de la cuenta | Resultado de evaluar si una cuenta admite acceso, con su motivo |
| Baja de la cuenta | Operación destructiva que elimina la cuenta y todos sus trabajos |
| Texto original | El texto que el alumno cargó, conservado íntegro |
| Posición de pieza | Lugar de la figura en el conjunto raíz; es la identidad de la pieza |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y no se guarda |
| Especie de observación | Atributo que vale `Advertencia` o `Error de validación` |
| Desenlace | Término colectivo de aprobar y rechazar |
| Cantidad de figuras del conjunto raíz | Cuántas figuras trae el texto interpretado, incluidas las que no se pudieron reconstruir; es el rango de posiciones válidas del trabajo |
| Alcance del administrador | Los tres estados de trabajo que no son `Borrador` |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá.

### 4.4 `GeometriaFactory-Infrastructure`

### 4.1 Del glosario raíz de 00

Ya declarados con la misma semántica con la que esta categoría los usa: Trabajo; Pieza en su referente del dominio, y en su segundo referente siempre calificado; Componente; Observación; Advertencia; Error de validación; Estado del trabajo con sus cuatro valores y la terminalidad de dos de ellos; Enviar como única acción de guardado; Aprobar / Rechazar; Comentario; Valor declarado / valor derivado; Laboratorio; Actividad 1; Punto de control; `Pendiente` con su forma calificada obligatoria; Etapa; Capacidad.

### 4.2 Del glosario funcional de GeometriaFactory-Domain

| Término | Qué designa, en una línea |
| --- | --- |
| Alumno, Papel, Estado de cuenta, Credencial derivada | El vocabulario de la cuenta |
| Texto original | El texto que el alumno cargó, conservado íntegro |
| Posición de pieza | Lugar de la figura en el conjunto raíz; es la identidad de la pieza |
| Familia plana o volumétrica | Clasificación que se deriva del tipo y **no se guarda** (`RC-06004`) |
| Especie de observación | Atributo que vale `Advertencia` o `Error de validación` |
| Desenlace | Término colectivo de aprobar y rechazar |
| Alcance del administrador | Los tres estados de trabajo que no son `Borrador` |
| Sujeto de la regla | La persona sobre la que recae una regla, que no es actor de ningún caso de uso |

### 4.3 Del glosario funcional de GeometriaFactory-Application

| Término | Qué designa, en una línea |
| --- | --- |
| Puerto | Contrato que aquella capa declara y que **ésta implementa** |
| Los cuatro puertos, uno por uno | Repositorio de trabajos, repositorio de cuentas, validación de figuras y reloj del sistema |
| Consumidor de los casos de uso | El proyecto de código que invoca la superficie pública de aquella capa |
| Verificación de pertenencia y verificación de facultad | Las dos comprobaciones que **no se hacen acá** |
| Alcance de consulta | El recorte que el caso de uso traslada al pedido **antes** de pedir |
| Unidad de trabajo | El tramo dentro del cual las escrituras ocurren enteras o no ocurren |
| Motivo | El valor de la enumeración cerrada con el que se explica que algo no procede |
| Doble | Implementación de prueba de un puerto. **Acá es lo que se reemplaza**, no lo que se escribe |
| Marca de cambio de contraseña pendiente | El atributo que el reseteo deja sobre la cuenta y que sólo el cambio efectivo levanta |
| Metadato de orquestación | Los sellos de alta, de modificación y de desenlace, que aquella capa aporta al materializar |
| Camino de alta | Cada una de las dos vías por las que nace una cuenta, con reglas opuestas |

Los seis términos normativos del framework —producto, unidad de entrega, módulo, solución de código, proyecto de código y proyecto— conservan el sentido de `Vocabulario-Rules.md` §2 y no se redefinen acá. En particular, **«trabajo» no es «unidad de entrega»**: las unidades de entrega de este producto son las dos piezas desplegables, y el trabajo del alumno es un registro de datos que no se despliega.

## 5. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión** (`Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4). Pasa de ser el documento de un proyecto de código a ser el de la **unidad de entrega**, con una subsección por proyecto y su texto transpuesto **sin reescritura**. Entra **§0** con lo que sólo se ve con las capas juntas. Los documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-m10/`. Sube **major**. |
