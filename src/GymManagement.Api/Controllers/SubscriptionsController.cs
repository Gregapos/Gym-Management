using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateSubscription(CreateSubscriptionRequest request)
    {
        return Ok(request);
    }
}