//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GuessWhere.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SavedGames
    {
        public int IDSavedGame { get; set; }
        public int IDGame { get; set; }
        public int IDuser { get; set; }
        public decimal score { get; set; }
        public System.DateTime datePlayed { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
