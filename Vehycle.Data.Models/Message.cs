﻿namespace Vehycle.Data.Models
{
	public class Message
	{

		public string UserName { get; set; } = null!;

		public string Content { get; set; } = null!;

		public DateTime Time { get; set; }

		public string ChatId { get; set; } = null!;

		public Chat Chat { get; set; } = null!;

	}
}
