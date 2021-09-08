using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;

namespace MQGroup.PetShop.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetAllOwners()
        {
            return Ok(_ownerService.GetAllOwners());
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetOwnerById(int id)
        {
            return Ok(_ownerService.GetOwnerById(id));
        }

        [HttpPost]
        public ActionResult<Owner> CreateOwner([FromBody] Owner owner)
        {
            return Ok(_ownerService.CreateOwner(owner));
        }

        [HttpPut("{id}")]
        public ActionResult<Owner> UpdateOwner(int id, [FromBody] Owner owner)
        {
            return Ok(_ownerService.UpdateOwner(owner));
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteOwner(int id)
        {
            return Ok(_ownerService.DeleteOwnerById(id));
        }
    }
}