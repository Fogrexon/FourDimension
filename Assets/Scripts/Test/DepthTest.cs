using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Test {
    public class DepthTest : MonoBehaviour
    {
        public Material mat;
        void Start () 
        {
            GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
        }

        public void OnRenderImage(RenderTexture source, RenderTexture dest)
        {                
            Graphics.Blit(source, dest, mat);
        }
    }
}
