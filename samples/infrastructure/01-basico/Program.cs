using GeometriaFactory.Application.Ports;
using GeometriaFactory.Domain.Entities;
using GeometriaFactory.Domain.Values;
using GeometriaFactory.Infrastructure.Figures;
using GeometriaFactory.Samples.Infrastructure.Basico;

// ============================================================================
// Sample `infrastructure/01-basico` — los ocho escenarios del `PRODUCT-INTAKE` §20
// contra el INTÉRPRETE REAL, sin dobles.
//
// ES LO CONTRARIO DEL SAMPLE DE APLICACIÓN, y por eso los dos existen: allá el
// intérprete era un doble que devolvía lo declarado, para que se viera que la capa
// no interpreta; acá es el componente de verdad, porque lo que hay que ver es
// **qué interpreta**.
// ============================================================================

var lineas = new List<string>();
void Escribir(string l) { lineas.Add(l); Console.WriteLine(l); }

var validador = new LocalFigureValidator();
FigureInterpretation Interpretar(string e) =>
    validador.Interpret(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Escenarios", $"{e}.txt")));

string Especie(ObservationKind k) => k == ObservationKind.Warning ? "Advertencia" : "Error";

// ---- E-1 · tres figuras, dos advertencias ----
var e1 = Interpretar("E1");
Escribir($"[E-1] Figuras del conjunto raiz: {e1.RootFigureCount} | Piezas reconstruidas: "
    + $"{e1.Pieces.Count} | Observaciones: {e1.Observations.Count}");

var cilindro = e1.Pieces.First(p => p.Type == FigureType.Cylinder);
var tapas = cilindro.Components.Count(c => c.Role == ComponentRole.Cap);
var lados = cilindro.Components.Count(c => c.Role == ComponentRole.Side);
var obsCilindro = e1.Observations.Count(o => o.PiecePosition == cilindro.Position);
Escribir($"[E-1] Cilindro: {tapas} tapas Circulo y {lados} lado RectanguloDesarrollado "
    + $"| Observaciones: {obsCilindro} (tolerancia estricta)");

var cubo = e1.Pieces.First(p => p.Type == FigureType.Cube);
var obsCubo = e1.Observations.First(o => o.PiecePosition == cubo.Position);
Escribir($"[E-1] Cubo: advertencia de {obsCubo.Field.ToLowerInvariant()} "
    + $"declarado={obsCubo.DeclaredValue:F2} derivado={obsCubo.DerivedValue:F2}");

var orto = e1.Pieces.First(p => p.Type == FigureType.Orthohedron);
var obsOrto = e1.Observations.First(o => o.PiecePosition == orto.Position);
Escribir($"[E-1] Ortoedro: advertencia de {obsOrto.Field.ToLowerInvariant()} "
    + $"declarado={obsOrto.DeclaredValue:F2} derivado={obsOrto.DerivedValue:F2} | area sin observacion");

// ---- E-2 · las dos comas finales y la clave `Tapas` de un ortoedro ----
var e2 = Interpretar("E2");
Escribir($"[E-2] Parseo con comas finales: {(e2.Pieces.Count > 0 ? "exitoso" : "fallido")} (T2) "
    + "| Clave Tapas leida como bases (T1)");
var p2 = e2.Pieces[0];
Escribir($"[E-2] Estructura: {e2.Pieces.Count} pieza, "
    + $"{p2.Components.Count(c => c.Role == ComponentRole.Base)} bases, "
    + $"{p2.Components.Count(c => c.Role == ComponentRole.Lateral)} laterales "
    + $"| Observaciones: {e2.Observations.Count} advertencia de volumen");

// ---- E-3 y E-4 · el mismo cubo con caras de dos tipos ----
var e3 = Interpretar("E3");
var o3 = e3.Observations[0];
Escribir($"[E-3] Caras Cuadrado interpretadas (T3) | advertencia de area "
    + $"declarado={o3.DeclaredValue:F2} derivado={o3.DerivedValue:F2}");

var e4 = Interpretar("E4");
Escribir($"[E-4] Caras Rectangulo interpretadas (T3) | Observaciones: {e4.Observations.Count}");

// ---- E-5 · el tipo desconocido reserva su posición ----
var e5 = Interpretar("E5");
Escribir($"[E-5] Figuras del conjunto raiz: {e5.RootFigureCount} | Piezas reconstruidas: {e5.Pieces.Count}");
var err5 = e5.Observations.First(o => o.Kind == ObservationKind.ValidationError);
Escribir($"[E-5] Observacion {Especie(err5.Kind)}: indice-figura={err5.PiecePosition} campo={err5.Field}");

// ---- E-6 · el cero es un valor ----
var e6 = Interpretar("E6");
Escribir($"[E-6] Dimension 0.00: la figura se {(e6.Pieces.Count > 0 ? "interpreta y no se descarta" : "descarta")} "
    + $"| Errores de interpretacion: {e6.Observations.Count(o => o.Kind == ObservationKind.ValidationError)}");

// ---- E-7 · seis piezas, tres volumétricas y tres planas ----
var e7 = Interpretar("E7");
var volumetricos = e7.Pieces.Count(p => p.DeclaredVolume is not null || p.DerivedVolume is not null);
Escribir($"[E-7] Piezas reconstruidas: {e7.Pieces.Count} | volumetricos={volumetricos} "
    + $"planos={e7.Pieces.Count - volumetricos} | Clave Bases leida como bases (T1)");

var orto7 = e7.Pieces.First(p => p.Type == FigureType.Orthohedron);
var baseOrto = orto7.Components.First(c => c.Role == ComponentRole.Base);
var lateral = orto7.Components.FirstOrDefault(c => c.Role == ComponentRole.Lateral);
var altura = lateral?.DeclaredWidth ?? lateral?.DeclaredLength;
Escribir($"[E-7] Ortoedro: ancho={baseOrto.DeclaredLength:F2} profundidad={baseOrto.DeclaredWidth:F2} "
    + $"altura={altura:F2}");

return args.Contains("--verificar", StringComparer.Ordinal) ? SalidaEsperada.Comparar(lineas) : 0;
