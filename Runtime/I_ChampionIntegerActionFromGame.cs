using System;

namespace Eloi.UWCWarcraft {
    public interface I_ChampionIntegerActionFromGame
{ 

    void AddIntegerToActionFromGame(Action<int> gameIntegerAction);
    void RemoveIntegerToActionFromGame(Action<int> gameIntegerAction);
}

}