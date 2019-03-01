using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Senai.InLock.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Adicionando moedelo MVC compatível com a versão utlizaada na hora de criar o projeto.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            //SENTA QUE LÁ VEM MERDA
            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }
                ).AddJwtBearer("JwtBearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Quem esta solicitando
                        ValidateIssuer = true,
                        //Quem está validando
                        ValidateAudience = true,
                        //Definindo o tempo de  expiração
                        ValidateLifetime = true,
                        //Forma de criptografar
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InlockGames-authenticacao")),
                        //Definindo o tempo
                        ClockSkew = TimeSpan.FromMinutes(38),
                        //Nome do Issuer, de onde esta vindo
                        ValidIssuer = "InLockGames.WebApi",
                        //Nome da Audience, de onde está vindo
                        ValidAudience = "InLockGames.WebApi"

                    };
                }
                //Pode descansar agora meu rapaz..
                );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CorsPolicy");
                app.UseMvc();

                app.UseAuthentication();
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
