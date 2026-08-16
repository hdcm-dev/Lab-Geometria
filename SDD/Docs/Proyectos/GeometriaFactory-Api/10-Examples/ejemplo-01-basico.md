# Ejemplo 01 — El canje, la guardia y el envío que no verifica: por qué esa respuesta es exitosa

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** ejemplo-01-basico.md
**Versión:** 1.0
**Estado:** Aprobado
**Fecha:** 2026-08-11
**Autor:** Developer Advocate / Sample Engineer Senior (AG-10)
**Nivel:** Básico
**Ubicación del código:** `/samples/api/01-basico/`
**Trazabilidad upstream:** [`../02-Especificacion-Funcional/Casos-De-Uso/`](../02-Especificacion-Funcional/Casos-De-Uso/) `CU-00001`, `CU-00002` y `CU-00009`; [`../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md`](../02-Especificacion-Funcional/Definicion-Superficie-HTTP.md) §3 y §6; [`../03-UX-UI-DX/Guia-Onboarding-Developer.md`](../03-UX-UI-DX/Guia-Onboarding-Developer.md) §3.3 y §3.4; `PRODUCT-INTAKE` 1.23 §20, escenarios `E-5` y `E-8`
**Trazabilidad downstream:** [`../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md`](../08-Calidad-Y-Pruebas/Matriz-Sensado-Deriva.md), que toma `VER-00001` como sonda; `11-Documentacion` cuando se emita

---

## 1. Objetivo del sample

Demostrar la frontera de esta capa en el recorrido más corto que la deja a la vista: canjear credenciales por un acceso firmado, comprobar que la guardia rechaza lo que tiene que rechazar, y enviar el escenario `E-5` para ver **una respuesta exitosa que trae un trabajo en `Borrador`**. Al terminar, quien lo ejecuta sabe que **el código de respuesta habla de la petición y el estado del trabajo habla del texto del alumno**, y que confundirlos es el defecto más caro de esta capa.

## 2. Nivel

**Básico.** Punto de entrada absoluto del proyecto de código. Usa **dos** de los ocho escenarios y **seis** de los quince puntos de acceso: `A-01`, `A-02`, `A-03`, `A-05`, `A-07` y `A-10`. Es el sample que [`../03-UX-UI-DX/Guia-Onboarding-Developer.md`](../03-UX-UI-DX/Guia-Onboarding-Developer.md) §3.3 llama «el primer ejemplo con sentido», y no el camino feliz, por el motivo que esa sección declara: quien entiende por qué esta respuesta es exitosa entiende dónde está la frontera.

## 3. Prerequisites

- **Entorno de desarrollo contenido del repositorio.** Todo el ciclo ocurre adentro, porque el host no tiene las herramientas y no va a tenerlas.
- **El almacén llevado a su estado de primer arranque** con el guion de reinicio, y **el servicio levantado** con su guion de ejecución. Los dos nombres de guion salen del `PRODUCT-INTAKE` §16 y §18 y **no se eligen acá**.
- **La dirección del servicio tomada del entorno.** El sample no contiene ninguna dirección concreta, ninguna clave de firma y ninguna contraseña real.
- **Un cliente de peticiones capaz de leer un archivo de petición y comparar la respuesta.** Se nombra por su función y no por su producto: la herramienta concreta y su anclaje de versión son de la etapa `a`.

## 4. Cómo correrlo

1. Abrir el repositorio dentro del entorno de desarrollo contenido.
2. Reiniciar el almacén: `bash scripts/reset-db.sh`.
3. Levantar el servicio: `bash scripts/run-api.sh`.
4. Ejecutar el sample: `bash samples/api/01-basico/run.sh`.
5. Comparar la salida con §6.

**Cinco pasos, el máximo que `Rules-Examples.md` §4.2 admite**, y los cinco dentro del entorno contenido: **cero** pasos en el host.

## 5. Estructura del código

```
samples/api/01-basico/
├── README.md                     # Copia corta de §1, §3 y §4 de este documento
├── run.sh                        # Punto de entrada único
├── peticiones/
│   ├── 01-configurar-admin       # A-03
│   ├── 02-registrar-alumno       # A-02
│   ├── 03-habilitar-alumno       # A-07, devuelve la provisoria
│   ├── 04-cambiar-contrasena     # A-05, con la provisoria como vigente
│   ├── 05-canjear                # A-01
│   └── 06-enviar-trabajo         # A-10, con E5.txt y E8.txt como cuerpo
├── cuerpos/
│   ├── E5.txt  E8.txt            # Transcriptos del PRODUCT-INTAKE §20, sin modificación
└── esperado/
    └── salida.txt                # Snapshot de la salida de §6
```

