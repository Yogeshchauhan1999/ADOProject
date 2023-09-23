using Microsoft.AspNetCore.Mvc;
using myADOProject.DataAccess;
using myADOProject.Models;

namespace myADOProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerDA _cstmDA;

        public CustomerController(CustomerDA cstmDA)
        {
            _cstmDA = cstmDA;
        }

        [HttpGet]
        public IActionResult CtmIndex()
        {
            List<Customer> ctmlist = new List<Customer>();
            try
            {
                ctmlist=_cstmDA.GetAll();
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return View(ctmlist);
        }

        [HttpGet]
        public IActionResult Create() { 
            return View();
        }


        [HttpPost]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                bool result=_cstmDA.Insert(model);
                TempData["Success Message"] = "Congratulation! New Customer is Added Successfully";
                return RedirectToAction("CtmIndex");
            }

            else
            {
                TempData["error"] = "Model data is Invalid!";
                return View(model);

            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Customer ctm = _cstmDA.GetbyId(id);
                if (ctm.cid == 0)
                {
                    TempData["error"] = "Customer record not found by id:{id}";
                    return RedirectToAction("CtmIndex");
                }

                return View(ctm);
            }
            catch (Exception ex) {
                TempData["error message"] = ex.Message;
                return View();
             }
            
        }


        [HttpPost]
        public IActionResult Edit(Customer model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    bool result = _cstmDA.Update(model);
                    TempData["Success Message"] = "Customer data is updated Successfully";
                    return RedirectToAction("CtmIndex");
                }

               else
                {
                    TempData["error message"] = "Model data is not valid!";
                    return View();
                }

            }

            catch (Exception ex)
            {
                TempData["error message"] = ex.Message;
                return View();
            }

        }

        //----------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Delete(int id)
        {
           try {
                Customer ctm = _cstmDA.GetbyId(id);
                if (ctm.cid==0)
                {
                    TempData["error message"] = "Can't delete the record";
                    return RedirectToAction("CtmIndex");
                }

                return View(ctm);
            }
            catch(Exception ex)
            {
                TempData["error message"]= ex.Message;
                return View();
            }
            
        }



        [HttpPost]
        public IActionResult Delete(Customer model)
        {
            try
            {
                 bool result = _cstmDA.Delete(model.cid);
                if (result == true)
                {
                    TempData["Success Message"] = "Customer data is Deleted Successfully";
                    return RedirectToAction("CtmIndex");
                }

                else
                {
                    TempData["error message"] = "record data can't be deleted!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error message"] = ex.Message;
                return View();
            }

        }


















    }
}
