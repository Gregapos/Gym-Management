using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymManagement.Application.Common.Models;

namespace GymManagement.Application.Common.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
    }
}