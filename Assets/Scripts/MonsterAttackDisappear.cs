using UnityEngine;
using System.Collections;

public class MonsterAttackDisappear : MonoBehaviour {
    
	
	// Update is called once per frame
	void Update () {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacles");

        for (int j = 0; j < obstacles.Length; j++)
        {
            if (obstacles[j].transform.position.y <= -40)
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i].GetComponent<Rigidbody2D>().gravityScale = 0;
                    obstacles[i].transform.position = new Vector3(-20, -20, 0);
                    obstacles[i].SetActive(false);
                }
            }
        }
    }
}
