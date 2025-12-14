using DiaryPortfolio.Domain.Enum;

namespace DiaryPortfolio.Domain.Entities;

public class TextModel
{
    public Guid Id { get; set; }
    public TextStyle TextStyle { get; set; } = TextStyle.TimesNewRoman;
    public int FontSize { get; set; } = 11;
}