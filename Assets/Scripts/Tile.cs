using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private TileState state;
    [SerializeField]
    Sprite reveladSprite;

    public void TileConstructor(TileState state, Vector2 location)
    {
        this.state = state;
        gameObject.transform.position = location;
    }

    public void SetAdjecentMines(int adjecentMines)
    {
        state.SetAdjecentMines(adjecentMines);
    }

    public bool Revel()
    {
        bool notMine = state.Revel();

        if(notMine)
        {
            GetComponentInChildren<TextMesh>().text = AdjacementMines.ToString();
            GetComponent<SpriteRenderer>().sprite = reveladSprite;
        }
        else
        {
            GetComponentInChildren<TextMesh>().text = "M";
        }


        return notMine;
    }

    public bool IsMine
    {
        get { return state.IsMine; }
    }

    public int AdjacementMines
    {
        get { return state.AdjacementMines; }
    }
}

public class TileState
{
    private bool isMine;
    private bool isTrench;
    private bool isReveled;
    private int adjecentMines;

    public TileState(bool isMine, bool isTrench)
    {
        this.isMine = isMine;
        this.isTrench = isTrench;
    }

    public void SetAdjecentMines(int adjecentMines)
    {
        this.adjecentMines = adjecentMines;
    }

    public bool Revel()
    {
        if (!isReveled)
        {
            isReveled = true;
            if (isMine)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsMine
    {
        get { return isMine; }
    }
    public int AdjacementMines
    {
        get { return adjecentMines; }
    }

    public void setTrench()
    {
        isTrench = true;
    }
}
