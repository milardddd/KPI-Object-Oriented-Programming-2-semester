namespace ClassesDemo;

public class Rectangle
{
    private double sideA;
    private double sideB;

    public Rectangle(double a, double b)
    {
        this.sideA = a;
        this.sideB = b;
    }

    public Rectangle(double a)
        : this(a, 5)
    {
    }

    public Rectangle()
        : this(4, 3)
    {
    }

    public double GetSideA() => this.sideA;

    public double GetSideB() => this.sideB;

    public double Area() => this.sideA * this.sideB;

    public double Perimeter() => 2 * (this.sideA + this.sideB);

    public bool IsSquare() => this.sideA == this.sideB;

    public void ReplaceSides()
    {
        double temp = this.sideA;
        this.sideA = this.sideB;
        this.sideB = temp;
    }
}
