using System;

[Serializable]
public class ResourceBag
{
    public float Heat;
    public int Hydrogen, Methane, Silicon;

    public static ResourceBag operator + (ResourceBag a) => a;
    public static ResourceBag operator - (ResourceBag a) => new ResourceBag(-a.Heat, -a.Hydrogen, -a.Methane, -a.Silicon);

    public static ResourceBag operator + (ResourceBag a, ResourceBag b) => new ResourceBag(a.Heat + b.Heat, a.Hydrogen + b.Hydrogen, a.Methane + b.Methane, a.Silicon + b.Silicon);
    public static ResourceBag operator - (ResourceBag a, ResourceBag b) => a + -b;

    public int this [ResourceType type]
    {
        get
        {
            switch (type)
            {
                case ResourceType.Heat:
                    return (int) Heat;

                case ResourceType.Hydrogen:
                    return Hydrogen;

                case ResourceType.Methane:
                    return Methane;

                case ResourceType.Silicon:
                    return Silicon;

                default:
                    throw new ArgumentException("unexpected ResourceType " + type);
            }
        }
    }

    public ResourceBag (float heat, int hydrogen, int methane, int silicon)
    {
        Heat = heat;
        Hydrogen = hydrogen;
        Methane = methane;
        Silicon = silicon;
    }

    public void AddOneOfType (ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Heat:
                Heat++;
                return;

            case ResourceType.Hydrogen:
                Hydrogen++;
                return;

            case ResourceType.Methane:
                Methane++;
                return;

            case ResourceType.Silicon:
                Silicon++;
                return;

            default:
                throw new ArgumentException("unexpected ResourceType " + type);
        }
    }
}

public enum ResourceType
{
    Heat, Hydrogen, Methane, Silicon
}
