using UnityEngine;
using System.Collections;

public class ProcessDamage : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMove player=GameObject.Find("Player").GetComponent<PlayerMove>();
            if (player.unbeat == 0)
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().SetState(CreatureState.BeShot);
                GameObject.Find("GameManager").GetComponent<PlayerInfo>().ReduceHP();
                player.unbeat = 10.0f;
            }
        }

    }
}
