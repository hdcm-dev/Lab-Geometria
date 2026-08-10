# 03 · UX / UI / DX — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** README.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/` completo —`Especificacion-Funcional.md` con sus §6 y §7, los diez casos de uso `CU-01` a `CU-10` y `Glosario-Funcional.md`—; `../../../00-Contexto/Vision-Producto.md` §2, §3, §9; `../../../00-Contexto/Alcance-Producto.md` §4.1; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §4; las nueve `NB-XX` de `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/`; `../../GeometriaFactory-Visor/02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4, §4.2, §5, §6, §7, §14 y §17.6 íntegro; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` §5 (flags del proyecto de código); catálogo de diseño de `Devs/References/Design/`
**Trazabilidad downstream:** la **Fase B2** de validación visual de maqueta, que es el downstream inmediato; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`; `11-Documentacion`

---

## Tabla de contenido

- [1. Qué hay en esta carpeta](#1-qué-hay-en-esta-carpeta)
- [2. Variante aplicada y por qué](#2-variante-aplicada-y-por-qué)
- [3. Orden de lectura sugerido](#3-orden-de-lectura-sugerido)
- [4. Las once superficies y su criterio de recorte](#4-las-once-superficies-y-su-criterio-de-recorte)
- [5. Artefactos previstos para la Fase B2](#5-artefactos-previstos-para-la-fase-b2)
- [6. Artefactos omitidos y su motivo](#6-artefactos-omitidos-y-su-motivo)
- [7. Catálogos de diseño aplicados y no aplicados](#7-catálogos-de-diseño-aplicados-y-no-aplicados)
- [8. Notas de uso de esta sección](#8-notas-de-uso-de-esta-sección)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Qué hay en esta carpeta

**Veinte documentos**: el marco de experiencia, once wireframes, tres representaciones reutilizadas, el glosario, este índice y los **tres artefactos de línea de base que la Fase B2 emitió al aprobarse la maqueta**.

| Documento | Propósito | Estado |
| --- | --- | --- |
| [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) | Marco de experiencia con las once secciones obligatorias: audiencia, principios, flujos clave, estados, accesibilidad, internacionalización, performance percibida, errores y recuperación. **Es el punto de entrada** | Propuesto |
| [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md) | Primer arranque: la única puerta de un laboratorio recién desplegado | Propuesto |
| [`Wireframes-Registro-De-Cuenta.md`](Wireframes-Registro-De-Cuenta.md) | Alta del alumno, sin contraseña y sin correo | Propuesto |
| [`Wireframes-Ingreso.md`](Wireframes-Ingreso.md) | Acceso, y el lugar al que vuelve todo lo que no tiene sesión | Propuesto |
| [`Wireframes-Credencial-Propia.md`](Wireframes-Credencial-Propia.md) | Establecer y cambiar la contraseña, con sus **tres** cursos —incluido el **cambio forzado** tras un reseteo— y sus dos shells | Propuesto |
| [`Wireframes-Panel-De-Trabajos-Del-Alumno.md`](Wireframes-Panel-De-Trabajos-Del-Alumno.md) | Listado propio con los cuatro estados y sus acciones por estado | Propuesto |
| [`Wireframes-Envio-De-Trabajo.md`](Wireframes-Envio-De-Trabajo.md) | La acción única de guardado, con previsualización que dibuja y no verifica | Propuesto |
| [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md) | La superficie de cuatro partes, **con su disposición decidida aguas arriba** | Propuesto |
| [`Wireframes-Resolucion-Del-Trabajo.md`](Wireframes-Resolucion-Del-Trabajo.md) | Desenlace con comentario opcional, y retiro. Alojada en la vista de trabajo | Propuesto |
| [`Wireframes-Panel-De-Cuentas.md`](Wireframes-Panel-De-Cuentas.md) | Las **cinco** operaciones sobre las cuentas —incluido el **reseteo de contraseña**—, con la confirmación escrita de la baja | Propuesto |
| [`Wireframes-Listado-De-La-Comision.md`](Wireframes-Listado-De-La-Comision.md) | La entrega agrupada y filtrada por alumno, **sin borradores** | Propuesto |
| [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) | **El estado degradado como superficie**, con sus dos tramos independientes | Propuesto |
| [`Representacion-Fila-De-Trabajo.md`](Representacion-Fila-De-Trabajo.md) | La fila y su insignia de estado, usadas en cinco artefactos | Propuesto |
| [`Representacion-Lista-De-Observaciones.md`](Representacion-Lista-De-Observaciones.md) | Las observaciones y el par declarado/derivado, usadas en dos superficies | Propuesto |
| [`Representacion-Sello-De-Version.md`](Representacion-Sello-De-Version.md) | El sello y su detalle de diagnóstico, en las dos ubicaciones obligatorias | Propuesto |
| [`Glosario-UX.md`](Glosario-UX.md) | Vocabulario de superficie, la resolución de «panel» y «sección», y las diez palabras que esta categoría deliberadamente no usa | Propuesto |
| `README.md` | Este archivo | Propuesto |
| [`Linea-Base-Visual.md`](Linea-Base-Visual.md) | Inventario identificado de lo aprobado al mirar la maqueta: once superficies, setenta y tres componentes, setenta y cuatro estados y veinticuatro rutas | Propuesto |
| [`Contrato-Datos-Maqueta.md`](Contrato-Datos-Maqueta.md) | Los veintinueve campos que la maqueta exhibe, con su tipo, su ejemplo, sus superficies y su correspondencia con el modelo conceptual del dominio | Propuesto |
| [`Bitacora-Validacion-Maqueta.md`](Bitacora-Validacion-Maqueta.md) | Las cuatro iteraciones de validación, la aprobación explícita del Product Owner y los seis hallazgos con su destino de propagación | Propuesto |

**Once wireframes**, sobre el mínimo de **cuatro** que `Rules-UX-UI-DX.md` §2.2 fija para `web-monolith`. El mínimo es piso; el techo lo fijó la cobertura de los diez casos de uso.

## 2. Variante aplicada y por qué

**Variante UX/UI**, leída de `Rules-UX-UI-DX.md` §1.2, fila `web-monolith`: el usuario final recorre superficies en un navegador, y se diseñan experiencia, disposición, estados y accesibilidad. El flag `tiene_ui_final` es true.

**Es el primer proyecto de código de este producto que diseña para una persona.** Los cinco anteriores eran de variante DX y se consumen por código; sus casos de uso tenían por actor al código que los invoca. Acá los actores son un alumno y un docente, en un navegador, y lo que esta sección escribe es lo que después se materializa en una maqueta navegable que el docente va a mirar y corregir.

No hay combinación con la variante DX: el flag `tiene_portal_developers` es false y este proyecto de código **no expone contrato a nadie**. Es hoja del grafo de dependencias y punto de entrada del usuario final.

## 3. Orden de lectura sugerido

1. [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) **§2.4**, las tres reglas de arquitectura traducidas a restricción de diseño. Son media página y es lo primero que hay que haber entendido: **una superficie que las viole es un defecto, no una alternativa**.
2. [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.1 y §3.2, el mapa de las once superficies y los dos shells. Es el índice mental de todo lo demás.
3. [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md), la superficie más consecuente del producto y la única cuya disposición **viene decidida aguas arriba**.
4. [`Wireframes-Estado-Degradado-Y-Reconexion.md`](Wireframes-Estado-Degradado-Y-Reconexion.md) §1, la tabla que separa los dos tramos. Conviene leerla temprano aunque nada esté fallando: confundirlos es el error de lectura más probable de toda la sección.
5. [`Wireframes-Aprovisionamiento-Inicial.md`](Wireframes-Aprovisionamiento-Inicial.md), la superficie que se usa una vez en la vida de la instancia y que, mal diseñada, deja el producto inutilizable.
6. [`Glosario-UX.md`](Glosario-UX.md) §3 y §4, si quien lee viene de otra categoría: «panel» y «sección» tienen dos referentes acá, y hay diez palabras de superficie que deliberadamente no se usan.
7. El resto de los wireframes, en el orden de la tabla de §1, que sigue el recorrido de las personas.
8. Las tres representaciones, que se leen sueltas y que los wireframes invocan por nombre.

## 4. Las once superficies y su criterio de recorte

El criterio es **una superficie por caso de uso**, con dos ajustes y una fusión, todos declarados en [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.1.

| Superficie | Caso de uso |
| --- | --- |
| `Aprovisionamiento-Inicial` | CU-04 FA-03 y FA-04 |
| `Registro-De-Cuenta` | CU-01 |
| `Ingreso` | CU-02 |
| `Credencial-Propia` | CU-03 |
| `Panel-De-Trabajos-Del-Alumno` | CU-06 |
| `Envio-De-Trabajo` | CU-05 |
| `Vista-De-Trabajo` | CU-07 |
| `Resolucion-Del-Trabajo` | CU-09 |
| `Panel-De-Cuentas` | CU-04 flujo principal, FA-01, FA-02, FA-05 |
| `Listado-De-La-Comision` | CU-08 |
| `Estado-Degradado-Y-Reconexion` | CU-10 |

- **CU-04 se parte en dos superficies**, porque su aprovisionamiento vive en otro shell, se usa una vez en la vida de la instancia y tiene guard propio. La partición es de presentación y no contradice la fusión que la categoría 02 decidió: allá el criterio de recorte era el objeto sobre el que se actúa, acá es la unidad maquetable.
- **CU-10 se emite como superficie propia aunque sea transversal**, porque el estado degradado es una superficie y no un error, y documentarlo una vez evita que diez wireframes lo redibujen distinto.
- **CU-09 no tiene ruta propia**: su bloque de decisión se aloja en `Vista-De-Trabajo`. Se emite como wireframe separado porque tiene mapa de estados e interacciones propios y porque la maqueta lo va a tener que demostrar como recorrido.

Cada superficie declara su **nombre canónico** en la sección 1 de su wireframe, y ése es el nombre que la maqueta y la línea de base visual van a reusar sin cambios.

## 5. Artefactos previstos para la Fase B2

`requiere_maqueta` era **true** y **la fase se ejecutó**: la maqueta de `SDD/Maquetas/GeometriaFactory-Web/` quedó **aprobada por el Product Owner el 2026-08-09**, tras cuatro iteraciones. Los tres artefactos que esta sección declaraba previstos **están emitidos**.

| Artefacto | Quién lo emitió | Cuándo | Qué contiene |
| --- | --- | --- | --- |
| [`Linea-Base-Visual.md`](Linea-Base-Visual.md) | AG-03M | Fase B2, al aprobarse la maqueta | Inventario identificado: once `SUP-XX`, setenta y tres `CMP-XX`, setenta y cuatro `EST-XX` y veinticuatro `NAV-XX` |
| [`Contrato-Datos-Maqueta.md`](Contrato-Datos-Maqueta.md) | AG-03M | Ídem | Veintinueve `DM-XX` con su tipo, ejemplo, superficies y correspondencia con el modelo conceptual del dominio, más el formato de presentación como parte del contrato |
| [`Bitacora-Validacion-Maqueta.md`](Bitacora-Validacion-Maqueta.md) | AG-03M | Fase B2, durante | Las cuatro iteraciones con su vía, observación, cambio y documento retroalimentado; la aprobación explícita; los seis hallazgos con su destino |

**Un cuarto artefacto salió de esta carpeta.** La matriz de sensado de deriva que `Deriva-Rules.md` §2.3 fija vive en `../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`, porque es un instrumento de verificación y 08 es la categoría dueña de la verificación. Es el **único** artefacto de esa categoría emitido por la Fase B2: el resto de 08 se genera en la Fase E.

**Qué le toca a esta sección antes de la Fase B2, y ya está hecho:** cada wireframe declara su nombre canónico de superficie; la sección 5 de cada wireframe es **la lista de estados que la maqueta va a tener que demostrar**, y un estado no declarado ahí no se maqueta y por lo tanto no se valida; los seis flujos clave de [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3 son las rutas de navegación que la maqueta va a materializar. Esta sección **no dibuja la maqueta ni fija valores visuales concretos**: sigue rigiendo el anti-patrón de wireframe con detalle de estilo.

**Qué le tocó después, y ya está hecho:** esta sección recibió la retroalimentación del paso 6. Los ocho artefactos afectados —`Experiencia-De-Uso.md`, `Glosario-UX.md`, los cuatro wireframes de resolución, vista de trabajo, listado de la comisión y panel de trabajos del alumno, el wireframe de envío y la representación de lista de observaciones— absorbieron los cambios **sin subir versión**, por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`, y cada uno sumó a su control de cambios el motivo de la validación de maqueta. Ninguno requirió archivado previo.

