using grab_the_cheese.interfaces;
using System;
using System.Linq;
using System.Text;

namespace grab_the_cheese.game
{
    class MenuConfig
    {
        private static readonly string headerSymbol = "*";
        private static readonly string footerSymbol = "*";
        private static readonly string lineSymbol = "|";

        private static readonly int marginalLength = 25;
        private static readonly int lineSpacing = 2;

        public void PrintGameBoard(Board board)
        {
            IFieldEntity[,] field = board.Field;

            int fieldLength = field.GetLength(0);

            // Header Print
            Console.WriteLine(MultiplyString("-", fieldLength * 2 + 1));

            // Line print
            for (int i = 0; i < fieldLength; i++)
            {
                Console.Write(lineSymbol);
                for (int j = 0; j < fieldLength; j++)
                {
                    if (field[i, j] == null)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        field[i, j].PrintDisplayValue();
                    }

                    if (j != fieldLength - 1)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine(lineSymbol);
            }

            // Footer Print
            Console.WriteLine(MultiplyString("-", field.GetLength(0) * 2 + 1));
        }

        public void PrintGameMessage(double score)
        {
            string text = $@"
MOVE WITH THE ARROW KEYS
TO PLAY

Score {score}
";

            text = BuildTextBlock(text);

            Console.WriteLine(text);
        }

        public void PrintEndGameMessage(double score)
        {
            string text = $@"
YOU LOST!

Score: {score}
";

            text = BuildTextBlock(text);

            Console.WriteLine(text);
        }

        public void PrintGameChoiceMenu()
        {
            string text = @"
Choose a difficulty

1 - Easy
2 - Normal
3 - Hard
4 - Master Splinter
";
            text = BuildTextBlock(text);

            Console.WriteLine(text);
        }

        public void PrintMenuConfig()
        {
            string text = @"Welcome to the MISHOKOVA game

↓↓↓↓↓ CURRENT STATISTICS ↓↓↓↓↓

Total games: 0
Points collected: 0

↑↑↑↑↑ CURRENT STATISTICS ↑↑↑↑↑

Press ENTER to start a new game

";

            text = BuildTextBlock(text);

            Console.WriteLine(text);
        }

        public string BuildTextBlock(string textBlock)
        {
            string header = BuildHeader();
            string footer = BuildFooter();
            string lineSeparator = BuildLineSeparator();

            string appendedText = string.Join("\n",
                textBlock
                .Split("\n")
                .Select(x => lineSeparator + x));

            return $"{header}\n{appendedText}\n{footer}";
        }

        private string BuildHeader()
        {
            return MultiplyString(headerSymbol, marginalLength);
        }

        private string BuildFooter()
        {
            return MultiplyString(footerSymbol, marginalLength);
        }

        private string BuildLineSeparator()
        {
            return lineSymbol + MultiplyString(" ", lineSpacing);
        }

        private string MultiplyString(string symbol, int times)
        {
            StringBuilder multiplication = new StringBuilder(times);
            for (int i = 0; i < times; i++)
            {
                multiplication.Append(symbol);
            }
            return multiplication.ToString();
        }
    }
}
