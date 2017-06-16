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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// Ticket purchase controller
    /// </summary>
    public class TicketsApiController : Controller
    {
        private readonly TripAppContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public TicketsApiController(TripAppContext context, IConfiguration configuration, ILogger<TicketsApiController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// adds an ticket purchase item
        /// </summary>
        /// <remarks>Adds an item to the system</remarks>
        /// <param name="ticketPurchase">TicketPurchase item to add</param>
        /// <response code="201">item created</response>
        /// <response code="400">invalid input, object invalid</response>
        /// <response code="409">item already exists</response>
        [HttpPost]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/tickets")]
        [SwaggerOperation("AddTicketPurchase")]
        [Authorize(ActiveAuthenticationSchemes = "apikey")]
        public virtual IActionResult AddTicketPurchase([FromBody]TicketPurchase ticketPurchase)
        {
            // TODO FTN: Add validation!
            var loggedInUserId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            

            var hasTypeAndUser = ticketPurchase != null
                                && ticketPurchase.TypeId != null
                                && ticketPurchase.TypeId > 0
                                && ticketPurchase.UserId != null
                                && ticketPurchase.UserId > 0
                                && ticketPurchase.NumberOfPassangers > 0;

            if (!hasTypeAndUser)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ticketPurchase);
            }

            if (loggedInUserId != ticketPurchase.UserId)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ticketPurchase);
            }

            try
            {
                if (_context.Purchases.FirstOrDefault(p => p.Id == ticketPurchase.Id) != null)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ticketPurchase); // 409 already exists!
                }
                //TODO: skinuti useru novac ili vratiti gresku ako nema dovoljno
                var type = _context.Types.First(t => t.Id == ticketPurchase.TypeId);
                var user = _context.Users.First(u => u.Id == ticketPurchase.UserId);

                if (user.Balance - type.Price * ticketPurchase.NumberOfPassangers < 0.0d)
                {
                    return StatusCode(StatusCodes.Status402PaymentRequired, ticketPurchase);
                }

                ticketPurchase.Code = Guid.NewGuid();
                ticketPurchase.StartDateTime = DateTime.Now.ToUniversalTime().AddMinutes(_configuration.GetSection(Startup.AppSettingsConfigurationSectionKey).GetValue<int>(Startup.AppSettingsMinutesUntilTicketStartKey));
                ticketPurchase.EndDateTime = DateTime.Now.ToUniversalTime().AddMinutes(type.Duration.Value * 60 + _configuration.GetSection(Startup.AppSettingsConfigurationSectionKey).GetValue<int>(Startup.AppSettingsMinutesUntilTicketStartKey));
                ticketPurchase.Price = type.Price;
                user.Balance = user.Balance - type.Price * ticketPurchase.NumberOfPassangers;

                _context.Purchases.Add(ticketPurchase);
                _context.SaveChanges();

                ticketPurchase = _context.Purchases.Include(u => u.User).First(p => p.Id == ticketPurchase.Id);
                return StatusCode(StatusCodes.Status201Created, ticketPurchase);
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.INSERT_ITEM, "AddTicketPurchase({ticketPurchase}) NOT ADDED", ticketPurchase);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// searches tickets purchases
        /// </summary>
        /// <remarks>By passing in the appropriate options, you can search for available ticket in the system </remarks>
        /// <param name="searchString">pass an optional search string for looking up tickets.
        /// format: [command]:[value]
        /// examples:
        /// all:1 -> takes all tickets for user with id: 1
        /// my:1 -> takes all ticket purchased for user id: 1 where end datetime is >= datetime.now
        /// id:1 -> takes ticket purchase with id: 1
        /// </param>
        /// <param name="skip">number of records to skip for pagination</param>
        /// <param name="limit">maximum number of records to return</param>
        /// <response code="200">search results matching criteria</response>
        /// <response code="400">bad input parameter</response>
        [HttpGet]
        [Route("/sergiosuperstar/TripAppSimple/1.0.0/tickets")]
        [SwaggerOperation("SearchTickets")]
        [SwaggerResponse(200, type: typeof(List<TicketPurchase>))]
        public virtual IActionResult SearchTickets([FromQuery]string searchString, [FromQuery]int? skip, [FromQuery]int? limit)
        {
            int id;
            string[] commands = { "all", "id", "my" };
            searchString = WebUtility.HtmlDecode(searchString).ToLower();

            var searchItems = searchString.Split(':');
            if (searchItems.Length != 2)
            {
                return StatusCode(StatusCodes.Status400BadRequest, searchString);
            }
            var searchBy = searchItems[0];
            var searchValue = searchItems[1];

            if (!commands.Contains(searchBy))
            {
                return StatusCode(StatusCodes.Status400BadRequest, searchBy);
            }

            if (!int.TryParse(searchValue, out id))
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                List<TicketPurchase> purchases;
                TicketPurchase purchase;
                switch (searchBy)
                {
                    case "id":
                        purchase = _context.Purchases.Include(t => t.Type).Include(u => u.User).Where(c => c.Id == id).FirstOrDefault();
                        return new ObjectResult(purchase);
                    case "all":
                        purchases = _context.Purchases.Include(t => t.Type).Include(u => u.User).Where(c => c.UserId == id).ToList();
                        return new ObjectResult(purchases);
                    case "my":
                        purchase = _context.Purchases.Include(t => t.Type).Include(u => u.User).Where(c => (c.UserId == id && c.EndDateTime > DateTime.Now)).OrderByDescending(p => p.StartDateTime).FirstOrDefault();
                        return new ObjectResult(purchase);
                    default:
                        _logger.LogError(LoggingEvents.LIST_ITEMS, "SearchTickets({searchString}) BAD SEARCH REQUEST", searchString);
                        return StatusCode(StatusCodes.Status400BadRequest);
                };
            }
            catch (Exception)
            {
                _logger.LogError(LoggingEvents.LIST_ITEMS, "SearchTickets({searchString}) NOT FOUND", searchString);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
