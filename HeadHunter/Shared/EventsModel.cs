namespace HeadHunter.Shared;

public class EventsModel
{
    public int Status { get; set; }
    public List<EventData> Data { get; set; }
}

public class EventData
{
    public string Uuid { get; set; }
    public string DisplayName { get; set; }
    public string ShortDisplayName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string AssetPath { get; set; }
}
