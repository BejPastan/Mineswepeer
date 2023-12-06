using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bombardment : MonoBehaviour
{
    [SerializeField]
    int artyDelay;
    [SerializeField]
    int flightTime;
    Tile[] target = new Tile[0];
    [SerializeField]
    int explosionRange;
    [SerializeField]
    Vector2 accuracy;
    [SerializeField]
    MapHandler map;
    [SerializeField]
    SquadControler squad;
    [SerializeField]
    int shotsNum;
    [SerializeField]
    GameObject explosionParticle;

    public void BombardControl(Tile[] newTarget)
    {
        target = newTarget;
    }

    public async Task StartBombard()
    {
        if(target.Length != 0)
        {
            for (int i = 0; i < shotsNum; i++)
            {
                Tile targetTile = target[Random.Range(0, target.Length)];
                float randomAngle = Random.Range(0f, 2f * Mathf.PI);
                // Calculate random position within the specified radius
                int x = targetTile.posX + Mathf.RoundToInt(accuracy.x * Mathf.Cos(randomAngle));
                int y = targetTile.posY + Mathf.RoundToInt(accuracy.y * Mathf.Sin(randomAngle));
                await Task.Delay(1000);
                Debug.Log("cel: " + x + " " + y);
                Splash(map.map[x, y]);
            }
        }
        await Task.Delay(artyDelay);
        if(Application.isPlaying)
            StartBombard();
    }

    private async Task Splash(Tile splashPoint)
    {
        Debug.Log("zaczyna eksplozję");
        bool trench = splashPoint.IsTrench;
        //czeka czas lotu pocisku
        await Task.Delay(flightTime);
        Transform splashAnimation = Instantiate(explosionParticle, splashPoint.transform.position, Quaternion.identity).transform;
        List<Tile> tilesInRange;
        if (trench)
        {
            //zadawanie dmg tylko w trenchach graniczących z tym na którym spadł pocisk
            Tile[] trenches = map.GetTrenchesInRow(splashPoint);
            tilesInRange = new List<Tile>();
            foreach(Tile trenchTile in trenches)
            {
                tilesInRange.AddRange(map.GetTilesInRange(trenchTile, explosionRange));
            }   
        }
        else
        {
            //tworzy tablicę z wszystkimi tile'ami w zasięgu eksplozji oprócz trench'y
            tilesInRange = map.GetTilesInRange(splashPoint, explosionRange);
        }
        Debug.Log("Będzie tworzył listę Tile'i w zasięgu");
        //make list of vectors from tilesInRange
        List<Vector2> tilesInRangeVector = new List<Vector2>();
        foreach(Tile tile in tilesInRange)
        {
            tilesInRangeVector.Add(new Vector2(tile.posX, tile.posY));
        }

        Debug.Log("Zaczyna pobierać miejsca zagrożone trafieniem");
        //pobieranie jednostek znajdujących się na tych tile'ach za pomocą
        Soldier[] soldiersInRange = squad.GetSoldiersOnTile(tilesInRangeVector);
        Debug.Log("soldiers in range = "+soldiersInRange.Length);
        //zadaje dmg jednostkom
        foreach(Soldier soldier in soldiersInRange)
        {
            soldier.Hit();
        }
        //wait 0.5s and remove splash animation
        await Task.Delay(1000);
        Debug.Log("usuwa wybuch");
        Destroy(splashAnimation.gameObject);
    }
}
