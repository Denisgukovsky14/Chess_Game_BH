using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueSaver 
{
    public static float LES ;

    public static void Remember(float value)
    {
        LES = value;
    }
}
