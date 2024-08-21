using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoAI
{
    public class MementoAIPhishing
    {
        public string MailTemplate { get; set; }
        MailRedoTaker RT;
        MailUndoTaker UT;
        public MementoAIPhishing()
        {
            RT = new();
            UT = new();
        }
        public void Save()
        {
            UT.Mail = new AIMailMemento() { MailTemplate = MailTemplate };
        }
        public void Undo()
        {
            RT.Mail = new AIMailMemento() { MailTemplate = this.MailTemplate };
            this.MailTemplate = UT.Mail.MailTemplate;
        }
        public void Redo()
        {
            UT.Mail = new AIMailMemento() { MailTemplate = this.MailTemplate };
            this.MailTemplate = RT.Mail.MailTemplate;
        }

        public class MailUndoTaker
        {
            Stack<AIMailMemento> mementos = new Stack<AIMailMemento>();
            public AIMailMemento Mail
            {
                get => mementos.TryPop(out AIMailMemento result) ? result : new AIMailMemento();
                set
                {
                    mementos.Push(value);
                }
            }
        }
        public class MailRedoTaker
        {
            Stack<AIMailMemento> mementos = new Stack<AIMailMemento>();
            public AIMailMemento Mail
            {
                get => mementos.TryPop(out AIMailMemento result) ? result : new AIMailMemento();
                set
                {
                    mementos.Push(value);
                }
            }
        }
        public class AIMailMemento
        {
            public string MailTemplate { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
