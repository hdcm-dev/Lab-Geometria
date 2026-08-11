# CU-10 — Componer la aplicación y conectar los puertos con sus adaptadores

**Producto:** Fábrica de Geometría
**Proyecto de código:** GeometriaFactory-Api
**Documento:** CU-10-Componer-La-Aplicacion-Y-Conectar-Los-Puertos-Con-Sus-Adaptadores.md
**Versión:** 1.2
**Estado:** Propuesto
**Fecha:** 2026-08-11
**Autor:** Analista Funcional + API Designer (AG-02)
**Trazabilidad upstream:** `PRODUCT-INTAKE-Fabrica-De-Geometria.md` **1.26** §13 (dependencias y orden topológico), §14 (contratos entre proyectos de código; RA-03), §17.5.P.1, §17.5.P.2 («más la composición de raíz que conecta puertos con adaptadores»), §17.5.P.4, §17.5.P.5 (secretos por variable de entorno o archivo montado), §17.5.P.8, §17.5.P.11; `PRODUCT-MANIFEST-Fabrica-De-Geometria.md` **1.3** §2 y §3; `Proyectos/GeometriaFactory-Application/02-Especificacion-Funcional/Especificacion-Funcional.md` §3, que declara **los cuatro puertos**; `Proyectos/GeometriaFactory-Infrastructure/02-Especificacion-Funcional/Especificacion-Funcional.md` §3, que declara los cuatro adaptadores, los dos mecanismos y la responsabilidad de arranque
**Trazabilidad downstream:** `03-UX-UI-DX`, `05-Arquitectura-Tecnica`, `06-Backlog-Tecnico`, `08-Calidad-Y-Pruebas` y `09-Devops` de GeometriaFactory-Api

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

---

## 1. Propósito

Declarar la **composición de raíz**: el único lugar del producto donde los puertos que declara `GeometriaFactory-Application` se encuentran con los adaptadores que los implementan en `GeometriaFactory-Infrastructure`, y donde la configuración que el despliegue provee entra al proceso.

Es lo que el intake §17.5.P.2 declara junto a los puntos de acceso, y es lo que hace que **todo lo demás del backend sea probable sin nada**: las tres capas de adentro se prueban con dobles precisamente porque **ninguna de ellas conoce a la que la implementa**. La factura de esa propiedad se paga acá, una sola vez.

También es el único lugar del proceso donde entran los dos valores que el repositorio de código **no contiene**: la ubicación del almacén y la clave de firma. El intake §17.5.P.5 y §17.5.P.8 son explícitos: **ningún secreto entra al repositorio, ni siquiera en la construcción automatizada**.

Este caso de uso **no es un punto de acceso** y no atiende ninguna petición. No traza a ninguna necesidad de negocio, y el índice maestro §7.2 declara por qué: conectar un puerto con su adaptador es construcción y no capacidad.

## 2. Actores

| Actor | Tipo | Rol |
| --- | --- | --- |
| El proceso de este proyecto de código | Primario | Ejerce la composición una vez, al construirse, antes de atender ninguna petición |
| Operador del despliegue | Sistema | Provee la ubicación del almacén y la clave de firma **por variable de entorno o por archivo montado** |
| Las tres capas ensambladas | Sistema | Ninguna se conoce entre sí: **es esta composición la que las conecta** |

## 3. Precondiciones

- El proceso está arrancando y todavía no atiende peticiones.
- La configuración está disponible en el entorno del proceso.

## 4. Flujo principal

1. Se conectan **los cuatro puertos** que la capa de aplicación declara con los adaptadores que los implementan: el repositorio de trabajos, el repositorio de cuentas, la validación de figuras y el reloj del sistema.
2. Se conectan **los dos mecanismos** que la capa de infraestructura provee y que no son puertos: el de credenciales y el del acceso firmado.
3. Se toma de configuración **la ubicación del almacén**, que en producción apunta a un volumen persistente y **nunca al interior de la imagen**.
4. Se toma de configuración **la clave de firma**, por variable de entorno o por archivo montado.
5. Se conectan los casos de uso de la capa de aplicación, que quedan disponibles para los puntos de acceso.
6. La composición termina y el proceso pasa al arranque (CU-11).

