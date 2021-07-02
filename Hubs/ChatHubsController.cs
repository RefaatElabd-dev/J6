using AutoMapper;
using J6.DAL.Database;
using J6.DAL.Entities;
using J6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Hubs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatHubsController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly DbContainer _context;

        public ChatHubsController(UserManager<AppUser> userManager, IMapper mapper, DbContainer context)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            _context = context;
        }



        //get all message for specific customer

        // api/ChatHubs/8/1

        [HttpGet("{id}/{sellerid}")]
        public async Task<ActionResult<IEnumerable<Message>>> Getallmessage(int id,int sellerid)
        {
            //id is user id

            var customers = await userManager.GetUsersInRoleAsync("Customer");
           
            var currentuser = customers.SingleOrDefault(a => a.Id==id);

            var messages = _context.Messages.Where(a => a.UserID == currentuser.Id&& a.sellerId==sellerid).OrderBy(a=>a.When);
            return Ok(messages);


        }


        ////////////////////////////////////////////////////////////
        //send message  addmessage

        // api/ChatHubs
         // /8/1

        [HttpPost]
        public async Task<ActionResult> sendmessage(Message mess)
        {
            //id is user id
            Message newmessage = new Message();
            var customers = await userManager.GetUsersInRoleAsync("Customer");

            var currentuser =  customers.SingleOrDefault(a => a.Id == mess.UserID);
            var sellers = await userManager.GetUsersInRoleAsync("Seller");
            var currentseller = sellers.SingleOrDefault(a => a.Id == mess.sellerId);
            newmessage.UserID = currentuser.Id;
            newmessage.UserName = mess.UserName;
            newmessage.sellerId = currentseller.Id ;
            newmessage.Text = mess.Text;

         await  _context.Messages.AddAsync(newmessage);
          await _context.SaveChangesAsync();
            return Ok("added");

        }



        ///////////////////////////////////////////////////////////////////////////////////////////
        // remove all messageses
        // api/ChatHubs/8/1

        [HttpDelete("{id}/{sellerid}")]
        public async Task<ActionResult> deleteallmessage(int id, int sellerid)
        {
            //id is user id

            var customers = await userManager.GetUsersInRoleAsync("Customer");

            var currentuser = customers.SingleOrDefault(a => a.Id == id);

            var messages =await _context.Messages.Where(a => a.UserID == currentuser.Id && a.sellerId == sellerid).ToListAsync();
            _context.Messages.RemoveRange(messages);
            _context.SaveChanges();
            return Ok("all chat deleted");


        }

        ////////////////////////////////////////////////////
        // remove one messageses
        // api/ChatHubs/removemessage/12
        [HttpDelete("{id}")]
        [Route("removemessage/{id}")]
        public async Task<ActionResult> deleteonemessage(int id)
        {
            //id is message id


            var message = await _context.Messages.FindAsync( id);
            _context.Messages.Remove(message);
           await _context.SaveChangesAsync();
            return Ok("one are deleted");


        }
        ////////////////////////////////////////////////////////////////////
        // all seller that specific user call
        // api/ChatHubs/getallcalledseller/8
        [HttpGet("{id}")]
        [Route("getallcalledseller/{id}")]
        public async Task<ActionResult> GetAllCallSeller(int id)
        {
            //id is user id
            List<SellerDto> allCallseller = new List<SellerDto>();
            var Sellers = await userManager.GetUsersInRoleAsync("Seller");
            var all= await  _context.Messages.Where(a => a.UserID == id).ToListAsync();
            foreach (var item in all)
            {
                var seller = Sellers.SingleOrDefault(a => a.Id == item.sellerId);
                if(seller!=null)
                {
                    var SellerToRetuen = mapper.Map<SellerDto>(seller);
                    if (!allCallseller.Contains(SellerToRetuen))
                    {
                        allCallseller.Add(SellerToRetuen);
                        
                    }
                }
            }
       
            return Ok(allCallseller.GroupBy(a=>a.Id).Select(v=>v.First()).ToList());


        }


    }
}
