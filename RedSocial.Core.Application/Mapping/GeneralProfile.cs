using AutoMapper;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Friend;
using RedSocial.Core.Application.ViewModels.Publication;
using RedSocial.Core.Application.ViewModels.User;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Mapping
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<User, UserVM>()
            .ReverseMap()
                .ForMember(dest=>dest.Publications,opt=>opt.Ignore());

            CreateMap<User, SaveUserVM>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.validateUser, opt => opt.Ignore())
            .ReverseMap()
                .ForMember(dest => dest.Publications, opt => opt.Ignore());

            CreateMap<User, LoginVM>()
                .ForMember(dest=>dest.validate,opt=>opt.Ignore())
            .ReverseMap()
                .ForMember(dest => dest.Publications, opt => opt.Ignore());


            CreateMap<UserVM, SaveUserVM>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.validateUser, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<Publications, PublicationVM>()
            .ReverseMap()
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Publications, SavePublicationVM>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
            .ReverseMap()
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

            CreateMap<PublicationVM, SavePublicationVM>()
               .ForMember(dest => dest.File, opt => opt.Ignore())
           .ReverseMap()
               .ForMember(dest => dest.Creadted, opt => opt.Ignore());

            CreateMap<Comments, CommentVM>()
           .ReverseMap()
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());


            CreateMap<Comments, SaveCommentVM>()
           .ReverseMap()
                .ForMember(dest => dest.Creadted, opt => opt.Ignore())
                .ForMember(dest => dest.LastModified, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                 .ForMember(dest => dest.Publication, opt => opt.Ignore());


            CreateMap<Friends, FriendVM>()
                .ForMember(dest => dest.friend, opt => opt.Ignore())
           .ReverseMap();


            CreateMap<Friends, SaveFriendVM>()
           .ReverseMap()
            .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<FriendVM, SaveFriendVM>()
            .ReverseMap()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.friend, opt => opt.Ignore());

        }
    }
}
