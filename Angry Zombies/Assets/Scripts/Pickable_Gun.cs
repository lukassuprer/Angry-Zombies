using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Gun : MonoBehaviour
{
    public static bool shotgunUnlocked = false;
    public static bool umpUnlocked = false;
    public static bool gunsUnlocked = false;
    private string shotgunName = "droppable_shotgun_prefab(Clone)";
    private string umpName = "droppable_ump_prefab(Clone)";
    void Update()
    {
        //Checks if guns are unlocked
        if(shotgunUnlocked == true && umpUnlocked == true){
            gunsUnlocked = true;
        }
    }
    //On collision with player sets that weapon activate and destroys this prefab
    private void OnTriggerEnter(Collider player){
        if(gameObject.name == shotgunName){
            shotgunUnlocked = true;
            Destroy(this.gameObject);
        }
        else if(gameObject.name == umpName){
            umpUnlocked = true;
            Destroy(this.gameObject);
        }
        else{}
    }
}
