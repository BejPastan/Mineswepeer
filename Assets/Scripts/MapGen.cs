using System;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    int width, height;
    [SerializeField]
    float mineChance;
    [SerializeField]
    GameObject tilePref;
    Tile[,] map = new Tile[0,0];

    public void GenerateMap(int width, int height)
    {
        this.width = width;
        this.height = height;
        
        for(int i = 0; i < height; i++)
        {
            RowGen();
        }
    }

    private void RowGen()
    {
        int mapHeight = map.GetLength(1);
        
        Resize2DArray(ref map, width, mapHeight+1);

        for(int xPos = 0; xPos < width; ++xPos)
        {
            TileState state;
            //generating Tile
            Debug.Log("test");
            GameObject newTile = Instantiate(tilePref);
            Debug.Log(newTile);
            if(UnityEngine.Random.Range(0, 1)>mineChance)
            {
                state = new TileState(false, false);
            }
            else
            {
                state = new TileState(true, false);
            }
            Vector2 location = new Vector2(xPos, mapHeight);
            Tile tileData = newTile.AddComponent<Tile>();
            tileData.TileConstructor(state, location);

            Debug.Log("mapHeight:" + mapHeight + ", map height:" + map.GetLength(1) + ", xPos:" + xPos + "map width:" + map.GetLength(0));

            map[xPos, mapHeight] = tileData;
            //sprawdza czy Tile xPos-1 istnieje
            if (xPos>0)
            {
                //oblicza liczbę min wokół pola xPos-1
                if (!map[xPos, mapHeight].IsMine)
                {
                    map[xPos,mapHeight].SetAdjecentMines(AdjacentMins(xPos, mapHeight));
                }
                if(mapHeight>0) 
                {
                    
                }
            }            
        }
    }

    private int AdjacentMins(int xPos, int yPos)
    {
        int mines = 0;

        for(int x = xPos-1; x<xPos+1; x++)
        {
            for (int y = yPos - 1; y < yPos + 1; y++)
            {
                if(x==xPos && y==yPos)
                {
                    continue;
                }
                try
                {
                    if(map[x, y].IsMine)
                    {
                        mines++;
                    }
                }
                catch{ }  
            }
        }

        return mines;
    }
    
    public void MoveMap(int x)
    {

    }


    static void Resize2DArray<T>(ref T[,] originalArray, int newRowCount, int newColumnCount)
    {
        int originalRowCount = originalArray.GetLength(0);
        int originalColumnCount = originalArray.GetLength(1);

        T[,] newArray = new T[newRowCount, newColumnCount];

        int minRowCount = Math.Min(originalRowCount, newRowCount);
        int minColumnCount = Math.Min(originalColumnCount, newColumnCount);

        for (int i = 0; i < minRowCount; i++)
        {
            for (int j = 0; j < minColumnCount; j++)
            {
                newArray[i, j] = originalArray[i, j];
            }
        }
        originalArray = newArray;
    }

}
