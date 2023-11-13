using UnityEngine;

public class Tile : MonoBehaviour
{
    private TileState state;

    public GameObject CreateTile(GameObject tilePref)
    {
        GameObject tile = Instantiate(tilePref);
        return tile;
    }

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
        Debug.Log(state.Revel());
        return state.Revel();
    }

    public bool IsMine
    {
        get { return state.IsMine; }
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

    public void setTrench()
    {
        isTrench = true;
    }
}
