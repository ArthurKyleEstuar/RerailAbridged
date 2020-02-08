using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offSet = new Vector3(0, 20, 0);
    [SerializeField] private bool lockZ = true;

    private void Start()
    {
        offSet.z = this.transform.position.z;

        if (target == null) Debug.LogError("No camera target assigned!");
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        this.transform.position = target.transform.position + offSet;
    }
}
