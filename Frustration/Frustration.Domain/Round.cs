namespace Frustration.Domain;

public class Round
{
    public Round(IEnumerable<Guid> completedRound)
    {
        if (!completedRound.Any())
            throw new ArgumentException("At least one player has to complete the round");

        CompletedRound = completedRound;
    }

    public Round(IEnumerable<Guid> completedRound, IDictionary<Guid, uint> points) : this(completedRound)
    {
        Points = points;
    }

    public IEnumerable<Guid> CompletedRound { get; set; }
    public IDictionary<Guid, uint> Points { get; set; } = new Dictionary<Guid, uint>();
}
