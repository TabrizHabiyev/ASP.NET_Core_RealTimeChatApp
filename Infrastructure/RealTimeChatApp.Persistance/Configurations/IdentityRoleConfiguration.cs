

namespace RealTimeChatApp.Persistance.Configurations;
internal class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {

        builder.HasData(
            new IdentityRole
            {
                Id =Guid.NewGuid().ToString(),
                Name = UserRole.Admin.ToString(),
                NormalizedName = UserRole.Admin.ToString().ToUpper(),
                ConcurrencyStamp = "1"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = UserRole.Member.ToString(),
                NormalizedName = UserRole.Member.ToString().ToUpper(),
                ConcurrencyStamp = "2"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = UserRole.Guest.ToString(),
                NormalizedName = UserRole.Guest.ToString().ToUpper(),
                ConcurrencyStamp = "3"
            }
        );

        builder.ToTable("Roles");
        
    }
}