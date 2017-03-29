using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class IdentityModels : IdentityDbContext<User>
    {
        // Your context has been configured to use a 'IdentityModels' connection string from your application's
        // configuration file (App.config or Web.config). By default, this connection string targets the
        // 'My.AppStore.Models.IdentityModels' database on your LocalDb instance.
        //
        // If you wish to target a different database and/or database provider, modify the 'IdentityModels'
        // connection string in the application configuration file.
        public IdentityModels()
            : base("name=AppStore")
        {
        }
    }

    public class User : IdentityUser
    {
    }
}