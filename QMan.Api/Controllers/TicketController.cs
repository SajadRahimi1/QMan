using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos;
using QMan.Application.Dtos.Ticket;
using QMan.Infrastructure.Interfaces;

namespace QMan.Api.Controllers;

[ApiController(),Route("[controller]/[action]")]
public class TicketController:ControllerBase
{
    private readonly ITicketRepository ticketRepository;

    public TicketController(ITicketRepository ticketRepository)
    {
        this.ticketRepository = ticketRepository;
    }

    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] PaginationBaseDto dto)
    {
        return new BaseResult(await ticketRepository.GetAllTicket(dto));
    }
    
    [HttpPost,Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateNewTicket([FromForm] CreateTicketDto dto)
    {
        return new BaseResult(await ticketRepository.CreateTicket(dto));
    }
}