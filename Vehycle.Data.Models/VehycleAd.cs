﻿namespace Vehycle.Data.Models
{
	public class VehycleAd
	{

        public Guid VehycleId { get; set; }

		public Vehycle Vehycles { get; set; } = null!;

		public Guid AdId { get; set; }

		public Ad Ads { get; set; } = null!;

	}
}
