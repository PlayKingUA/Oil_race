using UnityEngine;

namespace BlueStellar.Cor
{
    public class CharacterCanvas : MonoBehaviour
    {
        #region Variables

        [SerializeField] Transform targetCharacter;
        [SerializeField] Transform targetMonster;

        Transform _target;
        Transform _transform;

        #endregion

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _transform.parent = null;
            _target = targetCharacter;
        }

        private void LateUpdate()
        {
            if(_target != null)
                transform.position = _target.position;
        }

        public void SetMonsterTarget()
        {
            _target = targetMonster;
        }
    }
}
