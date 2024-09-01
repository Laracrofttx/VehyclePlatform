﻿namespace Vehycles.Services
{
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Http;
	using Microsoft.EntityFrameworkCore;
	using Vehycle.Data.Models;
	using Vehycle.Web.ViewModels.Vehycles;
	using Vehycles.Data;
	using Vehycles.Services.Interfaces;
	public class VehycleService : IVehycleService
	{
		private readonly VehyclePlatformDbContext dbContext;
		public VehycleService(VehyclePlatformDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task AddVehycleAsync(VehycleFormModel vehycles, List<IFormFile> file)
		{
			var vehycle = new Vehycle
			{
				Id = vehycles.Id,
				Brand = vehycles.Brand,
				Model = vehycles.Model,
				Year = vehycles.Year,
				Price = vehycles.Price,
				Color = vehycles.Color,
				Doors = vehycles.Doors,
				Condition = vehycles.Condition,
				HorsePower = vehycles.HorsePower,
				Transmition = vehycles.Transmition,
				EuStandart = vehycles.EuroStrandart,
				VehycleInfo = vehycles.VehycleInfo,
				VehycleType = vehycles.VehycleType,
				CategoryId = vehycles.CategoryId,
			};

            foreach (var photo in file)
            {
                using (var memoryStream = new MemoryStream())
                {
                    var lastVehycleId = await this.dbContext
                        .Vehycles
                        .OrderByDescending(c => c.Id)
                        .FirstOrDefaultAsync();

                    var fileExtension = Path.GetExtension(photo.FileName);
                    var fileName = Path.GetFileName(photo.FileName);
                    var newFile = new Photo()
                    {
                        Id = vehycles.Id,
                        FileName = fileName,
                        FileType = fileExtension,
                        FormFile = memoryStream.ToArray(),
                        VehycleId = lastVehycleId!.Id
                    };
                    await dbContext.Photos.AddAsync(newFile);
                    await dbContext.SaveChangesAsync();
                }
            }
            await dbContext.Vehycles.AddAsync(vehycle);
			await dbContext.SaveChangesAsync();
		}
		public async Task<IEnumerable<VehycleCategoriesViewModel>> AllVehycleCategoriesAsync()
		{
			var categories = await this.dbContext
				.Categories
				.AsNoTracking()
				.Select(c => new VehycleCategoriesViewModel()
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return categories;
		}
		//public async Task UploadImageAsync(VehycleFormModel model, List<IFormFile> file)
		//{
		//	foreach (var photo in file)
		//	{
		//		using (var memoryStream = new MemoryStream())
		//		{
		//			var lastVehycleId = await this.dbContext
		//				.Vehycles
		//				.OrderByDescending(c => c.Id)
		//				.FirstOrDefaultAsync();

		//			var fileExtension = Path.GetExtension(photo.FileName);
		//			var fileName = Path.GetFileName(photo.FileName);
		//			var newFile = new Photo()
		//			{
		//				Id = model.Id,
		//				FileName = fileName,
		//				FileType = fileExtension,
		//				FormFile = memoryStream.ToArray(),
		//				VehycleId = lastVehycleId!.Id
		//			};
		//			await dbContext.Photos.AddAsync(newFile);
		//			await dbContext.SaveChangesAsync();
		//		}
		//	}
		//}
	}
}
