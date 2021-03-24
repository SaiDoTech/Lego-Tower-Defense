using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanBePlaced
{
    bool CheckCell(CellObject cell);

    void PlaceIt();
}
