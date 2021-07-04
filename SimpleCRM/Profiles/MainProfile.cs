using System.Collections.Generic;
using AutoMapper;
using SimpleCRM.DAL.Entities;
using SimpleCRM.Models;

namespace SimpleCRM.Profiles
{
	public class MainProfile : Profile
	{
		public MainProfile()
		{
			CreateMap<TaskEntity, TaskModel>();
			CreateMap<TaskModel, TaskEntity>();
			//CreateMap<List<SubtaskEntity>, List<SubtaskModel>>();
			//CreateMap<List<SubtaskModel>, List<SubtaskEntity>>();
			//CreateMap<SubtaskEntity, SubtaskModel>();
			//CreateMap<SubtaskModel, SubtaskEntity>();
			CreateMap<StateEntity, StateModel>();
			CreateMap<StateModel, StateEntity>();
			//CreateMap<List<TaskEntity>, List<TaskModel>>();
			//CreateMap<List<TaskModel>, List<TaskEntity>>();
		}
	}
}
