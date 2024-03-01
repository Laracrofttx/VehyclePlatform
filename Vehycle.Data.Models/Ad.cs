﻿namespace Vehycle.Data.Models
{
	public class Ad
	{
        public Ad()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime PostedOn { get; set; }

        public ApplicationUser PostedBy { get; set; } = null!;
	}
}