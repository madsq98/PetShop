using System;
using System.Collections.Generic;
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
            return Ok(_service.GetAllPetTypes());
        }

        [HttpGet("{id}")]
        public ActionResult<PetType> GetByID(int id)
        {
            return Ok(_service.GetByID(id));
        }

        [HttpPost]
        public ActionResult<PetType> Create([FromBody] PetTypeDto petType)
        {
            return Ok(_service.SavePetType(new PetType
            {
                Name = petType.Name
            }));
        }

        [HttpPut("{id}")]
        public ActionResult<PetType> Update(int id, [FromBody] PetTypeDto petType)
        {
            return Ok(_service.UpdatePetType(id, new PetType
            {
                Name = petType.Name
            }));
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            return Ok(_service.DeletePetTypeById(id));
        }
    }
}