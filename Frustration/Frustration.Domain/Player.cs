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

    public Guid Id { get; }
    public string Name { get; }
    public uint CurrentScore { get; private set; }
    public uint CurrentTask { get; private set; }

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
