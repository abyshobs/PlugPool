using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.DAL;
using PlugPool.Domain.Code;
using PlugPool.Domain.Code.Interfaces;

namespace PlugPool
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<IAccountDAL, accountDAL>();
        container.RegisterType<IProfileDAL, ProfileDAL>();
        container.RegisterType<IJobDAL, JobDAL>();
        container.RegisterType<IPermissionDAL, PermissionDAL>();
        container.RegisterType<IAccountPermissionDAL, AccountPermissionDAL>();
        container.RegisterType<IUserSession, UserSession>();
        container.RegisterType<ISessionWrapper, SessionWrapper>();
        container.RegisterType<IEmail, Email>();
        container.RegisterType<IConfiguration, Configuration>();
        container.RegisterType<IProfileImageDAL, ProfileImageDAL>();
        container.RegisterType<INoteDAL, NoteDAL>();
    }
  }
}