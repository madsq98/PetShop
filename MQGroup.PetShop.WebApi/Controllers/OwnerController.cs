using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using MQGroup.PetShop.Core.IServices;
using MQGroup.PetShop.Core.Models;
using MQGroup.PetShop.WebApi.DTO;

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
            try
            {
                return Ok(_ownerService.GetAllOwners());
            }
            catch (SystemException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetOwnerById(int id)
        {
            try
            {
                return Ok(_ownerService.GetOwnerById(id));
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
        public ActionResult<Owner> CreateOwner([FromBody] OwnerDto owner)
        {
            try
            {
                return Ok(_ownerService.CreateOwner(new Owner
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber
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
        public ActionResult<Owner> UpdateOwner(int id, [FromBody] OwnerDto owner)
        {
            try
            {
                return Ok(_ownerService.UpdateOwner(id, new Owner
                {
                    FirstName = owner.FirstName,
                    LastName = owner.LastName,
                    Address = owner.Address,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber
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
        public ActionResult<bool> DeleteOwner(int id)
        {
            try
            {
                return Ok(_ownerService.DeleteOwnerById(id));
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