**Los archivos de cuerpo llevan extensión `.txt` y no `.json`, a propósito.** Los textos del alumno se transportan **sin normalizar en el borde**, que es lo que `US-00019` exige, y nombrarlos `.json` invitaría a que una herramienta los reformateara al abrirlos. Es el mismo criterio que ya adoptaron las categorías 10 de `GeometriaFactory-Contracts` y de `GeometriaFactory-Visor`.

**Los cuatro primeros archivos de petición no son el objeto del sample: son su preparación.** Existen porque sin cuenta habilitada no hay canje, y `A-04` —el punto anónimo que fijaba la contraseña— **está retirado** desde `PRODUCT-INTAKE` 1.13: hoy la habilitación devuelve la provisoria y el alumno cambia la suya por `A-05`, ya autenticado.

## 6. Qué esperar

```
[canje] Credenciales validas: 200 | acceso firmado recibido: si
[canje] Credenciales invalidas: 401 CONTRATO_CREDENCIAL_INVALIDA | campo que fallo declarado: no
[canje] Cuenta pendiente: 403 CONTRATO_CUENTA_NO_HABILITADA | motivo presente: si
[guardia] Peticion sin acceso: 401 | Peticion con acceso vencido: 401 | Firma ajena: 401
[guardia] Papel insuficiente sobre un punto de administracion: 403
[guardia] Cuenta con cambio pendiente sobre cualquier punto salvo uno: 403 CONTRATO_CAMBIO_DE_CONTRASENA_REQUERIDO
[envio] E-5: 201 | estado del trabajo: Borrador
[envio] E-5: observacion de error indice-figura=1 campo=Tipo
[envio] E-8: 201 | estado del trabajo: Borrador
[envio] E-8: observacion de error localizada por indice de figura y campo
[traduccion] Respuestas con codigo del contrato reconocido: 6 de 6
[traduccion] Respuestas con direccion, ruta, traza o secreto: 0
Peticiones ejecutadas: 14 | Respuestas comparadas: 14 | Diferencias: 0
```

**Las dos líneas de `[envio]` con `201` y `Borrador` juntas son la lección entera.** El código es de **éxito** y el trabajo quedó guardado; lo que no verificó es **el texto del alumno**, y eso viaja en el cuerpo y no en el número. Un servicio que devolviera `400` acá estaría diciendo que la petición está mal formada, y no lo está: el alumno mandó exactamente lo que su programa emite.

**`E-8` está en este sample y no sólo en la colección** porque es el modo de falla que el propio `PRODUCT-INTAKE` §20.E-8 llama **el más probable de todos**: lo produce la configuración regional de la máquina —la coma decimal— y no un error de programación del alumno.

**La última línea de `[traduccion]` tiene umbral exactamente cero y es `RA-03`.** Ninguna respuesta lleva la dirección de un servicio interno, la ruta del archivo del almacén, una traza ni un secreto. Esta capa es **la última que toca un dato del backend antes de que salga del servidor propio**, y por eso acá es donde esa regla se puede violar hacia afuera.

## 7. Variaciones sugeridas

| Variación | Qué cambiar | Resultado |
| --- | --- | --- |
| Declarar qué campo falló | Hacer que el canje distinga correo inexistente de contraseña incorrecta | La respuesta deja de ser genérica y `US-00002` deja de cumplirse: se filtra qué correos existen |
| Enviar `E-5` como `400` | Tratar el error de interpretación como petición mal formada | El trabajo **no se guarda**, el alumno pierde su texto y `RN-00008` deja de poder cumplirse |
| Exceptuar un segundo punto de la guardia del cambio pendiente | Dejar pasar otro punto además del cambio de contraseña propia | `US-00006` deja de cumplirse: la cuenta marcada alcanza una capacidad que `RN-00013` prohíbe |
| Devolver la ruta del almacén en un mensaje | Incluir la ruta del archivo de datos en el cuerpo de un error | La línea de umbral cero pasa a **1** y el criterio de aceptación falla. Es `RA-03` |

## 8. Trazabilidad

