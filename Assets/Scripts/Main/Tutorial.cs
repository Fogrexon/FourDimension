using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Main {
    public class Tutorial : MonoBehaviour
    {
        private bool flag = true;
        private Material fadeTutorial;

        private void Start() {
            fadeTutorial = GameObject.Find("TutorialBoard").GetComponent<RawImage>().material;
            Debug.Log(fadeTutorial);
            fadeTutorial.SetFloat("_Alpha", 1f);
        }

        private void Update()
        {
            if (flag && Input.anyKey) {
                // game end
                flag = false;
                DOTween.To(
                    () => 1,
                    v => fadeTutorial.SetFloat("_Alpha", v),
                    0f,
                    1f
                ).OnComplete(() => {
                    GameJudge.isPlaying = true;
                });
            }
        }
    }
}
