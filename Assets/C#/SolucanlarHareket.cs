using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolucanlarHareket : MonoBehaviour
{
    float hiz = 5f;
    Vector3 direction;
    float timer = 0f, baslangicHazirlik = 0f;
    float dusunmeAraligi;

    //static int raySayisi = 0;
    public GameObject solucanKuyruk;
    public GameObject yem;
    SolucanlarKuyruk kuyruk;


    float timerYem = 0f;
   



    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        dusunmeAraligi = 3f;//Random.Range(2, 6);
        kuyruk = solucanKuyruk.GetComponent<SolucanlarKuyruk>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (baslangicHazirlik < 2f) ;
        baslangicHazirlik += Time.deltaTime;
        timerYem += Time.deltaTime;
    }
       

    public void solucanYokEt()
    {
        foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
        {
            KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x, 0, KuyrukX.transform.position.z);
            Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
        }
        //Debug.Log("Solucan Bas carptý yok oluyor");
        Destroy(transform.parent.gameObject);
    }


}
