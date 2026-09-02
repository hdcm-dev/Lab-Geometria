# El recorrido de extremo a extremo, corriendo sin permiso de nadie

**Qué se corrió:** `scripts/pruebas-e2e.sh`, **sin ninguna variable de entorno**. La suite publicó
el producto entero, lo levantó sobre puertos que le pidió al sistema, sembró su administrador,
recorrió las pantallas con un navegador de verdad y al terminar borró todo lo que había creado.

```text
== Recorrido en chromium ==
Passed!  - Failed: 0, Passed: 32, Skipped: 0, Total: 32, Duration: 18 s
```

**Ningún secreto, ningún dato del docente tocado, ningún contenedor del Product Owner rozado.**
Antes de este cambio la misma batería exigía cuatro variables —dos secretas— y sembraba cuentas en
el laboratorio real.

---

## Lo que la batería encontró apenas se la pudo correr

| # | Qué apareció | De quién era |
| --- | --- | --- |
| 1 | **`AprobarPideConfirmacionYAplicaElDesenlace` estaba en rojo en `main`.** El aserto buscaba «quedo finalizado» y el producto emite «quedó finalizado»: **nunca pudo coincidir**. Entró en la misma fusión que el acuse que verifica | de la prueba |
| 2 | **`LaInteractividadEstaVivaEnElAnfitrion` era intermitente por construcción.** Miraba una marca con resolución de **un segundo**: contra un banco local la consulta entera entra en el mismo segundo y el texto queda idéntico | de la prueba |
| 3 | La medición de la disposición pedida a mitad del redibujo devuelve **caja nula**, y el rojo decía «no se dibujó» donde lo que pasaba era «todavía no» | de la prueba |

**Los tres son de las pruebas y ninguno del producto**, y eso también es un dato: la batería
llevaba menos de un día y ya tenía dos casos que no medían lo que decían medir. **Un verde que
nadie volvió a correr no es un verde.**

---

## La versión angosta, mirada y no contada

Las tres capturas son de la corrida, en una ventana de **390 px**, y quedan versionadas porque en
esta casa la evidencia de lo visual **se mira**: el `P0` `MI-02` se cerró una vez sin mostrar un
teléfono con filas, y cuando se sacaron capturas aparecieron tres recortes que los conteos daban
por buenos.

| Captura | Qué se ve, y qué regla afirma |
| --- | --- |
| [`cuentas-en-390px.png`](cuentas-en-390px.png) | El listado **son tarjetas y tiene filas**. Es el aserto del `P0` `MI-02`: por debajo de 768 px la hoja apaga la tabla y enciende las tarjetas, y durante catorce días **no las emitía ningún componente** |
| [`acciones-en-390px.png`](acciones-en-390px.png) | **Aprobar arriba, Retirar abajo.** Es `R-06`: `column-reverse` ponía la acción que borra en el primer lugar de la pantalla, que es el que el pulgar alcanza sin mirar |
| [`vista-de-trabajo-en-390px.png`](vista-de-trabajo-en-390px.png) | **Una sola columna, con la escena primero.** Y la escena dibuja: el cubo del trabajo sembrado está ahí |

---

## Lo que esta corrida NO prueba

- **Que el sitio publicado ande.** Eso sólo lo puede decir el modo desplegado, contra el anfitrión
  real y con la credencial del docente.
- **Nada que dependa de la marca `Secure` de la cookie.** El banco local corre en `Development` y
  sobre HTTP llano; el apartamiento está declarado como `AP-E2E-01` en
  [`../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Pruebas-Extremo-A-Extremo.md`](../../SDD/Docs/Unidades-Entrega/GeometriaFactory-Web/08-Calidad-Y-Pruebas/Pruebas-Extremo-A-Extremo.md).

---

## Y un hallazgo que queda abierto: `firefox`

**`chromium` recorre los 32 casos en 18 s. `firefox` no termina la batería.** Medido el mismo día,
tres veces, con la máquina en carga 15 y también en carga 3:

| Qué se corrió en `firefox` | Resultado |
| --- | --- |
| `IngresoTests` —3 casos, **ninguno necesita el circuito interactivo**— | **3 de 3 en 7 s** |
| `DisenoResponsivoTests` —6 casos de disposición— | pasan y dejan sus capturas |
| Los casos que **esperan a que el circuito habilite un control** | **agotan sus 30 s, uno por uno** |
| La batería completa | **no terminó en 25 minutos**, con y sin traza |

**No se sabe todavía de quién es**, y por eso se declara en vez de resolverse por criterio propio:
puede ser del banco —que sirve por WebSocket, mientras el anfitrión real se repliega a sondeo
largo—, del navegador, o del producto en ese navegador. **Subir el margen de espera hasta que dé
verde sería tapar la pregunta con un número.**

**Qué NO se afirma, entonces:** que el producto ande en los tres navegadores. La matriz de tres
existe en el flujo del modo desplegado desde antes de este cambio; **lo verificado hoy sobre el
banco local es `chromium`**, y el trabajo que corre en cada cambio recorre ése. Queda como
`H-E2E-01` en la especificación de la batería.
