using BookOrGetBooked.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IGenericService<TEntity, TCreateDto, TResponseDto, TUpdateDto>
    {
        Task<Result<IEnumerable<TResponseDto>>> GetAllAsync();
        Task<Result<TResponseDto>> GetByIdAsync(int id);
        Task<Result<bool>> ExistsAsync(int id);
        Task<Result<TResponseDto>> CreateAsync(TCreateDto createDto);
        Task<Result<TResponseDto>> UpdateAsync(int id, TUpdateDto updateDto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
