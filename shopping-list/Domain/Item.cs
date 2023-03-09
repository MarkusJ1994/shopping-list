namespace shopping_list;

public class Item
{

    public Item(Guid id, string name, bool inStorage)
    {
        Id = id;
        Name = name;
        InStorage = inStorage;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool InStorage { get; set; }

    public override string ToString() => $" {{ {Name} : {InStorage} }}";

}

