namespace shopping_list
{
	public class AddItemDto
	{

        public AddItemDto(string name, bool inStorage)
        {
            Name = name;
            InStorage = inStorage;
        }

        public string Name { get; set; }

        public bool InStorage { get; set; }
    }
}

