using UnityEngine;
using System.Collections;

public class StatusVeiwer : MonoBehaviour
{
     public void LifeView()
    {
        GameObject[]Life =  GameObject.FindGameObjectsWithTag("RedHeart");
        for(int i =0; i< Life.Length; i++)
        {
            Debug.Log(Life[i].name);
            Life[i].SetActive(false);
        }
        GameObject red_pos = GameObject.FindGameObjectWithTag("T_Red");
        GameObject gray_pos = GameObject.FindGameObjectWithTag("T_Gray");
        Debug.Log(red_pos.name + " " + gray_pos);
        red_pos.transform.position = gray_pos.transform.position;
    }

    void Start()
    {
        LifeView();
    }
}
