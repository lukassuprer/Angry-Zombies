using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableStart : MonoBehaviour
{
    //Disables navmesh colliders on start
    void Start()
    {
        Destroy(gameObject);
    }
}
