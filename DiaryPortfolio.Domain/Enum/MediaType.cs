namespace DiaryPortfolio.Domain.Enum;

public enum MediaType //related with how files path created
{
    Post,
    Short,
    Education,
    Project,

    //modules
    PortfolioProfile,
    DiaryProfile,
}

public enum MediaStatus
{
    Public,
    Private,
}

public enum MediaSubType //related with how files path created
{
    Video,
    Image,
    Audio,
    Text,
    File,
}