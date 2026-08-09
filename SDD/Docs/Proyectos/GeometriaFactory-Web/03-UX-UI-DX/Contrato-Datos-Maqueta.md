# Contrato de datos de la maqueta — GeometriaFactory-Web

**Proyecto de código:** GeometriaFactory-Web
**Documento:** Contrato-Datos-Maqueta.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-09
**Autor:** Maquetador de validación visual (AG-03M)
**Variante:** UX/UI
**Trazabilidad upstream:** `SDD/Maquetas/GeometriaFactory-Web/assets/js/Datos-Maqueta.js`, arreglo `CONTRATO_DE_CAMPOS` y las colecciones `CUENTAS`, `TRABAJOS`, `OBSERVACIONES_*`, `ARBOL_*` e `IDENTIDAD_DE_VERSION`; `../../GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` §2.1 a §2.5; `../02-Especificacion-Funcional/Casos-De-Uso/` `CU-01` a `CU-10`; los once `Wireframes-*.md` y las tres `Representacion-*.md` de esta categoría; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §20 (los siete escenarios de datos) y §4.1 (RN-05, RN-08, RN-09); `Deriva-Rules.md` §2.2
**Trazabilidad downstream:** [`Linea-Base-Visual.md`](Linea-Base-Visual.md), cuyos `SUP-XX` cita este documento; `Matriz-Sensado-Deriva.md` de `08-Calidad-Y-Pruebas`; `05-Arquitectura-Tecnica`, modelo lógico; `08-Calidad-Y-Pruebas`, casos de prueba de datos

---

## Tabla de contenido

