# Definition of Ready — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** Definition-Of-Ready.md
**Versión:** 2.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**`tipo_unidad_entrega` (D8):** `web-monolith`
**Proyectos de código que la componen:** `GeometriaFactory-Web`, `GeometriaFactory-Visor` y `GeometriaFactory-Contracts`
**Consolida a:** el documento homónimo de `GeometriaFactory-Visor`, por `Audit/Migracion-M10-Consolidacion-Fusion.md` 1.2 §4

---

## 0. Cómo leer este documento

**La unidad de entrega tiene un solo documento de esta clase**, y cada sección lleva **una subsección
por proyecto de código**, con su texto **transpuesto sin reescritura**.

**Las dos secciones de cada apartado son la del portal y la del bundle del visor.** Las dos declaran las mismas secciones: la unidad de entrega es una y el visor viaja adentro.

---

## 1. Criterios DoR para historias de usuario

### 1.1 `GeometriaFactory-Web`

Ocho criterios, todos respondibles con sí o no. Los cuatro últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.
2. **Declara su necesidad de negocio y su etapa del producto**, de las **ocho** que este proyecto de código toca.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara la superficie de [`../03-UX-UI-DX/`](../03-UX-UI-DX/) que la aloja**, de las **once**, y el componente de `05` §3.1 que la sostiene. Una historia que no tenga superficie **no está lista**: o le falta diseño, o no es una historia de esta pieza.
5. **Declara qué restricciones transversales de `02` §6 la alcanzan**, de las **trece**. La historia que introduce interactividad declara además **si puede originar una petición desde el navegador**, y la respuesta admitida es una sola.
6. **Toda condición que la historia presenta a la persona es uno de los diecisiete códigos vivos** del conjunto cerrado de `GeometriaFactory-Contracts`, o el camino de ausencia de respuesta. Una historia que necesite un código nuevo **no está lista**: el conjunto es cerrado y los códigos no se acuñan acá.
7. **Ninguna afirmación de la historia depende de que la pieza pública haga cumplir una regla.** Ocultar un control, no armar una ruta o no ofrecer una acción **acotan lo que se ofrece**; si la historia necesita que algo se haga cumplir, declara **quién lo hace cumplir** y, cuando corresponde, **verifica la acotación forzando la solicitud sin pasar por la pantalla**.
8. **La historia se puede maquetar y validar sin servicio de datos.** Entre una superficie y la salida hay siempre un servicio de aplicación de front (`05` §3.2 punto 1), y es lo que hizo posible la Fase B2. Si no se puede, alguna superficie está invocando al cliente tipado.

### 1.2 `GeometriaFactory-Visor`

