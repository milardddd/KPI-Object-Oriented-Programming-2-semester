namespace ClassesDemo;

public class ArrayRectangles
{
    private readonly Rectangle?[] rectangleArray;

    public ArrayRectangles(int n)
    {
        this.rectangleArray = new Rectangle?[n];
    }

    public ArrayRectangles(Rectangle[] rectangles)
    {
        this.rectangleArray = rectangles;
    }

    public bool AddRectangle(Rectangle rectangle)
    {
        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            if (this.rectangleArray[i] == null)
            {
                this.rectangleArray[i] = rectangle;
                return true;
            }
        }

        return false;
    }

    public int NumberMaxArea()
    {
        int maxIndex = 0;
        double maxArea = -1;

        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            var rect = this.rectangleArray[i];
            if (rect != null && rect.Area() > maxArea)
            {
                maxArea = rect.Area();
                maxIndex = i;
            }
        }

        return maxIndex;
    }

    public int NumberMinPerimeter()
    {
        int minIndex = 0;
        double minPerimeter = double.MaxValue;

        for (int i = 0; i < this.rectangleArray.Length; i++)
        {
            var rect = this.rectangleArray[i];
            if (rect != null && rect.Perimeter() < minPerimeter)
            {
                minIndex = i;
                minPerimeter = rect.Perimeter();
            }
        }

        return minIndex;
    }

    public int NumberSquare()
    {
        int count = 0;
        foreach (var rect in this.rectangleArray)
        {
            if (rect != null && rect.IsSquare())
            {
                count++;
            }
        }

        return count;
    }
}
