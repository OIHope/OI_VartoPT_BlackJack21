using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Core.Managers
{
    public class LoadingScreenManager : MonoBehaviour
    {
        [SerializeField] private Image fillScreenImage;
        [SerializeField] private TextMeshProUGUI titleTextVisual;
        [Space(25)]
        [SerializeField] private string loadingText;
        [SerializeField] private string resettingText;
        [Space(25)]
        [SerializeField] private Color screenColor;
        [SerializeField] private Color textColor;
        [Space(10)]
        [SerializeField] private AnimationCurve transitionCurve;


        public void FastLoadingScreen()
        {
            SetScreenState(true, loadingText);
        }

        public void FastResettingScreen()
        {
            SetScreenState(true, resettingText);
        }

        private void SetScreenState(bool enable, string text)
        {
            fillScreenImage.enabled = true;
            titleTextVisual.enabled = true;

            titleTextVisual.text = text;

            fillScreenImage.color = screenColor;
            titleTextVisual.color = textColor;

            if (!enable)
            {
                fillScreenImage.color = new Color(screenColor.r, screenColor.g, screenColor.b, 0);
                titleTextVisual.color = new Color(textColor.r, textColor.g, textColor.b, 0);
            }
        }

        public IEnumerator ToggleLoadingScreen(bool enable)
        {
            yield return ToggleScreen(enable, loadingText);
        }

        public IEnumerator ToggleResettingScreen(bool enable)
        {
            yield return ToggleScreen(enable, resettingText);
        }

        private IEnumerator ToggleScreen(bool enable, string text)
        {
            float startAlphaValue = enable ? 0 : 1;
            float endAlphaValue = enable ? 1 : 0;

            SetScreenState(enable, text);

            float elapsedTime = 0f;
            float transitionDuration = GetAnimationCurveDuration(transitionCurve);

            while (elapsedTime < transitionDuration)
            {
                float alphaValue = Mathf.Lerp(startAlphaValue, endAlphaValue, transitionCurve.Evaluate(elapsedTime / transitionDuration));

                Color screenColorWithAlpha = screenColor;
                screenColorWithAlpha.a = alphaValue;
                fillScreenImage.color = screenColorWithAlpha;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            while (elapsedTime < transitionDuration)
            {
                float alphaValue = Mathf.Lerp(startAlphaValue, endAlphaValue, transitionCurve.Evaluate(elapsedTime / transitionDuration));

                Color textColorWithAlpha = textColor;
                textColorWithAlpha.a = alphaValue;
                titleTextVisual.color = textColorWithAlpha;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        private float GetAnimationCurveDuration(AnimationCurve animationCurve)
        {
            Keyframe maxTimeKeyframe = animationCurve.keys[0];
            foreach (Keyframe key in animationCurve.keys)
            {
                if (key.time > maxTimeKeyframe.time) maxTimeKeyframe = key;
            }
            return maxTimeKeyframe.time;
        }
    }
}