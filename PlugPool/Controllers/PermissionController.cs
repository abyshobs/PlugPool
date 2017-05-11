using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using PlugPool.ViewModels.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugPool.Controllers
{
    public class PermissionController : Controller
    {
        private IPermissionDAL permissionDAL;

        public PermissionController(IPermissionDAL permissionDAL)
        {
            this.permissionDAL = permissionDAL;
        }

        public ActionResult Index()
        {
            var permissions = permissionDAL.FetchAll();
            IndexViewModel model = new IndexViewModel(permissions);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateViewModel model = new CreateViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Permission permission = new Permission
                {
                    name = model.name
                };

                var existingPermission = permissionDAL.FetchByName(model.name);
                if (existingPermission != null)
                {
                    TempData["errorMessage"] = "This permission already exists";
                    return RedirectToAction("Index");
                }

                else if (existingPermission == null)
                {
                    permissionDAL.CreatePermission(permission);

                    if (permission != null)
                    {
                        TempData["SuccessMessage"] = "Permission was successfully created";
                    }

                    else
                    {
                        TempData["errorMessage"] = "Error saving Permission";
                    }

                    return RedirectToAction("Index");
                }
            }

            return View();
        }

       
    }
}