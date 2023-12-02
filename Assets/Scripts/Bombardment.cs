using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bombardment : MonoBehaviour
{
    [SerializeField]
    int artyDelay;
    bool moved;
    Tile[] target;

    public void BombardControl(Tile[] newTarget)
    {
        target = newTarget;
    }

    private async Task StartBombard()
    {
        Splash(target);
        Task.Delay(artyDelay);
    }

    private async Task Splash(Tile[] splashArea)
    {
        //czeka czas lotu pocisku
        //zadawanie dmg
    }
}
