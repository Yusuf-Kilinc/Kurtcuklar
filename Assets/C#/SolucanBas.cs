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

        skorText.text = "SKOR " + (int)(skor * 100);                // Canvasdaki skor de�erini g�nceller
        oyunZamani += Time.deltaTime;                               // Player kuyruklar� olusana kadar hareketi durdurmak i�in.
        RotX = Input.GetAxis("Horizontal");
        RotY = Input.GetAxis("Vertical");


        // Y duzleminde bir plane olu�tur ve bu plane' e ���k yollay�p player'�m�z�n gidece�i y�n� belirle.
        //  Plane playerPlane = new Plane(Vector3.up, transform.position);
        //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //   float hitdist = 0.0f;

        //if (playerPlane.Raycast(ray, out hitdist))
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitdist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, hiz * Time.deltaTime);
        //    if (transform.position.y >= yKoordinatDuzlemi)                        // y koordinat�n� kontrol ediyoruz.
        //        transform.Translate(0f, -0.2f, 0f);
        //    if (oyunZamani > 0.5f)                                      // kuyruklar olustu harekete ba�la
        //        transform.Translate(Vector3.forward * hiz * Time.deltaTime);
        //  }

        RotX = Input.GetAxis("Horizontal");
        RotY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-RotX, 0, -RotY) * hiz * Time.deltaTime;
        Bas.transform.Translate(movement);




        // mouse sag tus bas�l� tutuldu�u s�rece player'� h�zland�r�r.
        if (Input.GetMouseButton(1))
        {
            yemKaybetmeAraligi += Time.deltaTime;                    // hizland���nda yem kaybedece�i zaman aral���n� belirler.
            kuyrukKaybet += Time.deltaTime;                          // hizland���nda kuyruk kaybedece�i zaman aral���n� belirler.

            hiz = 15f;
            PlayerKuyruk.hiz = 15f;                                  //static parametrelere di�er class'lardan eri�ilebilir. Bunun yerine method yaz�labilir.

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
        // mouse sa� tu� �ekildi�inde eski h�za d�nd�r�r.
        if (Input.GetMouseButtonUp(1))
        {
            hiz = 5f;
            PlayerKuyruk.hiz = 5f;
        }



        //// Player ba��m�z�n bir nesneye �arpma kontrol�n� ���k yollayarak yap�yoruz.
        //Ray carpismaRay = new Ray(transform.position, transform.forward);
        //RaycastHit hitted;
        ////Debug.DrawRay(transform.position, transform.forward * transform.parent.localScale.z, Color.red, 3f);
        //if (Physics.Raycast(carpismaRay, out hitted, 0.4f))
        //{
        //    //�arpt���m objenin parent'� varsa ve bu obje yem de�ilse 
        //    // a. Bu obje kendi kuyru�um olabilir
        //    // b. Di�er solucanlar�n g�z� olarak kullan�lan capsuleCollider olabilir
        //    // c. Ba�ka bir solucan�n kuyru�u yada sahne duvarlar� olabilir.


        //    if (hitted.collider.gameObject.transform.parent != null && hitted.collider.gameObject.tag != "Yem")
        //    {

        //        // a. Bu obje kendi kuyru�um olabilir
        //        if (hitted.collider.gameObject.transform.parent.parent != null && hitted.collider.gameObject.transform.parent.parent.tag == transform.parent.tag)
        //            return;

        //        //b.Di�er solucanlar�n g�z� olarak kullan�lan capsuleCollider olabilir
        //        if (hitted.collider.GetType().ToString() == "UnityEngine.CapsuleCollider")
        //            return;

        //        // c. Ba�ka bir solucan�n kuyru�u yada sahne duvarlar� olabilir. Bu durumda kuyru�umdaki her bir kuyrukX objesini yokederken yerine bir yem olu�tur.
        //        foreach (GameObject KuyrukX in kuyruk.getKuyrugum())
        //        {
        //            KuyrukX.transform.position = new Vector3(KuyrukX.transform.position.x, 6f, KuyrukX.transform.position.z);
        //            Instantiate(yem, KuyrukX.transform.position, KuyrukX.transform.rotation);
        //        }
        //        OyunYoneticisi.oyunSonu = true;                             // Oyun y�neticisine oyunun sona erdi�ini bildir.
        //        Destroy(transform.parent.gameObject);                       // Ana player objesini yok et.
        //    }

        //}


    }


    // Trigger tabanl� �arp��malar� kontrol et
    private void OnTriggerEnter(Collider collision)
    {
        // E�er capsuleCollider'a �arpt�msa a�a��daki hesaplamalara girmeden geri d�n.
        if (collision.GetType().ToString() == "UnityEngine.CapsuleCollider")
        {
            return;
        }
        else if (collision.gameObject.tag == "Yem")
        {
            float buyutmeDegeri = collision.GetComponent<Yem>().getBuyutmeDegeri();          // Yem'in skor ve buyutme de�erini al.
            //boyutSkor += buyutmeDegeri;
            uzunlukSkor += buyutmeDegeri;                                                   // Kuyruk uzunlu�unu kontrol eder.
            skor += buyutmeDegeri;                                                          // skor'u kontrol eder.
            transform.localScale += new Vector3(buyumeHiz, 0f, buyumeHiz);                  // Her yem ald���mda solucan ba��n� b�y�t.
            kuyruk.kuyruguBuyut(transform.localScale);                                      // Kuyrugu b�y�t.
            Camera.main.orthographicSize += buyumeHiz * 2 * Time.deltaTime;                                    // Solucan b�y�d��� i�in g�rme a��s�n� art�r.

            // Kuyru�un olu�ma s�kl���.
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
        //a�a��daki else blo�u kontrol ama�l� eklenebilir. 
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
