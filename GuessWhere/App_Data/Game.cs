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
    
    public partial class Game
    {
        public Game()
        {
            this.LeaderBoard = new HashSet<LeaderBoard>();
            this.SavedGames = new HashSet<SavedGames>();
        }
    
        public int IDgame { get; set; }
        public int IDimg1 { get; set; }
        public int IDimg2 { get; set; }
        public int IDimg3 { get; set; }
        public int IDimg4 { get; set; }
        public int IDimg5 { get; set; }
        public int IDimg6 { get; set; }
        public int IDimg7 { get; set; }
    
        public virtual Image Image { get; set; }
        public virtual Image Image1 { get; set; }
        public virtual Image Image2 { get; set; }
        public virtual Image Image3 { get; set; }
        public virtual Image Image4 { get; set; }
        public virtual Image Image5 { get; set; }
        public virtual Image Image6 { get; set; }
        public virtual ICollection<LeaderBoard> LeaderBoard { get; set; }
        public virtual ICollection<SavedGames> SavedGames { get; set; }
    }
}