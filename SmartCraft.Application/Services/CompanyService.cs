using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartCraft.Application.DTO;
using SmartCraft.Application.DTO.Company;
using SmartCraft.Application.Extensions;
using SmartCraft.Application.Services.Interfaces;
using SmartCraft.Infrastructure.Data;

namespace SmartCraft.Application.Services;

public class CompanyService(ILogger<CompanyService> _logger, AppDbContext _dbContext) : ICompanyService
{
    public async Task<IEnumerable<CompanyDto>> GetListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var companies = await _dbContext.Companies
                .Include(c => c.Contacts)
                .ThenInclude(c => c.Interactions)
                .ToListAsync(cancellationToken);

            return companies.Select(c => c.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{method}: {message}", this.GetCurrentMethodName(), ex.Message);
            throw ex;
        }
    }

    public async Task<CompanyDto> CreateAsync(AddCompanyDto data, CancellationToken cancellationToken = default)
    {
        try
        {
            var newCompany = await _dbContext.Companies.AddAsync(data.ToEntity(), cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newCompany.Entity.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{method}: {message}", this.GetCurrentMethodName(), ex.Message);
            throw ex;
        }
    }

    public async Task UpdateAsync(UpdateCompanyDto data, CancellationToken cancellationToken = default)
    {
        try
        {
            var rowsUpdated = await _dbContext.Companies
                .Where(c => c.Id == data.Id)
                .ExecuteUpdateAsync(setters =>
                        setters.SetProperty(b => b.Name, b => data.Name ?? b.Name)
                            .SetProperty(b => b.Industry, b => data.Industry ?? b.Industry)
                            .SetProperty(b => b.Website, b => data.Website ?? b.Website)
                            .SetProperty(b => b.Size, b => data.Size ?? b.Size)
                    , cancellationToken);

            if (rowsUpdated == 0) throw new Exception($"Company with Id {data.Id} was not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{method}: {message}", this.GetCurrentMethodName(), ex.Message);
            throw ex;
        }
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        try
        {
            var rowsDeleted = await _dbContext.Companies.Where(c => c.Id == id).ExecuteDeleteAsync(ct);

            if (rowsDeleted == 0) throw new Exception($"Company with Id {id} was not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{method}: {message}", this.GetCurrentMethodName(), ex.Message);
            throw ex;
        }
    }
}