# CU-10010 — Sostener la aplicación en estado degradado y reconexión

**Proyecto de código:** GeometriaFactory-Web
**Documento:** CU-10010-Sostener-La-Aplicacion-En-Estado-Degradado-Y-Reconexion.md
**Versión:** 1.2
**Estado:** Aprobado
**Fecha:** 2026-08-09
**Autor:** Analista Funcional senior (AG-02)
**Trazabilidad upstream:** `../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md` §1, §5 (tercer y cuarto criterio); `../../../../00-Contexto/Vision-Producto.md` §9.1 (fallo silencioso); `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §7 (CL-2, CL-8, CL-9), §11 (RN-B1, RN-B2, RN-B4), §14 (RA-03), §17.6 P.4, P.9, **P.10** (PT-01.b y PT-01.c, observabilidad), P.12
**Trazabilidad downstream:** `03-UX-UI-DX` de este proyecto de código; `05-Arquitectura-Tecnica`; `06-Backlog-Tecnico`; `08-Calidad-Y-Pruebas`; `09-Devops`, donde vive la verificación de campo

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [13. Interacción multiusuario y concurrencia](#13-interacción-multiusuario-y-concurrencia)

---

## 1. Propósito

Garantizar que la aplicación siga en pie y lo diga cuando algo se corta, en los **dos tramos independientes** que la sostienen: el circuito entre el navegador y la pieza pública, que se avisa y se reconecta, y la llamada de la pieza pública a la pieza de datos, que falla con estado degradado explícito. Nunca una excepción sin manejar, nunca un fallo silencioso y nunca la dirección de un servicio interno a la vista.

Es el caso de uso transversal de esta especificación: los otros nueve lo referencian en lugar de declarar cada uno su propio tratamiento de la indisponibilidad.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Persona que usa el laboratorio | Primario | Recibe el aviso, entiende qué pasó y decide si reintenta o vuelve más tarde |
| Pieza pública | Sistema | Detecta el corte de cada tramo, presenta el aviso que corresponde y conserva lo que la persona escribió |
| Pieza de datos | Secundario | Está caída, inalcanzable o responde fuera de tiempo. Su indisponibilidad es el disparador del segundo tramo |

El actor primario es uno solo y no distingue papeles: el tratamiento es el mismo para el alumno y para el administrador, y esa uniformidad es deliberada.

## 3. Precondiciones

- La persona está usando alguna ruta del producto, con o sin sesión.
- La pieza pública **no guarda estado propio**: no hay copia local de los datos con la que responder cuando la pieza de datos no está.
- Los dos tramos son independientes: un corte del circuito no llega a la pieza de datos, y un fallo hacia la pieza de datos no rompe el circuito.

## 4. Flujo principal

1. La persona ejecuta una acción que necesita datos: abrir un listado, abrir un trabajo, enviar, resolver.
2. La pieza pública invoca desde su servidor el contrato correspondiente contra la pieza de datos.
3. La pieza de datos no responde, o responde fuera de tiempo, y el contrato devuelve `CONTRATO_SERVICIO_NO_DISPONIBLE`.
4. La pieza pública **no propaga ninguna excepción sin manejar** y entra en estado degradado sobre la ruta en la que la persona estaba.
5. La pieza pública informa explícitamente que el laboratorio no tiene los datos en este momento, con un mensaje que **no incluye ninguna dirección de servicio interno**, ningún nombre de archivo de datos y ninguna traza de la implementación.
6. La pieza pública conserva a la vista lo que la persona había escrito y le ofrece reintentar la misma acción.
7. La persona reintenta. Si la pieza de datos volvió, la acción procede con normalidad y el estado degradado desaparece.

## 5. Flujos alternativos

| Id | Disparador | Curso | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | Se corta el circuito entre el navegador y la pieza pública | La pieza pública muestra su **cartel de reconexión**, que es propio del circuito y distinto del estado degradado. La pieza de datos ni se entera del corte. Al restablecerse, la sesión sigue vigente porque la credencial de sesión nunca vivió en el navegador | El flujo vuelve al punto donde la persona estaba |
| FA-02 | El circuito se repliega a un transporte de mayor latencia | La aplicación sigue funcionando, con la latencia percibida degradada al escribir. **No es motivo de rediseño ni de aviso a la persona**: no hay estado degradado por este motivo | El flujo continúa normalmente |
| FA-03 | La dirección configurada de la pieza de datos ya no responde porque cambió | Desde la persona es indistinguible del corte: mismo estado degradado, mismo mensaje. La reconfiguración es materia de 09-Devops y **nunca se expone en la pantalla** | El flujo vuelve al paso 6 |
| FA-04 | El proceso que sostiene el circuito se recicla en el hosting público | El circuito se pierde y se aplica FA-01. Si la sesión no se puede restablecer, la pieza pública devuelve a la ruta de ingreso con el motivo, y no a una pantalla rota | El flujo vuelve a CU-10002, paso 1 |
| FA-05 | La acción que falló era un envío con texto ya pegado | La pieza pública conserva el texto y los datos escritos en la vista, sin guardarlos en ningún lado, para que el reintento no obligue a volver a pegar | El flujo vuelve al paso 6 |

## 6. Excepciones y errores

| Código | Causa | Respuesta del sistema |
| --- | --- | --- |
| `CONTRATO_SERVICIO_NO_DISPONIBLE` | La pieza de datos no responde o responde fuera de tiempo | Estado degradado explícito sobre la ruta vigente, con reintento disponible. Es el curso del flujo principal |
| `CONTRATO_ERROR_NO_CLASIFICADO` | Un fallo que el contrato no previó | Mismo tratamiento que el anterior, con un mensaje que declara que la acción no pudo completarse. **Es la garantía de que ningún fallo llega sin representación**, que es la definición de fallo silencioso que el producto viene a eliminar |
| Fallo no representado en el contrato | Un defecto de la propia pieza pública | Se presenta como estado degradado con mensaje neutro y se registra del lado del servidor. En ningún caso llega a la persona una traza, una dirección interna ni una pantalla rota |
| `CAPACIDAD_GRAFICA_AUSENTE` | El navegador no provee la capacidad gráfica tridimensional | No es indisponibilidad del laboratorio: se informa como limitación del navegador y el resto del producto sigue disponible. Su tratamiento está en CU-10005 FA-04 y CU-10007 FA-05 |

## 7. Postcondiciones

- En caso de indisponibilidad: la aplicación sigue en pie, la persona sabe qué pasó y qué puede hacer, y ninguna acción quedó a medio aplicar.
- En caso de corte del circuito: la persona ve el cartel de reconexión y, al restablecerse, retoma donde estaba con su sesión vigente.
- En caso de recuperación: el estado degradado desaparece sin que la persona tenga que volver a ingresar.
- En ningún caso: se presenta una excepción sin manejar, se muestra una dirección de servicio interno, o se muestran datos viejos como si fueran actuales.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El servicio de datos detenido y una persona con sesión iniciada | Abre su listado de trabajos | La página sigue en pie, muestra el estado degradado explícito y **cero** excepciones sin manejar llegan a la pantalla |
| CA-02 | El mismo escenario | Se lee el mensaje mostrado | **Cero** apariciones de una dirección de servicio interno, de un nombre de archivo de datos o de una traza de implementación |
| CA-03 | Un envío con texto ya pegado y el servicio de datos detenido | La persona confirma el envío y luego reintenta con el servicio ya repuesto | El texto seguía a la vista, el reintento procede y el trabajo queda registrado |
| CA-04 | Una sesión iniciada y navegación en curso | Se corta y se restablece la red del navegador | Aparece el cartel de reconexión, y al volver la sesión sigue vigente sin pedir credenciales otra vez |
| CA-05 | Una sesión iniciada | Se navega de forma continua durante 20 minutos | El circuito no se recicla y la persona no pierde su sesión |
| CA-06 | El servicio de datos detenido y luego repuesto | La persona reintenta la acción que había fallado | La acción procede y el estado degradado desaparece sin volver a ingresar |
| CA-07 | Un listado sin elementos y el servicio de datos disponible | La persona lo abre | Ve un listado vacío explicado, **distinto** del estado degradado: se distinguen por el tipo recibido y no por el conteo |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [`NB-00008`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00008-Alcance-Del-Laboratorio-Desde-El-Aula.md) |
| Reglas de negocio aplicables | **Ninguna, y con motivo.** Las **dieciséis** reglas del producto restringen el dominio —cuentas, trabajos, estados y observaciones— y este caso de uso no toca ninguno de esos objetos: gobierna la presentación de la indisponibilidad. Lo que sí lo restringe es la regla de arquitectura de nivel producto RA-03, verificada en CA-02, y su enunciado vive en `PRODUCT-INTAKE` §14. Inventar una `RN-XX` acá sería redactar una regla que no existe aguas arriba |
| Contratos de uso consumidos | [`GeometriaFactory-Contracts` CU-08006](../../../GeometriaFactory-Contracts/02-Especificacion-Funcional/Casos-De-Uso/CU-08006-Contrato-De-Respuesta-De-Error.md) completo, en particular sus códigos `CONTRATO_SERVICIO_NO_DISPONIBLE` y `CONTRATO_ERROR_NO_CLASIFICADO` |
| Fachada del visualizador | Ninguna función. El bundle no hace red y por eso no participa de ningún tramo |
| Historias de usuario a generar en 06 | US-10026, US-10027 |
| Componentes esperados en 05 | Tratamiento transversal de la respuesta de error del contrato, componente de estado degradado y cartel de reconexión del circuito |
| Tests previstos en 08 | Guion de demostración de la etapa `a` para CA-04 y CA-05, que son las mediciones de viabilidad del hosting público; y verificación acumulativa de CA-01 y CA-02 en cada punto de control posterior, por la regla de no regresión |

## 10. Notas y supuestos

- **Los dos tramos son independientes y se avisan distinto.** Confundirlos es el error de lectura más probable de este caso de uso: el cartel de reconexión habla del circuito con el navegador; el estado degradado habla de la pieza de datos. Una persona puede ver el segundo sin que el primero aparezca nunca.
- El producto **no tiene alta disponibilidad ni la va a tener**: es un laboratorio de aula sobre un servidor domiciliario. Lo que el negocio exige no es que no se caiga, sino que la caída se explique.
- La verificación de que la red de la facultad alcanza el producto, y el despliegue de las dos piezas desplegables, pertenecen a 09-Devops. Acá sólo vive lo que la persona ve cuando algo no está.
- El repliegue a un transporte de mayor latencia es un trade-off aceptado por escrito aguas arriba y **no** se presenta como degradación a la persona.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-09 | Emisión inicial. |
| 1.1 | 2026-08-09 | Absorbe el `PRODUCT-INTAKE` **1.10**: la fila «Reglas de negocio aplicables» de §9 fundamenta su **ninguna** sobre el recuento de reglas del producto, que pasó de once a **quince**. El fundamento no cambia —las reglas restringen el dominio y este caso de uso no toca ninguno de esos objetos, mientras que lo que sí lo restringe es la regla de arquitectura **RA-03**—; lo que se corrige es el número. **Ningún flujo, estado, criterio de aceptación ni verificación de este caso de uso cambia.** Sube minor. |
| 1.2 | 2026-08-10 | Alineación de recuento con `PRODUCT-INTAKE` **1.13**, que incorpora la regla **RN-10016** —habilitar una cuenta produce su contraseña provisoria— y lleva las reglas de negocio del producto de quince a **dieciséis**. §9 actualiza el recuento de las reglas del producto que este caso de uso declara **no** aplicables; el motivo de la ausencia es el mismo y **RN-10016 tampoco lo alcanza**: gobierna el circuito de credenciales y este caso de uso gobierna la presentación de la indisponibilidad. **Ninguna decisión de este documento cambia.** Sube minor. |

## 13. Interacción multiusuario y concurrencia

Sección opcional admitida por `Rules-Especificacion-Funcional.md` §4.3 para el tipo `web-monolith`.

Cada persona tiene su propio circuito y su propio estado de circuito: el corte de uno no afecta a los demás. La indisponibilidad de la pieza de datos, en cambio, alcanza a todas las personas a la vez, y todas ven el mismo estado degradado. El reciclado del proceso del hosting público es el caso que puede cortar todos los circuitos simultáneamente; su medición está declarada como puerta técnica y **no tiene mitigación en el código**, de modo que este caso de uso sólo garantiza que el regreso sea ordenado y no una pantalla rota.
