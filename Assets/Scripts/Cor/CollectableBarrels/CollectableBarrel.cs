using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace BlueStellar.Cor
{
    public class CollectableBarrel : MonoBehaviour
    {
        #region Variables

        [Header("BarrelsMaterials")]
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] Material matsBarrel;

        [Header("BarrelPhysics")]
        [SerializeField] Rigidbody _rb;
        private bool isBallDestroyed;
        private bool cantStack;

        [Header("BarrelType")]
        [SerializeField] CharacterColorType _ballType;

        CharacterColorType newType;
        CollectableBarrelField _collectableBarrelField;

        #endregion

        public CharacterColorType Type()
        {
            return _ballType;
        }

        private void Start()
        {
            _collectableBarrelField = GameObject.FindObjectOfType<CollectableBarrelField>();
        }

        public bool IsTrueCharacter(CharacterColorType characterColorType)
        {
            if (cantStack)
                return false;

            if (_ballType == characterColorType ||
                _ballType == CharacterColorType.Neutral)
            {
                newType = characterColorType;
                SwitchColor(characterColorType);
                return true;
            }

            return false;
        }

        public void BallInStack()
        {
            cantStack = true;
            _rb.isKinematic = true;
            //meshRenderer.material.DOColor(colorClaim, 0.2f);
            _collectableBarrelField = GameObject.FindObjectOfType<CollectableBarrelField>();
            _collectableBarrelField.RemoveBall(this);
            transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 1);
            StopAllCoroutines();
            StartCoroutine(IE_ReturnColorBall());
        }
       
        public void BallNeutral()
        {
            cantStack = true;
            transform.SetParent(null);
            gameObject.GetComponent<Collider>().isTrigger = false;
           // meshRenderer.material.DOColor(neutral, 0.2f);
            _rb.isKinematic = false;
            _rb.AddForce(new Vector3(0f, 6f, -1f), ForceMode.Impulse);
            _ballType = CharacterColorType.Neutral;
        }

        public void Normal()
        {
            _ballType = CharacterColorType.Neutral;
            gameObject.GetComponent<Collider>().isTrigger = true;
            _rb.isKinematic = true;
            cantStack = false;
        }

        public void BallToMonster()
        {
            if (!isBallDestroyed)
            {
                //ballsMonster.BallActiveted(_ballType);
                isBallDestroyed = true;
                Destroy(gameObject, 0.28f);
            }
        }

        private IEnumerator IE_ReturnColorBall()
        {
            yield return new WaitForSeconds(0.2f);

            //meshRenderer.material.DOColor(colorBall, 0.2f);
            _ballType = newType;
        }

        private void SwitchColor(CharacterColorType _characterColorType)
        {
            switch (_characterColorType)
            {
                case CharacterColorType.Blue:
                    //colorBall = colors[0];
                    break;
                case CharacterColorType.Green:
                    //colorBall = colors[1];
                    break;
                case CharacterColorType.Violet:
                    //colorBall = colors[2];
                    break;
                case CharacterColorType.Yellow:
                    //colorBall = colors[3];
                    break;
                case CharacterColorType.Red:
                    //colorBall = colors[4];
                    break;
                case CharacterColorType.Purple:
                    //colorBall = colors[5];
                    break;
            }
        }
    }
}
