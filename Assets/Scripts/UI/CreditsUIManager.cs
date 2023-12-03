#region

using Gameplay.Config;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class CreditsUIManager : MonoBehaviour
    {
        #region Inspector Variables

        [Header("Buttons")]
        [SerializeField]
        private Button backButton;

        #endregion

        #region Init Data

        void Start()
        {
            SubscribeToEvents();
        }

        void SubscribeToEvents()
        {
            backButton
                .onClick
                .AddListener(() =>
                {
                    OnBackButtonClicked();
                });
        }

        #endregion

        #region Logic

        void OnBackButtonClicked()
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
            backButton.onClick.RemoveAllListeners();
        }

        #endregion
    }
}
