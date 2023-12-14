using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;
using usermanagment.dbcontext;
using usermanagment.Model;

namespace usermanagment.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class usercontroller : ControllerBase
    {
        private readonly usercontext _usercontext;

        public usercontroller(usercontext usercontext)
        {
            _usercontext = usercontext;
        }

        public record CreateShopperCommand(int Empid, string password);
        public class createproject
        {
            [Required]
            public int ProjectId { get; set; }
           
            [Required]
            public string Name { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public string technologies { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> CheckUser(CreateShopperCommand createShopperCommand)
        {
            try
            {
                var existingUser = await _usercontext.users
                    .FirstOrDefaultAsync(c => c.Empid == createShopperCommand.Empid && c.Password == createShopperCommand.password);

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<project>>> Get()
        {
            try
            {
                var projects = await _usercontext.projects.ToListAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("create project")]

        public async Task<ActionResult> Createproject(createproject createprojects)
        {

            if (createprojects == null)
            {
                return NotFound();
            }
            try
            {
                var createproject = new project
                {
                   
                    Name = createprojects.Name,
                    Description = createprojects.Description,
                    technologies = createprojects.technologies,
                    ProjectId = createprojects.ProjectId,

                };

                _usercontext.projects.Add(createproject); // Assuming projects DbSet is used for projects

                await _usercontext.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
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

        [HttpPut("{id}")]
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
                existingProject.Description = updateUserCommand.Description;
                existingProject.technologies = updateUserCommand.technologies;
                existingProject.Name = updateUserCommand.Name;

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
        [Route("employee")]
        public async Task<ActionResult<IEnumerable<Employee>>> Getall()
        {
            try
            {
                var employees = await _usercontext.Employees.ToListAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id employee}")]
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




    }
}
