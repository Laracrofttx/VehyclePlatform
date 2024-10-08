﻿using System.ComponentModel.DataAnnotations;

namespace Vehycle.Data.Models
{
	public class Message
	{
        public Message()
        {
			this.Id = Guid.NewGuid();	
        }

        [Key]
		public Guid Id { get; set; }
		public string UserName { get; set; } = null!;
		public string Content { get; set; } = null!;
		public DateTime Time { get; set; }
		public int ChatId { get; set; } 
		public virtual Chat Chat { get; set; } = null!;
	}
}
