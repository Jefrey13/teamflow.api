using AutoMapper;
using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Dtos.ResponseDtos;
using teamflow.API.Models;

namespace teamflow.API.Helpers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterRequestDto, User>();
            CreateMap<UserUpdateRequestDto, User>();
            CreateMap<User, UserResponseDto>();

            CreateMap<ProjectCreateRequestDto, Project>();
            CreateMap<ProjectUpdateRequestDto, Project>();
            CreateMap<Project, ProjectResponseDto>();
            CreateMap<Project, ProjectSummaryDto>();

            CreateMap<ProjectTaskCreateRequestDto, ProjectTask>();
            CreateMap<ProjectTaskUpdateRequestDto, ProjectTask>();
            CreateMap<ProjectTask, ProjectTaskResponseDto>();

            CreateMap<TeamCreateRequestDto, Team>();
            CreateMap<TeamUpdateRequestDto, Team>();
            CreateMap<Team, TeamResponseDto>();

            CreateMap<ProjectMemberCreateRequestDto, ProjectMember>();
            CreateMap<ProjectMember, ProjectMemberResponseDto>();

            CreateMap<ProjectFileUploadRequestDto, ProjectFile>();
            CreateMap<ProjectFile, ProjectFileResponseDto>();

            CreateMap<Notification, NotificationDto>();
        }

    }
}
