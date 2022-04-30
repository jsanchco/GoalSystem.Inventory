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
        private readonly IItemInventoryService _feedsService;

        public ItemsInventoryController(
            ILogger<ItemsInventoryController> logger,
            IMapper mapper,
            IItemInventoryService feedsService)
        {
            _logger = logger;
            _mapper = mapper;
            _feedsService = feedsService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ItemInventoryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] ...");

            var feeds = await _feedsService.GetAll();
            var result = _mapper.Map<List<ItemInventoryDto>>(feeds);

            return Ok(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] GetByName ...");

            var feed = await _feedsService.GetByName(name);
            if (feed == null)
                return NotFound();

            var result = _mapper.Map<ItemInventoryDto>(feed);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        public async Task<IActionResult> Add(ItemInventoryDto itemInventoryDto)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpPos] Add ...");

            var newItemInventory = _mapper.Map<ItemInventory>(itemInventoryDto);
            var itemInventory = await _feedsService.Add(newItemInventory);
            var result = _mapper.Map<ItemInventoryDto>(itemInventory);

            return Ok(result);
        }
    }
}
