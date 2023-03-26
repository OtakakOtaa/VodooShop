using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;
using JetBrains.Annotations;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public class DialogueParser : TableParser<DialogueParser.Dialogue, DialogueParser.DialogueTable>
    {
        protected override void FillEntity(string field, Dialogue entity, string key)
        {
            switch (key)
            {
                case DialogueTable.Id:
                    entity.Id = field;
                    break;
                case DialogueTable.Speak:
                    entity.Speak = RestoreTextWithComma(field);
                    break;
                case DialogueTable.PromptSpeak:
                    entity.PromptSpeak = RestoreTextWithComma(field);
                    break;
            }
        }

        protected override bool IsEntityFilled(Dialogue entity)
            => entity.Id is not null && entity.PromptSpeak is not null && entity.Speak is not null;

        public sealed class DialogueTable : TableTemplate
        {
            public const string Id = "id";
            public const string Speak = "speak";
            public const string PromptSpeak = "what";

            public DialogueTable()
                => IdKey = Id;

            public override bool HasBeenDetected => Keys.ContainsKey(Id) &&
                                                    Keys.ContainsKey(PromptSpeak) &&
                                                    Keys.ContainsKey(Speak);
            
            public override bool ThisReadKey(string comparedField)
                => comparedField is Id or Speak or PromptSpeak;
        }

        public sealed class Dialogue
        {
            [CanBeNull] public string Id;
            [CanBeNull] public string Speak;
            [CanBeNull] public string PromptSpeak;
        }
    }
}