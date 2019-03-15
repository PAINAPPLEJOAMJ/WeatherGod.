using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

//GamaManager에 넣어주어야 한다.
public class ControllerManager : MonoBehaviour {

    public HandController[] handAnim;

    private SteamVR_Input_Sources any = SteamVR_Input_Sources.Any;
    private SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
    private SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean triggerAction;
    public SteamVR_Action_Boolean padAction;
    public SteamVR_Action_Vibration vibration;
    public SteamVR_Action_Vector2 posAction;

    public static bool isSelect;
    public static bool isGrab;
    public static string selecting;

    HeadRaycast headRaycast;
    public GameObject playerTr;
    public GameObject[] vrCamTr;
    public GameObject[] item;
    private GameObject grabbedItem;
    private int grabHand = 2;
    private bool isPlayerTrCenter = true;

    public float high = 1.0f;
    public float throwSpeed = 9.0f;
    public Book book2;
    public GameObject book;
    public GameObject ui;
    public Transform targetTr;
    public Transform targetTr2;
    LineRenderer targeting;
    LineRenderer targeting2;


    void Start () {
        targeting = targetTr.GetComponent<LineRenderer>();
        targeting2 = targetTr2.GetComponent<LineRenderer>();
        ui.SetActive(true);
        targeting.enabled = false;
        book.SetActive(false);
    }

    void Update () {
        InputKey();
    }

    // 컨트롤러 입력을 받았음을 확인하는 함수
    void InputKey () {
        SteamVR_Input_Sources hand;
        for (int i = 0; i < handAnim.Length; i++) // 오른쪽 -> 왼쪽
        {
            //다른 손이 지시 상태일때 그 손은 지시가 불가능
            //grabHand가 2일때 어떤 손도 지시상태가 아니다.
            if (grabHand != 2 &&
                grabHand != i) continue;

            //오른손 -> 왼손
            if (i == 0) hand = rightHand;
            else hand = leftHand;

            //양손 텔레포트 누를시 
            if (padAction.GetStateDown(hand)
                || (Input.GetKeyDown(KeyCode.W) && i == 0)
                || (Input.GetKeyDown(KeyCode.S) && i == 1)) {
                Debug.Log("padAction");
                MoveCamTr(hand);
            }

            //트리거를 누른 경우 해당 손을 지시 상태로
            if (triggerAction.GetStateDown(hand)
                || (Input.GetMouseButtonDown(0) && i == 0)
                || (Input.GetMouseButtonDown(1) && i == 1)) {
                handAnim[i].ToFinger();
                grabHand = i;
            }
            //지시 상태인 손에게 아이템을 오래 응시하면 그 아이템 소지 상태가 되도록
            else if (triggerAction.GetState(hand)
               || (Input.GetMouseButton(0) && i == 0)
               || (Input.GetMouseButton(1) && i == 1)) {
                if (isSelect && !isGrab) {
                    if (selecting.Equals("THUNDER")) {
                        handAnim[i].FingerToGrabver();
                        Grab(i);
                        isGrab = true;
                        vibration.Execute(10, 1, 150, 75, hand);
                    } else if (selecting.Equals("RIGHT")) {
                        Debug.Log("Test");
                        book2.TweenForwardRight();
                        isGrab = true;
                        Grab(i);
                    } else if (selecting.Equals("LEFT")) {
                        Debug.Log("Test2");
                        book2.TweenForwardLeft();
                        isGrab = true;
                        Grab(i);
                    } else if (selecting.Equals("BOOK")) {
                        book.SetActive(true);
                        ui.SetActive(false);
                        isGrab = true;
                        Grab(i);
                    } else if (selecting.Equals("START")) {
                        SceneManager.LoadScene("Play");
                        isGrab = true;
                        Grab(i);
                    } else if (selecting.Equals("QUIT")) {
                        ui.SetActive(true);
                        book.SetActive(false);
                        isGrab = true;
                        Grab(i);
                    } else if (selecting.Equals("RESTART")) {
                        SceneManager.LoadScene("Main");
                        isGrab = true;
                        Grab(i);
                    } else {
                        handAnim[i].FingerToGrabbig();
                        Grab(i);
                        isGrab = true;
                        vibration.Execute(10, 1, 150, 75, hand);
                    }
                } else
                    return;
            }
            //트리거를 놓게 되면 지시상태를 해제하거나 소지 상태를 해제
            else if (triggerAction.GetStateUp(hand)
                || (Input.GetMouseButtonUp(0) && i == 0)
                || (Input.GetMouseButtonUp(1) && i == 1)) {
                handAnim[i].ToNormal();
                grabHand = 2;
                //소지상태인 경우 아이템을 사용
                if (isGrab) {
                    ThrowItem(i);
                    isGrab = false;
                }
            }
        }
    }


