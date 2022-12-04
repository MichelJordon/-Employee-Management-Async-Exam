using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class CountrieService
{
    private readonly DapperContext _context;
    public CountrieService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Countrie>>> GetCountries()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, country_name as CountryName from Countries";
            var response = await connection.QueryAsync<Countrie>(sql);
            return new Response<List<Countrie>>(response.ToList());
        }
    }
    public async Task<Response<Countrie>> InsertCountrie(Countrie countrie)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into Countries(country_name, region_id) values ('{countrie.CountryName}',{countrie.RegionId} ) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                countrie.Id = response;
                return new Response<Countrie>(countrie);
            }
        }
        catch (Exception ex)
        {
            return new Response<Countrie>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Countrie>> UpdateCountrie(Countrie countrie)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update Countries set country_name = '{countrie.CountryName}', region_id = {countrie.RegionId}  where id = {countrie.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                countrie.Id = response;
                return new Response<Countrie>(countrie);
            }
        }
        catch (Exception ex)
        {
            return new Response<Countrie>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteCountrie(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from Countries where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}