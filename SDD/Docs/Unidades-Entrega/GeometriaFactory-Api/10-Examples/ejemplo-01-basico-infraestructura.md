# Ejemplo 01 — Leer el texto del alumno y verificar sus números, sin abrir el almacén

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-01-basico.md
**Versión:** 1.1
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/infrastructure/01-basico/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) y [`CU-06002`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md); [`../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md`](../02-Especificacion-Funcional/Definicion-Contrato-Del-Validador-De-Figuras.md); [`../05-Arquitectura-Tecnica/Flujo-Ejecucion.md`](../05-Arquitectura-Tecnica/Flujo-Ejecucion.md) §5, la tabla de derivación por tipo; `PRODUCT-INTAKE` 1.23 §20, los **ocho** escenarios, y §21
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-06001` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar la mitad de esta capa que **no toca el almacén**: leer el texto que el programa del alumno emite de verdad, con las **cuatro** tolerancias `T1` a `T4`; reconstruir las piezas con su posición y sus componentes; derivar `Area` y `Volumen` y compararlos con los declarados con tolerancia **0.01** y operador **estricto**; y emitir la observación con lo que hace falta para ubicarla. Al terminar, quien lo ejecuta sabe **por qué un número mal calculado advierte y un tipo desconocido no**, y por qué esta parte de la biblioteca se puede correr sin base de datos.

## 2. Nivel

**Básico.** Punto de entrada absoluto del proyecto de código. No supone ningún otro sample, no abre el archivo de datos y no necesita ninguna preparación previa: es la partición que [`../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md`](../05-Arquitectura-Tecnica/Arquitectura-Unidad-Entrega.md) §2 punto 2 declara —«la mitad de esta capa no toca el almacén»— vista como sample.

## 3. Prerequisites

- **.NET 10**, la plataforma que el `PRODUCT-INTAKE` declara para los seis proyectos de código de la solución de código (encabezado de la Parte C).
- **Entorno de desarrollo contenido del repositorio.** Todo el ciclo ocurre adentro, porque el host no tiene el SDK.
- **Etapa `a` del plan de entrega cerrada.**
- **Linux**, que es la plataforma del entorno contenido.

**Sin almacén, sin red y sin ninguna otra pieza del producto.** El sample instancia el motor de interpretación y el de verificación de valores, y nada más.

**Un prerequisito de datos, no de herramientas:** los ocho textos se transcriben del `PRODUCT-INTAKE` §20 **sin modificación**. Son datos reales emitidos por la aplicación de escritorio de los alumnos y por los dos ejemplos de la cátedra, y no se sustituyen por datos sintéticos.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Ejecutar el sample: `dotnet run --project samples/infrastructure/01-basico`.
4. Comparar la salida con §6.

## 5. Estructura del código

```
samples/infrastructure/01-basico/
├── README.md                          # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                      # Recorre los ocho escenarios en orden E-1 a E-8
├── Escenarios/
│   ├── E1.txt  E2.txt  E3.txt  E4.txt # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── E5.txt  E6.txt  E7.txt  E8.txt #   E2.txt NO es JSON estrictamente válido: es su gracia
└── tests/
    └── SalidaEsperada.<ext>           # Compara la salida contra el snapshot de §6
