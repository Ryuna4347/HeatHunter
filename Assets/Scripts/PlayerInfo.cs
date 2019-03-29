using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour
{
    public int health;
    public int burserk;
    GameObject[] Life;

    // Use this for initialization
    void Awake()
    {
        health = 5;
        burserk = 0;

        Life = GameObject.FindGameObjectsWithTag("RedHeart");
        for (int i = 0; i < Life.Length; i++)
        {
            Life[i].SetActive(false);
        }
        Life[4].transform.localPosition = new Vector3(0, 0, 0);
        Life[3].transform.localPosition = new Vector3(135, 0, 0);
        Life[2].transform.localPosition = new Vector3(270, 0, 0);
        Life[1].transform.localPosition = new Vector3(404, 0, 0);
        Life[0].transform.localPosition = new Vector3(531, 0, 0);




        GameObject red_pos = GameObject.FindGameObjectWithTag("T_Red");
        GameObject gray_pos = GameObject.FindGameObjectWithTag("T_Gray");
        red_pos.GetComponent<RectTransform>().localPosition = gray_pos.GetComponent<RectTransform>().localPosition;

        ShowHP();
    }

    public void ShowHP()
    {

        for (int i = 0; i < 5; i++)
        {
            Life[i].SetActive(false);
        }
        for (int i = 0; i < health; i++)
        {
            Life[i].SetActive(true);
        }
    }

    public void ReduceHP()
    {
        if (health <= 0) { return; }
        Life[5 - health].SetActive(false);
        health--;
        ShowHP();
        if (health == 0)
        {
            GameObject.Find("Player").GetComponent<PlayerMove>().SetState(CreatureState.Dead);
            return;
        }
    }

}