using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Pathfinding
{
    private static Node[,] nodeMap;
    private static int endX, endY;

    public static bool GetWay(int StartX, int StartY, int endX, int endY, ref Tile[,] map, ref Tile[] path)
    {
        path = new Tile[0];
        nodeMap = new Node[map.GetLength(0), map.GetLength(1)];
        nodeMap[StartX, StartY] = new Node(CalcDistance(StartX, StartY), 0, false, StartX, StartY, map[StartX, StartY]);
        Node selectedNode = nodeMap[StartX, StartY];

        //Debug.LogWarning("Cel: "+ endX + ", "+ endY);
        while (selectedNode.x != endX || selectedNode.y != endY)
        {
            bool newSelect = false;
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if(nodeMap[x, y] != null)
                    {
                        if (!nodeMap[x, y].checkedNode)
                        {
                            //tu się wyjebie bo stary node będzie miał za małą cene
                            nodeMap[x, y].SetNewPrice(selectedNode);
                            if (nodeMap[x, y].GetPrice() < selectedNode.GetPrice() || !newSelect)
                            {
                                selectedNode = nodeMap[x, y];
                                newSelect = true;
                            }
                        }
                    }
                }
            }
            //Debug.LogWarning("selected node = " + selectedNode.x + ", " + selectedNode.y);
            selectedNode.checkedNode = true;
            if(newSelect)
            {
                for (int x = selectedNode.x - 1; x <= selectedNode.x + 1; x++)
                {

                    for (int y = selectedNode.y - 1; y <= selectedNode.y + 1; y++)
                    {

                        if ((x == selectedNode.x && y == selectedNode.y) || x < 0 || x>=map.GetLength(0) || y<0)
                        {
                            continue;
                        }
                        else
                        {
                            //updateuje lub tworzy nowy node
                            //Debug.Log("Sprawdza czy pole "+x+", "+y + " jest odkryte");
                            if (map[x, y].IsReveald)
                            {
                                //Debug.Log("nowy node w "+x+", "+y);
                                if (nodeMap[x, y] == null)
                                {
                                    nodeMap[x, y] = new Node(CalcDistance(x, y), 2, false, x, y, map[x, y], selectedNode);
                                }
                                nodeMap[x, y].SetNewPrice(selectedNode);
                                //Debug.Log(nodeMap[x, y].GetPrice());
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //Debug.Log("Znalazł trasę");

        //zbieranie trasy
        //muszę to odwrócić
        int nodeId = 0;
        path = new Tile[1];
        path[0] = selectedNode.tile;
        while (selectedNode.parent!= null)
        {
            Array.Resize(ref path, path.Length+1);
            path[path.Length - 1] = selectedNode.parent.tile;
            //Debug.Log(path[nodeId]);
            selectedNode = selectedNode.parent;
            nodeId++;
        }
        Array.Reverse(path);
        return true;
    }

    private static int CalcDistance(int x, int y)
    {
        int price;

        price = Mathf.Abs(endX-x) + Mathf.Abs(endY - y);

        return price;
    }
}

public class Node
{
    public float targetPrice;
    public float movePrice;
    public bool checkedNode;
    public Node parent;
    public int x, y;
    public Tile tile;

    public Node(float targetPrice, float movePrice, bool checkedNode, int x, int y, Tile tile, Node parent = null)
    {
        this.targetPrice = targetPrice;
        this.movePrice = movePrice;
        this.checkedNode = checkedNode;
        this.parent = parent;
        this.x = x;
        this.y = y;
        this.tile = tile;
    }

    public float GetPrice()
    {
        return targetPrice+movePrice;
    }

    public void SetNewPrice(Node parentNode)
    {
        float newMovePrice;
        if(parentNode.x==x || parentNode.y==y)
        {
            newMovePrice = parentNode.movePrice+1;
        }
        else
        {
            newMovePrice = parentNode.movePrice + 2;
        }

        if(newMovePrice<movePrice)
        {
            parent = parentNode;
            movePrice = newMovePrice;
            checkedNode = false;
        }
    }
}
