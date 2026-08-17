# Ejemplo 01 — La página integradora mínima: crear la escena, dibujar `E-1` y liberar

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ejemplo-01-basico.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/visor/01-basico/`
**Trazabilidad upstream:** [`CU-12001`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12001-Inicializar-Instancia-Del-Visor.md), [`CU-12002`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) y [`CU-12005`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12005-Destruir-La-Instancia-Y-Liberar-Recursos.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §4.1, §4.2, §4.5, §5.1 y §5.2; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0 `TC-12001`, `TC-12004`, `TC-12005` y `TC-12009`; `PRODUCT-INTAKE` 1.22 §18 sample **S-1** y §20.E-1
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-12001` como sonda `SD-12013`; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el recorrido mínimo del archivo de guion sobre una página sin ninguna pieza del backend: crear una instancia sobre una superficie de dibujo, cargar el texto del escenario `E-1` y ver dibujadas sus **tres** piezas —ortoedro incluido—, y liberar la instancia. Al terminar, quien lo ejecuta sabe cómo un componente anfitrión conversa con la fachada y por qué el identificador de instancia deja de valer en cuanto se libera.

**Este sample es la primera de las tres partes del sample `S-1` del `PRODUCT-INTAKE` §18**: la página integradora sin backend que RT §8.3 exige conservar. Las otras dos son los ejemplos 02 y 03.

## 2. Nivel

**Básico.** Toca **tres** de las **seis** funciones de la fachada —`inicializar`, `cargarJson` y `destruir`— y **tres** de los **siete** casos de uso. No hay selección, ni ajuste, ni gobierno del movimiento: eso es de los ejemplos 02 y 03.

## 3. Prerequisites

