using Domain.Dto;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class JobHistoryController{
    private JobHistoryService _jobHistoryService;
    public JobHistoryController(JobHistoryService jobHistoryService){
        _jobHistoryService = _jobHistoryService;
    }
    [HttpGet]
    public async Task<Response<List<JobHistory>>> GetJobHistorys(){
        return await _jobHistoryService.GetJobHistorys();
    }
    [HttpPost]
    public async Task<Response<JobHistory>> InsertJobHistory(JobHistory jobHistory){
        return await _jobHistoryService.InsertJobHistory(jobHistory);
    }
    [HttpPut]
    public async Task<Response<JobHistory>> UpdateJobHistory(JobHistory jobHistory){
        return await _jobHistoryService.UpdateJobHistory(jobHistory);
    }
    [HttpDelete]
    public async Task<Response<string>> DeleteJobHistory(int id){
        return await _jobHistoryService.DeleteJobHistory(id);
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