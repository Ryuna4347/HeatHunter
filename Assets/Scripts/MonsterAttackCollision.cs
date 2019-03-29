using UnityEngine;
using System.Collections;

public class MonsterAttackCollision : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.transform.position = new Vector3(-40, 40, 0); //캐릭터에 닿으면 없어지게 만듬. 삭제하면 안됨.
            if (!(other.gameObject.GetComponent<PlayerMove>().unbeat==0)) //무적상태가 아니라면 상처입음.
            {
                other.gameObject.GetComponent<PlayerMove>().SetState(CreatureState.BeShot);
            }
        }
    }
}
