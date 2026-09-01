# Medición — cuánto volumen sostiene el listado sin paginación

**Producto:** Fábrica de Geometría
**Documento:** Medicion-Volumen-De-Comision-2026-08-31.md
**Versión:** 1.1
**Fecha:** 2026-08-31
**Instrumento:** [`tools/medicion-volumen-de-comision.sh`](../../../tools/medicion-volumen-de-comision.sh)
**Estado:** **Emitido.** Mide el servicio de datos; **no cierra `PT-05`**

---

## 0. El resultado, primero

> **A mil trabajos y trescientos treinta y cuatro alumnos, el listado de la comisión responde con un
> p99 de 12,3 ms contra un umbral de 500. Margen de cuarenta veces.**
>
> **El servicio de datos no es el límite del volumen, y por un margen que no admite discusión.**

| Trabajos | Alumnos | p50 | p99 | máx | Peso | B/fila | p99 vs 500 ms |
|---:|---:|---:|---:|---:|---:|---:|---|
| 30 | 10 | 2,5 ms | **5,5 ms** | 5,5 ms | 7,6 KB | 257 | **PASA** |
| 102 | 34 | 2,2 ms | **2,8 ms** | 2,8 ms | 26 KB | 259 | **PASA** |
| 300 | 100 | 3,1 ms | **5,2 ms** | 5,2 ms | 77 KB | 259 | **PASA** |
| 1002 | 334 | 8,0 ms | **12,3 ms** | 12,3 ms | 257 KB | 262 | **PASA** |

**Control · listado propio de un alumno** (3 trabajos): p50 1,6 ms · p99 2,1 ms · 790 B. **Se queda
quieto mientras el de la comisión crece por dos órdenes de magnitud**, que es lo que corresponde: su
alcance tiene tope natural. Si los dos hubieran crecido igual, el problema no sería el volumen sino la
consulta.

---

## 1. Por qué se midió esto, y no lo que la pregunta decía

**`D5` está cerrada desde el 2026-08-20, y se cerró declarando que el dato no se puede saber.** El
Product Owner resolvió que el volumen de la comisión **no se sabe ni se puede saber de antemano** y que
**no se fija número** — commit `b4a4804`, sobre cinco documentos.

**Ese cierre es correcto y esta medición no lo discute.** Lo que hace es atacar lo que el cierre dejó
abierto, que no es la pregunta sino su consecuencia:

> El **caudal de 20 peticiones por minuto** de `Web/05` §8 **se derivaba** de «una comisión operando
> durante una clase». Sin tamaño conocido, **ese fundamento se cayó**. El número quedó **provisorio**.

Con el volumen declarado incognoscible, **preguntar «cuántos alumnos» dejó de tener sentido y la
pregunta útil pasó a ser otra**:

> **¿A partir de qué volumen el listado deja de servir?**

Ésa **sí se mide**, y no necesita saber el tamaño de ninguna comisión real. Es la diferencia entre un
pendiente que espera un dato que nadie tiene y uno que espera que alguien corra un guion.

---

## 2. Método

**Un almacén propio y efímero, un puerto propio, todo en contenedor.** No toca el almacén de trabajo ni
los contenedores del Product Owner: la ruta llega por `ConnectionStrings__Store` a un archivo bajo
`mktemp -d` que se borra al salir, y el puerto se fija por `Kestrel__Endpoints__Http__Url`
(`scripts/store-path.sh` documenta por qué esto importa: el 2026-08-15 una corrida de guiones se llevó
la cuenta de administrador del Product Owner).

**La forma del dato es la de una comisión y no la de una prueba de carga:** muchos alumnos con pocos
trabajos —tres cada uno—, dados de alta por el circuito real del producto, con su habilitación, su
contraseña provisoria y su cambio de contraseña. El texto de cada trabajo es el **escenario `E1` del
seed**, que es un dato real del producto y no una cadena inventada.

**Se mide `GET /trabajos` pedido por el administrador**, que es el listado de la comisión entera y el
único sin tope. Treinta repeticiones por corte, con dos llamadas de calentamiento que **no se cuentan**:
la primera pide el plan de consulta y mediría el arranque, no el listado.

