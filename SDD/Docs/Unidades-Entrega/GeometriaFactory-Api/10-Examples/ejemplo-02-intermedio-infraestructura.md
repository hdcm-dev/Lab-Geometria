# Ejemplo 02 — El almacén: guardar, recuperar con el recorte ya decidido, retirar y arrastrar

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-02-intermedio.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Intermedio
**Ubicación del código:** `/samples/infrastructure/02-intermedio/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-06003`, `CU-06004` y `CU-06005`; [`../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md`](../05-Arquitectura-Tecnica/Modelo-Datos-Logico.md), el esquema físico de las **cinco** entidades; [`../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) y [`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md); `PRODUCT-INTAKE` 1.23 §20, escenarios `E-1`, `E-2` y `E-5`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-06002` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar la otra mitad de esta capa: la que **sí abre el almacén**. Materializar un trabajo con sus piezas, sus componentes y sus observaciones; resolver la consulta **con el recorte ya trasladado al pedido**, y no filtrando después en memoria; devolver el listado **sin componentes ni texto original** frente al detalle que sí los lleva; retirar físicamente un trabajo con todo lo que cuelga de él; y arrastrar todos los trabajos de una cuenta dada de baja, todo o nada. Al terminar, quien lo ejecuta sabe **por qué el alcance de una consulta llega decidido de afuera** y qué pasa cuando no llega.

## 2. Nivel

**Intermedio.** Supone hecho el ejemplo 01: ya se sabe qué produce el validador. Agrega el **contexto de persistencia y mapeo** y los **dos** adaptadores de repositorio, que son los tres componentes que el 01 no tocaba, y es el primero que necesita un almacén.

## 3. Prerequisites

Los mismos cuatro ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico-api.md) §3: **.NET 10**, entorno de desarrollo contenido del repositorio, etapa `a` cerrada y Linux.

**Y uno propio: un almacén en su estado de primer arranque.** El sample lo obtiene con el guion de reinicio del repositorio y aplica las transformaciones de esquema antes de escribir nada. **La ruta del almacén llega de configuración y no está escrita en el sample**, porque la ruta del archivo de datos es una dirección de servicio interno a los efectos de `RA-03`, y así la trata la capa que la conoce.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/02-intermedio`.
5. Comparar la salida con §6.

## 5. Estructura del código

```
samples/infrastructure/02-intermedio/
├── README.md                            # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                        # Recorre los cinco actos en orden
├── Escenarios/
│   ├── E1.txt  E2.txt  E5.txt           # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── Interpretaciones.<ext>           # Resultados del ejemplo 01, reusados sin recalcular
├── Actos/
│   ├── ActoMaterializar.<ext>           # CU-06003, escritura
│   ├── ActoConsultar.<ext>              # CU-06003, las dos formas de lectura
│   ├── ActoRetirar.<ext>                # CU-06004
│   ├── ActoArrastrar.<ext>              # CU-06004, la baja de una cuenta
│   └── ActoCuentas.<ext>                # CU-06005, incluidas las dos preguntas sobre el conjunto
└── tests/
    └── SalidaEsperada.<ext>             # Compara la salida contra el snapshot de §6
```

**`Interpretaciones.<ext>` no recalcula nada.** Toma el resultado que el ejemplo 01 produjo sobre los mismos tres textos. Es deliberado: este sample verifica **qué se guarda y cómo se recupera**, no qué produce el validador, y recalcularlo acá mezclaría dos verificaciones que la partición de `05` §2 punto 2 separa a propósito.

## 6. Qué esperar

