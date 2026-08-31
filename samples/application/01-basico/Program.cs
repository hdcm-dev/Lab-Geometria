using GeometriaFactory.Application.Accounts;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Samples.Application.Basico;
using GeometriaFactory.Samples.Application.Basico.Actos;
using GeometriaFactory.Samples.Application.Basico.Dobles;

// ============================================================================
// Sample `application/01-basico` — los cuatro actos de la capa de aplicación,
// contra DOBLES DE SUS PUERTOS y no contra la infraestructura real.
//
// Eso es lo que el sample enseña: la capa declara los puertos y no sabe quién los
// implementa. Cambiar los dobles por los adaptadores de base de datos no cambia
// una línea de los cuatro actos.
// ============================================================================

var bitacora = new Bitacora();
var cuentas = new RepositorioDeCuentasEnMemoria();
var trabajos = new RepositorioDeTrabajosEnMemoria();
var reloj = new RelojFijo();

// [1] El alta de alumno, y el correo que ya está tomado.
var alumna = await ActoAltaDeAlumno.EjecutarAsync(bitacora, cuentas, reloj);

// [2] El administrador, y su ventana que se cierra.
await ActoAltaDeAdministrador.EjecutarAsync(bitacora, cuentas, reloj);

// [3] Los tres desenlaces de la admisibilidad, sobre DOS CUENTAS DISTINTAS.
// La primera versión de este sample pedía la misma cuenta dos veces y las habilitaba a las dos:
// el repositorio devuelve LA MISMA INSTANCIA, no una copia. Es la consecuencia de que el doble no
// copie estado — que es lo correcto — y de haberlo olvidado al escribir el acto.
var pendiente = (await cuentas.FindByIdAsync(alumna.Id))!;
var segunda = (await new RegisterAccountUseCase(cuentas, reloj)
    .ExecuteAsync("otra@frre.utn.edu.ar", "Otra", "Alumna")).Value!;
var habilitada = (await cuentas.FindByIdAsync(segunda.Id))!;
habilitada.Enable("hash-de-la-provisoria");
ActoAdmisibilidad.Ejecutar(bitacora, pendiente, habilitada);

// [4] La puerta de ADR-04004, de punta a punta, sobre una tercera cuenta con la marca puesta.
var tercera = (await new RegisterAccountUseCase(cuentas, reloj)
    .ExecuteAsync("tercera@frre.utn.edu.ar", "Tercera", "Alumna")).Value!;
var marcada = (await cuentas.FindByIdAsync(tercera.Id))!;
marcada.Enable("hash-de-la-provisoria");
await ActoCambioDeCredencial.EjecutarAsync(bitacora, cuentas, trabajos, marcada);

bitacora.Cerrar();

// LA COMPARACIÓN CORRE SIEMPRE, Y ANTES ERA OPCIONAL.
//
// Hasta el 2026-08-30 estaba detrás de `--verificar`, y el comando que el §4 del documento declara
// —y que el contrato de verificación de su §9 cita— **no pasa esa bandera**. Corrido como está
// documentado, el sample imprimía sus renglones y devolvía **cero sin comparar nada**.
//
// ERA UN INSTRUMENTO QUE SE LEÍA COMO VERDE SIN HABER VERIFICADO, que es el defecto que estos
// samples existen para encontrar en otros lados. Se midió sobre los nueve de .NET: **siete estaban
// así**. Quien sólo quiera ver la salida la sigue viendo; lo que se agrega es el veredicto.
return SalidaEsperada.Comparar(bitacora.Lineas);
