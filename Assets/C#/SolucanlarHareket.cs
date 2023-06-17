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
    Quaternion targetRotation;
    Vector3 targetPoint;

    bool yemGordum = false;
    float yemAra = 0.5f, timerYem = 0f;
    float skor = 1f, uzunlukSkor = 0f;
    float buyumeHiz = 0.01f;
    CarpismaKontrol carpismaKontrol;
    float yKoordinatDuzlemi = 6f;


    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        dusunmeAraligi = 3f;//Random.Range(2, 6);
        kuyruk = solucanKuyruk.GetComponent<SolucanlarKuyruk>();
        carpismaKontrol = transform.Find("CarpismaKontrol").GetComponent<CarpismaKontrol>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (baslangicHazirlik < 2f) ;
        baslangicHazirlik += Time.deltaTime;
        timerYem += Time.deltaTime;


        if (carpismaKontrol.menzilBilgisiDon())
        {
            //raySayisi++;
            // Debug.Log("Ray Sayisi" + raySayisi);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hitted;
            Debug.DrawRay(transform.position, transform.forward * transform.parent.localScale.z, Color.red, 3f);
            if (Physics.Raycast(ray, out hitted, transform.parent.localScale.z))
            {
                if (hitted.collider.gameObject.transform.parent != null && hitted.collider.gameObject.tag != "Yem")
                {
                    //Debug.Log(hitted.collider.gameObject.transform.parent.tag);
                    if (hitted.collider.gameObject.transform.parent.parent != null && hitted.collider.gameObject.transform.parent.parent == transform.parent)
                        goto devamEt;
                    if (hitted.collider.GetType().ToString() == "UnityEngine.CapsuleCollider")
                        goto devamEt;
                    //Debug.Log(hitted.collider.gameObject.transform.parent.parent.tag);
                    foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
                    {
                        KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x, yKoordinatDuzlemi, KuyrukX.transform.position.z);
                        Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
                    }
                    Debug.Log("Solucan Bas carptý yok oluyor");
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    devamEt:
        if (!yemGordum)
        {
            if (timer > dusunmeAraligi)
            {

                float x = Random.Range(3, 7);
                float z = Random.Range(5, 10);

                if (x - z > 0)
                {
                    direction.x *= -1f;
                }

                timer = 0f;

                targetPoint = new Vector3(Random.Range(-50, 50f) + transform.position.x, yKoordinatDuzlemi, Random.Range(-50, 50f) + transform.position.z);

            }
            transform.position = new Vector3(transform.position.x, yKoordinatDuzlemi, transform.position.z);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, hiz * Time.deltaTime);
        }
        //direction.y = 0f;
        if (baslangicHazirlik > 1f)
            transform.Translate(direction * Time.deltaTime * hiz);

    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.transform);
        if (other.gameObject.tag == "Zemin")
            return;


        foreach (Collider colliderX in gameObject.GetComponents<Collider>())
        {

            if (colliderX.GetType().ToString() == "UnityEngine.CapsuleCollider")
            {

                if (other.GetType().ToString() == "UnityEngine.BoxCollider" && other.gameObject.tag != "Yem")
                {
                    Vector3 dist = transform.position - other.transform.position;
                    dist.y = 0f;
                    Quaternion targetRotationX = Quaternion.LookRotation(dist);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationX, hiz * 2f * Time.deltaTime);
                }
                else if (other.gameObject.tag == "Player")
                {

                    Vector3 dist = transform.position - other.transform.position;
                    dist.y = 0f;
                    Quaternion targetRotationX = Quaternion.LookRotation(dist);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationX, hiz * 2f * Time.deltaTime);

                }
                else if (other.gameObject.transform.parent != null && other.gameObject.tag != "Yem")
                {
                    //Debug.Log(hitted.collider.gameObject.transform.parent.tag);
                    if (other.gameObject.transform.parent.parent != null && other.gameObject.transform.parent.parent.tag == transform.parent.tag)
                        return;
                    if (other.GetType().ToString() == "UnityEngine.CapsuleCollider")
                        return;
                }
                else if (other.gameObject.tag == "Yem")
                {
                    if (timerYem > yemAra)
                    {
                        yemGordum = true;
                        timerYem = 0f;
                    }
                    Vector3 dist = transform.position - other.transform.position;
                    dist.y = 0f;
                    Quaternion targetRotationX = Quaternion.LookRotation(-dist);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationX, hiz * 2f * Time.deltaTime);

                }


            }

            if (colliderX.GetType().ToString() == "UnityEngine.BoxCollider")
            {
                if (other.gameObject.tag == "Zemin")
                    return;
                if (other.GetType().ToString() == "UnityEngine.CapsuleCollider")
                    return;

                if (other.GetType().ToString() == "UnityEngine.BoxCollider" && other.gameObject.tag != "Yem")
                {
                    if (other.gameObject.transform.parent != null)
                    {
                        if (other.gameObject.transform.parent.parent != null && other.gameObject.transform.parent.parent.tag == transform.parent.tag)
                            return;
                        if (other.GetType().ToString() == "UnityEngine.CapsuleCollider")
                            return;
                        Debug.Log(gameObject.name + "=>" + other.gameObject.name);
                        if (Vector3.Distance(transform.position, other.gameObject.transform.position) < 1f)
                        {
                            solucanYokEt();
                        }
                    }
                }
                else if (other.gameObject.tag == "Yem")
                {

                    if (Vector3.Distance(transform.position, other.gameObject.transform.position) < 1f)
                    {
                        yemGordum = false;
                        float buyutmeDegeri = other.gameObject.GetComponent<Yem>().getBuyutmeDegeri();
                        //boyutSkor += buyutmeDegeri;
                        uzunlukSkor += buyutmeDegeri;
                        skor += buyutmeDegeri;
                        //transform.parent.localScale += new Vector3(buyumeHiz, 0f,buyumeHiz);
                        transform.localScale += new Vector3(buyumeHiz, 0f, buyumeHiz);
                        //transform.parent.Find("Kuyruk").localScale += new Vector3(buyumeHiz, 0f, buyumeHiz); 
                        kuyruk.kuyruguBuyut(transform.localScale);
                        //Debug.Log(skor +",uzunluk/2:"+uzunlukSkor+",boyut/3:"+ boyutSkor);

                        if (uzunlukSkor * 10 > 2)
                        {
                            //Debug.Log("uzunSkor "+uzunlukSkor*10f);
                            int index = (int)(uzunlukSkor % 2f);
                            uzunlukSkor = 0f;
                            for (int i = 0; i < index; i++)
                                kuyruk.kuyrukEkle(transform.localScale);
                        }
                        carpismaKontrol.menzilBilgisiGir(false);
                        Destroy(other.gameObject.transform.parent.parent.gameObject);
                    }
                }

            }
        }
    }

    public void solucanYokEt()
    {
        foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
        {
            KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x, yKoordinatDuzlemi, KuyrukX.transform.position.z);
            Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
        }
        //Debug.Log("Solucan Bas carptý yok oluyor");
        Destroy(transform.parent.gameObject);
    }


}
