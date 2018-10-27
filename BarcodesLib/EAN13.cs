using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BarcodesLib
{
    /// <summary>
    /// Respresent static methods for work with barcode EAN-13
    /// </summary>
    public static class EAN13
    {
        /// <summary>
        /// Validate barcode
        /// </summary>
        /// <param name="barcode">Barcode for validation</param>
        /// <returns></returns>
        static public bool Validation(string barcode)
        {   
            if (string.IsNullOrEmpty(barcode) || barcode.Length != 13)
            {
                return false;
            }

            foreach (var digital in barcode)
            {
                if (!char.IsDigit(digital))
                {
                    return false;
                }
            }

            int checkNumb = ParseCharToInt32(barcode[barcode.Length - 1]);

            string barcodeWithoutCheckNumber = new string(barcode.ToCharArray(0, 12));
            int calNumb = CalculateCheckNumber(barcodeWithoutCheckNumber);

            return calNumb == checkNumb;
        }

        /// <summary>
        /// Generate random string barcode 
        /// </summary>
        /// <returns></returns>
        static public string Generate()
        {
            Random random = new Random();
            string barcode = "";

            for (int counter = 0; counter < 12; counter++)
            {
                barcode += random.Next(0, 9);
            }
            barcode += CalculateCheckNumber(barcode);

            return barcode;
        }


        /// <summary>
        /// Draw barcode, validation barcode does inside
        /// </summary>
        /// <param name="surface">Place where will be drawn barcode</param>
        /// <param name="widhtControl">Place widht for drawing</param>
        /// <param name="heightControl">Place height for drawing</param>
        /// <param name="barcode">String barcode</param>
        static public void DrawBarcode(Graphics surface, int widhtControl, int heightControl, string barcode)
        {
            if (!Validation(barcode))
            {
                throw new ArgumentException("Barcode have to be 13 digitals lenght");
            }

            BarcodeOneZeroForm barcodeOneZeroForm = CalculateDrawForm(barcode);

            int countLeftSideBars = 0, countRightSideBars = 0;
            for (int index = 0; index < 6; index++)
            {
                countLeftSideBars += barcodeOneZeroForm.LeftSideBars[index].Length;
                countRightSideBars += barcodeOneZeroForm.RightSideBars[index].Length;
            }
            int count = barcodeOneZeroForm.LeftGuardBars.Length + countLeftSideBars + barcodeOneZeroForm.CentralGuardBars.Length
                + countRightSideBars + barcodeOneZeroForm.RightGuardBars.Length;

            Brush brush = Brushes.Black;
            Font font = new Font("Cambria", 14);
            string firstNumber = barcode[0].ToString();
            string leftSide = barcode.Substring(1, 6);
            string rightSide = barcode.Substring(7, 6);
            float oneNumberWight = surface.MeasureString(firstNumber, font).Width;
            float oneNumberHeight = surface.MeasureString(firstNumber, font).Height;
            float sixNumberWight = surface.MeasureString(leftSide, font).Width;
            float sixNumberHeight = surface.MeasureString(leftSide, font).Height;

            if (oneNumberWight >= widhtControl || oneNumberHeight >= heightControl)
            {
                throw new ArgumentException("Surface for drawing is very small, it can't to draw there");
            }

            SizeF sizeForGuardBars = new SizeF((widhtControl - oneNumberWight) / count, heightControl);
            SizeF sizeForNotGuardBars = new SizeF(sizeForGuardBars.Width, sizeForGuardBars.Height - oneNumberHeight);
            PointF point = new PointF(oneNumberWight, 0);

            point = DrawOneDimentionalArray(surface, point, sizeForGuardBars, barcodeOneZeroForm.LeftGuardBars);
            float leftSideBarsX = point.X;
            point = DrawJaggedArray(surface, point, sizeForNotGuardBars, barcodeOneZeroForm.LeftSideBars);
            float sideBarsWight = point.X - leftSideBarsX;
            point = DrawOneDimentionalArray(surface, point, sizeForGuardBars, barcodeOneZeroForm.CentralGuardBars);
            float rightSideBarsX = point.X;
            point = DrawJaggedArray(surface, point, sizeForNotGuardBars, barcodeOneZeroForm.RightSideBars);
            DrawOneDimentionalArray(surface, point, sizeForGuardBars, barcodeOneZeroForm.RightGuardBars);

            if (surface.MeasureString(leftSide, font).Width > sideBarsWight || surface.MeasureString(rightSide, font).Width > sideBarsWight)
            {
                throw new ArgumentException("Surface for drawing is very small, it can't to draw there");
            }

            float y = sizeForGuardBars.Height - oneNumberHeight;
            surface.DrawString(firstNumber, font, brush, new PointF(0f, y));
            leftSide = IncreaseString(leftSide, sideBarsWight, surface, font);
            surface.DrawString(leftSide, font, Brushes.Black, new PointF(leftSideBarsX, y));
            rightSide = IncreaseString(rightSide, sideBarsWight, surface, font);
            surface.DrawString(rightSide, font, Brushes.Black, new PointF(rightSideBarsX, y));
        }
        
        static private int CalculateCheckNumber(string barcode)
        {
            int[] digitals = new int[12];

            for (int counter = 0; counter < 12; counter++)
            {
                digitals[counter] = ParseCharToInt32(barcode[counter]);
            }

            int sum = 0;
            for (int counter = 0; counter < 12; counter++)
            {
                int coef;
                if (counter % 2 == 0)
                {
                    coef = 1;
                }
                else
                {
                    coef = 3;
                }
                sum += digitals[counter] * coef;
            }

            int remainder = sum % 10;

            if (remainder == 0)
            {
                return 0;
            }
            return 10 - remainder;
        }

        static private string IncreaseString(string numbers, float wight, Graphics surface, Font font)
        {


            bool isRight = true;
            string returnString = numbers;

            int countStrings = numbers.Length;
            string[] numbersArray = new string[countStrings];
            for (int index = 0; index < countStrings; index++)
            {
                numbersArray[index] = numbers[index].ToString();
            }

            while (isRight)
            {
                if (surface.MeasureString(numbers, font).Width <= wight)
                {
                    returnString = numbers;

                    numbers = "";
                    for (int index = 0; index < countStrings; index++)
                    {
                        numbersArray[index] += ' ';
                        numbers += numbersArray[index];
                    }
                }
                else
                {
                    isRight = false;
                }
            }
            return returnString;
        }

        static private PointF DrawOneDimentionalArray(Graphics surface, PointF point, SizeF size, int[] array)
        {
            foreach (var number in array)
            {
                if (number == 1)
                {
                    Brush brush = Brushes.Black;
                    RectangleF rectangle = new RectangleF(point, size);
                    surface.FillRectangle(brush, rectangle);
                }
                point.X += size.Width;
            }
            return point;
        }

        static private PointF DrawJaggedArray(Graphics surface, PointF point, SizeF size, int[][] jaggedArray)
        {
            foreach (var array in jaggedArray)
            {
                SizeF newSize = new SizeF(size.Width, size.Height - 10);
                point = DrawOneDimentionalArray(surface, point, newSize, array);
            }
            return point;
        }

        static private BarcodeOneZeroForm CalculateDrawForm(string barcode)
        {
            int[] leftGuardBars = { 1, 0, 1 }, centerGuardBars = { 0, 1, 0, 1, 0 }, rightGuardBars = { 1, 0, 1 };
            int[][] leftSideBars = new int[6][], rightSideBars = new int[6][];

            Parity[] parity = GetParity(barcode);

            for (int index = 0; index < 6; index++)
            {
                leftSideBars[index] = GetLeftSideOneZeroForm(ParseCharToInt32(barcode[index + 1]), parity[index]);
                rightSideBars[index] = GetRightSideOneZeroForm(ParseCharToInt32(barcode[index + 7]));
            }

            return new BarcodeOneZeroForm(leftGuardBars, centerGuardBars, rightGuardBars, leftSideBars, rightSideBars);
        }

        static private Parity[] GetParity(string barcode)
        {
            int firstNumber = ParseCharToInt32(barcode[0]);
            Parity[] parity = new Parity[6];

            int[] oddParity, evenParity;
            switch (firstNumber)
            {
                case 0:
                    for (int index = 0; index < parity.Length; index++)
                    {
                        parity[index] = Parity.Odd;
                    }
                    break;
                case 1:
                     oddParity = new int[]{ 0, 1, 3 };
                     evenParity = new int[]{2, 4, 5 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 2:
                    oddParity = new int[] { 0, 1, 4 };
                    evenParity = new int[] { 2, 3, 5 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 3:
                    oddParity = new int[] { 0, 1, 5 };
                    evenParity = new int[] { 2, 3, 4 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 4:
                    oddParity = new int[] { 0, 2, 3 };
                    evenParity = new int[] { 1, 4, 5 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 5:
                    oddParity = new int[] { 0, 3, 4 };
                    evenParity = new int[] { 1, 2, 5 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 6:
                    oddParity = new int[] { 0, 4, 5 };
                    evenParity = new int[] { 1, 2, 3 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 7:
                    oddParity = new int[] { 0, 2, 4 };
                    evenParity = new int[] { 1, 3, 5 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 8:
                    oddParity = new int[] { 0, 2, 5 };
                    evenParity = new int[] { 1, 3, 4 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
                case 9:
                    oddParity = new int[] { 0, 3, 5 };
                    evenParity = new int[] { 1, 2, 4 };
                    for (int index = 0; index < 3; index++)
                    {
                        parity[oddParity[index]] = Parity.Odd;
                        parity[evenParity[index]] = Parity.Even;
                    }
                    break;
            }

            return parity;
        }

        static private int[] GetLeftSideOneZeroForm(int number, Parity parity)
        {
            int[] numberOneZeroForm = null;

            switch (parity)
            {
                case Parity.Odd:
                    switch (number)
                    {
                        case 0:
                            numberOneZeroForm = new int[] {0, 0, 0, 1, 1, 0, 1 };
                            break;
                        case 1:
                            numberOneZeroForm = new int[] { 0, 0, 1, 1, 0, 0, 1 };
                            break;
                        case 2:
                            numberOneZeroForm = new int[] { 0, 0, 1, 0, 0, 1, 1 };
                            break;
                        case 3:
                            numberOneZeroForm = new int[] { 0, 1, 1, 1, 1, 0, 1 };
                            break;
                        case 4:
                            numberOneZeroForm = new int[] { 0, 1, 0, 0, 0, 1, 1 };
                            break;
                        case 5:
                            numberOneZeroForm = new int[] { 0, 1, 1, 0, 0, 0, 1 };
                            break;
                        case 6:
                            numberOneZeroForm = new int[] { 0, 1, 0, 1, 1, 1, 1 };
                            break;
                        case 7:
                            numberOneZeroForm = new int[] { 0, 1, 1, 1, 0, 1, 1 };
                            break;
                        case 8:
                            numberOneZeroForm = new int[] { 0, 1, 1, 0, 1, 1, 1 };
                            break;
                        case 9:
                            numberOneZeroForm = new int[] { 0, 0, 0, 1, 0, 1, 1 };
                            break;
                    }
                    break;
                case Parity.Even:
                    switch (number)
                    {
                        case 0:
                            numberOneZeroForm = new int[] { 0, 1, 0, 0, 1, 1, 1 };
                            break;
                        case 1:
                            numberOneZeroForm = new int[] { 0, 1, 1, 0, 0, 1, 1 };
                            break;
                        case 2:
                            numberOneZeroForm = new int[] { 0, 0, 1, 1, 0, 1, 1 };
                            break;
                        case 3:
                            numberOneZeroForm = new int[] { 0, 1, 0, 0, 0, 0, 1 };
                            break;
                        case 4:
                            numberOneZeroForm = new int[] { 0, 0, 1, 1, 1, 0, 1 };
                            break;
                        case 5:
                            numberOneZeroForm = new int[] { 0, 1, 1, 1, 0, 0, 1 };
                            break;
                        case 6:
                            numberOneZeroForm = new int[] { 0, 0, 0, 0, 1, 0, 1 };
                            break;
                        case 7:
                            numberOneZeroForm = new int[] { 0, 0, 1, 0, 0, 0, 1 };
                            break;
                        case 8:
                            numberOneZeroForm = new int[] { 0, 0, 0, 1, 0, 0, 1 };
                            break;
                        case 9:
                            numberOneZeroForm = new int[] { 0, 0, 1, 0, 1, 1, 1 };
                            break;
                    }
                    break;
            }

            return numberOneZeroForm;
        }

        static private int[] GetRightSideOneZeroForm(int number)
        {
            int[] numberOneZeroForm = null;

            switch (number)
            {
                case 0:
                    numberOneZeroForm = new int[] { 1, 1, 1, 0, 0, 1, 0 };
                    break;
                case 1:
                    numberOneZeroForm = new int[] { 1, 1, 0, 0, 1, 1, 0 };
                    break;
                case 2:
                    numberOneZeroForm = new int[] { 1, 1, 0, 1, 1, 0, 0 };
                    break;
                case 3:
                    numberOneZeroForm = new int[] { 1, 0, 0, 0, 0, 1, 0 };
                    break;
                case 4:
                    numberOneZeroForm = new int[] { 1, 0, 1, 1, 1, 0, 0 };
                    break;
                case 5:
                    numberOneZeroForm = new int[] { 1, 0, 0, 1, 1, 1, 0 };
                    break;
                case 6:
                    numberOneZeroForm = new int[] { 1, 0, 1, 0, 0, 0, 0 };
                    break;
                case 7:
                    numberOneZeroForm = new int[] { 1, 0, 0, 0, 1, 0, 0 };
                    break;
                case 8:
                    numberOneZeroForm = new int[] { 1, 0, 0, 1, 0, 0, 0 };
                    break;
                case 9:
                    numberOneZeroForm = new int[] { 1, 1, 1, 0, 1, 0, 0 };
                    break;
            }

            return numberOneZeroForm;
        }

        static private int ParseCharToInt32(char digital)
        {
            string tempString = digital.ToString();
            return Convert.ToInt32(tempString);
        }

        private enum Parity { Odd, Even }

        private class BarcodeOneZeroForm
        {
            public int[] LeftGuardBars { get; set; }
            public int[] CentralGuardBars { get; set; }
            public int[] RightGuardBars { get; set; }

            public int[][] LeftSideBars { get; set; }
            public int[][] RightSideBars { get; set; }

            public BarcodeOneZeroForm(int[] leftGuardBars, int[] centralGuardBars, int[] rightGuardBars, int[][] leftSideBars, int[][] rightSideBars)
            {
                LeftGuardBars = leftGuardBars;
                CentralGuardBars = centralGuardBars;
                RightGuardBars = rightGuardBars;
                LeftSideBars = leftSideBars;
                RightSideBars = rightSideBars;
            }
        }
    }     
}
