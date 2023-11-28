#region

using UnityEngine;

#endregion

namespace Gameplay.Config
{
    public class SoundManager : MonoBehaviour
    {
        #region Member Variables

        public static SoundManager Instance { get; private set; }

        #endregion

        #region InitData

        private void Awake()
        {
            ManageSingleton();
        }


        void ManageSingleton()
        {
            if (Instance != null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion
    }
}