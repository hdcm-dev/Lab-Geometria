# Ejemplo 03 — Las seis funciones sin backend, con los dos movimientos prendidos y el contador de red en cero

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Web
**Documento:** ejemplo-03-avanzado.md
**Versión:** 2.0
**Estado:** Aprobado
**Fecha:** 2026-08-30
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Avanzado
**Ubicación del código:** `/samples/visor/03-avanzado/`
**Trazabilidad upstream:** [`CU-12006`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12006-Ejercitar-La-Fachada-Sin-Backend.md) y [`CU-12007`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12007-Gobernar-El-Movimiento-Automatico-De-La-Escena.md); [`../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md`](../02-Especificacion-Funcional/Definicion-Contrato-De-Fachada.md) 1.1 §3.2, §4.6, §5.4, §5.5 y §6; [`../02-Especificacion-Funcional/Especificacion-Funcional.md`](../02-Especificacion-Funcional/Especificacion-Funcional.md) 1.2 §6, las **seis** propiedades transversales; [`../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md`](../08-Calidad-Y-Pruebas/Casos-Prueba-Referenciales.md) 1.0 `TC-12013` a `TC-12021`; [`../05-Arquitectura-Tecnica/Extensibilidad.md`](../05-Arquitectura-Tecnica/Extensibilidad.md); `PRODUCT-INTAKE` 1.22 §15 puertas `PT-02` y `PT-03`, §17.2.P.6 · GeometriaFactory-Visor y §18
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-12003` como sonda `SD-12015`; [`../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md`](../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md), que lo usa como batería de aceptación de un reemplazo; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar el punto de extensión del producto entero: las **seis** funciones de la fachada, recorridas de punta a punta **sin ninguna pieza del backend**, con los dos movimientos automáticos prendidos y sostenidos, y con el contador de peticiones de red en **cero**. Al terminar, quien lo ejecuta sabe gobernar el movimiento de una escena viva sin reconstruirla, y sabe medir una ausencia en su peor caso en lugar de medirla en el caso fácil.

**Es la tercera parte del sample `S-1`, y la que cierra su promesa.** El `PRODUCT-INTAKE` §18 declara que S-1 «ejerce el contrato entero sin ninguna pieza del backend, que es exactamente la propiedad que hace reemplazable al motor 3D».

## 2. Nivel

**Avanzado.** Es el único de los tres que ejerce las **seis** funciones, que mide las **dos** puertas técnicas `PT-02` y `PT-03`, y que inspecciona el **archivo de guion generado** además de la escena viva. Supone hechos los ejemplos 01 y 02.

## 3. Prerequisites

Los mismos cinco ítems de [`ejemplo-01-basico.md`](ejemplo-01-basico.md) §3, y dos agregados propios:

| Ítem | Versión mínima | Motivo |
| --- | --- | --- |
| Conductor de navegador capaz de **declarar preferencia de movimiento reducido** del sistema | — | Es el único doble admitido por [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) §5, y lo que se simula es el entorno del anfitrión, **no** una dependencia del archivo de guion |
| Comprobación reproducible de texto sobre el archivo de guion generado | — | El acto `[10]` inspecciona el **bundle generado** y no sólo la fuente: una dependencia que hiciera una petición por dentro no aparecería en la fuente |

**Sin acceso a redes de distribución externas.** El acto `[11]` mide `PT-03` abriendo la página en esas condiciones, de modo que dar acceso invalidaría la medición.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido, con el conductor configurado para declarar preferencia de movimiento reducido.
2. Generar el archivo de guion: `bash scripts/build-visor.sh`.
3. Ejecutar la verificación del sample: `npm --prefix samples/visor/03-avanzado run verify`.
4. Para mirarlo a mano, abrir `samples/visor/03-avanzado/index.html` y usar los dos controles de movimiento.
5. Comparar con §6.

## 5. Estructura del código

```
samples/visor/03-avanzado/
├── README.md                          # Copia corta de §1, §3 y §4 de este documento
├── package.json                       # Declara el comando `verify` del contrato de §9
├── index.html                         # Superficie de dibujo, árbol y los dos controles de movimiento
├── anfitrion.<ext>                    # Conserva la preferencia de cada movimiento; la fachada no
├── datos/
│   ├── E1.txt  E7.txt                 # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   └── trabajos.<ext>                 # Dos trabajos entre los que ir y volver diez veces
└── tests/
    ├── seis-funciones-sin-backend.<ext>   # CU-12006 y las seis propiedades transversales
    ├── gobierno-del-movimiento.<ext>      # CU-12007 y las ocho reglas de §5.5
    └── puertas-tecnicas.<ext>             # PT-02 y PT-03, y la inspección del bundle generado
