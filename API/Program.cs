
using API.Errors;
using API.Extensions;
using API.Middleware;
using core.Entities;
using core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


        var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddControllers();
         builder.Services.AddApplicationServices(builder.Configuration);

         var MyAllowSpecification = "_myAllowSpecifiction";

         builder.Services.AddCors(options => {
            options.AddPolicy(name: MyAllowSpecification,
           policy  => {
                 policy.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod();

           });
         });

        var app = builder.Build();


        // Configure the HTTP request pipeline.

        app.UseMiddleware<ExceptionMiddleware>();


        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        
        
            app.UseSwagger();
            app.UseSwaggerUI();
        
        

        //app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.UseCors("Corspolicy");

            app.UseAuthorization();

            app.MapControllers();


        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured  during migrations");
        }

         app.UseCors(MyAllowSpecification);

        app.Run();
  