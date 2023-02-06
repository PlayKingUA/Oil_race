using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlueStellar.Cor
{
    public class ButtonManager : MonoBehaviour
    {
        public void StartLevel()
        {
            LevelController.Instance.LevelStart();
        }

        public void Continue()
        {
            UIManager.Instance.MoneyScreen(true);
            UIManager.Instance.RewardScreen(true);
            UIManager.Instance.BonusScreen(false);
        }

        public void RestartLevel()
        {
            MoneyWallet.Instance.MoneyPlus(50);
            SceneLoader(0);
        }

        public void NextLevel()
        {
            MoneyWallet.Instance.MoneyPlus(100);
            LevelController.Instance.NextLevel();
            SceneLoader(0);
        }

        private void SceneLoader(int indexScene)
        {
            SceneManager.LoadScene(indexScene);
        }
    }
}
