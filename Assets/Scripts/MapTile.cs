using UnityEngine;

public enum TileAction
{
    None,
    Flag,
    Question,
    Uncover
}

public class MapTile : ScriptableObject
{
    private int mineNum;
    private bool mine;
    private TileAction action;

    public int MineNum => mineNum;
    public bool IsMine => mine;
    public TileAction Action => action;

    public void SetMine(int mineNumber)
    {
        mine = true;
        mineNum = mineNumber;
        action = TileAction.None;
    }

    public void ClearMine()
    {
        mine = false;
        mineNum = 0;
        action = TileAction.None;
    }

    public void SetAction(TileAction tileAction)
    {
        action = tileAction;
    }

    public void ClearAction()
    {
        action = TileAction.None;
    }
}
