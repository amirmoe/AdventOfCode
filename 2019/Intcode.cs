using System;
using System.Collections.Generic;

namespace AdventOfCode2019
{
	//Rewritten from python day 9
    public class Intcode
    {
	    private List<int> Instructions { get; }
		private int InputSignal { get; }
		private int OutputSignal { get; set; }
		private int Pointer { get; set; }
		private int RelativeBase { get; set; }
		private bool Halted { get; set; }
      
		public Intcode(List<int> instructions, int inputSignal)
		{
			Instructions = instructions;
			InputSignal = inputSignal;
			OutputSignal = 0;
			Pointer = 0;
			RelativeBase = 0;
			Halted = false;
		}
		
		private int GetOperation()
		{
			var number = Instructions[Pointer].ToString("D2");
			return int.Parse(number.Substring(number.Length - 2, 2));
		}

		private int[] GetMode()
		{
			var number = Instructions[Pointer].ToString("D5");

			return new[]
			{
				int.Parse(number.Substring(2, 1)),
				int.Parse(number.Substring(1, 1)),
				int.Parse(number.Substring(0, 1))
			};
		}
		
		private int GetValue(int mode, int parameter)
		{
			return mode switch
			{
				0 => Instructions[Instructions[Pointer + parameter]],
				1 => Instructions[Pointer + parameter],
				2 => Instructions[Instructions[Pointer + parameter] + RelativeBase],
				_ => 0
			};
		}

		private void SetValue(int mode, int parameter, int value)
		{
			switch (mode)
			{
				case 0:
					Instructions[Instructions[Pointer + parameter]] = value;
					break;
				case 1:
					Instructions[Pointer + parameter] = value;
					break;
				case 2:
					Instructions[Instructions[Pointer + parameter] + RelativeBase] = value;
					break;
			}
		}
		
		public void RunComputer()
		{
			while (Instructions[Pointer] != 99 )
			{
				var operation = GetOperation();
				var mode = GetMode();
				//Console.WriteLine($"Preforming operation {operation} with mode {mode} on pointer {Pointer}");

				switch (operation)
				{
					case 1:
						var addition = GetValue(mode[0], 1) + GetValue(mode[1], 2);
						SetValue(mode[2], 3, addition);
						Pointer += 4;
						break;
					case 2:
						var multiplication = GetValue(mode[0], 1) * GetValue(mode[1], 2);
						SetValue(mode[2], 3, multiplication);
						Pointer += 4;
						break;
					case 3:
						SetValue(mode[0], 1, InputSignal);
						Pointer += 2;
						break;
					case 4:
						OutputSignal = GetValue(mode[0], 1);
						Pointer += 2;
						Console.WriteLine($"Output: {OutputSignal}");
						break;
					case 5:
						var jumpIfTrue = GetValue(mode[0], 1);
						if (jumpIfTrue != 0)
							Pointer = GetValue(mode[1], 2);
						else
							Pointer += 3;
						break;
					case 6:
						var jumpIfFalse = GetValue(mode[0], 1);
						if (jumpIfFalse == 0)
							Pointer = GetValue(mode[1], 2);
						else
							Pointer += 3;
						break;
					case 7:
						var first = GetValue(mode[0], 1);
						var second = GetValue(mode[1], 2);
						SetValue(mode[2],3, first < second ? 1:0 );
						Pointer += 4;
						break;
					case 8:
						var firstValue = GetValue(mode[0], 1);
						var secondValue = GetValue(mode[1], 2);
						SetValue(mode[2],3, firstValue == secondValue ? 1:0 );
						Pointer += 4;
						break;		
					case 9:
						RelativeBase = RelativeBase + GetValue(mode[0], 1);
						Pointer += 2;
						break;
					default:
						Console.WriteLine("Something went wrong");
						break;
				}
				
				if (Instructions[Pointer] == 99)
					Halted = true;
			}
		}
	}
}
