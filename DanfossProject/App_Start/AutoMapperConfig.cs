using AutoMapper;
using DanfossProject.Data.Models.CreateModel;
using DanfossProject.Data.Models.Entities;
using DanfossProject.Data.Models.ReturnModel;
using DanfossProject.Data.Models.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DanfossProject.App_Start
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