using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characthericon : MonoBehaviour
{
    public MenuCharactherController Mcc;
    public List<Sprite> materials;
    public Image kup;

    private void Start()
    {
      //  Mcc=GetComponent<MenuCharactherController>();
    }



    void FixedUpdate()
    {
        if (Mcc.SelectCharacther == 0)
        {
            kup.sprite = materials[0];
        }
        if (Mcc.SelectCharacther == 1)
        {
            kup.sprite = materials[1];
        }
        if (Mcc.SelectCharacther == 2)
        {
            kup.sprite = materials[2];
        }
        if (Mcc.SelectCharacther == 3)
        {
            kup.sprite = materials[3];
        }
        if (Mcc.SelectCharacther == 4)
        {
            kup.sprite = materials[4];
        }
        if (Mcc.SelectCharacther == 5)
        {
            kup.sprite = materials[5];
        }
        if (Mcc.SelectCharacther == 6)
        {
            kup.sprite = materials[6];
        }
    }
}
