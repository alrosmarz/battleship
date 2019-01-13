
namespace Battleship.Ascii
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
    using System.Text.RegularExpressions;
    using Battleship.GameController;
   using Battleship.GameController.Contracts;

   internal class Program
   {
      private static List<Ship> myFleet;

      private static List<Ship> enemyFleet;

      static void Main()
      {
            
            
            for (int i = 5; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine("LOADING FUEGO PARIENTE IN " + (i).ToString());
                System.Threading.Thread.Sleep(500);
            }

            Console.Clear();
         Console.WriteLine("                                     |__");
         Console.WriteLine(@"                                     |\/");
         Console.WriteLine("                                     ---");
         Console.WriteLine("                                     / | [");
         Console.WriteLine("                              !      | |||");
         Console.WriteLine("                            _/|     _/|-++'");
         Console.WriteLine("                        +  +--|    |--|--|_ |-");
         Console.WriteLine(@"                     { /|__|  |/\__|  |--- |||__/");
         Console.WriteLine(@"                    +---------------___[}-_===_.'____                 /\");
         Console.WriteLine(@"                ____`-' ||___-{]_| _[}-  |     |_[___\==--            \/   _");
         Console.WriteLine(@" __..._____--==/___]_|__|_____________________________[___\==--____,------' .7");
         Console.WriteLine(@"|                        Welcome to FUEGO PARIENTE!                    BB-61/");
         Console.WriteLine(@" \_________________________________________________________________________|");
         Console.WriteLine();
            
         InitializeGame();
            
            
            for (int i = 0; i < 4; i++)
            {
                Console.Clear();
                
                if (i == 0) Console.WriteLine("CREATING THE BOARD...");
                if (i == 1) Console.WriteLine("MOVING FLEET...");
                if (i == 2) Console.WriteLine("LOADING CANNONS...");
                if (i == 3) Console.WriteLine("TIME TO PLAY!...");
                System.Threading.Thread.Sleep(1000);
            }
            StartGame();
      }

      private static void StartGame()
      {
         Console.Clear();
            //Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                  __");
         Console.WriteLine(@"                 /  \");
         Console.WriteLine("           .-.  |    |");
         Console.WriteLine(@"   *    _.-'  \  \__/");
         Console.WriteLine(@"    \.-'       \");
         Console.WriteLine("   /          _/");
         Console.WriteLine(@"  |      _  /""");
         Console.WriteLine(@"  |     /_\'");
         Console.WriteLine(@"   \    \_/");
         Console.WriteLine(@"    """"""""");
            Console.ResetColor();

            do
            {
            Console.WriteLine();
            Console.WriteLine("Player, it's your turn");
            Console.WriteLine("Enter coordinates for your shot :");
            var position = ParsePosition(Console.ReadLine());
            var isHit = GameController.CheckIsHit(enemyFleet, position);
                if (isHit)
                {
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"                \         .  ./");
                    Console.WriteLine(@"              \      .:"";'.:..""   /");
                    Console.WriteLine(@"                  (M^^.^~~:.'"").");
                    Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                    Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                    Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                    Console.WriteLine(@"                 -\  \     /  /-");
                    Console.WriteLine(@"                   \  \   /  /");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Yeah ! Nice hit !");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Miss, you hit the water");
                }

                Console.ResetColor();

                position = GetRandomPosition();
            isHit = GameController.CheckIsHit(myFleet, position);
            Console.WriteLine();
                if (isHit)
                {
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(@"                \         .  ./");
                    Console.WriteLine(@"              \      .:"";'.:..""   /");
                    Console.WriteLine(@"                  (M^^.^~~:.'"").");
                    Console.WriteLine(@"            -   (/  .    . . \ \)  -");
                    Console.WriteLine(@"               ((| :. ~ ^  :. .|))");
                    Console.WriteLine(@"            -   (\- |  \ /  |  /)  -");
                    Console.WriteLine(@"                 -\  \     /  /-");
                    Console.WriteLine(@"                   \  \   /  /");

                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Computer shot in {0}{1} and has hit your ship !", position.Column, position.Row);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Computer shot in {0}{1} and hit the water", position.Column, position.Row);
                }

                Console.ResetColor();
         }
         while (true);
      }

        internal static Position ParsePosition(string input)
        {
            string pattern = "^[a-hA-H][1-9]$";
            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (m.Success)
            {
                var letter = (Letters)Enum.Parse(typeof(Letters), input.ToUpper().Substring(0, 1));
                var number = int.Parse(input.Substring(1, 1));
                return new Position(letter, number);
            }
            else { Console.WriteLine(Battleship.GameController.GameController.MensajeDeError());
                return ParsePosition(Console.ReadLine());
            }
                
      }

      private static Position GetRandomPosition()
      {     
          int rows = 8;
          int lines = 8;
          var random = new Random();
          var letter = (Letters)random.Next(lines);
          var number = random.Next(rows);
          var position = new Position(letter, number);
          return position;
      }

      private static void InitializeGame()
      {
         InitializeMyFleet();

         InitializeEnemyFleet();
      }

      private static void InitializeMyFleet()
      {
         myFleet = GameController.InitializeShips().ToList();

         Console.WriteLine("Please position your fleet (Game board size is from A to H and 1 to 8) :");

         foreach (var ship in myFleet)
         {
           
                if (ship.Name == "Aircraft Carrier") Console.ForegroundColor = ConsoleColor.Yellow;
                if (ship.Name == "Battleship") Console.ForegroundColor = ConsoleColor.Gray;
                if (ship.Name == "Submarine") Console.ForegroundColor = ConsoleColor.Blue;
                if (ship.Name == "Destroyer") Console.ForegroundColor = ConsoleColor.Green;
                if (ship.Name == "Patrol Boat") Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine();
            Console.WriteLine("Please enter the positions for the {0} (size: {1})", ship.Name, ship.Size);
            for (var i = 1; i <= ship.Size; i++)
            {
               Console.WriteLine("Enter position {0} of {1} (i.e A3):", i, ship.Size);
               bool respuesta = ship.AddPosition(Console.ReadLine());

                    if (!respuesta) i--;
            }
         }
            Console.ResetColor();
      }

      private static void InitializeEnemyFleet()
      {
         enemyFleet = GameController.InitializeShips().ToList();

         enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 4 });
         enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 5 });
         enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 6 });
         enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 7 });
         enemyFleet[0].Positions.Add(new Position { Column = Letters.B, Row = 8 });

         enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 6 });
         enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 7 });
         enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 8 });
         enemyFleet[1].Positions.Add(new Position { Column = Letters.E, Row = 9 });

         enemyFleet[2].Positions.Add(new Position { Column = Letters.A, Row = 3 });
         enemyFleet[2].Positions.Add(new Position { Column = Letters.B, Row = 3 });
         enemyFleet[2].Positions.Add(new Position { Column = Letters.C, Row = 3 });

         enemyFleet[3].Positions.Add(new Position { Column = Letters.F, Row = 8 });
         enemyFleet[3].Positions.Add(new Position { Column = Letters.G, Row = 8 });
         enemyFleet[3].Positions.Add(new Position { Column = Letters.H, Row = 8 });

         enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 5 });
         enemyFleet[4].Positions.Add(new Position { Column = Letters.C, Row = 6 });
      }
   }
}
