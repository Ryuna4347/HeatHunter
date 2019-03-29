using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    public float width; //화면 맨 오른쪽 위치
    public float screenwidth; //스크린 길이
    public float height;
    Transform player;

	// Use this for initialization
	void Awake() {
        height = 2*Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        screenwidth = width;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.position.x >= width / 2)
        {
            GetComponent<Transform>().position = new Vector3(player.position.x,5,-10);
            width += player.position.x - (width / 2); //플레이어가 중앙을 넘은만큼 기준선을 옮김.(뒤로 움직이는걸 방지)
        }
	}

    public GameObject[] ObjectinSight(string name) //카메라의 시야 내에 name의 태그를 달고있는 오브젝트가 있으면 반환
    {
        GameObject[] findObject = GameObject.FindGameObjectsWithTag(name); //name에 해당하는 태그를 가진 모든 active상태의 오브젝트 검색
        GameObject[] returnObject = new GameObject[findObject.Length]; //최대 findObject에서 찾은 오브젝트의 갯수만큼이므로 미리 초기화
        float minpos = width - screenwidth / 2;
        float maxpos = width + screenwidth / 2;
        int j = 0;

        for (int i = 0; i < findObject.Length; i++)
        {
            if ((findObject[i].transform.position.x + findObject[i].transform.localScale.x > minpos) ||
                findObject[i].transform.position.x - findObject[i].transform.localScale.x < maxpos)
            { //물체 중에 어느 일부분이라도 카메라 범위 안에 들어간다면(localscale을 더하거나 빼서 각 오브젝트의 양쪽 끝 좌표를 얻는 방식)
                returnObject[j++] = findObject[i];
            }
        }

        for (int i = 0; i < returnObject.Length; i++)
        {
            Debug.Log(returnObject[i]);
        }

        //오브젝트끼리 섞여서 불러와지므로 x위치에 따라 정렬
        for (int i = 0; i < j; i++)
        {
            for (int k = 1; k < j; k++)
            {
                if (returnObject[i].transform.position.x > returnObject[k].transform.position.x)
                {
                    GameObject temp = returnObject[i];
                    returnObject[i] = returnObject[k];
                    returnObject[k] = temp;
                }
            }
        }

        for (int i = 0; i < returnObject.Length; i++)
        {
            Debug.Log( returnObject[i]);
        }

        return returnObject; //수정해야함
    }
}
