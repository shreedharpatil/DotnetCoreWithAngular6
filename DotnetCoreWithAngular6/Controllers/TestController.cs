using Data.Repository.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreWithAngular6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly AemContext dbContext;

        public TestController(AemContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            try
            {
                return this.Ok(this.dbContext.Login.ToList());
            }
            catch(Exception ex)
            {
                return this.Ok(ex);
            }
        }

        [HttpGet]
        [Route("get1")]
        public IActionResult Get1()
        {
            try
            {
                var optionbuilder = new DbContextOptionsBuilder<AemContext>().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=DB\\AEM.mdf;Connect Timeout=30");
                var context = new AemContext(optionbuilder.Options);
                return this.Ok(context.Login.ToList());
            }
            catch (Exception ex)
            {
                return this.Ok(ex);
            }
        }

        [HttpGet]
        [Route("get2")]
        public IActionResult Get2()
        {
            try
            {
                var optionbuilder = new DbContextOptionsBuilder<AemContext>().UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\home\\site\\wwwroot\\DB\\AEM.mdf;Connect Timeout=30");
                var context = new AemContext(optionbuilder.Options);
                return this.Ok(context.Login.ToList());
            }
            catch (Exception ex)
            {
                return this.Ok(ex);
            }
        }

        [HttpGet]
        [Route("get3")]
        public IActionResult Get3()
        {
            try
            {
                var optionbuilder = new DbContextOptionsBuilder<AemContext>().UseSqlServer(System.IO.File.ReadAllText("Cn.txt"));
                var context = new AemContext(optionbuilder.Options);
                return this.Ok(context.Login.ToList());
            }
            catch (Exception ex)
            {
                return this.Ok(ex);
            }
        }
    }
}
