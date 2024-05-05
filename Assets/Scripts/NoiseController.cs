using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseController : MonoBehaviour
{
    public MovePlate Plate;
    public float Volume;

    public void Start()
    {
        Volume = Plate.Volume;
    }

    public void SetVolume(float value)
    {
        Plate.Volume = value;
        Volume = value;
    }

}
