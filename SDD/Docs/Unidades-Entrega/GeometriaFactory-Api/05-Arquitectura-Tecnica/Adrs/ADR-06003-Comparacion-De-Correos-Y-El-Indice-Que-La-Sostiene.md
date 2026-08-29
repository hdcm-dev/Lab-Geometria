# ADR-06003 — Criterio de comparación de dos correos, y el índice que lo sostiene

**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-10
**Autor:** Arquitecto de Software Senior + API Designer (AG-05)
**Categoría:** Persistencia

---

## 1. Contexto

`RN-06002` declara que el correo del alumno es **único** y fija cómo se verifica: «registrar dos veces el mismo correo se rechaza con mensaje explícito». `INV-01` lo sostiene desde el dominio. Ninguna de las dos dice **qué significa «el mismo correo»**, y ésa es la pregunta que tres categorías se pasaron sin resolver:

- `GeometriaFactory-Domain` la dejó abierta, porque el dominio no lee el conjunto de entidades.
- `GeometriaFactory-Application` la reasignó explícitamente: «es el adaptador del puerto de repositorio de cuentas quien la materializa, y la decisión le corresponde a la categoría 05 de `GeometriaFactory-Infrastructure`, **junto con el índice que la sostenga**» ([`Application README`](../README.md) §7 y su `PA-03`).
- La categoría 02 de este proyecto de código declaró que **acá se vuelve visible**, porque la restricción de unicidad del almacén la materializa, y la volvió a derivar a esta categoría.

El caso concreto que obliga a decidir no es teórico. El alumno se registra escribiendo su correo; el administrador lo habilita y le comunica la provisoria **de viva voz**; el alumno vuelve a escribir el correo para entrar. Si «Alumno@Ejemplo.Com» y «alumno@ejemplo.com» son dos cuentas, el laboratorio tiene dos identidades para una persona, una de ellas con trabajos y la otra sin ninguno, y nadie se entera hasta que el alumno no encuentra su trabajo. Y si son dos cuentas para el almacén pero una sola para quien las mira, `RN-06002` deja de tener criterio de verificación ejecutable.

Motivación upstream: NB-00001, NB-00002; RN-06001, RN-06002; INV-01, INV-05; `PRODUCT-INTAKE` §4.1, §17.1.P.4 · GeometriaFactory-Infrastructure.

## 2. Decisión

**Dos correos son el mismo correo cuando coinciden ignorando mayúsculas y minúsculas, y ninguna otra normalización se aplica.** La decisión tiene cuatro mitades y las cuatro son exigibles:

1. **Se compara sin distinguir mayúsculas de minúsculas**, sobre la cadena completa.
2. **No se aplica ninguna otra normalización.** No se quitan puntos, no se recorta lo que sigue a un signo de suma, no se resuelven alias de dominio y no se recortan espacios interiores. Los espacios al principio y al final sí se descartan, porque son un artefacto de escritura y no parte del correo.
3. **El correo se guarda tal como la persona lo escribió**, y lo que se compara y se indexa es su **forma normalizada**. La forma escrita es lo que se muestra; la normalizada es lo que decide la identidad.
4. **La unicidad la sostiene un índice único sobre la forma normalizada**, y es la **segunda línea**: la consulta previa del consumidor no es una garantía por sí sola, y la colisión que la consulta no vio termina en `EMAIL_ALREADY_REGISTERED`, camino que `GeometriaFactory-Application` `CU-06001` **FA-02** ya declara.

**La misma normalización se usa para recuperar una cuenta por su correo.** Un criterio de comparación distinto entre el alta y el ingreso dejaría cuentas inalcanzables para su dueño.

**Esta decisión es derivación de esta categoría, no transcripción**, y va rotulada como tal: ninguna fuente del producto la enuncia. Está elevada al Product Owner en `PA-01` de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §11 sólo en lo que hace al identificador del puerto; el criterio en sí lo cierra esta ADR.