**El percentil se calcula sobre los tiempos ordenados y no sobre el promedio.** Un listado que casi
siempre responde rápido y de vez en cuando no es exactamente el caso que el umbral quiere atrapar, y un
promedio lo esconde.

**El umbral no lo inventa el instrumento:** es el **p99 de 500 ms** de `PRODUCT-INTAKE` §22 `A-5`,
**confirmado por el Product Owner el 2026-08-26** con la decisión `D1`.

---

## 3. Lo que además quedó verificado, y no era el objetivo

**El peso por fila se mantiene entre 257 y 262 bytes en los cuatro cortes.** `A-5` declara un NFR
**estructural** para `GeometriaFactory-Contracts`: que el payload de listado **no lleve el
`OriginalJson` ni los componentes de las piezas**. Si ese NFR estuviera roto, el peso por fila crecería
con el tamaño del texto que cada alumno pegó — y **todos los trabajos de esta medición llevan el mismo
texto real de `E1`, que no es corto**.

**Se cumple.** Es una verificación barata que este instrumento hace de paso, y que **ninguna prueba del
producto estaba haciendo sobre volumen**: las que existen lo verifican por forma, sobre un caso.

---

## 4. Qué NO contesta esta medición, dicho antes de que alguien la use de más

**No mide la pieza pública, y ahí es donde el diseño puso su suposición.** El «supone decenas y no
cientos» de `Web/05` §11 `PA-06` es sobre **la superficie**, no sobre el servicio: habla de pintar filas
agrupadas por alumno, no de devolver JSON. Esta medición dice que **el servicio de datos no va a ser el
motivo** de un rediseño por volumen; no dice nada sobre el otro lado.

~~**Y el otro lado tiene una particularidad que conviene tener presente sin exagerarla.** […] Lo único
que corresponde anotar es que **pintar un listado grande es uno de esos actos discretos**, y su costo del
lado del front **no está medido**.~~

> **CORREGIDO el 2026-08-31, el mismo día, en la emisión 1.1.** Este apartado afirmaba que **pintar el
> listado viaja por el circuito de la sesión interactiva**, y **es falso para esa superficie**.
> `ClassSubmissionList.razor` **no declara `@rendermode`** —la pieza pública registra el modo interactivo
> y **sólo seis lo adoptan**, y éste no está —contados el 2026-08-31 en `ADR-10001` §2.1; una cuenta anterior decía ocho porque incluía dos que **mencionan** `@rendermode` en prosa sin declararlo—, y su filtro es un `<form method="get">`. El
> propio código lo dice: *«los dos viajan por la dirección porque esta superficie es de render
> estático»*. **La sesión interactiva no participa.**
>
> **La consecuencia es buena y por eso importa corregirlo:** el comportamiento del listado ante el
> volumen **no depende del transporte**, de modo que el repliegue a long polling del hosting —medido,
> aceptado y cerrado como `PT-01.b`— **no lo afecta**. Lo que se mide en local es **representativo**,
> salvo la latencia de red, y **no un piso**.
>
> **Y el costo del front ya está medido**, desde el mismo día:
> [`Medicion-Pintado-Del-Listado-2026-08-31.md`](Medicion-Pintado-Del-Listado-2026-08-31.md) — **96 ms a
> treinta trabajos, 1,6 s y 1,2 MB a mil**, con un hallazgo propio: el esqueleto que el wireframe declara
> para esta pantalla **no existe, y bajo render estático no puede existir**.

**Lo que del párrafo original sigue en pie, y no es poco.** El hosting **no ofrece WebSocket** y la
sesión va por *long polling*: está medido y **funciona** —`PT-01.b` en amarillo estable, `PT-01.c`
cerrado sobre el hosting real con veinte minutos de navegación continua sin un solo aviso de
reconexión—. El intake lo declara aceptado, con el fundamento de que la interacción continua —rotar,
acercar, recorrer el árbol— **no viaja por el circuito** porque el visor resuelve todo en el navegador.
**Eso no cambia.** Lo que cambia es que **el listado tampoco viaja por ahí**, y por lo tanto la reserva
que este apartado dejaba abierta sobre él **no tenía objeto**.

