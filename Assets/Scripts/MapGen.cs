using System;
using UnityEngine;

public static class MapGen
{
    public static Tile[,] GenerateMap(int width, int height, GameObject tilePref, float mineChance, ref Tile[,] map)
    {
        for(int i = 0; i < height; i++)
        {
            RowGen(width, ref map, tilePref, mineChance);
        }

        return map;
    }

    static void RowGen(int width, ref Tile[,] map,GameObject tilePref,float mineChance)
    {
        int mapHeight = map.GetLength(1);
        
        Resize2DArray(ref map, width, mapHeight+1);

        for(int xPos = 0; xPos < width; ++xPos)
        {
            TileState state;
            //generating Tile
            if(UnityEngine.Random.Range(0, 1)>mineChance)
            {
                state = new TileState(false, false);
            }
            else
            {
                state = new TileState(true, false);
            }
            Vector2 location = new Vector2(xPos, mapHeight);
            GameObject newTile = GameObject.Instantiate(tilePref);
            newTile.AddComponent<Tile>().TileConstructor(state, location);
            Tile tileData = newTile.GetComponent<Tile>();

            map[xPos, mapHeight] = tileData;
            //sprawdza czy Tile xPos-1 istnieje
            if (xPos>0)
            {
                //oblicza liczbę min wokół pola xPos-1
                if (!map[xPos, mapHeight].IsMine)
                {
                    map[xPos,mapHeight].SetAdjecentMines(AdjacentMins(xPos, mapHeight, ref map));
                }
                if(mapHeight>0) 
                {
                    
                }
            }            
        }
    }

    static int AdjacentMins(int xPos, int yPos, ref Tile[,] map)
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