**El modelo UX-UI quedó elegido en el paso 1 de la Fase B2**, y la elección la tomó el humano sobre lo que había para ofrecer: el catálogo de `Devs/Modelos-UX-UI/` **estaba vacío** —su índice lo declara: «el catálogo arranca vacío»—, de modo que la única opción ofrecible era la de por defecto. Se aplicó el **catálogo base de `References/Design/`**: `Design-Rules-Web-Generico` + `Design-Rules-Primer-Arranque` + `Design-Rules-Identidad-De-Version`, que es lo que la maqueta declara en su `README.md` y en el sello de cada página. No se capitalizó ningún modelo nuevo: `Maqueta-Rules.md` §3.7 lo admite, y la fase cierra sin entrada en el índice de modelos ni template ofuscado.

## 6. Artefactos omitidos y su motivo

`Rules-UX-UI-DX.md` §2.1 define trece artefactos posibles. Se emiten seis clases —marco, wireframes, representaciones, glosario, README, más los tres previstos de la Fase B2— y se omiten cinco, cada una con el fundamento que la propia regla declara.

| Artefacto omitido | Regla que lo admite u obliga | Motivo de la omisión |
| --- | --- | --- |
| `DX-Developer-Experience.md` | §2.1 lo marca obligatorio para `library`, `cli-tool`, `worker-service`, `rest-api` y `web-microservices` sin frontend, y **omitible para tipos con UI final únicamente** | `tiene_ui_final` es true y el tipo es `web-monolith`. Este proyecto de código **no expone contrato a nadie**: es hoja del grafo y punto de entrada del usuario final. La experiencia de developer del producto vive en las categorías 03 de los cinco proyectos de código de variante DX |
| `Guia-Onboarding-Developer.md` | §2.1 lo marca obligatorio para `library`, `rest-api` y `web-microservices` sin frontend | No hay integrador que recorra una primera hora: nadie compila contra este proyecto de código |
| `DX-Error-Messages.md` | §2.1 lo marca obligatorio para `cli-tool` y `library`, recomendado para `rest-api` y `worker-service` | Los mensajes de este proyecto de código son de superficie y no de catálogo público: viven en la sección 5 de cada wireframe y en §8 de [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md). El catálogo de códigos que este proyecto de código **consume** está en la categoría 03 de `GeometriaFactory-Contracts` |
| `DX-Portal-Developers.md` | §2.1 lo marca obligatorio para `rest-api` con portal visible y para `web-microservices` con SDK público | `tiene_portal_developers` es false. No hay integradores externos y `redistribuible` es false |
| `DX-Operability.md` | §2.1 lo marca obligatorio para `worker-service` | El tipo D8 es `web-monolith`. La experiencia del operador de las dos piezas desplegables es de `09-Devops` |

