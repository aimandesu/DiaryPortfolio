namespace DiaryPortfolio.Domain.Entities;

public class ConditionModel
{
    public Guid Id { get; set; }
    public DateTime AvailableTime { get; set; }
    public DateTime? DeletedTime { get; set; }
    
    //EF Relationship
    public Guid MediaId { get; set; }
    public MediaModel? MediaModel { get; set; }
}