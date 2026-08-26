# ADR-14003 — La dirección del backend viaja como IP pública dinámica, y se actualiza a mano

**Producto:** Fábrica de Geometría
**Documento:** ADR-14003-Direccion-Del-Backend-Por-IP-Dinamica.md
**Versión:** 1.2
**Estado:** **Aceptado** — aprobado por el Product Owner el 2026-08-18
**Fecha:** 2026-08-18
**Autor:** Orquestador SDD
**Nivel:** Producto
**Tipo:** **Apartamiento declarado** (`Root-Rules.md` §11)
**Alcanza a:** el secreto `API_BASE_URL` del flujo `deploy-front-ftp.yml`; `Entornos-Deploy.md` §3; la fase `i` del roadmap y su puerta `PT-05`

---

## 1. Contexto

**La topología del producto la impone una restricción de red, no una preferencia.** El intake
declara en `RN-B1` el riesgo que la ordena: **los alumnos no pueden alcanzar la aplicación desde la
red de la facultad**, que es el escenario de uso previsto, porque las redes de la facultad y otras
redes privadas **bloquean por política** el acceso a direcciones residenciales.

De ahí la partición: la pieza pública se publica en un hosting alcanzable desde cualquier red
—somee.com— y **es ella la que alcanza al servicio de datos**, que corre en un servidor propio. El
navegador del alumno nunca toca el servidor propio: el front habla con la API **desde su propio
servidor**, servidor a servidor.

**Y ahí aparece lo que este apartamiento declara.** El servidor propio **no tiene IP pública
estática**. La tiene dinámica, y el proveedor la rota cuando quiere.

El front recibe la dirección del servicio de datos por el secreto `API_BASE_URL`, que el flujo de
publicación **escribe dentro de `appsettings.json` en el momento de publicar**. `Program.cs` la lee
**una sola vez, al arrancar**.

---

## 2. Decisión

**Se acepta que la dirección del backend sea una IP pública dinámica, y que actualizarla sea un acto
manual del Product Owner**, en lugar de resolverlo con un nombre DDNS.

Cuando la IP cambia, el procedimiento es: actualizar el secreto `API_BASE_URL`, subir por FTP el
`appsettings.json` con la dirección nueva y reiniciar la aplicación en el panel del hosting.

---

## 3. Motivo

**Configurar DDNS es trabajo de infraestructura y no de este producto**, y el Product Owner decidió
—el 2026-08-18— **ver el laboratorio funcionando antes** de agregarle una pieza de infraestructura
más. Es una decisión de orden, no de rechazo: el DDNS es la solución correcta y está declarada como
el disparador que supera este apartamiento (§6).

**Y el costo real es más bajo de lo que parece**, por dos propiedades que el producto ya tiene:

1. **No hace falta volver a publicar entero.** Como `ApiBaseUrl` se lee al arrancar y vive en un
   archivo de configuración, alcanza con **subir ese archivo** y reiniciar: segundos, sin
   recompilar ni reempaquetar el visor. El flujo completo se reserva para cuando cambia el código.
2. **La falla es visible y no silenciosa.** `ADR-10005` trata el estado degradado como **superficie**:
   si el servicio de datos no responde, el front lo muestra como estado y no se rompe.

---

## 4. Consecuencias

**A favor.** El laboratorio funciona hoy, con la infraestructura que hay, y la restricción de red
que motivó la topología queda resuelta: el alumno alcanza el front desde la facultad, y el front
alcanza al backend desde Internet.

**En contra, y es real.** **Entre que la IP rota y que alguien la actualiza, el laboratorio no
funciona para nadie.** Nada avisa: el front carga, responde `200`, y falla al hablar con la API.
El aviso llega por una persona que no puede entrar.

**Y hay una consecuencia sobre la medición de `PT-05`** que conviene anticipar: la puerta se mide
**contra una dirección que puede cambiar**. Una medición que pasó ayer no dice nada sobre hoy si la
IP rotó en el medio. Por eso la medición **registra la dirección usada y su fecha**, para que una
puerta en verde no se lea como una garantía permanente.

**Lo que este apartamiento NO cubre.** No dice nada sobre si el hosting permite la conexión saliente
—eso está verificado por evidencia: el despliegue ya funcionó y el front alcanzó al backend— ni
sobre el reenvío del router, que es del proyecto de contenedor.

---

## 5. Alternativas descartadas

