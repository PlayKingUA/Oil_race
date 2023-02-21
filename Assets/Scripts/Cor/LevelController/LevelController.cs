using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using BlueStellar.Cor.SDK;

namespace BlueStellar.Cor
{
    public class LevelController : MonoBehaviour
    {
        #region Singelton

        public static LevelController Instance;

        private void Awake()
        {
            Instance = this;
            NewLevel();
        }

        #endregion

        #region Variables

        [SerializeField] LevelSpawner _levelSpawner;
        [SerializeField] LevelsProgress _levelsProgress;
        [SerializeField] AppMetricaAnalytics _analytics;
        [SerializeField] Text textLvlNumber;
        [SerializeField] private int lvlNumber;
        [SerializeField] private bool isEditor;
        private int lvlIndex;
        private bool isLevelEnd;

        #endregion

        #region LevelEvents

        [HideInInspector]
        public UnityEvent OnLevelStart;
        [HideInInspector]
        public UnityEvent OnLevelFailed;
        [HideInInspector]
        public UnityEvent OnLevelCompleted;

        #endregion

        public int LvlNumber()
        {
            return lvlNumber;
        }

        public int LvlIndex()
        {
            return lvlIndex;
        }

        #region LevelActions

        public void LevelStart()
        {
            _analytics.NewAttempt();
            _analytics.LevelStartEvent();
            OnLevelStart.Invoke();
            UIManager.Instance.TutorialScreen(false);   
            UIManager.Instance.StartScreen(false);
        }

        public void LevelCompleted()
        {
            if (isLevelEnd) 
                return;

            isLevelEnd = true;
            _analytics.LevelFinishEvent("win");
            _analytics.NewAttempt();
            LevelEnd();
            OnLevelCompleted?.Invoke();
            StartCoroutine(IE_WinCondtional());
        }

        public void LevelFailed()
        {
            if (isLevelEnd)
                return;

            isLevelEnd = true;

            _analytics.LevelFinishEvent("lose");
            _analytics.NewAttempt();
            LevelEnd();
            OnLevelFailed.Invoke();
            StartCoroutine(IE_LoseConditional());
        }

        private void LevelEnd()
        {
            UIManager.Instance.JoystickScreen(false);
            UIManager.Instance.SettingsButtonScreen(false);
            UIManager.Instance.SettingsScreen(false);
            UIManager.Instance.MoneyScreen(false);
        }

        private void NewLevel()
        {
            LoadSave();
            if (isEditor)
                return;

            _levelSpawner.SpawnLevel(lvlIndex);
            _levelsProgress.CheckLevelsProgress();
            textLvlNumber.text = "Level " + lvlNumber;
        }

        public void NextLevel()
        {
            lvlIndex++;
            lvlNumber++;
            if (lvlIndex >= 15)
            {
                _analytics.NewLevelLoop();
                lvlIndex = 0;
            }
            _levelsProgress.ProgressUp();
            _analytics.NewLevel();
            Save();
        }

        #endregion

        #region LevelConditional

        private IEnumerator IE_WinCondtional()
        {
            yield return new WaitForSeconds(2.5f);

            UIManager.Instance.WinScreen(true);
        }

        private IEnumerator IE_LoseConditional()
        {
            yield return new WaitForSeconds(2f);

            UIManager.Instance.LoseScreen(true);
        }

        #endregion

        #region Load&Save

        private void LoadSave()
        {
            lvlIndex = ES3.Load("lvlIndex", lvlIndex);
            lvlNumber = ES3.Load("lvlNumber", lvlNumber);
        }

        private void Save()
        {
            ES3.Save("lvlIndex", lvlIndex);
            ES3.Save("lvlNumber", lvlNumber);
        }

        #endregion

        private void OnApplicationQuit()
        {
            _analytics.LevelFinishEvent("game_closed");
        }
    }
}
