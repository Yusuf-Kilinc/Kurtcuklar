using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCharactherController : MonoBehaviour
{
    public GameObject[] Characthers;
    public int SelectCharacther = 0;



    public void NextCharacther()
    {
        Characthers[SelectCharacther].SetActive(false);
        SelectCharacther = (SelectCharacther + 1) % Characthers.Length;
        Characthers[SelectCharacther].SetActive(true);
    }

    public void PreviusCharacther()
    {
        Characthers[SelectCharacther].SetActive(false);
        SelectCharacther--;
        if (SelectCharacther < 0)
        {
            SelectCharacther += Characthers.Length;
        }
        Characthers[SelectCharacther].SetActive(true);
    }


    public void StartCharacther()
    {
        PlayerPrefs.SetInt("SelectCharacther", SelectCharacther);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
