using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using IssueTracker.Entities;
using IssueTracker.Repository.Contracts;

namespace IssueTracker.Web.ApiControllers
{
    [RoutePrefix("Issues")]
    public class IssuesController : ApiController
    {
        private readonly IIssueRepository _issueRepository;

        public IssuesController(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var issues = _issueRepository.GetAll("Assignee");
            return Ok(issues);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id, bool expand = false)
        {
            var issue = _issueRepository.GetById(id, expand ? new[] {"Assignee"} : null);
            return Ok(issue);
        }

        [Route("NotStarted")]
        public IHttpActionResult GetNotStarted()
        {
            var issues = _issueRepository.GetByStatus(IssueStatus.NotStarted);
            return Ok(issues);
        }


        [Route("Started")]
        public IHttpActionResult GetStarted()
        {
            var issues = _issueRepository.GetByStatus(IssueStatus.Started);
            return Ok(issues);
        }

        [Route("Started/{id}")]
        [HttpPut]
        public IHttpActionResult StartIssue(int id)
        {
            var issue = _issueRepository.GetById(id);
            issue.Start();
            _issueRepository.Update(issue);
            return Ok(issue);
        }

        [Route("Finished")]
        public IHttpActionResult GetFinished()
        {
            var issues = _issueRepository.GetByStatus(IssueStatus.Finished);
            return Ok(issues);
        }

        [Route("Finished/{id}")]
        [HttpPut]
        public IHttpActionResult FinishIssue(int id)
        {
            var issue = _issueRepository.GetById(id);
            issue.Finish();
            _issueRepository.Update(issue);
            return Ok(issue);
        }

        [Route("")]
        public IHttpActionResult Post(Issue issue)
        {
            _issueRepository.Add(issue);
            return Created("http://localhost:50765/issues/" + issue.Id, issue);
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, Issue issue)
        {
            var tempIssue = _issueRepository.GetById(id);
            tempIssue.Description = issue.Description;
            tempIssue.AssigneeId = issue.AssigneeId;
            _issueRepository.Update(tempIssue);
            return Ok(tempIssue);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _issueRepository.Delete(id);
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

    }
}
