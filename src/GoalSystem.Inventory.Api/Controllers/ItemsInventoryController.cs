using AutoMapper;
using GoalSystem.Inventory.Api.DTOs;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Api.Controllers
{
    [Route("api/v1/itemsinventory")]
    [Produces("application/json")]
    [ApiController]
    public class ItemsInventoryController : ControllerBase
    {
        private readonly ILogger<ItemsInventoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IItemInventoryService _itemInventoryService;

        public ItemsInventoryController(
            ILogger<ItemsInventoryController> logger,
            IMapper mapper,
            IItemInventoryService itemInventoryService)
        {
            _logger = logger;
            _mapper = mapper;
            _itemInventoryService = itemInventoryService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ItemInventoryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] ...");

            var itemsInventory = await _itemInventoryService.GetAll();
            var result = _mapper.Map<List<ItemInventoryDto>>(itemsInventory);

            return Ok(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] GetByName ...");

            var itemInventory = await _itemInventoryService.GetByName(name);
            if (itemInventory == null)
                return NotFound();

            var result = _mapper.Map<ItemInventoryDto>(itemInventory);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        public async Task<IActionResult> Add([FromBody] ItemInventoryDto itemInventoryDto)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpPos] Add ...");

            var newItemInventory = _mapper.Map<ItemInventory>(itemInventoryDto);
            var itemInventory = await _itemInventoryService.Add(newItemInventory);
            var result = _mapper.Map<ItemInventoryDto>(itemInventory);

            return Ok(result);
        }
    }
}
