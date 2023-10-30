using static System.Security.Cryptography.RandomNumberGenerator;

namespace WebApi.Services
{
  public record Hug(string Name);

  public record Hugged(string Name, string Kind)
  {
    public DateTime Timestamp { get; set; } = DateTime.UnixEpoch;
    public string  BestTeam { get; set; } = "Rams";
  }

  public class HuggingService
  {
    private readonly string[] _hugKinds = {"Side Hug", "Bear Hug", "Polite Hug", "Back Hug", "Self Hug"};

    private string RandomKind => _hugKinds[GetInt32(0, _hugKinds.Length)];

    public Hugged Hug(Hug hug) => new(hug.Name, RandomKind);
  }
}