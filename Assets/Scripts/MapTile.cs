using UnityEngine;


[CreateAssetMenu(fileName = "MapTile", menuName = "Minesweeper/Map Tile")]
public class MapTile : ScriptableObject
{
    public enum TileState
    {
        Covered,
        Uncovered,
        Flagged,
        QuestionMark
    }

    [SerializeField] private TileState currentState = TileState.Covered;
    [SerializeField] private bool containsMine = false;
    [SerializeField] private int nearbyMinesCount = 0;

    public TileState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public bool ContainsMine
    {
        get { return containsMine; }
        set { containsMine = value; }
    }

    public int NearbyMinesCount
    {
        get { return nearbyMinesCount; }
        set { nearbyMinesCount = value; }
    }

    public bool IsUncovered
    {
        get { return currentState == TileState.Uncovered; }
    }

    public bool IsFlagged
    {
        get { return currentState == TileState.Flagged; }
    }

    public bool IsQuestionMarked
    {
        get { return currentState == TileState.QuestionMark; }
    }

    public void Uncover()
    {
        if (currentState == TileState.Covered)
        {
            currentState = TileState.Uncovered;
            // Perform any additional game logic when a tile is uncovered.
        }
    }

    public void ToggleFlag()
    {
        if (currentState == TileState.Covered)
        {
            currentState = IsFlagged ? TileState.Covered : TileState.Flagged;
            // Toggle flag on the tile.
        }
    }

    public void ToggleQuestionMark()
    {
        if (currentState == TileState.Covered)
        {
            currentState = IsQuestionMarked ? TileState.Covered : TileState.QuestionMark;
            // Toggle question mark on the tile.
        }
    }
}
