using System;
using System.Collections.Generic;

namespace Calculator.Core
{
	public class LargeNumber
	{
		private static readonly Dictionary<char, int> DigitMap = new()
		{
			{'0', 0},
			{'1', 1},
			{'2', 2},
			{'3', 3},
			{'4', 4},
			{'5', 5},
			{'6', 6},
			{'7', 7},
			{'8', 8},
			{'9', 9}
		};

		private readonly string numberValue;

		public LargeNumber(string number)
		{
			numberValue = TrimLeadingZeros(number);
		}
		public LargeNumber(int number)
		{
			numberValue = TrimLeadingZeros(number.ToString());
		}

		public bool isNegative()
		{
			return numberValue.StartsWith("-");
		}

		public string GetValue()
		{
			return TrimLeadingZeros(numberValue);
		}

		public string TrimLeadingZeros(string number)
		{
			bool isNegative = number.StartsWith("-");
			int i = isNegative ? 1 : 0;

			while (i < number.Length - 1 && number[i] == '0')
			{
				i++;
			}

			if (i == number.Length)
			{
				return "0";
			}

			string result = number.Substring(i);
			return (isNegative && result != "0") ? "-" + result : result;
		}

		public bool IsValid()
		{
			foreach (char c in numberValue)
			{
				if (!char.IsDigit(c))
				{
					return false;
				}
			}
			return true;
		}

		public static LargeNumber operator +(LargeNumber left, LargeNumber right)
		{
			if (left.isNegative() && right.isNegative())
			{
				var l = new LargeNumber(left.numberValue.Substring(1));
				var r = new LargeNumber(right.numberValue.Substring(1));
				var res = l + r;
				return new LargeNumber("-" + res.GetValue());
			}
			if (left.isNegative())
			{
				var l = new LargeNumber(left.numberValue.Substring(1));
				return right - l;
			}
			if (right.isNegative())
			{
				var r = new LargeNumber(right.numberValue.Substring(1));
				return left - r;
			}

			int overflow = 0;
			string result = "";
			for (int i = left.numberValue.Length - 1, j = right.numberValue.Length - 1; i >= 0 || j >= 0; i--, j--)
			{
				int digitLeft = i >= 0 ? DigitMap[left.numberValue[i]] : 0;
				int digitRight = j >= 0 ? DigitMap[right.numberValue[j]] : 0;

				int sum = digitLeft + digitRight + overflow;
				overflow = sum / 10;
				result = (sum % 10).ToString() + result;
			}

			if (overflow > 0)
			{
				result = overflow.ToString() + result;
			}

			return new LargeNumber(result);
		}

		public static LargeNumber operator -(LargeNumber left, LargeNumber right)
		{
			int borrow = 0;
			string result = "";
			bool isNegative = false;
			if (left < right)
			{
				isNegative = true;
				var temp = left;
				left = right;
			}

			for (int i = left.numberValue.Length - 1, j = right.numberValue.Length - 1; i >= 0 || j >= 0; i--, j--)
			{
				int digitLeft = i >= 0 ? DigitMap[left.numberValue[i]] - borrow : 0;
				int digitRight = j >= 0 ? DigitMap[right.numberValue[j]] : 0;

				if (digitLeft < digitRight)
				{
					borrow = 1;
					digitLeft += 10;
					int diff = digitLeft - digitRight;
					result = diff.ToString() + result;
				}
				else
				{
					borrow = 0;
					int diff = digitLeft - digitRight;
					result = diff.ToString() + result;
				}
			}
			if (isNegative)
			{
				result = "-" + result;
			}

			return new LargeNumber(result);
		}

