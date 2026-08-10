# Auditoría de coherencia y consistencia del corpus completo · ronda 2

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/api-fase-b` |
| Alcance auditado | **Todo `SDD/Docs/`** —nivel producto y los **siete** proyectos de código— y `SDD/Intake/`, contra `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.15** y `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** |
| Motivo de la ronda | Dictaminar si se levanta el **rechazo** de `Coherencia-Corpus-r1.md` 1.0 (doce hallazgos: 2 P0, 4 P1, 4 P2, 2 P3), tras los commits de corrección `5103e10` y `6f406fb` y la emisión **1.15** del intake |
| Criterio de la ronda | **El instrumento, no la conclusión.** Ninguna declaración de cierre se acepta porque esté escrita en un control de cambios: se abre el texto vivo, se recuenta sobre los archivos y se contrasta con `git diff`. El alcance del cierre se mide contra el **conteo real**, no contra el que r1 declaró |
| Fuera de alcance | `_legacy/` (185 archivos); las tres fuentes del intake, que viven en otro repositorio bajo `PROMPTs/`; categorías 04 a 09 donde no están emitidas |
| Auditor | Auditor independiente, sin participación en la generación ni en la corrección de ninguna tanda |
| Fecha | 2026-08-10 |
| Volumen contado | **396** archivos `.md` en el árbol; **185** en `_legacy/`; **211** vivos, de los cuales **191** fuera de `Audit/` |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Los doce hallazgos de r1, uno por uno](#2-los-doce-hallazgos-de-r1-uno-por-uno)
- [3. Lo que la corrección tocó: verificación con recuento propio](#3-lo-que-la-correccion-toco-verificacion-con-recuento-propio)
- [4. La verificación de contenido más importante: ¿el registro sigue siendo anónimo?](#4-la-verificacion-de-contenido-mas-importante-el-registro-sigue-siendo-anonimo)
- [5. Las siete preguntas de coherencia entre capas](#5-las-siete-preguntas-de-coherencia-entre-capas)
- [6. Conjuntos cerrados y forma, recontados](#6-conjuntos-cerrados-y-forma-recontados)
- [7. Hallazgos nuevos](#7-hallazgos-nuevos)
- [8. Lo que no reporto, y lo que no pude verificar](#8-lo-que-no-reporto-y-lo-que-no-pude-verificar)
- [9. Dictamen](#9-dictamen)
- [10. Estado general de consistencia del corpus](#10-estado-general-de-consistencia-del-corpus)

---

## 1. Resumen ejecutivo

**Se levanta el rechazo.** Los **doce** hallazgos de r1 están **cerrados**, y once de ellos con un alcance **mayor** que el que el informe describía: la corrección no se limitó a las líneas citadas sino que recontó cada familia sobre el árbol y encontró ocurrencias que r1 había subcontado. Lo verifiqué del modo inverso al declarado: **no leí los controles de cambios para creerles, los leí para ir a buscar el texto vivo que dicen haber tocado**, y después barrí el árbol entero con las frases defectuosas para comprobar que no quedara ninguna en pie.

El resultado del barrido independiente es el que importa: **cero declaraciones vivas de «quince reglas»** fuera de filas históricas de control de cambios y del propio `Audit/` (C-02), **cero apariciones vivas de «diecisiete» aplicado al conjunto de códigos de contrato** (C-04, C-05, C-07), **cero citas vivas al intake 1.3 o 1.7** en las líneas de trazabilidad (C-08, la parte que r1 declaró que importaba), **cero apariciones de «escritura anónima en el sistema»** en texto vivo (C-10), y **cero documentos que declaren que el intake se contradice** (C-03).

La corrección también encontró, y arregló, un defecto **más grave que cualquiera de los doce** y que r1 no vio: las tablas de correspondencia RN→tramo de `GeometriaFactory-Application` y de `GeometriaFactory-Infrastructure` **no tenían fila para RN-16**. Lo verifiqué contra el instrumento y no contra el relato: las dos tablas tienen hoy **dieciséis filas**, y —lo que importa más— **el tramo que cada fila declara es verdadero contra el caso de uso que cita**, comprobado abriendo `Application/CU-02`, `Application/CU-03`, `Infrastructure/CU-07` y `Infrastructure/CU-05`.

La verificación de contenido que el Product Owner puso como condición —que **ninguna capa haya quedado exigiendo credencial para el registro de cuenta**— **pasa en las cinco capas donde el registro existe**. Abrí las precondiciones y el flujo principal de los cuatro casos de uso del alta y del punto de acceso: en todos, la petición **no trae acceso y no lo necesita**, y el aporte de credencial en el alta es un **rechazo declarado**. El producto especificado deja inscribirse a un alumno.

Levanto **tres hallazgos nuevos**, ninguno P0 ni P1: dos son residuos de recuento **anteriores** a esta corrección que r1 no había registrado, y el tercero es de trazabilidad de cabecera, de la misma familia benigna que C-08.

---

## 2. Los doce hallazgos de r1, uno por uno

Estado verificado sobre el árbol vivo. «Cerrado» significa que abrí el texto y que además barrí el corpus buscando la misma frase en otro lado.

| # | Sev. r1 | Hallazgo | Estado | **Cómo lo comprobé** |
| --- | --- | --- | --- | --- |
| **C-01** | P0 | `Domain/Definicion-Modelo-De-Dominio.md` §2.1 declaraba que la marca «la pone únicamente el reseteo» | **Cerrado, con alcance mayor** | Abrí la línea 68: hoy dice «que el sistema produjo al **habilitarla** (RN-16) o al **resetearla** (RN-14)». Contrasté con §4.1 línea 194 —«La ponen **la habilitación y el reseteo**»— y con INV-09 del intake línea 644: las tres coinciden. Barrí «únicamente el reseteo» / «sólo el reseteo» sobre los 191 archivos vivos fuera de `Audit/`: **cero** ocurrencias en texto vivo. r1 registró **1** ocurrencia; la corrección declara **4** y el barrido confirma que ninguna sobrevive (la cuarta era el quinto punto de §4 de `Application/Especificacion-Funcional.md`, que hoy declara los dos orígenes) |
| **C-02** | P0 | Seis declaraciones vivas decían «quince reglas» sobre dieciséis | **Cerrado, con alcance mayor** | Conté los archivos de `Domain/…/Reglas-De-Negocio/`: **16**, `RN-01`…`RN-16`, serie contigua. Después busqué `quince (reglas\|invariantes)` y `las quince` sobre todo el árbol vivo: **ninguna ocurrencia viva** referida al conjunto de reglas; las que quedan son filas de control de cambios que narran la transición y los `Audit/` anteriores, que no se tocan. El nivel producto —que r1 declaró fuera de alcance para este hallazgo— también quedó cubierto: `01-Necesidades-Negocio/Necesidades-Negocio.md` figura entre los archivos de `5103e10`. r1 contó 6 ocurrencias; el cierre alcanzó **17** según el commit, y mi barrido no encuentra ninguna en pie, que es la prueba que vale |
| **C-03** | P1 | Tres documentos declaraban que el intake «sigue diciendo» que la marca la pone sólo el reseteo | **Cerrado** | `Domain/Definicion-Modelo-De-Dominio.md` línea 202 hoy se titula «Traza del enunciado de INV-09, **que esta tabla transcribe hoy sin ninguna diferencia con su fuente**» y declara «Hubo un desfase y **está cerrado**». `Infrastructure/…/RC-07` §3 dice «**Desde la 1.14 esta regla conceptual y la letra de la fuente coinciden**». Grep de «sigue diciendo» y «para que el orquestador lo cierre» sobre el árbol vivo: **cero** fuera de `Audit/`. r1 contó 2 vivas; la corrección declara 3 |
| **C-04** | P1 | `Api/DX-Error-Messages.md` §2.3: «dieciséis códigos con destino sobre diecisiete» | **Cerrado** | Abrí §2.3: hoy dice «**son catorce códigos con destino sobre quince**». Coincide con la fuente hermana `Api/Definicion-Superficie-HTTP.md` §6 y con el conjunto cerrado contado en su dueño |
| **C-05** | P1 | El bloque `text` de `Api/Definicion-Superficie-HTTP.md` §5 decía «conjunto cerrado de diecisiete» | **Cerrado** | Abrí el bloque: dice «(conjunto cerrado **de quince**)». La tabla contigua y el título de §6 dicen quince. Grep de «diecisiete» sobre `GeometriaFactory-Api`: las únicas apariciones vivas son filas de control de cambios que narran la emisión 1.0, legítimas |
| **C-06** | P1 | `Web/CU-01` §10 fundaba su decisión en la exclusión **X-2**, retirada | **Cerrado, con alcance mayor** | Abrí §10: hoy cita **sólo X-1** como vigente y declara «**X-2 —recuperación de contraseña olvidada— fue retirada el 2026-08-09**». Grep de `X-2` sobre el árbol vivo: todas las apariciones restantes la declaran retirada (`Alcance-Producto.md` §5 con su fila tachada, `NB-02`, `Wireframes-Registro-De-Cuenta.md`). r1 contó 1; la corrección declara 4 |
| **C-07** | P2 | `Contracts/DX-Error-Messages.md` §3.2 decía «los diecisiete están enumerados juntos» | **Cerrado** | Abrí la línea 118: hoy dice «los **dieciocho** identificadores emitidos —los quince vivos y los tres retirados— están enumerados juntos». Conté la tabla: **18** filas `DXT-01`…`DXT-18`, tres tachadas. Quince vivos |
| **C-08** | P2 | Ningún documento citaba 1.14; 18 citas «que importaban» a 1.3 y 1.7 | **Cerrado en lo que r1 declaró que importaba; residuo declarado en N-03** | Extraje la versión de intake de la línea `Trazabilidad upstream` de los 187 documentos que la declaran: **cero** citan 1.3 y **cero** citan 1.7. Hoy hay 16 documentos en 1.14 y 15 que citan la 1.15 en su cuerpo o en su cabecera. Quedan 30 en 1.13 y 21 en 1.12, que r1 ya había clasificado como inocuas o de cabecera; ver **N-03** por el matiz nuevo |
| **C-09** | P2 | El título y el ancla de §4.1 del intake decían «RN-01 a RN-09» | **Cerrado** | Línea 189 del intake: «### §4.1 Reglas de negocio declaradas (**RN-01 a RN-16**)», y la entrada de la tabla de contenido de la línea 55 con el ancla `#41-reglas-de-negocio-declaradas-rn-01-a-rn-16`, que resuelve contra ese título. Registrado en la fila **1.15 (a)** |
| **C-10** | P2 | RN-16 afirmaba «no existe ninguna escritura anónima **en el sistema**» | **Cerrado, y era más grave que P2** | Línea 214 del intake: «no existe ninguna escritura anónima **de credencial**», con el rótulo `[PRECISADO 2026-08-09]` que nombra a **RF-03** y declara que el registro es anónimo por diseño. Grep de «anónima en el sistema» sobre el árbol vivo: **cero** en texto vivo; las tres apariciones restantes son filas de control de cambios que **narran la corrección**, más la fila 1.13 del intake, que es registro histórico de lo que esa emisión declaró. Ver §4 por la verificación de contenido que este hallazgo obligaba |
| **C-11** | P3 | `Infrastructure/CU-07` §7 y §10 atribuían un origen único al reseteo | **Cerrado** | §10 hoy dice «la marca que **la habilitación (RN-16) o el reseteo (RN-14)** dejan»; §7 dice «**La operación que pidió el valor —la habilitación o el reseteo— no ocurre**». El nombre del archivo sigue siendo «…-Del-Reseteo», que r1 declaró deliberadamente que no se toca |
| **C-12** | P3 | Recuentos que sí cerraban conviviendo con el que no | **Cerrado por consecuencia** | Recontados los archivos: `Domain/Casos-De-Uso/CU-*.md` son **13** y los invariantes de §4.1 son **9**. Las tablas de `Domain/README.md` §1 y `Especificacion-Funcional.md` §2 ya no tienen la fila discordante: el hallazgo pedía revisar las tablas enteras y las tablas están enteras |

