#region

using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Gameplay.Config
{
    public class SceneTransitionHandler : MonoBehaviour
    {
        #region Inspector Variables

        [SerializeField]
        public SceneStates DefaultScene = SceneStates.Home;

        [SerializeField]
        private float transitionTime = 1f;

        [SerializeField]
        private bool Debug;

        #endregion

        #region Member properties

        public static SceneTransitionHandler Instance { get; private set; }

        private SceneStates m_SceneState;

        private Animator m_SceneTransitionAnimator;

        private bool m_hasAnimator;
        private int m_animIDTransition = 0;

        #endregion

        #region Event Delegates

        public delegate void SceneStateChangedDelegateHandler(SceneStates newState);

        public event SceneStateChangedDelegateHandler OnSceneStateChanged;

        #endregion

        public enum SceneStates
        {
            InitBootstrap,
            Home,
            Home_Starting,
            SplashScreen,
            Credits,
            Settings,
            InGame_City,
            Hell,
            EndGame
        }

        #region InitData

        void Awake()
        {
            m_hasAnimator = TryGetComponent(out m_SceneTransitionAnimator);
            m_SceneTransitionAnimator = GetComponent<Animator>();
            AssignAnimationIDs();
            ManageSingleton();
            SetSceneState(SceneStates.InitBootstrap);
        }

        private void AssignAnimationIDs()
        {
            m_animIDTransition = Animator.StringToHash("Transition");
        }

        private void ManageSingleton()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        void Start()
        {
            if (Debug)
            {
                return;
            }
            if (m_SceneState == SceneStates.InitBootstrap)
            {
                LoadScene(DefaultScene);
            }
        }

        #endregion

        #region Logic

        public void LoadScene(SceneStates sceneState)
        {
            if (m_hasAnimator)
                m_SceneTransitionAnimator.SetTrigger(m_animIDTransition);
            SceneManager.LoadSceneAsync(sceneState.ToString());
            SetSceneState(sceneState);
        }

        private void SetSceneState(SceneStates sceneState)
        {
            m_SceneState = sceneState;
            if (OnSceneStateChanged != null)
            {
                OnSceneStateChanged.Invoke(m_SceneState);
            }

            if (sceneState == SceneStates.InGame_City)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        #endregion

        #region Destructor

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion

        #region Getter & Setter

        public SceneStates GetCurrentSceneState()
        {
            return m_SceneState;
        }

        #endregion
    }
}
