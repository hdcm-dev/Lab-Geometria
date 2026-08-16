# Definition of Ready — GeometriaFactory-Infrastructure

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Infrastructure
**Documento:** Definition-Of-Ready.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Scrum Master + Backlog Curator (AG-06)
**Trazabilidad upstream:** [`Product-Backlog.md`](Product-Backlog.md) 1.0 §5; [`Backlog-Tecnico.md`](Backlog-Tecnico.md) 1.0 §3; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.3 §3, §4 y §6; [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../../../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); las **siete** reglas conceptuales de [`../02-Especificacion-Funcional/Modelo-Datos/`](../02-Especificacion-Funcional/Modelo-Datos/); [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Infrastructure/DX-Error-Messages.md) (las **17** condiciones); [`../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) 1.1 §3.1, §8 y §9; [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) 1.5 §5.1
**Trazabilidad downstream:** `07-Plan-Sprint` de GeometriaFactory-Infrastructure

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

1. **Traza a un caso de uso.** La historia declara al menos un `CU-XX` de [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../../02-Especificacion-Funcional/Especificacion-Funcional.md) §5.
2. **Declara su necesidad de negocio y su etapa del producto.** Con **una excepción declarada**: las dos historias de `CU-06009` pueden no declarar necesidad, porque `02` §7.2 declara que ese caso de uso **no traza a ninguna** y explica por qué.
3. **Tiene criterios de aceptación en Given/When/Then, con al menos dos escenarios**, uno de camino feliz y uno de borde.
4. **Declara el componente de `05` §3.1 que la sostiene**, y si toca el almacén, **las reglas conceptuales de modelo que materializa**, de las siete.
5. **Declara que no toma ninguna decisión de negocio.** Una historia que decida un estado, una autorización, una admisibilidad o el resultado de una comparación de confirmación **está mal ubicada**: `02` §4 lo enuncia en una línea, y esta capa **provee el mecanismo**.
6. **Toda condición que la historia produce existe en el catálogo de las 17** de [`../03-UX-UI-DX/DX-Error-Messages.md`](../../../03-UX-UI-DX/_fusion/Infrastructure/DX-Error-Messages.md), y la historia declara si es un **resultado** o un **fallo**. Confundirlos es el segundo riesgo de `05` §9, con probabilidad **alta**.
7. **Si la historia tiene un camino en el que un mecanismo no puede cumplir su promesa, declara que se detiene y lo dice.** No la cumple a medias, no compone el valor por otro medio y no cae hacia un sustituto. Una historia sin ese camino declarado **no está lista** cuando el mecanismo puede fallar.
8. **Declara si toca el almacén.** Si no lo toca —los dos motores, el reloj y el mecanismo de credenciales—, su prueba es **unitaria y sin base**; si lo toca, la prueba de integración pertenece a `GeometriaFactory-Api` y la historia lo declara.

## 2. Criterios DoR para tareas técnicas

Seis criterios, todos respondibles con sí o no.

1. **Declara su fuente upstream por identificador**: un componente de `05` §3.1, una ADR, un NFR de su §8, un riesgo de su §9, un punto abierto de su §11, una regla conceptual de modelo o una puerta del intake §17.1.P.8 · GeometriaFactory-Infrastructure.
2. **Declara al menos una historia consumidora**, o se justifica como infraestructura compartida citando la ADR, la puerta o el punto abierto que la sostiene.
3. **Sus criterios de aceptación son verificables**, y cuando la propiedad que sostienen es una **ausencia** —cero peticiones de red, cero componentes cargados, cero retiros parciales, cero provisorias repetidas, cero mensajes con secreto— el criterio se expresa **con umbral cero y con la condición en la que se mide**.
4. **Si la tarea tiene un atajo destructivo conocido, lo escribe como prohibido.** `05` §9 identifica dos de impacto **muy alto**: **componer la provisoria por un contador, la fecha o el correo** cuando la fuente de material impredecible no responde, y **descartar el almacén y crearlo de nuevo** ante un esquema que no corresponde. Una tarea que los toque y no los declare **no está lista**.
5. **Sus dependencias están declaradas y ninguna es circular**, y ninguna cruza la regla de `05` §3.2: **ningún adaptador depende de otro adaptador** —el único par acoplado son los dos motores, y en una sola dirección—, **los dos motores, el reloj y el mecanismo de credenciales no dependen del contexto de persistencia**, y **la composición de raíz no es de acá**.
6. **Si es de tipo indagación, tiene caja temporal expresada en etapas o en el punto de control que la cierra**, nunca en horas; y si el punto abierto que cierra **no es de este proyecto de código**, lo declara.

## 3. Excepciones admitidas

| Caso | Qué se flexibiliza | Quién lo aprueba |
| --- | --- | --- |
| Historias de `CU-06009`, el sello del reloj | El criterio 2 de §1 se cumple **declarando que no traza a ninguna necesidad y por qué**, según `02` §7.2. Inventarle una traza sería peor que declarar la ausencia | El Product Owner |
| Tarea de indagación que cierra o eleva un punto abierto de `05` §11 | El criterio 3 de §2 puede cumplirse con el **resultado esperado** en lugar de con un criterio verificable de antemano | El Product Owner, en el punto de control de la etapa que la contiene |
| Historia cuya verificación depende de uno de los valores rotulados **[ASUNCIÓN]** de `05` §8 | El criterio 3 de §1 se cumple con el valor **vigente pero declarado como asunción**, hasta que `PA-09` del backlog se cierre con BT-06023. **No habilita a inventar otro número** | El Product Owner, o 08 al fijar su guion de medición |
| Historia del validador de figuras | Ninguno: **no se admite excepción al criterio 3 ni al 6**, y su material de prueba son **los ocho escenarios del intake §20 y ninguno inventado**. Es la mitigación del único riesgo de negocio del producto | — |
| Tarea que toca la producción de la provisoria o la preparación del almacén | Ninguno: **no se admite excepción al criterio 4 de §2**. Los dos atajos están escritos porque los dos **dejan el sistema aparentemente funcionando**: una provisoria adivinable no se nota hasta que alguien la usa, y un almacén recreado deja el servicio impecable y sin los trabajos de nadie | — |

## 4. Aprobador

| Papel | Quién | Qué aprueba |
| --- | --- | --- |
| Product Owner | El docente de la cátedra, que es también quien ejecuta (`PRODUCT-INTAKE` cabecera y §2) | Que un ítem cumple esta DoR antes de entrar, y las excepciones de §3 |
| AG-06, curaduría del backlog | La misma persona, en el papel de la categoría 06 | Que la historia o la tarea esté redactada, trazada y con sus criterios escritos |

**Con `equipo_n = 1` los dos papeles los ejerce la misma persona.** Lo que reemplaza al filtro de una segunda persona son tres cosas, y en este proyecto de código la tercera es la más dura:

1. El **punto de control bloqueante** de cada etapa (`PRODUCT-INTAKE` §15).
2. Las **puertas propias del pipeline**: construcción sin advertencias y **transformaciones aplicadas solas sobre un almacén inexistente**, que es la cuarta etapa y es propia de acá.
3. **La batería obligatoria de diez casos del validador, con los ocho escenarios del intake como entrada.** No depende de que alguien la revise: se corre. Es la mitigación declarada del **único riesgo de negocio del producto**, y la fuente le asigna probabilidad **alta si no se controla**.

## 5. Qué no es esta DoR

**No es la Definition of Done.** La DoD del proyecto de código vive en `08-Calidad-Y-Pruebas` y **todavía no está emitida**; hasta que lo esté, lo que gobierna el cierre son los criterios de transición de [`../../../00-Contexto/Roadmap-Producto.md`](../../../../../00-Contexto/Roadmap-Producto.md) §5. Esta DoR habla de **cuándo empezar**: no menciona los diez casos pasando, ni las tres coberturas alcanzadas, ni los 200 ms medidos.

**No redacta reglas de negocio ni invariantes.** Las **dieciséis** reglas y los **nueve** invariantes viven en `GeometriaFactory-Domain`; lo que esta DoR exige es que la historia los **cite por identificador**. Las **siete** reglas conceptuales de modelo sí son propias de la categoría 02 de este proyecto de código, y **no compiten con las reglas de negocio**: una regla conceptual de modelo declara cómo el dato sobrevive, no qué decidió el negocio.

**Y no decide el límite de tamaño del texto.** `ADR-06006` §2 decidió que el motor **no impone límite propio** y reasignó el valor y su forma de rechazo a la categoría 05 de `GeometriaFactory-Api`, que ya la tomó: **rechaza y nunca trunca**.

## 6. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. Declara **ocho** criterios de entrada para las historias —cuatro propios de este proyecto de código: que la historia no tome ninguna decisión de negocio, que declare si su condición es resultado o fallo, que declare el camino en que un mecanismo se detiene en lugar de cumplir a medias, y que declare si toca el almacén— y **seis** para las tareas técnicas, incluido el que exige escribir como prohibidos los **dos atajos destructivos** que `05` §9 identifica con impacto muy alto. Declara cinco casos de excepción, dos de ellos negativos y sin excepción posible, y uno que recoge la única historia del proyecto de código que no traza a ninguna necesidad. Declara el aprobador con la constancia de que el filtro más duro es la batería obligatoria del validador, y la delimitación contra la Definition of Done, contra las reglas de negocio y contra el límite de tamaño del texto, que ya está reasignado. |
