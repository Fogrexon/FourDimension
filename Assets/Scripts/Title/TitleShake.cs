using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Title {
    public class TitleShake : MonoBehaviour
    {
        private Vector3 basePos;
        void Start() {
            basePos = transform.position;
        }

        void Update() {
            transform.position = basePos +
                new Vector3(
                    Mathf.Sin(Time.time * 1.23456f),
                    Mathf.Sin(Time.time * 0.73821f),
                    (Mathf.Sin(Time.time * 0.99432f) * 2f + Mathf.Sin(Time.time * 0.34521f) * 3f)
                ) * 0.03f;
        }
    }
}
