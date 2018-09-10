using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
namespace SUIFW {
    [AddComponentMenu("UI/Internal/UUIEventListener")]
    public class UUIEventListener : MonoBehaviour,
                                    IPointerClickHandler,
                                    IPointerDownHandler,
                                    IPointerEnterHandler,
                                    IPointerExitHandler,
                                    IPointerUpHandler,
                                    ISelectHandler,
                                    IUpdateSelectedHandler,
                                    IDeselectHandler,
                                    IDragHandler,
                                    IEndDragHandler,
                                    IDropHandler,
                                    IScrollHandler,
                                    IMoveHandler {
        public delegate void VoidDelegate(GameObject go);
        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onSelect;
        public VoidDelegate onUpdateSelect;
        public VoidDelegate onDeSelect;
        public VoidDelegate onDrag;
        public VoidDelegate onDragEnd;
        public VoidDelegate onDrop;
        public VoidDelegate onScroll;
        public VoidDelegate onMove;

        ///my extension event
        public VoidDelegate onDoubleClick;// double click
        public VoidDelegate onLongPressDown;// long time down

        ///my extension event
        private DateTime m_firstTime_press;
        private DateTime m_firstTime_up_press;

        private DateTime m_firstTime;
        private DateTime m_secondTime;
        private void resetTime() {
            m_firstTime = default(DateTime);
            m_secondTime = default(DateTime);
        }

        private void onPress(PointerEventData eventData) {
            if (null != onLongPressDown)
                onLongPressDown(this.gameObject);
            //resetTime();
        }

        private void onDouble(PointerEventData eventData) {
            if (onDoubleClick != null) onDoubleClick(this.gameObject);
            //resetTime();
        }
        ///end

        public object parameter;

        public void OnPointerClick(PointerEventData eventData) { if (onClick != null) onClick(gameObject); }
        public void OnPointerDown(PointerEventData eventData) { 
            if (onDown != null) onDown(gameObject);
            // 按下按钮时对两次的时间进行记录
            if (m_firstTime_press.Equals(default(DateTime))) {
                m_firstTime_press = DateTime.Now;
            }

            if (m_firstTime.Equals(default(DateTime))){
                m_firstTime = DateTime.Now;
            }
            else
                m_secondTime = DateTime.Now;
        }
        public void OnPointerEnter(PointerEventData eventData) { if (onEnter != null) onEnter(gameObject); }
        public void OnPointerExit(PointerEventData eventData) { if (onExit != null) onExit(gameObject); }
        public void OnPointerUp(PointerEventData eventData) {
            if (onUp != null) onUp(gameObject);
            if (!m_firstTime.Equals(default(DateTime))) {
                m_firstTime_up_press = DateTime.Now;
                var intervalTime = m_firstTime_up_press - m_firstTime_press;
                float milliSeconds = intervalTime.Seconds * 1000 + intervalTime.Milliseconds;
                if (milliSeconds > 600) {
                    onPress(eventData);
                }
                m_firstTime_press = default(DateTime);
            }
           
            // 在第二次鼠标抬起的时候进行时间的触发,时差小于400ms触发
            if (!m_firstTime.Equals(default(DateTime)) && !m_secondTime.Equals(default(DateTime))) {
                var intervalTime = m_secondTime - m_firstTime;
                float milliSeconds = intervalTime.Seconds * 1000 + intervalTime.Milliseconds;
                if (milliSeconds < 400)
                    onDouble(eventData);
                resetTime();
            }
        }
        public void OnSelect(BaseEventData eventData) { if (onSelect != null) onSelect(gameObject); }
        public void OnUpdateSelected(BaseEventData eventData) { if (onUpdateSelect != null) onUpdateSelect(gameObject); }
        public void OnDeselect(BaseEventData eventData) { if (onDeSelect != null) onDeSelect(gameObject); }
        public void OnDrag(PointerEventData eventData) { if (onDrag != null) onDrag(gameObject); }
        public void OnEndDrag(PointerEventData eventData) { if (onDragEnd != null) onDragEnd(gameObject); }
        public void OnDrop(PointerEventData eventData) { if (onDrop != null) onDrop(gameObject); }
        public void OnScroll(PointerEventData eventData) { if (onScroll != null) onScroll(gameObject); }
        public void OnMove(AxisEventData eventData) { if (onMove != null) onMove(gameObject); }

        static public UUIEventListener Get(GameObject go) {
            UUIEventListener listener = go.GetComponent<UUIEventListener>();
            if (listener == null) listener = go.AddComponent<UUIEventListener>();
            return listener;
        }
    }
}
