﻿namespace Vehycle.Data.Models
{
	using System.ComponentModel.DataAnnotations;
	public class ForumPost
	{
		public ForumPost()
		{
			this.Id = Guid.NewGuid();
			this.Users = new HashSet<ApplicationUser>();
		}

		[Key]
		public Guid Id { get; set; }
		[Required]
		public string Theme { get; set; } = null!;
		[Required]
		public string Content { get; set; } = null!;
		public DateTime PostedOn { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}
}
