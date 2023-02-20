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
    public Text text;

    public GameObject Obj;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Searching";
        Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NRFrame.GetTrackables<NRTrackableImage>(m_NewMarkers, NRTrackableQueryFilter.New);

        if(m_NewMarkers.Count > 0)
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
            Obj.SetActive(true); // test를 위해 임시로 true로 설정. 빌드 전 false로 변경해야 함.
        }
    }
}
