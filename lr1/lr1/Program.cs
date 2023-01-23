new ConsoleApp().Run();

class ConsoleApp
{
    private string selectedMenuItem;

    public void Run()
    {
        ShowMenuAndGetUserInput();
        HandleUserInput();
    }

    private void ShowMenuAndGetUserInput()
    {
        Console.WriteLine("Select menu item");
        Console.WriteLine("1 Write the words of the text 'Lorem ipsum'");
        Console.WriteLine("2 Perform a mathematical operation");
        selectedMenuItem = Console.ReadLine();
    }

    private void HandleUserInput()
    {
        switch (selectedMenuItem)
        {
            case "1":
            {
                Console.WriteLine(ReadTextFromFileAndCropByNumberOfWords());
            }
                break;
            case "2":
            {
                Console.WriteLine(Mult());
            }
                break;
            default:
            {
                Console.WriteLine("Selected wrong menu item");
            }
                break;
        }
    }

    private string ReadTextFromFileAndCropByNumberOfWords()
    {
        Console.WriteLine("Enter number of words");
        int wordsNumber = Int32.Parse(Console.ReadLine());
        StreamReader streamReader =
            new StreamReader("C:\\Users\\olexi\\RiderProjects\\homework\\ConsoleApp1\\text.txt");
        string[] words = streamReader.ReadLine().Split(" ");
        return string.Join(" ", words.Take(wordsNumber));
    }

    private int Mult()
    {
        return 4 * 5;
    }
}