**Sobre el subconteo, que era el dato con el que había que medir el cierre.** La verificación no consistió en creerle al corrector que había más ocurrencias, sino en **barrer cada frase defectuosa sobre el árbol vivo entero** —191 archivos fuera de `Audit/`— y comprobar que no quede ninguna. Ese barrido es indiferente al recuento declarado: si el cierre hubiera alcanzado sólo las seis ocurrencias de C-02 que r1 nombra, las otras once seguirían apareciendo en mi grep. No aparecen.

---

## 3. Lo que la corrección tocó: verificación con recuento propio

### 3.1 Las dos filas de RN-16 que faltaban, y si su tramo es verdadero

Éste es el punto donde una corrección puede fabricar una regresión: agregar una fila a una tabla de correspondencia es fácil; que el tramo que declara sea **cierto** es otra cosa. Verifiqué las dos filas contra los casos de uso que citan, no contra sí mismas.

| Capa | Fila nueva y tramo que declara | Comprobación contra el caso de uso citado | ¿Verdadero? |
| --- | --- | --- | --- |
| `GeometriaFactory-Application` §6 | **RN-16 → CU-02**, en habilitar y rehabilitar: «piden el valor al puerto de producción, lo derivan y solicitan fijar la credencial derivada provisoria»; y **CU-03 por contraste**, FA-02 donde se ejerce la fijación y FA-05 el único lugar donde la marca se levanta | Abrí `Application/CU-02`. §4 fila «Puerto de producción de la contraseña provisoria»; paso **5** del flujo: «Si la operación es **habilitar** o **rehabilitar**, el caso de uso pide la contraseña provisoria al puerto de producción, la deriva … (RN-14, RN-16)»; §7 declara la marca puesta; FA-04 y FA-05 tratan los dos bordes; `HABILITACION_SIN_CREDENCIAL_PROVISORIA` está en §6; §10 dice «**Dos de las cuatro operaciones ponen la marca … y ninguna la levanta**». La fila describe lo que el caso de uso hace | **Sí** |
| `GeometriaFactory-Infrastructure` §6 | **RN-16 → CU-07**, que produce el valor también para la habilitación con el mismo mecanismo y sin saber qué lo motivó; y **CU-05**, que escribe la marca con la credencial derivada provisoria | Abrí `Infrastructure/CU-07` §3: «Este contrato tiene desde `PRODUCT-INTAKE` 1.13 **dos consumidores y un solo mecanismo**», y §4: «Pide una contraseña provisoria para una **habilitación** o para un **reseteo**»; §5 declara «**no sabe si lo que la motivó fue una habilitación o un reseteo**». Abrí `Infrastructure/CU-05`: paso 5 escribe la cuenta «**incluida la marca**», FA-03 y CA-04 la materializan sobre `Bloqueado` sin cambiar el estado. La fila describe lo que los dos casos de uso hacen | **Sí** |

