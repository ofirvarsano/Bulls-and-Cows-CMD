using System;
using System.Collections.Generic;
using System.Text;

namespace C19_Ex02
{
    public class Game
    {
        private int m_MaxNumOfGuesses;
        private string m_ComputerSelection;
        private List<string> m_Guesses;
        private List<string> m_Results;
        private bool m_PlayerWon;
        private bool m_LastGuess;

        public int MaxNumOfGuesses
        {
            get { return m_MaxNumOfGuesses; }
            set { m_MaxNumOfGuesses = value; }
        }

        public bool LastGuess
        {
            get { return m_LastGuess; }
            set { m_LastGuess = value; }
        }

        public bool PlayerWon
        {
            get { return m_PlayerWon; }
        }

        public Game(int i_MaxNumOfGuesses)
        {
            m_MaxNumOfGuesses = i_MaxNumOfGuesses;
            m_Guesses = new List<string>();
            m_Results = new List<string>();
            m_PlayerWon = false;
            m_LastGuess = false;
            pickSelection();
        }

        private void pickSelection()
        {
            List<string> chars = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            char[] stringChars = new char[4];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                int j = random.Next(chars.Count);
                stringChars[i] = char.Parse(chars[j]);
                char temp = stringChars[i];
                chars.RemoveAt(j);
            }

            m_ComputerSelection = new string(stringChars);
        }

        public string PrintBoard()
        {
            StringBuilder board = new StringBuilder();
            char asciiChar = (char)124;
            string firstLine = string.Format("{0}{1,-9}{0}{2,-9}{0}", asciiChar, "Pins:", "Result:");
            string secondLine = string.Empty;
            if (m_PlayerWon || m_LastGuess)
            {
                secondLine = string.Format("{0}{1,-9}{0}{2,-9}{0}", asciiChar, m_ComputerSelection, string.Empty);
            }
            else
            {
                secondLine = string.Format("{0}{1,-9}{0}{2,-9}{0}", asciiChar, "####", string.Empty);
            }

            string emptyLine = string.Format("{0}{1,-9}{0}{2,-9}{0}", asciiChar, string.Empty, string.Empty);
            string bufferLine = "|=========|=========|";
            board.AppendLine(firstLine).AppendLine(bufferLine).AppendLine(secondLine).AppendLine(bufferLine);
            for(int i = 0; i < m_Guesses.Count; i++)
            {
                string line = string.Format("{0}{1,-9}{0}{2,-9}{0}", asciiChar, m_Guesses[i].ToString(), m_Results[i].ToString());
                board.AppendLine(line).AppendLine(bufferLine);
            }

            for (int i = 0; i < m_MaxNumOfGuesses - m_Guesses.Count; i++)
            {
                board.AppendLine(emptyLine).AppendLine(bufferLine);
            }

            return board.ToString();
        }

        public string NextGuess(string i_guess)
        {
            i_guess = i_guess.ToUpper();
            i_guess = i_guess.Replace(" ", string.Empty);
            m_Guesses.Add(i_guess);
            if (i_guess.Equals(m_ComputerSelection))
            {
                this.m_PlayerWon = true;
            }

            return buildResult(i_guess);
        }

        private string buildResult(string i_guess)
        {
            bool isV = false;
            bool isX = false;
            string xResult = string.Empty;
            string tempResult = string.Empty;

            for (int j = 0; j < i_guess.Length; j++)
            {
                isV = i_guess[j].Equals(m_ComputerSelection.ToCharArray()[j]);
                isX = m_ComputerSelection.Contains(i_guess[j].ToString());
                if (isV)
                {
                    tempResult += "V";
                }
                else if (!isV && isX)
                {
                    xResult += "X";
                }
            }

            tempResult += xResult;
            m_Results.Add(tempResult);
            return PrintBoard();
        }
    }
}
