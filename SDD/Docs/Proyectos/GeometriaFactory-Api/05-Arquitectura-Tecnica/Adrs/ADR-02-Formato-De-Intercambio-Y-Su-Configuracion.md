# ADR-02 — El formato de intercambio y su configuración, fijados para los dos extremos

**Proyecto de código:** GeometriaFactory-Api
**Documento:** ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Comunicación

---

## 1. Contexto

`GeometriaFactory-Contracts` decidió que sus tipos de transferencia no tengan **ninguna** referencia a bibliotecas de serialización, y aceptó por escrito el trade-off que eso deja: «el ensamblado no impone formato de intercambio; la contrapartida es que dos consumidores mal configurados podrían serializar distinto». Su punto abierto `PA-03` reasignó la decisión a las categorías 05 de `GeometriaFactory-Api` y de `GeometriaFactory-Web`.

`GeometriaFactory-Web` recibió esa reasignación y **deliberadamente no la tomó**, con un fundamento que esta ADR comparte: «no se puede decidir de un solo lado; los dos extremos tienen que coincidir o el contrato deja de ser el mismo». Declaró que la decisión pertenece a esta categoría, **que es la del productor**, y que él la adopta (`Web` `PA-03`).

El riesgo que la decisión tiene que cerrar es concreto y no teórico. Los dos extremos compilan contra el mismo ensamblado, de modo que **un cambio incompatible rompe la compilación antes que el tiempo de ejecución**: ésa es toda la red que este producto tiene. Pero una **configuración** distinta en cada extremo no rompe ninguna compilación: rompe en producción, con un campo que llega nulo y un estado que no se reconoce.

Motivación upstream: NB-03, NB-04, NB-09; RN-08, RN-09; `PRODUCT-INTAKE` §17.5.P.3, §17.4.P.3, §17.4.P.11 punto 2.

## 2. Decisión

**El formato de intercambio es el que el intake declara —notación de objetos de texto, sobre los tipos de `GeometriaFactory-Contracts`— y su configuración se declara una sola vez, en un lugar citable por los dos extremos.** **Seis reglas de formato**, y las seis son verificables:

1. **Los campos se nombran exactamente como los declara el tipo de transferencia.** No hay transformación de estilo entre el nombre del campo y el nombre en el texto. Es la única convención que **no puede desincronizarse**, porque no hay nada que configurar distinto en cada lado.
2. **Los valores de conjunto cerrado viajan por su nombre y nunca por su posición.** El producto tiene **cuatro** conjuntos cerrados —papel de la cuenta, estado de cuenta, estado del trabajo y especie de observación— y un valor nuevo insertado en el medio cambiaría el significado de todos los datos ya emitidos si viajaran por posición. Es la regla que más silenciosamente se rompe.
3. **Los campos nulos se emiten.** Omitirlos hace que un campo agregado en el ensamblado sea indistinguible, para el otro extremo, de un campo viejo que llegó vacío, y en este producto la nulidad **significa cosas**: una credencial nula es una cuenta `Pendiente`, un comentario nulo es un trabajo sin desenlace escrito.
4. **Los números decimales viajan sin cultura, con punto decimal.** No es una preferencia: el escenario `E-8` documenta que **el modo de falla más probable del producto** es un separador decimal de la cultura de la máquina metido en un texto, y sería absurdo reproducirlo en la frontera propia.
5. **La lectura es estricta: un campo desconocido en el cuerpo de una petición se rechaza.** Aceptarlo en silencio permitiría que un extremo desactualizado envíe algo que el otro descarta sin decirlo, que es exactamente el fallo silencioso que el producto viene a eliminar. La compilación compartida hace que el costo de la estrictez sea nulo: no hay clientes de terceros a los que romper.
6. **Hay un solo límite de tamaño de cuerpo en todo el producto, tomado de configuración, y el cuerpo que lo excede se rechaza. Nunca se trunca.** `GeometriaFactory-Infrastructure` decidió que el motor de interpretación no imponga límite propio y **exigió por escrito que el borde rechace y no trunque**: truncar rompe `RN-08` en silencio, con el trabajo guardado y el texto mutilado. El número se ancla en la etapa `a`, calibrado sobre el texto más grande que la fuente documenta.

**Y una regla que no es de formato pero vive en la misma frontera: el texto original del alumno no se normaliza en el borde.** No se recodifica, no se recortan espacios, no se normalizan saltos de línea y no se reescriben separadores. Entra como llegó y se guarda como llegó.

**Esta decisión obliga a los dos extremos.** `GeometriaFactory-Web` declaró que la adopta; la verificación de que efectivamente coinciden es la batería de integración, que golpea el servicio real.

