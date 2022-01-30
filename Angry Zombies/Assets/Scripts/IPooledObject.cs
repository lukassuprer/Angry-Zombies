using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    //Creates method that acts like start method for spawned enemies 
    void OnObjectSpawn();
}
