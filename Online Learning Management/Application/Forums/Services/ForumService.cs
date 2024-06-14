using AutoMapper;
using Online_Learning_Management.Domain.Entities.Forums;
using Online_Learning_Management.Domain.Interfaces.Forums;
using Online_Learning_Management.Infrastructure.DTOs.Forum;

namespace Online_Learning_Management.Application.Forums.Services
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly IMapper _mapper;

        public ForumService(IForumRepository forumRepository, IMapper mapper)
        {
            _forumRepository = forumRepository;
            _mapper = mapper;
        }

        public async Task AddForumAsync(CreateForumDTO createForumDTO)
        {
            var forum = _mapper.Map<Forum>(createForumDTO);
            await _forumRepository.AddForumAsync(forum);
        }

        public async Task DeleteForumAsync(Guid id)
        {
            var forum = await _forumRepository.GetForumByIdAsync(id);
            if (forum == null)
            {
                throw new ArgumentException("Forum not found.");
            }
            await _forumRepository.DeleteForumAsync(id);
        }

        public async Task<IEnumerable<Forum>> GetAllForumsAsync()
        {
            var forums = await _forumRepository.GetAllForumsAsync();
            return _mapper.Map<IEnumerable<Forum>>(forums);
        }

        public async Task<Forum> GetForumByIdAsync(Guid id)
        {
            var selectedForum = await _forumRepository.GetForumByIdAsync(id);
            if (selectedForum == null)
            {
                throw new ArgumentException("Forum not found");
            }
            return _mapper.Map<Forum>(selectedForum);
        }

        public async Task UpdateForumAsync(Guid id, UpdateForumDTO forumDto)
        {
            var existingForum = await _forumRepository.GetForumByIdAsync(id);

            if (existingForum == null)
            {
                throw new ArgumentException("Module not found.");
            }

            _mapper.Map(forumDto, existingForum);

            await _forumRepository.UpdateForumAsync(existingForum);
        }
    }
}