**Cómo se cuenta esto, para que no haya dos números del mismo objeto.** [`../Contratos-REST.md`](../Contratos-REST.md) §2.2 publica la tabla del formato con **ocho** filas bajo la columna `Regla`, y **son estas mismas, sin ninguna agregada ni ninguna quitada**: las **seis reglas de formato** numeradas acá, más la **notación** —que es el formato mismo y no una regla sobre él, y que esta ADR declara en su oración de encabezado—, más la **prohibición de normalizar el texto original**, que esta ADR declara explícitamente **no siendo regla de formato**. **6 + 1 + 1 = 8.** El predicado «ninguna depende de que dos configuraciones coincidan» se predica de las **seis** de formato, que son las que tienen configuración detrás.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Una sola configuración declarada, con nombres literales, conjuntos por nombre, nulos emitidos, números sin cultura, lectura estricta y límite único (**adoptada**) | Cierra el trade-off que `Contracts ADR-01` aceptó por escrito; ninguna de las seis reglas de formato depende de que los dos extremos se configuren igual **por separado** | Obliga a que un cambio de configuración se coordine entre dos proyectos de código, y la lectura estricta rompe ante un extremo desactualizado, aunque de forma ruidosa |
| No fijar nada y dejar que cada extremo se configure | Ningún acoplamiento de configuración | **Descartada.** Es exactamente el riesgo que `Contracts ADR-01` declaró aceptado y que las dos categorías 05 destinatarias tenían que cerrar. Además `GeometriaFactory-Web` ya declaró que no lo decide de un solo lado |
| Transformar el estilo de los nombres al serializar | Es la convención habitual en superficies públicas | **Descartada.** Introduce una configuración que **puede diferir entre extremos sin romper la compilación**, y esta superficie no tiene clientes de terceros a quienes esa convención les importe. Ganancia cero, riesgo real |
| Omitir los campos nulos | Cuerpos más chicos, que en el listado de la comisión no es despreciable | **Descartada.** La nulidad significa cosas en este producto, y el listado ya está acotado por la proyección sin componentes ni texto original, que es donde estaba el peso de verdad |
| Lectura tolerante, que ignore campos desconocidos | Permite desplegar los dos extremos en orden distinto | **Descartada.** La regla operativa del producto es el **despliegue conjunto** ante un cambio de contrato, así que el orden no es un problema que haya que resolver; y la tolerancia convertiría un desajuste de versión en un dato perdido en silencio |
| Truncar el cuerpo que excede el límite | El servicio nunca rechaza un envío | **Descartada, y es la peor de todas.** Rompe `RN-08` sin que nada falle: el trabajo queda guardado con el texto mutilado y el alumno lo descubre al ver el dibujo |

## 5. Consecuencias positivas

1. **Cierra el punto abierto que `GeometriaFactory-Contracts` reasignó y que `GeometriaFactory-Web` devolvió a esta categoría**, sin que ninguno de los tres proyectos de código haya decidido de un solo lado.
2. Las seis reglas de formato están elegidas para que **ninguna dependa de que dos configuraciones coincidan**: los nombres son literales, los conjuntos viajan por nombre, la nulidad es explícita y el número no tiene cultura.
3. `RN-08` queda protegida en el único lugar del backend donde el texto puede alterarse por transporte, con una prueba byte a byte.
4. `RN-09` queda protegida al traducir: la posición y el campo cruzan la frontera sin recortarse.
5. Un valor nuevo en cualquiera de los cuatro conjuntos cerrados no cambia el significado de ningún dato ya emitido.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que un cambio de configuración obligue a coordinar dos proyectos de código.** Es la contracara del despliegue conjunto, que la fuente ya declara como regla operativa.
2. **Se acepta que la lectura estricta rompa ante un extremo desactualizado.** Se acepta porque rompe **ruidosamente**, con un código de respuesta, y no en silencio.
3. **Se acepta emitir campos nulos**, con el costo de cuerpo que eso tiene.
4. **Se acepta que el número del límite de cuerpo quede abierto hasta la etapa `a`.** Lo que no queda abierto es la forma de rechazo, que es la parte que puede romper una regla.

## 7. Implementación

