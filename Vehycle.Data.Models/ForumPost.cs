﻿namespace Vehycle.Data.Models
{
	public class ForumPost
	{
        public ForumPost()
        {
             Id = Guid.NewGuid();

            Users = new HashSet<ApplicationUser>();
        }

        public Guid Id { get; set; }

        public string Theme { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime PostedOn { get; set; }

		public ICollection<ApplicationUser> Users { get; set; }

	}
}
