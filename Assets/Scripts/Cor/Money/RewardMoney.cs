using System.Collections;
using UnityEngine;

namespace BlueStellar.Cor
{
    public class RewardMoney : MonoBehaviour
    {
        [SerializeField] ButtonManager buttonManager;
        [SerializeField] GameObject prefabMoney;
        [SerializeField] Transform[] pointSpawn;
        [SerializeField] GameObject button;
        [SerializeField] int rewardMoney;

        private int index;

        public void SpawnMoney()
        {
            buttonManager.NextLevel();
            button.SetActive(false);
        }

        private IEnumerator IE_Win()
        {
            yield return new WaitForSeconds(3.2f);

            buttonManager.NextLevel();
        }
    }
}
