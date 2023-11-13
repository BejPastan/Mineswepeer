using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField]
    MapHandler map;
    GameState gameState = GameState.start;
    //manage game states
        //play, pause, end, start

    private void Start()
    {
        map.CreateMap();
    }


}

enum GameState
{
    play,
    pause,
    end,
    start
};