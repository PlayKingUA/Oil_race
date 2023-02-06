using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlueStellar.Cor
{
    public class MemberBlock : MonoBehaviour
    {
        #region Variables

        [SerializeField] GameObject crown;
        [SerializeField] Text textBlock;
        [SerializeField] Slider sliderBlock;
        [SerializeField] Image imgBlock;
        [SerializeField] Color norm;
        [SerializeField] Color fade;
        [SerializeField] Image bgBlock;
        [SerializeField] Color colorStandart;
        [SerializeField] Color colorBlock;
        [SerializeField] DOTweenAnimation blockPunch;
        [SerializeField] private float scaleNormal;
        [SerializeField] private float scaleMinimal;

        #endregion

        public void SetColorBlock(Color _color)
        {
            colorBlock = _color;
            imgBlock.color = colorBlock;
            textBlock.color = Color.white;
            sliderBlock.value = 0;
            sliderBlock.minValue = 0;
            sliderBlock.maxValue = 100;
            transform.localScale = new Vector3(scaleNormal, scaleNormal, scaleNormal);
        }

        public void SetProgressBlock(string _text, int progressValue)
        {
            textBlock.text = _text;
            sliderBlock.value = progressValue;
        }

        public void BlockAnimation()
        {
            blockPunch.DORestart();
        }

        public void LeaderBlock(bool isLeader)
        {
            if (isLeader)
            {
                crown.SetActive(true);
                imgBlock.color = colorBlock;
                bgBlock.color = norm;
                textBlock.color = Color.white;
                transform.localScale = new Vector3(scaleNormal, scaleNormal, scaleNormal);
                return;
            }

            crown.SetActive(false);
            imgBlock.color = colorStandart;
            bgBlock.color = fade;
            textBlock.color = colorBlock;
            transform.localScale = new Vector3(scaleMinimal, scaleMinimal, scaleMinimal);
        }
    }
}
