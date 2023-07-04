using NRKernal;
using NRKernal.NRExamples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Icon Detect class using Trackable Images
/// </summary>
public class IconDetect : MonoBehaviour
{
    /// <summary>
    /// Trackable image list
    /// </summary>
    private List<NRTrackableImage> m_NewMarkers = new List<NRTrackableImage>();

    /// <summary>
    /// Display trackable status
    /// </summary>

    public class GestureName
    {
        public const string Gesture_Victory = "Victory";
    }

    public HandEnum handEnum;
    public Transform tipAnchor;
    public Text gestureTxt;

    private const string RIGHT_HAND_LABEL = "R:";
    private const string LEFT_HAND_LABEL = "L:";

    public Text text;
    public GameObject Obj;

    private float m_SystemGestureTimer;
    /// <summary> Duration of system gesture to trigger function. </summary>
    private const float SYSTEM_GESTURE_KEEP_DURATION = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "hello";
        Obj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        UpdateGestureTip();

        NRFrame.GetTrackables<NRTrackableImage>(m_NewMarkers, NRTrackableQueryFilter.New);

        if (m_NewMarkers.Count > 0)
        {
            NRTrackableImage image = m_NewMarkers[0];
            Debug.Log(image.GetCenterPose().ToString());
            // If you use NRAnchor, uncomment the following
            // NRAnchor anchor = image.CreateAnchor();
            text.text = "Detected";
            Obj.SetActive(true);
        }

        else
        {
            Obj.SetActive(true);
        }
    }

    private void UpdateGestureTip()
    {       

        var handState = NRInput.Hands.GetHandState(handEnum);
        if (handState == null)
            return;

        if (handState.currentGesture == HandGesture.Victory)
        {
            m_SystemGestureTimer += Time.deltaTime;
            if (m_SystemGestureTimer > SYSTEM_GESTURE_KEEP_DURATION)
            {
                m_SystemGestureTimer = float.MinValue;
            }
        }
        else
        {
            m_SystemGestureTimer = 0f;
        }
    }
}
