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
    
    public partial class QuestionType
    {
        public QuestionType()
        {
            this.Questions = new HashSet<Questions>();
        }
    
        public int TypeID { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<Questions> Questions { get; set; }
    }
}