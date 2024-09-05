using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Ticket;
using QMan.Domain.Entities.Ticket;
using QMan.Infrastructure.Interfaces;

namespace QMan.Api.Controllers;

[ApiController(), Route("[controller]/[action]")]
public class TicketController(ITicketRepository ticketRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationBaseDto dto) =>
        new BaseResult(await ticketRepository.GetAllTicket(dto));
    
    [HttpGet]
    public async Task<IActionResult> GetTicketMessages([FromQuery] int ticketId) =>
        new BaseResult(await ticketRepository.GetTicketMessages(ticketId));


    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> NewTicket([FromForm] CreateTicketDto dto) =>
        new BaseResult(await ticketRepository.CreateTicket(dto));


    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> NewTicketMessage([FromForm] NewTicketMessageDto dto)
    {
        dto.Status = TicketStatus.UserWaiting;
        return new BaseResult(await ticketRepository.NewTicketMessage(dto));
    }
}