using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMove : CreatureBase {
    public float m_speed = 5.0f;
    public CreatureState previous;
    public float unbeat = 0.0f;
    public GameObject attachedGround; //현재 캐릭터가 붙어있는 오브젝트(바닥)
    public float jump_power = 6f;

    public bool isforward = true; //앞으로 가는지 뒤로가는지 판단하기 위한 것

    // Use this for initialization
    public override void Awake () {
        previous = CreatureState.Idle;
        base.Awake();
	}

    void Update()
    {
        CheckForward();

        if (IsDead()||state==CreatureState.BeShot) //맞는 애니메이션 도중에는 이동불가
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift)) { //가속기능
            m_speed = 10.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //가속해제
        {
            m_speed = 5.0f;
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (state == CreatureState.Idle || state == CreatureState.Run))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SetState(CreatureState.Run_Shoot);
            }
            else
            {
                SetState(CreatureState.Run);
            }
        }
        if(Input.GetKey(KeyCode.Space) && (state == CreatureState.Idle || state == CreatureState.Run||state==CreatureState.Shoot)){
            SetState(CreatureState.Shoot);
        }
        if (state != CreatureState.Jump && state != CreatureState.Double_Jump && state != CreatureState.Jump_Shoot) //점프상태가 아닐경우 일반 점프
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //rigid.AddForce(transform.up * 5.0f, ForceMode2D.Impulse);
                if (attachedGround.tag == "Ground" || attachedGround.tag == "FootHold") { 
                    SetState(CreatureState.Jump);
                }
            }
        }
        else if (state == CreatureState.Jump)
        {
            if (Input.GetKeyDown(KeyCode.W)) //점프 도중 한번 더 점프 시
            {
                SetState(CreatureState.Double_Jump);
            }
            else if (Input.GetKeyDown(KeyCode.Space)) //점프 도중 한번 더 점프 시. 이때 점프샷은 그냥 떨어지게됨.
            {
                SetState(CreatureState.Jump_Shoot);
            }
        }

        if (unbeat > 0) //무적시간
        {
            unbeat -= Time.deltaTime;
        }
        else if(unbeat<0) {
            unbeat = 0;
        }
        
    }

    public override IEnumerator Idle()
    {
        //Entry
        do
        {
            yield return null;
            //Process
            if (m_speed != 5.0f)
            {
                m_speed = 5.0f;
            }
        } while (state == CreatureState.Idle);
        //Exit
    }

    public virtual IEnumerator Run()
    {
        //Entry
        do
        {
            float h = Input.GetAxis("Horizontal");

            yield return null;
            //Process
            if (h == 0 && (state != CreatureState.Jump || state != CreatureState.Double_Jump))//바닥에 붙어있으면서 아무것도 안하면 idle로
            {
                 SetState(CreatureState.Idle);
                 break;
            }

            transform.Translate(h * m_speed * Time.deltaTime, 0, 0);

        } while (state == CreatureState.Run);
        //Exit
    }

    public virtual IEnumerator Jump()
    {
        //Entry
        rigid.AddForce(transform.up * jump_power, ForceMode2D.Impulse);
        do
        {
            float h = Input.GetAxis("Horizontal");
            yield return null;
            //Process

            transform.Translate(h * m_speed * Time.deltaTime, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space)) { //점프 도중 총 발사
                SetState(CreatureState.Jump_Shoot);
                previous = CreatureState.Jump;
            }

        } while (state == CreatureState.Jump);
        //Exit
    }
    public virtual IEnumerator Double_Jump()
    {
        //Entry
        if (Mathf.Abs(rigid.velocity.y)>0)
        { //일정한 힘으로 점프를 하게 하기 위한 작업(이미 작용하는 힘(점프 도중)이 있다면 초기화)
            Vector2 temp = rigid.velocity;
            temp.y = 0;
            rigid.velocity = temp;
        }
        rigid.AddForce(transform.up * jump_power, ForceMode2D.Impulse);
        do
        {
            float h = Input.GetAxis("Horizontal");
            yield return null;
            //Process

            transform.Translate(h * m_speed * Time.deltaTime, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            { //점프 도중 총 발사
                SetState(CreatureState.Jump_Shoot);
                previous = CreatureState.Double_Jump;
            }

        } while (state == CreatureState.Double_Jump);
        //Exit
    }


    public virtual IEnumerator Run_Shoot()
    {
        //Entry
        float exitTime_shoot = 0.18f; //스페이스바(슈팅)에 대한 상태탈출까지의 시간
        float exitTime_run = 0.18f; //이동에 대한 상태탈출까지의 시간
        do
        {
            float h = Input.GetAxis("Horizontal");
            yield return null;
            //Process

            transform.Translate(h * m_speed * Time.deltaTime, 0, 0);

            if (Input.GetKey(KeyCode.W))//평지에서 이동샷 중에 점프할 경우 점프샷으로 상태 변경
            {
                rigid.AddForce(transform.up * jump_power, ForceMode2D.Impulse); //Jump_shoot상태에는 자체적으로 점프할 힘을 가해주는 코드가 없음.
                SetState(CreatureState.Jump_Shoot);
            }
            
            if (Input.GetKey(KeyCode.Space))
            {//차지샷도 있으므로 어떤상황이건 누른 상태만 확인
                exitTime_shoot = 0.18f;
            }
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
            {//현재 이동중인지 확인
                exitTime_run = 0.18f;
            }

            exitTime_shoot -= Time.deltaTime;
            exitTime_run -= Time.deltaTime;

            if (exitTime_shoot <= 0 || exitTime_run<=0) //일정시간 이내에 슈팅이나 이동을 하지 않았으므로
            {
                Debug.Log(exitTime_run + ",  " + exitTime_shoot);
                if(exitTime_shoot<=0 && exitTime_run > 0) //달리고 있는데 슈팅을 안하는 경우
                {
                    SetState(CreatureState.Run);
                }
                else if (exitTime_shoot > 0 && exitTime_run <= 0) //안 달리는데 슈팅은 하는 경우
                {
                    SetState(CreatureState.Shoot);
                }
                else //둘다 동시에 놓은 경우(짧은 시간차도 기록이 되므로 거의 없을 거라 생각함)
                {
                    SetState(CreatureState.Idle);
                }

            }
        } while (state == CreatureState.Run_Shoot);
        //Exit
    }
    public virtual IEnumerator Shoot()
    {
        float exitTime = 0.18f;
        //Entry
        do
        {
            yield return null;

            if (Input.GetKey(KeyCode.Space)) {//차지샷도 있으므로 어떤상황이건 누른 상태만 확인
                exitTime = 0.18f;
            }
            exitTime -= Time.deltaTime;
            if (exitTime <= 0) //일정시간 이내에 누르지 않았으므로 idle상태로
            {
                SetState(CreatureState.Idle);
            }
            //Process
            
        } while (state == CreatureState.Shoot);
        //Exit
    }

    public virtual IEnumerator Jump_Shoot()
    {
        //Entry
        float exitTime = 1.2f;
        do
        {
            float h = Input.GetAxis("Horizontal");
            yield return null;
            //Process

            transform.Translate(h * m_speed * Time.deltaTime, 0, 0);


            exitTime -= Time.deltaTime;
            if (exitTime <= 0)
            {
                SetState(CreatureState.Idle);
            }
        } while (state == CreatureState.Jump_Shoot);

        //Exit
    }

    public virtual IEnumerator Dead()
    {
        //Entry
        //게임 오버
        //이어하기 든 
        do
        {
            yield return new WaitForSeconds(2.0f);
            //Process

            SceneManager.LoadScene(3);
        } while (state == CreatureState.Dead);
        //Exit
    }
    public virtual IEnumerator BeShot()
    {
        do
        {
            
            yield return new WaitForSeconds(0.3f);
            SetState(CreatureState.Idle);

        } while (state == CreatureState.Dead);
        //Exit
    }

    void OnCollisionEnter2D(Collision2D other) //바닥에 닿을시 정지모션
    {
        if ((other.gameObject.tag == "Ground" || other.gameObject.tag == "FootHold")&&(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)))
        {//땅이나 발판을 밟고 현재 이동키가 눌려져있을 경우 Idle이 아닌 Run으로 바로 이동
            SetState(CreatureState.Run);
            return;
        }
        if (other.gameObject.tag == "Ground")
        {
            SetState(CreatureState.Idle);
        }
        else if(other.gameObject.tag=="FootHold")
        {
            if (transform.position.y > other.gameObject.transform.position.y) //주인공이 발판 위에 올라서면
            {
                SetState(CreatureState.Idle);

            }
        }
    }

    void OnCollisionStay2D(Collision2D other) { //점프상태 위해서 
        if ((attachedGround == null || attachedGround.name != other.gameObject.name)&&(other.gameObject.tag=="Ground"||other.gameObject.tag=="FootHold"))
        {
            attachedGround = other.gameObject;
        }
    }

    void CheckForward()
    {
        Vector3 temp;

        if (Input.GetKey(KeyCode.A))
        {
            if (isforward == true)
            {
                temp = GetComponent<Transform>().localScale;
                temp.x *= -1;
                GetComponent<Transform>().localScale = temp;
            }
            isforward = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (isforward == false)
            {
                temp = GetComponent<Transform>().localScale;
                temp.x *= -1;
                GetComponent<Transform>().localScale = temp;
            }
            isforward = true;
        }
    }
    /*
    public void ProcessDamage(float monsterAttack)
    {
        DataManager.Instance.playerManager.currentHP -= (int)monsterAttack;

        if (DataManager.Instance.playerManager.currentHP <= 0)
        {
            DataManager.Instance.playerManager.currentHP = 0;
            SetState(CharacterState.Dead);
        }
    }
    */
}
