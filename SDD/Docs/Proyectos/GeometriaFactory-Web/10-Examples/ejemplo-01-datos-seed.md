# Ejemplo 01 — Datos seed: la comisión desde la que arranca el guion de demostración

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Web
**Documento:** ejemplo-01-datos-seed.md
**Versión:** 1.1
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** [`/samples/web/01-datos-seed/`](../../../../../samples/web/01-datos-seed/), esqueletada con su README local y su comando previsto
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-05`, `CU-06` y `CU-08`; [`../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) y [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md); [`../08-Calidad-Y-Pruebas/Estrategia-Testing.md`](../08-Calidad-Y-Pruebas/Estrategia-Testing.md) 1.1 §3 y §6; `PRODUCT-INTAKE` **1.25** §16.1, §18 **S-3** y §20, los **ocho** escenarios
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-01` como fila `SD-62`; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Dejar la comisión en un **estado de partida reproducible** para que el guion de demostración de cada etapa arranque siempre desde lo mismo, y verificarlo **sin pasar por la pantalla**: una cuenta de administrador, dos cuentas de alumno, y los **ocho** escenarios del `PRODUCT-INTAKE` §20 ya enviados como trabajos, con los cuatro estados representados. Al terminar, quien lo ejecuta tiene los datos que las once superficies exhiben y sabe **cuántos trabajos tiene que ver cada papel** antes de abrir el navegador.

## 2. Nivel

**Básico**, y la progresión de este proyecto de código es **por capacidad** y no por nivel, de modo que el nivel implícito se declara acá como `Rules-Examples.md` §3.2 exige. Es básico porque no supone ningún otro sample, no necesita navegador y no toca la pieza pública: habla con el servicio de datos por la misma superficie que el cliente tipado de capa 3 usa después.

## 3. Prerequisites

- **Entorno de desarrollo contenido del repositorio.** Todo el ciclo ocurre adentro, porque el host no tiene las herramientas.
- **El servicio de datos levantado** por su guion de ejecución, sobre un almacén llevado a su estado de primer arranque por su guion de reinicio. Los dos nombres de guion salen del `PRODUCT-INTAKE` §16 y §18 y **no se eligen acá**.
- **La dirección del servicio de datos tomada de configuración**, como el resto de esta pieza ([`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md)). **No se escribe ninguna dirección concreta en el sample**, que es `RA-03`.
- **Ninguna credencial real.** Las identidades del seed son valores evidentemente ficticios y se declaran como tales en el propio archivo.

**No hace falta navegador y no hace falta la pieza pública construida.** Es deliberado: lo que el sample deja armado es el dato, y lo que la pieza pública hace con él es materia del guion de demostración.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio de datos: `bash scripts/run-api.sh`.
4. Ejecutar el sample: `bash samples/web/01-datos-seed/run.sh`.
5. Comparar la salida con §6.

**Cinco pasos, el máximo que `Rules-Examples.md` §4.2 admite**, y los cinco dentro del entorno contenido: **cero** pasos en el host.

## 5. Estructura del código

```
samples/web/01-datos-seed/
├── README.md                      # Copia corta de §1, §3 y §4 de este documento
├── run.sh                         # Punto de entrada único: siembra y verifica
├── datos/
│   ├── E1.txt  E2.txt  E3.txt     # Transcriptos del PRODUCT-INTAKE §20, sin modificación
│   ├── E4.txt  E5.txt  E6.txt     #   E2.txt NO es JSON estrictamente válido: es su gracia
│   └── E7.txt  E8.txt
├── identidades.env.ejemplo        # Nombres y correos ficticios, y NINGÚN valor de credencial real
└── verificacion/
    └── esperado.txt               # Snapshot de la salida de §6
```

**Los archivos de escenario llevan extensión `.txt` y no `.json`, a propósito.** El de `E-2` **no es JSON estrictamente válido** —tiene dos comas finales—, y nombrarlo `.json` invitaría a que una herramienta lo reformateara al abrirlo. Acá el texto se transporta al servicio de datos **carácter por carácter**, y la fila `SD-36` de la matriz de sensado declara deriva mayor, sin gradación, ante cualquier normalización. Es el mismo criterio que ya adoptaron las categorías 10 de `GeometriaFactory-Contracts` y de `GeometriaFactory-Visor`.

**`identidades.env.ejemplo` es un archivo de ejemplo y no una configuración.** No lleva ninguna credencial real ni ninguna dirección concreta: las dos cosas llegan del entorno, y la fila `SD-57` de la matriz de sensado sensa que **ningún mensaje visible** exponga una dirección de servicio interno.

## 6. Qué esperar

