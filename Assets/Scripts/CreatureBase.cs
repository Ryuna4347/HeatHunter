using UnityEngine;
using System.Collections;

/*
 * 몬스터 및 플레이어 공통 기능
 */
public class CreatureBase : MonoBehaviour {
    public Animator a;
    public Rigidbody2D rigid;

    public CreatureState state = CreatureState.Idle;

    public virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        a = GetComponentInChildren<Animator>();
    }

    public virtual void OnEnable()
    {
        StartCoroutine("FSMMain");
    }

    IEnumerator FSMMain()
    {
        while (true)
        {
            //Debug.Log(state.ToString());
            yield return StartCoroutine(state.ToString());
        }
    }

    public virtual IEnumerator Idle()
    {
        //Entry
        do
        {
            yield return null;
            //Process

        } while (state == CreatureState.Idle);
        //Exit
    }

    public void SetState(CreatureState newState)
    {
        state = newState;
        a.SetInteger("state", (int)state);
    }
    public int GetState() {
        return a.GetInteger("state");
    }

    public bool IsDead()
    {
        return (GameObject.FindGameObjectWithTag("DataManager").GetComponent<PlayerInfo>().health==0);
    }
}
