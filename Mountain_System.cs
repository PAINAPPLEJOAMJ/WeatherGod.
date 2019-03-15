using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain_System : FSM_Base
{

    public enum bState
    {
        Build1,
        Build2,
        Build3,
        Build4,
        Build5,
    }
    public bState currentState;

    // 시민 생성 포인트
    public Transform[] points;

    // 발전도
    public GameObject[] build;

    // 시민 종류
    public GameObject[] person;
    Transform tr_Pos;


    // 도둑
    public float pCnt = 0;
    public float fCnt = 0;
    public float aCnt = 0;
    public float maxP = 420;
    int rIdx, pIdx; // rIdx = respawnIdx, pIdx = personIdx

    protected override string GetMethodNameFromCurrentState()
    {
        return "State_" + currentState.ToString();
    }

    public bState GetCurrentState() { return currentState; }

    void Start()
    {
        points = GameObject.Find("Mountain_PointGroup").GetComponentsInChildren<Transform>();
        tr_Pos = this.transform;
        StartCoroutine(State_Build1());
        StartCoroutine(countPerson());
        StartCoroutine(createPerson());
    }

    void Update()
    {

    }

    public void ChangeState(bState state)
    {
        if (currentState != state)
        {
            currentState = state;
            NextState(); // 이거 어디서 쓰는거지
        }
    }

    IEnumerator countPerson () 
    {
        while (true) 
        {
            yield return new WaitForSeconds(1.0f);

            pCnt += 1;
            fCnt += 3;

            if (pCnt == maxP) 
            {
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator createPerson () 
    {
        while (true) 
        {
            yield return new WaitForSeconds(3.0f);

            if (pCnt < maxP) 
            {

                rIdx = Random.Range(1, points.Length);
                pIdx = Random.Range(0, person.Length);

                var M_Person = Instantiate(person[pIdx],
                                              new Vector3(points[rIdx].position.x + Random.Range(-1.0f, 1.0f),
                                                          points[rIdx].position.y,
                                                          points[rIdx].position.z + Random.Range(-1.0f, 1.0f)),
                                              Quaternion.identity);

                M_Person.transform.parent = gameObject.transform;
            } 
            else if (pCnt == maxP) 
            {
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator State_Build1 ()
    {
        yield return null;

        while (currentState == bState.Build1)
        {
            if (pCnt >= 80 && pCnt < 160)
            {
                ChangeState(bState.Build2);

            }
            yield return null;

        }
        yield return null;
    }

    IEnumerator State_Build2()
    {
        SoundMgr.inst.Play("Build_Up", tr_Pos.position);
        build[1].SetActive(true);

        yield return null;

        while (currentState == bState.Build2)
        {
            if (pCnt >= 160 && pCnt < 240)
            {
                ChangeState(bState.Build3);
            }
            yield return null;

        }
        yield return null;
    }

    IEnumerator State_Build3()
    {
        SoundMgr.inst.Play("Build_Up", tr_Pos.position);
        build[2].SetActive(true);

        yield return null;

        while (currentState == bState.Build3)
        {
            if (pCnt >= 240 && pCnt < 320)
            {
                ChangeState(bState.Build4);
            }
            yield return null;

        }
        yield return null;
    }

    IEnumerator State_Build4()
    {
        SoundMgr.inst.Play("Build_Up", tr_Pos.position);
        build[3].SetActive(true);

        yield return null;

        while (currentState == bState.Build4)
        {
            if (pCnt >= 320 && pCnt < 400)
            {
                ChangeState(bState.Build5);
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator State_Build5()
    {
        SoundMgr.inst.Play("Build_Up", tr_Pos.position);
        build[4].SetActive(true);

        yield return null;
    }
}