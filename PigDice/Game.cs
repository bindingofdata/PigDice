using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigDice
{
    internal class Game
    {
        private readonly Player[] players;
        private Player currentTurnPlayer;
        private string gameState;
        private bool winner;
        private bool keepPlaying;
        private int rollingScore;

        public Game ( string player1, string player2 )
        {
            players = new Player[] { new Player( player1 ), new Player( player2 ) };
            keepPlaying = true;
            NewGame();
        }

        private string UpdateGameState ()
        {
            StringBuilder header = new StringBuilder();

            header.Append( "---=== Pig Dice ===---\n\n" );
#pragma warning disable RCS1197 // Optimize StringBuilder.Append/AppendLine call.
            header.Append( $"Name: {players[0].Name} - Score: {players[0].Score}\n" );
            header.Append( $"Name: {players[1].Name} - Score: {players[1].Score}\n" );
            header.Append( "---------------------------------------------------\n" );
#pragma warning restore RCS1197 // Optimize StringBuilder.Append/AppendLine call.

            return header.ToString();
        }

        public void NewGame ()
        {
            while ( keepPlaying )
            {
                currentTurnPlayer = players[0];
                rollingScore = 0;
                winner = false;
                keepPlaying = true;
                gameState = UpdateGameState();

                while ( !winner )
                {
                    TakeTurn();
                    if ( !IsWinner( currentTurnPlayer, out winner ) )
                        ChangeTurns();
                } // end current game loop
                gameState = UpdateGameState();

                Console.Clear();

                Console.WriteLine( gameState );
                Console.WriteLine( $"{currentTurnPlayer.Name.ToUpper()} IS THE WINNER!" );
                Console.Write( "press any key to continue..." );
                Console.ReadLine();

                string userResponse = "";
                while (userResponse != "y" && userResponse != "n")
                {
                    Console.Clear();
                    Console.WriteLine("---=== Pig Dice ===---\n");
                    Console.WriteLine(@"Would you like to play again? [y/n]");
                    Console.Write("-> ");
                    userResponse = Console.ReadLine().ToLower();

                    if (userResponse != "y" && userResponse != "n")
                    {
                        Console.Clear();
                        Console.WriteLine("---=== Pig Dice ===---\n");
                        Console.WriteLine("Invalid entry. Please enter either \"y\" or \"n\".");
                        Console.Write("press any key to continue...");
                        Console.ReadKey();
                    }
                    else if (userResponse == "n")
                        keepPlaying = false;
                }
            } // end main game loop
        }

        private bool IsWinner ( Player currentTurnPlayer, out bool playerWon )
        {
            return playerWon = currentTurnPlayer.Score >= 100;
        }

        private void TakeTurn ()
        {
            rollingScore = 0;
            Random die = new Random();
            string playerInput = "";
            int firstRoll = 0;
            int secondRoll = 0;
            bool rollAgain = true;

            while ( rollAgain )
            {
                Console.Clear();
                PrintTurn(firstRoll, secondRoll);

                Console.WriteLine( "Would you like to roll the dice? [y/n]" );
                Console.Write( "-> " );
                playerInput = Console.ReadLine().ToLower();

                if ( playerInput == "n" )
                {
                    currentTurnPlayer.Score += rollingScore;
                    rollAgain = false;
                }
                else if (playerInput == "y")
                {
                    firstRoll = die.Next( 1, 7 );
                    secondRoll = die.Next( 1, 7 );

                    rollingScore += firstRoll + secondRoll;
                    
                    if ( firstRoll == 1 || secondRoll == 1 )
                    {
                        rollAgain = false;
                        Console.Clear();
                        Console.WriteLine( "---=== Pig Dice ===---\n" );
                        Console.WriteLine( "You rolled a 1!" );
                        Console.WriteLine( $"Die 1: {firstRoll}" );
                        Console.WriteLine( $"Die 2: {secondRoll}" );
                        Console.WriteLine( $"Points Lost: {rollingScore}\n" );
                        Console.Write( "press any key to continue..." );
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine( "---=== Pig Dice ===---\n" );
                    Console.WriteLine( "Invalid input." );
                    Console.WriteLine( "Please enter \"y\" for Yes, or \"n\" for No.\n" );
                    Console.Write( "press any key to continue..." );
                    Console.ReadKey();
                }
            }
        }

        private void PrintTurn (int firstRoll, int secondRoll)
        {
            gameState = UpdateGameState();

            StringBuilder turnState = new StringBuilder();
            turnState.Append( gameState );
#pragma warning disable RCS1197 // Optimize StringBuilder.Append/AppendLine call.
            turnState.Append( $"IT'S {currentTurnPlayer.Name.ToUpper()}'S TURN!\n" );
            turnState.Append( $"Your last roll ({firstRoll}, {secondRoll}) totaled {firstRoll + secondRoll} points.\n" );
            turnState.Append( $"You have {rollingScore} points so far this turn.\n" );
#pragma warning restore RCS1197 // Optimize StringBuilder.Append/AppendLine call.

            Console.Write(turnState);
        }

        private void ChangeTurns ()
        {
            if ( currentTurnPlayer == players[0] )
                currentTurnPlayer = players[1];
            else
                currentTurnPlayer = players[0];
        }
    }
}
