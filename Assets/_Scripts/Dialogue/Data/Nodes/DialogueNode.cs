using UnityEngine;
using Dialogues.Data;
using Dialogues.Logic;


namespace Dialogues.Nodes
{
    public abstract class DialogueNode : ScriptableObject
    {
        [SerializeField] private NarrationLine m_dialogueLine;

        public NarrationLine DialogueLine => m_dialogueLine;

        public abstract bool CanBeFollowedByNode(DialogueNode node);
        public abstract void Accept(DialogueNodeVisitor visitor);
    }
}

