using System.Collections.Generic;
using UnityEngine;

namespace BlueStellar.Cor
{
    public class PointerController : MonoBehaviour
    {
        #region Singelton

        public static PointerController Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        [SerializeField] Transform pointSpawn;
        [SerializeField] PointerArrow _pointerPrefab;
        [SerializeField] PlayerMovement _playerTransform;
        [SerializeField] Camera _camera;
        private bool canShow;

        private Dictionary<BotPointer, PointerArrow> _dictionary = new Dictionary<BotPointer, PointerArrow>();

        private void Start()
        {
            _playerTransform = GameObject.FindObjectOfType<PlayerMovement>();
            LevelController.Instance.OnLevelStart.AddListener(ShowedPointer);
        }

        public void AddToList(BotPointer enemyPointer)
        {
            PointerArrow newPointer = Instantiate(_pointerPrefab, pointSpawn);
            _dictionary.Add(enemyPointer, newPointer);
        }

        public void RemoveFromList(BotPointer enemyPointer)
        {
            Destroy(_dictionary[enemyPointer].gameObject);
            _dictionary.Remove(enemyPointer);
        }

        void LateUpdate()
        {
            if (!canShow)
                return;

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            foreach (var kvp in _dictionary)
            {

                BotPointer enemyPointer = kvp.Key;
                PointerArrow pointerIcon = kvp.Value;

                Vector3 toEnemy = enemyPointer.transform.position - _playerTransform.transform.position;
                Ray ray = new Ray(_playerTransform.transform.position, toEnemy);
                Debug.DrawRay(_playerTransform.transform.position, toEnemy);


                float rayMinDistance = Mathf.Infinity;
                int index = 0;

                for (int p = 0; p < 4; p++)
                {
                    if (planes[p].Raycast(ray, out float distance))
                    {
                        if (distance < rayMinDistance)
                        {
                            rayMinDistance = distance;
                            index = p;
                        }
                    }
                }

                rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
                Vector3 worldPosition = ray.GetPoint(rayMinDistance);
                Vector3 position = _camera.WorldToScreenPoint(worldPosition);
                Quaternion rotation = GetIconRotation(index);

                if (toEnemy.magnitude > rayMinDistance)
                {
                    pointerIcon.Show();
                }
                else
                {
                    pointerIcon.Hide();
                }

                pointerIcon.SetIconPosition(position, rotation);
            }
        }

        private void ShowedPointer()
        {
            canShow = true;
        }

        Quaternion GetIconRotation(int planeIndex)
        {
            if (planeIndex == 0)
            {
                return Quaternion.Euler(0f, 0f, 90f);
            }
            else if (planeIndex == 1)
            {
                return Quaternion.Euler(0f, 0f, -90f);
            }
            else if (planeIndex == 2)
            {
                return Quaternion.Euler(0f, 0f, 180);
            }
            else if (planeIndex == 3)
            {
                return Quaternion.Euler(0f, 0f, 0f);
            }
            return Quaternion.identity;
        }
    }
}
