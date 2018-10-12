using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Battle;

namespace Game.UI
{
    public class JoyStickInputView : MonoBehaviour
    {
        private Transform joyStick;
        private Transform stick;
        private int joyStickMaxRadius = 40;
        RectTransform stickTransform;
        private Vector2 originPosition;
        void Awake()
        {
            this.joyStick = this.transform;
            this.stick = this.joyStick.Find("stick");
            this.stickTransform = this.stick.GetComponent<RectTransform>();
            this.originPosition = stickTransform.anchoredPosition;
            this.gameObject.AddComponent<EventTriggerListener>().onDrag = OnDrag;
            this.gameObject.AddComponent<EventTriggerListener>().onDown = OnDragBegin;
            this.gameObject.AddComponent<EventTriggerListener>().onUp = OnDragEnd;
            this.gameObject.AddComponent<EventTriggerListener>().onEndDrag = OnDragEnd;
        }
        // begin draggin

        private void OnDrag(GameObject g, PointerEventData eventData)
        {            
            Vector2 v2 = GetJoyStickAxis(eventData);
            InputManager.Instance.SetDir(v2);
        }

        private void OnDragBegin(GameObject g, PointerEventData eventData)
        {
            Vector2 v2 = GetJoyStickAxis(eventData);
            InputManager.Instance.SetDir(v2);
        }

        private void OnDragEnd(GameObject g, PointerEventData eventData)
        {
            this.stick.localPosition = Vector3.zero;
            InputManager.Instance.SetDir(Vector2.zero);
        }

        private Vector2 GetJoyStickAxis(PointerEventData eventData)
        {
            //获取手指位置的世界坐标
            Vector3 worldPosition;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(stickTransform,
                     eventData.position, eventData.pressEventCamera, out worldPosition))
                stickTransform.position = worldPosition;
            //获取摇杆的偏移量
            Vector2 touchAxis = stickTransform.anchoredPosition - originPosition;
            //摇杆偏移量限制
            if (touchAxis.magnitude >= joyStickMaxRadius) {
                touchAxis = touchAxis.normalized * joyStickMaxRadius;
                stickTransform.anchoredPosition = touchAxis;
            }
            return touchAxis;
        }
    }

}

