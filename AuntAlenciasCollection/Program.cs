// See https://aka.ms/new-console-template for more information

using AuntAlenciasCollection.BotHandling;
using AuntAlenciasCollection.DatabaseConnection;

namespace AuntAlenciasCollection
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var auntAlencia = new Bot();
            auntAlencia.runAsync().GetAwaiter().GetResult();
        }
    }
}