    // player이동 로직
    void MoveCamTr (SteamVR_Input_Sources hand) {
        Vector2 pos = SteamVR_Input._default.inActions.TouchPostion.GetAxis(hand);

        //터치패드 가운데 빼고 누르면 보는 방향으로 이동
        if (pos.y <= 0.05 || pos.x >= 0.05 || pos.y <= -0.05f || pos.x <= -.05f) {
            headRaycast = FindObjectOfType<HeadRaycast>();
            headRaycast.HeadRayCast_Find(); // headRaycast에 있는 함수를 실행
        }
    }

    //아이템을 일정시간 응시하면 아이템을 소지하는 상태로 바꾸는 함수
    //i값은 오른손인지 왼손인지
    //일정시간 응시 판단은 RaycastController에서 실행
    void Grab (int i) {
        if (selecting.Equals("BOOK") || selecting.Equals("START") || selecting.Equals("QUIT") ||
            selecting.Equals("RIGHT") || selecting.Equals("LEFT") || selecting.Equals("RESTART")) {
            isGrab = true;
            return;
        }

        grabbedItem = Instantiate(item[StrToNum(selecting)]);

        if (selecting.Equals("THUNDER")) {
            grabbedItem.transform.SetParent(handAnim[i].throwPoint2);
            targeting.enabled = true;
        } else if (selecting.Equals("WATER")) {
            grabbedItem.transform.SetParent(handAnim[i].throwPoint3);
            targeting2.enabled = true;
        } else
            grabbedItem.transform.SetParent(handAnim[i].throwPoint);

        grabbedItem.transform.localPosition = Vector3.zero;
        grabbedItem.transform.localRotation = Quaternion.identity;
        grabbedItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    //응시한 지점의 Tag에 따라 소지할 아이템의 번호를 반환
    int StrToNum (string item) {
        switch (item) {
        case "SUN": return 0;
        case "CLOUD": return 1;
        case "THUNDER": return 2;
        case "WATER": return 3;
        case "BOOK": return 4;
        case "RIGHT": return 5;
        case "LEFT": return 6;
        case "START": return 7;
        case "QUIT": return 8;
        case "RESTART": return 9;
        default: return 10;

        }
    }

    //Trigger를 해제한 경우 소지한 아이템을 물리적으로 던진다.
    //i값은 오른손인지 왼손인지
    //ItemAI에서 던진 아이템이 점점 커지는 함수를 실행

    void ThrowItem (int i) {
        if (StrToNum(selecting) == 4 || StrToNum(selecting) == 5 || StrToNum(selecting) == 6 ||
            StrToNum(selecting) == 7 || StrToNum(selecting) == 8 || StrToNum(selecting) == 9) {
            isGrab = false;
        } else {
            grabbedItem.transform.SetParent(null);
            grabbedItem.GetComponent<Rigidbody>().isKinematic = false;
            targeting.enabled = false;
            targeting2.enabled = false;

            if (StrToNum(selecting) == 2) // Thunder
            {
                grabbedItem.GetComponent<Rigidbody>().useGravity = false;
                grabbedItem.GetComponent<Rigidbody>().velocity = grabbedItem.transform.up * 30.0f;
                grabbedItem.GetComponent<Rigidbody>().freezeRotation = true;
            } else if (StrToNum(selecting) == 3) // Water
              {
                grabbedItem.GetComponent<Rigidbody>().useGravity = false;
                grabbedItem.GetComponent<Rigidbody>().velocity = grabbedItem.transform.forward * -30.0f;
                grabbedItem.GetComponent<Rigidbody>().freezeRotation = true;
            } else {
                grabbedItem.GetComponent<Rigidbody>().velocity = new Vector3(handAnim[i].GetComponent<Hand>().GetTrackedObjectVelocity().x * throwSpeed,
                                                                             high,
                                                                             handAnim[i].GetComponent<Hand>().GetTrackedObjectVelocity().z * throwSpeed);
                grabbedItem.GetComponent<Rigidbody>().angularVelocity = handAnim[i].GetComponent<Hand>().GetTrackedObjectAngularVelocity() * 30.0f;
            }
            Destroy(grabbedItem, 8.0f);

        }
    }
}