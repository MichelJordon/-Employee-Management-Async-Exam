using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class JobService
{
    private readonly DapperContext _context;
    public JobService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<Job>>> GetJobs()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select id as Id, job_title as JobTitle, min_salary as MinSalary, max_salary as MaxSalary  from jobs";
            var response = await connection.QueryAsync<Job>(sql);
            return new Response<List<Job>>(response.ToList());
        }
    }
    public async Task<Response<Job>> InsertJob(Job job)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into jobs(job_title, max_salary, min_salary) values ('{job.JobTitle}', {job.MaxSalary}, {job.MinSalary} ) returning id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                job.Id = response;
                return new Response<Job>(job);
            }
        }
        catch (Exception ex)
        {
            return new Response<Job>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<Job>> UpdateJob(Job job)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update jobs set job_title = '{job.JobTitle}', max_salary = {job.MaxSalary}, min_salary = {job.MinSalary}  where id = {job.Id}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                job.Id = response;
                return new Response<Job>(job);
            }
        }
        catch (Exception ex)
        {
            return new Response<Job>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteJob(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from Jobs where id = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}