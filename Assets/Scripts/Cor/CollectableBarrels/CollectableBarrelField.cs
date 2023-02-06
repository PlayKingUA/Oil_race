using System.Collections.Generic;
using UnityEngine;

namespace BlueStellar.Cor
{
    public class CollectableBarrelField : MonoBehaviour
    {
        [System.Serializable]
        public class BallType
        {
            public GameObject ballPrefab;
            public CharacterColorType type;
        }

        #region Variables

        [Header("BallTypes")]
        [SerializeField] List<BallType> ballTypes = new List<BallType>();

        [Space]
        [Header("FieldPlacement")]
        [SerializeField] private int length;
        [SerializeField] private int line;
        [SerializeField] private float xOrder;
        [SerializeField] private float xPosition;
        [SerializeField] private float zPosition;
        [SerializeField] private float form;

        [Space]
        [Header("TimerResetBall")]
        [SerializeField] private float timeToResetBall;
        [SerializeField] private float timer;

        private Vector3 startPoint;
        private Vector3 position;

        List<SpawnedBall> spawnedBalls = new List<SpawnedBall>();
        List<SpawnedBall> respawnBalls = new List<SpawnedBall>();

        #endregion

        public List<Vector3> ListTypeBalls(CharacterColorType colorType)
        {
            List<Vector3> balls = new List<Vector3>();
            switch (colorType)
            {
                case CharacterColorType.Blue:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
                case CharacterColorType.Yellow:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
                case CharacterColorType.Green:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
                case CharacterColorType.Violet:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
                case CharacterColorType.Purple:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
                case CharacterColorType.Red:
                    foreach (var i in spawnedBalls)
                    {
                        if (i.GetSpawnedBallType() == colorType)
                            balls.Add(i.SpawnPosition());
                    }
                    return balls;
            }
            return null;
        }

        private void Start()
        { 
            SetStartPos();
            BallsPlacement();
        }

        private void Update()
        {
            ResetBall();
        }

        #region GenerateBalls

        public void RemoveBall(CollectableBall collectableBall)
        {
            foreach (var i in spawnedBalls)
            {
                if (collectableBall == i.GetCollectableBall())
                {
                    i.ClearSpawnedBall();
                    respawnBalls.Add(i);
                }
            }
        }

        public void RemoveSpawnedBall(CharacterColorType _characterColorType)
        {
            for (int i = 0; i < ballTypes.Count; i++)
            {
                if(ballTypes[i].type == _characterColorType)
                {
                    ballTypes.Remove(ballTypes[i]);
                }
            }

            for (int i = 0; i < spawnedBalls.Count; i++)
            {
                if(spawnedBalls[i].GetSpawnedBallType() == _characterColorType)
                {
                    if (spawnedBalls[i].GetCollectableBall() != null)
                    {
                        Destroy(spawnedBalls[i].GetCollectableBall().gameObject);
                        spawnedBalls[i].ClearSpawnedBall();
                        if (ballTypes.Count == 0)
                            return;
                        respawnBalls.Add(spawnedBalls[i]);
                    }
                }
            }
        }

        public void GenerateRemovedBall(SpawnedBall spawnedBall)
        {
            if (ballTypes.Count == 0)
                return;

            BallType ballType = ballTypes[Random.Range(0, ballTypes.Count)];
            GameObject createdBall = Instantiate(ballType.ballPrefab, spawnedBall.SpawnPosition(),
                Quaternion.identity);

            createdBall.transform.parent = transform;
            createdBall.transform.position = spawnedBall.SpawnPosition();
            spawnedBall.SetNewSpawnedBall(createdBall.GetComponent<CollectableBall>());
            respawnBalls.Remove(spawnedBall);
        }

        private void ResetBall()
        {
            timer += Time.deltaTime;

            if (timer >= timeToResetBall)
            {
                if (respawnBalls.Count == 0)
                {
                    timer = 0f;
                    return;
                }

                GenerateRemovedBall(respawnBalls[0]);

                timer = 0f;
            }
        }

        #endregion

        #region Placement

        private void SetStartPos()
        {
            startPoint = transform.position;
            zPosition = transform.position.z;
            xPosition = transform.position.x;
        }

        private void BallsPlacement()
        {
            for (int i = 0; i < length; i++)
            {
                xOrder += form;

                if (i % line == 0)
                {
                    zPosition -= 1f;
                    xOrder = 0;
                    position = new Vector3(xPosition, startPoint.y, zPosition);
                }
                else
                {
                    position = new Vector3(xPosition + xOrder, startPoint.y, zPosition);
                }

                BallType ballType = ballTypes[Random.Range(0, ballTypes.Count)];

                GameObject newCollectableBall = Instantiate(ballType.ballPrefab,
                 position, ballType.ballPrefab.transform.rotation);

                newCollectableBall.transform.parent = transform;

                SpawnedBall spawnedBall = new SpawnedBall();
                spawnedBall.SetSpawnedBall(newCollectableBall.GetComponent<CollectableBall>(), position, this);
                spawnedBalls.Add(spawnedBall);
            }
        }

        #endregion
    }
}
