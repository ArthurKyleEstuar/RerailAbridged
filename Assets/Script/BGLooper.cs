using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour
{
    [SerializeField] private GameObject bgPrefab;
    [SerializeField] private int        bgCount;
    [SerializeField] private float      bgSize;
    [SerializeField] private float      bgScale;

    void Start()
    {
        

        for(int i = 0; i < bgCount; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            spawnPos.x += (i * bgSize);
            
            GameObject go = Instantiate(bgPrefab, parent: this.transform);
            go.transform.localPosition = spawnPos;
            go.transform.localScale = new Vector3(bgScale, bgScale, bgScale);
        }
    }
}
