using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//LineRenderer를 가지고 있는 Raycast오브젝트에 넣어주어야한다.
public class RaycastController1 : MonoBehaviour {

    private float distance = 100.0f;
    private float selectedTime = 1.0f;
    private float passedTime = 0.0f;
    private bool isClicked = false;


    private Ray ray;
    private RaycastHit hit;

    public Transform tr;
    private LineRenderer laser;
    public bool isRaycast = true;
    public Image circleBar;

    private GameObject currButton;
    private GameObject prevButton;

    private void Start () {
        laser = tr.GetComponent<LineRenderer>();
        LaserOff();
    }

    void Update () {
        if (isRaycast == false) return;

        PointerEventData data = new PointerEventData(EventSystem.current);

        //10번 ITEM 레이어를 검출했을떄
        if (Physics.Raycast(tr.position, tr.forward, out hit, distance, 1 << 10)) {
            currButton = hit.collider.gameObject;

            //다른 것을 응시한 경우 이전에 선택된 것을 해제하고 응시시간을 초기화
            if (currButton != prevButton) {
                isClicked = false;
                passedTime = 0.0f;
                circleBar.fillAmount = 0.0f;

                ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerEnterHandler);
                ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                prevButton = currButton;
            }
            //같은 것을 응시하는 경우 응시시간을 측정
            else if (!isClicked) {
                passedTime += Time.deltaTime;
                circleBar.fillAmount = passedTime / selectedTime;

                //응시시간만큼 응시한 경우 ControllerManager에게 변수값 변경을 통해 알려준다.
                if (passedTime >= selectedTime) {
                    ControllerManager.isSelect = true;
                    ControllerManager.selecting = hit.collider.tag;
                }
            }
        }
        //Item을 응시하지 않은 경우 응시상태를 해제
        else {
            ControllerManager.isSelect = false;
            if (prevButton != null) {
                ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
                circleBar.fillAmount = 0.0f;
                prevButton = null;
            }
        }
    }

    //기본 상태이거나 소지 상태의 경우 Raycast를 실행하지 않는다.
    public void LaserOff () {
        //레이져를 끄기 전 여러가지 변수가 초기화
        PointerEventData data = new PointerEventData(EventSystem.current);
        ControllerManager.isSelect = false;
        circleBar.fillAmount = 0.0f;
        if (prevButton != null) {
            ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
            prevButton = null;
        }

        laser.enabled = isRaycast = false;
    }

    //지시상태일 경우 Raycast를 실행해야한다.
    public void LaserOn () {
        laser.enabled = isRaycast = true;
    }
}