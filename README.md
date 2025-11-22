Manual de uso:

La API de Minijuegos permite generar preguntas para tres tipos de desafíos —matemáticas, memoria y lógica— y validar respuestas enviadas por aplicaciones cliente.
Para generar una pregunta, el cliente debe realizar una petición GET al endpoint:
  /api/minijuegos/pregunta?tipo={tipo}
donde {tipo} puede ser matematicas, memoria o logica.
La API responde con un objeto JSON que incluye un ID único, los datos necesarios para el minijuego, y la fecha de creación.

Para validar la respuesta de un usuario, se debe enviar una solicitud POST a:
/api/minijuegos/validar
incluyendo el preguntaId y la respuesta.
La API devolverá si la respuesta es correcta o incorrecta, junto con información adicional para que la aplicación cliente pueda mostrar el resultado al usuario.



Aclaraciones 

Al definir la interfaz de minijuegos para que siempre devuelva un PreguntaGeneralDTO, aplicamos herencia para unificar la estructura de las respuestas. 
Sin embargo, esta elección de diseño no fue la más adecuada: al trabajar con el tipo base, los tests solo pueden acceder a las propiedades generales y no 
a las específicas de cada DTO derivado. 
Esto hace que en las pruebas no podamos verificar información propia de cada minijuego, como la secuencia en memoria o la proposición en lógica, 
porque el retorno está limitado al tipo general.
