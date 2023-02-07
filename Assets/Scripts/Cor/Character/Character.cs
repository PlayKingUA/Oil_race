using System.Collections;
using UnityEngine;
using DG.Tweening;
using BlueStellar.Cor.Transports;

namespace BlueStellar.Cor
{
    public class Character : MonoBehaviour
    {
        #region Variables

        [SerializeField] CharacterColorType _characterColorType;
        [SerializeField] GameObject crown;
        [SerializeField] ParticleSystem effectDamage;
        [SerializeField] ParticleSystem effectDie;
        [SerializeField] StackBarrels _stackBalls;

        [SerializeField] private bool isDeactiveCharacter;

        Transport _transport;
        Leaderboard leaderboard;

        #endregion

        private void Start()
        {
            leaderboard = GameObject.FindObjectOfType<Leaderboard>();
        }

        public void SetCharacterSettings(CharacterColorType characterColorType)
        {
            _characterColorType = characterColorType;
        }

        public void CrownActive(bool isActive)
        {
            if(crown != null)
                crown.SetActive(isActive);
        }

        public void ActiveCharacter(bool isActive)
        {
            isDeactiveCharacter = isActive;
        }

        public void JumpToMontser()
        {
            //transform.DOJump(new Vector3(_ballsMoster.transform.position.x,
            //    _ballsMoster.transform.position.y + 1f, _ballsMoster.transform.position.z), 4f, 1, 0.5f);
        }

        public void KnockCharacter(Transform m)
        {
            effectDamage.Play();
            _stackBalls.DestroyedStack();
        }

        public void KilledField(Transform p)
        {
            transform.LookAt(p);
        }

        #region CharacterCollisions

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Barrel")
            {
                CollectableBarrel _ball = other.GetComponent<CollectableBarrel>();
                
                if (!_ball.IsTrueCharacter(_characterColorType))
                    return;

                _stackBalls.AddCollectableBall(_ball);
               
            }

            if (other.gameObject.tag == "Character")
            {
                Character character = other.GetComponent<Character>();
                StackBarrels stackBalls = other.GetComponent<StackBarrels>();

                if (isDeactiveCharacter)
                    return;

                if (_stackBalls.AmmountBalls() == stackBalls.AmmountBalls())
                    return;
                

                if (_stackBalls.AmmountBalls() >= stackBalls.AmmountBalls())
                {
                    other.GetComponent<Character>().KnockCharacter(transform);
                    return;
                }

                KnockCharacter(other.transform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "TransportField")
            {
                _transport = other.GetComponentInParent<Transport>();
                _stackBalls.UnstackCollectableBarrel(_transport);

                //leaderboard.AddScoreMemeber(_characterColorType, _ballsMoster.GetFillingPercent(_characterColorType));

                //if (_ballsMoster.IsFullMonster())
                //{
                //    if (_ballsMoster.IsDeactivetedMonster())
                //        return;

                //    _characterStates.CharacterTransformation(_ballsMoster);
                //    _stackBalls.ClearStack();
                //}

                if (_stackBalls.AmmountBalls() == 0)
                    return; 

                //if (_characterStates.IsPlayerCharacter())
                //    VibrationController.Instance.UnstackVibration();
            }
        }

        #endregion
    }
}
