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
      int decimalIndex = findDecimal();
      if (isNegative())
      {
        for (int i = 2; i < numberValue.Length; i++)
        {
          if (!char.IsDigit(numberValue[i]))
          {
            if (i == decimalIndex)
            {
              continue;
            }
            return false;
          }
        }
      }
      for (int i = 1; i < numberValue.Length; i++)
      {
        if (!char.IsDigit(numberValue[i]))
        {
          if (i == decimalIndex)
          {
            continue;
          }
          return false;
        }
      }
			return true;
		}

    public int findDecimal()
    {
      return numberValue.IndexOf('.');
    }

    public LargeNumber RemoveDecimal()
    {
      int decimalIndex = findDecimal();
      if (decimalIndex == -1)
      {
        return this;
      }
      string newValue = numberValue.Remove(decimalIndex, 1);
      return new LargeNumber(newValue);
    }

    public LargeNumber RemoveZerosAfterDecimal()
    {
      int decimalIndex = findDecimal();
      if (decimalIndex == -1)
      {
        return this;
      }
      string newValue = numberValue;
      int i = newValue.Length - 1;
      while (i > decimalIndex && newValue[i] == '0')
      {
        newValue = newValue.Remove(i, 1);
        i--;
      }
      if (i == decimalIndex)
      {
        newValue = newValue.Remove(decimalIndex, 1);
      }
      return new LargeNumber(newValue);
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

      int decimalIndexLeft = left.findDecimal();
      int decimalIndexRight = right.findDecimal();
      if (decimalIndexLeft != -1 || decimalIndexRight != -1)
      {
        left.RemoveZerosAfterDecimal();
        right.RemoveZerosAfterDecimal();
        int decimalPlacesLeft = decimalIndexLeft != -1 ? left.numberValue.Length - decimalIndexLeft - 1 : 0;
        int decimalPlacesRight = decimalIndexRight != -1 ? right.numberValue.Length - decimalIndexRight - 1 : 0;
        int maxDecimalPlaces = Math.Max(decimalPlacesLeft, decimalPlacesRight);

        string leftValue = left.numberValue.Replace(".", "");
        string rightValue = right.numberValue.Replace(".", "");

        leftValue = leftValue.PadRight(leftValue.Length + (maxDecimalPlaces - decimalPlacesLeft), '0');
        rightValue = rightValue.PadRight(rightValue.Length + (maxDecimalPlaces - decimalPlacesRight), '0');

        LargeNumber newLeft = new LargeNumber(leftValue);
        LargeNumber newRight = new LargeNumber(rightValue);

        LargeNumber sum = newLeft + newRight;

        string sumValue = sum.GetValue();
        if (maxDecimalPlaces > 0)
        {
          if (sumValue.Length <= maxDecimalPlaces)
          {
            sumValue = sumValue.PadLeft(maxDecimalPlaces + 1, '0');
          }
          sumValue = sumValue.Insert(sumValue.Length - maxDecimalPlaces, ".");
        }
        return new LargeNumber(sumValue).RemoveZerosAfterDecimal();
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
        right = temp;
			}

      int decimalIndexLeft = left.findDecimal();
      int decimalIndexRight = right.findDecimal();
      if (decimalIndexLeft != -1 || decimalIndexRight != -1)
      {
        left.RemoveZerosAfterDecimal();
        right.RemoveZerosAfterDecimal();
        int decimalPlacesLeft = decimalIndexLeft != -1 ? left.numberValue.Length - decimalIndexLeft - 1 : 0;
        int decimalPlacesRight = decimalIndexRight != -1 ? right.numberValue.Length - decimalIndexRight - 1 : 0;
        int maxDecimalPlaces = Math.Max(decimalPlacesLeft, decimalPlacesRight);

        string leftValue = left.numberValue.Replace(".", "");
        string rightValue = right.numberValue.Replace(".", "");

        leftValue = leftValue.PadRight(leftValue.Length + (maxDecimalPlaces - decimalPlacesLeft), '0');
        rightValue = rightValue.PadRight(rightValue.Length + (maxDecimalPlaces - decimalPlacesRight), '0');

        LargeNumber newLeft = new LargeNumber(leftValue);
        LargeNumber newRight = new LargeNumber(rightValue);

        LargeNumber diff = newLeft - newRight;

        string diffValue = diff.GetValue();
        if (maxDecimalPlaces > 0)
        {
          if (diffValue.Length <= maxDecimalPlaces)
          {
            diffValue = diffValue.PadLeft(maxDecimalPlaces + 1, '0');
          }
          diffValue = diffValue.Insert(diffValue.Length - maxDecimalPlaces, ".");
        }

        if (isNegative)
        {
          diffValue = "-" + diffValue;
        }

        return new LargeNumber(diffValue).RemoveZerosAfterDecimal();
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

      int decimalIndexLeft = left.findDecimal();
      int decimalIndexRight = right.findDecimal();
      if (decimalIndexLeft != -1 || decimalIndexRight != -1)
      {
        left.RemoveZerosAfterDecimal();
        right.RemoveZerosAfterDecimal();
        int decimalPlacesLeft = decimalIndexLeft != -1 ? left.numberValue.Length - decimalIndexLeft - 1 : 0;
        int decimalPlacesRight = decimalIndexRight != -1 ? right.numberValue.Length - decimalIndexRight - 1 : 0;
        int totalDecimalPlaces = decimalPlacesLeft + decimalPlacesRight;

        LargeNumber newLeft = left.RemoveDecimal();
        LargeNumber newRight = right.RemoveDecimal();

        LargeNumber prod = newLeft * newRight;
        string prodValue = prod.GetValue();
        if (totalDecimalPlaces > 0)
        {
          if (prodValue.Length <= totalDecimalPlaces)
          {
            prodValue = prodValue.PadLeft(totalDecimalPlaces + 1, '0');
          }
          prodValue = prodValue.Insert(prodValue.Length - totalDecimalPlaces, ".");
        }

        if (isNegative)
        {
          prodValue = "-" + prodValue;
        }

        return new LargeNumber(prodValue).RemoveZerosAfterDecimal();
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

      int decimalIndexLeft = deljenik.findDecimal();
      int decimalIndexRight = delilac.findDecimal();
      if (decimalIndexLeft != -1 || decimalIndexRight != -1)
      {
        deljenik.RemoveZerosAfterDecimal();
        delilac.RemoveZerosAfterDecimal();
        int decimalPlacesLeft = decimalIndexLeft != -1 ? deljenik.numberValue.Length - decimalIndexLeft - 1 : 0;
        int decimalPlacesRight = decimalIndexRight != -1 ? delilac.numberValue.Length - decimalIndexRight - 1 : 0;

        string leftValue = deljenik.numberValue.Replace(".", "");
        string rightValue = delilac.numberValue.Replace(".", "");

        leftValue = leftValue.PadRight(leftValue.Length + decimalPlacesRight, '0');

        LargeNumber newLeft = new LargeNumber(leftValue);
        LargeNumber newRight = new LargeNumber(rightValue);

        LargeNumber decimalResult = newLeft / newRight;
        string decimalResultValue = decimalResult.GetValue();
        if (decimalPlacesLeft > decimalPlacesRight)
        {
          int decimalPlacesDiff = decimalPlacesLeft - decimalPlacesRight;
          if (decimalResultValue.Length <= decimalPlacesDiff)
          {
            decimalResultValue = decimalResultValue.PadLeft(decimalPlacesDiff + 1, '0');
          }
          decimalResultValue = decimalResultValue.Insert(decimalResultValue.Length - decimalPlacesDiff - 1, ".");
        }

        if (isNegative)
        {
          decimalResultValue = "-" + decimalResultValue;
        }

        return new LargeNumber(decimalResultValue).RemoveZerosAfterDecimal();
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

      left = left.RemoveZerosAfterDecimal();
      right = right.RemoveZerosAfterDecimal();

			bool bothNegative = left.isNegative() && right.isNegative();

			string leftValue = left.isNegative() ? left.numberValue.Substring(1) : left.numberValue;
			string rightValue = right.isNegative() ? right.numberValue.Substring(1) : right.numberValue;

			int decimalIndexLeft = leftValue.IndexOf('.');
			int decimalIndexRight = rightValue.IndexOf('.');

			string integerPartLeft = decimalIndexLeft >= 0 ? leftValue.Substring(0, decimalIndexLeft) : leftValue;
			string integerPartRight = decimalIndexRight >= 0 ? rightValue.Substring(0, decimalIndexRight) : rightValue;

			string decimalPartLeft = decimalIndexLeft >= 0 ? leftValue.Substring(decimalIndexLeft + 1) : "";
			string decimalPartRight = decimalIndexRight >= 0 ? rightValue.Substring(decimalIndexRight + 1) : "";

			if (integerPartLeft.Length != integerPartRight.Length)
			{
				bool result = integerPartLeft.Length < integerPartRight.Length;
				return bothNegative ? !result : result;
			}

			int maxDecimalLength = Math.Max(decimalPartLeft.Length, decimalPartRight.Length);
			string normalizedLeft = integerPartLeft + decimalPartLeft.PadRight(maxDecimalLength, '0');
			string normalizedRight = integerPartRight + decimalPartRight.PadRight(maxDecimalLength, '0');

      for (int i = 0; i < normalizedLeft.Length; i++)
      {
          if (normalizedLeft[i] != normalizedRight[i])
          {
            bool result = normalizedLeft[i] < normalizedRight[i];
            return bothNegative ? !result : result;
          }
      }

			return false;
		}
		public static bool operator >(LargeNumber left, LargeNumber right)
		{
			return right < left;
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
			// Normalizuj oba broja pre poređenja
			left = left.RemoveZerosAfterDecimal();
			right = right.RemoveZerosAfterDecimal();
			return left.TrimLeadingZeros(left.numberValue) == right.TrimLeadingZeros(right.numberValue);
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
				return this == other;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return numberValue.GetHashCode();
		}
	}
}