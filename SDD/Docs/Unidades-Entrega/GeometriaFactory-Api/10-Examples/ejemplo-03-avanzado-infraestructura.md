# Ejemplo 03 — Los mecanismos que no guardan nada: credencial, provisoria, acceso firmado, reloj y arranque

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** ejemplo-03-avanzado.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-30
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/infrastructure/03-avanzado/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-06006` a `CU-06010`; [`../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md), [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) y [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md); [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §2.4, la regla de detenerse en lugar de cumplir a medias
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-06003` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar los cinco mecanismos que esta capa provee y que **ninguna otra puede proveer**: derivar una contraseña y verificar una credencial sin guardar ni registrar nada en claro; producir la contraseña provisoria de la habilitación y del reseteo, no adivinable y sin repetirse; emitir el acceso firmado con sus **cuatro** reclamos; dar el sello del reloj **por un puerto**, para que las pruebas lo puedan fijar; y dejar el almacén en condiciones antes de la primera petición. Al terminar, quien lo ejecuta sabe **por qué cada uno de estos mecanismos se detiene en lugar de cumplir a medias**, y por qué ninguno cae hacia un sustituto.

## 2. Nivel

**Avanzado.** Supone hechos los ejemplos 01 y 02. Agrega los **dos** componentes que faltaban ejercer —el mecanismo de credenciales y el de acceso firmado y preparación del almacén— y es el único de los tres que toca material impredecible del sistema y linaje de transformaciones.

## 3. Prerequisites

Los mismos ítems de [`ejemplo-02-intermedio.md`](ejemplo-02-intermedio-api.md) §3: **.NET 10**, entorno de desarrollo contenido del repositorio, etapa `a` cerrada, Linux y un almacén en su estado de primer arranque.

**Y dos prerequisitos propios, los dos provistos por el entorno y nunca escritos en el sample:**

- **Una clave de firma de prueba**, tomada de configuración del entorno. **El sample no contiene ninguna clave, ni de prueba ni de ninguna otra clase**, y una de sus verificaciones es exactamente ésa.
- **La fuente de material impredecible del sistema**, disponible. Es lo que la producción de la contraseña provisoria necesita, y el sample recorre también el caso en que no responde.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `dotnet run --project samples/infrastructure/03-avanzado`.
5. Comparar la salida con §6.

## 5. Estructura del código

```
samples/infrastructure/03-avanzado/
├── README.md                              # Copia corta de §1, §3 y §4 de este documento
├── Program.<ext>                          # Recorre los cinco actos en orden
├── Actos/
│   ├── ActoDerivarYVerificar.<ext>        # CU-06006
│   ├── ActoProducirProvisoria.<ext>       # CU-06007
│   ├── ActoEmitirAcceso.<ext>             # CU-06008
│   ├── ActoReloj.<ext>                    # CU-06009
│   └── ActoPrepararAlmacen.<ext>          # CU-06010
├── Inspecciones/
│   ├── SinSecretosEnLaFuente.<ext>        # Recuento con umbral 0 sobre el árbol del sample
│   └── SinClaroEnLaTraza.<ext>            # Recuento con umbral 0 sobre la salida producida
└── tests/
    └── SalidaEsperada.<ext>               # Compara la salida contra el snapshot de §6
```

**Las dos inspecciones no son adorno y su umbral es exactamente cero.** Esta capa es **la que conoce** el valor derivado de una credencial, la clave de firma y la ruta del almacén, y por eso la prohibición de exponerlos no es una recomendación de estilo: es la única forma de que la regla de exposición del contrato del producto siga siendo cierta. Un umbral cero sin condición de medición sería un criterio mal escrito, y por eso las dos inspecciones declaran **sobre qué** cuentan: la primera sobre el árbol de fuentes del sample, la segunda sobre la salida que produce.

## 6. Qué esperar

```
[1] Derivacion de contrasena: valor derivado producido | contrasena en claro guardada: no
[1] Verificacion con la credencial correcta: verdadera
[1] Verificacion con la credencial incorrecta: falsa
[1] Verificacion contra un derivado ilegible: UNREADABLE_PASSWORD_HASH (distinto de falsa)
[1] Derivacion sin contrasena en claro: nulo, sin codigo tipado
[2] Provisorias producidas: 100 | repetidas: 0 | derivadas de un dato de la cuenta: no
[2] Produccion sin fuente de aleatoriedad: no provocable desde el sample | caminos alternativos en la fuente del componente: 0
[3] Acceso emitido: reclamos presentes=4 | verificacion del acceso propio: valida
[3] Acceso con firma ajena: invalido | Acceso vencido: invalido
[3] Emision sin clave de firma: nulo, sin codigo tipado | accesos emitidos: 0
[3] Emision con reclamos incompletos: nulo, sin codigo tipado | distinguible de la anterior: no
[4] Sello del reloj por el puerto: obtenido | dos corridas con el puerto fijado: sello identico
[5] Preparacion del almacen: transformaciones aplicadas | linaje registrado
[5] Segunda preparacion sobre el mismo almacen: sin transformaciones nuevas
[5] Preparacion sobre un almacen con linaje desconocido: arranque detenido InvalidOperationException, sin codigo tipado
[insp] Ocurrencias de clave de firma, contrasena real o ruta del almacen en la fuente del sample: 0
[insp] Ocurrencias de contrasena en claro o de valor derivado en la salida producida: 0
Actos recorridos: 5 | Rechazos tipados: 1 | Excepciones: 0
```

**Cinco líneas cambiaron en la versión 2.0, y las cinco dicen lo mismo: esta capa señala con `null` y no con código tipado.** Declara **dos** códigos —`UNREADABLE_PASSWORD_HASH` y `RANDOMNESS_SOURCE_UNAVAILABLE`— y este §6 le pedía **seis**. Los cuatro que faltaban **no existen en el árbol** y nunca existieron: `PLAINTEXT_PASSWORD_MISSING`, `SIGNING_KEY_MISSING`, `INCOMPLETE_CLAIMS` y `MIGRATION_NOT_APPLICABLE`.

**No se agregan, y el motivo es que no tienen consumidor.** La capa señala con nulo de forma consistente y sus consumidores lo manejan así; cuatro códigos que nadie lee serían trabajo sin destinatario. **La conducta que este documento describía se cumple entera en los cuatro casos**: sin contraseña no se deriva nada, sin clave de firma no se emite ningún acceso, con reclamos incompletos tampoco, y el arranque se detiene ante un linaje que no entiende.

**Con una excepción que sí se corrigió, y no acá.** `SIGNING_KEY_MISSING` describía una condición que **la aplicación compuesta no puede alcanzar**: `CompositionRoot` detiene el arranque si la clave falta o es más corta que el mínimo. El sample la alcanza porque construye el emisor **directamente**, que es legítimo para un sample de capa — pero lo que mide ahí es una **rama defensiva del componente** y no una condición del producto.

**`RANDOMNESS_SOURCE_UNAVAILABLE` sí existe, y su línea cambió por otro motivo.** La condición **no es provocable desde un sample**: la fuente es el generador criptográfico del sistema operativo, y fabricar su falla con un doble mediría el doble. Lo que el sample mide en su lugar es **la mitad que importa** —que no haya un segundo camino por el que la provisoria se componga igual— y da cero.

**La línea de `[5]` mejoró el 2026-08-30 aunque siga sin código.** Antes el arranque se detenía dejando escapar la excepción cruda del proveedor, con su traza y un mensaje sobre una tabla que ya existe. Hoy **se detiene por decisión propia**, con un mensaje que nombra la causa —el linaje no corresponde— y sin traza; el detalle queda en el registro.

**La cuarta línea de `[1]` es la que más se confunde.** Un derivado **ilegible** no es lo mismo que una credencial **incorrecta**: la segunda es una respuesta legítima del mecanismo y la primera es un almacén en el que no se puede confiar. Colapsarlas haría que un dato corrupto se leyera como «contraseña equivocada» y nadie se enteraría nunca.

**La segunda línea de `[2]` es la regla de detenerse dicha con números.** Sin fuente de material impredecible, los valores producidos son **0**: el mecanismo no compone la provisoria por otro medio, no cae hacia un sustituto y no la deriva de un dato de la cuenta. Es la regla que gobierna las **17** condiciones de [`../03-UX-UI-DX/DX-Error-Messages.md`](../03-UX-UI-DX/DX-Error-Messages.md) §2.4.

**La línea `[4]` con el sello idéntico entre dos corridas es lo que hace reproducible a todo el resto del producto.** El reloj llega **por un puerto** para que las pruebas lo puedan fijar; si esta capa leyera el reloj del sistema directamente, ningún sample de ningún proyecto de código podría tener un criterio de aceptación comparable.

**Las tres líneas de `[5]` son el arranque en su forma completa.** La primera aplica y registra; la segunda no vuelve a aplicar nada, porque el linaje es inmutable; y la tercera **detiene el arranque** en lugar de operar sobre un almacén dudoso. Atender peticiones sobre un almacén que no se entiende es peor que no atender ninguna.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Ruta del almacén ausente | Quitar la ruta de la configuración del entorno | `STORE_PATH_UNAVAILABLE`, y **el mensaje no nombra ninguna ruta**: es una dirección de servicio interno |
| Provisoria derivada de la cuenta | Producir la provisoria a partir del correo | La línea `derivadas de un dato de la cuenta` pasa a `si` y el criterio de aceptación falla. Es lo que [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) prohíbe |
| Parámetros de derivación sin anclar | Cambiar los parámetros de derivación sin versionarlos | Las credenciales derivadas antes dejan de verificar, y nada lo declara. Es lo que [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) viene a impedir |
| Reloj del sistema en vez del puerto | Leer el reloj directamente en un acto | La línea `sello identico` pasa a `no` y el sample deja de ser comparable entre corridas |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-06006`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06006-Derivar-La-Contrasena-Y-Verificar-Una-Credencial.md) | Caso de uso | Deriva sin guardar en claro y distingue el veredicto falso del derivado ilegible |
| [`CU-06007`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06007-Producir-La-Contrasena-Provisoria-Del-Reseteo.md) | Caso de uso | Produce **100** provisorias con **0** repetidas y se detiene cuando la aleatoriedad no responde |
| [`CU-06008`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06008-Emitir-El-Acceso-Firmado.md) | Caso de uso | Emite con sus **cuatro** reclamos y rechaza sin clave de firma y con reclamos incompletos |
| [`CU-06009`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06009-Proveer-El-Sello-Del-Reloj-Del-Sistema.md) | Caso de uso | Provee el sello por el puerto, con dos corridas que dan el mismo valor al fijarlo |
| [`CU-06010`](../05-Arquitectura-Tecnica/Operaciones-Internas/CU-06010-Preparar-El-Almacen-Al-Arrancar.md) | Caso de uso | Aplica las transformaciones, registra el linaje y detiene el arranque ante un linaje desconocido |
| [`RN-02014`](../02-Especificacion-Funcional/Reglas-De-Negocio/RN-02014-Provisoria-Producida-Por-El-Sistema.md) | Regla de negocio | La provisoria la produce el sistema, no adivinable y sin repetirse |
| [`ADR-06004`](../05-Arquitectura-Tecnica/Adrs/ADR-06004-Derivacion-De-Clave-Anclada-Con-Parametros-Versionados.md) | Decisión arquitectónica | La derivación anclada con parámetros versionados |
| [`ADR-06005`](../05-Arquitectura-Tecnica/Adrs/ADR-06005-Contrasena-Provisoria-No-Adivinable-Y-Sin-Repetirse.md) | Decisión arquitectónica | Las **100** provisorias sin repetición y sin derivar de un dato de la cuenta |
| [`ADR-06007`](../05-Arquitectura-Tecnica/Adrs/ADR-06007-Transformaciones-Al-Arrancar-Con-Linaje-Inmutable.md) | Decisión arquitectónica | El linaje inmutable y el arranque detenido |
| `RA-03` | Regla de arquitectura del producto | Las dos inspecciones con umbral **0**: ninguna clave, contraseña ni ruta del almacén en la fuente del sample ni en su salida |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-06003
  verifica: [CU-06006, CU-06007, CU-06008, CU-06009, CU-06010, US-06017, US-06018, US-06019, US-06020, US-06021, US-06022, US-06023, US-06024, US-06025]
  comando: "dotnet run --project samples/infrastructure/03-avanzado"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Etapa `a` del plan de entrega cerrada"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén queda en su estado de primer arranque"
    - "Clave de firma de prueba y ruta del almacén provistas por configuración del entorno; ninguna de las dos escrita en el sample"
    - "Fuente de material impredecible del sistema disponible, salvo en el acto que verifica su ausencia"
    - "Las dos inspecciones de umbral cero se miden sobre el árbol de fuentes del sample y sobre la salida producida, y no sobre el resto del repositorio"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Derivacion de contrasena: valor derivado producido | contrasena en claro guardada: no"
      - "[1] Verificacion contra un derivado ilegible: UNREADABLE_PASSWORD_HASH (distinto de falsa)"
      - "[2] Provisorias producidas: 100 | repetidas: 0 | derivadas de un dato de la cuenta: no"
      - "[2] Produccion sin fuente de aleatoriedad: no provocable desde el sample | caminos alternativos en la fuente del componente: 0"
      - "[3] Acceso emitido: reclamos presentes=4 | verificacion del acceso propio: valida"
      - "[3] Emision sin clave de firma: nulo, sin codigo tipado | accesos emitidos: 0"
      - "[4] Sello del reloj por el puerto: obtenido | dos corridas con el puerto fijado: sello identico"
      - "[5] Preparacion sobre un almacen con linaje desconocido: arranque detenido InvalidOperationException, sin codigo tipado"
      - "[insp] Ocurrencias de clave de firma, contrasena real o ruta del almacen en la fuente del sample: 0"
      - "[insp] Ocurrencias de contrasena en claro o de valor derivado en la salida producida: 0"
      - "Actos recorridos: 5 | Rechazos tipados: 1 | Excepciones: 0"
    stdout_no_contiene:
      - "contrasena en claro guardada: si"
      - "repetidas: 1"
      - "derivadas de un dato de la cuenta: si"
      - "valores producidos: 1"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye cuatro aserciones negativas.** Los cuatro son los modos de falla **silenciosos** de esta capa: una contraseña guardada en claro, una provisoria repetida, una provisoria derivada de un dato de la cuenta y un valor producido pese a que la fuente de aleatoriedad no respondió. Los cuatro dejarían el sistema aparentemente funcionando, y ninguna aserción positiva de la lista los detectaría.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3d` del renombre `F-03`, que lo cierra.** **1 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni **la prosa que narra el renombre** —una línea que trae la forma vieja y su par vigente está reportando, no usando—. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-06006` a `CU-06010`, los cinco mecanismos que sólo esta capa provee, con **dos** inspecciones de umbral **cero** y su condición de medición declarada. El contrato `VER-06003` declara once líneas exactas de salida y **cuatro aserciones negativas**, una por cada modo de falla silencioso de la capa; `evidencia` queda en `No verificado — sin código`. |
| 2.0 | 2026-08-30 | **§6 se alinea con lo que la capa hace.** Cinco líneas pedían códigos tipados que **no existen en el árbol** —`PLAINTEXT_PASSWORD_MISSING`, `SIGNING_KEY_MISSING`, `INCOMPLETE_CLAIMS` y `MIGRATION_NOT_APPLICABLE`— y una sexta pedía uno que existe pero cuya condición **no es provocable** desde un sample. Esta capa declara **dos** códigos y señala el resto con `null`. **No se agregan cuatro códigos sin consumidor**: la conducta que este documento describía se cumple entera en los cuatro casos, y lo que faltaba era el nombre. Se deja constancia de que `SIGNING_KEY_MISSING` describía una condición que **la aplicación compuesta no puede alcanzar** —`CompositionRoot` detiene el arranque sin clave— y que el sample la alcanza construyendo el emisor directamente. La línea de `[5]` mejoró en la misma tanda: el arranque **se detiene por decisión propia**, con un mensaje que nombra la causa y sin traza. Sube **major**: cinco líneas del snapshot cambian de contenido. |
