using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnableObject {
    NADA, SAW, SPIDER, COIN
};


[CreateAssetMenu(menuName = "Custom/Tile")]
public class TileData : ScriptableObject {

    [System.Serializable]
    public class linha : IEnumerable<SpawnableObject>
    {
        public List<SpawnableObject> line = new List<SpawnableObject>();

        public IEnumerator<SpawnableObject> GetEnumerator()
        {
            return line.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return line.GetEnumerator();

        }

        internal int IndexOf(SpawnableObject so)
        {
            return line.IndexOf(so);
        }
    }

    public List<linha> matriz = new List<linha>();

}