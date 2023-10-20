### Uso de Json Web Token
Ya que no se cargan usuarios en la base de datos por medio de csvs, es necesesario crearlo. Por defecto el rol de usuario va ser Empleado, el cual puede hacer peticiones a todo el CRUD menos a los enpoints especiales. Cuando se prueben los endpoints es necesario que el usuario tenga el rol de Administrador el cual se le asigna por medio del addrole.

Los datos necesarios para poder hacer post a los endpoints de JWT y en general se encuetran más facilmente en el Swagger, que se incializa por medio de dotnet watch run.

Nota: He tenido inconvenientes con la autorización, ya que, en vez de lanzarme la respuesta 401 o 403, me lanza 404 Not found, pero si le pasamos en token igualmente funcionará.

Si el token caduca, el programa esta diseñado para que por medio de su refresh token pueda generar otro.

Tomamos el refresh token

creamos la cookie y apartir de ese refresh token se generan nuevos tokens
Si el token expira podemos generar cuantos queramos con el mismo refresh token. Si el refresh token expira se debe generar otro token desde el endpoint token.

- Duración del refresh token: 1 hora
- Duración del token de acceso: 1 minuto
Si se presentan inconvenientes a la hora de generar un nuevo token desde el refresh token hay que borrar las cookies.
