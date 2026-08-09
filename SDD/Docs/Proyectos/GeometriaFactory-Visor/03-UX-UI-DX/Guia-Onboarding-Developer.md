# Guía de onboarding — la primera hora sobre la fachada del visor

**Proyecto de código:** GeometriaFactory-Visor
**Documento:** Guia-Onboarding-Developer.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** DX Lead (AG-03)
**Variante:** DX

**Trazabilidad upstream:** `DX-Developer-Experience.md` §1 (roles de intervención), §2 (tramos) y §3 (quick-start); `../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md` §3.1, §3.2, §4 —con §4.6, la sexta función—, §5.5 y §6; `../02-Especificacion-Funcional/Casos-De-Uso/CU-01` a `CU-07`, en su orden de lectura —`CU-01` a `CU-05`, después `CU-07` y por último `CU-06`, el recorrido de integración completo sin backend (`Especificacion-Funcional.md` §3.2)—; `../../../00-Contexto/Compatibilidad-Plataformas.md` §2.2 y §2.3; `../../../00-Contexto/Alcance-Producto.md` §4.1 (capacidad F-11); `../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-06-Visualizacion-Dentro-Del-Producto.md` §5; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §16 (estructura de repositorio y guiones), §16.1, §17.7 P.6, P.7, P.8 y P.10, §18 (sample S-1), §20 E-1 y E-7
**Trazabilidad downstream:** 06-Backlog-Tecnico, 08-Calidad-Y-Pruebas, 10-Examples (sample S-1), 11-Documentacion

---

## Tabla de contenido

