namespace Domain.Entities;

public class Likes
{
    public int LikedId { get; set; }
    public int LikerUserId { get; set; }
    public int PosterUserId { get; set; }
    public DateTime LikeDate { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }

}
