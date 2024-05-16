using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneControl : object
{
    
    public static int SN;
    public static int CL;

    public static void RememberNumber(int scene)
    {
        SN = scene;
        Debug.Log(SN);
    }

    public static void Restart(int level)
    {

    }
    
}
