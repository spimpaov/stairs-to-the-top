using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnableObject {
    NADA, SAW, SPIDER, COIN, POWER_UP, TOGGLE_SWITCH, LASER
};

[CreateAssetMenu(menuName = "Custom/Tile")]
public class TileData : ScriptableObject {

    [System.Serializable]
	public class linha : System.Object
    {
        public List<SpawnableObject> line = new List<SpawnableObject>();
    }

    public List<linha> matriz;

}
