using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using IssueTracker.Entities;
using IssueTracker.Repository.Contracts;

namespace IssueTracker.Web.ApiControllers
{
    [RoutePrefix("People")]
    public class PeopleController : ApiController
    {
        private readonly IPersonRepository _personRepository;

        public PeopleController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var people = _personRepository.GetAll();
            return Ok(people);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id, bool expand = false)
        {
            var person = _personRepository.GetById(id, expand ? new []{"Issues"} : null);
            return Ok(person);
        }

        [Route("")]
        public IHttpActionResult Post(Person person)
        {
            _personRepository.Add(person);
            return Created("http://localhost:50765/people/" + person.Id, person);
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, Person person)
        {
            var tempPerson = _personRepository.GetById(id);
            tempPerson.Firstname = person.Firstname;
            tempPerson.Lastname = person.Lastname;
            tempPerson.Email = person.Email;
            _personRepository.Update(tempPerson);
            return Ok(tempPerson);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _personRepository.Delete(id);
            return new StatusCodeResult(HttpStatusCode.NoContent, Request);
        }

    }
}
