using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Base;
using QMan.Domain.Entities.Base;
using QMan.Infrastructure.Contexts;
using QMan.Infrastructure.Interfaces;

namespace QMan.Infrastructure.Repositories;

public class CommentRepository(AppDbContext dbContext) : ICommentRepository
{
    public Task<BaseResponse> NewComment()
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> GetAllComment(PaginationBaseDto dto)
    {
        var skip = (dto.PageNumber - 1) * dto.PageSize;
        var comments = await dbContext.Comments.AsNoTracking().Skip(skip).Take(dto.PageSize).ToListAsync();

        return new BaseResponse() { Data = comments };
    }

    public async Task<BaseResponse> GetCommentText(int commentId)
    {
        var comment = await dbContext.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
        return comment is null ? new BaseResponse() { StatusCode = 404 } : new BaseResponse() { Data = comment };
    }

    public async Task<BaseResponse> ChangeCommentStatus(int commentId)
    {
        var comment = await dbContext.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
        if (comment is null) return new BaseResponse() { StatusCode = 404 };

        await dbContext.Comments.AsNoTracking().Where(c => c.Id == commentId)
            .ExecuteUpdateAsync(p => p.SetProperty(c => c.ShowInHome, !comment.ShowInHome));

        return new();
    }
}