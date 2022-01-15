using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Models;

namespace TechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await _mediator.Send(new GetBookQuery());
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authors = await _mediator.Send(new GetBookQuery(id));
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
        {
            if (command == null)
            {
                return BadRequest("Body can not be null");
            }

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBookCommand command)
        {
            //This should really be done at a middleware
            if (command == null)
            {
                return BadRequest();
            }

            var updatedBook = await _mediator.Send(command);

            return AcceptedAtAction(nameof(GetById), new {id = command.Id}, updatedBook);
        }

        [HttpPost("SellBookCopy")]
        public async Task<IActionResult> SellBookCopy([FromBody] int bookId)
        {
            var result = await _mediator.Send(new SellBookCommand(bookId));
            return AcceptedAtAction(nameof(GetById), new { id = bookId }, result );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _mediator.Send(new DeleteBookCommand(id));
            return NoContent();
        }
    }
}
