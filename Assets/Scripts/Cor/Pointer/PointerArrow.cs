using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BlueStellar.Cor
{
    public class PointerArrow : MonoBehaviour
    {
        [SerializeField] Image _image;
        bool _isShown = true;

        private void Awake()
        {
            _image.enabled = false;
            _isShown = false;
        }

        public void SetIconPosition(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }

        public void Show()
        {
            if (_isShown) return;
            _isShown = true;
            StopAllCoroutines();
            StartCoroutine(IE_ShowProcess());
        }

        public void Hide()
        {
            if (!_isShown) return;
            _isShown = false;

            StopAllCoroutines();
            StartCoroutine(IE_HideProcess());
        }

        private IEnumerator IE_ShowProcess()
        {
            _image.enabled = true;
            transform.localScale = Vector3.zero;
            for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
            {
                transform.localScale = Vector3.one * t;
                yield return null;
            }
            transform.localScale = Vector3.one;
        }

        private IEnumerator IE_HideProcess()
        {
            for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
            {
                transform.localScale = Vector3.one * (1f - t);
                yield return null;
            }
            _image.enabled = false;
        }

    }
}
