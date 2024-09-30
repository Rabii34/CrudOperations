using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment_1.Models;
using Assignment_1.Data;
using Assignment_1.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext context;
        public ProductController(ProductContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetProduct()
        {
            var availablemedicines = context.Products.Include(x => x.Company).ToList();
            return Ok(availablemedicines);
        }
        [HttpPost]
        public IActionResult AddProduct(ProducTDTO productdata)
        {
            if (productdata != null)
            {
                Product addme = new Product()
                {
                    ProductName = productdata.ProductName,
                    Description = productdata.Description,
                    Price = productdata.Price,
                    CompanyId = productdata.CompanyId
                };
                var newaddedproduct = context.Products.Add(addme);
                context.SaveChanges();
                return Ok(newaddedproduct.Entity);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult EditProduct(int ProductId, ProducTDTO productdata)
        {
            if (productdata != null && ProductId != null)
            {
                var product = context.Products.Find(ProductId);
                product.ProductName = productdata.ProductName;
                product.Description = productdata.Description;
                product.Price = productdata.Price;
                var newaddedproduct = context.Products.Update(product);
                context.SaveChanges();
                return Ok(newaddedproduct.Entity);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int Id)
        {
            var product = context.Products.Find(Id);
            if (product != null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }
        //[HttpGet("{id}")]
        //public IActionResult GetProoductDetails(int id)
        //{
        //    var product = context.Products.FirstOrDefault(x => x.ProductId == id);
        //    if (product != null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        return Ok(product);
        //    }
        //}

        [HttpGet("{pageno}/{pagesize}")]
        public IActionResult GetProduct(int pageno, int pagesize)
        {
            int Pageno = pageno;
            int Pagesize = pagesize;
            if (pageno < 1) { Pageno = 1; }
            if (pagesize < 1) { Pagesize = 1; }
            var prod = context.Products.Skip((Pageno - 1) * Pagesize).Take(pagesize).ToList();
            return Ok(prod);
        }
        [HttpGet("{p}")]
        public IActionResult SearchProducts(string p)
        {
            //var product = context.Products.Include(x => x.Company).Where(x => x.ProductName == p || x.Company.Name == p || x.Description == p).ToList();//Exact Match

            var product =
               context.Products.Include(x => x.Company)
               .Where(x => x.ProductName.Contains(p) || x.Company.Name.Contains(p) || x.Description.Contains(p))
               .ToList();//Partial Match

            if (product == null)
            {

                return NotFound();
            }
            else
            {

                return Ok(product);
            }
        }
    }
}
