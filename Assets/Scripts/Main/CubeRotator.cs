using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FourDimension.Core;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Game.Main {
    public class CubeRotator : MonoBehaviour
    {
        private FourDimensionTransform fdTransform;
        [SerializeField]
        private float rotationDelta = Mathf.PI;
        private void Start()
        {
            fdTransform = GameObject.Find("4DCube").GetComponent<FourDimensionTransform>();
        }

        private void Update() {
            if (!GameJudge.isPlaying) return;
            if (Input.GetKey(KeyCode.W)) {
                fdTransform.rotateXYZ.x = (fdTransform.rotateXYZ.x + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.S)) {
                fdTransform.rotateXYZ.x = (fdTransform.rotateXYZ.x - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.E)) {
                fdTransform.rotateXYZ.y = (fdTransform.rotateXYZ.y + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.D)) {
                fdTransform.rotateXYZ.y = (fdTransform.rotateXYZ.y - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.R)) {
                fdTransform.rotateXYZ.z = (fdTransform.rotateXYZ.z + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.F)) {
                fdTransform.rotateXYZ.z = (fdTransform.rotateXYZ.z - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            
            if (Input.GetKey(KeyCode.U)) {
                fdTransform.rotateW.x = (fdTransform.rotateW.x + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.J)) {
                fdTransform.rotateW.x = (fdTransform.rotateW.x - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.I)) {
                fdTransform.rotateW.y = (fdTransform.rotateW.y + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.K)) {
                fdTransform.rotateW.y = (fdTransform.rotateW.y - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.O)) {
                fdTransform.rotateW.z = (fdTransform.rotateW.z + rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }
            if (Input.GetKey(KeyCode.L)) {
                fdTransform.rotateW.z = (fdTransform.rotateW.z - rotationDelta * Time.deltaTime + Mathf.PI * 2f) % (Mathf.PI * 2f);
            }

            if (Input.GetKey(KeyCode.Space)) {
                DOTween.To(
                    () => fdTransform.rotateXYZ,
                    v => fdTransform.rotateXYZ = v,
                    Vector3.zero,
                    0.3f
                );
                DOTween.To(
                    () => fdTransform.rotateW,
                    v => fdTransform.rotateW = v,
                    Vector3.zero,
                    0.3f
                );
            }

            if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
