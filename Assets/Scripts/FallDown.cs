using UnityEngine;
using System.Collections;

public class FallDown : MonoBehaviour {
    float width;
    PlayerInfo pi;
    Transform cam;
    CameraMove cm;

    void Awake() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        width = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>().width;
        pi = GameObject.FindGameObjectWithTag("DataManager").GetComponent<PlayerInfo>();
        cm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMove>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Transform collide;

        if (other.gameObject.tag == "Player") //플레이어 낙사(라이프 -1 처리)
        {
            pi.ReduceHP();
            collide=CheckRevivalPosition();
            other.gameObject.transform.position = new Vector3(collide.position.x + (collide.localScale.x/2 - 0.5f), collide.position.y + 1.0f, 0); //가장 왼쪽에 위치한 발판이나 땅 위에 착지
        }
    }

    Transform CheckRevivalPosition() //카메라 안에 보이는 발판이나 땅 중에서 가장 왼쪽에 있는 오브젝트 위치 반환
    {
        GameObject[] gr = cm.ObjectinSight("Ground");
        GameObject[] fh = cm.ObjectinSight("FootHold");

        if (gr[0].transform.position.x > fh[0].transform.position.x)
        {
            return fh[0].transform;
        }
        else
        {
            return gr[0].transform;
        }
    }
}
