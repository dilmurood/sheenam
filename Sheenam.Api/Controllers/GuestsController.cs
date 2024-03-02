using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Sheenam.Api.Models.Foundations;
using Sheenam.Api.Models.Foundations.Exceptions;
using Sheenam.Api.Services.Foundations.Guests;
using System.Threading.Tasks;

namespace Sheenam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : RESTFulController
    {
        private readonly IGuestService guestService;

        public GuestsController(IGuestService guestService)
        {
            this.guestService = guestService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Guest>> PostGuestAsync(Guest guest)
        {
            try
            {
                Guest postedGuest = await this.guestService.AddGuestAsync(guest);

                return Created(postedGuest);
            }
            catch (GuestValidationException ex)
            {
                return BadRequest(ex.InnerException);
            }
            catch(GuestDependencyValidationException exception) 
                when (exception.InnerException is AlreadyExistGuestException)
            {
                return Conflict(exception.InnerException);
            }
            catch (GuestDependencyValidationException exception)
            {
                return BadRequest(exception.InnerException);
            }
            catch (GuestDependencyException ex)
            {
                return InternalServerError(ex.InnerException);
            }
            catch (GuestServiceException ex)
            {
                return InternalServerError(ex.InnerException);
            }
        } 
    }
}
