using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fluttua : MonoBehaviour
{
    [SerializeField] private bool fluattuamento = true;
    [SerializeField] private float gainRotazione = 10;
    [SerializeField] private float gainFluttuamento = 0.001f;

    public void Start()
    {
        SetFluttuamento(fluattuamento);
    }

    public void SetGainFluttuamento(float gainFluttuamento)
    {
        this.gainFluttuamento = gainFluttuamento;
    }

    public void SetGainRotazione(float gainRotazione)
    {
        this.gainRotazione = gainRotazione;
    }

    public bool GetFluttuamento()
    {
        return this.fluattuamento;
    }

    public void SetFluttuamento(bool fluattuamento)
    {
        this.fluattuamento = fluattuamento;

        if (this.fluattuamento)
        {
            // fai galleggiare l'oggetto e tienielo a galla
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            // crea un animazione di galleggiamento con il Lerp
            StartCoroutine(FluttuaOperation());
        }
        else
        {
            // fai tornare l'oggetto a cadere
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }

    IEnumerator FluttuaOperation()
    {
        while (fluattuamento)
        {
            this.transform.Rotate(Vector3.up, gainRotazione * Time.deltaTime, Space.World);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + Mathf.Sin(Time.time) * gainFluttuamento, this.transform.position.z);

            yield return null;
        }
    }
}