**Los recuentos que cuelgan de esas filas, recontados sobre las tablas.** `Application` §6 tiene **16** filas `RN-01`…`RN-16` y declara «**Quince de las dieciséis tienen tramo acá** —la excepción es RN-14», con RN-14 como única fila sin tramo: 16 − 1 = 15, **cierra**. `Infrastructure` §6 tiene **16** filas y declara «**Catorce de las dieciséis tienen tramo acá y dos no lo tienen**», con RN-06 y RN-10 como las dos sin tramo: 16 − 2 = 14, **cierra**; las tres con tramo principal siguen siendo RN-08, RN-09 y RN-14. Las dos capas se citan mutuamente sobre RN-14 —«la única de las dieciséis sin tramo en aquella capa»— y las dos citas son exactas.

### 3.2 Los dos conjuntos que debían cerrar

| Conjunto | Debe cerrar en | **Contado por mí** | ¿Cierra? |
| --- | --- | --- | --- |
| Reglas de negocio | dieciséis, en todo el corpus | **16** archivos `RN-01`…`RN-16` en `Domain/…/Reglas-De-Negocio/`, serie contigua sin huecos ni duplicados. Ninguna declaración viva del corpus dice otro número | **Sí** |
| Códigos de contrato | quince vivos sobre dieciocho emitidos | La tabla `DXT` de `Contracts/DX-Error-Messages.md` §3.2 tiene **18** filas y **18** identificadores distintos, tres tachados y rotulados «Retirado del conjunto cerrado» (`DXT-09`, `DXT-13`, `DXT-18`): **15 vivos**. La unión de los `CONTRATO_*` de los ocho contratos de uso da **19** identificadores: los 15 vivos, los 3 retirados y `CONTRATO_LISTADO_VACIO`, que §3.3 declara señal y nunca perteneció al conjunto | **Sí** |

