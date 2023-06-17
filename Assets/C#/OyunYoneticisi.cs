using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OyunYoneticisi : MonoBehaviour
{
    public GameObject yem;
    public Transform parent;
    float zamanSayac = 0f, yemOlusturmaAraligi = 5f, oyunSonuSayac = 0f, olusacakYemSayisi = 20f;
    bool yemUret = true;
    public static bool oyunSonu = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (oyunSonu)
        {
            oyunSonuSayac += Time.deltaTime;
            if (oyunSonuSayac > 2f)                 // player carptýktan sonra 2 saniye bekle oyun sonu ekranýný getir
            {
                canvas.GetComponent<Animator>().SetTrigger("oyunSonu");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            oyunSonu = false;
            SceneManager.LoadScene("Scene1");
        }
        zamanSayac += Time.deltaTime;
        if (yemUret)
        {
            if (zamanSayac > yemOlusturmaAraligi)  // her 5 saniyede bir belirtilen sayida yem uret
            {

                for (int i = 0; i < olusacakYemSayisi; i++)
                {
                    Vector3 randPosition = new Vector3(Random.Range(-100, 100f) + transform.position.x, 6f, Random.Range(-100, 100f) + transform.position.z);
                    GameObject yeniYem = Instantiate(yem, randPosition, Quaternion.identity);
                    yeniYem.transform.parent = parent;
                }
                zamanSayac = 0f;
            }
        }
        if (GameObject.Find("YEMLER").transform.childCount > 250)  //sahnede ayný anda en fazla kac yem olacagi 
            yemUret = false;
        else
            yemUret = true;

    }

}