// FileName: /ClientController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AppBankData.Models;
using System.Configuration; // Tambahkan ini untuk mengakses AppSettings
using System.IO;

namespace AppBankData.Controllers
{
    [AllowAnonymous]
    public class ClientController : Controller
    {
        private readonly ClientContext _objClient = new ClientContext();
        // Tambahkan properti untuk mendapatkan versi agen dari Web.config
        private string LATEST_AGENT_VERSION
        {
            get
            {
                // Pastikan kunci "AgentVersion" ada di Web.config
                return ConfigurationManager.AppSettings["AgentVersion"] ?? "0.0.0"; // Default jika tidak ditemukan
            }
        }

        private string LATEST_UPDATER_VERSION
        {
            get
            {
                // Pastikan kunci "UpdaterVersion" ada di Web.config
                return ConfigurationManager.AppSettings["UpdaterVersion"] ?? "0.0.0";
            }
        }

        public ActionResult Index()
        {
            var data = _objClient.GetAll();
            return View(data);
        }


        [HttpPost]
        public JsonResult ReceiveData(Client client)
        {
            if (client == null)
            {
                Response.StatusCode = 400;
                return Json(new { status = "error", message = "Invalid JSON" }, JsonRequestBehavior.AllowGet);
            }

            client.LastUpdate = DateTime.Now;
            _objClient.Save(client);

            var cmd = _objClient.GetPendingRename(client.ComputerName);

            var responseData = new
            {
                status = "ok",
                renameCommand = cmd != null ? new RenameCommand
                {
                    Id = cmd.Id,
                    NewName = cmd.NewName
                } : null,
                latestAgentVersion = LATEST_AGENT_VERSION
            };

            return Json(responseData, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AckRename(AckCommandRequest request) // <-- Ubah parameter menjadi objek DTO
        {
            // Periksa apakah objek request null atau commandId kosong
            if (request == null || request.CommandId == Guid.Empty)
            {
                // Tambahkan logging untuk debugging
                System.Diagnostics.Debug.WriteLine("AckRename received invalid request or empty Guid.");
                return Json(new { success = false, message = "Invalid commandId" });
            }
            try
            {
                _objClient.AckRename(request.CommandId); // <-- Gunakan request.commandId
                // Tambahkan logging untuk konfirmasi update
                System.Diagnostics.Debug.WriteLine($"AckRename successful for CommandId: {request.CommandId}");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Tambahkan logging error
                System.Diagnostics.Debug.WriteLine($"AckRename failed for CommandId: {request.CommandId}. Error: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendRename(string computerName, string newName)
        {
            if (string.IsNullOrWhiteSpace(computerName) || string.IsNullOrWhiteSpace(newName))
            {
                TempData["Err"] = "ComputerName dan NewName wajib diisi.";
                return RedirectToAction("Index");
            }

            _objClient.EnqueueRename(computerName, newName);
            TempData["Msg"] = $"Perintah rename ke '{newName}' untuk '{computerName}' dikirim.";
            return RedirectToAction("Index");
        }


        public ActionResult DownloadAgent()
        {
            string agentFilePath = Server.MapPath("~/App_Data/client_agent.exe");

            if (!System.IO.File.Exists(agentFilePath))
            {
                Response.StatusCode = 404;
                return Content("Agent file not found on server.");
            }

            return File(agentFilePath, "application/octet-stream", "client_agent.exe");
        }

        public ActionResult DownloadUpdater()
        {
            string updaterFilePath = Server.MapPath("~/App_Data/client_updater.exe");
            if (!System.IO.File.Exists(updaterFilePath))
            {
                Response.StatusCode = 404;
                return Content("Updater file not found on server.");
            }
            return File(updaterFilePath, "application/octet-stream", "client_updater.exe");
        }
    }
}
