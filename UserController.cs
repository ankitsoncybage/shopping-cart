using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ShoppingCart.DTO;
using ShoppingCart.Model;
using ShoppingCart.Repository;
using Serilog;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepo _repo;

        public UserController(IUserRepo repo)
        {
            this._repo = repo;

        }

        [HttpPost]
        [Route("user_signup")]
        public async Task<ActionResult> SignUp(User user)
        {
            Log.Information("user_signup method is called");
            int res = await _repo.SignUp(user);
            if (res > 0)
            {
                return Ok(
                    new ResponseDto
                    {
                        status = "success",
                        Message = "User Added Successfully"
                    }
                );
            }
            else
                return Ok(
                        new ResponseDto
                        {
                            status = "Failed",
                            Message = "User Added Failed"
                        }
                        );

        }

        [HttpPost]
        [Route("user_signin")]
        public async Task<ActionResult> SignIn(LoginDto dto)
        {
            Log.Information("login method called");
            var res = await _repo.SignIn(dto);
            if (res != null)
            {
                return Ok(
                    new ResponseGeneric<User>
                    {
                        Status = "success",
                        Data = res
                    }
                );
            }
            else
                return Ok(
                        new ResponseDto
                        {
                            status = "Failed",
                            Message = "User Signin Failed"
                        }
                        );

        }

        [HttpPut]
        [Route("user_update")]
        public async Task<ActionResult> UpdateUser(UpdateDto updatedto)
        {
            Log.Information("udpate user method called ");
            int res = await _repo.Updateuser(updatedto);
            if (res > 0)
            {
                return Ok(
                    new ResponseDto
                    {
                        status = "success",
                        Message = "User Updated Successfully"
                    }
                );
            }
            else
                return Ok(
                        new ResponseDto
                        {
                            status = "Failed",
                            Message = "User Updatation Failed"
                        }
                        );

        }

        [HttpPost]
        [Route("user_getproduct")]
        public async Task<ActionResult> GetProudctByID(int id)
        {
            var res = await _repo.GetProductById(id);
            if (res != null)
            {
                return Ok(
                    new ResponseDto
                    {
                        status = "success",
                        Message = "Fetch Product Successfully"
                    }
                );
            }
            else
                return Ok(
                        new ResponseDto
                        {
                            status = "Failed",
                            Message = "Fetch Product Failed"
                        }
                        );

        }
        //[HttpPost]
        //[Route("email")]
        //public async Task<ActionResult> emailmessage()
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Shopping Cart", "ankitson@cybage.com"));
        //    message.To.Add(new MailboxAddress("ankit", "soniank@gmail.com"));
        //    message.Subject = "Shopping Cart";
        //    message.Body = new TextPart("Thank You")
        //    {
        //        Text = "Hello from shopping cart"
        //    };
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //    return Ok();

        //}


        //[HttpPost]
        //[Route("email")]
        //public void emailsend()
        //{
        //    var client = new SmtpClient("smtp.gmail.com", 61724)
        //    {
        //        Credentials = new NetworkCredential("ankitson@cybage.com", "Cybageank@123*"),
        //        EnableSsl = true
        //    };
        //    client.Send("ankitson@cybage.com", "soniank102@gmail.com", "Welcome to Shopping Cart", "Thankyou for become part of our family!!");
        //    Console.WriteLine("Sent");
        //    Console.ReadLine();

        //    }



        //}

        [HttpGet]
        [Route("productbycategory")]
        public ActionResult GetProudctByCategory([FromQuery] string category)
        {
            var res = _repo.GetProductByCategory(category);
            if (res != null)
            {
                return Ok(
                    new ResponseGeneric<IEnumerable<Product>>
                    {
                        Status = "Success",
                        Data = res
                    });

            }

            else
            {
                return Ok(
                     new ResponseDto
                     {
                         status = "Failed",
                         Message = "category"
                     });
            }


        }
    }
}