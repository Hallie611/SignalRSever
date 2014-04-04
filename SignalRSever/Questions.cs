//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SignalRSever
{
    using System;
    using System.Collections.Generic;
    
    public partial class Questions
    {
        public Questions()
        {
            this.AnswerFillBlanks = new HashSet<AnswerFillBlanks>();
            this.AnswerFindBugs = new HashSet<AnswerFindBugs>();
            this.AnswerMultiChoices = new HashSet<AnswerMultiChoices>();
            this.Histories = new HashSet<Histories>();
        }
    
        public int QuestionID { get; set; }
        public Nullable<int> QuestionDif { get; set; }
        public string SRC { get; set; }
    
        public virtual ICollection<AnswerFillBlanks> AnswerFillBlanks { get; set; }
        public virtual ICollection<AnswerFindBugs> AnswerFindBugs { get; set; }
        public virtual ICollection<AnswerMultiChoices> AnswerMultiChoices { get; set; }
        public virtual ICollection<Histories> Histories { get; set; }
        public virtual QuestionType QuestionType { get; set; }
    }
}
