/*
 * Simple TripApp API
 *
 * This is a simple TripApp API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IO.Swagger.Data;
using IO.Swagger.Logging;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// Users API 
    /// </summary>
    public class UsersApiController : Controller
    {
        private readonly TripAppContext _context;
        private readonly IPasswordHasher<User> _hasher;
        private readonly ILogger _logger;
        /// <summary>
        /// Initializes controller.
        /// </summary>
        /// <param name="context">Db context to use.</param>
        /// <param name="hasher">Hasher for user passwords.</param>
        public UsersApiController(TripAppContext context, IPasswordHasher<User> hasher, ILogger<UsersApiController> logger)
        {
            _context = context;
            _hasher = hasher;
            _logger = logger;
        }

        /// <summary>
        /// Creates / Registers user
        /// </summary>
        /// <remarks>This can be done by any user.</remarks>
        /// <param name="user">Created user object</param>
        /// <response code="400">bad input parameter</response>
        /// <response code="409">an existing item already exists</response>
        /// <response code="201">successful operation</response>
        [HttpPost]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user")]
        [SwaggerOperation("CreateUser")]
        public virtual IActionResult CreateUser([FromBody]User user)
        {
            // TODO ftn: Add validation to the user parameter!!!
            // Return 400 - BadRequest if not valid!
            if (_context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, user); // 409 already exists!
            }

            try
            {
                user.Password = _hasher.HashPassword(null, user.Password);
                // Ensure token is created:
                if (user.RefreshToken == null)
                {
                    user.RefreshToken = Guid.NewGuid();
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                user.Password = null;
                return Created(Request.Host.ToString(), user); // 201 Created successfuly.
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.INSERT_ITEM, "CreateUser({user}) NOT ADDED", user);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Deletes user
        /// </summary>
        /// <remarks>This can only be done by an administrator.</remarks>
        /// <param name="username">The username that needs to be deleted</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">User not found</response>
        [HttpDelete]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user/{username}")]
        [SwaggerOperation("DeleteUser")]
        [Authorize(ActiveAuthenticationSchemes = "apikey")]
        public virtual void DeleteUser([FromRoute]string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user by username
        /// </summary>
        /// <param name="username">The username that needs to be fetched.</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username supplied</response>
        /// <response code="404">User not found</response>
        [HttpGet]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user/{username}")]
        [SwaggerOperation("GetUserByUsername")]
        [SwaggerResponse(200, type: typeof(User))]
        [Authorize(ActiveAuthenticationSchemes = "apikey")]
        public virtual IActionResult GetUserByUsername([FromRoute]string username)
        {
            // EXAMPLE: How to use logged in user identity:
            var userName = User.Identity.Name;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userFirstName = User.FindFirst(ClaimTypes.GivenName).Value;
            var userLastName = User.FindFirst(ClaimTypes.Surname).Value;

            // TODO: Check if your role allows you to get user by username?
            // if (!User.IsInRole("administrator")){ return StatusCode(StatusCodes.YOU HAVE NO RIGHT....

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                user.Password = null;
                return new ObjectResult(user);
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.GET_ITEM, "GetUserByUsername({username}) NOT FOUND", username);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Logs user into the system
        /// </summary>
        /// <param name="username">The user name for login</param>
        /// <param name="password">The password for login in clear text</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid username/password supplied</response>
        [HttpGet]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user/login")]
        [SwaggerOperation("LoginUser")]
        [SwaggerResponse(200, type: typeof(string))]
        public virtual IActionResult LoginUser([FromQuery]string username, [FromQuery]string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == username);
                
                if (user != null && _hasher.VerifyHashedPassword(null, user.Password, password).Equals(PasswordVerificationResult.Success))
                {
                    user.Password = null;
                    return new ObjectResult(user);
                }
                
                // Set it to null so it is not
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.GET_ITEM, "LoginUser({username}) NOT FOUND", username);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Logs out currenty logged in user session
        /// </summary>
        /// <response code="200">successful operation</response>
        [HttpGet]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user/logout")]
        [SwaggerOperation("LogoutUser")]
        [Authorize(ActiveAuthenticationSchemes = "apikey")]
        public IActionResult LogoutUser()
        {
            // TODO FTN: Add support for security - read user token or whatever!
            return Ok();
        }


        /// <summary>
        /// Updates user
        /// </summary>
        /// <remarks>This can only be done by the logged in user.</remarks>
        /// <param name="username">username of a user that is about to be updated</param>
        /// <param name="user">Updated user object</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid user supplied</response>
        /// <response code="404">User not found</response>
        [HttpPut]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user/{username}")]
        [SwaggerOperation("UpdateUser")]
        [SwaggerResponse(200, type: typeof(User))]
        [Authorize(ActiveAuthenticationSchemes = "apikey")]
        public virtual IActionResult UpdateUser([FromRoute]string username, [FromBody]User user)
        {
            // TODO ftn: Add validation to the user parameter!!!
            // Return 400 - BadRequest if not valid!
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, username); // 400 not found!
            }

            try
            {
                user.Password = _hasher.HashPassword(null, user.Password);
                // Ensure token is changed:
                user.RefreshToken = Guid.NewGuid();
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.UPDATE_ITEM, "UpdateUser({username}) NOT UPDATED", username);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// searches users
        /// </summary>
        /// <remarks>By passing in the appropriate options, you can search for available users in the system </remarks>
        /// <param name="searchString">pass an optional search string for looking up users</param>
        /// <param name="skip">number of records to skip for pagination</param>
        /// <param name="limit">maximum number of records to return</param>
        /// <response code="200">search results matching criteria</response>
        /// <response code="400">bad input parameter</response>
        [HttpGet]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/user")]
        [SwaggerOperation("SearchUsers")]
        [SwaggerResponse(200, type: typeof(List<User>))]
        public virtual IActionResult SearchUsers([FromQuery]string searchString, [FromQuery]int? skip, [FromQuery]int? limit)
        {
            try
            {
                var users = _context.Users;
                return new ObjectResult(users);
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.LIST_ITEMS, "SearchUsers({searchString}) NOT FOUND", searchString);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
