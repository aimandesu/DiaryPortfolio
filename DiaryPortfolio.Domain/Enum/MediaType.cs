namespace DiaryPortfolio.Domain.Enum;

public enum MediaType //related with how files path created
{
    Post,
    Short,
    Education,
    Project,
    Chat,

    //modules
    PortfolioProfile,
    DiaryProfile,
}

public enum MediaStatus //only for media in DiaryProfile
{
    Public,
    Private,
}

public enum MediaSubType //related with how files path created
{
    Video,
    Image,
    Audio,
    File,
}