```
[1] Trabajo de E-1 materializado: piezas=3 componentes=15 observaciones=2 | texto original: guardado literal
[1] Trabajo de E-2 materializado: piezas=1 componentes=6 observaciones=1
[1] Trabajo de E-5 materializado: piezas=1 observaciones=1
[2] Consulta de listado: 3 trabajos | componentes en el resultado: 0 | texto original en el resultado: no
[2] Consulta de detalle: 1 trabajo | piezas y componentes presentes: si | texto original presente: si
[2] Consulta sin alcance declarado: rechazada CONSULTA_SIN_ALCANCE_DECLARADO
[3] Retiro de un trabajo: retirado | piezas, componentes y observaciones que quedaron: 0
[4] Baja de la cuenta con 2 trabajos: arrastre aplicado | trabajos que quedaron de esa cuenta: 0
[4] Arrastre interrumpido a la mitad: RETIRO_PARCIAL_NO_ADMITIDO | trabajos que quedaron: 2
[5] Alta con un correo ya registrado: rechazada CORREO_YA_REGISTRADO
[5] Segunda cuenta con papel Administrador: rechazada UNICIDAD_DE_ADMINISTRADOR_VIOLADA
[5] Cuenta recuperada con su marca de cambio pendiente: si | estado sin alterar: si
[6] Escritura que reemplaza el texto original: rechazada ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL
Actos recorridos: 5 | Rechazos tipados: 5 | Excepciones: 0
```

**La línea `[2]` de la consulta sin alcance es la razón de ser de este sample.** El adaptador **no filtra después**: exige que el recorte venga en el pedido. Un repositorio genérico que devolviera todo y dejara el recorte del lado del consumidor es exactamente lo que esa condición viene a impedir, y es la alternativa que `05` §2.1 descarta con ese fundamento.

**Las dos líneas de `[4]` son la unidad de trabajo vista desde afuera.** Cuando el arrastre se completa quedan **0** trabajos de esa cuenta; cuando se interrumpe quedan **2**, los mismos que había. No hay estado intermedio observable: es el todo o nada que [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) fija, con la baja con arrastre como caso testigo.

**La diferencia entre las dos líneas de `[2]` es contrato y no optimización.** El listado no lleva componentes ni texto original; el detalle sí. Pedir componentes en un listado obligaría a traer el detalle de cada fila, y el contrato del producto ya separó las dos proyecciones antes de que esta capa existiera.

