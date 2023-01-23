using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineClassRegister.Areas.Identity.Data;

// Add profile data for application users by adding properties to the OnlineClassRegisterUser class
public class OnlineClassRegisterUser : IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }

    [PersonalData]
    public string? LastName { get; set; }

    [PersonalData] 
    public DateTime DateOfBirth { get; set; }
    [PersonalData] 
    public string? CityOfBirth { get; set; }

    public int? StudentGroupId { get; set; }
}

