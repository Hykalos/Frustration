namespace Frustration.Domain;

public class Player
{
    public Player(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
        CurrentScore = 0;
        CurrentTask = 1;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint CurrentScore { get; set; }
    public uint CurrentTask { get; set; }

    public void IncrementScore(uint points)
    {
        CurrentScore += points;
    }

    public void DecrementScore(uint points)
    {
        CurrentScore -= points;
    }

    public void CompleteTask()
    {
        ++CurrentTask;
    }

    public void CancelTaskCompletion()
    {
        --CurrentTask;
    }
}
