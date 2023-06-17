using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yem : MonoBehaviour
{

    float buyutmeDegeri = 1.0f;
    public Transform merkezTrans;       // Yem'lerin etrafýnda döneceði position deðerini tutar.
    Material yemMat;
    public Light lightYem;

    // Start is called before the first frame update
    void Start()
    {
        Color color = Color.HSVToRGB(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));       // Yem için rastgele bir renk üret.

        //lightYem = gameObject.GetComponent<Light>();
        if (lightYem != null)
            lightYem.color = color;

        yemMat = gameObject.GetComponent<Renderer>().material;
        yemMat.color = color;

        float boyutDegeri = Random.Range(0.2f, 0.6f);
        transform.localScale = new Vector3(boyutDegeri, 0.2f, boyutDegeri);
        buyutmeDegeri += boyutDegeri / 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (merkezTrans != null)
            merkezTrans.Rotate(0f, 60f * Time.deltaTime, 0f);

    }
    public float getBuyutmeDegeri()
    {
        return buyutmeDegeri;
    }

    public void setRotation(Transform rotateTrans)
    {
        merkezTrans = rotateTrans;
        transform.parent = rotateTrans;
    }
}
