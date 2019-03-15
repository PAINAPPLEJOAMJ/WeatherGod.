using System.Collections;
using UnityEngine;

public class PF_System : MonoBehaviour
{

    // 날씨에 따라 인구 식량 값 변동
    // 날씨는 10초동안 적용
    // 다른 상태머신의 변동을 받아 다시 인구 식량의 변동을 바꿔준다.

    StateCtrl.eState state;
    
    public float A; // Achieve     // 1680max
    public float P; // Population  // 420max
    public float F; // Food        // 1260max
    public float aTime;
    public Ground_System gound_System;
    public Desert_System desert_System;
    public Port_System port_System;
    public Mountain_System mountain_System;


    void Start()
    {
        P = gound_System.pCnt;
        StartCoroutine(PF_Controller());
    }

    void Update()
    {
        aTime += Time.deltaTime;
    }

    IEnumerator PF_Controller()
    {

        while (true)
        {

            if (state == StateCtrl.eState.Clear)
            {
                P += 1.0f * Time.deltaTime;
                F += 3.0f * Time.deltaTime;
            }
            else if (state != StateCtrl.eState.Clear)
            {
                switch (state)
                {
                    case StateCtrl.eState.Heat:
                        P += 0 * 3.0f * Time.deltaTime;
                        F += 0 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Cloud:
                        P += 1.0f * Time.deltaTime;
                        F += 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Rain:
                        P += 0 * 3.0f * Time.deltaTime;
                        F += 0 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Snow:
                        P += 0 * 3.0f * Time.deltaTime;
                        F += 0 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Drought:
                        P -= 1 * 1.0f * Time.deltaTime;
                        F -= 3 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.HeavyRain:
                        P -= 1 * 1.0f * Time.deltaTime;
                        F -= 2 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.HeavySnow:
                        P -= 2 * 1.0f * Time.deltaTime;
                        F -= 3 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.ThunderCloud:
                        P -= 3 * 1.0f * Time.deltaTime;
                        F -= 0 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Chief:
                        P -= 2 * 1.0f * Time.deltaTime;
                        F -= 2 * 3.0f * Time.deltaTime;
                        break;

                    case StateCtrl.eState.Fire:
                        P -= 0 * 1.0f * Time.deltaTime;
                        F -= 3 * 3.0f * Time.deltaTime;
                        break;

                    default:
                        break;
                }
            }

            yield return null;
        }
    }

}
