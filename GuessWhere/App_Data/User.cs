//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GuessWhere.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.LeaderBoard = new HashSet<LeaderBoard>();
            this.SavedGames = new HashSet<SavedGames>();
            this.RegisteredUser = new HashSet<RegisteredUser>();
        }
    
        public int IDuser { get; set; }
        public string username { get; set; }
    
        public virtual ICollection<LeaderBoard> LeaderBoard { get; set; }
        public virtual ICollection<SavedGames> SavedGames { get; set; }
        public virtual ICollection<RegisteredUser> RegisteredUser { get; set; }
    }
}