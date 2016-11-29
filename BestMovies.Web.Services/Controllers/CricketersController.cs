using BestCricketers.Core.BL;
using BestCricketers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BestMovies.Web.Services.Controllers
{
    public class CricketersController : ApiController
    {
        #region
        HttpResponseMessage response;
        CricketerBL cricketerBL;
        # endregion

        # region Public Method  
        ///<summary>  
        /// This method is used to get cricketer list  
        ///</summary>  
        ///<returns></returns>  
        [HttpGet, ActionName("GetCricketerList")]
        public HttpResponseMessage GetCricketerList()
        {
            Result result;
            cricketerBL = new CricketerBL();
            try
            {
                var cricketerList = cricketerBL.GetCricketerList();
                if (!object.Equals(cricketerList, null))
                {
                    response = Request.CreateResponse<List<CricketerProfile>>(HttpStatusCode.OK, cricketerList);
                }
            }
            catch (Exception ex)
            {
                result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return response;
        }



        ///<summary>  
        /// This method is used to get cricketer list by id  
        ///</summary>  
        ///<returns></returns>  
        [HttpGet, ActionName("GetCricketerInfoById")]
        public HttpResponseMessage GetCricketerInfoById(int CricketerId)
        {
            Result result;
            cricketerBL = new CricketerBL();
            try
            {
                var cricketerList = cricketerBL.GetCricketerDetailsById(CricketerId);
                if (!object.Equals(cricketerList, null))
                {
                    response = Request.CreateResponse<List<CricketerProfile>>(HttpStatusCode.OK, cricketerList);
                }
            }
            catch (Exception ex)
            {
                result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return response;
        }



        ///<summary>  
        /// This method is used to add cricketer info in the database.  
        ///</summary>  
        ///<returns></returns>  
        [HttpPost, ActionName("AddCricketerInfo")]
        public HttpResponseMessage AddCricketerInfo(CricketerProfile Cricketer)
        {
            Result ObjResult;
            int result;
            cricketerBL = new CricketerBL();
            try
            {
                result = cricketerBL.AddUpdateCricketerInfo(Cricketer);
                if (result > 0)
                {
                    if (result == 1)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Inserted Successfully!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else if (result == 2)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Already Exists!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Not Added!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResult = new Result();
                ObjResult.Status = 0;
                ObjResult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ObjResult);
            }
            return response;
        }



        ///<summary>  
        /// This method is used to update cricketer info in the database.  
        ///</summary>  
        ///<param name="Cricketer"></param>  
        ///<returns></returns>  
        [HttpPut, ActionName("UpdateCricketerInfo")]
        public HttpResponseMessage UpdateCricketerInfo(CricketerProfile Cricketer)
        {
            Result ObjResult;
            int result;
            cricketerBL = new CricketerBL();
            try
            {
                result = cricketerBL.AddUpdateCricketerInfo(Cricketer);

                if (result > 0)
                {
                    if (result == 3)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Updated Successfully!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else if (result == 2)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record does not Exists!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Not Added!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResult = new Result();
                ObjResult.Status = 0;
                ObjResult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ObjResult);
            }
            return response;
        }


        ///<summary>  
        /// This method is used to delete the cricketer info  
        ///</summary>  
        ///<param name="CricketerId"></param>  
        ///<returns></returns>  
        [HttpDelete, ActionName("DeleteCricketerInfo")]
        public HttpResponseMessage DeleteCricketerInfo(int CricketerId)
        {
            Result ObjResult;
            int result;
            cricketerBL = new CricketerBL();

            try
            {
                CricketerProfile cricketer = new CricketerProfile();
                cricketer.Id = CricketerId;

                result = cricketerBL.DeleteCricketerInfo(cricketer);
                if (result > 0)
                {
                    if (result == 1)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Deleted Successfully!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else if (result == 2)
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record does not Exists!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                    else
                    {
                        ObjResult = new Result();
                        ObjResult.Status = result;
                        ObjResult.Message = "Record Not Found!!";
                        response = Request.CreateResponse<Result>(HttpStatusCode.OK, ObjResult);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResult = new Result();
                ObjResult.Status = 0;
                ObjResult.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ObjResult);
            }
            return response;
        }

        #endregion

    }
}