# La fase `i` — qué contesta, qué se aparcó ahí, y cuatro decisiones que no la esperan

**Producto:** Fábrica de Geometría
**Documento:** Fase-i-Que-Contesta-Y-Que-No-2026-08-31.md
**Versión:** 1.0
**Fecha:** 2026-08-31
**Instrumento:** barrido de los ítems con evento en la fase `i` sobre el corpus vivo
**Estado:** **Emitido.** Clasifica y **presenta cuatro decisiones**; no toma ninguna

---

## 0. El resultado, primero

> **Diez asuntos están atados a la fase `i`. Seis la necesitan. Cuatro no.**
>
> Los cuatro que no la necesitan **son decisiones del Product Owner que se pueden tomar hoy**, y están
> ahí por la misma razón: **el último punto de control es el destino por defecto de todo lo que no tiene
> evento propio.**

| | Asuntos | Quién los cierra |
|---|---|---|
| **Los que la fase `i` contesta** | **6** | El destino real, midiendo |
| **Los que se aparcaron ahí** | **4** | **El Product Owner, cuando quiera** |

**Si los cuatro se deciden, la fase `i` arranca con seis asuntos en vez de diez, y los seis son suyos.**

---

## 1. Por qué existe este documento

**La escalada `E-04` de la mesa del 2026-08-27 hizo lo correcto, y este documento no la discute.** Cuatro
filas declaraban «falta declarar el evento», que es **peor que estar vencidas**: sin evento **nada las
podía vencer nunca**, y una fila así no se ve en ninguna comprobación. El Product Owner las cerró el
**2026-08-30** asignándoles el **punto de control de la fase `i`**, que `PRODUCT-INTAKE` §10 y §15
declaran bloqueante y es el último que queda.

**Lo que este documento observa es lo que ese arreglo dejó como efecto secundario.** El último punto de
control es el único evento que sirve para cualquier cosa, y por eso es donde va a parar todo lo que no
tiene uno natural. **Las cuatro filas pasaron de «sin evento» a «al final», y no a «cuando
corresponda».** El evento es real; **lo que es falso es la premisa de que ese evento las contesta.**

