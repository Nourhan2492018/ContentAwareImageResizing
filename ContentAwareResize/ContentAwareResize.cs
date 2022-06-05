using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ContentAwareResize
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public class ContentAwareResize
    {
        public struct coord
        {
            public int row;
            public int column;
        }
        //========================================================================================================
        //Your Code is Here:
        //===================
        /// <summary>
        /// Develop an efficient algorithm to get the minimum vertical seam to be removed
        /// </summary>
        /// <param name="energyMatrix">2D matrix filled with the calculated energy for each pixel in the image</param>
        /// <param name="Width">Image's width</param>
        /// <param name="Height">Image's height</param>
        /// <returns>BY REFERENCE: The min total value (energy) of the selected seam in "minSeamValue" & List of points of the selected min vertical seam in seamPathCoord</returns>
        public void CalculateSeamsCost(int[,] energyMatrix, int Width, int Height, ref int minSeamValue, ref List<coord> seamPathCoord)
        {
            int NumColum = Width, NumRow = Height;
            int[,] MatrixMinPath = new int[NumRow, NumColum];
            MatrixMinPath = MatrixMinPathValues(energyMatrix, NumRow, NumColum);
            int[] values = new int[3];
            values = MinSeamValue(MatrixMinPath, NumRow, NumColum);
            minSeamValue = values[0];
            int mi = MinThreeNumber(NumColum, NumRow, values[0]);
            seamPathCoord = new List<coord>();
            seamPathCoord = TraceBack(MatrixMinPath, NumRow, NumColum);
        }
        List<coord> TraceBack(int[,] MatrixMinPath, int NumRow, int NumColum)
        {
            List<coord> trcePath = new List<coord>();
            coord coordMin1, coordMinRow;
            coordMinRow.column = 0;
            coordMinRow.row = 0;
            coordMin1.column = 0;
            coordMin1.row = 0;
            int[] values = new int[3];
            values = MinSeamValue(MatrixMinPath, NumRow, NumColum);
            int row = values[1]; int col = values[2];
            coordMin1.column = col;
            coordMin1.row = row;
            trcePath.Add(coordMin1);
            int checkPath = 0;
            for (int r = NumRow - 1; r > 0; r--)
            {
                coordMinRow.column = 0; coordMinRow.row = 0;
                if ((col == 0) || (col == NumColum - 1))
                {
                    if (col == 0)
                    {
                        checkPath = Math.Min(MatrixMinPath[(r - 1), col], MatrixMinPath[(r - 1), (col + 1)]);
                        if (checkPath == MatrixMinPath[(r - 1), col])
                        {
                            row = r - 1;
                            col = col;
                        }
                        else
                        {
                            row = r - 1;
                            col = col + 1;
                        }
                    }
                    else //if (col == NumColum - 1)
                    {
                        checkPath = Math.Min(MatrixMinPath[(r - 1), col], MatrixMinPath[(r - 1), (col - 1)]);
                        if (checkPath == MatrixMinPath[(r - 1), col])
                        {
                            row = r - 1;
                            col = col;
                        }
                        else
                        {
                            row = r - 1;
                            col = col - 1;
                        }
                    }
                }
                else
                {
                    int m = Math.Min(MatrixMinPath[(r - 1), (col - 1)], MatrixMinPath[(r - 1), (col + 1)]);
                    checkPath = Math.Min(MatrixMinPath[(r - 1), col], m);
                    if (checkPath == MatrixMinPath[(r - 1), col])
                    {
                        row = r - 1;
                        col = col;
                    }
                    else if (checkPath == MatrixMinPath[(r - 1), (col + 1)])
                    {
                        row = r - 1;
                        col = col + 1;
                    }
                    else
                    {
                        row = r - 1;
                        col = col - 1;
                    }
                }
                coordMinRow.row = row;
                coordMinRow.column = col;
                trcePath.Add(coordMinRow);
                trcePath.Reverse();
            }
            return trcePath;
        }
        int MinThreeNumber(int n1, int n2, int n3)
        {
            if (n1 < n2)
            {
                if (n1 < n3)
                    return n1;
                else
                    return n3;
            }
            else
            {
                if (n2 < n3)
                    return n2;
                else
                    return n3;
            }
        }
        int[] MinSeamValue(int[,] MatrixMinPath, int NumRow, int NumColum)
        {
            int[] Values = new int[3];
            int row = 0; int col = 0;
            int min = MatrixMinPath[NumRow - 1, 0];
            int i = 1;
            while (i < NumColum)
            {
                if (min > MatrixMinPath[NumRow - 1, i])
                {
                    min = MatrixMinPath[NumRow - 1, i];
                    col = i;
                    row = NumRow - 1;
                }
                i++;
            }
            Values[0] = min;
            Values[1] = row;
            Values[2] = col;
            return Values;
        }
        int[,] MatrixMinPathValues(int[,] energyMatrix, int NumRow, int NumColum)
        {
            int[,] MinPath = new int[NumRow, NumColum];
            int k = 0;
            while (k < NumColum)
            {
                MinPath[0, k] = energyMatrix[0, k];
                k++;
            }
            int i = 1;
            while (i < NumRow)
            {
                for (int j = 0; j < NumColum; j++)
                {
                    if ((j == 0) || (j == NumColum - 1))
                    {
                        if (j == 0)
                        {
                            MinPath[i, j] = Math.Min(MinPath[(i - 1), j], MinPath[(i - 1), (j + 1)]) + energyMatrix[i, j];
                        }
                        else // (j == NumColum - 1)
                        {
                            MinPath[i, j] = Math.Min(MinPath[(i - 1), j], MinPath[(i - 1), (j - 1)]) + energyMatrix[i, j];
                        }
                    }
                    else
                    {
                        int m = Math.Min(MinPath[(i - 1), (j - 1)], MinPath[(i - 1), (j + 1)]);
                        MinPath[i, j] = Math.Min(MinPath[(i - 1), j],
                            m) + energyMatrix[i, j];
                    }
                }
                i++;
            }
            return MinPath;
        }
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO 
        // *****************************************
        #region DON'TCHANGETHISCODE
        public MyColor[,] _imageMatrix;
        public int[,] _energyMatrix;
        public int[,] _verIndexMap;
        public ContentAwareResize(string ImagePath)
        {
            _imageMatrix = ImageOperations.OpenImage(ImagePath);
            _energyMatrix = ImageOperations.CalculateEnergy(_imageMatrix);
            int _height = _energyMatrix.GetLength(0);
            int _width = _energyMatrix.GetLength(1);
        }
        public void CalculateVerIndexMap(int NumberOfSeams, ref int minSeamValueFinal, ref List<coord> seamPathCoord)
        {
            int Width = _imageMatrix.GetLength(1);
            int Height = _imageMatrix.GetLength(0);

            int minSeamValue = -1;
            _verIndexMap = new int[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    _verIndexMap[i, j] = int.MaxValue;

            bool[] RemovedSeams = new bool[Width];
            for (int j = 0; j < Width; j++)
                RemovedSeams[j] = false;

            for (int s = 1; s <= NumberOfSeams; s++)
            {
                CalculateSeamsCost(_energyMatrix, Width, Height, ref minSeamValue, ref seamPathCoord);
                minSeamValueFinal = minSeamValue;

                //Search for Min Seam # s
                int Min = minSeamValue;

                //Mark all pixels of the current min Seam in the VerIndexMap
                if (seamPathCoord.Count != Height)
                    throw new Exception("You selected WRONG SEAM");
                for (int i = Height - 1; i >= 0; i--)
                {
                    if (_verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column] != int.MaxValue)
                    {
                        string msg = "overalpped seams between seam # " + s + " and seam # " + _verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column];
                        throw new Exception(msg);
                    }
                    _verIndexMap[seamPathCoord[i].row, seamPathCoord[i].column] = s;
                    //remove this seam from energy matrix by setting it to max value
                    _energyMatrix[seamPathCoord[i].row, seamPathCoord[i].column] = 100000;
                }

                //re-calculate Seams Cost in the next iteration again
            }
        }
        public void RemoveColumns(int NumberOfCols)
        {
            int Width = _imageMatrix.GetLength(1);
            int Height = _imageMatrix.GetLength(0);
            _energyMatrix = ImageOperations.CalculateEnergy(_imageMatrix);
            int minSeamValue = 0;
            List<coord> seamPathCoord = null;
            //CalculateSeamsCost(_energyMatrix,Width,Height,ref minSeamValue, ref seamPathCoord);
            CalculateVerIndexMap(NumberOfCols, ref minSeamValue, ref seamPathCoord);
            MyColor[,] OldImage = _imageMatrix;
            _imageMatrix = new MyColor[Height, Width - NumberOfCols];
            for (int i = 0; i < Height; i++)
            {
                int cnt = 0;
                for (int j = 0; j < Width; j++)
                {
                    if (_verIndexMap[i, j] == int.MaxValue)
                        _imageMatrix[i, cnt++] = OldImage[i, j];
                }
            }

        }
        #endregion
    }
}