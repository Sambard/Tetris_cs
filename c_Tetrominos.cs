using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_cs
{
	internal class C_Tetrominos
	{
		private string				tetromino	=	"...." +
													"...." +
													"...." +
													"....";
		private ConsoleColor		CC_Color	= ConsoleColor.White;
		private int					x			= 0;

		public void				set_tetromino(string tetromino)
		{ this.tetromino = tetromino; }


		public void				set_Color(ConsoleColor CC_Color)
		{ this.CC_Color = CC_Color; }
		
		public ConsoleColor		get_Color()
		{ return CC_Color; }

		public string			get_tetromino()
		{ return tetromino; }

		public void set_x(int x)
		{ this.x = x; }

		public int get_x()
		{ return x; }
	}
}