| Artefacto upstream | Tipo | Cómo lo ilustra este sample |
| --- | --- | --- |
| [`CU-00001`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00001-Canjear-Credenciales-Por-Un-Acceso-Firmado.md) | Caso de uso | Canjea correo y contraseña por un acceso firmado, y recorre sus tres desenlaces |
| [`CU-00002`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00002-Admitir-La-Peticion-Acceso-Papel-Y-Marca.md) | Caso de uso | Ejercita la guardia en sus tres dimensiones: acceso, papel y marca |
| [`CU-00009`](../02-Especificacion-Funcional/Casos-De-Uso/CU-00009-Traducir-El-Motivo-Del-Contrato-A-Respuesta-De-Protocolo.md) | Caso de uso | Comprueba que los **6** códigos de contrato que aparecen se traducen según la tabla única, sin inventar ninguno |
| [`RN-02005`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02005-Finalizacion-Sin-Errores-De-Validacion.md) | Regla de negocio | `E-5` y `E-8` quedan en `Borrador` sin que la respuesta sea un fallo |
| [`RN-02009`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02009-Observacion-De-Error-Con-Posicion-Y-Campo.md) | Regla de negocio | Índice **1** y campo en las dos observaciones de error |
| [`RN-02013`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02013-Cambio-Forzado-Antes-De-Toda-Otra-Capacidad.md) | Regla de negocio | La guardia del cambio pendiente sobre todos los puntos salvo uno |
| [`RN-02016`](../../GeometriaFactory-Domain/02-Especificacion-Funcional/Reglas-De-Negocio/RN-02016-Habilitar-Produce-La-Provisoria.md) | Regla de negocio | La habilitación devuelve la provisoria, que el cambio usa como vigente |
| [`ADR-00003`](../05-Arquitectura-Tecnica/Adrs/ADR-00003-Credencial-Firmada-Papel-Por-Punto-Y-Guardia-Transversal.md) | Decisión arquitectónica | La credencial firmada, el papel por punto y la guardia transversal |
| [`ADR-00004`](../05-Arquitectura-Tecnica/Adrs/ADR-00004-Dos-Traducciones-Con-Tabla-Unica-Y-Sin-Codigos-Inventados.md) | Decisión arquitectónica | Las dos traducciones con tabla única y **0** códigos inventados |
| `RA-03` | Regla de arquitectura del producto | **0** respuestas con dirección, ruta, traza o secreto |
| `PRODUCT-INTAKE` §20 `E-5`, `E-8` | Escenario con payload real | Los dos textos se transcriben sin modificación y son los cuerpos del envío |

## 9. Contrato de verificación

```yaml
verificacion:
  id: VER-00001
  verifica: [CU-00001, CU-00002, CU-00009, US-00001, US-00002, US-00003, US-00004, US-00005, US-00006, US-00024, US-00025]
  comando: "bash samples/api/01-basico/run.sh"
  precondiciones:
    - "Repositorio abierto dentro del entorno de desarrollo contenido del propio repositorio"
    - "`bash scripts/reset-db.sh` ejecutado: el almacén queda en su estado de primer arranque"
    - "`bash scripts/run-api.sh` ejecutado y el punto de salud respondiendo antes de la primera petición"
    - "Dirección del servicio y clave de firma provistas por configuración del entorno; ninguna de las dos escrita en el sample"
    - "Los dos textos de cuerpos/ transcriptos del PRODUCT-INTAKE §20 sin modificación y sin reformatear"
  criterio_aceptacion:
    exit_code: 0
    http:
      - punto: "canje con credenciales válidas"
        status: 200
      - punto: "canje con credenciales inválidas"
        status: 401
        body_json: { codigo: "CONTRATO_CREDENCIAL_INVALIDA" }
      - punto: "envío del escenario E-5"
        status: 201
        body_json: { estado: "Borrador" }
      - punto: "envío del escenario E-8"
        status: 201
        body_json: { estado: "Borrador" }
    stdout_contiene:
      - "[envio] E-5: observacion de error indice-figura=1 campo=Tipo"
      - "[traduccion] Respuestas con codigo del contrato reconocido: 6 de 6"
      - "[traduccion] Respuestas con direccion, ruta, traza o secreto: 0"
      - "Peticiones ejecutadas: 14 | Respuestas comparadas: 14 | Diferencias: 0"
    stdout_no_contiene:
      - "[envio] E-5: 400"
      - "indice-figura=0"
      - "campo que fallo declarado: si"
  evidencia:
    estado: "No verificado — sin código"
```

**Por qué el criterio incluye tres aserciones negativas.** Las tres son defectos que **pasarían todas las aserciones positivas** si la implementación se equivocara de la manera más natural: devolver `400` ante un texto que no verifica, informar siempre la primera figura en lugar de calcular el índice, y declarar cuál de los dos campos del canje falló, que filtra qué correos existen.

## 10. Control de cambios

| Versión | Fecha | Descripción |
| --- | --- | --- |
| 1.0 | 2026-08-11 | Emisión inicial en la **pasada de diseño**. Cubre `CU-00001`, `CU-00002` y `CU-00009` sobre **seis** puntos de acceso y **dos** escenarios reales, `E-5` y `E-8`, transcriptos sin modificación. Declara por qué los archivos de cuerpo llevan extensión `.txt`, por qué las cuatro primeras peticiones son preparación y no objeto, y por qué `A-04` no aparece. El contrato `VER-00001` declara **cuatro** aserciones de respuesta HTTP con código y cuerpo, cuatro líneas exactas de salida y **tres aserciones negativas**; `evidencia` queda en `No verificado — sin código`. |
