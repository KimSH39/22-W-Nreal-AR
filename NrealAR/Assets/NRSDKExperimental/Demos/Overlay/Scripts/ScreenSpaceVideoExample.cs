using NRKernal.Experimental;
using UnityEngine;
using UnityEngine.Video;

namespace NRKernal.Experimental.NRExamples
{
    /// <summary>
    /// overlay ScreenSpace Videoplayer Example
    /// </summary>
    public class ScreenSpaceVideoExample : MonoBehaviour
    {
        [SerializeField]
        private NROverlay m_NROverlay;
        [SerializeField]
        private VideoPlayer m_Player;

        private RenderTexture m_RenderTexture;

        // Start is called before the first frame update
        void Start()
        {
            int width = (int)m_Player.width;
            int height = (int)m_Player.height;

            m_RenderTexture = new RenderTexture(800, 600, 0, RenderTextureFormat.ARGB32);

            m_NROverlay.isDynamic = true;

            m_Player.targetTexture = m_RenderTexture;
            m_Player.Prepare();
            m_Player.Play();
            m_Player.started += p => { 
            
                m_NROverlay.MainTexture = m_RenderTexture;
                m_NROverlay.enabled = true;
            };

            m_NROverlay.onBufferChanged += p => {
                Graphics.Blit(m_Player.texture, p);
            };
           
        }
    }
}