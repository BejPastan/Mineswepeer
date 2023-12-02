using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField]
    MapHandler map;
    [SerializeField]
    SquadControler squad;
    GameState gameState = GameState.start;
    //manage game states
        //play, pause, end, start

    private void Start()
    {
        map.CreateMap();
        squad.StartGame(ref map.map);
    }
}

enum GameState
{
    play,
    pause,
    end,
    start
};