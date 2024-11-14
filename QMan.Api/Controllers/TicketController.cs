using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Ticket;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Ticket;

namespace QMan.Api.Controllers;

public class TicketController(ITicketRepository ticketRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationBaseDto dto) =>
        new BaseResult(await ticketRepository.GetAllTicket(dto));

    [HttpGet]
    public async Task<IActionResult> GetTicketMessages([FromQuery] int ticketId) =>
        new BaseResult(await ticketRepository.GetTicketMessages(ticketId));
    
   


    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> NewTicket([FromForm] CreateTicketDto dto)
    {
        dto.UserId = UserJwtModel?.UserId;
        return new BaseResult(await ticketRepository.CreateTicket(dto));
    }


    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> NewTicketMessage([FromForm] NewTicketMessageDto dto)
    {
        dto.UserId = UserJwtModel?.UserId;
        return new BaseResult(await ticketRepository.NewTicketMessage(dto));
    }
}