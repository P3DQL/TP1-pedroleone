using System;
using System.Collections.Generic;
using System.Threading;
/*Realizar un programa que represente una simulación de copos de nieve cayendo en la consola, utilizando el símbolo "*" para cada copo.

El programa debe cumplir con las siguientes condiciones:
Definir una clase Configuracion que almacene parámetros de la simulación, como la cantidad de filas, columnas y la velocidad de caída de los copos.
Definir una clase Copo que modele el comportamiento de un copo de nieve. Cada copo debe tener una posición en la consola y un método para mostrarse y desplazarse hacia abajo.
Usar una lista para administrar todos los copos activos durante la simulación.
Implementar una lógica que controle la caída de los copos de nieve, evitando que se superpongan en la misma posición.
Al completarse una fila con copos en todas las columnas, esta debe eliminarse para permitir que continúe la simulación.
El programa debe ejecutarse en un ciclo continuo, simulando de manera animada la caída de los copos.
*/
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
                    copos.RemoveAll(c => c.Y == fila);

                    foreach (Copo c in copos)
                    {
                        if (c.Y < fila)
                        {
                            c.Y++;
                        }
                    }
                }
            }

            Console.Clear();

            foreach (Copo c in copos)
            {
                c.Mostrar();
            }

            Thread.Sleep(config.Velocidad);
        }
    }
}
