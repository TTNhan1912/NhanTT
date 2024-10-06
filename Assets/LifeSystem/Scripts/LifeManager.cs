using UnityEngine;
using System;
using Framework;

namespace LifeSystem
{
    public class LifeManager : MonoBehaviour
    {
        [SerializeField] private int _maxLives = 5;
        [SerializeField] private float _timeBetweenLives = 300f;
        [SerializeField , ReadOnly] private int _currentLives;

        private DateTime _lastLifeUpdateTime;
        private const string LIVES_KEY = "CurrentLives";
        private const string LAST_LIFE_UPDATE_TIME_KEY = "LastLifeUpdateTime";

        #region UNITY
        private void Start()
        {
            LoadLifeData();
            CalculateLives();
        }


        private void Update()
        {
            if (_currentLives >= _maxLives) return;
            var timePassed = DateTime.Now - _lastLifeUpdateTime;
            if (!(timePassed.TotalSeconds >= _timeBetweenLives)) return;
            int livesRecovered = Mathf.FloorToInt((float)timePassed.TotalSeconds / _timeBetweenLives);
            _currentLives = Mathf.Min(_maxLives, _currentLives + livesRecovered);

            _lastLifeUpdateTime = DateTime.Now - TimeSpan.FromSeconds(timePassed.TotalSeconds % _timeBetweenLives);
            SaveLifeData();
        }



        #endregion


        #region PUBLIC API

        public int CurrentLives() => _currentLives;


        public bool IsLivesFull() => _currentLives >= _maxLives;


        public TimeSpan TimeUntilNextLifeTimeSpan()
        {
            return TimeSpan.FromSeconds(TimeUntilNextLife());
        }

        [ButtonMethod]
        public void UseLife()
        {
            if (_currentLives > 0)
            {
                _currentLives--;
                SaveLifeData();
            }
            else
            {
                Debug.Log("No lives left!");
            }
        }


        #endregion


        #region PRIVATE

        private float TimeUntilNextLife()
        {
            if (_currentLives >= _maxLives)
            {
                return 0;
            }

            var timePassed = DateTime.Now - _lastLifeUpdateTime;
            return (float)(_timeBetweenLives - timePassed.TotalSeconds);
        }


      


        private void CalculateLives()
        {
            if (_currentLives >= _maxLives) return;
            var timePassed = DateTime.Now - _lastLifeUpdateTime;
            var livesRecovered = Mathf.FloorToInt((float)timePassed.TotalSeconds / _timeBetweenLives);
            _currentLives = Mathf.Min(_maxLives, _currentLives + livesRecovered);

            if (_currentLives < _maxLives)
            {
                _lastLifeUpdateTime = DateTime.Now - TimeSpan.FromSeconds(timePassed.TotalSeconds % _timeBetweenLives);
            }
            else
            {
                _lastLifeUpdateTime = DateTime.Now;
            }

            SaveLifeData();
        }

        #endregion


        #region SAVELOAD

       

        private void SaveLifeData()
        {
            PlayerPrefs.SetInt(LIVES_KEY, _currentLives);
            PlayerPrefs.SetString(LAST_LIFE_UPDATE_TIME_KEY, _lastLifeUpdateTime.ToString());
            PlayerPrefs.Save();
        }


        private void LoadLifeData()
        {
            _currentLives = PlayerPrefs.GetInt(LIVES_KEY, _maxLives);
            _lastLifeUpdateTime = DateTime.Parse(PlayerPrefs.GetString(LAST_LIFE_UPDATE_TIME_KEY, DateTime.Now.ToString()));
        }

        #endregion
    }

}