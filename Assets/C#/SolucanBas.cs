using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolucanBas : MonoBehaviour
{
    private float hiz = 5f;
    private PlayerKuyruk kuyruk;
    private float oyunZamani = 0f;
    public GameObject yem;
    private float skor = 1f, uzunlukSkor = 0f, yemKaybetmeAraligi = 0f, kuyrukKaybet = 0f;
    private float buyumeHiz = 0.01f;
    float yKoordinatDuzlemi = 6f;
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

        skorText.text = "SKOR " + (int)(skor * 100);                // Canvasdaki skor deðerini günceller
        oyunZamani += Time.deltaTime;                               // Player kuyruklarý olusana kadar hareketi durdurmak için.
        RotX = Input.GetAxis("Horizontal");
        RotY = Input.GetAxis("Vertical");


        // Y duzleminde bir plane oluþtur ve bu plane' e ýþýk yollayýp player'ýmýzýn gideceði yönü belirle.
        //  Plane playerPlane = new Plane(Vector3.up, transform.position);
        //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //   float hitdist = 0.0f;

        //if (playerPlane.Raycast(ray, out hitdist))
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitdist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, hiz * Time.deltaTime);
        //    if (transform.position.y >= yKoordinatDuzlemi)                        // y koordinatýný kontrol ediyoruz.
        //        transform.Translate(0f, -0.2f, 0f);
        //    if (oyunZamani > 0.5f)                                      // kuyruklar olustu harekete baþla
        //        transform.Translate(Vector3.forward * hiz * Time.deltaTime);
        //  }

        RotX = Input.GetAxis("Horizontal");
        RotY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-RotX, 0, -RotY) * hiz * Time.deltaTime;
        Bas.transform.Translate(movement);




        // mouse sag tus basýlý tutulduðu sürece player'ý hýzlandýrýr.
        if (Input.GetMouseButton(1))
        {
            yemKaybetmeAraligi += Time.deltaTime;                    // hizlandýðýnda yem kaybedeceði zaman aralýðýný belirler.
            kuyrukKaybet += Time.deltaTime;                          // hizlandýðýnda kuyruk kaybedeceði zaman aralýðýný belirler.

            hiz = 15f;
            PlayerKuyruk.hiz = 15f;                                  //static parametrelere diðer class'lardan eriþilebilir. Bunun yerine method yazýlabilir.

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
        if (Input.GetMouseButtonUp(1))
        {
            hiz = 5f;
            PlayerKuyruk.hiz = 5f;
        }



        //// Player baþýmýzýn bir nesneye çarpma kontrolünü ýþýk yollayarak yapýyoruz.
        //Ray carpismaRay = new Ray(transform.position, transform.forward);
        //RaycastHit hitted;
        ////Debug.DrawRay(transform.position, transform.forward * transform.parent.localScale.z, Color.red, 3f);
        //if (Physics.Raycast(carpismaRay, out hitted, 0.4f))
        //{
        //    //çarptýðým objenin parent'ý varsa ve bu obje yem deðilse 
        //    // a. Bu obje kendi kuyruðum olabilir
        //    // b. Diðer solucanlarýn gözü olarak kullanýlan capsuleCollider olabilir
        //    // c. Baþka bir solucanýn kuyruðu yada sahne duvarlarý olabilir.


        //    if (hitted.collider.gameObject.transform.parent != null && hitted.collider.gameObject.tag != "Yem")
        //    {

        //        // a. Bu obje kendi kuyruðum olabilir
        //        if (hitted.collider.gameObject.transform.parent.parent != null && hitted.collider.gameObject.transform.parent.parent.tag == transform.parent.tag)
        //            return;

        //        //b.Diðer solucanlarýn gözü olarak kullanýlan capsuleCollider olabilir
        //        if (hitted.collider.GetType().ToString() == "UnityEngine.CapsuleCollider")
        //            return;

        //        // c. Baþka bir solucanýn kuyruðu yada sahne duvarlarý olabilir. Bu durumda kuyruðumdaki her bir kuyrukX objesini yokederken yerine bir yem oluþtur.
        //        foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
        //        {
        //            KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x, 6f, KuyrukX.transform.position.z);
        //            Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
        //        }
        //        OyunYoneticisi.oyunSonu = true;                             // Oyun yöneticisine oyunun sona erdiðini bildir.
        //        Destroy(transform.parent.gameObject);                       // Ana player objesini yok et.
        //    }

        //}


    }


    // Trigger tabanlý çarpýþmalarý kontrol et
    private void OnTriggerEnter(Collider collision)
    {
        // Eðer capsuleCollider'a çarptýmsa aþaðýdaki hesaplamalara girmeden geri dön.
        if (collision.GetType().ToString() == "UnityEngine.CapsuleCollider")
        {
            return;
        }
        else if (collision.gameObject.tag == "Yem")
        {
            float buyutmeDegeri = collision.GetComponent<Yem>().getBuyutmeDegeri();          // Yem'in skor ve buyutme deðerini al.
            //boyutSkor += buyutmeDegeri;
            uzunlukSkor += buyutmeDegeri;                                                   // Kuyruk uzunluðunu kontrol eder.
            skor += buyutmeDegeri;                                                          // skor'u kontrol eder.
            transform.localScale += new Vector3(buyumeHiz, 0f, buyumeHiz);                  // Her yem aldýðýmda solucan baþýný büyüt.
            kuyruk.kuyruguBuyut(transform.localScale);                                      // Kuyrugu büyüt.
            Camera.main.orthographicSize += buyumeHiz * 2 * Time.deltaTime;                                    // Solucan büyüdüðü için görme açýsýný artýr.

            // Kuyruðun oluþma sýklýðý.
            if (uzunlukSkor * 10 > 2)
            {
                //Debug.Log("uzunSkor "+uzunlukSkor*10f);
                int index = (int)(uzunlukSkor % 2f);
                uzunlukSkor = 0f;
                for (int i = 0; i < index; i++)
                    kuyruk.kuyrukEkle(transform.localScale);
            }
            Destroy(collision.gameObject);  // yem'i yok et.
        }
        //aþaðýdaki else bloðu kontrol amaçlý eklenebilir. 
        /*else
        {
            foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
            {
                KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x,yKoordinatDuzlemi, KuyrukX.transform.position.z);
                Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
            }
            Destroy(transform.parent.gameObject);
        }*/
    }
}
