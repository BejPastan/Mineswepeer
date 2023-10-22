using System;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    private IMap map;
    private IMapMover mapMover;
    private IFieldInteractor fieldInteractor;

    public MapControler(IMap generator, IMapMover mover, IFieldInteractor interactor)
    {
        map = generator;
        mapMover = mover;
        fieldInteractor = interactor;
    }

    public void CreateMap(int rowCount)
    {
        for(int i = 0; i < rowCount; i++)
        {
            map.CreateNewRow();
        }
        
    }

    public void MoveMap()
    {
        mapMover.MoveMap();
    }

    public void InteractWithField()
    {
        fieldInteractor.InteractWithField();
    }
}

public interface IFieldInteractor
{
    void InteractWithField();
}

public class FieldInteractor : IFieldInteractor
{
    IMap map;
    // Implementation of IFieldInteractor method
    public void InteractWithField()
    {
        // Implement field interaction logic
    }
}

public interface IMapMover
{
    void MoveMap();
}

public class MapMover : IMapMover
{
    private IMap map;

    public MapMover(IMap generator)
    {
        map = generator;
    }

    // Implementation of IMapMover method
    public void MoveMap()
    {
        map.RemoveLastRow();
        map.CreateNewRow();
    }
}

public interface IMap
{
    void CreateNewRow();
    void RemoveLastRow();
}

public class Map : IMap
{
    private MapTile[] map;

    public void CreateNewRow()
    {

    }

    public void RemoveLastRow()
    {

    }
}
