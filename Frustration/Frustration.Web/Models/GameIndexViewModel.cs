namespace Frustration.Web.Models;

public class GameIndexViewModel
{
    public Game Game { get; set; } = Game.Empty;
    public bool GameCompleted { get; set; }
    public Player? Winner { get; set; }
}
