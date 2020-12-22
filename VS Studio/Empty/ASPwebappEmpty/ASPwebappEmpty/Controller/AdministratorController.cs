using ASPwebappEmpty.Model;
using ASPwebappEmpty.ViewData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPwebappEmpty
{
    [Authorize(Roles = "Admin")]
    // [Authorize(Policy = "AdminRolePolicy")] for policy based auth using roles.
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole userrole = new IdentityRole()
                {
                    Name = roleViewModel.Role
                };
                IdentityResult identityResult = await roleManager.CreateAsync(userrole);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("DisplayRoles", "Administrator");
                }

                foreach(IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }
            }
            return View();
        }

        public IActionResult DisplayRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public IActionResult DisplayUsers()
        {
            var users = userManager.Users.OrderBy(d => d.UserName);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole identityRole = await roleManager.FindByIdAsync(id);
            if (identityRole == null)
            {
                ViewBag.ErrorMessage = $"Role with the ID: {id} is not available.";
                return View();
            }
            else
            {
                var users = userManager.Users.ToList();
                List<string> userslist = new List<string>();
                foreach(var user in users)
                {
                    if(await userManager.IsInRoleAsync(user, identityRole.Name))
                    {
                        userslist.Add(user.UserName);
                    }
                }
                UpdateRoleViewModel updateRoleViewModel = new UpdateRoleViewModel()
                {
                    ID = id,
                    RoleName = identityRole.Name,
                    Users = userslist
                };
                return View(updateRoleViewModel);
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleViewModel updateRoleViewModel)
        {
            if (ModelState.IsValid)
            {            
                IdentityRole identityRole = await roleManager.FindByIdAsync(updateRoleViewModel.ID);
                if (identityRole == null)
                {
                    ViewBag.ErrorMessage = $"Role with the ID: {updateRoleViewModel.ID} is not available.";
                    return View();
                }
                else
                {
                    identityRole.Name = updateRoleViewModel.RoleName;
                    var result = await roleManager.UpdateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("DisplayRoles", "Administrator");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddRemoveUsers(string roleid)
        {
            
            var roleResult = await roleManager.FindByIdAsync(roleid);
            if (roleResult == null)
            {
                ViewBag.Message = $"The role {roleid} is not available.";
                return View("PageNotFound");
            }
            else
            {
                ViewBag.RoleName = roleResult.Name;
                ViewBag.RoleID = roleResult.Id;
                List<UserRoleViewModel> _userRoleViewModel = new List<UserRoleViewModel>();
                var users = userManager.Users.ToList();
                foreach (var user in users)
                {
                    var userRoleViewModel = new UserRoleViewModel()
                    {
                        UserID = user.Id,
                        UserName = user.UserName
                    };
                    var result = await userManager.IsInRoleAsync(user, roleResult.Name);
                    if (result)
                    {
                        userRoleViewModel.IsSelected = true;
                    }
                    _userRoleViewModel.Add(userRoleViewModel);
                }
                return View(_userRoleViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveUsers(List<UserRoleViewModel> userRoleViewModels, string roleid)
        {
            var roleResult = await roleManager.FindByIdAsync(roleid);
            if (roleResult == null)
            {
                ViewBag.Message = $"The role {roleid} is not available.";
                return View("PageNotFound");
            }
            else
            {
                try
                {
                    foreach (var model in userRoleViewModels)
                    {
                        var userResult = await userManager.FindByNameAsync(model.UserName);
                        var result = await userManager.IsInRoleAsync(userResult, roleResult.Name);
                        if (model.IsSelected && !result)
                        {
                            if (!result)
                            {
                                await userManager.AddToRoleAsync(userResult, roleResult.Name);
                            }
                        }
                        else if(!model.IsSelected && result)
                        {

                            await userManager.RemoveFromRoleAsync(userResult, roleResult.Name);
                        }
                    }
                    return RedirectToAction("UpdateRole", "Administrator", new { id = roleResult.Id });
                }
                finally
                {
                    
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userid)
        {
            var user = await userManager.FindByIdAsync(userid);
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            IList<Claim> userClaims = await userManager.GetClaimsAsync(user);
            List<string> claims = new List<string>();
            foreach(var claim in userClaims)
            {
                claims.Add(claim.Type);
            }
            UserUpdateRoleViewModel userUpdateRoleModel = new UserUpdateRoleViewModel()
            {
                UserID = user.Id,
                UserName = user.UserName,
                UserEmail = user.Email,
                UserCity = user.City,
                UserRoles = userRoles,
                UserClaims = claims
            };
            return View(userUpdateRoleModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateRoleViewModel userUpdateRoleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(userUpdateRoleViewModel.UserID);
                if (applicationUser == null)
                {
                    ViewBag.Message = $"The user with the id:{userUpdateRoleViewModel.UserID} is not available.";
                    return View();
                }
                else
                {
                    applicationUser.UserName = userUpdateRoleViewModel.UserName;
                    applicationUser.Email = userUpdateRoleViewModel.UserEmail;
                    applicationUser.City = userUpdateRoleViewModel.UserCity;

                    var result = await userManager.UpdateAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("DisplayUsers", "Administrator");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userid)
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userid);
            if(applicationUser == null)
            {
                ViewBag.Message = $"The user with the id:{userid} is not available.";
                return View();
            }
            else
            {
                var result = await userManager.DeleteAsync(applicationUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("DisplayUsers", "Administrator");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "DeleteRolePolicy")]
        public async Task<IActionResult> DeleteRole(string roleid)
        {
             IdentityRole identityRole = await roleManager.FindByIdAsync(roleid);
            if (identityRole == null)
            {
                ViewBag.Message = $"The user with the id:{roleid} is not available.";
                return View();
            }
            else
            {
                var result = await roleManager.DeleteAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("DisplayUsers", "Administrator");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult PageNotFound(string data)
        {
            ViewBag.Message = data;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddRemoveClaims(string userid)
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userid);
            if (applicationUser == null)
            {
                ViewBag.Message = $"The user with the id:{userid} is not available.";
                return View("PageNotFound");
            }
            else
            {
                var userClaims = await userManager.GetClaimsAsync(applicationUser);
                var model = new UserClaimsViewModel()
                {
                    UserId = userid,
                };
                
                foreach (var claim in ClaimsStore.AllClaims)
                {
                    UserClaim userClaim = new UserClaim();
                    userClaim.ClaimType = claim.Type;
                    if (userClaims.Any(c => c.Type == claim.Type))
                    {
                        userClaim.IsSelected = true;
                    }
                    else
                    {
                        userClaim.IsSelected = false;
                    }
                    model.Claims.Add(userClaim);
                }
                
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveClaims(UserClaimsViewModel userClaimsViewModel)
        {
            ApplicationUser applicationUser = await userManager.FindByIdAsync(userClaimsViewModel.UserId);
            if (applicationUser == null)
            {
                ViewBag.Message = $"The user with the id:{userClaimsViewModel.UserId} is not available.";
                return View("PageNotFound");
            }
            else
            {
                var userClaims = await userManager.GetClaimsAsync(applicationUser);
                var result = await userManager.RemoveClaimsAsync(applicationUser, ClaimsStore.AllClaims);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user claims");
                    return View(userClaimsViewModel);
                }

                result = await userManager.AddClaimsAsync(applicationUser,
                    userClaimsViewModel.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Error in adding claims to user");
                    return View(userClaimsViewModel);
                }

                return RedirectToAction("UpdateUser", "Administrator", new { userid = userClaimsViewModel.UserId });
            }
            return View();
        }
    }
}
