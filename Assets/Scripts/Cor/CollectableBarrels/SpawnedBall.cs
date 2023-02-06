using UnityEngine;

namespace BlueStellar.Cor
{
    public class SpawnedBall
    {
        CollectableBarrel _collectableBall;
        CharacterColorType _type;
        Vector3 _spawnPosition;

        public CollectableBarrel GetCollectableBall()
        {
            return _collectableBall;
        }

        public CharacterColorType GetSpawnedBallType()
        {
            return _type;
        }

        public Vector3 SpawnPosition()
        {
            return _spawnPosition;
        }

        public void SetSpawnedBall(CollectableBarrel ball, Vector3 pos, CollectableBarrelField collectableBallsField)
        {
            _collectableBall = ball;
            _type = _collectableBall.Type();
            _spawnPosition = pos;
        }

        public void SetNewSpawnedBall(CollectableBarrel collectableBall)
        {
            _collectableBall = collectableBall;
            _type = _collectableBall.Type();
        }

        public void ClearSpawnedBall()
        {
            _collectableBall = null;
        }
    }
}
