# CU-00021 — Dar de alta una cuenta de alumno

**Producto:** Fábrica de Geometría
**Unidad de entrega:** GeometriaFactory-Api
**Documento:** CU-00021-Dar-De-Alta-Una-Cuenta-De-Alumno.md
**Versión:** 1.0
**Estado:** Propuesto
**Fecha:** 2026-08-16
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** [`NB-00002`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md) §1 y §5; [`NB-00001`](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md) §5; `00-Contexto/Vision-Producto.md` §9.1 y §9.2; `PRODUCT-INTAKE-Fabrica-De-Geometria.md` §4 (F-02, con origen en RF-03), §4.1 (RN-02002, RN-02016), §6 (flujo 1), §17.1.P.2 (INV-01)
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico` y `08-Calidad-Y-Pruebas` de la unidad de entrega
**Consolida a:** `CU-00003` §A-02, [`CU-04001`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-04001-Registrar-El-Alta-De-Una-Cuenta.md) y [`CU-02001`](../../../../_legacy/2026-08-16-consolidacion-8.5/GeometriaFactory-Api/CU-02001-Registrar-El-Alta-De-Un-Alumno.md), por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1

---

## Tabla de contenido

- [1. Propósito](#1-propósito)
- [2. Actores](#2-actores)
- [3. Precondiciones](#3-precondiciones)
- [4. Flujo principal](#4-flujo-principal)
- [5. Flujos alternativos](#5-flujos-alternativos)
- [6. Excepciones y errores](#6-excepciones-y-errores)
- [7. Postcondiciones](#7-postcondiciones)
- [8. Criterios de aceptación](#8-criterios-de-aceptación)
- [9. Trazabilidad](#9-trazabilidad)
- [10. Notas y supuestos](#10-notas-y-supuestos)
- [11. Control de cambios](#11-control-de-cambios)
- [17. Compatibilidad de la superficie pública](#17-compatibilidad-de-la-superficie-pública)

---

## 1. Propósito

Que una persona que todavía no tiene cuenta quede registrada en el laboratorio con su correo, su
nombre y su apellido, **sin elegir contraseña**, y con la cuenta a la espera del acto explícito de
habilitación de un administrador.

El caso de uso abarca el camino completo, desde la petición que entra por el punto de acceso **A-02**
hasta la cuenta materializada: la verificación de que el correo esté libre, el sello de alta tomado
del reloj, la constitución de la cuenta con papel `Alumno` y situación `Pendiente`, y su
materialización en una única unidad de trabajo.

**El registro no elige contraseña, y eso no es un detalle de formulario.** Es lo que hace posible el
circuito sin canal de correo: la cuenta nace **sin credencial** y la recibe en el acto de
habilitación, con la contraseña provisoria que el sistema produce y que el administrador comunica en
persona; recién entonces la persona elige la suya. Hasta el `PRODUCT-INTAKE` 1.12 la fijaba ella
misma, sin credencial, y ése es el agujero que **RN-02016** cierra.

**Éste no es el camino de alta del administrador.** El producto tiene **dos caminos de alta con
reglas opuestas** —situación inicial, credencial y ventana de alta—, y son dos casos de uso: el
auto-registro es éste y la configuración del administrador es `CU-00025`. Ninguna regla de este
documento se le aplica a aquél.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| Una persona que todavía no tiene cuenta | Primario | Ejerce el registro desde el formulario del portal |
| `GeometriaFactory-Web` | Intermediario | Arma la solicitud y la envía servidor a servidor (RA-01). **Ningún guion del navegador alcanza el punto de acceso** |
| Administrador | Sujeto de la regla | No interviene en el alta. Su acto explícito es posterior, y es la habilitación (`CU-00023`) |
| Almacén de cuentas | Sistema | Responde si el correo está libre y materializa la cuenta constituida |
| Reloj del sistema | Sistema | Provee el sello de alta, para que sea verificable en prueba |

**Quién es el actor primario acá, y por qué cambió.** Los tres documentos que este caso de uso
consolida declaraban como actor primario al código de la capa de arriba —el portal, para el punto de
acceso; el punto de acceso, para la orquestación; la orquestación, para el dominio—. Era correcto
cuando cada capa era un proyecto de código con su propia especificación funcional. En el modelo de
unidad de entrega las capas son internas, y el actor primario es **quien ejerce la capacidad**: la
persona que se registra.

## 3. Precondiciones

- El servicio arrancó y dejó el almacén en condiciones.
- La petición **no trae acceso firmado y no lo necesita**: el registro es anónimo por diseño
  (`PRODUCT-INTAKE` **1.15** §4.1). No es una concesión, es el requisito: quien se registra todavía
  no tiene con qué identificarse.
- La solicitud aporta **correo, nombre y apellido**, y **no** aporta contraseña: la superficie del
  punto de acceso **no declara ningún campo de credencial**.
- Ya existe la cuenta de administrador, constituida en el primer arranque por `CU-00025`. Es lo que
  hace que la cuenta que este caso de uso deja `Pendiente` tenga después quién la habilite.

## 4. Flujo principal

1. La persona completa el formulario de registro con su correo, su nombre y su apellido, y lo envía.
2. Llega una petición al punto de acceso **A-02** con esos tres datos, **sin campo de contraseña**.
3. Se consulta al almacén de cuentas si ese correo ya está registrado (RN-02002, INV-01). **Esta
   verificación no la hace el dominio**, porque exige conocer el conjunto de cuentas y el dominio
   verifica sobre una entidad.
4. El correo está libre: se toma el **sello de alta** del reloj del sistema.
5. Se constituye la cuenta, declarando que la unicidad del correo fue verificada y sin aportar
   credencial ni situación. La constitución verifica que correo, nombre y apellido estén presentes y
   no vacíos, que la unicidad venga declarada como comprobada y que no se aporte credencial derivada.
6. Se fija el papel en `Alumno` y la situación en `Pendiente`, se deja la credencial derivada **sin
   valor** y el conjunto de trabajos **vacío**. Ni el papel ni la situación se eligen desde afuera.
7. La cuenta constituida se materializa en el almacén dentro de **una única unidad de trabajo**.
8. Se responde `201` con el resultado del registro, que declara la situación inicial de la cuenta: la
   persona queda informada de que su cuenta está pendiente de habilitación y **todavía no obtiene
   acceso** (RN-02006).

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | La consulta del paso 3 dice que el correo está libre, pero el almacén rechaza la materialización del paso 7 por una colisión de correo | No se materializa nada y se responde con el correo ya registrado. **La verificación previa no es una garantía por sí sola**: la unicidad efectiva la sostiene también la capa que guarda | Termina |
| FA-02 | Los datos llegan con espacios alrededor del correo o del nombre | Se conservan **tal como llegan**: no se normaliza el texto. Cómo se comparan dos correos —tal cual o normalizados— es un punto abierto declarado que resuelve `05-Arquitectura-Tecnica` junto con la capa que ejerce la verificación | Paso 3 |

## 6. Excepciones y errores

| Motivo interno | Código del contrato | Respuesta | Causa |
| --- | --- | --- | --- |
| `CORREO_YA_REGISTRADO` | `CONTRATO_CORREO_YA_REGISTRADO` | `409` | El correo ya pertenece a una cuenta. **La respuesta no declara la situación ni el papel de esa cuenta** |
| `DATO_OBLIGATORIO_AUSENTE` | `CONTRATO_CAMPO_REQUERIDO_AUSENTE` | `400` | Falta el correo, el nombre o el apellido. La respuesta **nombra el campo ausente** |
| `CREDENCIAL_NO_ADMITIDA_EN_EL_ALTA` | — | — | Se aporta una credencial derivada en el auto-registro. **Inalcanzable desde A-02 por construcción**: la superficie no declara ningún campo de contraseña |
| `ESTADO_INICIAL_NO_NEGOCIABLE` | — | — | Se pide constituir la cuenta en una situación distinta de `Pendiente`. **Inalcanzable desde A-02 por construcción**: la superficie no declara la situación |
| `PAPEL_DE_ADMINISTRADOR_FUERA_DE_ESTE_CAMINO` | — | — | Se pide constituir una cuenta con papel `Administrador` por el auto-registro. **Inalcanzable desde A-02 por construcción**: la superficie no declara el papel. El camino correcto es `CU-00025` |
| `UNICIDAD_DE_CORREO_NO_VERIFICADA` | — | — | Se solicita la constitución sin declarar que la unicidad fue comprobada. **Inalcanzable por construcción**: el paso 5 declara siempre la verificación que el paso 3 hizo |
| — | `CONTRATO_ERROR_NO_CLASIFICADO` | `503` | El almacén no está disponible. **La respuesta no incluye su ruta** |

**Por qué los cuatro motivos inalcanzables se listan igual.** Son los invariantes que la constitución
sostiene, y siguen valiendo: protegen a la cuenta de cualquier consumidor interno, no sólo del punto
de acceso. Se listan para que **su ausencia entre los códigos de respuesta no se lea como olvido**, y
porque la prueba de que son inalcanzables —que la superficie no declara esos campos— es un criterio
de aceptación de este documento y no un supuesto.

**Ninguna condición devuelve una contraseña, en claro ni derivada, y ninguna la registra.** Ninguna
deja efecto parcial: la unidad de trabajo no se abre hasta el paso 7.

## 7. Postcondiciones

- **Éxito:** existe una cuenta con correo, nombre, apellido, papel `Alumno`, sello de alta y situación
  `Pendiente`, **sin credencial derivada** y con **0 trabajos**, a la espera del acto explícito de
  habilitación. La cuenta **no admite acceso** hasta ser habilitada (INV-06, RN-02006), y la credencial
  la recibe en ese acto, no acá.
- **Fallo:** no existe ninguna cuenta nueva, ningún dato quedó a medio escribir y **el almacén queda
  como estaba**.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | La solicitud de registro del ensamblado de contratos | Se inspecciona su superficie | Declara **correo, nombre y apellido**, y **0 campos** de contraseña, de papel y de situación |
| CA-02 | Un almacén sin ninguna cuenta con el correo `ana.perez@ejemplo.edu` y un reloj fijado en 2026-03-15 | Se registra `ana.perez@ejemplo.edu`, «Ana», «Pérez» por A-02 | Responde `201`; existe **1** cuenta con papel `Alumno`, situación `Pendiente`, **sin credencial derivada**, **0 trabajos** y sello de alta 2026-03-15 |
| CA-03 | Un almacén con una cuenta cuyo correo es `ana.perez@ejemplo.edu` | Se registra el mismo correo con otro nombre | Responde `409`, el cuerpo **no declara la situación ni el papel** de la cuenta que lo ocupa, y el almacén sigue con **1** cuenta |
| CA-04 | Un almacén vacío | Se registra por A-02 con el nombre vacío | Responde `400`, el cuerpo **nombra el campo ausente** y el almacén sigue vacío |
| CA-05 | La cuenta recién registrada, en situación `Pendiente` | Se intenta canjear sus datos por un acceso firmado | **No se obtiene acceso**: la habilitación es un acto posterior y explícito |
| CA-06 | El almacén no disponible | Se registra por A-02 | Responde `503`, el cuerpo **no incluye la ruta del almacén** y no queda ninguna cuenta a medio escribir |
| CA-07 | La respuesta y el registro del servidor observados | Se registra con éxito y con fallo | **0 apariciones** de cualquier valor de credencial, de la clave de firma y de la ruta del almacén |
| CA-08 | Un almacén cuya verificación de unicidad devuelve «libre» y cuya materialización rechaza por colisión | Se registra por A-02 | Responde `409` y el almacén **no suma ninguna cuenta**: la unicidad efectiva la sostiene también la capa que guarda |
| CA-09 | Un almacén vacío | Se intenta registrar por A-02 aportando papel `Administrador`, situación `Habilitado` o una credencial derivada | Los **3** intentos son **irrepresentables en la superficie**: el ensamblado de contratos no declara esos campos, y el almacén sigue vacío |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | [NB-00002](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00002-Identidad-Propia-Del-Alumno-Sin-Correo.md), en sus criterios de circuito sin correo y de alta de punta a punta; [NB-00001](../../../../01-Necesidades-Negocio/Necesidades-De-Negocio/NB-00001-Control-De-Admision-Al-Laboratorio.md), en su criterio de admisión explícita |
| Reglas de negocio aplicables | [RN-02002](../Reglas-De-Negocio/RN-02002-Correo-Del-Alumno-Unico.md), en la verificación del paso 3 y en la traducción del correo ocupado. [RN-02001](../Reglas-De-Negocio/RN-02001-Administrador-Unico-Y-Papeles-Fijos.md), en que este camino no constituye administradores. [RN-02006](../Reglas-De-Negocio/RN-02006-Cuenta-Pendiente-O-Bloqueada-Sin-Acceso.md), en la situación inicial que el alta fija y en que no otorga acceso |
| Regla de arquitectura del producto | **RA-01**, que el portal ejerce servidor a servidor aunque el punto no exija acceso; **RA-03**, en las condiciones de §6 |
| Puntos de acceso | **A-02** |
| Contrato de uso que transporta | `GeometriaFactory-Contracts` `CU-00002` |
| Puertos que consume | Almacén de cuentas, reloj del sistema |
| Historias de usuario a generar en 06 | US-00007 |
| Componentes esperados en 05 | El punto de acceso; el caso de uso de auto-registro con su resultado tipado; el contrato del puerto de almacén de cuentas; el contrato del puerto de reloj |
| Tests previstos en 08 | Integración por los **nueve** criterios, con almacén simulado para el correo libre, el correo ocupado, el dato ausente, la colisión en la materialización y el almacén caído; inspección de la superficie del ensamblado por CA-01 y CA-09; e inspección de que ninguna traza contiene credenciales |

## 10. Notas y supuestos

- **La unicidad del correo se verifica en la orquestación y no en la constitución**, porque exige
  conocer el conjunto de cuentas y la constitución verifica sobre una entidad. El invariante sigue
  existiendo en las dos capas, y por eso `UNICIDAD_DE_CORREO_NO_VERIFICADA` figura en §6 aunque sea
  inalcanzable.
- **La situación inicial no la elige quien pide el alta.** Se fija distinta en cada camino:
  `Pendiente` en el auto-registro y `Habilitado` en la configuración del administrador. RN-02001 e
  INV-05 **no fundamentan ninguna situación inicial**: declaran la unicidad del administrador y la
  ventana en la que su alta es posible.
- **El sello de alta es un metadato de orquestación.** No se confunde con la «Fecha» que el alumno
  declara en su trabajo, que es dato del alumno.
- **El producto no envía correo, y este caso de uso es donde eso se nota.** El intake §9 X-1 declara
  la exclusión y sigue vigente: ninguna contraseña se transporta por un canal del sistema hacia la
  persona. Incorporar el envío de correo cambiaría el flujo de alta entero.
- **Este caso de uso no verifica pertenencia ni facultad**, porque quien lo ejerce todavía no tiene
  cuenta. La protección del alta del administrador vive en `CU-00025`.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-16 | Emisión inicial, como **caso de uso consolidado** de la unidad de entrega por `Audit/Migracion-8.5-Consolidacion-Decidida.md` 1.2 §2.1. Absorbe el punto **A-02** de `CU-00003` 1.5, `CU-04001` 1.0 y `CU-02001` 1.4, que eran **tres vistas por capa de la misma capacidad**. La unión no es la suma: el actor primario pasa a ser la persona que se registra, en lugar del código de la capa de arriba, y §2 registra por qué; el flujo es de punta a punta, de la petición a la cuenta materializada; §6 declara los motivos internos y su traducción a respuesta **en una sola tabla**, con los cuatro que la superficie vuelve **inalcanzables por construcción** marcados como tales en lugar de omitidos; y los criterios de aceptación se rehacen sobre la capacidad, con **CA-09** nuevo, que verifica en la superficie lo que las tres vistas verificaban por separado en cada capa. Los tres documentos absorbidos quedan archivados en `_legacy/2026-08-16-consolidacion-8.5/` y citados desde la cabecera. |

## 17. Compatibilidad de la superficie pública

Agregar datos **opcionales** al alta es compatible. Agregar un campo de contraseña, de papel o de
situación **no lo es**: reabre el defecto que RN-02016 cerró y contradice CA-01 y CA-09. Dejar de
verificar la unicidad del correo antes de constituir contradice RN-02002. Declarar en la respuesta
del `409` la situación o el papel de la cuenta que ocupa el correo contradice CA-03, porque convierte
el punto en un oráculo de qué correos están registrados y con qué papel.
