﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Entities.Request
{
    public class SupplierAddReq
    {
        public string Name { get; set; }           // Supplier name
        public string ContactName { get; set; }    // Contact person name
        public string Address { get; set; }        // Supplier address
        public string Phone { get; set; }          // Phone number
       
        [EmailAddress(ErrorMessage = "Invalid email address format.")]

        public string Email { get; set; }          // Email address
    }
}
