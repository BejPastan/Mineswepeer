using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public Tile soldierLocation;
    [SerializeField]
    SoldierHP hpBar;
    int maxHP = 5;

    public void StartGame(Tile startLocation)
    {
        soldierLocation = startLocation;
        transform.position = startLocation.transform.position;
        hpBar.SetHP(maxHP);
    }

    public void Hit()
    {
        if(!hpBar.TakeDamage(1))
        {
            Killed();
        }
    }

    private void Killed()
    {
        
    }

    //poruszanie jednostkami
    public async Task Move(Tile[] route)
    {
        Debug.Log("zaczyna poruszać");
        //przechodzi przez każdy punkt trasy
        foreach(Tile tile in route)
        {
            //Debug.Log(tile.name+" dla "+gameObject.name);
            Vector3 move = (tile.transform.position - transform.position) /25;
            for(int i = 25;  i > 0; i--) 
            {
                transform.position += move;
                await Task.Delay(40);
            }
            soldierLocation = tile;

        }
    }
}
