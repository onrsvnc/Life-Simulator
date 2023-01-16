using Dialogues.Data;
using Dialogues.Logic;
using Dialogues.Nodes;
using Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogues
{
    public class UIDialogueTextBoxController : MonoBehaviour, DialogueNodeVisitor
    {
        [SerializeField] private TextMeshProUGUI m_SpeakerText;
        [SerializeField] private TextMeshProUGUI m_DialogueText;
        [SerializeField] private RectTransform m_ChoicesBoxTransform;
        [SerializeField] private UIDialogueChoiceController m_ChoiceControllerPrefab;
        [SerializeField] private DialogueChannel m_DialogueChannel;
        [SerializeField] private Shop shopkeeper;
        

        private bool m_ListenToInput = false;
        private DialogueNode m_NextNode = null;

        private void Awake()
        {
            m_DialogueChannel.OnDialogueNodeStart += OnDialogueNodeStart;
            m_DialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;

            gameObject.SetActive(false);
            m_ChoicesBoxTransform.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            m_DialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
            m_DialogueChannel.OnDialogueNodeStart -= OnDialogueNodeStart;
        }

        private void Update()
        {
            if (m_ListenToInput && Input.GetButtonDown("Submit"))
            {
                m_DialogueChannel.RaiseRequestDialogueNode(m_NextNode);
            }
        }

        private void OnDialogueNodeStart(DialogueNode node)
        {
            gameObject.SetActive(true);

            m_DialogueText.text = node.DialogueLine.Text;
            m_SpeakerText.text = node.DialogueLine.Speaker.CharacterName;

            node.Accept(this);
        }

        private void OnDialogueNodeEnd(DialogueNode node)
        {
            m_NextNode = null;
            m_ListenToInput = false;
            m_DialogueText.text = "";
            m_SpeakerText.text = "";

            foreach (Transform child in m_ChoicesBoxTransform)
            {
                Destroy(child.gameObject);
            }

            gameObject.SetActive(false);
            m_ChoicesBoxTransform.gameObject.SetActive(false);
        }

        public void Visit(BasicDialogueNode node)
        {
            m_ListenToInput = true;
            m_NextNode = node.NextNode;
        }

        public void Visit(ChoiceDialogueNode node)
        {
            m_ChoicesBoxTransform.gameObject.SetActive(true);

            foreach (DialogueChoice choice in node.Choices)
            {
                UIDialogueChoiceController newChoice = Instantiate(m_ChoiceControllerPrefab, m_ChoicesBoxTransform);
                newChoice.Choice = choice;
            }
        }

        public void Visit(Go2ShopDialogueNode node)
        {
            shopkeeper.Invoke("OpenShop", 1.5f);
            m_ListenToInput = true;
            m_NextNode = node.NextNode;
            Invoke("ShopOpened", 1.5f);
        }

        private void ShopOpened()
        {
            m_DialogueChannel.RaiseRequestDialogueNode(m_NextNode);
        }
    }
}