- [1. Rol de intervención y prerrequisitos](#1-rol-de-intervención-y-prerrequisitos)
  - [1.1 Para quién es esta guía](#11-para-quién-es-esta-guía)
  - [1.2 Prerrequisitos](#12-prerrequisitos)
  - [1.3 Las tres reglas que valen más que el resto de la guía](#13-las-tres-reglas-que-valen-más-que-el-resto-de-la-guía)
- [2. Acceso al archivo de guion](#2-acceso-al-archivo-de-guion)
- [3. Primer ejemplo ejecutable](#3-primer-ejemplo-ejecutable)
  - [3.1 Los primeros 5 minutos: dibujar](#31-los-primeros-5-minutos-dibujar)
  - [3.2 Hasta los 30 minutos: ejercer el contrato entero](#32-hasta-los-30-minutos-ejercer-el-contrato-entero)
  - [3.3 Hasta la hora: tocar el interior sin tocar el contrato](#33-hasta-la-hora-tocar-el-interior-sin-tocar-el-contrato)
- [4. Diagnóstico de problemas frecuentes en la primera hora](#4-diagnóstico-de-problemas-frecuentes-en-la-primera-hora)
- [5. Próximos pasos](#5-próximos-pasos)
- [6. Trazabilidad](#6-trazabilidad)
- [7. Control de cambios](#7-control-de-cambios)

---

## 1. Rol de intervención y prerrequisitos

### 1.1 Para quién es esta guía

Para quien tiene que **integrar** el archivo de guion del visor en una superficie anfitriona, o **modificarlo** por dentro. En este producto los dos roles los cumple la misma persona, asistida por un agente de IA que construye por etapas; los dos lectores reales de esta guía son ese developer volviendo sobre su propio trabajo semanas después, y ese agente entrando por una sección suelta. No hay integradores externos: el artefacto no se publica y sus dos únicos consumidores son internos al producto (`DX-Developer-Experience.md` §1.2).

La guía se recorre **de arriba hacia abajo y una sola vez**. Es el modo tutorial de Diátaxis: enseña ejerciendo. Para consultar una función puntual está el reference; para resolver una tarea concreta, el how-to. Los enlaces están en §5.

### 1.2 Prerrequisitos

| Prerrequisito | Cómo se verifica | Si falta |
| --- | --- | --- |
| El repositorio abierto en el **entorno de desarrollo contenido** del producto | El entorno arranca y los guiones de `scripts/` son ejecutables desde adentro | No hay alternativa: `Compatibilidad-Plataformas.md` §2.3 declara que el host de desarrollo no tiene ni va a tener las herramientas de construcción. Ningún paso de esta guía se ejecuta en el host |
| Un navegador con **capacidad gráfica tridimensional** | Se comprueba en el paso 3 de §3.1: si falta, `inicializar` informa `CAPACIDAD_GRAFICA_AUSENTE` | La combinación está declarada no soportada (`Compatibilidad-Plataformas.md` §4). Se cambia de navegador; no hay repliegue |
| Haber leído `Definicion-Contrato-De-Fachada.md` §4 | Poder nombrar las **seis** funciones y qué devuelve cada una | La guía se puede recorrer igual, pero el tramo de 30 minutos no va a cerrar: sus verificaciones son sobre el contrato |
| **Ningún** servicio del backend en marcha | No hace falta apagar nada: simplemente no se levanta | Tampoco es un problema si estuviera levantado, pero entonces el recorrido de integración deja de demostrar lo que vino a demostrar |

**Lo que no es prerrequisito, y conviene decirlo:** conocer el motor de dibujo tridimensional. Si en algún punto de esta hora hiciera falta saber cómo se llama una clase del motor, el punto de extensión del producto estaría roto y eso sería un defecto a reportar, no un tema a estudiar.

### 1.3 Las tres reglas que valen más que el resto de la guía

1. **Todo pasa por las seis funciones.** `inicializar`, `cargarJson`, `seleccionarPieza`, `redimensionar`, `destruir` y `establecerMovimiento`. Nada más es superficie pública. Un anfitrión que llame a algo interno, que manipule el elemento de dibujo después de habérselo entregado a `inicializar`, o que toque la escena para prender un movimiento en lugar de invocar la sexta función, ata el producto al motor de dibujo de hoy.
2. **La fachada no obtiene nada por su cuenta.** No hace red, no lee configuración y no sabe quién es la persona. El texto del trabajo se lo entrega el anfitrión, siempre. El umbral de peticiones de red es exactamente 0.
3. **La fachada informa y sigue viva.** Devuelve un código de condición y no reintenta, no pide datos y no emite observaciones sobre el trabajo. Cada condición es trabajo que le queda al anfitrión, y está catalogada en `DX-Error-Messages.md`.

## 2. Acceso al archivo de guion

No hay instalación ni publicación: el artefacto de este proyecto de código es un **archivo generado** que se sirve como recurso estático, y **nunca se edita a mano** (PRODUCT-INTAKE §17.7 P.7). Se obtiene construyéndolo.

| Camino | Guion | Cuándo se usa |
| --- | --- | --- |
| **Ciclo corto** | `scripts/build-visor.sh` | Es el camino de esta guía y el de todo trabajo sobre el visor: genera sólo el archivo de guion, sin compilar el resto del producto |
| Ciclo completo | `scripts/build.sh` | Encadena la construcción del archivo de guion con la del resto del producto. No hace falta para nada de esta hora |

Los dos se ejecutan **dentro del entorno de desarrollo contenido**. El archivo generado queda listo para dos destinos: la página integradora del sample S-1, que es con la que se trabaja acá, y la superficie del componente anfitrión del producto.

Si `scripts/build-visor.sh` termina con error, la guía se detiene: no hay archivo de guion que cargar. La generación sin errores es un gate declarado del proyecto de código (PRODUCT-INTAKE §17.7 P.8).

## 3. Primer ejemplo ejecutable

### 3.1 Los primeros 5 minutos: dibujar

| Paso | Qué hacer | Qué tiene que pasar |
| --- | --- | --- |
| 1 | Ejecutar `scripts/build-visor.sh` en el entorno contenido | El archivo de guion queda generado, sin errores |
| 2 | Abrir la página integradora del sample S-1 en el navegador | Carga, muestra un área de texto y un elemento de dibujo vacío, y no dibuja nada todavía |
| 3 | Pegar el texto del escenario **E-7** (`PRODUCT-INTAKE` §20.E-7) y pedir que se cargue | Se dibujan **6 piezas** con los índices 0 a 5, una por cada tipo dibujable, y aparece la estructura del texto como árbol colapsable |
| 4 | Abrir la pestaña de red del navegador y rotar y acercar la escena con el mouse | **0 peticiones** originadas por la fachada, durante la carga y durante los gestos |

**Objetivo del tramo, verificable:** 6 piezas dibujadas y 0 peticiones. Si las dos cosas se cumplen, el tramo cerró.

Vale la pena detenerse un segundo en lo que acaba de pasar: se dibujó un trabajo completo **sin ningún servicio del backend disponible**. Esa no es una comodidad del entorno de desarrollo; es la propiedad que el producto exige conservar y la que demuestra que el motor de dibujo es reemplazable (PRODUCT-INTAKE §18).

### 3.2 Hasta los 30 minutos: ejercer el contrato entero

La secuencia de invocaciones es la misma que ejerce la página integradora. Se escribe acá **en forma neutral, con los nombres del contrato y sin ningún lenguaje concreto**, porque los nombres de las **seis** funciones son los del contrato y no cambian —las cinco primeras las declara el intake y la sexta la acuña `Definicion-Contrato-De-Fachada.md` §4.6—, mientras que la forma de escribirlas es decisión de 05-Arquitectura-Tecnica:

```text
id = inicializar(elementoDeDibujo, opciones)      → identificador de instancia
resultado = cargarJson(id, textoDelTrabajo)       → piezas dibujadas, piezas no dibujadas, estructura del texto
            seleccionarPieza(id, 3)               → confirma el índice resaltado
            redimensionar(id)                     → confirma el ajuste al tamaño vigente
estado    = establecerMovimiento(id, opciones)    → estado efectivo de los dos movimientos automáticos
            destruir(id)                          → confirma la liberación; id deja de ser válido
```

Sobre esa secuencia, seis comprobaciones, en este orden:

| # | Qué hacer | Qué tiene que pasar | Referencia |
| --- | --- | --- | --- |
| 1 | Elegir el elemento de índice 3 en el árbol | Queda resaltada esa pieza en la escena, y **ninguna otra**: el resaltado es exclusivo | `CU-03` CA-02 |
| 2 | Elegir el mismo índice otra vez | Sigue resaltada la misma pieza, sin doble resaltado ni parpadeo: la operación es idempotente | `CU-03` CA-04 |
| 3 | Pedir el índice 6, que no existe en un trabajo de seis piezas | Se informa `INDICE_FUERA_DE_RANGO`, **la selección vigente se conserva** y no se resalta nada por aproximación | `CU-03` CA-03 |
| 4 | Cambiar el tamaño del elemento de dibujo e invocar `redimensionar` | La escena se ajusta, las piezas conservan su proporción y la pieza resaltada sigue siendo la misma | `CU-04` CA-01 y CA-02 |
| 5 | Con la pieza de índice 3 todavía resaltada, invocar `establecerMovimiento(id, opciones)` prendiendo los dos movimientos | Los dos movimientos corren, **la pieza de índice 3 sigue siendo la única resaltada**, la disposición no se movió, el identificador sigue valiendo y el retorno declara el **estado efectivo de los dos** | `CU-07` CA-01 y CA-03 |
| 6 | Invocar `destruir` y después cualquier otra función con el mismo identificador, incluida `establecerMovimiento` | Cada invocación posterior informa `INSTANCIA_DESCONOCIDA` y nada se rompe | `CU-05` CA-02; `CU-07` CA-04 |

**Objetivo del tramo, verificable:** las seis comprobaciones dan lo esperado, incluidas las dos que informan condición.

**Qué llega en `opciones`, y cómo se cambia después.** Dos de las opciones que `inicializar` recibe están declaradas por el contrato y gobiernan el **movimiento automático de la escena** (`Definicion-Contrato-De-Fachada.md` §4.1 y §5.5, capacidad **F-25**): el estado inicial de la **órbita de la cámara** —el punto de vista gira solo y las piezas quedan quietas— y el estado inicial del **giro de las figuras** —cada pieza rota sobre su eje, en su lugar—. Son independientes, pueden estar prendidos los dos a la vez y, con las opciones ausentes o parciales, los dos arrancan **apagados**: la fachada no consulta preferencias del sistema. En la forma neutral de la secuencia:

```text
id = inicializar(elementoDeDibujo, { orbitaDeLaCamara: apagada, giroDeLasFiguras: apagado })
                                                  → las dos opciones de gobierno de §5.5, con su valor inicial
```

Para cambiar el estado con la instancia **ya cargada** hay una función y una sola, la sexta: `establecerMovimiento` (`Definicion-Contrato-De-Fachada.md` §4.6, contrato de uso en `CU-07`). Una línea, sin reconstruir nada.

```text
estado = establecerMovimiento(id, { orbitaDeLaCamara: prendida })
                                                  → prende la órbita; el giro queda como estaba
                                                  → devuelve el estado efectivo de los DOS, para el control del anfitrión
```

Cuatro cosas que conviene tener claras antes de escribirlo en un anfitrión:

1. **Lo no nombrado conserva su estado.** Es la diferencia de semántica con `inicializar`, y es la que más confunde: en `inicializar` una opción ausente significa **apagado**, porque la instancia nace y hay que darle un estado; en `establecerMovimiento` significa **dejalo como está**, porque la escena ya tiene uno. Nombrar la órbita no es opinar sobre el giro.
2. **Se lee el retorno, no se supone.** La función devuelve el estado efectivo de los **dos** movimientos: es con eso, y no con lo que el anfitrión creía haber pedido, que se sincroniza el control visible.
3. **No reconstruye nada, y por eso reemplaza a la vía vieja.** Antes de la sexta función esto se conseguía con `destruir` → `inicializar` con las opciones nuevas → `cargarJson` con el mismo texto: era inocuo para la disposición por G-6, pero **perdía la selección vigente** y parpadeaba. Ahora la disposición, la selección, el encuadre, el resultado de dibujo vigente y el identificador **quedan exactamente como estaban**, y no hay nada que reponer. Además es idempotente: pedir lo que ya está no cambia nada. Al **apagar el giro**, cada pieza vuelve a su orientación de partida.
4. **Su única condición es `INSTANCIA_DESCONOCIDA`**, la misma de siempre, que con esta función pasa a presentarse en **cinco**. **No hay ningún código nuevo**: los del contrato siguen siendo **siete** (entrada `E-VIS-13` del catálogo).

Y una precisión que no es de detalle: **el estado de los movimientos sobrevive a `cargarJson`**. Cargar otro trabajo reemplaza el contenido dibujado, no el gobierno de la escena, así que el anfitrión **no tiene que volver a pedir el movimiento** después de cada carga.

**Lo que sigue siendo del anfitrión, y por qué no puede ser del bundle.** Tres cosas: **dibujar el control visible** con el que alguien prende y apaga cada movimiento, **consultar la preferencia de movimiento reducido del sistema** antes de pedirlo —consultarla la fachada sería leer configuración propia y violaría **G-3**— y **conservar la elección** de quien mira —guardarla la fachada sería persistir y violaría **G-2**—. La fachada sólo **recibe el estado deseado, lo aplica y devuelve el efectivo**. Y una constancia para el tramo: prender los dos movimientos **no agrega ninguna petición de red**; el conteo de la pestaña de red sigue en 0, y es justamente **con los dos prendidos** como se mide, porque es el peor caso (`../02-Especificacion-Funcional/Especificacion-Funcional.md` §6).

Ese es el punto del tramo: **las condiciones no son fallas del visor**. Son la forma en que un visualizador puro le devuelve el problema al único que puede resolverlo. Con eso ya alcanza para integrar: un integrador puede parar acá.

### 3.3 Hasta la hora: tocar el interior sin tocar el contrato

Este tramo es para el rol de mantenedor. Se toma una modificación chica del interior del archivo de guion —por ejemplo, en la construcción de la malla de un tipo, o en la lectura de la dimensión de un volumen— y se recorre el ciclo entero:

| Paso | Qué hacer | Qué tiene que pasar |
| --- | --- | --- |
| 1 | Modificar el interior, **nunca el archivo generado** | El cambio vive en el código fuente del proyecto de código; el artefacto se regenera, no se edita |
| 2 | Ejecutar `scripts/build-visor.sh` otra vez y recargar la página integradora | Se regenera sin errores |
| 3 | Cargar el texto del escenario **E-1** (`PRODUCT-INTAKE` §20.E-1) | Se dibujan **3 piezas** con los índices 0, 1 y 2, **ortoedro incluido** |
| 4 | Cargar el mismo texto dos veces seguidas y comparar | Las dos cargas producen la **misma disposición**, comparable pieza por pieza, y el mismo resultado de dibujo |
| 5 | Repetir crear, cargar, destruir alternando entre E-1 y E-7, **diez veces**, con los **dos movimientos prendidos** en cada vuelta | Los diez recorridos de ida y vuelta terminan con todas sus piezas dibujadas y la visualización no degrada. Los movimientos se prenden a propósito: un bucle de dibujo en curso al momento de `destruir` es el peor caso de esta propiedad, y con los movimientos apagados no se ejercitaría (`../02-Especificacion-Funcional/Especificacion-Funcional.md` §6) |
| 6 | Revisar el contrato | Las **seis** firmas, las siete garantías y los siete códigos siguen siendo los mismos |

**Objetivo del tramo, verificable:** el paso 6 no encuentra ninguna diferencia. El éxito de esta hora es una **no-diferencia**: cambió el interior y el contrato quedó idéntico.

Si el paso 6 sí encuentra diferencias, no es un detalle de implementación. Perder cualquiera de las siete garantías, o cambiar qué recibe una función, es **cambio mayor** de la superficie pública y rompe al componente anfitrión y al sample (`Definicion-Contrato-De-Fachada.md` §7).

## 4. Diagnóstico de problemas frecuentes en la primera hora

| Síntoma | Causa habitual en la primera hora | Qué hacer | Entrada del catálogo |
| --- | --- | --- | --- |
| La página integradora carga pero nunca dibuja, y no hay ninguna condición informada | No se ejecutó `scripts/build-visor.sh` después del último cambio, o se abrió la página apuntando a un archivo de guion viejo | Regenerar y recargar. El archivo generado no se edita a mano: si parece desactualizado, es que falta regenerarlo | — |
| `inicializar` no devuelve identificador | El navegador no provee la capacidad gráfica tridimensional | Cambiar de navegador. No hay repliegue: la combinación está declarada no soportada | E-VIS-01 |
| `inicializar` no devuelve identificador y el navegador sí tiene la capacidad | Se invocó antes de que el elemento de dibujo existiera o tuviera tamaño, o estando oculto | Invocar cuando el elemento esté presente, visible y con tamaño distinto de cero | E-VIS-02 |
| Todas las funciones informan `INSTANCIA_DESCONOCIDA` | Se está usando un identificador de una instancia ya destruida, o se guardó mal el que devolvió `inicializar` | Volver a inicializar y conservar el identificador nuevo. Una instancia liberada no vuelve | E-VIS-03 a E-VIS-06 |
| Se cargó un texto y la escena quedó vacía | Del texto no se pudo obtener un conjunto de piezas | Cargar otro texto: la instancia sigue viva. **No presentarlo como veredicto sobre el trabajo**: la fachada no valida | E-VIS-08 |
| Se dibujan menos piezas de las que trae el trabajo | Hay piezas de tipo no dibujable, o piezas dibujables cuya dimensión no se pudo leer | Leer la lista de piezas **no dibujadas** del resultado de dibujo y señalarlas por su índice. Siempre está: ninguna pieza desaparece sin registro | E-VIS-09, E-VIS-10 |
| La figura que declara una dimensión en `0.00` no aparece en la escena | No es el comportamiento esperado: **el cero es una dimensión legible** y esa pieza tiene que dibujarse, aunque la malla se vea degenerada. Lo que hace ilegible una dimensión es la **ausencia** de la clave o del componente, nunca su valor | Verificar primero que la pieza no esté entre las **no dibujadas** del resultado de dibujo. Si está, es defecto: la lectura de dimensiones evalúa la verdad del número en lugar de su presencia, que es el defecto del visualizador previo, contradice el escenario `E-6` del intake §20 y vacía la garantía G-5. Se reporta, no se explica al anfitrión | E-VIS-10 |
| Un ortoedro no se dibuja aunque el backend sí lo interpreta | Es el defecto histórico que el producto viene a eliminar: la lectura de la clave de las bases | Reportarlo como defecto. El contrato exige que la fachada acepte las mismas variantes de clave que el backend, para que no haya piezas interpretadas que la escena no dibuje | E-VIS-10 |
| Elegir un elemento del árbol no resalta nada | Los índices del árbol son de una carga anterior, o la pieza existe pero no se dibujó | Reconstruir el árbol con los índices del último resultado de dibujo. Si la pieza está enumerada como no dibujada, explicar por qué en vez de dejarla seleccionable | E-VIS-11, E-VIS-12 |
| El movimiento automático se prendió y en algún momento se detuvo solo | No es una falla: los dos movimientos se detienen mientras se **arrastra la cámara** y mientras la **superficie de dibujo no está visible**, y esa detención **no cambia el estado gobernado**. Si nunca arrancó, revisar si las opciones llegaron a `inicializar` —ausentes o parciales, los dos arrancan apagados— o pedirlo con `establecerMovimiento` sobre la instancia viva | Nada que corregir en la fachada. Del lado del anfitrión, reflejar en su propio control el **estado efectivo** que devolvió `establecerMovimiento` más la preferencia que él conserva, y **no acuñar un código** para esto: no es una condición de la fachada | `DX-Error-Messages.md` §4 |
| Se prendió un movimiento y el árbol perdió la pieza que estaba resaltada | Es un defecto del anfitrión, no del visor: `establecerMovimiento` **no toca la selección**, así que si el resaltado se perdió es que el anfitrión reconstruyó la instancia —la vía vieja, `destruir` → `inicializar` → `cargarJson`— en lugar de invocar la sexta función | Cambiar el estado del movimiento con `establecerMovimiento(id, opciones)`, que conserva disposición, selección, encuadre e identificador. La reconstrucción para esto ya no es una vía documentada | — |
| `establecerMovimiento` informa `INSTANCIA_DESCONOCIDA` | El control de movimiento del anfitrión siguió vivo en la página después de que la instancia se destruyó | Desactivar el control en el mismo momento en que se invoca `destruir`, igual que se desactiva la interacción del árbol. No mostrarlo como error: es un pedido llegado tarde | E-VIS-13 |
| Aparece alguna petición en la pestaña de red | No es una condición: es una **violación del gate de cero red** | Es defecto bloqueante. El umbral es exactamente 0 y no admite excepción | `DX-Error-Messages.md` §4 |
| Después de varios idas y vueltas la visualización se pone lenta | Falta invocar `destruir` al salir de cada vista | Liberar la instancia en el cierre de la vista del anfitrión. Diez recorridos de ida y vuelta no deben degradar | `DX-Error-Messages.md` §4 |

## 5. Próximos pasos

| Modo Diátaxis | Adónde ir | Para qué |
| --- | --- | --- |
| **Tutorial** | Este documento | Ya se recorrió. No se vuelve: se consulta el modo reference |
| **How-to** | 11-Documentacion, entradas de tarea de integración; material ejecutable en 10-Examples (sample S-1) | Embeber el archivo de guion en una superficie, sincronizar un árbol con la escena por índice, liberar la instancia al cerrar una vista, reaccionar a cada condición |
| **Reference** | [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §4, §5 y §6, y [`DX-Error-Messages.md`](DX-Error-Messages.md) | Consultar una función, el resultado de dibujo, un código de condición o la política de compatibilidad de la superficie pública |
| **Explanation** | [`../../../00-Contexto/Vision-Producto.md`](../../../00-Contexto/Vision-Producto.md) §3, `PRODUCT-INTAKE` §14 y §18, y [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) §1 | Entender por qué el visor es un visualizador puro y por qué su contrato es el punto de extensión del producto |

Complementos dentro de esta misma categoría: el marco DX completo en [`DX-Developer-Experience.md`](DX-Developer-Experience.md) y el vocabulario de la sección en [`Glosario-UX.md`](Glosario-UX.md).

## 6. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Superficie pública documentada | Las seis funciones de la fachada, recorridas en el orden de su ciclo de vida, con `establecerMovimiento` incorporada el 2026-08-09 (`Definicion-Contrato-De-Fachada.md` §4.6). Las **siete** garantías y los **siete** códigos no cambian |
| Rol de intervención | Developer integrador del bundle en §3.1 y §3.2; developer mantenedor del bundle en §3.3 |
| Necesidad de negocio | `NB-06` §5, criterios primero (3 de 3 piezas), segundo (6 de 6 tipos), tercero (10 de 10 recorridos de ida y vuelta) y cuarto (disposición estable), que son los que verifican los tramos |
| CU origen | `CU-01` a `CU-05` para cada comprobación de §3.2 y §3.3; `CU-07` para la comprobación de movimiento de §3.2; `CU-06` para el recorrido de integración completo sin backend. Ése es también el **orden de lectura** de los siete: `CU-01` a `CU-05`, después `CU-07` y por último el transversal `CU-06`, que los recorre juntos (`Especificacion-Funcional.md` §3.2). `CU-07` lleva número más alto que `CU-06` porque se emitió después, con la sexta función, y **no se renumera** |
| Reglas de negocio relevantes | Ninguna. Este proyecto de código no declara RN |
| Wireframes asociados | N/A. Variante DX con cero wireframes |
| Propiedades transversales | Las seis, con su membresía y su umbral, en `../02-Especificacion-Funcional/Especificacion-Funcional.md` §6, lugar único. Los tramos de §3 las ejercen y **no las re-enumeran** |
| US a generar | 06-Backlog-Tecnico: US del guion de construcción corto, US de la página integradora sin backend y US del recorrido de integración que verifica el contrato tras modificar el interior |
| Tests previstos | 08-Calidad-Y-Pruebas: los tramos de §3 como recorrido de integración de humo del archivo de guion, con E-7 y E-1 como material |
| Catálogo de diseño aplicado | N/A para la variante DX |
| Validación visual de maqueta | **Ejecutada y aprobada** dentro de la maqueta de `GeometriaFactory-Web`: este proyecto de código no tuvo maqueta propia. De ella salen el gobierno del movimiento automático que ejerce §3.2 —con la **sexta función** que el Product Owner decidió al cerrarla— y el síntoma de la dimensión en `0.00` de §4 |

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-08 | Emisión inicial. Recorrido de integración de la primera hora en tres tramos con objetivo verificable, íntegramente dentro del entorno de desarrollo contenido y sin ningún servicio del backend: cinco minutos hasta dibujar las seis piezas de E-7 con cero peticiones, treinta hasta ejercer el contrato entero incluidas dos condiciones, y una hora hasta modificar el interior del archivo de guion y comprobar que el contrato quedó idéntico. Suma diez síntomas de diagnóstico de la primera hora enlazados al catálogo de condiciones y los cuatro modos de Diátaxis con su destino. |
| 1.0 | 2026-08-08 | Correcciones absorbidas del audit `B-02-03-GeometriaFactory-Visor-r1.md`, sin subir versión por `Master-Prompt.md` §5 (documento en estado `Propuesto`). **H-03**: se califican las nueve ocurrencias desnudas del sustantivo «recorrido» —cabecera, §1.2, §3.3 paso 5, §4, §6 en cuatro filas y el control de cambios—, que pasan a «recorrido de integración» o a «recorrido de ida y vuelta» según su referente; §5 sustituye además el participio «ya está recorrido» por «ya se recorrió», que no admite lectura de sustantivo. **H-06**: el control de cambios decía «once síntomas» y la tabla de §4 tiene **diez** filas, contadas una a una; el conteo queda corregido a diez y la tabla no se modifica. **H-02, de su lado**: §6 suma la fila que remite a `Especificacion-Funcional.md` §6 como lugar único de la membresía y del umbral de las **seis** propiedades transversales. |
| 1.0 | 2026-08-09 | Retroalimentación de la Fase B2 de validación de maqueta del proyecto de código `GeometriaFactory-Web`, dentro de la cual se validó la fachada de este proyecto de código por no tener maqueta propia. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) Capacidad F-25, movimiento automático de la escena** (`Definicion-Contrato-De-Fachada.md` §4.1 y §5.5): **§3.2** suma, después de las cinco comprobaciones, qué llega en `opciones` —las dos opciones independientes de órbita de la cámara y giro de las figuras, con arranque **apagado** ante opciones ausentes o parciales— y cómo se cambia el estado con la instancia viva **dentro de las cinco funciones**, con el bloque neutral `destruir` → `inicializar` → `cargarJson` → `seleccionarPieza`, la advertencia de que la **selección vigente no sobrevive** y hay que reponerla, la constancia de que es inocuo por G-6 y la de que **no hay sexta función** mientras el punto abierto del contrato siga abierto; deja además del lado del anfitrión conservar la preferencia (G-2) y consultar la de movimiento reducido del sistema (G-3), y declara que prender los dos movimientos no agrega peticiones de red. **§4** suma el síntoma del movimiento que se detiene solo, remitido a `DX-Error-Messages.md` §4 y sin código. **(b) El cero como dimensión legible**: **§4** suma el síntoma de la figura con una dimensión en `0.00` que no aparece, con el diagnóstico de que es **defecto a reportar** y no condición, anclado en el escenario `E-6` del intake §20 y en la garantía G-5, y remitido a `E-VIS-10`. La tabla de §4 pasa de **diez** a **doce** síntomas. **§6** actualiza la fila de validación visual de maqueta, que pasa de prevista a **ejecutada y aprobada**. |
| 1.0 | 2026-08-09 | Alineación con la **sexta función de la fachada**, `establecerMovimiento(id, opciones)`, acuñada por `Definicion-Contrato-De-Fachada.md` §4.6 al cerrar la **Fase B2**, con contrato de uso en el **`CU-07` nuevo** y consolidación en el intake **1.6**. **Sin subir versión** por `Master-Prompt.md` §5, que lo admite mientras el documento está en estado `Propuesto`. **(a) El recorrido de integración pasa a seis funciones**: **§1.2** exige poder nombrar las **seis**, **§1.3 regla 1** las enumera con `establecerMovimiento` y agrega que tocar la escena para prender un movimiento es fuga, **§3.2** suma su línea a la secuencia neutral y una **sexta comprobación** —prender los dos movimientos con la pieza de índice 3 resaltada y verla seguir resaltada (`CU-07` CA-01 y CA-03)—, la quinta pasa a incluir a `establecerMovimiento` entre las funciones que informan `INSTANCIA_DESCONOCIDA` tras `destruir`, y **§3.3 paso 6** revisa **seis** firmas. **(b) La vía vieja queda reemplazada**: el bloque «cómo se cambia después» ya no reconstruye la instancia —`destruir` → `inicializar` → `cargarJson` → `seleccionarPieza`, que perdía la selección y parpadeaba— sino que invoca la sexta función, y se declaran su **firma**, su **retorno** —el estado efectivo de los dos, que se lee y no se supone—, su **semántica de opciones parciales** —lo no nombrado conserva su estado, a diferencia de `inicializar`, donde lo ausente arranca **apagado**—, su idempotencia, la reposición de la orientación al apagar el giro y su **única condición**, `INSTANCIA_DESCONOCIDA`, **sin código nuevo**. Se declara además que el **estado de los movimientos sobrevive a `cargarJson`**. **(c) Frontera bundle/anfitrión**: §3.2 deja del lado del anfitrión el **control visible**, la **consulta de la preferencia de movimiento reducido** (G-3) y la **conservación de la elección** (G-2); la fachada recibe el estado deseado, lo aplica y devuelve el efectivo. **(d) Condiciones de medición**: §3.3 paso 5 hace los **diez recorridos de ida y vuelta con los dos movimientos prendidos**, que es el peor caso de la propiedad de liberación de recursos, y §3.2 recuerda que el conteo de peticiones se mide igual; los umbrales no cambian y su lugar único sigue siendo `Especificacion-Funcional.md` §6. **(e)** La tabla de §4 pasa de **doce** a **catorce** síntomas, con la selección perdida por reconstruir en lugar de invocar la sexta función y con `INSTANCIA_DESCONOCIDA` desde `establecerMovimiento` (entrada **`E-VIS-13`**). **(f)** La cabecera y **§6** pasan a `CU-01` a `CU-07` y declaran el **orden de lectura** —`CU-01` a `CU-05`, después `CU-07` y por último `CU-06`—. Las siete garantías y los siete códigos **no cambian**. |
| 1.0 | 2026-08-09 | Corrección absorbida de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`, **sin subir versión** por `Master-Prompt.md` §5. **`AB2-10`**: la fecha de cabecera decía 2026-08-08 y el documento tiene entradas de control de cambios fechadas 2026-08-09; pasa a **2026-08-09**, que es cuando se lo tocó por última vez. Ningún contenido cambia. |
