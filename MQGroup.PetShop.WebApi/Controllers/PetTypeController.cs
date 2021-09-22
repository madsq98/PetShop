using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.WebApi.DTO;

namespace MQGroup.PetShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _service;

        public PetTypeController(IPetTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<PetType>> GetAll()
        {
            try
            {
                return Ok(_service.GetAllPetTypes());
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<PetType> GetByID(int id)
        {
            try
            {
                return Ok(_service.GetByID(id));
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public ActionResult<PetType> Create([FromBody] PetTypeDto petType)
        {
            try
            {
                return Ok(_service.SavePetType(new PetType
                {
                    Name = petType.Name
                }));
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<PetType> Update(int id, [FromBody] PetTypeDto petType)
        {
            try
            {
                return Ok(_service.UpdatePetType(id, new PetType
                {
                    Name = petType.Name
                }));
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                return Ok(_service.DeletePetTypeById(id));
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}