```

**`anfitrion.<ext>` conserva la preferencia y la fachada no.** Es la línea divisoria de la garantía `G-2` y de `G-3`: la fachada **no consulta** la preferencia de movimiento reducido del sistema y **no conserva** la elección, y por eso la prueba puede prender los dos movimientos aunque el entorno declare esa preferencia. Sin esa propiedad, la medición de cero red quedaría en verde sin haber ejercitado nunca el bucle de dibujo.

## 6. Qué esperar

```
[1] Recorrido de las seis funciones con E-1: crear, cargar, seleccionar, ajustar, gobernar, destruir=6 de 6
[2] Servicios del backend disponibles durante el recorrido: 0
[3] Estado inicial con opciones ausentes: orbita=apagado giro=apagado
[4] Prender solo el giro: giro=prendido orbita=apagado (el no nombrado conserva su estado)
[5] Cambio en vivo: disposicion, seleccion, encuadre, resultado de dibujo e identificador sin cambios
[6] Invocar dos veces con el mismo valor: estado efectivo identico=si (idempotente)
[7] Apagar el giro: piezas de vuelta en su orientacion de partida=no, quedan donde estaban
[8] Cargar otro texto: estado de los dos movimientos conservado=si
[9] Arrastre de camara y superficie no visible: los dos se detienen | estado gobernado sin cambios
[10] Superficie del archivo de guion: funciones=6 | nombres propios en el objeto global=1 | globales sueltas=1 (__THREE__)
[10b] Ocurrencias de las tres formas de peticion, en la fuente y en el bundle generado: 0 y 0
[11] PT-03: motor de dibujo dentro del bundle=si | dependencias de red externa en ejecucion=0
[12] PT-02: carga=si escena=si E-1 con ortoedro=si diez recorridos sin degradar=si sincronizacion por indice=si
[13] Peticiones de red con los dos movimientos prendidos y sostenidos, y durante rotar y acercar: 0
[14] Claves escritas en el almacenamiento del navegador por la fachada: 0
[15] Codigos que el archivo de guion puede informar: 6 de 7 del contrato | acunados aguas abajo: 0
Funciones ejercidas: 6 de 6 | Propiedades transversales verificadas: 6 de 6 | Puertas tecnicas: 2 de 2
```

**La línea `[13]` sólo vale con la condición de medición que declara.** El umbral es exactamente **0**, medido **con los dos movimientos prendidos y sostenidos**, que es el peor caso porque el bucle de dibujo corre de continuo, y **durante los gestos de rotar y acercar**. Medirlo con los movimientos apagados —lo que ocurre por defecto en un entorno que declara preferencia de movimiento reducido— dejaría la prueba en verde sin haber ejercitado nunca el bucle. La condición es **vinculante**, no una recomendación.

**La línea `[10b]` inspecciona la fuente y el bundle generado, los dos.** Una dependencia que hiciera una petición por dentro no aparecería en la fuente, y el gate quedaría en verde sobre un archivo que sí hace red.

**La línea `[7]` cambió en la versión 2.0, y es una decisión del Product Owner y no una corrección de redacción.** Este documento afirmaba que al apagar el giro las piezas **vuelven** a su orientación de partida. El bucle **deja de incrementar** `rotation.y` y nada más: las piezas se quedan donde estaban. Lo mismo la cámara con la órbita.

**Se decidió que «apagar» significa detener y no volver**, y por dos motivos. El primero es que **la cámara la puede haber movido la persona** arrastrando, y devolverla al encuadre de partida le pisaría el suyo. El segundo es que `F-25` gobierna los dos movimientos **por separado y de forma simétrica**: si el giro volviera y la cámara no, dejarían de ser simétricos y habría que declarar por qué.

**Lo que no se hace es dejar la afirmación vieja.** Un documento que dice «vuelven» sobre un producto que detiene le promete a quien integra algo que no va a pasar.

**La línea `[10]` deja de exigir cero globales sueltas y declara la que hay.** `__THREE__` **no la pone el producto**: la registra el motor gráfico al cargarse, para avisar si hay dos copias suyas en la página. El nombre propio del paquete sigue siendo **uno solo** y las seis funciones están donde tienen que estar, que es lo que esta línea existe para medir. Exigir cero obligaría a parchear el motor, que es peor que declarar la global que trae.

**La línea `[15]` pasa de siete códigos a seis, y el que falta lo retiró una decisión.** `UNREADABLE_TEXT` era el código del **texto del alumno**, que la fachada ya no recibe desde [`ADR-08006`](../../../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md). **No se retira del contrato de la fachada**, que lo declara para el caso en que el anfitrión entregue algo que la fachada no pueda usar; lo que deja de ser cierto es que el bundle pueda informar los siete.

**Los acuñados aguas abajo siguen en cero, y estuvieron en uno.** Hasta el 2026-08-30 el bundle tenía `UNKNOWN` como respaldo de `reason ?? 'UNKNOWN'`, un código que el contrato no declara. Se retiró, y `MeshOutcome` pasó a ser una **unión discriminada** para que el caso que lo justificaba **no compile**.

**La línea `[15]` protege la distinción entre código y curso.** Los códigos son **siete** y su fuente única es §6 del contrato de fachada; `UNKNOWN_INSTANCE` aparece en **cinco** funciones y `INVALID_CANVAS_ELEMENT` en **dos cursos**, y ninguno de esos hechos multiplica el conjunto. La sexta función **no emite ninguna condición propia**.

**La línea `[12]` es `PT-02` entera, en sus cinco tramos, medidos juntos.** Una puerta que no pasa **detiene la planificación de la etapa `g`** y no se arrastra como deuda.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Medir la red con los movimientos apagados | Quitar la condición de medición del acto `[13]` | El recuento sigue en 0 **sin haber ejercitado el bucle**: es la medición del caso fácil, y es exactamente lo que la condición vinculante impide |
| Hacer que la fachada consulte la preferencia del sistema | Leer la preferencia dentro del archivo de guion | Se pierde `G-3`, y con ella la capacidad de prender los movimientos en la prueba. Es deriva mayor |
| Guardar la preferencia en el almacenamiento del navegador | Persistirla desde la fachada en lugar del anfitrión | El acto `[14]` deja de dar 0: se pierde `G-2`, sin gradación |
| Reemplazar la capa 3 por otro motor de dibujo | Sustituir el servicio de dibujo conservando la fachada | El sample entero debe seguir pasando **sin cambiar una línea del anfitrión**. Es la batería de aceptación que declara [`../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md`](../08-Calidad-Y-Pruebas/Guia-Testing-Extensibilidad.md) |

La última variación es el motivo por el que existe el punto de extensión: si el sample sigue pasando con otro motor, el motor era reemplazable.

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-12006`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12006-Ejercitar-La-Fachada-Sin-Backend.md) | Caso de uso | Actos `[1]` y `[2]`. Es el caso de uso que **materializa el sample S-1** |
| [`CU-12007`](../05-Arquitectura-Tecnica/Contrato-Componente-Visor/CU-12007-Gobernar-El-Movimiento-Automatico-De-La-Escena.md) | Caso de uso | Actos `[3]` a `[9]`: las **ocho** reglas de gobierno de §5.5 |
| Garantía `G-1` · Cero red | Garantía del contrato de fachada | Actos `[10b]` y `[13]`, con su condición de medición |
| Garantía `G-2` · Cero persistencia | Garantía del contrato de fachada | Acto `[14]` |
| Garantía `G-3` · Sin configuración propia | Garantía del contrato de fachada | Acto `[3]` y la segunda variación |
| Garantía `G-6` · Determinismo | Garantía del contrato de fachada | Acto `[5]`: ningún movimiento altera la disposición |
| [`ADR-12003`](../05-Arquitectura-Tecnica/Adrs/ADR-12003-Visualizador-Puro-Sin-Red-Ni-Identidad.md) | Decisión arquitectónica | Actos `[2]`, `[13]` y `[14]` |
| [`ADR-12004`](../05-Arquitectura-Tecnica/Adrs/ADR-12004-Motor-De-Dibujo-Empaquetado-Y-Aislado.md) | Decisión arquitectónica | Acto `[11]` y la cuarta variación |
| [`ADR-12006`](../05-Arquitectura-Tecnica/Adrs/ADR-12006-Bundle-Generado-Y-Versionado-Del-Punto-De-Extension.md) | Decisión arquitectónica | Actos `[10]` y `[10b]`, sobre el **bundle generado** |
| **`PT-02`** del `PRODUCT-INTAKE` §15 y §17.2.P.8 · GeometriaFactory-Visor | Puerta técnica | Acto `[12]`, en sus cinco tramos |
| **`PT-03`** del `PRODUCT-INTAKE` §15 y §17.2.P.8 · GeometriaFactory-Visor | Puerta técnica | Acto `[11]` |
| **RA-01** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Actos `[10b]` y `[13]`: el archivo de guion **es** el JavaScript del navegador del producto, y al no hacer red no puede invocar la API |
| **RA-02** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Actos `[2]`, `[3]`, `[13]` y `[14]`: sin red, sin configuración propia, sin persistencia y sin identidad |
| **RA-03** del `PRODUCT-INTAKE` §14 | Regla de arquitectura del producto | Acto `[15]`: ninguno de los **siete** códigos expone una dirección de servicio, porque el archivo de guion no conoce ninguna |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-12003
  verifica: [CU-12006, CU-12007, US-12002, US-12012, US-12013, US-12014]
  comando: "bash scripts/build-visor.sh && npm --prefix samples/visor/03-avanzado run verify"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "Navegador con capacidad gráfica tridimensional disponible para el conductor"
    - "El conductor declara preferencia de movimiento reducido del sistema, y la verificación prende igual los dos movimientos"
    - "0 servicios del backend levantados"
    - "Sin acceso a redes de distribución externas: es la condición con la que se mide PT-03"
    - "Comprobación reproducible de texto disponible sobre el archivo de guion generado, no sólo sobre la fuente"
    - "datos/E1.txt y datos/E7.txt transcriptos del PRODUCT-INTAKE §20 sin modificación"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[1] Recorrido de las seis funciones con E-1: crear, cargar, seleccionar, ajustar, gobernar, destruir=6 de 6"
      - "[7] Apagar el giro: piezas de vuelta en su orientacion de partida=no, quedan donde estaban"
      - "[10] Superficie del archivo de guion: funciones=6 | nombres propios en el objeto global=1 | globales sueltas=1 (__THREE__)"
      - "[10b] Ocurrencias de las tres formas de peticion, en la fuente y en el bundle generado: 0 y 0"
      - "[12] PT-02: carga=si escena=si E-1 con ortoedro=si diez recorridos sin degradar=si sincronizacion por indice=si"
      - "[13] Peticiones de red con los dos movimientos prendidos y sostenidos, y durante rotar y acercar: 0"
      - "[14] Claves escritas en el almacenamiento del navegador por la fachada: 0"
      - "[15] Codigos que el archivo de guion puede informar: 6 de 7 del contrato | acunados aguas abajo: 0"
      - "Funciones ejercidas: 6 de 6 | Propiedades transversales verificadas: 6 de 6 | Puertas tecnicas: 2 de 2"
  evidencia:
    estado: "No verificado — sin código"