Siete criterios, todos respondibles con sí o no. Los tres últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.
2. **Declara su necesidad de negocio y su momento del producto**: la etapa `a`, el momento de medición de `PT-02` y `PT-03`, o la etapa `g`.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §3.1 que la sostiene**, y su capa.
5. **Declara qué garantías del contrato de fachada ejerce**, de las **siete** de [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §3.2, y ninguna de sus afirmaciones contradice a otra garantía.
6. **Todo código de condición que la historia usa es uno de los siete** de ese mismo documento, §6. Una historia que necesite un código nuevo **no está lista**: un código sólo puede nacer allá, y un curso nuevo se agrega como fila de curso y no como código.
7. **Ninguna persona, papel, servicio ni credencial interviene como actor ni condiciona un flujo.** Nombrarlos para declarar qué queda fuera del contrato es obligatorio; que intervengan es un defecto por definición (`02` §2).

## 2. Criterios DoR para tareas técnicas

### 2.1 `GeometriaFactory-Web`

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, un artefacto de la categoría 03 o una puerta técnica del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero peticiones del navegador, cero apariciones de la credencial, cero invocaciones al interior del bundle, cero instancias no liberadas— el criterio se expresa **con umbral cero y con la condición en que se mide**. Un criterio de ausencia sin condición de medición **no está listo**: mediría el caso fácil. La condición de referencia para el conteo de red es **con los dos movimientos automáticos prendidos**, que es el peor caso declarado.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias de `05` §3.2: **ninguna superficie invoca al cliente tipado**, **ninguna superficie invoca al interior del bundle** y **el traductor no habla con el servicio de datos**.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas.
6. **Si el punto abierto que cierra no es de este proyecto de código, lo declara.** BT-10012 adopta el formato que fija `GeometriaFactory-Api`, BT-10023 acompaña una decisión de `09-Devops` y BT-10010, BT-10021 y BT-10022 elevan al Product Owner. **Adoptar y acompañar no es decidir.**

### 2.2 `GeometriaFactory-Visor`

Cinco criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, una puerta técnica del intake §15 o un punto abierto de su §11.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR o la puerta que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia**, el criterio se expresa con umbral cero y con la condición en que se mide. Un criterio de ausencia sin condición de medición **no está listo**: mediría el caso fácil (`02` §6).
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias entre capas: la capa 1 no conoce el interior, la capa 2 no contiene lógica de dibujo y la capa 3 no conoce al anfitrión.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el momento de medición que la cierra**, no en horas.

## 3. Excepciones admitidas

### 3.1 `GeometriaFactory-Web`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historia de las etapas `a` y `b` | **No aplica**: esas dos etapas **no tienen historias**, y su trabajo es íntegramente técnico. Se declara para que la ausencia no se lea como excepción tácita | — |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende del **umbral de tiempo de respuesta**, que no existe | El criterio 3 de §1 se cumple con verificación **cualitativa declarada**, hasta que `PA-06` del backlog se cierre con BT-10021. Es la salida que `05` §8 declara, y **no habilita a inventar un número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que introduce interactividad nueva | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un guion del navegador que llame al servicio de datos rompe `RA-01`, que es la regla que sostiene la topología entera, y `05` §9 le asigna impacto **muy alto** | — |
| Historia que toca el bundle del visor | Ninguno: **no se admite excepción al criterio 4 en su parte de superficie**. Sólo el anfitrión toca la fachada, y sólo por sus **seis** funciones; si una superficie necesita algo que las seis no dan, el procedimiento está en [`Visor Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 y **no** en tocar el interior | — |

### 3.2 `GeometriaFactory-Visor`

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Tarea de indagación que cierra un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el resultado esperado en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende del umbral de fluidez, que no existe | El criterio 3 de §1 se cumple con verificación **cualitativa declarada**, junto con `PT-02`, hasta que `PA-04` del backlog se cierre. Es la salida que `05` §8 declara, y no habilita a inventar un número | El Product Owner, o 08 al fijar su guion de medición |
| Historia que introduce comportamiento en la capa 3 | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Perder una garantía es un cambio mayor aunque las seis firmas no se toquen (`05` §10.2), y acuñar un código aguas abajo desincroniza 02, 03 y 08 (`05` §9, sexto riesgo) | — |

## 4. Aprobador

### 4.1 `GeometriaFactory-Web`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **puertas medidas** del flujo de publicación: construcción sin advertencias, bundle generado en el mismo flujo y **comprobación de que la dirección pública responde**.
3. Las **cuatro mediciones de `PT-01`**, que se hacen en la etapa `a` **antes que cualquier otra cosa** y de las que depende el modelo entero de esta pieza. `PT-01.c` es además el peor escenario del producto y la fuente declara que **no tiene mitigación en el código**.

### 4.2 `GeometriaFactory-Visor`

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son dos cosas, y en este proyecto de código la segunda es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **dos puertas técnicas medidas sobre el artefacto generado**, `PT-02` y `PT-03`. No dependen de que alguien las revise: se miden, y una puerta que no pasa **detiene la planificación de la etapa `g`** en lugar de arrastrarse como deuda (`Roadmap-Producto.md` §2.2).

## 5. Qué no es esta DoR

### 5.1 `GeometriaFactory-Web`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona el guion acumulativo ejecutado al cien por ciento, ni las 61 filas de la matriz de deriva verificadas, ni los diez recorridos sin degradación, que son condiciones de cierre.

**No es el diseño de las superficies.** Las once superficies, sus estados, sus interacciones y su línea de base visual están en la categoría 03, **emitida y validada contra una maqueta aprobada**. Esta DoR exige que la historia **declare** su superficie, no que la describa.

**Y no redacta reglas de negocio.** Las **dieciséis** viven en `GeometriaFactory-Domain`, y `02` §5 declara por qué esta pieza no las hace cumplir.

### 5.2 `GeometriaFactory-Visor`

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5 y las dos puertas técnicas. Esta DoR habla de **cuándo empezar**: no menciona los diez recorridos sin degradación, ni la medición de peticiones con los movimientos prendidos, ni la página integradora funcionando, que son condiciones de cierre.

## 6. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 2.0 | 2026-08-16 | **Consolidación de la fusión.** Pasa a ser el documento de la **unidad de entrega**, absorbiendo el de `GeometriaFactory-Visor`, con su texto transpuesto sin reescritura. Entra §0. Sube **major**. |
