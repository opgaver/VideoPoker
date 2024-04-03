using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

using VideoPoker.Core;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine(File.ReadAllText("welcome.txt"));
Console.ReadKey();

Game game = new(25);

do
{
    Console.Clear();
    Console.WriteLine("Video Poker");
    Console.WriteLine("-----------");
    Console.WriteLine($"This is your first draw - your wallet contains ${game.Wallet}");
    Console.WriteLine("Enter bet between 1-5 or enter for 1");
    
    var bet = ReadNumber();
    if (bet == 0) bet = 1;
    Console.WriteLine($"Ok - you bet ${bet}:");
    game.FirstDraw(bet);
    game.Hand.ConsoleWrite();
    Console.WriteLine();
    Console.WriteLine("To hold one or more cards please enter numbers like 12, 124, 5 etc. Press enter for none");
    int[]? holdNumbers = null;
    do
    {
        string? holds = Console.ReadLine();
        holds = holds?.Replace(" ", "").Replace(",", "");
        if (holds == "")
        {
            holdNumbers = Array.Empty<int>();
            break;
        }

        holdNumbers = ParseStringToUniqueNumbers(holds);
        if (holdNumbers != null)
            break;
        Console.WriteLine("Invalid input - press any key to try again");
    } while (true);
    Console.WriteLine($"Ok - holding {(holdNumbers.Length == 0 ? "none" : String.Join(" ", holdNumbers.OrderBy(i => i)))} ");
    Console.WriteLine($"This is your second draw:");

    var prize = game.SecondDraw(holdNumbers.Select(i=>i-1).ToArray());
    game.Hand.ConsoleWrite();
    
    Console.WriteLine();
    if (prize == Prizes.AllOther)
    {
        Console.WriteLine();
        Console.WriteLine($"Sorry - you didn't win anything -  your wallet contains ${game.Wallet}");
    }
    else
    {
        PlayWinningSound();
        Console.WriteLine();
        Console.WriteLine($"Congratulations - you won {prize.Name} and ${prize.Value * bet} - your wallet contains ${game.Wallet}");
    }
    Console.WriteLine();
    Console.WriteLine("Press any key for next game");
    Console.ReadKey();
} while (game.Wallet > 0);

Console.WriteLine("Sorry - you are out of money - game over");

int ReadNumber()
{
    var r = Console.ReadKey(true).KeyChar switch
    {
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '\r' => 0, // enter key = 0
        _ => 1
    };
    return r;
}

int[]? ParseStringToUniqueNumbers(string? input)
{
    if (string.IsNullOrEmpty(input)) return null;

    var distinctNumbers = input.Distinct().Where(c => c >= '1' && c <= '5').ToArray();

    if (distinctNumbers.Length != input.Length) return null;

    return distinctNumbers.Select(c => int.Parse(c.ToString())).ToArray();
}
void PlayWinningSound()
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        // Play a sequence of beeps to create a winning sound
        Console.Beep(440, 100); // A4 note for 200 milliseconds
        Console.Beep(523, 200); // C5 note for 200 milliseconds
        Console.Beep(659, 250); // E5 note for 200 milliseconds
        Console.Beep(880, 300); // A5 note for 300 milliseconds
    }
}