```
[seed] Administrador configurado: si
[seed] Cuentas de alumno habilitadas: 2
[seed] Trabajos enviados con los ocho escenarios: 8
[seed] Estados resultantes: Pendiente=4 Borrador=2 Aprobado=1 Rechazado=1
[verif] Listado propio del alumno 1: 8 trabajos
[verif] Trabajos con desenlace visible en el listado propio: 2
[verif] Listado de la comision pedido por el administrador: 6 trabajos
[verif] Borradores visibles en el listado de la comision: 0
[verif] Detalle de un trabajo: texto original identico al enviado: si
[verif] Observaciones de E-3: 1 advertencia de area declarado=36.00 derivado=54.00
[verif] Observaciones de E-4: 0
[verif] Observaciones de E-5: 1 error indice-figura=1 campo=Tipo
Seed completo | Trabajos: 8 | Verificaciones: 8 | Fallas: 0
```

**Las dos líneas del listado de la comisión son la razón de ser del seed.** El alumno ve **8** y el administrador ve **6**: la diferencia son exactamente los **2** borradores, los de `E-5` y `E-8`, y `RN-11` no admite que se vea ninguno. Un seed que dejara los ocho en el mismo estado haría invisible esa diferencia justo en el dato con el que después se demuestra el producto.

**`texto original identico al enviado: si` es `RN-08` medido donde se puede medir.** El sample envía el texto y lo vuelve a leer del detalle: si algo lo normalizó en el camino —el borde del servicio, el transporte o el propio sample—, esta línea lo dice antes de que nadie abra una pantalla.

