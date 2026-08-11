# Ejemplo 03 — Composición de raíz y arranque en dos fases: qué pasa antes de la primera petición

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** ejemplo-03-avanzado.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/api/03-avanzado/`
**Trazabilidad upstream:** [`CU-10`](../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md) y [`CU-11`](../02-Especificacion-Funcional/Casos-De-Uso/CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md); [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md), [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md), [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) y [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md); [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3, punto `A-16`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-03` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar lo que este proyecto de código tiene de propio y ningún otro del producto tiene: **la composición de raíz** —conectar los **cuatro** puertos con sus adaptadores, en un solo lugar y tomando la configuración de afuera— y **el arranque en dos fases**, que deja el almacén en condiciones **antes** de atender la primera petición y **se detiene** en lugar de atender sobre un almacén dudoso. Al terminar, quien lo ejecuta sabe **por qué el punto de salud no exige acceso** y por qué un arranque detenido es mejor noticia que uno que arranca igual.

## 2. Nivel

**Avanzado.** Supone hechos los ejemplos 01 y 02: ya se sabe qué hace la superficie. Éste mira lo que ocurre **antes** de que la superficie exista, ejercita el único punto de acceso que la colección deja afuera de su archivo —`A-16`— y es el único de los tres que arranca el servicio **dos veces**, una sobre un almacén sano y otra sobre uno que no se puede entender.

## 3. Prerequisites

- **Entorno de desarrollo contenido del repositorio.** Todo el ciclo ocurre adentro.
- **El servicio construido y sin advertencias**, con el guion de construcción del repositorio. La puerta de calidad del producto es cero advertencias, y arrastrarlas hace que la siguiente sea invisible.
- **Dos almacenes de partida**: uno llevado a su estado de primer arranque con el guion de reinicio, y **uno con linaje desconocido**, que el propio sample compone y declara como compuesto por él.
- **La configuración provista por el entorno.** El sample no contiene ninguna dirección, ninguna clave de firma y ninguna ruta de almacén.

**El servicio lo arranca el sample, no un paso previo.** Es lo que lo distingue de los otros dos: acá el arranque **es** el objeto de la verificación.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Construir la solución de código: `bash scripts/build.sh`.
3. Reiniciar el almacén: `bash scripts/reset-db.sh`.
4. Ejecutar el sample: `bash samples/api/03-avanzado/run.sh`.
5. Comparar la salida con §6.

## 5. Estructura del código

```
samples/api/03-avanzado/
├── README.md                          # Copia corta de §1, §3 y §4 de este documento
├── run.sh                             # Punto de entrada único: arranca, inspecciona y detiene
├── Actos/
│   ├── ActoArranqueSano.<ext>         # CU-11, fase 1 y fase 2
│   ├── ActoSalud.<ext>                # A-16, sin acceso firmado
│   ├── ActoArranqueDetenido.<ext>     # CU-11, sobre el almacén de linaje desconocido
│   └── ActoInspeccionDeComposicion.<ext>  # CU-10, los cuatro puertos y la configuración
├── almacenes/
│   └── linaje-desconocido.md          # Cómo se compone, y la constancia de que lo compone el sample
└── esperado/
    └── salida.txt                     # Snapshot de la salida de §6
```

**`linaje-desconocido.md` documenta cómo se compone ese almacén y declara que lo compone el sample.** No es una captura de un almacén real ni un archivo traído de ningún lado: es una condición provocada a propósito, y decirlo evita que alguien lo lea como un dato del producto.

## 6. Qué esperar

