using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SolucanBas : MonoBehaviour
{
    private float hiz = 5f;
    private PlayerKuyruk kuyruk;
    private float oyunZamani = 0f;
    public GameObject yem;
    private float skor = 1f, uzunlukSkor = 0f, yemKaybetmeAraligi = 0f, kuyrukKaybet = 0f;
    private float buyumeHiz = 0.01f;
    public TextMeshProUGUI skorText;
    float RotX;
    float RotY;
    public GameObject Bas;
    // Start is called before the first frame update
    void Start()
    {
        kuyruk = GameObject.FindGameObjectWithTag("PlayerKuyruk").GetComponent<PlayerKuyruk>();
    }

    // Update is called once per frame
    void Update()
    {
        if(skorText != null)
        {
            skorText.text = "SKOR " + (int)(skor * 100);
        }               
        oyunZamani += Time.deltaTime;                               
        RotX = Input.GetAxis("Horizontal");
        RotY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-RotX, 0, -RotY) * hiz * Time.deltaTime;
        Bas.transform.Translate(movement);


        if (Input.GetKeyDown(KeyCode.E))
        {
            yemKaybetmeAraligi += Time.deltaTime;                    
            kuyrukKaybet += Time.deltaTime;                          

            hiz = 15f;
            PlayerKuyruk.hiz = 15f;                                 

            skor -= 0.0008f;
            if (yemKaybetmeAraligi > 0.5f)
            {
                yemKaybetmeAraligi = 0f;
                Vector3 yemPos = PlayerKuyruk.lastPosition.position;
                yemPos.y = 6f;
                Instantiate(yem, yemPos, PlayerKuyruk.lastPosition.rotation);
            }
            if (kuyrukKaybet > 2f)
            {
                kuyrukKaybet = 0f;
                kuyruk.kuyrukYokEt();
            }
        }
        // mouse sað tuþ çekildiðinde eski hýza döndürür.
        if (Input.GetKeyUp(KeyCode.E))
        {
            hiz = 5f;
            PlayerKuyruk.hiz = 5f;
        }
    }
}
