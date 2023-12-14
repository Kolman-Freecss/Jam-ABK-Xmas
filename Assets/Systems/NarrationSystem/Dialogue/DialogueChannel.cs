#region

using Systems.NarrationSystem.Dialogue.Data.Nodes;
using UnityEngine;

#endregion

namespace Systems.NarrationSystem.Dialogue
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Narration/Dialogue/Dialogue Channel")]
    public class DialogueChannel : ScriptableObject
    {
        public delegate void DialogueCallback(Data.Dialogue dialogue);
        public delegate void DialogueCallbackWithPath(Data.Dialogue dialogue, bool followingRightPath);

        public DialogueCallback OnDialogueRequested;
        public DialogueCallback OnDialogueStart;
        public DialogueCallbackWithPath OnDialogueEnd;

        public delegate void DialogueNodeCallback(DialogueNode node);

        public DialogueNodeCallback OnDialogueNodeRequested;
        public DialogueNodeCallback OnDialogueNodeStart;
        public DialogueNodeCallback OnDialogueNodeEnd;

        public void RaiseRequestDialogue(Data.Dialogue dialogue)
        {
            OnDialogueRequested?.Invoke(dialogue);
        }

        public void RaiseDialogueStart(Data.Dialogue dialogue)
        {
            OnDialogueStart?.Invoke(dialogue);
        }

        public void RaiseDialogueEnd(Data.Dialogue dialogue, bool followingRightPath)
        {
            OnDialogueEnd?.Invoke(dialogue, followingRightPath);
        }

        public void RaiseRequestDialogueNode(DialogueNode node)
        {
            OnDialogueNodeRequested?.Invoke(node);
        }

        public void RaiseDialogueNodeStart(DialogueNode node)
        {
            OnDialogueNodeStart?.Invoke(node);
        }

        public void RaiseDialogueNodeEnd(DialogueNode node)
        {
            OnDialogueNodeEnd?.Invoke(node);
        }
    }
}
