using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cassa : MonoBehaviour
{
    private Fluttua fluttuamento;
    // Start is called before the first frame update
    void Start()
    {
        fluttuamento = this.GetComponent<Fluttua>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Mare" && !this.fluttuamento.GetFluttuamento())
        {
            Debug.Log("Cassa tocca il mare");

            fluttuamento.SetFluttuamento(true);
            fluttuamento.SetGainFluttuamento(0.0001f);
        }
    }
}
