using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Shared.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Services
{
    public class GenericService<TEntity, TCreateDto, TResponseDto, TUpdateDto> : IGenericService<TEntity, TCreateDto, TResponseDto, TUpdateDto>
        where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<Result<IEnumerable<TResponseDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (entities == null || !entities.Any())
            {
                return Result<IEnumerable<TResponseDto>>.Failure(ErrorCodes.Resource.NotFound);
            }

            var dtos = _mapper.Map<IEnumerable<TResponseDto>>(entities);
            return Result<IEnumerable<TResponseDto>>.Success(dtos);
        }

        public virtual async Task<Result<TResponseDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Result<TResponseDto>.Failure(ErrorCodes.Resource.NotFound, $"{typeof(TEntity).Name} not found.");
            }

            var dto = _mapper.Map<TResponseDto>(entity);
            return Result<TResponseDto>.Success(dto);
        }

        public virtual async Task<Result<bool>> ExistsAsync(int id)
        {
            var exists = await _repository.ExistsAsync(id);
            return exists
                ? Result<bool>.Success(true)
                : Result<bool>.Failure(ErrorCodes.Resource.NotFound, $"{typeof(TEntity).Name} does not exist.");
        }

        public virtual async Task<Result<TResponseDto>> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);

            await _repository.AddAsync(entity);

            var responseDto = _mapper.Map<TResponseDto>(entity);

            return Result<TResponseDto>.Success(responseDto);
        }

        public virtual async Task<Result<TResponseDto>> UpdateAsync(int id, TUpdateDto updateDto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return Result<TResponseDto>.Failure(ErrorCodes.Resource.NotFound, $"{typeof(TEntity).Name} not found.");
            }

            _mapper.Map(updateDto, existingEntity);

            await _repository.UpdateAsync(existingEntity);

            var responseDto = _mapper.Map<TResponseDto>(existingEntity);

            return Result<TResponseDto>.Success(responseDto);
        }

        public virtual async Task<Result<bool>> DeleteAsync(int id)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return Result<bool>.Failure(ErrorCodes.Resource.NotFound, $"{typeof(TEntity).Name} not found.");
            }

            await _repository.DeleteAsync(existingEntity);

            return Result<bool>.Success(true);
        }
    }
}
