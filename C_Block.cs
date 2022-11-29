using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_cs
{
	internal class C_Block
	{
		private char				c_Block;
		private ConsoleColor		CC_Color = ConsoleColor.White;

		public C_Block(char c_Block)
		{ this.c_Block = c_Block; }

		public void			set_Block(char c_Block)
		{ this.c_Block = c_Block; }

		public char			get_Block()
		{ return c_Block; }

		public void			set_Color(ConsoleColor CC_Color)
		{ this.CC_Color = CC_Color; }

		public ConsoleColor	get_Color()
		{ return CC_Color; }

	}
}
