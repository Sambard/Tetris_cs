
/*//////////////////////////////////////////  I
tetrominos[0] = ("XXXX" +
                    "...." +
                    "...." +
                    "....");
//////////////////////////////////////////  T
tetrominos[1] = ("..X." +
                    ".XXX" +
                    "...." +
                    "....");
//////////////////////////////////////////  O
tetrominos[2] = (".XX." +
                    ".XX." +
                    "...." +
                    "....");
//////////////////////////////////////////  Z
tetrominos[3] = (".XX." +
                    "..XX" +
                    "...." +
                    "....");
//////////////////////////////////////////  S
tetrominos[4] = ("..XX" +
                    ".XX." +
                    "...." +
                    "....");
//////////////////////////////////////////  L
tetrominos[5] = ("...X" +
                    ".XXX" +
                    "...." +
                    "....");
//////////////////////////////////////////  J
tetrominos[6] = (".X.." +
                    ".XXX" +
                    "...." +
                    "....");
//////////////////////////////////////////*/


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
        static bool             b_Game_Over             = false;
        static C_Tetrominos     tetrominos              = new C_Tetrominos();
        static C_Block[]        Field                   = new C_Block[i_Field_Height * i_Field_Width];
        static Random           rand                    = new Random();

        static void Main(string[] args)
        {
            Console.SetWindowSize(i_Field_Width + 8, i_Field_Height + 2);
            Console.SetBufferSize(i_Field_Width + 8, i_Field_Height + 2);
            Console.CursorVisible = false;
            change_C_Tetrominos(rand.Next(7));
            fill_Field();
            print_Field();
            int num = 0;
            game_loop();
/*            TimerCallback tm = new TimerCallback(game_loop);
            Timer timer = new Timer(tm, num, 0, 2000);*/
        }

        static void game_loop(/*object obj*/)
		{
            var sw = new Stopwatch();
            while (!b_Game_Over)
            {
                Console.Clear();
                print_Field();
                sw.Restart();
                while (sw.ElapsedMilliseconds <= i_Frame_Ms)
                {
                    if (Read_Movement() == 1)
                      break;
                }
                tetrominos.set_x(tetrominos.get_x() + i_Field_Width);
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
                    break;
                case ConsoleKey.D:
                    break;
                case ConsoleKey.Q:
                    break;
                case ConsoleKey.E:
                    break;
            }
            return 0;
        }

        static bool to_the_Right()
		{
			for (int i = 0; i < 16; i++)
			{
               // if (tetrominos.get_tetromino()[])
			}
            return false;
		}

        static void print_Field()
		{
			for (int i = 0; i < i_Field_Height * i_Field_Width; i++)
			{
                if (i % i_Field_Width == 0)
                    Console.Write(c_block);
                if (tetrominos.get_x() <= i && i <= tetrominos.get_x() + 3  && tetrominos.get_tetromino()[i - tetrominos.get_x()] == 'X' ||
                    tetrominos.get_x() + i_Field_Width <= i && i <= tetrominos.get_x() + i_Field_Width + 3 && tetrominos.get_tetromino()[i - (tetrominos.get_x() + i_Field_Width) + 4] == 'X' ||
                    tetrominos.get_x() + i_Field_Width * 2 <= i && i <= tetrominos.get_x() + i_Field_Width * 2 + 3 && tetrominos.get_tetromino()[i - (tetrominos.get_x() + i_Field_Width * 2) + 4 * 2] == 'X' ||
                    tetrominos.get_x() + i_Field_Width * 3 <= i && i <= tetrominos.get_x() + i_Field_Width * 3 + 3 && tetrominos.get_tetromino()[i - (tetrominos.get_x() + i_Field_Width * 3) + 4 * 3] == 'X')
                {
                    Console.ForegroundColor = tetrominos.get_Color();
                    Console.Write(c_block);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = Field[i].get_Color();
                    Console.Write(Field[i].get_Block());
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (i % i_Field_Width == i_Field_Width - 1)
                    Console.WriteLine(c_block);
            }
			for (int i = 0; i < i_Field_Width + 2; i++)
                Console.Write(c_block);
            Console.Write('\n');
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