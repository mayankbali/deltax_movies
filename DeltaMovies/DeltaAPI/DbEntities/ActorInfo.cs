//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeltaMovies.DeltaAPI.DbEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActorInfo
    {
        public ActorInfo()
        {
            this.ActorInMovies = new HashSet<ActorInMovie>();
        }
    
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public System.DateTime DOB { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<ActorInMovie> ActorInMovies { get; set; }
    }
}