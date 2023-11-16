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
    Vector2 shift;

    public void CreateMap()
    {
        shift = transform.position;
        MapGen.GenerateMap(width, height, tilePref, mineChance, shift, ref map);
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        GetComponent<BoxCollider2D>().offset = new Vector2(width, height)/2 - Vector2.one/2;
    }

    //muszę tutaj dodać rozróżnianie prawy lewy przycisk, aktualnie nie reaguje na prawy
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //wybiera Tile'a którego nacisnął
            Tile selectedTile = GetTileOnPlace(mousePosition);
            if (selectedTile != null)
            {
                //false is mine
                if (selectedTile.Revel())
                {

                }
            }
            //sprawdza czy trafił na minę
                //Ewentualnie jeśli trafił na pole wokół którego nie ma min(RevealMap)
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //wybiera Tile'a którego nacisnął
            Tile selectedTile = GetTileOnPlace(mousePosition);
            if(selectedTile != null)
            {
                selectedTile.Flag();
            }
        }

    }

    private void RevealMap()
    {
        //odkrywanie oustych pól na mapie wokół odkrytego pola
    }

    private Tile GetTileOnPlace(Vector2 positionOnMap)
    {
        positionOnMap -= shift;
        int posX, posY;
        posX = Mathf.RoundToInt(positionOnMap.x);
        posY = Mathf.RoundToInt(positionOnMap.y);
        return map[posX, posY];
    }
}