### 3.3 Los tres defectos del intake corregidos en 1.15

| Defecto | Corrección declarada | Comprobación | Estado |
| --- | --- | --- | --- |
| Rango del título de §4.1 | «RN-01 a RN-09» → «RN-01 a RN-16» | Línea 189 y entrada de la TdC línea 55, con el ancla coherente. Conté el cuerpo de la sección: 9 + 7 = **16** filas, sin huecos | **Corregido** |
| Rótulo de la segunda tabla | Encabezaba siete filas enumerando seis | Línea 205: hoy nombra las siete —RN-10, RN-11, RN-12 a RN-15 y RN-16— y **declara que enumera tandas y que envejece con cada una**, con la constancia de que ya se corrigió dos veces. Es la corrección que evita la tercera reincidencia | **Corregido, y de raíz** |
| Enunciado de **RN-16** | «escritura anónima **en el sistema**» → «**de credencial**» | Línea 214, verificada palabra por palabra, con el rótulo que nombra a **RF-03** y declara el registro anónimo por diseño. El criterio de verificación de la misma fila —«ningún punto de acceso que acepte un correo y **una contraseña nueva** sin credencial»— ya estaba acotado y sigue siendo verdadero contra la tabla de puntos de acceso | **Corregido** |

**Un matiz que reviso y no elevo a hallazgo.** El corchete de la fila RN-16 dice «lo que RN-16 elimina es la escritura anónima **de contraseña**» mientras el enunciado y la fila 1.15 del control de cambios dicen «**de credencial**». Las dos formulaciones son verdaderas y designan lo mismo en este producto —la credencial es la contraseña derivada—, y el corpus derivado usa las dos indistintamente con el mismo referente. Es una variación de vocabulario dentro de un contexto único, no una contradicción.

---

## 4. La verificación de contenido más importante: ¿el registro sigue siendo anónimo?

La afirmación excesiva de 1.13 estuvo propagada a los siete proyectos durante dos emisiones. Si alguna capa hubiera «resuelto» esa afirmación exigiendo credencial para inscribirse, el producto especificado no dejaría entrar a ningún alumno al laboratorio. **No ocurrió en ninguna.** Abrí las precondiciones y el flujo principal de cada uno, que es donde una exigencia de credencial tendría que estar escrita.

| Capa | Documento abierto | Qué dice literalmente sobre la identidad en el alta | ¿Exige credencial? |
| --- | --- | --- | --- |
| **Api** (punto de acceso del registro) | `Definicion-Superficie-HTTP.md` §3 y `CU-03-Exponer-El-Alta-De-Cuenta-Y-La-Credencial-Propia.md` §3 | Fila **A-02**, columna «Papel exigido»: **Ninguno**. §3 de `CU-03`: «Para **A-02** y **A-03**, la petición **no** trae acceso y **no** lo necesita». §7: «existe una cuenta en situación `Pendiente`, **sin credencial**» | **No** |
| **Domain** | `CU-01-Registrar-El-Alta-De-Un-Alumno.md` §3, §4, §6 | «No se aporta credencial derivada: **el auto-registro del alumno no incluye contraseña**». El paso 4 **verifica que no se aporte** credencial, y aportar una es el rechazo `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` (CA-04) | **No — la prohíbe** |
| **Application** | `CU-01-Registrar-El-Alta-De-Una-Cuenta.md` §3, §4, §6 | «El consumidor **no** aporta credencial: el auto-registro no incluye contraseña». Paso 4: invoca la constitución «**sin aportar credencial ni estado**». Mismo rechazo declarado | **No — la prohíbe** |
| **Web** | `CU-01-Registrar-La-Cuenta-De-Alumno.md` §3, §4 | Las precondiciones **no piden sesión**: sólo que la pieza pública esté publicada. Paso 2: formulario de «tres campos —correo, nombre y apellido— y **ningún campo de contraseña**». §10: «la contraseña provisoria del producto existe, pero la produce la **habilitación** (RN-16) y **no este registro**» | **No** |
| **Contracts** | `CU-02-Contrato-De-Administracion-De-Cuentas.md` | El tipo de solicitud de registro es el que expresa la escritura anónima; el documento pasó a decir «de credencial» en las dos ocurrencias donde la afirmación era falsa (commit `6f406fb`) | **No** |
| **Infrastructure / Visor** | — | No exponen ni describen el alta: `Infrastructure` materializa la cuenta y `Visor` es un visualizador puro. Su silencio es correcto, no una omisión | n/a |

