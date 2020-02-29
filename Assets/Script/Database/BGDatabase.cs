using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BGData : BaseData
{
    [SerializeField] private Sprite bgImage;

    public Sprite BGImage { get { return bgImage; } }
}

[CreateAssetMenu(fileName = "BGDatabase", menuName = "Database/BGDatabase")]
public class BGDatabase : BaseDatabase<BGData>
{
    
}
