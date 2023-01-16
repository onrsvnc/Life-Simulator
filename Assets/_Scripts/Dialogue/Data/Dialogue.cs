using Dialogues.Nodes;
using UnityEngine;

namespace Dialogues.Data

{
    [CreateAssetMenu(menuName = "Narration/Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject 
    {
        [SerializeField] private DialogueNode m_FirstNode;

        public DialogueNode FirstNode => m_FirstNode;
        
    }
    
}