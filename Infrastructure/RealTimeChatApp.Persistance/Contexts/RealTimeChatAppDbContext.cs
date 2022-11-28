using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Application.Common.Interfaces;

namespace RealTimeChatApp.Persistance.Contexts;

public class RealTimeChatAppDbContext :  IdentityDbContext<User, IdentityRole<Guid>, Guid> ,IRealTimeChatAppDbContext
{
   





}
