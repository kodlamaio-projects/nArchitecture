using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<OtpAuthenticator> OtpAuthenticators { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        AddGlobalSoftDeleteFilter(modelBuilder);
    }

    private static void AddGlobalSoftDeleteFilter(ModelBuilder modelBuilder)
    {
        Expression<Func<Entity<int>, bool>> softDeleteGlobalFilterExpression = x => !x.DeletedDate.HasValue;

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (entity.ClrType.IsAssignableTo(typeof(Entity<int>)))
            {
                var parameter = Expression.Parameter(entity.ClrType);
                var body = ReplacingExpressionVisitor.Replace(softDeleteGlobalFilterExpression.Parameters.First(), parameter, softDeleteGlobalFilterExpression.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                entity.SetQueryFilter(lambdaExpression);
            }
        }


    }
}
