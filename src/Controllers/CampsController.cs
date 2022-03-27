using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCodeCamp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository repository;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }
        [HttpGet]
        public async Task<ActionResult<CampModel[]>> Get(bool includeTalks = false)
        {
            try
            {
                var results = await repository.GetAllCampsAsync(includeTalks);

                return mapper.Map<CampModel[]>(results);
                //return Ok(CampModel);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return Ok();
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker, bool includeTalks = false)
        {
            try
            {
                var result = await repository.GetCampAsync(moniker, includeTalks);

                if (result == null) return NotFound();

                return mapper.Map<CampModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }
        }

        public async Task<ActionResult<CampModel>> Post(CampModel model) 
        {
            try
            {
                var location = linkGenerator.GetPathByAction("Get", "Camps",
                    new { moniker = model.Moniker });

                // create a new Camp
                var camp = mapper.Map<Camp>(model);
                repository.Add(camp);
                if(await repository.SaveChangesAsync())
                {
                    return Created(location, mapper.Map<CampModel>(camp));
                } 
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                throw;
            }
            return BadRequest();
        }
    }
}
