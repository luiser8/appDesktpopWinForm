using ClosedXML.Excel;
using InventoryApp.Data;
using InventoryApp.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace InventoryApp.Utility
{
    public static class ExcelGeneration
    {
        private static readonly AuditManager _auditManager = new AuditManager();

        public static void ExportAndOpen(DataTable dataTable, string worksheetName)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            DateTimeOffset datetime = DateTimeOffset.Now;
            string tempFilePath = $"Reporte_{worksheetName}_{datetime:yyyy-MM-dd}";
            tempFilePath = Path.ChangeExtension(tempFilePath, ".xlsx");

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(worksheetName);
                    worksheet.Cell(1, 1).InsertTable(dataTable);
                    worksheet.Columns().AdjustToContents();

                    var headerRow = worksheet.Row(1);
                    headerRow.Style.Font.Bold = true;
                    headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

                    workbook.SaveAs(tempFilePath);
                }

                Process.Start(new ProcessStartInfo
                {
                    FileName = tempFilePath,
                    UseShellExecute = true
                });
                _auditManager.InsertAudit(new AuditUser
                {
                    UserId = UserSession.SessionUID,
                    Table = worksheetName,
                    Action = "Exportar reporte en Excel",
                    Events = $"Exportar reporte en Excel de la tabla {worksheetName}"
                });
            }
            catch (Exception ex)
            {
                if (File.Exists(tempFilePath))
                {
                    try { File.Delete(tempFilePath); } catch { }
                }
                throw new Exception("Error al generar o abrir el archivo Excel", ex);
            }
        }
    }
}