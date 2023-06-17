using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolucanlarKuyruk : MonoBehaviour
{
    public GameObject basTrans;
    public GameObject kuyrukX;
    public Transform lastPosition;
    List<GameObject> kuyrugum;
    int kuyrukId = 0;
    public static float hiz = 5f;
    float kuyrukYukseklikFarki = 0.1f;

    Vector3 pos, targetPos;
    GameObject yeniKuyruk;

    // Start is called before the first frame update
    void Start()
    {
        kuyrugum = new List<GameObject>();
        kuyrugum.Add(new GameObject());
        lastPosition = basTrans.transform;

        kuyrukId = 1;
        pos = new Vector3(0f, 0.01f, 0.4f);
        targetPos = basTrans.transform.position - pos;
        yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
        yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
        yeniKuyruk.transform.parent = transform;
        lastPosition = yeniKuyruk.transform;
        kuyrugum.Add(yeniKuyruk);

        kuyrukId++;
        pos = new Vector3(0f, 0.02f, 0.4f);
        targetPos = lastPosition.position - pos;
        yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
        yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
        yeniKuyruk.transform.parent = transform;
        lastPosition = yeniKuyruk.transform;
        kuyrugum.Add(yeniKuyruk);

        kuyrukId++;
        pos = new Vector3(0f, 0.03f, 0.4f);
        targetPos = lastPosition.position - pos;
        yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
        yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
        yeniKuyruk.transform.parent = transform;
        lastPosition = yeniKuyruk.transform;
        kuyrugum.Add(yeniKuyruk);

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 1; i <= kuyrukId; i++)
        {

            if (i == 1)
            {
                Vector3 targetPosX = basTrans.transform.position - new Vector3(0f, kuyrukYukseklikFarki, 0f);

                // Determine the target rotation.  This is the rotation if the transform looks at the target point.
                Quaternion targetRotationX = Quaternion.LookRotation(targetPosX - kuyrugum[1].transform.position);

                // Smoothly rotate towards the target point.
                kuyrugum[1].transform.rotation = Quaternion.Slerp(kuyrugum[1].transform.rotation, targetRotationX, hiz * 3 * Time.deltaTime);

                //if (Vector3.Distance(targetPosX, kuyrugum[1].transform.position)>0.4f)
                kuyrugum[1].transform.Translate(Vector3.forward * hiz * Time.deltaTime);


            }
            else
            {
                Vector3 targetPos;
                if (i < 10)        // ilk 10 kuyruk yere yakýn olsunki baþ kuyruðun üstünde görünsün.
                    targetPos = kuyrugum[i - 1].transform.position - new Vector3(0f, kuyrukYukseklikFarki, 0f);
                else if (kuyrugum[i - 1].transform.position.y > 0.5f)  // 10 dan sonrakiler 0.01f daha alcakta. 
                    targetPos = kuyrugum[i - 1].transform.position - new Vector3(0f, kuyrukYukseklikFarki / 4, 0f);
                else
                    targetPos = kuyrugum[i - 1].transform.position; // zeminin altýna gitmemesi için son gelenler sabit.

                // Determine the target rotation.  This is the rotation if the transform looks at the target point.
                Quaternion targetRotation = Quaternion.LookRotation(targetPos - kuyrugum[i].transform.position);

                // Smoothly rotate towards the target point
                kuyrugum[i].transform.rotation = Quaternion.Slerp(kuyrugum[i].transform.rotation, targetRotation, hiz * 3 * Time.deltaTime);


                // if (Vector3.Distance(targetPos, kuyrugum[i].transform.position) > 0.4f)
                kuyrugum[i].transform.Translate(Vector3.forward * hiz * Time.deltaTime);
            }
        }
    }

    public void kuyrukEkle(Vector3 boyut)
    {
        kuyrukId++;
        pos = new Vector3(0f, 0f, 0.4f);
        targetPos = lastPosition.position - pos;
        yeniKuyruk = Instantiate(kuyrukX, targetPos, Quaternion.identity);
        yeniKuyruk.GetComponent<kuyrukX>().setKuyrukId(kuyrukId);
        yeniKuyruk.transform.parent = transform;
        yeniKuyruk.transform.localScale = boyut;
        lastPosition = yeniKuyruk.transform;
        kuyrugum.Add(yeniKuyruk);
    }

    public List<GameObject> getKuyrugum()
    {
        return kuyrugum;
    }
    public void kuyruguBuyut(Vector3 boyut)
    {
        foreach (GameObject kuyrukx in kuyrugum)
        {
            kuyrukx.transform.localScale = boyut;
        }
    }

}
