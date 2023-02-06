using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueStellar.Cor
{
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField] Animator _anim;

        public void RunAnimation(float speed)
        {
            _anim.SetFloat("Run", speed);
        }
    }
}
