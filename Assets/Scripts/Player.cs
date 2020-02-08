using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Player : Singleton<Player>
{
    public PlayerMovement Movement;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }
}
