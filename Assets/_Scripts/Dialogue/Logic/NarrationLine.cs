using UnityEngine;

namespace Dialogues.Logic
{
    [CreateAssetMenu(menuName = "Narration/New Line")]
    public class NarrationLine : ScriptableObject
    {
        [SerializeField] private NarrationCharacter m_speaker;
        [SerializeField] private string m_text;

        public NarrationCharacter Speaker => m_speaker;
        public string Text => m_text;
    }

}

