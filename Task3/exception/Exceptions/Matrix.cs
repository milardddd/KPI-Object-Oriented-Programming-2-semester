using System;

namespace Exceptions;

public class MatrixException : Exception
{
    public MatrixException()
    {
    }

    public MatrixException(string message)
        : base(message)
    {
    }

    public MatrixException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class Matrix
{
    private readonly double[,] matrixArray;

    public Matrix(int rows, int columns)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(rows);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(columns);

        this.matrixArray = new double[rows, columns];
    }

    public Matrix(Array array)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array is not double[,] typedArray)
        {
            throw new ArgumentException("Array must be a two-dimensional array of doubles.", nameof(array));
        }

        this.matrixArray = (double[,])typedArray.Clone();
    }

    public int Rows
    {
        get => this.matrixArray.GetLength(0);
    }

    public int Columns
    {
        get => this.matrixArray.GetLength(1);
    }

    public double[,] Array
    {
        get => this.matrixArray;
    }

    public double this[int row, int column]
    {
        get
        {
            this.ValidateIndex(row, column);
            return this.matrixArray[row, column];
        }

        set
        {
            this.ValidateIndex(row, column);
            this.matrixArray[row, column] = value;
        }
    }

    public double[,] ToArray() => (double[,])this.matrixArray.Clone();

    public Matrix Add(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);

        if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
        {
            throw new MatrixException("Matrix dimensions must be equal for addition.");
        }

        var result = new double[this.Rows, this.Columns];
        for (var i = 0; i < this.Rows; i++)
        {
            for (var j = 0; j < this.Columns; j++)
            {
                result[i, j] = this.matrixArray[i, j] + matrix.matrixArray[i, j];
            }
        }

        return new Matrix(result);
    }

    public Matrix Subtract(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);

        if (this.Rows != matrix.Rows || this.Columns != matrix.Columns)
        {
            throw new MatrixException("Matrix dimensions must be equal for subtraction.");
        }

        var result = new double[this.Rows, this.Columns];
        for (var i = 0; i < this.Rows; i++)
        {
            for (var j = 0; j < this.Columns; j++)
            {
                result[i, j] = this.matrixArray[i, j] - matrix.matrixArray[i, j];
            }
        }

        return new Matrix(result);
    }

    public Matrix Multiply(Matrix matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);

        if (this.Columns != matrix.Rows)
        {
            throw new MatrixException("Number of columns in left matrix must equal number of rows in right matrix.");
        }

        var result = new double[this.Rows, matrix.Columns];
        for (var i = 0; i < this.Rows; i++)
        {
            for (var j = 0; j < matrix.Columns; j++)
            {
                var sum = 0d;
                for (var k = 0; k < this.Columns; k++)
                {
                    sum += this.matrixArray[i, k] * matrix.matrixArray[k, j];
                }

                result[i, j] = sum;
            }
        }

        return new Matrix(result);
    }

    private void ValidateIndex(int row, int column)
    {
        if (row < 0 || row >= this.Rows || column < 0 || column >= this.Columns)
        {
            throw new ArgumentException("Index is out of range.");
        }
    }
}