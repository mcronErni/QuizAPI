

using AutoMapper;
using QuizAPI.Contract.DTO;
using QuizAPI.Domain.DTO;
using QuizAPI.Model;

namespace QuizAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bootcamper, ListOneBootcamperDTO>()
                .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.BootcamperQuizzes));
            CreateMap<BootcamperQuiz, BootcamperQuizDTO>()
                .ForMember(dest => dest.QuizTitle, opt => opt.MapFrom(src => src.Quizzes.QuizTitle))
                .ForMember(dest => dest.TotalScore, opt => opt.MapFrom(src => src.Quizzes.TotalScore))
                .ForMember(dest => dest.BootcamperName, opt => opt.MapFrom(src => src.Bootcampers.Name));

            CreateMap<Quiz, QuizDTO>()
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor.MentorName));
            CreateMap<QuizDTO, Quiz>();

            CreateMap<Quiz, ListQuizDTO>()
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor.MentorName));
            CreateMap<ListQuizDTO, Quiz>();

            CreateMap<AddBootcamperQuizDTO, BootcamperQuiz>();
            CreateMap<BootcamperDTO, Bootcamper>();

            CreateMap<MentorDTO, Mentor>().ReverseMap();
            CreateMap<ListMentorDTO, Mentor>().ReverseMap();
        }
    }
}
