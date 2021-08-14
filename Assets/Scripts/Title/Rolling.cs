using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourDimension.Core;

namespace Game.Title {
    public class Rolling : MonoBehaviour
    {
        private float startTime = 0f;
        private FourDimensionTransform fdTransform;

        private void Start() {
            startTime = Time.time;
            fdTransform = GetComponent<FourDimensionTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            float gameTime = Time.time - startTime;
            float scale = 0.1f;
            fdTransform.rotateXYZ.Set(Time.time * 0.12355f * scale, gameTime * 0.2346764f * scale, gameTime * 0.5834564f * scale);
            fdTransform.rotateW.Set(Time.time * 0.6345234f * scale, gameTime * 0.633289f * scale, gameTime * 0.356223f * scale);
        }
    }
}
