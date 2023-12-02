using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadControler : MonoBehaviour
{
    Tile[] visitedTrenches = new Tile[0];
    [SerializeField]
    Soldier[] soldiers;


    public void StartGame(ref Tile[,] map)
    {
        foreach(Soldier soldier in soldiers)
        {
            soldier.StartGame(map[Random.Range(0, map.GetLength(0)), 0]);
        }
    }

    //return true if can move squad
    public bool FindTrench(ref Tile trenchTile, ref Tile[,] map)
    {
        Debug.Log("sprawdza czy nie doszedł już do tego Trencha");
        foreach(Tile tile in visitedTrenches)
        {
            if(tile == trenchTile)
            {
                return false;
            }
        }
        Debug.Log("pierwszy pathfinding");
        //sprawdza czy może poruszyć się do tego Trench'a z aktualnej pozycji
        Tile[] path = new Tile[0];
        //nie działa, ale to niżej
        if (Pathfinding.GetWay(soldiers[0].soldierLocation.posX, soldiers[0].soldierLocation.posY, trenchTile.posX, trenchTile.posY, ref map, ref path))
        {
            Debug.Log("Można dotrzeć");
            return true;
        }

        return false;
    }

    

    //poruszanie jednostek
    public void MoveUnits(Tile[] newTrench,ref Tile[,] map)
    {
        Debug.Log("zaczyna poruszać ludków");
        for(int trenchNum = 0; trenchNum < newTrench.Length; trenchNum++)
        {
            Tile[] path = new Tile[0];
            Pathfinding.GetWay(soldiers[trenchNum].soldierLocation.posX, soldiers[trenchNum].soldierLocation.posY, newTrench[trenchNum].posX, newTrench[trenchNum].posY, ref map, ref path);
            soldiers[trenchNum].Move(path);
        }
    }
}
