using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpismaKontrol : MonoBehaviour
{
    private bool menzileGirdiMi = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zemin")
            return;
        menzileGirdiMi = true;
    }
    private void OnTriggerExit(Collider other)
    {
        menzileGirdiMi = false;
    }
    public bool menzilBilgisiDon()
    {
        return menzileGirdiMi;
    }
    public void menzilBilgisiGir(bool menzilBilgi)
    {
        menzileGirdiMi = menzilBilgi;
    }
}


