# Wireframes — Estado degradado y reconexión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Wireframes-Estado-Degradado-Y-Reconexion.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** UX/UI Designer + Frontend Lead (AG-03)
**Variante:** UX/UI
**Trazabilidad upstream:** `../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md` íntegro —§4, FA-01 a FA-05, §6, CA-01 a CA-07 y §13—; `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6 (RT-03, RT-06, RT-07); `../02-Especificacion-Funcional/Glosario-Funcional.md` §2, entradas «estado degradado» y «cartel de reconexión»; `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-08-Alcance-Del-Laboratorio-Desde-El-Aula.md` §1, §5 (tercer y cuarto criterio); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-2, CL-8, CL-9), §11 (RN-B1, RN-B2, RN-B4), §14 (RA-03), §17.6 P.4, P.9, **P.10** y P.12; `Design-Rules-Web-Generico.md` §5, §7, §10; `Design-Rules-Blazor-Mudblazor.md` §2 y §5
**Trazabilidad downstream:** Fase B2 de validación visual de maqueta; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`; `09-Devops`

---

## Tabla de contenido

- [1. Pantalla y propósito](#1-pantalla-y-propósito)
- [2. Layout](#2-layout)
- [3. Componentes principales](#3-componentes-principales)
- [4. Interacciones](#4-interacciones)
- [5. Estados](#5-estados)
- [6. Versión angosta](#6-versión-angosta)
- [7. Notas de implementación](#7-notas-de-implementación)
- [8. Trazabilidad](#8-trazabilidad)
- [9. Control de cambios](#9-control-de-cambios)

---

## 1. Pantalla y propósito

**Nombre canónico de superficie: `Estado-Degradado-Y-Reconexion`.**

**El estado degradado es una superficie, no un error.** Cuando algo se corta, la aplicación sigue en pie y lo dice: nunca una excepción sin manejar, nunca un fallo silencioso y nunca una pantalla rota. Esta superficie es transversal: se superpone a las otras diez en lugar de reemplazarlas, y ellas la referencian en vez de redibujarla.

**Dos tramos independientes, dos avisos distintos, y confundirlos es el error de lectura más probable de todo el diseño.** Una persona puede ver el segundo sin que el primero aparezca nunca.

| | Cartel de reconexión | Aviso de indisponibilidad |
| --- | --- | --- |
| Qué tramo se cortó | Navegador ↔ pieza pública: la conexión viva que sostiene la interacción | Pieza pública → pieza de datos: la obtención de datos |
| Quién lo detecta | El propio circuito | La pieza pública, al recibir el código correspondiente |
| Dónde se dibuja | Banda superpuesta en el borde superior, sobre todo lo demás | Dentro del área de contenido de la superficie donde la persona estaba |
| Qué pasa con el resto | La página queda inerte: nada responde hasta que vuelve | **El armazón sigue entero y navegable.** Lo que falta son los datos |
| Qué se ofrece | Reintentar la conexión | Reintentar la acción que falló |
| ¿Se entera el otro tramo? | La pieza de datos ni se entera | El circuito sigue vivo |

**Y una tercera cosa que no es ninguna de las dos y no se avisa:** el repliegue del transporte del circuito a uno de mayor latencia. Es un trade-off aceptado aguas arriba, degrada la latencia percibida al escribir y **no es motivo de aviso**: alarmar sin darle a nadie nada que hacer es peor que callar.

## 2. Layout

Aviso de indisponibilidad, dentro del área de contenido y con el armazón intacto:

```text
+----------+----------------------------------------------------------------+
| Laborat. |  Mis trabajos                            [ + Trabajo nuevo ]   |
|          |  ------------------------------------------------------------- |
| ·Mis     |  +----------------------------------------------------------+  |
|  trabajos|  |  [ico]  Ahora no podemos traer tus trabajos              |  |
| ·Trabajo |  |                                                          |  |
|  nuevo   |  |  El laboratorio está en pie, pero no está pudiendo       |  |
| ·Mi      |  |  llegar a tus datos en este momento. Probá de nuevo en   |  |
|  contra- |  |  un rato.                                                |  |
|  seña    |  |                                                          |  |
|          |  |  [ Reintentar ]                                          |  |
| -------- |  +----------------------------------------------------------+  |
| Ana Diaz |                                                                |
| [Cerrar] |                                                                |
| v1.4.2   |                                                                |
+----------+----------------------------------------------------------------+
```

Aviso de indisponibilidad sobre una acción con datos escritos, que es el caso que más importa:

```text
   |  [ico]  No pudimos enviar tu trabajo                        |
   |  El laboratorio está en pie, pero no está pudiendo llegar a |
   |  tus datos. Lo que escribiste sigue acá: probá de nuevo.    |
   |  [ Reintentar el envío ]                                    |
   +-------------------------------------------------------------+
   ... el formulario completo, con el texto pegado intacto ...
```

Cartel de reconexión, superpuesto en el borde superior:

```text
+==================================================================+
|  Se cortó la conexión con el laboratorio. Reintentando...   [↻]  |
+==================================================================+
|                                                                  |
|   ... la superficie donde la persona estaba, inerte ...          |
```

Estado vacío, que **no** es ninguno de los dos y se dibuja acá sólo para fijar la diferencia:

```text
   +-------------------------------------------+
   |         [ ilustración neutra ]            |
   |   Todavía no cargaste ningún trabajo      |
   |   [ + Cargar mi primer trabajo ]          |
   +-------------------------------------------+
```

## 3. Componentes principales

| Componente | Patrón del catálogo | Propósito | Datos que muestra | Comportamiento |
| --- | --- | --- | --- | --- |
| Aviso de indisponibilidad | Base §5, estado de error | Declarar que no hay datos y ofrecer salida | Qué no se pudo hacer y qué hacer al respecto | Reemplaza **el contenido**, no el armazón. La navegación sigue disponible |
| Acción de reintentar | Base §4.9 | Repetir la acción que falló | Verbo que nombra la acción concreta: «Reintentar el envío», no «Reintentar» a secas cuando hay una acción identificable | Al tener éxito, el aviso desaparece **sin volver a ingresar** |
| Cartel de reconexión | Blazor §2 | Declarar que se cortó el circuito | Que la conexión se cortó y que se está reintentando | Banda superpuesta en el borde superior. **Se estiliza con los tokens del producto**, no se deja el aspecto por omisión del sistema de componentes |
| Bloque de contenido conservado | Base §5 | **Conservar a la vista lo que la persona escribió** | El formulario completo, con su texto intacto | No se guarda en ningún lado: sigue en la superficie, nada más |
| Estado vacío | Base §5 | Declarar que no hay elementos, con el servicio disponible | Ilustración neutra, texto orientativo y acción siguiente | **Se distingue del aviso por el tipo recibido y no por el conteo** |

**Lo que ningún componente de esta superficie muestra:** la dirección de un servicio interno, el nombre de un archivo de datos, una traza de la implementación, un código de error, un tiempo estimado de recuperación y un canal de soporte. **El producto no tiene mesa de ayuda** y ofrecer una sería inventarla. Lo que sí hay para un reporte es el detalle de diagnóstico del sello de versión, con copiado en un solo gesto.

## 4. Interacciones

| Acción | Disparador | Resultado esperado | Precondición |
| --- | --- | --- | --- |
| Ejecutar una acción que necesita datos | Cualquier acción del producto | Si el servicio no responde, **no se propaga ninguna excepción sin manejar** y se entra en estado degradado sobre la ruta donde la persona estaba | — |
| Reintentar | Acción del aviso | Se repite la misma acción. Si el servicio volvió, procede con normalidad y el aviso desaparece | Hay un aviso a la vista |
| Navegar mientras hay un aviso | Barra lateral | **La navegación sigue funcionando**: el armazón está entero. La superficie de destino intentará traer sus datos y podrá mostrar su propio aviso | — |
| Perder el circuito | Corte de red del navegador, o reciclado del proceso que lo sostiene | Aparece el cartel de reconexión. **La pieza de datos ni se entera del corte** | — |
| Recuperar el circuito | Restablecimiento | El cartel desaparece y **la sesión sigue vigente**, porque la credencial nunca vivió en el navegador. Se retoma donde se estaba | — |
| No poder restablecer la sesión | El circuito volvió pero la sesión no | Se vuelve a `Ingreso` **con el motivo declarado**, y no a una pantalla rota ni a un error arbitrario en una acción cualquiera | — |
| Interactuar con la escena durante un corte del circuito | Giro o acercamiento | **Sigue funcionando**: la escena no hace red y no depende del circuito | Hay una instancia viva |
| Que se replieguen a un transporte de mayor latencia | Negociación del circuito | La aplicación sigue funcionando con la escritura más lenta. **No se muestra ningún aviso** | — |

## 5. Estados

| Estado | Condición que lo produce | Representación esperada |
| --- | --- | --- |
| **Vacío** | La colección llegó con cero elementos, **con el servicio disponible** | Estado vacío con ilustración, texto orientativo y acción siguiente. **Distinto del aviso de indisponibilidad**, y la distinción es por el tipo recibido, no por el conteo |
| **Cargando** | La acción está en curso | El tratamiento de carga de la superficie que la aloja |
| **Con datos** | Todo funciona | Sin aviso |
| **Indisponible** | El servicio de datos no responde, o responde fuera de tiempo | Aviso de indisponibilidad en el área de contenido, con el armazón intacto y reintento disponible |
| **Indisponible con contenido conservado** | La acción que falló llevaba datos escritos | Aviso, **más el formulario completo con lo escrito intacto**, para que el reintento no obligue a volver a escribir ni a volver a pegar |
| **Fallo no clasificado** | Un fallo que el contrato no previó, o un defecto de la propia pieza pública | Mismo aviso, con un mensaje que declara que la acción no pudo completarse. **Es la garantía de que ningún fallo llega sin representación**, que es la definición de fallo silencioso que el producto viene a eliminar |
| **Recuperado** | Se reintentó y el servicio volvió | La acción procede, el aviso desaparece y **no hace falta volver a ingresar** |
| **Reconectando** | Se cortó el circuito | Cartel superpuesto en el borde superior. La superficie queda inerte debajo |
| **Reconectado** | El circuito volvió | El cartel desaparece y se retoma donde se estaba, **con la sesión vigente** |
| **Sesión no restablecible** | El circuito volvió pero la sesión no | Regreso a `Ingreso` **con el motivo declarado**. Nunca un vencimiento silencioso que se manifieste como un error arbitrario |
| **Escena no disponible** | El navegador no provee la capacidad gráfica tridimensional | **No es indisponibilidad del laboratorio.** Se informa como limitación del navegador y el resto del producto sigue disponible. Su tratamiento vive en las superficies que dibujan |
| **Transporte replegado** | El circuito negoció un transporte de mayor latencia | **Ningún aviso.** La aplicación sigue funcionando con la escritura más lenta |

## 6. Versión angosta

Punto de quiebre principal en 768 px [ASUNCIÓN].

- El aviso de indisponibilidad toma el ancho disponible dentro del área de contenido. Su acción de reintentar pasa a ancho completo.
- **El cartel de reconexión conserva el borde superior y no se convierte en un aviso flotante centrado**: en pantalla angosta, un aviso flotante taparía el contenido que la persona quiere mirar mientras espera, y el cartel tiene que poder convivir con él.
- El cartel **no empuja el contenido hacia abajo**: se superpone. Empujarlo produciría un salto de disposición cada vez que la conexión oscila.
- En el estado con contenido conservado, el aviso queda **arriba** del formulario, para que se lea antes de que la persona vuelva a escribir.
- Legible sin desplazamiento horizontal a 320 px.

## 7. Notas de implementación

**Accesibilidad.** Es la superficie donde el anuncio importa más, porque su contenido aparece sin que nadie lo pida:

- El aviso de indisponibilidad se anuncia como **alerta**: es un cambio que interrumpe la tarea y quien no lo ve tiene que enterarse igual.
- El cartel de reconexión se anuncia como **estado**, no como alerta: es una condición del entorno y no una falla de la acción, y anunciarlo con la urgencia de una alerta cada vez que la conexión oscila sería intolerable.
- Al aparecer un aviso, **el foco se lleva a su encabezado**, para que el recorrido por teclado no siga apuntando a un control que ya no existe.
- La acción de reintentar declara **qué acción reintenta**, no sólo el verbo.
- Ni el aviso ni el cartel se comunican sólo por color: los dos llevan texto y forma propia.
- El estado vacío y el aviso se distinguen también para quien no ve el color: uno tiene ilustración y acción de avance; el otro, encabezado de falla y acción de reintento.

**Performance percibida.** El aviso aparece **cuando la acción falla**, no antes: no hay sondeo de disponibilidad, porque implicaría una consulta del navegador y la prohíbe la regla de arquitectura. Tampoco hay cuenta regresiva de recuperación: **no hay alta disponibilidad y no la va a haber**, y prometer un tiempo sería prometer algo que la topología no garantiza.

**Internacionalización.** Español rioplatense, segunda persona. Los mensajes son cortos y no se componen con partes del contrato.

**Restricciones de arquitectura.** **Ningún mensaje incluye la dirección de un servicio interno, un nombre de archivo de datos ni una traza.** Es la superficie donde esa regla es más fácil de violar, y por eso su verificación se lee, no se inspecciona. La pieza pública **no guarda copia de los datos**: cuando el servicio no está, no hay nada que mostrar, y **no se muestran datos viejos como si fueran actuales**. Una dirección de servicio que dejó de responder porque cambió es, desde la persona, indistinguible de un corte: **mismo aviso, mismo mensaje**, y la reconfiguración nunca se expone en la pantalla.

**Concurrencia.** Cada persona tiene su propio circuito: el corte de uno no afecta a los demás. La indisponibilidad del servicio de datos, en cambio, alcanza a todas a la vez y todas ven el mismo aviso. El reciclado del proceso del hosting público puede cortar todos los circuitos a la vez; **no tiene mitigación en el código**, y lo único que esta superficie garantiza es que el regreso sea ordenado.

## 8. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Persona objetivo | Alumno y docente por igual. **El tratamiento es el mismo para los dos y esa uniformidad es deliberada** |
| CU origen | [`CU-10`](../../../02-Especificacion-Funcional/Casos-De-Uso/CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md) íntegro. Es transversal: los otros nueve casos de uso lo referencian |
| Reglas de negocio relevantes | **Ninguna, y con motivo declarado aguas arriba**: las once reglas restringen el dominio y este caso de uso no toca ninguno de esos objetos. Lo que sí lo restringe es la regla de arquitectura RA-03 |
| Restricciones transversales | `RT-03`, `RT-06`, `RT-07` |
| Marco aplicado | [`Experiencia-De-Uso.md`](Experiencia-De-Uso.md) §3.8, §4.1, §8.1, §8.3 |
| Superficies que la referencian | Las otras diez, todas |
| Catálogo de diseño aplicado | `Design-Rules-Web-Generico.md` §5 y §10, `Design-Rules-Blazor-Mudblazor.md` §2 y §5 |
| US a generar en 06 | `US-26`, `US-27` |
| Tests previstos en 08 | Guion de demostración de la etapa `a` para el corte y restablecimiento de red y para veinte minutos de navegación continua sin perder la sesión; verificación acumulativa en cada punto de control posterior de que la página sigue en pie con cero excepciones sin manejar y de que el mensaje no contiene ninguna dirección de servicio interno; reintento tras reponer el servicio con el texto conservado; listado vacío distinguible del estado degradado; anuncio efectivo del aviso y del cartel con lector de pantalla |

## 9. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. El estado degradado tratado como superficie diseñada y no como error improvisado, con la tabla comparativa que separa los dos tramos independientes y sus dos avisos, la tercera condición que deliberadamente no se avisa, la conservación de lo escrito como estado propio, la distinción entre estado vacío y aviso por el tipo recibido, las tres prohibiciones de contenido de mensaje, y doce estados declarados para la Fase B2. |
| 1.0 | 2026-08-09 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Web-r1.md` (ronda 1), **sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **H-06**: la `NB-08` de la cabecera pasa a citarse con sección y criterio —§1, §5 (tercer y cuarto criterio)—. |