		public static LargeNumber operator *(LargeNumber left, LargeNumber right)
		{
			bool isNegative = false;
			if (left.isNegative() && right.isNegative())
			{
				isNegative = false;
				left = new LargeNumber(left.numberValue.Substring(1));
				right = new LargeNumber(right.numberValue.Substring(1));
			}
			else if (left.isNegative() || right.isNegative())
			{
				isNegative = true;
				if (left.isNegative())
				{
					left = new LargeNumber(left.numberValue.Substring(1));
				}
				else
				{
					right = new LargeNumber(right.numberValue.Substring(1));
				}
			}
			if (left.numberValue == "0" || right.numberValue == "0")
			{
				return new LargeNumber("0");
			}
			if (left.numberValue == "1")
			{
				return right;
			}
			if (right.numberValue == "1")
			{
				return left;
			}
			if (left.numberValue.Length < right.numberValue.Length)
			{
				var temp = left;
				left = right;
				right = temp;
			}

			int overflow = 0;
			string result = "";
			int trailingZeros = 0;
			int totalLength = left.numberValue.Length + right.numberValue.Length;
			List<string> resultsTemp = new();

			for (int i = right.numberValue.Length - 1; i >= 0; i--)
			{
				int digitRight = DigitMap[right.numberValue[i]];

				for (int j = left.numberValue.Length - 1; j >= 0; j--)
				{
					int digitLeft = DigitMap[left.numberValue[j]];
					int prod = digitLeft * digitRight + overflow;
					overflow = prod / 10;
					result = (prod % 10).ToString() + result;
				}

				if (overflow > 0)
				{
					result = overflow.ToString() + result;
					overflow = 0;
				}

				result = result.PadRight(result.Length + trailingZeros, '0');
				resultsTemp.Add(result);
				result = "";
				trailingZeros++;
			}

			LargeNumber resultSum = new LargeNumber("0");
			foreach (var res in resultsTemp)
			{
				resultSum += new LargeNumber(res);
			}

			if (isNegative)
			{
				resultSum = new LargeNumber("-" + resultSum.GetValue());
			}

			return resultSum;
		}

		public static LargeNumber operator *(LargeNumber left, int right)
		{
			return left * new LargeNumber(right);
		}

		public static LargeNumber operator /(LargeNumber left, LargeNumber right)
		{
			if (right.numberValue == "0")
			{
				throw new DivideByZeroException("Deljenje nulom nije dozvoljeno.");
			}

			bool isNegative = false;
			if (left.isNegative() && right.isNegative())
			{
				isNegative = false;
			}
			else if (left.isNegative() || right.isNegative())
			{
				isNegative = true;
			}

			LargeNumber deljenik = left.isNegative() ? new LargeNumber(left.numberValue.Substring(1)) : left;
			LargeNumber delilac = right.isNegative() ? new LargeNumber(right.numberValue.Substring(1)) : right;

			if (deljenik < delilac)
			{
				return new LargeNumber("0");
			}

			string result = "";
			LargeNumber ostatak = new LargeNumber("0");
			string deljenikStr = deljenik.GetValue();

			for (int i = 0; i < deljenikStr.Length; i++)
			{
				string nextVal = ostatak.GetValue();
				if (nextVal == "0") nextVal = "";

				nextVal += deljenikStr[i];

				ostatak = new LargeNumber(nextVal);

				int count = 0;

				while (ostatak >= delilac)
				{
					ostatak -= delilac;
					count++;
				}

				if (result.Length > 0 || count > 0)
					result += count.ToString();
			}

			if (result.Length == 0)
			{
				return new LargeNumber("0");
			}

			if (isNegative)
			{
				result = "-" + result;
			}

			return new LargeNumber(result);
		}

		public static bool operator >(LargeNumber left, LargeNumber right)
		{
			if (left.isNegative() && !right.isNegative())
			{
				return false;
			}
			if (!left.isNegative() && right.isNegative())
			{
				return true;
			}
			if (left.numberValue.Length != right.numberValue.Length)
			{
				return left.numberValue.Length > right.numberValue.Length;
			}

			for (int i = 0; i < left.numberValue.Length; i++)
			{
				if (left.numberValue[i] != right.numberValue[i])
				{
					return left.numberValue[i] > right.numberValue[i];
				}
			}

			return false;
		}
		public static bool operator <(LargeNumber left, LargeNumber right)
		{
			if (left.isNegative() && !right.isNegative())
			{
				return true;
			}
			if (!left.isNegative() && right.isNegative())
			{
				return false;
			}
			if (left.numberValue.Length != right.numberValue.Length)
			{
				return left.numberValue.Length < right.numberValue.Length;
			}

			for (int i = 0; i < left.numberValue.Length; i++)
			{
				if (left.numberValue[i] != right.numberValue[i])
				{
					return left.numberValue[i] < right.numberValue[i];
				}
			}

			return false;
		}
		public static bool operator >=(LargeNumber left, LargeNumber right)
		{
			return !(left < right);
		}
		public static bool operator <=(LargeNumber left, LargeNumber right)
		{
			return !(left > right);
		}
		public static bool operator ==(LargeNumber left, LargeNumber right)
		{
			return left.numberValue == right.numberValue;
		}
		public static bool operator !=(LargeNumber left, LargeNumber right)
		{
			return !(left == right);
		}


		//da se skloni warning
		public override bool Equals(object? obj)
		{
			if (obj is LargeNumber other)
			{
				return this.numberValue == other.numberValue;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return numberValue.GetHashCode();
		}
	}
}