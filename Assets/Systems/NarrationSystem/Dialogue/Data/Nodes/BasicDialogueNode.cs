#region

using UnityEngine;

#endregion

namespace Systems.NarrationSystem.Dialogue.Data.Nodes
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Node/Basic")]
    public class BasicDialogueNode : DialogueNode
    {
        [SerializeField]
        private DialogueNode m_NextNode;

        [SerializeField]
        private bool m_correctPath = false;
        public DialogueNode NextNode => m_NextNode;

        public override bool CanBeFollowedByNode(DialogueNode node)
        {
            return m_NextNode == node;
        }

        public override void Accept(IDialogueNodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override bool GetFollowingRightPath()
        {
            return m_correctPath;
        }
    }
}
