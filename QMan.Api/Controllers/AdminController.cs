using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Base;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Ticket;
using QMan.Application.Interfaces;
using QMan.Infrastructure.Helpers;

namespace QMan.Api.Controllers;

[ApiController(), Route("[controller]/[action]"), Authorize(Roles = "Admin")]
public class AdminController(IAdminRepository adminRepository, ITicketRepository ticketRepository,
    ICommentRepository commentRepository) : ControllerBase
{
    #region Ticket

    [HttpPost]
    public async Task<IActionResult> GetAllTickets([FromBody] PaginationBaseDto dto) =>
        new BaseResult(await ticketRepository.GetAllTicket(dto));

    [HttpPost]
    public async Task<IActionResult> ChangeTicketStatus([FromBody] ChangeTicketStatusDto dto) =>
        new BaseResult(await ticketRepository.ChangeTicketStatus(dto));


    [HttpGet]
    public async Task<IActionResult> GetTicketMessages([FromQuery] int ticketId) =>
        new BaseResult(await ticketRepository.GetTicketMessages(ticketId));


    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> NewTicketMessage([FromBody] NewTicketMessageDto dto)
    {
        var userModel = JwtHelper.GetUser(await HttpContext.GetTokenAsync("access_token") ?? "");
        dto.UserRole = userModel.Role;
        dto.UserId = userModel.UserId;
        return new BaseResult(await ticketRepository.NewTicketMessage(dto));
    }

    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAdmin([FromForm] AddAdminDto dto) =>
        new BaseResult(await adminRepository.AddAdmin(dto));

    [HttpPut, Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateAdmin([FromForm] UpdateAdminDto dto) =>
        new BaseResult(await adminRepository.UpdateAdmin(dto));

    #endregion

    #region Admins

    [HttpPost]
    public async Task<IActionResult> GetAllAdmins([FromBody] PaginationBaseDto dto) =>
        new BaseResult(await adminRepository.GetAllAdmins(dto));

    #endregion

    #region Comments

    [HttpPost]
    public async Task<IActionResult> GetAllComments([FromBody] PaginationBaseDto dto) =>
        new BaseResult(await commentRepository.GetAllComment(dto));

    [HttpPatch]
    public async Task<IActionResult> ChangeCommentStatus([FromQuery] int commentId) =>
        new BaseResult(await commentRepository.ChangeCommentStatus(commentId));

    [HttpGet]
    public async Task<IActionResult> GetCommentText([FromQuery] int commentId) =>
        new BaseResult(await commentRepository.GetCommentText(commentId));

    #endregion

    #region Business

    [HttpPost]
    public async Task<IActionResult> GetAllBusiness([FromBody] PaginationBaseDto dto) =>
        new BaseResult(await adminRepository.GetAllBusiness(dto));

    [HttpGet]
    public async Task<IActionResult> GetBusinessInformation([FromQuery] int businessId) =>
        new BaseResult(await adminRepository.GetBusinessInfo(businessId));

    [HttpPut]
    public async Task<IActionResult> UpdateBusiness([FromBody] UpdateBusinessDto dto) =>
        new BaseResult(await adminRepository.UpdateBusiness(dto));

    #endregion

    #region Test Token

    [HttpGet, AllowAnonymous]
    public IActionResult GetToken() =>
        Ok(JwtHelper.GenerateToken(new UserJwtModel() { UserId = 1, Role = UserRole.Admin }));

    [HttpGet, AllowAnonymous]
    public IActionResult TranslateToken([FromQuery] string token) =>
        Ok(JwtHelper.GetUser(token));

    #endregion
}