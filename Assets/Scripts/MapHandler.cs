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

    private void OnMouseDown()
    {
        //wybiera Tile'a którego nacisnął

        //sprawdza czy trafił na minę
            //Ewentualnie jeśli trafił na pole wokół którego nie ma min(RevealMap)
    }

    private void RevealMap()
    {
        //odkrywanie oustych pól na mapie wokół odkrytego pola
    }
}
