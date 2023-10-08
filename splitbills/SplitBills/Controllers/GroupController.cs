using BusinessLayer;
using Microsoft.AspNetCore.Authorization;﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.DataBase;


namespace SplitBills.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IBusinessLogic _businessLogic;
        private IConfiguration _config;      

        public GroupController(IBusinessLogic businessLogic, IConfiguration config)
        {
            _businessLogic = businessLogic;
            _config = config;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetClaim()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            return userClaims.FirstOrDefault((x) => x.Type == ClaimTypes.Email)?.Value;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequestDTO groupRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(400);
            }
            Guid groupId = await _businessLogic.GetGroupBusinessLogic().CreateGroup(GetClaim(), groupRequestDTO);
            return StatusCode(200, "Group created successfully");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAnGroupsById()
        {
            IEnumerable<CreateGroupDTO> groups = await _businessLogic.GetGroupBusinessLogic().FetchAllGroupsById(GetClaim());
            return StatusCode(200, groups);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetDetailsGroupById(Guid id)
        {
            IEnumerable<GroupDetailsDTO> groups = await _businessLogic.GetGroupBusinessLogic().GetDetailsGroupById( id);
            return StatusCode(200, groups);
        }


        //[HttpPut]
        //[Authorize]
        //public async Task<IActionResult> AddUserIntoExistingGroup(Guid groupId, Guid userId)
        //{
        //    bool resp = await _businessLogic.GetGroupBusinessLogic().AddUserIntoExistingGroup(groupId, userId);
        //    if (resp)
        //    {
        //        return StatusCode(201, "User added into the group");
        //    }
        //    return StatusCode(400, "Something happened");
        //}

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AddUserIntoExistingGroup([FromQuery]Guid groupId, [FromBody]string userEmailId)
        {
            bool resp = await _businessLogic.GetGroupBusinessLogic().AddUserIntoExistingGroup(groupId, userEmailId);
            if (resp)
            {
                return StatusCode(201, "User added into the group");
            }
            return StatusCode(400, "Something happened");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteGroupById(Guid groupId)
        {
            var result = await _businessLogic.GetGroupBusinessLogic().DeletegroupById(groupId);
            return StatusCode(200, result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemoveUser([FromQuery][Required] Guid groupId, [FromQuery][Required] string userEmailId)
        {
            bool resp = await _businessLogic.GetGroupBusinessLogic().RemoveUserFromGroup(groupId, userEmailId);
            if (resp)
            {
                return StatusCode(204, true);
            }
            return StatusCode(400, false);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> LeaveGroup([FromQuery][Required] Guid groupId)
        {
            bool resp = await _businessLogic.GetGroupBusinessLogic().LeaveGroup(groupId, GetClaim());
            if (resp)
            {
                return StatusCode(200, resp);
            }
            return StatusCode(400,resp);
        }
    }
}