| Alternativa | Por qué se descarta hoy |
| --- | --- |
| **Nombre DDNS en `API_BASE_URL`** | **Es la solución correcta y no se descarta: se difiere.** Es el disparador declarado en §6. La decisión del Product Owner es ver algo funcional primero, y agregar infraestructura después |
| **IP pública estática** | No se tiene, y conseguirla no depende del producto |
| **Que el front resuelva la dirección en cada arranque desde un servicio externo** | Agrega una dependencia de tiempo de ejecución hacia afuera para resolver un problema que un nombre DDNS resuelve sin ninguna. Y `PT-03` ya fijó el criterio para el visor: lo que el producto necesita, lo lleva adentro |
| **No declarar nada y arreglarlo cuando pase** | Es lo que este ADR existe para evitar. Un apartamiento sin declarar **se evalúa como omisión y no como decisión** (`Root-Rules.md` §11), y el día que el laboratorio se caiga nadie va a saber si fue previsto |

---

## 6. Estado del apartamiento

**Campos 4, 5 y 6 de `Root-Rules.md` §11.**

| Campo | Valor |
| --- | --- |
| **4 · Disparadores que superarían la decisión** | Cualquiera de los dos: que el servidor propio obtenga una **IP pública estática**, o que se configure un **nombre DDNS** y `API_BASE_URL` pase a apuntar a ese nombre. En los dos casos la actualización manual deja de hacer falta y este apartamiento queda superado |
| **5 · Estado** | **`vigente`** |
| **6 · Saltos de versión que sobrevivió** | **2** — se emitió en el conjunto **9.12** y sobrevivió **9.12 → 10.0** y **10.0 → 13.3**. **Revisado por la fase M4 de la migración 10.0 → 13.3, el 2026-08-25**, con resultado **no contemplado**: el campo 4 se contrastó contra las entradas **10.1 a 13.3** del `CHANGELOG.md` del framework y ninguna cumple el disparador. El incremento es de **+2** y no de +1 porque **la migración 9.12 → 10.0 no corrió esta revisión** —ni su plan ni su informe nombran la palabra «apartamiento»—, de modo que ese salto le pasó por encima sin contarse (`Audit/Plan-Migracion-10.0-a-13.3.md` §5.1) |

**Qué pasa si el contador llega a 2.** `Migracion-Rules.md` §4.7 declara que un apartamiento que
sobrevive dos o más saltos sin ser contemplado ya demostró que **no es de un producto**. Acá ese
umbral tendría una lectura propia y vale anticiparla: no significaría que el framework deba
contemplarlo, sino que **el laboratorio lleva dos saltos de versión funcionando con una dirección
que hay que actualizar a mano**, y que el DDNS dejó de ser «después» para ser una deuda.

---

## 7. Control de cambios

| Versión | Fecha | Cambios |
| --- | --- | --- |
| 1.1 | 2026-08-18 | **Aprobado por el Product Owner.** Pasa de `Propuesto` a `Aceptado` sin ninguna modificación de su contenido: el fundamento, el disparador, el estado y el contador quedan como se emitieron. Con la aprobación, el apartamiento **cuenta como decisión y no como omisión** (`Root-Rules.md` §11), que es la diferencia que este ADR existe para producir. |
| 1.0 | 2026-08-18 | Emisión inicial. Declara como apartamiento que la dirección del servicio de datos viaje como **IP pública dinámica** y se actualice a mano, decidido por el Product Owner el 2026-08-18 con el fundamento de ver el laboratorio funcionando antes de sumar infraestructura. Registra el procedimiento de actualización —subir `appsettings.json` y reiniciar, sin republicar entero—, las dos propiedades que abaratan el costo, y la consecuencia sobre `PT-05`: la medición registra la dirección usada y su fecha, porque una puerta en verde sobre una dirección que puede cambiar no es una garantía permanente. Disparador declarado: IP estática o DDNS. Contador en **0**. |
| 1.2 | 2026-08-25 | **Primera revisión de este apartamiento, en la fase M4 de la migración 10.0 → 13.3** (`Migracion-Rules.md` **3.19** §4.7). **Resultado: no contemplado**: los dos disparadores del campo 4 —IP pública estática o nombre DDNS— son **infraestructura del destino**, y ninguna versión del framework los alcanza. El contador pasa de **0 a 2**: el ADR se emitió en el conjunto **9.12** y **la migración 9.12 → 10.0 no corrió la revisión**, de modo que su campo 6 siguió declarando «ninguna migración lo revisó todavía» hasta hoy. **Con el contador en 2 cruza el umbral**, y acá la lectura es la que el propio §6 anticipaba: lo que el número dice es que **el laboratorio lleva dos saltos de versión funcionando con una dirección que puede cambiar sola**. **Levantado por el audit de M6 como P0.** |
