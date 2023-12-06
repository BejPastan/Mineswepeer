using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SquadControler : MonoBehaviour
{
    Tile[] visitedTrenches = new Tile[0];
    [SerializeField]
    Soldier[] soldiers;
    [SerializeField]
    Bombardment bombardment;

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
        //Debug.Log("sprawdza czy nie doszedł już do tego Trencha");
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
            Debug.Log("znalazł drogę");
            return true;
        }

        return false;
    }
    

    //poruszanie jednostek
    public void MoveUnits(Tile[] newTrench,ref Tile[,] map)
    {

        for (int i=0; i<newTrench.Length; i++)
        {
            Array.Resize(ref visitedTrenches, visitedTrenches.Length+1);
            visitedTrenches[visitedTrenches.Length-1] = newTrench[i];
        }
        for(int trenchNum = 0; trenchNum < newTrench.Length; trenchNum++)
        {
            Tile[] path = new Tile[0];
            try
            {
                Pathfinding.GetWay(soldiers[trenchNum].soldierLocation.posX, soldiers[trenchNum].soldierLocation.posY, newTrench[trenchNum].posX, newTrench[trenchNum].posY, ref map, ref path);
                soldiers[trenchNum].Move(path);
            }
            catch
            {
                break;
            }
        }
        bombardment.BombardControl(newTrench);
        //get visited trench with biggest Y
        int maxY = 0;
        foreach (Tile trench in visitedTrenches)
        {
            if (trench.posY > maxY && trench.posY< newTrench[0].posY)
            {
                maxY = trench.posY;
            }
        }

        //move camera
        MoveCamera(newTrench[0].posY - maxY);

    }

    private async Task MoveCamera(float yChange)
    {
        //move camera for yChange up
        Vector3 move = new Vector3(0, yChange, 0);
        for (int i = 0; i < 100; i++)
        {
            Camera.main.transform.position += move / 100;
            await Task.Delay(40);
        }

    }

    public Soldier[] GetSoldiersOnTile(List<Vector2> effectedTile)
    {
        //zbiera wszystkich żołnierzy na tile'ach z listy
        List<Soldier> soldiersOnTile = new List<Soldier>();
        foreach(Soldier soldier in soldiers)
        {
            foreach(Vector2 tile in effectedTile)
            {
                if(soldier.soldierLocation.posX == tile.x && soldier.soldierLocation.posY == tile.y)
                {
                    soldiersOnTile.Add(soldier);
                }
            }
        }
        return soldiersOnTile.ToArray();
    }
}