**El alcance de la unidad de trabajo llega declarado desde adentro** —un caso de uso, una unidad de trabajo— y esta composición lo respeta: **no abre ninguna por su cuenta y no comparte una entre operaciones**.

## 5. Flujos alternativos

| Id | Disparador | Desarrollo | Punto de retorno |
| --- | --- | --- | --- |
| FA-01 | El proceso se construye para una prueba de integración | La composición es **la misma**, con la ubicación del almacén apuntando a un almacén de prueba. Es lo que permite que las pruebas de integración golpeen el servicio real; el intake declara esa pirámide invertida a propósito, porque **lo que este proyecto de código aporta es cableado, y el cableado se verifica ejerciéndolo** | Paso 6 |
| FA-02 | El proceso se ejecuta en desarrollo | Escucha por HTTP **sin certificado**, para evitar la fricción del certificado de confianza dentro del contenedor de desarrollo. **Es una decisión declarada de la fuente y no un descuido** | Paso 6 |

## 6. Excepciones y errores

Este contrato no produce respuestas: **falla antes de que exista ninguna petición que responder**. Sus dos condiciones detienen la construcción.

| Condición | Qué ocurre | Por qué no hay salida alternativa |
| --- | --- | --- |
| **Clave de firma no provista** | La construcción no se completa y **el servicio no atiende**. No se genera una clave al vuelo y no se emite sin firmar | El sistema arrancaría, emitiría accesos y **nadie lo notaría hasta que alguien falsifique uno**. Una clave generada al vuelo, además, invalida todos los accesos en cada reemplazo de versión, con lo cual el síntoma visible es otro y el diagnóstico se va para otro lado |
| **Ubicación del almacén no provista o no utilizable** | La construcción no se completa y **el servicio no atiende**. No se cae hacia una ruta alternativa dentro de la imagen | El servicio arrancaría, aceptaría los trabajos de la comisión entera **y los perdería en el siguiente reemplazo de versión**. Nadie se entera hasta que alguien busca su trabajo y no está |

**Las dos fallan hacia el lado seguro, y las dos son atajos tentadores porque no fallan.** Es el mismo patrón que la capa de infraestructura declara sobre sus propias condiciones: cuando el mecanismo no puede cumplir su promesa, **se detiene y lo dice; no la cumple a medias**.

**Ningún mensaje de estas dos condiciones incluye la ruta que se intentó ni el lugar del que se esperaba leer la clave.** Es RA-03, y vale también para lo que se escribe en el registro de arranque, que es lo que el operador lee.

## 7. Postcondiciones

- **Éxito:** los cuatro puertos y los dos mecanismos están conectados, la configuración está tomada y **ningún secreto quedó escrito en ninguna parte del repositorio de código ni de la imagen**.
- **Fallo:** el proceso **no atiende ninguna petición**. No hay un estado intermedio en el que el servicio atienda con la mitad de sus adaptadores.

## 8. Criterios de aceptación

| ID | Given | When | Then |
| --- | --- | --- | --- |
| CA-01 | El proceso construido | Se inspecciona la composición | Los **4** puertos de la capa de aplicación tienen adaptador conectado y **0** quedan sin conectar |
| CA-02 | El proceso construido | Se inspeccionan los mecanismos | Los **2** mecanismos que no son puertos —credenciales y acceso firmado— están conectados |
| CA-03 | Un arranque sin clave de firma provista | Se construye el proceso | **No atiende ninguna petición**, y **0 accesos** se emiten. En particular **no** se generó ninguna clave al vuelo |
| CA-04 | Un arranque con la ubicación del almacén apuntando a un lugar no escribible | Se construye el proceso | **No atiende ninguna petición**, y **0 almacenes** se crearon dentro de la imagen |
| CA-05 | El repositorio de código y la imagen de despliegue | Se inspeccionan | **0 apariciones** de una clave de firma y **0** de una ruta de almacén de producción |
| CA-06 | Los mensajes de las dos condiciones de §6 y el registro de arranque | Se inspeccionan | **0 apariciones** de la ruta intentada y del lugar del que se esperaba leer la clave |
| CA-07 | El proceso construido para una prueba de integración | Se compara con el de producción | La composición es **la misma**: 0 adaptadores distintos, y lo único que cambia es la ubicación del almacén |

## 9. Trazabilidad

