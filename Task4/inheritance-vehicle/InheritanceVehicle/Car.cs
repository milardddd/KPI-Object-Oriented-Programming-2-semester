namespace InheritanceVehicle;

public class Car : Vehicle
{
    public Car(string name, int maxSpeed)
        : base(name, maxSpeed)
    {
    }

    public void SetCarName(string newName)
    {
        this.Name = newName;
    }

    public string GetCarName()
    {
        return this.Name;
    }
}
