# Ejemplo 02 — Árbol y escena sincronizados por índice, y ninguna pieza que desaparezca sin aviso

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Visor
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** [`/samples/visor/02-intermedio/`](../../../../../samples/visor/02-intermedio/), esqueletada con su README local y su comando previsto
**Trazabilidad upstream:** [`CU-12002`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md), [`CU-12003`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12003-Seleccionar-Una-Pieza-Por-Su-Indice.md) y [`CU-12004`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12004-Redimensionar-La-Escena.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §4.3, §4.4, §5.2, §5.3 y §6; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0 `TC-12006`, `TC-12007`, `TC-12008`, `TC-12011` y `TC-12012`; `PRODUCT-INTAKE` **1.25** §20, escenarios `E-2`, `E-5`, `E-6`, `E-7` y `E-8`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-12002` como sonda `SD-12014`; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar las dos capacidades que convierten al visor en instrumento didáctico y no en una escena bonita: que el árbol del texto y la escena **se sincronizan por índice** sin traducir identidades, y que **ninguna pieza desaparece sin quedar enumerada** con su índice y su código. Al terminar, quien lo ejecuta sabe leer el resultado de dibujo, resaltar una pieza desde el árbol y distinguir una dimensión ausente de una dimensión en cero.

**Es la segunda de las tres partes del sample `S-1` del `PRODUCT-INTAKE` §18.**

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01. Agrega **dos** funciones de la fachada —`seleccionarPieza` y `redimensionar`— y **cinco** de los **ocho** escenarios reales del intake §20, elegidos porque son los que ponen a prueba la lectura del dato del alumno.

## 3. Prerequisites

Los mismos cinco ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3, sin agregados de herramienta. Un agregado de datos: los **cinco** textos de escenario que el sample usa se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**, incluido el de `E-2`, que trae la clave `Tapas` y dos comas finales.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/02-intermedio run verify`.
4. Para mirarlo a mano, abrir `samples/visor/02-intermedio/index.html` y elegir un escenario del selector.
5. Comparar con §6.

## 5. Estructura del código

```
samples/visor/02-intermedio/
├── README.md                        # Copia corta de §1, §3 y §4 de este documento
├── package.json                     # Declara el comando `verify` del contrato de §9
├── index.html                       # Superficie de dibujo, árbol colapsable y selector de escenario
├── anfitrion.<ext>                  # Sincroniza el árbol con la escena por índice
├── datos/
│   ├── E2.txt  E5.txt  E6.txt       # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── E7.txt  E8.txt               #   E2.txt NO es JSON estrictamente válido: es su gracia
└── tests/
    └── lectura-y-seleccion.<ext>    # Conduce el navegador y compara con el snapshot de §6
```

**El árbol lo dibuja el anfitrión, no la fachada.** `cargarJson` **devuelve la estructura** del texto recibido y la presentación es del componente anfitrión (`Definicion-Contrato-De-Fachada.md` §5.2). El sample lo hace explícito porque es la frontera que más fácil se cruza sin darse cuenta.

## 6. Qué esperar

```
[1] E-7 cargado: piezas dibujadas=6 | tipos volumetricos=3 | tipos planos=3
[2] E-7, ortoedro: ancho=6 profundidad=4 altura=8
[3] E-2, clave Tapas: el ortoedro se dibuja=si
[4] E-7, clave Bases: el ortoedro se dibuja=si (las dos claves son sinonimos)
[5] E-5 cargado: dibujadas=1 no dibujadas=1 | indice=1 codigo=TIPO_NO_DIBUJABLE
[6] E-8 cargado: dibujadas=1 no dibujadas=1 | indice=1 codigo=DIMENSION_NO_LEGIBLE campo=Largo
[7] E-6 cargado: dibujadas=1 no dibujadas=0 (el cero es una dimension legible)
[8] Estructura del texto de E-8: piezas=2 (incluida la que no se dibujo)
[9] Seleccion del indice 0: resaltadas=1 | resaltado exclusivo=si
[10] Seleccion del indice 1 de E-8, enumerado como no dibujado: INDICE_FUERA_DE_RANGO
[11] Seleccion de un indice fuera del conjunto raiz: INDICE_FUERA_DE_RANGO | seleccion vigente conservada=si
[12] Redimensionar tras cambiar el tamano: relacion de aspecto recalculada=si
[13] Redimensionar con la superficie oculta: ELEMENTO_DE_DIBUJO_INVALIDO curso C-2 | instancia viva=si
[14] Redimensionar con la superficie devuelta a un tamano valido: ajuste aplicado=si
Funciones ejercidas: 5 de 6 | Piezas no dibujadas sin registro: 0 | Peticiones de red: 0
```

**Las líneas `[6]` y `[7]` juntas son la distinción que el producto viene a instalar.** En `E-8` la dimensión **está ausente** y la pieza no se dibuja, pero **queda enumerada** con su índice y su campo; en `E-6` la dimensión **está y vale `0.00`**, y la pieza **se dibuja**. Lo que produce `DIMENSION_NO_LEGIBLE` es la ausencia de la clave, nunca el valor que trae. El visualizador previo perdía la figura de `E-6` sin aviso porque evaluaba la verdad del número en lugar de su presencia.

**La línea `[10]` no es un error del sample.** Un índice que el resultado de dibujo enumera como **no dibujado** figura en el resultado pero **no tiene malla que resaltar**, y por eso `seleccionarPieza` informa `INDICE_FUERA_DE_RANGO`. Es uno de los dos casos que ese código cubre, y los dos son **un mismo curso**, no dos.

**Las líneas `[13]` y `[14]` son el segundo curso de `ELEMENTO_DE_DIBUJO_INVALIDO`.** Es el mismo código que la primera variación del ejemplo 01, con otro efecto: allá **no se crea** la instancia, acá **sigue viva** con su escena y su selección intactas, y una invocación posterior ajusta.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Cargar `E-3` y `E-4` | Agregar los dos cubos de lado 3 de los ejemplos de la cátedra | Los dos se dibujan igual, porque el campo que se usa es `Largo`. **La fachada no emite ninguna observación** sobre el área declarada: leer una dimensión no es validar un trabajo |
| Poner una dimensión de `E-6` en un valor negativo | Cambiar `"Largo": 0.00` por un negativo | La pieza sigue teniendo la clave presente; lo que decide si el trabajo es válido es el backend, no la fachada |
| Seleccionar desde la escena en lugar del árbol | Invertir la dirección de la sincronización | El índice es el mismo en las dos direcciones: es lo que hace innecesario traducir identidades |
| Cargar otro texto sin destruir la instancia | Encadenar dos escenarios | El contenido anterior se libera por completo y se dibuja el nuevo |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-12002`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) | Caso de uso | Actos `[1]` a `[8]` |
| [`CU-12003`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12003-Seleccionar-Una-Pieza-Por-Su-Indice.md) | Caso de uso | Actos `[9]` a `[11]` |
| [`CU-12004`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12004-Redimensionar-La-Escena.md) | Caso de uso | Actos `[12]` a `[14]` |
| Garantía `G-5` · Sin fallo silencioso | Garantía del contrato de fachada | Actos `[5]`, `[6]`, `[7]` y el recuento final de piezas sin registro en 0 |
| Garantía `G-7` · Terminación controlada | Garantía del contrato de fachada | Actos `[11]` y `[13]`: la instancia y la selección quedan como estaban |
| §5.3 del contrato de fachada, tipos dibujables | Elemento del concepto | Actos `[1]` a `[4]` y `[7]` |
| [`ADR-12002`](../05-Arquitectura-Tecnica/Adrs/ADR-12002-Superficie-De-Seis-Funciones-Planas.md) | Decisión arquitectónica | Las **cinco** funciones que este sample invoca —de las **seis** que ADR-12002 declara como superficie pública— se invocan desde el anfitrión, y ninguna otra. La sexta, `establecerMovimiento`, la ejerce el ejemplo 03 |
| [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | Decisión arquitectónica | El anfitrión no nombra ninguna primitiva del motor de dibujo |
| `PRODUCT-INTAKE` §20 `E-2`, `E-5`, `E-6`, `E-7`, `E-8` | Escenario con payload real | Los cinco textos, transcriptos sin modificación |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-12002
  verifica: [CU-12002, CU-12003, CU-12004, US-12005, US-12006, US-12007, US-12009, US-12010]
  comando: "bash scripts/build-visor.sh && npm --prefix samples/visor/02-intermedio run verify"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Navegador con capacidad gráfica tridimensional disponible para el conductor"
    - "0 servicios del backend levantados"
    - "Sin acceso a redes de distribución externas"
    - "Los cinco textos de datos/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] E-7 cargado: piezas dibujadas=6 | tipos volumetricos=3 | tipos planos=3"
      - "[2] E-7, ortoedro: ancho=6 profundidad=4 altura=8"
      - "[3] E-2, clave Tapas: el ortoedro se dibuja=si"
      - "[6] E-8 cargado: dibujadas=1 no dibujadas=1 | indice=1 codigo=DIMENSION_NO_LEGIBLE campo=Largo"
      - "[7] E-6 cargado: dibujadas=1 no dibujadas=0 (el cero es una dimension legible)"
      - "[13] Redimensionar con la superficie oculta: ELEMENTO_DE_DIBUJO_INVALIDO curso C-2 | instancia viva=si"
      - "Funciones ejercidas: 5 de 6 | Piezas no dibujadas sin registro: 0 | Peticiones de red: 0"
    stdout_no_contiene:
      - "E-6 cargado: dibujadas=0"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye una aserción negativa.** `E-6 cargado: dibujadas=0` no debe aparecer nunca: perder esa figura vaciaría la garantía `G-5` y repetiría el defecto del visualizador previo, que es lo que el producto viene a corregir.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección de precisión de recuento, hallada al resolver el informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0 y no reportada por él.** La fila de `ADR-12002` de la §8 decía «Las cinco funciones se invocan desde el anfitrión y ninguna otra», atribuyéndole a esa ADR una superficie de **cinco** funciones cuando declara **seis** desde su título y su §2 —las cinco son las que **este sample** invoca, no las que la ADR declara—. Se enlaza además la carpeta esqueletada de [`/samples/visor/02-intermedio/`](../../../../../samples/visor/02-intermedio/) creada al resolver el **P0-1**, y se actualiza la trazabilidad al `PRODUCT-INTAKE` **1.25**. Ningún acto, criterio de aceptación ni recuento del contrato cambia. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Segunda parte del sample **S-1**. Cubre `CU-12002`, `CU-12003` y `CU-12004`, lleva las funciones ejercidas a **5 de 6** y usa **cinco** escenarios del `PRODUCT-INTAKE` §20 transcriptos sin modificación. Verifica los **seis** tipos dibujables, los dos sinónimos de clave del emisor, los **dos** cursos de `ELEMENTO_DE_DIBUJO_INVALIDO` y los **dos** casos de `INDICE_FUERA_DE_RANGO`. El contrato `VER-12002` declara siete líneas exactas de salida y **una aserción negativa** sobre la figura de `E-6`; `evidencia` queda en `No verificado — sin código`. |