```
[1] Arranque, fase 1: transformaciones aplicadas sobre el almacen | peticiones atendidas durante la fase 1: 0
[1] Arranque, fase 2: la superficie atiende | puntos de acceso expuestos: 15
[2] Punto de salud sin acceso firmado: 200 | acceso exigido: no
[2] Punto de salud con el almacen indisponible: 503
[3] Arranque sobre un almacen de linaje desconocido: detenido | peticiones atendidas: 0
[3] Mensaje del arranque detenido: sin ruta, sin direccion y sin traza
[4] Puertos conectados con su adaptador: 4 de 4 | conectados fuera de la composicion de raiz: 0
[4] Configuraciones de intercambio declaradas en el proceso: 1
[5] Configuracion de intercambio de origen cruzado: ausente | canal de sesion interactiva: ausente
Actos recorridos: 4 | Arranques: 2 | Arranques detenidos: 1 | Diferencias contra lo esperado: 0
```

**La primera línea con `peticiones atendidas durante la fase 1: 0` es el arranque en dos fases medido.** Las transformaciones se aplican **antes** de que la superficie atienda: si alguna petición entrara durante la fase 1, operaría sobre un almacén a medio preparar y nadie se enteraría.

**La línea `[3]` con el arranque detenido es una buena noticia, no una falla del sample.** Ante un linaje que no se entiende, el servicio **no arranca**. Atender peticiones sobre un almacén dudoso es peor que no atender ninguna, y `US-28` lo exige explícitamente.

**La segunda línea de `[3]` es `RA-03` en el peor momento posible.** El mensaje del arranque detenido es el que más tienta a incluir la ruta del archivo o una traza, porque quien lo lee está diagnosticando. **No las lleva**: la ruta del almacén es una dirección de servicio interno a los efectos de esa regla.

**Las dos líneas de `[2]` juntas explican por qué el punto de salud no exige acceso.** Responde **200** cuando el servicio puede atender y **503** cuando no, y tiene que poder responder **antes** de que exista una credencial válida: es lo que el guion de ejecución del servicio espera para saber que puede seguir. Es uno de los **cuatro** puntos que no exigen acceso firmado, y es de sólo lectura.