| Ítem | Versión mínima | Origen |
| --- | --- | --- |
| Entorno de ejecución de la cadena de herramientas | Versión de soporte prolongado, anclada en la etapa que la introduce | `PRODUCT-INTAKE` §17.2.P.1 · GeometriaFactory-Visor; corre **dentro** del entorno de desarrollo contenido |
| Entorno de desarrollo contenido del repositorio | El del propio repositorio, `.devcontainer/` | `PRODUCT-INTAKE` §16 y §17.2.P.1 · GeometriaFactory-Visor |
| Navegador con **capacidad gráfica tridimensional** | Declarada por capacidad, no por versión | `PRODUCT-INTAKE` §17.2.P.9 · GeometriaFactory-Visor. Sin esa capacidad el visor **no es soportado** |
| Conductor de navegador capaz de contar peticiones de red | — | [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §3 |
| Etapa que genera el archivo de guion, cerrada | — | Sin ella no hay bundle que cargar |

**Sin backend, y es la propiedad del sample.** No hace falta base de datos, ni servicio de datos, ni credencial, ni acceso a redes de distribución externas: el motor de dibujo va **dentro** del archivo de guion (`PT-03`).

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Generar el archivo de guion con el comando de construcción corto: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/01-basico run verify`.
4. Para mirarlo a mano, abrir `samples/visor/01-basico/index.html` en un navegador con capacidad gráfica tridimensional y pegar el texto de `E-1` en el área de texto.
5. Comparar con §6.

**El paso 2 usa el comando corto y no el completo.** `scripts/build-visor.sh` produce **sólo** el archivo de guion; `scripts/build.sh` lo encadena con la compilación del resto del producto (`PRODUCT-INTAKE` §17.2.P.8 · GeometriaFactory-Visor). Para trabajar sobre el visor rige el corto.

## 5. Estructura del código

```
samples/visor/01-basico/
├── README.md                    # Copia corta de §1, §3 y §4 de este documento
├── package.json                 # Declara el comando `verify` del contrato de §9
├── index.html                   # Superficie de dibujo, área de texto y carga del archivo de guion
├── anfitrion.<ext>              # El componente anfitrión mínimo: invoca las tres funciones
├── datos/
│   └── E1.txt                   # Transcripto del PRODUCT-INTAKE §20.E-1, sin modificación
└── tests/
    └── recorrido-basico.<ext>   # Conduce el navegador y compara con el snapshot de §6
```

**`index.html` carga el archivo de guion desde una ruta local del repositorio y nada más.** No trae hojas de estilo externas, ni bibliotecas de interfaz, ni ninguna referencia a una red de distribución: es la misma propiedad que `PT-03` mide sobre el producto, ejercida acá desde el sample.

## 6. Qué esperar

```
[1] Instancia creada: identificador presente | escena viva | piezas dibujadas: 0
[2] Texto de E-1 cargado: piezas dibujadas=3 | no dibujadas=0
[3] Piezas por tipo: Cilindro=1 Cubo=1 Ortoedro=1
[4] Estructura del texto devuelta para el arbol: piezas=3
[5] Segundo procesado del mismo texto: disposicion identica pieza por pieza=si
[6] Instancia destruida: recursos graficos liberados | bucle de dibujo cortado
[7] Uso posterior del identificador liberado: INSTANCIA_DESCONOCIDA
[8] Peticiones de red originadas por el archivo de guion durante todo el recorrido: 0
Funciones ejercidas: 3 de 6 | Servicios del backend disponibles: 0 | Excepciones: 0
```

**La línea `[3]` es el caso insignia del producto.** `Ortoedro=1` significa que el ortoedro **se dibuja**: en el visualizador previo ningún ortoedro generado por la aplicación de los alumnos se dibujaba, y recuperarlo es lo que `PT-02` mide con este mismo escenario.

**La línea `[1]` dice `piezas dibujadas: 0` a propósito.** `inicializar` garantiza que la instancia **no dibuja ninguna pieza** hasta que se invoque `cargarJson` (`Definicion-Contrato-De-Fachada.md` §4.1). Una escena que naciera con contenido sería deriva.

**La línea `[5]` es la garantía `G-6`**, y compara **posición** y no orientación: el determinismo comprometido es el de la posición derivada del índice.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Superficie de dibujo de tamaño nulo | Poner la superficie en tamaño cero antes de crear | `ELEMENTO_DE_DIBUJO_INVALIDO` en su curso **C-1**, y **no se crea instancia** |
| Dos instancias en la misma página | Agregar una segunda superficie y crear otra instancia | Las dos quedan aisladas: no comparten escena, ni selección, ni disposición (`G-4`) |
| Texto del que no se obtienen piezas | Reemplazar `E1.txt` por un texto sin conjunto de piezas | `TEXTO_NO_LEGIBLE`, y la instancia queda **viva y vacía**: `G-7` |
| Diez recorridos de ida y vuelta | Crear y destruir la instancia diez veces seguidas | Sin acumulación de recursos gráficos. Es el tramo de `PT-02` que el ejemplo 03 mide entero |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-12001`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12001-Inicializar-Instancia-Del-Visor.md) | Caso de uso | Acto `[1]`, y las dos primeras variaciones |
| [`CU-12002`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12002-Cargar-El-Texto-Del-Trabajo-Y-Dibujar.md) | Caso de uso | Actos `[2]` a `[5]` |
| [`CU-12005`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12005-Destruir-La-Instancia-Y-Liberar-Recursos.md) | Caso de uso | Actos `[6]` y `[7]` |
| Garantía `G-1` · Cero red | Garantía del contrato de fachada | Acto `[8]`. La medición en su **peor caso** es del ejemplo 03 |
| Garantía `G-5` · Sin fallo silencioso | Garantía del contrato de fachada | `no dibujadas=0` en `[2]`: `E-1` no tiene piezas que queden fuera |
| Garantía `G-6` · Determinismo | Garantía del contrato de fachada | Acto `[5]` |
| Garantía `G-7` · Terminación controlada | Garantía del contrato de fachada | Acto `[7]` y la tercera variación |
| [`ADR-12001`](../05-Arquitectura-Tecnica/Adrs/ADR-12001-Tres-Capas-Con-Fachada-Plana.md) | Decisión arquitectónica | `anfitrion.<ext>` invoca sólo la fachada y no conoce el interior |
| [`ADR-12005`](../05-Arquitectura-Tecnica/Adrs/ADR-12005-Disposicion-Determinista-Derivada-Del-Indice.md) | Decisión arquitectónica | Acto `[5]` |
| **RA-02** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Acto `[8]` y el recuento de servicios del backend disponibles en 0 |
| `PRODUCT-INTAKE` §18 sample **S-1** | Estrategia de demostración | Este sample es la primera de sus tres partes |
| `PRODUCT-INTAKE` §20.E-1 | Escenario con payload real | `datos/E1.txt`, transcripto sin modificación |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-12001
  verifica: [CU-12001, CU-12002, CU-12005, US-12001, US-12004, US-12007, US-12008, US-12011]
  comando: "bash scripts/build-visor.sh && npm --prefix samples/visor/01-basico run verify"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Navegador con capacidad gráfica tridimensional disponible para el conductor"
    - "Conductor de navegador capaz de contar peticiones de red"
    - "0 servicios del backend levantados: el sample no usa ninguno"
    - "Sin acceso a redes de distribución externas: el motor de dibujo va dentro del archivo de guion"
    - "datos/E1.txt transcripto del PRODUCT-INTAKE §20.E-1 sin modificación"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[2] Texto de E-1 cargado: piezas dibujadas=3 | no dibujadas=0"
      - "[3] Piezas por tipo: Cilindro=1 Cubo=1 Ortoedro=1"
      - "[5] Segundo procesado del mismo texto: disposicion identica pieza por pieza=si"
      - "[7] Uso posterior del identificador liberado: INSTANCIA_DESCONOCIDA"
      - "[8] Peticiones de red originadas por el archivo de guion durante todo el recorrido: 0"
      - "Funciones ejercidas: 3 de 6 | Servicios del backend disponibles: 0 | Excepciones: 0"
  evidencia:
    estado: "No verificado — sin código"
```

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño** de `Rules-Examples.md` §0.2. Primera de las tres partes del sample **S-1** del `PRODUCT-INTAKE` §18. Cubre `CU-12001`, `CU-12002` y `CU-12005` con **tres** de las **seis** funciones de la fachada, sobre el escenario `E-1` transcripto sin modificación. El contrato `VER-12001` declara seis líneas exactas de salida, con el recuento de peticiones en **0** y el ortoedro dibujado; `evidencia` queda en `No verificado — sin código`. |
