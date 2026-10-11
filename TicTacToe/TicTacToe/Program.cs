using System;
using System.Threading;

MemoryBoard();

static void InputBoard(char[,] board, bool playerTurn)
{
    bool isValidInput = false;

    while (!isValidInput)
    {
        Console.Clear();

        Console.WriteLine("=== 틱택토 게임 ===");
        Console.WriteLine("두 사람이 X와 O를 번갈아 놓습니다. X가 먼저 시작합니다.");
        Console.WriteLine();

        Console.WriteLine("위치 번호:\n" +
            "1 | 2 | 3\n" +
            "---------\n" +
            "4 | 5 | 6\n" +
            "---------\n" +
            "7 | 8 | 9");
        Console.WriteLine();

        Console.WriteLine("현재 보드:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2)
                {
                    Console.Write(" | ");
                }
            }
            Console.WriteLine();
            if (i < 2)
            {
                Console.WriteLine("---------");
            }
        }

        string whoTurn = playerTurn ? "X" : "O";
        Console.WriteLine($"플레이어 {whoTurn}의 차례입니다.");
        Console.Write("위치를 선택하세요 (1~9): ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int number) && number >= 1 && number <= 9)
        {
            int row = (number - 1) / 3;
            int col = (number - 1) % 3;

            if (board[row, col] != '.')
            {
                Console.WriteLine("이미 선택한 칸입니다. 빈 칸을 선택하세요.");
                Thread.Sleep(1000);
            }
            else
            {
                if (playerTurn)
                {
                    board[row, col] = 'X';
                }
                else
                {
                    board[row, col] = 'O';
                }
                isValidInput = true;
            }
        }
        else
        {
            Console.WriteLine("1부터 9까지의 번호를 입력하세요.");
            Thread.Sleep(1000);
        }
    }
}

static void MemoryBoard()
{
    char[,] board = new char[3, 3];
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 3; j++)
        {
            board[i, j] = '.';
        }
    }
    
    bool playerTurn = true;
    for (int i = 0; i < 9; i++)
    {
        InputBoard(board, playerTurn);
        char win = MatchResult(board);
        if (win != 'n')
        {
            Console.Clear();
            InputBoard(board, playerTurn);
            Console.WriteLine($"플레이어 {win} 승리!");
            return;
        }
        playerTurn = !playerTurn;
        Console.WriteLine();
    }
    Console.Clear();

    for (int r = 0; r < 3; r++)
    {
        for (int c = 0; c < 3; c++)
        {
            Console.Write(board[r, c]);
            if (c < 2) Console.Write(" | ");
        }
        Console.WriteLine();
        if (r < 2) Console.WriteLine("---------");
    }
    Console.WriteLine("무승부 입니다!");
}

static char MatchResult(char[,] board)
{
    for (int i = 0; i < 3; i++)
    {
        if (board[i, 0] != '.' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            return board[i, 0];
        
        if (board[0, i] != '.' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
            return board[0, i];
    }
    if (board[0, 0] != '.' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        return board[0, 0];

    if (board[0, 2] != '.' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        return board[0, 2];

    return 'n';
}