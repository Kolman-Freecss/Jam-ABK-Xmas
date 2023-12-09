#region

using Gameplay.Config;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class HomeManager : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Buttons")]
        [SerializeField]
        private Button quitButton;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button creditsButton;

        #endregion

        #region Init Data

        void Start()
        {
            SubscribeToEvents();
        }

        void SubscribeToEvents()
        {
            quitButton
                .onClick
                .AddListener(() => OnQuitButtonClicked());
            startButton
                .onClick
                .AddListener(() => OnPlayButtonClicked());
            settingsButton
                .onClick
                .AddListener(() => OnSettingsButtonClicked());
            creditsButton
                .onClick
                .AddListener(() => OnCreditsButtonClicked());
        }

        #endregion

        #region Logic

        public void OnQuitButtonClicked()
        {
            Debug.Log("Quit button clicked");
            SoundManager.Instance.PlayButtonClickSound();
            Application.Quit();
        }

        public void OnPlayButtonClicked()
        {
            Debug.Log("Play button clicked");
            SoundManager.Instance.PlayButtonClickSound();
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.InGame_City);
            GameManager.Instance.StartGame();
        }

        public void OnSettingsButtonClicked()
        {
            Debug.Log("Settings button clicked");
            SoundManager.Instance.PlayButtonClickSound();
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.Settings);
        }

        public void OnCreditsButtonClicked()
        {
            Debug.Log("Credits button clicked");
            SoundManager.Instance.PlayButtonClickSound();
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.Credits);
        }

        #endregion

        #region Destructor

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        void UnsubscribeToEvents()
        {
            quitButton.onClick.RemoveListener(OnQuitButtonClicked);
            startButton.onClick.RemoveListener(OnPlayButtonClicked);
            settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            creditsButton.onClick.RemoveListener(OnCreditsButtonClicked);
        }

        #endregion
    }
}
