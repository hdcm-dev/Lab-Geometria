# Convierte un archivo de texto en un literal JSON de cadena.
#
# NO HAY `jq` NI `python3` EN LA IMAGEN DEL ENTORNO CONTENIDO, y agregar una herramienta al
# entorno para escapar comillas sería mover el anclaje de versión de la etapa `a` por comodidad
# de un sample. Esto hace lo mismo con lo que ya está.
#
# EL TEXTO SE TRANSPORTA SIN NORMALIZAR (`US-00019`): acá se escapa lo que JSON exige —barra
# invertida, comilla, tabulador y fin de línea— y NADA MÁS. No se reordena, no se compacta y no
# se reindenta. Es el mismo motivo por el que los cuerpos llevan extensión `.txt`.
BEGIN { ORS = ""; print "\"" }
{
  gsub(/\\/, "\\\\")
  gsub(/"/, "\\\"")
  gsub(/\t/, "\\t")
  gsub(/\r/, "")
  printf "%s\\n", $0
}
END { print "\"" }
