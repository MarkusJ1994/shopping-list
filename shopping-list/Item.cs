using System;
namespace shopping_list;

public class Item
{
    public Item(string name, int status)
    {
        Name = name;
        Status = status;
    }

    public string Name { get; set; }

    //TODO: Add enum
    public int Status { get; set; }

    public override string ToString() => $" {{ {Name} : {Status} }}";

}

