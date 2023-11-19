﻿using AutoMapper;
using HomeApi.Confugurations;
using HomeApi.Contracts.Devices;
using HomeApi.Contracts.Home;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Room;
using HomeApi.Data.Models;

namespace HomeApi.MappingProfiles
{
    public class MappingProfile: Profile 
    {
        /// <summary>
        /// В конструкторе настроим соответствие сущностей при маппинге
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                    opt => opt.MapFrom(src => src.Address));

            // Маппинг запросов:
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
            CreateMap<Room, RoomView>();
        }
    }
}
