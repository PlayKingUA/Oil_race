using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace BlueStellar.Cor
{
    public class Character : MonoBehaviour
    {
        #region Variables

        [SerializeField] CharacterColorType _characterColorType;
        [SerializeField] GameObject crown;
        [SerializeField] ParticleSystem effectDamage;
        [SerializeField] ParticleSystem effectDie;
        [SerializeField] StackBalls _stackBalls;

        [SerializeField] private bool isDeactiveCharacter;
         
   
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

        public void KillCharacter()
        {
            effectDie.transform.parent = null;
            effectDie.Play();
            gameObject.GetComponent<Collider>().enabled = false;
 
            leaderboard.RemoveMember(_characterColorType);
            StartCoroutine(IE_Die());
        }

        public void KilledField(Transform p)
        {
       
            transform.LookAt(p);
        }

        private IEnumerator IE_Die()
        {
            yield return new WaitForSeconds(2.5f);

            transform.DOScale(0, 0.9f);
            Destroy(gameObject, 2.5f);
        }

        #region CharacterCollisions

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ball")
            {
                CollectableBall _ball = other.GetComponent<CollectableBall>();
                
                if (!_ball.IsTrueCharacter(_characterColorType))
                    return;

                _stackBalls.AddCollectableBall(_ball);
               // if (_characterStates.IsPlayerCharacter())
                 //   VibrationController.Instance.ClaimVibration();
            }

            if (other.gameObject.tag == "Character")
            {
                Character character = other.GetComponent<Character>();
                StackBalls stackBalls = other.GetComponent<StackBalls>();

                if (isDeactiveCharacter)
                    return;

                if (_stackBalls.AmmountBalls() == stackBalls.AmmountBalls())
                    return;
                

                if (_stackBalls.AmmountBalls() >= stackBalls.AmmountBalls())
                {
                    other.GetComponent<Character>().KnockCharacter(transform);
                    //if (_characterStates.IsPlayerCharacter())
                      //  VibrationController.Instance.KnockVibration();

                    return;
                }

                //if (_characterStates.IsPlayerCharacter())
                  //  VibrationController.Instance.KnockVibration();

                KnockCharacter(other.transform);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "MonsterFields")
            {
                //_ballsMoster = other.GetComponentInParent<CollectableMonster>();

                //if (_ballsMoster.IsDeactivetedMonster())
                //    return;

                //if (_characterStates.IsDie())
                //    return;

                //_stackBalls.UnstackCollectablekBalls(_ballsMoster);
                //leaderboard.AddScoreMemeber(_characterColorType, _ballsMoster.GetFillingPercent(_characterColorType));

                //if (_ballsMoster.IsFullMonster())
                //{
                //    if (_ballsMoster.IsDeactivetedMonster())
                //        return;

                //    _characterStates.CharacterTransformation(_ballsMoster);
                //    _stackBalls.ClearStack();
                //}

                //if (_stackBalls.AmmountBalls() == 0)
                //    return; 

                //if (_characterStates.IsPlayerCharacter())
                //    VibrationController.Instance.UnstackVibration();
            }
        }

        #endregion
    }
}