**Y el defecto inverso, que ningún hallazgo de r1 cubría, también está cerrado.** Grep de «establece su contraseña en el primer ingreso» y sus variantes sobre el árbol vivo: **cero** ocurrencias. Desde RN-16 el alumno **entra con la provisoria y la cambia**, y así lo dice el relato de uso de `NB-02`, que es por donde se lee cómo funciona el producto.

---

## 5. Las siete preguntas de coherencia entre capas

La misma pregunta a los siete proyectos y al nivel producto. La columna final compara con r1.

| # | Pregunta | Respuesta única del corpus hoy | ¿Coherente? |
| --- | --- | --- | --- |
| 1 | ¿Qué produce la habilitación? | Una **contraseña provisoria** producida por el sistema, mostrada una sola vez al administrador, más la **marca de cambio de contraseña pendiente** (RN-16, INV-09) | **Sí en los siete.** La excepción que r1 registraba —§2.1 de `Definicion-Modelo-De-Dominio.md`— está corregida. `Visor` no la menciona y es correcto: no toca cuentas |
| 2 | ¿Cómo se identifica quien establece su contraseña por primera vez? | **Ya autenticado, con la provisoria como credencial vigente.** `A-04` retirado; F-04 se ejerce por `A-05`, el mismo punto del cambio voluntario y del posterior a un reseteo | **Sí en los siete**, y hoy mejor que en r1: los dos textos que todavía decían que el alumno «establece» su contraseña en el primer ingreso fueron corregidos |
| 3 | ¿El registro de cuenta es anónimo? | **Sí, por diseño (RF-03, F-02).** Lo que RN-16 elimina es la escritura anónima **de credencial**: ningún punto acepta un correo y una contraseña nueva sin credencial | **Sí en las cinco capas donde el registro existe.** La reserva de **C-10** quedó levantada en la fuente y en las siete propagaciones. Ver §4 |
| 4 | ¿Qué pone y qué levanta la marca? | La ponen **dos** actos —habilitación y reseteo—; la levanta **únicamente** el reemplazo hecho por la propia cuenta. No hay vencimiento de provisoria | **Sí en los siete.** Era la pregunta que fallaba en r1 por C-01 y C-03; hoy la respuesta es idéntica en el intake, en `Domain` §2.1 y §4.1, en `Application` §4 y §6, en `Infrastructure/RC-07` y en `CU-07` §10 |
| 5 | ¿El reseteo exige estado habilitado? | **No.** Procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, no altera el estado y no es transición de la máquina de estados (RN-15); sigue sin admitirse sobre el administrador (INV-08) | **Sí en los siete.** Sin cambios: RN-15 no fue tocada por la corrección y las citas siguen siendo exactas |
| 6 | ¿Quién produce la provisoria? | **El sistema** (RN-14), no el administrador. Mecanismo delegado a `Infrastructure/CU-07`, que declara **no saber** si lo motivó una habilitación o un reseteo | **Sí en los siete**, y sin el residuo de redacción de **C-11** |
| 7 | ¿Desenlace del envío ante dimensión no legible (`E-8`)? | En el **validador**: **error**, y el trabajo queda en `Borrador` (RN-05). En el **visor**: la pieza **no se dibuja y se enumera**. En la **superficie HTTP**: no es fallo de protocolo | **Sí en los siete.** Reverificado: `Infrastructure/Definicion-Contrato-Del-Validador-De-Figuras.md` mantiene «**Por qué E-8 es error y no advertencia**», y los cuatro documentos que lo tenían como punto abierto lo siguen declarando **cerrado** |

**Las siete preguntas tienen hoy la misma respuesta en los siete proyectos.** Es el primer informe de esta serie que puede escribir esa frase sin excepción.

---

## 6. Conjuntos cerrados y forma, recontados

Ninguna cifra copiada de un documento; todas contadas sobre el instrumento.

| Conjunto | Declarado | **Contado** | Cierra |
| --- | --- | --- | --- |
| Reglas de negocio | dieciséis | **16** archivos `RN-01`…`RN-16`, serie contigua | Sí |
| Invariantes | nueve | **9**, `INV-01`…`INV-09` | Sí |
| Códigos de contrato | quince vivos / dieciocho emitidos | **15** vivos, **18** identificadores, **19** con la señal que nunca perteneció | Sí |
| Puntos de acceso HTTP | quince | **15** filas; **1** declarada por la fuente y **14** rotuladas `[derivado]`; cuatro sin acceso firmado + once con él | Sí — con la salvedad de **N-01** |
| Casos de uso por proyecto | Api 12 · Application 11 · Contracts 8 · Domain 13 · Infrastructure 10 · Visor 7 · Web 10 | **12 · 11 · 8 · 13 · 10 · 7 · 10**, contando archivos | Sí, los siete |
| Capacidades, casos límite, escenarios, necesidades | F-01…F-26 · CL-1…CL-11 · E-1…E-8 · NB-01…NB-09 | Sin identificadores fuera de rango en ninguna de las familias | Sí |

