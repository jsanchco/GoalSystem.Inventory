using AutoMapper;
using GoalSystem.Inventory.Api.DTOs;
using GoalSystem.Inventory.Application.Services;
using GoalSystem.Inventory.Domain.Entities;
using GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Commands;
using GoalSystem.Inventory.Infrastructure.CQRS.ItemInventory.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GoalSystem.Inventory.Api.Controllers
{
    /// <summary>
    /// Controlles to hanlder the Items of Inventory
    /// </summary>
    [Route("api/v1/itemsinventory")]
    [Produces("application/json")]
    [ApiController]
    public class ItemsInventoryController : ControllerBase
    {
        /// <summary>
        /// Logger of App
        /// </summary>
        private readonly ILogger<ItemsInventoryController> _logger;

        /// <summary>
        /// Object to manage all mappigns with Automapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Use patron CQRS object
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Service to handler the Items of Inventory
        /// </summary>
        private readonly IItemInventoryService _itemInventoryService;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">logger of App (serilog)</param>
        /// <param name="mapper">Object to manage all mappigns with Automapper</param>
        /// <param name="itemInventoryService">Service to handler the Items of Inventory</param>
        public ItemsInventoryController(
            ILogger<ItemsInventoryController> logger,
            IMapper mapper,
            IMediator mediator,
            IItemInventoryService itemInventoryService)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
            _itemInventoryService = itemInventoryService;
        }

        /// <summary>
        /// Get all items of the Repository
        /// </summary>
        /// <returns>all items</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<ItemInventoryDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] ...");

            var itemsInventory = await _mediator.Send(new GetAllItemsInventoryQuery());
            var result = _mapper.Map<List<ItemInventoryDto>>(itemsInventory);

            return Ok(result);
        }

        /// <summary>
        /// Get item of Respository
        /// </summary>
        /// <param name="name">name criteria of search</param>
        /// <returns>item wit code of name</returns>
        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpGet] GetByName ...");

            var itemInventory = await _mediator.Send(new GetByNameItemInventoryQuery { Name = name });
            if (itemInventory == null)
                return NotFound();

            var result = _mapper.Map<ItemInventoryDto>(itemInventory);

            return Ok(result);
        }

        /// <summary>
        /// Add one item to Inventory
        /// </summary>
        /// <param name="itemInventoryDto"></param>
        /// <returns>Returns the item added</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        public async Task<IActionResult> Add([FromBody] ItemInventoryDto itemInventoryDto)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpPos] Add ...");

            var itemInventory = await _mediator.Send(new CreateItemInventoryCommand
            {
                Name = itemInventoryDto.Name,
                ExprirationDate = itemInventoryDto.ExprirationDate,
                Type = itemInventoryDto.Type
            });
            var result = _mapper.Map<ItemInventoryDto>(itemInventory);

            return Ok(result);
        }

        /// <summary>
        /// Remove one item of Repository
        /// </summary>
        /// <param name="code">code of item to remove</param>
        /// <returns>If operation is Ok return true, else false</returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ItemInventoryDto))]
        public async Task<IActionResult> Remove(string code)
        {
            _logger.LogInformation($"In ItemsInventoryController -> [HttpDelete] Remove ...");

            var result = await _mediator.Send(new RemoveItemInventoryCommand { Name = code });

            return Ok(result);
        }
    }
}
