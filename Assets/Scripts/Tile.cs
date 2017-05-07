using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    TileData data;
    

    public void setData(TileData data)
    {
        this.data = data;
        init();
    }

    public void init()
    {

    }
    
}
