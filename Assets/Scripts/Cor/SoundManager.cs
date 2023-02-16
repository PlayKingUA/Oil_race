using UnityEngine;

namespace BlueStellar.Cor
{
    public class SoundManager : MonoBehaviour
    {
        #region Singelton

        public static SoundManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Variables

        [Header("Sounds")]
        [SerializeField] AudioSource soundClaim;
        [SerializeField] AudioSource soundHit;

        private bool isOffSound;

        #endregion

        public bool IsOffSound()
        {
            return isOffSound;
        }

        public void SoundClaimActive()
        {
            if (isOffSound)
                return;

            soundClaim.Play();
        }

        public void SoundHitActive()
        {
            if (isOffSound)
                return;

            soundHit.Play();
        }
    }
}
