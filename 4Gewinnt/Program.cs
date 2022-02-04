using System;


namespace _4Gewinnt
{
    class Program
    {   //y-Achse
        public static int y = 6;
        //x-Achse
        public static int x = 7;
        struct Gamefield
        {
            // Das eigentliche Spielfeld
            public int[,] Field;

            // Verbleibende Züge
            public int Moves;

            // Spieler, welcher am Zug ist
            public int Player;

            public bool GameOver;
        }
         
        static Gamefield NewGame()
        {
            Gamefield Gamefields = new Gamefield();
            Gamefields.GameOver = false;

            // 0 = frei, 1 = Spieler 1, 2 = Spieler 2
            Gamefields.Field = new int[y, x];


            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Gamefields.Field[i, j] = 0;
                } // for End
            } // for End

            Gamefields.Moves = 22;
            Gamefields.Player = 1;

            return Gamefields;
        }

        static Gamefield Move(Gamefield Gamefield)
        {

            // Spiefeld ausgeben
            Output(Gamefield);

            while (true)
            {
                ConsoleKeyInfo k = Console.ReadKey();

                // Falls Eingabe Zahl ist
                if (char.IsDigit(k.KeyChar) && Convert.ToInt32(k.KeyChar) - 49 < 7 &&
                    Gamefield.Field[0, Convert.ToInt32(k.KeyChar) - 49] == 0)
                {

                    Gamefield = GameSet(Gamefield, Convert.ToInt32(k.KeyChar) - 49);
                    Gamefield = WinningCalculation(Gamefield, Convert.ToInt32(k.KeyChar) - 49);

                    Output(Gamefield);
                    break;
                }
                else
                {
                    Output(Gamefield);
                }  // else End                                         

            } // while End

            return Gamefield;
        }
        static Gamefield GameSet(Gamefield Gamefield, int gap)
        {


            for (int i = 0; i < 5; i++)

            {
                if (Gamefield.Field[i + 1, gap] != 0)
                {
                    Gamefield.Field[i, gap] = Gamefield.Player;

                    // Nächster Spieler
                    if (Gamefield.Player == 1)
                    {
                        Gamefield.Player = 2;
                    }
                    else
                    {
                        Gamefield.Player = 1;
                    } // else End

                    return Gamefield;
                } // if End
            } // for End

            Gamefield.Field[5, gap] = Gamefield.Player;

            // Nächster Spieler
            if (Gamefield.Player == 1)
            {
                Gamefield.Player = 2;
            }
            else
            {
                Gamefield.Player = 1;
            } // else End

            return Gamefield;
        }

        static void Output(Gamefield Gamefield)
        {
            Console.Clear();

            if (Gamefield.Player == 1)
                Console.WriteLine("Spieler 1 ist an der Reihe!\n");
            else
                Console.WriteLine("Spieler 2 ist an der Reihe!\n");

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write(Gamefield.Field[i, j]);
                } // for End
                Console.WriteLine();
            } // for End

            Console.WriteLine("\n\nWähle eine Spalte durch eingeben der Nummer(1-7) aus.");

            Console.SetCursorPosition(0, 8);
        }

        static Gamefield WinningCalculation(Gamefield Gamefield, int gap)
        {
            int row = 0;

            // Zeile ermitteln
            for (int i = 0; i < y; i++)
            {
                if (Gamefield.Field[i, gap] != 0)
                {
                    row = i;
                    break;
                } // if End
            } // for End

            int e = gap + 1;

            // Erster Stein von Links
            for (int i = gap; Gamefield.Field[row, i] == Gamefield.Field[row, gap] && i > 0; i--)
            {
                e--;
            } // for End

            // Gewinn in einer Zeile?
            for (int i = e + 1; i < e + 4 && i < x; i++)
            {
                if (Gamefield.Field[row, i] != Gamefield.Field[row, gap])
                {
                    break;
                }
                else if (i == e + 3)
                {
                    Gamefield.GameOver = true;
                    return Gamefield;
                } // else if End
            } // for End

            // Gewinn in einer Spalte?
            for (int i = 1; i < 5 && i + row < y; i++)
            {
                if (Gamefield.Field[i + row, gap] != Gamefield.Field[row, gap])
                {
                    break;
                }
                else if (i == 3)
                {
                    Gamefield.GameOver = true;
                    return Gamefield;
                } // else if End
            } // for End

            // Gewinn Diagonal?
            int diagonal = 1;

            for (int i = 1; i < 5 && gap - i > 0 && row - i > 0; i++)
            {
                if (Gamefield.Field[row - i, gap - i] == Gamefield.Field[row, gap])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End            

            for (int i = 1; i < 5 && gap + i < x && row + i < y; i++)
            {
                if (Gamefield.Field[row + i, gap + i] == Gamefield.Field[row, gap])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End     

            if (diagonal >= 4)
            {
                Gamefield.GameOver = true;
                return Gamefield;
            } // if End

            diagonal = 1;

            for (int i = 1; i < 5 && gap - i > 0 && row + i < y; i++)
            {
                if (Gamefield.Field[row + i, gap - i] == Gamefield.Field[row, gap])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End            

            for (int i = 1; i < 5 && gap + i < x && row - i > 0; i++)
            {
                if (Gamefield.Field[row - i, gap + i] == Gamefield.Field[row, gap])
                {
                    diagonal++;
                }
                else
                {
                    break;
                } // else End
            } // for End     

            if (diagonal >= 4)
            {
                Gamefield.GameOver = true;
                return Gamefield;
            } // if End

            return Gamefield;
        }

        static void Main(string[] args)
        {
            // Neues Spielfeld 
            Gamefield Gamefield = NewGame();

            // Ausgabe
            Console.WriteLine("Willkommen bei 4 Gewinnt!\n\n" +
                "Um ein neues Spiel zu starten drücke eine beliebige Taste...");
            while (!Console.KeyAvailable) { }

            while (true)
            {
                Gamefield = NewGame();

                while (!Gamefield.GameOver)
                {
                    // 1 Zug setzen
                    Gamefield = Move(Gamefield);

                } // while End
                Console.Clear();

                if (Gamefield.Player == 1)
                    Console.Write("Spieler 2 hat gewonnen!");
                else
                    Console.Write("Spieler 1 hat gewonnen!");

                Gamefield.GameOver = false;
                Console.Write("\n\n\nDrücke eine beliebige Taste um ein neues Spiel zu starten!");
                while (!Console.KeyAvailable) { }
            } // while End
        }
    }
}
