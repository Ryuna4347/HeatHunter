using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MonsterMove : MonsterBase {

    float idleElapTime = 0;
    float idelMaxTime = 4.0f;
    GameObject[] attackCollider;

    public override void Awake()
    {
        attackCollider = GameObject.FindGameObjectsWithTag("MonAttack");
        for(int i = 0; i<attackCollider.Length; i++)
        {
            attackCollider[i].SetActive(false);
        }
        base.Awake();
       // StartCoroutine("UpdateAnimationLoop");
    }

    public float restTime=20.0f;

    //IEnumerator UpdateAnimationLoop()
    //{
    //}

    override public void SetMonState(MonsterState newState)
    {
        base.SetMonState(newState);
        if(newState == MonsterState.Idle)
        {
            idleElapTime = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsDead() || monstate != MonsterState.Idle)
        {
            return;
        }

        if (monstate == MonsterState.Idle)
        {
            idleElapTime += Time.deltaTime;
            if(idleElapTime < idelMaxTime)
            {
                return;
            }
        }
        int random = Random.Range(0, 3);
        switch (random) {
            case 0:
                SetMonState(MonsterState.DownFake);
                break;
            case 1:
                SetMonState(MonsterState.MidFake);
                break;
            case 2:
                SetMonState(MonsterState.UpFake);
                break;
        }
	}
    

    public virtual IEnumerator DownFake()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process
            yield return new WaitForSeconds(2.0f);
            SetMonState(MonsterState.AttackWait);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.DownAttack);

        } while (monstate == MonsterState.DownFake);
        //Exit
    }

    public virtual IEnumerator DownAttack()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process
            
            yield return new WaitForSeconds(0.2f);
            attackCollider[1].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.AttackWait);
            attackCollider[1].SetActive(false);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.Idle);
        } while (monstate == MonsterState.DownAttack);
        //Exit
    }

    public virtual IEnumerator MidFake()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process

            yield return new WaitForSeconds(2.0f);
            SetMonState(MonsterState.AttackWait);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.MidAttack);

        } while (monstate == MonsterState.MidFake);
        //Exit
    }

    public virtual IEnumerator MidAttack()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process

            yield return new WaitForSeconds(0.2f);
            attackCollider[2].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            attackCollider[2].SetActive(false);
            SetMonState(MonsterState.AttackWait);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.Idle);
        } while (monstate == MonsterState.MidAttack);
        //Exit
    }

    public virtual IEnumerator UpFake()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process

            yield return new WaitForSeconds(2.0f);
            SetMonState(MonsterState.AttackWait);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.UpAttack);

        } while (monstate == MonsterState.UpFake);
        //Exit
    }

    public virtual IEnumerator UpAttack()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return null;
            //Process
            yield return new WaitForSeconds(0.2f);
            attackCollider[0].SetActive(true);
            yield return new WaitForSeconds(0.8f);
            attackCollider[0].SetActive(false);
            SetMonState(MonsterState.AttackWait);
            yield return new WaitForSeconds(0.8f);
            SetMonState(MonsterState.Idle);

        } while (monstate == MonsterState.UpAttack);
        //Exit
    }

    public virtual IEnumerator Dead()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            //Process
            gameObject.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene(2);
        } while (monstate == MonsterState.Dead);
        //Exit
    }
}
