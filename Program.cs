using System.ComponentModel.Design;

void resetBoard(char[,] b)
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            b[i, j] = '-';
        }
    }
}

void printBoard(char[,] b)
{
    for (int i = 0; i < 3; i++)
    {   
        if(i == 0) {
           Console.WriteLine("\n-----------------");
        }
        for (int j = 0; j < 3; j++)
        {
            Console.Write("  " + b[i, j]);
            if (j < 2)
            {
                Console.Write("  |");
            }
        }
        Console.WriteLine("\n-----------------");
    }
}

void placeMark(char[,] b, char[,] nb, char m, int pos)
{
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            if (Int32.Parse(nb[i,j].ToString()) == pos)
            {
                b[i, j] = m;
            }
        }
    }
}

void playerTurn(char[,] b, char[,] nb, char m)
{
    Console.WriteLine("{0}'s turn!", m);
    Console.WriteLine("Choose a position (1-9)");
    int position;
    bool correct;
    do
    {
        string input = Console.ReadLine();
        correct = Int32.TryParse(input, out position);
        if (position < 1 || position > 9)
        {
            Console.WriteLine("Incorrect position. Try again.");
            correct = false;
        }
        else if (correct)
        {
            placeMark(b, nb, m, position);
            printBoard(b);
        }
        else
        {
            Console.WriteLine("Incorrect position. Try again.");
            correct = false;
        }
    } while (!correct);

}

bool check(char[,] b)
{
    for (int i = 0; i < 3; i++)
    {
        if (b[i, 0] == b[i, 1] && b[i, 1] == b[i, 2] && b[i,2] != '-')
        {
            Console.WriteLine("{0} wins!", b[i, 0]);
            return true;
        }
        else if (b[0, i] == b[1, i] && b[1, i] == b[2, i] && b[2, i] != '-')
        {
            Console.WriteLine("{0} wins!", b[0, i]);
            return true;
        }
    }

    if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2] && b[2, 2] != '-')
    {
        Console.WriteLine("{0} wins!", b[1, 1]);
        return true;
    }
    else if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0] && b[2, 0] != '-')
    {
        Console.WriteLine("{0} wins!", b[1, 1]);
        return true;
    }

    bool tie = true;
    foreach (char c in b)
    {
        if(c == '-') {
            tie = false;
        }
    }

    if (tie)
    {
        Console.WriteLine("Tie!");
        return true;
    }

    return false;

}

void game()
{
    //initialize game
    char[,] board = new char[3, 3];
    char[,] numbers_board = { { '1', '2', '3' },
                              { '4', '5', '6' }, 
                              { '7', '8', '9' }
                              };
    resetBoard(board);
    printBoard(numbers_board);
    printBoard(board);
    bool game_loop = true;
    Random rnd = new Random();
    int round = rnd.Next(2);
    Console.WriteLine(round);

    while (game_loop)
    {
        if (round % 2 == 1)
        {
            playerTurn(board, numbers_board, 'X');
        }
        else
        {
            playerTurn(board, numbers_board, 'O');
        }

        if (check(board))
        {
            game_loop = false;
            Console.ReadKey();
            menu();
        }

        round++;
    }

    Console.ReadKey();
}


void menu()
{
    
    Console.WriteLine(" TTTTT    I    CCCCC");
    Console.WriteLine("   T           C");
    Console.WriteLine("   T      I    C");
    Console.WriteLine("   T      I    C");
    Console.WriteLine("   T      I    CCCCC");

    Console.WriteLine("\n TTTTT    AAAAAA    CCCCC");
    Console.WriteLine("   T      A    A    C");
    Console.WriteLine("   T      AAAAAA    C");
    Console.WriteLine("   T      A    A    C");
    Console.WriteLine("   T      A    A    CCCCC");

    Console.WriteLine("\n TTTTT    OOOOOO    EEEEE");
    Console.WriteLine("   T      O    O    E");
    Console.WriteLine("   T      O    O    EEEE");
    Console.WriteLine("   T      O    O    E");
    Console.WriteLine("   T      OOOOOO    EEEEE");

    Console.WriteLine("\n1 - play, 2 - exit");
    bool valid_option;
    int option;
    do
    {
        string choice = Console.ReadLine();
        valid_option = Int32.TryParse(choice, out option);
        if (option != 1 && option != 2)
        {
            Console.WriteLine("Invalid choice. Please try again.");
            valid_option = false;
        }
        else if (option == 1)
        {
            game();
        }
        else if (option == 2)
        {
            Environment.Exit(0);
        }
    } while (!valid_option);
}

menu();
