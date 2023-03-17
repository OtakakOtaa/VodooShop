using System.Collections.Generic;
using System.Linq;
using CodeBase.Editor.OriginGameConfig.TableParsers.ParserTemplate;
using JetBrains.Annotations;

namespace CodeBase.Editor.OriginGameConfig.TableParsers.Tables
{
    public class DialogueParser : TableParser<DialogueParser.Dialogue, DialogueParser.DialogueTable>
    {
        protected override void FillEntity(string field, Dialogue entity, string key)
        {
            switch (field)
            {
                case DialogueTable.Id:
                    entity.Id = field;
                    break;
                default:
                    entity.Chapters.Add(field);
                    break;
            }
        }

        protected override bool IsEntityFilled(Dialogue entity)
            => entity.Id is not null && entity.Chapters.Count > 0;

        public sealed class DialogueTable : TableTemplate
        {
            public const string Id = "id";
            public const string Chapter1 = "chapter_1";
            public const string Chapter2 = "chapter_2";
            public const string Chapter3 = "chapter_3";
            public const string Chapter4 = "chapter_4";
            public const string Chapter5 = "chapter_5";

            public DialogueTable()
                => IdKey = Id;

            public override bool HasBeenDetected => Keys.ContainsKey(Id) &&
                                                    Keys.ContainsKey(Chapter1) &&
                                                    Keys.ContainsKey(Chapter2) &&
                                                    Keys.ContainsKey(Chapter3) &&
                                                    Keys.ContainsKey(Chapter4) &&
                                                    Keys.ContainsKey(Chapter5);

            public override bool ThisReadKey(string comparedField)
                => comparedField is Id or Chapter1 or Chapter2 or Chapter3 or Chapter4;
        }

        public sealed class Dialogue
        {
            [CanBeNull] public string Id;
            public readonly List<string> Chapters = Enumerable.Empty<string>().ToList();
        }
    }
}