- La configuración de intercambio se declara **en la composición de raíz** y en ningún otro lado ([`ADR-06`](ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md)).
- **Convención impuesta:** ningún punto de acceso configura la serialización por su cuenta ni para un tipo en particular.
- **Convención impuesta:** el texto original del alumno viaja como cadena y **no se toca** en ningún tramo de esta capa.
- `GeometriaFactory-Web` adopta esta misma configuración en su cliente, según lo que declaró en su `PA-03`. La coincidencia se verifica ejerciendo el servicio real desde la batería de integración, no comparando dos archivos de configuración.
- El límite de cuerpo se toma de configuración y su valor se ancla en la etapa `a`; la **forma de rechazo** no es configurable.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Configuraciones de intercambio declaradas en el producto | Exactamente **1**, compartida por los dos extremos | Inspección de la composición de raíz y del cliente del front |
| Valores de conjunto cerrado emitidos por posición | Exactamente **0**, en los **4** conjuntos | Prueba de integración que compara el cuerpo contra el nombre del valor |
| Diferencia entre el texto enviado y el guardado | Exactamente **0** caracteres | Prueba que envía el texto de `E-1` y compara byte a byte lo guardado |
| Cuerpos truncados en silencio | Exactamente **0** | Prueba que envía un cuerpo por encima del límite y comprueba que **se rechaza** |
| Campos desconocidos aceptados en silencio | Exactamente **0** | Prueba que envía un campo que el tipo no declara |
| Campos nulos omitidos en una respuesta | Exactamente **0** | Prueba de integración sobre una cuenta `Pendiente` —credencial nula— y un trabajo sin comentario |
| Números decimales emitidos con separador de cultura | Exactamente **0** | Prueba de integración con la cultura del proceso alterada |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.18** §4.1 (RN-08, RN-09), §17.4.P.3, §17.4.P.11 punto 2, §17.5.P.3 y §20.E-8.
- [`../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md`](../../../GeometriaFactory-Contracts/05-Arquitectura-Tecnica/Adrs/ADR-01-Tipos-De-Transferencia-Planos-Sin-Dependencias.md) §6 punto 4, que es el trade-off que esta ADR cierra.
- [`../../../GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md`](../../../GeometriaFactory-Web/05-Arquitectura-Tecnica/Arquitectura-Proyecto-Codigo.md) §11 `PA-03`, que reasigna la decisión a esta categoría.
- [`../../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md`](../../../GeometriaFactory-Infrastructure/05-Arquitectura-Tecnica/Adrs/ADR-06-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) §2 punto 3, que reasigna el límite de tamaño acá con la exigencia de rechazar y no truncar.
- ADR relacionadas: [`ADR-04`](ADR-04-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md), [`ADR-06`](ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md), [`ADR-08`](ADR-08-Sin-Versionado-De-Rutas-Y-Despliegue-Conjunto.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. **Cierra el punto abierto del formato de intercambio que `GeometriaFactory-Contracts` reasignó y que `GeometriaFactory-Web` devolvió a esta categoría por ser la del productor**, con seis reglas de formato elegidas para que ninguna dependa de que dos configuraciones coincidan, más la prohibición de normalizar el texto original en el borde. **Cierra también la forma del límite de tamaño de cuerpo que `GeometriaFactory-Infrastructure` reasignó**: uno solo, de configuración, que rechaza y nunca trunca. Evalúa seis alternativas, declara cuatro trade-offs y fija siete métricas de validación. |
| 1.1 | 2026-08-10 | **Cierra el hallazgo `C-05-03` (P2) del informe de auditoría [`../../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md`](../../../../Audit/C-05-Arquitectura-Siete-Proyectos-r1.md) 1.0.** El mismo objeto se contaba con dos números en la misma ola: esta ADR decía «**seis** reglas» en §2, §4 y §10, mientras [`../Contratos-REST.md`](../Contratos-REST.md) §2.2 publica **ocho** filas bajo la columna `Regla`, su §9 declara «sus **ocho** reglas» y el README de la sección dice «**ocho** reglas elegidas para que ninguna dependa de que dos configuraciones coincidan» —predicado que acá se predica de las seis—. **No había contradicción de contenido: las ocho filas del contrato son exactamente las seis numeradas de esta ADR, más la notación y más el texto original**, y coinciden punto por punto. Lo que faltaba era que el lector pudiera reconciliar los dos números sin abrir los dos archivos y hacer la resta. Se unifica **nombrando los conjuntos**: las seis pasan a llamarse **reglas de formato** en §2, §4 y §5, y §2 agrega el cuadre explícito **6 + 1 + 1 = 8** identificando las dos filas que no son reglas de formato —la **notación**, que es el formato mismo, y la **prohibición de normalizar el texto original**, que esta ADR ya declaraba como no siendo regla de formato—. La referencia del intake pasa a **1.18**. **La decisión no cambia: ninguna regla se agrega, se quita ni se reenuncia**, y las siete métricas de validación de §8 quedan intactas. Sube minor. |