**La línea `[5]` tiene umbral exactamente cero y es `RA-01`.** No hay configuración de intercambio de origen cruzado y no hay canal de sesión interactiva, porque **el navegador no alcanza esta superficie**: el circuito del front termina en el front. Configurar cualquiera de las dos sería declarar que sí la alcanza, y reabriría las tres propiedades de la topología que el producto cerró.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Atender durante la fase 1 | Exponer la superficie antes de aplicar las transformaciones | La primera línea pasa a un número mayor que 0 y el criterio falla. Es lo que el arranque en dos fases viene a impedir |
| Arrancar igual con linaje desconocido | Continuar el arranque en lugar de detenerlo | `Arranques detenidos` pasa a **0** y el servicio atiende sobre un almacén en el que no se puede confiar |
| Conectar un puerto fuera de la composición de raíz | Instanciar un adaptador dentro de un punto de acceso | La línea de `conectados fuera de la composicion de raiz` pasa a **1**: el host deja de ser delgado (`ADR-01`) |
| Agregar configuración de intercambio de origen cruzado | Habilitarla «por las dudas» | La línea `[5]` deja de decir ausente, y con eso se declara que el navegador alcanza esta superficie. Rompe `RA-01`, que es regla de nivel producto |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-10`](../02-Especificacion-Funcional/Casos-De-Uso/CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md) | Caso de uso | Conecta **4 de 4** puertos con su adaptador, en un solo lugar, tomando la configuración del entorno |
| [`CU-11`](../02-Especificacion-Funcional/Casos-De-Uso/CU-11-Arrancar-El-Servicio-Y-Dejar-El-Almacen-En-Condiciones.md) | Caso de uso | Arranca en dos fases, responde salud y **detiene** el arranque ante un linaje desconocido |
| [`ADR-01`](../05-Arquitectura-Tecnica/Adrs/ADR-01-Host-Delgado-Con-Composicion-De-Raiz-Unica.md) | Decisión arquitectónica | **0** conexiones de puerto fuera de la composición de raíz |
| [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Formato-De-Intercambio-Y-Su-Configuracion.md) | Decisión arquitectónica | **1** sola configuración de intercambio declarada en el proceso |
| [`ADR-06`](../05-Arquitectura-Tecnica/Adrs/ADR-06-Composicion-De-Raiz-Ciclos-De-Vida-Y-Configuracion.md) | Decisión arquitectónica | Los ciclos de vida y la configuración resueltos en un único lugar |
| [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Arranque-En-Dos-Fases-Y-Punto-De-Salud-Sin-Acceso.md) | Decisión arquitectónica | Las dos fases con **0** peticiones atendidas en la primera, y el punto de salud sin acceso |
| `RA-01` | Regla de arquitectura del producto | **0** configuración de intercambio de origen cruzado y **0** canal de sesión interactiva: el navegador no alcanza esta superficie |
| `RA-03` | Regla de arquitectura del producto | El mensaje del arranque detenido no lleva ruta, dirección ni traza |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-03
  verifica: [CU-10, CU-11, US-26, US-27, US-28, US-29]
  comando: "bash samples/api/03-avanzado/run.sh"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "`bash scripts/build.sh` termina en 0 y sin advertencias"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén sano queda en su estado de primer arranque"
    - "El almacén de linaje desconocido lo compone el propio sample, y no es una captura de ningún almacén real"
    - "Configuración provista por el entorno; ninguna dirección, clave de firma ni ruta de almacén escrita en el sample"
    - "El sample arranca y detiene el servicio por su cuenta: no debe haber otra instancia atendiendo"
  criterio_aceptacion:
    exit_code: 0
    http:
      - punto: "punto de salud sin acceso firmado, con el servicio en condiciones"
        status: 200
      - punto: "punto de salud con el almacén indisponible"
        status: 503
    stdout_contiene:
      - "[1] Arranque, fase 1: transformaciones aplicadas sobre el almacen | peticiones atendidas durante la fase 1: 0"
      - "[1] Arranque, fase 2: la superficie atiende | puntos de acceso expuestos: 15"
      - "[3] Arranque sobre un almacen de linaje desconocido: detenido | peticiones atendidas: 0"
      - "[3] Mensaje del arranque detenido: sin ruta, sin direccion y sin traza"
      - "[4] Puertos conectados con su adaptador: 4 de 4 | conectados fuera de la composicion de raiz: 0"
      - "[4] Configuraciones de intercambio declaradas en el proceso: 1"
      - "[5] Configuracion de intercambio de origen cruzado: ausente | canal de sesion interactiva: ausente"
      - "Actos recorridos: 4 | Arranques: 2 | Arranques detenidos: 1 | Diferencias contra lo esperado: 0"
    stdout_no_contiene:
      - "Arranques detenidos: 0"
      - "conectados fuera de la composicion de raiz: 1"
      - "peticiones atendidas durante la fase 1: 1"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye tres aserciones negativas.** Los tres son defectos que dejan el servicio **aparentemente funcionando**: un arranque que continúa sobre un almacén dudoso, un puerto conectado por fuera de la composición de raíz y una petición atendida durante la fase 1. Ninguno produce un error visible, y ninguna aserción positiva de la lista los detectaría por sí sola.

**Una precisión sobre `puntos de acceso expuestos: 15`.** Es el recuento vivo de [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 desde su versión 1.1: **cuatro** sin acceso firmado más **once** bajo la guardia. `A-04` está retirado y no se recicla, de modo que un servicio que expusiera **16** estaría reponiendo un punto que el producto suprimió.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-10` y `CU-11`, que son lo que este proyecto de código tiene de propio: la composición de raíz con **4 de 4** puertos y **0** conexiones fuera de ella, y el arranque en dos fases con **0** peticiones atendidas en la primera y el arranque **detenido** ante un linaje desconocido. Ejercita `A-16`, el único punto que la colección del sample 02 deja fuera de su archivo, y verifica `RA-01` y `RA-03` con umbral cero. El contrato `VER-03` declara **dos** aserciones de respuesta HTTP, ocho líneas exactas de salida y **tres aserciones negativas** sobre defectos que dejan el servicio aparentemente funcionando; `evidencia` queda en `No verificado — sin código`. |
