using System;
using Microsoft.AspNetCore.Mvc;

namespace shopping_list;

[ApiController]
[Route("[controller]")]
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
        return new List<Item> { new Item("some item", 0) };
    }

    [HttpPost]
    public void Add([FromBody] Item item)
    {
        _itemService.AddItem(item);
    }

    [HttpPut]
    public void Edit([FromBody] Item item)
    {

    }

    [HttpDelete]
    public void Remove([FromBody] Item item)
    {

    }
}