**Forma, sobre los 211 archivos vivos.** Tres comprobaciones mecánicas propias, ejecutadas con las tuberías escapadas y el código en línea neutralizados, y saltando los bloques cercados:

| Comprobación | Resultado |
| --- | --- |
| Identificadores fuera de rango en once familias (`RN`, `INV`, `F`, `CL`, `A`, `DXT`, `NB`, `X`, `E`, `RC`, `RA`) | **0 fantasmas.** La única coincidencia del barrido fue el nombre de los informes `A-00-01-*`, que no es un identificador |
| Filas de tabla con tantas celdas como su encabezado | **0 discordancias** sobre todas las tablas del árbol vivo |
| Enlaces relativos que resuelvan | **0 rotos**, sobre la totalidad de los enlaces relativos del árbol vivo |

La corrección tocó cuarenta y tres documentos más quince y **no rompió un solo enlace ni una sola tabla**.

---

## 7. Hallazgos nuevos

Ninguno es P0 ni P1. Los dos primeros son **anteriores** a esta corrección —`git log -S` los ubica en `8520aa7` y `6ce9e28`— y r1 no los registró; los declaro igual porque son de la familia que este informe tiene que cerrar.

### N-01 · P2 · `GeometriaFactory-Api` dice en tres lugares que «quince de las quince rutas son propuesta derivada», y son catorce

**Dónde está.** `GeometriaFactory-Api/03-UX-UI-DX/README.md` **línea 52** (orden de lectura) y **línea 112** (fila 1.0 del control de cambios); `GeometriaFactory-Api/02-Especificacion-Funcional/README.md` **línea 58**: «hace creer que las quince rutas están decididas, y **quince de ellas no lo están**».

**Qué debería decir.** **Catorce**. Contado sobre la tabla de `Definicion-Superficie-HTTP.md` §3: **15** filas `A-XX`, de las cuales **14** llevan el rótulo `[derivado]` y **una** —el canje de credenciales, `A-01`— lleva `[declarada por la fuente]`.

**Por qué es P2 y no P1.** Los **mismos dos archivos** enuncian el número correcto en otro punto: `03-UX-UI-DX/README.md` línea 96 y `02-Especificacion-Funcional/README.md` línea 79 dicen los dos «**Catorce de las quince rutas son propuesta derivada**», igual que `DX-Developer-Experience.md` línea 60. Es un residuo que se contradice dentro de su propio documento, no una afirmación que engañe sola. Pero es exactamente el patrón que r1 diagnosticó —recuento derivado que sobrevive donde no se lee como texto— y conviene cerrarlo con la misma pasada.

**Cómo lo verifiqué.** Conté las filas `A-XX` de §3 y sus rótulos con `grep -c`; leí las cinco frases de los tres archivos; y ubiqué el origen con `git log -S`, que las sitúa en la emisión de la Fase B de `GeometriaFactory-Api` y en la propagación de RN-16, **antes** de esta corrección.

---

### N-02 · P3 · La fila 1.13 del control de cambios del intake sigue declarando «no queda ninguna escritura anónima en el sistema»

