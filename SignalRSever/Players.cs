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
    
    public partial class Players
    {
        public Players()
        {
            this.listMatches = new HashSet<listMatches>();
            this.listMatches1 = new HashSet<listMatches>();
            this.Histories = new HashSet<Histories>();
        }
    
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public int PlayerLevel { get; set; }
        public int PlayerPoint { get; set; }
        public Nullable<int> Win { get; set; }
        public Nullable<int> Lose { get; set; }
    
        public virtual ICollection<listMatches> listMatches { get; set; }
        public virtual ICollection<listMatches> listMatches1 { get; set; }
        public virtual ICollection<Histories> Histories { get; set; }
    }
}