public class Field
{
    public static readonly Field Grassland = new Field("Grassland", 0, 0);
    public static readonly Field Spikes = new Field("Spikes", 1, 5);
    public static readonly Field Wall = new Field("Wall", 2, 5);

    private readonly string name;
    private readonly int id;
    private readonly int damage;
    
    Field(string name, int id, int damage)
    {
        this.name = name;
        this.id = id;
        this.damage = damage;
    }

    public string Name { get { return name; } }
    public int Damage { get { return damage; } }

    public override string ToString()
    {
        return name;
    }
}