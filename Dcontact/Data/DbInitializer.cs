
using Dcontact.Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System.Data;

namespace dteam.Dcontact.Core.Data
{
    public static class DbInitializer
    {

        internal static void Initialize(Web_ProjectContext context, UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //data Generate
                if (!context.TbAvatars.Any())
                {
                    TbAvatar tbAvatar = new TbAvatar()
                    {
                        Id = "default",
                        PictureLocation = @"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ6swZO8d-ZWsYcFSMuRaipYJhGTDvJVJ-dIg&usqp=CAU"
                    };
                    context.TbAvatars.Add(tbAvatar);

                }

                if (!context.TbBackGrounds.Any())
                {
                    TbBackGround tbBackGround = new TbBackGround()
                    {
                        Id = "default",
                        PictureLocation = @"https://images.unsplash.com/photo-1666812272845-c5dcaae45453?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHx0b3BpYy1mZWVkfDV8NnNNVmpUTFNrZVF8fGVufDB8fHx8&auto=format&fit=crop&w=600&q=60"
                    };
                    context.TbBackGrounds.Add(tbBackGround);
                }

                //Role Generage

                if (!context.UserRoles.Any())
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Name = "User", NormalizedName = "USER" }
                    };
                    foreach (var item in roles)
                    {
                        roleManager.CreateAsync(item).Wait();
                    }
                }

                //User Generate


                if (!context.Users.Any())
                {
                    { //User
                        int NumberUser = 10;
                        var users = new List<UserIdentity>();
                        for (var i = 0; i < NumberUser; ++i)
                        {
                            var user = Activator.CreateInstance<UserIdentity>();
                            user.UserName = string.Concat("username", i.ToString("000"));
                            user.Email = String.Concat(user.UserName, "@tempmail.com");
                            user.NormalizedUserName = user.UserName.ToUpper();
                            user.NormalizedEmail = user.Email.ToUpper();
                            user.EmailConfirmed = true;
                            users.Add(user);
                        }

                        foreach (var user in users)
                        {
                            userManager.CreateAsync(user, user.UserName).Wait();
                            userManager.AddToRolesAsync(user, new string[] { "User" }).Wait();
                            context.CreateDcontact(user);
                        }
                    };
                    { //admin
                        int NumberUser = 10;
                        var users = new List<UserIdentity>();
                        for (var i = 0; i < NumberUser; ++i)
                        {
                            var user = Activator.CreateInstance<UserIdentity>();
                            user.UserName = string.Concat("admin", i.ToString("000"));
                            user.Email = string.Concat(user.UserName, "@tempmail.com");
                            user.NormalizedUserName = user.UserName.ToUpper();
                            user.NormalizedEmail = user.Email.ToUpper();
                            user.EmailConfirmed = true;
                            users.Add(user);
                        }

                        foreach (var user in users)
                        {
                            userManager.CreateAsync(user, user.UserName).Wait();
                            userManager.AddToRolesAsync(user, new string[] { "Admin" }).Wait();
                        }
                    }

                }
                context.SaveChanges();
            }
            catch (Exception) { throw; }


        }

    }
}