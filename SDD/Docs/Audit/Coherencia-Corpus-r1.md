# Auditoría de coherencia y consistencia del corpus completo · ronda 1

| Campo | Valor |
| --- | --- |
| Producto | Fábrica de Geometría |
| Rama auditada | `sdd/api-fase-b` |
| Alcance auditado | **Todo `SDD/Docs/`**: nivel producto (`00-Contexto`, `01-Necesidades-Negocio`) y los **siete** proyectos de código, contra `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.14** y `SDD/Intake/PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.2** |
| Motivo de la ronda | Pedido expreso del Product Owner: dictaminar sobre la **coherencia y consistencia entre toda la documentación**, después de tres tandas rechazadas por documentos que afirmaban cosas falsas sobre sus fuentes |
| Criterio de la ronda | **La fuente, no la cita.** Ninguna afirmación sobre otro documento se da por buena porque esté escrita: se abre la fuente y se lee la letra. Ningún recuento declarado se reproduce: se cuenta sobre el instrumento |
| Fuera de alcance | `_legacy/` (se cita sólo como término de comparación); categorías 04 a 09 donde no están emitidas, cuya ausencia no es hallazgo; `PROMPTs/`, material del Product Owner |
| Auditor | Auditor independiente, invocado desde cero, sin participación en la generación ni en la corrección de ninguna tanda |
| Fecha | 2026-08-10 |
| Informes anteriores | Los diecinueve de `SDD/Docs/Audit/` (leídos como contexto, **no modificados**) |

---

## Tabla de contenido

