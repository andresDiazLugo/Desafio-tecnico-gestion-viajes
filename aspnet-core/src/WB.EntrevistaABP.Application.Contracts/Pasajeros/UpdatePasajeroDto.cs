using System;

namespace WB.EntrevistaABP.Pasajeros;

public class UpdatePasajeroDto
{
    public string Nombre {get; set;}
    public string Apellido {get; set;}
    public string DNI {get; set;}
    public string Email {get; set;}
    public DateTime Fecha_de_nacimiento {get; set;}
}