using AutoMapper;
using MediatR;
using OnlineCoursesPlatform.Application.Abstractions.Repositories;
using OnlineCoursesPlatform.Application.Features.Tags.Dto;

namespace OnlineCoursesPlatform.Application.Features.Tags.Queries.Handlers
{
    public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, List<TagDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTagsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _unitOfWork.TagRepository.GetAllOrderedAsync();
            return _mapper.Map<List<TagDto>>(tags);
        }
    }
}
