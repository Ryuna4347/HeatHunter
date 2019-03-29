using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {
    GameObject[] obstacles;

	// Use this for initialization
	void Awake() {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                Debug.Log("i");
                obstacles[i].SetActive(true);
                obstacles[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                obstacles[i].transform.position = new Vector3(4 + i, -4, 0);
            }
            for (int i = 0; i < obstacles.Length; i++)
            {
                Vector2 power = new Vector2(Random.Range(-6, -1), Random.Range(6, 16));
                obstacles[i].GetComponent<Rigidbody2D>().AddForce(power, ForceMode2D.Impulse);
                Debug.Log(power);
            }
        }
    }
}
