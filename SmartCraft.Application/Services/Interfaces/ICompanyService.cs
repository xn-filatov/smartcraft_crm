using SmartCraft.Application.DTO.Company;

namespace SmartCraft.Application.Services.Interfaces;

public interface ICompanyService
{
    /// <summary>
    /// Get list of existing companies
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public Task<IEnumerable<CompanyDto>> GetListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new company using the specified data.
    /// </summary>
    /// <param name="data">An object containing the details of the company to create. Cannot be null.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CompanyDto"/></returns>
    public Task<CompanyDto> CreateAsync(AddCompanyDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing company with the specified data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    public Task UpdateAsync(UpdateCompanyDto data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default);
}