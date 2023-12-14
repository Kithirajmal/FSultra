using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using System.Text.Json;
using usermanagment.dbcontext;
using usermanagment.Model;
using static usermanagment.Controllers.usercontroller;

namespace usermanagment.Controllers
{

    [ApiController]
    [Route("api")]
    public class usercontroller : ControllerBase
    {
        private readonly usercontext _usercontext;

        public usercontroller(usercontext usercontext)
        {
            _usercontext = usercontext;
        }

        public record CreateShopperCommand(int Empid, string password);
      
     

        [HttpPost]
        [Route("user")]
        public async Task<ActionResult> CheckUser(CreateShopperCommand createShopperCommand)
        {
            try
            {
                var existingUser = await _usercontext.users
                    .FirstOrDefaultAsync(c => c.empId == createShopperCommand.Empid && c.pwd == createShopperCommand.password);

                if (existingUser == null)
                {
                    return BadRequest("Email or password is incorrect");
                }

                return new OkObjectResult(existingUser);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        public class createproject
        {
            [Required]
            public int ProjectId { get; set; }

            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public  string[] technologies { get; set; }
        }


        [HttpPost]
        [Route("project/createproject")]
        public async Task<ActionResult> CreateProject(createproject createprojects)
        {
            if (createprojects == null)
            {
                return NotFound();
            }

            try
            {
               

                var techString = string.Join(",", createprojects.technologies);


                var createproject = new project
                {
                    name = createprojects.Name,
                    description = createprojects.Description,
                    technologies = techString,
                    projectId = createprojects.ProjectId,
                };

                _usercontext.projects.Add(createproject);

                await _usercontext.SaveChangesAsync();

                
                return new OkObjectResult(createproject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("allproject")]
        public async Task<ActionResult<IEnumerable<project>>> Getalll()
        {
            try
            {
              

                var projects = await _usercontext.projects.ToListAsync();

               
                foreach (var project in projects)
                {
                    
                    var technologiesArray = project.technologies.Split(',');

                    var resultItem = new createproject
                    {
                        Name = project.name,
                        Description = project.description,
                        technologies = technologiesArray,
                        ProjectId = project.projectId,
                        
                    };
                }

                    // Return the array in the response
                    return Ok(projects);

                //return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        [HttpDelete("project/{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            try
            {
                var projectToDelete = await _usercontext.projects.FindAsync(id);

                if (projectToDelete == null)
                {
                    return NotFound(); // 404 Not Found
                }

                _usercontext.projects.Remove(projectToDelete);
                await _usercontext.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        public class UpdateUserCommand
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public string technologies { get; set; }

        }

        [HttpPut("project/{id}")]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserCommand updateUserCommand)
        {
            try
            {
                var existingProject = await _usercontext.projects.FindAsync(id);

                if (existingProject == null)
                {
                    return NotFound(); // 404 Not Found
                }

                // Update project properties based on the provided command
                existingProject.description = updateUserCommand.Description;
                existingProject.technologies = updateUserCommand.technologies;
                existingProject.name = updateUserCommand.Name;

                // No need to create a new UpdateUserCommand, just update the existing project

                await _usercontext.SaveChangesAsync();

                return Ok(); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("allemployee")]
        public async Task<ActionResult> Getall()
        {
            try
            {
                var employeesWithAllocations = await _usercontext.Employees
                    .Include(e => e.resourceallocations).Select( x=> new Createemployee()
                    {
                        dob = x.dob,
                        doj = x.doj,
                        email = x.email,
                        empId = x.empId,
                        
                        isAllocated = x.isAllocated,
                        name = x.name,
                        resource = x.resourceallocations.Select(x=> new Resource
                        {
                            employeeid = x.employeeid,
                            startdate = x.startdate,
                            enddate = x.enddate,
                            iscurrent = x.iscurrent,
                            projectid = x.projectid,
                            projectName = x.projectName

                        }).ToList()

                    })
                    .ToListAsync();

                return Ok(employeesWithAllocations);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("employee/{empId}")]
        public async Task<ActionResult> Deleteemployee(int id)
        {
            try
            {
                var employeeToDelete = await _usercontext.Employees.FindAsync(id);
           

                if (employeeToDelete == null)
                {
                    return NotFound(); // 404 Not Foundjbb
                }

                _usercontext.Employees.Remove(employeeToDelete);
                await _usercontext.SaveChangesAsync();

                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        public class Createemployee
        {
            public string empId { get; set; }
            [Required]
            public string email { get; set; }
            [Required]
            public string name { get; set; }
            [Required]
            public string dob { get; set; }
            [Required]
            public string doj { get; set; }

            public bool isAllocated { get; set; }
            
            public string? primarySkillSet { get; set; }

            public string? secondarySkillSet { get; set; }
            public List <Resource> resource { get; set; }

        }
       

        public class Resource
        {
            public int employeeid { get; set; }
            public int projectid { get; set; }

            public string projectName { get; set; }

            public bool iscurrent { get; set; }

            public string startdate { get; set; }

            public string enddate { get; set; }
        }

        [HttpPost]
        [Route("employee/createEmployee")]

        public async Task<ActionResult> Createemployees(Createemployee createemployee)
        {

            if (createemployee == null)
            {
                return NotFound();
            }
            try
            {
                //if (createemployee.isAllocated == null)
                //{
                //    var nonAllocatedEmployee = new NonAllocatedEmployee
                //    {
                //        name = createemployee.name,
                //        empId = createemployee.empId,
                //        email = createemployee.email,
                //        dob = createemployee.dob,
                //        doj = createemployee.doj,
                //        primarySkillSet = createemployee.primarySkillSet,
                //        secondarySkillSet = createemployee.secondarySkillSet,
                        
                //    };
                //    _usercontext.nonAllocatedEmployees.Add(nonAllocatedEmployee);
                //}
                //else
                //{
                //    var allocatedEmployee = new AllocatedEmployee
                //    {
                //        name = createemployee.name,
                //        empId = createemployee.empId,
                //        email = createemployee.email,
                //        dob = createemployee.dob,
                //        doj = createemployee.doj,
                //        primarySkillSet = createemployee.primarySkillSet,
                //        secondarySkillSet = createemployee.secondarySkillSet,
                       
                //    };
                //    _usercontext.AllocatedEmployees.Add(allocatedEmployee);
                //}

                    var emp = new Employee
                {
                    name = createemployee.name, 
                    empId = createemployee.empId,
                    email = createemployee.email,
                    dob = createemployee.dob,
                    doj = createemployee.doj,
                    primarySkillSet =createemployee.primarySkillSet,
                    secondarySkillSet =createemployee.secondarySkillSet,
                    isAllocated = createemployee.isAllocated,
                };
                if (emp.isAllocated && createemployee.resource != null) 
                {
                    foreach( var resource in createemployee.resource)
                    {
                        emp.resourceallocations.Add(new Resourceallocation
                        {
                            //employeeid = createemployee.empId,
                            projectName = resource.projectName,
                            projectid = resource.projectid,
                            startdate = resource.startdate,
                            enddate = resource.enddate,
                            iscurrent = resource.iscurrent,
                        });
                    }

                    

                }

                _usercontext.Employees.Add(emp); // Assuming projects DbSet is used for projects

                await _usercontext.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpPut]
        [Route("updateEmployee/{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, Createemployee createemployee)
            {
            if (createemployee == null)
            {
                return NotFound();
            }

            try
            {
                var existingEmployee = await _usercontext.Employees.FindAsync(id);

                if (existingEmployee == null)
                {
                    return NotFound($"Employee with ID {id} not found");
                }

                // Update properties of the existing employee
                existingEmployee.name = createemployee.name;
                existingEmployee.email = createemployee.email;
                existingEmployee.dob = createemployee.dob;
                existingEmployee.doj = createemployee.doj;
                existingEmployee.primarySkillSet = createemployee.primarySkillSet;
                existingEmployee.secondarySkillSet = createemployee.secondarySkillSet;
                existingEmployee.isAllocated = createemployee.isAllocated;

                // Clear existing resource allocations
                existingEmployee.resourceallocations.Clear();

                // Add new resource allocations if isAllocated is true
                if (existingEmployee.isAllocated && createemployee.resource != null)
                {
                    foreach (var resource in createemployee.resource)
                    {
                        existingEmployee.resourceallocations.Add(new Resourceallocation
                        {
                            //employeeid = createemployee.empId,
                            projectName = resource.projectName,
                            projectid = resource.projectid,
                            startdate = resource.startdate,
                            enddate = resource.enddate,
                            iscurrent = resource.iscurrent,
                        });
                    }
                }

                await _usercontext.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("projecthistroy")]

        public async Task<ActionResult<IEnumerable<project>>> Getprojecthistory()
        {
            try
            {
                var projects = await _usercontext.Resourceallocations.ToListAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("projecthistory/{projectid}")]
        public async Task<ActionResult<IEnumerable<project>>> GetProjectHistory(int projectid)
        {
            try
            {
                var projects = await _usercontext.Resourceallocations
                    .Where(ra => ra.projectid == projectid)
                    .Select(ra => ra.projectName) // Assuming Project is the navigation property in Resourceallocation
                    .Distinct()
                    .ToListAsync();
                    
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        //[HttpGet]
        //[Route("Allocated")]

        //public async Task<ActionResult> GetallocatedResourse()
        //{
        //    var existingCorporate = await _usercontext.Employees.Where(c => c.isAllocated).ToListAsync();
        //    if (existingCorporate == null)
        //    {
                

        //    }

          
        //        return Ok();
        //    }
        
        







    }
}
