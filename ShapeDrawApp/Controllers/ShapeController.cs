using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using ShapeDrawApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShapeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ShapeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from shape;
             ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShapeDrawAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Shape shape)
        {
            string query = @"
                insert into shape(xaxis, yaxis, height, width) values (@xaxis, @yaxis, @height, @width);
             ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShapeDrawAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@xaxis", shape.xaxis);
                    myCommand.Parameters.AddWithValue("@yaxis", shape.yaxis);
                    myCommand.Parameters.AddWithValue("@height", shape.height);
                    myCommand.Parameters.AddWithValue("@width", shape.width);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added successfully");
        }

        [HttpPut]
        public JsonResult Put(int id, Shape shape)
        {
            string query = @"
                update shape set xaxis = @xaxis, yaxis = @yaxis, height = @height, width = @width where id = @id;
             ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShapeDrawAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myCommand.Parameters.AddWithValue("@xaxis", shape.xaxis);
                    myCommand.Parameters.AddWithValue("@yaxis", shape.yaxis);
                    myCommand.Parameters.AddWithValue("@height", shape.height);
                    myCommand.Parameters.AddWithValue("@width", shape.width);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Update successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from shape where id = @id;
             ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShapeDrawAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted successfully");
        }
    }
}
