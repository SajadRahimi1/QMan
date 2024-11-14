using Microsoft.EntityFrameworkCore;
using QMan.Application.Dtos.Base;
using QMan.Application.Interfaces;
using QMan.Domain.Entities.Base;
using QMan.Infrastructure.Contexts;

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
        var comments = await dbContext.Comments.AsSplitQuery().Include(c => c.Business).AsSplitQuery().AsNoTracking().Skip(skip)
            .Take(dto.PageSize).Select(c => new
            {
                CommentId = c.Id,
                c.Business.Title,
                BusinessId = c.Business.Id,
                c.UpdateDateTime,
                c.ShowInHome
            }).ToListAsync();

        return new BaseResponse() { Data = comments };
    }

    public async Task<BaseResponse> GetCommentText(int commentId)
    {
        var comment = await dbContext.Comments.Include(c => c.Business).AsSplitQuery()
            .Select(c=>new
            {
                c.Id,
                c.Business.Title,
                BusinessId=c.Business.Id,
                c.Text,
                c.CreatedDateTime,
            }) .SingleOrDefaultAsync(c => c.Id == commentId);
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