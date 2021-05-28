using System;
using xadrez;
using tabuleiro;


namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            PisicaoXadrez pos = new PisicaoXadrez('c', 7);

            Console.WriteLine(pos);

            Console.WriteLine(pos.toPosicao());

            Console.ReadLine();
        }
    }
}
