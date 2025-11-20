using System;

namespace TelegramBot.Consts;

public static class BotMessages
{
    public const string Welcome = """
        ¡Hola! 🍣 Bienvenido a SushiPedidos.

        Estamos listos para calmar tu antojo con los rollos más frescos de la ciudad. Desde aquí puedes ver nuestro menú, hacer pedidos o contactar a soporte.

        👇 Selecciona una opción para comenzar:
        """;

    public const string WorkingHours =
        "Nuestro horario de atención es de lunes a sábado de 10:00 AM a 11:00 PM.";

    public const string GenericError = "Ocurrió un error inesperado. Por favor intenta más tarde.";
}
