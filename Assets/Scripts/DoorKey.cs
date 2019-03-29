using UnityEngine;
using System.Collections;

public class DoorKey : MonoBehaviour {
    public Transform tr;
    public float width;
    public float y;
    CameraMove cm;

	// Use this for initialization
	void Awake () {
        tr = GetComponent<Transform>();
        cm=GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
        width = cm.width;
        y = tr.position.y - 0.125f;
	}
	
	// Update is called once per frame
	void Update () {
        tr.position = new Vector3(tr.position.x,y+Mathf.PingPong(Time.time/2,0.25f), 0);
	}
    void OnTriggerEnter2D(Collider2D other)
    { //열쇠 충돌시 바로 옆에 있는 문을 열어버림
        if (other.CompareTag("Player"))
        {
            GameObject nextDoor=FindNextDoor();

            nextDoor.SetActive(false);

            gameObject.SetActive(false);
        }
    }
    GameObject FindNextDoor() {
        GameObject[] doorArray = cm.ObjectinSight("Door");
        float playerpos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x;
        
        //혹시 문을 불러오는 순서가 뒤죽박죽이면 이것도 수정해야함.

        for(int i = 0; i < doorArray.Length; i++)
        {
            if (doorArray[i].transform.position.x > playerpos)
            { //문이 순서대로 불러와진다면 playerpos보다 큰 첫번째 문이 바로 오른쪽에 있는 문일 것.
                return doorArray[i];
            }
        }
        return null;
    }
}
