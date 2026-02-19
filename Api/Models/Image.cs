namespace Api.Models;

public class Image
{
    public Guid Id { get; private set; }
    public Guid ImageGroupId { get; private set; }
    public ImageGroup ImageGroup { get; private set; } = default!;
    public string ImageUrl { get; private set; } = default!;
    public bool IsMain { get; private set; }

    private Image() { } 

    public Image(Guid imageGroupId, string imageUrl, bool isMain = false)
    {
        Id = Guid.NewGuid();
        ImageGroupId = imageGroupId;
        ImageUrl = imageUrl;
        IsMain = isMain;
    }
}