- [1. Qué fija este contrato](#1-qué-fija-este-contrato)
- [2. Campos exhibidos (`DM-XX`)](#2-campos-exhibidos-dm-xx)
- [3. Formato de presentación, que también es contrato](#3-formato-de-presentación-que-también-es-contrato)
- [4. Campos del modelo conceptual que ninguna superficie exhibe](#4-campos-del-modelo-conceptual-que-ninguna-superficie-exhibe)
- [5. Valores compuestos para la maqueta](#5-valores-compuestos-para-la-maqueta)
- [6. Invariantes de los datos que el sistema construido no puede contradecir](#6-invariantes-de-los-datos-que-el-sistema-construido-no-puede-contradecir)
- [7. Evidencia](#7-evidencia)
- [8. Control de cambios](#8-control-de-cambios)

---

## 1. Qué fija este contrato

Es el modelo de datos **tal como quedó validado visualmente**: cierra la brecha entre el modelo conceptual de `GeometriaFactory-Domain`, que es abstracto, y lo que el Product Owner efectivamente vio y aprobó en la maqueta el 2026-08-09.

Tres reglas, de `Deriva-Rules.md` §2.2:

1. **Todo campo que la maqueta exhibe tiene su fila.** Son veintinueve.
2. **Todo campo del modelo conceptual que ninguna superficie exhibe se declara en §4, con su motivo.** Un campo que el Product Owner nunca vio no está validado visualmente, y eso hay que saberlo.
3. **El formato de presentación es parte del contrato.** Una fecha aprobada en formato corto que aparezca como marca de tiempo cruda es deriva, aunque el dato sea el mismo. §3 lo fija.

El insumo directo es el arreglo `CONTRATO_DE_CAMPOS` de `Datos-Maqueta.js`, que es la fuente única de los datos de la maqueta: **ningún archivo de la maqueta los hardcodea**.

## 2. Campos exhibidos (`DM-XX`)

| ID | Entidad | Campo | Tipo | Obligatorio | Ejemplo exhibido | Superficies | Correspondencia en el modelo conceptual | Regla que lo condiciona |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `DM-01` | Cuenta | `correo` | Texto | Sí | `ana@ej.test` | `SUP-01`, `SUP-02`, `SUP-03`, `SUP-09`, `SUP-10` | `Cuenta` · Correo | Único en todo el sistema (INV-01, RN-02). Es el texto que el administrador transcribe para confirmar una baja (RN-07) |
| `DM-02` | Cuenta | `nombre` | Texto | Sí | `Ana` | `SUP-02`, `SUP-09`, `SUP-10`, `SUP-07` | `Cuenta` · Nombre | — |
| `DM-03` | Cuenta | `apellido` | Texto | Sí | `Diaz` | `SUP-02`, `SUP-09`, `SUP-10`, `SUP-07` | `Cuenta` · Apellido | — |
| `DM-04` | Cuenta | `papel` | Conjunto cerrado de 2: `Alumno`, `Administrador` | Sí | `Alumno` | `SUP-09`, y la barra lateral de las seis superficies del shell de trabajo | `Cuenta` · Papel | Conjunto cerrado, sin permisos configurables (RN-01, INV-05) |
| `DM-05` | Cuenta | `estadoCuenta` | Conjunto cerrado de 3: `Pendiente`, `Habilitado`, `Bloqueado` | Sí | `Habilitado`, rotulado **«situación»** y en femenino: `Habilitada` | `SUP-09` | `Cuenta` · Estado de cuenta | Conjunto cerrado. El valor inicial depende del camino de alta |
| `DM-06` | Cuenta | `fechaRegistro` | Fecha del sistema | Sí | `05/08/2026` | `SUP-09` | `Cuenta` · Fecha de alta | La provee el consumidor: el dominio no lee el reloj |
| `DM-07` | Cuenta | `iniciales` | Texto derivado de 2 caracteres | Sí | `AD` | `SUP-09`, `SUP-10` | Derivado de `Cuenta` · Nombre y Apellido. **No es un atributo del modelo conceptual** | Se compone en la presentación; no se persiste |
| `DM-08` | Trabajo | `nombre` | Texto declarado por el alumno | Sí | `Cubo y ortoedro` | `SUP-05`, `SUP-06`, `SUP-07`, `SUP-08`, `SUP-10` | `Trabajo` · Nombre | — |
| `DM-09` | Trabajo | `fechaTrabajo` | Fecha **declarada por el alumno** | Sí | `12/08/2026` | `SUP-05`, `SUP-06`, `SUP-07`, `SUP-10` | `Trabajo` · Fecha | Es dato del alumno y no del reloj del sistema. Se rotula distinto de la fecha de registro para que no se lean como la misma |
| `DM-10` | Trabajo | `descripcion` | Texto libre declarado por el alumno | No, admite vacío | `Entrega de la Actividad 1 con las tres figuras del enunciado.` | `SUP-06`, `SUP-07` | `Trabajo` · Descripción | — |
| `DM-11` | Trabajo | `estado` | Conjunto cerrado de 4: `Borrador`, `Pendiente`, `Finalizado`, `Rechazado` | Sí | `Pendiente`, calificado como estado del trabajo | `SUP-05`, `SUP-06`, `SUP-07`, `SUP-08`, `SUP-10` | `Trabajo` · Estado | Conjunto cerrado, con `Finalizado` y `Rechazado` terminales (INV-07). `SUP-10` **nunca muestra `Borrador`** (RN-11) |
| `DM-12` | Trabajo | `textoOriginal` | Cadena íntegra | Sí | El texto del escenario `E-1` del intake §20, carácter por carácter | `SUP-06`, `SUP-07`, `SUP-08` | `Trabajo` · Texto original | Se conserva íntegro y **nunca se reescribe** (RN-08). No se normaliza, no se reordena y no se le quita ningún carácter |
| `DM-13` | Trabajo | `piezas` | Entero | Sí | `3` | `SUP-05`, `SUP-07`, `SUP-10` | `Trabajo` · Cantidad de figuras del conjunto raíz | Incluye las figuras que no se pudieron reconstruir: es el rango de posiciones válidas (RN-09) |
| `DM-14` | Trabajo | `advertencias` | Entero | Sí | `2` | `SUP-05`, `SUP-07`, `SUP-10` | Derivado de `Trabajo` · Observaciones, contando las de especie advertencia | — |
| `DM-15` | Trabajo | `comentario` | Texto libre opcional, cardinalidad 0..1 | No | `Revisá el área del cubo.` | `SUP-07`, `SUP-08` | `Trabajo` · Comentario del administrador | A lo sumo uno, porque los dos desenlaces son terminales. **No es una observación y no es una calificación** |
| `DM-16` | Trabajo | `fechaDesenlace` | Fecha del sistema | No; presente sólo en los dos estados terminales | `13/08/2026` | `SUP-07`, `SUP-08` | Metadato de orquestación asociado a `Trabajo` · Estado. **No es un atributo del modelo conceptual del dominio** | Se exhibe únicamente en el bloque de trabajo resuelto |
| `DM-17` | Observación | `severidad` | Conjunto cerrado de 2: `Advertencia`, `Error de validación` | Sí | `Advertencia` | `SUP-06`, `SUP-07`, `SUP-08` | `Observación` · Especie | Sólo el error de validación impide el paso a estado `Pendiente` (RN-05). **Se muestra escrita**, no sólo por color |
| `DM-18` | Observación | `figura` | Índice entero | Sí cuando la observación es atribuible a una figura | `1` | `SUP-06`, `SUP-07`, `SUP-08` | `Observación` · Posición de pieza | Es la posición **en el texto**, dentro del rango del conjunto raíz (RN-09) |
| `DM-19` | Observación | `campo` | Texto | Sí en toda observación de especie error de validación | `Area` | `SUP-06`, `SUP-07`, `SUP-08` | `Observación` · Campo | Nunca se sustituye por un texto genérico (RN-09) |
| `DM-20` | Observación | `declarado` | Texto **sin reformatear** | Sí en las advertencias de discrepancia de valor | `36.00` | `SUP-06`, `SUP-07`, `SUP-08` | `Observación` · Valor declarado | **Se muestra exactamente como el texto del alumno lo trae.** Excepción declarada a la coma decimal (§3) |
| `DM-21` | Observación | `derivado` | Texto **sin reformatear** | Ídem | `54.00` | `SUP-06`, `SUP-07`, `SUP-08` | `Observación` · Valor derivado | **Se muestra exactamente como el sistema lo recalcula.** Misma excepción |
| `DM-22` | Observación | `texto` | Texto | Sí en el error de validación | `El tipo declarado no se pudo interpretar.` | `SUP-06`, `SUP-07`, `SUP-08` | Realización de presentación de `Observación` · Campo y Especie | No incluye traza, ni nombre de archivo, ni código de error (RA-03) |
| `DM-23` | Pieza | `indice` | Índice entero | Sí | `2` | `SUP-06`, `SUP-07`, `SUP-08` | `Pieza` · Posición | **Es la identidad de la pieza.** No se recalcula: una pieza conserva la posición de su figura en el texto aunque otras no se hayan podido reconstruir. Es el mismo índice con el que la pieza figura en el resultado de dibujo de la fachada |
| `DM-24` | Pieza | `tipo` | Discriminante declarado en el texto | Sí | `Ortoedro` | `SUP-06`, `SUP-07`, `SUP-08` | `Pieza` · Tipo | Seis tipos dibujables. Un tipo fuera del conjunto conocido produce observación de especie error de validación (RN-09) y condición `TIPO_NO_DIBUJABLE` de la fachada |
| `DM-25` | Condición del dibujo | `indice` | Índice entero | Sí | `1` | `SUP-06`, `SUP-07`, `SUP-08` | Del resultado de dibujo de la fachada, no del modelo de dominio | Se presenta **junto a la escena, nunca en la lista de observaciones**: no es un error del trabajo |
| `DM-26` | Condición del dibujo | `motivo` | Texto derivado del código de condición | Sí | `tipo no dibujable` | `SUP-06`, `SUP-07`, `SUP-08` | Realización de presentación de los códigos de `Definicion-Contrato-De-Fachada.md` §6 | Ningún mensaje visible incluye el código en crudo (RA-03) |
| `DM-27` | Identidad de versión | `versionLegible` | Cadena ya formada | Sí | `Versión 1.4.2` | Las once superficies, en el sello de versión | **No es un atributo del modelo conceptual**: la identidad de versión es del artefacto desplegado, no del dominio | Llega ya formada: **no se compone en la vista**. La rige `Design-Rules-Identidad-De-Version.md` §2 |
| `DM-28` | Identidad de versión | `identificadorDeConstruccion` | Referencia opaca | Sí | `a3f81c6` | `SUP-01`, `SUP-03`, y el detalle de diagnóstico | Ídem: **no es un atributo del modelo conceptual** | Es identidad del artefacto y **nunca topología**. Ídem `Design-Rules-Identidad-De-Version.md` §2 |
| `DM-29` | Identidad de versión | `esPreliminar` / `origenIndeterminado` | Booleanos | Sí | `false` / `false` | `SUP-01`, `SUP-03` | Ídem: **no es un atributo del modelo conceptual** | Gobiernan los estados `EST-13` y `EST-14` de la línea de base visual. Ídem `Design-Rules-Identidad-De-Version.md` §2 |

## 3. Formato de presentación, que también es contrato

| Campo | Formato aprobado | Qué sería deriva |
| --- | --- | --- |
| `DM-06`, `DM-09`, `DM-16` | Fecha corta `dd/mm/aaaa` | Una marca de tiempo cruda, o una fecha con hora |
| `DM-20`, `DM-21` | **Exactamente como llegan, sin reformatear.** Es la **excepción declarada** a la convención de coma decimal del producto | Reescribir `36.00` como `36,00`. Rompe la comparación que es el mayor valor didáctico del producto |
| Cualquier otro número visible | Coma decimal, que es la convención del país | — |
| `DM-11` | Insignia con **el texto siempre presente**; el color es refuerzo | Un punto de color sin texto |
| `DM-05` | Insignia con texto, rotulada **«situación»** y concordada en femenino | Rotularla «estado»: colisiona con `DM-11`, y `Pendiente` nombra dos estados distintos |
| `DM-12` | Texto íntegro, con avance uniforme por carácter, colapsado por omisión | Normalizarlo, reordenarlo o quitarle un carácter |
| `DM-15` | Bloque propio, con encabezado propio, **separado de la lista de observaciones**, sin severidad y sin tono de alerta | Mezclarlo con las observaciones, o presentarlo como calificación |

## 4. Campos del modelo conceptual que ninguna superficie exhibe

| Entidad · Atributo | Por qué no se exhibe |
| --- | --- |
| `Cuenta` · Identificador | Identidad interna. Ninguna superficie la muestra ni la pide: la persona se identifica por su correo. Exhibirla sería exponer topología |
| `Cuenta` · Credencial derivada | **Prohibido por contrato.** No se muestra, no se transporta y no se recupera: el producto no tiene canal de correo y la contraseña no se recupera, se vuelve a dar de alta la cuenta |
| `Trabajo` · Identificador | Identidad interna. La maqueta lo usa como parámetro de dirección para llevar el contexto entre superficies, pero **ninguna superficie lo dibuja** |
| `Trabajo` · Dueño | Se exhibe **realizado** como nombre y apellido del alumno (`DM-02` y `DM-03`) y como agrupación en `SUP-10`, nunca como referencia al identificador de la cuenta |
| `Trabajo` · Conjunto de piezas | Se exhibe **realizado** en el árbol de la estructura y en la escena, pieza por pieza, con `DM-23` y `DM-24`. La colección como tal no tiene representación propia |
| `Trabajo` · Observaciones | Ídem: se exhibe como lista, observación por observación, con `DM-17` a `DM-22` |
| `Pieza` · dimensiones y componentes —`Largo`, `Ancho`, `Radio`, `Caras`, `Bases`, `Tapas`, `Laterales`, `Lado`— | **Se ven, pero no como campos de una ficha**: viven dentro de `DM-12`, el texto original, que se muestra íntegro, y se recorren en el árbol de la estructura hasta la hoja. Ninguna superficie los presenta como pares de etiqueta y valor, y esa decisión es didáctica: el alumno tiene que leer su propio texto |
| `Observación` · atribución a una figura no reconstruida | Está cubierta por `DM-18`, que admite el índice de una figura que no se pudo reconstruir. No hay un campo aparte |

## 5. Valores compuestos para la maqueta

Cuatro valores que la documentación no declaraba y que el Product Owner autorizó componer para que la maqueta fuera recorrible. Están marcados con `origen: 'compuesto-para-la-maqueta'` en `Datos-Maqueta.js` y **no forman parte del contrato de datos del producto salvo donde se indica**:

| Valor | Situación al cierre de la Fase B2 |
| --- | --- |
| La credencial de la cuenta de administrador de prueba, exhibida a la vista | **Instrumento de la maqueta.** No se traslada ni a la especificación ni al código |
| La cuarta cuenta de alumno, en situación `Pendiente`, necesaria para que las tres situaciones de cuenta convivan con dos alumnos que además tienen trabajos | **Dato de ejemplo de la maqueta.** No agrega ningún campo: usa `DM-01` a `DM-07` |
| El nombre del quinto trabajo, `Segundo intento`, necesario para mostrar el rechazo **sin** comentario escrito | **Propagado**: `Wireframes-Listado-De-La-Comision.md` §2 lo incorporó al retroalimentarse, con su origen declarado. No agrega ningún campo |
| El texto del escenario `E-7` sin el `Radio` del círculo, para demostrar la condición `DIMENSION_NO_LEGIBLE` de la fachada | **Punto abierto del `PRODUCT-INTAKE` 1.5**: es la única de las siete condiciones sin escenario propio en §20 ni fila en §21. Corresponde al Product Owner decidir si se incorpora un escenario `E-8` o si la condición queda declarada sin dato de prueba. **Esta línea de base no lo resuelve** |

## 6. Invariantes de los datos que el sistema construido no puede contradecir

Son las que la maqueta respeta y que el sensado de deriva verifica:

1. El texto semilla del escenario `E-1` produce **exactamente 3 piezas y 2 advertencias**.
2. El cilindro de `E-1` **no produce ninguna observación**: declara `113.10` y sus componentes suman `113.09`, con diferencia de exactamente `0.01`. El operador de tolerancia es **estricto** —advierte si la diferencia es *mayor* que `0.01`—, de modo que ese caso no advierte. Tres advertencias contradirían el caso de prueba canónico del producto.
3. Los valores `DM-20` y `DM-21` se muestran **sin reformatear**.
4. El **comentario** del administrador no es una observación ni una calificación.
5. `Pendiente` nombra **dos estados distintos** —el de la cuenta y el del trabajo— y todo texto visible lo califica. En `SUP-09` la columna se llama **«situación»** justamente para no colisionar.
6. Ningún valor visible incluye la dirección de un servicio interno, un nombre de archivo de datos, una traza ni un código de error (RA-03).
7. Una pieza que expone una dimensión con valor `0.00` **se dibuja**: el cero es una dimensión legible y no produce `DIMENSION_NO_LEGIBLE`.

## 7. Evidencia

| ID | Tipo | Ruta o comando | Ubicación | Fecha |
| --- | --- | --- | --- | --- |
| `EV-06` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/assets/js/Datos-Maqueta.js` | Sección 9, arreglo `CONTRATO_DE_CAMPOS`, insumo directo de §2 | 2026-08-09 |
| `EV-07` | `artefacto` | `SDD/Maquetas/GeometriaFactory-Web/assets/js/Datos-Maqueta.js` | Sección 10, `INVARIANTES_DE_LA_MAQUETA`, origen de §6 puntos 1 a 6 | 2026-08-09 |
| `EV-08` | `artefacto` | `SDD/Docs/Proyectos/GeometriaFactory-Domain/02-Especificacion-Funcional/Definicion-Modelo-De-Dominio.md` | §2.1 a §2.5, contra las que se verificó campo por campo la columna de correspondencia de §2 | 2026-08-09 |
| `EV-09` | `humano` | Aprobación explícita de la maqueta por el Product Owner | [`Bitacora-Validacion-Maqueta.md`](Bitacora-Validacion-Maqueta.md) §3, cierre de la iteración 4 | 2026-08-09 |
| `EV-10` | `artefacto` | `SDD/Intake/PRODUCT-INTAKE-Fabrica-De-Geometria.md` | §20, los siete escenarios de datos de los que sale todo ejemplo de §2 | 2026-08-09 |

## 8. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-09 | Corrección del hallazgo **`AB2-17`** de la auditoría `B2-Maqueta-GeometriaFactory-Web-r1.md`: `DM-27` a `DM-29` ponían una regla del framework —`Design-Rules-Identidad-De-Version.md` §2— en la columna de correspondencia con el modelo conceptual, donde las demás filas ponen entidad y atributo. Las tres pasan a declarar, como ya lo hacen `DM-07` y `DM-16`, que **no son atributos del modelo conceptual**, y la referencia a la regla se mueve a la columna de nota. Ningún campo, tipo, obligatoriedad ni superficie cambia. |
| 1.0 | 2026-08-09 | Emisión inicial, al cierre de la Fase B2 con la maqueta aprobada por el Product Owner. Veintinueve campos exhibidos con su tipo, su obligatoriedad, su ejemplo, sus superficies y su correspondencia con el modelo conceptual del dominio; el formato de presentación como parte del contrato; ocho entradas de campos del modelo conceptual que ninguna superficie exhibe, con su motivo; los cuatro valores compuestos para la maqueta con su situación al cierre de la fase; siete invariantes de datos; y cinco evidencias. |
