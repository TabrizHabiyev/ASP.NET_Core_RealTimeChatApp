



namespace RealTimeChatApp.Persistance.Configurations.Filters
{
    public static class SoftDeleteExtension
    {
        public static void UseSoftDelete(this ModelBuilder modelBuilder)
        {
            var softDeleteEntities = modelBuilder.Model
            .GetEntityTypes()
            .Where(t => t.ClrType.IsAssignableTo(typeof(ISoftDelete)))
            .ToArray();
            foreach (var softDeleteEntity in softDeleteEntities)
            {
                var entityBuilder = modelBuilder.Entity(softDeleteEntity.ClrType);
                var parameter = Expression.Parameter(softDeleteEntity.ClrType, "e");
                var methodInfo = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(bool))!;
                var efPropertyCall = Expression.Call(null, methodInfo, parameter, Expression.Constant(nameof(ISoftDelete.IsDeleted)));
                var body = Expression.MakeBinary(ExpressionType.Equal, efPropertyCall, Expression.Constant(false));
                var expression = Expression.Lambda(body, parameter);
                entityBuilder.HasQueryFilter(expression);
            }
        }
    }
}
