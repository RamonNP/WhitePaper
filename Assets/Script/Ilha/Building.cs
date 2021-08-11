using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed;// {get; private set;}
    public BoundsInt area;

    #region  metodos
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanBePlaced() {
        Vector3Int positionInt = GridBuildingSystem.instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        if(GridBuildingSystem.instance.CantakeArea(areaTemp)) {
            return true;
        }
        return false;
    }

    public void Place() {
         Vector3Int positionInt = GridBuildingSystem.instance.GridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.instance.TakeArea(areaTemp);
    }

    #endregion

}
