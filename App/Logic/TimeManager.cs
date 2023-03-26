using System;

namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic;

public sealed class TimeManager {

    // 1 tick is 1 hour in game
    public int CurrentTick { get; private set; }

    public int GameHour => CurrentTick % 24;
    public int GameDay => CurrentTick / 24 + 1;
    public int GameYear => CurrentTick / 24 / 365 + 1;

    public void NextTick(Action logic) {
        CurrentTick += 1;
        logic?.Invoke( );
    }

}
