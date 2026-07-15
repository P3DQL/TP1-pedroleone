using System;
using System.Collections.Generic;
using System.Threading;

class Configuracion
{
    public int Filas;
    public int Columnas;
    public int Velocidad;

    public Configuracion()
    {
        Filas = 20;
        Columnas = 10;
        Velocidad = 100;
    }
}

class Copo
{
    public int X;
    public int Y;

    public Copo(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Mostrar()
    {
        Console.SetCursorPosition(X, Y);
        Console.Write("*");
    }

    public void Bajar()
    {
        Y++;
    }
}

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;

        Configuracion config = new Configuracion();
        List<Copo> copos = new List<Copo>();

        Random r = new Random();

        while (true)
        {
            // Crear un nuevo copo
            int columna = r.Next(config.Columnas);

            bool ocupado = false;

            foreach (Copo c in copos)
            {
                if (c.X == columna && c.Y == 0)
                {
                    ocupado = true;
                }
            }

            if (!ocupado)
            {
                copos.Add(new Copo(columna, 0));
            }

            // Mover copos
            for (int i = 0; i < copos.Count; i++)
            {
                if (copos[i].Y < config.Filas - 1)
                {
                    bool libre = true;

                    foreach (Copo otro in copos)
                    {
                        if (otro != copos[i] &&
                            otro.X == copos[i].X &&
                            otro.Y == copos[i].Y + 1)
                        {
                            libre = false;
                        }
                    }

                    if (libre)
                    {
                        copos[i].Bajar();
                    }
                }
            }

            // Verificar filas completas
            for (int fila = config.Filas - 1; fila >= 0; fila--)
            {
                int cantidad = 0;

                for (int col = 0; col < config.Columnas; col++)
                {
                    foreach (Copo c in copos)
                    {
                        if (c.X == col && c.Y == fila)
                        {
                            cantidad++;
                            break;
                        }
                    }
                }

                if (cantidad == config.Columnas)
                {
                    // Eliminar fila
                    copos.RemoveAll(c => c.Y == fila);

                    // Bajar las filas superiores
                    foreach (Copo c in copos)
                    {
                        if (c.Y < fila)
                        {
                            c.Y++;
                        }
                    }
                }
            }

            // Dibujar
            Console.Clear();

            foreach (Copo c in copos)
            {
                c.Mostrar();
            }

            Thread.Sleep(config.Velocidad);
        }
    }
}