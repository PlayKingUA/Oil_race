using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueStellar.Cor.Transports
{
    public class Transport : MonoBehaviour
    {
        #region Variables

        [System.Serializable]
        public class ColorTransport
        {
            public MeshRenderer[] _mesh;
            public MeshRenderer[] otherMesh;
            public Material[] materials;
            public GameObject tranperent;
            public Color _color;
            public Vector3 _offset;
            public float YProgress;
            public float maxProgress;
        }
        
        [SerializeField] List<ColorTransport> colorTransports = new List<ColorTransport>();
        [SerializeField] ParticleSystem[] effects;
        [SerializeField] Animator animTransport;

        #endregion

        public void SetupMaterialSettings(CharacterColorType characterColorType)
        {
            int indexTransoprtColor = 0;

            switch (characterColorType)
            {
                case CharacterColorType.Blue:
                    indexTransoprtColor = 0;
                    break;
                case CharacterColorType.Green:
                    indexTransoprtColor = 1;
                    break;
                case CharacterColorType.Yellow:
                    indexTransoprtColor = 2;
                    break;
                case CharacterColorType.Violet:
                    indexTransoprtColor = 3;
                    break;
                case CharacterColorType.Red:
                    indexTransoprtColor = 4;
                    break;
                case CharacterColorType.Purple:
                    indexTransoprtColor = 5;
                    break;
            }

            colorTransports[indexTransoprtColor]._offset.y += colorTransports[indexTransoprtColor].YProgress;
            foreach (var i in colorTransports[indexTransoprtColor]._mesh) { i.material.SetVector("_DissolveOffest", colorTransports[indexTransoprtColor]._offset); }
            if(colorTransports[indexTransoprtColor]._offset.y >= colorTransports[indexTransoprtColor].maxProgress)
            {
                for(int i = 0; i < colorTransports[indexTransoprtColor].otherMesh.Length; i++)
                {
                    colorTransports[indexTransoprtColor].otherMesh[i].material = colorTransports[indexTransoprtColor].materials[i];
                }
                colorTransports[indexTransoprtColor].tranperent.SetActive(false);
                foreach(var i in effects) { i.Play(); }
                animTransport.SetBool("Move", true);
            }
        }
    }
}
