using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnemyCheck : MonoBehaviour
{


    public PlayerController playercontrollerscript;
    public int howManyEnemieShouldBeDead;
    public EndGoal doorisopen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playercontrollerscript.howManyDeadEnemies >= howManyEnemieShouldBeDead)
        {
            doorisopen.FadeToLevel(3);
        }
    }
}
