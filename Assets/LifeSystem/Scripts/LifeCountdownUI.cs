using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LifeSystem
{
    public class LifeCountdownUI : MonoBehaviour
    {
        public LifeManager LifeManager;
        [SerializeField] private TextMeshProUGUI _lifeText;
        [SerializeField] private TextMeshProUGUI _countdownText;

        private void Update()
        {
            _lifeText.text = "Lives: " + LifeManager.CurrentLives();

            if (!LifeManager.IsLivesFull())
            {
                var timeRemaining = LifeManager.TimeUntilNextLifeTimeSpan();
                _countdownText.text = $"Next life in: {timeRemaining.Minutes:D2}:{timeRemaining.Seconds:D2}";
            }
            else
            {
                _countdownText.text = "Lives Full";
            }
        }
    }
}