## 3. Estado

**Propuesto** desde 2026-08-10.

## 4. Alternativas consideradas

| Alternativa | Pros | Contras |
| --- | --- | --- |
| Comparar ignorando mayúsculas y minúsculas, sin más normalización (**adoptada**) | Cubre el caso real —la misma persona escribe su correo con distinta capitalización en el alta y en el ingreso— con una regla que se explica en una línea y se verifica con dos filas de prueba | No cubre las variantes de alias que algunos proveedores de correo admiten; dos alias del mismo buzón serían dos cuentas |
| Comparar tal cual, sin ninguna normalización | Es la regla más simple posible y no supone nada sobre el correo | **Descartada.** Deja entrar dos cuentas que cualquier persona lee como la misma, y el criterio de verificación de `RN-06002` —«registrar dos veces el mismo correo se rechaza»— pasaría a depender de cómo el alumno apretó la tecla de mayúsculas |
| Normalizar además quitando puntos y lo que sigue al signo de suma | Cierra las variantes de alias de los proveedores más usados | **Descartada.** Sería aplicar el comportamiento de un proveedor concreto a todos, y en varios dominios el punto **sí** distingue dos buzones. Además ninguna fuente del producto nombra proveedor alguno, y elegir uno metería una suposición de negocio en un adaptador |
| Sostener la unicidad sólo con la consulta previa del consumidor, sin índice | Un objeto menos en el esquema | **Descartada.** La categoría 02 declara explícitamente que la consulta previa **no es una garantía por sí sola**, y la capa de aplicación ya declara el camino de la colisión que la consulta no vio. Sin índice, ese camino no puede ocurrir y la regla queda apoyada en una carrera |
| Guardar el correo ya normalizado y no conservar la forma escrita | Un solo valor por fila, sin posibilidad de que los dos se desincronicen | **Descartada.** El administrador ve el listado de la comisión y lo usa para reconocer a sus alumnos; devolverle el correo con la capitalización cambiada es alterar un dato que la persona escribió, en un producto cuya regla más citada es no reescribir lo que el alumno entregó |

## 5. Consecuencias positivas

1. `RN-06002` obtiene un criterio de verificación ejecutable y estable, que no depende de cómo se escribió el correo.
2. `INV-01` queda sostenido en dos líneas independientes —la consulta previa y el índice—, y la segunda es la que el propio catálogo de condiciones ya declaraba necesitar.
3. El punto abierto que `GeometriaFactory-Domain` abrió y `GeometriaFactory-Application` reasignó **queda cerrado**, con el índice que aquella categoría pidió que viniera junto.
4. El listado de la comisión sigue mostrando el correo tal como la persona lo escribió, que es lo que el administrador reconoce.
5. La misma normalización rige el alta y la recuperación, de modo que no puede existir una cuenta que su dueño no pueda alcanzar.

## 6. Consecuencias negativas y trade-offs

1. **Se acepta que dos alias del mismo buzón sean dos cuentas.** Es el costo de no meter el comportamiento de un proveedor concreto en un adaptador, y en un laboratorio de aula el administrador ve el listado completo y puede detectarlo.
2. **Se acepta guardar dos formas del mismo dato** —la escrita y la normalizada—, con la obligación de que la segunda se derive siempre de la primera y nunca se edite por separado.
3. **Se acepta que esta decisión sea derivada.** Ninguna fuente la enuncia y el Product Owner puede reemplazarla; si lo hace, cambia el índice y su prueba, no la forma del modelo.
4. **Se acepta no fijar acá el identificador del puerto.** La frontera la declara `GeometriaFactory-Application`, y su [`ADR-04002`](ADR-04002-Cuatro-Puertos-Y-La-Frontera-Que-Declaran.md) —ya emitida— ató el nombre al punto de control de la etapa `a`. **Lo que esta categoría sí fija es el criterio de nombrado del adaptador**: se nombra por el puerto que implementa y por el mecanismo que usa, en ese orden, para que el adaptador y su puerto se lean como par sin depender de que el nombre del puerto ya esté decidido. La propuesta que llega al punto de control es que el identificador del puerto siga el patrón de los **tres** que el intake sí declara —los tres empiezan por la misma letra de contrato y nombran la cosa, no el mecanismo—; **la decisión sigue siendo del punto de control y no de esta ADR**.

