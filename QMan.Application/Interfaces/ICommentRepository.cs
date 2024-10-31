using QMan.Application.Dtos.Base;
using QMan.Domain.Entities.Base;

namespace QMan.Application.Interfaces;

public interface ICommentRepository
{
    public Task<BaseResponse> NewComment();
    public Task<BaseResponse> GetAllComment(PaginationBaseDto dto);
    public Task<BaseResponse> GetCommentText(int commentId);
    public Task<BaseResponse> ChangeCommentStatus(int commentId);
}