using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Colors : Singleton<Colors>
{
    public Color On, Off;

    void Awake ()
    {
        SingletonSetInstance(this, true);
    }
}
