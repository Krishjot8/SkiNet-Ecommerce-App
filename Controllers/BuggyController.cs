﻿using ECommerce_App.Data;
using ECommerce_App.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]            //Error Requests here
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }


        [HttpGet("testauth")]
        [Authorize]



        public ActionResult<string> GetSecretText() 
        {

            return "secret stuff";
        
        }



        [HttpGet("notfound")]

        public ActionResult GetNotFoundRequest() {


            var thing = _context.Products.Find(42);


            if (thing == null) {
            
            return NotFound(new ApiResponse(404));
            
            }

            return Ok();
        
        
        }

        [HttpGet("servererror")]

        public ActionResult GetServerError()
        {

            var thing = _context.Products.Find(42);

            var thingToReturn = thing.ToString();


            return Ok();


        }


        [HttpGet("badrequest")]

        public ActionResult GetBadRequest()
        {


            return BadRequest(new ApiResponse(400));


        }


        [HttpGet("badrequest/{id}")]

        public ActionResult GetBadRequest(int id)
        {



            return BadRequest();


        }
    }
}
