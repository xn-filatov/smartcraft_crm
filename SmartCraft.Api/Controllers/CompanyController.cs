using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartCraft.Application.DTO.Company;
using SmartCraft.Application.Services.Interfaces;
using SmartCraft.Core.Entities;

namespace SmartCraft.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class
    CompanyController(ILogger<CompanyController> _logger, ICompanyService _companyService) : ControllerBase
{
    /// <summary>
    /// Returns a list of existing companies from the database
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetList(CancellationToken ct = default)
    {
        try
        {
            var companies = await _companyService.GetListAsync(ct);

            if (companies == null || !companies.Any())
            {
                return NoContent();
            }

            return Ok(companies);
        }
        catch (Exception e)
        {
            _logger.LogError(JsonConvert.SerializeObject(e));
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Creates a new Company in the database
    /// </summary>
    /// <param name="data"><see cref="AddCompanyDto"/></param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Create([FromBody] AddCompanyDto data, CancellationToken ct = default)
    {
        try
        {
            var newCompany = await _companyService.CreateAsync(data, ct);
            
            return CreatedAtAction(nameof(Create), newCompany);
        }
        catch (Exception e)
        {
            _logger.LogError(JsonConvert.SerializeObject(e));
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<ActionResult> Update(UpdateCompanyDto data, CancellationToken ct = default)
    {
        try
        {
            await _companyService.UpdateAsync(data, ct);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(JsonConvert.SerializeObject(e));
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Deletes a company from the database by Id
    /// </summary>
    /// <param name="id">Id of a company</param>
    /// <param name="ct"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] int id, CancellationToken ct = default)
    {
        try
        {
            await _companyService.DeleteAsync(id, ct);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(JsonConvert.SerializeObject(e));
            return BadRequest(e.Message);
        }
    }
}