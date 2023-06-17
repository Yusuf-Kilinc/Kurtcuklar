using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKuyruk : MonoBehaviour
{
    public Transform basTrans;
    public GameObject kuyrukX;
    public static Transform lastPosition;                   // Her zaman en son olu�mu� olan kuyru�un transform bilgisini tutar.
    private List<GameObject> kuyrugum;
    private int kuyrukId = 0;
    public static float hiz = 5f;
    private readonly float kuyrukYukseklikFarki = 0.1f;      // Ba�a yak�n olan kuyruklar�n uzak olan kuyruklar�n �zerinde g�r�nmesi i�in.0.04

    // Ba�lang��ta var olan kuyruklar� olu�turmak i�in.
    Vector3 pos, targetPos;
    GameObject yeniKuyruk;
    public float StartTimer = 3;

    // Start is called before the first frame update
    void Start()
    {
        kuyrugum = new List<GameObject>();
        kuyrugum.Add(new GameObject());
        lastPosition = basTrans;

        if (StartTimer == 0)
        {
            kuyrukId = 1;
            pos = new Vector3(0f, 0.01f, 0.4f);
            targetPos = basTrans.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);


            // 2. kuyruk
            kuyrukId++;
            pos = new Vector3(0f, 0.02f, 0.4f);
            targetPos = lastPosition.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);

            // 3. kuyruk
            kuyrukId++;
            pos = new Vector3(0f, 0.03f, 0.4f);
            targetPos = lastPosition.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);
        }  
      

    }

    void Update()
    {
        if (StartTimer > 0)
        {
            StartTimer -= Time.deltaTime;   
        }
        if (StartTimer <= 0)
        {
            StartTimer = 0;
        }

        if (StartTimer == 0)
        {
            kuyrukId = 1;
            pos = new Vector3(0f, 0.01f, 0.4f);
            targetPos = basTrans.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);


            // 2. kuyruk
            kuyrukId++;
            pos = new Vector3(0f, 0.02f, 0.4f);
            targetPos = lastPosition.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);

            // 3. kuyruk
            kuyrukId++;
            pos = new Vector3(0f, 0.03f, 0.4f);
            targetPos = lastPosition.position - pos;
            yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
            yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
            yeniKuyruk.transform.parent = transform;
            lastPosition = yeniKuyruk.transform;
            kuyrugum.Add(yeniKuyruk);
        }

        if (StartTimer == 0)
        {
            for (int i = 1; i <= kuyrukId; i++)
            {
                if (i == 1)
                {
                    Vector3 targetPosX = basTrans.transform.position - new Vector3(0f, kuyrukYukseklikFarki, 0f);
                    Quaternion targetRotationX = Quaternion.LookRotation(targetPosX - kuyrugum[1].transform.position);
                    kuyrugum[1].transform.rotation = Quaternion.Slerp(kuyrugum[1].transform.rotation, targetRotationX, hiz * 2f * Time.deltaTime);
                    kuyrugum[1].transform.Translate(Vector3.forward * hiz * Time.deltaTime);
                }
                else            // T�m kuyruklar kendilerinden �nceki kuyru�u takip edecekler.
                {
                    Vector3 targetPos;
                    if (i < 10)        // ilk 10 kuyruk yere yak�n olsunki kalan kuyruklar�n �zerinde g�r�ns�n.
                        targetPos = kuyrugum[i - 1].transform.position - new Vector3(0f, kuyrukYukseklikFarki, 0f);
                    else if (kuyrugum[i - 1].transform.position.y > 0.5f)  // 10 dan sonrakiler 0.01f daha alcakta. 
                        targetPos = kuyrugum[i - 1].transform.position - new Vector3(0f, kuyrukYukseklikFarki / 4, 0f);
                    else
                        targetPos = kuyrugum[i - 1].transform.position; // zeminin alt�na gitmemesi i�in son gelenler sabit.

                    Quaternion targetRotation = Quaternion.LookRotation(targetPos - kuyrugum[i].transform.position);
                    kuyrugum[i].transform.rotation = Quaternion.Slerp(kuyrugum[i].transform.rotation, targetRotation, hiz * 2f * Time.deltaTime);
                    kuyrugum[i].transform.Translate(Vector3.forward * hiz * Time.deltaTime);
                }
            }
        }
       
    }

    // Yem yedi�imde yeni kuyruk ekle.
    public void kuyrukEkle(Vector3 boyut)
    {
        kuyrukId++;
        pos = new Vector3(0f, 0f, 0.4f);
        targetPos = lastPosition.position - pos;
        yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
        yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
        yeniKuyruk.transform.parent = transform;
        yeniKuyruk.transform.localScale = boyut;
        yeniKuyruk.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2));
        lastPosition = yeniKuyruk.transform;
        kuyrugum.Add(yeniKuyruk);
    }

    // Yem yedik�e kuyru�un b�y�mesi i�in.
    public void kuyruguBuyut(Vector3 boyut)
    {
        foreach (GameObject kuyrukx in kuyrugum)
        {
            kuyrukx.transform.localScale = boyut;
        }
    }

    // H�zl� gitti�imde kuyruk kaybetmek i�in.
    public void kuyrukYokEt()
    {
        GameObject obj = kuyrugum[kuyrugum.Count - 1];
        kuyrugum.RemoveAt(kuyrugum.Count - 1);
        Destroy(obj);
        kuyrukId--;
        lastPosition = kuyrugum[kuyrugum.Count - 1].transform;
    }

    public List<GameObject> getKuyrugum()
    {
        return kuyrugum;
    }

}
