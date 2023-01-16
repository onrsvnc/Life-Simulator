using UnityEngine;


namespace Dialogues.Logic
{
    [CreateAssetMenu(menuName = "Narration/Character")]
    public class NarrationCharacter : ScriptableObject
    {
        [SerializeField] private string m_characterName;

        public string CharacterName => m_characterName;
    }
}

