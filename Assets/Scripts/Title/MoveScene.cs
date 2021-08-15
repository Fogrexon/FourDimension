using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FourDimension.Core;

namespace Game.Title {
    public class MoveScene : MonoBehaviour
    {
        [SerializeField]
        private float duration = 1f;

        private float startTime = -1f;

        private float maxVolume;

        private AudioSource se;
        [SerializeField]
        private AudioSource bgm;
        [SerializeField]
        private Material fadeMaterial;

        private FourDimensionTransform fdTransform;

        private void Start() {
            se = GetComponent<AudioSource>();
            maxVolume = bgm.volume;
            fadeMaterial.SetFloat("_Alpha", 0f);
            fdTransform = GameObject.Find("4DCube").GetComponent<FourDimensionTransform>();
        }

        void Update()
        {
            if (startTime < 0f && Input.GetKeyDown(KeyCode.Return)) {
                startTime = Time.time;
                se.Play();
                maxVolume = bgm.volume;
            }
            if (startTime >= 0f) {
                float progress = (Time.time - startTime) / duration;
                if(progress > 1f) {
                    SceneManager.LoadScene("Main");
                } else {
                    fadeMaterial.SetFloat("_Alpha", progress);
                    bgm.volume = (1f - progress) * maxVolume;
                    fdTransform.position.w = progress;
                }
            }
        }
    }
}
