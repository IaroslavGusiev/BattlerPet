namespace Code.StaticData
{
    public enum BattleState : byte
    {
        NotStarted = 0,
        InProgress = 1,
        Paused = 2,
        Finished = 3
    }

    public enum BattleMode : byte
    {
        Auto = 0,
        Manual = 1
    }
}