## 7. Catálogos de diseño aplicados y no aplicados

`Index-Design-Rules.md` es el punto de entrada del catálogo. Se aplican cuatro documentos y **dos no aplican, con su motivo declarado**, en lugar de darse por omitidos en silencio. El desarrollo completo está en [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §2.3.

| Documento | Estado | Motivo |
| --- | --- | --- |
| `Design-Rules-Web-Generico.md` | **Aplicado** | Es la base y siempre aplica |
| `Design-Rules-Blazor-Mudblazor.md` | **Aplicado** | Es la especialización del stack declarado en `PRODUCT-INTAKE` §17.6 P.1, que es exactamente el de este proyecto de código |
| `Design-Rules-Primer-Arranque.md` | **Aplicado** | El producto se despliega por instancia y arranca sin la configuración mínima que lo hace utilizable: no hay administrador hasta que alguien lo configura |
| `Design-Rules-Identidad-De-Version.md` | **Aplicado** | El producto produce artefactos desplegables identificables y esta pieza es la que tiene superficies donde exhibir el sello |
| `Design-Rules-Config-Esquema.md` | **No aplica** | **No hay superficies de configuración que la persona fije.** El único parámetro configurable del proyecto de código es la dirección de la pieza de datos, que es **configuración de entorno** —se inyecta al publicar— y no configuración de aplicación. Por la frontera de `Rules-UX-UI-DX.md` §1.4, un parámetro que la superficie no gobierna **no se dibuja, ni siquiera deshabilitado** |
| `Design-Rules-Acceso-Monousuario.md` | **No aplica** | El producto declara **dos papeles** y tiene gestión de cuentas: registro, habilitación, bloqueo y baja. La condición de carga de la extensión es una sola identidad de operación **sin** gestión de usuarios ni papeles diferenciados, y no se cumple. Rige el patrón de acceso general del documento base. Lo que sí se hereda por coincidencia de forma, y se declara para que no parezca omisión, es el shell partido —que la extensión de primer arranque también fija— y el rechazo de credenciales indiferenciado, que acá viene exigido por `CU-02` §6 y no por esta extensión |

**Ningún token visual se define en esta sección.** Todos se heredan del catálogo, y los patrones se referencian por su nombre del catálogo en lugar de redibujarse.

## 8. Notas de uso de esta sección

- **Autoridad.** Esta sección no origina ninguna capacidad, prioridad ni exclusión. Todo se deriva de los diez casos de uso de `../02-Especificacion-Funcional/`, de sus **trece restricciones transversales** `RT-01` a `RT-13`, del contrato de fachada del visualizador y del `PRODUCT-INTAKE`, y traza a su sección de origen.
- **Fronteras.** La arquitectura de la capa de presentación y los registros de decisión son de `05`; las historias de usuario, de `06`; el plan de sprint, de `07`; las pruebas, de `08`; la construcción y la publicación, de `09`; el cuerpo documental de entrega, de `11`. Y **la maqueta es de la Fase B2**: esta sección la alimenta y no la especifica.
- **Las tres reglas de arquitectura son restricción de diseño.** Ningún guion del navegador invoca la pieza de datos —de ahí que no haya actualizaciones parciales, ni validación remota al escribir, ni sondeo de estado—; el bundle del visualizador se opera sólo por sus **seis** funciones, **es la pieza pública la que consulta el entorno del navegador y le manda dos valores de verdad** para gobernar el movimiento automático, y ninguna superficie manipula el elemento de dibujo por su cuenta; y **ningún mensaje visible incluye la dirección de un servicio interno**. Una superficie que las viole es un defecto.
- **Lo que viene decidido aguas arriba y no se rediseña.** La disposición de cuatro partes de la vista de trabajo, probada en el aula. Que el alumno vea el desenlace en el **estado de su listado** y el comentario **al abrir el trabajo**, porque el listado no arrastra texto libre. Que el administrador **no vea los borradores**. Que sólo el borrador se edite y se elimine. Que los dos estados de desenlace sean **terminales**.
- **Accesibilidad.** **WCAG 2.2 nivel AA es el piso obligatorio**, no una mejora deseable: es un producto educativo de una universidad pública. Cada wireframe lleva sus notas propias en su sección 7, el compromiso general y el plan de verificación están en [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §5, y la resolución del punto difícil —la escena tridimensional— está en §5.2 y en la sección 7 de [`Wireframes-Vista-De-Trabajo.md`](Wireframes-Vista-De-Trabajo.md).
- **Capacidades que el producto no tiene y que ninguna superficie inventa.** No hay **recuperación autónoma** de contraseña —desde el `PRODUCT-INTAKE` 1.7 sí hay **reseteo por el administrador**, que es otra cosa: lo ejerce el docente desde `Panel-De-Cuentas` y no la persona desde `Ingreso`—, no hay notificaciones por correo, no hay calificación con nota, no hay múltiples administradores, no hay papeles configurables y no hay canal de soporte. Las ausencias que la persona podría buscar están **declaradas en la superficie** —la nota de la contraseña olvidada en `Ingreso`, que ahora **dice qué pedirle al docente** en lugar de advertir una pérdida, y el subtítulo de expectativa en `Registro-De-Cuenta`—, porque una ausencia silenciosa se busca y una declarada no.
- **Vocabulario.** El del dominio está en `Vision-Producto.md` §9 y el funcional en `../02-Especificacion-Funcional/Glosario-Funcional.md`; acá no se redefinen. **«Vista» no se reabre**: su polisemia está resuelta aguas arriba con forma calificada obligatoria y esta sección respeta esa resolución. `Pendiente` va **siempre calificado**, salvo en las enumeraciones del conjunto cerrado y en los identificadores literales. «Pieza» va calificada para los servicios desplegables. **«Trabajo» no es «unidad de entrega»**, «observación» es el superordinado de «advertencia» y «error de validación», **el comentario del administrador no es una observación ni una calificación**, y la palabra «proyecto» a secas no se usa. Lo que esta categoría acuña, y las diez palabras que deliberadamente no usa, están en [`Glosario-UX.md`](Glosario-UX.md).
- **Nombres de archivo.** Ningún archivo vivo de esta carpeta lleva sufijo de versión: cada uno declara su versión en el campo `Versión` de su cabecera. No hay `_legacy/` porque no hay ninguna versión superada: es la emisión inicial de la sección.
- **Supuestos abiertos.** Están rotulados en [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §10: el punto de quiebre en 768 px y la proporción de la escena, los dos sujetos a la validación visual de la Fase B2; y el volumen de la comisión, marcado para verificar, del que depende la decisión de no incorporar paginación en `Listado-De-La-Comision`.

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial del índice de la sección. Enumera los diecisiete documentos emitidos, declara la variante UX/UI con su fundamento y la novedad de ser el primer proyecto de código del producto que diseña para una persona, fija el orden de lectura, declara las once superficies con su criterio de recorte y sus dos ajustes, declara los **tres artefactos de la Fase B2 como previstos y no como omitidos** con lo que a esta sección le toca antes y después de esa fase, declara las cinco omisiones de variante DX con la regla que las admite, y declara los cuatro catálogos de diseño aplicados y los dos que no aplican con su motivo. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-07**: §1 y el control de cambios corrigen el recuento de documentos de la sección, que es **diecisiete** y no dieciséis: 1 marco + 11 wireframes + 3 representaciones + 1 glosario + este índice. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, **sin subir versión** por `Master-Prompt.md` §5. §1 pasa de diecisiete a **veinte** documentos y lista los tres artefactos de línea de base emitidos. §5 pasa de declararlos previstos a declararlos **emitidos**, con la aprobación explícita del Product Owner del 2026-08-09, y declara el cuarto artefacto que la fase emitió fuera de esta carpeta: la matriz de sensado de deriva, en `../08-Calidad-Y-Pruebas/`. El párrafo «qué le toca después» pasa a declarar qué artefactos absorbieron la retroalimentación y por qué ninguno subió versión ni requirió archivado. |
| 1.0 | 2026-08-09 | Corrección absorbida de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, **sin subir versión** por `Master-Prompt.md` §5. **`AB2-11`**: §5 afirmaba que «el modelo UX-UI de `Devs/Modelos-UX-UI/` no está elegido a esta fecha» en el mismo documento que declara la Fase B2 ejecutada y cerrada. Pasa a declarar el **resultado del paso 1**: el catálogo de modelos estaba vacío, la única opción ofrecible era la de por defecto, el humano eligió el **catálogo base de `References/Design/`**, y no se capitalizó ningún modelo nuevo. |
| 1.1 | 2026-08-09 | **Propagación del `PRODUCT-INTAKE` 1.7**, con sus dos decisiones. **(a) F-26**: §1 declara el **tercer curso** de `Credencial-Propia`, y §6 corrige la enumeración de capacidades ausentes, que decía «no hay recuperación de contraseña» sin más: lo que no hay es recuperación **autónoma**, y el reseteo por el administrador sí existe. **(b) F-25**: §6 corrige la formulación de RA-02, que declaraba **cinco** funciones del bundle y son **seis**, y suma que es la pieza pública la que consulta el entorno del navegador y manda los dos valores de verdad. **Los recuentos de la sección no cambian**: siguen siendo veinte documentos, once wireframes y once superficies; F-26 no agrega superficie, porque el cambio forzado es un curso de `Credencial-Propia` y el reseteo es una operación de `Panel-De-Cuentas`. |
