namespace API.Helpers;

public class Authorization
{
    public enum Roles
        {
            Administrador,
            Gerente,
            Empleado
        }
        public const Roles rol_default = Roles.Empleado;
}
