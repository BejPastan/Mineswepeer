using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public Tile soldierLocation;
    int HP = 5;

    public void StartGame(Tile startLocation)
    {
        soldierLocation = startLocation;
        transform.position = startLocation.transform.position;
    }

    public void Hit()
    {
        HP--;
        Debug.Log(HP);
        if(HP <= 0 )
            Killed();
    }

    private void Killed()
    {

    }

    //poruszanie jednostkami
    public async Task Move(Tile[] route)
    {
        //przechodzi przez każdy punkt trasy
        foreach(Tile tile in route)
        {
            Vector3 move = (tile.transform.position - transform.position) /25;
            for(int i = 25;  i < 25; i--) 
            {
                transform.position += move;
                await Task.Delay(40);
            }
            soldierLocation = tile;
        }
    }
}
