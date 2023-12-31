using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField]
    MapHandler map;
    [SerializeField]
    SquadControler squad;
    [SerializeField]
    Bombardment bombardment;
    GameState gameState = GameState.start;
    //manage game states
        //play, pause, end, start

    private void Start()
    {
        map.CreateMap();
        squad.StartGame(ref map.map);
        bombardment.StartBombard();
    }
}

enum GameState
{
    play,
    pause,
    end,
    start
};