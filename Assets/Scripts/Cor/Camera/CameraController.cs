using UnityEngine;
using Cinemachine;

namespace BlueStellar.Cor
{
    public class CameraController : MonoBehaviour
    {
        #region Singleton

        public static CameraController Instance;

        private void Awake()
        {
            Instance = this;    
        }

        #endregion

        #region Variables

        [SerializeField] GameObject characterStateCam;
        [SerializeField] GameObject monsterStateCam;
        [SerializeField] GameObject jumpStateCam;
        [SerializeField] GameObject finishCam;
        [SerializeField] GameObject skinCam;
        [SerializeField] ParticleSystem effect;

        Transform player;

        #endregion

        private void Start()
        {
            SetTargetCameras();
        }

        public void CharacterCam(bool isActive)
        {
            characterStateCam.SetActive(isActive);
        }

        public void ChangeMonsterCam(bool isActive)
        {
            monsterStateCam.SetActive(true);
        }

        public void JumpStateCam(bool isActive)
        {
            jumpStateCam.SetActive(true);
        }

        public void SkinCam(Transform targetSkin, bool isActive)
        {
            skinCam.GetComponent<CinemachineVirtualCamera>().Follow = targetSkin;
            skinCam.GetComponent<CinemachineVirtualCamera>().LookAt = targetSkin;
            skinCam.SetActive(isActive);
        }

        private void SetTargetCameras()
        {
            player = GameObject.FindObjectOfType<PlayerMovement>().transform;
            characterStateCam.GetComponent<CinemachineVirtualCamera>().Follow = player;
            characterStateCam.GetComponent<CinemachineVirtualCamera>().LookAt = player;
            monsterStateCam.GetComponent<CinemachineVirtualCamera>().Follow = player;
            monsterStateCam.GetComponent<CinemachineVirtualCamera>().LookAt = player;
            jumpStateCam.GetComponent<CinemachineVirtualCamera>().Follow = player;
            jumpStateCam.GetComponent<CinemachineVirtualCamera>().LookAt = player;
            finishCam.GetComponent<CinemachineVirtualCamera>().Follow = player;
            finishCam.GetComponent<CinemachineVirtualCamera>().LookAt = player;
        }
    }
}