```

**Las precondiciones no son decorativas.** Tres de ellas —la preferencia declarada, la ausencia de acceso a redes externas y la comprobación sobre el bundle generado— son **condiciones de medición vinculantes**: una corrida hecha sin ellas produce recuentos en 0 que no significan nada.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-29 | **Tramo `R-3d` del renombre `F-03`, que lo cierra.** **1 línea(s)** pasan los códigos de condición de la forma castellana a la vigente, con el mapeo de [`../../../Producto/Norma-De-Nomenclatura.md`](../../../Producto/Norma-De-Nomenclatura.md) **§6.8** —101 pares— y **sin elegir ninguno acá**. Se respeta **§4.1**: no se tocan las filas de control de cambios, ni lo que está entre «…», ni **la prosa que narra el renombre** —una línea que trae la forma vieja y su par vigente está reportando, no usando—. **Ninguna palabra de prosa cambia**, verificado con el control de diff del tramo. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Tercera parte del sample **S-1**, la que cierra su promesa: ejerce las **seis** funciones de la fachada sin ninguna pieza del backend. Cubre `CU-12006` y `CU-12007`, las **ocho** reglas de gobierno del movimiento de §5.5, las **seis** propiedades transversales, los **siete** códigos y las **dos** puertas técnicas `PT-02` y `PT-03`. El contrato `VER-12003` declara nueve líneas exactas de salida y **tres precondiciones que son condiciones de medición vinculantes**; `evidencia` queda en `No verificado — sin código`. |
| 2.0 | 2026-08-30 | **§6 se alinea con lo construido, en tres líneas y por tres motivos distintos.** **`[7]`** afirmaba que al apagar el giro las piezas vuelven a su orientación de partida: **detienen y no vuelven**, y el Product Owner decidió el 2026-08-30 que **«apagar» significa detener** —la cámara la puede haber movido la persona, y `F-25` gobierna los dos movimientos de forma simétrica—. **`[10]`** exigía cero globales sueltas y hay una, `__THREE__`, que **no la pone el producto** sino el motor gráfico al cargarse; exigir cero obligaría a parchearlo. **`[15]`** pasa de siete códigos a seis: `UNREADABLE_TEXT` era el del texto del alumno, que la fachada ya no recibe desde [`ADR-08006`](../../../Producto/Adrs/ADR-08006-El-Visor-Recibe-Piezas-Reconstruidas-Y-No-El-Texto.md) —y **no se retira del contrato**, que lo declara para otro caso—. **Los acuñados aguas abajo siguen en cero y estuvieron en uno**: el respaldo `UNKNOWN` se retiró el 2026-08-30 y la unión discriminada de `MeshOutcome` volvió imposible el caso que lo justificaba. Las tres las encontró la implementación del sample. Sube **major**: tres líneas del snapshot cambian de contenido. |
