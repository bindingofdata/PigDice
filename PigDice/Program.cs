using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    class Program
    {
        static void Main ( string[] args )
        {
            string menuSelection = "";

            while(MainMenu(out menuSelection))
            {
                // Game exit is hanled in the MainMenu method.
                switch (menuSelection)
                {
                    case "n":
                    case "1":
                        NewGame();
                        break;
                    case "h":
                    case "2":
                    case "?":
                        DisplayHelp();
                        break;
                    default:
                        break;
                }
            }
        }

        private static bool MainMenu(out string menuSelection)
        {
            bool keepPlaying = true;
            StringBuilder menu = new StringBuilder();

            menu.Append(header);
            menu.Append("+ Main Menu +\n");
            menu.Append("1. [N]ew Game\n");
            menu.Append("2. [H]ow to play\n");
            menu.Append("3. [E]xit\n\n");
            menu.Append("Please make your selection...\n");
            menu.Append("-> ");

            Console.Clear();
            Console.Write(menu);
            menuSelection = Console.ReadLine().ToLower();

            // If the user wants to exit, we do it here before the main switch is hit.
            if (menuSelection == "e" ||
                menuSelection == "q" ||
                menuSelection == "3")
            {
                keepPlaying = false;
            }

            return keepPlaying;
        }

        private static void NewGame()
        {
            bool startGame = false;
            string player1 = "";
            string player2 = "";

            while(!startGame)
            {
                Console.Clear();
                Console.Write(header);
                Console.WriteLine("What is the first player's name? ");
                Console.Write("-> ");
                player1 = Console.ReadLine();

                Console.Clear();
                Console.Write(header);
                Console.WriteLine("What is the second player's name? ");
                Console.Write("-> ");
                player2 = Console.ReadLine();

                Console.Clear();
                Console.Write(header);
                Console.WriteLine($"Player 1 = {player1}");
                Console.WriteLine($"Player 2 = {player2}");
                Console.WriteLine("Would you like to start the game with these players? [y/n]");

                if (Console.ReadLine().ToLower() == "y")
                    startGame = true;
            }

            Game newGame = new Game(player1, player2);
        }

        private static void DisplayHelp()
        {
            StringBuilder help = new StringBuilder();

            help.Append(header);
            help.Append("Pig Dice is a game played between two people.\n\n");
            help.Append("On your turn, you can choose one of two actions:\n");
            help.Append("1. Roll the dice.\n");
            help.Append("2. End your turn and save your points.\n\n");
            help.Append("press any key to continue... ");
            Console.Clear();
            Console.Write(help);
            Console.ReadKey();

            help.Clear();
            help.Append(header);
            help.Append("You may roll the dice as many times as you wish on your turn.\n");
            help.Append("However, if either die rolls a 1, your turn ends and you don't get any points.\n\n");
            help.Append("The first player to successfully save 100 or more points wins!\n\n");
            help.Append("press any key to continue... ");
            Console.Clear();
            Console.Write(help);
            Console.ReadKey();
        }

        private const string header = "---=== Pig Dice ===---\n\n";
    }
}
