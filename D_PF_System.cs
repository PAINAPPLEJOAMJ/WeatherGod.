using System.Collections;
using UnityEngine;

public class D_PF_System : MonoBehaviour
{

    // 날씨에 따라 인구 식량 값 변동
    // 날씨는 10초동안 적용
    // 다른 상태머신의 변동을 받아 다시 인구 식량의 변동을 바꿔준다.

    public StateCtrl.eState currentState;
    public float A; // Achieve     // 1680max
    public float P; // Population  // 420max
    public float F; // Food        // 1260max
    public float A_2; 
    public float P_2; 
    public float F_2; 

    public float aTime;
    public Desert_System village_System;

    void Start()
    {
        StartCoroutine(Timeupdate());
        StartCoroutine(PF_Controller());
    }

    void Update()
    {

        currentState = this.gameObject.GetComponent<StateCtrl>().currentState;

        aTime += Time.time;


    }
    IEnumerator Timeupdate()
    {
        while (true)
        {
            village_System.pCnt += P_2;

            P = village_System.pCnt;

            village_System.fCnt += F_2;

            F = village_System.fCnt;
            A = P + F;

            yield return new WaitForSeconds(1.0f);
        }
    }
    //break를 없애야 하나
    IEnumerator PF_Controller()
    {

        while (true)
        {

            if (currentState == StateCtrl.eState.None)
            {

                currentState = StateCtrl.eState.Clear;
            }

            switch (currentState)
            {
                case StateCtrl.eState.Clear:
                    P_2 = 0 * 3.0f;
                    F_2 = 0 * 3.0f;
                    break;

                case StateCtrl.eState.Heat:
                    P_2 = 0 * 3.0f;
                    F_2 = 0 * 3.0f;
                    break;

                case StateCtrl.eState.Cloud:
                    //     Debug.Log("inter2");
                    P_2 = 0 * 1.0f;
                    F_2 = 0 * 3.0f;
                    break;

                case StateCtrl.eState.Rain:
                    //      Debug.Log("inter3");
                    P_2 = 0 * 3.0f;
                    F_2 = 0 * 3.0f;
                    break;

                case StateCtrl.eState.Snow:
                    P_2 = 0 * 3.0f;
                    F_2 = 0 * 3.0f;
                    break;

                case StateCtrl.eState.Drought:
                    P_2 = -1 * 1.0f;
                    F_2 = -3 * 3.0f;
                    break;

                case StateCtrl.eState.HeavyRain:
                    //   Debug.Log("inter4");
                    P_2 = -1 * 1.0f;
                    F_2 = -2 * 3.0f;
                    break;

                case StateCtrl.eState.HeavySnow:
                    P_2 = -2 * 1.0f;
                    F_2 = -3 * 3.0f;
                    break;

                case StateCtrl.eState.ThunderCloud:
                    P_2 = -3 * 1.0f;
                    F_2 = -0 * 3.0f;
                    break;

                case StateCtrl.eState.Chief:
                    P_2 = -2 * 1.0f;
                    F_2 = -2 * 3.0f;
                    break;

                case StateCtrl.eState.Fire:
                    P_2 = -0 * 1.0f;
                    F_2 = -3 * 3.0f;
                    break;

                default:
                    break;
            }

            yield return null;
        }
    }

}
