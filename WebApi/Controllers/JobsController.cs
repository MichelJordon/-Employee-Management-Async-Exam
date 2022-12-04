using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class JobController{
    private JobService _jobService;
    public JobController(JobService jobService){
        _jobService = jobService;
    }
    [HttpGet]
    public async Task<Response<List<Job>>> GetJobs(){
        return await _jobService.GetJobs();
    }
    [HttpPost]
    public async Task<Response<Job>> InsertJob(Job job){
        return await _jobService.InsertJob(job);
    }
    [HttpPut]
    public async Task<Response<Job>> UpdateJob(Job job){
        return await _jobService.UpdateJob(job);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteJob(int id){
        return await _jobService.DeleteJob(id);
    }
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string? ToString()
    {
        return base.ToString();
    }
}