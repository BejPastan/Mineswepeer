using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField]
    MapGen map;
    //manage game states
        //play, pause, end, start

    private void Start()
    {
        map.GenerateMap(10, 10);
    }
}