- [1. Resumen ejecutivo](#1-resumen-ejecutivo)
- [2. Hallazgos](#2-hallazgos)
  - [2.1 P0 — bloqueantes](#21-p0--bloqueantes)
  - [2.2 P1 — graves](#22-p1--graves)
  - [2.3 P2 — moderados](#23-p2--moderados)
  - [2.4 P3 — menores](#24-p3--menores)
- [3. Las siete preguntas de coherencia entre capas](#3-las-siete-preguntas-de-coherencia-entre-capas)
- [4. Conjuntos cerrados y recuentos, contados a mano](#4-conjuntos-cerrados-y-recuentos-contados-a-mano)
- [5. Citas a versiones archivadas del intake: alcance real](#5-citas-a-versiones-archivadas-del-intake-alcance-real)
- [6. Las tres reglas de arquitectura](#6-las-tres-reglas-de-arquitectura)
- [7. Forma](#7-forma)
- [8. Lo que no reporto, y lo que no pude verificar](#8-lo-que-no-reporto-y-lo-que-no-pude-verificar)
- [9. Dictamen](#9-dictamen)
- [10. Estado general de consistencia del corpus](#10-estado-general-de-consistencia-del-corpus)

---

## 1. Resumen ejecutivo

**Se rechaza la tanda.** Hay **doce hallazgos**: 2 P0, 4 P1, 4 P2 y 2 P3. Ninguno es una decisión de negocio equivocada; todos son **residuos de una propagación que llegó al noventa por ciento del corpus y se detuvo antes de terminar**. La decisión de la última tanda —**RN-16**, habilitar una cuenta produce su contraseña provisoria— está correctamente entendida en los siete proyectos de código y en el nivel producto. Lo que falla es la letra en los últimos lugares donde nadie volvió a mirar.

Los dos P0 son de la misma clase y están en el proyecto de código que es **dueño** de lo que declaran mal. `GeometriaFactory-Domain` mantiene, en la tabla de conceptos de `Definicion-Modelo-De-Dominio.md` §2.1, que la marca de cambio de contraseña pendiente «la pone **únicamente** el reseteo» —exactamente lo contrario de lo que dice §4.1 del **mismo archivo**, catorce secciones más abajo—; y ese mismo proyecto declara en **dos** inventarios vivos «**Quince** reglas de negocio, una por archivo» sobre un directorio que tiene **dieciséis** archivos, que yo conté. El defecto se replicó a `GeometriaFactory-Web` y a `GeometriaFactory-Infrastructure`.

La buena noticia es la que el Product Owner venía persiguiendo desde hace tres tandas: **el defecto de fondo está en retirada**. El intake **1.14** cerró la contradicción de INV-09 y hoy **su letra y su decisión coinciden** —lo verifiqué abriendo `§17.1.P.2` línea 644—. La consecuencia es la esperable y también es un hallazgo: **tres documentos vivos siguen declarando una «constancia» de que el intake se contradice**, cuando ya no se contradice. Es un hallazgo mucho más barato que el anterior: antes los proyectos mentían sobre la fuente por descuido; ahora dicen la verdad de anteayer.

Y conviene decir qué **no** encontré, porque es donde más se buscó. **Ningún identificador fantasma**: las familias `RN`, `INV`, `NB`, `F-`, `CL-` y `A-` resuelven todas contra su definición, sin uno solo huérfano ni inventado. **Ninguna contradicción con RA-01, RA-02 ni RA-03.** **Cero enlaces relativos rotos** sobre los 394 archivos del árbol y **cero filas de tabla con celdas discordantes** fuera de `Audit/`. El conjunto de códigos de contrato —el que bajó de diecisiete a quince y era el riesgo declarado de esta ronda— **cierra en quince**, contado uno por uno; lo que no cerró son **tres recuentos derivados** que colgaban de él.

---

## 2. Hallazgos

### 2.1 P0 — bloqueantes

#### C-01 · P0 · El documento de concepto del dominio se contradice a sí mismo sobre quién pone la marca de cambio de contraseña pendiente

**Dónde está.** `SDD/Docs/Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md`, **§2.1**, fila «Cambio de contraseña pendiente» (línea 68).

**Qué dice.**

> «**Marca** que declara que la credencial vigente de la cuenta es una **contraseña provisoria** que el sistema produjo **al resetearla** (RN-14) […] La pone **únicamente** el reseteo del administrador (CU-13) y la levanta **únicamente** el reemplazo hecho por la propia cuenta (CU-03).»

**Qué debería decir.** Que la marca la ponen **dos** actos —la **habilitación** (CU-02) y el **reseteo** (CU-13)— y la levanta únicamente el reemplazo hecho por la propia cuenta. Es exactamente lo que declara **§4.1** del mismo archivo, línea 194: «La ponen **la habilitación y el reseteo** del administrador, y la levanta **únicamente** el cambio efectivo hecho por la propia cuenta». Y es lo que declaran **RN-16** e **INV-09** en el intake 1.14.

**Por qué es P0.** No es un recuento ni una cita: es la **definición normativa del concepto**, en el documento de concepto central del proyecto de código que es dueño del modelo, y dice lo contrario de la decisión vigente del Product Owner. Un lector que entre por §2.1 —que es por donde se entra a un glosario de conceptos— implementa el modelo equivocado y nunca llega a §4.1. Además el propio control de cambios de este archivo (fila 1.6) declara haber tocado §2.1 en esta misma propagación: «**§2.1**: la credencial derivada del alumno pasa a fijarse **en el acto de habilitación**». Tocó la fila de la credencial y **no** la fila de la marca, que está en la misma tabla.

**Cómo lo verifiqué.** Abrí las dos filas del mismo archivo y las comparé cara a cara (`sed -n '68p'` y `sed -n '194p'`). Después abrí la fuente: `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` línea 644 (INV-09, texto 1.14) y línea 214 (RN-16). Las tres coinciden entre sí y ninguna coincide con §2.1.

---

#### C-02 · P0 · El conjunto cerrado de reglas de negocio no cierra: seis declaraciones vivas dicen «quince» sobre dieciséis

**Dónde está.** Seis afirmaciones vivas en **tres** proyectos de código:

| # | Documento | Línea | Qué dice |
| --- | --- | --- | --- |
| 1 | `GeometriaFactory-Domain/02-Especificacion-Funcional/Especificacion-Funcional.md` | 50 | «`Reglas-De-Negocio/RN-XX-<Nombre>.md` \| **Quince** reglas de negocio, una por archivo» |
| 2 | `GeometriaFactory-Domain/02-Especificacion-Funcional/README.md` | 35 | «`Reglas-De-Negocio/` \| **Quince** reglas de negocio, una por archivo» |
| 3 | `GeometriaFactory-Web/02-Especificacion-Funcional/README.md` | 55 | «Las **quince** reglas viven en `GeometriaFactory-Domain`» |
| 4 | `GeometriaFactory-Web/02-Especificacion-Funcional/Especificacion-Funcional.md` | 169 | «Las **quince reglas** del producto viven en `GeometriaFactory-Domain`» |
| 5 | `GeometriaFactory-Web/02-Especificacion-Funcional/Especificacion-Funcional.md` | 171 | «las **quince** reglas restringen el dominio» |
| 6 | `GeometriaFactory-Infrastructure/02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md` | 163 | «Las **quince** reglas de negocio y los **nueve** invariantes» |

**Qué debería decir.** **Dieciséis**, `RN-01` a `RN-16`.

**Por qué es P0 y no un P2 de recuento.** Las dos primeras son **inventarios de archivos** que dicen «una por archivo» sobre un directorio que tiene dieciséis, y están en el proyecto de código **dueño** del conjunto. Un conjunto declarado cerrado cuyo recuento no cierra, en su propio inventario, es la clase de defecto que hace que la regla decimosexta desaparezca sin que nadie lo note: es exactamente lo que le pasó a `RN-02` y a `RN-06` antes del intake 1.3, según cuenta el propio control de cambios del intake. Además el fallo es **transversal**: alcanza a tres de los siete proyectos, y en Domain convive con un control de cambios (fila 1.8) que declara haber actualizado los recuentos «a dieciséis» —lo hizo en §7 y §8, no en §2—.

**Cómo lo verifiqué.** `ls RN-*.md | wc -l` sobre `GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/`: **16** archivos, serie contigua `RN-01`…`RN-16`, sin huecos ni duplicados (los listé todos). Después conté las apariciones vivas de «quince reglas» excluyendo `_legacy/`, `Audit/` y las filas de control de cambios que narran la transición «de quince a dieciséis», que son legítimas y no son hallazgo.

---

### 2.2 P1 — graves

#### C-03 · P1 · Tres documentos vivos declaran que el intake se contradice sobre INV-09, y el intake 1.14 ya no se contradice

**Dónde está.**

| Documento | Línea | Naturaleza |
| --- | --- | --- |
| `GeometriaFactory-Domain/…/Definicion-Modelo-De-Dominio.md` | 202 | Párrafo vivo de §4.1, rotulado «**Constancia sobre el enunciado de INV-09**» |
| `GeometriaFactory-Infrastructure/…/reglas-conceptuales-de-modelo/RC-07-La-Marca-No-Es-Un-Estado-De-Cuenta.md` | 30 | Viñeta viva de §3, rotulada «**Constancia**» |
| `GeometriaFactory-Domain/…/Especificacion-Funcional.md` | 240 | Fila de control de cambios 1.8 (**histórica**, ver abajo) |

**Qué dice.** El texto de Domain, palabra por palabra:

> «`PRODUCT-INTAKE` §17.1.P.2 **sigue diciendo**, en su versión **1.13**, que la marca "la pone **únicamente** el reseteo del administrador". Esa frase es de la 1.7 y quedó desactualizada […] Esta tabla transcribe **la condición que la fuente decidió**, no la letra que la fuente no actualizó, y deja constancia del desfase **para que el orquestador lo cierre aguas arriba**.»

**Qué debería decir.** Nada, o una nota histórica. El orquestador **ya lo cerró aguas arriba**: el intake **1.14** reescribió INV-09 y hoy su letra dice «La marca la ponen **únicamente** las dos operaciones que producen una contraseña provisoria —el **reseteo** (RN-14) y la **habilitación** (RN-16)—», con la corrección rotulada en el propio enunciado y registrada en la fila 1.14 del control de cambios del intake. La constancia se volvió falsa **por la buena razón**: describe un desfase que dejó de existir.

**Por qué es P1 y no P2.** Es una afirmación falsa sobre otra fuente, que es la clase de defecto que rechazó tres tandas. Es de la variante más benigna —el documento se autodeclara desviado de la letra, con lo que un lector queda advertido en vez de engañado— pero **pide una acción al orquestador que ya está hecha** y describe la fuente como rota cuando está sana. Su efecto práctico es que un revisor futuro abre el intake buscando el defecto, no lo encuentra, y no sabe si el que se equivoca es el proyecto o él.

**Cuáles cuentan y cuáles no.** Las **dos primeras** son texto vivo y son el hallazgo. Las filas de control de cambios que dicen lo mismo —`Domain/Especificacion-Funcional.md` 1.8, `Domain/Definicion-Modelo-De-Dominio.md` 1.6 y `RC-07` 1.2— narran **lo que era cierto el día de esa emisión** y no se tocan: una fila de control de cambios es un registro histórico y reescribirla sería peor que dejarla.

**Cómo lo verifiqué.** Grep de las cuatro variantes de la frase («únicamente el reseteo», «sólo el reseteo», «la ponía sólo el reseteo») sobre el árbol vivo; después abrí `Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` línea 644 y leí el enunciado completo de INV-09 en su versión vigente, más la fila 1.14 del control de cambios (línea 1597) que declara la corrección **(a)**.

---

#### C-04 · P1 · `GeometriaFactory-Api` declara dieciséis códigos con destino sobre diecisiete, dos párrafos después de declarar que son quince

**Dónde está.** `GeometriaFactory-Api/03-UX-UI-DX/DX-Error-Messages.md` **§2.3**, línea 139.

**Qué dice.**

> «Del conjunto cerrado de **quince** códigos, **uno** no tiene código de respuesta asignado y no puede tenerlo […] se repite acá para que una revisión posterior no lo levante como cobertura faltante: **son dieciséis códigos con destino sobre diecisiete**, y el hueco es intencional.»

La misma sección afirma quince en su primera oración y diecisiete en la última.

**Qué debería decir.** «Son **catorce** códigos con destino sobre **quince**». Es literalmente lo que declara la fuente hermana de la misma capa: `GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §6, línea 164: «**Quince códigos: catorce con destino en esta superficie y uno sin él.**»

**Por qué es P1.** Es un **recuento derivado** del conjunto que se achicó, y es el que la propia oración pide que «una revisión posterior no levante»: está escrito para blindar un hueco intencional y hoy blinda el número equivocado. Contradice a la vez a su propia sección, a la definición de superficie de su mismo proyecto de código y al conjunto cerrado de `GeometriaFactory-Contracts`.

**Cómo lo verifiqué.** Leí §2.3 entera; leí §6 de `Definicion-Superficie-HTTP.md`; y conté el conjunto cerrado en su dueño — ver §4 de este informe: **quince**.

---

#### C-05 · P1 · El diagrama de las dos traducciones de la superficie HTTP dice «conjunto cerrado de diecisiete»

**Dónde está.** `GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` **§5**, bloque de texto de la línea 126.

**Qué dice.**

```text
motivo de la capa de aplicación  →  código del contrato  →  código de respuesta
        (o condición del                (conjunto cerrado          (los diez de §4)
         adaptador)                      de diecisiete)
```

**Qué debería decir.** «de quince». La tabla que está **inmediatamente debajo**, en el mismo §5, dice «El conjunto cerrado de **quince** códigos del ensamblado», y el §6 siguiente se titula «La tabla de traducción de los **quince** códigos».

**Por qué es P1.** El diagrama es el elemento más citado de la sección —es el que explica el defecto característico de la capa— y es el único lugar del documento donde el número viejo sobrevive. Un bloque `text` no se revisa con los mismos ojos que una tabla, que es exactamente por lo que sobrevivió.

**Cómo lo verifiqué.** Grep de «diecisiete» sobre los archivos vivos de `GeometriaFactory-Api` y lectura del §5 completo; contraste con la tabla contigua (línea 131) y con el título de §6 (línea 142).

---

#### C-06 · P1 · `GeometriaFactory-Web` funda una decisión en la exclusión X-2, que el intake retiró

**Dónde está.** `GeometriaFactory-Web/02-Especificacion-Funcional/Casos-De-Uso/CU-01-Registrar-La-Cuenta-De-Alumno.md`, **§10 Notas y supuestos**, línea 105.

**Qué dice.**

> «La ausencia de canal de correo es decisión declarada del Product Owner (**exclusiones X-1 y X-2**). No existe confirmación de dirección ni contraseña provisoria: es el motivo por el que este caso de uso termina en una cuenta `Pendiente` y no en una sesión.»

**Qué debería decir.** Debe citar **sólo X-1**. El intake §9 (línea 308) muestra la fila de X-2 **tachada**, con el texto: «**Exclusión retirada el 2026-08-09.** Su justificación era que sin correo no hay canal de recuperación, y su salida declarada —dar de baja y volver a dar de alta— **destruía todos los trabajos del alumno** (RN-07). El Product Owner incorporó **F-26**».

**Por qué es P1.** Es una afirmación falsa sobre la fuente en el sentido más literal: cita como vigente y como fundamento una exclusión que la fuente retiró hace un día y medio, y lo hace en la sección de **supuestos**, que es la que un lector consulta justamente para saber sobre qué se apoya la decisión. El resto del corpus está al día: `NB-00002` declara correctamente «§9 (exclusión X-1 vigente y **X-2 retirada**)».

**Cómo lo verifiqué.** Abrí la nota en `CU-01` y después abrí `Intake/…§9` en la línea 308 y leí la fila de X-2 completa, con su tachado y su fecha de retiro.

---

### 2.3 P2 — moderados

#### C-07 · P2 · El catálogo de errores de `GeometriaFactory-Contracts` dice que enumera «los diecisiete»

**Dónde está.** `GeometriaFactory-Contracts/03-UX-UI-DX/DX-Error-Messages.md` **§3.2**, línea 118.

**Qué dice.** «**Quince entradas de código** […] Las quince son `DXT-01` a `DXT-18` **sin `DXT-09`, sin `DXT-13` y sin `DXT-18`** […]; ésta es además la única tabla de todo el proyecto de código donde **los diecisiete** están enumerados juntos.»

**Qué debería decir.** «los **dieciocho**», si lo que quiere decir es que la tabla enumera todos los identificadores emitidos —quince vivos más tres retirados—; o «los **quince**», si lo que quiere decir es el conjunto cerrado. Diecisiete no es ninguna de las dos cosas: es el tamaño que tenía el conjunto antes de RN-16.

**Por qué es P2 y no P1.** La misma oración enuncia el recuento correcto **dos veces** («Quince entradas», «Las quince son») y da la fórmula exacta para reconstruirlo, de modo que el número mal no puede engañar a nadie que lea la oración entera. Es un residuo aislado dentro de un párrafo por lo demás correcto.

**Cómo lo verifiqué.** Conté las filas de la tabla de §3.2: dieciocho identificadores `DXT-01`…`DXT-18`, tres de ellos tachados y rotulados «Retirado del conjunto cerrado» (`DXT-09`, `DXT-13`, `DXT-18`). Quince vivos.

---

#### C-08 · P2 · Ningún documento vivo del corpus cita el intake 1.14, y treinta y cinco citan versiones anteriores a la 1.12

**Dónde está.** En las cabeceras `Trazabilidad upstream:` de todo el árbol. El recuento por versión citada, sobre los documentos vivos que la declaran:

| Versión del intake citada | Documentos | Estado de esa versión |
| --- | --- | --- |
| **1.14** (vigente) | **0** | — |
| 1.13 | 41 | Archivada |
| 1.12 | 24 | Archivada |
| 1.10 y 1.11 | 7 | Archivadas |
| 1.9 | 8 | Archivada |
| 1.7 y 1.8 | 14 | Archivadas |
| 1.3 | 8 | Archivada |
| Sin versión declarada | 94 | — |

**Cuáles importan.** La distinción que pidió el Product Owner es la que decide el peso de este hallazgo, y **la mayoría no importa**:

- **Las 41 citas a 1.13 son inocuas.** El único delta entre 1.13 y 1.14 es la corrección de la **letra** de INV-09 en §17.1.P.2 y del recuento de la prosa de esa misma sección. Ninguna decisión cambió; la propia fila 1.14 lo declara: «Ninguna decisión cambia: los dos son defectos de transcripción de la fuente». Un documento que cita 1.13 §4.1 cita un texto que en 1.14 es idéntico.
- **Las 24 citas a 1.12 tampoco importan por sí solas**, salvo cuando la sección citada es **§4.1**: ahí 1.13 agregó RN-16 y la cita apunta a una sección que cambió. En los documentos que revisé, la cita a 1.12 convive con el tratamiento correcto de RN-16 en el cuerpo, de modo que el desfase es de cabecera, no de contenido.
- **Las que sí importan son las ocho a 1.3**, todas en `GeometriaFactory-Contracts` (`CU-04`, `CU-05`, `CU-07`, `Glosario-Funcional.md`, los dos `README.md` y `Guia-Onboarding-Developer.md`). Entre 1.3 y 1.14 el intake atravesó **once** emisiones, incluidas las que introdujeron F-25, F-26 y las reglas RN-12 a RN-16. Una cabecera que declara 1.3 declara que el documento se derivó de un intake que no conocía ni el reseteo ni la habilitación con provisoria.
- **Las diez a 1.7** —repartidas entre `Application`, `Contracts`, `Domain` y `Web`— son la segunda familia con peso: 1.7 es la versión cuya letra de RN-13 e INV-09 fue **precisada** en 1.8 y corregida en 1.14, que es precisamente el punto donde el corpus más se equivocó.

**Por qué es P2 y no P1.** Porque **verifiqué el cuerpo, no sólo la cabecera**: los documentos que citan 1.3 y 1.7 tratan correctamente RN-16 y el conjunto de quince códigos en su texto vivo (`Contracts/CU-06`, `Contracts/DX-Error-Messages.md`). La cabecera está vieja; el contenido no. El riesgo es de trazabilidad, no de contenido.

**Cómo lo verifiqué.** Extraje la primera versión de intake declarada en la línea `Trazabilidad upstream` de cada archivo vivo del árbol y las agrupé; después leí el delta 1.13 → 1.14 en el control de cambios del intake (línea 1597) para decidir cuáles citas son inocuas.

---

#### C-09 · P2 · El título y la tabla de contenido del intake declaran «RN-01 a RN-09» sobre una sección que tiene dieciséis reglas

**Dónde está.** `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md`, línea 189 (título de §4.1) y línea 62 (entrada de la tabla de contenido, con el ancla `#41-reglas-de-negocio-declaradas-rn-01-a-rn-09`).

**Qué dice.** «### §4.1 Reglas de negocio declaradas (**RN-01 a RN-09**)».

**Qué debería decir.** «RN-01 a RN-16», o simplemente «Reglas de negocio declaradas». La sección contiene hoy dos tablas: la de las nueve reglas de RF §7 y la de las siete que el Product Owner agregó después (`RN-10`, `RN-11`, `RN-14`, `RN-15`, `RN-12`, `RN-16`, `RN-13`, en ese orden).

**Por qué es P2.** Es la **fuente** del conjunto que C-02 declara mal aguas abajo, y su título dice el número viejo. No lo elevo a P1 porque el cuerpo de la sección es completo y correcto —las dieciséis reglas están, con su enunciado y su criterio de verificación— y porque el rótulo interno de la segunda tabla sí fue corregido (lleva su nota «ROTULADO CORREGIDO el 2026-08-09»). Pero es plausible que sea **el origen** de las seis declaraciones de C-02: un derivador que lee el título y no cuenta la tabla escribe quince o nueve, no dieciséis.

**Cómo lo verifiqué.** Leí §4.1 completa (líneas 189 a 216) y conté las filas de las dos tablas: 9 + 7 = **16**, identificadores `RN-01` a `RN-16` sin huecos ni repeticiones.

---

#### C-10 · P2 · RN-16 afirma que «no existe ninguna escritura anónima en el sistema», y F-01 es una

**Dónde está.** El enunciado, en `Intake/…` §4.1, fila **RN-16** (línea 214). La tensión, con **F-01** (línea 162: «Configurar la cuenta de administrador en el primer arranque, y sólo mientras no exista ninguna») y con el punto de acceso **A-03** de `GeometriaFactory-Api/02-Especificacion-Funcional/Definicion-Superficie-HTTP.md` §3.

**Qué dice.** RN-16: «En consecuencia **no existe ninguna escritura anónima en el sistema**: toda operación que fija una contraseña ocurre con la cuenta ya autenticada», y su criterio de verificación: «**No hay ningún punto de acceso que acepte un correo y una contraseña nueva sin credencial**».

**Qué debería decir.** El enunciado necesita el calificativo que la capa de abajo ya le puso. `A-03` —`POST /cuentas/administrador`, papel exigido **Ninguno**— acepta un correo y una contraseña sin credencial: es la configuración inicial del administrador, y es una decisión declarada del producto (F-01, RN-01, INV-05), no un defecto.

**Cómo lo resolvió la capa de abajo, y por qué eso salva la coherencia práctica.** `GeometriaFactory-Api` no repitió la frase absoluta: la reformuló con la precisión que la vuelve verdadera —«De los cuatro puntos que no exigen acceso firmado, **ninguno fija una contraseña sobre una cuenta existente**: A-01 canja credenciales, A-02 registra una cuenta sin contraseña, **A-03 sólo procede mientras no exista administrador** y A-16 es de sólo lectura»— y `NB-00002` construyó su criterio medible sobre «puntos de acceso que aceptan un correo y **una contraseña nueva** sin credencial». Las dos formulaciones son consistentes entre sí y con el producto.

**Por qué es P2.** Porque el corpus derivado está bien y el defecto es del enunciado de la fuente, que es más ancho que la verdad. Pero es la clase de frase absoluta que un derivador futuro va a transcribir literal, y entonces el corpus va a decir algo falso sobre su propio producto.

**Cómo lo verifiqué.** Leí RN-16 completa en el intake; leí la tabla de los quince puntos de acceso de `Definicion-Superficie-HTTP.md` §3 fila por fila, mirando la columna «Papel exigido»; leí el párrafo de reconciliación de la línea 94 y el criterio de `NB-00002` §5.

---

### 2.4 P3 — menores

#### C-11 · P3 · El contrato que produce la contraseña provisoria sigue llamándose «del reseteo» y su §10 le atribuye un solo origen

**Dónde está.** `GeometriaFactory-Infrastructure/02-Especificacion-Funcional/Casos-De-Uso/CU-07-Producir-La-Contrasena-Provisoria-Del-Reseteo.md`: el nombre del archivo y del título, y la nota de §10 (línea 115).

**Qué dice.** §10: «Lo que la vuelve provisoria es la **marca** que **el reseteo** deja sobre la cuenta». Y §7: «**Fallo:** el consumidor recibe el código y ningún valor. **El reseteo no ocurre.**»

**Qué debería decir.** «la marca que **la habilitación o el reseteo** dejan», y «la operación no ocurre». El **cuerpo del mismo archivo** ya lo tiene bien: §3 declara «Este contrato tiene desde `PRODUCT-INTAKE` 1.13 **dos consumidores y un solo mecanismo**» y la tabla de actores dice «Pide una contraseña provisoria para una **habilitación** o para un **reseteo**. Los dos piden lo mismo y reciben lo mismo».

**Por qué es P3.** El contenido normativo está correcto y explícito; lo que quedó viejo son dos frases de la sección de notas y el nombre del archivo. El nombre no lo elevo: renombrarlo rompería las citas y el propio documento declara el alcance ampliado en su primera sección.

**Cómo lo verifiqué.** Leí las secciones §3, §7 y §10 del archivo y contrasté con RN-16 y con `Application/CU-02` y `CU-11`, que son los dos consumidores declarados.

---

#### C-12 · P3 · Trece casos de uso y nueve invariantes: los recuentos que sí cierran conviven con el que no, en la misma tabla

**Dónde está.** `GeometriaFactory-Domain/02-Especificacion-Funcional/README.md` §1 y `Especificacion-Funcional.md` §2.

**Qué dice.** Las mismas dos tablas que C-02 declara mal para las reglas declaran **bien** «Trece casos de uso, uno por archivo» y «nueve invariantes vigentes».

**Por qué lo reporto igual, y como P3.** Porque es la evidencia de que el defecto de C-02 **no es un error de conteo sino de actualización parcial**: quien tocó estas tablas actualizó las filas que cambiaron con la tanda anterior y no la que cambió con ésta. Es un dato para el orquestador sobre dónde mirar, no un defecto en sí. Lo dejo declarado para que la corrección de C-02 no se limite a esas dos filas y revise las tablas enteras.

**Cómo lo verifiqué.** Conté los archivos: `Casos-De-Uso/CU-*.md` en Domain son **13**; los invariantes de `Definicion-Modelo-De-Dominio.md` §4.1 son **9**, `INV-01` a `INV-09`. Los dos recuentos cierran.

---

## 3. Las siete preguntas de coherencia entre capas

La misma pregunta, hecha a los siete proyectos de código y al nivel producto. Una respuesta distinta en cualquier fila es un defecto de coherencia; la columna final dice si la hubo.

| # | Pregunta | Respuesta única del corpus | ¿Coherente? |
| --- | --- | --- | --- |
| 1 | ¿Qué produce la habilitación de una cuenta? | Una **contraseña provisoria** producida por el sistema, mostrada al administrador para que la comunique, más la **marca de cambio de contraseña pendiente** (RN-16, INV-09) | **Sí en los siete**, salvo la fila de §2.1 de `Definicion-Modelo-De-Dominio.md` — es **C-01** |
| 2 | ¿Cómo se identifica quien establece su contraseña por primera vez? | **Ya autenticado, con la provisoria como credencial vigente.** No hay ruta anónima: el punto `A-04` fue retirado y la capacidad F-04 se ejerce por `A-05`, el mismo punto del cambio voluntario y del posterior a un reseteo | **Sí en los siete.** Verificado en `Api/Definicion-Superficie-HTTP.md` §3, `Contracts/CU-02` y `CU-08`, `Web/Wireframes-Credencial-Propia.md` («curso de primer ingreso», tres campos, **sin sesión**), `Domain/CU-03`, `Application/CU-03` |
| 3 | ¿Existe alguna escritura anónima? | **Ninguna que fije una contraseña sobre una cuenta existente.** La configuración inicial del administrador (F-01 / `A-03`) es la única escritura sin credencial y sólo procede mientras no exista administrador | **Sí**, con la reserva de **C-10**: el enunciado del intake es absoluto y las capas de abajo lo calificaron correctamente |
| 4 | ¿Qué pone y qué levanta la marca de cambio de contraseña pendiente? | La ponen **dos** actos —habilitación y reseteo—; la levanta **únicamente** el reemplazo hecho por la propia cuenta. Ni el administrador ni el paso del tiempo la levantan: no hay vencimiento de provisoria declarado por ninguna fuente | **No**, y es el defecto más caro del corpus: **C-01** (Domain §2.1 dice un solo origen) y **C-03** (tres documentos declaran que la fuente sigue diciendo un solo origen) |
| 5 | ¿El reseteo exige estado habilitado? | **No.** Procede sobre `Pendiente`, `Habilitado` y `Bloqueado`, no altera el estado y no es una transición de la máquina de estados de la cuenta (RN-15). Sigue sin admitirse sobre la cuenta de administrador (INV-08) | **Sí en los siete.** Verificado en `Domain/RN-15`, `Application/CU-11` FA-04, `Contracts/CU-08`, `Api/CU-05`, `Web/CU-04`, `Infrastructure/RC-07` y el criterio medible de `NB-00002` §5 («3 de 3 situaciones») |
| 6 | ¿Quién produce la contraseña provisoria? | **El sistema**, no el administrador (RN-14). No adivinable, sin repetirse entre cuentas ni entre reseteos. El mecanismo está delegado explícitamente a `Infrastructure/CU-07`, que declara no saber si lo motivó una habilitación o un reseteo | **Sí en los siete**, con el residuo de redacción **C-11** |
| 7 | ¿Cuál es el desenlace del envío ante una dimensión no legible (`E-8`)? | En el **validador**: **error**, y el trabajo queda en `Borrador` (RN-05). En el **visor**: la pieza **no se dibuja y se enumera** con índice y código. En la **superficie HTTP**: no es un fallo de protocolo | **Sí en los siete.** Fue punto abierto y lo cerró el intake **1.12**; `Infrastructure` (§ de `Definicion-Contrato-Del-Validador-De-Figuras.md`, fila 10 de la matriz), `Api` (`CU-12`), `Web` (`Wireframes-Vista-De-Trabajo.md`) y `Visor` (`CU-02`) coinciden, y los cuatro documentos que lo tenían como abierto lo declaran **cerrado** en vez de arrastrarlo |

---

## 4. Conjuntos cerrados y recuentos, contados a mano

Ninguna cifra de esta tabla se copió de un documento: todas se contaron sobre el instrumento.

| Conjunto | Declarado | **Contado** | Cierra |
| --- | --- | --- | --- |
| Reglas de negocio | dieciséis, `RN-01`…`RN-16` | **16** archivos en `Domain/…/Reglas-De-Negocio/`, serie contigua | **Sí** — pero seis declaraciones vivas dicen quince: **C-02** |
| Invariantes | nueve | **9**, `INV-01`…`INV-09`, en intake §17.1.P.2 y en `Domain/Definicion-Modelo-De-Dominio.md` §4.1 | Sí |
| Escenarios de datos | ocho, `E-1`…`E-8` | **8** en intake §20 | Sí |
| Necesidades de negocio | nueve | **9** archivos `NB-00001`…`NB-00009` | Sí |
| Capacidades | `F-01`…`F-26` | **26** identificadores en el intake, **los mismos 26** en el corpus derivado: ninguno de más, ninguno de menos | Sí |
| Casos límite | `CL-1`…`CL-11` | **11** en el intake, **los mismos 11** en el corpus | Sí |
| **Códigos de contrato** | quince | **15**: la tabla `DXT` enumera dieciocho identificadores, tres tachados y rotulados «Retirado del conjunto cerrado» (`DXT-09`, `DXT-13`, `DXT-18`). La unión de los `CONTRATO_*` de los ocho contratos de uso da diecinueve identificadores: los quince vivos, los tres retirados y `CONTRATO_LISTADO_VACIO`, que es **señal declarada y nunca perteneció al conjunto**, como declara §3.3 | **Sí** — el conjunto cierra; fallan **tres recuentos derivados**: **C-04**, **C-05**, **C-07** |
| Puntos de acceso HTTP | quince | **15** filas, `A-01`…`A-16` con `A-04` retirado y no reciclado. Cuatro sin acceso firmado (`A-01`, `A-02`, `A-03`, `A-16`) + once con acceso firmado = 15 | Sí |
| Códigos de respuesta HTTP | diez | **10**, con las dos ausencias (`422`, `429`) declaradas y justificadas | Sí |
| Casos de uso por proyecto | Api 12 · Application 11 · Contracts 8 · Domain 13 · Infrastructure 10 · Visor 7 · Web 10 | **12 · 11 · 8 · 13 · 10 · 7 · 10** — conté los archivos `CU-*.md` de cada proyecto | Sí, los siete |
| Proyectos de código | siete | **7** en el manifiesto §2, **7** carpetas bajo `Docs/Proyectos/`, siete `Nombre-Proyecto-Codigo` y siete `Identidad-Codigo` distintos | Sí |

**Nota sobre el conjunto de códigos de contrato, que era el riesgo declarado de esta ronda.** El descenso de diecisiete a quince está **bien fundado y bien documentado**: `Contracts/CU-06` §10 explica por qué la unificación de los dos mecanismos de credencial inicial quita dos códigos en lugar de agregar uno, nombra los dos que salen, declara que ninguno se recicla, y explica por qué **no entra** el que un lector esperaría (el rechazo del reseteo sobre cuenta no habilitada, que no existe porque RN-15 decide que el reseteo procede sobre `Pendiente`). El razonamiento es correcto y lo verifiqué contra RN-15 y RN-16. Lo que no se propagó fueron **tres números** que colgaban del anterior.

---

## 5. Citas a versiones archivadas del intake: alcance real

Cuantificado en **C-08**. El resumen que el Product Owner necesita:

- **112 citas** vivas de versión de intake, repartidas en **98 documentos**; otros **94 documentos** declaran trazabilidad upstream **sin versión**, lo cual no es hallazgo por sí mismo pero es la razón por la que este recuento no es completo.
- **Ninguna cita la 1.14.** Es esperable: la 1.14 se emitió como corrección de transcripción y ninguna categoría se re-derivó por ella.
- **Las que importan son 18**: las **ocho a 1.3** de `GeometriaFactory-Contracts` y las **diez a 1.7**, porque 1.7 es la versión cuya letra sobre RN-13 e INV-09 fue precisada dos veces después.
- **Las 41 a 1.13 son inocuas** por el delta nulo en decisiones entre 1.13 y 1.14; **las 24 a 1.12** son de cabecera y conviven con contenido correcto.
- **Las históricas —las de las filas de control de cambios— no son hallazgo** y no se tocan.

---

## 6. Las tres reglas de arquitectura

| Regla | Enunciado del intake §14 | Verificación | Resultado |
| --- | --- | --- | --- |
| **RA-01** | «Ningún JavaScript del navegador invoca la API» | Citada en **25** documentos vivos. Busqué contradicciones en los dos lugares donde podrían aparecer: la superficie HTTP (`Api/Definicion-Superficie-HTTP.md` §7 declara la **ausencia de CORS** con el fundamento «la superficie no recibe peticiones del navegador: RA-01. Configurar CORS sería declarar que sí las recibe») y la exclusión **X-9** del intake, que mantiene la pasarela `/api/*` como especificada y **no adoptada**. `Web/CU-01` prevé test de «verificación de **tráfico cero** del navegador hacia la pieza de datos» | **Sin contradicción** |
| **RA-02** | «El bundle del visor es un visualizador puro: sin configuración, sin red, sin conocimiento del sistema» | Citada en **26** documentos. Busqué en los diecinueve archivos vivos de `GeometriaFactory-Visor` toda mención de `fetch`, `XMLHttpRequest`, `websocket` o `localStorage`: **cero coincidencias**. El contrato de fachada no declara ningún parámetro de configuración ni de identidad | **Sin contradicción** |
| **RA-03** | «Todo lo que el navegador deba obtener del backend pasa por el front; los mensajes de error nunca incluyen direcciones de servicios internos» | Citada en **43** documentos, la más propagada de las tres. Verificada donde se puede romper: `Contracts/CU-06` CA-01 exige «**0 campos** que puedan transportar una dirección de servicio», `DXC-05` la convierte en criterio de rechazo de revisión, `DXT-11` declara «**0 detalles y ninguna dirección**», y `Api/Definicion-Superficie-HTTP.md` §7 lo repite para la superficie | **Sin contradicción** |

---

## 7. Forma

Tres comprobaciones mecánicas sobre los **394** archivos del árbol, excluyendo `_legacy/`:

| Comprobación | Resultado |
| --- | --- |
| Filas de tabla con exactamente tantas celdas como columnas | **0 discordancias.** Contadas con las tuberías escapadas (`\|`) y las que viven dentro de código en línea neutralizadas, que es donde un contador ingenuo produce falsos positivos |
| Filas de versión dentro de su tabla | **0 huérfanas.** Toda fila que empieza con un número de versión está precedida por su encabezado y su fila separadora |
| Enlaces relativos que resuelvan | **0 rotos**, sobre la totalidad de los enlaces relativos del árbol vivo |

La forma del corpus es la parte que está impecable, y conviene decirlo: es un árbol de casi cuatrocientos documentos con cientos de enlaces cruzados y no hay uno solo colgado.

---

## 8. Lo que no reporto, y lo que no pude verificar

**Lo que no reporto, y es deliberado:**

1. **Las polisemias con contextos disjuntos.** `Pendiente` nombra un estado de cuenta y un estado de trabajo; el intake §4.2 lo declara explícitamente y ordena que el término se escriba **siempre calificado**. El corpus lo cumple. No es hallazgo y reportarlo sería un defecto de este informe.
2. **Los puntos abiertos correctamente declarados como abiertos.** `Api/Especificacion-Funcional.md` §11 mantiene once; `Contracts` mantiene dos huecos del conjunto cerrado elevados al Product Owner; `Domain` eleva la decisión derivada de dónde se ejerce INV-09. Todos están rotulados como abiertos y con su dueño. Un punto abierto declarado no es un defecto.
3. **Las filas de control de cambios que narran recuentos viejos** —«catorce códigos», «quince reglas», «pasa de diecisiete a quince»—. Son registro histórico y son correctas para la fecha que declaran.
4. **El nombre de archivo de `Infrastructure/CU-07`.** Ver C-11: renombrarlo rompe citas y el documento declara su alcance ampliado en su primera sección.

**Lo que no pude verificar, y lo declaro no verificado en vez de suponerlo:**

1. **Las tres fuentes del intake** (`Requerimientos-Funcionales.md`, `Requerimientos-Tecnicos.md`, `Analisis-Actividad-Documento-Integrador.md`) viven en el repositorio `Lab-Geometria.Documentacion`, bajo `PROMPTs/`, fuera de este árbol y fuera de mi alcance de escritura y de lectura para esta ronda. Toda afirmación del intake rotulada `RF §x`, `RT §x` o `AN §x` queda **no verificada contra su origen**: verifiqué la coherencia del intake consigo mismo y con el corpus derivado, no su fidelidad a las tres fuentes.
2. **Los 94 documentos que declaran trazabilidad upstream sin número de versión de intake.** No puedo determinar de qué versión se derivaron, de modo que el recuento de C-08 es un **piso**, no un total.
3. **Las categorías 04 a 09.** Sólo `GeometriaFactory-Web/08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md` existe fuera de 02 y 03; el resto no está emitido y su ausencia no es hallazgo.
4. **La coherencia de los recuentos secundarios por proyecto** —«43 a 42 condiciones» de Domain, «17 condiciones» de Infrastructure, «18 entradas» del catálogo de Api— se muestreó pero no se contó exhaustivamente fila por fila en los siete proyectos. Los que sí conté están en §4.

---

## 9. Dictamen

# RECHAZADO

**Motivo.** Dos P0, los dos en el mismo eje y los dos en el proyecto de código que es dueño de lo que declara mal:

- **C-01** — el documento de concepto del dominio dice, en su tabla de conceptos, lo **contrario** de lo que dice su propia §4.1 y de lo que decidió el Product Owner sobre quién pone la marca de cambio de contraseña pendiente.
- **C-02** — el conjunto cerrado de reglas de negocio **no cierra**: seis declaraciones vivas en tres proyectos dicen quince sobre dieciséis, incluidos los **dos inventarios de archivos** de su propio dueño, que dicen «una por archivo» sobre un directorio de dieciséis.

**Qué hay que hacer para levantar el rechazo.** Las doce correcciones son textuales y ninguna reabre una decisión: seis números, tres frases de origen único, dos constancias que sobran y una cita a una exclusión retirada. Ninguna obliga a re-derivar una categoría ni a subir el intake, salvo **C-09** y **C-10**, que son del intake y pueden agruparse en una emisión 1.15 de transcripción.

**Lo que no motiva el rechazo, y conviene que quede dicho:** ni una sola decisión de negocio está mal entendida en el corpus. Los siete proyectos de código respondieron **igual** a seis de las siete preguntas de coherencia, y la séptima falla en dos lugares puntuales, no en su comprensión.

---

## 10. Estado general de consistencia del corpus

**El corpus está sano y la enfermedad que lo venía aquejando está en retirada.** Es la conclusión que el Product Owner necesita, y la sostengo con lo que conté, no con lo que los documentos dicen de sí mismos.

El defecto que rechazó tres tandas era **estructural**: documentos que afirmaban «el intake declara X» cuando la fuente no decía eso, en cadena, porque cada capa citaba a la anterior sin abrir la fuente. Ese mecanismo **ya no está operando**. El intake 1.14 cerró la última contradicción interna —la de INV-09— y hoy su letra y su decisión coinciden; lo comprobé abriendo la línea. De los doce hallazgos de esta ronda, **uno solo** es una cita falsa a una fuente en el sentido clásico (**C-06**, la exclusión X-2), y **tres más** (**C-03**) son la sombra que deja una corrección hecha bien: documentos que declaran un desfase que existía y ya no existe. Ese es un defecto de una clase mucho más barata, y su presencia es en realidad una **buena señal**: significa que las categorías dejaron constancia en vez de propagar en silencio, que es exactamente lo que se les pidió cuando la fuente estaba rota.

Lo que hoy falla es **la última milla de la propagación**, y falla con un patrón muy nítido y muy corregible: **la decisión llega siempre, el recuento a veces no**. RN-16 está entendida en los siete proyectos —en los wireframes de Web, en el contrato de Contracts, en el punto de acceso de Api, en el productor de la provisoria de Infrastructure, en la máquina de la marca de Domain, en el caso de uso de Application— y sin embargo seis inventarios siguen diciendo quince reglas y tres recuentos derivados siguen diciendo diecisiete códigos. El patrón se repite dentro de un mismo archivo: `Definicion-Modelo-De-Dominio.md` corrigió §4.1, §4.3, §5.1, §5.3 y §9, y no la fila de §2.1; `Api/Definicion-Superficie-HTTP.md` corrigió la tabla, el título de la sección y la prosa, y no el diagrama de tres líneas que está en el medio. Lo que sobrevive es lo que no se lee como texto: **filas de inventario, celdas de tabla y bloques de código**.

La otra cara —y es la que me hace calificar el estado general como **bueno con reservas acotadas** y no como frágil— es la solidez del resto. **Ningún identificador fantasma ni huérfano** en seis familias completas. **Los once conjuntos cerrados que conté cierran**, incluido el de códigos de contrato, que era el riesgo declarado de la ronda y cuyo descenso de diecisiete a quince está no sólo bien contado sino **bien argumentado**, con el motivo de cada retiro y la declaración explícita de que los identificadores no se reciclan. **Las tres reglas de arquitectura no tienen una sola contradicción** en el árbol. **Cero enlaces rotos y cero tablas malformadas** sobre trescientos noventa y cuatro archivos. Y las siete preguntas de coherencia entre capas tienen la misma respuesta en los siete proyectos, con la única excepción de la cuarta, que falla en dos frases identificables y no en la comprensión.

**Recomendación para la próxima tanda.** El corpus ya no necesita que se le audite el razonamiento: lo tiene bien. Necesita que la propagación termine en los tres lugares donde estructuralmente no termina —**inventarios de archivos, recuentos derivados de un conjunto que cambió, y bloques de texto preformateado**—. Un paso mecánico que, ante cada cambio de cardinalidad de un conjunto cerrado, recorra esas tres clases de lugar cerraría de una vez la familia entera de defectos que produjo ocho de los doce hallazgos de esta ronda.

---

## Control de cambios

| Versión | Fecha | Cambio | Autor |
| --- | --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Auditoría de coherencia y consistencia del corpus completo —nivel producto y los siete proyectos de código— contra `PRODUCT-INTAKE` 1.14 y `PRODUCT-MANIFEST` 1.2. **Doce hallazgos**: 2 P0, 4 P1, 4 P2, 2 P3. Verifica las siete preguntas de coherencia entre capas, once conjuntos cerrados contados sobre el instrumento, el alcance real de las citas a versiones archivadas del intake, las tres reglas de arquitectura y tres comprobaciones mecánicas de forma. Dictamen: **RECHAZADO**. | Auditor independiente |
