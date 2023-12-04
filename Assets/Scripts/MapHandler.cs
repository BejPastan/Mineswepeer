using System;
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
    public Tile[,] map = new Tile[0,0];
    [SerializeField]
    Sprite trenchSprite;
    Vector2 shift;
    [SerializeField]
    SquadControler squad;



    public void CreateMap()
    {
        shift = transform.position;
        MapGen.GenerateMap(width, height, tilePref, mineChance, shift, trenchSprite, ref map);
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
        GetComponent<BoxCollider2D>().offset = new Vector2(width, height)/2 - Vector2.one/2;

        Camera.main.orthographicSize = ((float)height / 2);
        Camera.main.transform.position = new Vector3((float)width / 2, ((float)height / 2) -0.5f, -10);
    }

    
    private void OnMouseOver()
    {
        //muszę dodać sprawdzanie czy pole obok jest odkryte
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
                    //tutaj sprawdza czy odkryty Tile nie dotyka Trench'a
                    int posX, posY;
                    GetPlaceOfTile(selectedTile, out posX, out posY);
                    for (int x = posX - 1; x <= posX + 1; x++)
                    {
                        for (int y = posY - 1; y <= posY + 1; y++)
                        {
                            //Debug.Log("X= "+x +" Y= "+y);
                            if (map[x, y].IsTrench)
                            {
                                //true == moving units
                                Debug.Log("będzie sprawdzał czy nie dotarł jeszcze do tego trencha");
                                Debug.Log(map[x, y]);
                                if (squad.FindTrench(ref map[x, y], ref map))
                                {
                                    Debug.Log("znaleziono trench");
                                    Tile[] trenchToMove = GetTrenchesInRow(map[x, y]);
                                    squad.MoveUnits(trenchToMove, ref map);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //trafił na minę                    
                }
            }
            //sprawdza czy trafił na minę
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

    public Tile[] GetTrenchesInRow(Tile trenchInRow)
    {
        Tile[] trenches = new Tile[0];
        int Xpos, Ypos;
        GetPlaceOfTile(trenchInRow, out Xpos, out Ypos);

        int newX = Xpos;
        while(map[newX, Ypos].IsTrench)
        {
            Array.Resize(ref trenches, trenches.Length + 1);
            trenches[trenches.Length - 1] = map[newX, Ypos];
            newX--;
        }
        newX = Xpos+1;
        while (map[newX, Ypos].IsTrench)
        {
            Array.Resize(ref trenches, trenches.Length + 1);
            trenches[trenches.Length - 1] = map[newX, Ypos];
            newX++;
        }

        return trenches;
    }


    private Tile GetTileOnPlace(Vector2 positionOnMap)
    {
        positionOnMap -= shift;
        int posX, posY;
        posX = Mathf.RoundToInt(positionOnMap.x);
        posY = Mathf.RoundToInt(positionOnMap.y);
        return map[posX, posY];
    }

    public void GetPlaceOfTile(Tile tile, out int posX, out int posY)
    {
        posX = Mathf.RoundToInt(tile.transform.position.x + shift.y);
        posY = Mathf.RoundToInt(tile.transform.position.y + shift.y);
    }
}
