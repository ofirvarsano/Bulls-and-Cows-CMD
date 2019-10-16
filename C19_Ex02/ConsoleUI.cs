using System;

namespace C19_Ex02
{
    public class ConsoleUI
    {
        public void BeginGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Enter the number of guesses you want (4-10): ");
            CheckIfLegalMaxNumOfGuesses(out int o_MaxNumOfGuesses);
            Ex02.ConsoleUtils.Screen.Clear();

            Game game = new Game(o_MaxNumOfGuesses);
            Console.WriteLine(game.PrintBoard());
            string stringTypeGuess = "Please type your next guess (A B C D) or 'Q' to quit";
            Console.WriteLine(stringTypeGuess);
            int countGuesses = 0;
            while (countGuesses < game.MaxNumOfGuesses && !game.PlayerWon)
            {
                string guess = Console.ReadLine();
                if (countGuesses == game.MaxNumOfGuesses - 1)
                {
                    game.LastGuess = true;
                }

                if (guess.Equals("q") || guess.Equals("Q"))
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                if (!CheckIfLegalGuessInput(guess))
                {
                    Console.WriteLine("invalid input");
                    continue;
                }

                Ex02.ConsoleUtils.Screen.Clear();
                countGuesses++;
                Console.WriteLine(game.NextGuess(guess));

                if (game.PlayerWon)
                {
                    Console.WriteLine("You guessed after {0} {1}", countGuesses.ToString(), countGuesses == 1 ? "step!" : "steps!");
                    break;
                }

                if (countGuesses < game.MaxNumOfGuesses)
                {
                    Console.WriteLine(stringTypeGuess);
                }
            }

            if (!game.PlayerWon)
            {
                Console.WriteLine("No more guesses allowed. you lost.");
            }

            AskForNewGame();
        }

        private void AskForNewGame()
        {
            Console.WriteLine("Would you like to start a new game? (Y/N)");

            bool isLegalInput = false;
            while (!isLegalInput)
            {
                string answer = Console.ReadLine();
                answer = answer.ToUpper();
                if (answer.Equals("Y"))
                {
                    isLegalInput = true;
                    BeginGame();
                }
                else if (answer.Equals("N"))
                {
                    isLegalInput = true;
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("invalid input. enter Y or N");
                }
            }
        }

        private void CheckIfLegalMaxNumOfGuesses(out int o_MaxNumOfGuesses)
        {
            o_MaxNumOfGuesses = 0;
            bool isLegalInput = false;
            while (!isLegalInput)
            {
                bool isNumeric = int.TryParse(Console.ReadLine(), out o_MaxNumOfGuesses);
                if (!isNumeric)
                {
                    Console.WriteLine("{0}Wrong input, Number of guesses should be a number between 4-10: ", Environment.NewLine);
                }
                else if (o_MaxNumOfGuesses < 4 || o_MaxNumOfGuesses > 10)
                {
                    Console.WriteLine("{0}Number of guesses should be between 4-10: ", Environment.NewLine);
                }
                else
                {
                    isLegalInput = true;
                }
            }
        }

        private bool CheckIfLegalGuessInput(string i_guess)
        {
            if (i_guess.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < i_guess.Length; i++)
            {
                for (int j = i + 1; j < i_guess.Length; j++)
                {
                    if (i_guess[i] == i_guess[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
