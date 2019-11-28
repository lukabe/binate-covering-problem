namespace BinateCoveringProblem.Console.Extensions
{
    using System;

    public static class ConsoleExtensions
    {
        public static void Write(string message, ConsoleColor backgroundColor)
        {
            var oldColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(message);
            Console.BackgroundColor = oldColor;
        }
    }
}
