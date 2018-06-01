//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyLibraryApplication.WPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.Copies = new HashSet<Copy>();
            this.Reservations = new HashSet<Reservation>();
            this.Authors = new HashSet<Author>();
        }
    
        public int Id { get; set; }
        public string Signatory { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> Pages { get; set; }
        public string Edition { get; set; }
        public Nullable<int> PublicationYear { get; set; }
        public string Description { get; set; }
        public int LibraryId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> LanguageId { get; set; }
        public Nullable<int> PublisherId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        public virtual Library Library { get; set; }
        public virtual Publisher Publisher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Copy> Copies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Authors { get; set; }
    }
}