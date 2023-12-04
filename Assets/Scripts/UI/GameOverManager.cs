#region

using Gameplay.Config;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Buttons")]
        [SerializeField]
        private Button homeButton;

        [SerializeField]
        private Button restartButton;

        #endregion

        #region Init Data

        void Start()
        {
            SubscribeToEvents();
        }

        void SubscribeToEvents()
        {
            homeButton
                .onClick
                .AddListener(() =>
                {
                    OnHomeButtonClicked();
                });
            restartButton
                .onClick
                .AddListener(() =>
                {
                    OnRestartButtonClicked();
                });
        }

        #endregion

        #region Logic

        void OnRestartButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound();
            GameManager.Instance.RestartGame();
        }

        void OnHomeButtonClicked()
        {
            SoundManager.Instance.PlayButtonClickSound();
            SceneTransitionHandler.Instance.LoadScene(SceneTransitionHandler.SceneStates.Home);
        }

        #endregion

        #region Destructor

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        void UnsubscribeToEvents()
        {
            homeButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}
