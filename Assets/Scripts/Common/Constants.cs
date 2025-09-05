
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
        public const string Walk = "Walk";
        public const string Idle = "Idle";
        public const string Jump = "Jump";
        public const string Attack = "Attack";
        public const string Hit = "Hit";
        public const string Dead = "Dead";
}

public static class EnemyState
{
    public const string Idle = "Idle";
    public const string Wander = "Wander";
    public const string Flee = "Flee";
    public const string Dead = "Dead";
    public const string Hit = "Hit";
}

public static class Timer
{
    //Player
    public const float INVINCIBLE_TIME = 1f;
    public const float STUN_DURATION = 0.2f;
    public const float RESURRECTION_TIME = 3f;
}
public static class AnimatorString
{
    public static class PlayerAnimation
    {
        public const string Walk = "walk";
        public const string Idle = "idle";
        public const string Attack = "attack";
        public const string Jump = "jump";
        public const string Hit = "hit";
        public const string Dead = "dead";
    }

    public static class PlayerParameters
    {
        public const string IsWalking = "isWalking";
        public const string IsGrounded = "isGrounded";
        public const string Jump = "Jump";
        public const string Hit = "Hit";
        public const string Dead = "Dead";
    }

    public static class EnemyAnimation
    {
        public  const string Walk = "walk";
        public const string Idle = "idle";
        public const string Run = "run";
        public const string Hit = "hit";
        public const string Dead = "dead";
    }
    public static class EnemyParameters
    {
        public const string IsWalking = "isWalking";
        public const string IsRunning = "isRunning";
        public const string Hit = "hit";
        public const string Dead = "dead";
    }
}
public class Constants
{
    public const string UICommonPath = "Prefabs/UI/Common/";
    public const string UIElementsPath = "Prefabs/UI/Elements/";
    public const string Canvas = "Canvas";
    public const string EventSystem = "EventSystem";
    public const string DataHolder = "Prefabs/DataHolder/";
    public const string Player = "Prefabs/Entities/Player";
    public const string Sounds = "Sounds/";
}
