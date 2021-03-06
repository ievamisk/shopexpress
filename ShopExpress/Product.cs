//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopExpress
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int productID { get; set; }
        public string genderCategory { get; set; }
        public int categoryID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public double price { get; set; }
        public string size { get; set; }
        public string picture { get; set; }
        public int userID { get; set; }
        public int colorID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual Color Color { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
