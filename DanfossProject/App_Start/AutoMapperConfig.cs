using AutoMapper;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;

namespace DanfossProject
{
	public class AutoMapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(cfg => {
				cfg.CreateMap<BuildingModel, BuildingReturnModel>();
				cfg.CreateMap<BuildingCreateModel, BuildingModel>();
				cfg.CreateMap<BuildingUpdateModel, BuildingModel>();
				cfg.CreateMap<WaterMeterModel, WaterMeterReturnModel>();
				cfg.CreateMap<WaterMeterUpdateModel, WaterMeterModel>();
				cfg.CreateMap<WaterMeterUpdateModel, WaterMeterModel>();
			});
		}
	}
}