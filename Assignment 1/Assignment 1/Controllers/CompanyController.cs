using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment_1.Models;
using Assignment_1.Data;
using Assignment_1.Models.DTO;

namespace Assignment_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ProductContext db;
        public CompanyController(ProductContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(db.Companies.ToList());
        }
        [HttpPost]
        public IActionResult AddCompany(CompanyDTO companydata)
        {
            if (companydata != null)
            {
                var company = new Company()
                {
                    Name = companydata.Name,
                    Address = companydata.Address,
                    Cell = companydata.Cell,
                };
                var newaddedcompany = db.Companies.Add(company);
                db.SaveChanges();
                return Ok(newaddedcompany.Entity);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult EditCompany(int CompanyId, CompanyDTO companydata)
        {
            if (companydata != null && CompanyId != null)
            {
                var company = db.Companies.Find(CompanyId);
                company.Name = companydata.Name;
                company.Address = companydata.Address;
                company.Cell = companydata.Cell;
                var newaddedcompany = db.Companies.Update(company);
                db.SaveChanges();
                return Ok(newaddedcompany.Entity);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult DeleteCompany(int Id)
        {
            var company = db.Companies.Find(Id);
            if (company is null)
            {
                return NotFound();
            }
            db.Companies.Remove(company);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult GetCompanyDetails(int id)
        {
            var company = db.Companies.FirstOrDefault(x => x.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(company);
            }
        }
        [HttpGet("{pageno}/{pagesize}")]
        public IActionResult GetCompany(int pageno, int pagesize)
        {
            int Pageno = pageno;
            int Pagesize = pagesize;
            if (pageno < 1) { Pageno = 1; }
            if (pagesize < 1) { Pagesize = 1; }
            var comp = db.Companies.Skip((Pageno - 1) * Pagesize).Take(pagesize).ToList();
            if (comp == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest(comp);
            }
        }
    }
}
