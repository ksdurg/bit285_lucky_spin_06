using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinDataContext _dbc;
        private LuckListViewModel _llvm;
        Random random;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinDataContext luckyspin )
        {
            random = new Random();
            //TODO: Inject the LuckySpinDataContext
            _dbc = luckyspin;
        }

        /***
         * Entry Page Action
         **/

        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            // TODO: Add the Player to the DB and save the changes
            _dbc.Players.Add(player);
            _dbc.SaveChanges();

            // TODO: BONUS: Build a new SpinItViewModel object with data from the Player and pass it to the View
            SpinViewModel svm = new SpinViewModel();
            svm.Balance = player.Balance;
            svm.Luck = player.Luck;
            svm.


            return RedirectToAction("SpinIt");
        }
    }

        /***
         * Spin Action
         **/  
               
         public IActionResult SpinIt()
        {
            Spin spin = new Spin
            {
                //Luck = player.Luck,
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10)
            };

            spin.IsWinning = (spin.A == spin.Luck || spin.B == spin.Luck || spin.C == spin.Luck);

            //Add to Spin Repository
            //repository.AddSpin(spin);

            //Prepare the View
            if(spin.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";

            //ViewBag.FirstName = player.FirstName;

            return View("SpinIt", spin);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList()
        {
                return View();
        }

    }
}

