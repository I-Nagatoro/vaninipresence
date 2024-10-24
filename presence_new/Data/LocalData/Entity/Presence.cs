public class PresenceLocalEntity
{
    public Guid UserGuid { get; set; } // Замените int на Guid
    public int GroupId { get; set; }
    public int LessonNumber { get; set; }
    public DateTime Date { get; set; }
    public bool IsAttedance { get; set; }
}
