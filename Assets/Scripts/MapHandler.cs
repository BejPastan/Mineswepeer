using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField]
    int width, height;
    [SerializeField]
    GameObject tilePref;
    [SerializeField]
    int mineChance;
    [SerializeField]
    Tile[,] map = new Tile[0,0];

    public void CreateMap()
    {
        MapGen.GenerateMap(width, height, tilePref, mineChance, ref map);

    }
}
