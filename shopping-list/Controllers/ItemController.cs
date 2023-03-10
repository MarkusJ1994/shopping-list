using Microsoft.AspNetCore.Mvc;

namespace shopping_list;

[ApiController]
[Route("/item")]
public class ItemController : ControllerBase
{
    readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public List<Item> GetAll()
    {
        return _itemService.GetItems().Result;
    }

    [HttpPost]
    public void Add([FromBody] AddItemDto item)
    {      
        _itemService.AddItem(item);
    }

    [HttpPut]
    public void Edit([FromQuery(Name = "id")] String guid, [FromBody] AddItemDto item)
    {
        _itemService.UpdateItem(new Guid(guid), item);
    }

    [HttpDelete]
    public void Remove([FromQuery(Name = "id")] String guid)
    {
        _itemService.RemoveItem(new Guid(guid));
    }
}


