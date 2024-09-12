namespace PIM.Entities
{
    public class Supplier
    {
            public int Id { get; set; }                // Primary key
            public string Name { get; set; }           // Supplier name
            public string ContactName { get; set; }    // Contact person name
            public string Address { get; set; }        // Supplier address
            public string Phone { get; set; }          // Phone number
            public string Email { get; set; }          // Email address
            public DateTime Created { get; set; } = DateTime.Now;  // Creation date
            public DateTime? Updated { get; set; }     // Last update date
            public bool IsActive { get; set; } = true; // Is the supplier active
        
    }
}
