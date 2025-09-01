
public enum SceneType{
    Intro,
    Title,
    BaseCamp,
    Stage
}


public static class SceneNames
{
    public const string Intro = "IntroScene";
    public const string Title = "TitleScene";
    public const string BaseCamp = "BaseCampScene";
    public const string Stage = "StageScene";
    public const string Loading = "LoadingScene";
}

public static class PlayerState
{
    public static class Movement
    {
        public const string Walk = "Walk";
        public const string Idle = "Idle";
    }

    public static class Action
    {
        public const string Jump = "Jump";
        public const string Idle = "Idle";
        public const string Attack = "Attack";
    }
}
public class Constants
{
}
