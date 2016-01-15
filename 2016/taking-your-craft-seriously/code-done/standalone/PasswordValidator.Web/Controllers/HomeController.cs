using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasswordValidator.Web.Controllers
{
  public class Result
  {
    public string Style { get; set; }
    public string Text { get; set; }
    public string Input { get; set; }
  }

  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var pwd = Request.QueryString["pwd"];
      var res = new Result { Style = "info", Text = "<strong>Hello!</strong> " +
        "Enter a password and click the button to get started.", Input = pwd };
      if (pwd != null)
      {
        var req =
          new PowerValidator(new[] {
            Validators.AsciiLowerRequirement,
            Validators.AsciiUpperRequirement,
            Validators.LengthRequirement
            });
        if (req.IsSatisfied(pwd))
          res = new Result { Style = "success", Text = "<strong>Success!</strong> " +
            "This password satisfies the sample conditions.", Input = pwd };
        else
          res = new Result { Style = "danger", Text = "<strong>Error!</strong> " +
            "The password does not satisfy the requirements.", Input = pwd };
      }
      return View(res);
    }
  }
}