## 7. Implementación

- El adaptador de repositorio de cuentas de [`../Arquitectura-Unidad-Entrega.md`](../Arquitectura-Unidad-Entrega.md) §3.1 es el único lugar donde la normalización se aplica.
- **Convención impuesta:** ningún otro componente normaliza correos. Si dos lugares normalizan, tarde o temprano lo hacen distinto.
- El esquema declara **dos** columnas para el correo y **un** índice único sobre la normalizada; el detalle está en [`../Modelo-Datos-Logico.md`](../Modelo-Datos-Logico.md) §2.1 y §3.
- La colisión que la consulta previa no vio termina en `EMAIL_ALREADY_REGISTERED`, con la precisión ya declarada por la categoría 03: **no se informa el estado ni el papel** de la cuenta que ocupa el correo.
- La restricción de unicidad del papel `Administrador` es otro índice del mismo componente y no comparte criterio con éste: se compara por valor de conjunto cerrado, sin normalización.

## 8. Métricas de validación

| Métrica | Objetivo | Cómo se mide |
| --- | --- | --- |
| Altas aceptadas con correos que sólo difieren en capitalización | Exactamente **0** | Prueba que registra el mismo correo dos veces con distinta capitalización |
| Ingresos fallidos por capitalización distinta a la del alta | Exactamente **0** | Prueba que registra con una forma y recupera con la otra |
| Índices únicos sobre el correo | Exactamente **1**, sobre la forma normalizada | Inspección del esquema y de la transformación inicial |
| Componentes que normalizan correos | Exactamente **1** | Inspección en revisión |
| Correos devueltos con capitalización distinta de la escrita | Exactamente **0** | Prueba que registra con capitalización mixta y compara la recuperación |
| Cuentas con papel `Administrador` que el almacén admite | **A lo sumo 1** | Prueba que intenta materializar la segunda |

## 9. Referencias

- `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.17** §4.1 (RN-06001, RN-06002) y §17.1.P.4 · GeometriaFactory-Infrastructure.
- [`../../02-Especificacion-Funcional/Especificacion-Funcional.md`](../../02-Especificacion-Funcional/Especificacion-Funcional.md) §4 precisión 2 y §11.
- [`../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md`](../../02-Especificacion-Funcional/Modelo-Datos/Modelo-Conceptual.md) §3.2 y §7.
- [`../../../GeometriaFactory-Application/05-Arquitectura-Tecnica/README.md`](../README.md) §7, que es donde la decisión quedó reasignada a esta categoría.
- ADR relacionadas: [`ADR-06001`](ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md), [`ADR-06002`](ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md).

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3c` del renombre `F-03`**, reactivado por el Product Owner el 2026-08-29 y registrado en [`../../../../Producto/Norma-De-Nomenclatura.md`](../../../../Producto/Norma-De-Nomenclatura.md) §8. **2 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni los informes de `Audit/`. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-10 | Emisión inicial. **Cierra el punto abierto que `GeometriaFactory-Domain` abrió y `GeometriaFactory-Application` reasignó a esta categoría**: dos correos son el mismo ignorando mayúsculas y minúsculas y nada más, la forma escrita se conserva, la normalizada decide la identidad, y un índice único sobre ella la sostiene como segunda línea. Evalúa cinco alternativas, declara cuatro trade-offs —incluido el de no fijar acá el identificador del puerto, con el criterio de nombrado del adaptador que sí le corresponde— y fija seis métricas de validación. |
