using System.Net;
using Dapper;
using Npgsql;
using Domain.Dto;
namespace Infrastructure.Services;

public class JobHistoryService
{
    private readonly DapperContext _context;
    public JobHistoryService(DapperContext context)
    {
        _context = context;
    }

    public async Task<Response<List<JobHistory>>> GetJobHistorys()
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = "Select employee_id as EmployeeId, start_date as StartDate, end_date as EndDate, job_id as JobId, department_id as DepartmentId from job_history";
            var response = await connection.QueryAsync<JobHistory>(sql);
            return new Response<List<JobHistory>>(response.ToList());
        }
    }
    public async Task<Response<JobHistory>> InsertJobHistory(JobHistory jobHistory)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Insert into job_history(employee_id, start_date, end_date,job_id, department_id) values ({jobHistory.EmployeeId}, '{jobHistory.StartDate}', '{jobHistory.EndDate}', {jobHistory.JobId}, {jobHistory.DepartmentId} ) returning employee_id";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                //jobHistory.employee_id = response;
                return new Response<JobHistory>(jobHistory);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobHistory>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<JobHistory>> UpdateJobHistory(JobHistory jobHistory)
    {
        try
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = $"Update job_history set employee_id = {jobHistory.EmployeeId}, start_date = '{jobHistory.StartDate}', end_date = '{jobHistory.EndDate}',  job_id = {jobHistory.JobId}, department_id = {jobHistory.DepartmentId}  where id = {jobHistory.EmployeeId}";
                var response = await connection.ExecuteScalarAsync<int>(sql);
                //jobHistory.employee_id = response;
                return new Response<JobHistory>(jobHistory);
            }
        }
        catch (Exception ex)
        {
            return new Response<JobHistory>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> DeleteJobHistory(int id)
    {
        using (var connection = _context.CreateConnection())
        {
            var sql = $"Delete from job_history where EmployeeId = {id}";
            var response = await connection.ExecuteAsync(sql);
            if(response>0)
                return new Response<string>("Regions deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest,"Category not found");
        }
    }

}