**Y no cierra `PT-05`.** `PT-05` mide la premisa completa de la topología **sobre uso real desde la
facultad**, con alumnos de verdad y una red de verdad. Esto es un servicio en `localhost` contra un
almacén sembrado. **Son cosas distintas y la segunda no reemplaza a la primera.**

---

## 5. Qué se puede hacer con esto

| Si… | Entonces |
|---|---|
| La comisión resulta de **decenas** | Nada. El diseño acertó y el servicio sobra |
| La comisión resulta de **cientos** | **El servicio sigue sobrando.** Lo único a revisar es la superficie, y `PA-06` ya declara que el cambio es **acotado** y que la superficie afectada es `Listado-De-La-Comision` |
| El caudal provisorio de 20 pet/min hay que revisarlo | Esta medición **no lo fija** —lo fija `PT-05`— pero acota el riesgo: a 334 alumnos, el listado responde en 12 ms |

**Extrapolación, declarada como tal.** Los cuatro puntos crecen de forma aproximadamente lineal —≈1,2 ms
de p99 por cada cien trabajos—, lo que ubicaría el umbral de 500 ms cerca de los **cuarenta mil
trabajos**. **Es una extrapolación de tres órdenes de magnitud y no una medición**: el instrumento acepta
otros cortes por `GF_MEDICION_CORTES` y quien necesite el número real lo corre.

---

## 6. Cómo se reproduce

```bash
docker run --rm -u "$(id -u):$(id -g)" -e HOME=/tmp --network host \
  -v "$PWD:/repo" -w /repo mcr.microsoft.com/dotnet/sdk:10.0 \
  bash tools/medicion-volumen-de-comision.sh
```

Variables: `GF_MEDICION_CORTES` (por omisión `30 100 300 1000`), `GF_MEDICION_REPETICIONES` (30),
`GF_MEDICION_PUERTO` (5099).

---

## 7. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.1 | 2026-08-31 | **Corrige §4 el mismo día de la emisión.** Este informe advertía que **pintar el listado viaja por el circuito de la sesión interactiva**, y **es falso**: `ClassSubmissionList.razor` es de **render estático** —no declara `@rendermode`, y su filtro es un `<form method="get">`—, con lo cual la sesión interactiva no participa. **La consecuencia es buena**: el comportamiento del listado ante el volumen **no depende del transporte**, y el repliegue a long polling del hosting no lo afecta. La reserva que §4 dejaba abierta sobre el front **no tenía objeto**, y el costo real **ya está medido** en [`Medicion-Pintado-Del-Listado-2026-08-31.md`](Medicion-Pintado-Del-Listado-2026-08-31.md). **Lo que del párrafo sigue en pie se conserva**: el hosting no ofrece WebSocket, la sesión va por long polling, está medido y funciona. | Orquestador SDD |
| 1.0 | 2026-08-31 | Emisión inicial. Mide `GET /trabajos` del administrador en cuatro cortes —30, 102, 300 y 1002 trabajos, de 10 a 334 alumnos— contra el umbral de **p99 ≤ 500 ms** que `D1` confirmó el 2026-08-26. **El servicio de datos no es el límite del volumen: 12,3 ms a mil trabajos, margen de cuarenta veces.** Verifica de paso, y sin proponérselo, el **NFR estructural** de `A-5` para `GeometriaFactory-Contracts`: el peso por fila se mantiene entre 257 y 262 bytes en los cuatro cortes, o sea que el payload de listado **no arrastra el `OriginalJson`** aunque los mil trabajos lleven el mismo texto real de `E1`. Declara explícitamente lo que **no** contesta: el costo de pintar el listado del lado de la pieza pública, que exige el front publicado, y `PT-05`, que mide sobre uso real desde la facultad. Nace de que **`D5` se cerró por incognoscible** el 2026-08-20 y la pregunta útil dejó de ser «cuántos alumnos» para pasar a ser «cuánto sostiene». | Orquestador SDD |