**La última línea es `RN-06008` medida donde se puede violar.** El texto original se guarda literal y **ninguna escritura lo reemplaza**: la que lo intenta se rechaza, en lugar de aplicarse y dejar el defecto para que lo descubra alguien mirando una pantalla.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Dos escritores a la vez | Lanzar dos operaciones de escritura simultáneas | `ESCRITURA_CONCURRENTE_RECHAZADA`. **Esta capa no reintenta**: la decisión de reintentar es del consumidor, y `05` §2.1 declara por qué |
| Almacén ausente | Correr sin ejecutar el paso 3 | `ALMACEN_NO_DISPONIBLE`, y **no** un valor compuesto por otro medio |
| Materializar sin texto original | Quitar el texto del trabajo antes de guardarlo | `TEXTO_ORIGINAL_AUSENTE`: el trabajo no se guarda a medias |
| Correos que difieren sólo en mayúsculas | Dar de alta dos cuentas con el mismo correo escrito distinto | Depende del criterio de comparación, que **no está decidido todavía** ([`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) §6). El sample lo declara como variación y **no afirma un resultado** |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-06003`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06003-Guardar-Y-Recuperar-Los-Trabajos.md) | Caso de uso | Materializa los tres trabajos y resuelve las **dos** formas de lectura con el recorte ya trasladado |
| [`CU-06004`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06004-Ejecutar-El-Borrado-Fisico-Y-El-Arrastre-De-La-Baja.md) | Caso de uso | Retira un trabajo con todo lo que cuelga y arrastra los **2** restantes de una cuenta dada de baja |
| [`CU-06005`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06005-Guardar-Y-Recuperar-Las-Cuentas-De-La-Comision.md) | Caso de uso | Responde las **dos** preguntas sobre el conjunto y transporta la marca sin alterar el estado |
| [`RN-02002`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md) | Regla de negocio | La unicidad del correo la sostiene el almacén, no el consumidor |
| [`RN-02001`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md) | Regla de negocio | La unicidad del administrador la sostiene el almacén |
| [`RN-02007`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02007-Baja-Con-Arrastre-Y-Confirmacion-Escrita.md) | Regla de negocio | El arrastre es todo o nada |
| [`RN-02008`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02008-Texto-Original-Conservado-Integro.md) | Regla de negocio | El texto se guarda literal y toda escritura que lo reemplace se rechaza |
| [`ADR-06001`](../05-Arquitectura-Tecnica/Adrs/ADR-06001-Adaptadores-Por-Puerto-Sin-Repositorio-Generico.md) | Decisión arquitectónica | Un adaptador por puerto, sin repositorio genérico: la consulta sin alcance se rechaza en lugar de resolverse afuera |
| [`ADR-06002`](../05-Arquitectura-Tecnica/Adrs/ADR-06002-Un-Archivo-Escritor-Unico-Y-Una-Unidad-De-Trabajo-Por-Operacion.md) | Decisión arquitectónica | Una unidad de trabajo por operación y escritor único, con el arrastre como caso testigo |
| [`ADR-06003`](../05-Arquitectura-Tecnica/Adrs/ADR-06003-Comparacion-De-Correos-Y-El-Indice-Que-La-Sostiene.md) | Decisión arquitectónica | La comparación de correos y su índice, con el punto que sigue abierto declarado en §7 |
| `PRODUCT-INTAKE` §20 `E-1`, `E-2`, `E-5` | Escenario con payload real | Los tres textos que se materializan, transcriptos sin modificación |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-06002
  verifica: [CU-06003, CU-06004, CU-06005, US-06008, US-06009, US-06010, US-06011, US-06012, US-06013, US-06014, US-06015, US-06016]
  comando: "dotnet run --project samples/infrastructure/02-intermedio"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén queda en su estado de primer arranque"
    - "La ruta del almacén provista por configuración del entorno; ninguna ruta escrita en el sample"
    - "Los tres textos de Escenarios/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Trabajo de E-1 materializado: piezas=3 componentes=15 observaciones=2 | texto original: guardado literal"
      - "[2] Consulta de listado: 3 trabajos | componentes en el resultado: 0 | texto original en el resultado: no"
      - "[2] Consulta sin alcance declarado: rechazada CONSULTA_SIN_ALCANCE_DECLARADO"
      - "[3] Retiro de un trabajo: retirado | piezas, componentes y observaciones que quedaron: 0"
      - "[4] Baja de la cuenta con 2 trabajos: arrastre aplicado | trabajos que quedaron de esa cuenta: 0"
      - "[4] Arrastre interrumpido a la mitad: RETIRO_PARCIAL_NO_ADMITIDO | trabajos que quedaron: 2"
      - "[6] Escritura que reemplaza el texto original: rechazada ESCRITURA_QUE_REESCRIBE_EL_TEXTO_ORIGINAL"
      - "Actos recorridos: 5 | Rechazos tipados: 5 | Excepciones: 0"
    stdout_no_contiene:
      - "componentes en el resultado: 15"
      - "trabajos que quedaron: 1"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye tres aserciones negativas.** Que el listado traiga los **15** componentes es el defecto que la separación de proyecciones existe para impedir, y una implementación que los trajera **pasaría** la aserción de «3 trabajos». Y un arrastre que dejara **1** trabajo sería un estado intermedio observable: es la negación exacta del todo o nada, y ninguna aserción positiva sobre el caso exitoso lo detectaría.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-06003`, `CU-06004` y `CU-06005` sobre un almacén real llevado a su estado de primer arranque, con **tres** de los ocho escenarios materializados y sus interpretaciones reusadas del ejemplo 01 sin recalcular. Declara por qué la ruta del almacén no se escribe en el sample y por qué la variación de comparación de correos **no afirma un resultado**, dado que `ADR-06003` §6 la deja abierta. El contrato `VER-06002` declara ocho líneas exactas de salida y **tres aserciones negativas** —los componentes en el listado, el estado intermedio del arrastre y el trabajo que no se retiró—; `evidencia` queda en `No verificado — sin código`. |
