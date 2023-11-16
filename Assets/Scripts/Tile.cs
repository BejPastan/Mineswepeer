using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private TileState state;
    [SerializeField]
    Sprite reveladSprite;
    Transform flag;
    Transform text;
    bool trench;

    public void TileConstructor(TileState state, Vector2 location)
    {
        this.state = state;
        gameObject.transform.position = location;


        Transform[] potentialFlags = GetComponentsInChildren<Transform>();
        for(int i = 0; i < potentialFlags.Length; i++)
        {
            if (potentialFlags[i].name == "Flag")
            {
                flag = potentialFlags[i];
                break;
            }
        }

        Transform[] potentialText = GetComponentsInChildren<Transform>();
        for (int i = 0; i < potentialText.Length; i++)
        {
            if (potentialText[i].name == "MineText")
            {
                text = potentialText[i];
                break;
            }
        }
    }

    public void SetTrench()
    {
        trench = true;
        //tutaj musi podmieniać sprite i wyłączać metody
    }

    public void SetAdjecentMines(int adjecentMines)
    {
        state.SetAdjecentMines(adjecentMines);
    }

    public bool Revel()
    {
        bool notMine = state.Revel();
        
        if(!state.isFlaged)
        {
            if (notMine)
            {
                //tutaj fajnie jakby nie wstawiał tekstu jeśli nie ma min wokół
                text.GetComponent<TextMesh>().text = AdjacementMines.ToString();
                GetComponent<SpriteRenderer>().sprite = reveladSprite;
            }
            else
            {
                //znalazł minę
                text.GetComponent<TextMesh>().text = "M";
            }
            return notMine;
        }
        return true;
    }

    public void Flag()
    {
        if(state.isFlaged)
        {
            state.isFlaged = false;
            flag.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            state.isFlaged = true;
            flag.GetComponent<SpriteRenderer>().enabled = true;
        }
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
    private bool isReveled;
    public bool isFlaged;
    private int adjecentMines;

    public TileState(bool isMine)
    {
        this.isMine = isMine;
    }

    public void SetAdjecentMines(int adjecentMines)
    {
        this.adjecentMines = adjecentMines;
    }

    public bool Revel()
    {
        if (!isReveled && !isFlaged)
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
}
