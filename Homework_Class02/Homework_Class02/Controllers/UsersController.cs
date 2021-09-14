using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_Class02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, StaticDb.UserNames);
        }

        [HttpGet("{index}")]
        public ActionResult<string> Get(int index)
        {
            try
            {
                if (index < 0)
                {                 
                    return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value!");
                }
                if (index > StaticDb.UserNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no user with index {index}");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.UserNames[index]);
            }
            catch (Exception e) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string note = reader.ReadToEnd();
                    StaticDb.UserNames.Add(note);
                    return StatusCode(StatusCodes.Status201Created, "The user was created");
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(Request.Body))
                {
                    string requestBody = streamReader.ReadToEnd();
                    int index = Int32.Parse(requestBody);
                    if (index < 0)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "The index has negative value!");
                    }
                    if (index >= StaticDb.UserNames.Count)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, $"There is no user with index {index}");
                    }
                    StaticDb.UserNames.RemoveAt(index);
                    return StatusCode(StatusCodes.Status204NoContent, "The user was deleted");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }
    }
}
