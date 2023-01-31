using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmedButtonPress : MonoBehaviour
{
    public Material[] material;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial = material[0];
    }

    public void confirm()
    {
        rend.sharedMaterial = material[1];
    }
}