**Dónde está.** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` línea **1599**, fila 1.13.

**Qué dice.** «Dos consecuencias: **no queda ninguna escritura anónima en el sistema**, y el producto tiene un solo mecanismo de credencial inicial en lugar de dos.»

**Por qué lo reporto, y por qué es P3 y no más.** Es **registro histórico** y por el criterio de r1 —que comparto— una fila de control de cambios narra lo que era cierto el día de su emisión y no se reescribe. Lo declaro por una sola razón: es la **única** ocurrencia de la frase absoluta que queda en la fuente, y la fila **1.15**, cuatro renglones más arriba en la misma tabla, la desmiente nombrándola. Un lector que recorra la tabla de arriba abajo encuentra primero la corrección y después el texto corregido, en ese orden, que es el orden que hace inofensivo el residuo. **No pido tocarla**; la registro para que una ronda futura no la levante como defecto abierto.

**Cómo lo verifiqué.** Grep exacto de «anónima en el sistema» sobre el árbol vivo y el intake: cuatro apariciones, tres de ellas filas de control de cambios que **narran la corrección** y ésta, que narra la decisión original.

---

### N-03 · P3 · Trece documentos absorbieron la corrección 1.15 sin actualizar la versión de intake de su cabecera

**Dónde está.** De los dieciséis archivos que el commit `6f406fb` modificó, **uno** declara `1.15` en su cabecera de trazabilidad (`Domain/…/RN-16-Habilitar-Produce-La-Provisoria.md`), **seis** declaran `1.14`, **ocho** declaran `1.13` y uno no declara versión. Casos concretos: `NB-02`, `Api/CU-03`, `Contracts/CU-02` y `Domain/CU-02` tienen una fila de control de cambios que dice «**Absorbe la corrección de `PRODUCT-INTAKE` 1.15**» y una cabecera que sigue declarando **1.13**.

**Por qué es P3.** Es la variante de **C-08** que r1 clasificó como riesgo de trazabilidad y no de contenido, y sigue siéndolo: **el cuerpo de los trece es correcto**, lo verifiqué documento por documento en §4. La diferencia con C-08 es que acá el documento declara en su propio control de cambios haber absorbido una versión que su cabecera no cita, con lo cual el desfase es autoevidente dentro del archivo. En el balance general la trazabilidad **mejoró** mucho: desaparecieron las dieciocho citas a 1.3 y 1.7 que r1 declaró como las únicas que importaban.

**Cómo lo verifiqué.** Extraje la primera versión de intake de la línea `Trazabilidad upstream` de cada uno de los dieciséis archivos del commit y la crucé con su fila de control de cambios más reciente.

---

## 8. Lo que no reporto, y lo que no pude verificar

**Lo que no reporto, y es deliberado:**

1. **Las polisemias con contextos disjuntos.** `Pendiente` como estado de cuenta y como estado de trabajo; «credencial» y «contraseña» usadas con el mismo referente en RN-16. El intake §4.2 ordena el calificativo y el corpus lo cumple.
2. **Los puntos abiertos correctamente declarados como abiertos.** Los once de `Api/Especificacion-Funcional.md` §11, los dos huecos del conjunto cerrado de `Contracts` elevados al Product Owner, la decisión derivada de dónde se ejerce INV-09 en `Domain`. Todos rotulados y con dueño.
3. **Las filas de control de cambios que narran recuentos viejos.** Son registro histórico y son correctas para la fecha que declaran. La única que menciono, y sólo para dejarla declarada, es **N-02**.
4. **El nombre de archivo de `Infrastructure/CU-07`.** Igual que en r1: renombrarlo rompería citas y el documento declara su alcance ampliado en su §3.

**Lo que no pude verificar, y lo declaro no verificado en vez de suponerlo:**

1. **Las tres fuentes del intake** (`Requerimientos-Funcionales.md`, `Requerimientos-Tecnicos.md`, `Analisis-Actividad-Documento-Integrador.md`) viven en otro repositorio bajo `PROMPTs/`, fuera de mi alcance. Toda afirmación rotulada `RF §x`, `RT §x` o `AN §x` queda **no verificada contra su origen** — incluida la premisa de la corrección de 1.15 de que **RF-03 declara el registro anónimo**, que verifiqué contra el corpus derivado y contra la coherencia interna del producto, no contra RF.
2. **Los documentos que declaran trazabilidad upstream sin número de versión.** Siguen existiendo y el recuento de N-03 y de C-08 es un **piso**, no un total.
3. **Las categorías 04 a 09**, no emitidas salvo `Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`. Su ausencia no es hallazgo.
4. **Los recuentos secundarios por proyecto** —«36 condiciones» de Application, «17 condiciones» y «25 historias» de Infrastructure, «18 entradas» del catálogo de Api— se muestrearon y no se recontaron fila por fila en los siete proyectos. Los que sí conté están en §6.

---

## 9. Dictamen

# APROBADO

**Motivo.** Los **doce** hallazgos de r1 están cerrados, verificados sobre el texto vivo y confirmados por un barrido independiente que no encontró ninguna de las frases defectuosas en pie. Los **dos P0** —la contradicción de `Domain` §2.1 sobre quién pone la marca, y el conjunto de reglas que no cerraba— están resueltos en su totalidad y con **más alcance** que el que el informe describía. Los dos conjuntos que debían cerrar cierran: **dieciséis** reglas y **quince** códigos vivos sobre **dieciocho** emitidos, contados sobre el instrumento. Las **siete** preguntas de coherencia entre capas tienen hoy, por primera vez, la misma respuesta en los siete proyectos.

**La condición de contenido que el Product Owner puso como bloqueante se cumple:** ninguna capa exige credencial para el registro de cuenta. Las cinco capas donde el alta existe declaran que la petición no trae acceso y no lo necesita, y dos de ellas **rechazan explícitamente** que se aporte una credencial en el alta. El alumno puede inscribirse.

**Los tres hallazgos nuevos no motivan rechazo** y no reabren ninguna decisión: **N-01** es un recuento de tres frases que sus propios documentos desmienten dos líneas más abajo y que es **anterior** a esta corrección; **N-02** es una fila histórica que la fila de arriba ya corrige; **N-03** es trazabilidad de cabecera sobre cuerpos que verifiqué correctos. Los tres pueden agruparse en la próxima tanda de la categoría que los contiene.

**Lo que conviene que quede dicho:** la corrección encontró y arregló un defecto **más grave que los doce que se le pidieron** —las dos tablas de correspondencia sin fila para RN-16— y lo declaró como hallazgo propio en lugar de silenciarlo. Verifiqué que las filas nuevas no sean de relleno: el tramo que declaran es verdadero contra los casos de uso que citan.

---

## 10. Estado general de consistencia del corpus

**El corpus está consistente, y el mecanismo que lo enfermaba dejó de operar.** Es la respuesta que el Product Owner viene buscando desde hace cuatro tandas, y esta vez la sostengo sin reservas de fondo.

El defecto estructural era una cadena de copiado: cada capa afirmaba cosas sobre la anterior sin abrir la fuente, y una frase falsa aguas arriba llegaba intacta a los siete proyectos. Esa cadena se cortó en dos puntos. **Aguas arriba**, el intake pasó de contradecirse a sí mismo a corregirse en el mismo renglón: la 1.14 arregló INV-09, la 1.15 arregló el rango del título, el rótulo de tandas y —lo que importa— el enunciado de RN-16, y las tres correcciones dejan la marca en el propio texto para que se vea qué decía antes. **Aguas abajo**, esta corrección hizo lo que ninguna anterior había hecho: en vez de tocar las líneas que el informe citaba, **recontó cada familia sobre el árbol** y encontró que las seis ocurrencias eran diecisiete, que la de C-01 era cuatro, que la de C-06 era cuatro. Ése es el cambio de método que hace que el resultado sea distinto, y es verificable: mi barrido independiente de las frases defectuosas devuelve cero, que no es lo que devolvería un cierre limitado a las líneas nombradas.

El episodio de RN-16 merece el párrafo que el Product Owner va a querer leer, porque es el más instructivo de toda la serie. Una emisión enunció una garantía **más ancha que la decisión que la sostenía** —«no existe ninguna escritura anónima en el sistema» cuando lo decidido era «de credencial»— y esa media palabra de más se propagó a los siete proyectos antes de que nadie la mirara. El daño potencial no era documental: llevado a código, el producto no habría dejado inscribirse a ningún alumno. Lo que salva al corpus no es que nadie se equivocara, sino **dónde no se equivocó**: las capas propagaron la frase en su prosa y **no la ejercieron en sus contratos**. `A-02` nunca pidió acceso, el dominio siempre rechazó la credencial en el alta, y el formulario de `Web` nunca tuvo campo de contraseña. La decisión estaba bien entendida en el lugar donde se implementa, y lo que estaba mal era la explicación. Un corpus donde el error vive en la prosa y no en el contrato es un corpus del que se puede construir software.

Lo que queda es residuo de la clase más barata y en cantidad decreciente: **tres frases** con un recuento de rutas que sus propios documentos desmienten en el párrafo siguiente, y **trece cabeceras** que citan una versión anterior a la que su control de cambios declara haber absorbido. Ninguno de los dos toca una decisión, ninguno contradice a otra capa, y los dos son de la familia que r1 diagnosticó con precisión: **la decisión llega siempre, el recuento a veces no**. La diferencia con la ronda anterior es de escala —de ocho hallazgos de esa familia a uno— y de gravedad: ninguno es P0 ni P1.

Sobre la solidez que hay debajo, los números son los mismos que r1 celebró y ahora sin sus excepciones: **cero identificadores fantasma** en once familias, **cero enlaces rotos** y **cero tablas malformadas** sobre los 211 archivos vivos, **los conjuntos cerrados cierran todos**, y las **siete preguntas de coherencia entre capas tienen la misma respuesta en los siete proyectos**. Un árbol de casi cuatrocientos documentos, cuarenta y tres de ellos tocados en una sola pasada de corrección, y ni un enlace colgado.

**Recomendación para la próxima tanda.** La recomendación de r1 —un paso mecánico que, ante cada cambio de cardinalidad de un conjunto cerrado, recorra inventarios de archivos, recuentos derivados y bloques preformateados— **se aplicó y funcionó**, y por eso esta ronda se aprueba. Sugiero extenderla a una segunda clase de lugar, que es la que produjo los tres hallazgos nuevos: **las cabeceras de trazabilidad y las frases de recuento que viven en READMEs y guías de onboarding**, que son documentos que nadie re-deriva porque no contienen decisiones, y que por eso mismo son donde los números viejos se quedan a vivir. Y una recomendación de fondo, que vale más que la mecánica: **enunciar las garantías al ancho exacto de la decisión que las sostiene**. RN-16 costó dos emisiones y siete propagaciones por dos palabras de más.

---

## Control de cambios

| Versión | Fecha | Cambio | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Ronda 2 de la auditoría de coherencia del corpus, sobre los commits `5103e10` y `6f406fb` y el intake **1.15**. Verifica los **doce** hallazgos de `Coherencia-Corpus-r1.md` uno por uno contra el texto vivo y contra `git diff`, midiendo el cierre por barrido independiente de cada frase defectuosa sobre los 191 archivos vivos fuera de `Audit/` en lugar de por el recuento declarado: **doce cerrados**. Verifica con recuento propio las dos filas de correspondencia de RN-16 que la corrección agregó en `Application` e `Infrastructure`, comprobando que el tramo declarado sea verdadero contra los cuatro casos de uso que citan; los dos conjuntos que debían cerrar —**dieciséis** reglas y **quince** códigos vivos sobre **dieciocho** emitidos—; los tres defectos del intake corregidos en 1.15; y, como condición bloqueante, que **ninguna de las cinco capas donde existe el alta exija credencial para el registro de cuenta**. Recorre las siete preguntas de coherencia entre capas, que por primera vez tienen la misma respuesta en los siete proyectos, y tres comprobaciones mecánicas de forma con cero fantasmas, cero enlaces rotos y cero tablas discordantes. Levanta **tres hallazgos nuevos**: 0 P0, 0 P1, 1 P2, 2 P3. Dictamen: **APROBADO**. | Auditor independiente |
