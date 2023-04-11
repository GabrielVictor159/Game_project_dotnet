using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int jogadorPosX;
    static int jogadorPosY;
    static string jogadorAparencia = "(X_X)";
    static List<Comida> comidas = new List<Comida>();
    static int velocidadeJogador = 1;
    static DateTime tempoEstadoEspecial;

    static void Main(string[] args)
    {
        jogadorPosX = Console.WindowWidth / 2;
        jogadorPosY = Console.WindowHeight / 2;
        int previusConsoleWidth = Console.WindowWidth;
        int previusConsoleLeft = Console.WindowLeft;
        GerarComida();
        DesenharJogador();
        DesenharComidas();

        while (true)
        {
            ConsoleKeyInfo teclaPressionada = Console.ReadKey();
            Console.Clear();
            switch (teclaPressionada.Key)
            {
                case ConsoleKey.UpArrow:
                    jogadorPosY -= velocidadeJogador;
                    break;
                case ConsoleKey.DownArrow:
                    jogadorPosY += velocidadeJogador;
                    break;
                case ConsoleKey.LeftArrow:
                    jogadorPosX -= velocidadeJogador;
                    break;
                case ConsoleKey.RightArrow:
                    jogadorPosX += velocidadeJogador;
                    break;
                default:
                    Console.WriteLine("Console foi redimensionado. Programa encerrando.");
                    return;
            }

            if (jogadorPosX < 0 || jogadorPosY < 0 || jogadorPosX >= Console.WindowWidth || jogadorPosY >= Console.WindowHeight)
            {
                Console.WriteLine("Console foi redimensionado. Programa encerrando.");
                return;
            }
            if (Console.WindowWidth < previusConsoleWidth || Console.WindowWidth > previusConsoleWidth)
            {
                Console.WriteLine("Console foi redimensionado. Programa encerrando.");
                return;
            }
            if (Console.WindowLeft < previusConsoleLeft || Console.WindowLeft > previusConsoleLeft)
            {
                Console.WriteLine("Console foi redimensionado. Programa encerrando.");
                return;
            }
            for (int i = 0; i < comidas.Count; i++)
            {
                int margemErro = 1;

                if (Math.Abs(jogadorPosX - comidas[i].PosX) <= margemErro && Math.Abs(jogadorPosY - comidas[i].PosY) <= margemErro)
                {
                    jogadorAparencia = "(^-^)";
                    tempoEstadoEspecial = DateTime.Now.AddSeconds(5);
                    velocidadeJogador = 2;
                    comidas.RemoveAt(i);
                    GerarComida();
                }
            }


            if (DateTime.Now > tempoEstadoEspecial)
            {
                jogadorAparencia = "(X_X)";
                velocidadeJogador = 1;
            }

            DesenharJogador();
            DesenharComidas();
        }
    }

    static void DesenharJogador()
    {
        Console.SetCursorPosition(jogadorPosX, jogadorPosY);
        Console.Write(jogadorAparencia);
    }

    static void GerarComida()
    {
        Random random = new Random();

        while (comidas.Count < 5)
        {
            int comidaPosX = random.Next(0, Console.WindowWidth);
            int comidaPosY = random.Next(0, Console.WindowHeight);
            Comida novaComida = new Comida(comidaPosX, comidaPosY);
            comidas.Add(novaComida);
        }
    }



    static void DesenharComidas()
    {
        foreach (Comida comida in comidas)
        {
            Console.SetCursorPosition(comida.PosX, comida.PosY);
            Console.Write("@");
        }
    }
}

class Comida
{
    public int PosX { get; set; }
    public int PosY { get; set; }

    public Comida(int posX, int posY)
    {
        PosX = posX;
        PosY = posY;
    }
}
