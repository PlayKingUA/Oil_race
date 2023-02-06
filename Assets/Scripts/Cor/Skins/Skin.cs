using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlueStellar.Cor
{
    public class Skin : MonoBehaviour
    {
        #region Variables

        [Header("IdSkin")]
        [SerializeField] private string idSkin;

        [Header("Buttons")]
        [SerializeField] GameObject next;
        [SerializeField] GameObject claim;

        [Header("FragmentsSkin")]
        [SerializeField] SkinnedMeshRenderer[] fragments;
        [SerializeField] Material mat;

        [Header("SliderProgress")]
        [SerializeField] Slider _slider;
        [SerializeField] Text textProgress;
        [SerializeField] private int ammountFramgents;
        private int setProgress;
        private int progress;

        [Header("PointSkin")]
        [SerializeField] Transform pointSkin;

        [Header("SkinEffeñå³")]
        [SerializeField] GameObject effect;

        [Header("SkinAnimation")]
        [SerializeField] DOTweenAnimation punchSkin;

        [Header("StatusSkin")]
        [SerializeField] private bool isOpenSkin;
        private bool isPartOpen;
        private bool isSetProgress;

        SkinsController _skinsController;

        #endregion

        public Transform Point()
        {
            return pointSkin;
        }

        private void Start()
        {
            LoadSave();
            CheckStatusSkin();
            _skinsController = GameObject.FindObjectOfType<SkinsController>();
        }

        public void ActiveFragmentsSkin()
        {
            if (isPartOpen)
                return;

            if (!isPartOpen)
            {
                punchSkin.DOPlay();
                fragments[ammountFramgents].material = mat;

                UIManager.Instance.SettingsButtonScreen(false);
                UIManager.Instance.MoneyScreen(false);
                UIManager.Instance.RewardScreen(true);
                
                SaveSkin();
                isPartOpen = true;
            }
        }

        public void UpdateSkin()
        {
            _slider.minValue = 0;
            _slider.maxValue = 100;

            if (!isSetProgress)
            {
                if (ammountFramgents == 0)
                {
                    progress = 0;
                }
                if (ammountFramgents == 1)
                {
                    progress = 25;
                }
                if (ammountFramgents == 2)
                {
                    progress = 50;
                }
                if (ammountFramgents == 3)
                {
                    progress = 75;
                }
                if (ammountFramgents == 4)
                {
                    progress = 100;
                }
                setProgress = progress;
                _slider.value = progress;
                textProgress.text = progress + "%";

                UIManager.Instance.BonusScreen(true);
                isSetProgress = true;
            }

            if (_slider.value < setProgress + 25)
                _slider.value += 0.1f;

            if (_slider.value < 10)
            {
                textProgress.text = _slider.value.ToString().Substring(0, 1) + "%";
            }

            if (_slider.value >= 10) { 
            textProgress.text = _slider.value.ToString().Substring(0, 2) + "%";
        }

            if (!effect.activeSelf)
                effect.SetActive(true);
        }

        private void CheckStatusSkin()
        {
            if (isOpenSkin)
            {
                foreach(var i in fragments)
                {
                    i.material = mat;
                }
                return;
            }

            if (ammountFramgents > 0)
            {
                for (int i = 0; i < ammountFramgents; i++)
                {
                    fragments[i].material = mat;
                }
            }
        }

        public void OpenFramgentSkin()
        {
            ActiveFragmentsSkin();
            effect.SetActive(false);
            ammountFramgents++;

          
            if (ammountFramgents >= 4)
                isOpenSkin = true;

            if (ammountFramgents == 4)
            {
                next.SetActive(false);
                claim.SetActive(true);
                isOpenSkin = true;
                _skinsController.NewProgressSkin();
            }

            SaveSkin();
        }

        public void CloseSkin()
        {
            isOpenSkin = false;
            ammountFramgents = 0;
            SaveSkin();
        }

        #region Load&Save

        private void LoadSave()
        {
            ammountFramgents = ES3.Load("ammountFramgents" + idSkin, ammountFramgents);
            isOpenSkin = ES3.Load("isOpenSkin" + idSkin, isOpenSkin);
        }

        private void SaveSkin()
        {
            ES3.Save("ammountFramgents" + idSkin, ammountFramgents);
            ES3.Save("isOpenSkin" + idSkin, isOpenSkin);
        }

        #endregion
    }
}
