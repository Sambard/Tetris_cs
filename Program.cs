
using System.Diagnostics;
using System;

namespace Tetris_cs
{
    internal class Program
    {
        const int               i_Field_Height          = 20;
        const int               i_Field_Width           = 10;
        const char              c_block                 = '█';
        const int               i_Frame_Ms              = 2000;
        const int               i_Indent                = 6;
        const int               i_block_size            = 1;
        static bool             b_Game_Over             = false;
        static C_Tetrominos     tetrominos              = new C_Tetrominos();
        static C_Block[]        Field                   = new C_Block[i_Field_Height * i_Field_Width];
        static Random           rand                    = new Random();

        static void Main(string[] args)
        {
            Console.SetWindowSize(i_Field_Width + i_Indent * 2, i_Field_Height + i_Indent);
            Console.SetBufferSize(i_Field_Width + i_Indent * 2, i_Field_Height + i_Indent);
            Console.Title = "TETRIS";
            Console.CursorVisible = false;
            change_C_Tetrominos(rand.Next(7));
            print_Border();
            fill_Field();
            Print_Tetromino(c_block);
            game_loop();
        }

        static void game_loop()
		{
            var sw = new Stopwatch();
            while (!b_Game_Over)
            {
                Print_Tetromino(c_block);

                sw.Restart();
                while (sw.ElapsedMilliseconds <= i_Frame_Ms)
                {
                    if (Read_Movement() == 1)
                      break;
                }
                if (Move_To_The_Down())
                {
                    Print_Tetromino(' ');
                    if (!Tetromino_To_The_Down())
                        tetrominos.set_x(tetrominos.get_x() + i_Field_Width);
                    Print_Tetromino(c_block);
                }
                else
                {
					for (int i = 0; i < 16; i++)
					{
                        if (tetrominos.get_tetromino()[i] == 'X')
						{
                            Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4].set_Block('X');
                            Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4].set_Color(tetrominos.get_Color());
                        }
					}
					for (int i = 0; i < i_Field_Height; i++)
					{
                        int t = 0;
						for (int j = 0; j < i_Field_Width; j++)
                            if (Field[i * i_Field_Width + j].get_Block() == 'X')
                                t++;
                        if (t == i_Field_Width)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                                for (int j = 0; j < i_Field_Width; j++)
                                {
                                    Console.SetCursorPosition(i_Indent + (i * i_Field_Width + j) % i_Field_Width, (i * i_Field_Width + j) / i_Field_Width);
                                    Console.Write(' ');
                                }
                                //score посчииать
							for (int j = i * i_Field_Width - 1; j > -1; j--)
							{
                                Field[j + i_Field_Width].set_Block(Field[j].get_Block());
                                Field[j + i_Field_Width].set_Color(Field[j].get_Color());
                                Field[j].set_Block(' ');
                                Field[j].set_Color(ConsoleColor.White);
							}
                            Print_Field();
                        }
                    }
                    change_C_Tetrominos(rand.Next(7));
                }
            }
		}

        static void Print_Field()
		{
            for (int i = 0; i < i_Field_Width * i_Field_Height - 1; i++)
            {
                Console.ForegroundColor = Field[i].get_Color();
                Console.SetCursorPosition(i_Indent + i % i_Field_Width, i / i_Field_Width);
                if (Field[i].get_Block() == 'X')
                    Console.Write(c_block);
                else
                    Console.Write(' ');
            }
        }

        static void Print_Tetromino(char c)
		{
            Console.ForegroundColor = tetrominos.get_Color();
            for (int i = 0; i < 16; i++)
			{
                if (tetrominos.get_tetromino()[i] == 'X')
				{
                    if (Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4].get_Block() == 'X')
					{
                        b_Game_Over = true;
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("GAME_OVER");
                        Console.ReadLine();
                        return;//конец игры
					}
                    Console.SetCursorPosition(i_Indent + tetrominos.get_x() % i_Field_Width + i % 4, tetrominos.get_x() / i_Field_Width + i / 4 );
                    Console.Write(c);
                }
			}
		}

        static int Read_Movement()
        {
            if (!Console.KeyAvailable)
                return 0;
            ConsoleKey key = Console.ReadKey(true).Key;
            switch(key)
            {
                case ConsoleKey.S:
                    return 1;
                case ConsoleKey.A:
                    if (Move_To_The_Left())
                    {
                        Print_Tetromino(' ');
                        if (!Tetromino_To_The_Left())
                            tetrominos.set_x(tetrominos.get_x() - 1);
                        Print_Tetromino(c_block);
                    }
                    break;
                case ConsoleKey.D:
                    if (Move_To_The_Right())
                    {
                        Print_Tetromino(' ');
                        if (!Tetromino_To_The_Right())
                            tetrominos.set_x(tetrominos.get_x() + 1);
                        Print_Tetromino(c_block);
                    }
                    break;
                case ConsoleKey.Q:
                    break;
                case ConsoleKey.E:
                    break;
            }
            return 0;
        }

        static bool Move_To_The_Down()
        {
            int t = 0;
            for (int i = 0; i < 16; i++)
                if (tetrominos.get_tetromino()[i] == 'X' &&
                    (tetrominos.get_x() + i / 4 * i_Field_Width + i % 4) / i_Field_Width < i_Field_Height - 1 &&
                    Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4 + i_Field_Width].get_Block() != 'X')
                            t++;
            if (t == 4)
                return true;
            return false;
        }

        static bool Tetromino_To_The_Down()
        {
            if (tetrominos.get_tetromino()[12] != 'X' &&
                tetrominos.get_tetromino()[13] != 'X' &&
                tetrominos.get_tetromino()[14] != 'X' &&
                tetrominos.get_tetromino()[15] != 'X')
            {
                char[] tet = tetrominos.get_tetromino().ToCharArray();
                for (int i = 15; i > -1; i--)
                {
                    if (tet[i] == 'X')
                    {
                        tet[i] = '.';
                        tet[i + 4] = 'X';
                    }
                }
                tetrominos.set_tetromino(new string(tet));
                return true;
            }
            return false;
        }

        static bool Move_To_The_Right()
		{
            int t = 0;
			for (int i = 0; i < 16; i++)
                if (tetrominos.get_tetromino()[i] == 'X' &&
                    (tetrominos.get_x() + i / 4 * i_Field_Width + i % 4) % i_Field_Width != i_Field_Width - 1 &&
                    Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4 + 1].get_Block() != 'X')
                    t++;  
            if (t == 4)
                return true;
            return false;
		}

        static bool Tetromino_To_The_Right()
		{
            if (tetrominos.get_tetromino()[3] != 'X' && 
                tetrominos.get_tetromino()[7] != 'X' && 
                tetrominos.get_tetromino()[11] != 'X' && 
                tetrominos.get_tetromino()[15] != 'X')
			{
                char [] tet = tetrominos.get_tetromino().ToCharArray();
				for (int i = 15; i > -1; i--)
				{
                    if (tet[i] == 'X')
					{
                        tet[i] = '.';
                        tet[i + 1] = 'X';
					}
				}
                tetrominos.set_tetromino(new string(tet));
                return true;
            }
            return false;
		}

        static bool Move_To_The_Left()
        {
            int t = 0;
            for (int i = 0; i < 16; i++)
                    if (tetrominos.get_tetromino()[i] == 'X' &&
                    (tetrominos.get_x() + i / 4 * i_Field_Width + i % 4) % i_Field_Width != 0 &&
                    Field[tetrominos.get_x() + i / 4 * i_Field_Width + i % 4 - 1].get_Block() != 'X')
                    t++;
            if (t == 4)
                return true;
            return false;
        }

        static bool Tetromino_To_The_Left()
        {
            if (tetrominos.get_tetromino()[0] != 'X' &&
                tetrominos.get_tetromino()[4] != 'X' &&
                tetrominos.get_tetromino()[8] != 'X' &&
                tetrominos.get_tetromino()[12] != 'X')
            {
                char[] tet = tetrominos.get_tetromino().ToCharArray();
                for (int i = 0; i < 16; i++)
                {
                    if (tet[i] == 'X')
                    {
                        tet[i] = '.';
                        tet[i - 1] = 'X';
                    }
                }
                tetrominos.set_tetromino(new string(tet));
                return true;
            }
            return false;
        }

        static void print_Border()
		{
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < i_Field_Height; i++)
			{
                Console.SetCursorPosition(i_Indent - i_block_size, i);
                Console.Write(c_block);
                Console.SetCursorPosition(i_Indent - i_block_size + i_Field_Width + 1, i);
                Console.Write(c_block);
            }
			for (int i = 0; i < i_Field_Width + 2; i++)
			{
                Console.SetCursorPosition(i_Indent - i_block_size + i, i_Field_Height);
                Console.Write(c_block);
            }
        }

        static void fill_Field()
        {
            int i = 0;
            while (i < i_Field_Height * i_Field_Width)
                Field[i++] = new C_Block(' ');
		}

        static void change_C_Tetrominos(int t)
        {
			switch (t)
			{
                case 0:
                    tetrominos.set_Color(ConsoleColor.Cyan);
                    tetrominos.set_tetromino   ("XXXX" +
                                                "...." +
                                                "...." +
                                                "....");
                    break;
                case 1:
                    tetrominos.set_Color(ConsoleColor.DarkMagenta);
                    tetrominos.set_tetromino   ("..X." +
                                                ".XXX" +
                                                "...." +
                                                "....");
                    break;
                case 2:
                    tetrominos.set_Color(ConsoleColor.Yellow);
                    tetrominos.set_tetromino   (".XX." +
                                                ".XX." +
                                                "...." +
                                                "....");
                    break;
                case 3:
                    tetrominos.set_Color(ConsoleColor.Red);
                    tetrominos.set_tetromino   (".XX." +
                                                "..XX" +
                                                "...." +
                                                "....");
                    break;
                case 4:
                    tetrominos.set_Color(ConsoleColor.Green);
                    tetrominos.set_tetromino   ("..XX" +
                                                ".XX." +
                                                "...." +
                                                "....");
                    break;
                case 5:
                    tetrominos.set_Color(ConsoleColor.Gray);
                    tetrominos.set_tetromino   ("...X" +
                                                ".XXX" +
                                                "...." +
                                                "....");
                    break;
                case 6:
                    tetrominos.set_Color(ConsoleColor.Blue);
                    tetrominos.set_tetromino   (".X.." +
                                                ".XXX" +
                                                "...." +
                                                "....");
                    break;
			}
            tetrominos.set_x(i_Field_Width / 2 - 2);
        }
    }
}