using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QMan.Api.Commons;
using QMan.Application.Dtos.Admin;
using QMan.Application.Dtos.Base;
using QMan.Application.Dtos.Business;
using QMan.Application.Dtos.Login;
using QMan.Application.Dtos.Ticket;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Admin;
using QMan.Infrastructure.Helpers;

namespace QMan.Api.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(
    IAdminRepository adminRepository,
    ITicketRepository ticketRepository,
    ICommentRepository commentRepository) : BaseController
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
    public async Task<IActionResult> NewTicketMessage([FromForm] NewTicketMessageDto dto)
    {
        dto.UserRole = UserRole.Admin;
        dto.UserId = UserJwtModel?.UserId;
        return new BaseResult(await ticketRepository.NewTicketMessage(dto));
    }

    #endregion

    #region Admins

    [HttpPost, AccessActionFilter(AccessEnum.AdminsPage)]
    public async Task<IActionResult> GetAllAdmins([FromBody] PaginationBaseDto dto) =>
        new BaseResult(await adminRepository.GetAllAdmins(dto));

    [HttpGet, AccessActionFilter(AccessEnum.AdminsPage)]
    public IActionResult GetAllAccess() =>
        new BaseResult(adminRepository.GetAllAccesses());
    
    [HttpGet]
    public IActionResult GetAllAdminAccess([FromQuery] int adminId) =>
         Ok(adminRepository.GetAdminAccesses(adminId));

    [HttpPost, AccessActionFilter(AccessEnum.ChangeAdminStatus)]
    public async Task<IActionResult> ChangeAdminStatus([FromBody] ChangeAdminStatusDto dto) =>
        new BaseResult(await adminRepository.ChangeAdminStatus(dto.AdminId));

    [HttpPost, Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAdmin([FromForm] AddAdminDto dto) =>
        new BaseResult(await adminRepository.AddAdmin(dto));

    [HttpPut, Consumes("multipart/form-data")]
    public async Task<IActionResult> UpdateAdmin([FromForm] UpdateAdminDto dto) =>
        new BaseResult(await adminRepository.UpdateAdmin(dto));

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

    #region Login

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> SendCode([FromBody] SendCodeDto dto) =>
        new BaseResult(await adminRepository.SendCode(dto));

    [HttpPost, AllowAnonymous]
    public IActionResult CheckCode([FromBody] CheckCodeDto dto)
    {
        var result = adminRepository.CheckCode(dto);
        if (result.StatusCode == 200)
        {
            if (int.TryParse(result.Data?.ToString(), out var userId))
                result.Token = JwtHelper.GenerateToken(new UserJwtModel() { UserId = userId, Role = UserRole.Admin });
        }

        return new BaseResult(result);
    }

    #endregion

    #region ContactUs

    [HttpGet]
    public IActionResult GetAllContactUs() =>
        new BaseResult(adminRepository.GetAllAccesses());

    #endregion
}