Es la figura del hallazgo `HM-02` del [reporte 17](https://github.com/hdcm-dev/IA.SDD.Documentacion) al
framework —*el diferimiento se contrasta contra el calendario y no contra los hechos*— **mirando hacia
adelante**: allá el evento había ocurrido y la premisa había muerto; acá el evento no ocurrió y **la
premisa nunca fue cierta**.

---

## 2. Los seis que la fase `i` sí contesta

| Asunto | Dónde vive | Por qué necesita el destino real |
|---|---|---|
| **La construcción de la imagen en destino desde el repositorio** | `Api/05` `PA-08`, `Api/09` `PD-01`, `Api/06` `PA-09` | Hay que probar **una vez** que el motor de contenedores del destino resuelva la referencia al repositorio, con credenciales si es privado. `BT-00026` es esa prueba. **No hay forma de saberlo sin el destino** |
| **El dominio propio para el túnel saliente** | `PRODUCT-INTAKE` `X-10` | O aparece un dominio, o no aparece. Nada del árbol lo produce |
| **El caudal de 20 peticiones por minuto** | `PRODUCT-INTAKE` §22 `A-5` | `PT-05` lo mide sobre **uso real desde la facultad**. [`Medicion-Volumen-De-Comision-2026-08-31.md`](Medicion-Volumen-De-Comision-2026-08-31.md) acotó el riesgo del lado del servicio y **declara expresamente que no lo reemplaza** |
| **La herramienta que calcula la versión** | `Api/05` `PA-04` y `PA-06`, `Api/06` `PA-03` y `PA-04`, `Api/09` `PD-03` | **Ya está decidido así**: el Product Owner contestó la escalada `E-02` el 2026-08-30 con la opción (c). La fase `i` es **cuando aparece el primer release real que hay que versionar**, y elegir antes sería anclar una herramienta sin ejercerla |
| **La vigencia del acceso firmado** | `Api/05` `PA-04`, `Api/09` `PD-04`, `Api/06` `PA-05` | **A medias, y conviene decirlo con precisión.** El valor **ya existe**: `SigningOptions.cs:25` lo fija en **480 minutos** por omisión. Lo que la fase `i` agrega no es el número sino **declararlo de verdad en el ambiente**, que es cuando la configuración deja de ser un valor por omisión |
| **El generador del inventario de componentes** | `Supply-Chain-Seguridad.md` `PD-10` | El formato ya lo decidiste —`D9`, CycloneDX JSON—; lo que falta es el generador, y corre **en el flujo real** |

---

## 3. Los cuatro que se aparcaron, con lo que rige de hecho hoy

**Los cuatro son de la escalada `E-04`, los cuatro llevan «punto de control de la fase `i`», y ninguno de
los cuatro se contesta ahí.** Lo que sigue es, para cada uno, **el valor que rige hoy medido contra el
árbol** — porque en los cuatro casos hay uno, aunque nadie lo haya declarado.

### 3.1 · La frecuencia del respaldo — `Api/05` `PA-07`, `Api/09` `PD-04`, `Api/06` `PA-07`

**Lo que rige hoy: no hay respaldo.** Verificado el 2026-08-31: no existe ningún guion de respaldo en
`scripts/`, ni tarea en el flujo, ni nada en `deploy/`. El almacén vive en un volumen y **nadie lo copia
nunca**.

El intake lo declara «a definir por el docente», o sea que **la fuente no lo omitió: te lo delegó**. **Es
una decisión de operación y la fase `i` no aporta nada a tomarla** — el volumen y el guion son los
mismos antes y después de desplegar.

**Lo que está en juego, dicho sin adornar:** desde la etapa `e` ese archivo tiene **las entregas de la
comisión**. Un almacén SQLite sin respaldo es un archivo. `scripts/store-path.sh` ya documenta que el
**2026-08-15 una corrida de guiones se llevó la cuenta de administrador**, y deja escrito que *«en la
etapa `e` el mismo archivo va a tener las entregas de una comisión y el mismo descuido se las lleva»*.

> **Recomendación.** Decidir **cuándo** —una copia por clase, o diaria— y **dónde**, y que el guion
> entre con el despliegue. Es la única de las cuatro donde no decidir **tiene un costo que crece**.

### 3.2 · La fecha de última modificación de la cuenta — `Api/05` `PA-09`, `Api/06` `PA-07`

**Lo que rige hoy: no existe.** `GeometriaFactory.Domain/Entities/Account.cs` no la declara y nadie la
registra. El trabajo **sí** tiene sus dos sellos —`Work.cs` declara `CreatedAt` y `UpdatedAt`—; la cuenta
tiene sólo el de alta.

**Nada del despliegue la produce.** Si entra, entra por el dominio.

> **Recomendación: no incorporarla, y cerrar el punto por decisión.** Ninguna capacidad del producto la
> pide, ninguna pantalla la muestra y ninguna regla la usa. Incorporar un atributo «por si acaso» a una
> entidad del dominio es exactamente lo que el resto del producto viene evitando. **Si mañana hace
> falta, entra con el caso de uso que la necesite** — y ahí se sabrá qué debe registrar.

### 3.3 · El sello de desenlace — `Api/05` `PA-04` de §11.4, `Api/06` `PA-06`

**Lo que rige hoy, y es más sutil que los otros tres.** El momento del desenlace **viaja** —
`WorkResolution` lo lleva y `WorkOutcomeResponse` lo devuelve — pero **no es un atributo del trabajo**:
`Work.cs` declara `CreatedAt` y `UpdatedAt`, y ninguno se llama desenlace.

**Y de hecho hoy se puede recuperar, por una coincidencia y no por diseño:** como **un trabajo resuelto
no se puede modificar** —el contrato rechaza editar o reenviar fuera de `Borrador`—, su `UpdatedAt`
**queda congelado exactamente en el momento del desenlace**. El sello existe, disfrazado de otra cosa.

> **Recomendación: declararlo, y no por completitud.** Hoy el dato es correcto **porque un invariante lo
> protege, no porque alguien lo haya declarado**. El día que un trabajo resuelto pueda tocarse —una
> reapertura, un comentario del docente, una corrección— **el momento del desenlace se pierde sin que
> nada falle ni nadie se entere**. Es barato ahora y no lo es después: los desenlaces ya emitidos no se
> pueden reconstruir.

### 3.4 · Hasta dónde llega el conjunto de tipos reconstruibles — `Api/05` `PA-04` de §11.5, `Api/06` `PA-04`

**Lo que rige hoy: seis.** Son los que los escenarios ejercitan y los que la pieza que dibuja sabe
dibujar. El análisis del que sale el intake menciona **siete** clases en un ejemplo y **diez** en el
otro, y **ninguna fuente las enumera**.

**Un tipo fuera del conjunto produce error de validación** — que es correcto, y puede no ser lo deseado:
un alumno que use una clase de la actividad que el producto no reconstruye recibe un rechazo que parece
un defecto suyo.

**El dato lo tenés vos**, y no el despliegue: es la enumeración de las clases de la actividad.

> **Recomendación: enumerarlas y contrastarlas contra los seis.** Es la única de las cuatro que puede
> **descubrir trabajo**: si de la enumeración salen ocho o diez, la diferencia es backlog, y **conviene
> saberlo antes de la fase `i` y no durante**. Si salen seis, se cierra el punto y se gana la certeza de
> que el conjunto está completo, que hoy nadie tiene.

---

## 4. Qué cambia si los cuatro se deciden

| | Hoy | Después |
|---|---|---|
| Asuntos que la fase `i` arrastra | **10** | **6**, y los seis son suyos |
| Decisiones del Product Owner pendientes | **4**, sin que se vean como tales —figuran como puntos con evento futuro— | **0** |
| Riesgo que crece mientras tanto | El respaldo | — |

**Y una que no cambia:** los seis de §2 siguen exactamente donde están. Este documento **no adelanta la
fase `i`**; le saca de encima lo que no le tocaba.

---

## 5. Lo que este documento no hace

- **No discute `E-04`.** Fue un arreglo correcto de un defecto real, y sin él estas cuatro filas seguirían siendo invisibles para toda comprobación del método.
- **No toma ninguna de las cuatro decisiones.** Las presenta con lo que rige de hecho y con una recomendación fundada, que es lo que `D1` hizo con las asunciones numéricas.
- **No toca las filas.** Si el Product Owner decide, las filas se cierran en su propia tanda, con su desenlace escrito donde viven.
- **No afirma que los seis de §2 estén bien atados por igual.** La vigencia del acceso firmado está a medias —el número existe— y se declara así en su fila.

---

## 6. Control de cambios

| Versión | Fecha | Descripción | Autor |
|---|---|---|---|
| 1.0 | 2026-08-31 | Emisión inicial. Barre los ítems con evento en la fase `i` sobre el corpus vivo y los clasifica: **diez asuntos, seis que la fase `i` contesta y cuatro que se aparcaron ahí**. Los cuatro son las filas de la escalada `E-04`, y **ninguno se contesta en la fase `i`**: son decisiones del Product Owner que llegaron al último punto de control **porque es el destino por defecto de todo lo que no tiene evento propio** — pasaron de «sin evento» a «al final», y no a «cuando corresponda». Presenta las cuatro con **el valor que rige de hecho hoy, medido contra el árbol**: **no hay ningún respaldo** del almacén; la **fecha de última modificación de la cuenta no existe**; el **sello de desenlace existe disfrazado de `UpdatedAt`** y es correcto sólo porque un invariante impide tocar un trabajo resuelto; y los **tipos reconstruibles son seis** contra las siete y diez clases que el análisis menciona. Cada una con recomendación fundada. **No toma ninguna decisión y no toca ninguna fila.** | Orquestador SDD |
