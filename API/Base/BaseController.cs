using API.Contexts;
using API.Models;
using API.Repositories;
using API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BaseController<Entity,T,X> : ControllerBase 
        where Entity : class
        where X : IRepository<Entity,T>
    {
        //private GeneralRepository _repo;
        private X _repositories;
        public BaseController(X repositories)
        {
            _repositories = repositories;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var result = _repositories.Get();
                return result.Count() == 0
                    ? Ok(new { statusCode = 200, message = "Data Not Found!" })
                    : Ok(result );
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Something Wrong!!" });
            }
        }

        [HttpGet]
        [Route("{key}")]
        public ActionResult GetById(T key)
        {
            try
            {
                var result = _repositories.Get(key);
                return result == null
                    ? Ok(new { statusCode = 200, message = "Data Not Found!" })
                    : Ok(new { statusCode = 200, message = "Success", data = result });
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Wrong Input" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                var result = _repositories.Insert(entity);
                return result == 0
                    ? Ok(new { statusCode = 200, messgae = "Data Failed to Save" })
                    : Ok(new { statusCode = 200, messgae = "Data Has Been Saved", data = result });
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Input Valid Property" });
            }

        }

        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                var result = _repositories.Update(entity);
                return result == 0
                    ? Ok(new { statusCode = 200, message = "Data Not Updated" })
                    : Ok(new { statusCode = 200, message = "Data Updated", data = result });
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Something Wrong with your Input" });
            }
        }

        [HttpDelete]
        [Route("{key}")]
        public ActionResult Delete(T key)
        {
            try
            {
                var result = _repositories.Delete(key);
                if (result == 0)
                {
                    return Ok(new { statusCode = 200, message = $"Id {key} is Not Found" });
                }
                return Ok(new { statusCode = 200, message = "Data has been deleted", data = result });
            }
            catch
            {
                return BadRequest(new { statusCode = 500, message = "Something Wrong with your Input" });
            }
        }
    }
}
