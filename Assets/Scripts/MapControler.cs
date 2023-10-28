using System;
using Unity.Mathematics;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    private IMap map;
    private IMapMover mapMover;
    private IFieldInteractor fieldInteractor;

    public MapControler(IMap generator, IMapMover mover, IFieldInteractor interactor)
    {
        map = generator;
        mapMover = mover;
        fieldInteractor = interactor;
    }

    public void CreateMap(int rowCount)
    {
        for(int i = 0; i < rowCount; i++)
        {
            map.CreateNewRow();
        }
        
    }

    public void MoveMap()
    {
        mapMover.MoveMap();
    }

    public void InteractWithField()
    {
        fieldInteractor.InteractWithField();
    }
}

public interface IFieldInteractor
{
    void InteractWithField();
}

public class FieldInteractor : IFieldInteractor
{
    IMap map;
    // Implementation of IFieldInteractor method
    public void InteractWithField()
    {
        // Implement field interaction logic
    }
}

public interface IMapMover
{
    void MoveMap();
}

public class MapMover : IMapMover
{
    private IMap map;

    public MapMover(IMap generator)
    {
        map = generator;
    }

    // Implementation of IMapMover method
    public void MoveMap()
    {
        map.RemoveLastRow();
        map.CreateNewRow();
    }
}

public interface IMap
{
    void CreateNewRow();
    void RemoveLastRow();
}

public class Map : IMap
{
    public MapTile[,] map;
    public int rows; // Number of rows in the map.
    public int cols; // Number of columns in the map.
    public int mineCount; // Number of mines in the map.

    public float mineChance;

    public Map() { }

    public void CreateNewRow()
    {
        // Create a new row at the top of the map.
        MapTile[,] newMap = new MapTile[rows + 1, cols];

        // Initialize the new row with MapTile objects.
        for (int i = 0; i < cols; i++)
        {
            newMap[0, i] = CreateNewMapTile();
        }

        // Copy the existing map into the new map, shifting it down.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                newMap[i + 1, j] = map[i, j];
            }
        }

        // Update the map with the new row.
        map = newMap;
        rows++;

        // Update adjacent tiles with the count of mines around them.
        UpdateAdjacentTileCounts(0);
    }

    private MapTile CreateNewMapTile()
    {
        MapTile newTile = ScriptableObject.CreateInstance<MapTile>();

        if(UnityEngine.Random.Range(0, 1)<mineChance)
        {
            newTile.ContainsMine = true;
        }
        // Initialize the new tile as needed, e.g., set its state, check for mines, etc.
        return newTile;
    }

    private void UpdateAdjacentTileCounts(int row)
    {
        for (int i = 0; i < cols; i++)
        {
            MapTile currentTile = map[row, i];
            if (!currentTile.ContainsMine)
            {
                int nearbyMines = CountMinesAroundTile(row, i);
                currentTile.NearbyMinesCount = nearbyMines;
            }
        }
    }

    private int CountMinesAroundTile(int row, int col)
    {
        int mineCount = 0;

        // Check the eight adjacent tiles for mines.
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    if (map[newRow, newCol].ContainsMine)
                    {
                        mineCount++;
                    }
                }
            }
        }

        return mineCount;
    }

    public void RemoveLastRow()
    {
        if (rows <= 1)
        {
            // Cannot remove the last row.
            return;
        }

        // Remove the last row from the map.
        MapTile[,] newMap = new MapTile[rows - 1, cols];

        for (int i = 0; i < rows - 1; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                newMap[i, j] = map[i, j];
            }
        }

        map = newMap;
        rows--;

        // Update the row above the removed row to meet Minesweeper rules.
        UpdateAdjacentTileCounts(rows - 1);
    }
}