| Dimensión | Referencia |
| --- | --- |
| Necesidad de negocio | **Ninguna**, y el índice maestro §7.2 lo declara con su motivo |
| Reglas de negocio aplicables | **Ninguna directamente.** Lo que este contrato sostiene es la **condición** de que todas las demás se puedan ejercer: sin la composición, ningún caso de uso llega a su adaptador |
| Regla de arquitectura del producto | **RA-03**, en los mensajes de sus dos condiciones y en el registro de arranque |
| Puntos de acceso | Ninguno |
| Historias de usuario a generar en 06 | US-26 |
| Componentes esperados en 05 | Composición de raíz; lectura de configuración; y la decisión de qué herramienta la resuelve, que es de aquella categoría |
| Tests previstos en 08 | Los siete criterios: **cuatro son de inspección estructural** —puertos conectados, mecanismos conectados, repositorio e imagen sin secretos, composición idéntica en prueba— y tres son de arranque fallido |

## 10. Notas y supuestos

- **El repositorio de cuentas no tiene identificador declarado por ninguna fuente.** El intake nombra tres puertos y no éste; `GeometriaFactory-Application` lo declara punto abierto y lo nombra en lenguaje de dominio, y `GeometriaFactory-Infrastructure` **no lo reabre**. Esta categoría tampoco: lo nombra igual y lo cita como abierto en el índice maestro §11. **Los puertos son cuatro**, con tres nombres declarados y uno por declarar.
- **La composición es el único lugar del producto donde las tres capas se conocen**, y es lo que hay que poder señalar cuando alguien pregunta por qué el dominio no referencia la persistencia. La regla de dependencias apunta hacia adentro; acá se cierra el círculo, desde afuera y una sola vez.
- **Los nombres de tipos y de espacios de nombres están abiertos aguas arriba** y se validan en el punto de control de la primera etapa. Esta categoría **no los fija** y nombra cada puerto por lo que hace.
- **Las versiones exactas de los paquetes se anclan en la primera etapa**, por la regla de anclaje del producto: toda versión se fija explícitamente y un cambio mayor es una decisión que se documenta, nunca el efecto colateral de una actualización.
- **La composición idéntica entre prueba y producción es una propiedad y no una casualidad.** Si la prueba de integración conectara adaptadores distintos, dejaría de verificar lo único que este proyecto de código aporta.

## 11. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.0 | 2026-08-10 | Emisión inicial. |
| 1.1 | 2026-08-10 | **Realineación de la cita viva al `PRODUCT-INTAKE` 1.13.** Este proyecto de código se emitió contra la **1.12** y la fuente está hoy en **1.13**, que incorpora la regla **RN-16** —habilitar una cuenta produce su contraseña provisoria— y precisa la capacidad **F-04**. La cabecera de trazabilidad pasa a citar **1.13**; la cita de la emisión inicial se conserva en la fila 1.0, que es trazabilidad y no una referencia desactualizada. **Ninguna sección de este contrato de uso se toca**: la decisión de 1.13 alcanza al circuito de credenciales y este caso de uso no lo expone. Sube minor: corrige una cita de trazabilidad. |
| 1.2 | 2026-08-11 | **Cierra el hallazgo `B-API-13` (P3)** del informe [`B-02-03-GeometriaFactory-Api-r1.md`](../../../../Audit/B-02-03-GeometriaFactory-Api-r1.md) 1.0, en la extensión que la búsqueda de propagación que el propio informe exige dejó al descubierto: la cabecera citaba `PRODUCT-INTAKE` **1.13** y `PRODUCT-MANIFEST` **1.3** y pasa a citar **1.26** y `PRODUCT-MANIFEST` **1.3**, vigentes hoy. El informe listaba **nueve** cabeceras envejecidas y sólo una de esta carpeta, `CU-12`; el `grep` sobre las categorías 02 y 03 devuelve **diecinueve** archivos con la cita vieja, **los doce casos de uso entre ellos**, y los diecinueve se corrigen en esta tanda. Se abrieron las secciones del intake que este caso de uso cita y **su contenido no cambió** entre 1.13 y 1.26 en nada que este documento afirme, de modo que **no había ninguna afirmación falsa**: lo que se repara es la trazabilidad. **Ningún paso, código, regla, criterio de aceptación ni recuento cambia.** Sube minor. |
