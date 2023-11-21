using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadControler : MonoBehaviour
{
    Tile[] visitedTrenches;

    //return true if can move squad
    public bool FindTrench(Tile trenchTile)
    {
        foreach(Tile tile in visitedTrenches)
        {
            if(tile == trenchTile)
            {
                return false;
            }
        }

        //sprawdza czy może poruszyć się do tego Trench'a z aktualnej pozycji


        return true;
    }

    

    //poruszanie jednostek
    public void MoveUnits(Tile[] newTrench,ref Tile[,] map)
    {
        if(Pathfinding.GetWay())
        {

        }
    }

}
