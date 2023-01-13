using grab_the_cheese.game;
using grab_the_cheese.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grab_the_cheese
{
    class MenuConfig
    {
        private static readonly string headerSymbol = "*";
        private static readonly string footerSymbol = "*";
        private static readonly string lineSymbol = "|";

        private static readonly int marginalLength = 35;
        private static readonly int lineSpacing = 2;

        public void PrintGameBoard(Board board)
        {
            IFieldEntity[,] field = board.Field;

            int fieldLength = field.GetLength(0);

            // Header Print
            Console.WriteLine(MultiplyString(headerSymbol, fieldLength * 2 + 1));

            // Line print
            for (int i = 0; i < fieldLength; i++)
            {
                Console.Write(lineSymbol);
                for (int j = 0; j < fieldLength; j++)
                {
                    if (field[i, j] == null)
                    {
                        Console.Write(" ");
                    } else
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
            Console.WriteLine(MultiplyString(headerSymbol, field.GetLength(0) * 2 + 1));
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
        }

        public void PrintMenuConfig()
        {
            string text = @"Welcome to the MISHOKOVA game

↓↓↓↓↓ CURRENT STATISTICS ↓↓↓↓↓

Wins: 0
Loses: 0
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