**El par `E-3` contra `E-4` está en el seed a propósito.** Son el mismo cubo de lado 3 emitido por los dos ejemplos de la cátedra: el primero produce una advertencia con su par de valores y el segundo **cero** observaciones. Es el contraste que la superficie de envío tiene que saber presentar, y el que la fila `SD-33` de la matriz de sensado protege exigiendo que los dos valores se muestren **sin reformatear**.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Seed sin desenlaces | Omitir la aprobación y el rechazo | El listado propio deja de exhibir los cuatro estados y la superficie de trabajos del alumno no se puede recorrer entera |
| Reformatear `E2.txt` | Abrir el archivo con una herramienta que lo normalice y volver a correr | `texto original identico al enviado` pasa a `no` y la verificación falla. **Es lo que la extensión `.txt` viene a evitar** |
| Correr dos veces sin reiniciar el almacén | Repetir el paso 4 sin el paso 2 | El administrador ya está configurado y los correos ya están registrados: el seed lo declara como resultado legítimo y no como falla |
| Seed con el servicio de datos caído | Detener el servicio antes del paso 4 | El sample termina con falla y **no** deja el almacén a medias. Es el estado degradado que `CU-10` presenta del lado de la pantalla, visto desde afuera |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-05`](../02-Especificacion-Funcional/Casos-De-Uso/CU-05-Enviar-Un-Trabajo-Y-Ver-El-Resultado-De-La-Interpretacion.md) | Caso de uso | Envía los **ocho** escenarios y comprueba el resultado de la interpretación de cada uno |
| [`CU-06`](../02-Especificacion-Funcional/Casos-De-Uso/CU-06-Consultar-El-Listado-Propio-Y-Operar-Sobre-El-Borrador.md) | Caso de uso | Deja el listado propio con los **cuatro** estados representados y con **2** desenlaces visibles |
| [`CU-08`](../02-Especificacion-Funcional/Casos-De-Uso/CU-08-Recorrer-La-Entrega-De-La-Comision.md) | Caso de uso | Deja la entrega de la comisión con **6** trabajos y **0** borradores visibles |
| [`RN-08`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-08-Texto-Original-Conservado-Integro.md) | Regla de negocio | El texto leído del detalle es idéntico al enviado |
| [`RN-11`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-11-El-Administrador-No-Ve-Los-Borradores.md) | Regla de negocio | **0** borradores visibles en el listado de la comisión |
| [`RN-05`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-05-Finalizacion-Sin-Errores-De-Validacion.md) | Regla de negocio | `E-5` y `E-8` quedan en `Borrador`; los otros seis pasan a `Pendiente` |
| [`ADR-02`](../05-Arquitectura-Tecnica/Adrs/ADR-02-Sin-Estado-Propio-Y-Sin-Persistencia.md) | Decisión arquitectónica | El seed escribe en el servicio de datos y **no** en ningún almacén de esta pieza, que no tiene ninguno |
| [`ADR-07`](../05-Arquitectura-Tecnica/Adrs/ADR-07-Direccion-Del-Servicio-De-Datos-Desde-Configuracion.md) | Decisión arquitectónica | La dirección llega de configuración y no está escrita en el sample |
| `RA-01` | Regla de arquitectura del producto | El sample corre **servidor a servidor** dentro del entorno contenido: no hay navegador, y por lo tanto no hay JavaScript del navegador llamando al servicio de datos |
| `RA-03` | Regla de arquitectura del producto | **0** apariciones de una dirección de servicio interno en el sample y en su salida |
| `PRODUCT-INTAKE` §18 **S-3** | Muestra del producto | El juego de datos de los ocho escenarios que esa muestra describe como «listos para pegar en el formulario de carga». **S-3 está asignada a `GeometriaFactory-Infrastructure`**, y su categoría 10 la materializa contra el validador; acá los mismos ocho textos se usan como **estado de partida de la pieza pública**, que es el otro destino que la propia muestra nombra. No se duplica aquélla: se la consume desde el otro lado de la frontera |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-01
  verifica: [CU-05, CU-06, CU-08, US-11, US-15, US-17, US-22, US-23]
  comando: "bash samples/web/01-datos-seed/run.sh"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén queda en su estado de primer arranque"
    - "`bash scripts/run-api.sh` ejecutado y el punto de salud del servicio de datos respondiendo"
    - "La dirección del servicio de datos provista por configuración del entorno; ninguna dirección escrita en el sample"
    - "Los ocho textos de datos/ transcriptos del PRODUCT-INTAKE §20 sin modificación, con E2.txt sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    stdout_contiene:
      - "[seed] Trabajos enviados con los ocho escenarios: 8"
      - "[seed] Estados resultantes: Pendiente=4 Borrador=2 Aprobado=1 Rechazado=1"
      - "[verif] Listado propio del alumno 1: 8 trabajos"
      - "[verif] Listado de la comision pedido por el administrador: 6 trabajos"
      - "[verif] Borradores visibles en el listado de la comision: 0"
      - "[verif] Detalle de un trabajo: texto original identico al enviado: si"
      - "[verif] Observaciones de E-4: 0"
      - "Seed completo | Trabajos: 8 | Verificaciones: 8 | Fallas: 0"
    stdout_no_contiene:
      - "Borradores visibles en el listado de la comision: 1"
      - "texto original identico al enviado: no"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye dos aserciones negativas.** `RN-11` y `RN-08` son las dos reglas que este seed puede violar en silencio, y en las dos el umbral de la matriz de sensado es **Mayor** para el supuesto que el sample verifica, sin tramo menor: `SD-36`, para el texto original, está entre las filas **sin gradación** que enumera §5 de esa matriz; `SD-10`, la superficie del listado de la comisión, **no** está en esa lista, pero declara «**Mayor**: aparece un trabajo en estado `Borrador`…», de modo que para este supuesto tampoco admite grado. Un solo borrador visible o un solo carácter cambiado no es una diferencia de grado: es la falla.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.1 | 2026-08-11 | **Corrección del hallazgo P3-2 del informe `G-10-Examples-Siete-Proyectos-r1.md` 1.0.** El párrafo de §9 sobre las aserciones negativas atribuía umbral «**sin gradación**» a las dos filas que miran lo mismo, y **`SD-10` no figura en la lista de filas sin gradación** de §5 de la matriz de sensado de este proyecto de código; `SD-36` sí. Se escribe ahora la distinción exacta —`SD-10` declara **Mayor** para ese supuesto y no tiene tramo menor, de modo que **no hay contradicción de umbral**, que era lo que había que descartar—, sin agregarle a `SD-10` una clasificación que la matriz no le da y sin tocar ninguna de sus **61** filas de línea de base. **El informe ubicaba este segundo lugar en el §3 del `README.md` de la categoría; está acá, en la §9 de este documento**, y el `README.md` no contiene la afirmación. Se enlaza además la carpeta esqueletada de [`/samples/web/01-datos-seed/`](../../../../../samples/web/01-datos-seed/) creada al resolver el **P0-1**, y se actualiza la trazabilidad al `PRODUCT-INTAKE` **1.25**. Ningún acto, criterio de aceptación ni recuento cambia. |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Declara el juego de datos seed con los **ocho** escenarios del `PRODUCT-INTAKE` §20 transcriptos sin modificación, la comisión que deja armada —**1** administrador, **2** alumnos, **8** trabajos con los cuatro estados representados— y la verificación **sin pasar por la pantalla**, con **8** comprobaciones. Declara por qué los archivos de escenario llevan extensión `.txt`, por qué el sample no viola `RA-01` ni `RA-03`, y el nivel implícito, porque la progresión de este proyecto de código es por capacidad. El contrato `VER-01` declara ocho líneas exactas de salida y **dos aserciones negativas** sobre `RN-11` y `RN-08`; `evidencia` queda en `No verificado — sin código`. |
