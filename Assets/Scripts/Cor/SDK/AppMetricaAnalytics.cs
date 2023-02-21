using UnityEngine;

namespace BlueStellar.Cor.SDK
{
    public class AppMetricaAnalytics : MonoBehaviour
    {
        #region Variables

        [SerializeField] private int _levelNumber;
        [SerializeField] private int _levelName;
        [SerializeField] private int _levelCount;
        [SerializeField] private int _levelLoop;
        [SerializeField] private int _time;

        #endregion

        private void Start()
        {
            Load();
        }

        public void LevelStartEvent()
        {
            _time = (int)Time.time;

            var eventVariables = "{\"level_number\":\"" + _levelNumber + "\"," +
                               "\"level_name\":\"" + _levelName + "\"," +
                               "\"level_count\":\"" + _levelCount + "\"," +
                               "\"level_diff\":\"" + "easy" + "\"," +
                               "\"level_loop\":\"" + _levelLoop + "\"," +
                               "\"level_random\":\"" + 0 + "\"," +
                               "\"level_type\":\"normal\"," +
                               "}";

            AppMetrica.Instance.ReportEvent("level_start", eventVariables);
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void LevelFinishEvent(string _levelResult)
        {
            _time = (int)(Time.time - _time);

            var eventVariables = "{\"level_number\":\"" + _levelNumber + "\"," +
                                  "\"level_name\":\"" + _levelName + "\"," +
                                  "\"level_count\":\"" + _levelCount + "\"," +
                                   "\"level_diff\":\"" + "easy" + "\"," +
                                  "\"level_loop\":\"" + _levelLoop + "\"," +
                                  "\"level_random\":\"" + 0 + "\"," +
                                  "\"level_type\":\"normal\"," +
                                  "\"result\":\"" + _levelResult + "\"," +
                                  "\"time\":\"" + _time + "\"," +
                                  "\"continue\":\"0\"," +
                                  "}";

            AppMetrica.Instance.ReportEvent("level_finish", eventVariables);
            AppMetrica.Instance.SendEventsBuffer();
        }

        public void NewLevel()
        {
            _levelNumber++;
            _levelName++;
            Save();
        }

        public void NewAttempt()
        {
            _levelCount++;
            Save();
        }

        public void NewLevelLoop()
        {
            _levelLoop++;
            _levelName = 1;
            Save();
        }

        #region Load&Save

        private void Load()
        {
            _levelNumber = ES3.Load("_levelNumber", _levelNumber);
            _levelName = ES3.Load("_levelName", _levelName);
            _levelCount = ES3.Load("_levelCount", _levelCount);
            _levelLoop = ES3.Load("_levelLoop", _levelLoop);
        }

        private void Save()
        {
            ES3.Save("_levelNumber", _levelNumber);
            ES3.Save("_levelName", _levelName);
            ES3.Save("_levelCount", _levelCount);
            ES3.Save("_levelLoop", _levelLoop);
        }

        #endregion
    }
}
