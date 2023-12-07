#region

using UnityEngine;

#endregion

namespace Data.SO.ThrowableItem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ThrowableItem")]
    public class ThrowableItem : ScriptableObject
    {
        [SerializeField]
        private string m_ItemName;

        [SerializeField]
        private AudioClip m_ItemThrownSound;

        [SerializeField]
        private AudioClip m_ItemPickedUpSound;

        [SerializeField]
        private GameObject m_ItemPrefab;

        private GameObject m_InstancedItemPrefab;

        public string ItemName => m_ItemName;
        public AudioClip ItemThrownSound => m_ItemThrownSound;
        public AudioClip ItemPickedUpSound => m_ItemPickedUpSound;
        public GameObject ItemPrefab => m_ItemPrefab;

        public GameObject InstancedItemPrefab
        {
            get => m_InstancedItemPrefab;
            set => m_InstancedItemPrefab = value;
        }
    }
}
