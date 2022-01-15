using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MediatR;
using TechTest.Core.Entities;
using TechTest.Core.Interfaces;
using TechTest.Core.Models;

namespace TechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var authors = await _mediator.Send(new GetAuthorQuery());
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authors = await _mediator.Send(new GetAuthorQuery(id));
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAuthorCommand createAuthorCommand)
        {
            if (createAuthorCommand == null)
            {
                return BadRequest("Body can not be null");
            }

            var result = await _mediator.Send(createAuthorCommand);

            return CreatedAtAction(nameof(GetById), new { id = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateAuthorCommand command)
        {
            //This should really be done at a middleware
            if (command == null)
            {
                return BadRequest();
            }

            var updatedAuthor = await _mediator.Send(command);

            return AcceptedAtAction(nameof(GetById), new {id = command.Id}, updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _mediator.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
    }
}
