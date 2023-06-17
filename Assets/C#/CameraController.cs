using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        if (Player != null)
            offset = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
            transform.position = Player.transform.position + offset;
    }
}