```

**Los archivos de escenario llevan extensión `.txt` y no `.json`, a propósito.** El de `E-2` **no es JSON estrictamente válido** —tiene dos comas finales—, y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo. Reformatearlo borraría la tolerancia `T2`, que es precisamente lo que ese escenario existe para ejercitar: el sample dejaría de probar lo único que ese texto prueba y seguiría pasando. Es el mismo criterio que ya adoptaron las categorías 10 de `GeometriaFactory-Contracts` y de `GeometriaFactory-Visor`.

**No hay carpeta `Dobles/` y no es un olvido.** Este sample no necesita ninguno: los dos componentes que ejercita no dependen de nada del producto salvo el dominio.

## 6. Qué esperar

```
[E-1] Figuras del conjunto raiz: 3 | Piezas reconstruidas: 3 | Observaciones: 2
[E-1] Cilindro: 2 tapas Circulo y 1 lado RectanguloDesarrollado | Observaciones: 0 (tolerancia estricta)
[E-1] Cubo: advertencia de area declarado=36.00 derivado=54.00
[E-1] Ortoedro: advertencia de volumen declarado=343.00 derivado=1029.00 | area sin observacion
[E-2] Parseo con comas finales: exitoso (T2) | Clave Tapas leida como bases (T1)
[E-2] Estructura: 1 pieza, 2 bases, 4 laterales | Observaciones: 1 advertencia de volumen
[E-3] Caras Cuadrado interpretadas (T3) | advertencia de area declarado=36.00 derivado=54.00
[E-4] Caras Rectangulo interpretadas (T3) | Observaciones: 0
[E-5] Figuras del conjunto raiz: 2 | Piezas reconstruidas: 1
[E-5] Observacion Error: indice-figura=1 campo=Tipo
[E-6] Dimension 0.00: la figura se interpreta y no se descarta | Errores de interpretacion: 0
[E-7] Piezas reconstruidas: 6 | volumetricos=3 planos=3 | Clave Bases leida como bases (T1)
[E-7] Ortoedro: ancho=6.00 profundidad=4.00 altura=8.00
[E-8] Observacion Error: indice-figura=1 campo=Largo
Escenarios recorridos: 8 | Observaciones de error: 2 | Advertencias: 4 | Excepciones: 0
```

**La línea del cilindro de `E-1` con `Observaciones: 0` es la que más fácil se rompe.** El `PRODUCT-INTAKE` §20.E-1 punto 2 declara que el cilindro **no produce ninguna observación**: sus fórmulas son correctas. La diferencia que aparece al derivar no supera la tolerancia de **0.01**, y el operador es **estricto**. Un operador que comparara «mayor o igual» produciría una tercera advertencia acá y el escenario canónico del producto dejaría de dar 3 piezas y 2 advertencias.

**El contraste `E-3` contra `E-4` es el criterio negativo.** Son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra: `E-3` declara área 36.00 y advierte con el par de valores, `E-4` declara 54.00 y produce **cero** observaciones. Un validador que advirtiera siempre pasaría el primero y fallaría el segundo (`PRODUCT-INTAKE` §20.E-4 punto 4).

**Las dos líneas de `E-5` juntas dicen la regla entera.** Las figuras del conjunto raíz son **2** y las piezas reconstruidas **1**: la primera, que es válida, se interpreta igual, y la segunda no se reconstruye pero **su posición se informa**. El índice reportado es **1 y no 0** porque el primer elemento es válido a propósito, y ésa es la forma de comprobar que el índice se calcula.

**`E-6` es existencia contra veracidad.** La dimensión en `0.00` **no descarta la figura**: el validador comprueba que el campo esté, no que su valor tenga sentido geométrico. Descartarla sería aplicar un juicio que ninguna regla pidió, y dejaría al alumno sin ver su propio error.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Tolerancia no estricta | Comparar con «mayor o igual» en lugar de estricto | `E-1` pasa a **3** advertencias y el escenario canónico del producto deja de dar 3 piezas y 2 advertencias |
| Quitar el sinónimo de clave | Leer sólo `Bases` y no `Tapas` | `E-2` deja de reconstruirse: es el defecto que el visor original tiene y que este producto viene a corregir (`T1`) |
| Reformatear `E2.txt` | Abrir el archivo con una herramienta que lo normalice | Las comas finales desaparecen, `T2` deja de ejercitarse y el sample **sigue pasando**. Es lo que la extensión `.txt` viene a evitar |
| Texto que no parsea ni con tolerancia | Reemplazar un escenario por un texto arbitrario | Condición `PIECE_SET_NOT_REBUILT`, que es error de interpretación y no advertencia |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-06001`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06001-Interpretar-El-Texto-Original-Y-Reconstruir-Las-Piezas.md) | Caso de uso | Lee los ocho textos con las cuatro tolerancias y reconstruye las piezas con su posición y sus componentes |
| [`CU-06002`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06002-Verificar-Los-Valores-Declarados-Contra-Los-Derivados.md) | Caso de uso | Deriva y compara con tolerancia **0.01** y operador estricto, y emite la advertencia con el par de valores |
| [`RN-02005`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Regla de negocio | Distingue las **4** advertencias de las **2** observaciones de error, que es lo que después decide el estado |
| [`RN-02009`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Regla de negocio | Índice **1** y campo en `E-5` y en `E-8`, nunca un texto genérico |
| [`ADR-06006`](../05-Arquitectura-Tecnica/Adrs/ADR-06006-Lectura-Tolerante-Y-Tabla-De-Derivacion-Por-Tipo.md) | Decisión arquitectónica | Las cuatro tolerancias y la tabla de derivación por tipo, ejercidas sobre datos reales |
| [`ADR-06001`](../05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Decisión arquitectónica | El validador se instancia solo, sin arrastrar la dependencia de persistencia. Es lo que hace barata a la batería obligatoria |
| `PRODUCT-INTAKE` §20 `E-1` a `E-8` | Escenario con payload real | Los ocho textos se transcriben sin modificación y son la entrada del recorrido |
| `PRODUCT-INTAKE` §21 | Matriz de cobertura | El reparto de las tolerancias `T1` a `T4` entre los escenarios, que §2 del [`README.md`](README.md) de esta carpeta recuenta |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-06001
  verifica: [CU-06001, CU-06002, US-06001, US-06002, US-06003, US-06004, US-06005, US-06006, US-06007]
  comando: "dotnet run --project samples/infrastructure/01-basico"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "Los ocho textos de Escenarios/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
    - "Sin almacén levantado: el sample no abre el archivo de datos"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[E-1] Figuras del conjunto raiz: 3 | Piezas reconstruidas: 3 | Observaciones: 2"
      - "[E-1] Cilindro: 2 tapas Circulo y 1 lado RectanguloDesarrollado | Observaciones: 0 (tolerancia estricta)"
      - "[E-2] Parseo con comas finales: exitoso (T2) | Clave Tapas leida como bases (T1)"
      - "[E-4] Caras Rectangulo interpretadas (T3) | Observaciones: 0"
      - "[E-5] Observacion Error: indice-figura=1 campo=Tipo"
      - "[E-6] Dimension 0.00: la figura se interpreta y no se descarta | Errores de interpretacion: 0"
      - "[E-7] Ortoedro: ancho=6.00 profundidad=4.00 altura=8.00"
      - "Escenarios recorridos: 8 | Observaciones de error: 2 | Advertencias: 4 | Excepciones: 0"
    stdout_no_contiene:
      - "indice-figura=0"
      - "[E-1] Cilindro: 2 tapas Circulo y 1 lado RectanguloDesarrollado | Observaciones: 1"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye dos aserciones negativas.** `indice-figura=0` no debe aparecer: el primer elemento de `E-5` y el de `E-8` son válidos a propósito, y que el índice reportado sea **1** es lo que prueba que la ubicación se calcula. Y el cilindro de `E-1` **no puede** tener una observación: si la tiene, el operador de tolerancia dejó de ser estricto, y ése es un defecto que ninguna aserción positiva de esta lista detectaría por sí sola.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3d` del renombre `F-03`, que lo cierra.** **1 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni **la prosa que narra el renombre** —una línea que trae la forma vieja y su par vigente está reportando, no usando—. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-06001` y `CU-06002` sobre los **ocho** escenarios reales del `PRODUCT-INTAKE` §20 como **texto literal**, transcriptos sin modificación, y ejercita las **cuatro** tolerancias `T1` a `T4`. Declara por qué los archivos llevan extensión `.txt` y por qué el sample no abre el almacén. El contrato `VER-06001` declara ocho líneas exactas de salida y **dos aserciones negativas** —el índice reportado y el cilindro sin observación, que es donde se rompe el operador estricto—; `evidencia` queda en `No verificado — sin código`. |
