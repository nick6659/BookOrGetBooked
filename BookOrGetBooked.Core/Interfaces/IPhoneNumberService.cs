using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IPhoneNumberService
    {
        /// <summary>
        /// Creates a new phone number for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user to associate the phone number with.</param>
        /// <param name="prefix">The prefix for the phone number (e.g., country code).</param>
        /// <param name="number">The local phone number.</param>
        /// <returns>A result containing the created phone number or an error message.</returns>
        Task<Result<PhoneNumber>> CreatePhoneNumberAsync(int userId, string prefix, string number);

        /// <summary>
        /// Retrieves a phone number by its ID.
        /// </summary>
        /// <param name="phoneNumberId">The ID of the phone number.</param>
        /// <returns>A result containing the phone number or an error message if not found.</returns>
        Task<Result<PhoneNumber>> GetPhoneNumberByIdAsync(int phoneNumberId);

        /// <summary>
        /// Deletes a phone number by its ID.
        /// </summary>
        /// <param name="phoneNumberId">The ID of the phone number to delete.</param>
        /// <returns>A result indicating whether the deletion was successful.</returns>
        Task<Result<bool>> DeletePhoneNumberAsync(int phoneNumberId);

        /// <summary>
        /// Updates an existing phone number.
        /// </summary>
        /// <param name="phoneNumberId">The ID of the phone number to update.</param>
        /// <param name="prefix">The updated prefix for the phone number.</param>
        /// <param name="number">The updated local phone number.</param>
        /// <returns>A result containing the updated phone number or an error message.</returns>
        Task<Result<PhoneNumber>> UpdatePhoneNumberAsync(int phoneNumberId, int userId, string prefix, string number);
    }
}
