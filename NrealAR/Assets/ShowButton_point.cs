using NRKernal;
using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowButton_point : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer m_MeshRender;

    private List<NRTrackableImage> m_NewMarkers = new List<NRTrackableImage>();


    public class GestureName
    {
        public const string Gesture_Point = "Point";
    }

    public HandEnum handEnum;
    public Transform tipAnchor;
    public Text gestureTxt;

    private const string RIGHT_HAND_LABEL = "R:";
    private const string LEFT_HAND_LABEL = "L:";
    private ShowButton button;

    public Text text;
    public GameObject Obj;

    private float m_SystemGestureTimer;
    /// <summary> Duration of system gesture to trigger function. </summary>
    private const float SYSTEM_GESTURE_KEEP_DURATION = 0.8f;


    void Awake()
    {
        m_MeshRender = transform.GetComponent<MeshRenderer>();
    }

    void Start()
    {

        m_MeshRender.enabled = false;
    }

    /// <summary> Updates this object. </summary>
    void Update()
    {
        UpdateGestureTip();
        ReUpdate();
    }

    private void UpdateGestureTip()
    {

        var handState = NRInput.Hands.GetHandState(handEnum);
        if (handState == null)
            return;

        if (handState.currentGesture == HandGesture.Point)
        {
            m_SystemGestureTimer += Time.deltaTime;

            if (m_SystemGestureTimer > SYSTEM_GESTURE_KEEP_DURATION)
            {

                m_SystemGestureTimer = float.MinValue;
                m_MeshRender.enabled = true;
            }
        }
        else
        {
            m_SystemGestureTimer = 0f;
        }
    }

    private void ReUpdate()
    {

        var handState = NRInput.Hands.GetHandState(handEnum);
        if (handState == null)
            return;


        if (m_MeshRender.enabled == true)
        {
            if (handState.currentGesture == HandGesture.Point)
            {
                m_SystemGestureTimer += Time.deltaTime;

                if (m_SystemGestureTimer > SYSTEM_GESTURE_KEEP_DURATION)
                {

                    m_SystemGestureTimer = float.MinValue;
                    m_MeshRender.enabled = false;
                }
            }
            else
            {
                m_SystemGestureTimer = 0f;
            }
        }
    }
}