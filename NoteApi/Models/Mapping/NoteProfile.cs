using AutoMapper;
using NoteApi.Models.Data;
using NoteApi.Models.Entities;
using NoteApi.Models.Request;

namespace NoteApi.Models.Mapping
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {

            CreateMap<NoteRequest, NoteData>();
            CreateMap<PathWrapperRequest, PathWrapperData>();
            CreateMap<PointRequest, PointData>();
            CreateMap<NoteData, Note>();
            CreateMap<PathWrapperData, PathWrapper>();
            CreateMap<PointData, Point>();
            CreateMap<Note, NoteData>();
            CreateMap<PathWrapper, PathWrapperData>();
            CreateMap<Point, PointData>();
        }
    }
}
