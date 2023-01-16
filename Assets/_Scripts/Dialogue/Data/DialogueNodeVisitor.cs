

using Dialogues.Nodes;

namespace Dialogues.Data
{
    public interface DialogueNodeVisitor
    {
        void Visit(BasicDialogueNode node);
        void Visit(ChoiceDialogueNode node);
        void Visit(Go2ShopDialogueNode node);
    }
}

