namespace QMan.Application.Dtos.Base;

public class PaginationBaseDto
{
    public int PageNumber
    {
        get => PageNumberSetter < 1 ? 1 : PageNumberSetter;
        set => PageNumberSetter = value;
    }

    private int PageNumberSetter { set; get; } = 1;
    
    
    public int PageSize
    {
        get => PageSizeSetter < 1 ? 1 : PageSizeSetter;
        set => PageSizeSetter = value;
    }

    private int PageSizeSetter { set; get; } = 15;
}