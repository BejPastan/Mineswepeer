using System;
using UnityEngine;

public static class MapGen
{
    public static Tile[,] GenerateMap(int width, int height, GameObject tilePref, float mineChance, Vector2 shift, ref Tile[,] map)
    {
        for(int i = 0; i < height; i++)
        {
            RowGen(width, ref map, tilePref, mineChance, shift);
        }

        //generacja okopów
        GenerateTrench(ref map);

        return map;
    }

    static void RowGen(int width, ref Tile[,] map,GameObject tilePref,float mineChance, Vector2 shift)
    {
        int mapHeight = map.GetLength(1);
        
        Resize2DArray(ref map, width, mapHeight+1);

        for(int xPos = 0; xPos < width; ++xPos)
        {
            TileState state;
            //generating Tile
            if(UnityEngine.Random.Range(0, 100)>(mineChance))
            {
                state = new TileState(false);
            }
            else
            {
                state = new TileState(true);
            }
            Vector2 location = new Vector2(xPos, mapHeight);
            GameObject newTile = GameObject.Instantiate(tilePref);
            newTile.AddComponent<Tile>().TileConstructor(state, location+shift);
            Tile tileData = newTile.GetComponent<Tile>();

            map[xPos, mapHeight] = tileData;
                       
        }
        //ustawianie min dla każdego Tile'a
        for(int xPos = 0;xPos < width; ++xPos)
        {
            //sprawdza czy Tile xPos-1 istnieje
            if (xPos > 0)
            {
                //oblicza liczbę min wokół pola xPos-1
                if (!map[xPos, mapHeight].IsMine)
                {
                    map[xPos, mapHeight].SetAdjecentMines(AdjacentMins(xPos, mapHeight, ref map));
                }
                if (mapHeight > 0)
                {
                    for (int x = xPos - 1; x <= xPos + 1; x++)
                    {
                        try
                        {
                            map[x, mapHeight - 1].SetAdjecentMines(AdjacentMins(x, mapHeight - 1, ref map));
                        }
                        catch { }
                    }
                }
            }
        }
    }

    static int AdjacentMins(int xPos, int yPos, ref Tile[,] map)
    {
        int mines = 0;

        for(int x = xPos - 1; x<=xPos+1; x++)
        {
            for (int y = yPos - 1; y <=yPos + 1; y++)
            {
                if(x!=xPos || y!=yPos)
                {
                    try
                    {
                        if (map[x, y].IsMine)
                        {
                            mines++;
                        }
                    } catch { }
                }
            }
        }

        return mines;
    }

    static void GenerateTrench(ref Tile[,] map)
    {
        //generuje serie po 4-6 tile'i okopów obok siebie, ma 20% szansa na pójście w górę 
        for(int yPos = 5; yPos < map.GetLength(1);yPos++)
        {
            int inRow = 0;
            for (int xPos =0; xPos < map.GetLength(0); xPos++)
            {
                if (map[xPos, yPos].IsMine && map[xPos, yPos].AdjacementMines==0)
                {
                    inRow++;
                }
                else
                {
                    //tutaj sprawdza czy ma już wystarczająco długi łańcuch żeby zrobić okopy
                    if(inRow>=4)
                    {
                        inRow-=UnityEngine.Random.Range(0, inRow-3);
                        //generuje okop
                        for(int x = 0;  x < inRow; x++)
                        {
                            map[xPos-inRow+x, yPos].SetTrench();
                        }
                    }
                    inRow = 0;
                }
            }
        }
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
