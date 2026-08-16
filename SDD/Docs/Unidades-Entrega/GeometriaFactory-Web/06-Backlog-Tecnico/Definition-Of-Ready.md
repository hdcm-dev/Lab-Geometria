# Definition of Ready — GeometriaFactory-Web

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** Definition-Of-Ready.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-12
**Autor:** Scrum Master (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.7 §3, §5, §6 y §7; [`../03-UX-UI-DX/Experiencia-De-Uso.md`](../03-UX-UI-DX/Experiencia-De-Uso.md) y [`../03-UX-UI-DX/Linea-Base-Visual.md`](../03-UX-UI-DX/Linea-Base-Visual.md); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.0 §3.1, §3.4, §8 y §9; [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md); [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Web

---

## Tabla de contenido

- [1. Criterios DoR para historias de usuario](#1-criterios-dor-para-historias-de-usuario)
- [2. Criterios DoR para tareas técnicas](#2-criterios-dor-para-tareas-técnicas)
- [3. Excepciones admitidas](#3-excepciones-admitidas)
- [4. Aprobador](#4-aprobador)
- [5. Qué no es esta DoR](#5-qué-no-es-esta-dor)
- [6. Control de cambios](#6-control-de-cambios)

---

## 1. Criterios DoR para historias de usuario

Ocho criterios, todos respondibles con sí o no. Los cuatro últimos son propios de este proyecto de código.

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) §3.
2. **Declara su necesidad de negocio y su etapa del producto**, de las **ocho** que este proyecto de código toca.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara la superficie de [`../03-UX-UI-DX/`](../03-UX-UI-DX/) que la aloja**, de las **once**, y el componente de `05` §3.1 que la sostiene. Una historia que no tenga superficie **no está lista**: o le falta diseño, o no es una historia de esta pieza.
5. **Declara qué restricciones transversales de `02` §6 la alcanzan**, de las **trece**. La historia que introduce interactividad declara además **si puede originar una petición desde el navegador**, y la respuesta admitida es una sola.
6. **Toda condición que la historia presenta a la persona es uno de los diecisiete códigos vivos** del conjunto cerrado de `GeometriaFactory-Contracts`, o el camino de ausencia de respuesta. Una historia que necesite un código nuevo **no está lista**: el conjunto es cerrado y los códigos no se acuñan acá.
7. **Ninguna afirmación de la historia depende de que la pieza pública haga cumplir una regla.** Ocultar un control, no armar una ruta o no ofrecer una acción **acotan lo que se ofrece**; si la historia necesita que algo se haga cumplir, declara **quién lo hace cumplir** y, cuando corresponde, **verifica la acotación forzando la solicitud sin pasar por la pantalla**.
8. **La historia se puede maquetar y validar sin servicio de datos.** Entre una superficie y la salida hay siempre un servicio de aplicación de front (`05` §3.2 punto 1), y es lo que hizo posible la Fase B2. Si no se puede, alguna superficie está invocando al cliente tipado.

## 2. Criterios DoR para tareas técnicas

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, un artefacto de la categoría 03 o una puerta técnica del intake §15.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero peticiones del navegador, cero apariciones de la credencial, cero invocaciones al interior del bundle, cero instancias no liberadas— el criterio se expresa **con umbral cero y con la condición en que se mide**. Un criterio de ausencia sin condición de medición **no está listo**: mediría el caso fácil. La condición de referencia para el conteo de red es **con los dos movimientos automáticos prendidos**, que es el peor caso declarado.
4. **Sus dependencias sobre otras tareas están declaradas y ninguna es circular**, y ninguna cruza la regla de dependencias de `05` §3.2: **ninguna superficie invoca al cliente tipado**, **ninguna superficie invoca al interior del bundle** y **el traductor no habla con el servicio de datos**.
5. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas.
6. **Si el punto abierto que cierra no es de este proyecto de código, lo declara.** BT-10012 adopta el formato que fija `GeometriaFactory-Api`, BT-10023 acompaña una decisión de `09-Devops` y BT-10010, BT-10021 y BT-10022 elevan al Product Owner. **Adoptar y acompañar no es decidir.**

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historia de las etapas `a` y `b` | **No aplica**: esas dos etapas **no tienen historias**, y su trabajo es íntegramente técnico. Se declara para que la ausencia no se lea como excepción tácita | — |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende del **umbral de tiempo de respuesta**, que no existe | El criterio 3 de §1 se cumple con verificación **cualitativa declarada**, hasta que `PA-06` del backlog se cierre con BT-10021. Es la salida que `05` §8 declara, y **no habilita a inventar un número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia que introduce interactividad nueva | Ninguno: **no se admite excepción al criterio 5 ni al 6**. Un guion del navegador que llame al servicio de datos rompe `RA-01`, que es la regla que sostiene la topología entera, y `05` §9 le asigna impacto **muy alto** | — |
| Historia que toca el bundle del visor | Ninguno: **no se admite excepción al criterio 4 en su parte de superficie**. Sólo el anfitrión toca la fachada, y sólo por sus **seis** funciones; si una superficie necesita algo que las seis no dan, el procedimiento está en [`Visor Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md) §5 y **no** en tocar el interior | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **puertas medidas** del flujo de publicación: construcción sin advertencias, bundle generado en el mismo flujo y **comprobación de que la dirección pública responde**.
3. Las **cuatro mediciones de `PT-01`**, que se hacen en la etapa `a` **antes que cualquier otra cosa** y de las que depende el modelo entero de esta pieza. `PT-01.c` es además el peor escenario del producto y la fuente declara que **no tiene mitigación en el código**.

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona el guion acumulativo ejecutado al cien por ciento, ni las 61 filas de la matriz de deriva verificadas, ni los diez recorridos sin degradación, que son condiciones de cierre.

**No es el diseño de las superficies.** Las once superficies, sus estados, sus interacciones y su línea de base visual están en la categoría 03, **emitida y validada contra una maqueta aprobada**. Esta DoR exige que la historia **declare** su superficie, no que la describa.

**Y no redacta reglas de negocio.** Las **dieciséis** viven en `GeometriaFactory-Domain`, y `02` §5 declara por qué esta pieza no las hace cumplir.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **ocho** criterios de entrada para las historias —cuatro de ellos propios de este proyecto de código: la superficie que la aloja, las restricciones transversales que la alcanzan, que ninguna afirmación dependa de que la pieza pública haga cumplir una regla, y que la historia se pueda maquetar sin servicio de datos— y **seis** para las tareas técnicas, incluido el que exige que toda tarea que adopte o acompañe una decisión ajena lo declare. Declara cinco casos de excepción, dos de ellos negativos y sin excepción posible, y uno que registra que las etapas `a` y `b` no tienen historias; el aprobador con la constancia de que el filtro más duro son las cuatro mediciones de `PT-01`; y la delimitación contra la Definition of Done y contra el diseño de las superficies. |
| 1.1 | 2026-08-12 | **Absorbe la decisión (a) del Product Owner** (`PRODUCT-INTAKE` **1.29** §17.4 P.3): entran al conjunto cerrado del contrato `CONTRATO_OPERACION_EXCLUSIVA_DEL_ADMINISTRADOR` —el papel no alcanza **fuera del desenlace**: gobernar cuentas (F-03), resetear la contraseña de una cuenta de alumno (F-26) y ver el listado de la comisión (F-12)— y `CONTRATO_ESTADO_NO_PERMITE_MODIFICAR` —enviar o reeditar un trabajo en `Pendiente`, `Finalizado` o `Rechazado`—. El conjunto pasa de **quince a diecisiete vivos** sobre **veinte** identificadores emitidos, con los **tres retirados intactos y ninguno reciclado**; `GeometriaFactory-Contracts` los emite formalmente en su `Contratos-Abstractions.md` §5.1. `CONTRATO_DESENLACE_EXCLUSIVO_DEL_ADMINISTRADOR` y `CONTRATO_ESTADO_NO_PERMITE_ELIMINAR` **no cambian de enunciado**. Acá se actualizan los recuentos que citaban el conjunto, y **ninguna otra decisión, contrato o caso de prueba cambia**. **Alcance de la búsqueda de propagación**: `grep` sobre todo el árbol vivo de `SDD/Docs/` —excluidos `Audit/` y `_legacy/`— por «quince», «dieciocho», «catorce», «15», «18» y «14» en contexto de código del contrato, más `CONJUNTO_DE_PIEZAS_NO_RECONSTRUIDO`, `PA-XX` y «E-2 y E-5». Alcanzó **167 documentos** y **420 lugares**; en este documento, **1**. Sube minor. |
