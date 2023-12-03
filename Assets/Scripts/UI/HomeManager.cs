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
                .AddListener(() =>
                {
                    OnQuitButtonClicked();
                });
            startButton
                .onClick
                .AddListener(() =>
                {
                    OnPlayButtonClicked();
                });
            settingsButton
                .onClick
                .AddListener(() =>
                {
                    OnSettingsButtonClicked();
                });
            creditsButton
                .onClick
                .AddListener(() =>
                {
                    OnCreditsButtonClicked();
                });
        }

        #endregion

        #region Logic

        void OnQuitButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
            Application.Quit();
        }

        void OnPlayButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
            GameManager.Instance.StartGame();
        }

        void OnSettingsButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.Settings);
        }

        void OnCreditsButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
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
