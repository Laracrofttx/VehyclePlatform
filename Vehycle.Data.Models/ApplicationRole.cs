﻿using Microsoft.AspNetCore.Identity;
using Vehycle.Data.Models.Common;

namespace Vehycle.Data.Models
{
    public class ApplicationRole : IdentityRole<Guid>, IAuditable
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid();
        }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
