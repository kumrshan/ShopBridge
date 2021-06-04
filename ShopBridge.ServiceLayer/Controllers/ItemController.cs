using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridge.BusinessaLogic.Interface;
using ShopBridge.BusinessObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /// <summary>
    /// Item Controller
    /// <summary>
    public class ItemController : ControllerBase
    {

        private readonly IItemManager _itemManager;

        /// <summary>
        /// Item Controller Constructor
        /// <paramref name="itemManager"/>
        /// <summary>
        public ItemController(IItemManager itemManager)
        {
            _itemManager = itemManager;
        }

        [HttpGet]
        [Route("item")]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {

            var items = await _itemManager.GetItems();
            if (items.Any())
            {
                return Ok(items);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Route("item/{itemID}")]
        public async Task<ActionResult<Item>> UpdateItem(int itemID, [FromBody] Item item)
        {

            var response = await _itemManager.updateItem(itemID, item);
            if (response.StatusCode != 400)
            {
                return Accepted(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpPost]
        [Route("item")]
        public async Task<ActionResult<Item>> AddItem([FromBody] Item item)
        {

            var response = await _itemManager.AddItem(item);
            if (response.StatusCode != 400)
            {
              
                return Created(response.Message, item);
            }
            else
            {
                return BadRequest(response.Message);
            }
            
        }

        [HttpDelete]
        [Route("item/{itemID}")]
        public async Task<ActionResult<Item>> DeleteItem(int itemID)
        {

            var response = await _itemManager.DeleteItem(itemID);
            if (response.StatusCode != 400)
            {
                return Accepted(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

    }
}
