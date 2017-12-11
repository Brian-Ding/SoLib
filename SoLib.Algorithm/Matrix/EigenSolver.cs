using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoLib.Algorithm.Matrix
{
    public class EigenSolver
    {
        private Double[,] _matrix;
        private Double[,] _temp;
        private Int32 _dimension;
        private Int32 _maxIteration;
        private const Double TOLERANCE = 0.0001;

        public Double[,] EigenVectors { get; set; }
        public Double[] EigenValues { get; set; }

        public EigenSolver(Double[,] matrix, Int32 maxIteration = 100)
        {
            _matrix = matrix;
            _dimension = _matrix.GetLength(0);
            _maxIteration = maxIteration;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Solve()
        {
            Int32 iteration = 0;
            while (GetOffDiagonalSum() >= TOLERANCE && iteration <= _maxIteration)
            {
                iteration++;
                Int32 i, j;
                Double sin, cos, tan, tau;
                (Int32 _maxOffDiagonalRow, Int32 _maxOffDiagonalColumn, Double w) = FindMaxOffDiagonal();
                if (_maxOffDiagonalRow <= _maxOffDiagonalColumn)
                {
                    i = _maxOffDiagonalRow;
                    j = _maxOffDiagonalColumn;
                }
                else
                {
                    i = _maxOffDiagonalColumn;
                    j = _maxOffDiagonalRow;
                }
                tan = SolveQuadratic(w);
                (sin, cos) = SolveSinCos(tan);
                tau = (1 - cos) / sin;

                InitializeTempMatrix();

                // Update Matrix[i, i]
                _temp[i, i] = _matrix[i, i] - tan * _matrix[i, j];

                // Update Matrix[j, j]
                _temp[j, j] = _matrix[j, j] + tan * _matrix[i, j];

                // Update Matrix[i ,j] = Matrix[j, i] = 0
                _temp[i, j] = _temp[j, i] = 0;

                // Update row i
                for (int k = 0; k < _dimension; k++)
                {
                    if (k != i && k != j)
                    {
                        _temp[i, k] = _temp[k, i] = _matrix[i, k] - sin * (_matrix[j, k] + tau * _matrix[i, k]);
                    }
                }

                // Update row j
                for (int k = 0; k < _dimension; k++)
                {
                    if (k != i && k != j)
                    {
                        _temp[j, k] = _temp[k, j] = _matrix[j, k] + sin * (_matrix[i, k] - tau * _matrix[j, k]);
                    }
                }

                // Multiply Givens rotation matrix


                CopyTempMatrix();
                Debug.WriteLine(_matrix.Print());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private (Int32 _maxOffDiagonalRow, Int32 _maxOffDiagonalColumn, Double w) FindMaxOffDiagonal()
        {
            Double max = 0;
            Int32 _maxOffDiagonalRow = 0;
            Int32 _maxOffDiagonalColumn = 0;

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (i != j)
                    {
                        Double element = Math.Abs(_matrix[i, j]);
                        if (element > max)
                        {
                            max = element;
                            _maxOffDiagonalRow = i;
                            _maxOffDiagonalColumn = j;
                        }
                    }
                }
            }

            Double w = (_matrix[_maxOffDiagonalColumn, _maxOffDiagonalColumn] - _matrix[_maxOffDiagonalRow, _maxOffDiagonalRow]) / (2 * _matrix[_maxOffDiagonalRow, _maxOffDiagonalColumn]);

            return (_maxOffDiagonalRow, _maxOffDiagonalColumn, w);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <returns>t = tanΘ</returns>
        private Double SolveQuadratic(Double w)
        {
            Double t1 = -w + Math.Sqrt(w * w + 1);
            Double t2 = -w - Math.Sqrt(w * w + 1);

            if (t1 >= t2)
            {
                return t2;
            }
            else
            {
                return t1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tan"></param>
        /// <returns></returns>
        private (Double sin, Double cos) SolveSinCos(Double tan)
        {
            Double sin = tan / Math.Sqrt(1 + tan * tan);
            Double cos = 1 / Math.Sqrt(1 + tan * tan);

            return (sin, cos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Double GetOffDiagonalSum()
        {
            Double sum = 0;

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (i != j)
                    {
                        sum += _matrix[i, j] * _matrix[i, j];
                    }
                }
            }

            return sum;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTempMatrix()
        {
            _temp = new Double[_dimension, _dimension];
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    _temp[i, j] = Double.NaN;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CopyTempMatrix()
        {
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    _matrix[i, j] = Double.IsNaN(_temp[i, j]) ? _matrix[i, j] : _temp[i, j];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeEigenVector()
        {
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    EigenVectors[i, j] = 1;
                }
            }
        }
    }
}
